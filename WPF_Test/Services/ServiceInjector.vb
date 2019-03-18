
Imports Schiebepuzzle.ViewModel.Infrastructure

Namespace Services

    Public NotInheritable Class ServiceInjector
        Private Sub New()
        End Sub
        ' Loads service objects into the ServiceContainer on startup.
        Public Shared Sub InjectServices()
            ServiceContainer.Instance.AddService(Of IMessageboxService)(New MessageboxService())
            ServiceContainer.Instance.AddService(Of IWaitingCursorService)(New WaitingCursorService())
            ServiceContainer.Instance.AddService(Of IDialogWindowService)(New DialogWindowService())
        End Sub
    End Class
End Namespace