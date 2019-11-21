using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobInfoAPI.Models
{
    public class JobForCreateDto
    {
   
        [Required(ErrorMessage = "You should provide a name value.")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "You should provide a name value.")]
        public string Discription { get; set; }
        [Required(ErrorMessage = "You should provide a name value.")]
        public string JobCategory { get; set; }
    }
}
