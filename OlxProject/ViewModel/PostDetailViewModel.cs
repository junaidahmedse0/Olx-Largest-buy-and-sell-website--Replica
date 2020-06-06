using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OlxProject.ViewModel
{
    public class PostDetailViewModel
    {

        public User  User { get; set; }
        public Ad Ad { get; set; }
        public List<Image> Image { get; set; }
        public List<AddAttribute> AddAttribute { get; set; }
        public List<Review> Reviews { get; set; }

    }
}