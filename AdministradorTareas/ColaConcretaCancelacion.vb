Imports System
Imports System.Data
Imports System.Threading


Public Class ColaConcretaCancelacion : Implements Cola

#Region "Variables"
    Private iTareas As New Collection()
    Private iTareaEjecutandose As RunnableTask
    Private iWaiting As Boolean = False
    Private iShutdown As Boolean = True
    Private Shared iInstancia As ColaConcretaCancelacion
    Private iThreadStart As Thread
    Private Shared iMutex As New System.Threading.Mutex()

#End Region

#Region "Atributos"
    Public Property waiting() As Boolean
        Get
            Return iWaiting
        End Get
        Set(ByVal Value As Boolean)
            iWaiting = Value
        End Set
    End Property
    Public Property shutdown() As Boolean
        Get
            Return iShutdown
        End Get
        Set(ByVal Value As Boolean)
            iShutdown = Value
        End Set
    End Property

    Public Property tareaEjecutandose As RunnableTask
        Get
            Return iTareaEjecutandose
        End Get
        Set(value As RunnableTask)
            iTareaEjecutandose = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Shared Function getInstancia() As ColaConcretaCancelacion
        Try
            iMutex.WaitOne()
            If iInstancia Is Nothing Then iInstancia = New ColaConcretaCancelacion()
            iMutex.ReleaseMutex()
            Return iInstancia

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Function

    Sub put(ByVal eTask As RunnableTask) Implements Cola.put
        Try
            'agregamos el nuevo task a la cola
            eTask.id = Now.ToString("yyyyMMddHHmmssfff")
            iTareas.Add(eTask)
            iShutdown = False

            If IsNothing(iThreadStart) OrElse Not iThreadStart.IsAlive() Then
                iThreadStart = New Thread(AddressOf Me.ejecutarTareas)
                iThreadStart.Start()
            End If

            If (iWaiting) Then
                SyncLock (iTareas)
                    Monitor.PulseAll(iTareas)
                End SyncLock
            End If
        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Sub

    Public Function take() As RunnableTask Implements Cola.take
        Try
            'si hay por lo menos un task en la cola lo devolemos evitando la 
            'concurrencia de varios threads
            Dim iTarea As RunnableTask

            If (iTareas.Count <= 0) Then
                SyncLock (iTareas)
                    iShutdown = True
                    iWaiting = False
                End SyncLock
            Else
                iTarea = iTareas.Item(1)
                tareaEjecutandose = iTareas.Item(1)
                tareaEjecutandose.horaInicio = Now.ToString("HH:mm:ss")
                iTareas.Remove(1)
                If (iTareas.Count <= 0) Then iShutdown = True
            End If

            Return iTarea

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Function

    Friend Sub ejecutarTareas()
        Dim iRunnableTask As RunnableTask

        While (Not iShutdown)
            Try
                iRunnableTask = take()
                If Not IsNothing(iRunnableTask) Then iRunnableTask.execute()
                iTareaEjecutandose = Nothing

            Catch exception As Exception
                'Agarramos las excepcines para que no se pare la cola de tareas por un error
            End Try
        End While
    End Sub

    Public Function obtenerTareas() As DataTable
        Dim iDataTableTareas As New DataTable
        Dim iDataRow As DataRow
        Dim i As Integer


        Try

            iDataTableTareas.Columns.Add(New DataColumn("id", GetType(String)))
            iDataTableTareas.Columns.Add(New DataColumn("Tarea", GetType(String)))
            iDataTableTareas.Columns.Add(New DataColumn("HoraInicio", GetType(String)))
            iDataTableTareas.Columns.Add(New DataColumn("Estado", GetType(String)))
            '  iDataTableTareas.Columns.Add(New DataColumn("Cancelar", GetType(String)))

            'primero agrego la tarea que se esta ejecutando
            If Not IsNothing(iTareaEjecutandose) <> Nothing Then
                iDataRow = iDataTableTareas.NewRow
                iDataRow("id") = iTareaEjecutandose.id
                iDataRow("Tarea") = Replace(iTareaEjecutandose.ToString, "di.financiera.tareas.Tarea", Nothing)
                iDataRow("HoraInicio") = iTareaEjecutandose.horaInicio
                iDataRow("Estado") = "En ejecución"
                '  iDataRow("Cancelar") = "[Cancelar]"
                iDataTableTareas.Rows.Add(iDataRow)
            End If

            For i = 1 To iTareas.Count
                iDataRow = iDataTableTareas.NewRow
                iDataRow("id") = CType(iTareas.Item(i), RunnableTask).id
                iDataRow("Tarea") = Replace(iTareas.Item(i).ToString, "di.financiera.tareas.Tarea", Nothing)
                iDataRow("HoraInicio") = ""
                iDataRow("Estado") = "En espera"
                '  iDataRow("Cancelar") = "[Cancelar]"
                iDataTableTareas.Rows.Add(iDataRow)
            Next i

            Return iDataTableTareas

        Catch exception As Exception
            Throw exception
        Finally
            iDataTableTareas = Nothing
            iDataRow = Nothing
        End Try
    End Function

    Public Sub eliminarTarea(eId As String)
        Dim i As Integer
        Try
            For i = iTareas.Count To 1 Step -1
                If CType(iTareas(i), RunnableTask).id = eId Then
                    iTareas.Remove(i)
                End If
            Next i

        Catch exception As Exception
            'Agarramos las excepcines para que no se pare la cola de tareas por un error
        End Try
    End Sub

#End Region

End Class
