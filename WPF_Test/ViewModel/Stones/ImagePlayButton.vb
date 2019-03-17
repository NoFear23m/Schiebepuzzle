Namespace Model.Stones
    Public Class ImagePlayButton
        Inherits PlayButton

        Public Sub New()

        End Sub

        Public Sub New(image As BitmapImage)
            Me.Image = image
        End Sub


        Private _image As BitmapImage

        Public Property Image() As BitmapImage
            Get
                Return _image
            End Get
            Set(ByVal value As BitmapImage)
                _image = value
                RaisePropertyChanged()
            End Set
        End Property

    End Class
End Namespace