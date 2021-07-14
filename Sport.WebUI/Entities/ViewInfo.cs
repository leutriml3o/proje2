using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.Entities
{
    public class ViewInfo
    {
        public double BasalMetabolic { get; set; }
        public double Sedentary { get; set; }
        public double LightExercise { get; set; }
        public double ModerateExercise { get; set; }
        public double HeavyExercise { get; set; }
        public double Athlete { get; set; }

        public double WeeklyCalories { get; set; }
        public double Selection { get; set; }


        public double maintenanceModerateProtein { get; set; }
        public double maintenanceModerateFats { get; set; }
        public double maintenanceModerateCarbs { get; set; }

        public double cuttingModerateProtein { get; set; }
        public double cuttingModerateFats { get; set; }
        public double cuttingModerateCarbs { get; set; }

        public double bulkingModerateProtein { get; set; }
        public double bulkingModerateFats { get; set; }
        public double bulkingModerateCarbs { get; set; }

        public double maintenanceLowerProtein { get; set; }
        public double maintenanceLowerFats { get; set; }
        public double maintenanceLowerCarbs { get; set; }

        public double cuttingLowerProtein { get; set; }
        public double cuttingLowerFats { get; set; }
        public double cuttingLowerCarbs { get; set; }

        public double bulkingLowerProtein { get; set; }
        public double bulkingLowerFats { get; set; }
        public double bulkingLowerCarbs { get; set; }

        public double maintenanceHigherProtein { get; set; }
        public double maintenanceHigherFats { get; set; }
        public double maintenanceHigherCarbs { get; set; }

        public double cuttingHigherProtein { get; set; }
        public double cuttingHigherFats { get; set; }
        public double cuttingHigherCarbs { get; set; }

        public double bulkingHigherProtein { get; set; }
        public double bulkingHigherFats { get; set; }
        public double bulkingHigherCarbs { get; set; }

        public double FHamwi { get; set; }
        public double FDevine { get; set; }
        public double FRobinson { get; set; }
        public double FMiller { get; set; }
        public double BMI { get; set; }
        public double max { get; set; }
        public double min { get; set; }
    }
}
