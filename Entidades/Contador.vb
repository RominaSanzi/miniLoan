Imports System.ServiceModel
Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils

Public Class Contador
    Inherits Entidad

#Region "Variables"
    Private iId As Long
    Private iValorActual As Integer

    Private iConexion As accesoDatos
#End Region
#Region "Atributos"
    Public Property id As Long
        Get
            Return iId
        End Get
        Set(value As Long)
            iId = value
        End Set
    End Property
    Public Property valorActual As Integer
        Get
            Return iValorActual
        End Get
        Set(value As Integer)
            iValorActual = value
        End Set
    End Property
#End Region
#Region "Metodos"
    Public Function obtenerContador() As Contador
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("valorActual")

            iGeneradorSql.agregarTabla("contador")
            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If valorActual <> Nothing Then iGeneradorSql.agregarCondicionWhere("valorActual=" & FuncionComun.nuloSiEsNothing(valorActual))

            Using iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect(), iGeneradorSql.parametrosSQL)
                If iDataReader.Read() Then
                    id = iDataReader.Item("id").ToString()
                    valorActual = FuncionComun.ceroSiEsNulo(iDataReader.Item("valorActual").ToString())
                    iDataReader.Close()

                    Return Me
                Else
                    Throw New ContadorNoEncontradoException()
                End If
            End Using
        Catch excepcion As Exception
            Throw New ContadorNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Function

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            ' validarCrear() TODO: Validar crear

            iGeneradorSql.agregarTabla("Contador")
            iGeneradorSql.agregarColumna("valorActual")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(valorActual))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New ContadorNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Contador")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New ContadorNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql()

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarTabla("Contador")
            iGeneradorSql.agregarSet("valorActual=" & FuncionComun.nuloSiEsNothing(valorActual))
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New ContadorNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
        End Try
    End Sub
#End Region
End Class
