Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils


Public Class CentroCosto
    Inherits Entidad

#Region "Atributos"

    Private iId As Integer
    Private iCodigo As String
    Private iDescripcion As String
    Private iConexion As accesoDatos

#End Region

#Region "Propiedades"

    Public Property id() As Integer
        Get
            Return iId
        End Get
        Set(ByVal Value As Integer)
            iId = Value
        End Set
    End Property
    Public Property codigo() As String
        Get
            Return iCodigo
        End Get
        Set(ByVal Value As String)
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

    Public Function obtenerCentroCosto() As CentroCosto
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("CentroCosto")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & iId)
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion like '%" & iDescripcion & "%'")
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo='" & iCodigo & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iCodigo = iDataReader.Item("codigo").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                Return Me
            Else
                Throw New CentroCostoNoEncontradoException
            End If
        Catch CentroCostoNoEncontradoException As CentroCostoNoEncontradoException
            Throw CentroCostoNoEncontradoException
        Catch exception As Exception
            Throw New CentroCostoNoEncontradoException(exception)
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

    Public Function obtenerCentroCostos(Optional ByVal eSinCentroCostoGenerico As Boolean = False) As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("CentroCosto")
            iGeneradorSql.agregarOrden("descripcion")

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New CentroCostoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarCrear()
        If codigo = Nothing Then
            Throw New CentroCostoNoCreadoException("El código no puede ser nulo")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarTabla("CentroCosto")
            iGeneradorSql.agregarCondicionWhere("codigo='" & codigo & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New CentroCostoNoCreadoException("La CentroCosto ya existe en la base de datos")
            End If
            iDataReader.Close()

        Catch excepcion As ErrorConexionException
            Throw New CentroCostoNoCreadoException(excepcion)
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

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarCrear()


            iGeneradorSql.agregarTabla("CentroCosto")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))

            If id <> Nothing Then
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarValue(id)
                iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Else
                id = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            End If

        Catch excepcion As Exception
            Throw New CentroCostoNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub obtenerUltimoId()
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("CentroCosto")
            iGeneradorSql.agregarColumna("max(id) as id")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iId = CLng(iDataReader.Item("id"))
            Else
                Throw New CentroCostoNoCreadoException()
            End If

        Catch excepcion As Exception
            Throw New CentroCostoNoCreadoException(excepcion)
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

    Private Sub validarModificar()
        If codigo = Nothing OrElse codigo = "" Then
            Throw New CentroCostoNoModificadoException("El código no puede ser nulo")
        End If

        If id = Nothing Then
            Throw New CentroCostoNoModificadoException("La CentroCosto es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("CentroCosto")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New CentroCostoNoModificadoException("La CentroCosto es inexistente")
            End If
        Catch excepcion As Exception
            Throw New CentroCostoNoModificadoException(excepcion)
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

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarTabla("CentroCosto")
            iGeneradorSql.agregarSet("codigo='" & codigo & "'")
            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New CentroCostoNoModificadoException(excepcion)
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
            Throw New CentroCostoNoEliminadoException("La CentroCosto es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("idCentroCosto")
            iGeneradorSql.agregarTabla("comercio")
            iGeneradorSql.agregarCondicionWhere("idCentroCosto=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New CentroCostoNoEliminadoException("No se puede eliminar la CentroCosto porque se relaciona con un comercio")
            End If
        Catch excepcion As Exception
            Throw New CentroCostoNoEliminadoException(excepcion)
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
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("CentroCosto")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New CentroCostoNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerCentroCostosGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("CentroCosto")

            If codigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo='" & codigo & "'")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "CentroCosto")

        Catch excepcion As Exception
            Throw New CentroCostoNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

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
