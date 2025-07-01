using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassStockIn
/// </summary>
public class ClassStockIn
{
	public ClassStockIn()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int SaveBarcodes(string StockInId, string SrBarVodeID, string ItemId, string Type, string Barcode, 
         string Warranty, string WarrantyTo, string CoderLifeTo, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StockInItemsBarcodes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "SaveBarcodes");
            
            cmd.Parameters.AddWithValue("@StockInId", StockInId);
            cmd.Parameters.AddWithValue("@SrBarVodeID", SrBarVodeID);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Barcode", Barcode);
            cmd.Parameters.AddWithValue("@Warranty", Warranty);
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

    public static int Save(string StockInId, string GRNNo, string SupplierId, string StockInDate,
        string ChallanNo, string ChallanDate, string InvoiceNo, string InvoiceDate, string POID, string ConsignmentNO, string PODate, 
        string DeliveryBy, string ReceiveBy, string Mobile, string VehicleNo, string StateCode, string UserId, string BranchId, DateTime DOE,string ProjectId,string ProjectName)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StockIn";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@StockInId", StockInId);
            cmd.Parameters.AddWithValue("@GRNNo", GRNNo);
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@StockInDate", StockInDate);
            cmd.Parameters.AddWithValue("@ChallanNo", ChallanNo);
            cmd.Parameters.AddWithValue("@ChallanDate", ChallanDate);
            cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            cmd.Parameters.AddWithValue("@InvoiceDate", InvoiceDate);
            cmd.Parameters.AddWithValue("@POID", POID);
            cmd.Parameters.AddWithValue("@ConsignmentNO", ConsignmentNO);
            cmd.Parameters.AddWithValue("@PODate", PODate);
            cmd.Parameters.AddWithValue("@DeliveryBy", DeliveryBy);
            cmd.Parameters.AddWithValue("@ReceiveBy", ReceiveBy);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);
            cmd.Parameters.AddWithValue("@VehicleNo", VehicleNo);
            cmd.Parameters.AddWithValue("@StateCode", StateCode);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
            cmd.Parameters.AddWithValue("@ProjectName", ProjectName);
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

    public static int SaveDetails(string StockInId, string StockInDetailsId, string ItemId, Decimal Qty, string POID,
       string BarcodeStatus, string Warranty, string UserId, string BranchId, DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StockInDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@StockInId", StockInId);
            cmd.Parameters.AddWithValue("@StockInDetailsId", StockInDetailsId);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@POId", POID);
            cmd.Parameters.AddWithValue("@BarcodeStatus", BarcodeStatus);
            cmd.Parameters.AddWithValue("@Warranty", Warranty);
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

    public static DataSet FetchPOItem(string POId)
    {
        try
        {
            DataSet dt = new DataSet();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_StockInDetails";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchPOItem");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@StockInId", "");
                    cmd.Parameters.AddWithValue("@StockInDetailsId", "");
                    cmd.Parameters.AddWithValue("@ItemId", "");
                    cmd.Parameters.AddWithValue("@Qty", 0);
                    cmd.Parameters.AddWithValue("@POId", POId);
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

    public static DataSet FetchStockInDetails(string StockInId)
    {
        try
        {
            DataSet dt = new DataSet();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_StockInDetails";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", "FetchStockInDetails");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@StockInId", StockInId);
                    cmd.Parameters.AddWithValue("@StockInDetailsId", "");
                    cmd.Parameters.AddWithValue("@ItemId", "");
                    cmd.Parameters.AddWithValue("@Qty", 0);
                    cmd.Parameters.AddWithValue("@POId", "");
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
}