using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {
        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        TriviaContext triviaContext = new TriviaContext();
        User user = new User();

        //Implememnt interface here
        public bool ShowLogin()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
            return true;
        }
        public bool ShowSignUp()
        {
            //Logout user if anyone is logged in!
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            //this.currentyPLayer == null

            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
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
           if(user.Points >= 100)
            {
                Console.WriteLine("add Question text");
                Question q = new Question();
                q.Text = Console.ReadLine();

                q.
                user.Points = 0;
            }
        }

        public void ShowPendingQuestions()
        {
            Console.WriteLine("pending questions");
            char x;
            x = '5';
            
            foreach (Question q in triviaContext.Questions)
            {
                if (q.StatusId == 1)
                {
                    Console.WriteLine(q.Text);
                    Console.WriteLine(q.RightAnswer);
                    Console.WriteLine(q.WrongAnswer1);
                    Console.WriteLine(q.WrongAnswer2);
                    Console.WriteLine(q.WrongAnswer3);
                    Console.WriteLine("Press 1 to aprove ,Press 2 to reject, Press 3 to skip");

                    while (x == '5')
                    {
                        x = Console.ReadKey().KeyChar;
                        if (x == 1)
                            q.StatusId = 2;
                        if (x == 2) q.StatusId = 3;
                        if (x == 3)
                            q.StatusId = 1;
                        else x = '5';

                    }


                }
            }
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
