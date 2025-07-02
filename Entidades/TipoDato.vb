Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public MustInherit Class TipoDato

#Region "Constantes"
    Public Const DATOSTRING = 1
    Public Const DATOINTEGER = 2
    Public Const DATOLONG = 3
    Public Const DATODOUBLE = 4
    Public Const DATODATE = 5
#End Region

#Region "Variables"
    Private iId As Long
    Private iDescripcion As String

    Public Property id As Long
        Get
            Return iId
        End Get
        Set(value As Long)
            iId = value
        End Set
    End Property
#End Region

#Region "Metodos"

    MustOverride Function isDatoString() As Boolean
    MustOverride Function isDatoInteger() As Boolean
    MustOverride Function isDatoLong() As Boolean
    MustOverride Function isDatoDouble() As Boolean
    MustOverride Function isDatoDate() As Boolean

    Public Shared Function obtenerTiposDato() As Collection
        Dim iColeccion As New Collection()
        iColeccion.Add("DATOSTRING", DATOSTRING)
        iColeccion.Add("DATOINTEGER", DATOINTEGER)
        iColeccion.Add("DATOLONG", DATOLONG)
        iColeccion.Add("DATODOUBLE", DATODOUBLE)
        iColeccion.Add("DATODATE", DATODATE)

        Return iColeccion
    End Function
#End Region

End Class
