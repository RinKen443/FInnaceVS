Imports MySql.Data.MySqlClient

Public Class Form1
    Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;database=infost")
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If TextBox2.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As New MySqlCommand("SELECT `username`, `passowrd` FROM `adminuser` WHERE username = '" & TextBox1.Text & "' AND passowrd = '" & TextBox2.Text & "'", connection)

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable

        adapter.Fill(table)

        If table.Rows.Count = 1 Then

            loadinglogin.Timer1.Enabled = True
            Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")

            Dim table1 As New DataTable()

            Dim adapter1 As New MySqlDataAdapter("SELECT * FROM infost.student", connection)

            adapter.Fill(table)

            Form2.DataGridView1.DataSource = table

            MessageBox.Show("Logged In")

            loadinglogin.Show()
            Me.Hide()

        Else
            MessageBox.Show("Invalid Username/Password")
        End If





    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
    End Sub


End Class
