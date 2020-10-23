using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Validation
{
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            { }
        }
}
