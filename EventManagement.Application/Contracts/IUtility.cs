﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Helpers.Utilities
{
    public interface IUtility
    {
        string GetUserIdFromJWTToken();

    }
}
