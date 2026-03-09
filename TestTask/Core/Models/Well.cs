using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Models
{
    public class Well
    {
        public string WellId { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }

        public List<Intervals> Intervals { get; set; } = new();
    }
}
