using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPM1ProcessPension.Entiries
{
    public class PensionerInput
    {
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public string PAN { get; set; }
        public int AaadharNumber { get; set; }
        public string PensionType { get; set; }
    }
}
