Imports System.Runtime.InteropServices

Public Class ParametroIni

#Region "Funcion para leer ini"


    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpValor As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer


    Public Shared Function obtenerIni(ByVal eNombreAplicacion As String, ByVal eClave As String, ByVal ePathArchivo As String) As String
        Dim iResultado As String
        Dim iRetorno As Integer
        Try
            iResultado = New String(Chr(0), 255)

            iRetorno = GetPrivateProfileString(eNombreAplicacion, eClave, vbNullString, iResultado, 255, ePathArchivo)



            Return Left(iResultado, iRetorno)

        Catch exception As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function obtenerIniSeccion(ByVal ePathArchivo As String, ByVal eSeccion As String) As String()
        '--------------------------------------------------------------------------
        ' Lee una sección entera de un fichero INI                      (27/Feb/99)
        ' Adaptada para devolver un array de string                     (04/Abr/01)
        '
        ' Esta función devolverá un array de índice cero
        ' con las claves y valores de la sección
        '
        ' Parámetros de entrada:
        '   sFileName   Nombre del fichero INI
        '   sSection    Nombre de la sección a leer
        ' Devuelve:
        '   Un array con el nombre de la clave y el valor
        '   Para leer los datos:
        '       For i = 0 To UBound(elArray) -1 Step 2
        '           sClave = elArray(i)
        '           sValor = elArray(i+1)
        '       Next
        Dim iResultado As String
        Dim iParametros() As String
        Dim iRetorno As Integer
        '
        ReDim iParametros(0)
        '
        ' El tamaño máximo para Windows 95
        iResultado = New String(ChrW(0), 32767)
        '
        iRetorno = GetPrivateProfileSection(eSeccion, iResultado, iResultado.Length, ePathArchivo)
        '
        If iRetorno > 0 Then
            '
            ' Cortar la cadena al número de caracteres devueltos
            ' menos los dos últimos que indican el final de la cadena
            iResultado = iResultado.Substring(0, iRetorno - 1).TrimEnd()
            ' Cada elemento estará separado por un Chr(0)
            ' y cada valor estará en la forma: clave = valor
            iParametros = iResultado.Split(New Char() {ChrW(0), "="c})
        End If
        ' Devolver el array
        Return iParametros
    End Function

    Public Shared Sub grabarIni(ByVal eNombreAplicacion As String, ByVal eClave As String, ByVal ePathArchivo As String, ByVal eValor As String)
        Try
            WritePrivateProfileString(eNombreAplicacion, eClave, eValor, 255, ePathArchivo)

        Catch exception As Exception
            Throw exception
        End Try
    End Sub
#End Region

End Class
