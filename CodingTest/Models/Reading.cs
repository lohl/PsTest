using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingTest.Models
{
    public class Reading
    {
        public int Id { get; set; }
        public decimal Depth { get; set; }
        [Range(0, 10)]
        public decimal MagX { get; set; }
        [Range(0, 10)]
        public decimal MagY { get; set; }
        [Range(0, 10)]
        public decimal MaxZ { get; set; }
        [NotMapped]
        public decimal TotalMag { get
            {
                return MagX + MagY + MaxZ;
            }
        }
    }


}