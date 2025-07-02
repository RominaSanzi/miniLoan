
Public Class DataSetHelper
    Private ds As DataSet

#Region "Variables"
    Private iDataSet As DataSet
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
#End Region

    Public Sub New(ByVal DataSet As DataSet)
        iDataSet = DataSet
    End Sub

    Public Sub New()
        iDataSet = Nothing
    End Sub

    Private Function columnasIguales(ByVal eColumnaA As Object, ByVal eColumnaB As Object) As Boolean
        If eColumnaA Is DBNull.Value And eColumnaB Is DBNull.Value Then Return True
        If eColumnaA Is DBNull.Value Or eColumnaB Is DBNull.Value Then Return False
        Return eColumnaA = eColumnaB
    End Function

    Private Function columnasIguales(ByVal eColumnaA As Object, ByVal eColumnaB As Object, ByVal eColumnaC As Object, ByVal eColumnaD As Object) As Boolean
        If eColumnaA Is DBNull.Value And eColumnaB Is DBNull.Value Then Return True
        If eColumnaA Is DBNull.Value Or eColumnaB Is DBNull.Value Then Return False
        If eColumnaC Is DBNull.Value And eColumnaD Is DBNull.Value Then Return True
        If eColumnaC Is DBNull.Value Or eColumnaD Is DBNull.Value Then Return False

        Return (eColumnaA = eColumnaB) AndAlso (eColumnaC = eColumnaD)

    End Function

    Public Function selectDistinct(ByVal eTablaOrigen As DataTable, ByVal eNombreCampo As String) As DataTable
        Dim iDataTable As New DataTable
        Dim iDataRow As DataRow
        Dim iUltimoValor As Object

        Try
            iDataTable.Columns.Add(eNombreCampo, eTablaOrigen.Columns(eNombreCampo).DataType)
            For Each iDataRow In eTablaOrigen.Select("", eNombreCampo)
                If iUltimoValor Is Nothing OrElse Not columnasIguales(iUltimoValor, iDataRow(eNombreCampo)) Then
                    iUltimoValor = iDataRow(eNombreCampo)
                    iDataTable.Rows.Add(New Object() {iUltimoValor})
                End If
            Next

            Return iDataTable

        Catch exception As Exception
            Throw exception
        End Try
    End Function

    Public Function selectDistinct(ByVal eTablaOrigen As DataTable, ByVal eNombreCampo As String, ByVal eNombreCampo2 As String) As DataTable
        Dim iDataTable As New DataTable
        Dim iDataRow As DataRow
        Dim iUltimoValor As Object
        Dim iUltimoValor2 As Object
        Try
            iDataTable.Columns.Add(eNombreCampo, eTablaOrigen.Columns(eNombreCampo).DataType)
            iDataTable.Columns.Add(eNombreCampo2, eTablaOrigen.Columns(eNombreCampo2).DataType)

            For Each iDataRow In eTablaOrigen.Select("", eNombreCampo & "," & eNombreCampo2)
                If (iUltimoValor Is Nothing AndAlso iUltimoValor2 Is Nothing) OrElse Not columnasIguales(iUltimoValor, iDataRow(eNombreCampo), iUltimoValor2, iDataRow(eNombreCampo2)) Then
                    iUltimoValor = iDataRow(eNombreCampo)
                    iUltimoValor2 = iDataRow(eNombreCampo2)
                    iDataTable.Rows.Add(New Object() {iUltimoValor, iUltimoValor2})
                End If
            Next

            Return iDataTable

        Catch exception As Exception
            Throw exception
        End Try
    End Function

    Public Function selectDistinct(ByVal eTablaOrigen As DataTable, ByVal eNombreCampo As String, eFiltrarCampo As Boolean, eFiltro As String) As DataTable
        Dim iDataTable As New DataTable
        Dim iDataRow As DataRow
        Dim iUltimoValor As Object

        Try
            iDataTable.Columns.Add(eNombreCampo, eTablaOrigen.Columns(eNombreCampo).DataType)
            For Each iDataRow In eTablaOrigen.Select(eFiltro, eNombreCampo)
                If iUltimoValor Is Nothing OrElse Not columnasIguales(iUltimoValor, iDataRow(eNombreCampo)) Then
                    iUltimoValor = iDataRow(eNombreCampo)
                    iDataTable.Rows.Add(New Object() {iUltimoValor})
                End If
            Next

            Return iDataTable

        Catch exception As Exception
            Throw exception
        End Try
    End Function


    Public Sub mergueDataset(ByRef eDataTableOriginal As DataTable, eCampoClave As String, eDataTableAMerguear As DataTable)
        Dim iDataRow As DataRow

        Dim iDataColumn(1) As DataColumn
        iDataColumn(0) = eDataTableOriginal.Columns(eCampoClave)

        eDataTableOriginal.PrimaryKey = iDataColumn


        For Each iRows As DataRow In eDataTableAMerguear.Rows
            iDataRow = eDataTableOriginal.Rows.Find(iRows(eCampoClave))
            If Not IsNothing(iDataRow) Then
                iDataRow.BeginEdit()
                For i As Integer = 0 To eDataTableOriginal.Columns.Count - 1
                    If Not IsNothing(eDataTableAMerguear.Columns(eDataTableOriginal.Columns(i).ColumnName)) Then iDataRow(eDataTableOriginal.Columns(i).ColumnName) = iRows(eDataTableOriginal.Columns(i).ColumnName)
                Next
                iDataRow.EndEdit()
            Else
                iDataRow = eDataTableOriginal.NewRow
                For i As Integer = 0 To eDataTableOriginal.Columns.Count - 1
                    If Not IsNothing(eDataTableAMerguear.Columns(eDataTableOriginal.Columns(i).ColumnName)) Then iDataRow(eDataTableOriginal.Columns(i).ColumnName) = iRows(eDataTableOriginal.Columns(i).ColumnName)
                Next
                eDataTableOriginal.Rows.Add(iDataRow)
            End If
        Next
        eDataTableOriginal.PrimaryKey = Nothing

    End Sub
End Class
