Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Imports System.Configuration
Imports System.IO
Imports System.Net.Mail
 
Public Class Version 
    Inherits Entidad

#Region "Version"
    Public Const NUMEROVERSION As String = "5.0.0.0001"
    Public Const FECHAVERSION As String = "15/11/2023"
    Public Const EMAILVERSION As String = "soportehd@divinf.com.ar;marcosgonzalez@divinf.com.ar;danielaraujo@divinf.com.ar;requerimientos@divinf.com.ar;brunovillasanti@divinf.com.ar;dariopettigrosso@divinf.com.ar"
#End Region

#Region "Variables"
    Private iId As Long
    Private iNumero As String
    Private iFecha As String
    Private iFechaCambio As Date
    Private iHoraCambio As TimeSpan
    Private iConfiguracionMail As String

    Private iConexion As accesoDatos
#End Region

#Region "Atributos"
    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property
    Public Property numero() As String
        Get
            Return iNumero
        End Get
        Set(ByVal Value As String)
            iNumero = Value
        End Set
    End Property
    Public Property fecha() As String
        Get
            Return iFecha
        End Get
        Set(ByVal Value As String)
            iFecha = Value
        End Set
    End Property
    Public Property fechaCambio() As Date
        Get
            Return iFechaCambio
        End Get
        Set(ByVal Value As Date)
            iFechaCambio = Value
        End Set
    End Property
    Public Property horaCambio() As TimeSpan
        Get
            Return iHoraCambio
        End Get
        Set(ByVal Value As TimeSpan)
            iHoraCambio = Value
        End Set
    End Property
    Public Property configuracionMail() As String
        Get
            Return iConfiguracionMail
        End Get
        Set(ByVal Value As String)
            iConfiguracionMail = Value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function obtenerVersion() As Version
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql()

        Try

            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Version")
            iGeneradorSql.agregarColumna("max(id) as id")

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.ceroSiEsNulo(iDataReader.Item("id"))
            End If

            iDataReader.Close()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("Numero")
            iGeneradorSql.agregarColumna("Fecha")
            iGeneradorSql.agregarColumna("FechaCambio")
            iGeneradorSql.agregarColumna("HoraCambio")
            iGeneradorSql.agregarColumna("ConfiguracionMail")

            iGeneradorSql.agregarTabla("Version")

            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = iDataReader.Item("id").ToString
                iNumero = iDataReader.Item("numero").ToString()
                iFecha = iDataReader.Item("fecha").ToString()
                iFechaCambio = FuncionComun.nothingSiEsNulo(iDataReader.Item("fechaCambio"))
                iHoraCambio = iDataReader.Item("horaCambio")
                iConfiguracionMail = iDataReader.Item("ConfiguracionMail").ToString()

                iDataReader.Close()

            End If

            Return Me

        Catch LocalidadNoEncontradaException As VersionNoEncontradaException
            Throw LocalidadNoEncontradaException
        Catch excepcion As Exception
            Throw New VersionNoEncontradaException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql
        Dim iVersion As New Version

        Try

            iConexion = obtenerConexion()

            If Not Debugger.IsAttached Then

                iVersion.accesoDatos = iConexion
                iVersion = obtenerVersion()
                iVersion.accesoDatos = Nothing

                If iVersion.numero <> NUMEROVERSION OrElse iVersion.fecha <> FECHAVERSION Then

                    iHoraCambio = New TimeSpan(Format(Now, "HH"), Format(Now, "mm"), Format(Now, "ss"))

                    iConfiguracionMail = ConfigurationManager.AppSettings("servidorSmtp").ToString() & "|"
                    iConfiguracionMail &= ConfigurationManager.AppSettings("puertoSalida").ToString() & "|"
                    iConfiguracionMail &= ConfigurationManager.AppSettings("userName").ToString() & "|"
                    iConfiguracionMail &= ConfigurationManager.AppSettings("sendPassword").ToString() & "|"
                    iConfiguracionMail &= ConfigurationManager.AppSettings("ssl").ToString() & "|"
                    iConfiguracionMail &= ConfigurationManager.AppSettings("direccionSalida").ToString() & "|"
                    iConfiguracionMail &= ConfigurationManager.AppSettings("nombreDireccionSalida").ToString() & "|"
                    iConfiguracionMail &= EMAILVERSION

                    iGeneradorSql.agregarColumna("Numero")
                    iGeneradorSql.agregarColumna("Fecha")
                    iGeneradorSql.agregarColumna("FechaCambio")
                    iGeneradorSql.agregarColumna("HoraCambio")
                    iGeneradorSql.agregarColumna("configuracionMail")

                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(NUMEROVERSION))
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(FECHAVERSION))
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(Today))
                    iGeneradorSql.agregarValue("'" & iHoraCambio.ToString & "'")
                    iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(iConfiguracionMail))

                    iGeneradorSql.agregarTabla("Version")

                    iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

                    validarEstrucuturaDirectoriosSistema(iVersion)

                End If

            End If

        Catch excepcion As Exception
            Throw New VersionNoCreadaException(excepcion)
        Finally
            iVersion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub validarEstrucuturaDirectoriosSistema(eVersion As Version)
        Dim iDirectorio As DirectoryInfo
        Dim iVersion As New Version
        Dim iCantidadDirectorioHmologacion As Integer
        Dim iCantidadDirectorioProduccion As Integer
        Dim iDirectorioError As Boolean = False
        Dim iDirectorioHomologacion As String
        Dim iCantidadDirectorioTest As Integer
        Dim iDirectorioProduccion As String
        Dim iDirectorioTest As String
        Dim iPathArchivos As String()
        Dim iPathArchivo As String
        Dim iRaiz As String
        Dim iWeb As String
        Dim iMensaje As String
        Dim i As Integer

        Try

            'Al directorio le resto las dos ultima partes de la ruta, eso nos va a indicar donde esta el directorio raiz
            iPathArchivos = Split(ConfigurationManager.AppSettings("archivosGenerados"), "\")

            iWeb = iPathArchivos(iPathArchivos.Length - 3).ToUpper()
            For i = 0 To iPathArchivos.Length - 4
                iPathArchivo &= iPathArchivos(i) & "\"
                iRaiz = iPathArchivos(i).ToUpper
            Next

            Select Case iRaiz.ToUpper
                Case "TEST", "HOMOLOGACION"
                    'no hago nada
                    iCantidadDirectorioProduccion = 4
                    iCantidadDirectorioHmologacion = 5
                    iCantidadDirectorioTest = 5
                Case "WWWROOT"
                    iDirectorio = New DirectoryInfo(iPathArchivo)

                    For Each iSubDirectorio As DirectoryInfo In iDirectorio.GetDirectories()

                        Select Case iSubDirectorio.Name.ToUpper()
                            Case "TEST"
                                For Each iSubSubDirectorio As DirectoryInfo In iSubDirectorio.GetDirectories()
                                    Select Case iSubSubDirectorio.Name.ToUpper()
                                        Case "SERVICIOSAPI"
                                            iDirectorioTest &= vbTab & "SERVICIOSAPI" & vbNewLine
                                            iCantidadDirectorioTest += 1
                                        Case "SERVICIOSWEB"
                                            iDirectorioTest &= vbTab & "SERVICIOSWEB" & vbNewLine
                                            iCantidadDirectorioTest += 1
                                        Case "SERVICIOWCF"
                                            iDirectorioTest &= vbTab & "SERVICIOWCF" & vbNewLine
                                            iCantidadDirectorioTest += 1
                                        Case "PROCESOS"
                                            iDirectorioTest &= vbTab & "PROCESOS" & vbNewLine
                                            iCantidadDirectorioTest += 1
                                        Case "WEB"
                                            iDirectorioTest &= vbTab & "WEB" & vbNewLine
                                            iCantidadDirectorioTest += 1
                                    End Select
                                Next
                            Case "HOMOLOGACION"
                                For Each iSubSubDirectorio As DirectoryInfo In iSubDirectorio.GetDirectories()
                                    Select Case iSubSubDirectorio.Name.ToUpper()
                                        Case "SERVICIOSAPI"
                                            iDirectorioHomologacion &= vbTab & "SERVICIOSAPI" & vbNewLine
                                            iCantidadDirectorioHmologacion += 1
                                        Case "SERVICIOSWEB"
                                            iDirectorioHomologacion &= vbTab & "SERVICIOSWEB" & vbNewLine
                                            iCantidadDirectorioHmologacion += 1
                                        Case "SERVICIOWCF"
                                            iDirectorioHomologacion &= vbTab & "SERVICIOWCF" & vbNewLine
                                            iCantidadDirectorioHmologacion += 1
                                        Case "PROCESOS"
                                            iDirectorioHomologacion &= vbTab & "PROCESOS" & vbNewLine
                                            iCantidadDirectorioHmologacion += 1
                                        Case "WEB"
                                            iDirectorioHomologacion &= vbTab & "WEB" & vbNewLine
                                            iCantidadDirectorioHmologacion += 1
                                    End Select
                                Next
                            Case "SERVICIOSAPI"
                                iDirectorioProduccion &= vbTab & "SERVICIOSAPI" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case "SERVICIOSWEB"
                                iDirectorioProduccion &= vbTab & "SERVICIOSWEB" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case "SERVICIOWCF"
                                iDirectorioProduccion &= vbTab & "SERVICIOWCF" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case "PROCESOS"
                                iDirectorioProduccion &= vbTab & "PROCESOS" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case iWeb
                                iDirectorioProduccion &= vbTab & iSubDirectorio.Name.ToUpper() & vbNewLine
                        End Select

                    Next
                Case Else
                    iDirectorio = New DirectoryInfo(iPathArchivo)
                    For Each iSubDirectorio As DirectoryInfo In iDirectorio.GetDirectories()
                        Select Case iSubDirectorio.Name.ToUpper()
                            Case "SERVICIOSAPI"
                                iDirectorioProduccion &= vbTab & "SERVICIOSAPI" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case "SERVICIOSWEB"
                                iDirectorioProduccion &= vbTab & "SERVICIOSWEB" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case "SERVICIOWCF"
                                iDirectorioProduccion &= vbTab & "SERVICIOWCF" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case "PROCESOS"
                                iDirectorioProduccion &= vbTab & "PROCESOS" & vbNewLine
                                iCantidadDirectorioProduccion += 1
                            Case iWeb
                                iDirectorioProduccion &= vbTab & iSubDirectorio.Name.ToUpper() & vbNewLine
                        End Select
                    Next

                    iPathArchivos = Split(iPathArchivo, "\")
                    For i = 0 To iPathArchivos.Length - 3
                        iPathArchivo &= iPathArchivos(i) & "\"
                        iRaiz = iPathArchivos(i).ToUpper
                    Next
                    If iRaiz = "WWWROOT" Then
                        iDirectorio = New DirectoryInfo(iPathArchivo)

                        For Each iSubDirectorio As DirectoryInfo In iDirectorio.GetDirectories()

                            Select Case iSubDirectorio.Name.ToUpper()
                                Case "TEST"
                                    For Each iSubSubDirectorio As DirectoryInfo In iSubDirectorio.GetDirectories()
                                        Select Case iSubSubDirectorio.Name.ToUpper()
                                            Case "SERVICIOSAPI"
                                                iDirectorioTest &= vbTab & "SERVICIOSAPI" & vbNewLine
                                                iCantidadDirectorioTest += 1
                                            Case "SERVICIOSWEB"
                                                iDirectorioTest &= vbTab & "SERVICIOSWEB" & vbNewLine
                                                iCantidadDirectorioTest += 1
                                            Case "SERVICIOWCF"
                                                iDirectorioTest &= vbTab & "SERVICIOWCF" & vbNewLine
                                                iCantidadDirectorioTest += 1
                                            Case "PROCESOS"
                                                iDirectorioTest &= vbTab & "PROCESOS" & vbNewLine
                                                iCantidadDirectorioTest += 1
                                            Case "WEB"
                                                iDirectorioTest &= vbTab & "WEB" & vbNewLine
                                                iCantidadDirectorioTest += 1
                                        End Select
                                    Next
                                Case "HOMOLOGACION"
                                    For Each iSubSubDirectorio As DirectoryInfo In iSubDirectorio.GetDirectories()
                                        Select Case iSubSubDirectorio.Name.ToUpper()
                                            Case "SERVICIOSAPI"
                                                iDirectorioHomologacion &= vbTab & "SERVICIOSAPI" & vbNewLine
                                                iCantidadDirectorioHmologacion += 1
                                            Case "SERVICIOSWEB"
                                                iDirectorioHomologacion &= vbTab & "SERVICIOSWEB" & vbNewLine
                                                iCantidadDirectorioHmologacion += 1
                                            Case "SERVICIOWCF"
                                                iDirectorioHomologacion &= vbTab & "SERVICIOWCF" & vbNewLine
                                                iCantidadDirectorioHmologacion += 1
                                            Case "PROCESOS"
                                                iDirectorioHomologacion &= vbTab & "PROCESOS" & vbNewLine
                                                iCantidadDirectorioHmologacion += 1
                                            Case "WEB"
                                                iDirectorioHomologacion &= vbTab & "WEB" & vbNewLine
                                                iCantidadDirectorioHmologacion += 1
                                        End Select
                                    Next
                            End Select
                        Next
                    End If
            End Select

            'Verifico si la carpeta que contiene la version de produccion es "loan" bajo un nivel mas para buscar los demas archivos
            If iCantidadDirectorioProduccion <> 4 OrElse iCantidadDirectorioHmologacion <> 5 OrElse iCantidadDirectorioTest <> 5 Then

                iMensaje = "**************************************" & vbNewLine
                iMensaje &= "ESTRUCTURA ACTUAL DEL SERVER     " & vbNewLine
                iMensaje &= "**************************************" & vbNewLine

                iMensaje &= "**************************************" & vbNewLine
                iMensaje &= "PRODUCCION: " & IIf(iCantidadDirectorioProduccion <> 4, "INCOMPLETO", "COMPLETO") & vbNewLine
                iMensaje &= "**************************************" & vbNewLine
                iMensaje &= iDirectorioProduccion & vbNewLine
                If iCantidadDirectorioProduccion <> 4 Then
                    iMensaje &= "**************************************" & vbNewLine
                    iMensaje &= "ESTRUCTURA CORRECTA" & vbNewLine
                    iMensaje &= "\SERVICIOSWEB" & vbNewLine
                    iMensaje &= "\SERVICIOWCF" & vbNewLine
                    iMensaje &= "\SERVICIOSAPI" & vbNewLine
                    iMensaje &= "\PROCESOS" & vbNewLine
                    iMensaje &= "\" & iWeb & vbNewLine
                    iMensaje &= "**************************************" & vbNewLine & vbNewLine
                End If

                iMensaje &= "**************************************" & vbNewLine
                iMensaje &= "TEST: " & IIf(iCantidadDirectorioTest <> 5, "INCOMPLETO", "COMPLETO") & vbNewLine
                iMensaje &= "**************************************" & vbNewLine & vbNewLine
                iMensaje &= iDirectorioTest & vbNewLine

                If iCantidadDirectorioTest <> 5 Then
                    iMensaje &= "**************************************" & vbNewLine
                    iMensaje &= "ESTRUCTURA CORRECTA" & vbNewLine
                    iMensaje &= "\TEST\SERVICIOSWEB" & vbNewLine
                    iMensaje &= "\TEST\SERVICIOWCF" & vbNewLine
                    iMensaje &= "\TEST\SERVICIOSAPI" & vbNewLine
                    iMensaje &= "\TEST\PROCESOS" & vbNewLine
                    iMensaje &= "\TEST\WEB" & vbNewLine
                    iMensaje &= "**************************************" & vbNewLine & vbNewLine
                End If

                iMensaje &= "**************************************" & vbNewLine
                iMensaje &= "HOMOLOGACION: " & IIf(iCantidadDirectorioHmologacion <> 5, "INCOMPLETO", "COMPLETO") & vbNewLine
                iMensaje &= "**************************************" & vbNewLine
                iMensaje &= iDirectorioHomologacion & vbNewLine & vbNewLine

                If iCantidadDirectorioHmologacion <> 5 Then
                    iMensaje &= "**************************************" & vbNewLine
                    iMensaje &= "ESTRUCTURA CORRECTA" & vbNewLine
                    iMensaje &= "\HOMOLOGACION\SERVICIOSWEB" & vbNewLine
                    iMensaje &= "\HOMOLOGACION\SERVICIOWCF" & vbNewLine
                    iMensaje &= "\HOMOLOGACION\SERVICIOSAPI" & vbNewLine
                    iMensaje &= "\HOMOLOGACION\PROCESOS" & vbNewLine
                    iMensaje &= "\HOMOLOGACION\WEB" & vbNewLine
                    iMensaje &= "**************************************" & vbNewLine & vbNewLine
                End If

                iMensaje &= "Por favor corregir la estructura para el correcto funcionamiento del sistema."

                enviarEmail(eVersion, "Estructura del server incompleta", iMensaje)

            End If

        Catch exception As Exception
            FuncionComun.loguearProcesoVersion(Format(Now, "dd/MM/yyyy HH:mm:ss") & vbTab & "VALIDACION ESTRUCTURA" & vbTab & iMensaje & vbTab & FuncionComun.obtenerMotivoOriginal(exception))
        Finally
            iDirectorio = Nothing
            iVersion = Nothing
        End Try
    End Sub

    Public Shadows Sub enviarEmail(eVersion As Version, eSubject As String, eBody As String)
        Dim iClienteSMTP As New SmtpClient
        Dim iCorreo As New MailMessage
        Dim iConfiguracionEmail As String()
        Dim i As Integer

        Try

            iConfiguracionEmail = Split(eVersion.configuracionMail, "|")

            If iConfiguracionEmail(7).Contains(";") Then
                Dim iDireccion As String()
                iDireccion = iConfiguracionEmail(7).Split(";")
                For i = 0 To iDireccion.Length - 1
                    iCorreo.To.Add(iDireccion(i))
                Next
            ElseIf iConfiguracionEmail(7).Contains(",") Then
                Dim iDireccion As String()
                iDireccion = iConfiguracionEmail(7).Split(",")
                For i = 0 To iDireccion.Length - 1
                    iCorreo.To.Add(iDireccion(i))
                Next
            Else
                iCorreo.To.Add(iConfiguracionEmail(7))
            End If
            iCorreo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

            iCorreo.Subject = eSubject
            iCorreo.Body = eBody

            iClienteSMTP.Timeout = 200000
            iClienteSMTP.Host = iConfiguracionEmail(0)
            iClienteSMTP.Port = iConfiguracionEmail(1)
            iClienteSMTP.UseDefaultCredentials = True
            iClienteSMTP.Credentials = New Net.NetworkCredential(iConfiguracionEmail(2), iConfiguracionEmail(3))
            iClienteSMTP.EnableSsl = iConfiguracionEmail(4)
            iCorreo.From = New MailAddress(iConfiguracionEmail(5), iConfiguracionEmail(6), System.Text.Encoding.UTF8)

            iClienteSMTP.Send(iCorreo)

        Catch exception As Exception
            FuncionComun.loguearProcesoVersion(Format(Now, "dd/MM/yyyy HH:mm:ss") & vbTab & eSubject & vbTab & eBody & vbTab & FuncionComun.obtenerMotivoOriginal(exception))
            'intento enviar mail por otro servidor
            iClienteSMTP = New SmtpClient
            iCorreo = New MailMessage

            Try

                iConfiguracionEmail = Split(eVersion.configuracionMail, "|")

                If iConfiguracionEmail(7).Contains(";") Then
                    Dim iDireccion As String()
                    iDireccion = iConfiguracionEmail(7).Split(";")
                    For i = 0 To iDireccion.Length - 1
                        iCorreo.To.Add(iDireccion(i))
                    Next
                ElseIf iConfiguracionEmail(7).Contains(",") Then
                    Dim iDireccion As String()
                    iDireccion = iConfiguracionEmail(7).Split(",")
                    For i = 0 To iDireccion.Length - 1
                        iCorreo.To.Add(iDireccion(i))
                    Next
                Else
                    iCorreo.To.Add(iConfiguracionEmail(7))
                End If
                iCorreo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

                iCorreo.Subject = eSubject

                eBody = "La configuracion del email del cliente no esta funcionando. " & vbNewLine & eBody

                iCorreo.Body = eBody

                iClienteSMTP.Timeout = 200000
                iClienteSMTP.Host = "smtp.divinf.com.ar"
                iClienteSMTP.Port = 25
                iClienteSMTP.UseDefaultCredentials = True
                iClienteSMTP.Credentials = New Net.NetworkCredential("soportehd@divinf.com.ar", "Soporte1422")
                iClienteSMTP.EnableSsl = False
                iCorreo.From = New MailAddress("soportehd@divinf.com.ar", iConfiguracionEmail(6), System.Text.Encoding.UTF8)

                iClienteSMTP.Send(iCorreo)

            Catch ex As Exception
                FuncionComun.loguearProcesoVersion(Format(Now, "dd/MM/yyyy HH:mm:ss") & vbTab & eSubject & vbTab & eBody & vbTab & FuncionComun.obtenerMotivoOriginal(ex))
            End Try
        Finally
            iClienteSMTP = Nothing
            iCorreo = Nothing
        End Try
    End Sub

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub
#End Region

End Class