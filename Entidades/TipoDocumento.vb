Imports System.Configuration
Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public MustInherit Class TipoDocumento

    Inherits Entidad

#Region "Constantes"
    Public Shared DNI As Integer = 1
    Public Shared CI As Integer = 2
    Public Shared LE As Integer = 3
    Public Shared LC As Integer = 4
    Public Shared PAS As Integer = 5
    Public Shared CUIT As Integer = 6
    Public Shared LECABAL As Integer = 1
    Public Shared LCCABAL As Integer = 2
    Public Shared DNICABAL As Integer = 3
    Public Shared CICABAL As Integer = 4
    Public Shared PASCABAL As Integer = 9
#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String
    Private iConexion As accesoDatos

#End Region

#Region "Atributos"
    Public Property id() As Long
        Get
            Return iId
        End Get
        Set(ByVal Value As Long)
            iId = Value
        End Set
    End Property

    Public Property descripcion() As String
        Get
            Return iDescripcion
        End Get
        Set(ByVal Value As String)
            iDescripcion = Value
        End Set
    End Property
#End Region

#Region "Metodos"

    Public Function obtenerTipoDocumento() As TipoDocumento
        Try

            iId = id
            Select Case id
                Case TipoDocumento.DNI
                    iDescripcion = "DNI"
                Case TipoDocumento.CI
                    iDescripcion = "CI"
                Case TipoDocumento.LE
                    iDescripcion = "LE"
                Case TipoDocumento.LC
                    iDescripcion = "LC"
                Case TipoDocumento.PAS
                    iDescripcion = "PAS"
                Case TipoDocumento.CUIT
                    iDescripcion = "CUIT"
                Case TipoDocumento.LECABAL
                    iDescripcion = "LE"
                Case TipoDocumento.LCCABAL
                    iDescripcion = "LC"
                Case TipoDocumento.DNICABAL
                    iDescripcion = "DNI"
                Case TipoDocumento.CICABAL
                    iDescripcion = "CI"
                Case TipoDocumento.PASCABAL
                    iDescripcion = "PAS"
                Case Else
                    Throw New TipoDocumentoNoEncontradoException()
            End Select

        Catch excepcion As Exception
            Throw New TipoDocumentoNoEncontradoException(excepcion)
        Finally
        End Try
    End Function

    MustOverride Function isDNI() As Boolean
    MustOverride Function isLC() As Boolean
    MustOverride Function isLE() As Boolean
    MustOverride Function isCI() As Boolean
    MustOverride Function isPAS() As Boolean
    MustOverride Function isCUIT() As Boolean

    Public Shared Function obtenerTiposDocumento() As SortedList
        Dim iSortedList As New SortedList()
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                iSortedList.Add(DNI, "DNI")
                iSortedList.Add(CI, "CI")
                iSortedList.Add(LC, "LC")
                iSortedList.Add(LE, "LE")
                iSortedList.Add(PAS, "PAS")
                iSortedList.Add(CUIT, "CUIT")
            Case "URUGUAY"
                iSortedList.Add(CI, "CI")
                iSortedList.Add(CUIT, "RUC")
            Case "PARAGUAY"
                iSortedList.Add(CI, "CI")
            Case "COLOMBIA"
                iSortedList.Add(CI, "CI")
                iSortedList.Add(CUIT, "RUT")
        End Select
        Return iSortedList
    End Function

    Public Shared Function obtenerTiposDocumentoClienteAlta() As SortedList
        Dim iSortedList As New SortedList()
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                iSortedList.Add(DNI, "DNI")
                iSortedList.Add(LC, "LC")
                iSortedList.Add(LE, "LE")
            Case "URUGUAY"
                iSortedList.Add(CI, "CI")
            Case "PARAGUAY"
                iSortedList.Add(CI, "CI")
            Case "COLOMBIA"
                iSortedList.Add(CI, "CI")
        End Select
        Return iSortedList
    End Function

    Public Shared Function obtenerTipoDocumentoPorDefecto() As TipoDocumento
        Dim iSortedList As New SortedList()
        Select Case ConfigurationManager.AppSettings("Region")
            Case "ARGENTINA"
                Return New DNI
            Case "URUGUAY"
                Return New CI
            Case "PARAGUAY"
                Return New CI
            Case "COLOMBIA"
                Return New CI
        End Select
    End Function

    Public Sub dispose()
        Try
            If Not IsNothing(iConexion) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        Catch exception As Exception
            Throw New RootException(exception)
        End Try
    End Sub

#End Region

End Class
