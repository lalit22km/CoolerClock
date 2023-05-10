Imports System.Net
Imports Microsoft.Win32
Imports System.Data
Imports Newtonsoft.Json
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json.Linq
Imports Newtonsoft

Public Class Form1
    Dim regKey As RegistryKey
    Dim timeZone As String = My.Computer.Registry.GetValue _
        ("HKEY_CURRENT_USER\CoolerClock", "timezone", Nothing)

    Private Function Setkeyval(ByVal keyname As String, ByVal emoji As String) As Int32
        regKey = My.Computer.Registry.CurrentUser.OpenSubKey("Control Panel\International", True)
        regKey.SetValue(keyname, emoji)
        regKey.Close()
        Return 0
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim checkFirstRun As String = My.Computer.Registry.GetValue _
        ("HKEY_CURRENT_USER\CoolerClock", "FirstRun", Nothing)
        If checkFirstRun = "1" Then
            welcome.Close()
            Me.Enabled = True
            timeTimer.Enabled = True
            weatherTimer.Enabled = False 'NOT YET IMPLIMENTED

        Else
            Me.Enabled = False
            welcome.Show()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim json As String = New System.Net.WebClient().DownloadString("http://api.openweathermap.org/geo/1.0/zip?zip=244001,IN&appid=26200d22d5c2805032da5cd619a095f6")
        Dim parsejson As JObject = JObject.Parse(json)
        Dim lat = parsejson.SelectToken("lat").ToString()
        Dim lon = parsejson.SelectToken("lon").ToString()
        TextBox1.Text = lat + ", " + lon
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles timeTimer.Tick
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim timeapi As String = New System.Net.WebClient().DownloadString("https://timeapi.io/api/Time/current/zone?timeZone=" + timeZone)
        Dim parsejson As JObject = JObject.Parse(timeapi)
        Dim hour = parsejson.SelectToken("hour").ToString()
        Dim min = parsejson.SelectToken("minute").ToString()
        If hour + min > 0 Then
            If hour + min < 259 Then
                Setkeyval("s1159", "🌑")
            End If
        ElseIf hour + min > 300 Then
            If hour + min < 559 Then
                Setkeyval("s1159", "🌒")
            End If
        ElseIf hour + min > 600 Then
            If hour + min < 859 Then
                Setkeyval("s1159", "🌓")
            End If
        ElseIf hour + min > 900 Then
            If hour + min < 1759 Then
                Setkeyval("s1159", "☀")
                Setkeyval("s2359", "☀")
            End If
        ElseIf hour + min > 1800 Then
            If hour + min < 2059 Then
                Setkeyval("s2359", "🌅")
            End If
        ElseIf hour + min > 2100 Then
            If hour + min < 2359 Then
                Setkeyval("s2359", "🌔")
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles settz.Click
    End Sub
End Class
