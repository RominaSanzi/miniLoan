Public Class BoletinCruceNoRealizadoException

    Inherits ComunException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "No se pudo realizar el cruce."
    End Sub

    Sub New()
        MyBase.iMensaje = "No se pudo realizar el cruce."
    End Sub
End Class
