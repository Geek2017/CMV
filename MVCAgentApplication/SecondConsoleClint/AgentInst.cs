using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVCAgent
{
    [RunInstaller(true)]
    public partial class AgentInst : System.Configuration.Install.Installer
    {
        private static readonly ILog loggerInstaller = LogManager.GetLogger(typeof(AgentInst));
        public AgentInst()
        {
            InitializeComponent();
            this.AfterInstall+= new InstallEventHandler(MyInstaller_AfterInstall);
        }
        //Needed for auto start after the installation.
        private void MyInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName
               (Assembly.GetExecutingAssembly().Location));
            Process.Start(Path.GetDirectoryName(
              Assembly.GetExecutingAssembly().Location) + "\\MVCAgent.exe");
        }
    }
}
