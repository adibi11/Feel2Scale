using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feel2Scale.Data
{
    /// <summary>
    /// A class representing a musical scale and its related data as compund in Database.
    [Table("ScaleData")]
    public class ScaleData
    {
        //Will represent the primary key in the database table
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // Will represent the name of the scale, e.g., 'Ionian', 'Dorian', etc.
        [Required]
        public string? ScaleName { get; set; }
        //All the notMapped properties will not be stored in the database, but will be used in the application logic
        //All names have the names have same meaning, no need for further explanation
        [NotMapped]
        public List<string>? Scale { get; set; } // Array of Greek numbers related to the scale, e.g., 'I', 'IIIm', etc.
        [NotMapped]
        public List<string>? Chords { get; set; } // Array of all the chords given by a certain random key
        public List<string>? Instruments { get; set; } // Array of instrument names that will fit
        [NotMapped]
        public List<string>? Effects { get; set; } // Array of effects that are worth playing with
        [Required]
        public string? Message { get; set; } // Suggestion from the AI and explanation why it works, should be a string only

        // <summary>
        /// Returns a string representation of the ScaleData object, including all properties.
        public string ToString()
        {
            return $"ScaleData(Id: {Id}, ScaleName: {ScaleName}, Scale: [{string.Join(", ", Scale ?? new List<string>())}], Chords: [{string.Join(", ", Chords ?? new List<string>())}], Instruments: [{string.Join(", ", Instruments ?? new List<string>())}], Effects: [{string.Join(", ", Effects ?? new List<string>())}], Message: {Message})";
        }
    }
}
