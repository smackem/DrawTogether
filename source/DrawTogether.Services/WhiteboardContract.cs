using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    public class WhiteboardContract
    {
        public WhiteboardContract(int id, string name, int width, int height)
        {
            Id = id;
            Name = name;
            Width = 800;
            Height = 600;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int[] AttachedUsers { get; set; }
        public FigureContract[] Figures { get; set; }
    }
}
