using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {

        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        private TriviaContext context = new TriviaContext();
        public User LoggedUser { get; private set; }
        //Implememnt interface here
        public bool ShowLogin()
        {
            if (LoggedUser != null)//Logs out if a user is currently logged in
            {
                LoggedUser = null;
            }
            bool loggedIn = false;
            while (!loggedIn)
            {
                if (LoggedUser != null)
                {
                    Console.WriteLine(LoggedUser.Id);
                }
                Console.WriteLine("Enter email:");
                string email = Console.ReadLine();
                LoggedUser = context.GetUserByEmail(email);
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                if (LoggedUser != null)
                {
                    if (password == LoggedUser.Pswrd)
                    {
                        loggedIn = true;
                        Console.WriteLine("Log in successfull");
                    }
                }
                else
                {
                    ClearScreenAndSetTitle("Login");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Email or Password is incorrect");
                    Console.ResetColor();
                }
            }
            return loggedIn;
        }
        public bool ShowSignUp()
        {
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            //this.currentyPLayer == null

            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
            if (LoggedUser != null)//Logs out if a user is currently logged in
            {
                LoggedUser = null;
            }
            char c = ' ';
            while (c != 'B' && c != 'b' /*&& this.currentyPLayer == null*/)
            {
                //Clear screen
                ClearScreenAndSetTitle("Signup");

                Console.Write("Please Type your email: ");
                string email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Bad Email Format! Please try again:");
                    Console.ResetColor();
                    email = Console.ReadLine();
                }

                Console.Write("Please Type your password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.ForegroundColor= ConsoleColor.Red;  
                    Console.Write("password must be at least 8 characters! Please try again: ");
                    Console.ResetColor();   
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");
                string name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("name must be at least 2 characters! Please try again: ");
                    Console.ResetColor();
                    name = Console.ReadLine();
                }

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Connecting to Server...");
                Console.ResetColor();
                /* Create instance of Business Logic and call the signup method
                 * For example:
                try
                {
                    TriviaDBContext db = new TriviaDBContext();
                    this.currentyPLayer = db.SignUp(email, password, name);
                }
                catch (Exception ex)
                {
                Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                Console.ResetColor();
                }
                
                */

                //Provide a proper message for example:
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                //Get another input from user
                c = Console.ReadKey(true).KeyChar;
            }
            //return true if signup suceeded!
            return (false);
        }

        public void ShowAddQuestion()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }

        public void ShowPendingQuestions()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowProfile()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }


        //Private helper methods down here...
        private void ClearScreenAndSetTitle(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{title,65}");
            Console.WriteLine();
            Console.ResetColor();   
        }

        private bool IsEmailValid(string emailAddress)
        {
            //regex is string based pattern to validate a text that follows a certain rules
            // see https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

            var pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }



        private bool IsPasswordValid(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 8;
        }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 2;
        }
    }
}
