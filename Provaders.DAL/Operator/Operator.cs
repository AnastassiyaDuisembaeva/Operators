using Provaders.DAL.Operator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provaders.DAL.Operator
{
    [Serializable]
    public class Operator : IOperator
    {
        
        public List<Prefics> prefics { get; set; }
        public string logo { get; set; }
        public string nameOperator { get; set; }
        public double procent { get; set; }
        private int Price { get; set; } = 5000;

    }
}
