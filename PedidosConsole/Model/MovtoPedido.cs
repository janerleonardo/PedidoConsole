using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosConsole.Model
{
    public class MovtoPedidoModel : IDisposable
    {
        /// <summary>
        /// Código del item. Excluyente con el identificador interno 
        /// del item,la referencia y el codigo de barras.
        /// Obligatorio si el identificador interno del item(RowidItem)
        /// es cero(0),la referencia(ReferenciaItem) es vacia y 
        /// el codigo de barras(CodigoBarras) es vacio.
        /// </summary>
        public int IdItem { get; set; }



        /// <summary>
        /// f431_rowid_item_ext 
        /// Identificador interno del item. Excluyente con el Código 
        /// del item(IdItem),La referencia(ReferenciaItem),Codigo de
        /// barras(CodigoBarras). Obligatorio si el Código del 
        /// item (IdItem) es vacio,La referencia(ReferenciaItem),
        /// Codigo de barras(CodigoBarras).
        /// </summary>
        public int RowidItemExt { get; set; }

        /// <summary>
        /// Referencia del item. Excluyente con el Código del item(IdItem),
        /// identificador interno del item(RowidItem) 
        /// y el codigo de barras(CodigoBarras).
        /// Obligatorio si el Código del item (IdItem) es vacio,
        /// identificador interno del item(RowidItem) es cero(0) 
        /// y el codigo de barras es vacio.
        /// </summary>
        [MaxLength(50, ErrorMessage = "Campo 'ReferenciaItem' debe ser de máximo de 50 carácteres.")]
        public string ReferenciaItem { get; set; }


        /// <summary>
        /// codigo de barras del item. Excluyente con el Código del item(IdItem),
        /// identificador interno del item(RowidItem) y la referencia(ReferenciaItem). 
        /// Obligatorio si el Código del item (IdItem) es vacio,
        /// identificador interno del item(RowidItem) es cero(0)
        /// y la referencia(ReferenciaItem) es vacio.
        /// </summary>
        public int CodigoBarras { get; set; }



        /// <summary>
        /// Motivo
        /// </summary>
        [Required(ErrorMessage = "Movto: El motivo es obligatorio")]
        [MaxLength(2, ErrorMessage = "Movto: El codigo del motivo debe ser de máximo 2 posiciones.")]
        public string IdMotivo { get; set; }


        /// <summary>
        /// Código de la bodega. Excluyente con el identificador interno de la bodega(RowidBodega).
        /// </summary>
        [MaxLength(5, ErrorMessage = "Campo 'IdBodega' debe ser de máximo de 5 carácteres.")]
        public string IdBodega { get; set; }

        /// <summary>
        /// f431_rowid_bodega 
        /// Identificador interno de la bodega. Excluyente con el código de la bodega(IdBodega).
        /// </summary>
        public int RowidBodega { get; set; }

        /// <summary>
        /// Conceptos.
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdConcepto' es obligatorio.")]
        public int IdConcepto { get; set; }

        /// <summary>
        /// f431_ind_obsequio 
        /// Indicador de obsequio Valores: 0=No, 1=Si.
        /// </summary>
        [Required(ErrorMessage = "Campo 'f431_ind_obsequio' es obligatorio.")]
        [Range(0, 1, ErrorMessage = "Campo 'f431_ind_obsequio' debe estar en un rango de 0 a 1")]
        public int IndObsequio { get; set; }

        /// <summary>
        /// f431_id_co_movto 
        /// Centro de operacion del movimiento.
        /// </summary>
        [MaxLength(3, ErrorMessage = "Campo 'IdCOMovto' debe ser de máximo de 3 carácteres.")]
        public string IdCOMovto { get; set; }

        /// <summary>
        /// f431_id_un_movto
        /// Centro de operacion del movimiento.
        /// </summary>
        [MaxLength(20, ErrorMessage = "Campo 'IdUNMovto' debe ser de máximo de 20 carácteres.")]
        public string IdUNMovto { get; set; }

        /// <summary>
        /// Código del centro de costo del movimiento.
        /// Excluyente con el identificador interno del centro de costo(RowidCCostoMovto).
        /// </summary>
        [MaxLength(15, ErrorMessage = "Campo 'IdUNMovto' debe ser de máximo de 15 carácteres.")]
        public string IdCCostoMovto { get; set; }

        /// <summary>
        /// Identificador interno del centro de costos. 
        /// Excluyente con el código del centro de costos(IdCCostoMovto).
        /// </summary>
        public int RowidCCostoMovto { get; set; }

        /// <summary>
        /// Código del proyecto del movimiento.
        /// </summary>
        [MaxLength(15, ErrorMessage = "Campo 'IdProyectoMovto' debe ser de máximo de 15 carácteres.")]
        public string IdProyectoMovto { get; set; }

        /// <summary>
        /// f431_fecha_entrega 
        /// Fecha de entrega del movimiento.
        /// </summary>
        [Required(ErrorMessage = "Campo 'FechaEntregaMovto' es obligatorio.")]
        public DateTime FechaEntregaMovto { get; set; }

        /// <summary>
        /// Numero de dias de entrega del movimiento
        /// </summary>
        [Range(0, 9999, ErrorMessage = "Campo 'NumDiasEntregaMovto' debe estar en un rango de 0 a 9999")]
        public int NumDiasEntregaMovto { get; set; }

        /// <summary>
        /// f431_id_lista_precio 
        /// Código de la lista de precios del movimiento..
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdListaPrecios' es obligatorio.")]
        [MaxLength(3, ErrorMessage = "Campo 'IdListaPrecios' debe ser de máximo de 3 carácteres.")]
        public string IdListaPrecios { get; set; }

        /// <summary>
        /// f431_id_unidad_medida_captura
        /// Código de la unidad de medida del movimiento..
        /// </summary>
        [Required(ErrorMessage = "Campo 'IdUnidadMedida' es obligatorio.")]
        [MaxLength(4, ErrorMessage = "Campo 'IdUnidadMedida' debe ser de máximo de 4 carácteres.")]
        public string IdUnidadMedida { get; set; }

        /// <summary>
        /// f431_cant1_pedida
        /// Código de la unidad de medida del movimiento..
        /// </summary>
        [Required(ErrorMessage = "Campo 'CantPedidaBase' es obligatorio.")]
        public string CantPedidaBase { get; set; }

        /// <summary>
        /// Cantidad adicional del movimiento.
        /// </summary>
        public double Cant2Pedida { get; set; }

        /// <summary>
        /// Precio unitario del movimiento.
        /// </summary>
        /// .0
        public double PrecioUnitario { get; set; }

        /// <summary>
        /// f431_ind_impuesto_precio_venta
        /// Indicador de impuesto asumido Valores: 0=No liquida; 1=Asume compañía, 2=Asume cliente.
        /// </summary>
        [Required(ErrorMessage = "Campo 'IndImpuestoAsumido' es obligatorio.")]
        [Range(0, 2, ErrorMessage = "Campo 'IndImpuestoAsumido' debe estar en un rango de 0 a 2")]
        public int IndImpuestoAsumido { get; set; }

        /// <summary>
        /// f431_notas 
        /// Notas del movimiento..
        /// </summary>
        [MaxLength(255, ErrorMessage = "Campo 'NotasMovto' debe ser de máximo de 255 carácteres.")]
        public string NotasMovto { get; set; }

        /// <summary>
        /// Detalle del movimiento.
        /// </summary>
        [MaxLength(2000, ErrorMessage = "Campo 'DetalleMovto' debe ser de máximo de 2000 carácteres.")]
        public string DetalleMovto { get; set; }

        /// <summary>
        /// Indicador de backorder Valores: 
        /// 0=Disponible y lo demás pendiente,
        /// 1=Disponible y lo demás cancele, 
        /// 5=por linea.
        /// </summary>
        [Range(0, 5, ErrorMessage = "Campo 'IndBackorderMovto' debe estar en un rango de 0 a 5")]
        public int IndBackorderMovto { get; set; }

        /// <summary>
        /// Indicador de precio Valores: 
        /// 0=Reliquida precio con lista de precio del cliente 
        /// 1=Liquida precio con lista de precio del registro
        /// 2=Liquida con precio del registro
        /// </summary>
        [Range(0, 2, ErrorMessage = "Campo 'IndPrecio' debe estar en un rango de 0 a 2")]
        public int IndPrecio { get; set; }

        /// <summary>
        /// Código del punto de envio del movimieento. 
        /// Excluyente con el identificador interno del
        /// punto de envio del movimiento(RowidPuntoEnvioMovto).
        /// </summary>
        [MaxLength(3, ErrorMessage = "Campo 'IdPuntoEnvioMovto' debe ser de máximo de 3 carácteres.")]
        public string IdPuntoEnvioMovto { get; set; }

        /// <summary>
        /// Identificador interno del punto de envio del movimiento.
        /// Excluyente con el código del punto de envio(IdPuntoEnvioMovto).
        /// </summary>
        public int RowidPuntoEnvioMovto { get; set; }

        /// <summary>
        /// Código del tercero vendedor del movimiento.
        /// Excluyente con el identificador interno del tercero vendedor(RowidTerceroVendedorMovto).
        /// </summary>
        [MaxLength(15, ErrorMessage = "Campo 'IdTerceroVendedorMovto' debe ser de máximo de 15 carácteres.")]
        public string IdTerceroVendedorMovto { get; set; }

        /// <summary>
        /// Identificador interno del tercero vendedor. 
        /// Excluyente con el código del tercero vendedor(IdTerceroVendedorMovto).
        /// </summary>
        public int RowidTerceroVendedorMovto { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
