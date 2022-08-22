using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCampFinalProject.Code.Utils
{
    class Triplet<AValue, BValue, CValue>
    {
        public readonly AValue aValue;
        public readonly BValue bValue;
        public readonly CValue cValue;
        public Triplet(AValue a, BValue b, CValue c)
        {
            aValue = a;
            bValue = b;
            cValue = c;
        }
    }
}
