Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class Sexo
    Inherits Entidad

#Region "Constante"
    Public Const GENERICO As Integer = 1
    Public Const MASCULINO As Integer = 2
    Public Const FEMENINO As Integer = 3
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

    Public Function obtenerSexo() As Sexo
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSetSingleton As DataSet
        Dim iDataRow As DataRow
        Try

            iDataSetSingleton = SexoSingleton.getInstancia(False).dataSet

            If Not IsNothing(iDataSetSingleton) AndAlso iId <> Nothing Then
                Dim iBusqueda(0) As Object
                iBusqueda(0) = iId
                iDataRow = iDataSetSingleton.Tables("Sexo").Rows.Find(iBusqueda)

                If Not IsNothing(iDataRow) Then
                    id = iDataRow.Item("id").ToString
                    iDescripcion = FuncionComun.vacioSiEsNulo(iDataRow.Item("descripcion").ToString)
                    Return Me
                Else
                    Throw New SexoNoEncontradoException
                End If
            Else

                iConexion = obtenerConexion()
                iGeneradorSql.agregarColumna("id")
                iGeneradorSql.agregarColumna("descripcion")
                iGeneradorSql.agregarTabla("Sexo")
                iGeneradorSql.agregarCondicionWhere("id=" & id)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    iId = iDataReader.Item("id").ToString
                    iDescripcion = FuncionComun.vacioSiEsNulo(iDataReader.Item("descripcion").ToString)
                    Return Me
                Else
                    Throw New SexoNoEncontradoException()
                End If
            End If
        Catch excepcion As Exception
            Throw New SexoNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) AndAlso Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing

        End Try

    End Function

    Public Function obtenerSexos(Optional ByVal eSinSexoGenerico As Boolean = False) As IDataReader
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataReader As iDataReader
        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("Sexo")
            If eSinSexoGenerico Then iGeneradorSql.agregarCondicionWhere("id<>" & Sexo.GENERICO)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)
            Return iDataReader
        Catch excepcion As Exception
            Throw New SexoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.Destructor()
            iGeneradorSql = Nothing
        End Try
    End Function
    Public Function crear() As Sexo
        Dim iGeneradorSql As New GeneradorSql()
        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("Sexo")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsVacio(descripcion))

            id = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)
            Return Me
        Catch excepcion As Exception
            Throw New SexoNoEncontradoException(excepcion)
        End Try
    End Function

    Public Function obtenerSexosGrilla(Optional ByVal eSinSexoGenerico As Boolean = False) As DataSet
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            
            iGeneradorSql.agregarTabla("Sexo")
            If eSinSexoGenerico Then iGeneradorSql.agregarCondicionWhere("id<>" & Sexo.GENERICO)

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "sexos")
        Catch excepcion As Exception
            Throw New SexoNoEncontradoException(excepcion)
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
