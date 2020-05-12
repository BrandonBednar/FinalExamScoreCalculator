using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FinalExamScoreCalculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public double? CurrentScore { get; set; }

        [BindProperty]
        public double? DesiredScore { get; set; }

        [BindProperty]
        public double? FinalWeight { get; set; }

        [BindProperty]
        public string ResultMessage { get; set; } = "";

        /// <summary>
        /// Method that updates the information displayed each time the page is called.
        /// </summary>
        public void OnGet()
        {
            if (CurrentScore != null && DesiredScore != null && FinalWeight != null) FinalScoreCalculation();
            else ResultMessage = "Enter Class Info";
        }

        /// <summary>
        /// This method converts the entered info from the website and converts 
        /// it to primative doubles for the result calculation
        /// </summary>
        public void FinalScoreCalculation()
        {
            double cs = Convert.ToDouble(CurrentScore);
            double ds = Convert.ToDouble(DesiredScore);
            double fw = Convert.ToDouble(FinalWeight);

            double result = ((ds - (cs * (1 - (fw / 100))))) / (fw / 100);

            SetMessage(result);
        }

        /// <summary>
        /// This method sets the ResultMessage property to a certain 
        /// message depending on what score the user needs on their
        /// final
        /// </summary>
        /// <param name="result">The calculated score needed</param>
        public void SetMessage(double result)
        {
            switch (result)
            {
                case var x when result >= 85.0:
                    ResultMessage = ("You need a " + result + " on the final. You will want to study a lot.");
                    break;
                case var x when result >= 70.0:
                    ResultMessage = ("You need a " + result + " on the final. You will want to study, but not too much.");
                    break;
                case var x when result >= 50.0:
                    ResultMessage = ("You need a " + result + " on the final. You will want to study, but you are in a good spot for the final.");
                    break;
                case var x when result >= 30.0:
                    ResultMessage = ("You need a " + result + " on the final. You should not need to study much");
                    break;
                case var x when result >= 00.0:
                    ResultMessage = ("You need a " + result + " on the final. You are almost guranteed the grade you want.");
                    break;
                default:
                    ResultMessage = "Error";
                    break;
            }
        }
    }
}
