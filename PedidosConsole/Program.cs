using PedidosConsole.Logica;
using PedidosConsole.Model;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace PedidosConsole
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            EventLogs eventLogs = new EventLogs();
            try
            {
                // requires using System.Configuration;
                string programName = "PedidosConsole";
                var sourceHostFile = Directory.GetCurrentDirectory() + @"\" + programName + @".dll.config";
                Console.WriteLine("¡Procesando pedidos (No cierre)!" );
                // to load yourProgram.dll.config
                // With Single-file executables, all files are bundled in a single host file with the .exe extension. 
                // When that file runs for the first time, it unpacks its contents to AppData\Local\Temp\.net\, in a new folder named for the application
                var targetHostFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
                // ignore when in debug mode in vs ide
                if (sourceHostFile != targetHostFile)
                {
                    File.Copy(sourceHostFile, targetHostFile, true);
                }




                string url = ConfigurationSettings.AppSettings["URL_API"].ToString();
                string user = ConfigurationSettings.AppSettings["USER"].ToString();
                string pass = ConfigurationSettings.AppSettings["PASS"].ToString();
                string conexion = ConfigurationSettings.AppSettings["CONEXION"].ToString();
                string comp = ConfigurationSettings.AppSettings["COMPANIA"].ToString();
                string database = ConfigurationSettings.AppSettings["DATABASE"].ToString();

    

                eventLogs.WriteEntry("Sincronizando Pedidos", EventLogEntryType.Information);

                Database databaseTools = new Database(database);
                //DataSet  ds = databaseTools.RunQuery("select top 1 id Id, IdCierre, IdTerceroVendedor,Factura,Placa,DineroTotal,PuntosTercero1 from Adm_Ventas");
                DataSet ds = databaseTools.RunStoreProcedure("dbo.PedidosServices");
                string TipoPedido = "";



                DateTime thisDay = DateTime.Today;
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                   // PedidoModel pedido = new PedidoModel();
                   // MovtoPedidoModel movto = new MovtoPedidoModel();
                    using (PedidoModel pedido = new PedidoModel())
                    {

                  
                            pedido.IdCo = row["IdCo"].ToString();
                            pedido.IdTipoDocto = row["IdTipoDocto"].ToString();
                            pedido.ConsecDocto = int.Parse(row["ConsecDocto"].ToString());
                            pedido.IdFecha = DateTime.Parse(row["IdFecha"].ToString());
                            pedido.IndEstado = int.Parse(row["IndEstado"].ToString());
                            pedido.IndBackorder = short.Parse(row["IndBackorder"].ToString());
                            pedido.IdTerceroFact = row["IdTerceroFact"].ToString();
                            pedido.IdSucursalFact = row["IdSucursalFact"].ToString();
                            pedido.IdTerceroRem = row["IdTerceroRem"].ToString();
                            pedido.IdSucursalRem = row["IdSucursalRem"].ToString();
                            pedido.IdTipoClienteFact = row["IdTipoClienteFact"].ToString();
                            pedido.IdCOFact = row["IdCOFact"].ToString();
                            pedido.FechaEntrega = DateTime.Parse(row["FechaEntrega"].ToString());
                            pedido.NumDiasEntrega = int.Parse(row["NumDiasEntrega"].ToString());
                            pedido.IdMonedaDocto = row["IdMonedaDocto"].ToString();
                            pedido.IdMonedaConv = row["IdMonedaConv"].ToString();
                            pedido.TasaConv = int.Parse(row["TasaConv"].ToString());
                            pedido.IdMonedaLocal = row["IdMonedaLocal"].ToString();
                            pedido.TasaLocal = int.Parse(row["TasaLocal"].ToString());
                            pedido.IdCondPago = row["IdCondPago"].ToString();
                            pedido.IndImpresion = int.Parse(row["IndImpresion"].ToString());
                            pedido.Notas = row["Notas"].ToString();
                            pedido.IdPuntoEnvio = row["IdPuntoEnvio"].ToString();
                            pedido.IdTerceroVendedor = row["IdTerceroVendedor"].ToString();
                            pedido.Contacto = row["Contacto"].ToString();
                            pedido.Direccion1 = row["Direccion1"].ToString();
                            pedido.Pais = row["Pais"].ToString();
                            pedido.Departamento = row["Departamento"].ToString();
                            pedido.Ciudad = row["Ciudad"].ToString();
                            pedido.Barrio = row["Barrio"].ToString();
                            pedido.IndContacto = row["IndContacto"].ToString();

                            using (MovtoPedidoModel movto = new MovtoPedidoModel())
                            {
                                movto.IdItem = int.Parse(row["IdItem"].ToString());
                                movto.IdBodega = row["IdBodega"].ToString();
                                movto.IdConcepto = int.Parse(row["IdConcepto"].ToString());
                                movto.IdMotivo = row["IdMotivo"].ToString();
                                movto.IndObsequio = int.Parse(row["IndObsequio"].ToString());
                                movto.IdCOMovto = row["IdCOMovto"].ToString();
                                movto.IdUNMovto = row["IdUNMovto"].ToString();
                                TipoPedido = row["Pedido"].ToString();
                                if ("CosumoInterno".Equals(TipoPedido))
                                    movto.IdCCostoMovto = row["IdCCostoMovto"].ToString();
                                movto.FechaEntregaMovto = DateTime.Parse(row["FechaEntregaMovto"].ToString());
                                movto.NumDiasEntregaMovto = int.Parse(row["NumDiasEntregaMovto"].ToString());
                                movto.IdListaPrecios = row["IdListaPrecios"].ToString();
                                movto.IdUnidadMedida = row["IdUnidadMedida"].ToString();
                                string CantidadPunto = Double.Parse(row["CantPedidaBase"].ToString()).ToString(CultureInfo.InvariantCulture);
                                movto.CantPedidaBase = CantidadPunto;
                                movto.PrecioUnitario = Double.Parse(row["PrecioUnitario"].ToString());
                                movto.IndImpuestoAsumido = int.Parse(row["IndImpuestoAsumido"].ToString());
                                movto.NotasMovto = row["NotasMovto"].ToString();
                                movto.IndBackorderMovto = int.Parse(row["IndBackorderMovto"].ToString());
                                movto.IndPrecio = int.Parse(row["IndPrecio"].ToString());
                                movto.IdPuntoEnvioMovto = row["IdPuntoEnvioMovto"].ToString();
                                movto.IdTerceroVendedorMovto = row["IdTerceroVendedorMovto"].ToString();

                                pedido.MovimientoPedido.Add(movto);
                          
                                 var JObjetApi = Task.Run(async () => await WebApiEE.CrearPedido(pedido, url, conexion, comp, user, pass)).GetAwaiter().GetResult();
                                Newtonsoft.Json.Linq.JArray Errores = JObjetApi["Errores"];

                                if (Errores.Count > 0)
                                {
                                    eventLogs.WriteEntry($"Error {Errores}, IdItem: {pedido.MovimientoPedido[0].IdItem}, Tipo: {TipoPedido}, IdTerceroFact: {pedido.IdTerceroFact} ", EventLogEntryType.Error);
                                }
                        }
                    }
                }




                //////Borrar
                ////thisDay = thisDay.AddDays(10);


                eventLogs.WriteEntry("Finalizando Sincronizacion Pedidos", EventLogEntryType.Information);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.ReadLine();
                eventLogs.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

        }
    }
}
