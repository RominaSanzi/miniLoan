
Public Class DatosVaciosException

    Inherits ComercioException

    Sub New(ByVal eMensaje As String, ByVal eException As Exception)
        MyBase.iMensaje = eMensaje
        MyBase.originalCause = eException
    End Sub

    Sub New(ByVal eMensaje As String)
        MyBase.iMensaje = eMensaje
    End Sub

    Sub New(ByVal eException As Exception)
        MyBase.originalCause = eException
        MyBase.iMensaje = "El cuil está vacio o incompleto."
    End Sub

    Sub New()
        MyBase.iMensaje = "El cuil está vacio o incompleto."
    End Sub

End Class
