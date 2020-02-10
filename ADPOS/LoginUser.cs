using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ADPOS
{
    public class LoginUser
    {
        public static string cs = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
        private static int id;
       

        public static int IDGlobal
        {
            get { return id; }
            set { id = value; }
        }


        private string USerName;
        private string Password;
        public string Uname
        {
            get { return USerName; }
            set { USerName = value; }
        }

        public string Pswd
        {
            get { return Password; }
            set { Password = value; }
        }
    }
}
