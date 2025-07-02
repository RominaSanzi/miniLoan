Imports di.financiera.datos
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class UsuariosGrillaVO

    Inherits Entidad

#Region "Variables"
    Private iNivel As Nivel
    Private iPerfil As perfil
    Private iLogin As String
    Private iNombre As String
    Private iEstado As Estado
#End Region

#Region "Atributos"
    Public Property nivel() As Nivel
        Get
            Return iNivel
        End Get
        Set(ByVal Value As Nivel)
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

    Public Property login() As String
        Get
            Return iLogin
        End Get
        Set(ByVal Value As String)
            iLogin = Value
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

    Public Property estado() As Estado
        Get
            Return iEstado
        End Get
        Set(ByVal Value As Estado)
            iEstado = Value
        End Set
    End Property

#End Region

End Class
