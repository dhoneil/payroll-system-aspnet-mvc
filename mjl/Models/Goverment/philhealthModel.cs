using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mjl.Models
{
    public class philhealthModel
    {
        public static decimal getphilhealthAmount(decimal monthly_rate)
        {
            decimal first_number = 10000m;
            decimal second_number = 40000m;
            decimal new_contribution = 0;

            if (monthly_rate <= first_number)
            {
                //Range 1 - 10,000
                new_contribution = 137.50m;
            }
            else if (monthly_rate > first_number && monthly_rate < second_number)
            {
                //Range 10,0001 - 39,999
                new_contribution = ((monthly_rate * 2.75m) / 100m) / 2;
            }
            else
            {
                //Range 40,000 beyond
                new_contribution = 550m;
            }
            return new_contribution;
        }
    }
}