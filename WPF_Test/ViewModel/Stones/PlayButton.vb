Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace Model.Stones
    Public MustInherit Class PlayButton
        Inherits PlayStoneBase
        Implements INotifyPropertyChanged


        Public Sub New()
            StoneType = PlayStoneType.NormalButton
        End Sub


        Private _number As Integer

        Public Property Number() As Integer
            Get
                Return _number
            End Get
            Set(ByVal value As Integer)
                _number = value
                RaisePropertyChanged()
            End Set
        End Property

        Friend Sub RaisePropertyChanged(<CallerMemberName> Optional member As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(member))
        End Sub


        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace