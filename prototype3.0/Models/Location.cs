using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prototype3._0.Models
{
    [Table("Location")]
    public class Location
    {

        [Key]
        public int LID { get; set; }

        
        [ForeignKey("Client")]
        public int CID { get; set; }

        public Client Client { get; set; }

        
        [ForeignKey("Voiture")]
        public int car_ID { get; set; }

        public Voiture Voiture { get; set; }



        [Required]
        [MaxLength(50)]
        public string type_location { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
        public DateTime date_debut { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime date_fin { get; set; }
        [Required]
        public double prix_location { get; set; }

        [Required]
        public bool Isvalid { get; set; } = false;




    }
}