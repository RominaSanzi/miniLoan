
Public Class FTPNoConectadoException
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
        MyBase.iMensaje = "Ocurrio un error con el FTP."
    End Sub

    Sub New()
        MyBase.iMensaje = "Ocurrio un error con el FTP."
    End Sub
End Class
