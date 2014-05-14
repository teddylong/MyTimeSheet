using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MyTimeSheet
{
    public class Information
    {

        private static XmlDocument userNameFile;

        public static XmlDocument UserNameFile
        {
            get { return Information.userNameFile; }
            set { Information.userNameFile = value; }
        }
        private static string userNameAddress;

        public static string UserNameAddress
        {
            get { return Information.userNameAddress; }
            set { Information.userNameAddress = value; }
        }



        


    }
}
