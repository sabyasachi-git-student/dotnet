using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassRawMaterialMaster
/// </summary>
public class ClassRawMaterialMaster
{
	public ClassRawMaterialMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int Save(string ItemId, string GroupName, string HSNCode, string ItemName, string Unit, string Description,string Colour, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RawMaterialMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@GroupName", GroupName);
            cmd.Parameters.AddWithValue("@HSNCode", HSNCode);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Colour", Colour);
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
    public static int Update(string ItemId, string GroupName, string HSNCode, string ItemName, string Unit, string Description, string Colour, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RawMaterialMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Update");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@GroupName", GroupName);
            cmd.Parameters.AddWithValue("@HSNCode", HSNCode);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Colour", Colour);
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
    public static DataTable FetchForEdit(string ItemId)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_RawMaterialMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchForEdit");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@ItemId", ItemId);
                    cmd.Parameters.AddWithValue("@GroupName", "");
                    cmd.Parameters.AddWithValue("@HSNCode", "");
                    cmd.Parameters.AddWithValue("@ItemName", "");
                    cmd.Parameters.AddWithValue("@Unit", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@ColorName", "");
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
    public static DataTable FetchForPO(string ItemId)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_RawMaterialMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchForPO");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@ItemId", ItemId);
                    cmd.Parameters.AddWithValue("@GroupName", "");
                    cmd.Parameters.AddWithValue("@HSNCode", "");
                    cmd.Parameters.AddWithValue("@ItemName", "");
                    cmd.Parameters.AddWithValue("@Unit", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@ColorName", "");
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
    public static DataTable FetchForItemAdd(string ItemId)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_RawMaterialMaster";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchForItemAdd");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@ItemId", ItemId);
                    cmd.Parameters.AddWithValue("@GroupName", "");
                    cmd.Parameters.AddWithValue("@HSNCode", "");
                    cmd.Parameters.AddWithValue("@ItemName", "");
                    cmd.Parameters.AddWithValue("@Unit", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@Colour", "");
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


    public static int SaveGroup(string GroupID, string GroupName, string GroupDescription, string GroupType)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_groupMasster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@GroupID", GroupID);
            cmd.Parameters.AddWithValue("@GroupName", GroupName);
            cmd.Parameters.AddWithValue("@GroupDescription", GroupDescription);
            cmd.Parameters.AddWithValue("@GroupType", GroupType);
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


    public static int SaveUnit(string UnitID, string UnitName)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_unitMasster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@UnitID", UnitID);
            cmd.Parameters.AddWithValue("@UnitName", UnitName);


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
    public static int UpdateUnit(string UnitID, string UnitName)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_unitMasster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Update");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@UnitID", UnitID);
            cmd.Parameters.AddWithValue("@UnitName", UnitName);


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
    public static int UpdateGroup(string GroupID, string GroupName, string GroupDescription, string GroupType)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_groupMasster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Update");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@GroupID", GroupID);
            cmd.Parameters.AddWithValue("@GroupName", GroupName);
            cmd.Parameters.AddWithValue("@GroupDescription", GroupDescription);
            cmd.Parameters.AddWithValue("@GroupType", GroupType);
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


    public static int Savecolor(string ColorId, string ColorName)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_ColorMasster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ColorId", ColorId);
            cmd.Parameters.AddWithValue("@ColorName", ColorName);
          

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

    public static int Updatecolor(string ColorId, string ColorName)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_ColorMasster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Update");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ColorId", ColorId);
            cmd.Parameters.AddWithValue("@ColorName", ColorName);


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





}