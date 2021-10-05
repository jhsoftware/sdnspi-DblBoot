using System;
using System.Collections.Generic;
using JHSoftware.SimpleDNS.Plugin;
using JHSoftware.SimpleDNS;
using System.Threading.Tasks;

namespace dbl_boot
{
  public class DblPlugin : IGetHostPlugIn
  {
    internal const string MyTitle = "Domain blacklist BOOT";

    private MyConfig MyCfg;
    private JHSoftware.SimpleDNS.SdnsIP MyCfgIPv4, MyCfgIPv6;
    Dictionary<DomName, object> Domains;

    private System.IO.FileSystemWatcher fMon;
    private DateTime LastReload;

    private IHost _Host;
    public IHost Host { get => _Host; set => _Host = value; }

    public IPlugInBase.PlugInTypeInfo GetPlugInTypeInfo()
    {
      var x = new IPlugInBase.PlugInTypeInfo();
      x.Name = MyTitle;
      x.Description = @"Blocks domain names based on a BOOT file";
      x.InfoURL = "https://simpledns.plus/kb/172/domain-blacklist-boot-plug-in";
      return x;
    }

    public void LoadConfig(string config, Guid instanceID, string dataPath)
    {
      MyCfg = MyConfig.DeSerialize(config);
      if (!string.IsNullOrEmpty(MyCfg.IPv4)) MyCfgIPv4 = SdnsIP.Parse(MyCfg.IPv4);
      if (!string.IsNullOrEmpty(MyCfg.IPv6)) MyCfgIPv6 = SdnsIP.Parse(MyCfg.IPv6);
    }

    public Task<IGetHostPlugIn.Result<SdnsIP>> Lookup(IDNSRequest req)
    {
      return Task.FromResult(Lookup2(req));
    }
    private IGetHostPlugIn.Result<SdnsIP> Lookup2(IDNSRequest req)
    {
      if (req.QType == DNSRecType.A && MyCfgIPv4 !=null && Match(req.QName)) return new IGetHostPlugIn.Result<SdnsIP> { Value = MyCfgIPv4, TTL = MyCfg.TTL };
      if (req.QType == DNSRecType.AAAA && MyCfgIPv6 != null && Match(req.QName)) return new IGetHostPlugIn.Result<SdnsIP> { Value = MyCfgIPv6, TTL = MyCfg.TTL };
      return null;
    }

    public Task<IGetHostPlugIn.Result<string>> LookupTXT(IDNSRequest req)
    {
      return Task.FromResult(LookupTXT2(req));
    }
    public IGetHostPlugIn.Result<string> LookupTXT2(IDNSRequest req)
    {
      if (string.IsNullOrEmpty(MyCfg.TXT) || !Match(req.QName)) return null;
      return new IGetHostPlugIn.Result<string> { Value = MyCfg.TXT, TTL = MyCfg.TTL };
    }

    private bool Match(DomName d)
    {
      while (d.SegmentCount() > 0)
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

    Task IPlugInBase.StartService()
    {
      LoadData();
      if (MyCfg.Monitor)
      {
        fMon = new System.IO.FileSystemWatcher();
        fMon.Path = System.IO.Path.GetDirectoryName(MyCfg.DataFile);
        fMon.Filter = System.IO.Path.GetFileName(MyCfg.DataFile);
        fMon.IncludeSubdirectories = false;
        fMon.NotifyFilter = System.IO.NotifyFilters.LastWrite;
        fMon.EnableRaisingEvents = true;
        fMon.Changed += fMon_Changed;
      }
      return Task.CompletedTask;
    }

    private void LoadData()
    {
      try
      {
        Domains = new Dictionary<DomName, object>();

        var sr = new System.IO.StreamReader(MyCfg.DataFile, true);
        string x;
        int i;
        DomName d = DomName.Root;
        char[] ws = { ' ', '\t' };

        while (!sr.EndOfStream)
        {
          x = sr.ReadLine();
          if (!x.StartsWith(@"PRIMARY", StringComparison.InvariantCultureIgnoreCase)) continue;
          x = x.Substring(7).TrimStart();
          i = x.IndexOfAny(ws);
          if (i < 0) continue;
          x = x.Substring(0, i).ToLowerInvariant();
          if (!DomName.TryParse(x, ref d)) continue;
          if (Domains.ContainsKey(d)) continue;
          Domains.Add(d, null);
        }
        sr.Close();
        LastReload = DateTime.UtcNow;
        Host.LogLine("Loaded " + Domains.Count.ToString() + " domains");
      }
      catch (Exception ex)
      {
        Domains = new Dictionary<DomName, object>();
        Host.AsyncError(ex);
      }
    }

    void fMon_Changed(object sender, System.IO.FileSystemEventArgs e)
    {
      if (DateTime.UtcNow.Subtract(LastReload).TotalSeconds < 5) return;
      Host.LogLine(@"Data file update detected - reloading");
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

    public Task<IGetHostPlugIn.Result<DomName>> LookupReverse(SdnsIP ip, IDNSRequest req)
    {
      return Task.FromResult<IGetHostPlugIn.Result<DomName>>(null);
    }

    public void LoadState(string state)
    {
      return;
    }

    public string SaveState()
    {
      return null;
    }

    Task<object> IPlugInBase.Signal(int code, object data)
    {
      return Task.FromResult<object>(null);
    }
  }

}
