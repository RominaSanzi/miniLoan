Public Class CompaniaAseguradoraNoEliminadaException
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
        MyBase.iMensaje = "No se pudo eliminar la compañia aseguradora en la base de datos."
    End Sub

    Sub New()
        MyBase.iMensaje = "No se pudo eliminar la compañia aseguradora en la base de datos."
    End Sub

End Class
