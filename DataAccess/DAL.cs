using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace patientportalapi.DataAccess
{
    public class DAL
    {
        public static string dbusername = "sa";
        public static string dbpassword = "muhammad";
        public static string dbname = "userminhaj";
        public static string dbpasword = "@5KmojmC3Tc6czlw";
        //private string connectionstring = "server=WIN-KSKVQUO27T1;Initial Catalog=citilab;Uid=sa;password=Pk123456;Persist Security Info=True;pooling=false;Integrated Security=False";
        private string connectionstring = "server=onlineserver;Initial Catalog=citilab;Uid=" + dbusername + ";password=" + dbpassword + ";Persist Security Info=True;pooling=false;Integrated Security=False";
        private string connectionstring_ = "server=95.217.230.169;Initial Catalog=minhajDB;Uid=" + dbname + ";password=" + dbpasword + ";Persist Security Info=True;pooling=false;Integrated Security=False";

        public Boolean exeQuery(string query)
        {

            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                conn.Close();
                return false;

            }

        }

        public DataTable dtFetchData(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand com = new SqlCommand(query, conn);
            com.CommandTimeout = 0;
            SqlDataReader dreader = com.ExecuteReader();
            dt.Load(dreader);
            conn.Close();
            com.Dispose();
            return dt;
        }
        public Boolean exeQuery_(string query)
        {

            SqlConnection conn_ = new SqlConnection(connectionstring_);
            conn_.Open();

            SqlCommand cmd = new SqlCommand(query, conn_);

            try
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn_.Close();
                return true;
            }
            catch (Exception)
            {
                conn_.Close();
                return false;

            }

        }
        public DataTable dtFetchData_(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection conn_ = new SqlConnection(connectionstring_);
            conn_.Open();
            SqlCommand com = new SqlCommand(query, conn_);
            com.CommandTimeout = 0;
            SqlDataReader dreader = com.ExecuteReader();
            dt.Load(dreader);
            conn_.Close();
            com.Dispose();
            return dt;
        }
    }
}