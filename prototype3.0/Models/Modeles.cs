using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prototype3._0.Models
{
    [Table("Models")]
    public class Modeles
    {
        [Key]
        public int model_ID { get; set; }

        [Required]
        [MinLength(2), MaxLength(100)]
        public string nom_marque { get; set; }

        [Required]
        [MinLength(2), MaxLength(100)]

        public string num_serie { get; set; }

    }
}