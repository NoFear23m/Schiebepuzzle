
Imports MahApps.Metro.Controls

Public Class SpsWindow
    Inherits MetroWindow


    Public MContext As Object

    Public Property CanCloseWithEsc As Boolean
        Get
            Return CBool(GetValue(CanCloseWithEscProperty))
        End Get

        Set
            SetValue(CanCloseWithEscProperty, Value)
        End Set
    End Property


    Public Shared ReadOnly CanCloseWithEscProperty As DependencyProperty =
                           DependencyProperty.Register("CanCloseWithEsc",
                           GetType(Boolean), GetType(SpsWindow),
                           New PropertyMetadata(True))



    Public Property AsSpsModalDialog As Boolean
        Get
            Return CBool(GetValue(AsSpsModalDialogProperty))
        End Get

        Set
            SetValue(AsSpsModalDialogProperty, Value)
        End Set
    End Property

    Public Shared ReadOnly AsSpsModalDialogProperty As DependencyProperty =
                           DependencyProperty.Register("AsSpsModalDialog",
                           GetType(Boolean), GetType(SpsWindow),
                           New PropertyMetadata(True))


    Public Sub New(name As String)
        Me.Name = name
        InitializeComponent()
    End Sub

    Public Sub New(name As String, datacontext As Object, Optional sizeToContent As SizeToContent = SizeToContent.WidthAndHeight,
                   Optional startPosition As WindowStartupLocation = WindowStartupLocation.CenterScreen)
        Me.Name = name
        MContext = datacontext

        Me.SizeToContent = sizeToContent
        WindowStartupLocation = startPosition
        InitializeComponent()
    End Sub

    Private Sub SPSWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AddHandler PreviewKeyDown, AddressOf Window_KeyDown

        If AsSpsModalDialog Then
            If Owner IsNot Nothing Then
                ShowInTaskbar = False
            End If
        End If

        DataContext = MContext
        BringIntoView()
    End Sub

    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Escape AndAlso CanCloseWithEsc Then
            Close()
        End If
    End Sub


    Public Function FindOwnerWindow(viewModel As Object) As Window
        ' if the ViewModel of the caller is set, we try to find the corresponding Window
        If viewModel IsNot Nothing Then
            If viewModel.GetType Is GetType(SpsWindow) Then Return CType(viewModel, Window)

            If Application.Current.Windows.Cast(Of Window)().SingleOrDefault(Function(x) x.DataContext IsNot Nothing AndAlso x.DataContext.GetType Is viewModel.GetType) Is Nothing Then
                For Each window As Window In (From win In Application.Current.Windows()).ToList
                    If window.DataContext IsNot Nothing AndAlso window.DataContext.GetType Is viewModel.GetType Then
                        Return window
                    End If
                Next
            Else
                Return Application.Current.Windows.Cast(Of Window)().SingleOrDefault(Function(x) x.DataContext IsNot Nothing AndAlso x.DataContext.GetType Is viewModel.GetType)
            End If
        End If
        ' by default we use the active Window in the List as owner
        Return Application.Current.Windows.Cast(Of Window)().SingleOrDefault(Function(x) x.IsActive)
    End Function


    Public Sub SetwinState(maximize As Boolean)
        If maximize Then
            WindowState = WindowState.Maximized
        Else
            WindowState = WindowState.Normal
        End If
    End Sub

End Class
