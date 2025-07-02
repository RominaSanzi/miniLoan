Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports System.Collections.Generic

Public Class AccesoDirecto

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iImagen As String
    Private iDescripcion As String
    Private iPagina As String
    Private iMenu As String
    Private iOrden As Integer
    Private iUsuario As Usuario
    Private iAbrirModal As Boolean

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
    Public Property imagen() As String
        Get
            Return iImagen
        End Get
        Set(ByVal Value As String)
            iImagen = Value
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
    Public Property pagina() As String
        Get
            Return iPagina
        End Get
        Set(ByVal Value As String)
            iPagina = Value
        End Set
    End Property
    Public Property menu() As String
        Get
            Return iMenu
        End Get
        Set(ByVal Value As String)
            iMenu = Value
        End Set
    End Property
    Public Property orden() As Integer
        Get
            Return iOrden
        End Get
        Set(ByVal Value As Integer)
            iOrden = Value
        End Set
    End Property
    Public Property usuario() As Usuario
        Get
            Return iUsuario
        End Get
        Set(ByVal Value As Usuario)
            iUsuario = Value
        End Set
    End Property
    Public Property abrirModal() As Boolean
        Get
            Return iAbrirModal
        End Get
        Set(ByVal Value As Boolean)
            iAbrirModal = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Private Sub validarCrear()

        Try

            If imagen = Nothing Then
                Throw New AccesoDirectoNoCreadoException("La imagen no puede ser nula")
            End If
            If descripcion = Nothing Then
                Throw New AccesoDirectoNoCreadoException("La descripcion no puede ser nula")
            End If
            If pagina = Nothing Then
                Throw New AccesoDirectoNoCreadoException("La pagina no puede ser nula")
            End If
            If orden = Nothing Then
                Throw New AccesoDirectoNoCreadoException("El orden no puede ser nulo")
            End If
            If IsNothing(iUsuario) Then
                Throw New AccesoDirectoNoCreadoException("El usuario no puede ser nulo")
            End If

        Catch excepcion As Exception
            Throw New AccesoDirectoNoCreadoException(excepcion)
        End Try
    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Accesodirecto")

            iGeneradorSql.agregarColumna("imagen")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("pagina")
            iGeneradorSql.agregarColumna("menu")
            iGeneradorSql.agregarColumna("orden")
            iGeneradorSql.agregarColumna("idUsuario")
            iGeneradorSql.agregarColumna("abrirModal")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(imagen))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(pagina))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(menu))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(orden))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(usuario.id))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(abrirModal))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New AccesoDirectoNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerAccesosDirectosPorUsuario(eUsuario As Usuario) As List(Of AccesoDirecto)
        Dim iDataSet As DataSet
        Dim iGeneradorSql As New GeneradorSql
        Dim iAccesosDirectos As New List(Of AccesoDirecto)
        Dim iAccesoDirecto As AccesoDirecto
        Dim i As Integer

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("imagen")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("pagina")
            iGeneradorSql.agregarColumna("menu")
            iGeneradorSql.agregarColumna("orden")
            iGeneradorSql.agregarColumna("abrirModal")

            iGeneradorSql.agregarTabla("AccesoDirecto")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & eUsuario.id)
            iGeneradorSql.agregarOrden("orden")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "AccesosDirectos")

            For i = 0 To iDataSet.Tables("AccesosDirectos").Rows.Count - 1
                iAccesoDirecto = New AccesoDirecto
                With iAccesoDirecto
                    .id = iDataSet.Tables("AccesosDirectos").Rows(i).Item("Id").ToString
                    .imagen = iDataSet.Tables("AccesosDirectos").Rows(i).Item("imagen").ToString
                    .descripcion = iDataSet.Tables("AccesosDirectos").Rows(i).Item("descripcion").ToString
                    .pagina = iDataSet.Tables("AccesosDirectos").Rows(i).Item("pagina").ToString
                    .menu = iDataSet.Tables("AccesosDirectos").Rows(i).Item("menu").ToString
                    .orden = iDataSet.Tables("AccesosDirectos").Rows(i).Item("orden").ToString
                    .abrirModal = FuncionComun.byteBoolean(iDataSet.Tables("AccesosDirectos").Rows(i).Item("abrirModal").ToString)
                End With
                iAccesosDirectos.Add(iAccesoDirecto)
                iAccesoDirecto = Nothing
            Next

            Return iAccesosDirectos

        Catch excepcion As Exception
            Throw New AccesoDirectoNoEncontradoException(excepcion)
        Finally
            iDataSet = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
            iAccesoDirecto = Nothing
        End Try
    End Function

    Public Sub eliminarAccesosDirectosPorUsuario(eUsuario As Usuario)
        Dim iDataSet As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("AccesoDirecto")
            iGeneradorSql.agregarCondicionWhere("idUsuario=" & eUsuario.id)
    
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New AccesoDirectoNoEliminadoException(excepcion)
        Finally
            iDataSet = Nothing
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
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class