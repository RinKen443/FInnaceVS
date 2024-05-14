Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class UpdateStudent





    Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;database=infost")
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Dim byteImage = DirectCast(DataGridView1.CurrentRow.Cells(3).Value, Byte())

        Using ms As New MemoryStream(byteImage)
            PictureBox1.Image = Image.FromStream(ms)
        End Using


        Label4.Text = DataGridView1.CurrentRow.Cells(4).Value
        TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
        TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value
        TextBox3.Text = DataGridView1.CurrentRow.Cells(1).Value

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click




        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure you want to update student to '" & TextBox1.Text & "'?", "Update Student", MessageBoxButtons.YesNo)

        If dialog = DialogResult.No Then


        ElseIf dialog = DialogResult.Yes Then


            If String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text) Or Label4.Text = "0" Then
                MessageBox.Show("To proceed to add student you must fill all textboxes")
            ElseIf PictureBox2.Image Is Nothing Then
                MessageBox.Show("Please Complete the Information need to UPDATE the student")
            ElseIf Not IsNumeric(TextBox3.Text) Then

                MessageBox.Show("Please insert a numeric number in Balance")
            Else

                Dim command1 As New MySqlCommand("SELECT `name` FROM `student` WHERE name = '" & TextBox1.Text & "'", connection)
                Dim adapter As New MySqlDataAdapter(command1)
                Dim table As New DataTable

                adapter.Fill(table)
                If table.Rows.Count = 1 Then
                    MessageBox.Show("Student name is already exist from database")
                Else
                    Dim ms As New MemoryStream()
                    PictureBox2.Image.Save(ms, PictureBox2.Image.RawFormat)
                    Dim command As New MySqlCommand("UPDATE `student` SET `name`=@name,`balance`=@balance,`yearandcourse`=@yearandcourse,`picture`=@picture WHERE ID = '" & Label4.Text & "'", connection)


                    command.Parameters.AddWithValue("@name", TextBox1.Text)
                    command.Parameters.AddWithValue("@balance", TextBox3.Text)
                    command.Parameters.AddWithValue("@yearandcourse", TextBox2.Text)
                    command.Parameters.AddWithValue("@picture", ms.ToArray())


                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Updated Student")
                    PictureBox1.Image = Nothing
                    PictureBox2.Image = Nothing
                    Label4.Text = "0"
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()

                    Viewers()
                    connection.Close()


                    Form2.PictureBox1.Image = Nothing
                    Form2.Label5.Text = "Label5"
                    Form2.Label6.Text = "Label5"
                    Form2.Label7.Text = "Label5"

                End If

            End If


        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif"


        If opf.ShowDialog = DialogResult.OK Then
            PictureBox2.Image = Image.FromFile(opf.FileName)
        End If
    End Sub

    Private Sub UpdateStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Viewers()
    End Sub

    Public Sub Viewers()

        Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")

        Dim table As New DataTable()

        Dim adapter As New MySqlDataAdapter("SELECT * FROM infost.student", connection)

        adapter.Fill(table)

        DataGridView1.DataSource = table
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")

        Dim table As New DataTable()

        Dim adapter As New MySqlDataAdapter("SELECT * FROM infost.student", connection)

        adapter.Fill(table)

        Form2.DataGridView1.DataSource = table

        Me.Close()
    End Sub
End Class