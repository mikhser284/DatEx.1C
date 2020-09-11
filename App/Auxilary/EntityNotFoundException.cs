using System;
using System.Collections.Generic;
using System.Text;

namespace App.Auxilary
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(String message) : base(message) { }

        public EntityNotFoundException(String message, Exception innerException) : base(message, innerException) { }
    }
}
