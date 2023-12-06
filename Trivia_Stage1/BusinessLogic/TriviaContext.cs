using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia_Stage1.Models
{
    public partial class TriviaContext
    {
        public Player Login(string username, string password, string email)
        {
            return this.Players.Where(p=>p.Name == username && p.Email == email && p.Password == password).FirstOrDefault();
        }
    }
}
