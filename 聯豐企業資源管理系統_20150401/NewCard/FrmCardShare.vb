Public Class FrmCardShare

    Private Function ReadCard() As String

        Dim portptr As IntPtr = ReadWriteCard.readwriteDll.OpenCommPort(1, 9600)
        Dim port As Integer = Int32.Parse(portptr.ToString())
        Dim isclock As Boolean

        If port <> -1 AndAlso port <> 0 Then
            isclock = ReadWriteCard.readwriteDll.CallClock(portptr, Int32.Parse(0))
            If isclock Then


                Dim temp As New ReadWriteCard.info
                temp.CardNo = New Byte(16) {}
                temp.CardName = New Byte(16) {}
                temp.Money = 0
                temp.Times = 0
                temp.Ver = 0
                Try
                    Dim suc As Boolean = ReadWriteCard.readwriteDll.ReadICCard(portptr, temp.CardNo, temp.CardName, temp.Money, temp.Times, temp.Ver)

                    If suc Then

                        If Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 9, 1) = "2" Then
                            ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8)
                        Else
                            ReadCard = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7)
                        End If
                    Else
                        MessageBox.Show("�L�k�����u�d�Ψ�d�����s���I")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                ReadWriteCard.readwriteDll.CloseCommPort(1)
            Else
                MessageBox.Show("�p������!")
                ReadCard = ""
            End If
        ElseIf port = 0 Then
            MessageBox.Show("�L�k���}�ݤf!")
        ElseIf port = -1 Then

            MessageBox.Show("�ݤf�L�ĩΥ��b�ϥ�!")

        End If

    End Function

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        TextEdit1.Text = ReadCard()
        CardNo = TextEdit1.Text
        'Me.Close()
    End Sub

    Private Sub FrmCardShare_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim reset As New ResetPassWords.SetPassWords
        reset.SetPassWords()
    End Sub

    Private Sub FrmCardShare_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextEdit1.Text = ReadCard()
            CardNo = TextEdit1.Text
            'Me.Close()
        End If
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class