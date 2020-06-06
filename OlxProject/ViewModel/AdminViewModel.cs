using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OlxProject.ViewModel
{
    public class AdminViewModel
    {
        OlxDbEntities db = new OlxDbEntities();
      public PostDetailViewModel PostDetail(int id)
      {


            var post = db.Ads.Find(id);
            var user = db.Users.Find(post.UserId);
            var images = db.Images.Where(x => x.AdId == post.Id).ToList();
            var attributes = db.AddAttributes.Where(x => x.AdId == post.Id).ToList();
            var reviews = db.Reviews.Where(x => x.AdId == post.Id).ToList();
            var viewModel = new PostDetailViewModel()
            {
                User = user,
                Ad = post,
                Image = images,
                AddAttribute = attributes,
                Reviews = reviews

            };
            return viewModel;


        }






























    }
}