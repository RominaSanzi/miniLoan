
Public Class UsuarioClaveErroneaException

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
        MyBase.iMensaje = "No se encontro el usuario en la base de datos. Compruebe el uso de mayusculas y minusculas."
    End Sub

    Sub New()
        MyBase.iMensaje = "No se encontro el usuario en la base de datos. Compruebe el uso de mayusculas y minusculas."
    End Sub
End Class
