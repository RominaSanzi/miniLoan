Imports di.financiera.datos
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.excepciones
Imports di.financiera.seguridad

Public Class ParametroSingleton
    Inherits Entidad


#Region "Variables"

    Private Shared iFechaHora As Date
    Private Shared iDataSet As DataSet
    Private Shared iForzarActualizacion As Boolean

#End Region

#Region "Atributos"
    Public Property fechaHora() As Date
        Get
            Return iFechaHora
        End Get
        Set(ByVal Value As Date)
            iFechaHora = Value
        End Set
    End Property

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


    Private Shared iInstancia As ParametroSingleton
    Private Shared iMutex As New System.Threading.Mutex()

    Public Shared Function getInstancia(eActualizar As Boolean) As ParametroSingleton
        Try
            iMutex.WaitOne()
            If iInstancia Is Nothing OrElse DateDiff(DateInterval.Hour, iFechaHora, Now) > 12 OrElse eActualizar OrElse iForzarActualizacion Then
                iInstancia = New ParametroSingleton
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
            obtenerParametros()

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Sub

    Private Shared Sub obtenerParametros()
        Dim iGeneradorSql As New GeneradorSql
        Dim iConexion As accesoDatos

        Try
            iForzarActualizacion = False

            iConexion = New accesoDatos

            iGeneradorSql.agregarTabla("parametro")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("valor")
            iGeneradorSql.agregarColumna("idTipoDato")
            iGeneradorSql.agregarColumna("idNivel")
            iGeneradorSql.agregarColumna("idTipoParametro")
            iGeneradorSql.agregarCondicionWhere("singleton=" & FuncionComun.booleanByte(True))

            iDataSet = iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "parametro")

            Dim iColumnaClave(1) As DataColumn

            iColumnaClave(0) = iDataSet.Tables("parametro").Columns("descripcion")
            iColumnaClave(1) = iDataSet.Tables("parametro").Columns("idNivel")
            iDataSet.Tables("parametro").PrimaryKey = iColumnaClave

            iFechaHora = Now

        Catch excepcion As Exception
            Throw New ParametroNoEncontradoException(excepcion)
        Finally
            iConexion.cerrar()
            iConexion = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub


End Class
