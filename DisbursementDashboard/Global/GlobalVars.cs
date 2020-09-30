using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DisbursementDashboard.Global
{
    public class GlobalVars
    {
        private static string _Server = "P07W07MCLDZZ003.man.co"; //Prod      
        //private static string _Server = "S07W02MCLDZZ013.man.prep"; //Pre-Prod
        private static string _DB = "001_Synergy";
        private static string _User = "MSM";
        private static string _Pass = "M$M";
        private static string _Conn = "Server=" + _Server + "; Database=" + _DB + "; Integrated Security=False; User Id=" + _User + "; Password=" + _Pass + ";";
        private static string _EasyPostAPI_Test = "5QsUv0NTvMNEQqf_jHNkhw";
        private static string _EasyPostAPI_Prod = "eHWFY_Z0Molw8gbLEfTQdg";
        public static bool IsTesting()
        {
            return false;
        }

        public static Dictionary<string, string> DBConnection = new Dictionary<string, string>()
        {
            { "Conn", _Conn }
        };
    }
}