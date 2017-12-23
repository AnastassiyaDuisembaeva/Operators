using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provaders.DAL.Operator.Interface
{
    public interface IOperator
    {
        List <Prefics> prefics { get; set; }
        string logo { get; set; }
        string nameOperator { get; set; }
        double procent { get; set; }
    }
}
