using System;
using System.Collections.Generic;
using System.Text;

namespace HashingOgSalting
{
    class User
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string hashedpassword;
        public string Hashedpassword
        {
            get { return hashedpassword; }
            set { hashedpassword = value; }
        }

        private byte[] hashedsaltedpassword;
        public byte[] Hashedsaltedpassword
        {
            get { return hashedsaltedpassword; }
            set { hashedsaltedpassword = value; }
        }

        private static byte[] salt;
        public static byte[] Salt
        {
            get { return salt; }
            set { salt = value; }
        }

        private int loginattempts;
        public int Loginattempts
        {
            get { return loginattempts; }
            set { loginattempts = value; }
        }

        private static byte[] hashedbytepassword;
        public static byte[] Hashedbytepassword
        {
            get { return hashedbytepassword; }
            set { hashedbytepassword = value; }
        }

        private static string sqlusername;
        public static string Sqlusername
        {
            get { return sqlusername; }
            set { sqlusername = value; }
        }

        private static byte[] sqlpassword;
        public static byte[] Sqlpassword
        {
            get { return sqlpassword; }
            set { sqlpassword = value; }
        }




    }
}
