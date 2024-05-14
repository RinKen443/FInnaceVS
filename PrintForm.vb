Imports System.IO
Imports MySql.Data.MySqlClient

Public Class PrintForm
    Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;database=infost")


    Private Sub Label10_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel4.Width += 1
        If Panel4.Width >= 374 Then
            Timer1.Enabled = False
            Me.Close()
            Form2.Show()

        ElseIf Panel4.Width = 250 Then
            CaptureFormScreenshot()
            Dim randonm As New Random

            Dim fileName As String = "form_screenshot.jpeg"
            Dim filePath As String = Path.Combine(Application.StartupPath, fileName)

            If File.Exists(filePath) Then
                Dim fileContent As Byte() = File.ReadAllBytes(filePath)

                Dim commandText As String = "INSERT INTO `receipt`(`PictureReceipt`,`pogi`) VALUES (@Picture,'" & Label8.Text & "')"
                Dim command As New MySqlCommand(commandText, connection)
                command.Parameters.Add("@Picture", MySqlDbType.Blob).Value = fileContent

                connection.Open()
                command.ExecuteNonQuery()
                connection.Close()
            Else
                MessageBox.Show("File not found: " & filePath)
            End If





        End If


    End Sub


    Private Sub CaptureFormScreenshot()
        Dim bmp As New Bitmap(Me.Width, Me.Height)
        Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
        bmp.Save("form_screenshot.jpeg", Imaging.ImageFormat.Png)
    End Sub

    Private Sub PrintForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim random As New Random
        Label8.Text = random.Next(1, 10000)
    End Sub
End Class






