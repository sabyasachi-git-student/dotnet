using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassTDSMasterVendor
/// </summary>
public class ClassTDSMasterVendor
{
	public ClassTDSMasterVendor()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int Save(string Section, decimal Percentage, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_TDSMaster_Vendor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@Section", Section);
            cmd.Parameters.AddWithValue("@Percentage", Percentage);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            n = Convert.ToInt32(cmd.Parameters["@flag"].Value);
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            return n;
        }
        catch (SqlException Ex)
        {
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            return 0;
        }
    }

    public static DataTable FetchTDS()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_TDSMaster_Vendor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchTDS");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@Section", "");
                    cmd.Parameters.AddWithValue("@Percentage", 0);
                    cmd.Parameters.AddWithValue("@UserId", "");
                    cmd.Parameters.AddWithValue("@BranchId", "");
                    cmd.Parameters.AddWithValue("@DOE", DateTime.Now);
                    cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
        catch (SqlException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static int SaveSupplierTDS(string SupplierId, string Section, decimal Percentage)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_TDSMaster_Vendor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveSupplierTDS");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@Section", Section);
            cmd.Parameters.AddWithValue("@Percentage", Percentage);
            cmd.Parameters.AddWithValue("@UserId", SupplierId);
            cmd.Parameters.AddWithValue("@BranchId", "");
            cmd.Parameters.AddWithValue("@DOE", DateTime.Now);
            cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            n = Convert.ToInt32(cmd.Parameters["@flag"].Value);
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            return n;
        }
        catch (SqlException Ex)
        {
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            return 0;
        }
    }

    public static int DeleteSupplierTDS(string SupplierId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_TDSMaster_Vendor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "DeleteSupplierTDS");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@Section", "");
            cmd.Parameters.AddWithValue("@Percentage", 0);
            cmd.Parameters.AddWithValue("@UserId", SupplierId);
            cmd.Parameters.AddWithValue("@BranchId", "");
            cmd.Parameters.AddWithValue("@DOE", DateTime.Now);
            cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            n = Convert.ToInt32(cmd.Parameters["@flag"].Value);
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            return n;
        }
        catch (SqlException Ex)
        {
            if (cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }
            return 0;
        }
    }

    public static DataTable FetchTDSBySupplierId(string SupplierId)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_TDSMaster_Vendor";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchTDSBySupplierId");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@Section", "");
                    cmd.Parameters.AddWithValue("@Percentage", 0);
                    cmd.Parameters.AddWithValue("@UserId", SupplierId);
                    cmd.Parameters.AddWithValue("@BranchId", "");
                    cmd.Parameters.AddWithValue("@DOE", DateTime.Now);
                    cmd.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
        catch (SqlException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}