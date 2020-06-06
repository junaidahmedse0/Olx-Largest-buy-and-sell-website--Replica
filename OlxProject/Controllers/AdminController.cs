using OlxProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OlxProject.Controllers
{

    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        //Admin ViewModel for Functionalities
        AdminViewModel admin = new AdminViewModel();
        //Database Object
        OlxDbEntities db = new OlxDbEntities();
        // Return all ads that are not cancelled
        public ActionResult Index()
        {
            return View(db.Ads.Where(x => x.Cancel == false).ToList());
        }
        //Main Dashboard
        public ActionResult Dashboard()
        {

            return View(db.Ads.Where(x=>x.Cancel==false).ToList());
        }
        //Review Add Function view
        public ActionResult ReviewAd()
       {
            ViewBag.Show = "Review";
            return View(db.Ads.Where(x => x.Cancel == false).ToList());
        }
        // show All Ads that user want to make featured
        public ActionResult FeatureList()
        {
            ViewBag.Show = "feature";
            return View(db.Ads.Where(x=>x.Feature==true && x.Featured==false &&x.Cancel==false).ToList());
        }
        //Make Ad Featured
        public ActionResult Feature(int id)
        {
            var ad = db.Ads.Find(id);
            ad.Featured=true;
            db.SaveChanges();
            return RedirectToAction("FeatureList");
        }
        //Make Feature Ad to Unfeatured
        public ActionResult Featured(int id)
        {
            
            var ad = db.Ads.Find(id);
            ad.Featured = false;
            db.SaveChanges();
            return RedirectToAction("FeaturedList");
        }
        //List of Featured Ads
        public ActionResult FeaturedList()
        {
            ViewBag.Show = "feature";
            return View(db.Ads.Where(x => x.Featured == true && x.Feature==true && x.Cancel==false).ToList());
        }
        //List of ads that you can make cancel
        public ActionResult CancelList()
        {
            return View(db.Ads.Where(x => x.Cancel == false).ToList());
        }
        //List of Ads that are cancelled
        public ActionResult CanceledList()
        {
         
            return View(db.Ads.Where(x => x.Cancel == true).ToList());
        }
        //make Single ad Cancelled
        public ActionResult Cancel(int id)
        {
            var ad = db.Ads.Find(id);
            ad.Cancel = true;
            db.SaveChanges();
            return RedirectToAction("CancelList");
        }
        //Make cancel ads to Remove from cancelled ads
        public ActionResult Canceled(int id)
        {
            var ad = db.Ads.Find(id);
            ad.Cancel = false;
            db.SaveChanges();
            return RedirectToAction("CanceledList");
        }
        //Show Details of a posts
        public ActionResult PostDetail(int id)
        {
            return View(admin.PostDetail(id));
        }
 
        public ActionResult Review(int id, string review)
        {
            var r = new Review();
            r.Review1 = review;
            r.AdId = id;
            db.Reviews.Add(r);
            db.SaveChanges();
            return RedirectToAction("PostDetail", new { id = id });
        }



    }
}