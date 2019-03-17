Option Strict On

Imports System.IO

Namespace Helper.Serializer


    ''' <summary>
    ''' Der XMLSerializer serialisiert eine beliebige Klasse welche auch Primitiven Datentypen oder solchen welche als Serialisiert gekennzeichnet sind
    ''' Klasse ist generisch aufgebaut und gibt die selbe Type von Klasse zurück welche ihm übergeben wird.
    ''' </summary>
    Public Class XmlSerializer
        Implements ISerializer



#Region "ISerializer-Implementation"
        ''' <summary>
        ''' Serialisiert eine Klasse in ein XML
        ''' </summary>
        ''' <typeparam name="T">Den Klassentyp angeben</typeparam>
        ''' <param name="path">Der Dateipfad inkl. Dateiendung angeben</param>
        ''' <param name="instance">Die Instanz der Klasse welche serialisiert werden soll</param>
        Public Sub Serialize(Of T)(path As String, instance As T) Implements ISerializer.Serialize
            Try
                Dim dirName As String = IO.Path.GetDirectoryName(path)
                If Not Directory.Exists(dirName) Then
                    Directory.CreateDirectory(dirName)
                End If

                File.Delete(path)

                Using fs As New FileStream(path, FileMode.OpenOrCreate)

                    SaveToStream(fs, instance)
                End Using

            Catch ex As Exception
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        End Sub

        ''' <summary>
        ''' Serialisiert eine Klasse über einen Stream
        ''' </summary>
        ''' <typeparam name="T">Den Klassentyp angeben</typeparam>
        ''' <param name="s">Der Stream welcher die Daten enthält (Filestream, MemoryStream,...)</param>
        ''' <param name="o">Die Instanz der Klasse welche serialisiert werden soll</param>
        Public Sub SaveToStream(Of T)(s As Stream, o As T)
            Try
                Dim x As New Xml.Serialization.XmlSerializer(GetType(T))
                x.Serialize(s, o)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        End Sub

        ''' <summary>
        ''' Deserialisiert eine XML in eine Klasseninstanz - Die Instanz muss also nicht erstellt sein.
        ''' </summary>
        ''' <typeparam name="T">Der Typ der Klasse welche erwartet wird.</typeparam>
        ''' <param name="path">Der Pfad zur XML-Datei inkl. Dateiendung</param>
        ''' <param name="defaultInstance">Die Instanz der Klasse falls die Datei noch nicht Existiert oder nicht gefunden werden kann</param>
        ''' <returns>Gibt die Deserialissierte Klasse zurück</returns>
        Public Function DeSerialize(Of T)(path As String, defaultInstance As T) As T Implements ISerializer.DeSerialize
            Try
                If Not File.Exists(path) Then
                    Return defaultInstance
                End If

                Using fs As New FileStream(path, FileMode.OpenOrCreate)
                    Return LoadFromStream(Of T)(fs)
                End Using
            Catch ex As Exception
                Throw New Exception(ex.Message, ex.InnerException)
            End Try
        End Function

        ''' <summary>
        ''' Läd ein Klasse generisch über einen Stream, Methode ist gut geeignet für UnitTests
        ''' </summary>
        ''' <typeparam name="T">Der Typ der Klasse welche erwartet wird</typeparam>
        ''' <param name="s">Der Stream welcher die Daten enthält (Filestream, MemoryStream,...)</param>
        ''' <returns>Gibt die Deserialisierte Klasse zurück</returns>
        Public Function LoadFromStream(Of T)(s As Stream) As T
            Dim x As New Xml.Serialization.XmlSerializer(GetType(T))
            Dim res = CType(x.Deserialize(s), T)
            Return CType(res, T)
        End Function
#End Region

    End Class
End Namespace