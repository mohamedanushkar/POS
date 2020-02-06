using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADPOS
{
    public class LoginUser
    {
        public string USerName;
        public string Password;
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
