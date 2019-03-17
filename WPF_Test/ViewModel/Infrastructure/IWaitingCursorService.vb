Namespace ViewModel.Infrastructure
    Public Interface IWaitingCursorService

        Sub SetWaitingCursor(isWaiting As Boolean)

        Sub SetWaiting()

        Sub ResetCursor()
    End Interface
End Namespace