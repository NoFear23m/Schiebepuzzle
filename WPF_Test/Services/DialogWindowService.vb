Imports Schiebepuzzle.ViewModel.Infrastructure

Namespace Services
    Public Class DialogWindowService
        Implements IDialogWindowService

        Private _currentSpsWindow As SpsWindow



        Public Function ShowModalDialog(windowname As String, datacontext As Object, owner As Object, Optional topMost As Boolean = False, Optional showInTaskbar As Boolean = True) As Boolean Implements IDialogWindowService.ShowModalDialog
            Dim spswin As New SpsWindow(windowname, datacontext, SizeToContent.WidthAndHeight, WindowStartupLocation.CenterOwner)
            _currentSpsWindow = spswin

            spswin.Topmost = topMost
            spswin.ShowInTaskbar = showInTaskbar
            spswin.Owner = spswin.FindOwnerWindow(owner)
            spswin.WindowStyle = WindowStyle.SingleBorderWindow
            spswin.ShowMaxRestoreButton = False
            spswin.ShowMinButton = False
            spswin.AsSpsModalDialog = True

            Return CType(Application.Current.Dispatcher.Invoke(Function() spswin.ShowDialog), Boolean)
        End Function

        Public Sub CloseDialog() Implements IDialogWindowService.CloseDialog

            If _currentSpsWindow IsNot Nothing Then
                _currentSpsWindow.Close()
            End If
        End Sub

        Public Sub CloseDialog(vm As Object) Implements IDialogWindowService.CloseDialog
            Dim owner As Window = Application.Current.Windows.Cast(Of Window)().SingleOrDefault(Function(x) x.DataContext IsNot Nothing AndAlso x.DataContext.GetType Is vm.GetType)

            If owner Is Nothing Then
                For Each window As Window In (From win In Application.Current.Windows()).ToList
                    If window.DataContext.GetType Is vm.GetType Then
                        window.Close()
                    End If
                Next
            Else
                owner.Close()
            End If
        End Sub

    End Class
End Namespace