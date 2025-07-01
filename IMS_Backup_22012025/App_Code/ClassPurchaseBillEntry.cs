using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassPurchaseBillEntry
/// </summary>
public class ClassPurchaseBillEntry
{
	public ClassPurchaseBillEntry()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int Save(string PurchaseBillId, string StockInId, string SupplierId, string InvoiceNo, string InvoiceDate, 
        string StatCode, decimal GrandTotal, decimal Freight, decimal OtherCharges, decimal TotalBillAmount, decimal FreightCGST,
        decimal FreightSGST, decimal FreightIGST, decimal FreightWithGST, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_PurchaseBill";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@PurchaseBillId", PurchaseBillId);
            cmd.Parameters.AddWithValue("@StockInId", StockInId);
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            cmd.Parameters.AddWithValue("@InvoiceDate", InvoiceDate);
            cmd.Parameters.AddWithValue("@StatCode", StatCode);
            cmd.Parameters.AddWithValue("@GrandTotal", GrandTotal);
            cmd.Parameters.AddWithValue("@Freight", Freight);
            cmd.Parameters.AddWithValue("@OtherCharges", OtherCharges);
            cmd.Parameters.AddWithValue("@TotalBillAmount", TotalBillAmount);
            cmd.Parameters.AddWithValue("@FreightCGST", FreightCGST);
            cmd.Parameters.AddWithValue("@FreightSGST", FreightSGST);
            cmd.Parameters.AddWithValue("@FreightIGST", FreightIGST);
            cmd.Parameters.AddWithValue("@FreightWithGST", FreightWithGST);
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

    public static int Paid(string PurchaseBillId, DateTime PaidDate)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_PurchaseBill";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Paid");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@PurchaseBillId", PurchaseBillId);
            cmd.Parameters.AddWithValue("@StockInId", "");
            cmd.Parameters.AddWithValue("@SupplierId", "");
            cmd.Parameters.AddWithValue("@InvoiceNo", "");
            cmd.Parameters.AddWithValue("@InvoiceDate", "");
            cmd.Parameters.AddWithValue("@StatCode", "");
            cmd.Parameters.AddWithValue("@GrandTotal", 0);
            cmd.Parameters.AddWithValue("@UserId", "");
            cmd.Parameters.AddWithValue("@BranchId", "");
            cmd.Parameters.AddWithValue("@DOE", PaidDate);
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

    public static int SaveDetails(string PurchaseBillId, string ItemId, string HSNCode, decimal Qty, decimal Rate, decimal Amount,
        decimal CGSTPer, decimal CGSTAmount, decimal SGSTPer, decimal SGSTAmount, decimal IGSTPer, decimal IGSTAmount, decimal CESSPer, decimal CESSAmount, decimal Total, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_PurchaseBillDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@PurchaseBillId", PurchaseBillId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@HSNCode", HSNCode);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Rate", Rate);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@CGSTPer", CGSTPer);
            cmd.Parameters.AddWithValue("@CGSTAmount", CGSTAmount);
            cmd.Parameters.AddWithValue("@SGSTPer", SGSTPer);
            cmd.Parameters.AddWithValue("@SGSTAmount", SGSTAmount);
            cmd.Parameters.AddWithValue("@IGSTPer", IGSTPer);
            cmd.Parameters.AddWithValue("@IGSTAmount", IGSTAmount);
            cmd.Parameters.AddWithValue("@CESSPer", CESSPer);
            cmd.Parameters.AddWithValue("@CESSAmount", CESSAmount);
            cmd.Parameters.AddWithValue("@Total", Total);
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