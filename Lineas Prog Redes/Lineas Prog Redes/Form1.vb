Imports System.IO
Public Class Form1

    Private Sub btnIniciar_Click(sender As Object, e As EventArgs) Handles btnIniciar.Click
        Dim numLineas As Integer = Integer.Parse(txtNumLineas.Text)
        BackgroundWorker1.RunWorkerAsync(numLineas)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        BackgroundWorker1.CancelAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim numLineas As Integer = CType(e.Argument, Integer)

        Using sw As StreamWriter = New StreamWriter("Archivo.txt")
            For i As Integer = 1 To numLineas
                sw.WriteLine("Linea " & i.ToString())
                Dim porcentaje As Integer = CInt((CDbl(i) / CDbl(numLineas)) * 100)
                BackgroundWorker1.ReportProgress(porcentaje)

                If BackgroundWorker1.CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If
            Next
        End Using
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ' Actualizar la barra de progreso
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If e.Cancelled Then
            MessageBox.Show("Proceso cancelado por el usuario.")
        Else
            MessageBox.Show("Archivo creado correctamente.")
        End If
    End Sub

End Class
