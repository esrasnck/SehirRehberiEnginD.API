using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberiEnginD.API.Models
{
    public class User
    {
        public User()
        {
            Cities = new List<City>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        //relational properties

        public List<City> Cities { get; set; }
    }
}
