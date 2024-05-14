Imports System.IO
Imports System.Net.NetworkInformation
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Common
Public Class Form2
    Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;database=infost")
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2.Enabled = False
        Button1.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True

        loading.Timer1.Enabled = True
        loading.Show()
        Viewers1()

        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button1.Enabled = True
        Button2.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button3.Enabled = False
        loading.Timer1.Enabled = True
        loading.Show()
        Viewers1()

        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button4.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button5.Enabled = True
        loading.Timer1.Enabled = True
        loading.Show()
        Viewers1()

        TabControl1.SelectedTab = TabPage3
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        TabControl1.SelectedTab = TabPage4


        Label76.Text = DataGridView2.RowCount
        Dim column As Double
        For Each h As DataGridViewRow In DataGridView2.Rows
            column += h.Cells(1).Value

        Next

        Label75.Text = column

        Button5.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True

        Button4.Enabled = True
        Viewers1()
        loading.Timer1.Enabled = True
        loading.Show()


        TabControl1.SelectedTab = TabPage4

        Studentbalance()
        Currentpay()




    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click





        Form1.Show()
        Close()
        Form1.TextBox1.Clear()
        Form1.TextBox2.Clear()
        Viewers1()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        If String.IsNullOrEmpty(TextBox1.Text.Trim) Then
            MessageBox.Show("Please Insert a amount to proceed to pay")
        ElseIf String.IsNullOrEmpty(TextBox4.Text.Trim) Then
            MessageBox.Show("Please Input a purpose to proceed to pay")

        ElseIf Not IsNumeric(TextBox1.Text.Trim) Then
            MessageBox.Show("Please Insert a Valid Amount to pay")
        ElseIf Label5.Text.Trim = "Label5" Or Label6.Text.Trim = "Label5" Or Label7.Text.Trim = "Label5" Then
            MessageBox.Show("You must select student to proceed from payment")
        ElseIf TextBox1.Text <= 0 Then
            MessageBox.Show("Please insert a value greater than 0")
        Else

            If Label6.Text.Trim = 0 Then
                MessageBox.Show("You have 0 balance already")
            ElseIf Convert.ToDouble(Label6.Text.Trim) > Convert.ToDouble(TextBox1.Text.Trim) Then

                Dim balance As Double
                balance = Convert.ToDouble(Label6.Text.Trim) - Convert.ToDouble(TextBox1.Text.Trim)

                Dim command As New MySqlCommand("UPDATE `student` SET `balance`=balance - '" & TextBox1.Text & "' WHERE name ='" & Label5.Text & "'", connection)
                Dim command1 As New MySqlCommand("INSERT INTO `currentpay`(`name`, `amount`, `Balance`) VALUES ('" & Label5.Text & "','" & TextBox1.Text & "','" & balance & "')", connection)

                connection.Open()
                command1.ExecuteNonQuery()
                command.ExecuteNonQuery()
                MessageBox.Show("Complete")
                View()

                History()

                connection.Close()

                Viewers1()
                Dim change As Integer
                change = Convert.ToDouble(Label6.Text.Trim) - Convert.ToDouble(TextBox1.Text.Trim)

                PrintForm.Label17.Text = "0"
                PrintForm.Label14.Text = Label5.Text
                PrintForm.Label15.Text = Label7.Text
                PrintForm.Label16.Text = TextBox4.Text.ToString
                PrintForm.Label20.Text = TextBox1.Text

                PrintForm.Label18.Text = change
                PrintForm.Label22.Text = Label6.Text
                PrintForm.Label6.Text = Date.Now

                PrintForm.Timer1.Enabled = True
                PrintForm.Show()
                Hide()
                TextBox1.Clear()
                TextBox4.Clear()



                DataGridView2.Rows.Add(Label5.Text, PrintForm.Label20.Text)

                PictureBox1.Image = Nothing
                Label5.Text = "Label5"
                Label6.Text = "Label5"
                Label7.Text = "Label5"

            ElseIf Convert.ToDouble(TextBox1.Text.Trim) >= Convert.ToDouble(Label6.Text.Trim) Then
                Dim change As Integer

                change = Convert.ToDouble(TextBox1.Text) - Convert.ToDouble(Label6.Text)

                Dim command As New MySqlCommand("UPDATE `student` Set `balance`= 0 WHERE name ='" & Label5.Text.Trim & "'", connection)
                Dim command1 As New MySqlCommand("INSERT INTO `currentpay`(`name`, `amount`, `Balance`,`yearandcourse`,`datetime`,`changess`) VALUES ('" & Label5.Text & "','" & TextBox1.Text & "','0','" & Label7.Text & "','" & Date.Now.ToString & "','" & change & "')", connection)

                connection.Open()
                If command.ExecuteNonQuery > 1 Then

                Else

                End If
                command1.ExecuteNonQuery()
                command.ExecuteNonQuery()
                MessageBox.Show("The balance is = 0")



                View()

                History()

                connection.Close()

                PrintForm.Label17.Text = change
                PrintForm.Label14.Text = Label5.Text.Trim
                PrintForm.Label15.Text = Label7.Text.Trim
                PrintForm.Label16.Text = TextBox4.Text.ToString
                PrintForm.Label20.Text = TextBox1.Text.Trim
                PrintForm.Label22.Text = Label6.Text.Trim
                PrintForm.Label18.Text = "0"
                Viewers1()

                PrintForm.Label6.Text = Date.Now
                PrintForm.Timer1.Enabled = True

                PrintForm.Show()
                Hide()
                TextBox1.Clear()
                TextBox4.Clear()


                DataGridView2.Rows.Add(Label5.Text, PrintForm.Label20.Text)

                PictureBox1.Image = Nothing
                Label5.Text = "Label5"
                Label6.Text = "Label5"
                Label7.Text = "Label5"



            End If



        End If





    End Sub




    Private Sub Viewers1()
        Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")

        Dim table As New DataTable()

        Dim adapter As New MySqlDataAdapter("SELECT * FROM infost.receipt", connection)

        adapter.Fill(table)

        DataGridView32.DataSource = table

    End Sub

    Private Sub View()
        Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")

        Dim table As New DataTable()

        Dim adapter As New MySqlDataAdapter("SELECT * FROM infost.student", connection)

        adapter.Fill(table)

        DataGridView1.DataSource = table

    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        SearchData(TextBox2.Text)
    End Sub
    Private Sub SearchData(searchTerm As String)
        Dim query As String = "SELECT * FROM student WHERE yearandcourse LIKE @searchTerm Or name LIKE @searchTerm"
        Dim adapter As New MySqlDataAdapter(query, connection)
        adapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")

        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.RowHeadersVisible = False
        DataGridView32.RowHeadersVisible = False
        DataGridView3.RowHeadersVisible = False
        DataGridView4.RowHeadersVisible = False
        DataGridView2.RowHeadersVisible = False

        Viewers1()
        View()
        connection.Open()
        History()
        connection.Close()
    End Sub

    Public Sub History()
        Dim student1 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1", connection)
        Dim student1a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1", connection)
        Dim student1b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1", connection)

        Dim student2 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 1", connection)
        Dim student2a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 1", connection)
        Dim student2b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 1", connection)

        Dim student3 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC  LIMIT 1 OFFSET 2", connection)
        Dim student3a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC  LIMIT 1 OFFSET 2", connection)
        Dim student3b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 2", connection)

        Dim student4 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 3", connection)
        Dim student4a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 3", connection)
        Dim student4b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 3", connection)


        Dim student5 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 4", connection)
        Dim student5a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 4", connection)
        Dim student5b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 4", connection)

        Dim student6 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 5", connection)
        Dim student6a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 5", connection)
        Dim student6b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 5", connection)


        Dim student7 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID LIMIT 1 OFFSET 6", connection)
        Dim student7a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 6", connection)
        Dim student7b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 6", connection)

        Dim student8 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 7", connection)
        Dim student8a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 7", connection)
        Dim student8b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 7", connection)


        Dim student9 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 8", connection)
        Dim student9a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 8", connection)
        Dim student9b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 8", connection)

        Dim student10 As New MySqlCommand("SELECT name FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 9", connection)
        Dim student10a As New MySqlCommand("SELECT amount FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 9", connection)
        Dim student10b As New MySqlCommand("SELECT Balance FROM currentpay ORDER BY ID DESC LIMIT 1 OFFSET 9", connection)



        Label10.Text = Convert.ToString(student1.ExecuteScalar())
        Label14.Text = Convert.ToString(student1a.ExecuteScalar()) 'student 1' 
        Label13.Text = Convert.ToString(student1b.ExecuteScalar())

        Label19.Text = Convert.ToString(student2.ExecuteScalar())
        Label18.Text = Convert.ToString(student2a.ExecuteScalar()) 'student 2' 
        Label17.Text = Convert.ToString(student2b.ExecuteScalar())

        Label22.Text = Convert.ToString(student3.ExecuteScalar())
        Label41.Text = Convert.ToString(student3a.ExecuteScalar()) 'student 3 ' 
        Label40.Text = Convert.ToString(student3b.ExecuteScalar())

        Label23.Text = Convert.ToString(student4.ExecuteScalar())
        Label45.Text = Convert.ToString(student4a.ExecuteScalar()) 'student 4' 
        Label44.Text = Convert.ToString(student4b.ExecuteScalar())

        Label35.Text = Convert.ToString(student5.ExecuteScalar())
        Label49.Text = Convert.ToString(student5a.ExecuteScalar()) 'student 5' 
        Label48.Text = Convert.ToString(student5b.ExecuteScalar())

        Label34.Text = Convert.ToString(student6.ExecuteScalar())
        Label53.Text = Convert.ToString(student6a.ExecuteScalar()) 'student 6' 
        Label52.Text = Convert.ToString(student6b.ExecuteScalar())


        Label33.Text = Convert.ToString(student7.ExecuteScalar())
        Label57.Text = Convert.ToString(student7a.ExecuteScalar()) 'student 7' 
        Label56.Text = Convert.ToString(student7b.ExecuteScalar())

        Label32.Text = Convert.ToString(student8.ExecuteScalar())
        Label61.Text = Convert.ToString(student8a.ExecuteScalar()) 'student 8' 
        Label60.Text = Convert.ToString(student8b.ExecuteScalar())

        Label36.Text = Convert.ToString(student9.ExecuteScalar())
        Label65.Text = Convert.ToString(student9a.ExecuteScalar()) 'student 9' 
        Label64.Text = Convert.ToString(student9b.ExecuteScalar())

        Label37.Text = Convert.ToString(student10.ExecuteScalar())
        Label69.Text = Convert.ToString(student10a.ExecuteScalar()) 'student 10' 
        Label68.Text = Convert.ToString(student10b.ExecuteScalar())


    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim byteImage = DirectCast(DataGridView1.CurrentRow.Cells(3).Value, Byte())

        Using ms As New MemoryStream(byteImage)
            PictureBox1.Image = Image.FromStream(ms)
        End Using

        Label5.Text = DataGridView1.CurrentRow.Cells(0).Value
        Label6.Text = DataGridView1.CurrentRow.Cells(1).Value
        Label7.Text = DataGridView1.CurrentRow.Cells(2).Value
    End Sub

    Private Sub DataGridView32_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView32.CellClick
        Dim byteImage = DirectCast(DataGridView32.CurrentRow.Cells(1).Value, Byte())

        Using ms As New MemoryStream(byteImage)
            PictureBox2.Image = Image.FromStream(ms)
        End Using
        Label8.Text = DataGridView32.CurrentRow.Cells(0).Value
    End Sub

    Private Sub SearchData1(searchTerm As String)
        Dim query As String = "SELECT * FROM receipt WHERE pogi LIKE @searchTerm "
        Dim adapter As New MySqlDataAdapter(query, connection)
        adapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")

        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView32.DataSource = table
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        SearchData1(TextBox3.Text)
    End Sub



    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        AddStudent.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click



        TabControl1.SelectedTab = TabPage4


        Label76.Text = DataGridView2.RowCount
        Dim column As Double
        For Each h As DataGridViewRow In DataGridView2.Rows
            column += h.Cells(1).Value

        Next

        Label75.Text = column
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click, Button13.Click, Button16.Click
        TabControl1.SelectedTab = TabPage6

        Label90.Text = DataGridView4.RowCount
        Dim column As Double
        For Each h As DataGridViewRow In DataGridView4.Rows
            column += h.Cells(1).Value

        Next

        Label89.Text = column
        currentpay()

    End Sub


    Private Sub Currentpay()
        Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")

        Dim table As New DataTable()

        Dim adapter As New MySqlDataAdapter("SELECT `name`,`amount` FROM infost.currentpay", connection)

        adapter.Fill(table)

        DataGridView4.DataSource = table

    End Sub

    Private Sub Studentbalance()

        Dim connection As New MySqlConnection("datasource=localhost;username=root;port=3306;password=;")

        Dim table As New DataTable()

        Dim adapter As New MySqlDataAdapter("SELECT `name`,`balance` FROM infost.student", connection)

        adapter.Fill(table)

        DataGridView3.DataSource = table

    End Sub



    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        TabControl1.SelectedTab = TabPage4


        Label76.Text = DataGridView2.RowCount
        Dim column As Double
        For Each h As DataGridViewRow In DataGridView2.Rows
            column += h.Cells(1).Value

        Next

        Label75.Text = column
    End Sub



    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        TabControl1.SelectedTab = TabPage4


        Label76.Text = DataGridView2.RowCount
        Dim column As Double
        For Each h As DataGridViewRow In DataGridView2.Rows
            column += h.Cells(1).Value

        Next

        Label75.Text = column
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click, Button14.Click, Button17.Click
        TabControl1.SelectedTab = TabPage5
        studentbalance()

        Label85.Text = DataGridView3.RowCount
        Dim column As Double
        For Each h As DataGridViewRow In DataGridView3.Rows
            column += h.Cells(1).Value

        Next

        Label80.Text = column
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Application.Exit()

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click

        Dim dialog As DialogResult


        dialog = MessageBox.Show("Are you sure you want to delete this student '" & Label5.Text & "'?", "Delete Student", MessageBoxButtons.YesNo)


        If dialog = DialogResult.No Then

        ElseIf dialog = DialogResult.Yes Then

            If Label5.Text = "Label5" Then
                MessageBox.Show("To proceed to delete please select in datagridview")
            Else

                Dim command As New MySqlCommand("DELETE FROM `student` WHERE name='" & Label5.Text.ToString.Trim & "'", connection)

                connection.Open()
                command.ExecuteNonQuery()
                MessageBox.Show("Deleted")
                connection.Close()

                PictureBox1.Image = Nothing
                Label5.Text = "Label5"
                Label6.Text = "Label5"
                Label7.Text = "Label5"

                View()
            End If

        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        UpdateStudent.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class