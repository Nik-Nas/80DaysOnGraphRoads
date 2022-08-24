using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCampFinalProject.Code.Utils
{
    public class Triplet<TKey, TValue, TArgument>
    {
        public readonly TKey Key;
        public readonly TValue Value;
        public readonly TArgument Argument;
        public Triplet(TKey key, TValue value, TArgument argument)
        {
            Key = key;
            Value = value;
            Argument = argument;
        }
    }
}
