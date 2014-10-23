using System;
using System.Windows.Forms;

namespace dbl_boot
{
    public partial class MyOptionsUI : JHSoftware.SimpleDNS.Plugin.OptionsUI
    {
        public MyOptionsUI()
        {
            InitializeComponent();
        }

        public override void LoadData(string config)
        {
            if (string.IsNullOrEmpty(config)) return;
            MyConfig cfg = MyConfig.DeSerialize(config);
            txtDataFile.Text = cfg.DataFile;
            chkMonitor.Checked = cfg.Monitor;
            chkA.Checked = !string.IsNullOrEmpty(cfg.IPv4);
            txtIPv4.Text = cfg.IPv4;
            chkAAAA.Checked = !string.IsNullOrEmpty(cfg.IPv6);
            txtIPv6.Text = cfg.IPv6;
            chkTXT.Checked = !string.IsNullOrEmpty(cfg.TXT);
            txtTXT.Text = cfg.TXT;
            txtTTL.Text = cfg.TTL.ToString();
        }

        public override string SaveData()
        {
            MyConfig cfg = new MyConfig();
            cfg.DataFile = txtDataFile.Text.Trim();
            cfg.Monitor = chkMonitor.Checked;
            cfg.IPv4 = chkA.Checked ? txtIPv4.Text.Trim() :"" ;
            cfg.IPv6 =chkAAAA.Checked ? txtIPv6.Text.Trim() : "";
            cfg.TXT =chkTXT.Checked ? txtTXT.Text.Trim(): "";
            cfg.TTL = int.Parse(txtTTL.Text.Trim());
            return cfg.Serialize();
        }

        public override bool ValidateData()
        {
            if (txtDataFile.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Data file value is empty!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (chkA.Checked)
            {
                if (txtIPv4.Text.Trim().Length==0)
                {
                    MessageBox.Show(this, "IPv4 address is empty under A-records!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                System.Net.IPAddress ip;
                if (!System.Net.IPAddress.TryParse( txtIPv4.Text.Trim(),out ip) || ip.AddressFamily!= System.Net.Sockets.AddressFamily.InterNetwork )
                {
                    MessageBox.Show(this, "Invalid IPv4 address under A-records!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (chkAAAA.Checked)
            {
                if (txtIPv6.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "IPv6 address is empty under AAAA / A6-records!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                System.Net.IPAddress ip;
                if (!System.Net.IPAddress.TryParse(txtIPv6.Text.Trim(), out ip) || ip.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    MessageBox.Show(this, "Invalid IPv6 address under AAAA / A6-records!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (chkTXT.Checked)
            {
                string x=txtTXT.Text.Trim();
                if (x.Length==0)
                {
                    MessageBox.Show(this, "TXT-record data is empty!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                for (int i = 0; i < x.Length; i++)
                {
                    if(x[i] <32 || x[i]>126 )
                    {
                        MessageBox.Show(this, "Invalid characters in TXT-record data (only ASCII)!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            if(!chkA.Checked && !chkAAAA.Checked && !chkTXT.Checked)
            {
                MessageBox.Show(this, "At least one DNS record type must be selected!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int ttl;
            if(!int.TryParse(txtTTL.Text.Trim(),out ttl) || ttl<0)
            {
                MessageBox.Show(this, "Invalid TTL value!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void chkA_CheckedChanged(object sender, EventArgs e)
        {
            lblIPv4.Enabled = chkA.Checked;
            txtIPv4.Enabled = chkA.Checked;
        }

        private void chkAAAA_CheckedChanged(object sender, EventArgs e)
        {
            lblIPv6.Enabled = chkAAAA.Checked;
            txtIPv6.Enabled = chkAAAA.Checked;
        }

        private void chkTXT_CheckedChanged(object sender, EventArgs e)
        {
            lblTXT.Enabled = chkTXT.Checked;
            txtTXT.Enabled = chkTXT.Checked;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.RemoteGUI)
            {
                MessageBox.Show(this, "Cannot browse remote file system!", DblPlugin.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult res=ofDlg.ShowDialog();
            if (res != DialogResult.OK) return;

            txtDataFile.Text = ofDlg.FileName; 
        }
    }
}
