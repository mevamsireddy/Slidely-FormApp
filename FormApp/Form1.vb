Public Class Form1
    Private WithEvents Timer1 As New Timer()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Form loaded event
        Me.KeyPreview = True ' To capture key events
        Timer1.Interval = 5000 ' 5 seconds
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        ' View Submissions button click event
        LoadFormInPanel(New FormViewSubmissions())
    End Sub

    Private Sub btnCreateNewSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateNewSubmission.Click
        ' Create New Submission button click event
        LoadFormInPanel(New FormCreateSubmission())
    End Sub

    Private Sub LoadFormInPanel(formToLoad As Form)
        ' Clear the panel
        SplitContainer1.Panel2.Controls.Clear()

        ' Set the form properties to make it a child of the panel
        formToLoad.TopLevel = False
        formToLoad.FormBorderStyle = FormBorderStyle.None
        formToLoad.Dock = DockStyle.Fill

        ' Add the form to the panel and show it
        SplitContainer1.Panel2.Controls.Add(formToLoad)
        formToLoad.Show()

        ' Set focus to the loaded form to handle its own key events
        formToLoad.Focus()
    End Sub

    ' Keyboard shortcuts for the buttons
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Handle Ctrl + V for View Submissions
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        End If

        ' Handle Ctrl + N for Create New Submission
        If e.Control AndAlso e.KeyCode = Keys.N Then
            Dim activeForm = SplitContainer1.Panel2.Controls.OfType(Of Form)().FirstOrDefault()
            If Not TypeOf activeForm Is FormViewSubmissions Then
                btnCreateNewSubmission.PerformClick()
            End If
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
    Private Sub SplitContainer1_Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles SplitContainer1.Panel2.MouseDown
        If e.Button = MouseButtons.Right Then
            ListBox1.Visible = False
        End If
        If e.Button = MouseButtons.Left Then
            ListBox1.Visible = False
        End If
    End Sub
End Class
