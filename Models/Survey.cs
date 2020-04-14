using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecreatedSurvey.Models
{
    public class Survey
    {
       // public int SurveyID { get; set; }
        //public string QuestionOne { get; set; }
        //public string QuestionTwo { get; set; }
        //public string QuestionThree { get; set; }
        //public string QuestionFour { get; set; }
        //public string QuestionFive { get; set; }
        [Required]
        [Range(0, 5)]
        public int AnswerOne { get; set; }
        [Required]
        [Range(0, 5)]
        public int AnswerTwo { get; set; }
        [Required]
        [Range(0, 5)]
        public int AnswerThree { get; set; }
        [Required]
        [Range(0, 5)]
        public int AnswerFour { get; set; }
        [Required]
        [Range(0, 5)]
        public int AnswerFive { get; set; }
    }
}
