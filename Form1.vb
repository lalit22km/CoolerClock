﻿Imports System.Net
Imports Microsoft.Win32
Imports System.Data
Imports Newtonsoft.Json
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json.Linq

Public Class Form1
    Dim AMorPM As Int32 '0 is AM 1 is PM
    Dim regKey As RegistryKey
    Private Function Setkeyval(ByVal keyname As String, ByVal emoji As String) As Int32
        regKey = My.Computer.Registry.CurrentUser.OpenSubKey("Control Panel\International", True)
        regKey.SetValue(keyname, emoji)
        regKey.Close()
        Return 0
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        welcome.Show()
        Dim readValue1 As String = My.Computer.Registry.GetValue _
        ("HKEY_CURRENT_USER\CoolerClock", "timezone", Nothing)
        If readValue1 = "" Then
            MsgBox("Please set Timezone.")
        Else
            ComboBox1.SelectedItem = readValue1
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim timeapi As String = New System.Net.WebClient().DownloadString("https://timeapi.io/api/Time/current/zone?timeZone=" + ComboBox1.Text)
            Dim parsejson As JObject = JObject.Parse(timeapi)
            Dim thehour = parsejson.SelectToken("hour").ToString()
            Dim themin = parsejson.SelectToken("minute").ToString()
            Dim time As Int32 = thehour + themin
            If time > 12 Then
                AMorPM = 1
            Else
                AMorPM = 0
            End If
            Timer1.Enabled = True
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim currenttime As String = Date.Now.ToString("hhmm")

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles settz.Click
        My.Computer.Registry.CurrentUser.CreateSubKey("CoolerClock")
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "timezone", ComboBox1.Text)
        Dim readValue As String
        readValue = My.Computer.Registry.GetValue _
        ("HKEY_CURRENT_USER\CoolerClock", "timezone", Nothing)
        MsgBox("The TZ has been set as " & readValue)
    End Sub
End Class
