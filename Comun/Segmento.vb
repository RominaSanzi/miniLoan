Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades


Public Class Segmento

    Inherits Entidad

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

    Public Function obtenerSegmentos() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("segmento")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New SegmentoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Private Sub validarCrear()
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try

            If descripcion = Nothing Then
                Throw New SegmentoNoCreadoException("La descripcion no puede ser nula")
            End If

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("segmento")
            iGeneradorSql.agregarCondicionWhere("descripcion=" & iDescripcion)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SegmentoNoCreadoException("Ya existe el segmento")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New SegmentoNoCreadoException(excepcion)
        Finally
        End Try
    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Segmento")

            iGeneradorSql.agregarColumna("descripcion")


            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SegmentoNoCreadoException(excepcion)
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

            If descripcion = Nothing Then
                Throw New SegmentoNoModificadoException("La descripcion no puede ser nula")
            End If


        Catch excepcion As Exception
            Throw New SegmentoNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))


            iGeneradorSql.agregarTabla("Segmento")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SegmentoNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerSegmentoGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("Segmento")
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion like '" & descripcion & "%'")


            iGeneradorSql.agregarLimit("100")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Segmento")
        Catch excepcion As Exception
            Throw New SegmentoNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerSegmento() As Segmento
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")

            iGeneradorSql.agregarTabla("Segmento")

            iGeneradorSql.agregarCondicionWhere("id=" & FuncionComun.nuloSiEsNothing(id))


            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iDescripcion = FuncionComun.nothingSiEsNulo(iDataReader.Item("Descripcion"))

                iDataReader.Close()

                Return Me
            Else
                Throw New SegmentoNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New SegmentoNoEncontradoException(excepcion)
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


    Private Sub validarEliminar()
        If id = Nothing Then
            Throw New SegmentoNoEliminadoException("El Segmento a eliminar es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As iDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("idSegmento")
            iGeneradorSql.agregarTabla("unidaddenegocios")
            iGeneradorSql.agregarCondicionWhere("idSegmento=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New SegmentoNoEliminadoException("No se puede eliminar el Segmento porque se relaciona con una unidad de negocios")
            End If

            iDataReader.Close()

        Catch excepcion As Exception
            Throw New SegmentoNoEliminadoException(excepcion)
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

    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("Segmento")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New SegmentoNoEliminadoException(excepcion)
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
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class