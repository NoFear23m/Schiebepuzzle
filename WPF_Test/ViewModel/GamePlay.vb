Imports System.Collections.ObjectModel
Imports Schiebepuzzle.Helper
Imports Schiebepuzzle.Model
Imports Schiebepuzzle.Model.Stones
Imports Schiebepuzzle.ViewModel.Infrastructure

Namespace ViewModel
    Public Class GamePlay
        Inherits ViewModelBase

        Private ReadOnly _soundCollector As SoundCollector

        ''' <summary>
        ''' !!! Constructor only for Designtime-Support !!!
        ''' </summary>
        Public Sub New()
            If Not IsInDesignMode Then
                Throw New Exception("This Constructor is only for DesignTime-Support")
            Else
                Status = New GameStatus
            End If
        End Sub

        Public Sub New(gameSetting As GamePlaySettingsVm)
            If Not IsInDesignMode Then
                _soundCollector = New SoundCollector()
                GameSettings = gameSetting
                AddHandler GameSettings.SettingsChanged, AddressOf Settings_Changed

                AllButtons = New ObservableCollection(Of PlayStoneBase)
                MixStonesCommand.Execute(Nothing)
            End If
        End Sub

        Private Sub Settings_Changed()
            MixStonesCommand.Execute(Nothing)
        End Sub

        Private _gameSettings As GamePlaySettingsVm

        Public Property GameSettings() As GamePlaySettingsVm
            Get
                Return _gameSettings
            End Get
            Set(ByVal value As GamePlaySettingsVm)
                _gameSettings = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _currentImagePath As String
        Public Property CurrentImagePath As String
            Get
                Return _currentImagePath
            End Get
            Set(value As String)
                _currentImagePath = value
                RaisePropertyChanged()
            End Set
        End Property

        Friend Sub CreateField(fieldType As GamePlaySettingsVm.PlayFieldType)
            AllButtons.Clear()

            If fieldType = GamePlaySettingsVm.PlayFieldType.ImagedPlayField Then
                If Not IO.File.Exists(Environment.CurrentDirectory & "\images\" & GameSettings.SelectedImage) Then
                    ServiceContainer.GetService(Of IMessageboxService).Show("Das Bild für das Spielsfeld wurde nicht gefunden. Lade Spieltyp 'Nummern'", "Bild nicht gefunden")
                    fieldType = GamePlaySettingsVm.PlayFieldType.NumberedPlayField
                End If
            End If
            Select Case fieldType
                Case GamePlaySettingsVm.PlayFieldType.ImagedPlayField
                    Dim pieces As List(Of System.Drawing.Image)
                    Dim picPath = Environment.CurrentDirectory & "\images\" & GameSettings.SelectedImage
                    CurrentImagePath = picPath
                    pieces = ImageHelper.ImageCutter(GameSettings.FieldSize, picPath)
                    For i As Integer = 0 To (GameSettings.Rows * GameSettings.Columns) - 2
                        AllButtons.Add(New ImagePlayButton With {.Image = ImageHelper.ConvertImageToBitMapImage(pieces(i)), .Number = i + 1})
                    Next
                Case GamePlaySettingsVm.PlayFieldType.NumberedPlayField
                    CurrentImagePath = Nothing
                    For i As Integer = 0 To (GameSettings.Rows * GameSettings.Columns) - 2
                        AllButtons.Add(New NumberedPlayButton With {.Number = i + 1})
                    Next
            End Select
            AllButtons.Add(New Placeholder())

        End Sub


        Private _allButtons As ObservableCollection(Of PlayStoneBase)
        Public Property AllButtons() As ObservableCollection(Of PlayStoneBase)
            Get
                Return _allButtons
            End Get
            Set(ByVal value As ObservableCollection(Of PlayStoneBase))
                _allButtons = value
                RaisePropertyChanged()
            End Set
        End Property


        Private _status As GameStatus
        Public Property Status() As GameStatus
            Get
                Return _status
            End Get
            Set(ByVal value As GameStatus)
                _status = value
                RaisePropertyChanged()
            End Set
        End Property




        Private _moveButtonCommand As ICommand
        Public Property MoveButtonCommand() As ICommand
            Get
                If _moveButtonCommand Is Nothing Then _
                    _moveButtonCommand = New RelayCommand(AddressOf MoveButtonCommand_Execute)
                Return _moveButtonCommand
            End Get
            Set(ByVal value As ICommand)
                _moveButtonCommand = value
                RaisePropertyChanged()
            End Set
        End Property


        Private Sub MoveButtonCommand_Execute(obj As Object)
            If Status.IsGameWon Then Exit Sub
            Dim currIndex = AllButtons.IndexOf(DirectCast(obj, PlayButton))
            Dim placeholder = AllButtons.Where(Function(x) x.StoneType = PlayStoneType.Placeholder).Single
            Dim emptyIndex = AllButtons.IndexOf(placeholder)

            'Liegt Stein unmittelbar neben dem Placeholder?
            If IsStoneNeerPlaceholder(currIndex, emptyIndex) Then
                If Status.GameTimerStatus <> TimerStatus.Running Then Status.StartGameTimer()
                DoMoveButton(currIndex, emptyIndex, GameSettings.PlaySounds)
                Status.RefreshStatus(AllButtons.ToList)
            End If
        End Sub

        Private Function IsStoneNeerPlaceholder(stoneindex As Integer, placeholderIndex As Integer) As Boolean
            Dim ret = (placeholderIndex - GameSettings.Columns) = stoneindex OrElse (placeholderIndex + GameSettings.Columns) = stoneindex OrElse stoneindex - placeholderIndex = 1 OrElse placeholderIndex - stoneindex = 1
            If ret Then
                If (placeholderIndex + 1) Mod GameSettings.Columns = 0 Then 'Der Stein ist ganz rechts
                    If stoneindex - 1 = placeholderIndex Then ret = False
                End If
                If placeholderIndex Mod GameSettings.Columns = 0 Then 'Der Stein ist ganz links
                    If stoneindex + 1 = placeholderIndex Then ret = False
                End If
            End If
            Return ret
        End Function

        Private Sub DoMoveButton(stoneindex As Integer, placeholderindex As Integer, Optional withSound As Boolean = True)
            If withSound Then _soundCollector.PlaySound(SoundType.StoneMove)
            Dim placeHolder = AllButtons(placeholderindex)
            AllButtons.Move(stoneindex, placeholderindex)
            AllButtons.Remove(placeHolder)
            AllButtons.Insert(stoneindex, New Placeholder())

            'Gewonnen?
            Dim hasWin As Boolean = True
            For i As Integer = 0 To AllButtons.Count - 2
                If Not TypeOf AllButtons(i) Is PlayButton OrElse Not DirectCast(AllButtons(i), PlayButton).Number = i + 1 Then
                    hasWin = False
                    Exit For
                End If
            Next

            If hasWin Then
                Status.GameHasWin()
                If withSound Then _soundCollector.PlaySound(SoundType.GameWin)
                ServiceContainer.GetService(Of IMessageboxService).Show("Gratulation, du hast gewonnen!!!", "Gewonnen!", EnuMessageBoxButton.Ok, EnuMessageBoxImage.Information)
            End If
        End Sub


        Private _mixStonesCommand As ICommand
        Public Property MixStonesCommand() As ICommand
            Get
                If _mixStonesCommand Is Nothing Then _
                _mixStonesCommand = New RelayCommand(AddressOf MixStonesCommand_Execute)
                Return _mixStonesCommand
            End Get
            Set(ByVal value As ICommand)
                _mixStonesCommand = value
                RaisePropertyChanged()
            End Set
        End Property

        Private Sub MixStonesCommand_Execute(obj As Object)
            CreateField(GameSettings.SelectedFieldType)
            If GameSettings.PlaySounds Then _soundCollector.PlaySound(SoundType.MixStones)
            Dim r As New Random()

            For i As Integer = 0 To (GameSettings.SelectedLevel + 3) * (5 * (GameSettings.SelectedLevel + 1))
                Dim wasMoved As Boolean = False
                Dim lastMovedIndex As Integer = -1
                Do Until wasMoved
                    Dim currButtonIndex = r.Next((GameSettings.Columns * GameSettings.Rows) - 1)
                    Dim placeholderindex = AllButtons.IndexOf(AllButtons.Where(Function(x) x.StoneType = PlayStoneType.Placeholder).Single)
                    If currButtonIndex <> lastMovedIndex AndAlso IsStoneNeerPlaceholder(currButtonIndex, placeholderindex) Then
                        DoMoveButton(currButtonIndex, placeholderindex, False)
                        wasMoved = True : lastMovedIndex = currButtonIndex
                    End If
                Loop
            Next

            Status?.Dispose()
            Status = New GameStatus
            Status.RefreshStatus(AllButtons.ToList)
        End Sub

        Public Overrides Sub Dispose(disposing As Boolean)
            MyBase.Dispose(disposing)
            RemoveHandler GameSettings.SettingsChanged, AddressOf Settings_Changed
        End Sub
    End Class


End Namespace