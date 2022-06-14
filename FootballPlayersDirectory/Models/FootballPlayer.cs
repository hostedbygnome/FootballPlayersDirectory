using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballPlayersDirectory.Models
{
    public class FootballPlayer
    {
        [Key]
        public int Id { get; set; }

        public int TeamId { get; set; } = 0;

        [Required(ErrorMessage = "The first name is empty!")]
        [StringLength(20)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The second name is empty!")]
        [StringLength(20)]
        public string SecondName { get; set; } = string.Empty;

        public Genders Gender { get; set; } = Genders.Male;

        [Required(ErrorMessage = "The date of birth is empty!")]
        public string DateBirth { get; set; } = string.Empty;

        public Countries Country { get; set; } = Countries.Russia;
    }
}
