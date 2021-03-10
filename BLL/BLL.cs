using System;
using Extensions;
using Models;
using Models.Interface;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace LogicLayer
{
    public class BLL : IBLL
    {
        private IDataLayer _dlayer;
        public BLL(IDataLayer dataLayer)
        {
            _dlayer = dataLayer;
        }

        public IEnumerable<ApplicationUser> GetUser(int userId)
        {
            return _dlayer.GetUser(userId);
        }

        public bool AuthenticateUser(string username, string password, out ApplicationUser userModel)
        {
            var user = _dlayer.AuthenticateUser(username, password);
            userModel = new ApplicationUser();

            if (user.Any())
            {
                userModel = user.First();
                return true;
            }

            return false;
        }

        public IEnumerable<LossType> GetLossTypeDateForUser(int userId)
        {
           return  _dlayer.GetLossTypeDateForUser(userId);
        }
    }
}
