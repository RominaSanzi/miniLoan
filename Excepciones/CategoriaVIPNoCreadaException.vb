
Public Class CategoriaVIPNoCreadaException
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
        MyBase.iMensaje = "No se pudo crear la Categoria VIP en la base de datos."
    End Sub

    Sub New()
        MyBase.iMensaje = "No se pudo crear la Categoria VIP en la base de datos."
    End Sub
End Class
