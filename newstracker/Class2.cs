using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


public class Class2
{
    Class1 c = new Class1();

    public string idu(string pname, params object[] objvalues)
    {
        try { 
                c.opencon();
                c.getcmd.CommandType = CommandType.StoredProcedure;
                c.getcmd.CommandText = pname;
                SqlCommandBuilder.DeriveParameters(c.getcmd);
                int i = 0;
                int j = 0;
                foreach (SqlParameter sparam in c.getcmd.Parameters)
                {
                    if (j > 0)
                     {

                     sparam.Value = objvalues[i];
                     i++;
                      }
                    j++;
                 }
            c.getcmd.ExecuteNonQuery();
            c.closecon();
            return "success";
        }
        catch
            {
                c.closecon();
                return "error";
            }
    }
    public DataSet select(string pname, params object[] objvalues)
    {
        
        c.opencon();
        c.getcmd.CommandType = CommandType.StoredProcedure;
        c.getcmd.CommandText = pname;
        SqlCommandBuilder.DeriveParameters(c.getcmd);
        int i = 0;
        int j = 0;
        foreach (SqlParameter sparam in c.getcmd.Parameters)
        {
            if (j > 0)
            {

                sparam.Value = objvalues[i];
                i++;
            }
            j++;
        }
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds = new DataSet();
        adp.SelectCommand = c.getcmd;
        adp.Fill(ds);
        c.closecon();
        return ds;
    }
    public int scale(string sname, params object[] par)
    {
        try
        {
            c.opencon();
            c.getcmd.CommandType = CommandType.StoredProcedure;
            c.getcmd.CommandText = sname;

            SqlCommandBuilder.DeriveParameters(c.getcmd);
            int i = 0;
            int j = 0;
            foreach (SqlParameter sparam in c.getcmd.Parameters)
            {
                if (j > 0)
                {
                    sparam.Value = par[i];
                    i++;

                }
                j++;


            }
            int count = Convert.ToInt16(c.getcmd.ExecuteScalar());
            c.closecon();
            return count;
        }
        catch (Exception e)
        {
            c.closecon();
            return 0;

        }

    }

    public string scalestr(string sname, params object[] par)
    {
        try
        {
            c.opencon();
            c.getcmd.CommandType = CommandType.StoredProcedure;
            c.getcmd.CommandText = sname;

            SqlCommandBuilder.DeriveParameters(c.getcmd);
            int i = 0;
            int j = 0;
            foreach (SqlParameter sparam in c.getcmd.Parameters)
            {
                if (j > 0)
                {
                    sparam.Value = par[i];
                    i++;

                }
                j++;


            }
            string str = c.getcmd.ExecuteScalar().ToString();
            c.closecon();
            return str;
        }
        catch (Exception e)
        {
            c.closecon();
            return "";
        }
        //db.getcmd.ExecuteNonQuery();


    }
}
