
Namespace Helper.Serializer
    ''' <summary>
    ''' Interface für Serializer
    ''' </summary>
    Public Interface ISerializer

        Sub Serialize(Of T)(path As String, instance As T)

        Function DeSerialize(Of T)(path As String, defaultInstance As T) As T

    End Interface
End Namespace