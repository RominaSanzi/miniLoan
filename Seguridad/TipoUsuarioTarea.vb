Imports di.financiera.excepciones
Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.utils
Imports di.financiera.seguridad

Public Class TipoUsuarioTarea

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iSoloTramiteGestion As Boolean?

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
    Public Property descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property

    Public Property soloTramiteGestion As Boolean?
        Get
            Return iSoloTramiteGestion
        End Get
        Set(value As Boolean?)
            iSoloTramiteGestion = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerTipoUsuarioTarea() As TipoUsuarioTarea
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("soloTramiteGestion")
            iGeneradorSql.agregarTabla("TipoUsuarioTarea")

            iGeneradorSql.agregarCondicionWhere("id=" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                Return Me
            Else
                Throw New RootException
            End If

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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

    Public Function obtenerTiposUsuarioTareaDataSet() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("soloTramiteGestion")
            If iDescripcion <> "" Then
                iGeneradorSql.agregarCondicionWhere("descripcion Like'" & iDescripcion & "%'")
            End If
            If Not IsNothing(iSoloTramiteGestion) Then
                iGeneradorSql.agregarCondicionWhere("soloTramiteGestion=" & FuncionComun.booleanByte(iSoloTramiteGestion.Value))
            End If
            iGeneradorSql.agregarTabla("TipoUsuarioTarea")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "TipoUsuarioTarea")

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerTiposUsuarioTarea() As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("soloTramiteGestion")
            If Not IsNothing(iDescripcion) Then iGeneradorSql.agregarCondicionWhere("descripcion Like'" & iDescripcion & "%'")
            If Not IsNothing(iId) AndAlso iId <> 0 Then iGeneradorSql.agregarCondicionWhere("id =" & iId)
            If Not IsNothing(iSoloTramiteGestion) Then
                iGeneradorSql.agregarCondicionWhere("soloTramiteGestion=" & FuncionComun.booleanByte(iSoloTramiteGestion.Value))
            End If

            iGeneradorSql.agregarTabla("tipoUsuarioTarea")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarCrear()
        If descripcion = Nothing Then
            Throw New RootException("La descripcion no puede ser nula")
        Else
            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader

            Try
                iConexion = obtenerConexion()
                iGeneradorSql.agregarTabla("tipoUsuarioTarea")
                iGeneradorSql.agregarColumna("descripcion")
                iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    Throw New RootException("La descripción del tipo de usuario tarea ya existe")
                End If

            Catch excepcion As Exception
                Throw New RootException(excepcion)
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
        End If
    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("tipoUsuarioTarea")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("soloTramiteGestion")

            iGeneradorSql.agregarValue("'" & descripcion & "'")
            If Not IsNothing(iSoloTramiteGestion) Then
                iGeneradorSql.agregarValue(FuncionComun.booleanByte(iSoloTramiteGestion.Value))
            Else
                iGeneradorSql.agregarValue("null")
            End If


            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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
        If id = Nothing Then
            Throw New RootException("El tipo de usuario tarea es inexistente")
        End If

        If descripcion = Nothing Then
            Throw New RootException("La descripcion no puede ser nula")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("tipoUsuarioTarea")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If Not iDataReader.Read Then
                Throw New RootException("El tipo de usuario tarea es inexistente")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarTabla("tipoUsuarioTarea")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id <>" & id)
            iGeneradorSql.agregarCondicionWhere("descripcion ='" & descripcion & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New RootException("La descripcion existe para otro usuario tarea")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New RootException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarTabla("tipoUsuarioTarea")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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
            Throw New RootException("El tipo de usuario tarea es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("tipoUsuarioTarea")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If Not iDataReader.Read Then
                Throw New RootException("El tipo de usuario tarea es inexistente")
            End If

            iDataReader.Close()

            iGeneradorSql.agregarTabla("usuario")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idTipoUsuarioTarea=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New RootException("El tipo de usuario tarea esta relacionada con un usuario")
            End If

            iDataReader.Close()

            iGeneradorSql.agregarTabla("tarea")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idTipoUsuarioTarea=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New RootException("El tipo de usuario tarea esta relacionada con una tarea")
            End If

            iDataReader.Close()




            iGeneradorSql.agregarTabla("MotivotramiteGestion")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idTipoUsuarioTarea=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New RootException("El tipo de usuario tarea esta relacionada con una motivo de tramite")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarTabla("tramitegestion")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idTipoUsuarioTarea=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New RootException("El tipo de usuario tarea esta relacionada con una tramite gestion")
            End If
            iDataReader.Close()

            iGeneradorSql.agregarTabla("tramitegestiondetalle")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idTipoUsuarioTarea=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New RootException("El tipo de usuario tarea esta relacionada con una tramite gestion detalle")
            End If
            iDataReader.Close()
        Catch excepcion As Exception
            Throw New RootException(excepcion)
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

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("tipoUsuarioTarea")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New RootException(excepcion)
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

        Catch exception As exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class
