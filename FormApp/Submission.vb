' Define the Submission class
Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property github_link As String
    Public Property stopwatch_time As String

    Public Sub New(name As String, email As String, phone As String, github_link As String, stopwatch_time As String)
        Me.Name = name
        Me.Email = email
        Me.Phone = phone
        Me.github_link = github_link
        Me.stopwatch_time = stopwatch_time
    End Sub
End Class