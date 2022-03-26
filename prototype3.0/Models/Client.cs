using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prototype3._0.Models
{
    [Table("cls")]
    public class Client
    {
        [Key] // ==> PRIMARY KEY
        public int CID { get; set; } // GUID

        [Required] // ==> NOT NULL
        [MinLength(4), MaxLength(100)]
        
        public string Nom_c{ get; set; }

        [Required]
        [MaxLength(100)]
        
        [EmailAddress]
        [Index(IsUnique = true)]

        public string AdresseMail { get; set; }
        
        [Required]
        [Phone]
        
        public string numero_tel { get; set; }

        [Required] [MaxLength(100)] public string MotDePasse { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
        public DateTime DateNaissance { get; set; }
        
        [Required]
        public bool valider { get; set; } = false;
        
        [Required]
        public bool Isadmin { get; set; } = false;

        // public  string image_car { get; set; }
        public string Image { get; set; } = "";

        [NotMapped] // designer que ce champs ne sera pas cree au niveau de la BDD
        public HttpPostedFileBase ImageTemp { get; set; }




    }
}