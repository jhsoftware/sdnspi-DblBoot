using System;
using System.Collections.Generic;
using JHSoftware.SimpleDNS.Plugin;

namespace dbl_boot
{
    public class DblPlugin : IGetHostPlugIn
    {
        internal const string MyTitle = "Domain block list - BOOT";

        private MyConfig MyCfg;
        private IPAddress MyCfgIPv4, MyCfgIPv6;
        Dictionary<DomainName, object> Domains;
        private DNSRRType RecTypeA = DNSRRType.Parse("A");

        public event IPlugInBase.LogLineEventHandler LogLine;
        public event IPlugInBase.SaveConfigEventHandler SaveConfig;
        public event IPlugInBase.AsyncErrorEventHandler AsyncError;


        public IPlugInBase.PlugInTypeInfo GetPlugInTypeInfo()
        {
            var x = new IPlugInBase.PlugInTypeInfo();
            x.Name =MyTitle ;
            x.Description = @"Blocks domain names based on a BOOT file";
            x.InfoURL = null;
            x.ConfigFile = false;
            x.MultiThreaded = false;
            return x;
        }

        public DNSAskAboutGH GetDNSAskAbout()
        {
            var x=new DNSAskAboutGH();
            x.Domain = null;
            x.ForwardIPv4 = !string.IsNullOrEmpty(MyCfg.IPv4);
            x.ForwardIPv6 = !string.IsNullOrEmpty(MyCfg.IPv6);
            x.TXT = !string.IsNullOrEmpty(MyCfg.TXT);
            x.RevIPv4Addr = null;
            x.RevIPv6Addr = null;
            return x;
        }

        public void LoadConfig(string config, Guid instanceID, string dataPath, ref int maxThreads)
        {
            MyCfg = MyConfig.DeSerialize(config);
            if (!string.IsNullOrEmpty(MyCfg.IPv4)) MyCfgIPv4 = IPAddress.Parse(MyCfg.IPv4);
            if (!string.IsNullOrEmpty(MyCfg.IPv6)) MyCfgIPv6 = IPAddress.Parse(MyCfg.IPv6);
        }

        public void Lookup(IDNSRequest req, ref IPAddress resultIP, ref int resultTTL)
        {
            if (!Match(req.QName)) { resultIP = null; return; }
            if (req.QType == RecTypeA) { resultIP = MyCfgIPv4; } else { resultIP = MyCfgIPv6; }
            resultTTL = MyCfg.TTL;
        }

        public void LookupTXT(IDNSRequest req, ref string resultText, ref int resultTTL)
        {
            if (!Match(req.QName)) { resultText = null; return; }
            resultText = MyCfg.TXT;
            resultTTL = MyCfg.TTL;
        }

        private bool Match(DomainName d)
        {
            while (d.SegmentCount()>0) 
            {
                if (Domains.ContainsKey(d)) return true;
                d = d.Parent();
            }
            return false;
        }

        public OptionsUI GetOptionsUI(Guid instanceID, string dataPath)
        {
            return new dbl_boot.MyOptionsUI();
        }

        public bool InstanceConflict(string config1, string config2, ref string errorMsg)
        {
            return false;
        }

        public void StartService()
        {
            Domains = new Dictionary<DomainName,object>();

            var sr = new System.IO.StreamReader(MyCfg.DataFile, true);
            string x;
            int i;
            DomainName d=null;
            char[] ws = {' ', '\t'};

            while (!sr.EndOfStream)
            {
                x = sr.ReadLine();
                if (!x.StartsWith(@"PRIMARY", StringComparison.InvariantCultureIgnoreCase)) continue;
                x = x.Substring(7).TrimStart();
                i = x.IndexOfAny(ws);
                if (i < 0) continue;
                x = x.Substring(0, i).ToLowerInvariant();
                if(! DomainName.TryParse(x,ref d)) continue;
                if(Domains.ContainsKey(d)) continue;
                Domains.Add(d,null);
            }
            sr.Close();
            LogLine.Invoke("Loaded " + Domains.Count.ToString() + " domains");
        }

        public void StopService()
        {
            return;
        }

        public void LookupReverse(IDNSRequest req, ref DomainName resultName, ref int resultTTL)
        {
            throw new NotImplementedException();
        }

        public void LoadState(string state)
        {
            return;
        }

        public string SaveState()
        {
            return null;
        }
    
    }
   
}
