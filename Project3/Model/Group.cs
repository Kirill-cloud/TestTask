using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Model
{
    public class Group
    {
        public int Id { get; set; }
        public DateTime SaveDateTime { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
