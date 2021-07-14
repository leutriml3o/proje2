using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.Entities
{
    public class CalorieCalculator
    {
        [Required]
        public CalorieCalculatorGender CalorieCalculatorGender { get; set; }
        [Required]
        public double CalorieCalculatorAge { get; set; }
        [Required]
        public double CalorieCalculatorWeight { get; set; }
        [Required]
        public double CalorieCalculatorHeight { get; set; }
        [Required]
        public CalorieCalculatorActivity CalorieCalculatorActivity { get; set; }
        [Required]
        public double CalorieCalculatorBodyFat { get; set; }

    }
    public enum CalorieCalculatorGender
    {
        Male,
        Female
    }
    public enum CalorieCalculatorActivity
    {
        Sedentary,
        LightExercise,
        ModerateExercise,
        HeavyExercise,
        Athlete
    }
}

