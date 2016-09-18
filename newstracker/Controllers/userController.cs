using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace news2.Controllers
{
    public class userController : Controller
    {
        //
        // GET: /user/
        Class2 c1 = new Class2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult newsview()
        {
            return View();
        }

        public ActionResult user_index()
        {
            return View();
        }
        public ActionResult data_mine()
        {
            apriori ap = new apriori();
            string[] result = ap.apriori1(Session["uname"].ToString());
            if (result != null)
            {
                string rss = c1.scalestr("get_rss_by_cid_pid", result[0], result[1]);
                return Json(rss);
            }
            return Json("NoData");
        }

    }
}
