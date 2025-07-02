
Public Class MovimientoStockException
    Inherits ExternoException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "Se produjo un error en al entidad movimiento stock."
    End Sub

    Sub New()
        MyBase.iMensaje = "Se produjo un error en al entidad movimiento stock."
    End Sub

End Class
