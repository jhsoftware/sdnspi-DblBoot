using System;
using System.Collections.Generic;
using JHSoftware.SimpleDNS.Plugin;

namespace dbl_boot
{
    public class DblPlugin : IGetHostPlugIn
    {
        internal const string MyTitle = "Domain blacklist BOOT";

        private MyConfig MyCfg;
        private IPAddress MyCfgIPv4, MyCfgIPv6;
        Dictionary<DomainName, object> Domains;
        private DNSRRType RecTypeA = DNSRRType.Parse("A");

        private System.IO.FileSystemWatcher fMon;
        private DateTime LastReload;

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
            LoadData();

            if (MyCfg.Monitor) { 
              fMon = new System.IO.FileSystemWatcher();
              fMon.Path = System.IO.Path.GetDirectoryName(MyCfg.DataFile);
              fMon.Filter = System.IO.Path.GetFileName(MyCfg.DataFile);
              fMon.IncludeSubdirectories = false;
              fMon.NotifyFilter = System.IO.NotifyFilters.LastWrite;
              fMon.EnableRaisingEvents = true;
              fMon.Changed += fMon_Changed;
            }
        }

        private void LoadData()
        {
            try
            {
                Domains = new Dictionary<DomainName, object>();

                var sr = new System.IO.StreamReader(MyCfg.DataFile, true);
                string x;
                int i;
                DomainName d = null;
                char[] ws = { ' ', '\t' };

                while (!sr.EndOfStream)
                {
                    x = sr.ReadLine();
                    if (!x.StartsWith(@"PRIMARY", StringComparison.InvariantCultureIgnoreCase)) continue;
                    x = x.Substring(7).TrimStart();
                    i = x.IndexOfAny(ws);
                    if (i < 0) continue;
                    x = x.Substring(0, i).ToLowerInvariant();
                    if (!DomainName.TryParse(x, ref d)) continue;
                    if (Domains.ContainsKey(d)) continue;
                    Domains.Add(d, null);
                }
                sr.Close();
                LastReload = DateTime.UtcNow;
                LogLine.Invoke("Loaded " + Domains.Count.ToString() + " domains");
            }
            catch (Exception ex)
            {
                Domains=new Dictionary<DomainName, object>();
                AsyncError.Invoke(ex);
            }
        }

        void fMon_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            if (DateTime.UtcNow.Subtract(LastReload).TotalSeconds < 5) return;
            LogLine.Invoke(@"Data file update detected - reloading");
            LoadData();
        }

        public void StopService()
        {
            if (MyCfg.Monitor)
            {
                fMon.EnableRaisingEvents = false;
                fMon.Changed -= fMon_Changed;
                fMon = null;
            }
            Domains = null;
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
