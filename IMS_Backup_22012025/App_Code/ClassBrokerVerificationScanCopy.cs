using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassBrokerVerificationScanCopy
/// </summary>
public class ClassBrokerVerificationScanCopy
{
	public ClassBrokerVerificationScanCopy()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int QualityVerificationScanCopy(string image, string guid, string BrokerId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_BrokerVerificationScanCopy";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@image", image);
            cmd.Parameters.AddWithValue("@guid", guid);
            cmd.Parameters.AddWithValue("@BrokerId", BrokerId);
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


    public static int LeadSiteVisitScanCopy(string image, string guid, string StockInId, string GRNNo)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_LeadSiteVisitScanCopy";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@image", image);
            cmd.Parameters.AddWithValue("@guid", guid);
            cmd.Parameters.AddWithValue("@StockInId", StockInId);
            cmd.Parameters.AddWithValue("@GRNNo", GRNNo);

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


    public static int CIBILLoanCheckUploadedFile(string LeadCIBILLoanId, string FileTitelID, string FileTitel, string image, string guid)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_CIBILLoanCheckUploadedFile";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeadCIBILLoanId", LeadCIBILLoanId);
            cmd.Parameters.AddWithValue("@FileTitelID", FileTitelID);
            cmd.Parameters.AddWithValue("@FileTitel", FileTitel);
            cmd.Parameters.AddWithValue("@image", image);
            cmd.Parameters.AddWithValue("@guid", guid);
           

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


    public static int LeadConvertUploadedFile(string LeadConId, string FileTitelID, string FileTitel, string image, string guid)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_LeadConvertUploadedFile";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeadConId", LeadConId);
            cmd.Parameters.AddWithValue("@FileTitelID", FileTitelID);
            cmd.Parameters.AddWithValue("@FileTitel", FileTitel);
            cmd.Parameters.AddWithValue("@image", image);
            cmd.Parameters.AddWithValue("@guid", guid);


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