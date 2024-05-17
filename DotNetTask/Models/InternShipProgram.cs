using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DotNetTask.Models
{
    public class InternShipProgram
    {
        public class SummeryInternShipProgram
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("programTitle")]
            public string ProgramTitle { get; set; }
            [JsonProperty("programDiscription")]
            public string ProgramDiscription { get; set; }
        }

        public class ParagraphQuestion
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("question")]
            public string Question { get; set; }
            [JsonProperty("answer")]
            public string Answer { get; set; }
        }

        public class NumericQuestion
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("question")]
            public string Question { get; set; }
            [JsonProperty("answer")]
            public string Answer { get; set; }
        }

        public class DateQuestion
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("question")]
            public string Question { get; set; }
            [JsonProperty("answer")]
            public string Answer { get; set; }
        }
        public class YesOrNoQuestion
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("question")]
            public string Question { get; set; }
            [JsonProperty("answer")]
            public string Answer { get; set; }
        }
        public class DropDownTemplate 
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("question")]
            public string Question { get; set; }
            [JsonProperty("answer")]
            public List<DropDownMultipleChoice> dropDownMultipleChoices { get; set; }
        }
        public class DropDownMultipleChoice
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("answer")]
            public string Answer { get; set; }
        }
        public class PersonalInformation
        {
            [JsonProperty("id")]
            public string id { get; set; }
            [JsonProperty("firstName")]
            public string FirstName { get; set; }
            [JsonProperty("lastName")]
            public string LastName { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("phone")]
            public string Phone { get; set; }
            [JsonProperty("natinality")]
            public string Natinality { get; set; }
            [JsonProperty("idNumber")]
            public string IdNumber { get; set; }
            [JsonProperty("dateOfBirth")]
            public int DateOfBirth { get; set; }
            [JsonProperty("gender")]
            public string Gender { get; set; }
            public List<SummeryInternShipProgram> summeryInternShipPrograms { get; set; } = new List<SummeryInternShipProgram>();
            public List<ParagraphQuestion> ParagraphQuestion { get; set; } = new List<ParagraphQuestion>();
            public List<NumericQuestion> NumericQuestion { get; set; } = new List<NumericQuestion>();
            public List<DateQuestion> DateQuestion { get; set; } = new List<DateQuestion>();
            public List<YesOrNoQuestion> YesOrNoQuestion { get; set; } = new List<YesOrNoQuestion>();
            public List<DropDownTemplate> dropDownTemplates { get; set; } = new List<DropDownTemplate>();
        }
    }
}
