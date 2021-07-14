using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sport.WebUI.Entities
{
    public class CalculateFatRate
    {
        [Required]
        public FatRateGender FatRateGender { get; set; }
        [Required]
        public double Height { get; set; } //boy
        [Required]
        public double Neck { get; set; } //boyun
        [Required]
        public double Waist { get; set; } //bel
        [Required]
        public double Hip { get; set; } //kalça
    }
    public enum FatRateGender
    {
        Male = 0,
        Female = 1
    }
}