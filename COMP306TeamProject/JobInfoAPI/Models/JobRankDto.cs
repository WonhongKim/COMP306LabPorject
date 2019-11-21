﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobInfoAPI.Models
{
    public class JobRankDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Rank { get; set; }
        public string BestLocations { get; set; }
        public string WorstLocations { get; set; }
    }
}
