using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassWarrantyExtension
/// </summary>
public class ClassWarrantyExtension
{
	public ClassWarrantyExtension()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int Save(string WarExId, string WarExDate, string POId, string StoCkInId,
       string ItemId, string Qty, string WarrantyTo, string WarEx, string WarrantyTo1, string FileUpload, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_WarrantyExtension";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@WarExId", WarExId);
            cmd.Parameters.AddWithValue("@WarExDate", WarExDate);
            cmd.Parameters.AddWithValue("@POID", POId);
            cmd.Parameters.AddWithValue("@StoCkInId", StoCkInId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@WarrantyTo", WarrantyTo);
            cmd.Parameters.AddWithValue("@WarrantyExtension", WarEx);
            cmd.Parameters.AddWithValue("@WarrantyTo1", WarrantyTo1);
            cmd.Parameters.AddWithValue("@FileUpload", FileUpload);
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
}