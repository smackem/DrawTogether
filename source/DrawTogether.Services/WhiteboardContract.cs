using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    public class WhiteboardContract
    {
        public WhiteboardContract(int id, string name, int width, int height,
            int[] attachedUsers, FigureContract[] figures)
        {
            Id = id;
            Name = name;
            Width = width;
            Height = height;
            AttachedUsers = attachedUsers;
            Figures = figures;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int[] AttachedUsers { get; private set; }
        public FigureContract[] Figures { get; private set; }
    }
}
