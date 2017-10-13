using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FridgeRestServer.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}