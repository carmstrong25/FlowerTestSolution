using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerTestApp.Data.Model
{
    public class Flower
    {
        [Key]
        public int FlowerId { get; set; }

        [Required(ErrorMessage = "Pedal Length is Required.")]
        [Range(0, 10, ErrorMessage = "Please enter 1 or 10 for Pedal Length")]
        public double PedalLength { get; set; }

        [Required(ErrorMessage = "Pedal Width is Required.")]
        [Range(0, 10, ErrorMessage = "Please enter 1 or 10 for Pedal Width")]
        public double PedalWidth { get; set; }

        [Required(ErrorMessage = "Type is Required.")]
        [Range(0, 1, ErrorMessage = "Please enter 1 or 0 for Flower Type")]
        public double Type { get; set; }
    }
}
