using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport.WebUI.Entities;

namespace Sport.WebUI.Models
{
    public class FatRate
    {
        public double FateRate(CalculateFatRate fatRateEntities)
        {
            if (fatRateEntities.FatRateGender == FatRateGender.Male)
            {
                double BFForMen = Math.Ceiling(495 / (1.0324 - 0.19077 * Math.Log10(fatRateEntities.Waist - fatRateEntities.Neck) + 0.15456 * Math.Log10(fatRateEntities.Height)) - 450);
                return BFForMen;

                //%BF                       = 495 / ( 1.0324 - 0.19077 * log10( waist - neck ) + 0.15456 * log10( height ) ) - 450
                //Vücut Yağları (erkekler) = 495 / (1.29579 - .35004 * log 10 (Bel - Boyun) + 0.22100 * log 10 (Yükseklik)) - 450
            }
            else
            {
                double BFForWomen = Math.Ceiling(495 / (1.29579 - 0.35004 * Math.Log10(fatRateEntities.Waist + fatRateEntities.Hip - fatRateEntities.Neck) + 0.22100 * Math.Log10(fatRateEntities.Height)) - 450);
                return BFForWomen;

                // Vücut Yağları(kadınlar) = 495 / (1.29579 - .35004 * log 10(Bel + Kalça - Boyun) + 0.22100 * log 10(Yükseklik)) -450
                //%BF =                      495 / ( 1.29579 - 0.35004 * log10( waist + hip - neck ) + 0.22100 * log10( height ) ) - 450

            }
        }
    }
}