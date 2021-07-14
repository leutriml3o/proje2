using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sport.WebUI.ViewModels
{
    public class QuestionsSportViewModel
    {
        public int Gun { get; set; }
        public int Bolge { get; set; }
        public int Haraket { get; set; }

    }
}