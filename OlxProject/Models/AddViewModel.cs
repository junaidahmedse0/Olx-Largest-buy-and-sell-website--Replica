using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlxProject.Models
{
    public class AddViewModel
    {
       
        public int Id { get; set; }

        public bool Feature { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.0, 1000000000000)]
        public Nullable<int> Price { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Category { get; set; }
        public List<Attribute> Attribute { get; set; }

        public HttpPostedFileBase[] Files { get; set; }
        public Nullable<int> UserId { get; set; }

    }
}