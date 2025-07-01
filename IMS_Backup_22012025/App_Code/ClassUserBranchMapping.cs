using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassUserBranchMapping
/// </summary>
public class ClassUserBranchMapping
{
	public ClassUserBranchMapping()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int Save(string UserId,string BranchId,string EnterBy,DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_UserBranchMapping";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@EnterBy", EnterBy);
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
    public static DataTable SaveAndFetchBranch(string UserId,string BranchId,string EnterBy)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_UserBranchMapping";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "SaveAndFetchBranch");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BranchId", BranchId);
                    cmd.Parameters.AddWithValue("@EnterBy", EnterBy);
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


    public static int Delete(string UserId, string BranchId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_UserBranchMapping";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Delete");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@EnterBy", "");
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


    public static DataTable FetchCurrentBranchId(string UserId)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_UserBranchMapping";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchCurrentBranchId");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BranchId", "");
                    cmd.Parameters.AddWithValue("@EnterBy", "");
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