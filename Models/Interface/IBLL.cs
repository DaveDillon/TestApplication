using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interface
{
   public interface IBLL
    {
        public IEnumerable<ApplicationUser> GetUser(int userId);

        public bool AuthenticateUser(string username, string password, out ApplicationUser userModel);

        public IEnumerable<LossType> GetLossTypeDateForUser(int userId);
    }
}
