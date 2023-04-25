using System;
using System.ComponentModel.DataAnnotations;
using yacht_harbour.Data;
namespace yacht_harbour.Models
{
    public class OrderFront
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime start_date { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime end_date { get; set; }

        [Required]
        public bool isAccepted { get; set; }

        [Required]
        public bool isReleased { get; set; }

        [Required]
        public int YachtId { get; set; }
        [Required]
        public int SailorId { get; set; }
        [Required]
        public orderType.option orderType { get; set; }

    }
}
