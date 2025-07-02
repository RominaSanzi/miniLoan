
Public Class DetallesCreditosInsercionExternaException

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
        MyBase.iMensaje = "Hubo problemas en la carga o lectura de datos."
    End Sub

    Sub New()
        MyBase.iMensaje = "Hubo problemas en la carga o lectura de datos."
    End Sub

End Class

