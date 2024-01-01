using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia_Stage1.Models
{
    public partial class TriviaContext
    {
        public User? GetUserByEmail(string email)
        {
            try
            {
                return this.Users.Where(user => user.Email == email).First();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't connect to server");
            }
        }
        public User? GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                return this.Users.Where(user => user.Email == email && user.Pswrd == password).First();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't connect to server");
            }
        }
        public Question GetRandomQuestion()
        {
            Random rand = new Random();
            try
            {
                int q = rand.Next(1, this.Questions.Count()+1);
                return this.Questions.Where(x => x.Id == q).First();
            }
            catch (Exception ex) {
                throw new Exception("Couldn't connect to server");
            }
        }
    }
}
