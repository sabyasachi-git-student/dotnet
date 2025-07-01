using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection
{

	public Connection()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static SqlConnection con()
    { 
       SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Profit"].ConnectionString);
        return con1;
    }
}