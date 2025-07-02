Imports di.financiera.datos
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.excepciones
Public Class ColorSingleton

    Inherits Entidad

#Region "Variables"
    Private Shared iDataSet As DataSet
    Private Shared iForzarActualizacion As Boolean
#End Region

#Region "Atributos"
    Public Property dataSet() As DataSet
        Get
            Return iDataSet
        End Get
        Set(ByVal Value As DataSet)
            iDataSet = Value
        End Set
    End Property

    Public Shared Property forzarActualizacion() As Boolean
        Get
            Return iForzarActualizacion
        End Get
        Set(ByVal Value As Boolean)
            iForzarActualizacion = Value
        End Set
    End Property

#End Region

    Private Shared iInstancia As ColorSingleton
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia(eActualizar As Boolean) As ColorSingleton
        Try
            iMutex.WaitOne()
            If iInstancia Is Nothing OrElse eActualizar OrElse iForzarActualizacion Then
                iInstancia = New ColorSingleton
            End If

            Return iInstancia
        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        Finally
            iMutex.ReleaseMutex()
        End Try
    End Function

    Public Sub New()
        Try
            obtenerColor()

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Sub

    Private Shared Sub obtenerColor()
        Dim iGeneradorSql As New GeneradorSql
        Dim iConexion As accesoDatos

        Try
            iForzarActualizacion = False

            iConexion = New accesoDatos

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("archivo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("grafico")
            iGeneradorSql.agregarTabla("color")

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "color")

            Dim iColumnaClave(1) As DataColumn

            iColumnaClave(0) = iDataSet.Tables("color").Columns("id")
            iDataSet.Tables("color").PrimaryKey = iColumnaClave

        Catch excepcion As Exception
            Throw New ColorNoEncontradoException(excepcion)
        Finally
            iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

End Class
