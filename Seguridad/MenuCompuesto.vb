Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class MenuCompuesto : Inherits Menu

#Region "Variables"
    Private iMenues As Collection
    Private iIdRaiz As Integer
    Private iConexion As accesoDatos

#End Region

#Region "Atributos"
    Public Property menues() As Collection
        Get
            Return iMenues
        End Get
        Set(ByVal Value As Collection)
            iMenues = Value
        End Set
    End Property

#End Region

#Region "Metodos"
    Sub New()
        iMenues = New Collection()
    End Sub
    Public Overrides Function isMenuSimple() As Boolean
        Return False
    End Function

    Public Overrides Function isMenuCompuesto() As Boolean
        Return True
    End Function

    Public Overrides Function isHijoDeRaiz() As Boolean
        Return idPadre = idRaiz
    End Function

    Public Function obtenerMenuCompuesto() As MenuCompuesto
        Dim iGeneradorSql As New GeneradorSql()
        Dim iDataSetSingleton As DataSet
        Dim iDataRow As DataRow
        Try

            iDataSetSingleton = MenuCompuestoSingleton.getInstancia(False).dataSet

            Dim iBusqueda(0) As Object
            iBusqueda(0) = id
            iDataRow = iDataSetSingleton.Tables("MenuCompuesto").Rows.Find(iBusqueda)


            If Not IsNothing(iDataRow) Then
                id = iDataRow.Item("id")
                nombre = iDataRow.Item("nombre")
                idPadre = IIf(IsDBNull(iDataRow.Item("idPadre")), Nothing, iDataRow.Item("idPadre"))
                Return Me
            Else
                Throw New MenuNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New MenuNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Overrides Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iMenues) Then
                Dim i As Integer
                For i = 1 To iMenues.Count
                    iMenues.Item(i).dispose()
                Next
            End If

        Catch exception As exception
            Throw New RootException(exception)
        End Try

    End Sub


#End Region

End Class
