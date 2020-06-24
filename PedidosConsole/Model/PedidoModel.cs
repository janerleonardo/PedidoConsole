using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosConsole.Model
{
    public class PedidoModel
    {
        /// <summary>
        /// Username del usuario logeado en Siesa App
        /// </summary>
        [Required(ErrorMessage = "Campo 'user' es obligatorio.")]
        public string user { get; set; }

        /// <summary>
        /// Password del usuario logeado en Siesa App
        /// </summary>
        [Required(ErrorMessage = "Campo 'password' es obligatorio.")]
        public string password { get; set; }

        /// <summary>
        /// Código del centro de operación del documento. f430_id_co
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdCo' es obligatorio.")]
        [MaxLength(3, ErrorMessage = "Campo 'IdCo' debe ser de máximo de 3 carácteres.")]
        public string IdCo { get; set; }

        /// <summary>
        /// Número del documento. f430_consec_docto
        /// </summary>
        [Required(ErrorMessage = "Campo 'ConsecDocto' es obligatorio.")]
        public int ConsecDocto { get; set; }

        /// <summary>
        /// Fecha del documento. f430_id_fecha
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdFecha' es obligatorio.")]
        public DateTime IdFecha { get; set; }

        /// <summary>
        /// f430_ind_estado
        /// Indicador de estado Valores:
        /// 0=Elaboración; 
        /// 1=Aprobado cartera; 
        /// 2=Aprobado;
        /// 9=Anulado
        /// </summary>
        [Required(ErrorMessage = "Campo 'IndEstado' es obligatorio.")]
        public int IndEstado { get; set; }

        /// <summary>
        /// Indicador de backorder Valores:0=Despachar solo lo disponible,
        /// 1=Despachar solo lo disponible y lo demás cancele,
        /// 2=Despachar sólo lineas completas y demas pendientes,
        /// 3=Despachar sólo lineas completas y demás cancele,
        /// 4=Despachar sólo pedido completo,
        /// 5=por linea,
        /// 6=Facturación diferida
        /// </summary>
        public short? IndBackorder { get; set; }

        /// <summary>
        /// Código del tercero a facturar. Excluyente con el identificador interno del tercero del documento(RowidTerceroFact).
        /// </summary>
        [MaxLength(15, ErrorMessage = "Campo 'IdTerceroFact' debe ser de máximo de 15 carácteres.")]
        public string IdTerceroFact { get; set; }

        /// <summary>
        /// f430_rowid_tercero_fact
        /// Identificador interno del tercero a facturar. Excluyente con el código del tercero
        /// del documento(IdTerceroFact).
        /// </summary>
        [Required(ErrorMessage = "Campo 'RowidTerceroFact' es obligatorio.")]
        public int RowidTerceroFact { get; set; }

        /// <summary>
        /// Codigo de la sucursal del tercero a facturar.
        /// </summary>
        [MaxLength(3, ErrorMessage = "Campo 'IdSucursalFact' debe ser de máximo de 3 carácteres.")]
        [Required(ErrorMessage = "Campo 'f430_id_sucursal_fact' es obligatorio.")]
        public string IdSucursalFact { get; set; }

        /// <summary>
        /// Código del tercero a remisionar. Excluyente con el identificador interno del tercero del documento(RowidTerceroRem).
        /// </summary>
        [MaxLength(15, ErrorMessage = "Campo 'IdTerceroRem' debe ser de máximo de 15 carácteres.")]
        public string IdTerceroRem { get; set; }

        /// <summary>
        /// f430_rowid_tercero_fact
        /// Identificador interno del tercero a remisionar.Excluyente con el código del tercero 
        /// del documento(IdTerceroRem).
        /// </summary>
        [Required(ErrorMessage = "Campo 'RowidTerceroRem' es obligatorio.")]
        public int RowidTerceroRem { get; set; }

        /// <summary>
        /// Codigo de la sucursal del tercero a remisionar.
        /// </summary>
        [MaxLength(3, ErrorMessage = "Campo 'IdSucursalRem' debe ser de máximo de 3 carácteres.")]
        public string IdSucursalRem { get; set; }

        /// <summary>
        /// Codigo de tipo cliente.
        /// </summary>
        public string IdTipoClienteFact { get; set; }

        /// <summary>
        /// Código del centro de operación del documento. f430_id_co
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdCOFact' es obligatorio.")]
        [MaxLength(3, ErrorMessage = "Campo 'IdCOFact' debe ser de máximo de 3 carácteres.")]
        public string IdCOFact { get; set; }

        /// <summary>
        /// Fecha de entrega. f430_fecha_entrega
        /// </summary>
        [Required(ErrorMessage = "Campo 'FechaEntrega' es obligatorio.")]
        public DateTime FechaEntrega { get; set; }

        /// <summary> 
        /// Numero de dias de entrega. f430_num_dias_entrega
        /// </summary>
        [Required(ErrorMessage = "Campo 'NumDiasEntrega' es obligatorio.")]
        public int NumDiasEntrega { get; set; }

        /// <summary>
        /// Referencia. f430_num_docto_referencia
        /// </summary>
        [MaxLength(10, ErrorMessage = "Campo 'Referencia' debe ser de máximo de 10 carácteres.")]
        public string Referencia { get; set; }

        /// <summary>
        /// Codigo del cargue.
        /// </summary>
        public string IdCargue { get; set; }


        /// <summary>
        /// Moneda Documento. f430_id_moneda_docto
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdMonedaDocto' es obligatorio.")]
        [MaxLength(3, ErrorMessage = "Campo 'IdMonedaDocto' debe ser de máximo de 3 carácteres.")]
        public string IdMonedaDocto { get; set; }

        /// <summary>
        /// Moneda de conversion. f430_id_moneda_conv
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdMonedaConv' es obligatorio.")]
        [MaxLength(3, ErrorMessage = "Campo 'IdMonedaConv' debe ser de máximo de 3 carácteres.")]
        public string IdMonedaConv { get; set; }

        /// <summary>
        /// f430_tasa_conv
        /// Tasa de conversion Coloque 1 si la moneda del documento es 
        /// igual a la moneda local o a la moneda base de conversion 
        /// de lo contrario coloque la tasa de conversion.
        /// </summary>
        [Required(ErrorMessage = "Campo 'TasaConv' es obligatorio.")]
        public double TasaConv { get; set; }

        /// <summary>
        /// Moneda local. f430_id_moneda_local
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdMonedaLocal' es obligatorio.")]
        [MaxLength(3, ErrorMessage = "Campo 'IdMonedaLocal' debe ser de máximo de 3 carácteres.")]
        public string IdMonedaLocal { get; set; }

        /// <summary>
        /// f430_tasa_local
        /// Tasa local Coloque 1 si la moneda del documento es igual 
        /// a la moneda local de lo contrario coloque la tasa de cambio.
        /// </summary>
        [Required(ErrorMessage = "Campo 'TasaLocal' es obligatorio.")]
        public double TasaLocal { get; set; }

        /// <summary>
        /// Condicion de pago. f430_id_cond_pago
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdCondPago' es obligatorio.")]
        [MaxLength(3, ErrorMessage = "Campo 'IdCondPago' debe ser de máximo de 3 carácteres.")]
        public string IdCondPago { get; set; }

        /// <summary>
        /// f430_ind_impresion
        /// Indicador de impresion Valores:0=No Impreso; 1=Impreso.
        /// </summary>
        [Required(ErrorMessage = "Campo 'IndImpresion' es obligatorio.")]
        public int IndImpresion { get; set; }

        /// <summary>
        /// Notas. f430_notas
        /// </summary>
        [MaxLength(2000, ErrorMessage = "Campo 'Notas' debe ser de máximo de 2000 carácteres.")]
        public string Notas { get; set; }

        /// <summary>
        /// Código del tercero cliente contado. Excluyente con el identificador
        /// interno del tercero cliente contado(RowidCliContado).
        /// </summary>
        [MaxLength(15, ErrorMessage = "Campo 'IdCliContado' debe ser de máximo de 15 carácteres.")]
        public string Contacto { get; set; }


        /// <summary>
        /// Identificador interno del tercero cliente contado.
        /// Excluyente con el código del tercero cliente contado(IdCliContado).
        /// </summary>
        public int RowidCliContado { get; set; }


        /// <summary>
        /// Código del punto de envio. Excluyente con el identificador interno del punto de envio(RowidPuntoEnvio).
        /// </summary>
        public string IdPuntoEnvio { get; set; }


        /// <summary>
        /// RowidPuntoEnvio
        /// Identificador interno del punto de envio. Excluyente con el código del punto de envio(IdPuntoEnvio).
        /// </summary>
        public int RowidPuntoEnvio { get; set; }

        /// <summary>
        /// IdTerceroVendedor
        /// Código del tercero vendedor. Excluyente con el identificador interno del tercero 
        /// vendedor(RowidTerceroVendedor).
        /// </summary>
        [MaxLength(15, ErrorMessage = "Campo 'IdTerceroVendedor' debe ser de máximo de 15 carácteres.")]
        public string IdTerceroVendedor { get; set; }

        /// <summary>
        /// f430_rowid_tercero_vend
        /// Identificador interno del tercero vendedor. Excluyente con el código del tercero.
        /// </summary>
        public int RowidTerceroVendedor { get; set; }



        /// <summary>
        /// ID del vendedor
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdVendedor' es obligatorio.")]
        public string IdVendedor { get; set; }

        /// <summary>
        /// ID de la compañia
        /// </summary>

        [Required(ErrorMessage = "Campo 'IdCia' es obligatorio.")]
        public string IdCia { get; set; }

        /// <summary>
        /// Direccion 1.
        /// </summary>
        [MaxLength(40, ErrorMessage = "Campo 'Direccion1' debe ser de máximo de 40 carácteres.")]
        public string Direccion1 { get; set; }

        /// <summary>
        /// Direccion 1.
        /// </summary>
        [MaxLength(40, ErrorMessage = "Campo 'Direccion2' debe ser de máximo de 40 carácteres.")]
        public string Direccion2 { get; set; }

        /// <summary>
        /// Direccion 2.
        /// </summary>
        [MaxLength(40, ErrorMessage = "Campo 'Direccion3' debe ser de máximo de 40 carácteres.")]
        public string Direccion3 { get; set; }

        /// <summary>
        /// Id Pais.
        /// </summary>
        [MaxLength(3, ErrorMessage = "Campo 'Pais' debe ser de máximo de 3 carácteres.")]
        public string Pais { get; set; }

        /// <summary>	
        ///  Id Departamento.
        /// </summary>
        [MaxLength(2, ErrorMessage = "Campo 'Departamento' debe ser de máximo de 2 carácteres.")]
        public string Departamento { get; set; }

        /// <summary>
        /// Id Ciudad.
        /// </summary>
        [MaxLength(3, ErrorMessage = "Campo 'Ciudad' debe ser de máximo de 3 carácteres.")]
        public string Ciudad { get; set; }

        /// <summary>
        ///	Id Barrio.
        /// </summary>
        [MaxLength(40, ErrorMessage = "Campo 'Barrio' debe ser de máximo de 40 carácteres.")]
        public string Barrio { get; set; }

        /// <summary>
        /// Telefono.
        /// </summary>
        [MaxLength(20, ErrorMessage = "Campo 'Telefono' debe ser de máximo de 20 carácteres.")]
        public string Telefono { get; set; }

        /// <summary>
        /// Fax.
        /// </summary>
        [MaxLength(20, ErrorMessage = "Campo 'Fax' debe ser de máximo de 20 carácteres.")]
        public string Fax { get; set; }

        /// <summary>
        /// CodPostal.
        /// </summary>
        [MaxLength(10, ErrorMessage = "Campo 'CodPostal' debe ser de máximo de 10 carácteres.")]
        public string CodPostal { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [MaxLength(255, ErrorMessage = "Campo 'Email' debe ser de máximo de 255 carácteres.")]
        public string Email { get; set; }

        /// <summary>
        /// Identificador interno del contacto Excluyente con la informacion de contacto 
        /// (Contacto),(Direccion1),(Direccion2),(Direccion3),(Ciudad),(Barrio),
        /// (Telefono),(Fax),(CodPostal),(Email)
        /// </summary>
        public int RowidContacto { get; set; }

        /// <summary>
        /// f430_tasa_dscto_global_cap
        /// Tasa de descuento global.
        /// </summary>
        public double TasaDsctoGlobalCap { get; set; }

        /// <summary>
        /// Unidad de negocio del movimiento. viene de f431_id_un_movto
        /// </summary>
        [MaxLength(20, ErrorMessage = "Campo 'IdUnSuc' debe ser de máximo de 20 carácteres.")]
        public string IdUnSuc { get; set; }


        /// <summary>
        /// Orden de compra del documento
        /// </summary>
        [MaxLength(50, ErrorMessage = "Campo 'NumDoctoReferencia' debe ser de máximo de 50 carácteres.")]
        public string NumDoctoReferencia { get; set; }

        /// <summary>
        /// Unidad de negocio del movimiento. viene de f431_id_un_movto
        /// </summary>
        [Required(ErrorMessage = "Campo 'IndContacto' es obligatorio.")]
        [Range(0, 1, ErrorMessage = "Campo 'IndContacto' debe estar en un rango de 0 a 1")]
        public string IndContacto { get; set; }


        /// <summary>
        /// Listado de Movimientos del Pedido (Items)
        /// </summary>
        public List<MovtoPedidoModel> MovimientoPedido { get; set; } = new List<MovtoPedidoModel>();

        #region Seccion Campos Consulta Entity
        /// <summary>
        /// Código del tipo de documento.
        /// </summary>
        [MaxLength(3, ErrorMessage = "Campo 'IdTipoDocto' debe ser de máximo de 3 carácteres.")]
        public string IdTipoDocto { get; set; }



        #endregion



    }
}
