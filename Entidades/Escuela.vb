Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Public Class Escuela
    Inherits Entidad
#Region "Variables"
    Private iId As Integer
    Private iNombre As String
    Private iDomicilio As String
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
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property domicilio() As String
        Get
            Return iDomicilio
        End Get
        Set(ByVal Value As String)
            iDomicilio = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function obtenerListaEscuelas() As List(Of Escuela)
        Dim iLista As New List(Of Escuela)
        Dim iEscuela As Escuela
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataset As DataSet

        Try
            iConexion = obtenerConexion()
            iGeneradorSql.agregarColumna("Id")
            iGeneradorSql.agregarColumna("Nombre")
            iGeneradorSql.agregarColumna("idDomicilio")
            iGeneradorSql.agregarTabla("Escuela")
            iDataset = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Escuelas")

            For Each fila As DataRow In iDataset.Tables("Escuelas").Rows
                iEscuela = New Escuela
                iEscuela.id = fila("Id")
                iEscuela.nombre = FuncionComun.vacioSiEsNulo(fila("Nombre"))
                iEscuela.domicilio = FuncionComun.ceroSiEsNulo(fila("idDomicilio"))
                iLista.Add(iEscuela)
            Next

            Return iLista

        Catch ex As Exception
            Throw New Exception("Error al obtener las escuelas. " & ex.Message)
        End Try
    End Function
#End Region
End Class
