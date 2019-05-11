using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L300TestApp.Models
{
    public class Todo
    {
        public int ID { get; set; }
        public string Description { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
    }

    public class Todoes
    {
        public List<Todo> toDoes = new List<Todo>();
    }
}