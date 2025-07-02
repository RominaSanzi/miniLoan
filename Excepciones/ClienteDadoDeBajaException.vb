
Public Class ClienteDadoDeBajaException

    Inherits ClienteException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "El cliente se encuentra dado de baja"
    End Sub

    Sub New()
        MyBase.iMensaje = "El cliente se encuentra dado de baja"
    End Sub

End Class
