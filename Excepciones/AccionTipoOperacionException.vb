
Public Class AccionTipoOperacionException

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
        MyBase.iMensaje = "No fue posible reconocer el tipo de Operacion"
    End Sub

    Sub New()
        MyBase.iMensaje = "No fue posible reconocer el tipo de Operacion"
    End Sub

End Class
