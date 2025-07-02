Imports di.financiera.utils

Public Class SFTPVO

#Region "Enumerados"

    Public Enum enumProtocolo
        SFTP = 0
        SCP = 1
        FTP = 2
        FTPS = 3
    End Enum

#End Region

#Region "Variables"
    Private iServidor As String
    Private iPuerto As String
    Private iUsuario As String
    Private iPasswd As String
    Private iSshostKey As String
    Private iPath As String
    Private iProtocolo As enumProtocolo
    Private iHostKeyFingerPrint As String
    Private iHostCertificateFingerPrint As String
#End Region

#Region "Atributos"
    Public Property servidor() As String
        Get
            Return iServidor
        End Get
        Set(ByVal Value As String)
            iServidor = Value
        End Set
    End Property
    Public Property puerto() As String
        Get
            Return iPuerto
        End Get
        Set(ByVal Value As String)
            iPuerto = Value
        End Set
    End Property
    Public Property usuario() As String
        Get
            Return iUsuario
        End Get
        Set(ByVal Value As String)
            iUsuario = Value
        End Set
    End Property
    Public Property passwd() As String
        Get
            Return iPasswd
        End Get
        Set(ByVal Value As String)
            iPasswd = Value
        End Set
    End Property
    Public Property sshostKey() As String
        Get
            Return iSshostKey
        End Get
        Set(ByVal Value As String)
            iSshostKey = Value
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
    Public Property protocolo() As enumProtocolo
        Get
            Return iProtocolo
        End Get
        Set(ByVal Value As enumProtocolo)
            iProtocolo = Value
        End Set
    End Property

    Public Property hostKeyFingerPrint As String
        Get
            Return iHostKeyFingerPrint
        End Get
        Set(value As String)
            iHostKeyFingerPrint = value
        End Set
    End Property

    Public Property hostCertificateFingerPrint As String
        Get
            Return iHostCertificateFingerPrint
        End Get
        Set(value As String)
            iHostCertificateFingerPrint = value
        End Set
    End Property


#End Region

End Class
