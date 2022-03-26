using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prototype3._0.Models
{
    [Table("Car")]
    public class Voiture
    {
        [Key]
        public int car_ID { get; set; }

        
        [ForeignKey("Categories")]
        public int Cat_ID { get; set; }

        public Categories Categories { get; set; }

        
        [ForeignKey("Modeles")]
        public int model_ID { get; set; }

        public Modeles Modeles { get; set; }

        [Required]
        [MinLength(4)]
        public string num_immatri { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime circulation { get; set; }

        [Required]
        [MinLength(5) ,MaxLength(10)]
        public string carburant { get; set; }
        
        [Required]
         public double loca_jounalier { get; set; }

        // public  string image_car { get; set; }
        public string Image { get; set; } = "";

        [NotMapped] // designer que ce champs ne sera pas cree au niveau de la BDD
        public HttpPostedFileBase ImageTemp { get; set; }


    }
}