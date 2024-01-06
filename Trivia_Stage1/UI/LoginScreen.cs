using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia_Stage1.UI
{
    class LoginScreen : Screen
    {
        public LoginScreen() : base("Login     ")
        {

        }

        public override void Show()
        {
            //Clear screen and set title (implemented by Screen Show)
            base.Show();

            if (Program.ui.Screens.ShowLogin())
            {
                //Show main menu once user is logged in
                MainMenu menu = new MainMenu();
                menu.Show();
            }
            
            
        }
    }
}
