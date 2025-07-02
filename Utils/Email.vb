Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.IO
Imports System.Configuration

Public Class Email
    Inherits Entidad

#Region "Constantes"
    Public Const NOENVIADO As Integer = 1
    Public Const ENVIADO As Integer = 2
    Public Const CONERROR As Integer = 3
#End Region

#Region "Variables"
    Private iId As Long
    Private iFechaEnvio As Date
    Private iHoraEnvio As TimeSpan
    Private iDireccionDestino As String
    Private iMotivo As String
    Private iCuerpo As String
    Private iAdjuntos As Collection
    Private iAccion As Integer

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
    Public Property horaEnvio() As TimeSpan
        Get
            Return iHoraEnvio
        End Get
        Set(ByVal Value As TimeSpan)
            iHoraEnvio = Value
        End Set
    End Property
    Public Property fechaEnvio() As Date
        Get
            Return iFechaEnvio
        End Get
        Set(ByVal Value As Date)
            iFechaEnvio = Value
        End Set
    End Property
    Public Property direccionDestino() As String
        Get
            Return iDireccionDestino
        End Get
        Set(ByVal Value As String)
            iDireccionDestino = Value
        End Set
    End Property
    Public Property cuerpo() As String
        Get
            Return iCuerpo
        End Get
        Set(ByVal Value As String)
            iCuerpo = Value
        End Set
    End Property
    Public Property motivo() As String
        Get
            Return iMotivo
        End Get
        Set(ByVal Value As String)
            iMotivo = Value
        End Set
    End Property
    Public Property adjuntos() As Collection
        Get
            Return iAdjuntos
        End Get
        Set(ByVal Value As Collection)
            iAdjuntos = Value
        End Set
    End Property
    Public Property accion() As Integer
        Get
            Return iAccion
        End Get
        Set(ByVal Value As Integer)
            iAccion = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerEmail() As Email
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql


        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("direccionDestino")
            iGeneradorSql.agregarColumna("motivo")
            iGeneradorSql.agregarColumna("cuerpo")
            iGeneradorSql.agregarColumna("fechaEnvio")
            iGeneradorSql.agregarColumna("horaEnvio")
            iGeneradorSql.agregarColumna("accion")

            iGeneradorSql.agregarTabla("Email")

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id")
                iDireccionDestino = iDataReader.Item("direccionDestino").ToString
                iMotivo = iDataReader.Item("motivo").ToString
                iCuerpo = iDataReader.Item("cuerpo").ToString
                iFechaEnvio = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaEnvio").ToString)
                iHoraEnvio = iDataReader.Item("HoraEnvio")
                iAccion = iDataReader.Item("accion")

                iDataReader.Close()

                obtenerAdjuntos()

                Return Me
            Else
                Throw New EmailNoEncontradoException
            End If

        Catch excepcion As Exception
            Throw New EmailNoEncontradoException(excepcion)
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

    Public Function obtenerEmails(ByVal eEmailConsultaVO As EmailConsultaVO) As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("e.id")
            iGeneradorSql.agregarColumna("REPLACE(e.direccionDestino,';',' ') as direccionDestino")
            iGeneradorSql.agregarColumna("e.motivo")
            iGeneradorSql.agregarColumna("e.cuerpo")
            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar(FuncionComun.sqlFormatoFecha("e.fechaEnvio", FuncionComun.enumFormatoFecha.DDMMYY) & ",' - '," & FuncionComun.sqlFormatoFecha("e.horaEnvio", FuncionComun.enumFormatoFecha.HHMM)) & " as fechaEnvio")
            iGeneradorSql.agregarColumna("count(ea.id) as cantidadAdjuntos")

            iGeneradorSql.agregarTabla("email e left join emailAdjunto ea on ea.idEmail=e.id")

            With eEmailConsultaVO
                If .fechaEnvioDesde <> Nothing Then iGeneradorSql.agregarCondicionWhere("e.fechaEnvio>=" & FuncionComun.nuloSiEsNothing(.fechaEnvioDesde))
                If .fechaEnvioHasta <> Nothing Then iGeneradorSql.agregarCondicionWhere("e.fechaEnvio<=" & FuncionComun.nuloSiEsNothing(.fechaEnvioHasta))
                If .direccionDestino <> Nothing Then iGeneradorSql.agregarCondicionWhere("e.direccionDestino like '" & .direccionDestino & "%'")
                If .asunto <> Nothing Then iGeneradorSql.agregarCondicionWhere("e.motivo like '" & .asunto & "%'")
                Select Case .accion
                    Case EmailConsultaVO.EnumTipoAccion.CONERROR
                        iGeneradorSql.agregarCondicionWhere("e.accion=" & Email.CONERROR)
                    Case EmailConsultaVO.EnumTipoAccion.NOENVIADO
                        iGeneradorSql.agregarCondicionWhere("e.accion=" & Email.NOENVIADO)
                    Case EmailConsultaVO.EnumTipoAccion.CONERRORNOENVIADO
                        iGeneradorSql.agregarCondicionWhere("e.accion in (" & Email.CONERROR & " ," & Email.NOENVIADO & ")")
                    Case EmailConsultaVO.EnumTipoAccion.ENVIADO
                        iGeneradorSql.agregarCondicionWhere("e.accion=" & Email.ENVIADO)
                    Case EmailConsultaVO.EnumTipoAccion.TODOS
                End Select
            End With

            iGeneradorSql.agregarLimit("500")
            iGeneradorSql.agregarGroupBy("e.id")
            iGeneradorSql.agregarOrden("id DESC")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "email")

        Catch excepcion As Exception
            Throw New EmailNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Private Sub validarCrear()
        If direccionDestino = Nothing Then
            Throw New EmailNoCreadoException("La dirección destino no puede ser nula")
        End If

        If fechaEnvio = Nothing Then
            Throw New EmailNoCreadoException("La fecha de envio no puede ser nula")
        End If

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("email")

            iGeneradorSql.agregarColumna("direccionDestino")
            iGeneradorSql.agregarColumna("motivo")
            iGeneradorSql.agregarColumna("cuerpo")
            iGeneradorSql.agregarColumna("fechaEnvio")
            iGeneradorSql.agregarColumna("horaEnvio")
            iGeneradorSql.agregarColumna("accion")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(direccionDestino))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(motivo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(cuerpo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(fechaEnvio))
            iGeneradorSql.agregarValue("'" & iHoraEnvio.ToString & "'")
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(accion))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            crearAdjuntos()

        Catch excepcion As Exception
            Throw New EmailNoCreadoException(excepcion)
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
            Throw New EmailNoEliminadoException("El mail es inexistente")
        End If
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            eliminarAdjuntos()

            iGeneradorSql.agregarTabla("email")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New EmailNoEliminadoException(excepcion)
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
        If direccionDestino = Nothing Then
            Throw New EmailNoModificadoException("La dirección destino no puede ser nula")
        End If

        If id = Nothing Then
            Throw New EmailNoModificadoException("El identificador de envio no puede ser nulo")
        End If
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("direccionDestino=" & FuncionComun.nuloSiEsNothing(direccionDestino))
            iGeneradorSql.agregarSet("accion=" & FuncionComun.nuloSiEsNothing(accion))

            iGeneradorSql.agregarTabla("email")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New EmailNoModificadoException(excepcion)
        Finally
            iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

#Region "Adjuntos"
    Private Sub obtenerAdjuntos()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("EmailAdjunto")
            iGeneradorSql.agregarColumna("Nombre")
            iGeneradorSql.agregarCondicionWhere("idEmail=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            iAdjuntos = New Collection
            While iDataReader.Read
                iAdjuntos.Add(ConfigurationManager.AppSettings("archivosEmail") & iDataReader.Item("nombre").ToString)
            End While

        Catch excepcion As Exception
            Throw New EmailNoEncontradoException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub

    Public Sub crearAdjuntos()
        Dim iGeneradorSql As New GeneradorSql
        Dim iNombreArchivo As String
        Dim i As Integer

        Try
            iConexion = obtenerConexion()

            If Not IsNothing(iAdjuntos) Then

                For i = 1 To iAdjuntos.Count

                    iNombreArchivo = Format(Now, "yyyyMMddHHmmss") & Right(iAdjuntos.Item(i), iAdjuntos.Item(i).Length - InStrRev(iAdjuntos.Item(i), "\"))

                    File.Copy(iAdjuntos.Item(i), ConfigurationSettings.AppSettings("archivosEmail") & iNombreArchivo, True)

                    iGeneradorSql.agregarTabla("EmailAdjunto")

                    iGeneradorSql.agregarColumna("idEmail")
                    iGeneradorSql.agregarColumna("Nombre")

                    iGeneradorSql.agregarValue(id)
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iNombreArchivo))

                    iConexion.ejecutar(iGeneradorSql.GenerarInsert, iGeneradorSql.parametrosSQL)
                Next i

            End If

        Catch excepcion As Exception
            Throw New EmailNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub

    Public Sub eliminarAdjuntos()
        Dim iGeneradorSql As New GeneradorSql
        Dim iPathArchivo As String
        Dim i As Integer

        Try

            iConexion = obtenerConexion()

            If Not IsNothing(iAdjuntos) AndAlso iAdjuntos.Count > 0 Then
                For i = 1 To iAdjuntos.Count
                    iPathArchivo = iAdjuntos.Item(i)
                    If File.Exists(iPathArchivo) Then File.Delete(iPathArchivo)
                    iPathArchivo = Nothing
                Next
            End If

            iGeneradorSql.agregarCondicionWhere("idEmail=" & iId)
            iGeneradorSql.agregarTabla("EmailAdjunto")

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New EmailNoEliminadoException(excepcion)
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

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If

        Catch exception As Exception
            Throw New RootException(exception)
        End Try

    End Sub

#End Region

End Class
