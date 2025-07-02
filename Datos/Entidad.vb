Imports di.financiera.datos

Public Class Entidad

    Private iAccesoDatos As accesoDatos

    Public Property accesoDatos() As accesoDatos
        Get
            Return iAccesoDatos
        End Get
        Set(ByVal Value As accesoDatos)
            iAccesoDatos = Value
        End Set
    End Property

#Region "Conexion"
    Public Function obtenerConexion() As accesoDatos

        If (IsNothing(accesoDatos)) Then
            Return New accesoDatos
        Else
            Return accesoDatos
        End If
    End Function
#End Region

End Class
