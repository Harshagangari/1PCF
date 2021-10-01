using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalManagement.Entities
{
    public class PensionDetails
    {
        public enum PensionType { self, family }
        public double AadharNo { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PensionType pensionType { get; set; }
        public string PAN { get; set; }
    }
}
