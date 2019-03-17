Imports System.Windows.Threading
Imports Schiebepuzzle.ViewModel.Infrastructure

Namespace Services

    Public Class WaitingCursorService
        Implements IWaitingCursorService

        Public Sub SetWaitingCursor(isWaiting As Boolean) Implements IWaitingCursorService.SetWaitingCursor
            If isWaiting Then
                Application.Current.Dispatcher.Invoke(Sub() Mouse.OverrideCursor = Cursors.Wait)
            Else
                Application.Current.Dispatcher.Invoke(Sub() Mouse.OverrideCursor = Nothing)
            End If
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, New Action(Sub()

                                                                                            End Sub))
        End Sub

        Public Sub SetWaiting() Implements IWaitingCursorService.SetWaiting
            SetWaitingCursor(True)
        End Sub

        Public Sub ResetCursor() Implements IWaitingCursorService.ResetCursor
            SetWaitingCursor(False)
        End Sub
    End Class
End Namespace