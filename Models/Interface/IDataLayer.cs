using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interface
{
    public interface IDataLayer
    {
        public IEnumerable<ApplicationUser> GetUser(int userId);

        public IEnumerable<ApplicationUser> AuthenticateUser(string username, string password);

        public IEnumerable<LossType> GetLossTypeDateForUser(int userId);
    }

   
}
