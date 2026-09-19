Public Class ProductGroup
    Public Property PGId As Integer
    Public Property CodePG As String
    Public Property Description As String
    ''' <summary>Un Product Group appartient toujours à un seul Product Manager.</summary>
    Public Property PMId As Integer
End Class
