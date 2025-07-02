Imports System.IO
Imports System.Configuration
Imports System.Web
Imports System.Net.Mail
Imports di.financiera.excepciones
Imports SendGrid
Imports SendGrid.Helpers.Mail
Imports System.Threading.Tasks
Imports di.financiera.datos

Public Class EnviaMail

    Public Shared Sub enviarMail(ByVal eDireccionDestino As String, ByVal eMotivo As String, ByVal eCuerpo As String, ByVal ePathArchivo As String, ByVal eHtml As Boolean, ByVal eNombreDireccionSalida As String, ByVal eClienteSMTP As SmtpClient, ByVal eDireccionSalida As String)
        Dim iColeccion As New Collection

        Try

            If ePathArchivo <> Nothing Then
                iColeccion.Add(ePathArchivo)
            End If

            enviarMail(eDireccionDestino, eMotivo, eCuerpo, iColeccion, Nothing, Nothing, eHtml, eClienteSMTP, eDireccionSalida)

        Catch exception As Exception
            Throw New MailNoEnviadoException(exception)
        Finally
            iColeccion = Nothing
        End Try
    End Sub

    Public Shared Sub enviarMail(ByVal eDireccionDestino As String, ByVal eMotivo As String, ByVal eCuerpo As String, Optional ByVal eHtml As Boolean = False)
        Dim iColeccion As New Collection

        Try
            enviarMail(eDireccionDestino, eMotivo, eCuerpo, iColeccion, Nothing, Nothing, eHtml)

        Catch exception As Exception
            Throw New MailNoEnviadoException(exception)
        Finally
            iColeccion = Nothing
        End Try
    End Sub

    Public Shared Sub enviarMail(ByVal eDireccionDestino As String, ByVal eMotivo As String, ByVal eCuerpo As String, ByVal ePathArchivo As String, Optional ByVal eHtml As Boolean = False)
        Dim iColeccion As New Collection

        Try

            If ePathArchivo <> Nothing Then
                iColeccion.Add(ePathArchivo)
            End If

            enviarMail(eDireccionDestino, eMotivo, eCuerpo, iColeccion, Nothing, Nothing, eHtml)

        Catch exception As Exception
            Throw New MailNoEnviadoException(exception)
        Finally
            iColeccion = Nothing
        End Try
    End Sub

    Public Shared Sub enviarMail(ByVal eDireccionDestino As String, ByVal eMotivo As String, ByVal eCuerpo As String, ByVal eColeccionArchivos As Collection, Optional eEmail As Email = Nothing, Optional eNombreDireccionSalida As String = Nothing, Optional ByVal eHtml As Boolean = False, Optional eClienteSMTP As SmtpClient = Nothing, Optional eDireccionSalida As String = Nothing)
        Dim iSendGridMessage As SendGridMessage
        Dim iDireccionDestino As EmailAddress
        Dim iDireccionOrigen As EmailAddress
        Dim iSendGrid As SendGridClient
        Dim iClienteSMTP As SmtpClient
        Dim iCorreo As MailMessage
        Dim i As Integer
        Dim iError As Boolean = False
        Dim iEnviado As Boolean = False
        Dim iApiKey As String

        Try

            If CType(ConfigurationManager.AppSettings("enviaMails"), Boolean) Then
                If ConfigurationManager.AppSettings("usaSendGrid") <> "" AndAlso CType(ConfigurationManager.AppSettings("usaSendGrid"), Boolean) Then

                    iApiKey = ConfigurationManager.AppSettings("sendGridApiKey")
                    iSendGrid = New SendGridClient(iApiKey)
                    iDireccionOrigen = New EmailAddress(ConfigurationManager.AppSettings("direccionSalida").ToString)
                    iDireccionDestino = New EmailAddress(eDireccionDestino)

                    iSendGridMessage = New SendGridMessage
                    iSendGridMessage = MailHelper.CreateSingleEmail(iDireccionOrigen, iDireccionDestino, eMotivo, eCuerpo, eCuerpo)
                    iSendGrid.SendEmailAsync(iSendGridMessage)

                Else
                    iClienteSMTP = New SmtpClient
                    iCorreo = New MailMessage

                    If eDireccionDestino.Contains(";") Then
                        Dim iDireccion As String()
                        iDireccion = eDireccionDestino.Split(";")
                        For i = 0 To iDireccion.Length - 1
                            iCorreo.To.Add(iDireccion(i))
                        Next
                    ElseIf eDireccionDestino.Contains(",") Then
                        Dim iDireccion As String()
                        iDireccion = eDireccionDestino.Split(",")
                        For i = 0 To iDireccion.Length - 1
                            iCorreo.To.Add(iDireccion(i))
                        Next
                    Else
                        iCorreo.To.Add(eDireccionDestino)
                    End If

                    iCorreo.IsBodyHtml = eHtml
                    iCorreo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

                    If Not IsNothing(eColeccionArchivos) AndAlso eColeccionArchivos.Count > 0 Then
                        For i = 1 To eColeccionArchivos.Count
                            If File.Exists(eColeccionArchivos.Item(i)) Then
                                If ((FileSystem.FileLen(eColeccionArchivos.Item(i)) / 1024) / 1024) > 25 Then iError = True
                                iCorreo.Attachments.Add(New Net.Mail.Attachment(eColeccionArchivos.Item(i)))
                            End If
                        Next i
                    End If

                    iCorreo.Subject = eMotivo
                    If eCuerpo <> Nothing Then iCorreo.Body = eCuerpo

                    If IsNothing(eClienteSMTP) Then
                        iClienteSMTP.Timeout = 200000
                        If InStr(eDireccionDestino.Substring(InStr(eDireccionDestino, "@")), ".") > 0 Then
                            iClienteSMTP.Host = ConfigurationManager.AppSettings("servidorSmtp")
                            iClienteSMTP.Port = ConfigurationManager.AppSettings("puertoSalida")
                            iClienteSMTP.UseDefaultCredentials = False
                            iClienteSMTP.Credentials = New Net.NetworkCredential(ConfigurationManager.AppSettings("userName"), ConfigurationManager.AppSettings("sendPassword"))
                            iClienteSMTP.EnableSsl = ConfigurationManager.AppSettings("ssl")
                            If eNombreDireccionSalida <> Nothing Then
                                iCorreo.From = New MailAddress(ConfigurationManager.AppSettings("direccionSalida"), eNombreDireccionSalida, System.Text.Encoding.UTF8)
                            Else
                                iCorreo.From = New MailAddress(ConfigurationManager.AppSettings("direccionSalida"), ConfigurationManager.AppSettings("nombreDireccionSalida"), System.Text.Encoding.UTF8)
                            End If
                        Else
                            iClienteSMTP.Host = ConfigurationManager.AppSettings("servidorSmtpLocal")
                            iClienteSMTP.Port = ConfigurationManager.AppSettings("puertoSalidaLocal")
                            iClienteSMTP.UseDefaultCredentials = False
                            iClienteSMTP.Credentials = New Net.NetworkCredential(ConfigurationManager.AppSettings("userNameLocal"), ConfigurationManager.AppSettings("sendPasswordLocal"))
                            iClienteSMTP.EnableSsl = ConfigurationManager.AppSettings("ssllocal")
                            If eNombreDireccionSalida <> Nothing Then
                                iCorreo.From = New MailAddress(ConfigurationManager.AppSettings("direccionSalidalocal"), eNombreDireccionSalida, System.Text.Encoding.UTF8)
                            Else
                                iCorreo.From = New MailAddress(ConfigurationManager.AppSettings("direccionSalidalocal"), ConfigurationManager.AppSettings("nombreDireccionSalidalocal"), System.Text.Encoding.UTF8)
                            End If
                        End If
                        If iError Then Throw New SmtpException("Archivos demasiado grandes.")
                        iClienteSMTP.Send(iCorreo)
                    Else
                        iCorreo.From = New MailAddress(eDireccionSalida, eNombreDireccionSalida, System.Text.Encoding.UTF8)

                        eClienteSMTP.Send(iCorreo)
                    End If

                    iEnviado = True
                End If
            End If

        Catch SmtpException As SmtpException
            iError = True
            FuncionComun.loguearErrores("ERROR MAIL:" & FuncionComun.obtenerMotivoOriginal(SmtpException))
            'Throw New MailNoEnviadoException("El envío de email no ha sido realizado correctamente. Intente enviar el email desde la copia de email o bien descargar el archivo adjunto.")
        Catch exception As Exception
            iError = True
            FuncionComun.loguearErrores("ERROR MAIL:" & FuncionComun.obtenerMotivoOriginal(exception))
            'Throw New MailNoEnviadoException(exception)
        Finally
            Try
                If Not IsNothing(eEmail) Then
                    actualizaEmail(eEmail, iEnviado, iError)
                ElseIf CType(ConfigurationManager.AppSettings("guardaCopiaMails"), Boolean) OrElse iError Then
                    Dim iAccesoDatos As accesoDatos
                    Try
                        iAccesoDatos = New accesoDatos()
                        iAccesoDatos.beginTransaction()
                        guardarCopiaEmail(iAccesoDatos, eDireccionDestino, eMotivo, eCuerpo, eColeccionArchivos, iEnviado, iError)
                        iAccesoDatos.commit()
                    Catch exception As Exception
                        iAccesoDatos.rollback()
                    Finally
                        iAccesoDatos.cerrar()
                        iAccesoDatos = Nothing
                    End Try
                End If
            Catch ex As Exception
                If iError Then
                    Throw New MailNoEnviadoException(ex)
                End If
            End Try
            If Not IsNothing(iClienteSMTP) Then iClienteSMTP.Dispose()
            iClienteSMTP = Nothing
            If Not IsNothing(iCorreo) Then iCorreo.Dispose()
            iCorreo = Nothing
            iSendGridMessage = Nothing
            iDireccionDestino = Nothing
            iDireccionOrigen = Nothing
            iSendGrid = Nothing
        End Try
    End Sub

    Public Shared Sub guardarCopiaEmail(eAccesoDatos As accesoDatos, ByVal eDireccionDestino As String, ByVal eMotivo As String, ByVal eCuerpo As String, ByVal ePathArchivo As String, ByVal eEnviado As Boolean, ByVal eError As Boolean)
        Dim iColeccion As New Collection

        Try

            If ePathArchivo <> Nothing Then
                iColeccion.Add(ePathArchivo)
            End If

            guardarCopiaEmail(eAccesoDatos, eDireccionDestino, eMotivo, eCuerpo, iColeccion, eEnviado, eError)

        Catch exception As Exception
            Throw New MailNoEnviadoException(exception)
        Finally
            iColeccion = Nothing
        End Try
    End Sub

    Private Shared Sub guardarCopiaEmail(eAccesoDatos As accesoDatos, ByVal eDireccionDestino As String, ByVal eMotivo As String, ByVal eCuerpo As String, ByVal eColeccionArchivos As Collection, ByVal eEnviado As Boolean, ByVal eError As Boolean)
        Dim iEmail As New Email

        Try
            With iEmail
                .direccionDestino = eDireccionDestino
                .fechaEnvio = Now
                .horaEnvio = New TimeSpan(Format(Now, "HH"), Format(Now, "mm"), Format(Now, "ss"))
                .motivo = eMotivo
                .cuerpo = eCuerpo
                .adjuntos = eColeccionArchivos
                If eError Then
                    .accion = Email.CONERROR
                ElseIf eEnviado Then
                    .accion = Email.ENVIADO
                Else
                    .accion = Email.NOENVIADO
                End If
            End With
            iEmail.accesoDatos = eAccesoDatos
            iEmail.crear()
            iEmail.accesoDatos = Nothing
        Catch exception As Exception
            FuncionComun.loguearErrores("ERROR GUARDAR MAIL:" & FuncionComun.obtenerMotivoOriginal(exception))
            Throw New MailNoEnviadoException(exception)
        Finally
            iEmail = Nothing
        End Try
    End Sub

    Public Shared Sub actualizaEmail(ByVal eEmail As Email, ByVal eEnviado As Boolean, ByVal eError As Boolean)
        Try
            With eEmail
                If eError Then
                    .accion = Email.CONERROR
                ElseIf eEnviado Then
                    .accion = Email.ENVIADO
                Else
                    .accion = Email.NOENVIADO
                End If
            End With

            eEmail.modificar()

        Catch exception As Exception
            FuncionComun.loguearErrores("ERROR GUARDAR MAIL:" & FuncionComun.obtenerMotivoOriginal(exception))
            Throw New MailNoEnviadoException(exception)
        Finally

        End Try
    End Sub

End Class
