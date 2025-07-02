Imports Microsoft.VisualBasic
Imports System.Text

Public Class HardKey
    Public sBox1() As Integer = {&H72, &HFB, &H9C, &HB3, &H5E, &HE5, &H10, &HC5, &HB2, &H28, &H2E, &H7B, &H6E, &H63, &H4E, &H46, _
&H92, &H7D, &HFE, &H79, &HF, &H59, &HE6, &H77, &HD2, &H58, &HC6, &HC7, &HF6, &H71, &H3C, &HA9, _
&H52, &HA3, &H82, &H64, &H66, &HB7, &H5C, &HA, &H0, &H93, &H5A, &H51, &HCE, &HC1, &HD0, &H50, _
&HA2, &H42, &H9A, &H1E, &HD6, &HF7, &H6C, &H5B, &H4A, &H3D, &H8, &H78, &H34, &HE3, &H54, &H1F, _
&HF2, &H7F, &H56, &HC2, &H3A, &H1C, &HBC, &H37, &H62, &HD7, &HE2, &H73, &H33, &HF0, &HD8, &HA0, _
&H38, &H6B, &H36, &H3, &HEE, &H87, &H98, &H7E, &H44, &H90, &H2, &HE, &H8A, &HAD, &H3E, &HB6, _
&H18, &HE0, &H4C, &HB5, &HBA, &HC8, &HE8, &H7, &HAA, &H7A, &H96, &H45, &H6, &HF1, &HA6, &H2C, _
&H2F, &H9E, &H68, &H30, &HDA, &HF5, &HC4, &H99, &H74, &HD3, &H84, &H9D, &H57, &H97, &HE4, &HB4, _
&H40, &HAC, &H41, &HC0, &H70, &H8C, &H1D, &H5D, &H20, &HAE, &H5F, &H13, &H32, &HBB, &H4B, &H23, _
&H88, &HC, &H26, &H2A, &H89, &H3B, &H81, &HD5, &HEA, &H94, &HF4, &HB, &H4, &H9F, &HA8, &HDD, _
&H60, &H17, &HCC, &H8E, &HA1, &HC9, &H27, &HBD, &H8F, &H19, &H61, &HF9, &HBE, &HC3, &H12, &HE7, _
&H43, &HA4, &H76, &HDC, &HCA, &H9, &H80, &HCF, &H95, &H5, &H24, &H22, &HB0, &H16, &HA7, &H31, _
&H15, &HB8, &HFA, &HCD, &H4F, &HD9, &HFC, &H2B, &H83, &H49, &H8D, &H67, &H29, &H11, &HD4, &HFF, _
&H65, &HDE, &H1A, &H9B, &H1B, &H1, &H14, &HD1, &H91, &H69, &H85, &H55, &H6F, &HDF, &H47, &HF8, _
&HA5, &HEB, &H53, &H86, &H48, &H6A, &H39, &HB9, &HD, &HFD, &H2D, &H3F, &HAF, &HE1, &H7C, &H25, _
&HEC, &HBF, &HED, &HAB, &H21, &H6D, &HB1, &HDB, &H75, &H35, &H4D, &HCB, &HF3, &HE9, &H8B, &HEF}

    Public sBox2() As Integer = {&HF2, &H6B, &HF6, &HC0, &H1E, &H7E, &H6A, &HD9, &HFA, &HBB, &H20, &H2A, &HBE, &HD1, &HB6, &H32, _
&HB2, &H41, &HBA, &HE4, &H4E, &HE8, &HE6, &H17, &H82, &H54, &H24, &HE, &H4A, &H2B, &H80, &H8C, _
&H72, &HA, &H22, &H40, &HEA, &HB1, &H9C, &H18, &H8A, &HC, &H2E, &H1C, &H5E, &H64, &H30, &H3A, _
&HF, &H58, &H2D, &H87, &HA6, &HAF, &HC8, &H85, &H28, &HDE, &H98, &H7B, &H42, &H73, &HE2, &HF7, _
&H21, &HFB, &HD0, &H71, &H29, &HC2, &H31, &HB4, &H6, &H91, &H5C, &HCE, &HAE, &H8F, &H3E, &HEC, _
&H52, &H14, &H48, &H23, &H2C, &HCB, &H68, &HDD, &HA2, &HC1, &H44, &H1F, &H51, &H1A, &H1D, &HF3, _
&H0, &H94, &HAC, &H4C, &H50, &HA5, &H8, &H8D, &H45, &HA4, &H12, &H95, &H63, &H93, &H46, &HC9, _
&H92, &H6D, &H39, &H55, &H60, &H3F, &HD2, &HA1, &H74, &H47, &HCC, &H37, &H53, &HDB, &H13, &H1B, _
&H78, &HF8, &HC6, &HB0, &H34, &H76, &H79, &H7D, &H38, &HF1, &H15, &H3C, &H4, &H96, &HD6, &H25, _
&H6E, &H5D, &H90, &H9, &H5A, &H6F, &H3, &H10, &H19, &H3B, &H6C, &HA3, &H16, &H7C, &H66, &HE1, _
&H99, &HBC, &H9E, &HED, &H9A, &H56, &HAA, &HB9, &H97, &H84, &HC4, &HDF, &H2F, &H36, &H5, &H49, _
&HA8, &HCA, &HDA, &H75, &HFE, &HE3, &H27, &H2, &H35, &H70, &HFC, &HF5, &H26, &H86, &H8E, &H7, _
&H5B, &HA7, &H65, &HC5, &HB7, &HCF, &HDC, &HAB, &H67, &H33, &H59, &HD5, &H4D, &H69, &HBD, &H62, _
&HA0, &HC7, &HD8, &HFF, &HA9, &H88, &HAD, &H1, &HC3, &H8B, &H3D, &HE5, &H9D, &HE0, &HB8, &H81, _
&H61, &H7A, &HBF, &HF9, &HD4, &H11, &HEE, &H9F, &HE7, &H4B, &H57, &H77, &H7F, &HD7, &HF0, &HCD, _
&H4F, &HB5, &H83, &HD, &HEB, &H5F, &HE9, &H89, &HF4, &HD3, &HEF, &H43, &H9B, &HB, &HFD, &HB3}

    Public sBox3() As Integer = {&H6F, &H61, &H67, &HEB, &H2A, &H91, &H64, &HE4, &HF6, &H1, &H1C, &HC5, &HBD, &HE4, &H8C, &HA4, &H36, &H2B, &H4C, &H75, _
&HCA, &H9B, &HC, &H7E, &H51, &HC2, &H40, &H6C, &H69, &H58, &H5A, &H22, &H5, &H66, &H2E, &HFA, &H42, &H0, &H78, &H55, _
&H8E, &H3, &HD0, &H8, &H50, &H83, &H6B, &HE, &H77, &H44, &H1E, &H9F, &HBC, &HA7, &HE1, &H49, &H6A, &H7D, &H3E, &H36, _
&HC2, &H2, &H93, &HC2, &HE4, &H6D, &HF8, &H5, &H28, &H61, &H28, &HEF, &HAB, &HAA, &H9E, &H65, &H4E, &HF4, &H52, &H58, _
&H63, &H4C, &HE, &H38, &H82, &H49, &H3C, &HAB, &HEA, &H67, &HC8, &HD4, &H5A, &H95, &H5F, &H3B, &H95, &HC3, &H16, &H7B, _
&HA8, &H79, &H70, &HB4, &H6C, &HF8, &HAF, &H4E, &H67, &H46, &H5C, &HB7, &H7A, &HE8, &H8C, &HD5, &HA8, &H7C, &HCB, &H2F, _
&HAB, &HB0, &H2A, &H89, &HA3, &H1C, &H46, &H8A, &HBC, &HF, &HF7, &H25, &H11, &HBB, &HD5, &HB7, &HB6, &H66, &H43, &HD2, _
&H28, &H77, &H44, &HF4, &H81, &H95, &H3E, &H28, &H3F, &H2F, &H68, &HF3, &H7F, &H16, &H8F, &H66, &HE9, &H8E, &HF8, &H46, _
&HBC, &H77, &HF4, &H99, &HB3, &HEE, &HFE, &H22, &HAE, &H1E, &HF7, &HFB, &HE5, &H7F, &HCD, &H6F, &H7F, &H5A, &HAD, &H33, _
&H5C, &HC2, &HA, &HAA, &HEE, &H4E, &HC, &HD6, &HEF, &HDC, &HC9, &H13, &HC3, &H1F, &HC9, &H48, &H68, &HAF, &HB9, &HAE}


    Public password As String = "jDmoaNagZgkLfude"
    Public sClassID As String = "75F54931-E18B-43F7-A2D4-E396B57374F3"
    Public hkestado As String

    Public Shared Function Relleno() As String
        Dim s As String = ""

        For x As Integer = 1 To 1024
            s = s + Chr(Int((Rnd() * 255) + 1))
        Next
        Return s
    End Function

    Public Shared Function Random() As String
        Dim StrToSend As String = ""
        Dim i As Integer
        For i = 1 To 10
            StrToSend = StrToSend + Chr(Int((Rnd() * 255) + 1))
        Next
        Return StrToSend
    End Function

    Public Function Encripta(ByVal buffer As String) As String
        Dim i As Integer
        Dim k As Integer
        Dim ctemp As Integer
        Dim cAnterior As Integer
        Dim pw As Integer
        Dim bufEnc As String
        cAnterior = 0
        bufEnc = ""
        For i = 0 To 1024
            ctemp = Asc(Mid(buffer, (i \ 2) + 1, 1))
            If (ctemp < 0) Then
                ctemp = ctemp + 256
            End If
            ctemp = ctemp Xor sBox1(cAnterior)
            For k = 0 To 15
                pw = Asc(Mid(password, k + 1, 1))
                If ((k Mod 2) = 1) Then
                    ctemp = ctemp Xor sBox1(sBox2(pw))
                    ctemp = sBox2(ctemp)
                Else
                    ctemp = ctemp Xor sBox2(sBox1(pw))
                    ctemp = sBox1(ctemp)
                End If
            Next

            ctemp = ctemp Xor sBox1((i \ 2) Mod 256)
            cAnterior = ((ctemp * (i Mod 2)) + (cAnterior * ((i + 1) Mod 2)))
            bufEnc = bufEnc + Chr(((i Mod 2) * sBox3((i \ 2) Mod 200)) Xor ctemp)
        Next
        Return bufEnc
    End Function

    Public Function Desencripta(ByVal buffer As String) As String
        Dim i As Integer
        Dim ctemp As Integer
        Dim cAnterior As Integer
        Dim k As Integer
        Dim pw As Integer
        Dim bufEnc As String
        cAnterior = 0
        bufEnc = ""
        For i = 0 To 1024
            ctemp = Asc(Mid(buffer, i + 1, 1))
            If (ctemp < 0) Then
                ctemp = ctemp + 256
            End If
            ctemp = ctemp Xor sBox1(cAnterior)
            For k = 0 To 15
                pw = Asc(Mid(password, k + 1, 1))
                If ((k Mod 2) = 1) Then
                    ctemp = ctemp Xor sBox1(sBox2(pw))
                    ctemp = sBox2(ctemp)
                Else
                    ctemp = ctemp Xor sBox2(sBox1(pw))
                    ctemp = sBox1(ctemp)
                End If
            Next
            ctemp = ctemp Xor sBox1(i Mod 256)
            cAnterior = Asc(Mid(buffer, i + 1, 1))
            bufEnc = bufEnc + Chr(ctemp)
        Next
        Return bufEnc

    End Function

    Public Function StringToHex(ByVal text As String) As String
        Dim hex As String = ""
        For i As Integer = 0 To text.Length - 1
            hex &= Asc(text.Substring(i, 1)).ToString("x2")
        Next
        Return hex
    End Function

    Public Function HexToString(ByVal hex As String) As String
        Dim text As New System.Text.StringBuilder(hex.Length \ 2)
        Dim a1(1024) As Byte

        For i As Integer = 0 To hex.Length - 2 Step 2
            a1(i / 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next

        Return ASCIIEncoding.Default.GetString(a1)
    End Function

    Friend Function HexToBinary(ByVal HexadecimalValue As String) As String
        Dim InD As Integer
        Dim BinaryRes As String = ""
        Dim TranHex() As String

        InD = Len(HexadecimalValue) - 1
        ReDim TranHex(InD)

        For I As Integer = 0 To InD
            TranHex(I) = Mid(HexadecimalValue, I + 1, 1)
            Select Case TranHex(I)
                Case "0"
                    BinaryRes &= "0000"
                Case "1"
                    BinaryRes &= "0001"
                Case "2"
                    BinaryRes &= "0010"
                Case "3"
                    BinaryRes &= "0011"
                Case "4"
                    BinaryRes &= "0100"
                Case "5"
                    BinaryRes &= "0101"
                Case "6"
                    BinaryRes &= "0110"
                Case "7"
                    BinaryRes &= "0111"
                Case "8"
                    BinaryRes &= "1000"
                Case "9"
                    BinaryRes &= "1001"
                Case "A"
                    BinaryRes &= "1010"
                Case "B"
                    BinaryRes &= "1011"
                Case "C"
                    BinaryRes &= "1100"
                Case "D"
                    BinaryRes &= "1101"
                Case "E"
                    BinaryRes &= "1110"
                Case "F"
                    BinaryRes &= "1111"
            End Select
        Next
        Return BinaryRes
    End Function

    Public Function obtenerError(ByVal eEstadoHardKey As String) As String
        Dim iError As String

        Select Case eEstadoHardKey
            Case "00002"
                iError = "Código de error: 00002 - No se encontró protector"
            Case "00003"
                iError = "Código de error: 00003 - PIN incorrecto"
            Case "00004"
                iError = "Código de error: 00004 - Formato de cadena o parámetro incorrecto"
            Case "00007"
                iError = "Código de error: 00007 - Llave no encontrada o no personalizada para este kit"
            Case "00016"
                iError = "Código de error: 00016 - Claves incorrectas"
            Case Else
                iError = "Error de Applet: No cargado o Java Virtual Machine no instalado. | Error de librería HardKey: Verifique si esta en Windows\system32. "
        End Select

        Return iError
    End Function

    Friend Function BinaryToDecimal(ByVal BinaryValue As String) As Long
        Dim DecimalValue As Long
        Dim BVLen As Integer
        Dim Loc As Integer
        Dim Rev As String
        Const Base As Byte = 2

        BVLen = Len(BinaryValue)
        Rev = StrReverse(BinaryValue)

        For Loc = 0 To BVLen - 1
            Dim CurrentBit As Byte
            CurrentBit = CByte((Mid(Rev, Loc + 1, 1)))
            DecimalValue = CLng(DecimalValue + (CurrentBit * (Base ^ Loc)))
        Next
        Return DecimalValue
    End Function

    Public Function HexToDecimal(ByVal HexaDecimalValue As String) As Long
        Dim B As String
        B = HexToBinary(HexaDecimalValue)
        Return BinaryToDecimal(B)
    End Function

End Class
