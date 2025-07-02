
Public Class TipoDocumentoException

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
        MyBase.iMensaje = "no se pudo generar la prorroga, el tipo de documento no es correcto."
    End Sub

    Sub New()
        MyBase.iMensaje = "no se pudo generar la prorroga, el tipo de documento no es correcto."
    End Sub

End Class

