
Public Class ArchivoYaProcesadoException

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
        MyBase.iMensaje = "El archivo ya fue procesado anteriormente."
    End Sub

    Sub New()
        MyBase.iMensaje = "El archivo ya fue procesado anteriormente."
    End Sub

End Class
