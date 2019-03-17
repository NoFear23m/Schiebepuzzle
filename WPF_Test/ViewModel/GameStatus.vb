Imports System.Timers
Imports Schiebepuzzle.Model.Stones

Namespace ViewModel
    Public Class GameStatus
        Inherits ViewModelBase

        Private ReadOnly _runningTimer As Timer

        Public Sub New()
            RunningTime = New TimeSpan(0)
            If Not IsInDesignMode Then
                _runningTimer = New Timer(50)
                AddHandler _runningTimer.Elapsed, AddressOf RunningTimer_Elapsed
                _runningTimer.Start()
            Else
                MoveCount = 5
                ValidPositionedStones = 3
                WrongPositionedStones = 8
                RunningTime = TimeSpan.FromSeconds(325)
            End If
        End Sub

        Private Sub RunningTimer_Elapsed(sender As Object, e As ElapsedEventArgs)
            RunningTime = RunningTime.Add(New TimeSpan(0, 0, 1))
        End Sub


        Friend Sub RefreshStatus(buttons As List(Of PlayStoneBase))
            MoveCount += 1
            _validPositionedStones = 0
            _wrongPositionedStones = 0
            For i As Integer = 0 To buttons.Count - 2
                Dim playStoneBase = TryCast(buttons(i), PlayButton)
                If (playStoneBase IsNot Nothing) Then
                    If playStoneBase.Number = i + 1 Then
                        _validPositionedStones += 1
                    Else
                        _wrongPositionedStones += 1
                    End If
                Else
                    _wrongPositionedStones += 1
                End If
            Next
            RaisePropertyChanged(NameOf(WrongPositionedStones))
            RaisePropertyChanged(NameOf(ValidPositionedStones))
        End Sub



        Private _runningTime As TimeSpan
        Public Property RunningTime() As TimeSpan
            Get
                Return _runningTime
            End Get
            Friend Set(ByVal value As TimeSpan)
                _runningTime = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _wrongPositionedStones As Integer = 0

        Public Property WrongPositionedStones() As Integer
            Get
                Return _wrongPositionedStones
            End Get
            Friend Set(ByVal value As Integer)
                _wrongPositionedStones = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _validPositionedStones As Integer = 0

        Public Property ValidPositionedStones() As Integer
            Get
                Return _validPositionedStones
            End Get
            Friend Set(ByVal value As Integer)
                _validPositionedStones = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _moveCount As Integer = -1

        Public Property MoveCount() As Integer
            Get
                Return _moveCount
            End Get
            Friend Set(ByVal value As Integer)
                _moveCount = value
                RaisePropertyChanged()
            End Set
        End Property

        Public Overrides Sub Dispose(disposing As Boolean)
            MyBase.Dispose(disposing)
            RemoveHandler _runningTimer.Elapsed, AddressOf RunningTimer_Elapsed
            _runningTimer.Dispose()
        End Sub

    End Class
End Namespace