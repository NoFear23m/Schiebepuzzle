Imports Schiebepuzzle.ViewModel.Infrastructure

Namespace ViewModel
    Public Class GamePlaySettingsVm
        Inherits ViewModelBase

        Public Event SettingsChanged()


        Public Sub New()
            If Not IsInDesignMode Then
                AviableImages = New List(Of String)
                Dim supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff,*.g01,*.g02,*.g03,*.g04,*.g05,*.g06,*.g07,*.g08"
                For Each f In IO.Directory.GetFiles($"{Environment.CurrentDirectory}\images\", "*.*").Where(Function(x) supportedExtensions.Contains(IO.Path.GetExtension(x).ToLower()))
                    AviableImages.Add(IO.Path.GetFileName(f))
                Next
                Dim setting = SettingsHelper.GetSettings

                SelectedImage = AviableImages.Where(Function(x) x = setting.LastSelectedImage).FirstOrDefault
                If Not IO.File.Exists(Environment.CurrentDirectory & "\images\" & SelectedImage) Then
                    SelectedImage = AviableImages.FirstOrDefault
                End If
                FieldSize = setting.FieldSize
                SelectedImage = setting.LastSelectedImage
                SelectedLevel = setting.Level
                SelectedFieldType = CType(setting.GameType, PlayFieldType)

            End If
        End Sub



        Public ReadOnly Property Rows As Integer
            Get
                Return FieldSize
            End Get
        End Property


        Public ReadOnly Property Columns As Integer
            Get
                Return FieldSize
            End Get
        End Property


        Private _fieldSize As Integer = 4
        Public Property FieldSize() As Integer
            Get
                Return _fieldSize
            End Get
            Set(ByVal value As Integer)
                _fieldSize = value
                RaisePropertyChanged(NameOf(Columns))
                RaisePropertyChanged(NameOf(FieldSize))
                RaisePropertyChanged(NameOf(Rows))
                Dim sett = SettingsHelper.GetSettings
                sett.FieldSize = value
                SettingsHelper.SaveSettings(sett)
                RaiseEvent SettingsChanged()
            End Set
        End Property


        Private _selectedFieldType As PlayFieldType = PlayFieldType.NumberedPlayField

        Public Property SelectedFieldType() As PlayFieldType
            Get
                Return _selectedFieldType
            End Get
            Set(ByVal value As PlayFieldType)
                _selectedFieldType = value
                RaisePropertyChanged()
                RaisePropertyChanged(NameOf(SelectedFieldIndex))
                Dim sett = SettingsHelper.GetSettings
                sett.GameType = value
                SettingsHelper.SaveSettings(sett)
                RaiseEvent SettingsChanged()
            End Set
        End Property

        Public Property SelectedFieldIndex As Integer
            Get
                Return CInt(SelectedFieldType)
            End Get
            Set(value As Integer)
                SelectedFieldType = CType(value, PlayFieldType)
            End Set
        End Property



        Private _selectedLevel As Integer = 0
        Public Property SelectedLevel() As Integer
            Get
                Return _selectedLevel
            End Get
            Set(ByVal value As Integer)
                _selectedLevel = value
                RaisePropertyChanged()
                Dim sett = SettingsHelper.GetSettings
                sett.Level = value
                SettingsHelper.SaveSettings(sett)
                RaiseEvent SettingsChanged()
            End Set
        End Property


        Private _aviableImages As List(Of String)

        Public Property AviableImages() As List(Of String)
            Get
                Return _aviableImages
            End Get
            Set(ByVal value As List(Of String))
                _aviableImages = value
                RaisePropertyChanged()
                RaiseEvent SettingsChanged()
            End Set
        End Property

        Public ReadOnly Property SelectedImageFullPath As String
            Get
                Return Environment.CurrentDirectory & "\images\" & SelectedImage
            End Get
        End Property

        Private _selectedImage As String

        Public Property SelectedImage() As String
            Get
                Return _selectedImage
            End Get
            Set(ByVal value As String)
                _selectedImage = value
                RaisePropertyChanged()
                Dim sett = SettingsHelper.GetSettings
                sett.LastSelectedImage = value
                SettingsHelper.SaveSettings(sett)
                RaiseEvent SettingsChanged()
                RaisePropertyChanged(SelectedImageFullPath)
            End Set
        End Property

        Private _playSounds As Boolean
        Public Property PlaySounds As Boolean
            Get
                Return _playSounds
            End Get
            Set(value As Boolean)
                _playSounds = value
                Dim sett = SettingsHelper.GetSettings
                sett.PlaySounds = value
                SettingsHelper.SaveSettings(sett)
                RaisePropertyChanged(SelectedImageFullPath)
            End Set
        End Property

        Public Enum PlayFieldType
            ImagedPlayField = 0
            NumberedPlayField = 1
        End Enum


        Private _showInfoDialogCommand As ICommand
        Public ReadOnly Property ShowInfoDialogCommand As ICommand
            Get
                If _showInfoDialogCommand Is Nothing Then _
                    _showInfoDialogCommand = New RelayCommand(AddressOf ShowInfoDialogCommandExecute)
                Return _showInfoDialogCommand
            End Get
        End Property
        Private Sub ShowInfoDialogCommandExecute(ByVal obj As Object)
            ServiceContainer.GetService(Of IDialogWindowService).ShowModalDialog("dialogBox", New AboutVm(), Me, True, False)
        End Sub

    End Class
End Namespace