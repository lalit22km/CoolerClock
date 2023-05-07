Imports System.Net
Imports Microsoft.Win32
Imports System.Data
Imports Newtonsoft.Json
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json.Linq

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
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
        Dim json As String = (New WebClient).DownloadString("https://timeapi.io/api/Time/current/zone?timeZone=Asia/Kolkata")
        RichTextBox1.Text = json
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim json As String = New System.Net.WebClient().DownloadString("https://timeapi.io/api/Time/current/zone?timeZone=Asia/Kolkata")
        Dim parsejson As JObject = JObject.Parse(json)
        Dim thehour = parsejson.SelectToken("hour").ToString()
        Dim themin = parsejson.SelectToken("minute").ToString()
        TextBox1.Text = ":" + thehour + themin
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim currenttime As String = Date.Now.ToString("hhmm")

    End Sub


End Class
