Imports di.financiera.excepciones
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.utils
Imports di.financiera.seguridad

Public Class ComprobanteCompra
    Inherits Entidad

#Region "Enumerados"
    Public Enum enumFormaPago
        CAJA = 1
        BANCO = 2
    End Enum

#End Region

#Region "Variables"
    Private iId As Long
    'Private iProveedor As Proveedor
    Private iTipoComprobante As TipoComprobante
    Private iNumeroFactura As String
    Private iFecha As Date
    Private iFechaContable As Date
    Private iMontoNetoGravado105 As Double
    Private iMontoNetoGravado21 As Double
    Private iMontoNetoGravado27 As Double
    Private iPorcIVA As Integer
    Private iMontoIVA As Double
    Private iMontoNoGravado As Double
    Private iMontoPercepIVA As Double
    Private iMontoPercepIIBB As Double
    Private iMontoRetenSicreb As Double
    Private iMontoRetenIIBB As Double
    Private iMontoRetenIVA As Double
    Private iMontoRetenSuss As Double
    Private iMontoRetenGanancias As Double
    Private iMontoTotal As Double
    Private iDetalle As String
    Private iPagado As Int16
    Private iEstado As Estado
    Private iTerminoDePago As Integer
    'Private iTipoCondicionCompra As TipoCondicionCompra
    'Private iAsientosModelos As AsientosModelos
    Private iFormaPago As enumFormaPago

    Private iConexion As accesoDatos

#End Region

#Region "Atributos"

    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property
    'Public Property proveedor() As Proveedor
    '    Get
    '        Return iProveedor
    '    End Get
    '    Set(ByVal Value As Proveedor)
    '        iProveedor = Value
    '    End Set
    'End Property
    Public Property tipoComprobante() As TipoComprobante
        Get
            Return iTipoComprobante
        End Get
        Set(ByVal Value As TipoComprobante)
            iTipoComprobante = Value
        End Set
    End Property
    Public Property numeroFactura() As String
        Get
            Return iNumeroFactura
        End Get
        Set(ByVal Value As String)
            iNumeroFactura = Value
        End Set
    End Property
    Public Property fecha() As Date
        Get
            Return iFecha
        End Get
        Set(ByVal Value As Date)
            iFecha = Value
        End Set
    End Property
    Public Property fechaContable() As Date
        Get
            Return iFechaContable
        End Get
        Set(ByVal Value As Date)
            iFechaContable = Value
        End Set
    End Property
    Public Property montoNetoGravado105() As Double
        Get
            Return iMontoNetoGravado105
        End Get
        Set(ByVal Value As Double)
            iMontoNetoGravado105 = Value
        End Set
    End Property
    Public Property montoNetoGravado21() As Double
        Get
            Return iMontoNetoGravado21
        End Get
        Set(ByVal Value As Double)
            iMontoNetoGravado21 = Value
        End Set
    End Property
    Public Property montoNetoGravado27() As Double
        Get
            Return iMontoNetoGravado27
        End Get
        Set(ByVal Value As Double)
            iMontoNetoGravado27 = Value
        End Set
    End Property
    Public Property porcIVA() As Integer
        Get
            Return iPorcIVA
        End Get
        Set(ByVal Value As Integer)
            iPorcIVA = Value
        End Set
    End Property
    Public Property montoIVA() As Double
        Get
            Return iMontoIVA
        End Get
        Set(ByVal Value As Double)
            iMontoIVA = Value
        End Set
    End Property
    Public Property montoNoGravado() As Double
        Get
            Return iMontoNoGravado
        End Get
        Set(ByVal Value As Double)
            iMontoNoGravado = Value
        End Set
    End Property
    Public Property montoPercepIVA() As Double
        Get
            Return iMontoPercepIVA
        End Get
        Set(ByVal Value As Double)
            iMontoPercepIVA = Value
        End Set
    End Property
    Public Property montoPercepIIBB() As Double
        Get
            Return iMontoPercepIIBB
        End Get
        Set(ByVal Value As Double)
            iMontoPercepIIBB = Value
        End Set
    End Property
    Public Property montoRetenSicreb() As Double
        Get
            Return iMontoRetenSicreb
        End Get
        Set(ByVal Value As Double)
            iMontoRetenSicreb = Value
        End Set
    End Property
    Public Property montoRetenIIBB() As Double
        Get
            Return iMontoRetenIIBB
        End Get
        Set(ByVal Value As Double)
            iMontoRetenIIBB = Value
        End Set
    End Property
    Public Property montoRetenIVA() As Double
        Get
            Return iMontoRetenIVA
        End Get
        Set(ByVal Value As Double)
            iMontoRetenIVA = Value
        End Set
    End Property
    Public Property montoRetenSuss() As Double
        Get
            Return iMontoRetenSuss
        End Get
        Set(ByVal Value As Double)
            iMontoRetenSuss = Value
        End Set
    End Property
    Public Property montoRetenGanancias() As Double
        Get
            Return iMontoRetenGanancias
        End Get
        Set(ByVal Value As Double)
            iMontoRetenGanancias = Value
        End Set
    End Property
    Public Property montoTotal() As Double
        Get
            Return iMontoTotal
        End Get
        Set(ByVal Value As Double)
            iMontoTotal = Value
        End Set
    End Property
    Public Property detalle() As String
        Get
            Return iDetalle
        End Get
        Set(ByVal Value As String)
            iDetalle = Value
        End Set
    End Property
    Public Property pagado() As Int16
        Get
            Return iPagado
        End Get
        Set(ByVal Value As Int16)
            iPagado = Value
        End Set
    End Property
    Public Property estado() As Estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As Estado)
            iEstado = Value
        End Set
    End Property
    'Public Property tipoCondicionCompra() As TipoCondicionCompra
    '    Get
    '        Return iTipoCondicionCompra
    '    End Get
    '    Set(ByVal Value As TipoCondicionCompra)
    '        iTipoCondicionCompra = Value
    '    End Set
    'End Property
    Public Property terminoDePago() As Integer
        Get
            Return iTerminoDePago
        End Get
        Set(ByVal Value As Integer)
            iTerminoDePago = Value
        End Set
    End Property

    'Public Property asientosModelos() As AsientosModelos
    '    Get
    '        Return iAsientosModelos
    '    End Get
    '    Set(ByVal Value As AsientosModelos)
    '        iAsientosModelos = Value
    '    End Set
    'End Property

    Public Property formaPago As enumFormaPago
        Get
            Return iFormaPago
        End Get
        Set(value As enumFormaPago)
            iFormaPago = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Public Function obtenerComprobanteCompraGrilla(ByVal eNivel As Nivel, Optional ByVal eEstado As Estado = Nothing) As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("comprobanteCompra cc")
            iGeneradorSql.agregarTabla("proveedor pr")
            iGeneradorSql.agregarTabla("tipoComprobante tc")

            iGeneradorSql.agregarColumna("pr.codigo")
            iGeneradorSql.agregarColumna("cc.id")
            iGeneradorSql.agregarColumna("pr.razonSocial")
            iGeneradorSql.agregarColumna("tc.descripcion")
            iGeneradorSql.agregarColumna("cc.fecha")
            iGeneradorSql.agregarColumna("cc.numeroFactura")
            iGeneradorSql.agregarColumna("cc.idAsientosModelos")

            iGeneradorSql.agregarCondicionWhere("cc.idProveedor=pr.id")
            iGeneradorSql.agregarCondicionWhere("tc.id=cc.idTipoComprobante")

            'If proveedor.Codigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.codigo=" & proveedor.Codigo)
            'If proveedor.RazonSocial <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.razonSocial LIKE '" & proveedor.RazonSocial & "%'")
            If Not IsNothing(tipoComprobante) Then iGeneradorSql.agregarCondicionWhere("tc.id=" & tipoComprobante.id)
            If formaPago <> Nothing Then iGeneradorSql.agregarCondicionWhere("cc.FormaPago=" & formaPago)

            iGeneradorSql.agregarOrden("pr.codigo, cc.fecha, cc.numeroFactura")
            iGeneradorSql.agregarLimit("500")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "comprobantegrilla")

        Catch ex As Exception
            'Throw New ProveedorNoEncontradoException(ex)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            'iDomicilio.accesoDatos = iConexion
            'iDomicilio.crear()
            'iDomicilio.accesoDatos = Nothing

            iGeneradorSql.agregarTabla("comprobantecompra")

            iGeneradorSql.agregarColumna("idProveedor")
            iGeneradorSql.agregarColumna("idTipoComprobante")
            iGeneradorSql.agregarColumna("idTipoCondicionCompra")
            iGeneradorSql.agregarColumna("terminoDePago")
            iGeneradorSql.agregarColumna("numeroFactura")
            iGeneradorSql.agregarColumna("fecha")
            iGeneradorSql.agregarColumna("fechaContable")
            iGeneradorSql.agregarColumna("montoNetoGravado105")
            iGeneradorSql.agregarColumna("montoNetoGravado21")
            iGeneradorSql.agregarColumna("montoNetoGravado27")
            iGeneradorSql.agregarColumna("porcIVA")
            iGeneradorSql.agregarColumna("montoIVA")
            iGeneradorSql.agregarColumna("montoNoGravado")
            iGeneradorSql.agregarColumna("montoPercepIVA")
            iGeneradorSql.agregarColumna("montoPercepIIBB")
            iGeneradorSql.agregarColumna("montoRetenSicreb")
            iGeneradorSql.agregarColumna("montoRetenIIBB")
            iGeneradorSql.agregarColumna("montoRetenIVA")
            iGeneradorSql.agregarColumna("montoRetenSuss")
            iGeneradorSql.agregarColumna("montoRetenGanancias")
            iGeneradorSql.agregarColumna("montoTotal")
            iGeneradorSql.agregarColumna("detalle")
            iGeneradorSql.agregarColumna("pagado")
            iGeneradorSql.agregarColumna("idEstado")
            iGeneradorSql.agregarColumna("idAsientosModelos")
            iGeneradorSql.agregarColumna("formaPago")

            'iGeneradorSql.agregarValue(proveedor.id)
            iGeneradorSql.agregarValue(tipoComprobante.id)
            'iGeneradorSql.agregarValue(tipoCondicionCompra.id)
            iGeneradorSql.agregarValue(terminoDePago)
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(numeroFactura))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fecha))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaContable))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoNetoGravado105))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoNetoGravado21))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoNetoGravado27))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(porcIVA))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoIVA))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoNoGravado))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoPercepIVA))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoPercepIIBB))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoRetenSicreb))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoRetenIIBB))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoRetenIVA))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoRetenSuss))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoRetenGanancias))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoTotal))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(detalle))
            iGeneradorSql.agregarValue(pagado)
            iGeneradorSql.agregarValue(Estado.ALTA)
            'If Not IsNothing(iAsientosModelos) Then
            '    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iAsientosModelos.id))
            'Else
            '    iGeneradorSql.agregarValue("null")
            'End If
            'iGeneradorSql.agregarValue(formaPago)

            id = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch ex As Exception
            'Throw New ProveedorNoCreadoException(ex)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Private Sub validarCrear()

        'If proveedor.id = Nothing Then
        '    Throw New ComprobanteNoCreadoException("El proveedor no puede ser nulo")
        'End If

        If tipoComprobante.id = Nothing Then
            Throw New ComprobanteNoCreadoException("El tipo de comprobante no puede ser nulo")
        End If

        Try
            'iConexion = obtenerConexion()

            'iGeneradorSql.agregarColumna("codigo")
            'iGeneradorSql.agregarTabla("Proveedor")
            'iGeneradorSql.agregarCondicionWhere("codigo=" & Codigo)

            'iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            'If IDataReader.Read Then
            '    Throw New ProveedorNoCreadoException("Ya existe un Proveedor con el codigo " & iCodigo)
            'End If

        Catch ex As Exception
            'Throw New ProveedorNoCreadoException(ex)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            'If Not IsNothing(IDataReader) AndAlso Not IDataReader.IsClosed Then IDataReader.Close()
            'IDataReader = Nothing
            'iGeneradorSql.destructor()
            'iGeneradorSql = Nothing
        End Try

    End Sub

    Public Function obtenerComprobanteCompra(Optional ByVal eNivel As Nivel = Nothing, Optional ByVal eEstado As Estado = Nothing, Optional ByVal eUsuario As Usuario = Nothing) As ComprobanteCompra
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("cc.id")
            iGeneradorSql.agregarColumna("cc.idProveedor")
            iGeneradorSql.agregarColumna("cc.idTipoComprobante")
            iGeneradorSql.agregarColumna("cc.idTipoCondicionCompra")
            iGeneradorSql.agregarColumna("cc.terminoDePago")
            iGeneradorSql.agregarColumna("cc.numeroFactura")
            iGeneradorSql.agregarColumna("cc.fecha")
            iGeneradorSql.agregarColumna("cc.fechaContable")
            iGeneradorSql.agregarColumna("cc.montoNetoGravado105")
            iGeneradorSql.agregarColumna("cc.montoNetoGravado21")
            iGeneradorSql.agregarColumna("cc.montoNetoGravado27")
            iGeneradorSql.agregarColumna("cc.porcIVA")
            iGeneradorSql.agregarColumna("cc.montoIVA")
            iGeneradorSql.agregarColumna("cc.montoNoGravado")
            iGeneradorSql.agregarColumna("cc.montoPercepIVA")
            iGeneradorSql.agregarColumna("cc.montoPercepIIBB")
            iGeneradorSql.agregarColumna("cc.montoRetenSicreb")
            iGeneradorSql.agregarColumna("cc.montoRetenIIBB")
            iGeneradorSql.agregarColumna("cc.montoRetenIVA")
            iGeneradorSql.agregarColumna("cc.montoRetenSuss")
            iGeneradorSql.agregarColumna("cc.montoRetenGanancias")
            iGeneradorSql.agregarColumna("cc.montoTotal")
            iGeneradorSql.agregarColumna("cc.detalle")
            iGeneradorSql.agregarColumna("cc.pagado")
            iGeneradorSql.agregarColumna("cc.idEstado")
            iGeneradorSql.agregarColumna("cc.idAsientosModelos")
            iGeneradorSql.agregarColumna("cc.formaPago")

            iGeneradorSql.agregarTabla("comprobantecompra cc")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("cc.id=" & id)
            'If Not IsNothing(proveedor) Then
            '    If proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("cc.idProveedor=" & proveedor.id)
            'End If
            If Not IsNothing(tipoComprobante) Then
                If tipoComprobante.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("cc.idTipoComprobante=" & tipoComprobante.id)
            End If
            If numeroFactura <> Nothing Then iGeneradorSql.agregarCondicionWhere("cc.numeroFactura=" & FuncionComun.nuloSiEsNothing(numeroFactura))

            If Not IsNothing(eEstado) Then
                If eEstado.isAlta Then iGeneradorSql.agregarCondicionWhere("cc.idEstado=" & Estado.ALTA)
                If eEstado.isBaja Then iGeneradorSql.agregarCondicionWhere("cc.idEstado=" & Estado.BAJA)
            End If


            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then

                id = iDataReader.Item("id").ToString
                'proveedor = New Proveedor
                'proveedor.id = iDataReader.Item("idProveedor").ToString

                'Select Case FuncionComun.ceroSiEsNuloInt(iDataReader.Item("idTipoComprobante"))
                '    Case TipoComprobante.FACTURAA
                '        tipoComprobante = New FacturaA
                '    Case TipoComprobante.FACTURAB
                '        tipoComprobante = New FacturaB
                '    Case TipoComprobante.NOTADECREDITOA
                '        tipoComprobante = New NotaDeCreditoA
                '    Case TipoComprobante.NOTADEDEBITOA
                '        tipoComprobante = New NotaDeDebitoA
                '    Case TipoComprobante.NOTADECREDITOB
                '        tipoComprobante = New NotaDeCreditoB
                '    Case TipoComprobante.NOTADEDEBITOB
                '        tipoComprobante = New NotaDeDebitoB
                'End Select

                numeroFactura = iDataReader.Item("numeroFactura").ToString
                fecha = iDataReader.Item("fecha").ToString
                If Not IsDBNull(iDataReader.Item("fechaContable")) Then fechaContable = iDataReader.Item("fechaContable")
                terminoDePago = iDataReader.Item("terminoDePago").ToString
                montoNetoGravado105 = iDataReader.Item("montoNetoGravado105").ToString
                montoNetoGravado21 = iDataReader.Item("montoNetoGravado21").ToString
                montoNetoGravado27 = iDataReader.Item("montoNetoGravado27").ToString
                porcIVA = iDataReader.Item("porcIVA").ToString
                montoIVA = iDataReader.Item("montoIVA").ToString
                montoNoGravado = iDataReader.Item("montoNoGravado").ToString
                montoPercepIVA = iDataReader.Item("montoPercepIVA").ToString
                montoPercepIIBB = iDataReader.Item("montoPercepIIBB").ToString
                montoRetenSicreb = iDataReader.Item("montoRetenSicreb").ToString
                montoRetenIIBB = iDataReader.Item("montoRetenIIBB").ToString
                montoRetenIVA = iDataReader.Item("montoRetenIVA").ToString
                montoRetenSuss = iDataReader.Item("montoRetenSuss").ToString
                montoRetenGanancias = iDataReader.Item("montoRetenGanancias").ToString
                montoTotal = iDataReader.Item("montoTotal").ToString
                detalle = iDataReader.Item("detalle").ToString
                pagado = iDataReader.Item("pagado").ToString

                If Not IsDBNull(iDataReader.Item("idTipoCondicionCompra")) Then
                    'tipoCondicionCompra = New TipoCondicionCompra
                    'tipoCondicionCompra.id = iDataReader.Item("idTipoCondicionCompra").ToString
                End If
                estado = IIf(iDataReader.Item("idEstado") = Estado.ALTA, New Alta, New Baja)

                If Not IsDBNull(iDataReader.Item("idAsientosModelos")) Then
                    'iAsientosModelos = New AsientosModelos
                    'iAsientosModelos.id = iDataReader.Item("idAsientosModelos").ToString
                End If
                If Not IsDBNull(iDataReader.Item("formaPago")) Then
                    formaPago = iDataReader.Item("formaPago")
                Else
                    formaPago = enumFormaPago.CAJA
                End If

                iDataReader.Close()

                'If Not IsNothing(proveedor) Then
                '    proveedor.accesoDatos = iConexion
                '    proveedor = proveedor.obtenerProveedor
                '    proveedor.accesoDatos = Nothing
                'End If

                'If Not IsNothing(tipoCondicionCompra) Then
                '    tipoCondicionCompra.accesoDatos = iConexion
                '    tipoCondicionCompra = tipoCondicionCompra.obtenerTipoCondicionCompra
                '    tipoCondicionCompra.accesoDatos = Nothing
                'End If

                'If Not IsNothing(iAsientosModelos) Then
                '    iAsientosModelos.accesoDatos = iConexion
                '    iAsientosModelos = iAsientosModelos.obtenerAsientosModelos
                '    iAsientosModelos.accesoDatos = Nothing
                'End If

                Return Me

            Else
                Throw New ComprobanteNoEncontradoException
            End If

        Catch ProveedorNoEncontradoException As ComprobanteNoEncontradoException
            Throw ProveedorNoEncontradoException
        Catch ex As Exception
            Throw New ComprobanteNoEncontradoException(ex)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            'If Not IsNothing(proveedor) Then proveedor.dispose()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            validarEliminar()
            iGeneradorSql.agregarTabla("comprobantecompra")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch ex As Exception
            Throw New ComprobanteNoEliminadoException(ex)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    Private Sub validarEliminar()
        If id = Nothing Then
            Throw New ComprobanteNoEliminadoException("El comprobante es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("comprobantecompra")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New ComprobanteNoEliminadoException("El comprobante es inexistente")
            End If

            iDataReader.Close()

            'iGeneradorSql.agregarColumna("id")
            'iGeneradorSql.agregarTabla("comprobantecompra")
            'iGeneradorSql.agregarCondicionWhere("idProveedor=" & id)

            'iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            'If iDataReader.Read Then
            '    Throw New ComprobanteNoEliminadoException("El proveedor tiene comprobantes de compra cargados")
            'End If
            'iDataReader.Close()

        Catch ex As Exception
            Throw New ComprobanteNoEliminadoException(ex)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Sub

    'Public Function obtenerReporteLibroIVACompras(ByVal eFacturacionLibroIVAComprasVO As FacturacionLibroIVAComprasVO) As DataSet
    '    Dim iGeneradorSql As New GeneradorSql
    '    Dim iDataSet As New DataSet
    '    Dim signoValor As String

    '    signoValor = "CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END * "

    '    Try
    '        iConexion = obtenerConexion()

    '        ' +-------------------+
    '        ' | LIBRO IVA COMPRAS |
    '        ' +-------------------+

    '        ' ((( LIBRO IVA COMPRAS | DETALLE )))

    '        ' (1) IdFactura
    '        iGeneradorSql.agregarColumna("cc.id AS IdFactura")

    '        ' (2) Fecha
    '        iGeneradorSql.agregarColumna("cc.fecha AS Fecha")

    '        ' (3) Tipo Comprobante
    '        iGeneradorSql.agregarColumna("tc.descripcion AS TipoComprobante")

    '        ' (4) Numero Factura
    '        iGeneradorSql.agregarColumna("cc.numeroFactura AS NumeroFactura")

    '        ' (5) Proveedor Nombre
    '        iGeneradorSql.agregarColumna("IFNULL(pr.razonSocial, pr.nombreComercial) AS Nombre")

    '        ' (6) CUIT
    '        iGeneradorSql.agregarColumna("pr.cuit AS CUIT")

    '        ' (7) Monto Neto Gravado
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoNetoGravado105, 0) AS MontoNetoGravado105")
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoNetoGravado21, 0) AS MontoNetoGravado21")
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoNetoGravado27, 0) AS MontoNetoGravado27")
    '        iGeneradorSql.agregarColumna("(IFNULL(cc.montoNetoGravado105, 0) + IFNULL(cc.montoNetoGravado21, 0) + IFNULL(cc.montoNetoGravado27, 0)) AS MontoNetoGravado")

    '        ' (8) Monto No Gravado / Exento
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoNoGravado, 0) AS MontoNoGravado")

    '        ' (9) Monto IVA 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(cc.montoNetoGravado105, 0) * 0.105, 2) AS MontoIVA105")

    '        ' (10) Monto IVA 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(cc.montoNetoGravado21, 0) * 0.21, 2) AS MontoIVA21")

    '        ' (11) Monto IVA 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(cc.montoNetoGravado27, 0) * 0.27, 2) AS MontoIVA27")

    '        ' (12) Monto Percepción IVA
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoPercepIVA, 0) AS MontoPercepIVA")

    '        ' (13) Monto Percepción IIBB
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoPercepIIBB, 0) AS MontoPercepIIBB")

    '        ' (14) Monto Retención Ganancias
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoRetenGanancias, 0) AS MontoRetenGanancias")

    '        ' (15) Monto Retención IIBB
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoRetenIIBB, 0) AS MontoRetenIIBB")

    '        ' (16) Monto Retención IVA
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoRetenIVA, 0) AS MontoRetenIVA")

    '        ' (17) Monto Retención SICREB
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoRetenSicreb, 0) AS MontoRetenSICREB")

    '        ' (18) Monto Retención SUSS
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoRetenSuss, 0) AS MontoRetenSUSS")

    '        ' (19) Total
    '        iGeneradorSql.agregarColumna("IFNULL(cc.montoTotal, 0) AS Total")

    '        ' (20) Signo Valor
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")
    '        iGeneradorSql.agregarTablaConJoin("tipocomprobante tc", "cc.idTipoComprobante=tc.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)
    '        End With

    '        iGeneradorSql.agregarOrden("cc.fecha")
    '        iGeneradorSql.agregarOrden("cc.numeroFactura")

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( LIBRO IVA COMPRAS | TOTALES )))

    '        ' (1) IdFactura
    '        iGeneradorSql.agregarColumna("NULL AS IdFactura")

    '        ' (2) Fecha
    '        iGeneradorSql.agregarColumna("NULL AS Fecha")

    '        ' (3) Tipo Comprobante
    '        iGeneradorSql.agregarColumna("'' AS TipoComprobante")

    '        ' (4) Numero Factura
    '        iGeneradorSql.agregarColumna("'' AS NumeroFactura")

    '        ' (5) Proveedor Nombre
    '        iGeneradorSql.agregarColumna("'TOTALES' AS Nombre")

    '        ' (6) CUIT
    '        iGeneradorSql.agregarColumna("'' AS CUIT")

    '        ' (7) Monto Neto Gravado
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) AS MontoNetoGravado105")
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) AS MontoNetoGravado21")
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0) AS MontoNetoGravado27")
    '        iGeneradorSql.agregarColumna("(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) + IFNULL(SUM(" & signoValor & "cc.montoNetoGravado21), 0) + IFNULL(SUM(" & signoValor & "cc.montoNetoGravado27), 0)) AS MontoNetoGravado")

    '        ' (8) Monto No Gravado / Exento
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoNoGravado), 0) AS MontoNoGravado")

    '        ' (9) Monto IVA 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) * 0.105, 2) AS MontoIVA105")

    '        ' (10) Monto IVA 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) * 0.21, 2) AS MontoIVA21")

    '        ' (11) Monto IVA 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0) * 0.27, 2) AS MontoIVA27")

    '        ' (12) Monto Percepción IVA
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoPercepIVA), 0) AS MontoPercepIVA")

    '        ' (13) Monto Percepción IIBB
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoPercepIIBB), 0) AS MontoPercepIIBB")

    '        ' (14) Monto Retención Ganancias
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoRetenGanancias), 0) AS MontoRetenGanancias")

    '        ' (15) Monto Retención IIBB
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoRetenIIBB), 0) AS MontoRetenIIBB")

    '        ' (16) Monto Retención IVA
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoRetenIVA), 0) AS MontoRetenIVA")

    '        ' (17) Monto Retención SICREB
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoRetenSicreb), 0) AS MontoRetenSICREB")

    '        ' (18) Monto Retención SUSS
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoRetenSuss), 0) AS MontoRetenSUSS")

    '        ' (19) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoTotal), 0) AS Total")

    '        ' (20) Signo Valor
    '        'iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '        '                             " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '        '                             " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '        '                             " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '        '                             " THEN -1 ELSE 1 END AS SignoValor")
    '        iGeneradorSql.agregarColumna("1 AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")
    '        iGeneradorSql.agregarTablaConJoin("tipocomprobante tc", "cc.idTipoComprobante=tc.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)
    '        End With

    '        iGeneradorSql.agregarOrden("cc.fecha")
    '        iGeneradorSql.agregarOrden("cc.numeroFactura")

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)

    '        iDataSet = iConexion.getDataSet(iGeneradorSql.generarUnion, iGeneradorSql.parametrosSQL, "LibroIVACompras")


    '        ' +------------------+
    '        ' | CONDICION I.V.A. |
    '        ' +------------------+

    '        ' ((( CONDICION I.V.A. | 10.5% )))


    '        ' (1) Proveedor Tipo IVA
    '        iGeneradorSql.agregarColumna("ti.descripcion AS ProveedorTipoIVA")

    '        ' (2) Alicuota
    '        iGeneradorSql.agregarColumna("10.5 AS Alicuota")

    '        ' (3) Monto Neto Gravado 10.5%
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) AS MontoNetoGravado")

    '        ' (4) Monto IVA 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) * 0.105, 2) AS MontoIVA")

    '        ' (5) Total + 10.5%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) * 1.105, 2) AS Total")

    '        ' (6) Signo Valor
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END AS SignoValor")


    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")
    '        iGeneradorSql.agregarTablaConJoin("tipoiva ti", "pr.idTipoIVA=ti.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta IVA 10.5%
    '            iGeneradorSql.agregarCondicionWhere("cc.montoNetoGravado105 > 0")

    '        End With

    '        iGeneradorSql.agregarGroupBy("ti.descripcion")
    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( CONDICION I.V.A. | 21% )))

    '        ' (1) Proveedor Tipo IVA
    '        iGeneradorSql.agregarColumna("ti.descripcion AS ProveedorTipoIVA")

    '        ' (2) Alicuota
    '        iGeneradorSql.agregarColumna("21.0 AS Alicuota")

    '        ' (3) Monto Neto Gravado 21%
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) AS MontoNetoGravado")

    '        ' (4) Monto IVA
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) * 0.21, 2) AS MontoIVA")

    '        ' (5) Total
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) * 1.21, 2) AS Total")

    '        ' (6) Signo Valor
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")
    '        iGeneradorSql.agregarTablaConJoin("tipoiva ti", "pr.idTipoIVA=ti.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta IVA 21%
    '            iGeneradorSql.agregarCondicionWhere("cc.montoNetoGravado21 > 0")

    '        End With

    '        iGeneradorSql.agregarGroupBy("ti.descripcion")
    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( CONDICION I.V.A. | 27% )))

    '        ' (1) Proveedor Tipo IVA
    '        iGeneradorSql.agregarColumna("ti.descripcion AS ProveedorTipoIVA")

    '        ' (2) Alicuota
    '        iGeneradorSql.agregarColumna("27.0 AS Alicuota")

    '        ' (3) Monto Neto Gravado 27%
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0) AS MontoNetoGravado")

    '        ' (4) Monto IVA 27%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0) * 0.27, 2) AS MontoIVA")

    '        ' (5) Total + 27%
    '        iGeneradorSql.agregarColumna("ROUND(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0) * 1.27, 2) AS Total")

    '        ' (6) Signo Valor
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")
    '        iGeneradorSql.agregarTablaConJoin("tipoiva ti", "pr.idTipoIVA=ti.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta IVA 27%
    '            iGeneradorSql.agregarCondicionWhere("cc.montoNetoGravado27 > 0")

    '        End With

    '        iGeneradorSql.agregarGroupBy("ti.descripcion")
    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( CONDICION I.V.A. | TOTALES )))

    '        ' (1) Proveedor Tipo IVA
    '        iGeneradorSql.agregarColumna("'TOTALES' AS ProveedorTipoIVA")

    '        ' (2) Alicuota
    '        iGeneradorSql.agregarColumna("NULL AS Alicuota")

    '        ' (3) Monto Neto Gravado
    '        iGeneradorSql.agregarColumna("(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) + IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) + IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0)) AS MontoNetoGravado")

    '        ' (4) Monto IVA
    '        iGeneradorSql.agregarColumna("(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) * 0.105 + IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) * 0.21 + IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0) * 0.27) AS MontoIVA")

    '        ' (5) Total
    '        iGeneradorSql.agregarColumna("(IFNULL(SUM(" & signoValor & " cc.montoNetoGravado105), 0) * 1.105 + IFNULL(SUM(" & signoValor & " cc.montoNetoGravado21), 0) * 1.21 + IFNULL(SUM(" & signoValor & " cc.montoNetoGravado27), 0) * 1.27) AS Total")

    '        ' (6) Signo Valor
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")
    '        iGeneradorSql.agregarTablaConJoin("tipoiva ti", "pr.idTipoIVA=ti.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoNetoGravado105 > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoNetoGravado21 > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoNetoGravado27 > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' +-------+
    '        ' | UNION |
    '        ' +-------+

    '        ' + LibroIVACompras
    '        ' + CondicionIVA
    '        iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarUnion, iGeneradorSql.parametrosSQL, "CondicionIVA")


    '        ' +-----------------+
    '        ' | OTROS CONCEPTOS |
    '        ' +-----------------+

    '        ' ((( OTROS CONCEPTOS | PERCEPCION I.V.A. )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'PERCEPCION I.V.A.' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoPercepIVA), 0) AS Total")

    '        ' (3) Signo Valor (+)
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhere("cc.montoPercepIVA > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( OTROS CONCEPTOS | PERCEPCION IIBB )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'PERCEPCION IIBB' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoPercepIIBB), 0) AS Total")

    '        ' (3) Signo Valor (+)
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN -1 ELSE 1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhere("cc.montoPercepIIBB > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( OTROS CONCEPTOS | RETENCION GANANCIAS )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'RETENCION GANANCIAS' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoRetenGanancias), 0) AS Total")

    '        ' (3) Signo Valor (-)
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN 1 ELSE -1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhere("cc.montoRetenGanancias > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( OTROS CONCEPTOS | RETENCION IIBB )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'RETENCION IIBB' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoRetenIIBB), 0) AS Total")

    '        ' (3) Signo Valor (-)
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN 1 ELSE -1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhere("cc.montoRetenIIBB > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( OTROS CONCEPTOS | RETENCION I.V.A. )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'RETENCION I.V.A.' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoRetenIVA), 0) AS Total")

    '        ' (3) Signo Valor (-)
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN 1 ELSE -1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhere("cc.montoRetenIVA > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( OTROS CONCEPTOS | RETENCION SICREB )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'RETENCION SICREB' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoRetenSicreb), 0) AS Total")

    '        ' (3) Signo Valor (-)
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN 1 ELSE -1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhere("cc.montoRetenSicreb > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( OTROS CONCEPTOS | RETENCION SUSS )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'RETENCION SUSS' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoRetenSuss), 0) AS Total")

    '        ' (3) Signo Valor (-)
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN 1 ELSE -1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhere("cc.montoRetenSuss > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' ((( OTROS CONCEPTOS | TOTALES )))

    '        ' (1) Concepto
    '        iGeneradorSql.agregarColumna("'TOTAL' AS Concepto")

    '        ' (2) Total
    '        iGeneradorSql.agregarColumna("IFNULL(SUM(cc.montoPercepIIBB), 0) + " &
    '                                     "IFNULL(SUM(cc.montoPercepIVA), 0) - " &
    '                                     "IFNULL(SUM(cc.montoRetenGanancias), 0) - " &
    '                                     "IFNULL(SUM(cc.montoRetenIIBB), 0) - " &
    '                                     "IFNULL(SUM(cc.montoRetenIVA), 0) - " &
    '                                     "IFNULL(SUM(cc.montoRetenSicreb), 0) - " &
    '                                     "IFNULL(SUM(cc.montoRetenSuss), 0) AS Total")

    '        ' (3) Signo Valor
    '        iGeneradorSql.agregarColumna("CASE WHEN cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOA &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOB &
    '                                     " Or cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOC &
    '                                     " OR cc.idTipoComprobante=" & TipoComprobante.NOTADECREDITOBMOSTRADOR &
    '                                     " THEN 1 ELSE -1 END AS SignoValor")

    '        iGeneradorSql.agregarTablaPrincipal("comprobantecompra cc")
    '        iGeneradorSql.agregarTablaConJoin("proveedor pr", "cc.idProveedor=pr.id")

    '        With eFacturacionLibroIVAComprasVO
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha>=" & FuncionComun.nuloSiEsNothing(.fechaDesde))
    '            iGeneradorSql.agregarCondicionWhere("cc.fecha<=" & FuncionComun.nuloSiEsNothing(.fechaHasta))
    '            If Not IsNothing(.proveedor) AndAlso .proveedor.id <> Nothing Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)

    '            ' Consulta
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoRetenPercepIIBB > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoRetenPercepIVA > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoRetenGanancias > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoRetenIIBB > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoRetenIVA > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoRetenSicreb > 0")
    '            iGeneradorSql.agregarCondicionWhereConOr("cc.montoRetenSuss > 0")

    '        End With

    '        iGeneradorSql.agregarSelect(iGeneradorSql.generarSelect)


    '        ' +-------+
    '        ' | UNION |
    '        ' +-------+

    '        ' + LibroIVACompras
    '        ' + CondicionIVA
    '        ' + OtrosConceptos
    '        iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarUnion, iGeneradorSql.parametrosSQL, "OtrosConceptos")

    '        Return iDataSet

    '    Catch ex As Exception
    '        Throw New ComprobanteNoEncontradoException(ex)
    '    Finally
    '        If (IsNothing(MyBase.accesoDatos)) Then
    '            iConexion.cerrar()
    '            iConexion = Nothing
    '        End If
    '        iGeneradorSql.destructor()
    '        iGeneradorSql = Nothing
    '        iDataSet = Nothing
    '    End Try

    'End Function

    'Public Function obtenerComprasAContabilizarPorEmpresa(eAccesoDatos As accesoDatos, eFecha As Date, eAsientosModelos As AsientosModelos) As DataSet
    '    Dim iGeneradorSql As New GeneradorSql
    '    Dim iDataSet As New DataSet
    '    Dim iDataRelation As DataRelation
    '    Dim iColumnaHijo, iColumnaPadre As DataColumn

    '    Try

    '        iConexion = obtenerConexion()

    '        iGeneradorSql.agregarColumna("cc.id")
    '        iGeneradorSql.agregarColumna("cc.idProveedor")
    '        iGeneradorSql.agregarColumna("cc.idTipoComprobante")
    '        iGeneradorSql.agregarColumna("cc.idTipoCondicionCompra")
    '        iGeneradorSql.agregarColumna("cc.terminoDePago")
    '        iGeneradorSql.agregarColumna("cc.numeroFactura")
    '        iGeneradorSql.agregarColumna("cc.fecha")
    '        iGeneradorSql.agregarColumna("cc.fechaContable")
    '        iGeneradorSql.agregarColumna("cc.montoNetoGravado105")
    '        iGeneradorSql.agregarColumna("cc.montoNetoGravado21")
    '        iGeneradorSql.agregarColumna("cc.montoNetoGravado27")
    '        iGeneradorSql.agregarColumna("cc.porcIVA")
    '        iGeneradorSql.agregarColumna("cc.montoIVA")
    '        iGeneradorSql.agregarColumna("cc.montoNoGravado")
    '        iGeneradorSql.agregarColumna("cc.montoPercepIVA")
    '        iGeneradorSql.agregarColumna("cc.montoPercepIIBB")
    '        iGeneradorSql.agregarColumna("cc.montoRetenSicreb")
    '        iGeneradorSql.agregarColumna("cc.montoRetenIIBB")
    '        iGeneradorSql.agregarColumna("cc.montoRetenIVA")
    '        iGeneradorSql.agregarColumna("cc.montoRetenSuss")
    '        iGeneradorSql.agregarColumna("cc.montoRetenGanancias")
    '        iGeneradorSql.agregarColumna("cc.montoTotal")
    '        iGeneradorSql.agregarColumna("cc.detalle")
    '        iGeneradorSql.agregarColumna("cc.pagado")

    '        Select Case eAsientosModelos.agruparPor
    '            Case FuncionComun.enumAgrupacionContabilidad.PORMOVIMIENTO
    '                iGeneradorSql.agregarColumna("min(cc.id) as relacion")
    '            Case FuncionComun.enumAgrupacionContabilidad.PORFECHA
    '                iGeneradorSql.agregarColumna("min(cc.fecha) as relacion")
    '            Case FuncionComun.enumAgrupacionContabilidad.MES
    '                iGeneradorSql.agregarColumna("min(" & FuncionComun.sqlFormatoFecha("cc.fecha", FuncionComun.enumFormatoFecha.MMYYYY) & ") as relacion")
    '        End Select

    '        iGeneradorSql.agregarTabla("comprobantecompra cc left join RelacionMovimientoContable r  on (r.idEntidad=cc.id and r.idTipoEntidad=" & TipoEntidad.COMPROBANTECOMPRA & ") ")

    '        iGeneradorSql.agregarCondicionWhere("r.id is null")
    '        iGeneradorSql.agregarCondicionWhere("cc.idEstado=" & Estado.ALTA)
    '        iGeneradorSql.agregarCondicionWhere("cc.fecha<='" & Format(eFecha, "yyyy-MM-dd") & "'")
    '        iGeneradorSql.agregarCondicionWhere("cc.idAsientosModelos=" & eAsientosModelos.id)

    '        If Not IsNothing(eAsientosModelos.bancoDebito) Then
    '            iGeneradorSql.agregarTablaConJoin("proveedor p", "cc.idproveedor=p.id", False)
    '            iGeneradorSql.agregarTablaConJoin("bancodebitobanco bd", "bd.idBanco=p.idBanco", False)
    '            iGeneradorSql.agregarCondicionWhere("bd.idBancoDebito=" & eAsientosModelos.bancoDebito.id)
    '            iGeneradorSql.agregarCondicionWhere("cc.FormaPago=" & ComprobanteCompra.enumFormaPago.BANCO)
    '        Else
    '            iGeneradorSql.agregarCondicionWhere("cc.FormaPago=" & ComprobanteCompra.enumFormaPago.CAJA)
    '        End If

    '        Select Case eAsientosModelos.agruparPor
    '            Case FuncionComun.enumAgrupacionContabilidad.PORMOVIMIENTO
    '                iGeneradorSql.agregarGroupBy("cc.id")
    '            Case FuncionComun.enumAgrupacionContabilidad.PORFECHA
    '                iGeneradorSql.agregarGroupBy("cc.fecha")
    '            Case FuncionComun.enumAgrupacionContabilidad.MES
    '                iGeneradorSql.agregarGroupBy(FuncionComun.sqlFormatoFecha("cc.fecha", FuncionComun.enumFormatoFecha.MMYYYY))
    '        End Select

    '        iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "ComprobanteCompraAContabilizar")

    '        iGeneradorSql.agregarColumna("cc.id")
    '        Select Case eAsientosModelos.agruparPor
    '            Case FuncionComun.enumAgrupacionContabilidad.PORMOVIMIENTO
    '                iGeneradorSql.agregarColumna("cc.id as relacion")
    '            Case FuncionComun.enumAgrupacionContabilidad.PORFECHA
    '                iGeneradorSql.agregarColumna("cc.fecha as relacion")
    '            Case FuncionComun.enumAgrupacionContabilidad.MES
    '                iGeneradorSql.agregarColumna(FuncionComun.sqlFormatoFecha("cc.fecha", FuncionComun.enumFormatoFecha.MMYYYY) & " as relacion")
    '        End Select

    '        iGeneradorSql.agregarTabla("comprobantecompra cc left join RelacionMovimientoContable r on (r.idEntidad=cc.id and r.idTipoEntidad=" & TipoEntidad.COMPROBANTECOMPRA & ") ")

    '        iGeneradorSql.agregarCondicionWhere("r.id is null")
    '        iGeneradorSql.agregarCondicionWhere("cc.idEstado=" & Estado.ALTA)
    '        iGeneradorSql.agregarCondicionWhere("cc.fecha<='" & Format(eFecha, "yyyy-MM-dd") & "'")
    '        iGeneradorSql.agregarCondicionWhere("cc.idAsientosModelos=" & eAsientosModelos.id)

    '        If Not IsNothing(eAsientosModelos.bancoDebito) Then
    '            iGeneradorSql.agregarTablaConJoin("proveedor p", "cc.idproveedor=p.id", False)
    '            iGeneradorSql.agregarTablaConJoin("bancodebitobanco bd", "bd.idBanco=p.idBanco", False)
    '            iGeneradorSql.agregarCondicionWhere("bd.idBancoDebito=" & eAsientosModelos.bancoDebito.id)
    '            iGeneradorSql.agregarCondicionWhere("cc.FormaPago=" & ComprobanteCompra.enumFormaPago.BANCO)
    '        Else
    '            iGeneradorSql.agregarCondicionWhere("cc.FormaPago=" & ComprobanteCompra.enumFormaPago.CAJA)
    '        End If

    '        iDataSet = iConexion.getDataSet(iDataSet, iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "ComprobanteCompraAContabilizarMarcar")

    '        iColumnaPadre = iDataSet.Tables("ComprobanteCompraAContabilizar").Columns("relacion")
    '        iColumnaHijo = iDataSet.Tables("ComprobanteCompraAContabilizarMarcar").Columns("relacion")
    '        iDataRelation = New DataRelation("ComprobanteCompraDetalle", iColumnaPadre, iColumnaHijo)
    '        iDataSet.Relations.Add(iDataRelation)

    '        Return iDataSet

    '    Catch excepcion As Exception
    '        'Throw New SolicitudNoEncontradaException(excepcion)
    '    Finally
    '        If (IsNothing(MyBase.accesoDatos)) Then
    '            iConexion.cerrar()
    '            iConexion = Nothing
    '        End If
    '        iGeneradorSql.destructor()
    '        iGeneradorSql = Nothing
    '    End Try
    'End Function

    'Public Function obtenerComprobanteCompraGrillaOrdenDePago(ByVal eComprobanteCompraVO As ComprobanteCompraVO) As DataSet
    '    Dim iGeneradorSql As New GeneradorSql
    '    Dim iTablaOrdenDePago As String

    '    Try

    '        iConexion = obtenerConexion()

    '        iGeneradorSql.agregarColumna("DISTINCT odpcc.idComprobanteCompra")
    '        iGeneradorSql.agregarTabla("Ordendepagodetalle odpcc")
    '        iGeneradorSql.agregarTabla("Ordendepago odp")
    '        iGeneradorSql.agregarCondicionWhere("odpcc.idOrdendepago=odp.id")
    '        iGeneradorSql.agregarCondicionWhere("odp.idestado=" & Estado.ALTA)

    '        iTablaOrdenDePago = iGeneradorSql.generarSelect

    '        iGeneradorSql.agregarColumna("cc.id")
    '        iGeneradorSql.agregarColumna("tc.descripcion as tipoComprobante")
    '        iGeneradorSql.agregarColumna("cc.numeroFactura as numero")
    '        iGeneradorSql.agregarColumna("cc.fecha")
    '        iGeneradorSql.agregarColumna("cc.montoTotal as importeTotal")

    '        iGeneradorSql.agregarTabla("comprobanteCompra cc left join (" & iTablaOrdenDePago & ") odp on cc.id=odp.idComprobanteCompra")
    '        iGeneradorSql.agregarTabla("proveedor pr")
    '        iGeneradorSql.agregarTabla("tipoComprobante tc")

    '        iGeneradorSql.agregarCondicionWhere("cc.idProveedor=pr.id")
    '        iGeneradorSql.agregarCondicionWhere("tc.id=cc.idTipoComprobante")

    '        With eComprobanteCompraVO
    '            If Not IsNothing(.proveedor) Then iGeneradorSql.agregarCondicionWhere("pr.id=" & .proveedor.id)
    '            If Not IsNothing(.tipoComprobante) Then iGeneradorSql.agregarCondicionWhere("tc.id=" & .tipoComprobante.id)
    '            If Not IsNothing(.ordenDePago) Then
    '                If .ordenDePago.Booleano Then
    '                    iGeneradorSql.agregarCondicionWhere("odp.idComprobanteCompra is not null")
    '                Else
    '                    iGeneradorSql.agregarCondicionWhere("odp.idComprobanteCompra is null")
    '                End If
    '            End If
    '            If .formaPago <> Nothing Then iGeneradorSql.agregarCondicionWhere("cc.formaPago=" & .formaPago)
    '        End With

    '        Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "ComprobanteCompra")

    '    Catch ex As Exception
    '        'Throw New ProveedorNoEncontradoException(ex)
    '    Finally
    '        If (IsNothing(MyBase.accesoDatos)) Then
    '            iConexion.cerrar()
    '            iConexion = Nothing
    '        End If
    '        iGeneradorSql.destructor()
    '        iGeneradorSql = Nothing
    '    End Try
    'End Function

    Public Sub marcarPago()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarSet("pagado=" & FuncionComun.booleanByte(True))
            iGeneradorSql.agregarTabla("comprobantecompra")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL())

        Catch excepcion As Exception
            Throw New ComprobanteNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        Catch ex As Exception
            Throw New RootException(ex)
        End Try
    End Sub

#End Region

End Class