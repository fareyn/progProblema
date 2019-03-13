//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProblemaProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class pelicula
    {
        public int id { get; set; }
        public string nombre { get; set; }
        [Display(Name = "Genero")]
        public string genero { get; set; }
        public string sinopsis { get; set; }
        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> falta { get; set; }
        public Nullable<bool> disponible { get; set; }
        [Display(Name = "Fecha de Retorno")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fretorno { get; set; }
        [Display(Name = "Genero")]
        public virtual genero genero1 { get; set; }
    }
}