Public Class uclGameField



    Public Property FieldSize As Integer
        Get
            Return CType(GetValue(FieldSizeProperty), Integer)
        End Get

        Set(ByVal value As Integer)
            SetValue(FieldSizeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FieldSizeProperty As DependencyProperty =
                           DependencyProperty.Register("FieldSize",
                           GetType(Integer), GetType(uclGameField),
                           New PropertyMetadata(4))




End Class
