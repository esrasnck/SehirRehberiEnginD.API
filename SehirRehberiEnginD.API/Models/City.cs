using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberiEnginD.API.Models
{
    public class City
    {
        public City()
        {
            Photos = new List<Photo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

      
        // relational properties
        public int UserId { get; set; }
        public List<Photo> Photos { get; set; }
        public User User { get; set; }

    }
}
