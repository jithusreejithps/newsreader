using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.Net;
using System.ServiceModel.Syndication;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


    public class rss_rad
    {
        public DataTable readXml(string location)
        {
            try
            {

                XmlReader xrdr = XmlReader.Create("http://www.manoramaonline.com/news/just-in.feed");
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";
                DataTable dt = new DataTable();
                dt.Columns.Add("index");
                dt.Columns.Add("title");
                int i = 0;
                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;
                    DataRow dr = dt.NewRow();
                    dr[0] = i;
                    dr[1] = item.Title.Text;
                    dt.Rows.Add(dr);
                    i++;

                }
                return dt;

            }
            catch
            {
                DataTable dt2 = null;
                return dt2;

            }
        }
        public string view_justin(int index, string url)
        {
            if (url == "")
                url = "http://www.manoramaonline.com/news/just-in.feed";
            try
            {
                XmlReader xrdr = XmlReader.Create(url);
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";

                int i = 0;
                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;
                    if (i == index)
                    {
                        news = item.Title.Text + Environment.NewLine + Environment.NewLine + item.Summary.Text;
                    }
                    i++;
                }
                return news;
            }
            catch
            {
                return null;
            }
        }
        public DataTable readsports(string rss)
        {
            DataTable dt = new DataTable();
            try
            {
                XmlReader xrdr = XmlReader.Create(rss);
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";

                dt.Columns.Add("index");
                dt.Columns.Add("title");
                dt.Columns.Add("img");
                dt.Columns.Add("summary");
                int i = 0;
                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;

                    string s = item.Summary.Text;
                    if (item.Title.Text != "" || item.Summary.Text != "")
                    {

                        string img = "";
                        string strt = "img src=";
                        if (s.Contains(strt))
                        {
                            string end = "";
                            if (s.Contains("alt="))
                                end = "alt=";
                            else if (s.Contains(".jpg"))
                                end = ".jpg";
                            else if (s.Contains(".jpeg"))
                                end = ".jpeg";
                            int strtindex = s.IndexOf(strt);
                            strtindex = strtindex + 9;
                            int endindex = s.IndexOf(end);
                            //  endindex = endindex - 1;
                            if (end == "alt=")
                                img = s.Substring(strtindex, (endindex - strtindex));
                            else
                            {
                                if ((endindex - strtindex) > 0)
                                {
                                    img = s.Substring(strtindex, (endindex - strtindex));

                                    img += end;
                                }
                            }
                        }
                        s = Regex.Replace(s, "<.*?>", String.Empty);

                        DataRow dr = dt.NewRow();
                        dr[0] = i;
                        dr[1] = item.Title.Text;
                        //if (rss != "http://www.ibnlive.com/rss/sports.xml")
                        //{
                        // dr[2] = "/img/news_3.gif";
                        // }
                        //else
                        //{
                        if (img != "")
                        {
                            dr[2] = img;
                        }
                        else
                        {
                            dr[2] = "/Images/news_3.gif";

                        }
                        //}
                        dr[3] = s;
                        if (item.Title.Text != null)
                        {
                            dt.Rows.Add(dr);
                        }
                        i++;
                        if (i == 2)
                            break;
                    }


                }
            }
            catch
            {
                DataTable dt1 = null;
                return dt1;
            }

            return dt;
        }

        public DataTable readnews(int index, string rss)
        {
            try
            {
                XmlReader xrdr = XmlReader.Create(rss);
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";
                DataTable dt = new DataTable();
                dt.Columns.Add("index");
                dt.Columns.Add("title");
                dt.Columns.Add("img");
                dt.Columns.Add("summary");
                dt.Columns.Add("date");
                dt.Columns.Add("url");
                dt.Columns.Add("news_prov");
                dt.Columns.Add("category");
                dt.Columns.Add("time");
                int i = 0;

                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;
                    if (i == index)
                    {
                        string s = item.Summary.Text;

                        string img = "";
                        string strt = "img src=";
                        if (s.Contains(strt))
                        {
                            string end = "";
                            if (s.Contains("alt="))
                                end = "alt=";
                            else if (s.Contains(".jpg"))
                                end = ".jpg";
                            else if (s.Contains(".jpeg"))
                                end = ".jpeg";
                            int strtindex = s.IndexOf(strt);
                            strtindex = strtindex + 9;
                            int endindex = s.IndexOf(end);
                            //  endindex = endindex - 1;
                            if (end == "alt=")
                                img = s.Substring(strtindex, (endindex - strtindex));
                            else
                            {
                                img = s.Substring(strtindex, (endindex - strtindex));
                                img += end;
                            }
                        }
                        s = Regex.Replace(s, "<.*?>", String.Empty);
                        DataRow dr = dt.NewRow();
                        string pdate = item.PublishDate.Date.ToString();
                        string ptime = item.PublishDate.Offset.ToString();
                        dr[0] = i;
                        dr[1] = item.Title.Text;
                        //if (rss != "http://www.ibnlive.com/rss/sports.xml")
                        // {
                        //     dr[2] = "/img/news_3.gif";
                        // }
                        //else
                        //{
                        dr[2] = img;
                        // }
                        dr[3] = s;
                        dr[4] = pdate;
                        dr[5] = item.Id;
                        dr[6] = item.Authors;
                        dr[7] = item.Categories;
                        dr[8] = ptime;
                        dt.Rows.Add(dr);
                        break;

                    }
                    i++;
                }
                return dt;

            }
            catch
            {
                DataTable dt1 = null;

                return dt1;
            }
        }

        public DataTable readmain1(string rss)
        {
            DataTable dt = new DataTable();
            try
            {
                XmlReader xrdr = XmlReader.Create(rss);
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";

                dt.Columns.Add("index");
                dt.Columns.Add("title");
                dt.Columns.Add("img");
                dt.Columns.Add("summary");
                int i = 0;
                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;

                    string s = item.Summary.Text;
                    if (item.Title.Text != "" || item.Summary.Text != "")
                    {

                        string img = "";
                        string strt = "img src=";
                        if (s.Contains(strt))
                        {
                            string end = "";
                            if (s.Contains("alt="))
                                end = "alt=";
                            else if (s.Contains(".jpg"))
                                end = ".jpg";
                            else if (s.Contains(".jpeg"))
                                end = ".jpeg";
                            int strtindex = s.IndexOf(strt);
                            strtindex = strtindex + 9;
                            int endindex = s.IndexOf(end);
                            //  endindex = endindex - 1;
                            if (end == "alt=")
                                img = s.Substring(strtindex, (endindex - strtindex));
                            else
                            {
                                if ((endindex - strtindex) > 0)
                                {
                                    img = s.Substring(strtindex, (endindex - strtindex));

                                    img += end;
                                }
                            }
                        }
                        s = Regex.Replace(s, "<.*?>", String.Empty);

                        DataRow dr = dt.NewRow();
                        dr[0] = i;
                        dr[1] = item.Title.Text;
                        //if (rss != "http://www.ibnlive.com/rss/sports.xml")
                        //{
                        // dr[2] = "/img/news_3.gif";
                        // }
                        //else
                        //{
                        if (img != "")
                        {
                            dr[2] = img;
                        }
                        else
                        {
                            dr[2] = "/Images/news_3.gif";

                        }
                        //}
                        dr[3] = s;
                        if (item.Title.Text != null)
                        {
                            dt.Rows.Add(dr);
                        }
                        i++;
                        if (i == 4)
                            break;
                    }


                }
            }
            catch
            {
                DataTable dt1 = null;
                return dt1;
            }

            return dt;
        }


        public DataTable readmain2(string rss)
        {
            DataTable dt = new DataTable();
            try
            {
                XmlReader xrdr = XmlReader.Create(rss);
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";

                dt.Columns.Add("index");
                dt.Columns.Add("title");
                dt.Columns.Add("img");
                dt.Columns.Add("summary");
                int i = 0;
                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;

                    string s = item.Summary.Text;
                    if (item.Title.Text != "" || item.Summary.Text != "")
                    {

                        string img = "";
                        string strt = "img src=";
                        if (s.Contains(strt))
                        {
                            string end = "";
                            if (s.Contains("alt="))
                                end = "alt=";
                            else if (s.Contains(".jpg"))
                                end = ".jpg";
                            else if (s.Contains(".jpeg"))
                                end = ".jpeg";
                            int strtindex = s.IndexOf(strt);
                            strtindex = strtindex + 9;
                            int endindex = s.IndexOf(end);
                            //  endindex = endindex - 1;
                            if (end == "alt=")
                                img = s.Substring(strtindex, (endindex - strtindex));
                            else
                            {
                                if ((endindex - strtindex) > 0)
                                {
                                    img = s.Substring(strtindex, (endindex - strtindex));

                                    img += end;
                                }
                            }
                        }
                        s = Regex.Replace(s, "<.*?>", String.Empty);

                        DataRow dr = dt.NewRow();
                        dr[0] = i;
                        dr[1] = item.Title.Text;
                        //if (rss != "http://www.ibnlive.com/rss/sports.xml")
                        //{
                        // dr[2] = "/img/news_3.gif";
                        // }
                        //else
                        //{
                        if (img != "")
                        {
                            dr[2] = img;
                        }
                        else
                        {
                            dr[2] = "/Images/news_3.gif";

                        }
                        //}
                        dr[3] = s;
                        if (item.Title.Text != null)
                        {
                            dt.Rows.Add(dr);
                        }
                        i++;
                        if (i == 3)
                            break;
                    }


                }
            }
            catch
            {
                DataTable dt1 = null;
                return dt1;
            }

            return dt;
        }

        public DataTable readone(string rss)
        {
            DataTable dt = new DataTable();
            try
            {
                XmlReader xrdr = XmlReader.Create(rss);
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";

                dt.Columns.Add("index");
                dt.Columns.Add("title");
                dt.Columns.Add("img");
                dt.Columns.Add("summary");
                int i = 0;
                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;

                    string s = item.Summary.Text;
                    if (item.Title.Text != "" || item.Summary.Text != "")
                    {

                        string img = "";
                        string strt = "img src=";
                        if (s.Contains(strt))
                        {
                            string end = "";
                            if (s.Contains("alt="))
                                end = "alt=";
                            else if (s.Contains(".jpg"))
                                end = ".jpg";
                            else if (s.Contains(".jpeg"))
                                end = ".jpeg";
                            int strtindex = s.IndexOf(strt);
                            strtindex = strtindex + 9;
                            int endindex = s.IndexOf(end);
                            //  endindex = endindex - 1;
                            if (end == "alt=")
                                img = s.Substring(strtindex, (endindex - strtindex));
                            else
                            {
                                if ((endindex - strtindex) > 0)
                                {
                                    img = s.Substring(strtindex, (endindex - strtindex));

                                    img += end;
                                }
                            }
                        }
                        s = Regex.Replace(s, "<.*?>", String.Empty);

                        DataRow dr = dt.NewRow();
                        dr[0] = i;
                        dr[1] = item.Title.Text;
                        //if (rss != "http://www.ibnlive.com/rss/sports.xml")
                        //{
                        // dr[2] = "/img/news_3.gif";
                        // }
                        //else
                        //{
                        if (img != "")
                        {
                            dr[2] = img;
                        }
                        else
                        {
                            dr[2] = "/Images/news_3.gif";

                        }
                        //}
                        dr[3] = s;
                        if (item.Title.Text != null)
                        {
                            dt.Rows.Add(dr);
                        }
                        i++;
                        if (i == 1)
                            break;
                    }


                }
            }
            catch
            {
                DataTable dt1 = null;
                return dt1;
            }

            return dt;
        }
        public DataTable readlist(string rss)
        {
            DataTable dt = new DataTable();
            try
            {
                XmlReader xrdr = XmlReader.Create(rss);
                SyndicationFeed sfeed = SyndicationFeed.Load(xrdr);
                string news = "";

                dt.Columns.Add("index");
                dt.Columns.Add("title");
                dt.Columns.Add("img");
                dt.Columns.Add("summary");
                int i = 0;
                foreach (SyndicationItem item in sfeed.Items)
                {

                    // news += "    " +item.Title.Text+"   "+ item.Summary.Text;

                    string s = item.Summary.Text;
                    if (item.Title.Text != "" || item.Summary.Text != "")
                    {

                        string img = "";
                        string strt = "img src=";
                        if (s.Contains(strt))
                        {
                            string end = "";
                            if (s.Contains("alt="))
                                end = "alt=";
                            else if (s.Contains(".jpg"))
                                end = ".jpg";
                            else if (s.Contains(".jpeg"))
                                end = ".jpeg";
                            int strtindex = s.IndexOf(strt);
                            strtindex = strtindex + 9;
                            int endindex = s.IndexOf(end);
                            //  endindex = endindex - 1;
                            if (end == "alt=")
                                img = s.Substring(strtindex, (endindex - strtindex));
                            else
                            {
                                if ((endindex - strtindex) > 0)
                                {
                                    img = s.Substring(strtindex, (endindex - strtindex));

                                    img += end;
                                }
                            }
                        }
                        s = Regex.Replace(s, "<.*?>", String.Empty);

                        DataRow dr = dt.NewRow();
                        dr[0] = i;
                        dr[1] = item.Title.Text;
                        //if (rss != "http://www.ibnlive.com/rss/sports.xml")
                        //{
                        // dr[2] = "/img/news_3.gif";
                        // }
                        //else
                        //{
                        if (img != "")
                        {
                            dr[2] = img;
                        }
                        else
                        {
                            dr[2] = "/Images/news_3.gif";

                        }
                        //}
                        dr[3] = s;
                        if (item.Title.Text != null)
                        {
                            dt.Rows.Add(dr);
                        }
                        i++;
                        if (i == 10)
                            break;
                    }


                }
            }
            catch
            {
                DataTable dt1 = null;
                return dt1;
            }

            return dt;
        }
    }
