Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Namespace Helper

    Friend Module ImageHelper





        Public Function ImageCutter(fieldsize As Integer, imagePath As String) As List(Of Image)
            Return ImageCutter(fieldsize, Image.FromFile(imagePath))
        End Function


        Public Function ImageCutter(fieldsize As Integer, image As Image) As List(Of Image)
            Dim imgarray(fieldsize * fieldsize) As Image
            Dim imageSplitSize As Integer = CInt(image.Width / fieldsize)
            Dim img = image
            Dim i As Integer = 0
            Do While (i < fieldsize)
                Dim j As Integer = 0
                Do While (j < fieldsize)
                    Dim index = (i * fieldsize) + j
                    imgarray(index) = New Bitmap(imageSplitSize, imageSplitSize)
                    Dim graphics = System.Drawing.Graphics.FromImage(imgarray(index))
                    graphics.DrawImage(image:=img, destRect:=New Rectangle(0, 0, imageSplitSize, imageSplitSize),
                                       srcRect:=New Rectangle(j * imageSplitSize, i * imageSplitSize, imageSplitSize, imageSplitSize), srcUnit:=GraphicsUnit.Pixel)
                    graphics.Dispose()
                    j = j + 1
                Loop

                i = i + 1
            Loop
            Return New List(Of Image)(imgarray)
        End Function


        Public Function ConvertImageToBitMapImage(ByVal img As Image) As BitmapImage
            Dim memory = New MemoryStream
            img.Save(memory, ImageFormat.Png)
            memory.Position = 0
            Dim bitmapImage = New BitmapImage
            bitmapImage.BeginInit()
            bitmapImage.StreamSource = memory
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad
            bitmapImage.EndInit()
            Return bitmapImage
        End Function
    End Module
End Namespace