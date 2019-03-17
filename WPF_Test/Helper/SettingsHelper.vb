Module SettingsHelper

    Private Const SettingFilename As String = "settings.xml"
    Private ReadOnly SettingsPath As String = $"{Environment.CurrentDirectory}\{SettingFilename}"

    Public Function GetSettings() As Model.Settings
        Return New Helper.Serializer.XmlSerializer().DeSerialize(SettingsPath, New Model.Settings)
    End Function

    Public Sub SaveSettings(settingInstance As Model.Settings)
        Dim ser = New Helper.Serializer.XmlSerializer()
        ser.Serialize(SettingsPath, settingInstance)
    End Sub

End Module
