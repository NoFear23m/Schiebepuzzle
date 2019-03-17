

Namespace ViewModel
    Public Class MainWorkspace
        Inherits ViewModelBase

        Public Sub New()
            GamePlaySettings = New GamePlaySettingsVm
            GameZone = New GamePlay(GamePlaySettings)
            Footer = New FooterVm
        End Sub


        Private _gamePlaySettings As GamePlaySettingsVm

        Public Property GamePlaySettings() As GamePlaySettingsVm
            Get
                Return _gamePlaySettings
            End Get
            Set(ByVal value As GamePlaySettingsVm)
                _gamePlaySettings = value
                RaisePropertyChanged()
            End Set
        End Property


        Private _gameZone As GamePlay

        Public Property GameZone() As GamePlay
            Get
                Return _gameZone
            End Get
            Set(ByVal value As GamePlay)
                _gameZone = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _footer As FooterVm

        Public Property Footer() As FooterVm
            Get
                Return _footer
            End Get
            Set(ByVal value As FooterVm)
                _footer = value
                RaisePropertyChanged()
            End Set
        End Property

    End Class
End Namespace