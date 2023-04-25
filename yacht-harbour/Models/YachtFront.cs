using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace yacht_harbour.Models
{
    public class YachtFront
    {
        [Required]
        public int IdYacht { get; set; }

        [Required]
        public string Yacht_name { get; set; }

        [Required]
        public string Type { get; set; }




        [Required]
        public float Length { get; set; }
        [Required]
        public float Width { get; set; }
        [Required]
        public float Draft { get; set; }
        [Required]
        public float Sailed_Surface { get; set; }
        [Required]
        public int Crew_Number { get; set; }

        public string Remarks{ get; set; }



    }
}
