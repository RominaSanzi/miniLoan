Imports System.Collections.Generic

Public Class PDFColumnasVO

#Region "Atributos"
    Private iEncabezado As Boolean
    Private iColorFondo As iTextSharp.text.BaseColor
    Private iCantidadColumnas As Integer
    Private iTamanio As Single()
    Private iPDFCeldasVO As List(Of PDFCeldasVO)
    Private iBorde As Integer
    Private iDistanciaSuperior As Single
#End Region

#Region "Atributos"
    Public Property encabezado As Boolean
        Get
            Return iEncabezado
        End Get
        Set(ByVal value As Boolean)
            iEncabezado = value
        End Set
    End Property
    Public Property cantidadColumnas As Integer
        Get
            Return iCantidadColumnas
        End Get
        Set(ByVal value As Integer)
            iCantidadColumnas = value
        End Set
    End Property
    Public Property tamanio As Single()
        Get
            Return iTamanio
        End Get
        Set(ByVal value As Single())
            iTamanio = value
        End Set
    End Property
    Public Property PDFCeldasVO As List(Of PDFCeldasVO)
        Get
            Return iPDFCeldasVO
        End Get
        Set(ByVal value As List(Of PDFCeldasVO))
            iPDFCeldasVO = value
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
    Public Property borde As Integer
        Get
            Return iBorde
        End Get
        Set(ByVal value As Integer)
            iBorde = value
        End Set
    End Property

    Public Property distanciaSuperior As Single
        Get
            Return iDistanciaSuperior
        End Get
        Set(ByVal value As Single)
            iDistanciaSuperior = value
        End Set
    End Property
#End Region

End Class
