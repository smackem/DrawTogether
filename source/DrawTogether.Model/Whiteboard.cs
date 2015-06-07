﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Model
{
    public class Whiteboard
    {
        readonly int id;

        public Whiteboard(int id, string name)
        {
            this.id = id;

            Name = name;
            Width = 800;
            Height = 600;
        }

        [Key]
        public int Id
        {
            get { return this.id; }
        }

        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public void AttachUser(int userId)
        {
        }

        public void DetachUser(int userId)
        {
        }
    }
}
