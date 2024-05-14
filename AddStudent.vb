Imports System.Drawing.Text
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class AddStudent

    Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;database=infost")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text) Then
            MessageBox.Show("To proceed to add student you must fill all textboxes")
        ElseIf PictureBox1.Image Is Nothing Then
            MessageBox.Show("Please Complete the Information need to insert a new student")
        ElseIf Not IsNumeric(TextBox3.Text) Then

            MessageBox.Show("Please insert a numeric number in Balance")
        Else

            Dim ms As New MemoryStream()
            Dim command1 As New MySqlCommand("SELECT `name` FROM `student` WHERE name = '" & TextBox1.Text & "'", connection)
            Dim adapter As New MySqlDataAdapter(command1)
            Dim table As New DataTable

            adapter.Fill(table)

            If table.Rows.Count >= 1 Then
                MessageBox.Show("The Student name is already exist in datagridview")
            Else
                PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
                Dim command As New MySqlCommand("INSERT INTO `student`(`name`, `balance`, `yearandcourse`, `picture`) VALUES (@name, @balance, @yearandcourse, @picture)", connection)
                command.Parameters.AddWithValue("@name", TextBox1.Text)
                command.Parameters.AddWithValue("@balance", TextBox3.Text)
                command.Parameters.AddWithValue("@yearandcourse", TextBox2.Text)
                command.Parameters.AddWithValue("@picture", ms.ToArray())


                connection.Open()


                command.ExecuteNonQuery()
                MessageBox.Show("Inserted Student")
                connection.Close()


                PictureBox1.Image = Nothing
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                view1()
            End If
        End If




    End Sub

    Private Sub View1()
        Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")
        Dim table As New DataTable()
        Dim adapter As New MySqlDataAdapter("SELECT * FROM infost.student", connection)
        adapter.Fill(table)
        Form2.DataGridView1.DataSource = table
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif"

        If opf.ShowDialog = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(opf.FileName)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form2.Show()
        Me.Close()

        view1()
    End Sub
End Class