using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingTest.Models
{

    public class ReadingGrav
    {
        public int Id { get; set; }
        [Range(0, 10)]
        public decimal Depth { get; set; }
        public decimal GravX { get; set; }
        [Range(0, 10)]
        public decimal GravY { get; set; }
        [Range(0, 10)]
        public decimal GravZ { get; set; }
        [NotMapped]
        public decimal TotalGrav => GravZ == 0m ? 0m : (GravX + GravY) / GravZ;
    }
}