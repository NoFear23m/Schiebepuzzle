Namespace ViewModel.Infrastructure
    Public Interface IDialogWindowService

        Function ShowModalDialog(windowname As String, datacontext As Object, owner As Object, Optional topMost As Boolean = False, Optional showInTaskbar As Boolean = True) As Boolean
        Sub CloseDialog()
        Sub CloseDialog(vm As Object)

    End Interface
End Namespace
