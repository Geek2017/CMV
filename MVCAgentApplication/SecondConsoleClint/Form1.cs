using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecondConsoleClint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Set_property_of_Notify();
        }
        private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void Set_property_of_Notify()
        {
            Hide();
            notifyIcon1.ContextMenuStrip = contextMenuStrip;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = Global.GlobalVar;
            notifyIcon1.Text = Global.GlobalVar;
            notifyIcon1.BalloonTipTitle = "Connection Status";
            notifyIcon1.ShowBalloonTip(2000);
            ToolStripMenuItem FileMenu = new ToolStripMenuItem("Status");
            contextMenuStrip.Items.Add(FileMenu);
            if (Global.GlobalStatus==true)
            {
                FileMenu.Image = Image.FromFile("E:\\CurrentProjectLog\\AgentConnected.png");
                FileMenu.ToolTipText = "Connected To HUB.";
            }
            else if (Global.GlobalStatus == false)
            {
                FileMenu.Image = Image.FromFile("E:\\CurrentProjectLog\\AgentDisconnected.png");
                FileMenu.ToolTipText = "Not Connected To HUB.";
            }
            Hide();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Will Exit Agent Application 
            notifyIcon1.Dispose();
            Program.ExitLog();
            Application.Exit();
        }
        //private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //Will Restore/Reconnect Agent Application 
        //    Hide();
            
        //    //notifyIcon1.Dispose();
        //    Program.Connect("Reconnect");
        //    WindowState = FormWindowState.Normal;
        //}

    }
}
