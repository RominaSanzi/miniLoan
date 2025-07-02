Public Class PDFCeldasVO

#Region "Atributos"
    Private iTexto As String
    Private iAlineacion As PDF.enumAlineacion
    Private iAlineacionCelda As PDF.enumAlineacion
    Private iTamañoFuente As Long
    Private iNegrita As Boolean
    Private iSubrayado As Boolean
    Private iInterlineado As Long
    Private iBordeInferior As Integer = 0
    Private iBordeSuperior As Integer = 0
    Private iBordeIzquierdo As Integer = 0
    Private iBordeDerecho As Integer = 0
    Private iDistancia As Integer = 0
    Private iBorde As Integer = 1
    Private iColorFuente As iTextSharp.text.BaseColor = iTextSharp.text.BaseColor.BLACK
    Private iColorFondo As iTextSharp.text.BaseColor = iTextSharp.text.BaseColor.WHITE
#End Region

#Region "Atributos"

    Public Property texto As String
        Get
            Return iTexto
        End Get
        Set(ByVal value As String)
            iTexto = value
        End Set
    End Property

    Public Property alineacion As PDF.enumAlineacion
        Get
            Return iAlineacion
        End Get
        Set(ByVal value As PDF.enumAlineacion)
            iAlineacion = value
        End Set
    End Property

    Public Property alineacionCelda As PDF.enumAlineacion
        Get
            Return iAlineacionCelda
        End Get
        Set(ByVal value As PDF.enumAlineacion)
            iAlineacionCelda = value
        End Set
    End Property

    Public Property tamañoFuente As Long
        Get
            Return iTamañoFuente
        End Get
        Set(ByVal value As Long)
            iTamañoFuente = value
        End Set
    End Property

    Public Property negrita As Boolean
        Get
            Return iNegrita
        End Get
        Set(ByVal value As Boolean)
            iNegrita = value
        End Set
    End Property

    Public Property subrayado As Boolean
        Get
            Return iSubrayado
        End Get
        Set(ByVal value As Boolean)
            iSubrayado = value
        End Set
    End Property

    Public Property interlineado As Boolean
        Get
            Return iInterlineado
        End Get
        Set(ByVal value As Boolean)
            iInterlineado = value
        End Set
    End Property

    Public Property bordeInferior As Integer
        Get
            Return iBordeInferior
        End Get
        Set(ByVal value As Integer)
            iBordeInferior = value
        End Set
    End Property

    Public Property bordeSuperior As Integer
        Get
            Return iBordeSuperior
        End Get
        Set(ByVal value As Integer)
            iBordeSuperior = value
        End Set
    End Property

    Public Property bordeDerecho As Integer
        Get
            Return iBordeDerecho
        End Get
        Set(ByVal value As Integer)
            iBordeDerecho = value
        End Set
    End Property

    Public Property bordeIzquierdo As Integer
        Get
            Return iBordeIzquierdo
        End Get
        Set(ByVal value As Integer)
            iBordeIzquierdo = value
        End Set
    End Property

    Public Property borde As Integer
        Get
            Return iBorde
        End Get
        Set(ByVal value As Integer)
            iBorde = value
        End Set
    End Property

    Public Property distancia As Integer
        Get
            Return iDistancia
        End Get
        Set(ByVal value As Integer)
            iDistancia = value
        End Set
    End Property

    Public Property colorFuente As iTextSharp.text.BaseColor
        Get
            Return iColorFuente
        End Get
        Set(ByVal value As iTextSharp.text.BaseColor)
            iColorFuente = value
        End Set
    End Property

    Public Property colorFondo As iTextSharp.text.BaseColor
        Get
            Return iColorFondo
        End Get
        Set(ByVal value As iTextSharp.text.BaseColor)
            iColorFondo = value
        End Set
    End Property
#End Region

End Class
