using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {
        TriviaContext context = new TriviaContext();
        Random rand = new Random();
        //Place here any state you would like to keep during the app life time
        //For example, player login details...


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
            List<int> = new List<int>();
            Question question = context.GetRandomQuestion();
            Console.WriteLine(question.Text);
            string[] answers = new string[4];
            answers[0] = question.RightAnswer;
            answers[1] = question.WrongAnswer1;
            answers[2] = question.WrongAnswer2;
            answers[3] = question.WrongAnswer3;
            int[] answerNum = new int[4];
            answerNum[0] = rand.Next(0, 4);
            answerNum[1] = rand.Next(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (answerNum[1] == answerNum[0])
                {
                    if (answerNum[1] == 0) answerNum[1] = 4;
                    else answerNum[1]--;
                }
            }
            answerNum[2] = rand.Next(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (answerNum[2] == answerNum[0] || answerNum[2] == answerNum[1])
                {
                    if (answerNum[2] == 0) answerNum[2] = 4;
                    else answerNum[2]--;
                }
            }
            answerNum[3] = rand.Next(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (answerNum[3] == answerNum[0] || answerNum[3] == answerNum[1] || answerNum[3] == answerNum[2])
                {
                    if (answerNum[3] == 0) answerNum[3] = 4;
                    else answerNum[3]--;
                }
            }
            Console.WriteLine("answer 1 - "+answers[answerNum[0]]);
            Console.WriteLine("answer 2 - " + answers[answerNum[1]]);
            Console.WriteLine("answer 3 - " + answers[answerNum[2]]);
            Console.WriteLine("answer 4 - " + answers[answerNum[3]]);
            Console.WriteLine("Enter the number of the correct answer");

            int answer=-1;
            while (answer < 0 || answer > 4)
            {
                Console.WriteLine("Enter integer between 1 - 4");
                while (!int.TryParse(Console.ReadLine(), out answer))
                    Console.WriteLine("Enter integer between 1 - 4");
            }

            if (answerNum[answer] == 0)
            {
                Console.WriteLine("Correct! Well done");
            }
            else
            {
                Console.WriteLine("Wrong. Maybe next time");
            }

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
