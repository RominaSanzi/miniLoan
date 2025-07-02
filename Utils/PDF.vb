Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Web
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Security
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.AcroFields

Public Class PDF

#Region "Enumerados"
    Public Enum enumAlineacion
        DERECHA
        IZQUIERDA
        CENTRADA
        JUSTIFICADA
    End Enum

#End Region

#Region "Atributos"
    Private iDocumento As Document
    Private iPdfWriter As PdfWriter
    Private iMargenIzquierdo As Double
    Private iMargenSuperior As Double
    Private iReader As PdfReader
    Private iPdfStamper As PdfStamper
#End Region

#Region "Atributos"
    Public Property margenIzquierdo As Double
        Get
            Return iMargenIzquierdo
        End Get
        Set(value As Double)
            iMargenIzquierdo = value
        End Set
    End Property
    Public Property margenSuperior As Double
        Get
            Return iMargenSuperior
        End Get
        Set(value As Double)
            iMargenSuperior = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Sub abrirTemplate(ByVal eArchivoTemplate As String, ByVal Stream As Stream)
        Try
            iReader = New PdfReader(eArchivoTemplate)
            iPdfStamper = New PdfStamper(iReader, Stream)
        Catch Exception As Exception
            Throw Exception
        End Try
    End Sub

    Public Sub agregarTemplate(ePathArchivosTemplate As String(), ePathArchivoFinal As String, Optional ByVal eMargenLeft As Single = 0, Optional ByVal eMargenRight As Single = 0, Optional ByVal eMargenTop As Single = 0, Optional ByVal eMargenBotom As Single = 0, Optional ByVal eRotacion As Boolean = False)
        Dim iPdfContentByte As PdfContentByte
        Dim iPagina As PdfImportedPage
        Dim iPdfWriter As PdfWriter
        Dim i As Integer
        Try

            iReader = New PdfReader(ePathArchivosTemplate(0))
            'Leo el archivo temporal para generar el nuevo
            iDocumento = New Document(iReader.GetPageSize(1), eMargenLeft, eMargenRight, eMargenTop, eMargenBotom)
            iReader.Close()
            If eRotacion Then iDocumento.SetPageSize(PageSize.A4.Rotate)
            iPdfWriter = PdfWriter.GetInstance(iDocumento, New FileStream(ePathArchivoFinal, IIf(File.Exists(ePathArchivoFinal), FileMode.Open, FileMode.Create)))
            iDocumento.Open()
            registrarFuentesAUtilizar()

            iPdfContentByte = iPdfWriter.DirectContent

            For Each iPathArchivoTemplate As String In ePathArchivosTemplate
                iReader = New PdfReader(iPathArchivoTemplate)
                For i = 1 To iReader.NumberOfPages
                    iPagina = iPdfWriter.GetImportedPage(iReader, i)
                    iDocumento.NewPage()
                    iPdfContentByte.AddTemplate(iPagina, 0, 0)
                Next
            Next

            'Cierro el documento
            iReader.Close()
            iPdfWriter.Flush()
            iDocumento.Close()

            'Borro los archivos anteriores
            For Each iPathArchivoTemplate As String In ePathArchivosTemplate
                File.Delete(iPathArchivoTemplate)
            Next

        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub reemplazarCampoTemplate(ByVal eNombreParametro As String, ByVal eValorParametro As String)
        Try
            iPdfStamper.AcroFields.SetField(eNombreParametro, eValorParametro)

        Catch Exception As Exception
            FuncionComun.loguearFTP(eNombreParametro & " - " & eValorParametro)
            Throw Exception
        End Try
    End Sub

    Public Sub agregarLineaCodigoBarraTemplate(ByVal eLinea As String, ByVal ePosX As Long, ByVal ePosY As Long, ByVal eTamañoFuente As Long)
        Dim iImagen As iTextSharp.text.Image
        Dim iBarCod As Barcode128
        'convierto los milimetros a pixeles
        ePosX = ePosX * 2.8335
        ePosY = ePosY * 2.8335

        '  iPdfContentByte = iPdfStamper.GetUnderContent(1)
        iBarCod = New Barcode128
        iBarCod.Code = eLinea
        iBarCod.CodeType = Barcode128.CODE128
        iImagen = iBarCod.CreateImageWithBarcode(iPdfStamper.GetOverContent(1), iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.BLACK)
        iImagen.SetAbsolutePosition(ePosX + iMargenIzquierdo, (iTextSharp.text.PageSize.A4.Height - (ePosY + iMargenSuperior)))
        iPdfStamper.GetOverContent(1).AddImage(iImagen)

    End Sub

    Public Sub agregarLineaCodigoBarraTemplate(ByVal eLinea As String, ByVal eTamañoFuente As Long, eNombreParametro As String)
        Dim iImagen As iTextSharp.text.Image
        Dim iBarCod As Barcode128
        Dim iPosicion As IList(Of FieldPosition)
        Dim iNumeroPagina As Integer
        Dim iPosicionX As Integer
        Dim iPosicionY As Integer

        iPosicion = iPdfStamper.AcroFields.GetFieldPositions(eNombreParametro)
        If Not IsNothing(iPosicion) Then
            For Each item As FieldPosition In iPosicion
                iNumeroPagina = item.page
                iPosicionX = item.position.Left
                iPosicionY = item.position.Bottom
                eTamañoFuente = item.position.Height
                'iPdfContentByte = iPdfStamper.GetUnderContent(1)
                iBarCod = New Barcode128
                iBarCod.Code = eLinea
                iBarCod.CodeType = Barcode128.CODE128
                iBarCod.BarHeight = eTamañoFuente
                iImagen = iBarCod.CreateImageWithBarcode(iPdfStamper.GetOverContent(1), iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.BLACK)
                iImagen.SetAbsolutePosition(iPosicionX, iPosicionY)
                iPdfStamper.GetOverContent(iNumeroPagina).AddImage(iImagen)
            Next

        End If
    End Sub

    Public Sub cerrarTemplate()
        Try
            iPdfStamper.FormFlattening = True
            iPdfStamper.FreeTextFlattening = True
            iPdfStamper.Writer.CloseStream = False
            iPdfStamper.Close()
            iReader.Close()
        Catch Exception As Exception
            Throw Exception
        Finally
            iPdfStamper = Nothing
            iReader = Nothing
        End Try
    End Sub

    Public Sub crearDocumento(ePathArchivo As String, Optional ByVal eMargenLeft As Single = 0, Optional ByVal eMargenRight As Single = 0, Optional ByVal eMargenTop As Single = 0, Optional ByVal eMargenBotom As Single = 0, Optional ByVal eRotacion As Boolean = False, Optional ByVal eFormatoHojaA5 As Boolean = False)
        Dim iTipoFormatoHoja As Rectangle
        Try
            'Se setea el valor del tipo de hoja que se va a utilizar acorde a la parametria
            If Not eFormatoHojaA5 Then
                iTipoFormatoHoja = iTextSharp.text.PageSize.A4
            Else
                iTipoFormatoHoja = iTextSharp.text.PageSize.A5
            End If

            iDocumento = New iTextSharp.text.Document(iTipoFormatoHoja, eMargenLeft, eMargenRight, eMargenTop, eMargenBotom)
            If eRotacion Then iDocumento.SetPageSize(iTipoFormatoHoja.Rotate())
            iPdfWriter = PdfWriter.GetInstance(iDocumento, New FileStream(ePathArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
            iDocumento.Open()

        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub abrirDocumento(ePathArchivo As String, Optional ByVal eMargenLeft As Single = 0, Optional ByVal eMargenRight As Single = 0, Optional ByVal eMargenTop As Single = 0, Optional ByVal eMargenBotom As Single = 0, Optional ByVal eRotacion As Boolean = False)
        Dim iPdfContentByte As PdfContentByte
        Dim iPagina As PdfImportedPage
        Dim i As Integer
        Try

            Dim iPathArchivoSolicitud As String
            iPathArchivoSolicitud = ePathArchivo & "X"
            File.Copy(ePathArchivo, iPathArchivoSolicitud)
            File.Delete(ePathArchivo)

            iDocumento = New Document(PageSize.A4, eMargenLeft, eMargenRight, eMargenTop, eMargenBotom)
            If eRotacion Then iDocumento.SetPageSize(PageSize.A4.Rotate())
            iPdfWriter = PdfWriter.GetInstance(iDocumento, New FileStream(ePathArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
            iDocumento.Open()

            iPdfContentByte = iPdfWriter.DirectContent

            iReader = New PdfReader(iPathArchivoSolicitud)

            For i = 1 To iReader.NumberOfPages
                iPagina = iPdfWriter.GetImportedPage(iReader, i)
                iDocumento.NewPage()
                iPdfContentByte.AddTemplate(iPagina, 0, 0)
            Next


        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub crearDocumentoTamañoLegal(ByVal ePathArchivo As String)
        Try
            iDocumento = New iTextSharp.text.Document(iTextSharp.text.PageSize.LEGAL, 0, 0, 0, 0)
            iPdfWriter = PdfWriter.GetInstance(iDocumento, New FileStream(ePathArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
            iDocumento.Open()

        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub cerrarDocumento()

        Try
            iPdfWriter.Flush()
            iDocumento.Close()

        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub nuevaHoja()
        Try
            iDocumento.NewPage()
        Catch exception As Exception
            Throw exception
        End Try
    End Sub

    Public Sub registrarFuentesAUtilizar()
        iTextSharp.text.FontFactory.Register("c:\Windows\Fonts\arial.ttf", "Arial")
        iTextSharp.text.FontFactory.Register("c:\Windows\Fonts\ARIALBD.TTF", "Arial Bold")
    End Sub

    Public Sub registrarFuenteAUtilizar(ByVal eNombreFuente As String)
        iTextSharp.text.FontFactory.Register("c:\Windows\Fonts\" & eNombreFuente & ".ttf", eNombreFuente)
        iTextSharp.text.FontFactory.Register("c:\Windows\Fonts\" & eNombreFuente & "BD.TTF", eNombreFuente & " Bold")
    End Sub

    Public Sub agregarLinea(ByVal eLinea As String, ByVal ePosX As Long, ByVal ePosY As Long, ByVal eTamañoFuente As Long, ByVal eNegrita As Boolean, Optional ByVal eSubrayado As Boolean = False, Optional ByVal eRotacion As Integer = 0, Optional ByVal eNombreFuente As String = "Arial")
        Dim iPdfContentByte As PdfContentByte
        Dim iFuente As BaseFont

        'convierto los milimetros a pixeles
        ePosX = ePosX * 2.8335
        ePosY = ePosY * 2.8335

        iPdfContentByte = iPdfWriter.DirectContent
        iPdfContentByte.BeginText()
        iFuente = iTextSharp.text.FontFactory.GetFont(eNombreFuente & IIf(eNegrita, " Bold", ""), eTamañoFuente, IIf(eSubrayado, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.Font.NORMAL)).BaseFont
        iPdfContentByte.SetFontAndSize(iFuente, eTamañoFuente)
        iPdfContentByte.SetColorFill(iTextSharp.text.BaseColor.BLACK)
        iPdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, eLinea, ePosX + iMargenIzquierdo, (iTextSharp.text.PageSize.A4.Height - (ePosY + iMargenSuperior)), eRotacion)
        iPdfContentByte.EndText()
    End Sub

    Public Sub agregarLineaCodigoBarra(ByVal eLinea As String, ByVal ePosX As Long, ByVal ePosY As Long, ByVal eTamañoFuente As Long)
        Dim iPdfContentByte As PdfContentByte
        Dim iImagen As iTextSharp.text.Image
        Dim iBarCod As Barcode128

        'convierto los milimetros a pixeles
        ePosX = ePosX * 2.8335
        ePosY = ePosY * 2.8335


        iPdfContentByte = iPdfWriter.DirectContent
        iBarCod = New Barcode128
        iBarCod.Code = eLinea
        iBarCod.CodeType = Barcode128.CODE128
        iImagen = iBarCod.CreateImageWithBarcode(iPdfContentByte, iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.BLACK)
        iImagen.SetAbsolutePosition(ePosX + iMargenIzquierdo, (iTextSharp.text.PageSize.A4.Height - (ePosY + iMargenSuperior)))
        iPdfContentByte.AddImage(iImagen)

    End Sub

    Public Sub agregarImagen(ePathArchivoImagen As String, ByVal ePosX As Long, ByVal ePosY As Long, Optional ByVal ePorcentaje As Long = Nothing)

        Dim iPdfContentByte As PdfContentByte
        Dim iImagen As iTextSharp.text.Image
        Dim iHeight, iWidth As Single

        'convierto los milimetros a pixeles
        ePosX = ePosX * 2.8335
        ePosY = ePosY * 2.8335

        iPdfContentByte = iPdfWriter.DirectContent
        iImagen = iTextSharp.text.Image.GetInstance(ePathArchivoImagen)

        If ePorcentaje <> Nothing Then
            iHeight = (iImagen.Height * ePorcentaje) / 100
            iWidth = (iImagen.Width * ePorcentaje) / 100

            iImagen.ScaleAbsoluteHeight(iHeight)
            iImagen.ScaleAbsoluteWidth(iWidth)
        End If

        iImagen.SetAbsolutePosition(ePosX + iMargenIzquierdo, (iTextSharp.text.PageSize.A4.Height - (ePosY + iMargenSuperior)))

        iPdfContentByte.AddImage(iImagen)

    End Sub

    Public Sub agregarImagen(ePathArchivoImagen As String, ByVal eNombreCampo As String)
        Dim iPdfContentByte As PdfContentByte
        Dim iImagen As iTextSharp.text.Image
        Dim iPosicion As IList(Of FieldPosition)
        Dim iNumeroPagina As Integer
        Dim iPosicionX As Integer
        Dim iPosicionY As Integer
        Dim iAlto As Integer
        Dim iAncho As Integer

        iPosicion = iPdfStamper.AcroFields.GetFieldPositions(eNombreCampo)

        If Not IsNothing(iPosicion) Then
            For Each item As FieldPosition In iPosicion

                iNumeroPagina = item.page
                iPosicionX = item.position.Left
                iPosicionY = item.position.Bottom

                iAlto = item.position.Height
                iAncho = item.position.Width

                '  iPdfContentByte = iPdfWriter.DirectContent
                iImagen = iTextSharp.text.Image.GetInstance(ePathArchivoImagen)

                ' iImagen.ScaleAbsolute(iAncho, iAlto)
                iImagen.ScaleToFit(iAncho, iAlto)

                iImagen.SetAbsolutePosition(iPosicionX, iPosicionY)
                iPdfStamper.GetOverContent(iNumeroPagina).AddImage(iImagen)
            Next
        End If
    End Sub

    Public Sub agregarImagenCentrada(ePathArchivoImagen As String, Optional ByVal eNuevoHeight As Single = Nothing, Optional ByVal eNuevoWidth As Single = Nothing)

        Dim iPdfContentByte As PdfContentByte
        Dim iImagen As iTextSharp.text.Image

        iPdfContentByte = iPdfWriter.DirectContent
        iImagen = iTextSharp.text.Image.GetInstance(ePathArchivoImagen)

        If eNuevoHeight <> Nothing AndAlso eNuevoWidth <> Nothing Then
            'No hago nada
        ElseIf eNuevoHeight = Nothing AndAlso eNuevoWidth <> Nothing Then
            eNuevoHeight = iImagen.Height
        ElseIf eNuevoHeight <> Nothing AndAlso eNuevoWidth = Nothing Then
            eNuevoWidth = iImagen.Width
        ElseIf iImagen.Width > iTextSharp.text.PageSize.A4.Width OrElse iImagen.Height > iTextSharp.text.PageSize.A4.Height Then

            If iImagen.Width > iTextSharp.text.PageSize.A4.Width Then

                eNuevoWidth = ((iTextSharp.text.PageSize.A4.Width - 20) / iImagen.Width) * iImagen.Width
                eNuevoHeight = ((iTextSharp.text.PageSize.A4.Width - 20) / iImagen.Width) * iImagen.Height

            Else
                eNuevoHeight = iImagen.Height
                eNuevoWidth = iImagen.Width
            End If
            If eNuevoHeight > iTextSharp.text.PageSize.A4.Height Then

                eNuevoHeight = ((iTextSharp.text.PageSize.A4.Height - 20) / iImagen.Height) * iImagen.Height
                eNuevoWidth = ((iTextSharp.text.PageSize.A4.Height - 20) / iImagen.Height) * iImagen.Width
            End If
        Else
            eNuevoHeight = iImagen.Height
            eNuevoWidth = iImagen.Width
        End If

        'Cambio el tamaño de la imagen
        iImagen.ScaleAbsolute(eNuevoWidth, eNuevoHeight)

        'ubico la imagen en la hoja del pdf
        iImagen.SetAbsolutePosition((iTextSharp.text.PageSize.A4.Width - eNuevoWidth) / 2, iTextSharp.text.PageSize.A4.Height - eNuevoHeight - 10)

        iPdfContentByte.AddImage(iImagen)

    End Sub

    Public Sub agregarRecuadro(ByVal ePosX As Long, ByVal ePosY As Long, ByVal eAlto As Long, ByVal eAncho As Long)
        Dim iPDFContentByte As PdfContentByte
        iPDFContentByte = iPdfWriter.DirectContent

        'convierto los milimetros a pixeles
        ePosX = ePosX * 2.8335
        ePosY = ePosY * 2.8335
        eAlto = eAlto * 2.8335
        eAncho = eAncho * 2.8335

        iPDFContentByte.Rectangle(ePosX + iMargenIzquierdo, iTextSharp.text.PageSize.A4.Height - (ePosY + iMargenSuperior), eAncho, eAlto)
        iPDFContentByte.Stroke()
    End Sub

    Public Sub agregarParrafo(ByVal eLinea As String, ByVal eDistanciaSuperior As Long, ByVal eTamañoFuente As Long, ByVal eNegrita As Boolean, ByVal eAliniacion As enumAlineacion, ByVal eSubrayado As Boolean, ByVal eInterlineado As Long, Optional ByVal eCursiva As Boolean = False, Optional ByVal eLineasParrafo As List(Of LineaParrafoVO) = Nothing, Optional ByVal eSangriaParrafo As Integer = 0)
        Dim iPdfParrafo As New iTextSharp.text.Paragraph(9 + eInterlineado)
        Dim iLineaParrafo As LineaParrafoVO
        'convierto los milimetros a pixeles
        eDistanciaSuperior = eDistanciaSuperior * 2.8335

        iPdfParrafo.IndentationLeft = iMargenIzquierdo + eSangriaParrafo
        iPdfParrafo.IndentationRight = (iMargenIzquierdo + eSangriaParrafo) / 2
        iPdfParrafo.SpacingBefore = eDistanciaSuperior

        Select Case eAliniacion
            Case enumAlineacion.CENTRADA
                iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            Case enumAlineacion.DERECHA
                iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_RIGHT
            Case enumAlineacion.IZQUIERDA
                iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_LEFT
            Case enumAlineacion.JUSTIFICADA
                iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
        End Select


        If IsNothing(eLineasParrafo) Then
            iPdfParrafo.Font = iTextSharp.text.FontFactory.GetFont("Arial", eTamañoFuente)
            If eNegrita Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.BOLD)
            If eSubrayado Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.UNDERLINE)
            If eCursiva Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.ITALIC)
            iPdfParrafo.ExtraParagraphSpace = 0
            iPdfParrafo.Add(eLinea)
        Else
            For Each iLineaParrafo In eLineasParrafo

                If iLineaParrafo.tamanioFuente <> Nothing Then
                    iPdfParrafo.Font = iTextSharp.text.FontFactory.GetFont("Arial", iLineaParrafo.tamanioFuente)
                Else
                    iPdfParrafo.Font = iTextSharp.text.FontFactory.GetFont("Arial", eTamañoFuente)
                End If
                If iLineaParrafo.negrita Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.BOLD)
                If eSubrayado Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.UNDERLINE)
                If eCursiva Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.ITALIC)
                iPdfParrafo.ExtraParagraphSpace = 0
                iPdfParrafo.Add(iLineaParrafo.linea)
            Next

        End If

        iDocumento.Add(iPdfParrafo)

    End Sub

    Public Sub agregarImagenParrafo(ePathArchivoImagen As String, ByVal ePosX As Long, ByVal ePosY As Long, ByVal eAliniacion As enumAlineacion, Optional ByVal ePorcentaje As Long = Nothing)
        Dim iPdfParrafo As New iTextSharp.text.Paragraph
        Dim iImagen As iTextSharp.text.Image
        Dim iHeight, iWidth As Single

        'convierto los milimetros a pixeles
        ePosX = ePosX * 2.8335
        ePosY = ePosY * 2.8335

        iImagen = iTextSharp.text.Image.GetInstance(ePathArchivoImagen)

        If ePorcentaje <> Nothing Then
            iHeight = (iImagen.Height * ePorcentaje) / 100
            iWidth = (iImagen.Width * ePorcentaje) / 100

            iImagen.ScaleAbsoluteHeight(iHeight)
            iImagen.ScaleAbsoluteWidth(iWidth)
        End If

        Select Case eAliniacion
            Case enumAlineacion.CENTRADA
                iImagen.Alignment = iTextSharp.text.Element.ALIGN_CENTER
            Case enumAlineacion.DERECHA
                iImagen.Alignment = iTextSharp.text.Element.ALIGN_RIGHT
            Case enumAlineacion.IZQUIERDA
                iImagen.Alignment = iTextSharp.text.Element.ALIGN_LEFT
            Case Else
                iImagen.Alignment = iTextSharp.text.Element.ALIGN_MIDDLE
        End Select

        If ePosX <> Nothing OrElse ePosY <> Nothing Then iImagen.SetAbsolutePosition(ePosX + iMargenIzquierdo, (iTextSharp.text.PageSize.A4.Height - (ePosY + iMargenSuperior)))

        iPdfParrafo.AddSpecial(iImagen)

        iDocumento.Add(iPdfParrafo)

    End Sub

    Public Sub agregarTabla(ByVal ePDFColumnasVO As PDFColumnasVO, Optional ByVal eAlineacionTabla As PDF.enumAlineacion = enumAlineacion.CENTRADA)
        Dim iPdfTable As New PdfPTable(ePDFColumnasVO.cantidadColumnas)
        iPdfTable.SetTotalWidth(ePDFColumnasVO.tamanio)
        Dim iPdCelda As PdfPCell
        Dim iTamanio As Single
        Dim i As Integer


        For Each iTamanio In ePDFColumnasVO.tamanio
            iPdfTable.TotalWidth += iTamanio
        Next
        iPdfTable.SetTotalWidth(ePDFColumnasVO.tamanio)
        iPdfTable.LockedWidth = True
        iPdfTable.SetWidths(ePDFColumnasVO.tamanio)
        Select Case eAlineacionTabla
            Case enumAlineacion.CENTRADA
                iPdfTable.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
            Case enumAlineacion.DERECHA
                iPdfTable.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT
            Case enumAlineacion.IZQUIERDA
                iPdfTable.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
            Case enumAlineacion.JUSTIFICADA
                iPdfTable.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
        End Select
        iPdfTable.SpacingBefore = ePDFColumnasVO.distanciaSuperior

        For i = 0 To ePDFColumnasVO.PDFCeldasVO.Count - 1
            'Formateo el parafop para la celda
            Dim iPdfParrafo As New iTextSharp.text.Paragraph(9 + ePDFColumnasVO.PDFCeldasVO.Item(i).interlineado)

            Select Case ePDFColumnasVO.PDFCeldasVO.Item(i).alineacion
                Case enumAlineacion.CENTRADA
                    iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_CENTER
                Case enumAlineacion.DERECHA
                    iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_RIGHT
                Case enumAlineacion.IZQUIERDA
                    iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_LEFT
                Case enumAlineacion.JUSTIFICADA
                    iPdfParrafo.Alignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
            End Select

            iPdfParrafo.Font = iTextSharp.text.FontFactory.GetFont("Arial", IIf(i <= ePDFColumnasVO.cantidadColumnas AndAlso ePDFColumnasVO.encabezado, ePDFColumnasVO.PDFCeldasVO.Item(i).tamañoFuente + 1, ePDFColumnasVO.PDFCeldasVO.Item(i).tamañoFuente), ePDFColumnasVO.PDFCeldasVO.Item(i).colorFuente)
            If ePDFColumnasVO.PDFCeldasVO.Item(i).negrita Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.BOLD)
            If ePDFColumnasVO.PDFCeldasVO.Item(i).subrayado Then iPdfParrafo.Font.SetStyle(iTextSharp.text.Font.UNDERLINE)
            iPdfParrafo.ExtraParagraphSpace = 0

            'Agrego el testo al parafo
            iPdfParrafo.Add(ePDFColumnasVO.PDFCeldasVO.Item(i).texto)

            'Agrego el parafo a la celda
            iPdCelda = New PdfPCell(iPdfParrafo)

            Select Case ePDFColumnasVO.PDFCeldasVO.Item(i).alineacionCelda
                Case enumAlineacion.CENTRADA
                    iPdCelda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                Case enumAlineacion.DERECHA
                    iPdCelda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT
                Case enumAlineacion.IZQUIERDA
                    iPdCelda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                Case enumAlineacion.JUSTIFICADA
                    iPdCelda.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
            End Select

            'le quito los bordes
            If ePDFColumnasVO.PDFCeldasVO.Item(i).borde > 0 Then
                iPdCelda.BorderWidth = ePDFColumnasVO.PDFCeldasVO.Item(i).borde
            Else
                iPdCelda.BorderWidthLeft = ePDFColumnasVO.PDFCeldasVO.Item(i).bordeIzquierdo
                iPdCelda.BorderWidthBottom = ePDFColumnasVO.PDFCeldasVO.Item(i).bordeInferior
                iPdCelda.BorderWidthRight = ePDFColumnasVO.PDFCeldasVO.Item(i).bordeDerecho
                iPdCelda.BorderWidthTop = ePDFColumnasVO.PDFCeldasVO.Item(i).bordeSuperior
            End If

            If ePDFColumnasVO.PDFCeldasVO.Item(i).distancia > 0 Then
                iPdCelda.PaddingBottom = ePDFColumnasVO.PDFCeldasVO.Item(i).distancia
                iPdCelda.PaddingTop = ePDFColumnasVO.PDFCeldasVO.Item(i).distancia
            End If

            iPdCelda.BackgroundColor = ePDFColumnasVO.PDFCeldasVO.Item(i).colorFondo

            'Agrego la celda a la tabla
            iPdfTable.AddCell(iPdCelda)

            ''Si es la primer columna verifico si tiene encabezado
            'If i = ePDFColumnasVO.cantidadColumnas - 1 AndAlso ePDFColumnasVO.encabezado AndAlso iPdfTable.Rows.Count > 0 Then
            '    'Le doy formato al encabezado
            '    For Each iCeldas As PdfPCell In iPdfTable.Rows(0).GetCells()
            '        If Not IsNothing(ePDFColumnasVO.colorFondo) Then iCeldas.BackgroundColor = ePDFColumnasVO.colorFondo
            '        iCeldas.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
            '        iCeldas.Padding = 3
            '    Next
            'End If

            iPdfParrafo = Nothing
        Next

        ' una vez recorrido todas la celdas agrego la tabla al  documento
        iDocumento.Add(iPdfTable)

    End Sub

    Public Sub agregarCodigoQR(ByVal eLinea As String, ByVal eTamañoFuente As Long, eNombreParametro As String)
        Dim iImagen As iTextSharp.text.Image
        Dim iBarCod As BarcodeQRCode
        Dim iPosicion As IList(Of FieldPosition)
        Dim iNumeroPagina As Integer
        Dim iPosicionX As Integer
        Dim iPosicionY As Integer

        iPosicion = iPdfStamper.AcroFields.GetFieldPositions(eNombreParametro)
        If Not IsNothing(iPosicion) Then
            iNumeroPagina = iPosicion(0).page
            iPosicionX = iPosicion(0).position.Left
            iPosicionY = iPosicion(0).position.Bottom
            eTamañoFuente = iPosicion(0).position.Height
            'iPdfContentByte = iPdfStamper.GetUnderContent(1)
            iBarCod = New BarcodeQRCode(eLinea, eTamañoFuente, eTamañoFuente, Nothing)
            iImagen = iBarCod.GetImage()
            iImagen.SetAbsolutePosition(iPosicionX, iPosicionY)
            iPdfStamper.GetOverContent(iNumeroPagina).AddImage(iImagen)
        End If
    End Sub

    Public Sub combinarPDFS(ByVal eArchivosPDFS As Microsoft.VisualBasic.Collection, eArchivoSalida As String)
        Dim document As New iTextSharp.text.Document()
        Dim writer As PdfCopy

        Try
            writer = New PdfCopy(document, New FileStream(eArchivoSalida, FileMode.Create))
            If writer Is Nothing Then Throw New Exception

            document.Open()

            For Each fileName As String In eArchivosPDFS
                Dim reader As New PdfReader(fileName)
                reader.ConsolidateNamedDestinations()

                ' step 4: we add content
                For i As Integer = 1 To reader.NumberOfPages
                    Dim page As PdfImportedPage = writer.GetImportedPage(reader, i)
                    writer.AddPage(page)
                Next

                Dim form As PRAcroForm = reader.AcroForm
                If form IsNot Nothing Then
                    writer.CopyAcroForm(reader)
                End If

                reader.Close()
            Next

            writer.Close()
            document.Close()

        Catch exception As Exception
        Finally
            document = Nothing
            writer = Nothing
        End Try
    End Sub

#End Region

End Class
