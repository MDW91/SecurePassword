using System;
using System.Text;
using System.Threading;
namespace HashingOgSalting
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();


            Console.WriteLine("To create an account type 1");
            Console.WriteLine("To login with an existing account type 2 ");
            string answer = Console.ReadLine();

            //chose login or create user loop

            //string answer = "";
            while (answer != "1" && answer != "2")
            {
                Console.Clear();
                Console.WriteLine("input invalid, please try again");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("To create an account type 1");
                Console.WriteLine("To login with an existing account type 2 ");

                answer = Console.ReadLine();
            }

            //create user
            if (answer == "1")
            {
                Console.Clear();

                //inndtastning af brugernavn
                Console.Write("type in a username: ");
                user.Username = Console.ReadLine();
                Console.Clear();

                //indtastning af password
                Console.Write("type in a password: ");
                user.Password = Console.ReadLine();
                user.Hashedpassword = Encryption.ComputeSHA2Hash(user.Password);

                User.Salt = Encryption.GenerateSalt();
                user.Hashedsaltedpassword = Encryption.HashPasswordWithSalt(Convert.FromBase64String(user.Hashedpassword), User.Salt);

                Console.Clear();

                Console.WriteLine("username and password saved, User created.");
                Thread.Sleep(2000);

                // brugerens oplysninger gemmes i sql databasen her..... password skal hashes inden.
                SQL.SaveUserInfoToDatabase(user.Username, Convert.ToBase64String(user.Hashedsaltedpassword), Convert.ToBase64String(User.Salt));
                

                //set answer til 2 så brugeren bliver ført til login
                answer = "2";

            }


            //login 
            if (answer == "2")
            {
                Console.Clear();
                Console.WriteLine("Login");

                Console.Write("Username: ");
                user.Username = Console.ReadLine();
                Console.Clear();


                Console.Write("password: ");
                user.Password = Console.ReadLine();
                user.Hashedpassword = Encryption.ComputeSHA2Hash(user.Password);


                //sql metode til at hente bruger info...... gem salt i user.salt
                // derefter kør nedenstående metode til at få hashed/salted password med samme salt.
                // derefter tjek om resultaterne er ens

                
                if (SQL.CheckLoginInfo(user.Username, Convert.FromBase64String(user.Hashedpassword)) == true)
                {
                    Console.WriteLine("yay login successful");
                }
                else
                {


                    while (user.Loginattempts < 5)
                    {
                        Console.Clear();
                        Console.WriteLine("input invalid, please try again");
                        user.Loginattempts += 1;
                        Thread.Sleep(2000);
                        Console.Clear();

                        Console.WriteLine("Login");

                        Console.Write("password: ");
                        user.Password = Console.ReadLine();
                        user.Hashedpassword = Encryption.ComputeSHA2Hash(user.Password);

                        
                    }


                    Console.WriteLine("Too many login attempts, account locked");
                }

            }

            Console.ReadKey();

        }
    }
}
