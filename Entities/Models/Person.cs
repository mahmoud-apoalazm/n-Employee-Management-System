using Entities.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Entities.Models
{
   public abstract class Person
    {

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoin { get; set; }


    }
}
