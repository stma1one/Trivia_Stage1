using Microsoft.EntityFrameworkCore;
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
                return this.Users.Where(user => user.Email == email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;// new Exception("Couldn't connect to server");
            }
        }
        public User? GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                return this.Users.Where(user => user.Email == email && user.Pswrd == password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't connect to server");
            }
        }
        public Question? GetRandomQuestion()
        {
            try
            {
                return this.Questions.Where(x => x.StatusId == 1).ToList().OrderBy(x => Random.Shared.Next()).FirstOrDefault();
            }
            catch (Exception ex) {
                throw new Exception("Couldn't connect to server");
            }
        }
        public Question GetRandomQuestion(List<int> list)
        {
            Random rand = new Random();
            int q = rand.Next(0, this.Questions.Count());
            return this.Questions.Where(x => x.Id == q).First();
        }
        public bool AddQ(Question q)
        {
            try
            {
                this.Questions.Add(q);

                this.SaveChanges();
                return true;
            }
            catch 
            {
                Console.WriteLine("an eror has aquered Quistion wan't be added point wont be removed ");
                if (this.Questions.Contains(q))
                    this.Questions.Remove(q);
                return false;
            }
        }
        public bool EditUser(User user, Question q)
        {

            try
            {
                if (user.Points != null)
                    user.Points = 0;
                if (user.Questionsadded != null)
                    user.Questionsadded++;
                this.Users.Update(user);
                this.SaveChanges();
                return true;
            }
            catch
            {
                if (this.Questions.Contains(q))
                    this.Questions.Remove(q);
                if (user.Points != null)
                    user.Points = 100;
                if (user.Questionsadded != null)
                    user.Questionsadded--;
                this.Users.Update(user);
                user.Questions.Add(q);
                this.SaveChanges();
                return false;
                
            }
            
        }


    }
}
