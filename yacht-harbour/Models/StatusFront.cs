using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace yacht_harbour.Models
{
    public class StatusFront
    {

        [Display(Name = "Informacje")]
        [StringLength(60, ErrorMessage = "Za Długie.")]
        
        public string condition { get; set; }

        [Display(Name = "Możliwość pływania")]
        [Required]
        public bool sailling_possibility { get; set; }

        [Display(Name = "Data")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime status_date { get; set; }
    }
}
