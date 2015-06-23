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
        bool LogoffUser(int userId);
    }

    public interface IUserServiceCallback
    {
    }
}
