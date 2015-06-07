using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Model
{
    public class User
    {
        readonly int id;
        readonly string name;

        public User(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        [Key]
        public int Id
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}
