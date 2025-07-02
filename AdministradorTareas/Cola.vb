Imports System.Threading

Public Interface Cola
    Sub put(ByVal eTask As RunnableTask)
    Function take() As RunnableTask
End Interface
