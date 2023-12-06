using Microsoft.EntityFrameworkCore;
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
        private TriviaDbContext context = new TriviaDbContext();
        //Place here any state you would like to keep during the app life time
        //For example, player login details...


        private DbContext db = new TriviaDbContext();

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
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    Console.ResetColor();   
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");
                string name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("name must be at least 3 characters! Please try again: ");
                    Console.ResetColor();
                    name = Console.ReadLine();
                }

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Connecting to Server...");
                Console.ResetColor();
                //Create instance of Business Logic and call the signup method
                // *For example:
                //try
                //{
                //    TriviaDBContext db = new TriviaDBContext();
                //    this.currentyPLayer = db.SignUp(email, password, name);
                //}
                //catch (Exception ex)
                //{
                //    Console.ForegroundColor = ConsoleColor.Red;
                //    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                //    Console.ResetColor();
                //}



                //Provide a proper message for example:
                Console.WriteLine("Press (B)ack to go back or any other key to signup again...");
                //Get another input from user
                c = Console.ReadKey(true).KeyChar;
            }
            //return true if signup suceeded!
            return (false);
        }
        //איתמר
        public void ShowAddQuestion()
        {

            Console.WriteLine("Question");
            foreach(Player p in context.Players)
            {

                if (p.Points >= 100)
                {
                    Console.WriteLine("Horray you reached 100 points now you can add a question");
                    Console.WriteLine("Write the subject");
                    string subject=Console.ReadLine();
                    Console.WriteLine("Write the question");
                    string question=Console.ReadLine();
                    Console.WriteLine("Write the correct answer");
                    string correctanswer=Console.ReadLine();
                    Console.WriteLine("Write the first wrong answer");
                    string WrongAnswer1 = Console.ReadLine();
                    Console.WriteLine("write the second wrong answer");
                    string WrongAnswer2 = Console.ReadLine();
                    Console.WriteLine("write the third wrong answer");
                    string WrongAnswer3 = Console.ReadLine();


                }
                else { Console.WriteLine("if you want to add questions,you need to get more points!"); }
            }
        }

        public void ShowPendingQuestions()//Ben
        {
            // Shows a PendingQuestion
            Console.WriteLine("Pending question");
            char c;
            c = '5';
            foreach (Question q in context.Questions)
            {
                if (q.StatusId == 1)
                {
                    Console.WriteLine(q.RightA);
                    Console.WriteLine(q.WrongA1);
                    Console.WriteLine(q.WrongA2);
                    Console.WriteLine(q.WrongA3);
                    Console.WriteLine("Press 1 to approve ,Press 2 to reject, Press 2 to reject, Press 3 to skip");

                    while (c == '5')
                    {
                        c = Console.ReadKey().KeyChar;
                        if (c == 1)
                        {
                            q.StatusId = 2;
                        }
                        if (c == 2)
                        {
                            q.StatusId = 3;
                        }
                        else
                            c = '5';
                    }
                }
            }
        }
        //איתמר
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

        //another option is using .net System.Net.Mail library which has an EmailAddress class that stores email
        //we can use it to validate the structure of the email:
       // https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.mailaddress?view=net-7.0
            /*
             * try
             * {
             *     //try to create MailAddress objcect from the email address string
             *      var email=new MailAddress(emailAddress);
             *      //if success
             *      return true;
             * }
             *      //if it throws a formatExcpetion then the string is not email format.
             * catch (Exception ex)
             * {
             * return false;
             * }
             */

        }



        private bool IsPasswordValid(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 3;
        }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length >= 3;
        }
    }
}
