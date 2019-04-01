Imports LFERP.Library.Purchase.WareQuality

Public Class frmWareQualityFind
    Dim mqc As New WareQualityController

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        Dim strAddDateBegin, strAddDateEnd As String
        If dteAddDateBegin.Text.Trim = "" Then      '�P�_��J����O�_���šA�]�s�x�L�{�������Ŧr�Ŧ�P�_
            strAddDateBegin = Nothing               '�s�x�L�{���u���F�ŭ�(NULL)�P�_�A�]���S����J����ɡA�u���ŭ�(NULL)
        Else
            strAddDateBegin = Format(CDate(dteAddDateBegin.Text), "yyyy/MM/dd")        '�����榡�ഫ���T�w�榡
        End If
        If dteAddDateEnd.Text.Trim = "" Then
            strAddDateEnd = Nothing
        Else
            strAddDateEnd = Format(CDate(dteAddDateEnd.Text), "yyyy/MM/dd")
        End If
        frmWareQualityMain.Grid.DataSource = mqc.WareQuality_GetList(Trim(txtWQ_Code.Text), Trim(txtM_Code.Text), Trim(txtM_Name.Text), Trim(txtM_Gauge.Text), Trim(txtWO_ID.Text), strAddDateBegin, strAddDateEnd)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmWareQualityFind_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%
        For i = 0 To 6      '�M�ũҦ��奻�ؤ��e
            GroupBox1.Controls.Item(i).Text = ""
        Next
        txtWQ_Code.Focus()
    End Sub
End Class