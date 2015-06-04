using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Model
{
    public class Whiteboard
    {
        public Whiteboard(string name)
        {
            Name = name;
            Width = 800;
            Height = 600;
        }

        [Key]
        public string Name { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
