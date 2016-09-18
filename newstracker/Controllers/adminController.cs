using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Sql;
using Newtonsoft.Json;
namespace news.Controllers

{
    public class adminController : Controller
    {
        //
        // GET: /admin/
        Class2 c1 = new Class2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult admin_index()
        {
            return View();
        }
        public ActionResult category()
        {
            return View();
        }

        public ActionResult updaterss()
        {
            return View();
        }

        public ActionResult viewall()
        {
            DataSet ds = c1.select("selectall");
            ds.Tables[0].TableName = "rssall";
            string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(jdata);
        }
        public ActionResult editall(int rssid)
        {
            string rss = c1.scalestr("getrss", rssid);
            //ds.Tables[0].TableName = "rss";
            // string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(new { rss = rss });
        }
        public ActionResult editrss(int rssid, string rss)
        {

            int count = c1.scale("count_rss", rss);
            if (count > 0)
                return Json("exist");
            else
            {
                string result = c1.idu("updaterss", rssid, rss);
                return Json(result);
            }
        }

        public ActionResult add_category(string category)
        {
            int count = c1.scale("Cat_repeat",category);
            if (count > 0)
                return Json("exist");
            else
            {
                c1.idu("insncat",category);
            }
            return Json("success");

        }

        //provider//
        public ActionResult add_provider(int lang,string news_provider)
        {
            int count = c1.scale("pro_repeat", news_provider);
            if (count > 0)
                return Json("exist");
            else
            {
                c1.idu("insnpro", lang,news_provider);
            }
            return Json("success");

        }
        public ActionResult add_cat(int language,int category, int news_provider, string rss_feed)
        {

            int count = c1.scale("rss_repeat", rss_feed);
            if (count > 0)
                return Json("exist");
            else
            {
                c1.idu("insertcat", language, category, news_provider, rss_feed);
                return Json("inserted");
            }
            
            
        }

        public ActionResult deleterss(int rssid)
        {
            c1.idu("deleterss",rssid);
            return Json("inserted");
        }

        public ActionResult provider()
        {
            return View();
        }
    }
}
