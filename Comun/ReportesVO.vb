Public Class ReportesVO
#Region "Enumerado"
    Public Enum EnumAsignados
        SI = 1
        NO = 2
        TODOS = 3
    End Enum
#End Region

#Region "Variables"
    Private iTituloReporte As String
    Private iNombrefantasia As String
    Private iFechaEmision As Date
    Private iColeccionParametros As Collection
    Private iColeccionDatos As Collection
#End Region

#Region "Atributos"
    Public Property tituloReporte() As String
        Get
            Return iTituloReporte
        End Get
        Set(ByVal Value As String)
            iTituloReporte = Value
        End Set
    End Property
    Public Property nombrefantasia() As String
        Get
            Return iNombrefantasia
        End Get
        Set(ByVal Value As String)
            iNombrefantasia = Value
        End Set
    End Property
    Public Property fechaEmision() As Date
        Get
            Return iFechaEmision
        End Get
        Set(ByVal Value As Date)
            iFechaEmision = Value
        End Set
    End Property
    Public Property coleccionParametros() As Collection
        Get
            Return iColeccionParametros
        End Get
        Set(ByVal Value As Collection)
            iColeccionParametros = Value
        End Set
    End Property
    Public Property coleccionDatos() As Collection
        Get
            Return iColeccionDatos
        End Get
        Set(ByVal Value As Collection)
            iColeccionDatos = Value
        End Set
    End Property

#End Region
End Class
