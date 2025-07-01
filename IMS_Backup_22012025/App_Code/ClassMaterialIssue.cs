using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ClassMaterialIssue
/// </summary>
public class ClassMaterialIssue
{
	public ClassMaterialIssue()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int MaterialIssueSave(string IssueId, string ReqAppId, string ReqId, string IssueDate, string PrioritiesId, string RequisitionPurpose,
        string ReUserGroupId, string ReUserGroupName, string ReUserId, string ReUserName, string Remarks, string Status,
        string Status1, string Status2, string Status3, string Status4, string Status5, string UserGroupId,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_MaterialIssue";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@IssueId", IssueId);
            cmd.Parameters.AddWithValue("@ReqAppId", ReqAppId);
            cmd.Parameters.AddWithValue("@ReqId", ReqId);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
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

    public static int MaterialIssueSaveDetails(string IssueId, string IssueItemId, string ReqAppId, string ReqId, string ItemId, string Category, string ItemName,
        string Make, string Model, string Unit, Decimal ReqToQty, Decimal POPQty, Decimal Qty, Decimal Qty1, string Status6,
        string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_MaterialIssue";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveDetails");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@IssueId", IssueId);
            cmd.Parameters.AddWithValue("@IssueItemId", IssueItemId);
            cmd.Parameters.AddWithValue("@ReqAppId", ReqAppId);
            cmd.Parameters.AddWithValue("@ReqId", ReqId);
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