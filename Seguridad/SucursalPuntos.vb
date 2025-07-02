Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.seguridad

Public Class SucursalPuntos

    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iCodigo As Integer
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
    Public Property codigo() As Integer
        Get
            Return iCodigo
        End Get
        Set(ByVal Value As Integer)
            iCodigo = Value
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

#End Region

#Region "Metodos"
    Private Sub validarCrear()

        Try
            If codigo = Nothing Then
                Throw New SucursalPuntosException("El codigo no puede ser nulo")
            End If
            If descripcion = Nothing Then
                Throw New SucursalPuntosException("La descripcion no puede ser nula")
            End If

            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader

            Try
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarTabla("sucursalPuntos")
                iGeneradorSql.agregarCondicionWhere("codigo=" & FuncionComun.nuloSiEsNothing(codigo))

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
                If iDataReader.Read Then
                    Throw New SucursalPuntosException("La sucursal de puntos ya existe")
                End If
                iDataReader.Close()

            Catch excepcion As ErrorConexionException
                Throw New SucursalPuntosException(excepcion)
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

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
        End Try
    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("sucursalPuntos")

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarModificar()
        Try

            If id = Nothing Then
                Throw New SucursalPuntosException("El id no puede ser nulo")
            End If

            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader

            Try
                iConexion = obtenerConexion()

                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarTabla("sucursalPuntos")
                iGeneradorSql.agregarCondicionWhere("id<>" & id)
                iGeneradorSql.agregarCondicionWhere("codigo=" & FuncionComun.nuloSiEsNothing(codigo))

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
                If iDataReader.Read Then
                    Throw New SucursalPuntosException("La sucursal de puntos ya existe")
                End If
                iDataReader.Close()

            Catch excepcion As ErrorConexionException
                Throw New SucursalPuntosException(excepcion)
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

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(iDescripcion))

            iGeneradorSql.agregarTabla("sucursalPuntos")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerSucursalPuntosGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("sucursalPuntos")

            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo=" & iCodigo)

            iGeneradorSql.agregarLimit("100")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "sucursalPuntos")

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerSucursalesPuntos() As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna(FuncionComun.sqlConcatenar("codigo,'-',descripcion") & " as descripcion")

            iGeneradorSql.agregarTabla("sucursalPuntos")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerSucursalPuntos() As SucursalPuntos
        Dim iDataReader As iDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("sucursalPuntos")

            iGeneradorSql.agregarCondicionWhere("id=" & id)


            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iCodigo = FuncionComun.nothingSiEsNulo(iDataReader.Item("Codigo"))
                iDescripcion = FuncionComun.nothingSiEsNulo(iDataReader.Item("Descripcion"))

                iDataReader.Close()

                Return Me
            Else
                Throw New SucursalPuntosException
            End If

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
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

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("sucursalPuntos")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SucursalPuntosException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
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
            Throw New SucursalPuntosException(exception)
        End Try
    End Sub

#End Region

End Class