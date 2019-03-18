Namespace ViewModel
    Public Class WinBoxVm
        Inherits ViewModelBase

        Public Sub New()

        End Sub

        Public Sub New(totalMoves As Integer, elapsedTime As TimeSpan)
            Moves = totalMoves
            Time = elapsedTime
        End Sub

        Public Property Moves As Integer = 0
        Public Property Time As TimeSpan





    End Class
End Namespace
