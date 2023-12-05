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
                while (!emailValid || !emailExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (!emailValid) Console.Write("Bad Email Format! ");
                    else Console.Write("Email already exists! ");
                    Console.Write("Please try again: ");
                    Console.ResetColor();
                    email = Console.ReadLine();
                }
                LoggedUser.Email = email;
                Console.Write("Please Type your password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
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
            if (LoggedUser.)
            {
                Console.WriteLine("if you wants to return at any point type -1");

                Console.WriteLine("add Question text");
                Question q = new Question();
                Subject subject = new Subject();
                Console.WriteLine("choose a subject 1-sports,2-Politics,3-history,4-sience,5-ramon,");
                int y = 0;
                while (y == 0)
                {
                    try
                    {
                        y = int.Parse(Console.ReadLine());
                    }
                    catch { y = 0; Console.WriteLine("you wrote something incorect"); }
                    if (y == 1)
                        subject.SubjectName = "sports";
                    if (y == 2)
                        subject.SubjectName = "Politics";
                    if (y == 3)
                        subject.SubjectName = "history";
                    if (y == 4)
                        subject.SubjectName = "sience";
                    if (y == 5)
                        subject.SubjectName = "ramon";
                    if (y == -1)
                        return;
                    else y = 0;
                }
                string x;
                q.Text = Console.ReadLine();
                Console.WriteLine("add rightanswer");
                x = Console.ReadLine();
                if (x == "-1") return;
                q.RightAnswer = x;
                Console.WriteLine("add wrong answer one:");
                x = Console.ReadLine();
                if (x == "-1") return;
                q.WrongAnswer1 = x;
                Console.WriteLine("add wrong answer two:");
                x = Console.ReadLine();
                if (x == "-1") return;
                q.WrongAnswer2 = x;
                Console.WriteLine("add wrong answer three:");
                x = Console.ReadLine();
                if (x == "-1") return;
                q.WrongAnswer3 = x;
                q.StatusId = 1;
                q.UserId = LoggedUser.Id;
                context.Questions.Add(q);

                context.SaveChanges();
                LoggedUser.Points = 0;
                LoggedUser.Questionsadded++;
            }
        }

        public void ShowPendingQuestions()
        {
            Console.WriteLine("pending questions");
            char x;
            x = '5';
            
            foreach (Question q in context.Questions)
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
