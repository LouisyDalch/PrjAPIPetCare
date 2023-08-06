using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrjAPIPetCare.BancoDeDados
{
    public class ConexaoBD
    {
        String connBD = "Server=localhost\\SQLEXPRESS;Database=petcare1;User Id=sa;Password=12345;";
        SqlConnection SqlConnection { get; set; }
        public static ConexaoBD Instance { get { 
            if(instance == null) { instance = new ConexaoBD(); }
            return instance;
            } 
        }
        static ConexaoBD instance;

        public ConexaoBD()
        {
            SqlConnection = new SqlConnection(connBD);
            SqlConnection.Open();
        }
        
        public SqlDataReader ExecuteReader(String sql) { 
            SqlCommand cmd = SqlConnection.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteReader();
        }

        public SqlDataReader ExecuteReader(string sql, SqlParameter[] args)
        {
            SqlCommand cmd = SqlConnection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(args);
            return cmd.ExecuteReader();
        }

        public int ExecuteUpdate(string sql, SqlParameter[] args)
        {
            SqlCommand cmd = SqlConnection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(args);
            return cmd.ExecuteNonQuery();
        }
    }
}