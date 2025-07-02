Public Class CajaNoHabilitadaException
    Inherits CajaException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "La caja no esta habilitada."
    End Sub

    Sub New()
        MyBase.iMensaje = "La caja no esta habilitada."
    End Sub
End Class
