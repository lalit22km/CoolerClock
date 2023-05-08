Imports System.Net
Imports Microsoft.Win32
Imports System.Data
Imports Newtonsoft.Json
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json.Linq

Public Class Form1
    Dim regKey As RegistryKey
    Private Function Setkeyval(ByVal keyname As String, ByVal emoji As String) As Int32
        regKey = My.Computer.Registry.CurrentUser.OpenSubKey("Control Panel\International", True)
        regKey.SetValue(keyname, emoji)
        regKey.Close()
        Return 0
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Computer.Registry.CurrentUser.CreateSubKey("CoolerClock")
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "timezone", ComboBox1.Text)
        Dim readValue As String
        readValue = My.Computer.Registry.GetValue _
        ("HKEY_CURRENT_USER\CoolerClock", "timezone", Nothing)
        MsgBox("The value is " & readValue)
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
        TextBox1.Text = thehour + themin
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim currenttime As String = Date.Now.ToString("hhmm")

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ComboBox1.SelectedItem = "Africa/Cairo"
        TextBox1.Text = ComboBox1.Text
    End Sub
End Class
