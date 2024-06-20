Imports System.Net.Http
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Public Class FormCreateSubmission
    Private stopwatch As Stopwatch
    Private httpClient As HttpClient = New HttpClient()

    Private Sub FormCreateSubmission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        stopwatch = New Stopwatch()
        Me.KeyPreview = True ' To capture key events
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If Not stopwatch.IsRunning Then
            stopwatch.Start()
            btnToggleStopwatch.Text = "Pause Stopwatch"
            Timer1.Enabled = True
        Else
            stopwatch.Stop()
            btnToggleStopwatch.Text = "Resume Stopwatch"
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Validate form fields
        If Not ValidateForm() Then
            Return
        End If

        Dim submission = New With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhone.Text,
            .github_link = txtGitHubLink.Text,
            .stopwatch_time = lblStopwatchTime.Text
        }

        ' Serialize the submission object to JSON
        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Try
            Dim response As HttpResponseMessage = Await httpClient.PostAsync("http://localhost:3000/api/submit", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ResetForm()
            Else
                Dim errorContent As String = Await response.Content.ReadAsStringAsync()
                MessageBox.Show($"Submission failed: {response.StatusCode} - {errorContent}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As HttpRequestException
            MessageBox.Show($"Network error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateForm() As Boolean
        If String.IsNullOrWhiteSpace(txtName.Text) OrElse String.IsNullOrWhiteSpace(txtEmail.Text) OrElse String.IsNullOrWhiteSpace(txtPhone.Text) OrElse String.IsNullOrWhiteSpace(txtGitHubLink.Text) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub ResetForm()
        txtName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtGitHubLink.Clear()
        lblStopwatchTime.Text = "00:00:00"
        stopwatch.Reset()
        btnToggleStopwatch.Text = "Start Stopwatch"
    End Sub

    ' Keyboard shortcuts for the buttons
    Private Sub FormCreateSubmission_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Handle Ctrl + T for Toggle Stopwatch
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        End If

        ' Handle Ctrl + S for Submit
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

    ' Event handlers for text box changes, if needed
    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
    End Sub

    Private Sub lblStopwatchTime_Click(sender As Object, e As EventArgs) Handles lblStopwatchTime.Click
    End Sub

    Private Sub txtGitHubLink_TextChanged(sender As Object, e As EventArgs) Handles txtGitHubLink.TextChanged
    End Sub

    Private Sub txtPhone_TextChanged(sender As Object, e As EventArgs) Handles txtPhone.TextChanged
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
    End Sub
End Class
