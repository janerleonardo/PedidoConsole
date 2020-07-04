using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PedidosConsole.Logica
{
   public  class Database
    {

        public string Conexion { set; get; }
        public EventLogs eventLogs = new EventLogs();



        SqlConnection Con = null;
        public Database(string _conexion)
        {
            Conexion = _conexion;
        }

        public void OpenDatabase()
        {
           
            try
            {
                Con = new SqlConnection(Conexion);
                Con.Open();
                Console.WriteLine("Conecto!!!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                eventLogs.WriteEntry($"Error {ex.Message}", EventLogEntryType.Error);
            }


        }

        public void CloseDatabase()
        {
             Con.Close();
        }

        public DataSet Query(string query)
        {
            OpenDatabase();
            DataSet Ds = new DataSet();
            SqlDataAdapter sadapter = new SqlDataAdapter(query, Con);
            sadapter.Fill(Ds);
            CloseDatabase();
            return Ds;

        }

        public DataSet RunQuery(string Command)
        {
            try
            {


                DataSet Ds = new DataSet();
                using (SqlConnection sql = new SqlConnection(Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(Command, sql))
                    {
                        SqlDataAdapter sadapter = new SqlDataAdapter(cmd);
                        sql.Open();
                        sadapter.Fill(Ds);
                    }

                }

                return Ds;
            }
            catch (Exception ex)
            {

                eventLogs.WriteEntry($"Error {ex.Message}", EventLogEntryType.Error);
                throw;
            }

        }

        public DataSet RunStoreProcedure(string Command)
        {
            try
            {

            
                DataSet Ds = new DataSet();
                using (SqlConnection sql = new SqlConnection(Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(Command,sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sadapter = new SqlDataAdapter(cmd);
                        sql.Open();
                        sadapter.Fill(Ds);
                    }

                }

                    return Ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                Console.ReadLine();
                eventLogs.WriteEntry($"Error {ex.Message}", EventLogEntryType.Error);
                throw;
            }
        }

    }
}
