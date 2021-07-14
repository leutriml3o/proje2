using Sport.WebUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport.WebUI.Models
{
    public class CalorieCalculation
    {
        public ViewInfo Calculator(CalorieCalculator calorieCalculator, ViewInfo viewInfo)
        {
            double computation;
            if (calorieCalculator.CalorieCalculatorGender == CalorieCalculatorGender.Male)
            {
                computation = Convert.ToDouble(700 + (9.6 * calorieCalculator.CalorieCalculatorWeight) + (1.7 * calorieCalculator.CalorieCalculatorHeight) - (4.7 * calorieCalculator.CalorieCalculatorAge));
                viewInfo.FHamwi = Math.Ceiling(48 + 2.7 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.FDevine = Math.Floor(50 + 2.3 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.FRobinson = Math.Ceiling(52 + 1.9 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.FMiller = Math.Ceiling(56.2 + 1.41 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.BMI = Math.Ceiling(calorieCalculator.CalorieCalculatorWeight / ((calorieCalculator.CalorieCalculatorHeight / 100) * (calorieCalculator.CalorieCalculatorHeight / 100)));

                viewInfo.max = viewInfo.FHamwi;
                if (viewInfo.FDevine > viewInfo.max)
                    viewInfo.max = viewInfo.FDevine;
                if (viewInfo.FRobinson > viewInfo.max)
                    viewInfo.max = viewInfo.FRobinson;
                if (viewInfo.FMiller > viewInfo.max)
                    viewInfo.max = viewInfo.FMiller;


                viewInfo.min = viewInfo.FHamwi;
                if (viewInfo.FDevine < viewInfo.min)
                    viewInfo.min = viewInfo.FDevine;
                if (viewInfo.FRobinson < viewInfo.min)
                    viewInfo.min = viewInfo.FRobinson;
                if (viewInfo.FMiller < viewInfo.min)
                    viewInfo.min = viewInfo.FMiller;
            }
            else
            {
                computation = Convert.ToDouble(550 + (9.6 * calorieCalculator.CalorieCalculatorWeight) + (1.7 * calorieCalculator.CalorieCalculatorHeight) - (4.7 * calorieCalculator.CalorieCalculatorAge));
                viewInfo.FHamwi = Math.Ceiling(45.5 + 2.2 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.FDevine = Math.Floor(45.5 + 2.3 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.FRobinson = Math.Ceiling(49 + 1.7 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.FMiller = Math.Ceiling(53 + 1.36 * ((calorieCalculator.CalorieCalculatorHeight / 2.54) - 60));
                viewInfo.BMI = Math.Ceiling(calorieCalculator.CalorieCalculatorWeight / ((calorieCalculator.CalorieCalculatorHeight / 100) * (calorieCalculator.CalorieCalculatorHeight / 100)));

                viewInfo.max = viewInfo.FHamwi;
                if (viewInfo.FDevine > viewInfo.max)
                    viewInfo.max = viewInfo.FDevine;
                if (viewInfo.FRobinson > viewInfo.max)
                    viewInfo.max = viewInfo.FRobinson;
                if (viewInfo.FMiller > viewInfo.max)
                    viewInfo.max = viewInfo.FMiller;


                viewInfo.min = viewInfo.FHamwi;
                if (viewInfo.FDevine < viewInfo.min)
                    viewInfo.min = viewInfo.FDevine;
                if (viewInfo.FRobinson < viewInfo.min)
                    viewInfo.min = viewInfo.FRobinson;
                if (viewInfo.FMiller < viewInfo.min)
                    viewInfo.min = viewInfo.FMiller;


            }
            //viewInfo viewInfo = new viewInfo();
            viewInfo.BasalMetabolic += (computation);
            viewInfo.Sedentary += (computation + 300);
            viewInfo.LightExercise += (computation + 400);
            viewInfo.ModerateExercise += (computation + 500);
            viewInfo.HeavyExercise += (computation + 650);
            viewInfo.Athlete += (computation + 750);

            if (calorieCalculator.CalorieCalculatorActivity == CalorieCalculatorActivity.Sedentary)
            {
                viewInfo.WeeklyCalories = Math.Ceiling(viewInfo.Sedentary * 7);
                viewInfo.Selection = Math.Ceiling(viewInfo.Sedentary);

                viewInfo.maintenanceModerateCarbs = Math.Ceiling(viewInfo.Selection * 30 / 1000);
                viewInfo.maintenanceModerateFats = Math.Ceiling(viewInfo.Sedentary * 35 / 1000);
                viewInfo.maintenanceModerateProtein = Math.Ceiling(viewInfo.Sedentary * 35 / 1000);

                viewInfo.cuttingModerateCarbs = Math.Ceiling(viewInfo.Sedentary * 40 / 1000);
                viewInfo.cuttingModerateFats = Math.Ceiling(viewInfo.Sedentary * 40 / 1000);
                viewInfo.cuttingModerateProtein = Math.Ceiling(viewInfo.Sedentary * 20 / 1000);

                viewInfo.bulkingModerateCarbs = Math.Ceiling(viewInfo.Sedentary * 30 / 1000);
                viewInfo.bulkingModerateFats = Math.Ceiling(viewInfo.Sedentary * 20 / 1000);
                viewInfo.bulkingModerateProtein = Math.Ceiling(viewInfo.Sedentary * 50 / 1000);
                double cutting = (viewInfo.Sedentary - 500);
                viewInfo.maintenanceLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.maintenanceLowerFats = Math.Ceiling(cutting * 35 / 1000);
                viewInfo.maintenanceLowerProtein = Math.Ceiling(cutting * 35 / 1000);

                viewInfo.cuttingLowerCarbs = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerFats = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerProtein = Math.Ceiling(cutting * 20 / 1000);

                viewInfo.bulkingLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.bulkingLowerFats = Math.Ceiling(cutting * 20 / 1000);
                viewInfo.bulkingLowerProtein = Math.Ceiling(cutting * 50 / 1000);
                double bulking = (viewInfo.Sedentary + 500);
                viewInfo.maintenanceHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.maintenanceHigherFats = Math.Ceiling(bulking * 35 / 1000);
                viewInfo.maintenanceHigherProtein = Math.Ceiling(bulking * 35 / 1000);

                viewInfo.cuttingHigherCarbs = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherFats = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherProtein = Math.Ceiling(bulking * 20 / 1000);

                viewInfo.bulkingHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.bulkingHigherFats = Math.Ceiling(bulking * 20 / 1000);
                viewInfo.bulkingHigherProtein = Math.Ceiling(bulking * 50 / 1000);
            }
            else if (calorieCalculator.CalorieCalculatorActivity == CalorieCalculatorActivity.LightExercise)
            {
                viewInfo.WeeklyCalories = Math.Ceiling(viewInfo.LightExercise * 7);
                viewInfo.Selection = Math.Ceiling(viewInfo.LightExercise);

                viewInfo.maintenanceModerateCarbs = Math.Ceiling(viewInfo.LightExercise * 30 / 1000);
                viewInfo.maintenanceModerateFats = Math.Ceiling(viewInfo.LightExercise * 35 / 1000);
                viewInfo.maintenanceModerateProtein = Math.Ceiling(viewInfo.LightExercise * 35 / 1000);

                viewInfo.cuttingModerateCarbs = Math.Ceiling(viewInfo.LightExercise * 40 / 1000);
                viewInfo.cuttingModerateFats = Math.Ceiling(viewInfo.LightExercise * 40 / 1000);
                viewInfo.cuttingModerateProtein = Math.Ceiling(viewInfo.LightExercise * 20 / 1000);

                viewInfo.bulkingModerateCarbs = Math.Ceiling(viewInfo.LightExercise * 30 / 1000);
                viewInfo.bulkingModerateFats = Math.Ceiling(viewInfo.LightExercise * 20 / 1000);
                viewInfo.bulkingModerateProtein = Math.Ceiling(viewInfo.LightExercise * 50 / 1000);
                double cutting = (viewInfo.LightExercise - 500);
                viewInfo.maintenanceLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.maintenanceLowerFats = Math.Ceiling(cutting * 35 / 1000);
                viewInfo.maintenanceLowerProtein = Math.Ceiling(cutting * 35 / 1000);

                viewInfo.maintenanceLowerCarbs = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.maintenanceLowerFats = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.maintenanceLowerProtein = Math.Ceiling(cutting * 20 / 1000);

                viewInfo.bulkingLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.bulkingLowerFats = Math.Ceiling(cutting * 20 / 1000);
                viewInfo.bulkingLowerProtein = Math.Ceiling(cutting * 50 / 1000);
                double bulking = (viewInfo.LightExercise + 500);
                viewInfo.maintenanceHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.maintenanceHigherFats = Math.Ceiling(bulking * 35 / 1000);
                viewInfo.maintenanceHigherProtein = Math.Ceiling(bulking * 35 / 1000);

                viewInfo.cuttingHigherCarbs = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherFats = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherProtein = Math.Ceiling(bulking * 20 / 1000);

                viewInfo.bulkingHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.bulkingHigherFats = Math.Ceiling(bulking * 20 / 1000);
                viewInfo.bulkingHigherProtein = Math.Ceiling(bulking * 50 / 1000);
            }
            else if (calorieCalculator.CalorieCalculatorActivity == CalorieCalculatorActivity.ModerateExercise)
            {
                viewInfo.WeeklyCalories = Math.Ceiling(viewInfo.ModerateExercise * 7);
                viewInfo.Selection = Math.Ceiling(viewInfo.ModerateExercise);

                viewInfo.maintenanceModerateCarbs = Math.Ceiling(viewInfo.ModerateExercise * 30 / 1000);
                viewInfo.maintenanceModerateFats = Math.Ceiling(viewInfo.ModerateExercise * 35 / 1000);
                viewInfo.maintenanceModerateProtein = Math.Ceiling(viewInfo.ModerateExercise * 35 / 1000);

                viewInfo.cuttingModerateCarbs = Math.Ceiling(viewInfo.ModerateExercise * 40 / 1000);
                viewInfo.cuttingModerateFats = Math.Ceiling(viewInfo.ModerateExercise * 40 / 1000);
                viewInfo.cuttingModerateProtein = Math.Ceiling(viewInfo.ModerateExercise * 20 / 1000);

                viewInfo.bulkingModerateCarbs = Math.Ceiling(viewInfo.ModerateExercise * 30 / 1000);
                viewInfo.bulkingModerateFats = Math.Ceiling(viewInfo.ModerateExercise * 20 / 1000);
                viewInfo.bulkingModerateProtein = Math.Ceiling(viewInfo.ModerateExercise * 50 / 1000);
                double cutting = (viewInfo.ModerateExercise - 500);
                viewInfo.maintenanceLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.maintenanceLowerFats = Math.Ceiling(cutting * 35 / 1000);
                viewInfo.maintenanceLowerProtein = Math.Ceiling(cutting * 35 / 1000);

                viewInfo.cuttingLowerCarbs = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerFats = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerProtein = Math.Ceiling(cutting * 20 / 1000);

                viewInfo.bulkingLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.bulkingLowerFats = Math.Ceiling(cutting * 20 / 1000);
                viewInfo.bulkingLowerProtein = Math.Ceiling(cutting * 50 / 1000);
                double bulking = (viewInfo.ModerateExercise + 500);
                viewInfo.maintenanceHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.maintenanceHigherFats = Math.Ceiling(bulking * 35 / 1000);
                viewInfo.maintenanceHigherProtein = Math.Ceiling(bulking * 35 / 1000);

                viewInfo.cuttingHigherCarbs = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherFats = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherProtein = Math.Ceiling(bulking * 20 / 1000);

                viewInfo.bulkingHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.bulkingHigherFats = Math.Ceiling(bulking * 20 / 1000);
                viewInfo.bulkingHigherProtein = Math.Ceiling(bulking * 50 / 1000);
            }
            else if (calorieCalculator.CalorieCalculatorActivity == CalorieCalculatorActivity.HeavyExercise)
            {
                viewInfo.WeeklyCalories = Math.Ceiling(viewInfo.HeavyExercise * 7);
                viewInfo.Selection = Math.Ceiling(viewInfo.HeavyExercise);

                viewInfo.maintenanceModerateCarbs = Math.Ceiling(viewInfo.HeavyExercise * 30 / 1000);
                viewInfo.maintenanceModerateFats = Math.Ceiling(viewInfo.HeavyExercise * 35 / 1000);
                viewInfo.maintenanceModerateProtein = Math.Ceiling(viewInfo.HeavyExercise * 35 / 1000);

                viewInfo.cuttingModerateCarbs = Math.Ceiling(viewInfo.HeavyExercise * 40 / 1000);
                viewInfo.cuttingModerateFats = Math.Ceiling(viewInfo.HeavyExercise * 40 / 1000);
                viewInfo.cuttingModerateProtein = Math.Ceiling(viewInfo.HeavyExercise * 20 / 1000);

                viewInfo.bulkingModerateCarbs = Math.Ceiling(viewInfo.HeavyExercise * 30 / 1000);
                viewInfo.bulkingModerateFats = Math.Ceiling(viewInfo.HeavyExercise * 20 / 1000);
                viewInfo.bulkingModerateProtein = Math.Ceiling(viewInfo.HeavyExercise * 50 / 1000);
                double cutting = (viewInfo.HeavyExercise - 500);
                viewInfo.maintenanceLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.maintenanceLowerFats = Math.Ceiling(cutting * 35 / 1000);
                viewInfo.maintenanceLowerProtein = Math.Ceiling(cutting * 35 / 1000);

                viewInfo.cuttingLowerCarbs = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerFats = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerProtein = Math.Ceiling(cutting * 20 / 1000);

                viewInfo.bulkingLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.bulkingLowerFats = Math.Ceiling(cutting * 20 / 1000);
                viewInfo.bulkingLowerProtein = Math.Ceiling(cutting * 50 / 1000);
                double bulking = (viewInfo.HeavyExercise + 500);
                viewInfo.maintenanceHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.maintenanceHigherFats = Math.Ceiling(bulking * 35 / 1000);
                viewInfo.maintenanceHigherProtein = Math.Ceiling(bulking * 35 / 1000);

                viewInfo.cuttingHigherCarbs = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherFats = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherProtein = Math.Ceiling(bulking * 20 / 1000);

                viewInfo.bulkingHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.bulkingHigherFats = Math.Ceiling(bulking * 20 / 1000);
                viewInfo.bulkingHigherProtein = Math.Ceiling(bulking * 50 / 1000);
            }
            else if (calorieCalculator.CalorieCalculatorActivity == CalorieCalculatorActivity.Athlete)
            {
                viewInfo.WeeklyCalories = Math.Ceiling(viewInfo.Athlete * 7);
                viewInfo.Selection = Math.Ceiling(viewInfo.Athlete);

                viewInfo.maintenanceModerateCarbs = Math.Ceiling(viewInfo.Athlete * 30 / 1000);
                viewInfo.maintenanceModerateFats = Math.Ceiling(viewInfo.Athlete * 35 / 1000);
                viewInfo.maintenanceModerateProtein = Math.Ceiling(viewInfo.Athlete * 35 / 1000);

                viewInfo.cuttingModerateCarbs = Math.Ceiling(viewInfo.Athlete * 40 / 1000);
                viewInfo.cuttingModerateFats = Math.Ceiling(viewInfo.Athlete * 40 / 1000);
                viewInfo.cuttingModerateProtein = Math.Ceiling(viewInfo.Athlete * 20 / 1000);

                viewInfo.bulkingModerateCarbs = Math.Ceiling(viewInfo.Athlete * 30 / 1000);
                viewInfo.bulkingModerateFats = Math.Ceiling(viewInfo.Athlete * 20 / 1000);
                viewInfo.bulkingModerateProtein = Math.Ceiling(viewInfo.Athlete * 50 / 1000);
                double cutting = (viewInfo.Athlete - 500);
                viewInfo.maintenanceLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.maintenanceLowerFats = Math.Ceiling(cutting * 35 / 1000);
                viewInfo.maintenanceLowerProtein = Math.Ceiling(cutting * 35 / 1000);

                viewInfo.cuttingLowerCarbs = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerFats = Math.Ceiling(cutting * 40 / 1000);
                viewInfo.cuttingLowerProtein = Math.Ceiling(cutting * 20 / 1000);

                viewInfo.bulkingLowerCarbs = Math.Ceiling(cutting * 30 / 1000);
                viewInfo.bulkingLowerFats = Math.Ceiling(cutting * 20 / 1000);
                viewInfo.bulkingLowerProtein = Math.Ceiling(cutting * 50 / 1000);
                double bulking = (viewInfo.Athlete + 500);
                viewInfo.maintenanceHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.maintenanceHigherFats = Math.Ceiling(bulking * 35 / 1000);
                viewInfo.maintenanceHigherProtein = Math.Ceiling(bulking * 35 / 1000);

                viewInfo.cuttingHigherCarbs = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherFats = Math.Ceiling(bulking * 40 / 1000);
                viewInfo.cuttingHigherProtein = Math.Ceiling(bulking * 20 / 1000);

                viewInfo.bulkingHigherCarbs = Math.Ceiling(bulking * 30 / 1000);
                viewInfo.bulkingHigherFats = Math.Ceiling(bulking * 20 / 1000);
                viewInfo.bulkingHigherProtein = Math.Ceiling(bulking * 50 / 1000);
            }
            return viewInfo;
        }
    }
}
