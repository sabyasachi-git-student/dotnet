using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassVendorDetailsMail
/// </summary>
public class ClassVendorDetailsMail
{
	public ClassVendorDetailsMail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int Save(string SupplierId,string VCode,int Days,string EmailId,string UserId,string BranchId,DateTime DOE)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_VendorDetailsMail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TasK", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@VCode", VCode);
            cmd.Parameters.AddWithValue("@Days", Days);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
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

    public static DataTable FetchAll(string SupplierId, string VCode, DateTime DOE)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = Connection.con();
                    cmd.CommandText = "sp_VendorDetailsMail";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TasK", "FetchAll");
                    cmd.Parameters.AddWithValue("@RowId", 0);
                    cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                    cmd.Parameters.AddWithValue("@VCode", VCode);
                    cmd.Parameters.AddWithValue("@Days", 0);
                    cmd.Parameters.AddWithValue("@EmailId", "");
                    cmd.Parameters.AddWithValue("@UserId", "");
                    cmd.Parameters.AddWithValue("@BranchId", "");
                    cmd.Parameters.AddWithValue("@DOE", DOE);
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

    public static int SaveVendor(string SupplierId,string VCode,DateTime DOE,string Groups,string AlternativeContactNo,string WebSite,string Address,string StateCode,
			string AccountHolderName,string AccountNumber,string BankName,string BranchName,string IFSCCode,string BranchAddress,string ConcernedPersonName,string Designation,
            string cContactNo, string cEmailId, string cAddress, string GSTIN, string PANNo, string PANCard, string BankDocuments, string GSTINDocuments)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_VendorDetailsEnterByVendor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TasK", "Save");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
            cmd.Parameters.AddWithValue("@VCode", VCode);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@Groups", Groups);
            cmd.Parameters.AddWithValue("@AlternativeContactNo", AlternativeContactNo);
            cmd.Parameters.AddWithValue("@WebSite", WebSite);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@StateCode", StateCode);
            cmd.Parameters.AddWithValue("@AccountHolderName", AccountHolderName);
            cmd.Parameters.AddWithValue("@AccountNumber", AccountNumber);
            cmd.Parameters.AddWithValue("@BankName", BankName);
            cmd.Parameters.AddWithValue("@BranchName", BranchName);
            cmd.Parameters.AddWithValue("@IFSCCode", IFSCCode);
            cmd.Parameters.AddWithValue("@BranchAddress", BranchAddress);
            cmd.Parameters.AddWithValue("@ConcernedPersonName", ConcernedPersonName);
            cmd.Parameters.AddWithValue("@Designation", Designation);
            cmd.Parameters.AddWithValue("@cContactNo", cContactNo);
            cmd.Parameters.AddWithValue("@cEmailId", cEmailId);
            cmd.Parameters.AddWithValue("@cAddress", cAddress);
            cmd.Parameters.AddWithValue("@GSTIN", GSTIN);
            cmd.Parameters.AddWithValue("@PANNo", PANNo);
            cmd.Parameters.AddWithValue("@PANCard", PANCard);
            cmd.Parameters.AddWithValue("@BankDocuments", BankDocuments);
            cmd.Parameters.AddWithValue("@GSTINDocuments", GSTINDocuments);
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