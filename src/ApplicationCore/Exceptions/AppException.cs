﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException() : base() { }
        
        public AppException(string message) : base(message) { }

        public AppException(string message, Exception innerException) : base(message, innerException) { }
    }
}
