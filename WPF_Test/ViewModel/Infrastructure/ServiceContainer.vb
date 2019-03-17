Namespace ViewModel.Infrastructure
    Public Class ServiceContainer
        Public Shared ReadOnly Instance As New ServiceContainer()

        Shared Sub New()
            ServiceMap = New Dictionary(Of Type, Object)()
            ServiceMapLock = New Object()
        End Sub

        Public Sub AddService(Of TServiceContract As Class)(implementation As TServiceContract)
            SyncLock ServiceMapLock
                ServiceMap(GetType(TServiceContract)) = implementation
            End SyncLock
        End Sub

        Public Shared Function GetService(Of TServiceContract As Class)() As TServiceContract
            Dim service As Object = Nothing
            SyncLock ServiceMapLock
                ServiceMap.TryGetValue(GetType(TServiceContract), service)
            End SyncLock
            Return TryCast(service, TServiceContract)
        End Function

        Shared ReadOnly ServiceMap As Dictionary(Of Type, Object)
        Shared ReadOnly ServiceMapLock As Object
    End Class
End Namespace