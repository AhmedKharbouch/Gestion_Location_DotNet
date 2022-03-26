using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prototype3._0.Models
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        public int Cat_ID { get; set; }

        [Required] // ==> NOT NULL
        [MinLength(4), MaxLength(100)]
        public string Nom { get; set; }
    }
}