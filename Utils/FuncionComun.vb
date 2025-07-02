Imports System.Configuration
Imports di.financiera.excepciones
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Security.Cryptography
Imports System.Text

Class FuncionComun

#Region "Enumerados"
	Public Shared iBaseDeDatos As String = ConfigurationSettings.AppSettings("baseDeDatos")
	Public Enum enumFormatoFecha
		DDMMYY
		DDMMYYYY
		YYYYMMDD
		YYMMDD
		MMYY
		MMYYYY
		YYYYMM
		YYMM
		YYYYMMDDHHMMSS
		YYMMDDHHMMSS
		YYMMDDHHMM
		YYYYMMDDHHMM
		DDMMYYHHMM
		DDMMYYYYHHMM
		DDMMYYHHMMSS
		DDMMYYYYHHMMSS
		DD
		MM
		MMDD
		YY
		YYYY
		HHMM
		HHMMSS
	End Enum

	Public Enum enumFecha
		DIA
		MES
		AÑO
	End Enum

#End Region

#Region "Metodos"
	Public Shared Function sqlFormatoFecha(ByVal eCampo As String, ByVal eFormato As enumFormatoFecha) As String

        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'Formatos para SQL server
            Select Case eFormato
                Case enumFormatoFecha.DD
                    Return "DATEPART(d," & eCampo & ")"
                Case enumFormatoFecha.MM
                    Return "DATEPART(m," & eCampo & ")"
                Case enumFormatoFecha.MMDD
                    Return "Convert(varchar, DatePart(MM, " & eCampo & ")) +'/'+ Convert(varchar,DATEPART (DD," & eCampo & "))"
                Case enumFormatoFecha.DDMMYY
                    Return "Convert(varchar," & eCampo & ",3)"
                Case enumFormatoFecha.DDMMYYHHMM
                    Return "Convert(varchar," & eCampo & ",3)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.DDMMYYHHMMSS
                    Return "Convert(varchar," & eCampo & ",3)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.DDMMYYYY
                    Return "Convert(varchar," & eCampo & ",103)"
                Case enumFormatoFecha.DDMMYYYYHHMM
                    Return "Convert(varchar," & eCampo & ",103)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.DDMMYYYYHHMMSS
                    Return "Convert(varchar," & eCampo & ",103)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.MMYY
                    Return "( Convert(varchar,DATEPART(m," & eCampo & ")) + '/' + Convert(varchar,right(DATEPART(yyyy," & eCampo & ",2))))"
                Case enumFormatoFecha.MMYYYY
                    Return "( Convert(varchar,DATEPART(m," & eCampo & ")) + '/' + Convert(varchar,DATEPART(yyyy," & eCampo & ")))"
                Case enumFormatoFecha.HHMM
                    Return "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.HHMMSS
                    Return "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.YYMM
                    Return "( Convert(varchar,right(DATEPART(yyyy," & eCampo & "),2)) + '/' + Convert(varchar,DATEPART(m," & eCampo & ")))"
                Case enumFormatoFecha.YYMMDD
                    Return "Convert(varchar," & eCampo & ",11)"
                Case enumFormatoFecha.YYMMDDHHMM
                    Return "Convert(varchar," & eCampo & ",11)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.YYMMDDHHMMSS
                    Return "Convert(varchar," & eCampo & ",11)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
                Case enumFormatoFecha.YYYYMM
                    Return "( Convert(varchar,DATEPART(yyyy," & eCampo & ")) + '/' + Convert(varchar,DATEPART(m," & eCampo & ")))"
                Case enumFormatoFecha.YYYYMMDD
                    Return "Convert(varchar," & eCampo & ",111)"
                Case enumFormatoFecha.YYYYMMDDHHMM
                    Return "Convert(varchar," & eCampo & ",111)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
                Case enumFormatoFecha.YYYYMMDDHHMMSS
                    Return "Convert(varchar," & eCampo & ",20)"
                Case enumFormatoFecha.YYYY
                    Return "Convert(varchar,DatePart(YYYY," & eCampo & "))"
            End Select
        Else

            'Formatos para MySql
            Select Case eFormato

                Case enumFormatoFecha.DD
                    Return "Date_Format(" & eCampo & ",'%d')"
                Case enumFormatoFecha.MM
                    Return "Date_Format(" & eCampo & ",'%m')"
                Case enumFormatoFecha.MMDD
                    Return "Date_Format(" & eCampo & ", '%d-%m')"
                Case enumFormatoFecha.YY
                    Return "Date_Format(" & eCampo & ",'%y')"
                Case enumFormatoFecha.YYYY
                    Return "Date_Format(" & eCampo & ",'%Y')"
                Case enumFormatoFecha.DDMMYY
                    Return "Date_Format(" & eCampo & ",'%d/%m/%y')"
                Case enumFormatoFecha.DDMMYYHHMM
                    Return "Date_Format(" & eCampo & ",'%d/%m/%y %H:%i')"
                Case enumFormatoFecha.DDMMYYHHMMSS
                    Return "Date_Format(" & eCampo & ",'%d/%m/%y %H:%i:%s')"
                Case enumFormatoFecha.DDMMYYYY
                    Return "Date_Format(" & eCampo & ",'%d/%m/%Y')"
                Case enumFormatoFecha.DDMMYYYYHHMM
                    Return "Date_Format(" & eCampo & ",'%d/%m/%Y %H:%i')"
                Case enumFormatoFecha.DDMMYYYYHHMMSS
                    Return "Date_Format(" & eCampo & ",'%d/%m/%Y %H:%i:%s')"
                Case enumFormatoFecha.MMYY
                    Return "Date_Format(" & eCampo & ",'%m/%y')"
                Case enumFormatoFecha.MMYYYY
                    Return "Date_Format(" & eCampo & ",'%m/%Y')"
                Case enumFormatoFecha.HHMM
                    Return "Date_Format(" & eCampo & ",'%H:%i')"
                Case enumFormatoFecha.HHMMSS
                    Return "Date_Format(" & eCampo & ",'%H:%i:%s')"
                Case enumFormatoFecha.YYMM
                    Return "Date_Format(" & eCampo & ",'%y/%m')"
                Case enumFormatoFecha.YYMMDD
                    Return "Date_Format(" & eCampo & ",'%y/%m/%d')"
                Case enumFormatoFecha.YYMMDDHHMM
                    Return "Date_Format(" & eCampo & ",'%y/%m/%d %H:%i')"
                Case enumFormatoFecha.YYMMDDHHMMSS
                    Return "Date_Format(" & eCampo & ",'%y/%m/%d %H:%i:%s')"
                Case enumFormatoFecha.YYYYMM
                    Return "Date_Format(" & eCampo & ",'%Y/%m')"
                Case enumFormatoFecha.YYYYMMDD
                    Return "Date_Format(" & eCampo & ",'%Y/%m/%d')"
                Case enumFormatoFecha.YYYYMMDDHHMM
                    Return "Date_Format(" & eCampo & ",'%Y/%m/%d %H:%i')"
                Case enumFormatoFecha.YYYYMMDDHHMMSS
                    Return "Date_Format(" & eCampo & ",'%Y/%m/%d %H:%i:%s')"
            End Select
        End If
    End Function

    Public Shared Function sqlConcatenar(ByVal eCampos As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'Concatenar con SQL Server
            Dim iCaracter, eColumna, iResultadoFinal As String
            Dim iComasAbiertos As Integer
            iResultadoFinal = ""
            eColumna = ""
            For i As Integer = 1 To Len(eCampos)
                iCaracter = Mid(eCampos, i, 1)
                If iCaracter = "," And iComasAbiertos = 0 Then
                    iResultadoFinal &= "Convert(varchar," & eColumna & ") +"
                    eColumna = ""
                ElseIf iCaracter = "(" Then
                    iComasAbiertos += 1
                    eColumna &= iCaracter
                ElseIf iCaracter = ")" Then
                    iComasAbiertos -= 1
                    eColumna &= iCaracter
                Else
                    eColumna &= iCaracter
                End If
            Next
            iResultadoFinal &= "Convert(varchar," & eColumna & ")"
            eCampos = iResultadoFinal
        Else
            'Concatenar con MySQL
            eCampos = "Concat(" & eCampos & ")"
        End If
        Return eCampos
    End Function

    Public Shared Function sqlMD5(ByVal eCampo As String) As String
        Using md5Hash As MD5 = MD5.Create()
            Return generarMD5(md5Hash, eCampo)
        End Using
    End Function

    Shared Function generarMD5(ByVal eMD5Hash As MD5, ByVal eCampo As String) As String
        Dim data As Byte() = eMD5Hash.ComputeHash(Encoding.ASCII.GetBytes(eCampo))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        Return sBuilder.ToString()
    End Function

    Public Shared Function sqlDiferenciaHoras(ByVal ePrimerCampo As String, ByVal eSegundoCampo As String) As String

        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            'SQL Server
            Return "datediff(day," & eSegundoCampo & "," & ePrimerCampo & ")"
        Else
            'MySQL
            Return "timediff(" & ePrimerCampo & "," & eSegundoCampo & ")"
        End If
    End Function

    Public Shared Function diferenciaTiempo(ByVal eFechaInicio As Date, ByVal eFechafin As Date) As String
        Dim iHoras, iMinutos, iSegundos As Long

        Try
            iHoras = Int(DateDiff("s", eFechaInicio, eFechafin) / 3600)
            iMinutos = Int((DateDiff("s", eFechaInicio, eFechafin) - (iHoras * 3600)) / 60)
            iSegundos = Int(DateDiff("s", eFechaInicio, eFechafin) - (iHoras * 3600) - (iMinutos * 60))

            Return Right("00" & iHoras, 2) & ":" & Right("00" & iMinutos, 2) & ":" & Right("00" & iSegundos, 2)

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function vacioSiEsNulo(ByVal eObjeto As Object) As String

        If IsDBNull(eObjeto) Or eObjeto Is Nothing Then
            Return ""
        Else
            Return Trim(eObjeto)
        End If

    End Function

    Public Shared Function ceroDecimal(ByVal eNumero As String) As String
        If eNumero <> vbNullString Then
            ceroDecimal = Format(CDbl(eNumero), "0.00")
        Else
            ceroDecimal = "0,00"
        End If
    End Function

    Public Shared Function ceroSiEsNulo(ByVal eObjeto As Object) As Double

        If IsNothing(eObjeto) OrElse IsDBNull(eObjeto) OrElse eObjeto = Nothing Then
            Return 0
        Else
            Return CDbl(eObjeto)
        End If

    End Function

    Public Shared Function ceroSiEsVacio(ByVal eCadena As String) As Double
        If eCadena = "" Then
            Return 0
        Else
            Return CDbl(Replace(eCadena, ".", ","))
        End If

    End Function

    Public Shared Function nuloSiEsNothing(ByVal eObject As Object) As String
        If IsNothing(eObject) OrElse eObject = Nothing Then
            Return "null"
        Else
            If TypeOf (eObject) Is Double Then
                eObject = "'" & eObject & "'"
                Return Replace(eObject, ",", ".")
            ElseIf TypeOf (eObject) Is Date Then
                Return "'" & Format(eObject, "yyyy/MM/dd") & "'"
            Else
                Return "'" & eObject & "'"
            End If
        End If
    End Function

    Public Shared Function ceroSiEsNothing(ByVal eObject As Object) As String
        If eObject = Nothing Then
            Return "0"
        Else
            If TypeOf (eObject) Is Double Then
                eObject = "'" & eObject & "'"
                Return Replace(eObject, ",", ".")
            ElseIf TypeOf (eObject) Is Date Then
                Return "'" & Format(eObject, "yyyy/MM/dd") & "'"
            Else
                Return "'" & eObject & "'"
            End If
        End If
    End Function

    Public Shared Function nothingSiEsVacio(ByVal eCadena As String) As Date

        Try
            If eCadena = "" Then
                Return Nothing
            Else
                Return CDate(eCadena)
            End If
        Catch exception As Exception
            Return Nothing
        End Try

    End Function

    Public Shared Function nothingSiEsNulo(ByVal eObject As Object) As Object
        If IsDBNull(eObject) OrElse eObject = Nothing Then
            Return Nothing
        Else
            Return eObject
        End If
    End Function

    Public Shared Function byteBoolean(ByVal eNumero As Byte) As Boolean
        Return eNumero = 1
    End Function

    Public Shared Function booleanByte(ByVal eBoolean As Boolean) As Byte
        Return IIf(eBoolean, 1, 0)
    End Function

    Public Shared Function vacioSiEsNothing(ByVal eObject As Object) As String
        If eObject = Nothing Then
            Return ""
        Else
            Select Case TypeName(eObject)
                Case "Date"
                    Return Format(CType(eObject, Date), "dd/MM/yyyy")
                Case Else
                    Return eObject.ToString
            End Select
        End If

    End Function

    Public Shared Function obtenerDatosListaSiNo() As Collection
        Dim iColeccion As New Collection
        iColeccion.Add("SI")
        iColeccion.Add("NO")
        Return iColeccion
    End Function

    'Public Shared Sub zipearDirectorio(ByVal eDirectorio As String, ByVal ePathDestino As String)
    '    Try
    '        System.IO.Compression.ZipFile.CreateFromDirectory(eDirectorio, ePathDestino, System.IO.Compression.CompressionLevel.Optimal, True)
    '    Catch Exception As Exception
    '        Throw New ArchivoNoZipeadoException(Exception)
    '    Finally

    '    End Try
    'End Sub

    'Public Shared Sub zipearArchivos(ByVal eArchivosAZipear As Collection, ByVal ePathDestino As String)
    '    Dim iNombreAchivo As String
    '    Try
    '        If File.Exists(ePathDestino) Then File.Delete(ePathDestino)
    '        Directory.CreateDirectory(ePathDestino.ToUpper.Replace(".ZIP", ""))

    '        For i As Integer = 1 To eArchivosAZipear.Count
    '            iNombreAchivo = eArchivosAZipear.Item(i)
    '            File.Copy(iNombreAchivo, ePathDestino.ToUpper.Replace(".ZIP", "") & Right(iNombreAchivo, iNombreAchivo.Length - iNombreAchivo.LastIndexOf("\")))
    '        Next
    '        System.IO.Compression.ZipFile.CreateFromDirectory(ePathDestino.ToUpper.Replace(".ZIP", ""), ePathDestino, System.IO.Compression.CompressionLevel.Optimal, False)
    '    Catch Exception As Exception
    '        Throw New ArchivoNoZipeadoException(Exception)
    '    Finally
    '        If Directory.Exists(ePathDestino.ToUpper.Replace(".ZIP", "")) Then Directory.Delete(ePathDestino.ToUpper.Replace(".ZIP", ""), True)
    '    End Try
    'End Sub

    'Public Shared Sub zipearDirectorio(ByVal eDirectorio As String, ByVal ePathDestino As String)

    '    Dim iNombresArchivos() As String
    '    Dim iCrc32 As New Crc32
    '    Dim iZipOutputStream As ZipOutputStream
    '    Dim iZipEntry As ZipEntry
    '    Dim iNombreAchivo As String
    '    Dim iArchivo As FileStream

    '    Try
    '        iNombresArchivos = Directory.GetFiles(eDirectorio)
    '        iZipOutputStream = New ZipOutputStream(File.Create(ePathDestino))
    '        iZipOutputStream.SetLevel(6)

    '        'Compression Level: 0-9
    '        '0: no(Compression)
    '        '9: maximum compression

    '        For Each iNombreAchivo In iNombresArchivos
    '            iArchivo = File.OpenRead(iNombreAchivo)
    '            Dim iBuffer(iArchivo.Length - 1) As Byte

    '            iArchivo.Read(iBuffer, 0, iBuffer.Length)
    '            iZipEntry = New ZipEntry(Right(iNombreAchivo, iNombreAchivo.Length - InStrRev(iNombreAchivo, "\")))

    '            iZipEntry.DateTime = DateTime.Now
    '            iZipEntry.Size = iArchivo.Length
    '            iArchivo.Close()
    '            iCrc32.Reset()
    '            iCrc32.Update(iBuffer)
    '            iZipEntry.Crc = iCrc32.Value
    '            iZipOutputStream.PutNextEntry(iZipEntry)
    '            iZipOutputStream.Write(iBuffer, 0, iBuffer.Length)
    '        Next

    '    Catch Exception As Exception
    '        Throw New ArchivoNoZipeadoException(Exception)
    '    Finally
    '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Finish()
    '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Close()
    '        iCrc32 = Nothing
    '        iZipEntry = Nothing
    '        iArchivo = Nothing
    '    End Try
    'End Sub

    'Public Shared Sub zipearArchivos(ByVal eArchivosAZipear As Collection, ByVal ePathDestino As String)
    '    Dim iCrc32 As New Crc32
    '    Dim iZipOutputStream As ZipOutputStream
    '    Dim iZipEntry As ZipEntry
    '    Dim iNombreAchivo As String
    '    Dim iArchivo As FileStream
    '    Dim i As Integer

    '    Try

    '        iZipOutputStream = New ZipOutputStream(File.Create(ePathDestino))
    '        iZipOutputStream.SetLevel(6)

    '        'Compression Level: 0-9
    '        '0: no(Compression)
    '        '9: maximum compression

    '        For i = 1 To eArchivosAZipear.Count
    '            iNombreAchivo = eArchivosAZipear.Item(i)
    '            iArchivo = File.OpenRead(iNombreAchivo)
    '            Dim iBuffer(iArchivo.Length - 1) As Byte

    '            iArchivo.Read(iBuffer, 0, iBuffer.Length)
    '            iZipEntry = New ZipEntry(Right(iNombreAchivo, iNombreAchivo.Length - InStrRev(iNombreAchivo, "\")))
    '            iZipEntry.DateTime = DateTime.Now
    '            iZipEntry.Size = iArchivo.Length
    '            iArchivo.Close()
    '            iCrc32.Reset()
    '            iCrc32.Update(iBuffer)
    '            iZipEntry.Crc = iCrc32.Value
    '            iZipOutputStream.PutNextEntry(iZipEntry)
    '            iZipOutputStream.Write(iBuffer, 0, iBuffer.Length)
    '        Next i


    '    Catch Exception As Exception
    '        Throw New ArchivoNoZipeadoException(Exception)
    '    Finally
    '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Finish()
    '        If Not IsNothing(iZipOutputStream) Then iZipOutputStream.Close()
    '        iCrc32 = Nothing
    '        iZipEntry = Nothing
    '        iArchivo = Nothing
    '    End Try
    'End Sub

    Public Shared Sub UnZip(ByVal ePathArchivoZipeado As String)
        Dim FileZipUploaded As String = ePathArchivoZipeado
        Dim ZipStream As New ZipInputStream(File.OpenRead(FileZipUploaded))
        Dim TheEntry As ZipEntry = ZipStream.GetNextEntry()

        Try
            Do Until TheEntry Is Nothing
                Dim data(1024) As Byte
                Dim fileNameUnzipped As String = Left(ePathArchivoZipeado, InStrRev(ePathArchivoZipeado, "\")) & TheEntry.Name.ToString
                Dim newfs As FileStream = File.Create(fileNameUnzipped)
                Dim bw As New BinaryWriter(newfs)

                Dim buffer(1024) As Byte
                Dim length As Integer
                Dim dataToRead As Long
                dataToRead = TheEntry.Size
                While dataToRead > 0
                    length = ZipStream.Read(buffer, 0, 1024)
                    bw.Write(buffer, 0, length)
                    ReDim buffer(1024) ' Clear the buffer
                    dataToRead = dataToRead - length
                End While
                bw.Close()
                newfs.Close()
                TheEntry = ZipStream.GetNextEntry()
            Loop
        Catch Exception As Exception
            Throw New ArchivoNoZipeadoException(Exception)
        Finally
            FileZipUploaded = Nothing
            ZipStream = Nothing
            TheEntry = Nothing
        End Try

    End Sub

    Public Shared Function formatoFecha(ByVal eCampo As String, ByVal eFormato As enumFormatoFecha) As String
        'Dim iBaseDeDatos As String = ConfigurationManager.AppSettings("baseDeDatos")

        'If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
        '    'Formatos para SQL server
        '    Select Case eFormato
        '        Case enumFormatoFecha.DD
        '            Return "DDATEPART(d," & eCampo & ")"
        '        Case enumFormatoFecha.DDMMYY
        '            Return "Convert(varchar," & eCampo & ",3)"
        '        Case enumFormatoFecha.DDMMYYHHMM
        '            Return "Convert(varchar," & eCampo & ",3)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
        '        Case enumFormatoFecha.DDMMYYHHMMSS
        '            Return "Convert(varchar," & eCampo & ",3)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
        '        Case enumFormatoFecha.DDMMYYYY
        '            Return "Convert(varchar," & eCampo & ",103)"
        '        Case enumFormatoFecha.DDMMYYYYHHMM
        '            Return "Convert(varchar," & eCampo & ",103)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
        '        Case enumFormatoFecha.DDMMYYYYHHMMSS
        '            Return "Convert(varchar," & eCampo & ",103)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
        '        Case enumFormatoFecha.MMYY
        '            Return "( Convert(varchar,DATEPART(m," & eCampo & ")) + '/' + Convert(varchar,right(DATEPART(yyyy," & eCampo & ",2))))"
        '        Case enumFormatoFecha.MMYYYY
        '            Return "( Convert(varchar,DATEPART(m," & eCampo & ")) + '/' + Convert(varchar,DATEPART(yyyy," & eCampo & ")))"
        '        Case enumFormatoFecha.HHMM
        '            Return "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
        '        Case enumFormatoFecha.HHMMSS
        '            Return "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
        '        Case enumFormatoFecha.YYMM
        '            Return "( Convert(varchar,right(DATEPART(yyyy," & eCampo & "),2)) + '/' + Convert(varchar,DATEPART(m," & eCampo & ")))"
        '        Case enumFormatoFecha.YYMMDD
        '            Return "Convert(varchar," & eCampo & ",11)"
        '        Case enumFormatoFecha.YYMMDDHHMM
        '            Return "Convert(varchar," & eCampo & ",11)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
        '        Case enumFormatoFecha.YYMMDDHHMMSS
        '            Return "Convert(varchar," & eCampo & ",11)" & " + ' ' +" & "( Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")) + ':' + Convert(varchar,DATEPART(ss," & eCampo & ")))"
        '        Case enumFormatoFecha.YYYYMM
        '            Return "( Convert(varchar,DATEPART(yyyy," & eCampo & ")) + '/' + Convert(varchar,DATEPART(m," & eCampo & ")))"
        '        Case enumFormatoFecha.YYYYMMDD
        '            Return "Convert(varchar," & eCampo & ",111)"
        '        Case enumFormatoFecha.YYYYMMDDHHMM
        '            Return "Convert(varchar," & eCampo & ",111)" & " + ' ' +" & "(Convert(varchar,DATEPART(hh," & eCampo & ")) + ':' + Convert(varchar,DATEPART(mi," & eCampo & ")))"
        '        Case enumFormatoFecha.YYYYMMDDHHMMSS
        '            Return "Convert(varchar," & eCampo & ",20)"
        '    End Select
        'Else

        'Formatos para MySql
        Select Case eFormato

            Case enumFormatoFecha.DD
                Return "Date_Format(" & eCampo & ",'%d')"
            Case enumFormatoFecha.MM
                Return "Date_Format(" & eCampo & ",'%m')"
            Case enumFormatoFecha.YY
                Return "Date_Format(" & eCampo & ",'%y')"
            Case enumFormatoFecha.DDMMYY
                Return "Date_Format(" & eCampo & ",'%d/%m/%y')"
            Case enumFormatoFecha.DDMMYYHHMM
                Return "Date_Format(" & eCampo & ",'%d/%m/%y %H:%i')"
            Case enumFormatoFecha.DDMMYYHHMMSS
                Return "Date_Format(" & eCampo & ",'%d/%m/%y %H:%i:%s')"
            Case enumFormatoFecha.DDMMYYYY
                Return "Date_Format(" & eCampo & ",'%d/%m/%Y')"
            Case enumFormatoFecha.DDMMYYYYHHMM
                Return "Date_Format(" & eCampo & ",'%d/%m/%Y %H:%i')"
            Case enumFormatoFecha.DDMMYYYYHHMMSS
                Return "Date_Format(" & eCampo & ",'%d/%m/%Y %H:%i:%s')"
            Case enumFormatoFecha.MMYY
                Return "Date_Format(" & eCampo & ",'%m/%y')"
            Case enumFormatoFecha.MMYYYY
                Return "Date_Format(" & eCampo & ",'%m/%Y')"
            Case enumFormatoFecha.HHMM
                Return "Date_Format(" & eCampo & ",'%H:%i')"
            Case enumFormatoFecha.HHMMSS
                Return "Date_Format(" & eCampo & ",'%H:%i:%s')"
            Case enumFormatoFecha.YYMM
                Return "Date_Format(" & eCampo & ",'%y/%m')"
            Case enumFormatoFecha.YYMMDD
                Return "Date_Format(" & eCampo & ",'%y/%m/%d')"
            Case enumFormatoFecha.YYMMDDHHMM
                Return "Date_Format(" & eCampo & ",'%y/%m/%d %H:%i')"
            Case enumFormatoFecha.YYMMDDHHMMSS
                Return "Date_Format(" & eCampo & ",'%y/%m/%d %H:%i:%s')"
            Case enumFormatoFecha.YYYYMM
                Return "Date_Format(" & eCampo & ",'%Y/%m')"
            Case enumFormatoFecha.YYYYMMDD
                Return "Date_Format(" & eCampo & ",'%Y/%m/%d')"
            Case enumFormatoFecha.YYYYMMDDHHMM
                Return "Date_Format(" & eCampo & ",'%Y/%m/%d %H:%i')"
            Case enumFormatoFecha.YYYYMMDDHHMMSS
                Return "Date_Format(" & eCampo & ",'%Y/%m/%d %H:%i:%s')"
        End Select
        'End If
    End Function

    Public Shared Function concatenar(ByVal eCampos As String) As String
        'Dim iBaseDeDatos As String = ConfigurationManager.AppSettings("baseDeDatos")

        'If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
        '    'Concatenar con SQL Server
        '    eCampos = "(Convert(varchar," & Replace(eCampos, ",", ") + Convert(varchar,") & "))"
        'Else
        'Concatenar con MySQL
        eCampos = "Concat(" & eCampos & ")"
        'End If
        Return eCampos
    End Function

    Public Shared Function rellenarAIzquierda(ByVal eCampo As String, ByVal eCorte As String, ByVal eRelleno As String) As String
        'Dim iBaseDeDatos As String = ConfigurationManager.AppSettings("baseDeDatos")

        'If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
        '    'Right SQL Server
        '    Return "right(('" & eRelleno & "' + Convert(varchar," & eCampo & "))," & eCorte & ")"
        'Else
        'lPad MySQL 
        Return "lpad(" & eCampo & "," & eCorte & ",'" & eRelleno & "')"
        'End If
    End Function

    Public Shared Function diferenciaDias(ByVal ePrimerCampo As String, ByVal eSegundoCampo As String) As String
        If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
            '    'SQL Server
            Return "datediff(day," & eSegundoCampo & "," & ePrimerCampo & ")"
        Else
            'MySQL
            Return "to_days(" & ePrimerCampo & ") - to_days(" & eSegundoCampo & ")"
        End If
    End Function

    Public Shared Function truncar(ByVal eCampo As String, ByVal eCantidadDeDecimales As String) As String
        'Dim iBaseDeDatos As String = ConfigurationManager.AppSettings("baseDeDatos")

        'If (iBaseDeDatos = "SQLSERVER" OrElse iBaseDeDatos = "SQLSERVER14") Then
        '    'SQL Server
        '    Return "round(" & eCampo & "," & eCantidadDeDecimales & ",1)"
        'Else
        'MySQL
        Return "truncate(" & eCampo & "," & eCantidadDeDecimales & ")"
		'End If
	End Function

	Public Shared Function sumarFecha(ByVal eFecha As String, ByVal eSuma As String, ByVal eTipoFechaASumar As enumFecha) As String
		'Solo implementado para mysql
		If eTipoFechaASumar = enumFecha.DIA Then
			Return "DATE_ADD(" & eFecha & ",interval " & eSuma & " day)"
		ElseIf eTipoFechaASumar = enumFecha.MES Then
			Return "DATE_ADD(" & eFecha & ",interval " & eSuma & " month)"
		Else
			Return "DATE_ADD(" & eFecha & ",interval " & eSuma & " year)"
		End If
	End Function

	Public Shared Function obtenerEspaciosHTML(ByVal eCantidadDeEspacios As Integer) As String
		Dim iEspacio As String = "&nbsp"
		Dim iHTML
		Dim i As Integer

		For i = 1 To eCantidadDeEspacios
			iHTML += iEspacio
		Next i

		Return iHTML
	End Function

	Public Shared Function obtenerMotivoOriginal(ByVal eExcepcion As Exception, Optional eSinVBNewLine As Boolean = False) As String
		Dim iMensaje As String

		Try
			If Not IsNothing(eExcepcion) Then
				If TypeOf (eExcepcion) Is RootException Then
					Dim iExcepcionAplicacion As RootException = eExcepcion
					iMensaje = iExcepcionAplicacion.ToString & IIf(eSinVBNewLine, "", vbNewLine)
					While Not IsNothing(iExcepcionAplicacion.originalCause)
						If TypeOf (iExcepcionAplicacion.originalCause) Is RootException Then
							iExcepcionAplicacion = iExcepcionAplicacion.originalCause
							iMensaje = iExcepcionAplicacion.ToString & IIf(eSinVBNewLine, "", vbNewLine)
						Else
							iMensaje = iExcepcionAplicacion.originalCause.ToString
							iExcepcionAplicacion.originalCause = Nothing
						End If
					End While
				Else
					iMensaje = eExcepcion.ToString
				End If
			End If
			iMensaje = iMensaje.Replace(vbNewLine, "")
			Return iMensaje

		Catch exception As Exception
			Return Nothing
		End Try
	End Function

	Public Shared Function nuloSiEsNothingFechaHora(ByVal eObject As Object) As String
		If IsNothing(eObject) OrElse eObject = Nothing Then
			Return "null"
		ElseIf TypeOf (eObject) Is Date Then
			Return "'" & Format(eObject, "yyyy/MM/dd HH:mm:ss") & "'"
		Else
			Return "'" & eObject & "'"
		End If
	End Function

	Public Shared Function nuloSiEsNothingHora(ByVal eObject As Object) As String
		If IsNothing(eObject) OrElse eObject = Nothing Then
			Return "null"
		Else
			If TypeOf (eObject) Is Double Then
				eObject = "'" & eObject & "'"
				Return Replace(eObject, ",", ".")
			ElseIf TypeOf (eObject) Is Date Then
				Return "'" & Format(eObject, "HH:mm:ss") & "'"
			Else
				Return "'" & eObject & "'"
			End If
		End If
	End Function

	Public Shared Sub loguearFTP(ByVal eError As String)
		Dim iArchivoPaginas As StreamWriter

		Try
			iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresFTP" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
			iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

		Catch exception As Exception
		Finally
			If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
			iArchivoPaginas = Nothing
		End Try
	End Sub

    Public Shared Sub loguearErrores(ByVal eError As String)
        Dim iArchivoPaginas As StreamWriter

        Try
            iArchivoPaginas = New StreamWriter(ConfigurationManager.AppSettings("archivosGenerados") & "ErroresGenericos" & Format(Today, "ddMMyyyy") & ".txt", True, System.Text.Encoding.GetEncoding(1252))
            iArchivoPaginas.WriteLine(Format(Now, "dd/MM/yyyy HH:mm:ss") & " - " & eError)

        Catch exception As Exception
        Finally
            If Not IsNothing(iArchivoPaginas) Then iArchivoPaginas.Close()
            iArchivoPaginas = Nothing
        End Try
    End Sub

#End Region

End Class
