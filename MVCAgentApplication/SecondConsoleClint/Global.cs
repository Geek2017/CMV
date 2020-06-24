using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAgentApplication
{
    public static class Global
    {
        private static string _globalVar = "";
        public static string GlobalVar
        {
            get { return _globalVar; }
            set { _globalVar = value; }
        }
        private static bool _globalStatus = false;
        public static bool GlobalStatus
        {
            get { return _globalStatus; }
            set { _globalStatus = value; }
        }
    }
}
