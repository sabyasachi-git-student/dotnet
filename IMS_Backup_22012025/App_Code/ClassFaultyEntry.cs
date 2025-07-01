using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassFaultyEntry
/// </summary>
public class ClassFaultyEntry
{
	public ClassFaultyEntry()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int Save(string FaultyId, string FaultyDate, string Barcode, string Remarks,
       string FaultyReId, string FaultyReason, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_FaultyEntry";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@FaultyId", FaultyId);
            cmd.Parameters.AddWithValue("@FaultyDate", FaultyDate);
            cmd.Parameters.AddWithValue("@Barcode", Barcode);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@FaultyReId", FaultyReId);
            cmd.Parameters.AddWithValue("@FaultyReason", FaultyReason);
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

    public static int SaveDetails(string FaultyId, string FaultyDeId, string FaultyDate, string ItemId, string SrBarVodeID,
        string Barcode, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_FaultyEntry";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@FaultyId", FaultyId);
            cmd.Parameters.AddWithValue("@FaultyDeId", FaultyDeId);
            cmd.Parameters.AddWithValue("@FaultyDate", FaultyDate);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@SrBarVodeID", SrBarVodeID);
            cmd.Parameters.AddWithValue("@Barcode", Barcode);
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


    public static int FaultyInWarSave(string FaultyId,string FaultyInWarId,string ToOEM, string Pickup, string CorComName, string CnNo, string CnDate,
        string RmaNo,string RmaDate,string ImageUpload, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_FaultyInWar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveItemInWar");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@FaultyId", FaultyId);
            cmd.Parameters.AddWithValue("@FaultyInWarId", FaultyInWarId);
            cmd.Parameters.AddWithValue("@OEM", ToOEM);
            cmd.Parameters.AddWithValue("@Pickup", Pickup);
            cmd.Parameters.AddWithValue("@CorrierName", CorComName);
            cmd.Parameters.AddWithValue("@ConsignNo", CnNo);
            cmd.Parameters.AddWithValue("@ConsignDate", CnDate);
            cmd.Parameters.AddWithValue("@RmaNo", RmaNo);
            cmd.Parameters.AddWithValue("@RmaDate", RmaDate);
            cmd.Parameters.AddWithValue("@ImageUpload", ImageUpload);
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

    public static int SaveFaultyInWarDetails(string FaultyInWarId,string FaultyInWarDeId,string SrBarVodeId,string Barcode,string ItemId,string ItemName,string WarrantyPeriod,string WarrantyTo, string CoderLifeTo,string UserId,string BranchId,DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_FaultyInWar";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveItemInWarDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@FaultyInWarId", FaultyInWarId);
            cmd.Parameters.AddWithValue("@FaultyInWarDeId", FaultyInWarDeId);
            cmd.Parameters.AddWithValue("@SrBarVodeId", SrBarVodeId);
            cmd.Parameters.AddWithValue("@Barcode", Barcode);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@WarrantyPeriod", WarrantyPeriod);
            cmd.Parameters.AddWithValue("@WarrantyTo", WarrantyTo);
            cmd.Parameters.AddWithValue("@CoderLifeTo", CoderLifeTo);
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

    public static int FaultyWarExSave(string FaultyId,string FaultyWarExId, string ToRepairer, string CorComName, string CnNo, string CnDate,
        string RmaNo, string RmaDate, string ImageUpload, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_FaultyWarEx";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveItemWarEx");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@FaultyId", FaultyId);
            cmd.Parameters.AddWithValue("@FaultyWarExId", FaultyWarExId);
            cmd.Parameters.AddWithValue("@ToRepairer", ToRepairer);
            cmd.Parameters.AddWithValue("@CorrierName", CorComName);
            cmd.Parameters.AddWithValue("@ConsignNo", CnNo);
            cmd.Parameters.AddWithValue("@ConsignDate", CnDate);
            cmd.Parameters.AddWithValue("@RmaNo", RmaNo);
            cmd.Parameters.AddWithValue("@RmaDate", RmaDate);
            cmd.Parameters.AddWithValue("@ImageUpload", ImageUpload);
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

    public static int SaveFaultyWarExDetails(string FaultyWarExId, string FaultyWarExDeId,string SrBarVodeId,string Barcode, string ItemId, string ItemName, string WarrantyPeriod, string WarrantyTo, string CoderLifeTo, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_FaultyWarEx";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveItemWarExDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@FaultyWarExId", FaultyWarExId);
            cmd.Parameters.AddWithValue("@FaultyWarExDeId", FaultyWarExDeId);
            cmd.Parameters.AddWithValue("@SrBarVodeId", SrBarVodeId);
            cmd.Parameters.AddWithValue("@Barcode", Barcode);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@WarrantyPeriod", WarrantyPeriod);
            cmd.Parameters.AddWithValue("@WarrantyTo", WarrantyTo);
            cmd.Parameters.AddWithValue("@CoderLifeTo", CoderLifeTo);
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