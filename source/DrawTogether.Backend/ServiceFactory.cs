﻿using DrawTogether.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    public class ServiceFactory : IDisposable
    {
        public IWhiteboardService CreateWhiteboardService(IWhiteboardServiceCallback callback)
        {
            var backend = BackendService.Instance;
            backend.RegisterCallback(callback);
            return backend;
        }

        public IUserService CreateUserService(IUserServiceCallback callback)
        {
            var backend = BackendService.Instance;
            backend.RegisterCallback(callback);
            return backend;
        }

        public void Dispose()
        {
            BackendService.Instance.Clear();
        }
    }
}
