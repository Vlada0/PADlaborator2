using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PADLab2_1part.Validation
{
    public class ResourseAlreadyExistException: Exception
    {
        public ResourseAlreadyExistException(string message) : base(message)
        { }
    }
}
