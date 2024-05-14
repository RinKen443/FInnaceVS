Public Class loadinglogin
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel3.Width += 2

        If Panel3.Width >= 221 Then
            Timer1.Enabled = False
            Me.Close()
            Form2.Show()

        End If
    End Sub
End Class