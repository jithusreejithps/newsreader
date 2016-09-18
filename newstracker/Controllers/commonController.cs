using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using newstracker.Models;
using System.Data;
using Newtonsoft.Json;

namespace newstracker.Controllers
{
    public class commonController : Controller
    {
        //
        // GET: /common/
        Class2 c1 = new Class2();
        rss_rad r = new rss_rad();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dispsavenews()
        {
            return View();

        }

        public ActionResult list_category()
        {
            DataSet ds = c1.select("get_category");
            ds.Tables[0].TableName = "category";
            string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(jdata);
        }

        public ActionResult list_language()
        {
            DataSet ds = c1.select("get_lang");
            ds.Tables[0].TableName = "lang_name";
            string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(jdata);
        }
        public ActionResult selectprovider(int lang)
        {

            DataSet ds = c1.select("sel_pro", lang);
            ds.Tables[0].TableName = "pro_name";
            string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(jdata);

        }
        public ActionResult page(int provider,int category)
        {
            Session["p_id"] = provider;
            Session["c_id"] = category;
            int count = c1.scale("chek_cat", provider, category);
            if (count > 0)
                return Json("a");
            else
            {
                
                return Json("error");
            }   
        }

        //public ActionResult visited()
        //{
        //    string user_name = Session["uname"].ToString();
        //    string c_id = Session["c_id"].ToString();
        //    string p_id = Session["p_id"].ToString();
        //    return View();
        //}

        public ActionResult savenews()
        {
            if (Session["uname"] == null)
                return Json("error");
            string username = Session["uname"].ToString();
            string rss = Session["rss"].ToString();

            int index = Convert.ToInt32(Session["index"]);
            rss_rad r = new rss_rad();

            string category = Session["category"].ToString();
            DataTable dt = r.readnews(index, rss);
            string prov = dt.Rows[0]["news_prov"].ToString();
            string title = dt.Rows[0]["title"].ToString();
            string summm = dt.Rows[0]["summary"].ToString();
            string img = dt.Rows[0]["img"].ToString();
            string url = dt.Rows[0]["url"].ToString();
            string date = dt.Rows[0]["date"].ToString();
            
            
            //string cat = dt.Rows[0]["category"].ToString();
            //string time = dt.Rows[0]["time"].ToString();
            date = date.Substring(0, 9);
            if (url == "")
                url = "Null";
            if (img == "")
                img = "/Images/news_3.gif";


            string result = c1.idu("savenews",username ,prov, category,title,summm, img, url, date, System.DateTime.Now.ToShortDateString());
            return Json(result);


        }


        public ActionResult get_cat()
        {
            {
                DataSet ds = c1.select("get_category");
                ds.Tables[0].TableName = "category";
                string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
                return Json(jdata);
            }

        }
        /// <summary>
        /// main news//////
        /// </summary>
        /// <returns></returns>
        public ActionResult main_news()
        {
            try
            {
                DataTable dt = r.readmain1("http://www.ibnlive.com/rss/india.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 1; i < 2; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);

            }
            catch
            {
                return Json("error");
            }

        }
        
        public ActionResult main_news1()
        {
            try
            {
                DataTable dt = r.readmain1("http://www.ibnlive.com/rss/india.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 0; i < 1; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);

            }
            catch
            {
                return Json("error");
            }
        }
        public ActionResult main_news2()
        {
            try
            {

                DataTable dt = r.readmain1("http://www.ibnlive.com/rss/india.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 2; i < 3; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);
            }
            catch
            {
                return Json("error");
            }

        }
        public ActionResult main_news3()
        {
            try
            {
                DataTable dt = r.readmain1("http://www.ibnlive.com/rss/india.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 3; i < 4; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);

            }
            catch
            {
                return Json("error");
            }
        }

        //main news 2///
        public ActionResult main_n()
        {
            try
            {
                DataTable dt = r.readmain2("http://www.ibnlive.com/rss/tv.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 1; i < 2; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);

            }
            catch
            {
                return Json("error");
            }

        }
        
        public ActionResult main_n1()
        {
            try
            {
                DataTable dt = r.readmain2("http://www.ibnlive.com/rss/tv.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 0; i < 1; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);

            }
            catch
            {
                return Json("error");
            }
        }
        public ActionResult main_n2()
        {
            try
            {

                DataTable dt = r.readmain2("http://www.ibnlive.com/rss/tv.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 2; i < 3; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);
            }
            catch
            {
                return Json("error");
            }

        }
        

        public ActionResult getnews(string rss)
        {

            rss_rad r = new rss_rad();
            DataTable dt = r.readsports(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);

        }
        
        public ActionResult getnews2(string rss)
        {

            rss_rad r = new rss_rad();
            DataTable dt = r.readmain1(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);

        }
        public ActionResult getnews3(string rss)
        {

            rss_rad r = new rss_rad();
            DataTable dt = r.readmain2(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);

        }
        public ActionResult getnews4(string rss)
        {

            rss_rad r = new rss_rad();
            DataTable dt = r.readone(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);

        }
        public ActionResult news_list()
        {
            string type = Session["type"].ToString();
            string rss = "";
            if (type == "sports")
            {
                rss = "http://www.ibnlive.com/rss/sports.xml";
            }
            else if (type == "politics")
            {
                rss = "http://www.ibnlive.com/rss/politics.xml";
            }
            else if (type == "technology")
            {
                 rss = "http://www.ibnlive.com/rss/tech.xml";
            }
            else if (type == "business")
            {
                rss = "http://www.ibnlive.com/rss/business.xml";
            }
            rss_rad r = new rss_rad();
            DataTable dt = r.readlist(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);

        }

        public  ActionResult single(string rss,string index)
        {
            Session["rss"] = rss;
            Session["index"] = index;
            return Json("clicked");
        }

        public ActionResult single1(string rss, string index)
        {
           
            Session["index"] = index;
            return Json("clicked");
        }
        public ActionResult categoryview()
        {
            string c_id = Session["c_id"].ToString();
            string p_id = Session["p_id"].ToString();
            string rss = c1.scalestr("selectrssfeed",c_id,p_id);
            string category = c1.scalestr("selectcategory", c_id);
            Session["category"] = category;
            if (Session["uname"] != null)
            {
                c1.idu("insert_mine", Session["uname"], c_id, p_id);
            }
            Session["rss"] = rss;
            rss_rad r = new rss_rad();
            DataTable dt = r.readlist(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);
        }

        public ActionResult mineview(string rss)
        {

            string rssfeed = rss;
            //string category = c1.scalestr("selectcategory", c_id);
            //Session["category"] = category;
            //if (Session["uname"] != null)
            //{
            //    c1.idu("insert_mine", Session["uname"], c_id, p_id);
            //}
            //Session["rss"] = rss;
            rss_rad r = new rss_rad();
            DataTable dt = r.readlist(rssfeed);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);
        }
        public ActionResult dispnews()
        {
            
            return View();
        }
        public ActionResult dispnews_1()
        {
            try
            {

                int index = Convert.ToInt32(Session["index"]);
                string rss = Session["rss"].ToString();
                rss_rad r = new rss_rad();
               
                //if (Session["username"] != null)
                //{
                //    c1.idu("insert_mine", Session["username"], category, Session[category].ToString());
                //}

                DataTable dt = r.readnews(index, rss);
                var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
                return Json(jdata);
            }
            catch
            {
                return Json("error");
            }
        }
        public ActionResult newsview(int news_id)
        {
            Session["news_id"] = news_id;
     
            return Json("clicked");

        }
        public ActionResult viewall()
        {
            string uname = Session["uname"].ToString();
            DataSet ds = c1.select("get_savednews", uname);
            ds.Tables[0].TableName = "saved";

            string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
            return Json(jdata);
        }
        public ActionResult dispnews_2()
        {
            try
            {

                string news_id = Session["news_id"].ToString(); 
                DataSet ds = c1.select("get_saved",news_id);
                ds.Tables[0].TableName = "saved";
               
                string jdata = JsonConvert.SerializeObject(ds, Formatting.Indented);
                return Json(jdata);
                
                
            }
            catch
            {
                return Json("error");
            }
        }

        public ActionResult snews()
        {
            return View();
        }
        public ActionResult url()
        {
            try
            {

                int index = Convert.ToInt32(Session["index"]);
                string rss = Session["rss"].ToString();
                rss_rad r = new rss_rad();

                //if (Session["username"] != null)
                //{
                //    c1.idu("insert_mine", Session["username"], category, Session[category].ToString());
                //}

                DataTable dt = r.readnews(index, rss);
                string url = dt.Rows[0]["url"].ToString();
               // var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
                return Json(url);
            }
            catch
            {
                return Json("error");
            }
        }
        public ActionResult cat()
        {
            try
            {

                DataTable dt = r.readmain2("http://www.ibnlive.com/rss/india.xml");
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("img");
                dt1.Columns.Add("title");
                dt1.Columns.Add("index");
                for (int i = 2; i < 3; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["img"] = dt.Rows[i]["img"];
                    dr["title"] = dt.Rows[i]["title"];
                    dr["index"] = i;
                    dt1.Rows.Add(dr);

                }
                var jdata = JsonConvert.SerializeObject(new { main_news = dt1 }, Formatting.Indented);
                return Json(jdata);
            }
            catch
            {
                return Json("error");
            }

        }


        public ActionResult getsinglenews(string rss)
        {

            rss_rad r = new rss_rad();
            DataTable dt = r.readone(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);

        }

        //selecting catagory news//

        public ActionResult catnews()
        {
            string type = Session["type"].ToString();
            string rss = "";
            if (type == "sports")
            {
                rss = "http://www.ibnlive.com/rss/tech.xml";
            }
            
            rss_rad r = new rss_rad();
            DataTable dt = r.readlist(rss);
            var jdata = JsonConvert.SerializeObject(new { news = dt }, Formatting.Indented);
            return Json(jdata);

        }
        public ActionResult catview()
        {
            return View();
        }
        
        ////data mine////

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
