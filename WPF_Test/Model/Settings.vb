Namespace Model

    <Serializable>
    Public Class Settings

        Public Property FieldSize As Integer = 4
        Public Property Level As Integer = 1
        Public Property GameType As Integer = 0

        Public Property GameWinRelativeSoundFilePath As String = "\sounds\FanFare.wav"
        Public Property MixStonesRelativeSoundFilePath As String = "\sounds\Reverse gleam high frequency.wav"
        Public Property MoveStoneRelativeSoundFilePath As String = "\sounds\Fast zing.wav"
        Public Property PlaySounds As Boolean = True
        Public Property LastSelectedImage As String = "coffee.jpg"

    End Class
End Namespace