Public Class MenuNoEncontradoException
    Inherits SeguridadException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "No se encontró el menu asociado al perfil."
    End Sub

    Sub New()
        MyBase.iMensaje = "No se encontró el menu asociado al perfil."
    End Sub
End Class
