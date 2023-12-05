using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia_Stage1.Models
{
    public partial class TriviaContext
    {
        public bool DoesUserExist(string email)
        {
            return this.Users.Where(user => user.Email == email).Any();
        }
        public User GetUserByEmail(string email)
        {
            try
            {
                return this.Users.Where(user => user.Email == email).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
