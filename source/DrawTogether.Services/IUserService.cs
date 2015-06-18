using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    public interface IUserService
    {
        UserContract RegisterUser(string userName);
        void LogoffUser(UserContract user);
    }

    public interface IUserServiceCallback
    {
    }
}
