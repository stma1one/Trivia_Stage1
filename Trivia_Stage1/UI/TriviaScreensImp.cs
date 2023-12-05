using Microsoft.EntityFrameworkCore;
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
        //Place here any state you would like to keep during the app life time
        //For example, player login details...
        TriviaContext context = new TriviaContext();
        User LoggedUser;
        public bool ShowLogin()
        {
            bool loggedIn = false;
            while (!loggedIn)
            {
                if (LoggedUser != null)//Logs out if a user is currently logged in
                {
                    LoggedUser = null; ;
                }
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                LoggedUser = context.GetUserByEmail(email);
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();
                if (LoggedUser != null && password == LoggedUser.Pswrd)
                {
                        loggedIn = true;
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
            while (c != 'B' && c != 'b')
            {
                //Clear screen
                ClearScreenAndSetTitle("Signup");

                Console.Write("Please Type your email: ");
                string email = Console.ReadLine();
                bool emailValid = IsEmailValid(email);
                bool emailExists = context.DoesUserExist(email);
                while (!(emailValid && emailExists))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (!emailValid) Console.Write("Bad Email Format! ");
                    else Console.Write("Email already exists! ");
                    Console.Write("Please try again: ");
                }
                LoggedUser.Email = email;
                Console.Write("Please Type your password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.ForegroundColor= ConsoleColor.Red;  
                    Console.Write("password must be at least 8 characters! Please try again: ");
                    Console.ResetColor();   
                    password = Console.ReadLine();
                }
                LoggedUser.Pswrd = password;
                Console.Write("Please Type your Name: ");
                string name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("name must be at least 2 characters! Please try again: ");
                    Console.ResetColor();
                    name = Console.ReadLine();
                }
                LoggedUser.Username = name;
                LoggedUser.Points = 0;
                LoggedUser.Questionsadded = 0;
                LoggedUser.Rankid = 3;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Connecting to Server...");
                Console.ResetColor();
                try
                {
                    context.Users.Add(LoggedUser);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                Console.ResetColor();
                }
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
            if (LoggedUser.Rankid == 1 || LoggedUser.Rankid == 2)
            {
                ClearScreenAndSetTitle("Pending Questions");
                char x = '5';
                foreach (Question q in context.Questions)
                {
                    if (q.StatusId == 2)
                    {
                        Console.WriteLine($"Question: {q.Text}");
                        Console.WriteLine($"Correct Answer: {q.RightAnswer}");
                        Console.WriteLine($"Wrong Answer #1: {q.WrongAnswer1}");
                        Console.WriteLine($"Wrong Answer #2: {q.WrongAnswer2}");
                        Console.WriteLine($"Wrong Answer #3: {q.WrongAnswer3}");
                        Console.WriteLine("Press 1 to aprove ,Press 2 to reject, Press 3 to skip, Press 4 to exit");

                        while (x == '5')
                        {
                            x = Console.ReadKey().KeyChar;
                            if (x == '1')
                                q.StatusId = 1;
                            else if (x == '2')
                                q.StatusId = 3;
                            else if (x == '3')
                                q.StatusId = 2;
                            else if (x == '4')
                            {
                                context.SaveChanges();
                                return;
                            }
                            else x = '5';

                        }


                    }
                }
                context.SaveChanges();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{"You do not have premission to show this page",65}");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
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
