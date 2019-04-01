Imports LFERP.Library.PieceProcess

Public Class FrmPieceSelect

    Dim ds As New DataSet
    Dim StrA As String, StrB As String, StrC As String
    Dim TempA As String, TempB As String
    Dim ps As New LFERP.Library.PieceProcess.PieceSelectControl
    Dim piL As New List(Of LFERP.Library.PieceProcess.PieceSelectInfo)

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton3.Click
        Dim fr As New FrmPieceSelectSub
        fr.ShowDialog()
    End Sub

    Private Sub SimpleButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton8.Click
        Dim fr As New FrmPieceSelectSub
        fr.ShowDialog()
        TextEdit4.Text = tempValue
        tempValue = ""
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        ps.PieceSelect("�p��W��-�H��", TextEdit4.Text)
        Me.Close()
    End Sub

    Private Sub FrmPieceSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TempA = tempValue
        tempValue = ""
        CreateTable()
        LoadTable()
    End Sub
    Private Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("PieceSelect")
            .Columns.Add("AutoID", GetType(Integer))
            .Columns.Add("Category", GetType(String))
            .Columns.Add("Type", GetType(String))
            .Columns.Add("FieldName", GetType(String))
        End With
        GridControl1.DataSource = ds.Tables("PieceSelect")

        With ds.Tables.Add("SelectTbDel")
            .Columns.Add("DelConditon", GetType(String))
            .Columns.Add("DelDisplay", GetType(String))
        End With
        GridControl2.DataSource = ds.Tables("SelectTbDel")
    End Sub
    Private Sub LoadTable()
        Dim Row As DataRow
        On Error Resume Next

        If TempA = "�p��W��-�H��" Then
            StrA = "���u�էO,�إߤ��,����,�t�O,�~������"
            StrB = "��r,���,��r,��r,��r"
            StrC = "Per_WgNO,Per_AddDate,DepNO,FacNO,Per_PayType"
        End If
        If TempA = "�p��W��-�էO" Then
            StrA = "�t�O,����,�إߤ��"
            StrB = "��r,��r,���"
            StrC = "FacNO,DepNO,Wg_AddDate"
        End If
        If TempA = "�p��u���޲z" Then
            StrA = "�ާ@�H,�إߤ��"
            StrB = "��r,���"
            StrC = "PM_Action,PM_AddDate"
        End If
        If TempA = "�ӤH�p��-�u�ǭp��" Then
            StrA = "�t�ҽs��,�t�O,����,�إߤ��"
            StrB = "��r,��r,��r,���"
            StrC = "Per_NO,FacNO,DepNO,PP_AddDate"
        End If
        If TempA = "�ӤH�p��-�u�ǭp��" Then
            StrA = "�t�ҽs��,�t�O,����,�إߤ��"
            StrB = "��r,��r,��r,���"
            StrC = "Per_NO,FacNO,DepNO,PT_AddDate"
        End If
        If TempA = "�էO�p��" Then
            StrA = "�t�ҽs��,�t�O,����,�إߤ��"
            StrB = "��r,��r,��r,���"
            StrC = "Per_NO,FacNO,DepNO,PT_AddDate"
        End If
        If TempA = "�էO�p��" Then
            StrA = "�t�ҽs��,�t�O,����,�إߤ��"
            StrB = "��r,��r,��r,���"
            StrC = "Per_NO,FacNO,DepNO,PT_AddDate"
        End If
        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")
        Dim StrCarray As Array = Split(StrC, ",")

        For i As Integer = 0 To UBound(StrAarray)

            Row = ds.Tables("PieceSelect").NewRow
            Row("AutoID") = i
            Row("Category") = StrAarray(i)
            Row("Type") = StrBarray(i)
            Row("FieldName") = StrCarray(i)
            ds.Tables("PieceSelect").Rows.Add(Row)
        Next i

    End Sub

    Private Sub RadioButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.Click
        TextEdit3.Enabled = False
        TextEdit4.Enabled = False
    End Sub

    Private Sub RadioButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.Click
        ComboBoxEdit1.Enabled = False
        TextEdit4.Enabled = False
    End Sub

    Private Sub RadioButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.Click
        ComboBoxEdit1.Enabled = False
        TextEdit3.Enabled = False
    End Sub

    Private Sub SimpleButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton6.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim i As Integer = ds.Tables("SelectTbDel").Rows.Count - 1
        ds.Tables("SelectTbDel").Rows.RemoveAt(i)
    End Sub
End Class