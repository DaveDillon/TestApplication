using Models;
using Models.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace LogicLayer
{
    public class DataLayer : IDataLayer
    {

        private readonly IConfiguration _configuration;
        private readonly string ConnString;


        public DataLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnString = _configuration.GetConnectionString("db");
        }


        public IEnumerable<ApplicationUser> GetUser(int userId)
        {
            var rtnValue = new List<ApplicationUser>();
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                var parameters = new { UserId = userId }; // No SQL Injection here.
                rtnValue = db.Query<ApplicationUser>(@"
                SELECT 
                    UserID,
                    UserName,
                    DisplayName,
                    Active 
                FROM 
                    [dev_User] 
                WHERE 
                    UserID = @UserID", parameters).ToList();
             }
             return rtnValue;
        }


        public IEnumerable<ApplicationUser> AuthenticateUser(string username, string password)
        {
            var rtnValue = new List<ApplicationUser>();
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                // I'd prefer a stored pocedure in production code for maintainability.
                var parameters = new { UserName = username, Password = password }; // No SQL Injection here.
                rtnValue = db.Query<ApplicationUser>(@"
                SELECT 
                    UserID,
                    UserName,
                    DisplayName,
                    Active 
                FROM 
                    [dev_User] 
                WHERE 
                    UserName = @UserName 
                    AND Password = @Password 
                    AND active = 1", parameters).ToList();
            }
            return rtnValue;
        }

      

        public IEnumerable<LossType> GetLossTypeDateForUser(int userId)
        {
            var rtnValue = new List<LossType>();
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                rtnValue = db.Query<LossType>("Select LossTypeID,LossTypeCode,LossTypeDescription From dev_LossType").ToList();

            }
            return rtnValue;
        }

      
    }
}
