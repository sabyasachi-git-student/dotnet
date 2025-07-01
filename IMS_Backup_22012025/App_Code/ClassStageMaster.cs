using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassStageMaster
/// </summary>
public class ClassStageMaster
{
	public ClassStageMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Save
    public static int Save(string StageId, string StageName, string BranchId, string UserId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StageMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@StageId", StageId);
            cmd.Parameters.AddWithValue("@StageName", StageName);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
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
    #endregion


    #region Edit
    public static int Update(int RowId, string StageName, string BranchId, String UserId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StageMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Update");
            cmd.Parameters.AddWithValue("@RowId", RowId);
            cmd.Parameters.AddWithValue("@StageId", "");
            cmd.Parameters.AddWithValue("@StageName", StageName);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
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
    #endregion

    #region FetchForEdit
    public static DataTable FetchForEdit(int RowId)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_StageMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchForEdit");
                    cmd.Parameters.AddWithValue("@RowId", RowId);
                    cmd.Parameters.AddWithValue("@StageId", "");
                    cmd.Parameters.AddWithValue("@StagepName", "");
                    cmd.Parameters.AddWithValue("@BranchId", "");
                    cmd.Parameters.AddWithValue("@UserId", "");
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
    #endregion
}