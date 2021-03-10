using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.AspNetCore.Idendity;

namespace Models
{
    public class ApplicationUser 
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string MyPrDisplayNameoperty { get; set; }
        public bool Active { get; set; }

    }
}
