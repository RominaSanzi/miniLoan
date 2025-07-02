Imports System
Imports WinSCP
Imports System.Globalization
Imports System.IO
Imports System.Configuration
Imports di.financiera.excepciones

Public Class SFTP

#Region "Variables"
    Private iProtocol As Protocol
    Private iHostName As String
    Private iPortNumber As Integer
    Private iUserName As String
    Private iPassword As String
    Private iSshHostKeyFingerprint As String
    Private iPath As String = ""
    Private iSslHostCertificateFingerprint As String
#End Region

#Region "Atributos"
    Public Property protocol() As Protocol
        Get
            Return iProtocol
        End Get
        Set(ByVal Value As Protocol)
            iProtocol = Value
        End Set
    End Property
    Public Property hostName() As String
        Get
            Return iHostName
        End Get
        Set(ByVal Value As String)
            iHostName = Value
        End Set
    End Property
    Public Property portNumber() As Integer
        Get
            Return iPortNumber
        End Get
        Set(ByVal Value As Integer)
            iPortNumber = Value
        End Set
    End Property
    Public Property userName() As String
        Get
            Return iUserName
        End Get
        Set(ByVal Value As String)
            iUserName = Value
        End Set
    End Property
    Public Property password() As String
        Get
            Return iPassword
        End Get
        Set(ByVal Value As String)
            iPassword = Value
        End Set
    End Property
    Public Property sshHostKeyFingerprint() As String
        Get
            Return iSshHostKeyFingerprint
        End Get
        Set(ByVal Value As String)
            iSshHostKeyFingerprint = Value
        End Set
    End Property
    Public Property path() As String
        Get
            Return iPath
        End Get
        Set(ByVal Value As String)
            iPath = Value
        End Set
    End Property

    Public Property sslHostCertificateFingerprint As String
        Get
            Return iSslHostCertificateFingerprint
        End Get
        Set(value As String)
            iSslHostCertificateFingerprint = value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Shared Function subiraArchivo(ByVal ePathArchivo As String, ByVal eArchivo As String, Optional ByVal eSessionOptions As SessionOptions = Nothing) As Boolean
        Dim iSessionOptions As New SessionOptions
        Dim iTransferOptions As New TransferOptions
        Dim iTransferResult As TransferOperationResult

        Try
            ' Setup session options
            If IsNothing(eSessionOptions) Then
                With iSessionOptions
                    If IsNothing(ConfigurationManager.AppSettings("protocolo")) Then
                        .Protocol = Protocol.Sftp
                        .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                    Else
                        Select Case ConfigurationManager.AppSettings("protocolo")
                            Case "sftp"
                                .Protocol = Protocol.Sftp
                                .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                            Case "ftp"
                                .Protocol = Protocol.Ftp
                            Case Else
                                'valor por defecto 
                                .Protocol = Protocol.Sftp
                                .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                        End Select
                    End If
                    .HostName = ConfigurationManager.AppSettings("servidor")
                    .PortNumber = ConfigurationManager.AppSettings("puerto")
                    .UserName = ConfigurationManager.AppSettings("usuario")
                    .Password = ConfigurationManager.AppSettings("passwd")
                    '.SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")  ' TODO: REMOVER! ERROR SI PROTOCOLO ES FTP (NO SFTP)
                End With
            Else
                iSessionOptions = eSessionOptions
            End If

            Using session As Session = New Session

                If ConfigurationManager.AppSettings("loguearFTP") <> Nothing AndAlso CBool(ConfigurationManager.AppSettings("loguearFTP")) Then
                    session.DebugLogLevel = 1
                    session.DebugLogPath = ConfigurationManager.AppSettings("archivosGenerados") & "logFTP" & Format(Today, "ddMMyyyy") & ".txt"
                End If

                ' Connect
                session.Open(iSessionOptions)

                ' Upload files
                iTransferOptions.TransferMode = TransferMode.Binary
                iTransferResult = session.PutFiles(ePathArchivo, ConfigurationManager.AppSettings("pathEnvio") & eArchivo, False, iTransferOptions)

                ' Si no esta OK genera un eror
                iTransferResult.Check()

                ' Muesta el resultado
                Dim transfer As TransferEventArgs
                For Each transfer In iTransferResult.Transfers
                    Return True
                Next
                session.Dispose()
            End Using

        Catch exception As Exception
            Throw New SFTPNoConectadoException(exception)
        Finally
            iSessionOptions = Nothing
            iTransferOptions = Nothing
            iTransferResult = Nothing
        End Try
    End Function

    Public Function subirArchivo(ByVal ePathArchivo As String, ByVal eArchivo As String) As Boolean
        Dim iSessionOptions As New SessionOptions
        Dim iTransferOptions As New TransferOptions
        Dim iTransferResult As TransferOperationResult

        Try
            ' Setup session options
            With iSessionOptions
                .Protocol = iProtocol
                .SshHostKeyFingerprint = iSshHostKeyFingerprint
                .HostName = iHostName
                .PortNumber = iPortNumber
                .UserName = iUserName
                .Password = iPassword
                If iSslHostCertificateFingerprint <> Nothing Then
                    .TlsHostCertificateFingerprint = iSslHostCertificateFingerprint
                    .FtpSecure = FtpSecure.Implicit
                End If
            End With

            Using session As Session = New Session

                If ConfigurationManager.AppSettings("loguearFTP") <> Nothing AndAlso CBool(ConfigurationManager.AppSettings("loguearFTP")) Then
                    session.DebugLogLevel = 1
                    session.DebugLogPath = ConfigurationManager.AppSettings("archivosGenerados") & "logFTP" & Format(Today, "ddMMyyyy") & ".txt"
                End If

                ' Connect
                session.Open(iSessionOptions)

                ' Upload files
                iTransferOptions.TransferMode = TransferMode.Binary
                iTransferResult = session.PutFiles(ePathArchivo, iPath & eArchivo, False, iTransferOptions)

                ' Si no esta OK genera un eror
                iTransferResult.Check()

                ' Muesta el resultado
                Dim transfer As TransferEventArgs
                For Each transfer In iTransferResult.Transfers
                    Return True
                Next
            End Using



        Catch exception As Exception
            Throw New SFTPNoConectadoException(exception)
        Finally
            iSessionOptions = Nothing
            iTransferOptions = Nothing
            iTransferResult = Nothing
        End Try
    End Function

    Public Shared Function descargarArchivo(ByVal ePathArchivo As String, ByVal eArchivo As String, Optional ByVal eSessionOptions As SessionOptions = Nothing, Optional ByVal ePath As String = Nothing) As Boolean
        Dim iSessionOptions As New SessionOptions
        Dim iTransferOptions As New TransferOptions
        Dim iTransferResult As TransferOperationResult
        Dim iRemotePath As String
        Dim iLocalPath As String

        Try
            ' Setup session options
            If IsNothing(eSessionOptions) Then
                With iSessionOptions
                    If IsNothing(ConfigurationManager.AppSettings("protocolo")) Then
                        .Protocol = Protocol.Sftp
                        .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                    Else
                        Select Case ConfigurationManager.AppSettings("protocolo")
                            Case "sftp"
                                .Protocol = Protocol.Sftp
                                .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                            Case "ftp"
                                .Protocol = Protocol.Ftp
                            Case Else
                                'valor por defecto 
                                .Protocol = Protocol.Sftp
                                .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                        End Select
                    End If
                    .HostName = ConfigurationManager.AppSettings("servidor")
                    .PortNumber = ConfigurationManager.AppSettings("puerto")
                    .UserName = ConfigurationManager.AppSettings("usuario")
                    .Password = ConfigurationManager.AppSettings("passwd")
                    '.SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")  ' TODO: REMOVER! ERROR SI PROTOCOLO ES FTP (NO SFTP)
                End With
            Else
                iSessionOptions = eSessionOptions
            End If

            Using mySession As Session = New Session

                If ConfigurationManager.AppSettings("loguearFTP") <> Nothing AndAlso CBool(ConfigurationManager.AppSettings("loguearFTP")) Then
                    mySession.DebugLogLevel = 1
                    mySession.DebugLogPath = ConfigurationManager.AppSettings("archivosGenerados") & "logFTP" & Format(Today, "ddMMyyyy") & ".txt"
                End If

                ' Conecto con el sftp
                mySession.Open(iSessionOptions)

                ' Obtengo los archivos que tiene el directorio sftp
                If ePath <> Nothing Then
                    iRemotePath = ePath & eArchivo
                Else
                    iRemotePath = ConfigurationManager.AppSettings("path") & eArchivo
                End If

                iLocalPath = ePathArchivo & eArchivo

                ' Evaluo si el archivo a copiar existe
                If mySession.FileExists(iRemotePath) Then

                    ' Si el archivo local existe lo borro
                    If File.Exists(iLocalPath) Then File.Delete(iLocalPath)

                    ' Descargo el archivo
                    iTransferOptions.TransferMode = TransferMode.Binary
                    iTransferResult = mySession.GetFiles(iRemotePath, iLocalPath, False, iTransferOptions)

                    ' Si no esta OK genera un eror
                    iTransferResult.Check()

                Else
                    Throw New SFTPNoConectadoException("No existe el archivo a descargar")
                End If

            End Using

            Return True
        Catch exception As Exception
            Throw exception
        Finally
            iSessionOptions = Nothing
            iTransferOptions = Nothing
            iTransferResult = Nothing
        End Try

    End Function

    Public Shared Sub sicronizarDirectorio(ByVal ePathArchivo As String, ByVal eContiene As String, eCantidadCaracteres As Integer, Optional ByVal eSessionOptions As SessionOptions = Nothing, Optional ByVal ePath As String = Nothing)
        Dim iSessionOptions As New SessionOptions
        Dim iTransferOptions As New TransferOptions
        Dim iTransferResult As SynchronizationResult
        Dim iDirectorioInfo As RemoteDirectoryInfo
        Dim iFileInfo As RemoteFileInfo

        Try
            ' Setup session options
            If IsNothing(eSessionOptions) Then
                With iSessionOptions
                    If IsNothing(ConfigurationManager.AppSettings("protocolo")) Then
                        .Protocol = Protocol.Sftp
                        .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                    Else
                        Select Case ConfigurationManager.AppSettings("protocolo")
                            Case "sftp"
                                .Protocol = Protocol.Sftp
                                .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                            Case "ftp"
                                .Protocol = Protocol.Ftp
                            Case Else
                                'valor por defecto 
                                .Protocol = Protocol.Sftp
                                .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
                        End Select
                    End If
                    .HostName = ConfigurationManager.AppSettings("servidor")
                    .PortNumber = ConfigurationManager.AppSettings("puerto")
                    .UserName = ConfigurationManager.AppSettings("usuario")
                    .Password = ConfigurationManager.AppSettings("passwd")
                    '.SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")  ' TODO: REMOVER! ERROR SI PROTOCOLO ES FTP (NO SFTP)
                End With
            Else
                iSessionOptions = eSessionOptions
            End If

            Using session As Session = New Session

                If ConfigurationManager.AppSettings("loguearFTP") <> Nothing AndAlso CBool(ConfigurationManager.AppSettings("loguearFTP")) Then
                    session.DebugLogLevel = 1
                    session.DebugLogPath = ConfigurationManager.AppSettings("archivosGenerados") & "logFTP" & Format(Today, "ddMMyyyy") & ".txt"
                End If

                ' Connect
                session.Open(iSessionOptions)

                ' Obtengo los archivos que tiene el directorio sftp
                If ePath <> Nothing Then
                    iDirectorioInfo = session.ListDirectory(ePath)
                Else
                    iDirectorioInfo = session.ListDirectory(ConfigurationManager.AppSettings("path"))
                End If
                ' Recorro y verifico si existe el archivo
                For Each iFileInfo In iDirectorioInfo.Files
                    ' Si el archivo no esta procesado lo descargo para procesar
                    If iFileInfo.Name <> Nothing AndAlso iFileInfo.Name.Length >= eCantidadCaracteres AndAlso Not File.Exists(ePathArchivo & iFileInfo.Name & "-P") Then
                        Try
                            ' Descargo el archivo so contiene la la palabra enviada
                            If Left(iFileInfo.Name.ToUpper, eContiene.Length) = eContiene.ToUpper Then descargarArchivo(ePathArchivo, iFileInfo.Name, eSessionOptions, ePath)

                        Catch exception As Exception
                            Throw exception
                        End Try
                    End If

                Next

            End Using

        Catch exception As Exception
            Throw exception
        Finally
            iSessionOptions = Nothing
            iTransferOptions = Nothing
            iTransferResult = Nothing
        End Try
    End Sub

    Public Function descargaraArchivo(ByVal eLocalPath As String, ByVal eArchivo As String) As Boolean
        Dim iSessionOptions As New SessionOptions
        Dim iTransferOptions As New TransferOptions
        Dim iTransferResult As TransferOperationResult
        Dim iRemotePath As String
        Dim iLocalPath As String

        Try
            ' Setup session options
            With iSessionOptions
                .Protocol = iProtocol
                .SshHostKeyFingerprint = iSshHostKeyFingerprint
                .HostName = iHostName
                .PortNumber = iPortNumber
                .UserName = iUserName
                .Password = iPassword
            End With

            Using mySession As Session = New Session

                If ConfigurationManager.AppSettings("loguearFTP") <> Nothing AndAlso CBool(ConfigurationManager.AppSettings("loguearFTP")) Then
                    mySession.DebugLogLevel = 1
                    mySession.DebugLogPath = ConfigurationManager.AppSettings("archivosGenerados") & "logFTP" & Format(Today, "ddMMyyyy") & ".txt"
                End If

                ' Conecto con el sftp
                mySession.Open(iSessionOptions)

                iRemotePath = path & eArchivo
                iLocalPath = eLocalPath & eArchivo

                ' Evaluo si el archivo a copiar existe
                If mySession.FileExists(iRemotePath) Then

                    ' Si el archivo local existe lo borro
                    If File.Exists(iLocalPath) Then File.Delete(iLocalPath)

                    ' Descargo el archivo
                    iTransferOptions.TransferMode = TransferMode.Binary
                    iTransferResult = mySession.GetFiles(iRemotePath, iLocalPath, False, iTransferOptions)

                    ' Si no esta OK genera un eror
                    iTransferResult.Check()

                Else
                    Throw New SFTPNoConectadoException("No existe el archivo a descargar")
                End If

            End Using

            Return True
        Catch exception As Exception
            Throw exception
        Finally
            iSessionOptions = Nothing
            iTransferOptions = Nothing
            iTransferResult = Nothing
        End Try

    End Function

#End Region

    '#Region "Metodos"

    '    Public Shared Function subirArchivo(ByVal eSFTPVO As SFTPVO, ByVal ePathArchivo As String, ByVal eArchivo As String) As Boolean
    '        Dim iSessionOptions As New SessionOptions
    '        Dim iTransferOptions As New TransferOptions
    '        Dim iTransferResult As TransferOperationResult
    '        Dim iRemotePath As String
    '        Dim iLocalPath As String

    '        Try
    '            ' Setup session options
    '            With iSessionOptions
    '                Select Case eSFTPVO.protocolo
    '                    Case SFTPVO.enumProtocolo.SFTP
    '                        .Protocol = Protocol.Sftp
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .SshHostKeyFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.SCP
    '                        .Protocol = Protocol.Scp
    '                    Case SFTPVO.enumProtocolo.FTPS
    '                        .Protocol = Protocol.Ftp
    '                        .FtpSecure = FtpSecure.Implicit
    '                        .GiveUpSecurityAndAcceptAnyTlsHostCert = True
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .TlsHostCertificateFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .TlsHostCertificateFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.FTP
    '                        .Protocol = Protocol.Ftp
    '                End Select
    '                If eSFTPVO.servidor <> Nothing Then
    '                    .HostName = eSFTPVO.servidor
    '                Else
    '                    .HostName = ConfigurationManager.AppSettings("servidor")
    '                End If
    '                If eSFTPVO.puerto <> Nothing Then
    '                    .PortNumber = CInt(eSFTPVO.puerto)
    '                Else
    '                    .PortNumber = CInt(ConfigurationManager.AppSettings("puerto"))
    '                End If
    '                If eSFTPVO.usuario <> Nothing Then
    '                    .UserName = eSFTPVO.usuario
    '                Else
    '                    .UserName = ConfigurationManager.AppSettings("usuario")
    '                End If
    '                If eSFTPVO.passwd <> Nothing Then
    '                    .Password = eSFTPVO.passwd
    '                Else
    '                    .Password = ConfigurationManager.AppSettings("passwd")
    '                End If

    '                If eSFTPVO.hostCertificateFingerPrint <> Nothing Then
    '                    .SshHostKeyFingerprint = eSFTPVO.hostKeyFingerPrint
    '                    .FtpSecure = FtpSecure.ExplicitSsl
    '                End If

    '            End With

    '            Using session As Session = New Session
    '                ' Connect
    '                session.Open(iSessionOptions)

    '                ' Upload files
    '                iTransferOptions.TransferMode = TransferMode.Binary
    '                If eSFTPVO.path <> Nothing Then
    '                    iRemotePath = eSFTPVO.path & "\" & eArchivo
    '                Else
    '                    iRemotePath = eArchivo
    '                End If

    '                iLocalPath = ePathArchivo & eArchivo

    '                iTransferResult = session.PutFiles(iLocalPath, iRemotePath, False, iTransferOptions)

    '                ' Si no esta OK genera un eror
    '                iTransferResult.Check()

    '                ' Muesta el resultado
    '                Dim transfer As TransferEventArgs
    '                For Each transfer In iTransferResult.Transfers
    '                    Return True
    '                Next
    '            End Using

    '        Catch exception As Exception
    '            Throw New SFTPNoConectadoException(exception)
    '        Finally
    '            iSessionOptions = Nothing
    '            iTransferOptions = Nothing
    '            iTransferResult = Nothing
    '        End Try
    '    End Function

    '    Public Shared Function descargarArchivo(ByVal eSFTPVO As SFTPVO, ByVal ePathArchivo As String, ByVal eArchivo As String) As Boolean
    '        Dim iSessionOptions As New SessionOptions
    '        Dim iTransferOptions As New TransferOptions
    '        Dim iTransferResult As TransferOperationResult
    '        Dim iRemotePath As String
    '        Dim iLocalPath As String

    '        Try
    '            ' Setup session options
    '            With iSessionOptions
    '                Select Case eSFTPVO.protocolo
    '                    Case SFTPVO.enumProtocolo.SFTP
    '                        .Protocol = Protocol.Sftp
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .SshHostKeyFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.SCP
    '                        .Protocol = Protocol.Scp
    '                    Case SFTPVO.enumProtocolo.FTPS
    '                        .Protocol = Protocol.Ftp
    '                        .FtpSecure = FtpSecure.Implicit
    '                        .GiveUpSecurityAndAcceptAnyTlsHostCertificate = True
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .TlsHostCertificateFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .TlsHostCertificateFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.FTP
    '                        .Protocol = Protocol.Ftp
    '                End Select
    '                If eSFTPVO.servidor <> Nothing Then
    '                    .HostName = eSFTPVO.servidor
    '                Else
    '                    .HostName = ConfigurationManager.AppSettings("servidor")
    '                End If
    '                If eSFTPVO.puerto <> Nothing Then
    '                    .PortNumber = CInt(eSFTPVO.puerto)
    '                Else
    '                    .PortNumber = CInt(ConfigurationManager.AppSettings("puerto"))
    '                End If
    '                If eSFTPVO.usuario <> Nothing Then
    '                    .UserName = eSFTPVO.usuario
    '                Else
    '                    .UserName = ConfigurationManager.AppSettings("usuario")
    '                End If
    '                If eSFTPVO.passwd <> Nothing Then
    '                    .Password = eSFTPVO.passwd
    '                Else
    '                    .Password = ConfigurationManager.AppSettings("passwd")
    '                End If
    '                If eSFTPVO.hostCertificateFingerPrint <> Nothing Then
    '                    .TlsHostCertificateFingerprint = eSFTPVO.hostCertificateFingerPrint
    '                    .FtpSecure = FtpSecure.ExplicitSsl
    '                End If
    '            End With

    '            Using mySession As Session = New Session
    '                ' Conecto con el sftp
    '                mySession.Open(iSessionOptions)
    '                If eSFTPVO.path <> Nothing Then
    '                    iRemotePath = eSFTPVO.path & eArchivo
    '                Else
    '                    iRemotePath = eArchivo
    '                End If

    '                iLocalPath = ePathArchivo & eArchivo

    '                ' Evaluo si el archivo a copiar existe
    '                If mySession.FileExists(iRemotePath) Then

    '                    ' Si el archivo local existe lo borro
    '                    If File.Exists(iLocalPath) Then File.Delete(iLocalPath)

    '                    ' Descargo el archivo
    '                    iTransferOptions.TransferMode = TransferMode.Binary
    '                    iTransferResult = mySession.GetFiles(iRemotePath, iLocalPath, False, iTransferOptions)

    '                    ' Si no esta OK genera un eror
    '                    iTransferResult.Check()

    '                Else
    '                    Throw New SFTPNoConectadoException("No existe el archivo a descargar")
    '                End If

    '            End Using

    '            Return True
    '        Catch exception As Exception
    '            Throw exception
    '        Finally
    '            iSessionOptions = Nothing
    '            iTransferOptions = Nothing
    '            iTransferResult = Nothing
    '        End Try

    '    End Function

    '    Public Shared Sub sicronizarDirectorio(ByVal eSFTPVO As SFTPVO, ByVal ePathArchivo As String, ByVal eContiene As String, eCantidadCaracteres As Integer)
    '        Dim iSessionOptions As New SessionOptions
    '        Dim iTransferOptions As New TransferOptions
    '        Dim iTransferResult As SynchronizationResult
    '        Dim iDirectorioInfo As RemoteDirectoryInfo
    '        Dim iFileInfo As RemoteFileInfo

    '        Try
    '            ' Setup session options
    '            With iSessionOptions
    '                Select Case eSFTPVO.protocolo
    '                    Case SFTPVO.enumProtocolo.SFTP
    '                        .Protocol = Protocol.Sftp
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .SshHostKeyFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.SCP
    '                        .Protocol = Protocol.Scp
    '                    Case SFTPVO.enumProtocolo.FTPS
    '                        .Protocol = Protocol.Ftp
    '                        .FtpSecure = FtpSecure.Implicit
    '                        .GiveUpSecurityAndAcceptAnyTlsHostCertificate = True
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .TlsHostCertificateFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .TlsHostCertificateFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.FTP
    '                        .Protocol = Protocol.Ftp
    '                End Select
    '                If eSFTPVO.servidor <> Nothing Then
    '                    .HostName = eSFTPVO.servidor
    '                Else
    '                    .HostName = ConfigurationManager.AppSettings("servidor")
    '                End If
    '                If eSFTPVO.puerto <> Nothing Then
    '                    .PortNumber = CInt(eSFTPVO.puerto)
    '                Else
    '                    .PortNumber = CInt(ConfigurationManager.AppSettings("puerto"))
    '                End If
    '                If eSFTPVO.usuario <> Nothing Then
    '                    .UserName = eSFTPVO.usuario
    '                Else
    '                    .UserName = ConfigurationManager.AppSettings("usuario")
    '                End If
    '                If eSFTPVO.passwd <> Nothing Then
    '                    .Password = eSFTPVO.passwd
    '                Else
    '                    .Password = ConfigurationManager.AppSettings("passwd")
    '                End If

    '                If eSFTPVO.hostCertificateFingerPrint <> Nothing Then
    '                    .TlsHostCertificateFingerprint = eSFTPVO.hostCertificateFingerPrint
    '                    .FtpSecure = FtpSecure.ExplicitSsl
    '                End If

    '            End With

    '            Using session As Session = New Session
    '                ' Connect
    '                session.Open(iSessionOptions)

    '                ' Obtengo los archivos que tiene el directorio sftp
    '                If eSFTPVO.path <> Nothing Then
    '                    iDirectorioInfo = session.ListDirectory(eSFTPVO.path)
    '                Else
    '                    iDirectorioInfo = session.ListDirectory(ConfigurationManager.AppSettings("path").ToString())
    '                End If
    '                ' Recorro y verifico si existe el archivo
    '                For Each iFileInfo In iDirectorioInfo.Files
    '                    ' Si el archivo no esta procesado lo descargo para procesar
    '                    If iFileInfo.Name <> Nothing AndAlso iFileInfo.Name.Length >= eCantidadCaracteres AndAlso Not File.Exists(ePathArchivo & iFileInfo.Name & "-P") Then
    '                        Try
    '                            ' Descargo el archivo solo si contiene la palabra enviada
    '                            If iFileInfo.Name.Contains(eContiene) Then descargarArchivo(eSFTPVO, ePathArchivo, iFileInfo.Name)

    '                        Catch exception As Exception
    '                            Throw exception
    '                        End Try
    '                    End If

    '                Next

    '            End Using

    '        Catch exception As Exception
    '            Throw exception
    '        Finally
    '            iSessionOptions = Nothing
    '            iTransferOptions = Nothing
    '            iTransferResult = Nothing
    '        End Try
    '    End Sub

    '    Public Shared Sub sicronizarDirectorioCompleto(ByVal eSFTPVO As SFTPVO, ByVal ePathArchivo As String, ByVal eSynchronizationMode As SynchronizationMode)
    '        Dim iSessionOptions As New SessionOptions
    '        Dim iTransferOptions As New TransferOptions
    '        Dim iTransferResult As SynchronizationResult

    '        Try
    '            ' Setup session options
    '            With iSessionOptions
    '                Select Case eSFTPVO.protocolo
    '                    Case SFTPVO.enumProtocolo.SFTP
    '                        .Protocol = Protocol.Sftp
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .SshHostKeyFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .SshHostKeyFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.SCP
    '                        .Protocol = Protocol.Scp
    '                    Case SFTPVO.enumProtocolo.FTPS
    '                        .Protocol = Protocol.Ftp
    '                        .FtpSecure = FtpSecure.Implicit
    '                        .GiveUpSecurityAndAcceptAnyTlsHostCertificate = True
    '                        If eSFTPVO.sshostKey <> Nothing Then
    '                            .TlsHostCertificateFingerprint = eSFTPVO.sshostKey
    '                        Else
    '                            .TlsHostCertificateFingerprint = ConfigurationManager.AppSettings("sshostKey")
    '                        End If
    '                    Case SFTPVO.enumProtocolo.FTP
    '                        .Protocol = Protocol.Ftp
    '                End Select
    '                If eSFTPVO.servidor <> Nothing Then
    '                    .HostName = eSFTPVO.servidor
    '                Else
    '                    .HostName = ConfigurationManager.AppSettings("servidor")
    '                End If
    '                If eSFTPVO.puerto <> Nothing Then
    '                    .PortNumber = CInt(eSFTPVO.puerto)
    '                Else
    '                    .PortNumber = CInt(ConfigurationManager.AppSettings("puerto"))
    '                End If
    '                If eSFTPVO.usuario <> Nothing Then
    '                    .UserName = eSFTPVO.usuario
    '                Else
    '                    .UserName = ConfigurationManager.AppSettings("usuario")
    '                End If
    '                If eSFTPVO.passwd <> Nothing Then
    '                    .Password = eSFTPVO.passwd
    '                Else
    '                    .Password = ConfigurationManager.AppSettings("passwd")
    '                End If
    '            End With

    '            Using session As Session = New Session
    '                'Connect
    '                session.Open(iSessionOptions)

    '                'Sincronizo el directorio local
    '                session.SynchronizeDirectories(eSynchronizationMode, ePathArchivo, eSFTPVO.path, False)

    '            End Using

    '        Catch exception As Exception
    '            Throw exception
    '        Finally
    '            iSessionOptions = Nothing
    '            iTransferOptions = Nothing
    '            iTransferResult = Nothing
    '        End Try
    '    End Sub

    '#End Region

End Class