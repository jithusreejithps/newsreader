using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class Class1
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();

    public Class1()
    {
        con.ConnectionString = "Data Source=sreejith;Initial Catalog=news;Integrated Security=True";
        cmd.Connection = con;

    }
    public void opencon()
    {
        con.Open();
    }
    public void closecon()
    {
        con.Close();
    }
    public SqlCommand getcmd
    {
        get
        {
            return cmd;
        }
    }
}



