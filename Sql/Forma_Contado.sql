/****** Script for SelectTopNRows command from SSMS  ******/
DECLARE @v_fecha_inicial datetime
DECLARE @v_fecha_final  datetime
DECLARE @v_fecha  datetime

set @v_fecha = '2020-05-30 14:30:00.000';
	

	if @v_fecha <  convert(char(10),@v_fecha,20) + ' 07:00:00.000'
		begin
			set @v_fecha = DATEADD(D,-1,@v_fecha);
			set @v_fecha_inicial = convert(char(10),@v_fecha,20) + ' 05:30:00.000';
			set @v_fecha_final = convert(char(10),@v_fecha,20) + ' 06:00:00.000';
		end
	else if @v_fecha <  convert(char(10),@v_fecha,20) + ' 15:00:00.000'
		begin 
			set @v_fecha_inicial = convert(char(10),@v_fecha,20) + ' 13:30:00.000';
			set @v_fecha_final = convert(char(10),@v_fecha,20) + ' 14:00:00.000';
		end
	else
		begin
			set @v_fecha_inicial = convert(char(10),@v_fecha,20) + ' 21:30:00.000';
			set @v_fecha_final = convert(char(10),@v_fecha,20) + ' 22:00:00.000';
		end

SELECT 
				estacion.CentroDeCostos				IdCo
			   ,formaPago.IdExterno					IdTipoDocto
			   ,@v_fecha							IdFecha
			   ,2									IndEstado
			   ,1									IndBackorder
			   ,'222222001'							IdTerceroFact
			   ,estacion.Generico2					IdSucursalFact
			   ,'222222001'							IdTerceroRem
			   ,estacion.Generico2					IdSucursalRem
			   ,'0002'								IdTipoClienteFact
			   ,estacion.CentroDeCostos				IdCOFact
			   ,@v_fecha		 					FechaEntrega
			   ,0									NumDiasEntrega
			   ,'COP'								IdMonedaDocto
			   ,'COP'								IdMonedaConv
			   ,1									TasaConv  
			   ,'COP'								IdonedaLocal
			   ,1									TasaLocal
			   ,'1'									IdCondPago   --Cambia Credito
			   ,0									IndImpresion 
			   ,'Pedido Contado'					Notas
			   ,'001'								IdPuntoEnvio	 --000							 
			   ,'900045238'							IdTerceroVendedor
			   ,'JAIRO ALFONSO CONTRERAS FAJARDO'	Contacto
			   ,estacion.Direccion1					Direccion1  --ISNULL(estacion.Direccion1,'CR 24 12 50')
			   ,'169'								Pais
			   ,'76'								Departamento /*ISNULL(ciudad.IdDepartamento,'76')*/	
			   ,'111' 								Ciudad  /*ISNULL(ciudad.id,'111')*/	
			   ,'PALO BLANCO' 						Barrio /*ISNULL(estacion.Generico1,'PALO BLANCO')*/	
			   ,1     								IndContacto


				--Items
			   ,items.IdExterno						IdItem
			   ,estacion.Generico4				    IdBodega 
			   ,10									IdConcepto																							
			   ,'01'								IdMotivo

			   ,0									IndObsequio
			   ,estacion.CentroDeCostos				IdCOMovto

			   ,estacion.Generico3					IdUNMovto  --99
			   ,''									IdCCostoMovto
			   ,@v_fecha 							FechaEntregaMovto

			   ,0 NumDiasEntregaMovto

			   ,estacion.Generico1					IdListaPrecios  --003 

			   ,case when items.IdExterno =  '0000196' then 'UND'      
						when items.IdExterno =  '0000003'then  'MTS'
						else 'GLN' end				IdUnidadMedida --GLN acer un case con la tabla  Gen_UnidadesDeMedidas entrando con pproducto

			   ,ISNULL(sum(itemsVentas.Cantidad),0)	CantPedidaBase ------------------------

			   ,ISNULL(sum(itemsVentas.Ppu),0)	    PrecioUnitario

			   ,0									IndImpuestoAsumido  --0
				
			   ,'Notas Movimiento Contado'			NotasMovto
			   ,1									IndBackorderMovto --1
			   ,1									IndPrecio  --1
		       ,estacion.Generico2					IdPuntoEnvioMovto
			   ,'900045238'							IdTerceroVendedorMovto			--001
			   ,'Contado'							Pedido


  FROM [dbo].[Adm_Ventas] ventas
  INNER JOIN Adm_PagosDeVentas xventas ON xventas.IdVenta = ventas.Id
  inner join dbo.Par_Estaciones  estacion on  estacion.Id = ventas.IdEstacion
  inner join Gen_FormaDePagos formaPago	 on formaPago.Id = xventas.IdFormaDePago
  inner join Adm_Cierres	cierre on cierre.Id = ventas.IdCierre
  inner join Ubi_Ciudades ciudad on ciudad.Id = estacion.IdCiudad
  inner join Adm_ItemsPorVentas itemsVentas on  itemsVentas.IdVenta = ventas.Id
  inner join Gen_Productos items on items.Id  = itemsVentas.IdProducto
  where ventas.IdFlota IS  NULL and xventas.IdFormaDePago = 0 AND cierre.FechaFinaL  between @v_fecha_inicial and @v_fecha_final
  and DATEDIFF(MINUTE,cierre.FechaInicial,cierre.FechaFinal) >30
    group by estacion.CentroDeCostos,
		  formaPago.IdExterno,
		  estacion.CentroDeCostos,
		  estacion.Direccion1,
		  items.IdExterno,
		  items.NombreCorto
		  ,estacion.Generico1
		  ,estacion.Generico2
		  ,estacion.Generico4	
		  ,estacion.Generico3

union


SELECT 
				estacion.CentroDeCostos				IdCo
			   ,formaPago.IdExterno					IdTipoDocto
			   ,@v_fecha							IdFecha
			   ,2									IndEstado
			   ,1									IndBackorder
			   ,'222222001'							IdTerceroFact
			   ,estacion.Generico2					IdSucursalFact
			   ,'222222001'							IdTerceroRem
			   ,estacion.Generico2					IdSucursalRem
			   ,'0002'								IdTipoClienteFact
			   ,estacion.CentroDeCostos				IdCOFact
			   ,@v_fecha		 					FechaEntrega
			   ,0									NumDiasEntrega
			   ,'COP'								IdMonedaDocto
			   ,'COP'								IdMonedaConv
			   ,1									TasaConv  
			   ,'COP'								IdonedaLocal
			   ,1									TasaLocal
			   ,flotas.Generico5					IdCondPago   --Cambia Credito
			   ,0									IndImpresion 
			   ,'Pedido Credito'					Notas
			   ,'001'								IdPuntoEnvio	 --000							 
			   ,'900045238'							IdTerceroVendedor
			   ,'000'								Contacto
			   ,estacion.Direccion1					Direccion1  --ISNULL(estacion.Direccion1,'CR 24 12 50')
			   ,'169'								Pais
			   ,'76'								Departamento /*ISNULL(ciudad.IdDepartamento,'76')*/	
			   ,'111' 								Ciudad  /*ISNULL(ciudad.id,'111')*/	
			   ,'PALO BLANCO' 						Barrio /*ISNULL(estacion.Generico1,'PALO BLANCO')*/	
			   ,1     								IndContacto


				--Items
			   ,items.IdExterno						IdItem
			   ,estacion.Generico4				    IdBodega 
			   ,10									IdConcepto																							
			   ,'01'								IdMotivo

			   ,0									IndObsequio
			   ,estacion.CentroDeCostos				IdCOMovto

			   ,estacion.Generico3					IdUNMovto  --99
			   ,''									IdCCostoMovto
			   ,@v_fecha 							FechaEntregaMovto

			   ,0 NumDiasEntregaMovto

			   ,estacion.Generico1					IdListaPrecios  --003 

			   ,case when items.IdExterno =  '0000196' then 'UND'      
						when items.IdExterno =  '0000003'then  'MTS'
						else 'GLN' end				IdUnidadMedida --GLN acer un case con la tabla  Gen_UnidadesDeMedidas entrando con pproducto

			   ,ISNULL(itemsVentas.Cantidad,0)			CantPedidaBase ------------------------

			   ,ISNULL(itemsVentas.Ppu,0)				PrecioUnitario

			   ,0									IndImpuestoAsumido  --0
				
			   ,'Notas Movimiento Credito'			NotasMovto
			   ,1									IndBackorderMovto --1
			   ,1									IndPrecio  --1
		       ,estacion.Generico2					IdPuntoEnvioMovto
			   ,'900045238'							IdTerceroVendedorMovto			--001
			   ,'Credito'							Pedido
	


  FROM [dbo].[Adm_Ventas] ventas
  INNER JOIN Adm_PagosDeVentas xventas ON xventas.IdVenta = ventas.Id
  inner join dbo.Par_Estaciones  estacion on  estacion.Id = ventas.IdEstacion
  inner join Gen_FormaDePagos formaPago	 on formaPago.Id = xventas.IdFormaDePago
  inner join Adm_Cierres	cierre on cierre.Id = ventas.IdCierre
  inner join Ubi_Ciudades ciudad on ciudad.Id = estacion.IdCiudad
  inner join Adm_ItemsPorVentas itemsVentas on  itemsVentas.IdVenta = ventas.Id
  inner join Gen_Productos items on items.Id  = itemsVentas.IdProducto
  inner join Ter_Flotas flotas on flotas.Id  = ventas.IdFlota
  where  xventas.IdFormaDePago = 2 AND cierre.FechaFinaL  between @v_fecha_inicial and @v_fecha_final
  and DATEDIFF(MINUTE,cierre.FechaInicial,cierre.FechaFinal) >30 and flotas.Documento not in ( '900045238',  '9000452381' ,'9000452382')

  union

  SELECT 
				estacion.CentroDeCostos				IdCo
			   ,formaPago.IdExterno					IdTipoDocto
			   ,@v_fecha							IdFecha
			   ,2									IndEstado
			   ,1									IndBackorder
			   ,'222222001'							IdTerceroFact
			   ,estacion.Generico2					IdSucursalFact
			   ,'222222001'							IdTerceroRem
			   ,estacion.Generico2					IdSucursalRem
			   ,'0002'								IdTipoClienteFact
			   ,estacion.CentroDeCostos				IdCOFact
			   ,@v_fecha		 					FechaEntrega
			   ,0									NumDiasEntrega
			   ,'COP'								IdMonedaDocto
			   ,'COP'								IdMonedaConv
			   ,1									TasaConv  
			   ,'COP'								IdonedaLocal
			   ,1									TasaLocal
			   ,'1'									IdCondPago   --flotas.Generico5
			   ,0									IndImpresion 
			   ,'Pedido Tarjeta'					Notas
			   ,'001'								IdPuntoEnvio	 --000							 
			   ,'900045238'							IdTerceroVendedor
			   ,'000'								Contacto
			   ,estacion.Direccion1					Direccion1  --ISNULL(estacion.Direccion1,'CR 24 12 50')
			   ,'169'								Pais
			   ,'76'								Departamento /*ISNULL(ciudad.IdDepartamento,'76')*/	
			   ,'111' 								Ciudad  /*ISNULL(ciudad.id,'111')*/	
			   ,'PALO BLANCO' 						Barrio /*ISNULL(estacion.Generico1,'PALO BLANCO')*/	
			   ,1     								IndContacto


				--Items
			   ,items.IdExterno						IdItem
			   ,estacion.Generico4				    IdBodega 
			   ,10									IdConcepto																							
			   ,'01'								IdMotivo

			   ,0									IndObsequio
			   ,estacion.CentroDeCostos				IdCOMovto

			   ,estacion.Generico3					IdUNMovto  --99
			   ,''									IdCCostoMovto
			   ,@v_fecha 							FechaEntregaMovto

			   ,0 NumDiasEntregaMovto

			   ,estacion.Generico1					IdListaPrecios  --003 

			   ,case when items.IdExterno =  '0000196' then 'UND'      
						when items.IdExterno =  '0000003'then  'MTS'
						else 'GLN' end				IdUnidadMedida --GLN acer un case con la tabla  Gen_UnidadesDeMedidas entrando con pproducto

			   ,ISNULL(itemsVentas.Cantidad,0)			CantPedidaBase ------------------------

			   ,ISNULL(itemsVentas.Ppu,0)				PrecioUnitario

			   ,0									IndImpuestoAsumido  --0
				
			   ,'Notas Movimiento Tarjeta'			NotasMovto
			   ,1									IndBackorderMovto --1
			   ,1									IndPrecio  --1
		       ,estacion.Generico2					IdPuntoEnvioMovto
			   ,'900045238'							IdTerceroVendedorMovto			--001
			   ,'Tarjeta'							Pedido
	


  FROM [dbo].[Adm_Ventas] ventas
  INNER JOIN Adm_PagosDeVentas xventas ON xventas.IdVenta = ventas.Id
  inner join dbo.Par_Estaciones  estacion on  estacion.Id = ventas.IdEstacion
  inner join Gen_FormaDePagos formaPago	 on formaPago.Id = xventas.IdFormaDePago
  inner join Adm_Cierres	cierre on cierre.Id = ventas.IdCierre
  inner join Ubi_Ciudades ciudad on ciudad.Id = estacion.IdCiudad
  inner join Adm_ItemsPorVentas itemsVentas on  itemsVentas.IdVenta = ventas.Id
  inner join Gen_Productos items on items.Id  = itemsVentas.IdProducto
  where ventas.IdFlota IS  NULL and xventas.IdFormaDePago = 4 AND cierre.FechaFinaL  between @v_fecha_inicial and @v_fecha_final
  and DATEDIFF(MINUTE,cierre.FechaInicial,cierre.FechaFinal) >30 

 union
 
 SELECT 
				estacion.CentroDeCostos				IdCo
			   ,formaPago.IdExterno					IdTipoDocto
			   ,@v_fecha							IdFecha
			   ,2									IndEstado
			   ,1									IndBackorder
			   ,'222222001'							IdTerceroFact
			   ,estacion.Generico2					IdSucursalFact
			   ,'222222001'							IdTerceroRem
			   ,estacion.Generico2					IdSucursalRem
			   ,'0007'								IdTipoClienteFact
			   ,estacion.CentroDeCostos				IdCOFact
			   ,@v_fecha		 					FechaEntrega
			   ,0									NumDiasEntrega
			   ,'COP'								IdMonedaDocto
			   ,'COP'								IdMonedaConv
			   ,1									TasaConv  
			   ,'COP'								IdonedaLocal
			   ,1									TasaLocal
			   ,'1'									IdCondPago   --flotas.Generico5
			   ,0									IndImpresion 
			   ,'Pedido Vale'					Notas
			   ,'001'								IdPuntoEnvio	 --000							 
			   ,'900045238'							IdTerceroVendedor
			   ,'000'								Contacto
			   ,estacion.Direccion1					Direccion1  --ISNULL(estacion.Direccion1,'CR 24 12 50')
			   ,'169'								Pais
			   ,'76'								Departamento /*ISNULL(ciudad.IdDepartamento,'76')*/	
			   ,'111' 								Ciudad  /*ISNULL(ciudad.id,'111')*/	
			   ,'PALO BLANCO' 						Barrio /*ISNULL(estacion.Generico1,'PALO BLANCO')*/	
			   ,1     								IndContacto


				--Items
			   ,items.IdExterno						IdItem
			   ,estacion.Generico4				    IdBodega 
			   ,10									IdConcepto																							
			   ,'01'								IdMotivo

			   ,0									IndObsequio
			   ,estacion.CentroDeCostos				IdCOMovto

			   ,estacion.Generico3					IdUNMovto  --99
			   ,''									IdCCostoMovto
			   ,@v_fecha 							FechaEntregaMovto

			   ,0 NumDiasEntregaMovto

			   ,estacion.Generico1					IdListaPrecios  --003 

			   ,case when items.IdExterno =  '0000196' then 'UND'      
						when items.IdExterno =  '0000003'then  'MTS'
						else 'GLN' end				IdUnidadMedida --GLN acer un case con la tabla  Gen_UnidadesDeMedidas entrando con pproducto

			   ,ISNULL(itemsVentas.Cantidad,0)			CantPedidaBase ------------------------

			   ,ISNULL(itemsVentas.Ppu,0)				PrecioUnitario

			   ,0									IndImpuestoAsumido  --0
				
			   ,'Notas Movimiento Vale'			NotasMovto
			   ,1									IndBackorderMovto --1
			   ,1									IndPrecio  --1
		       ,estacion.Generico2					IdPuntoEnvioMovto
			   ,'900045238'							IdTerceroVendedorMovto			--001
			   ,'Vale'								Pedido

	


  FROM [dbo].[Adm_Ventas] ventas
  INNER JOIN Adm_PagosDeVentas xventas ON xventas.IdVenta = ventas.Id
  inner join dbo.Par_Estaciones  estacion on  estacion.Id = ventas.IdEstacion
  inner join Gen_FormaDePagos formaPago	 on formaPago.Id = xventas.IdFormaDePago
  inner join Adm_Cierres	cierre on cierre.Id = ventas.IdCierre
  inner join Ubi_Ciudades ciudad on ciudad.Id = estacion.IdCiudad
  inner join Adm_ItemsPorVentas itemsVentas on  itemsVentas.IdVenta = ventas.Id
  inner join Gen_Productos items on items.Id  = itemsVentas.IdProducto
  where ventas.IdFlota IS  NULL and xventas.IdFormaDePago = 6 AND cierre.FechaFinaL  between @v_fecha_inicial and @v_fecha_final
  and DATEDIFF(MINUTE,cierre.FechaInicial,cierre.FechaFinal) >30 

  union

  SELECT 
				estacion.CentroDeCostos				IdCo
			   ,formaPago.IdExterno					IdTipoDocto
			   ,@v_fecha							IdFecha
			   ,2									IndEstado
			   ,1									IndBackorder
			   ,'222222001'							IdTerceroFact
			   ,estacion.Generico2					IdSucursalFact
			   ,'222222001'							IdTerceroRem
			   ,estacion.Generico2					IdSucursalRem
			   ,'0013'								IdTipoClienteFact
			   ,estacion.CentroDeCostos				IdCOFact
			   ,@v_fecha		 					FechaEntrega
			   ,0									NumDiasEntrega
			   ,'COP'								IdMonedaDocto
			   ,'COP'								IdMonedaConv
			   ,1									TasaConv  
			   ,'COP'								IdonedaLocal
			   ,1									TasaLocal
			   ,'1				'					IdCondPago   --Cambia Credito
			   ,0									IndImpresion 
			   ,'Pedido Consumo Interno'					Notas
			   ,'001'								IdPuntoEnvio	 --000							 
			   ,'900045238'							IdTerceroVendedor
			   ,'000'								Contacto
			   ,estacion.Direccion1					Direccion1  --ISNULL(estacion.Direccion1,'CR 24 12 50')
			   ,'169'								Pais
			   ,'76'								Departamento /*ISNULL(ciudad.IdDepartamento,'76')*/	
			   ,'111' 								Ciudad  /*ISNULL(ciudad.id,'111')*/	
			   ,'PALO BLANCO' 						Barrio /*ISNULL(estacion.Generico1,'PALO BLANCO')*/	
			   ,1     								IndContacto


				--Items
			   ,items.IdExterno						IdItem
			   ,estacion.Generico4				    IdBodega 
			   ,10									IdConcepto																							
			   ,flotas.Generico2					IdMotivo

			   ,0									IndObsequio
			   ,estacion.CentroDeCostos				IdCOMovto

			   ,estacion.Generico3					IdUNMovto  --99
			   ,flotas.Generico3					IdCCostoMovto
			   ,@v_fecha 							FechaEntregaMovto

			   ,0 NumDiasEntregaMovto

			   ,estacion.Generico1					IdListaPrecios  --003 

			   ,case when items.IdExterno =  '0000196' then 'UND'      
						when items.IdExterno =  '0000003'then  'MTS'
						else 'GLN' end				IdUnidadMedida --GLN acer un case con la tabla  Gen_UnidadesDeMedidas entrando con pproducto

			   ,ISNULL(itemsVentas.Cantidad,0)			CantPedidaBase ------------------------

			   ,ISNULL(itemsVentas.Ppu,0)				PrecioUnitario

			   ,0									IndImpuestoAsumido  --0
				
			   ,'Notas Movimiento Consumo Interno'			NotasMovto
			   ,1									IndBackorderMovto --1
			   ,1									IndPrecio  --1
		       ,estacion.Generico2					IdPuntoEnvioMovto
			   ,'900045238'							IdTerceroVendedorMovto			--001
			   ,'CosumoInterno'						Pedido
	


  FROM [dbo].[Adm_Ventas] ventas
  INNER JOIN Adm_PagosDeVentas xventas ON xventas.IdVenta = ventas.Id
  inner join dbo.Par_Estaciones  estacion on  estacion.Id = ventas.IdEstacion
  inner join Gen_FormaDePagos formaPago	 on formaPago.Id = xventas.IdFormaDePago
  inner join Adm_Cierres	cierre on cierre.Id = ventas.IdCierre
  inner join Ubi_Ciudades ciudad on ciudad.Id = estacion.IdCiudad
  inner join Adm_ItemsPorVentas itemsVentas on  itemsVentas.IdVenta = ventas.Id
  inner join Gen_Productos items on items.Id  = itemsVentas.IdProducto
  inner join Ter_Flotas flotas on flotas.Id  = ventas.IdFlota
  where  xventas.IdFormaDePago = 101 AND cierre.FechaFinaL  between @v_fecha_inicial and @v_fecha_final
  and DATEDIFF(MINUTE,cierre.FechaInicial,cierre.FechaFinal) >30 and flotas.Documento  in ( '900045238',  '9000452381' ,'9000452382')

  --select * from Ter_Flotas where Documento in ( '900045238',  '9000452381' ,'9000452382')

  select  *
  from Ter_Flotas
  --where Documento  in ( '900045238',  '9000452381' ,'9000452382')

--select  *
--from Gen_FormaDePagos




 -- select *
 -- from Adm_Cierres
 ---- WHERE FechaFinaL  between '2020-05-30 13:30:00.000' and '2020-05-30 14:00:00.000' and DATEDIFF(MINUTE,FechaInicial,FechaFinal) >30
 -- order by  FechaFinal DESC



  --select FechaInicial,Fecha
  --from [dbo].[Adm_Ventas] ventas 
  --WHERE FechaInicial IS NOT NULL
  --ORDER BY FechaInicial,Fecha

  --select cierre.Id cierre, cierre.FechaInicial,cierre.FechaFinal,ventas.Id venta ,ventas.Factura,ventas.FechaInicial
  --FROM [dbo].[Adm_Ventas] ventas
  --inner join Adm_Cierres	cierre on cierre.Id = ventas.IdCierre
  --where cierre.FechaFinal is not null

  --order by 2 desc






  --select *
  --from Gen_UnidadesDeMedidas uni
  --  inner join Gen_Productos items on items.IdUnidadMedia = uni.Id



  --SELECT 
		--	  ISNULL(estacion.CentroDeCostos,'140')	Centro_Operacion
		--	  ,ISNULL(formaPago.IdExterno,'SBD')		Tipo_Docto
		--	  ,ISNULL(cierre.FechaInicial,getdate())		fecha_docto
		--	  ,2						IndEstado
		--	  ,1						IndBackorder
		--	  ,'222222001'				IdTerceroFact
		--	  ,'001'					IdSucursalFact
		--	  ,'222222001'				IdTerceroRem
		--	  ,'001'					IdSucursalRem
		--	  ,'0002'					IdTipoClienteFact
		--	  ,ISNULL(estacion.CentroDeCostos,'140')	IdCOFact
		--	  ,ISNULL(cierre.FechaInicial,getdate())		FechaEntrega
		--	   ,0						NumDiasEntrega
		--	   ,'COP'					IdMonedaDocto
		--	   ,'COP'					IdMonedaConv
		--		,1						TasaConv  
		--	   ,'COP'					IdonedaLocal
		--	   , 1						TasaLocal
		--	   , 1						IdCondPago  
		--		,0						IndImpresion 
		--		,'Pedido Contado'		Notas
		--		,'000'				IdPuntoEnvio								 ---?
		--		,'900045238'			IdTerceroVendedor
		--		,'000'					Contacto
		--		,ISNULL(estacion.Direccion1,'CR 24 12 50')	Direccion1
		--		,'169'					Pais
		--		,ISNULL(ciudad.IdDepartamento,'76')	Departamento --76
		--		,ISNULL(ciudad.id,'111')				Ciudad
		--		,ISNULL(estacion.Generico1,'PALO BLANCO')		Barrio
		--		,1     					IndContacto


		--		--Items
		--		,items.IdExterno			IdItem
		--		,ISNULL(items.NombreCorto,'00142')  IdBodega
		--		,10    IdConcepto																							
		--		,'01'						 IdMotivo

		--		,0 IndObsequio
		--		,ISNULL(estacion.CentroDeCostos,'140') IdCOMovto

		--		, '99'					 IdUNMovto
		--		,ISNULL(cierre.FechaInicial,GETDATE())		 fecha_docto

		--		,0 NumDiasEntregaMovto

		--		,ISNULL(estacion.Generico1,'003')  IdListaPrecios  --003 

		--		,case when items.IdExterno =  '0000196' then 'UND'      /*ISNULL(items.IdUnidadMedia,'GLN')*/	 
		--			  when items.IdExterno =  '0000003'then  'MTS'
		--			  else 'GLN' end  IdUnidadMedida --GLN acer un case con la tabla  Gen_UnidadesDeMedidas entrando con pproducto

		--		,ISNULL(itemsVentas.Cantidad,3) CantPedidaBase ------------------------

		--		,ISNULL(itemsVentas.Ppu,2500) PrecioUnitario

		--		,1  IndImpuestoAsumido  --0
		--		--
		--		,'Notas Movimiento' NotasMovto
		--		,1  IndBackorderMovto --1
		--		,1 IndPrecio  --1
		--		,'900045238'  IdTerceroVendedorMovto			--001
		--		,cierre.Id Cierre
		--		,cierre.FechaInicial,cierre.FechaFinal

  --FROM [dbo].[Adm_Ventas] ventas WITH (NOLOCK) 
  --INNER JOIN Adm_PagosDeVentas xventas ON xventas.IdVenta = ventas.Id
  --inner join dbo.Par_Estaciones  estacion on  estacion.Id = ventas.IdEstacion
  --inner join Gen_FormaDePagos formaPago	 on formaPago.Id = xventas.IdFormaDePago
  --inner join Adm_Cierres	cierre on cierre.Id = ventas.IdCierre
  --inner join Ubi_Ciudades ciudad on ciudad.Id = estacion.IdCiudad
  --inner join Adm_ItemsPorVentas itemsVentas on  itemsVentas.IdVenta = ventas.Id
  --inner join Gen_Productos items on items.Id  = itemsVentas.IdProducto
  --where IdfLOTA IS  NULL and IdFormaDePago = 0 -- AND cierre.FechaFinal  between '2020-05-30 13:30:00.000' and '2020-05-30 14:00:00.000'  and DATEDIFF(MINUTE,cierre.FechaInicial,cierre.FechaFinal) >30
  
  --ORDER BY 4 




