
Namespace Model.Stones
    Public MustInherit Class PlayStoneBase

        Public Property StoneType As PlayStoneType = PlayStoneType.NormalButton

    End Class
    Public Enum PlayStoneType
        NormalButton = 0
        Placeholder = 1
    End Enum
End Namespace