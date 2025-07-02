Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public MustInherit Class Estado

#Region "Constantes"
    Public Const ALTA As Integer = 1
    Public Const BAJA As Integer = 2
#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
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

    MustOverride Function isAlta() As Boolean
    MustOverride Function isBaja() As Boolean

    Public Shared Function obtenerEstados() As Collection
        Dim iColeccion As New Collection()
        iColeccion.Add("ALTA", ALTA)
        iColeccion.Add("BAJA", BAJA)
        Return iColeccion
    End Function
#End Region

End Class




