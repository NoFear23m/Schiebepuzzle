

Class MainWindow
    Inherits MahApps.Metro.Controls.MetroWindow

    Private Sub MainWindow_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        If e.Key = Key.Tab Then
            e.Handled = True
            Exit Sub
        End If
    End Sub
End Class



