using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassRequisitions
/// </summary>
public class ClassRequisitions
{
	public ClassRequisitions()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // Requisition Pop Save
    public static int RequisitionsPOPSave(string ReqPopId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose, 
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status, 
        string Status1, string Status2, string Status3 , string Status4, string Status5, string UserGroupId, 
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionPop";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqPopId", ReqPopId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

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

    // Requisition Pop Details Save
    public static int RequisitionsPOPSaveDetails(string ReqPopId, string ReqPopItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionPop";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqPopId", ReqPopId);
            cmd.Parameters.AddWithValue("@ReqPopItemId", ReqPopItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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

    // Requisition Pop Approval Save
    public static int RequisitionsPOPSaveApp(string ReqPopAppId, string ReqPopId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
       string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
       string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
       string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionPop";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqPopAppId", ReqPopAppId);
            cmd.Parameters.AddWithValue("@ReqPopId", ReqPopId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

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

    // Requisition Pop Details Approval Save
    public static int RequisitionsPOPSaveDetailsApp(string ReqPopAppId, string ReqPopId, string ReqPopItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionPop";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetailsApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqPopAppId", ReqPopAppId);
            cmd.Parameters.AddWithValue("@ReqPopId", ReqPopId);
            cmd.Parameters.AddWithValue("@ReqPopItemId", ReqPopItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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
    
    
    // Requisition Section Save
    public static int RequisitionsSecSave(string ReqSecId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
        string UserId, string BranchId, DateTime DOE, string Forword, string FrorwordFrom)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionSection";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqSecId", ReqSecId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);
            cmd.Parameters.AddWithValue("@Forword", Forword);
            cmd.Parameters.AddWithValue("@FrorwordFrom", FrorwordFrom);

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

    // Requisition Section Details Save
    public static int RequisitionsSecSaveDetails(string ReqSecId, string ReqSecItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionSection";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqSecId", ReqSecId);
            cmd.Parameters.AddWithValue("@ReqSecItemId", ReqSecItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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


    // Requisition Section Approval Save
    public static int RequisitionsSecSaveApp(string ReqSecAppId, string ReqSecId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
        string UserId, string BranchId, DateTime DOE, string Forword, string FrorwordFrom)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionSection";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqSecAppId", ReqSecAppId);
            cmd.Parameters.AddWithValue("@ReqSecId", ReqSecId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);
            cmd.Parameters.AddWithValue("@Forword", Forword);
            cmd.Parameters.AddWithValue("@FrorwordFrom", FrorwordFrom);

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


    // Requisition Section Details Approval Save
    public static int RequisitionsSecSaveDetailsApp(string ReqSecAppId, string ReqSecId, string ReqSecItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionSection";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetailsApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqSecAppId", ReqSecAppId);
            cmd.Parameters.AddWithValue("@ReqSecId", ReqSecId);
            cmd.Parameters.AddWithValue("@ReqSecItemId", ReqSecItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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


    // Requisition Terrytory Save
    public static int RequisitionsTetSave(string ReqTetId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
        string UserId, string BranchId, DateTime DOE, string Forword, string ReqPopId, string ReqSecId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTerrytory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqTetId", ReqTetId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);
            cmd.Parameters.AddWithValue("@Forword", Forword);
            cmd.Parameters.AddWithValue("@ReqPopId", ReqPopId);
            cmd.Parameters.AddWithValue("@ReqSecId", ReqSecId);

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

    // Requisition Terrytory Details Save
    public static int RequisitionsTetSaveDetails(string ReqTetId, string ReqTetItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTerrytory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqTetId", ReqTetId);
            cmd.Parameters.AddWithValue("@ReqTetItemId", ReqTetItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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

    // Requisition Terrytory Approval Save
    public static int RequisitionsTetSaveApp(string ReqTetAppId, string ReqTetId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
        string UserId, string BranchId, DateTime DOE, string Forword, string ReqPopId, string ReqSecId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTerrytory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqTetAppId", ReqTetAppId);
            cmd.Parameters.AddWithValue("@ReqTetId", ReqTetId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);
            cmd.Parameters.AddWithValue("@Forword", Forword);
            cmd.Parameters.AddWithValue("@ReqPopId", ReqPopId);
            cmd.Parameters.AddWithValue("@ReqSecId", ReqSecId);

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

    // Requisition Terrytory Details Approval Save
    public static int RequisitionsTetSaveDetailsApp(string ReqTetAppId, string ReqTetId, string ReqTetItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTerrytory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetailsApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqTetAppId", ReqTetAppId);
            cmd.Parameters.AddWithValue("@ReqTetId", ReqTetId);
            cmd.Parameters.AddWithValue("@ReqTetItemId", ReqTetItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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

    // Requisition Region Save
    public static int RequisitionsGerSave(string ReqRegId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
       string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
       string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
       string UserId, string BranchId, DateTime DOE, string Forword, string ReqPopId, string ReqSecId, string ReqTetId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionRegion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqRegId", ReqRegId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);
            cmd.Parameters.AddWithValue("@Forword", Forword);
            cmd.Parameters.AddWithValue("@ReqPopId", ReqPopId);
            cmd.Parameters.AddWithValue("@ReqSecId", ReqSecId);
            cmd.Parameters.AddWithValue("@ReqTetId", ReqTetId);


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

    // Requisition Region Details Save
    public static int RequisitionsRegSaveDetails(string ReqRegId, string ReqRegItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionRegion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqRegId", ReqRegId);
            cmd.Parameters.AddWithValue("@ReqRegItemId", ReqRegItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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


    // Requisition NOC Save
    public static int RequisitionsNOCSave(string ReqNOCId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status33, string Status4, string Status5, string UserGroupId,
        string UserId, string BranchId, DateTime DOE, string ReasonOfPriority, string TransportationMode)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionNOC";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqNOCId", ReqNOCId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status33", Status33);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@ReasonOfPriority", ReasonOfPriority);
            cmd.Parameters.AddWithValue("@TransportationMode", TransportationMode);
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

    // Requisition NOC Details Save
    public static int RequisitionsNOCSaveDetails(string ReqNOCId, string ReqNOCItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionNOC";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqNOCId", ReqNOCId);
            cmd.Parameters.AddWithValue("@ReqNOCItemId", ReqNOCItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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

    // Requisition NOC Approval Save
    public static int RequisitionsNOCSaveApp(string ReqNOCAppId, string ReqNOCId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
       string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
       string Status1, string Status2, string Status3, string Status4, string Status5, string Status66, string Status7, string UserGroupId,
       string UserId, string BranchId, DateTime DOE, string ReasonOfPriority, string TransportationMode)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionNOC";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqNOCAppId", ReqNOCAppId);
            cmd.Parameters.AddWithValue("@ReqNOCId", ReqNOCId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@Status66", Status66);
            cmd.Parameters.AddWithValue("@Status7", Status7);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@ReasonOfPriority", ReasonOfPriority);
            cmd.Parameters.AddWithValue("@TransportationMode", TransportationMode);

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

    // Requisition NOC Details Approval Save
    public static int RequisitionsNOCSaveDetailsApp(string ReqNOCAppId, string ReqNOCId, string ReqNOCItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionNOC";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetailsApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqNOCAppId", ReqNOCAppId);
            cmd.Parameters.AddWithValue("@ReqNOCId", ReqNOCId);
            cmd.Parameters.AddWithValue("@ReqNOCItemId", ReqNOCItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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


    // Requisition Project Save
    public static int RequisitionsProjectSave(string ReqProId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status4, string Status5, string Status6, string Status7, string UserGroupId,
        string UserId, string BranchId, DateTime DOE, string ReasonOfPriority, string TransportationMode)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionProject";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqProId", ReqProId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@Status6", Status6);
            cmd.Parameters.AddWithValue("@Status7", Status7);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@ReasonOfPriority", ReasonOfPriority);
            cmd.Parameters.AddWithValue("@TransportationMode", TransportationMode);

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

    // Requisition Project Details Save
    public static int RequisitionsProjectSaveDetails(string ReqProId, string ReqProItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionProject";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqProId", ReqProId);
            cmd.Parameters.AddWithValue("@ReqProItemId", ReqProItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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

    // Requisition Project Approval Save
    public static int RequisitionsProjectSaveApp(string ReqProAppId, string ReqProId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
       string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
       string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
       string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionProject";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqProAppId", ReqProAppId);
            cmd.Parameters.AddWithValue("@ReqProId", ReqProId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

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

    // Requisition Project Details Approval Save
    public static int RequisitionsProjectSaveDetailsApp(string ReqProAppId, string ReqProId, string ReqProItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionProject";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetailsApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqProAppId", ReqProAppId);
            cmd.Parameters.AddWithValue("@ReqProId", ReqProId);
            cmd.Parameters.AddWithValue("@ReqProItemId", ReqProItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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


    // Requisition Transfer Save
    public static int RequisitionTransfer(string ReqtransId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status4, string Status5, string Days, string UserGroupId,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTransfer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqtransId", ReqtransId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@Days", Days);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

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

    // Requisition Transfer Details Save
    public static int RequisitionTransferDetails(string ReqtransId, string ReqtransItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTransfer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqtransId", ReqtransId);
            cmd.Parameters.AddWithValue("@ReqtransItemId", ReqtransItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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

    // Requisition Transfer Approval Save
    public static int RequisitionTransferApproval(string ReqtransAppId, string ReqtransId, string RequisitionDate, string PrioritiesId, string RequisitionPurpose,
       string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
       string Status1, string Status2, string Status3, string Status4, string Status5, string Days, string UserGroupId,
       string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTransfer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqtransAppId", ReqtransAppId);
            cmd.Parameters.AddWithValue("@ReqtransId", ReqtransId);
            cmd.Parameters.AddWithValue("@RequisitionDate", RequisitionDate);
            cmd.Parameters.AddWithValue("@PrioritiesId", PrioritiesId);
            cmd.Parameters.AddWithValue("@RequisitionPurpose", RequisitionPurpose);
            cmd.Parameters.AddWithValue("@ReUserGroupId", ReUserGroupId);
            cmd.Parameters.AddWithValue("@ReUserGroupName", ReUserGroupName);
            cmd.Parameters.AddWithValue("@ReUserId", ReUserId);
            cmd.Parameters.AddWithValue("@ReUserName", ReUserName);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@Status1", Status1);
            cmd.Parameters.AddWithValue("@Status2", Status2);
            cmd.Parameters.AddWithValue("@Status3", Status3);
            cmd.Parameters.AddWithValue("@Status4", Status4);
            cmd.Parameters.AddWithValue("@Status5", Status5);
            cmd.Parameters.AddWithValue("@Days", Days);
            cmd.Parameters.AddWithValue("@UserGroupId", UserGroupId);

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

    // Requisition Transfer Details Approval Save
    public static int RequisitionTransferDetailsApproval(string ReqtransAppId, string ReqtransId, string ReqtransItemId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_RequisitionTransfer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetailsApproval");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@ReqtransAppId", ReqtransAppId);
            cmd.Parameters.AddWithValue("@ReqtransId", ReqtransId);
            cmd.Parameters.AddWithValue("@ReqtransItemId", ReqtransItemId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@ItemName", ItemName);
            cmd.Parameters.AddWithValue("@Make", Make);
            cmd.Parameters.AddWithValue("@Model", Model);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@ReqToQty", ReqToQty);
            cmd.Parameters.AddWithValue("@POPQty", POPQty);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Qty1", Qty1);
            cmd.Parameters.AddWithValue("@Status6", Status6);

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