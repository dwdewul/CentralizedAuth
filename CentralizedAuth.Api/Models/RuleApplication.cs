using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralizedAuth.Api.Models
{
    public class RuleApplication
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Revision { get; set; }
        public bool IsActive { get; set; }
    }
}
