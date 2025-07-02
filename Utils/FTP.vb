Imports System.IO
Imports System.Configuration
Imports System.Net
Imports System.Text
Imports di.financiera.excepciones

Public Class FTP

    Public Shared Sub subirArchivo(ByVal eDireccionFtp As String, eUsuarioFTP As String, eClaveFTP As String, ePathArchivo As String, eArchivo As String)
        Dim iFtpWebRequest As FtpWebRequest
        Dim iFtpWebResponse As FtpWebResponse
        Dim iFileStream As FileStream
        Dim iFileInfo As FileInfo
        Dim iStream As Stream

        Dim iBuffLength As Integer = 2048
        Dim iBuff(iBuffLength - 1) As Byte
        Dim iContentLen As Integer

        Try

            iFileInfo = New FileInfo(ePathArchivo)
            iFileStream = iFileInfo.OpenRead
            
            iFtpWebRequest = CType(System.Net.FtpWebRequest.Create(New Uri(eDireccionFtp.ToLower & eArchivo)), System.Net.FtpWebRequest)

            With iFtpWebRequest
                .Credentials = New NetworkCredential(eUsuarioFTP, eClaveFTP)
                .Method = WebRequestMethods.Ftp.UploadFile
                .Proxy = Nothing
                .KeepAlive = False
                .UseBinary = True
                .Timeout = 20000
                .ContentLength = iFileInfo.Length

                iStream = iFtpWebRequest.GetRequestStream()
                iContentLen = iFileStream.Read(iBuff, 0, iBuffLength)

                Do While iContentLen <> 0
                    iStream.Write(iBuff, 0, iContentLen)
                    iContentLen = iFileStream.Read(iBuff, 0, iBuffLength)
                Loop

            End With

        Catch exception As Exception
            Throw New FTPNoConectadoException(exception)
        Finally
            iFtpWebRequest = Nothing
            iFtpWebResponse = Nothing
            If Not IsNothing(iStream) Then
                iStream.Close()
                iStream.Dispose()
            End If
            If Not IsNothing(iFileStream) Then
                iFileStream.Close()
                iFileStream.Dispose()
            End If
        End Try
    End Sub

End Class
