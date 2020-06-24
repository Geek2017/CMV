namespace MVCAgentApplication
{
    partial class AgentLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentLoginForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginLogoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frmTxtCustId = new System.Windows.Forms.TextBox();
            this.frmTxtExtensionNo = new System.Windows.Forms.TextBox();
            this.lblExtensionNo = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblCompanyId = new System.Windows.Forms.Label();
            this.lblAgentName = new System.Windows.Forms.Label();
            this.frmTxtAgentName = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblVersionNo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginLogoutToolStripMenuItem,
            this.testConnectionToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(160, 70);
            // 
            // loginLogoutToolStripMenuItem
            // 
            this.loginLogoutToolStripMenuItem.Name = "loginLogoutToolStripMenuItem";
            this.loginLogoutToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.loginLogoutToolStripMenuItem.Text = "Login";
            this.loginLogoutToolStripMenuItem.Click += new System.EventHandler(this.loginLogoutToolStripMenuItem_Click);
            // 
            // testConnectionToolStripMenuItem
            // 
            this.testConnectionToolStripMenuItem.Name = "testConnectionToolStripMenuItem";
            this.testConnectionToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.testConnectionToolStripMenuItem.Text = "Test Connection";
            this.testConnectionToolStripMenuItem.Click += new System.EventHandler(this.testConnectionToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.exitToolStripMenuItem.Text = "Stop App";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // frmTxtCustId
            // 
            this.frmTxtCustId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmTxtCustId.Location = new System.Drawing.Point(47, 181);
            this.frmTxtCustId.MaxLength = 5;
            this.frmTxtCustId.Name = "frmTxtCustId";
            this.frmTxtCustId.Size = new System.Drawing.Size(216, 26);
            this.frmTxtCustId.TabIndex = 2;
            this.frmTxtCustId.Validating += new System.ComponentModel.CancelEventHandler(this.frmTxtCustId_Validating);
            // 
            // frmTxtExtensionNo
            // 
            this.frmTxtExtensionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmTxtExtensionNo.Location = new System.Drawing.Point(47, 121);
            this.frmTxtExtensionNo.MaxLength = 5;
            this.frmTxtExtensionNo.Name = "frmTxtExtensionNo";
            this.frmTxtExtensionNo.Size = new System.Drawing.Size(216, 26);
            this.frmTxtExtensionNo.TabIndex = 1;
            this.frmTxtExtensionNo.Validating += new System.ComponentModel.CancelEventHandler(this.frmTxtExtensionNo_Validating);
            // 
            // lblExtensionNo
            // 
            this.lblExtensionNo.AutoSize = true;
            this.lblExtensionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtensionNo.Location = new System.Drawing.Point(47, 95);
            this.lblExtensionNo.Name = "lblExtensionNo";
            this.lblExtensionNo.Size = new System.Drawing.Size(103, 20);
            this.lblExtensionNo.TabIndex = 4;
            this.lblExtensionNo.Text = "Extension No";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Crimson;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLogin.Location = new System.Drawing.Point(47, 248);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(216, 37);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblCompanyId
            // 
            this.lblCompanyId.AutoSize = true;
            this.lblCompanyId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyId.Location = new System.Drawing.Point(47, 156);
            this.lblCompanyId.Name = "lblCompanyId";
            this.lblCompanyId.Size = new System.Drawing.Size(60, 20);
            this.lblCompanyId.TabIndex = 3;
            this.lblCompanyId.Text = "Cust Id";
            // 
            // lblAgentName
            // 
            this.lblAgentName.AutoSize = true;
            this.lblAgentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgentName.Location = new System.Drawing.Point(47, 33);
            this.lblAgentName.Name = "lblAgentName";
            this.lblAgentName.Size = new System.Drawing.Size(98, 20);
            this.lblAgentName.TabIndex = 6;
            this.lblAgentName.Text = "Agent Name";
            // 
            // frmTxtAgentName
            // 
            this.frmTxtAgentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frmTxtAgentName.Location = new System.Drawing.Point(47, 57);
            this.frmTxtAgentName.MaxLength = 100;
            this.frmTxtAgentName.Name = "frmTxtAgentName";
            this.frmTxtAgentName.Size = new System.Drawing.Size(216, 26);
            this.frmTxtAgentName.TabIndex = 0;
            this.frmTxtAgentName.Validating += new System.ComponentModel.CancelEventHandler(this.frmTxtAgentName_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblVersionNo
            // 
            this.lblVersionNo.AutoSize = true;
            this.lblVersionNo.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionNo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblVersionNo.Location = new System.Drawing.Point(253, 295);
            this.lblVersionNo.Name = "lblVersionNo";
            this.lblVersionNo.Size = new System.Drawing.Size(74, 13);
            this.lblVersionNo.TabIndex = 8;
            this.lblVersionNo.Text = "V. {0}.{1}.{2}.{3}";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(290, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 26);
            this.button1.TabIndex = 9;
            this.button1.Text = "Call SF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AgentLoginForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(487, 314);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblVersionNo);
            this.Controls.Add(this.frmTxtAgentName);
            this.Controls.Add(this.lblAgentName);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblExtensionNo);
            this.Controls.Add(this.lblCompanyId);
            this.Controls.Add(this.frmTxtExtensionNo);
            this.Controls.Add(this.frmTxtCustId);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgentLoginForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agent Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginLogoutToolStripMenuItem;
        private System.Windows.Forms.TextBox frmTxtCustId;
        private System.Windows.Forms.TextBox frmTxtExtensionNo;
        private System.Windows.Forms.Label lblExtensionNo;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblCompanyId;
        private System.Windows.Forms.Label lblAgentName;
        private System.Windows.Forms.TextBox frmTxtAgentName;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripMenuItem testConnectionToolStripMenuItem;
        private System.Windows.Forms.Label lblVersionNo;
        private System.Windows.Forms.Button button1;
    }
}