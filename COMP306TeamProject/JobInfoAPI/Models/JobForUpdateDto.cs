using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobInfoAPI.Models
{
    public class JobForUpdateDto
    {      
        public string JobTitle { get; set; }
        public string Discription { get; set; }
        public string JobCategory { get; set; }
    }
}
