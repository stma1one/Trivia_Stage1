using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia_Stage1.UI
{
    class SignupScreen : Screen
    {
        public SignupScreen() : base("Signup     ")
        {

        }

        public override void Show()
        {
            //Clear screen and set title (implemented by Screen Show)
            base.Show();

            if (Program.ui.Screens.ShowSignUp())
            {
                //Show main menu once user is logged in
                MainMenu menu = new MainMenu();
                menu.Show();
            }
        }
    }
}
