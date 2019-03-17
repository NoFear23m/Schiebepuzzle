Imports System.Threading
Imports Schiebepuzzle.ViewModel.Infrastructure

Public Class MessageboxService
    Implements IMessageboxService
    Private Function IMessageBoxService_Show(text As String, caption As String,Optional buttons As EnuMessageBoxButton = 0,Optional image As EnuMessageBoxImage = 0) As EnuMessageBoxResult Implements IMessageboxService.Show
        Thread.CurrentThread.TrySetApartmentState(ApartmentState.STA)
        Dim wcServ As IWaitingCursorService = ServiceContainer.GetService(Of IWaitingCursorService)
        wcServ.SetWaitingCursor(False)
        Dim msgBoxResult As MessageBoxResult = Application.Current.Dispatcher.Invoke(Function() MessageBox.Show(text, caption, DirectCast(buttons, MessageBoxButton), DirectCast(image, MessageBoxImage)))

        Return CType(msgBoxResult, EnuMessageBoxResult)
    End Function

End Class


