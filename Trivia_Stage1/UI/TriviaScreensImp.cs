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
        Dictionary<string, string> ranks = new Dictionary<string, string>(){
            { "1", "Admin" },
            { "2", "Master" },
            { "3", "Rookie" }
        };
        public string CheckUsernameValidity()
        {
            string username = Console.ReadLine();
            while (!IsNameValid(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Username must be at least 2 characters! Please try again: ");
                Console.ResetColor();
                username = Console.ReadLine();
            }
            return username;
        }
        public string CheckPasswordValidity()
        {
            string password = Console.ReadLine();
            while (!IsPasswordValid(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Password must be at least 8 characters! Please try again: ");
                Console.ResetColor();
                password = Console.ReadLine();
            }
            return password;
        }
        public string CheckEmailValidity()
        {
            string email = Console.ReadLine();
            bool emailValid = IsEmailValid(email);
            while (!(emailValid && !context.DoesUserExist(email)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (!emailValid) Console.Write("Bad Email Format! ");
                else Console.Write("Email already exists! ");
                Console.Write("Please try again: ");
                Console.ResetColor();
                email = Console.ReadLine();
                emailValid = IsEmailValid(email);
            }
            return email;
        }
        public bool ShowLogin()
        {
            LoggedUser = null;
            while (LoggedUser == null)
            {
                User testedUser = null;
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();
                if (context.DoesUserExist(email))
                    testedUser = context.GetUserByEmail(email);
                if (testedUser != null && password == testedUser.Pswrd)
                {
                    LoggedUser = testedUser;
                }
                else
                {
                    ClearScreenAndSetTitle("Login");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Email or Password is incorrect. Wanna try again? (Y/n) ");
                    char command = Console.ReadKey().KeyChar;
                    if (command.ToString().ToUpper() == "N") return false;
                    Console.ResetColor();
                    ClearScreenAndSetTitle("Login");
                }
            }
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
            LoggedUser = new User();
            char c = ' ';
            while (c != 'B' && c != 'b')
            {
                //Clear screen
                ClearScreenAndSetTitle("Signup");

                Console.Write("Please Type your email: ");
                string email = CheckEmailValidity();
                LoggedUser.Email = email;
                Console.Write("Please Type your password: ");
                string password = CheckPasswordValidity();
                LoggedUser.Pswrd = password;
                Console.Write("Please Type your username: ");
                string username = CheckUsernameValidity();
                LoggedUser.Username = username;
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
            if (LoggedUser.Rankid == 1 || LoggedUser.Rankid == 2)
            {
                Console.Write("Add the Question's Text (B to go back): ");
                string qText = Console.ReadLine();
                Question q = new Question();
                if (qText.ToUpper() == "B")
                    return;
                q.Text = qText;
                Console.WriteLine("Choose a Subject 1 - Sports, 2 - Politics, 3 - History, 4 - Sience, 5 - Ramon");
                int y = 0;
                while (y == 0)
                {
                    while (!(int.TryParse(Console.ReadLine(), out y)))
                    {
                        Console.WriteLine("You Need to Type a Number");
                    }
                    if (y == 1)
                        q.SubjectId = 1;
                    else if (y == 2)
                        q.SubjectId = 2;
                    else if (y == 3)
                        q.SubjectId = 3;
                    else if (y == 4)
                        q.SubjectId = 4;
                    else if (y == 5)
                        q.SubjectId = 5;
                    else y = 0;
                }
                string x;
                Console.Write("Add the Correct Answer: ");
                x = Console.ReadLine();
                q.RightAnswer = x;
                Console.Write("Add Wrong Answer #1: ");
                x = Console.ReadLine();
                q.WrongAnswer1 = x;
                Console.Write("Add Wrong Answer #2: ");
                x = Console.ReadLine();
                q.WrongAnswer2 = x;
                Console.Write("Add Wrong Answer #3: ");
                x = Console.ReadLine();
                q.WrongAnswer3 = x;
                q.StatusId = 2;
                q.UserId = LoggedUser.Id;
                context.Questions.Add(q);

                context.SaveChanges();
                LoggedUser.Points = 0;
                LoggedUser.Questionsadded++;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{"You Do Not Have Permission to View This Page",80}");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Press Any Key to Continue");
                Console.ReadKey();
            }
        }

        public void ShowPendingQuestions()
        {
            if (LoggedUser.Rankid == 1 || LoggedUser.Rankid == 2)
            {
                ClearScreenAndSetTitle("Pending Questions");
                
                foreach (Question q in context.Questions)
                {
                    char x = '5';
                    if (q.StatusId == 2)
                    {
                        ClearScreenAndSetTitle("Pending Questions");
                        Console.WriteLine($"Question: {q.Text}");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Correct Answer: {q.RightAnswer}");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Wrong Answer #1: {q.WrongAnswer1}");
                        Console.WriteLine($"Wrong Answer #2: {q.WrongAnswer2}");
                        Console.WriteLine($"Wrong Answer #3: {q.WrongAnswer3}");
                        Console.ResetColor();
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
                Console.WriteLine($"{"You Do Not Have Permission to View This Page",80}");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Press Any Key to Continue");
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
            ClearScreenAndSetTitle("Your profile:");
            Console.WriteLine("Current Email Address: "+LoggedUser.Email);
            Console.WriteLine("Current Password: "+LoggedUser.Pswrd);
            Console.WriteLine("Current Points: " + LoggedUser.Points);
            Console.WriteLine("Current Rank: " + ranks[LoggedUser.Rankid.ToString()]);
            Console.Write("Change (E)mail Address/(U)sername/(P)assword (anything else to go back) ");
            char command = Console.ReadKey().KeyChar;
            Console.Clear();
            Console.Write("Insert new ");
            switch (command.ToString().ToUpper())
            {
                case "E":
                    Console.Write("email: ");
                    string email = CheckEmailValidity();
                    LoggedUser.Email = email;
                    break;
                case "U":
                    Console.Write("username: ");
                    string username = CheckUsernameValidity();
                    LoggedUser.Username = username;
                    break;
                case "P":
                    Console.Write("password: ");
                    string password = CheckPasswordValidity();
                    LoggedUser.Pswrd = password;
                    break;
                default:
                    return;
            }
            context.SaveChanges();
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

        private bool IsNameValid(string username)
        {
            return !string.IsNullOrEmpty(username) && username.Length >= 2;
        }
    }
}
