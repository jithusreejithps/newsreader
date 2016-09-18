using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

    public class apriori
    {
        Class2 d = new Class2();
       
        public string[] apriori1(string username)
        {
            try
            {
                DataSet ds = d.select("get_item_set", username);
                DataTable ItemSet = new DataTable();
                ItemSet.Columns.Add("cid");
                ItemSet.Columns.Add("pid");
                if (ds.Tables != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string cid = ds.Tables[0].Rows[i]["c_id"].ToString();
                            string pid = ds.Tables[0].Rows[i]["p_id"].ToString();


                            DataRow dr = ItemSet.NewRow();
                            dr[0] = cid;
                            dr[1] = pid;
                            ItemSet.Rows.Add(dr);

                        }

                    }
                }
                //Roule
                DataTable Frequent = new DataTable();
                Frequent.Columns.Add("cid");
                Frequent.Columns.Add("pid");
                Frequent.Columns.Add("support");
                for (int i = 0; i < ItemSet.Rows.Count; i++)
                {
                    if (Frequent != null)
                    {
                        bool flag = false;
                        for (int j = 0; j < Frequent.Rows.Count; j++)
                        {
                            string cid = Frequent.Rows[j]["cid"].ToString();
                            string pid = Frequent.Rows[j]["pid"].ToString();
                            if (ItemSet.Rows[i]["pid"].ToString() == pid && ItemSet.Rows[i]["cid"].ToString() == cid)
                            {
                                Frequent.Rows[j]["support"] = Convert.ToInt16(Frequent.Rows[j]["support"]) + 1;
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            DataRow dr = Frequent.NewRow();
                            dr[0] = ItemSet.Rows[i]["cid"];
                            dr[1] = ItemSet.Rows[i]["pid"];
                            dr[2] = 1;
                            Frequent.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow dr = Frequent.NewRow();
                        dr[0] = ItemSet.Rows[i]["cid"];
                        dr[1] = ItemSet.Rows[i]["pid"];
                        dr[2] = 1;
                        Frequent.Rows.Add(dr);
                    }
                }
                ///Input
                ///                
                int support = 0;
                int index = -1;
                for (int i = 0; i < Frequent.Rows.Count; i++)
                {
                    int sprt = Convert.ToInt16(Frequent.Rows[i]["support"]);
                    if (sprt > support)
                    {
                        support = sprt;
                        index = i;
                    }
                }
                string c_id = Frequent.Rows[index]["cid"].ToString();
                string p_id = Frequent.Rows[index]["pid"].ToString();
                string[] result = { c_id, p_id };
                return result;
            }
            catch (Exception)
            {

                return null;
            }
            
            ///
        }
    }
