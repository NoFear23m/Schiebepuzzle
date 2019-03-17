Imports System.Media

Namespace Model
    Public Class Sound
        Private _soundFile As String

        Private Sub New()
        End Sub

        Public Sub New(soundfilepath As String, sType As SoundType)
            SoundFile = soundfilepath
            SoundType = sType
        End Sub

        Public Property SoundFile As String
            Get
                Return _soundFile
            End Get
            Set
                _soundFile = Value
                Player = New SoundPlayer(Value)
            End Set
        End Property

        Public Property Player As SoundPlayer

        Public Property SoundType As SoundType

    End Class
    Public Enum SoundType
        StoneMove = 0
        GameWin = 1
        MixStones = 2
    End Enum
End Namespace