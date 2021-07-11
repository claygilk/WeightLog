using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeightLog.Models
{
    public class Set
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int LiftId { get; set; }
        public int UserId { get; set; }
        
        [Range(0, Double.PositiveInfinity)]
        public int Weight { get; set; }
        
        [Range(0, Double.PositiveInfinity)]
        public int Reps { get; set; }
        
        [Range(1,11)]
        public int Rpe { get; set; }
    }
}
