using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class DBAccess
{
        static SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Profit"].ConnectionString);
        public static string conn = ConfigurationManager.ConnectionStrings["Profit"].ToString();
        public SqlConnection con1 = new SqlConnection(DBAccess.conn);
        public SqlCommand cmd1 = new SqlCommand();

        public DBAccess()
        {
            cmd1.Connection = con1;
        }
        public static bool SaveData(String Query)
        {
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public static DataSet FetchData(String Query)
        {
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            
        }

        public static string FetchDatasingle(String Query)
        {

            string Value2 = "";
            SqlCommand cmd = new SqlCommand(Query, con);

            try
            {
                con.Open();
                Value2 = cmd.ExecuteScalar().ToString();
                return Value2;
            }
            catch (Exception ex)
            {
                return Value2;
            }
            finally
            {
                con.Close();
            }

        }

        public static string Outputvalue(String Query)
        {
            string oValue = "";
            SqlCommand cmd = new SqlCommand(Query, con);
            try
            {
                con.Open();
                oValue = cmd.ExecuteScalar().ToString();
                return oValue;
            }
            catch (Exception ex)
            {
                return oValue;
            }
            finally
            {
                con.Close();

            }
        }
        public static DataTable FetchDatatable(string query)
        {
            try
            {
                DBAccess c1 = new DBAccess();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, c1.con1);
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();

            }
        }
        public static Boolean InsertUpdateData(SqlCommand cmd)
        {
            //String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            //SqlConnection con = new SqlConnection(strConnString);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
                //con.Dispose();
            }
        }
        public static DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception exx)
            {
                return null;
            }
            finally
            {
                con.Close();
                //sda.Dispose();
                //con.Dispose();
            }
        }
        public static void SaveData1(string query)
        {
            //try
            //{
            DBAccess c1 = new DBAccess();
            c1.con1.Open();
            c1.cmd1.CommandText = query;
            c1.cmd1.Connection = c1.con1;
            c1.cmd1.ExecuteNonQuery();
            c1.con1.Close();
            //}
            //catch (Exception ex)
            //{

            //}
        }

        public static SqlDataReader Getrow(string readerqry)
        {
            DBAccess c2 = new DBAccess();
            c2.con1.Open();
            SqlDataReader dr;
            c2.cmd1 = new SqlCommand(readerqry, c2.con1);
            dr = c2.cmd1.ExecuteReader();
            //c2.con.Close();
            return (dr);
        }

        public static DataTable GetTable(string Query)
        {
            DBAccess obj = new DBAccess();
            try
            {
                
                obj.con1.Open();
                SqlDataReader dr;
                obj.cmd1 = new SqlCommand(Query, obj.con1);
                dr = obj.cmd1.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                return (dt);
            }
            catch
            {
                //ClientScript.RegisterStartupScript(Type.GetType("System.String"), "messagebox", "<script type=\"text/javascript\">alert('" + es.Message + "');</script>");
                return null;
            }
            finally
            {
                obj.con1.Close();
            }
        }
        public static string getvoucherno(string query,string pref)
        {
            string voucherno = "";
            String StockInID2 = DBAccess.FetchData(query).Tables[0].Rows[0][0].ToString();
            //StockInID2 = StockInID2.ToString().Replace("-", "/");
            DateTime dt = DateTime.Now;
            string getcurrentyear = dt.Year.ToString();

            string getnextyear = dt.AddYears(1).ToString("yy");
            voucherno = "MOC/" + getcurrentyear + '-' + getnextyear + '/' + pref + '/' + StockInID2;
            return voucherno;
        }
        public static string getDPRId_Equiry(string query, string pref)
        {
            string voucherno = "";
            String StockInID2 = DBAccess.FetchData(query).Tables[0].Rows[0][0].ToString();
            //StockInID2 = StockInID2.ToString().Replace("-", "/");
            DateTime dt = DateTime.Now;
            string getcurrentyear = dt.Year.ToString();

            string getnextyear = dt.AddYears(1).ToString("yy");
            voucherno = "DPR/EN/" + getcurrentyear + '-' + getnextyear + '/' + pref + '/' + StockInID2;
            return voucherno;
        }
        
        
}


