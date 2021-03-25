using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiiConcatelPractice.Models
{
    public class Rebel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Planet { get; set; }  
        public DateTime Date { get; set; }

        public Rebel()
        {

        }
    }
}
