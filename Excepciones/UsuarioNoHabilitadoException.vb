
Public Class UsuarioNoHabilitadoException
    Inherits UsuarioException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "El usuario no se encuentra habilitado."
    End Sub

    Sub New()
        MyBase.iMensaje = "El usuario no se encuentra habilitado."
    End Sub
End Class
