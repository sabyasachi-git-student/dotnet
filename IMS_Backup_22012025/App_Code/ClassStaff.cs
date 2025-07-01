using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClassStaff
/// </summary>
public class ClassStaff
{
    public ClassStaff()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int SaveStaffDetails(string stafftype, string designation, string firstname, string middlename, string lastname, string salution, string praddress, string prpostalcode, string resaddress, string respostalcode, string homeno, string mobileno, string workno,
       string fax, string emailaddress, DateTime dateofbirth, string passportnumber, string nationality, string usergroup,
       string site, string username, string password, string mailusername, string mailpassword, string gender, string staffcode, DateTime regdate,
       string FatherName,
		string Department,
		string DateofJoining,
		string Location,
		string PFNo,
		string UANNo,
		string ESICNo,
		string PANNo,
		string AadharNo,
		string MaritualStatus,
		string States,
		string BankName,
		string AccountNo,
		string IFSCCode,
		string BranchName,
        string MarriageAnniversary,
		string ProbationPeriod,
		string IncrementDate,
        string ProfilePicture)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_SaveStaffDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stafftype", stafftype);
            cmd.Parameters.AddWithValue("@designation", designation);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@middlename", middlename);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@salution", salution);
            cmd.Parameters.AddWithValue("@praddress", praddress);
            cmd.Parameters.AddWithValue("@prpostalcode", prpostalcode);
            cmd.Parameters.AddWithValue("@resaddress", resaddress);
            cmd.Parameters.AddWithValue("@respostalcode", respostalcode);
            cmd.Parameters.AddWithValue("@homeno", homeno);
            cmd.Parameters.AddWithValue("@mobileno", mobileno);
            cmd.Parameters.AddWithValue("@workno", workno);
            cmd.Parameters.AddWithValue("@fax", fax);
            cmd.Parameters.AddWithValue("@emailaddress", emailaddress);
            cmd.Parameters.AddWithValue("@dateofbirth", dateofbirth);
            cmd.Parameters.AddWithValue("@passportnumber", passportnumber);
            cmd.Parameters.AddWithValue("@nationality", nationality);
            cmd.Parameters.AddWithValue("@usergroup", usergroup);
            cmd.Parameters.AddWithValue("@site", site);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@mailusername", mailusername);
            cmd.Parameters.AddWithValue("@mailpassword", mailpassword);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@staffcode", staffcode);
            cmd.Parameters.AddWithValue("@regdate", regdate);

            cmd.Parameters.AddWithValue("@FatherName", FatherName);
            cmd.Parameters.AddWithValue("@Department", Department);
            cmd.Parameters.AddWithValue("@DateofJoining", DateofJoining);
            cmd.Parameters.AddWithValue("@Location", Location);
            cmd.Parameters.AddWithValue("@PFNo", PFNo);
            cmd.Parameters.AddWithValue("@UANNo", UANNo);
            cmd.Parameters.AddWithValue("@ESICNo", ESICNo);
            cmd.Parameters.AddWithValue("@PANNo", PANNo);
            cmd.Parameters.AddWithValue("@AadharNo", AadharNo);
            cmd.Parameters.AddWithValue("@MaritualStatus", MaritualStatus);
            cmd.Parameters.AddWithValue("@States", States);
            cmd.Parameters.AddWithValue("@BankName", BankName);
            cmd.Parameters.AddWithValue("@AccountNo", AccountNo);
            cmd.Parameters.AddWithValue("@IFSCCode", IFSCCode);
            cmd.Parameters.AddWithValue("@BranchName", BranchName);

            cmd.Parameters.AddWithValue("@MarriageAnniversary", MarriageAnniversary);
            cmd.Parameters.AddWithValue("@ProbationPeriod", ProbationPeriod);
            cmd.Parameters.AddWithValue("@IncrementDate", IncrementDate);
            cmd.Parameters.AddWithValue("@ProfilePicture", ProfilePicture);
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
    public static DataTable FetchAllBranch()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "Sp_FetchAllBranch";
                    cmd.CommandType = CommandType.StoredProcedure;
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
    public static DataTable FetchUserbyUserName(string username)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Connection.con())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "sp_FetchUserbyUserName";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
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


    public static int UpdateUser(string UserName, string password, string MailId, string usergroup, string BranchId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_UpdateUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@MailId", MailId);
            cmd.Parameters.AddWithValue("@usergroup", usergroup);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
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

    public static int InsertUpdateSpecializeSkills(string StaffCode, string A1text, bool A1IsCheck, string A2text, bool A2IsCheck, string A3text, bool A3IsCheck, string A4text, bool A4IsCheck,
        string A5text, bool A5IsCheck, bool A6IsCheck, bool A7IsCheck, bool A8IsCheck, string A9text, bool A9IsCheck, bool B1IsCheck, bool B2IsCheck, bool B3IsCheck, string B4text, string c1text,
        string d1text, string e1text, string e2text, bool f1IsCheck, bool f2IsCheck, string f3text, string f4text, bool EngRead, bool EngWrite, bool EngSpeak, bool HindiRead, bool HindiWrite, bool HindiSpeak, bool BengRead, bool BengiWrite, bool BengSpeak)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_InsertUpdateSpecializeSkills";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StaffCode", StaffCode);
            cmd.Parameters.AddWithValue("@A1text", A1text);
            cmd.Parameters.AddWithValue("@A1IsCheck", A1IsCheck);
            cmd.Parameters.AddWithValue("@A2text", A2text);
            cmd.Parameters.AddWithValue("@A2IsCheck", A2IsCheck);
            cmd.Parameters.AddWithValue("@A3text", A3text);
            cmd.Parameters.AddWithValue("@A3IsCheck", A3IsCheck);
            cmd.Parameters.AddWithValue("@A4text", A4text);
            cmd.Parameters.AddWithValue("@A4IsCheck", A4IsCheck);
            cmd.Parameters.AddWithValue("@A5text", A5text);
            cmd.Parameters.AddWithValue("@A5IsCheck", A5IsCheck);
            cmd.Parameters.AddWithValue("@A6IsCheck", A6IsCheck);
            cmd.Parameters.AddWithValue("@A7IsCheck", A7IsCheck);
            cmd.Parameters.AddWithValue("@A8IsCheck", A8IsCheck);
            cmd.Parameters.AddWithValue("@A9text", A9text);
            cmd.Parameters.AddWithValue("@A9IsCheck", A9IsCheck);
            cmd.Parameters.AddWithValue("@B1IsCheck", B1IsCheck);
            cmd.Parameters.AddWithValue("@B2IsCheck", B2IsCheck);
            cmd.Parameters.AddWithValue("@B3IsCheck", B3IsCheck);
            cmd.Parameters.AddWithValue("@B4text", B4text);
            cmd.Parameters.AddWithValue("@c1text", c1text);
            cmd.Parameters.AddWithValue("@d1text", d1text);
            cmd.Parameters.AddWithValue("@e1text", e1text);
            cmd.Parameters.AddWithValue("@e2text", e2text);
            cmd.Parameters.AddWithValue("@f1IsCheck", f1IsCheck);
            cmd.Parameters.AddWithValue("@f2IsCheck", f2IsCheck);
            cmd.Parameters.AddWithValue("@f3text", f3text);
            cmd.Parameters.AddWithValue("@f4text", f4text);

            cmd.Parameters.AddWithValue("@EngRead", EngRead);
            cmd.Parameters.AddWithValue("@EngWrite", EngWrite);
            cmd.Parameters.AddWithValue("@EngSpeak", EngSpeak);
            cmd.Parameters.AddWithValue("@HindiRead", HindiRead);
            cmd.Parameters.AddWithValue("@HindiWrite", HindiWrite);
            cmd.Parameters.AddWithValue("@HindiSpeak", HindiSpeak);
            cmd.Parameters.AddWithValue("@BengRead", BengRead);
            cmd.Parameters.AddWithValue("@BengiWrite", BengiWrite);
            cmd.Parameters.AddWithValue("@BengSpeak", BengSpeak);
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

    public static int InsertUpdateEducationAndTraining(string staffcode, string A1, string A2, string A3, string A4, string B1, string B2, string B3, string B4, string C1, string C2, string C3,
        string C4, string D1, string D2, string D3, string D4, string E1, string E2, string E3, string E4, string F1, string F2)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_InsertUpdateEducationAndTraining";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@staffcode", staffcode);
            cmd.Parameters.AddWithValue("@A1", A1);
            cmd.Parameters.AddWithValue("@A2", A2);
            cmd.Parameters.AddWithValue("@A3", A3);
            cmd.Parameters.AddWithValue("@A4", A4);
            cmd.Parameters.AddWithValue("@B1", B1);
            cmd.Parameters.AddWithValue("@B2", B2);
            cmd.Parameters.AddWithValue("@B3", B3);
            cmd.Parameters.AddWithValue("@B4", B4);
            cmd.Parameters.AddWithValue("@C1", C1);
            cmd.Parameters.AddWithValue("@C2", C2);
            cmd.Parameters.AddWithValue("@C3", C3);
            cmd.Parameters.AddWithValue("@C4", C4);
            cmd.Parameters.AddWithValue("@D1", D1);
            cmd.Parameters.AddWithValue("@D2", D2);
            cmd.Parameters.AddWithValue("@D3", D3);
            cmd.Parameters.AddWithValue("@D4", D4);
            cmd.Parameters.AddWithValue("@E1", E1);
            cmd.Parameters.AddWithValue("@E2", E2);
            cmd.Parameters.AddWithValue("@E3", E3);
            cmd.Parameters.AddWithValue("@E4", E4);
            cmd.Parameters.AddWithValue("@F1", F1);
            cmd.Parameters.AddWithValue("@F2", F2);

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


    public static int InsertUpdatePreviousEmployment(string staffcode, string nameofemployer, string Address, string lastposition, string phone, string nameofsupervisor,
                string leavingreason, DateTime startdate, DateTime enddate, string finalsalaryperannum, string duties)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "InsertUpdatePreviousEmployment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@staffcode", staffcode);
            cmd.Parameters.AddWithValue("@nameofemployer", nameofemployer);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@lastposition", lastposition);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@nameofsupervisor", nameofsupervisor);
            cmd.Parameters.AddWithValue("@leavingreason", leavingreason);
            cmd.Parameters.AddWithValue("@startdate", startdate);
            cmd.Parameters.AddWithValue("@enddate", enddate);
            cmd.Parameters.AddWithValue("@finalsalaryperannum", finalsalaryperannum);
            cmd.Parameters.AddWithValue("@duties", duties);
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

    public static int InsertUpdateReference(string staffcode, string name, string company, string Relation, string position, string telephone)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_InsertUpdateReference";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@staffcode", staffcode);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@company", company);
            cmd.Parameters.AddWithValue("@Relation", Relation);
            cmd.Parameters.AddWithValue("@position", position);
            cmd.Parameters.AddWithValue("@telephone", telephone);
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

    public static int SaveStaffImage(string StaffCode, string ImageID, string ImagePath, string ImageOf, string IsIDCard)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_SaveStaffImage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StaffCode", StaffCode);
            cmd.Parameters.AddWithValue("@ImageID", ImageID);
            cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            cmd.Parameters.AddWithValue("@ImageOf", ImageOf);
            cmd.Parameters.AddWithValue("@IsIDCard", IsIDCard);
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


    public static int SaveAttendence(DateTime Dates, string StaffCode, string Name, string Remarks, string UserId,bool ApproveStatus,string BranchId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_SaveAttendence";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Dates", Dates);
            cmd.Parameters.AddWithValue("@StaffCode", StaffCode);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@ApproveStatus", ApproveStatus);
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

    public static int SaveErrorAttendence(DateTime Dates, string StaffCode, string Name, string Remarks, string Error)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_SaveErrorAttendence";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Dates", Dates);
            cmd.Parameters.AddWithValue("@StaffCode", StaffCode);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Remarks", Remarks);
            cmd.Parameters.AddWithValue("@Error", Error);
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


    public static int Active(int RowId, DateTime DOE, string UserId, string BranchId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StaffActiveDeactive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "Active");
            cmd.Parameters.AddWithValue("@RowId", RowId);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@SupplierId", "");
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
    public static int DeActive(int RowId, DateTime DOE, string UserId, string BranchId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StaffActiveDeactive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "DeActive");
            cmd.Parameters.AddWithValue("@RowId", RowId);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@SupplierId", "");
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

    public static int DeActiveSupplier(string SupplierId, DateTime DOE, string UserId, string BranchId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StaffActiveDeactive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "DeActiveSupplier");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
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

    public static int ActiveSupplier(string SupplierId, DateTime DOE, string UserId, string BranchId)
    {
        int n = 0;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = Connection.con();
            cmd.CommandText = "sp_StaffActiveDeactive";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Task", "ActiveSupplier");
            cmd.Parameters.AddWithValue("@RowId", 0);
            cmd.Parameters.AddWithValue("@DOE", DOE);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
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