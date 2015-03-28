Imports System.Net
Imports System.Threading.Tasks
Imports System.Data
Imports System.Collections.Specialized
Imports System.Text
Imports System.IO

Public Class Yo
    '---------------------------------------------------------------
    'Made by Flaatt'
    '---------------------------------------------------------------
    Dim token As String = "Paste your token here" '<-your token here
    '---------------------------------------------------------------

    Dim url As String = "http://api.justyo.co/yoall/" 'for yo all
    Dim url2 As String = "http://api.justyo.co/yo/" ' for yo user
    Dim Data = New NameValueCollection
    Dim wb = New WebClient


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'set token
        Data("api_token") = token

        'send to yo
        Dim response = wb.UploadValues(url, "POST", Data)

        'get respnse
        Dim svar = String.Format("{0}", Encoding.ASCII.GetString(response))

        'clean string
        Dim svarClean = svar.Remove(0, 15)
        svarClean = svarClean.Remove(4)

        'print respnse
        If svarClean = "true" Then
            Button1.Text = "Yo sent"
            Timer1.Start()
        End If


    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        ' set button to default
        Button1.Text = "Yo all"
        TextBox1.Text = "TYPE USERNAME"
        Button2.Text = "Yo user"

        Timer1.Stop()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.Click
        'remove text in textbox
        If TextBox1.Text = "TYPE USERNAME" Then
            TextBox1.Clear()
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'set token end username
        Data("api_token") = token
        Data("username") = TextBox1.Text.Trim

        'send to yo
        Dim response = wb.UploadValues(url2, "POST", Data)

        'get respnse
        Dim svar = String.Format("{0}", Encoding.ASCII.GetString(response))

        'clean string
        Dim answerClean = svar.Remove(0, 15)
        answerClean = answerClean.Remove(4)

        'print respnse
        If answerClean = "true" Then
            Button2.Text = "Yo sent"
            Timer1.Start()
        End If

    End Sub

    Private Sub followers()

        'get followers
        Dim getSource As String = New System.Net.WebClient().DownloadString("http://api.justyo.co/subscribers_count/?api_token=" + token)

        'clean string
        Dim answerClean = getSource.Replace("{", String.Empty)
        answerClean = answerClean.Replace("}", String.Empty)
        answerClean = answerClean.Replace("""count"":", String.Empty)

        TextBox2.Text = answerClean
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        followers()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        followers()
    End Sub
End Class
