using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PedidosConsole.Logica
{
    public class EventLogs
    {
    
        /// <summary>
        /// Nombre de la aplicación o servicio que genera el Event
        /// </summary>
        public string Origin { set; get; }
      

        /// <summary>
        /// Type de Event a anotar. Posibles opciones (Application / System / nombre personalizado)
        /// </summary>
        public string TypeOrigin { set; get; }
       
     
        /// <summary>
        /// Nombre del Event a auditar
        /// </summary>
        public string Event { set; get; }
        
        /// <summary>
        /// Texto del Message a anotar
        /// </summary>
        public string Message { set; get; }
       
       
        /// <summary>
        /// Type de entrada para el Event.  Posibles opciones (1=Error/2=FailureAudit/3=Information/4=SuccessAudit/5=Warning)
        /// </summary>
        public EventLogEntryType TypeEntry { set; get; }
        

        
        /// <summary>
        /// File de logs en el que hará las anotaciones.  Debe incluir el nombre del File con la extensión
        /// </summary>
        public string File { set; get; }


        public EventLogs()
        {
            Origin = "PedidosService";           //Nombre de la aplicación o servicio que genera el evento
            TypeOrigin = "PedidosService";   //Origen del evento (Application/System/Nombre personalizado)
            Event = "Pedido";           //Nombre del evento a auditar
            //evento.Mensaje = mensaje;
            //evento.TipoEntrada = tipo;   // 1=Error/2=FailureAudit/3=Information/4=SuccessAudit/5=Warning
            File = "";
        }



        /// <summary>
        /// Métodos para anotar logs de Events de las aplicaciones
        /// </summary>
        public void WriteEntry(string message, EventLogEntryType type)
        {
            try
            {
                Message = message;
                TypeEntry = type;
                EventLog miLog = new EventLog(TypeOrigin, ".", Origin);
                //Comprobamos si existe el registro de sucesos
                if (!EventLog.SourceExists(Origin))
                {
                    //Si no existe el registro de sucesos, lo creamos
                    EventLog.CreateEventSource(Origin, TypeOrigin);
                }
                else
                {
                    // Recupera el registro de sucesos correspondiente del Origin.
                    TypeOrigin = EventLog.LogNameFromSourceName(Origin, ".");
                }

                miLog.Source = Origin;
                miLog.Log = TypeOrigin;

                //Comprobamos el Type de anotación y grabamos el Event
                switch (TypeEntry)
                {
                    case EventLogEntryType.Error:
                        miLog.WriteEntry(Message, EventLogEntryType.Error);
                        break;
                    case EventLogEntryType.FailureAudit:
                        miLog.WriteEntry(Message, EventLogEntryType.FailureAudit);
                        break;
                    case EventLogEntryType.Information:
                        miLog.WriteEntry(Message, EventLogEntryType.Information);
                        break;
                    case EventLogEntryType.SuccessAudit:
                        miLog.WriteEntry(Message, EventLogEntryType.SuccessAudit);
                        break;
                    case EventLogEntryType.Warning:
                        miLog.WriteEntry(Message, EventLogEntryType.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
