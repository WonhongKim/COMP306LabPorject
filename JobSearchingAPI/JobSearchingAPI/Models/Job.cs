﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchingAPI.Models
{
    public class Job
    {
        public string skillSet { get; set; }
        public string jobTitle { get; set; }
        public string jobSalary { get; set; }
        public string compnay { get; set; }
        public string WorkHours { get; set; }
        public string location { get; set; }
        public string jobDescription { get; set; }
    }
}
