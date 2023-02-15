using Entities.Constants;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObjects
{
    public record EmployeeForManipulationDto
    {

        [Required(ErrorMessage = "person name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "person Address is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is a required field.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "person Position is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Position { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoin { get; set; }


    }
}
