Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.datos

Public Class TipoEntidad
    Inherits Entidad

#Region "Constantes"
    Public Const SOLICITUD As Integer = 1
    Public Const RECIBO As Integer = 2
    Public Const TRAMITE As Integer = 3
    Public Const CLIENTE As Integer = 4
    Public Const COMERCIO As Integer = 5
    Public Const USUARIO As Integer = 6
    Public Const RESUMENTARJETA As Integer = 7
    Public Const MOVIMIENTO As Integer = 8
    Public Const CONVENIO As Integer = 9
    Public Const RECIBOCONVENIO As Integer = 10
    Public Const CONTROLREMITOSOLICITUD As Integer = 11
    Public Const CREARREMITOSOLICITUD As Integer = 12
    Public Const SOLICITUDSUPERVISOR As Integer = 13
    Public Const RECIBORESUMENTARJETA As Integer = 14
    Public Const ASIGNACIONESTUDIOMANUAL As Integer = 15
    Public Const RECIBOTRAMITE As Integer = 16
    Public Const CUENTACORRIENTE As Integer = 17
    Public Const NIVEL As Integer = 18
    Public Const SOLICITUDRECHAZO As Integer = 19
    Public Const SOLICITUDPENDIENTE As Integer = 20
    Public Const AUTORIZACION As Integer = 21
    Public Const LIQUIDACION As Integer = 22
    Public Const MOVIMIENTOCUENTACORRIENTE As Integer = 23
    Public Const CONTROLENTRADASOLICITUD As Integer = 24
    Public Const VERIFICACIONTELEFONICASOLICITUD As Integer = 25
    Public Const RECIBOCUOTASOCIAL As Integer = 26
    Public Const LIQUIDACIONALTA As Integer = 27
    Public Const RECIBOANULACION As Integer = 28
    Public Const CREARREMITOCOMPLEMENTARIO As Integer = 29
    Public Const RECIBOANULACIONCUOTASOCIAL As Integer = 30
    Public Const CONTROLDIGITAL As Integer = 31
    Public Const CONTROLEFECTIVO As Integer = 32
    Public Const SOLICITUDPENDIENTESERVICIO As Integer = 33
    Public Const CERTIFICADO As Integer = 34
    Public Const ACUERDOCOMERCIAL As Integer = 35
    Public Const SOLICITUDDESAPROBAR As Integer = 36
    Public Const TRAMITEGESTIONREFERENCIA As Integer = 37
    Public Const CONCEPTOCOMPRA As Integer = 40
    Public Const CONCEPTOVENTA As Integer = 41
    Public Const COMPROBANTECOMPRA As Integer = 42
    Public Const ALTACUOTASOCIAL As Integer = 43
    Public Const BAJACUOTASOCIAL As Integer = 44
    Public Const CHEQUERA As Integer = 45
    Public Const COMPROBANTE As Integer = 46
    Public Const OPERACIONREALIZADASOLICITUD As Integer = 47
    Public Const FORMAPAGO As Integer = 48
    Public Const OPERACIONREALIZADAREFINANCIACION As Integer = 49
    Public Const RECIBOTARJETA As Integer = 50
    Public Const CONFIRMARSOLICITUD As Integer = 51
    Public Const COMISIONSOLICITUD As Integer = 52
    Public Const DEVENGAMIENTOORIGEN As Integer = 53
    Public Const DEVENGAMIENTOCUOTA As Integer = 54
    Public Const CUOTANOCOBRADA As Integer = 55
    Public Const DEVENGAMIENTOCOMISION As Integer = 56
    Public Const DEVENGAMIENTOAJUSTE As Integer = 57
    Public Const PREVISIONBCRA As Integer = 58 
    Public Const COMPRADORCARTERA As Integer = 59
    Public Const CARTERA As Integer = 60
    Public Const COBRANZARESUMEN As Integer = 61
    Public Const ALTASOCIOTARJETA As Integer = 62
    Public Const CERTIFICADOPRECANCELACION As Integer = 63
    Public Const LOGDEBITODETALLE As Integer = 64
    Public Const LOGDEBITO As Integer = 65
    Public Const PRODUCTOCOMPRADO As Integer = 66
#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
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
    Public Property Descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerTipoEntidad() As TipoEntidad
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("TipoEntidad")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                id = iDataReader.Item("id").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                Return Me
            Else
                Throw New TipoEntidadNoEncontradaException
            End If

        Catch excepcion As Exception
            Throw New TipoEntidadNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerTiposEntidades() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("TipoEntidad")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarOrden("descripcion")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New TipoEntidadNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerTiposEntidadesTramite() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("TipoEntidad")
            iGeneradorSql.agregarCondicionWhere("id in(" & TipoEntidad.CLIENTE & "," & TipoEntidad.COMERCIO & "," & TipoEntidad.SOLICITUD & "," & TipoEntidad.USUARIO & ")")
            iGeneradorSql.agregarOrden("descripcion")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New TipoEntidadNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerTiposEntidadesInterfaceTemplate() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("TipoEntidad")
            iGeneradorSql.agregarCondicionWhere("id in(" & TipoEntidad.CLIENTE & "," & TipoEntidad.COMERCIO & "," & TipoEntidad.SOLICITUD & "," & TipoEntidad.RECIBO & ")")
            iGeneradorSql.agregarOrden("descripcion")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New TipoEntidadNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerTiposEntidadDatoAnexo() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("TipoEntidad")
            iGeneradorSql.agregarCondicionWhere("id in(" & TipoEntidad.CLIENTE & "," & TipoEntidad.COMERCIO & "," & TipoEntidad.COMPRADORCARTERA & "," & TipoEntidad.CARTERA & "," & TipoEntidad.PRODUCTOCOMPRADO & ")")
            iGeneradorSql.agregarOrden("descripcion")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New TipoEntidadNoEncontradaException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function
    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

        Catch exception As exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class
