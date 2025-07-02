Imports di.financiera.datos
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.excepciones
Public Class SexoSingleton

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

    Private Shared iInstancia As SexoSingleton
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia(eActualizar As Boolean) As SexoSingleton
        Try
            iMutex.WaitOne()
            If iInstancia Is Nothing OrElse eActualizar OrElse iForzarActualizacion Then
                iInstancia = New SexoSingleton
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
            obtenerSexo()

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Sub

    Private Shared Sub obtenerSexo()
        Dim iGeneradorSql As New GeneradorSql
        Dim iConexion As accesoDatos

        Try
            iForzarActualizacion = False

            iConexion = New accesoDatos

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarTabla("Sexo")
            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Sexo")

            Dim iColumnaClave(1) As DataColumn

            iColumnaClave(0) = iDataSet.Tables("Sexo").Columns("id")
            iDataSet.Tables("Sexo").PrimaryKey = iColumnaClave

        Catch excepcion As Exception
            Throw New SexoNoEncontradoException(excepcion)
        Finally
            iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

End Class
