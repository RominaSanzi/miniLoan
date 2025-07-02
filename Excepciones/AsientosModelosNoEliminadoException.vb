Public Class AsientosModelosNoEliminadoException
    Inherits AsientosModelosException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "La Asientos Modelos no se pudo eliminar."
    End Sub

    Sub New()
        MyBase.iMensaje = "La Asientos Modelos no se pudo eliminar."
    End Sub
End Class
