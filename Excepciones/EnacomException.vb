Public Class EnacomException
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
        MyBase.iMensaje = "Ocurrio un error en enacom."
    End Sub

    Sub New()
        MyBase.iMensaje = "Ocurrio un error en enacom."
    End Sub

End Class