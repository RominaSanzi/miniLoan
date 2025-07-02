Imports di.financiera.datos
Imports di.financiera.excepciones
Imports di.financiera.utils
Imports di.financiera.entidades
Imports di.financiera.seguridad

Public Class Grupo

    Inherits Entidad

    Enum enumRelacionLimite
        GRUPO = 1
        INGRESO = 2
    End Enum

#Region "Variables"
    Private iId As Long
    Private iCodigo As Long
    Private iDescripcion As String
    Private iLimiteCreditoMaximo As Double
    Private iLimiteCreditoMinimo As Double
    Private iMontoExcedente As Double
    Private iBloqueante As Boolean 'Determina si se emiten plásticos y si se encuentra para operar
    Private iRelacionLimite As enumRelacionLimite
    Private iCoeficienteMultiplicador As Double
    Private iProporcion As Double
    Private iIncluyeBonifComercio As Boolean 'Permite que el grupo se utilize para la bonificacion de comercios

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

    Public Property codigo As Long
        Get
            Return iCodigo
        End Get
        Set(value As Long)
            iCodigo = value
        End Set
    End Property

    Public Property descripcion As String
        Get
            Return iDescripcion
        End Get
        Set(value As String)
            iDescripcion = value
        End Set
    End Property

    Public Property limiteCreditoMaximo As Double
        Get
            Return iLimiteCreditoMaximo
        End Get
        Set(value As Double)
            iLimiteCreditoMaximo = value
        End Set
    End Property

    Public Property limiteCreditoMinimo As Double
        Get
            Return iLimiteCreditoMinimo
        End Get
        Set(value As Double)
            iLimiteCreditoMinimo = value
        End Set
    End Property

    Public Property montoExcedente As Double
        Get
            Return iMontoExcedente
        End Get
        Set(value As Double)
            iMontoExcedente = value
        End Set
    End Property

    Public Property bloqueante As Boolean
        Get
            Return iBloqueante
        End Get
        Set(value As Boolean)
            iBloqueante = value
        End Set
    End Property

    Public Property relacionLimite As enumRelacionLimite
        Get
            Return iRelacionLimite
        End Get
        Set(value As enumRelacionLimite)
            iRelacionLimite = value
        End Set
    End Property

    Public Property coeficienteMultiplicador As Double
        Get
            Return iCoeficienteMultiplicador
        End Get
        Set(value As Double)
            iCoeficienteMultiplicador = value
        End Set
    End Property

    Public Property proporcion As Double
        Get
            Return iProporcion
        End Get
        Set(value As Double)
            iProporcion = value
        End Set
    End Property

    Public Property incluyeBonifComercio As Boolean
        Get
            Return iIncluyeBonifComercio
        End Get
        Set(value As Boolean)
            iIncluyeBonifComercio = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Private Sub validarCrear()

        Try

            If iCodigo = Nothing Then
                Throw New GrupoNoCreadoException("El codigo no puede ser nulo")
            End If
            If iDescripcion = Nothing Then
                Throw New GrupoNoCreadoException("El descripcion no puede ser nulo")
            End If
            If iLimiteCreditoMaximo = Nothing Then
                Throw New GrupoNoCreadoException("La cantidad de dias vencimiento no puede ser nulo")
            End If
            If iLimiteCreditoMinimo = Nothing Then
                Throw New GrupoNoCreadoException("La cantidad de digitos no puede ser nulo")
            End If
            If limiteCreditoMaximo < limiteCreditoMinimo Then
                Throw New GrupoNoCreadoException("El limite minimo debe ser mayor al maximo")
            End If
            If limiteCreditoMinimo < iMontoExcedente Then
                Throw New GrupoNoCreadoException("El monto excedente no puede superar el 100% del total limite del grupo")
            End If

            If iRelacionLimite = enumRelacionLimite.INGRESO Then
                If coeficienteMultiplicador = Nothing Then
                    Throw New GrupoNoCreadoException("Si utilizas la configuracion de Ingreso, debes completar el coeficiente multiplicador.")
                End If
                If proporcion = Nothing Then
                    Throw New GrupoNoCreadoException("Si utilizas la configuracion de Ingreso, debes completar la proporcion.")
                End If
                If proporcion > 1 Then
                    Throw New GrupoNoCreadoException("La proporcion no puede superar el 1.")
                End If

            End If

            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader

            Try
                iGeneradorSql.agregarTabla("Grupo")
                iGeneradorSql.agregarColumna("id")

                iGeneradorSql.agregarCondicionWhereConOr("codigo=" & codigo)
                iGeneradorSql.agregarCondicionWhereConOr("descripcion='" & descripcion & "'")
                iGeneradorSql.agregarCondicionWhere(iGeneradorSql.generarWhereConOr, True)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    Throw New GrupoNoCreadoException("Ya existe el grupo que desea crear")
                End If

                iDataReader.Close()

            Catch excepcion As Exception
                Throw New BancoNoCreadoException(excepcion)
            Finally
                If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
                iDataReader = Nothing
                iGeneradorSql.destructor()
                iGeneradorSql = Nothing
            End Try

        Catch excepcion As Exception
            Throw New GrupoNoCreadoException(excepcion)
        End Try
    End Sub

    Public Sub crear()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarCrear()

            iGeneradorSql.agregarTabla("Grupo")

            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("limitecreditomaximo")
            iGeneradorSql.agregarColumna("limitecreditominimo")
            iGeneradorSql.agregarColumna("montoexcedente")
            iGeneradorSql.agregarColumna("bloqueante")
            iGeneradorSql.agregarColumna("relacionLimite")
            iGeneradorSql.agregarColumna("coeficienteMultiplicador")
            iGeneradorSql.agregarColumna("proporcion")
            iGeneradorSql.agregarColumna("incluyeBonifComercio")

            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(limiteCreditoMaximo))
            iGeneradorSql.agregarValue(FuncionComun.nuloSiEsNothing(limiteCreditoMinimo))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(montoExcedente))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(bloqueante))
            iGeneradorSql.agregarValue(relacionLimite)
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(coeficienteMultiplicador))
            iGeneradorSql.agregarValue(FuncionComun.ceroSiEsNothing(proporcion))
            iGeneradorSql.agregarValue(FuncionComun.booleanByte(incluyeBonifComercio))

            iId = iConexion.ejecutarInsert(iGeneradorSql.generarInsert, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New GrupoNoCreadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Private Sub validarModificar()
        Try

            If id = Nothing Then
                Throw New GrupoNoCreadoException("No envio el identificador del grupo para realizar la modificacion")
            End If

            If codigo = Nothing Then
                Throw New GrupoNoCreadoException("El codigo no puede ser nulo")
            End If
            If descripcion = Nothing Then
                Throw New GrupoNoCreadoException("La descripcion no puede ser nula")
            End If
            If limiteCreditoMaximo = Nothing Then
                Throw New GrupoNoCreadoException("La cantidad de dias vencimiento no puede ser nulo")
            End If
            If limiteCreditoMinimo = Nothing Then
                Throw New GrupoNoCreadoException("La cantidad de digitos no puede ser nulo")
            End If
            If limiteCreditoMaximo < limiteCreditoMinimo Then
                Throw New GrupoNoCreadoException("El limite minimo debe ser mayor al maximo")
            End If
            If limiteCreditoMinimo < iMontoExcedente Then
                Throw New GrupoNoCreadoException("El monto excedente no puede superar el 100% del total limite del grupo")
            End If

            If iRelacionLimite = enumRelacionLimite.INGRESO Then
                If coeficienteMultiplicador = Nothing Then
                    Throw New GrupoNoCreadoException("Si utilizas la configuracion de Ingreso, debes completar el coeficiente multiplicador.")
                End If
                If proporcion = Nothing Then
                    Throw New GrupoNoCreadoException("Si utilizas la configuracion de Ingreso, debes completar la proporcion.")
                End If
                If proporcion > 1 Then
                    Throw New GrupoNoCreadoException("La proporcion no puede superar el 1.")
                End If
            End If

            Dim iGeneradorSql As New GeneradorSql
            Dim iDataReader As IDataReader

            Try

                iGeneradorSql.agregarTabla("Grupo")
                iGeneradorSql.agregarColumna("id")

                iGeneradorSql.agregarCondicionWhere("id<>" & id)
                iGeneradorSql.agregarCondicionWhereConOr("codigo=" & codigo)
                iGeneradorSql.agregarCondicionWhereConOr("descripcion='" & descripcion & "'")
                iGeneradorSql.agregarCondicionWhere(iGeneradorSql.generarWhereConOr, True)

                iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

                If iDataReader.Read Then
                    Throw New GrupoNoModificadoException("Ya existe el grupo")
                End If

                iDataReader.Close()

            Catch excepcion As Exception
                Throw New GrupoNoModificadoException(excepcion)
            Finally
                If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
                iDataReader = Nothing
                iGeneradorSql.destructor()
                iGeneradorSql = Nothing
            End Try

        Catch excepcion As Exception
            Throw New GrupoNoModificadoException(excepcion)
        End Try
    End Sub

    Public Sub modificar()
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            validarModificar()

            iGeneradorSql.agregarSet("codigo=" & FuncionComun.nuloSiEsNothing(codigo))
            iGeneradorSql.agregarSet("descripcion=" & FuncionComun.nuloSiEsNothing(descripcion))
            iGeneradorSql.agregarSet("limitecreditomaximo=" & FuncionComun.nuloSiEsNothing(limiteCreditoMaximo))
            iGeneradorSql.agregarSet("limitecreditominimo=" & FuncionComun.nuloSiEsNothing(limiteCreditoMinimo))
            iGeneradorSql.agregarSet("montoexcedente=" & FuncionComun.nuloSiEsNothing(montoExcedente))
            iGeneradorSql.agregarSet("bloqueante=" & FuncionComun.booleanByte(bloqueante))
            iGeneradorSql.agregarSet("relacionLimite=" & relacionLimite)
            iGeneradorSql.agregarSet("coeficienteMultiplicador=" & FuncionComun.ceroSiEsNothing(coeficienteMultiplicador))
            iGeneradorSql.agregarSet("proporcion=" & FuncionComun.ceroSiEsNothing(proporcion))
            iGeneradorSql.agregarSet("incluyeBonifComercio=" & FuncionComun.booleanByte(incluyeBonifComercio))

            iGeneradorSql.agregarTabla("Grupo")
            iGeneradorSql.agregarCondicionWhere("id=" & id)

            iConexion.ejecutar(iGeneradorSql.generarUpdate, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New GrupoNoModificadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Function obtenerGrupoGrilla() As DataSet
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("limitecreditomaximo")
            iGeneradorSql.agregarColumna("limitecreditominimo")
            iGeneradorSql.agregarColumna("IFNULL(montoexcedente,0) AS montoexcedente")
            iGeneradorSql.agregarColumna("bloqueante")
            iGeneradorSql.agregarColumna("relacionLimite")
            iGeneradorSql.agregarColumna("coeficienteMultiplicador")
            iGeneradorSql.agregarColumna("proporcion")

            iGeneradorSql.agregarTabla("Grupo")

            If codigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo=" & codigo)
            If descripcion <> Nothing Then iGeneradorSql.agregarCondicionWhere("descripcion like '" & descripcion & "%'")

            iGeneradorSql.agregarLimit("1000")

            Return iConexion.getDataSet(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL, "Grupo")

        Catch excepcion As Exception
            Throw New GrupoNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Function

    Public Function obtenerGrupo() As Grupo
        Dim iDataReader As IDataReader
        Dim iGeneradorSql As New GeneradorSql

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("limitecreditomaximo")
            iGeneradorSql.agregarColumna("limitecreditominimo")
            iGeneradorSql.agregarColumna("montoexcedente")
            iGeneradorSql.agregarColumna("bloqueante")
            iGeneradorSql.agregarColumna("relacionLimite")
            iGeneradorSql.agregarColumna("coeficienteMultiplicador")
            iGeneradorSql.agregarColumna("proporcion")
            iGeneradorSql.agregarColumna("incluyeBonifComercio")

            iGeneradorSql.agregarTabla("Grupo")

            If iId <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            If iCodigo <> Nothing Then iGeneradorSql.agregarCondicionWhere("codigo=" & codigo)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                iId = FuncionComun.nothingSiEsNulo(iDataReader.Item("Id"))
                iCodigo = FuncionComun.nothingSiEsNulo(iDataReader.Item("Codigo"))
                iDescripcion = FuncionComun.nothingSiEsNulo(iDataReader.Item("Descripcion"))
                iLimiteCreditoMaximo = FuncionComun.nothingSiEsNulo(iDataReader.Item("limitecreditomaximo"))
                iLimiteCreditoMinimo = FuncionComun.nothingSiEsNulo(iDataReader.Item("limitecreditominimo"))
                iMontoExcedente = FuncionComun.nothingSiEsNulo(iDataReader.Item("montoexcedente"))
                iBloqueante = FuncionComun.byteBoolean(iDataReader.Item("bloqueante"))
                iRelacionLimite = iDataReader.Item("relacionLimite")
                iCoeficienteMultiplicador = FuncionComun.ceroSiEsNulo(iDataReader.Item("coeficienteMultiplicador"))
                iProporcion = FuncionComun.ceroSiEsNulo(iDataReader.Item("proporcion"))
                iIncluyeBonifComercio = FuncionComun.byteBoolean(iDataReader.Item("incluyeBonifComercio"))

                iDataReader.Close()

                Return Me
            Else
                Throw New GrupoNoEncontradoException()
            End If

        Catch excepcion As Exception
            Throw New GrupoNoEncontradoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Function obtenerGruposLista() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("limitecreditomaximo")
            iGeneradorSql.agregarColumna("limitecreditominimo")
            iGeneradorSql.agregarColumna("montoexcedente")
            iGeneradorSql.agregarColumna("bloqueante")
            iGeneradorSql.agregarColumna("relacionLimite")
            iGeneradorSql.agregarColumna("coeficienteMultiplicador")
            iGeneradorSql.agregarColumna("proporcion")
            iGeneradorSql.agregarColumna("incluyeBonifComercio")

            iGeneradorSql.agregarTabla("Grupo")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)

            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New GrupoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function


    Public Function obtenerGruposBonifLista() As IDataReader
        Dim iGeneradorSql As New GeneradorSql
        Dim iDataReader As IDataReader

        Try
            iConexion = obtenerConexion()

            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarColumna("codigo")
            iGeneradorSql.agregarColumna("descripcion")
            iGeneradorSql.agregarColumna("limitecreditomaximo")
            iGeneradorSql.agregarColumna("limitecreditominimo")
            iGeneradorSql.agregarColumna("montoexcedente")
            iGeneradorSql.agregarColumna("bloqueante")
            iGeneradorSql.agregarColumna("relacionLimite")
            iGeneradorSql.agregarColumna("coeficienteMultiplicador")
            iGeneradorSql.agregarColumna("proporcion")
            iGeneradorSql.agregarColumna("incluyeBonifComercio")

            iGeneradorSql.agregarTabla("Grupo")

            If id <> Nothing Then iGeneradorSql.agregarCondicionWhere("id=" & id)
            iGeneradorSql.agregarCondicionWhere("incluyeBonifComercio=" & FuncionComun.booleanByte(True))
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            Return iDataReader

        Catch excepcion As Exception
            Throw New GrupoNoEncontradoException(excepcion)
        Finally
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try

    End Function

    Public Sub validarEliminar()
        Dim iGeneradorSql As GeneradorSql
        Dim iDataReader As IDataReader

        Try

            iGeneradorSql = New GeneradorSql
            iGeneradorSql.agregarTabla("cliente")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idGrupo=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New GrupoNoModificadoException("El grupo esta asociado a un cliente")
            End If
            iDataReader.Close()

            iGeneradorSql = New GeneradorSql
            iGeneradorSql.agregarTabla("plandecreditogrupo")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idGrupo=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New GrupoNoModificadoException("El grupo esta asociado a un plan de credito")
            End If
            iDataReader.Close()

            iGeneradorSql = New GeneradorSql
            iGeneradorSql.agregarTabla("tarjeta")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idGrupo=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New GrupoNoModificadoException("El grupo esta asociado a tarjetas")
            End If
            iDataReader.Close()

            iGeneradorSql = New GeneradorSql
            iGeneradorSql.agregarTabla("politicacomercialgrupo")
            iGeneradorSql.agregarColumna("id")
            iGeneradorSql.agregarCondicionWhere("idGrupo=" & id)
            iDataReader = iConexion.getDataReader(iGeneradorSql.generarSelect, iGeneradorSql.parametrosSQL)

            If iDataReader.Read Then
                Throw New GrupoNoModificadoException("El grupo esta asociado a una politica comercial")
            End If
            iDataReader.Close()

        Catch excepcion As Exception
            Throw New BancoNoModificadoException(excepcion)
        Finally
            If Not IsNothing(iDataReader) AndAlso Not iDataReader.IsClosed Then iDataReader.Close()
            iDataReader = Nothing
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

    Public Sub eliminar()
        Dim iGeneradorSql As New GeneradorSql
        Try
            iConexion = obtenerConexion()

            validarEliminar()

            iGeneradorSql.agregarTabla("Grupo")
            iGeneradorSql.agregarCondicionWhere("id=" & id)
            iConexion.ejecutar(iGeneradorSql.generarDelete, iGeneradorSql.parametrosSQL)

        Catch excepcion As Exception
            Throw New GrupoNoEliminadoException(excepcion)
        Finally
            If (IsNothing(MyBase.accesoDatos)) Then
                iConexion.cerrar()
                iConexion = Nothing
            End If
            iGeneradorSql.destructor()
            iGeneradorSql = Nothing
        End Try
    End Sub

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
