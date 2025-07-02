Public Class ECommerceErrorInterfaceException

    Inherits RootException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "Error en el interface del servicio."
    End Sub

    Sub New()
        MyBase.iMensaje = "Error en el interface del servicio."
    End Sub
End Class
