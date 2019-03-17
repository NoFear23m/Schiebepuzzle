Imports System.Media

Namespace Model
    Public Class SoundCollector

        Private ReadOnly _soundsList As List(Of Sound)

        Public Sub New()
            _soundsList = New List(Of Sound)
            Dim ser As New Helper.Serializer.XmlSerializer()
            Dim sett As Settings = ser.DeSerialize($"{Environment.CurrentDirectory}\settings.xml", New Settings)
            SetSoundfile(sett.GameWinRelativeSoundFilePath, SoundType.GameWin)
            SetSoundfile(sett.MixStonesRelativeSoundFilePath, SoundType.MixStones)
            SetSoundfile(sett.MoveStoneRelativeSoundFilePath, SoundType.StoneMove)
        End Sub

        Public Sub SetSoundfile(path As String, sound As SoundType)
            Dim existingSound = _soundsList.Where(Function(x) x.SoundType = sound).SingleOrDefault
            If existingSound IsNot Nothing Then _soundsList.Remove(existingSound)
            _soundsList.Add(New Sound($"{Environment.CurrentDirectory}{path}", sound))
        End Sub

        Public Function GetSoundInfo(sound As SoundType) As Sound
            Dim existingSound = _soundsList.Where(Function(x) x.SoundType = sound).SingleOrDefault
            Return existingSound
        End Function


        Public Sub PlaySound(sound As SoundType)
            Dim existingSound = _soundsList.Where(Function(x) x.SoundType = sound).SingleOrDefault
            If existingSound IsNot Nothing Then existingSound.Player.Play()
        End Sub

    End Class
End Namespace