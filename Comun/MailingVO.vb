Imports di.financiera.entidades

Public Class MailingVO


#Region "Enumerado"
    Public Enum enumTipoMailing
        PREMORA = 1
        DEUDAVENCIDA = 2
        DEUDAVERAZ = 3
    End Enum
#End Region

#Region "Variables"
    Private iFechaVencimiento As Date
    Private iFechaVencimientoHasta As Date
    Private iTipoMailing As Integer
    Private iNombre As String
    Private iImporte As Double
    Private iEmail As String
    Private iSexo As Integer
    Private iCbu As String
#End Region

#Region "Propiedades"
    Public Property fechaVencimiento As Date
        Get
            Return iFechaVencimiento
        End Get
        Set(value As Date)
            iFechaVencimiento = value
        End Set
    End Property

    Public Property fechaVencimientoHasta As Date
        Get
            Return iFechaVencimientoHasta
        End Get
        Set(value As Date)
            iFechaVencimientoHasta = value
        End Set
    End Property

    Public Property sexo As Integer
        Get
            Return iSexo
        End Get
        Set(value As Integer)
            iSexo = value
        End Set
    End Property
    Public Property tipoMailing As Integer
        Get
            Return iTipoMailing
        End Get
        Set(value As Integer)
            iTipoMailing = value
        End Set
    End Property
    Public Property nombre As String
        Get
            Return iNombre
        End Get
        Set(value As String)
            iNombre = value
        End Set
    End Property

    Public Property cbu As String
        Get
            Return iCbu
        End Get
        Set(value As String)
            iCbu = value
        End Set
    End Property

    Public Property email As String
        Get
            Return iEmail
        End Get
        Set(value As String)
            iEmail = value
        End Set
    End Property

    Public Property importe As Double
        Get
            Return iImporte
        End Get
        Set(value As Double)
            iImporte = value
        End Set
    End Property
#End Region

End Class
