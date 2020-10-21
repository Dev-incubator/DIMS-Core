using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Enums
{
    public enum Sex : byte
    {
        //International standard https://en.wikipedia.org/wiki/ISO/IEC_5218
        Undefined = 0,
        Male = 1,
        Female = 2,
        NotApplicable = 9
    }
}
