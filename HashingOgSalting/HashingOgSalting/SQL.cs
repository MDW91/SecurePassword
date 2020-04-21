using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace HashingOgSalting
{
    class SQL
    {
        

        public static void SaveUserInfoToDatabase(string username, string hashedsaltedpassword, string salt)
        {

            string constring = GetConnectionString();

            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = constring;
                    //Open connection to database
                    con.Open();

                    //insert username and password into user table
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO [User] (Username, Hashedpassword, Salt) VALUES(@username, @hashedsaltedpassword, @salt)";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@hashedsaltedpassword", hashedsaltedpassword);
                    cmd.Parameters.AddWithValue("@salt", salt);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception c)
                {

                    Console.WriteLine(c); ;

                }

            }

        }


        public static bool CheckLoginInfo(string username, byte[] password)
        {
            string constring = GetConnectionString();
            User user = new User();

            //string sqlUsername;
            //byte[] sqlPassword;
            //string sqlSalt;
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = constring;
                    //Open connection to database

                    string sqlCmd = "SELECT Username, Hashedpassword, Salt FROM [User] WHERE Username = @username;";
                    SqlCommand cmd = new SqlCommand(sqlCmd, con);

                    cmd.Parameters.AddWithValue("@username", username);

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User.Sqlusername = reader[0].ToString();
                            User.Sqlpassword = Convert.FromBase64String(reader[1].ToString());
                            User.Salt = Convert.FromBase64String(reader[2].ToString());
                        }
                    }

                    con.Close();
                }
                catch (Exception c)
                {

                    Console.WriteLine(c); ;

                }

            }

            user.Hashedsaltedpassword = Encryption.HashPasswordWithSalt(password, User.Salt);

            if (Convert.ToBase64String(user.Hashedsaltedpassword) == Convert.ToBase64String(User.Sqlpassword))
            {

                return true;
            }
            else
            {
                return false;
            }


        }


        static private string GetConnectionString()
        {
            return "Data Source=DESKTOP-KMUBQ0U;Initial Catalog=hashdatabase2019;Integrated Security=True";
        }
    }
}
