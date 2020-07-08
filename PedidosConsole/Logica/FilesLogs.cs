using System;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace PedidosConsole.Logica
{
     public class FilesLogs
    {

        public string Origin { set; get; }

        public string TypeOrigin { set; get; }

        public string Event { set; get; }

        public string Message { set; get; }

        public EventLogEntryType TypeEntry { set; get; }




        public FilesLogs()
        {
            Origin = "PedidosService";           //Nombre de la aplicación o servicio que genera el evento
            TypeOrigin = "PedidosService";      //Origen del evento (Application/System/Nombre personalizado)
            Event = "Pedido";                   //Nombre del evento a auditar


        }
        public  void save(object obj, Exception ex)
        {
            string fecha = System.DateTime.Now.ToString("yyyyMMdd");
            string hora = System.DateTime.Now.ToString("HH:mm:ss");
            string strLogText = "Some details you want to log.";

            // Create a writer and open the file:
            StreamWriter log;

            if (!File.Exists($"{Origin}.txt"))
            {
                log = new StreamWriter($"{ Origin }.txt");
            }
            else
            {
                log = File.AppendText($"{ Origin }.txt");
            }

            // Write to the file:
            log.WriteLine(DateTime.Now);
            log.WriteLine(strLogText);
            log.WriteLine();

            // Close the stream:
            log.Close();
        }

    }
}
