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
        Dictionary<string, int> answersDict = new Dictionary<string, int>(){
            { "A", 1 },
            { "B", 2 },
            { "C", 3 },
            { "D", 4 }
        };
        public string CheckUsernameValidity()
        {
            string username = Console.ReadLine();
            if (username.ToUpper() == "B") //if the value that was entered is "B"\"b" the program will go to the previous screen (later when used)
                return username;
            while (!IsNameValid(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Username must be at least 2 characters! please try again: ");
                Console.ResetColor();
                username = Console.ReadLine();
            } // loops when username isn't valid
            return username;
        }
        public string CheckPasswordValidity()
        {
            string password = Console.ReadLine();
            if (password.ToUpper() == "B") //if the value that was entered is "B"\"b" the program will go to the previous screen (later when used)
                return password;
            while (!IsPasswordValid(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Password must be at least 8 characters! please try again: ");
                Console.ResetColor();
                password = Console.ReadLine();
            } // loops when password ins't valid
            return password;
        }
        public string CheckEmailValidity()
        {
            string email = Console.ReadLine();
            if (email.ToUpper() == "B") //if the value that was entered is "B"\"b" the program will go to the previous screen (later when used)
                return email;
            bool emailValid = IsEmailValid(email);
            while (!(emailValid && !context.DoesUserExist(email))) //checks if the email is in the wrong format and exists, if so, requests to enter the email again
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (!emailValid) Console.Write("Bad email format! ");
                else Console.Write("Email already exists! ");
                Console.Write("Please try again: ");
                Console.ResetColor();
                email = Console.ReadLine();
                if (email.ToUpper() == "B")
                    return email;
                emailValid = IsEmailValid(email);
            } // loops when email isn't valid or exists
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
                } // changed current logged user to login details if they are correct
                else
                {
                    ClearScreenAndSetTitle("Login               ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Email or password is incorrect. Wanna try again? (Y/n) ");
                    Console.ResetColor();
                    char command = Console.ReadKey().KeyChar;
                    if (command.ToString().ToUpper() == "N") return false;
                    Console.ResetColor();
                    ClearScreenAndSetTitle("Login");
                    ClearScreenAndSetTitle("Login               ");
                } // email/password are invalid
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
            while (c.ToString().ToUpper() != "B")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Press \"B\" at any point to go back to the main menu");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("Please type your email: ");
                string email = CheckEmailValidity();
                if (email.ToUpper() == "B") //goes to previous screen if entered value is "B"/"b"
                    return false;
                LoggedUser.Email = email;
                Console.Write("Please type your password: ");
                string password = CheckPasswordValidity();
                if (password.ToUpper() == "B")//goes to previous screen if entered value is "B"/"b"
                    return false;
                LoggedUser.Pswrd = password;
                Console.Write("Please type your username: ");
                string username = CheckUsernameValidity();
                if (username.ToUpper() == "B")//goes to previous screen if entered value is "B"/"b"
                    return false;
                LoggedUser.Username = username;
                LoggedUser.Points = 0;
                LoggedUser.Questionsadded = 0;
                LoggedUser.Rankid = 3;
                // sets user details
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Connecting to Server...");
                Console.ResetColor();
                try
                {
                    context.Users.Add(LoggedUser);
                    context.SaveChanges();
                    return true;
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
            if (LoggedUser.Points == 100 || LoggedUser.Rankid == 1)
            {
                Console.ForegroundColor= ConsoleColor.DarkBlue;
                Console.Write("Add the question's text (B to go back): ");
                Console.ResetColor();
                string qText = Console.ReadLine();
                Question q = new Question();
                if (qText.ToUpper() == "B")
                    return;
                q.Text = qText;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Choose a subject: 1 - Sports, 2 - Politics, 3 - History, 4 - Science, 5 - Ramon: ");
                Console.ResetColor();
                char y = '0'; //the index of the subject
                while (y == '0')
                {
                    y = Console.ReadKey().KeyChar;
                    if (y == '1')
                        q.SubjectId = 1;
                    else if (y == '2')
                        q.SubjectId = 2;
                    else if (y == '3')
                        q.SubjectId = 3;
                    else if (y == '4')
                        q.SubjectId = 4;
                    else if (y == '5')
                        q.SubjectId = 5;
                    else y = '0';
                } // choosing a subject
                Console.WriteLine();
                string x;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Add the correct answer: ");
                Console.ResetColor();
                x = Console.ReadLine();
                q.RightAnswer = x;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Add wrong answer #1: ");
                Console.ResetColor();
                x = Console.ReadLine();
                q.WrongAnswer1 = x;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Add wrong answer #2: ");
                Console.ResetColor();
                x = Console.ReadLine();
                q.WrongAnswer2 = x;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Add wrong answer #3: ");
                Console.ResetColor();
                x = Console.ReadLine();
                q.WrongAnswer3 = x;
                q.StatusId = 2;
                q.UserId = LoggedUser.Id;
                context.Questions.Add(q);
                context.SaveChanges();
                // adds question to db
                LoggedUser.Points = 0;
                LoggedUser.Questionsadded++;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{"You do not have permission to view this page",80}");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } // no permissions
        }

        public void ShowPendingQuestions()
        {
            if (LoggedUser.Rankid == 1 || LoggedUser.Rankid == 2)
            { 
                foreach (Question q in context.Questions)
                {
                    char x = '5'; //an index that will be changed back to '5' if a wrong value is entered
                    if (q.StatusId == 2)
                    {
                        ClearScreenAndSetTitle("Pending Questions         ");
                        Console.WriteLine($"Question: {q.Text}");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Correct Answer: {q.RightAnswer}");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Wrong answer #1: {q.WrongAnswer1}");
                        Console.WriteLine($"Wrong answer #2: {q.WrongAnswer2}");
                        Console.WriteLine($"Wrong answer #3: {q.WrongAnswer3}");
                        Console.ResetColor();
                        Console.WriteLine("Press 1 to approve ,Press 2 to reject, Press 3 to skip, Press 4 to exit");
                        // printing text
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

                        } // switch statement for approval options


                    }
                }
                context.SaveChanges();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{"You do not have permission to view this page",80}");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            } // no permission
        }
        public void ShowGame()
        {
            while (true)
            {
                ClearScreenAndSetTitle("Game On           ");
                Question question = context.GetRandomQuestion();
                List<string> answerList = new List<string>()
                {question.RightAnswer, question.WrongAnswer1, question.WrongAnswer2, question.WrongAnswer3};
                answerList = answerList.OrderBy(x => Random.Shared.Next()).ToList(); // randomizing order of answers
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"{question.Text, 80}");
                Console.ResetColor();
                Console.WriteLine("A. " + answerList[0]);
                Console.WriteLine("B. " + answerList[1]);
                Console.WriteLine("C. " + answerList[2]);
                Console.WriteLine("D. " + answerList[3]);
                Console.Write("Write the letter of the correct answer (or B to go back): ");
                // printing question
                string answer = Console.ReadKey().KeyChar.ToString().ToUpper();
                // getting answer
                while (!answersDict.ContainsKey(answer))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThis letter isn't a command. Please try again: ");
                    Console.ForegroundColor= ConsoleColor.White;
                    answer = Console.ReadKey().KeyChar.ToString().ToUpper();
                } // wrong letter
                if (answerList[answersDict[answer]-1] == question.RightAnswer)
                {
                    ClearScreenAndSetTitle("You are correct! the answer is indeed " + question.RightAnswer);
                    LoggedUser.Points += 10;
                } // correct answer
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{("You are wrong! The answer is " + question.RightAnswer),65}");
                    Console.WriteLine();
                    Console.ResetColor();
                    LoggedUser.Points -= 5;
                } //wrong answer
                if (LoggedUser.Points > 100) LoggedUser.Points = 100;
                if (LoggedUser.Points < 0) LoggedUser.Points = 0;
                Console.WriteLine($"\t\t\t\tYour current points: {LoggedUser.Points}");
                Console.Write("\t\t\t\t\tWanna play again? (y/N) ");
                char command = Console.ReadKey().KeyChar;
                if (command.ToString().ToUpper() != "Y")
                {
                    context.GetUserByEmail(LoggedUser.Email).Points = LoggedUser.Points;
                    return;
                } // adding/removing points from user
            }
        }
        public void ShowProfile()
        {
            string currEmail = LoggedUser.Email;
            ClearScreenAndSetTitle("Your profile:             ");
            Console.WriteLine("Email Address: " + LoggedUser.Email);
            Console.WriteLine("Password: " + LoggedUser.Pswrd);
            Console.WriteLine("Username: " + LoggedUser.Username);
            Console.WriteLine("Current Points: " + LoggedUser.Points);
            Console.WriteLine("Rank: " + ranks[LoggedUser.Rankid.ToString()]);
            Console.Write("Change (E)mail Address/(U)sername/(P)assword (anything else to go back) ");
            // printing details
            char command = Console.ReadKey().KeyChar;
            ClearScreenAndSetTitle("Update Details           ");
            Console.Write("\tInsert new ");
            switch (command.ToString().ToUpper())
            {
                case "E":
                    Console.Write("Email: ");
                    string email = CheckEmailValidity();
                    if (email.ToUpper() == "B")
                    {
                        ShowProfile();
                        break;
                    }
                    context.GetUserByEmail(currEmail).Email = email;
                    LoggedUser.Email = email;
                    break;
                case "U":
                    Console.Write("Username: ");
                    string username = CheckUsernameValidity();
                    if (username.ToUpper() == "B")
                    {
                        ShowProfile();
                        break;
                    }
                    context.GetUserByEmail(currEmail).Username = username;
                    LoggedUser.Username = username;
                    break;
                case "P":
                    Console.Write("Password: ");
                    string password = CheckPasswordValidity();
                    if (password.ToUpper() == "B")
                    {
                        ShowProfile();
                        break;
                    }
                    context.GetUserByEmail(currEmail).Pswrd = password;
                    LoggedUser.Pswrd = password;
                    break;
                default:
                    return;
            } // changing email/username/password
            context.SaveChanges();
        }


        //Private helper methods down here...
        private void ClearScreenAndSetTitle(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{title,75}");
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
