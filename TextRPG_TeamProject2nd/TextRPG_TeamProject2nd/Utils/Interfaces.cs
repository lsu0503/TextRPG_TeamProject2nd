using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamProject2nd.Utils
{
    public interface IObject
    {
        int id { get; }
        string name { get; }
        string desc { get; }
    }

    public interface IClone<T>
    {
        T Clone();
    }
}
