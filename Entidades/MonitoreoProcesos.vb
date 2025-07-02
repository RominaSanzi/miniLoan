Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.seguridad

Public Class MonitoreoProcesos
    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iProceso As String
    Private iFecha As Date
    Private iFechaInicio As Date
    Private iFechaFin As Date
    Private iEstado As String
    Private iMensaje As String

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
    Public Property Proceso() As String
        Get
            Return iProceso
        End Get
        Set(ByVal Value As String)
            iProceso = Value
        End Set
    End Property
    Public Property Fecha() As Date
        Get
            Return iFecha
        End Get
        Set(ByVal Value As Date)
            iFecha = Value
        End Set
    End Property
    Public Property FechaInicio() As Date
        Get
            Return iFechaInicio
        End Get
        Set(ByVal Value As Date)
            iFechaInicio = Value
        End Set
    End Property
    Public Property FechaFin() As Date
        Get
            Return iFechaFin
        End Get
        Set(ByVal Value As Date)
            iFechaFin = Value
        End Set
    End Property
    Public Property Estado() As String
        Get
            Return iEstado
        End Get
        Set(ByVal Value As String)
            iEstado = Value
        End Set
    End Property
    Public Property Mensaje() As String
        Get
            Return iMensaje
        End Get
        Set(ByVal Value As String)
            iMensaje = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Private Sub validarCrear()

        Try

            If Proceso = Nothing Then
                Throw New MonitoreoProcesosNoCreadoException("El Proceso no puede ser nulo")
            End If
            If Fecha = Nothing Then
                Throw New MonitoreoProcesosNoCreadoException("La Fecha no puede ser nula")
            End If
            If FechaInicio = Nothing Then
                Throw New MonitoreoProcesosNoCreadoException("La Fecha inicio no puede ser nula")
            End If
            If Estado = Nothing Then
                Throw New MonitoreoProcesosNoCreadoException("El Estado no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoCreadoException(excepcion)
        End Try
    End Sub
    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Monitoreoprocesos")

            iGeneradorSql.agregarColumna("Proceso")
            iGeneradorSql.agregarColumna("Fecha")
            iGeneradorSql.agregarColumna("FechaInicio")
            iGeneradorSql.agregarColumna("FechaFin")
            iGeneradorSql.agregarColumna("Estado")
            iGeneradorSql.agregarColumna("Mensaje")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Proceso))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Fecha))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingFechaHora(FechaInicio))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothingFechaHora(FechaFin))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Estado))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Mensaje))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
    Private Sub validarModificar()
        Try

            If Proceso = Nothing Then
                Throw New MonitoreoProcesosNoModificadoException("El Proceso no puede ser nulo")
            End If
            If Fecha = Nothing Then
                Throw New MonitoreoProcesosNoModificadoException("La Fecha no puede ser nula")
            End If
            If FechaInicio = Nothing Then
                Throw New MonitoreoProcesosNoModificadoException("La Fecha inicio no puede ser nula")
            End If
            If Estado = Nothing Then
                Throw New MonitoreoProcesosNoModificadoException("El Estado no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoModificadoException(excepcion)
        End Try
    End Sub
    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("Proceso=" & FuncionComun.nuloSiEsNothing(Proceso))
            iGeneradorSql.agregarSet("Fecha=" & FuncionComun.nuloSiEsNothing(Fecha))
            iGeneradorSql.agregarSet("FechaInicio=" & FuncionComun.nuloSiEsNothingFechaHora(FechaInicio))
            iGeneradorSql.agregarSet("FechaFin=" & FuncionComun.nuloSiEsNothingFechaHora(FechaFin))
            iGeneradorSql.agregarSet("Estado=" & FuncionComun.nuloSiEsNothing(Estado))
            iGeneradorSql.agregarSet("Mensaje=" & FuncionComun.nuloSiEsNothing(Mensaje))

            iGeneradorSql.agregarTabla("Monitoreoprocesos")

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
    Private Sub validarModificarEstado()
        Try

            If id = Nothing Then
                Throw New MonitoreoProcesosNoModificadoException("El ID no puede ser nulo")
            End If
            If FechaFin = Nothing Then
                Throw New MonitoreoProcesosNoModificadoException("La Fecha inicio no puede ser nula")
            End If
            If Estado = Nothing Then
                Throw New MonitoreoProcesosNoModificadoException("El Estado no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoModificadoException(excepcion)
        End Try
    End Sub
    Public Sub modificarEstado()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificarEstado()

            iGeneradorSql.agregarSet("FechaFin=" & FuncionComun.nuloSiEsNothingFechaHora(FechaFin))
            iGeneradorSql.agregarSet("Estado=" & FuncionComun.nuloSiEsNothing(Estado))
            iGeneradorSql.agregarSet("Mensaje=" & FuncionComun.nuloSiEsNothing(Mensaje))

            iGeneradorSql.agregarTabla("Monitoreoprocesos")

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
    Public Function obtenerMonitoreoprocesosGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Proceso")
            iGeneradorSql.agregarColumna("Fecha")
            iGeneradorSql.agregarColumna("FechaInicio")
            iGeneradorSql.agregarColumna("FechaFin")
            iGeneradorSql.agregarColumna("Estado")
            iGeneradorSql.agregarColumna("Mensaje")

            iGeneradorSql.agregarTabla("Monitoreoprocesos")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))

            iGeneradorSql.agregarLimit("100")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Monitoreoprocesos")

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function
    Public Function obtenerMonitoreoprocesos() As MonitoreoProcesos
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Proceso")
            iGeneradorSql.agregarColumna("Fecha")
            iGeneradorSql.agregarColumna("FechaInicio")
            iGeneradorSql.agregarColumna("FechaFin")
            iGeneradorSql.agregarColumna("Estado")
            iGeneradorSql.agregarColumna("Mensaje")

            iGeneradorSql.agregarTabla("Monitoreoprocesos")

            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iProceso = FuncionComun.nothingSiEsNulo(iDataReader.Item("Proceso"))
                iFecha = FuncionComun.nothingSiEsNulo(iDataReader.Item("Fecha"))
                iFechaInicio = FuncionComun.nothingSiEsNulo(iDataReader.Item("Fechainicio"))
                iFechaFin = FuncionComun.nothingSiEsNulo(iDataReader.Item("Fechafin"))
                iEstado = FuncionComun.nothingSiEsNulo(iDataReader.Item("Estado"))
                iMensaje = FuncionComun.nothingSiEsNulo(iDataReader.Item("Mensaje"))

                iDataReader.Close()

                Return Me
            Else
                Throw New MonitoreoProcesosNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoEncontradoException(excepcion)
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
    End Function
    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Monitoreoprocesos")

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New MonitoreoProcesosNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub
#End Region

End Class