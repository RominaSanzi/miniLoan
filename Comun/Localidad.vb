Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades

Public Class Localidad
    Inherits Entidad

#Region "Constante"
    Public Const LOCALIDADGENERICA As Integer = 1
    Public Const CAPITALFEDERAL As Integer = 5001
#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iCodigoPostal As String
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

    Public Property codigoPostal() As String
        Get
            Return iCodigoPostal
        End Get
        Set(ByVal Value As String)
            iCodigoPostal = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function obtenerListaLocalidad() As List(Of Localidad)
        Dim iLista As New List(Of Localidad)
        Dim iLocalidad As Localidad
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataset As DataSet

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("Id")
            iGeneradorSql.agregarColumna("Descripcion")
            iGeneradorSql.agregarColumna("CodigoPostal")
            iGeneradorSql.agregarTabla("Localidad")
            iDataset = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Localidades")

            For Each fila As DataRow In iDataset.Tables("Localidades").Rows
                iLocalidad = New Localidad
                iLocalidad.id = fila("Id")
                iLocalidad.descripcion = FuncionComun.vacioSiEsNulo(fila("Descripcion"))
                iLocalidad.codigoPostal = FuncionComun.vacioSiEsNulo(fila("CodigoPostal"))
                iLista.Add(iLocalidad)
            Next

            Return iLista

        Catch ex As Exception
            Throw New Exception("Error al obtener las localidades. " & ex.Message)
        End Try
    End Function
    Public Function obtenerLocalidad() As Localidad
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try

            iConexion = obtenerConexion()


            iGeneradorSql.agregarColumna("l.id")
            iGeneradorSql.agregarColumna("l.descripcion")
            iGeneradorSql.agregarColumna("l.CodigoPostal")

            iGeneradorSql.agregarTabla("localidad l")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.id=" & id)
            If iCodigoPostal <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.codigoPostal='" & codigoPostal & "'")
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("l.descripcion like '%" & descripcion & "%'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iDescripcion = FuncionComun.vacioSiEsNulo(iDataReader.Item("descripcion").ToString)
                iCodigoPostal = FuncionComun.vacioSiEsNulo(iDataReader.Item("codigoPostal").ToString)

                iDataReader.Close()

                Return Me
            Else
                Throw New LocalidadNoEncontradaException
            End If
        Catch LocalidadNoEncontradaException As LocalidadNoEncontradaException
            Throw LocalidadNoEncontradaException
        Catch excepcion As Exception
            Throw New LocalidadNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) AndAlso Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try

    End Function

    Public Function obtenerLocalidades() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("codigoPostal")

            iGeneradorSql.agregarTabla("localidad")

            If iCodigoPostal <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigoPostal=" & FuncionComun.nuloSiEsNothing(codigoPostal))

            iGeneradorSql.agregarOrden("descripcion")
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New LocalidadNoEncontradaException(excepcion)
        End Try
    End Function

    Public Function obtenerLocalidadesGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("l.id")
            iGeneradorSql.agregarColumna("l.descripcion")
            iGeneradorSql.agregarColumna("l.codigoPostal")
            iGeneradorSql.agregarTabla("localidad l")

            If iCodigoPostal <> "" Then iGeneradorSql.agregarCondicionWhere("l.codigoPostal=" & codigoPostal)
            If iDescripcion <> "" Then iGeneradorSql.agregarCondicionWhere("l.descripcion like'" & iDescripcion & "%'")

            iGeneradorSql.agregarOrden("descripcion")
            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Localidades")

        Catch excepcion As Exception
            Throw New LocalidadNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Function

    Public Function obtenerCodigoPostal() As String
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("codigoPostal")
            iGeneradorSql.agregarTabla("localidad")
            iGeneradorSql.agregarCondicionWhere("id=" & iId)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Return FuncionComun.vacioSiEsNulo(iDataReader.Item("codigoPostal"))
            Else
                Return Nothing
            End If

        Catch excepcion As Exception
            Throw New LocalidadNoEncontradaException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Function

    Private Sub validarCrear()

        If iDescripcion = Nothing Then Throw New LocalidadNoCreadaException("La descripcion no puede ser nula")

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("localidad")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New LocalidadNoCreadaException("La localidad ya existe")
            End If

        Catch excepcion As Exception
            Throw New LocalidadNoCreadaException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try

    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("CodigoPostal")

            iGeneradorSql.agregarValue(iId)
            iGeneradorSql.agregarValue("'" & iDescripcion & "'")
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iCodigoPostal))

            iGeneradorSql.agregarTabla("localidad")

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch exception As Exception
            Throw New LocalidadNoCreadaException(exception)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try

    End Sub

    Private Sub validarModificar()
        If iId = Nothing Then Throw New LocalidadNoModificadaException("No se envio el identificador para la modificacion")
        If iDescripcion = Nothing Then Throw New LocalidadNoModificadaException("La descripcion no puede ser nula")

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("localidad")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("descripcion='" & iDescripcion & "'")

            iGeneradorSql.agregarCondicionWhere("id<>" & iId)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New LocalidadNoModificadaException("El partido ya existe")
            End If

        Catch excepcion As Exception
            Throw New LocalidadNoModificadaException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarTabla("localidad")

            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarSet("codigoPostal=" & FuncionComun.nuloSiEsNothing(codigoPostal))

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New BuzonNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try

    End Sub

    Private Sub validarEliminar()
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()


            iGeneradorSql.agregarTabla("Domicilio")
            iGeneradorSql.agregarColumna("id")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("idLocalidad=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If Not iDataReader.Read Then
                Throw New LocalidadNoEliminadaException("La Localidad esta utilizada en un domicilio")
            End If

        Catch excepcion As Exception
            Throw New LocalidadNoEliminadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
        End Try
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("localidad")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New LocalidadNoEliminadaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub
#End Region
End Class