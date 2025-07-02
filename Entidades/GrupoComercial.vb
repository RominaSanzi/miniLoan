Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class GrupoComercial
    Inherits Entidad

#Region "Constantes"
    Public Const GENERICA = 1
#End Region

#Region "Atributos"

    Private iId As Integer
    Private iCodigo As String
    Private iDescripcion As String
    Private iEnviaMailComercio As Boolean
    Private iLeyendaMailComercio As String

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
    Public Property enviaMailComercio() As Boolean
        Get
            Return iEnviaMailComercio
        End Get
        Set(value As Boolean)
            iEnviaMailComercio = value
        End Set
    End Property
    Public Property leyendaMailComercio() As String
        Get
            Return iLeyendaMailComercio
        End Get
        Set(value As String)
            iLeyendaMailComercio = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Public Function obtenerGrupoComercial() As GrupoComercial
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("enviaMailComercio")
            iGeneradorSql.agregarColumna("leyendaMailComercio")

            iGeneradorSql.agregarTabla("GrupoComercial")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & iId)
            If iDescripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion like '%" & iDescripcion & "%'")
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo='" & iCodigo & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iCodigo = iDataReader.Item("codigo").ToString
                iDescripcion = iDataReader.Item("descripcion").ToString
                iEnviaMailComercio = FuncionComun.byteBoolean(FuncionComun.ceroSiEsNulo(iDataReader.Item("enviaMailComercio")))
                iLeyendaMailComercio = iDataReader.Item("leyendaMailComercio").ToString

                Return Me
            Else
                Throw New GrupoComercialNoEncontradoException
            End If
        Catch GrupoComercialNoEncontradaException As GrupoComercialNoEncontradoException
            Throw GrupoComercialNoEncontradaException
        Catch exception As Exception
            Throw New GrupoComercialNoEncontradoException(exception)
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

    Public Function obtenerGruposComerciales(Optional ByVal eSinGrupoComercialGenerico As Boolean = False) As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarOrden("descripcion")
            If eSinGrupoComercialGenerico Then iGeneradorSql.agregarCondicionWhere("id<>" & GrupoComercial.GENERICA)

            Return iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New GrupoComercialNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Private Sub validarCrear()
        If codigo = Nothing Then
            Throw New GrupoComercialNoCreadoException("El código no puede ser nulo")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarCondicionWhere("codigo='" & codigo & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New GrupoComercialNoCreadoException("El ya existe un Grupo comercial con el mismo codigo en la base de datos")
            End If
            iDataReader.Close()

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New GrupoComercialNoCreadoException("El ya existe un Grupo comercial con la misma descripcion en la base de datos")
            End If
            iDataReader.Close()

        Catch excepcion As ErrorConexionException
            Throw New GrupoComercialNoCreadoException(excepcion)
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

            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("enviaMailComercio")
            iGeneradorSql.agregarColumna("leyendaMailComercio")
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(enviaMailComercio))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(leyendaMailComercio))

            iConexion.ejecutar(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

            obtenerUltimoId()

        Catch excepcion As Exception
            Throw New GrupoComercialNoCreadoException(excepcion)
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

            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarColumna("max(id) as id")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                iId = CLng(iDataReader.Item("id"))
            Else
                Throw New GrupoComercialNoCreadoException()
            End If

        Catch excepcion As Exception
            Throw New GrupoComercialNoCreadoException(excepcion)
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
            Throw New GrupoComercialNoModificadoException("El código no puede ser nulo")
        End If

        If id = Nothing Then
            Throw New GrupoComercialNoModificadoException("El Grupo coemrcial es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If Not iDataReader.Read Then
                Throw New GrupoComercialNoModificadoException("El Grupo Comercial es inexistente")
            End If
            iDataReader.Close()

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarCondicionWhere("codigo='" & codigo & "'")
            iGeneradorSql.agregarCondicionWhere("id<>" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New GrupoComercialNoCreadoException("El ya existe un Grupo comercial con el mismo codigo en la base de datos")
            End If
            iDataReader.Close()

            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarCondicionWhere("descripcion='" & descripcion & "'")
            iGeneradorSql.agregarCondicionWhere("id<>" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New GrupoComercialNoCreadoException("El ya existe un Grupo comercial con la misma descripcion en la base de datos")
            End If
            iDataReader.Close()


        Catch excepcion As Exception
            Throw New GrupoComercialNoModificadoException(excepcion)
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

            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarSet("codigo='" & codigo & "'")
            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarSet("enviaMailComercio=" & FuncionComun.booleanByte(enviaMailComercio))
            iGeneradorSql.agregarSet("leyendaMailComercio=" & FuncionComun.nuloSiEsNothing(leyendaMailComercio))
            iGeneradorSql.agregarCondicionWhere("id = " & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New GrupoComercialNoModificadoException(excepcion)
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
            Throw New GrupoComercialNoEliminadoException("El grupo comercial es inexistente")
        End If

        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("idGrupoComercial")
            iGeneradorSql.agregarTabla("comercio")
            iGeneradorSql.agregarCondicionWhere("idGrupoComercial=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            If iDataReader.Read Then
                Throw New GrupoComercialNoEliminadoException("No se puede eliminar el grupo comercial porque se relaciona con un comercio")
            End If
        Catch excepcion As Exception
            Throw New GrupoComercialNoEliminadoException(excepcion)
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

            iGeneradorSql.agregarTabla("GrupoComercial")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New GrupoComercialNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerGruposComercialesGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("GrupoComercial")

            If codigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo='" & codigo & "'")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "GrupoComercial")

        Catch excepcion As Exception
            Throw New GrupoComercialNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
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

        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class
