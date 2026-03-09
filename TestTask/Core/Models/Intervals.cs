using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Intervals
    {
        public double DepthFrom {  get; set; }
        public double DepthTo { get; set; }
        public string Rock { get; set; } = "";
        public double Porosity { get; set; }
    }
}
