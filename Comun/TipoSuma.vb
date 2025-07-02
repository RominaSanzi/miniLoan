Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones

Public MustInherit Class TipoSuma

#Region "Constantes"
    Public Const DEBE = 1
    Public Const HABER = 2
#End Region

#Region "Atributos"
    Private iId As Integer
    Private iDescripcion As String
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
    Public Shared Function obtenerTiposSuma() As Collection
        Dim iColecion As New Collection
        iColecion.Add("DEBE", TipoSuma.DEBE)
        iColecion.Add("HABER", TipoSuma.HABER)
        Return iColecion
    End Function

    MustOverride Function isDebe() As Boolean
    MustOverride Function isHaber() As Boolean

#End Region

End Class
