Public Class AsientosModelosDetalleNoEliminadoException
    Inherits AsientosModelosDetalleException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "El Asientos Modelos Detalles no se pudo eliminar."
    End Sub

    Sub New()
        MyBase.iMensaje = "El Asientos Modelos Detalle no se pudo eliminar."
    End Sub
End Class
