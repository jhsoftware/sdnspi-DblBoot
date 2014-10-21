namespace dbl_boot
{
    partial class MyOptionsUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblIPv4 = new System.Windows.Forms.Label();
            this.lblIPv6 = new System.Windows.Forms.Label();
            this.txtIPv4 = new System.Windows.Forms.TextBox();
            this.txtIPv6 = new System.Windows.Forms.TextBox();
            this.lblTXT = new System.Windows.Forms.Label();
            this.chkA = new System.Windows.Forms.CheckBox();
            this.chkAAAA = new System.Windows.Forms.CheckBox();
            this.chkTXT = new System.Windows.Forms.CheckBox();
            this.txtTXT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTTL = new System.Windows.Forms.TextBox();
            this.ofDlg = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data file (BOOT file format):";
            // 
            // txtDataFile
            // 
            this.txtDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataFile.Location = new System.Drawing.Point(0, 16);
            this.txtDataFile.Name = "txtDataFile";
            this.txtDataFile.Size = new System.Drawing.Size(345, 20);
            this.txtDataFile.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(351, 14);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(27, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.toolTip1.SetToolTip(this.btnBrowse, "Browse...");
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblIPv4
            // 
            this.lblIPv4.AutoSize = true;
            this.lblIPv4.Enabled = false;
            this.lblIPv4.Location = new System.Drawing.Point(20, 98);
            this.lblIPv4.Margin = new System.Windows.Forms.Padding(20, 5, 3, 0);
            this.lblIPv4.Name = "lblIPv4";
            this.lblIPv4.Size = new System.Drawing.Size(72, 13);
            this.lblIPv4.TabIndex = 5;
            this.lblIPv4.Text = "IPv4 address:";
            // 
            // lblIPv6
            // 
            this.lblIPv6.AutoSize = true;
            this.lblIPv6.Enabled = false;
            this.lblIPv6.Location = new System.Drawing.Point(20, 154);
            this.lblIPv6.Margin = new System.Windows.Forms.Padding(20, 5, 3, 0);
            this.lblIPv6.Name = "lblIPv6";
            this.lblIPv6.Size = new System.Drawing.Size(72, 13);
            this.lblIPv6.TabIndex = 8;
            this.lblIPv6.Text = "IPv6 address:";
            // 
            // txtIPv4
            // 
            this.txtIPv4.Enabled = false;
            this.txtIPv4.Location = new System.Drawing.Point(98, 95);
            this.txtIPv4.Name = "txtIPv4";
            this.txtIPv4.Size = new System.Drawing.Size(125, 20);
            this.txtIPv4.TabIndex = 6;
            // 
            // txtIPv6
            // 
            this.txtIPv6.Enabled = false;
            this.txtIPv6.Location = new System.Drawing.Point(98, 151);
            this.txtIPv6.Name = "txtIPv6";
            this.txtIPv6.Size = new System.Drawing.Size(225, 20);
            this.txtIPv6.TabIndex = 9;
            // 
            // lblTXT
            // 
            this.lblTXT.AutoSize = true;
            this.lblTXT.Enabled = false;
            this.lblTXT.Location = new System.Drawing.Point(20, 212);
            this.lblTXT.Margin = new System.Windows.Forms.Padding(20, 5, 3, 0);
            this.lblTXT.Name = "lblTXT";
            this.lblTXT.Size = new System.Drawing.Size(55, 13);
            this.lblTXT.TabIndex = 11;
            this.lblTXT.Text = "Text data:";
            // 
            // chkA
            // 
            this.chkA.AutoSize = true;
            this.chkA.Location = new System.Drawing.Point(0, 78);
            this.chkA.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.chkA.Name = "chkA";
            this.chkA.Size = new System.Drawing.Size(74, 17);
            this.chkA.TabIndex = 4;
            this.chkA.Text = "A-records:";
            this.chkA.UseVisualStyleBackColor = true;
            this.chkA.CheckedChanged += new System.EventHandler(this.chkA_CheckedChanged);
            // 
            // chkAAAA
            // 
            this.chkAAAA.AutoSize = true;
            this.chkAAAA.Location = new System.Drawing.Point(0, 131);
            this.chkAAAA.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.chkAAAA.Name = "chkAAAA";
            this.chkAAAA.Size = new System.Drawing.Size(122, 17);
            this.chkAAAA.TabIndex = 7;
            this.chkAAAA.Text = "AAAA- / A6-records:";
            this.chkAAAA.UseVisualStyleBackColor = true;
            this.chkAAAA.CheckedChanged += new System.EventHandler(this.chkAAAA_CheckedChanged);
            // 
            // chkTXT
            // 
            this.chkTXT.AutoSize = true;
            this.chkTXT.Location = new System.Drawing.Point(0, 187);
            this.chkTXT.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.chkTXT.Name = "chkTXT";
            this.chkTXT.Size = new System.Drawing.Size(88, 17);
            this.chkTXT.TabIndex = 10;
            this.chkTXT.Text = "TXT-records:";
            this.chkTXT.UseVisualStyleBackColor = true;
            this.chkTXT.CheckedChanged += new System.EventHandler(this.chkTXT_CheckedChanged);
            // 
            // txtTXT
            // 
            this.txtTXT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTXT.Enabled = false;
            this.txtTXT.Location = new System.Drawing.Point(95, 209);
            this.txtTXT.MaxLength = 255;
            this.txtTXT.Name = "txtTXT";
            this.txtTXT.Size = new System.Drawing.Size(283, 20);
            this.txtTXT.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-3, 247);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "TTL (Time To Live) value for served records:";
            // 
            // txtTTL
            // 
            this.txtTTL.Location = new System.Drawing.Point(0, 263);
            this.txtTTL.Name = "txtTTL";
            this.txtTTL.Size = new System.Drawing.Size(60, 20);
            this.txtTTL.TabIndex = 14;
            this.txtTTL.Text = "300";
            // 
            // ofDlg
            // 
            this.ofDlg.AddExtension = false;
            this.ofDlg.Filter = "BOOT file|BOOT|All files|*.*";
            this.ofDlg.Title = "Select BOOT file";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "seconds";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-3, 52);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 13, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(212, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Serve DNS requests for these record types:";
            // 
            // MyOptionsUI
            // 
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTTL);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTXT);
            this.Controls.Add(this.chkTXT);
            this.Controls.Add(this.chkAAAA);
            this.Controls.Add(this.chkA);
            this.Controls.Add(this.lblTXT);
            this.Controls.Add(this.txtIPv6);
            this.Controls.Add(this.txtIPv4);
            this.Controls.Add(this.lblIPv6);
            this.Controls.Add(this.lblIPv4);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDataFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Name = "MyOptionsUI";
            this.Size = new System.Drawing.Size(378, 302);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDataFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblIPv4;
        private System.Windows.Forms.Label lblIPv6;
        private System.Windows.Forms.TextBox txtIPv4;
        private System.Windows.Forms.TextBox txtIPv6;
        private System.Windows.Forms.Label lblTXT;
        private System.Windows.Forms.CheckBox chkA;
        private System.Windows.Forms.CheckBox chkAAAA;
        private System.Windows.Forms.CheckBox chkTXT;
        private System.Windows.Forms.TextBox txtTXT;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTTL;
        private System.Windows.Forms.OpenFileDialog ofDlg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
