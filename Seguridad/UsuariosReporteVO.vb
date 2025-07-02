Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class UsuariosReporteVO

    Inherits Entidad

#Region "Variables"
    Private iMail As String
    Private iNivel As nivel
    Private iPerfil As perfil
    Private iEstado As Estado
    Private iNombre As String
    Private iLogin As String
#End Region

#Region "Atributos"
    Public Property mail() As String
        Get
            Return iMail
        End Get
        Set(ByVal Value As String)
            iMail = Value
        End Set
    End Property
    Public Property nivel() As nivel
        Get
            Return iNivel
        End Get
        Set(ByVal Value As nivel)
            iNivel = Value
        End Set
    End Property
    Public Property perfil() As perfil
        Get
            Return iPerfil
        End Get
        Set(ByVal Value As perfil)
            iPerfil = Value
        End Set
    End Property
    Public Property estado() As estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As estado)
            iEstado = Value
        End Set
    End Property
    Public Property nombre() As String
        Get
            Return iNombre
        End Get
        Set(ByVal Value As String)
            iNombre = Value
        End Set
    End Property
    Public Property login() As String
        Get
            Return iLogin
        End Get
        Set(ByVal Value As String)
            iLogin = Value
        End Set
    End Property
#End Region

End Class
