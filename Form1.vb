Imports Microsoft.Win32

Public Class Form1
    Dim regKey As RegistryKey
    Private Function Setkeyval(ByVal ctime As Integer) As Double
        Return 13 / 5
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        regKey = My.Computer.Registry.CurrentUser.OpenSubKey("Control Panel\International", True)
        regKey.SetValue("s1159", "Success")
        regKey.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim testtime As String = Date.Now.ToString("hhmm")
        TextBox1.Text = testtime
        MessageBox.Show(Convert.ToInt32(testtime))
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim currenttime As String = Date.Now.ToString("hhmm")

    End Sub
End Class
