Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.Win32
Imports Newtonsoft
Imports Newtonsoft.Json.Linq

Public Class welcome
    Dim regKey As RegistryKey
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Process.Start("https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2#Officially_assigned_code_elements")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If timezone.Text IsNot "" Then
            If api.Text IsNot "" Then
                If zip.Text IsNot "" Then
                    If country.Text IsNot "" Then
                        'MAIN CHECKS START HERE----
                        Try
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                            Dim timeapi As String = New System.Net.WebClient().DownloadString("https://timeapi.io/api/Time/current/zone?timeZone=" + timezone.Text)
                            My.Computer.Registry.CurrentUser.CreateSubKey("CoolerClock")
                            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "timezone", timezone.Text)
                            Try
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                                Dim owmapi As String = New System.Net.WebClient().DownloadString("http://api.openweathermap.org/geo/1.0/zip?zip=" + zip.Text + "," + country.Text + "&appid=" + api.Text)
                                Dim parsejson As JObject = JObject.Parse(owmapi)
                                Dim lat = parsejson.SelectToken("lat").ToString()
                                Dim lon = parsejson.SelectToken("lon").ToString()
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "latitude", lat)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "longitude", lon)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "zip", zip.Text)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "country", country.Text)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "api", api.Text)
                                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\CoolerClock", "FirstRun", "1")
                                MsgBox("Info saved.")
                                Me.Close()
                            Catch
                                MsgBox("Uh-oh ! Your API Key or Zip Code or the country code is wrong. If you just generated your API key, please wait around 15 minutes for it to start working.", MsgBoxStyle.Information)
                            End Try
                        Catch
                            MsgBox("Unable to connect to TimeAPI. Are you connected to the Internet ?", MsgBoxStyle.Exclamation)
                        End Try
                        'MAIN CHECKS END HERE----
                    Else
                        MsgBox("Please enter a Country Code.", MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox("Please enter a Zip Code.", MsgBoxStyle.Information)
                End If
            Else
                MsgBox("Please enter the API key.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Please select a timezone.", MsgBoxStyle.Information)
        End If
    End Sub
End Class