using DrawTogether.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    abstract class Figure
    {
        readonly object sync = new object();
        readonly WeakReference<User> user;

        protected Figure(User user, Argb argb)
        {
            this.user = new WeakReference<User>(user);

            Argb = argb;
        }

        public Argb Argb { get; set; }

        public User User
        {
            get { return this.user.GetTarget(); }
        }

        public abstract TResult Accept<TState, TResult>(
            IFigureVisitor<TState, TResult> visitor, TState state);
    }
}
