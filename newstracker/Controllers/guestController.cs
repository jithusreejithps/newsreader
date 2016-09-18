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
    public class guestController : Controller
    {


        Class2 c1 = new Class2();
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult guest_Index()
        {
            Session["uname"]="juest";
            return View();
        }
        public ActionResult insert()
        {
            return View();

        }
       
        public ActionResult user_reg(string first_name, string last_name, string gender, DateTime dob, float ph_no, string email, string address, string user_name, string password, string image)
        {
            c1.idu("insertreg", first_name, last_name, gender, dob, ph_no, email, address, user_name, password, image);
            Session["uname"] = user_name.ToString();
            return Json("a");
        }
        public ActionResult signinview()
        {
            return View();

        }

        public ActionResult login()
        {
            return View();
        }
        public ActionResult signin(string user_name,string password)
        {
            
            DataSet ds= c1.select("signin",user_name,password);
            if(ds != null)
            {
                
                if(ds.Tables[0].Rows.Count > 0)
                {
                    
                    string user = ds.Tables[0].Rows[0]["user_name"].ToString();
                    Session["uname"] = user;
                    //string pass = ds.Tables[0].Rows[0]["password"].ToString();
                    string type = ds.Tables[0].Rows[0]["user_type"].ToString();
                    if (type.Equals("user"))
                    {
                        return Json(new { valid = true, url = "/user/newsview" });

                    }
                    else if (type.Equals("admin"))
                    {
                        return Json(new { valid = true, url = "/admin/admin_index" });
                    }
                    
                }
                else
                {
                    return Json(new { valid = false });
                }
                    
            }
            return Json(new { valid = false });
        }

        public ActionResult getnews(string rss)
        {

            rss_rad r = new rss_rad();
            DataTable dt= r.readsports(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);
            
        }


        public ActionResult guestnewslist(string type)
        {
            Session["type"] = type;
            return View();
        }

    }
}
