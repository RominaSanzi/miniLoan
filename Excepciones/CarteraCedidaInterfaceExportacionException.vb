Public Class CarteraCedidaInterfaceExportacionException
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
        MyBase.iMensaje = "Ocurrió un error referido a la interface de exportación de cartera cedida."
    End Sub

    Sub New()
        MyBase.iMensaje = "Ocurrió un error referido a la interface de exportación de cartera cedida."
    End Sub

End Class
