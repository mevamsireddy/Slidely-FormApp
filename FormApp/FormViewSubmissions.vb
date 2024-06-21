Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Public Class FormViewSubmissions
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0
    Private httpClient As HttpClient = New HttpClient()
    Private WithEvents Timer1 As New Timer()

    Private Async Sub FormViewSubmissions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True ' To capture key events
        Timer1.Interval = 5000 ' 5 seconds
        Await LoadSubmissions()
        DisplaySubmission(currentIndex)
    End Sub

    Private Async Function LoadSubmissions() As Task
        Try
            Dim response As HttpResponseMessage = Await httpClient.GetAsync("http://localhost:3000/api/read")
            response.EnsureSuccessStatusCode()
            Dim content As String = Await response.Content.ReadAsStringAsync()
            submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(content)
        Catch ex As HttpRequestException
            MessageBox.Show($"Network error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub DisplaySubmission(index As Integer)
        If submissions IsNot Nothing AndAlso index >= 0 AndAlso index < submissions.Count Then
            Dim currentSubmission As Submission = submissions(index)
            txtName.Text = currentSubmission.Name
            txtEmail.Text = currentSubmission.Email
            txtPhone.Text = currentSubmission.Phone
            txtGitHubLink.Text = currentSubmission.github_link
            txtStopwatchTime.Text = currentSubmission.stopwatch_time
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    ' Keyboard shortcuts for the buttons
    Private Sub FormViewSubmissions_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Handle Ctrl + P for Previous
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        End If

        ' Handle Ctrl + N for Next
        If e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        End If

        ' Handle Ctrl + H to show ListBox1
        If e.Control AndAlso e.KeyCode = Keys.H Then
            ListBox1.Visible = True
            Timer1.Start()
        End If
    End Sub

    ' Timer tick event to hide ListBox1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ListBox1.Visible = False
        Timer1.Stop()
    End Sub

    ' Right-click event to hide ListBox1
    Private Sub FormViewSubmissions_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Right Then
            ListBox1.Visible = False
        End If
        If e.Button = MouseButtons.Left Then
            ListBox1.Visible = False
        End If
    End Sub

    ' Event handlers for text box changes, if needed
    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
    End Sub

    Private Sub txtGitHubLink_TextChanged(sender As Object, e As EventArgs) Handles txtGitHubLink.TextChanged
    End Sub

    Private Sub txtPhone_TextChanged(sender As Object, e As EventArgs) Handles txtPhone.TextChanged
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
    End Sub

    Private Sub txtStopwatchTime_TextChanged(sender As Object, e As EventArgs) Handles txtStopwatchTime.TextChanged
    End Sub
End Class
