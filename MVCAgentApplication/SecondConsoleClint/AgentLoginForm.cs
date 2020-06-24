using log4net;
using System;

using System.Collections.Generic;
using System.Text;
using System.Web;


using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MVCAgentApplication.Model;
using Newtonsoft.Json;
using System.Resources;
using System.Reflection;
using MVCAgent.Resources;
using MVCAgent.Properties;

namespace MVCAgentApplication
{
    public partial class AgentLoginForm : Form
    {
        public ResourceManager connectMeMsg = new ResourceManager("UsingRESX.ConnectMeResource", Assembly.GetExecutingAssembly());
        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
        private static bool isValid = false;
        private static bool isAgentConfigDelete = false;
        public AgentLoginForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemInSetPropertyOfNotifyError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                lblVersionNo.Text = String.Format(lblVersionNo.Text, version.Major, version.Minor, version.Build, version.Revision);
                Set_property_of_Notify();
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemInSetPropertyOfNotifyError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }
        private void Set_property_of_Notify()
        {
            try
            {
                WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemInSetPropertyOfNotifyError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //To Exit Agent Application but AgentConfig not delete.
                notifyIcon1.Dispose();
                isAgentConfigDelete = true;

                Program.AgentExitLog();
                logger.Info(ConnectMeResource.LoggerStoppedApplcationAt + DateTime.Now);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.LoggerStoppedApplcationAt);
                logger.Error(ConnectMeResource.LoggerProblemOnClickingStopAppError + ex.Message);
                logger.Error(ConnectMeResource.LoggerStoppedApplcationAt);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    if ((frmTxtAgentName.Text.Trim().Length > 0 &&
                       frmTxtCustId.Text.Trim().Length > 0 &&
                       frmTxtExtensionNo.Text.Trim().Length > 0) &&
                            (frmTxtAgentName.Text.Trim().Length <= 50 &&
                       frmTxtCustId.Text.Trim().Length <= 5 &&
                       frmTxtExtensionNo.Text.Trim().Length <= 5))
                    {
                        // To hide the current AgentLoginForm 
                        Hide();

                        // To start a Connection with entered values
                        Program.ConnecteToHub(frmTxtCustId.Text.Trim(), frmTxtExtensionNo.Text.Trim(),
                        frmTxtAgentName.Text.Trim());

                        // Setting context menu options
                        notifyIcon1.ContextMenuStrip = contextMenuStrip;
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon1.BalloonTipText = Global.GlobalVar;
                        notifyIcon1.Text = Global.GlobalVar;
                        notifyIcon1.BalloonTipTitle = ConnectMeResource.Welcome + frmTxtAgentName.Text.Trim();
                        notifyIcon1.ShowBalloonTip(2000);

                        // Dynamically adding Status at top of context menu
                        // so on Stop app show different image in menu
                        ToolStripMenuItem FileMenu = new ToolStripMenuItem(ConnectMeResource.Status);
                        //Number of fix context menu is 3 max
                        if (contextMenuStrip.Items.Count <= 3)
                        {
                            if (!contextMenuStrip.Items.Contains(FileMenu))
                            {
                                contextMenuStrip.Items.Insert(0, FileMenu);
                                if (Global.GlobalStatus == true)
                                {
                                    FileMenu.Image = Resources.AgentConnected;
                                    FileMenu.ToolTipText = ConnectMeResource.ConnectedToHUB;
                                    contextMenuStrip.Items[1].Text = ConnectMeResource.LogOut;
                                }
                                else if (Global.GlobalStatus == false)
                                {
                                    FileMenu.Image = Resources.AgentDisconnected;
                                    FileMenu.ToolTipText = ConnectMeResource.NotConnectedToHUB;
                                    contextMenuStrip.Items[1].Text = ConnectMeResource.LogIn;
                                }
                            }
                        }
                        else
                        {
                            contextMenuStrip.Items[0].Image = Resources.AgentConnected;
                            contextMenuStrip.Items[0].ToolTipText = ConnectMeResource.NotConnectedToHUB;
                            contextMenuStrip.Items[1].Text = ConnectMeResource.LogOut;
                            contextMenuStrip.Items[1].ToolTipText = ConnectMeResource.ClickToLogout;
                        }
                    }
                    else
                    {
                        MessageBox.Show(ConnectMeResource.AllFieldsMustBeCorrect);
                    }
                }
                else
                {
                    MessageBox.Show(ConnectMeResource.AllFieldsMustBeCorrect);
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemAtAgentLoginError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }
        public void loginLogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // To Stop existing Agent and reloggin.
            // Maintain the log info
            try
            {
                logger.Info(frmTxtAgentName.Text.Trim() + ConnectMeResource.LoggedOutAt + DateTime.Now);
                //To update Agents connection Status at database.
                Program.AgentLoggedOut();
                contextMenuStrip.Items[1].Text = ConnectMeResource.LogIn;
                contextMenuStrip.Items[1].ToolTipText = ConnectMeResource.ClickToLogin;
                contextMenuStrip.Items[0].Image = Resources.AgentDisconnected;
                contextMenuStrip.Items[0].ToolTipText = ConnectMeResource.NotConnectedMsg;

                frmTxtAgentName.Text = string.Empty;
                frmTxtExtensionNo.Text = string.Empty;
                frmTxtCustId.Text = string.Empty;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemAtLoginLogoutContextMenuClickError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (!isAgentConfigDelete)
                {
                    string strAgentInfo = Application.StartupPath + "\\Documents\\AgentConfig.txt"; ; //System.Configuration.ConfigurationSettings.AppSettings.Get("AgentInfo").ToString();
                    if (File.Exists(strAgentInfo))
                    {
                        File.SetAttributes(strAgentInfo, FileAttributes.Normal);
                        File.Delete(strAgentInfo);
                    }
                }
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                Global.GlobalVar = ConnectMeResource.NotConnectedMsg;
                if (notifyIcon1.ContextMenu == null)
                {
                    Set_property_of_Notify();
                    notifyIcon1.ContextMenuStrip = contextMenuStrip;

                    ToolStripMenuItem FileMenu = new ToolStripMenuItem(ConnectMeResource.Status);
                    if (contextMenuStrip.Items.Count <= 3)
                    {
                        if (!contextMenuStrip.Items.Contains(FileMenu))
                        {
                            contextMenuStrip.Items.Insert(0, FileMenu);

                            FileMenu.Image = Resources.AgentDisconnected;
                            FileMenu.ToolTipText = ConnectMeResource.NotConnectedToHUB;
                            contextMenuStrip.Items[1].Text = ConnectMeResource.LogIn;
                            contextMenuStrip.Items[1].ToolTipText = ConnectMeResource.ClickToLogout;
                        }
                    }
                }
                contextMenuStrip.Items[0].Image = Resources.AgentDisconnected;
                contextMenuStrip.Items[0].ToolTipText = Global.GlobalVar;
                contextMenuStrip.Items[1].Text = ConnectMeResource.LogIn;
                contextMenuStrip.Items[1].ToolTipText = ConnectMeResource.ClickToLogin;

                notifyIcon1.Text = Global.GlobalVar;
                notifyIcon1.BalloonTipText = Global.GlobalVar;
                notifyIcon1.BalloonTipTitle = ConnectMeResource.AgentNotConnected;
                notifyIcon1.ShowBalloonTip(2000);
                Hide();
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemOnClosingAgentCredentialFormError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }

        #region Validating Agents Input
        private void frmTxtAgentName_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(frmTxtAgentName.Text))
                {
                    e.Cancel = true;
                    frmTxtAgentName.Focus();
                    errorProvider1.SetError(frmTxtAgentName, ConnectMeResource.ProvideAgentName);
                }
                else if (frmTxtAgentName.Text.Trim().Length > 50)
                {
                    e.Cancel = true;
                    frmTxtAgentName.Focus();
                    errorProvider1.SetError(frmTxtAgentName, ConnectMeResource.FiftyCharactersOnly);
                }
                else
                {
                    e.Cancel = false;
                    isValid = true;
                    errorProvider1.SetError(frmTxtAgentName, "");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemOnClosingAgentCredentialFormError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }
        private void frmTxtExtensionNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(frmTxtExtensionNo.Text))
                {
                    e.Cancel = true;
                    frmTxtExtensionNo.Focus();
                    errorProvider1.SetError(frmTxtExtensionNo, ConnectMeResource.MaximumFiveNumericDigitsOnly);
                }
                else if (!frmTxtExtensionNo.Text.All(Char.IsDigit))
                {
                    e.Cancel = true;
                    frmTxtCustId.Focus();
                    errorProvider1.SetError(frmTxtExtensionNo, ConnectMeResource.MaximumFiveNumericDigitsOnly);
                }
                else if (frmTxtExtensionNo.Text.Trim().Length > 5)
                {
                    e.Cancel = true;
                    frmTxtExtensionNo.Focus();
                    errorProvider1.SetError(frmTxtExtensionNo, ConnectMeResource.MaximumFiveNumericDigitsOnly);
                }
                else
                {
                    e.Cancel = false;
                    isValid = true;
                    errorProvider1.SetError(frmTxtExtensionNo, "");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemOnClosingAgentCredentialFormError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }

        }
        private void frmTxtCustId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(frmTxtCustId.Text))
                {
                    e.Cancel = true;
                    frmTxtCustId.Focus();
                    errorProvider1.SetError(frmTxtCustId, ConnectMeResource.MaximumFiveNumericDigitsOnly);
                }
                else if (!frmTxtCustId.Text.All(Char.IsDigit))
                {
                    e.Cancel = true;
                    frmTxtCustId.Focus();
                    errorProvider1.SetError(frmTxtCustId, ConnectMeResource.MaximumFiveNumericDigitsOnly);
                }
                else if (frmTxtCustId.Text.Trim().Length > 5)
                {
                    e.Cancel = true;
                    frmTxtCustId.Focus();
                    errorProvider1.SetError(frmTxtCustId, ConnectMeResource.MaximumFiveNumericDigitsOnly);
                }
                else
                {
                    e.Cancel = false;
                    isValid = true;
                    errorProvider1.SetError(frmTxtCustId, "");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerProblemOnClosingAgentCredentialFormError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }

        #endregion Validating Agents Input

        private void testConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string agentName = string.Empty;
            string customerId = string.Empty;
            string extensionNo = string.Empty;

            #region Reading connected Agentname
            try
            {
                string strAgentInfo = System.Configuration.ConfigurationSettings.AppSettings.Get("AgentInfo").ToString();
                using (StreamReader r = new StreamReader(strAgentInfo))
                {
                    string json = r.ReadToEnd();
                    Agent items = JsonConvert.DeserializeObject<Agent>(json);
                    agentName = items.AgentName;
                    customerId = items.CustomerId;
                    extensionNo = items.ExtensionNo;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerPleaseLoginIntoTheApplicationToTestConnectionError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
                MessageBox.Show(ConnectMeResource.PleaseLoginIntoTheApplicationToTestConnection);
            }

            #endregion
            if ((customerId.Trim().Length > 0) && (extensionNo.Trim().Length > 0) && (agentName.Trim().Length > 0))
            {
                //Call Method to Connect to Hub with already present credentials
                Program.ConnecteToHub(customerId, extensionNo, agentName);

                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipText = Global.GlobalVar;
                notifyIcon1.Text = Global.GlobalVar;
                notifyIcon1.BalloonTipTitle = ConnectMeResource.Welcome + agentName;
                notifyIcon1.ShowBalloonTip(2000);

                // Dynamically adding Status at top of context menu
                // so on Stop app show different image in menu
                ToolStripMenuItem FileMenu = new ToolStripMenuItem(ConnectMeResource.Status);

                if (Global.GlobalStatus == true)
                {
                    contextMenuStrip.Items[0].Image = Resources.AgentConnected;
                    contextMenuStrip.Items[0].ToolTipText = ConnectMeResource.ConnectedToHUB;
                    contextMenuStrip.Items[1].Text = ConnectMeResource.LogOut;
                }
                else if (Global.GlobalStatus == false)
                {
                    contextMenuStrip.Items[0].Image = Resources.AgentDisconnected;
                    contextMenuStrip.Items[0].ToolTipText = ConnectMeResource.NotConnectedToHUB;
                    contextMenuStrip.Items[1].Text = ConnectMeResource.LogIn;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        }
    }
}
