Namespace ViewModel
    Public Class AboutVm
        Inherits ViewModelBase

        Public Sub New()
            If Not IsInDesignMode Then
                AllAssemblys = New List(Of AssemblyListItem)
                AppDomain.CurrentDomain.GetAssemblies().Where(Function(a) Not a.IsDynamic).ToList.ForEach(Sub(x) AllAssemblys.Add(New AssemblyListItem(x.Location)))
            End If
        End Sub


        Private _allAssemblys As List(Of AssemblyListItem)
        Public Property AllAssemblys As List(Of AssemblyListItem)
            Get
                Return _allAssemblys
            End Get
            Set(ByVal Value As List(Of AssemblyListItem))
                _allAssemblys = Value
                RaisePropertyChanged()
            End Set
        End Property

        Public ReadOnly Property AppName As String
            Get
                Return My.Application.Info.ProductName
            End Get
        End Property

        Public ReadOnly Property AppDescription As String
            Get
                Return My.Application.Info.Description
            End Get
        End Property

        Public ReadOnly Property Version As String
            Get
                Return My.Application.Info.Version.ToString
            End Get
        End Property

        Public ReadOnly Property CurrentPath As String
            Get
                Return My.Application.Info.DirectoryPath
            End Get
        End Property

        Public ReadOnly Property Copyright As String
            Get
                Return $"{ My.Application.Info.Copyright} {My.Application.Info.CompanyName} under MIT License"
            End Get
        End Property
    End Class

    Public Class AssemblyListItem
        Public Sub New()
        End Sub

        Public Sub New(location As String)
            Dim fvi = FileVersionInfo.GetVersionInfo(location)
            Name = fvi.InternalName
            Description = fvi.FileDescription
            Version = fvi.ProductVersion
        End Sub
        Public Property Name As String
        Public Property Description As String
        Public Property Version As String

    End Class

End Namespace
