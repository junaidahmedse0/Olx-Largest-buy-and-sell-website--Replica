using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OlxProject.Models;
using OlxProject.ViewModel;

namespace OlxProject.Controllers
{
    public class HomeController : Controller
    {


        OlxDbEntities db = new OlxDbEntities();
        //Home Page of a OLx

        //[Authorize(Roles = "user")]
        public ActionResult Index()
        {
            return View();
        }
        //All Categories of a products
        public ActionResult Category()
        {

            return View();
        }
        
        //Add Post  View
        public ActionResult AddPost(string Name)
        {
            ViewBag.Category = Name;
            ViewBag.states = new SelectList(db.States, "State1", "State1");
            ViewBag.cities = new SelectList(new List<City>(), "Id", "City1");


            return View();
        }
        //Adding posts to Db....

        [HttpPost]
        public ActionResult AddPost(AddViewModel model)
        {

            if (model.Files[0] == null || model.Files[1] == null|| model.Files[2] == null)
            {
                ViewBag.Category = model.Category;
                ModelState.AddModelError("", "Must upload Files");

                return View();
            }

            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            string name = Path.GetFileName(model.Files[0].FileName);
            string fileName= string.Concat( DateTime.Now.ToString("yyyyMMddHHmmssfff"),name);
            model.Files[0].SaveAs(Path.Combine(Server.MapPath("~/Data/User"), fileName));

            Ad ad=new Ad();
            ad.Condition = model.Condition;
            ad.Title = model.Title;
            ad.Description = model.Description;
            ad.Category = model.Category;
            ad.City = model.City;
            ad.State = model.State;
            ad.Price=model.Price;
            ad.UserId = user.Id;
            ad.Image = fileName;
            ad.Feature = model.Feature;
            db.Ads.Add(ad);
            foreach (var image in model.Files)
            {

                string path = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetFileName(image.FileName));
                string Fullpath = Path.Combine(Server.MapPath("~/Data/User"),path);
                image.SaveAs(Fullpath);
                var img = new Image();
                img.AdId = model.Id;
                img.ImagePath = path;




                db.Images.Add(img);

            }

            if (!(model.Attribute==null))
            {
                foreach (var item in model.Attribute)
                {
                    var attribute = new AddAttribute();
                    attribute.Attribute = item.key;
                    attribute.value = item.value;
                    attribute.AdId = model.Id;
                    db.AddAttributes.Add(attribute);
                }
            }
            db.SaveChanges();

            return RedirectToAction("Category");
        }
        
        public IList<City> Getstate(string state)
        {

            var s = db.States.Where(x=>x.State1==state).FirstOrDefault();
                return db.Cities.Where(m=>m.StateId == s.Id).ToList();
         
        }

        //Getting Data from Database of States
        public JsonResult GetJsonState(string state)
        {

            var cityListt = this.Getstate(state);
            var statesList = cityListt.Select(m => new SelectListItem()
            {
                Text = m.City1,
                Value = m.City1
            });

            return Json(statesList, JsonRequestBehavior.AllowGet);
        }

        //Filtering Posts
        public ActionResult Filter(string Province, string City1, string search, string condition, int? min, int? max)
        {
            using (var db = new OlxDbEntities())
            {
                var posts = db.Ads.Where(x => x.State == Province && x.City == City1 && x.Category.Contains(search) || (x.Condition==condition ) ).ToList();

                if(min!=null || max!=null)
                {

                    posts = posts.Where(x => x.Price >= min && x.Price <= max).ToList();
                }


                ViewBag.Province = Province;
                ViewBag.City = City1;
                ViewBag.Search = search;
                ViewBag.min = min;
                ViewBag.max = max;
                return View(posts);
            }

        }
        //Getting All Posts From Db
        public ActionResult AllPosts()
        {
            return PartialView("AdsBar", db.Ads.Where(x=>x.Cancel==false).OrderByDescending(x=>x.Featured).ToList());
        }
        //For Getting Attribute of a product from table
        public ActionResult Detail(string Name)
        {
            using (var db=new OlxDbEntities())
            {

                var attributes = db.Attributes.Where(x => x.Category == Name).ToList();

                return PartialView(attributes);
            }
         
        
        }
        //For Watching Post Detail
        public ActionResult PostDetail(int id)
        {
            var post = db.Ads.Find(id);

            var user = db.Users.Find(post.UserId);
            var images = db.Images.Where(x=>x.AdId==post.Id).ToList();
            var attributes = db.AddAttributes.Where(x=>x.AdId==post.Id).ToList();
            var reviews = db.Reviews.Where(x => x.AdId == post.Id).ToList();
            var viewModel = new PostDetailViewModel()
            {
                User = user,
                Ad = post,
                Image = images,
                AddAttribute = attributes,
                Reviews = reviews

            };

            return View(viewModel);
        }
        //For Watching User Ads
        public ActionResult UserAd(string name)
        {
         var user = db.Users.FirstOrDefault(x=>x.Email==name);
         var posts = db.Ads.Where(x => x.UserId == user.Id).ToList();
         return View(posts);
        }
        //Cancel Ad
        public ActionResult CancelAds(string name)
        {

            var user = db.Users.FirstOrDefault(x => x.Email == name);
            var posts = db.Ads.Where(x => x.UserId == user.Id&& x.Cancel==true).ToList();
            return View(posts);
        }

    }
}