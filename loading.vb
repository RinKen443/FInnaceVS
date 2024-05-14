Public Class loading
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel2.Width += 10
        If Panel2.Width >= 785 Then

            Me.Timer1.Enabled = False
            Me.Close()
            Panel2.Width = 16
        End If
    End Sub
End Class