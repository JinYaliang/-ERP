Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess

Public Class ProductionRetrocedeSelect

    Dim ds As New DataSet
    Dim TempA As String, TempB As String
    Dim StrA As String, StrB As String, StrC As String


    Private Sub ProductionRetrocedeSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label2.Text = tempValue  '����
        TempA = Label2.Text
        Label3.Text = tempValue4  '�ܮw�s��
        tempValue = ""
        tempValue4 = ""

        If Label2.Text = "�Ͳ��ɰh�f��" Then
            ComboBoxEdit1.EditValue = "�ɰh�f�渹"
        Else
            ComboBoxEdit1.EditValue = "�h�f�渹"
        End If

        CreateTable()
        LoadTable()
        LoadPM_M_Code()
        RadioButton1.Checked = True

    End Sub


    Private Sub XtraTabControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If XtraTabControl1.SelectedTabPage.Text = "�T�w�˦�" Then
            TextEdit1.Select()
        Else
            TextEdit2.Select()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            ComboBoxEdit1.Focus()
        End If
    End Sub

    '�O�s�d�ߵ��G�H��
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        '���w��e��ڭܮw�H�� �]�ثe���Ҷ��D�n�w��˰t�h�f�A�Ͳ��ɰe�f�H���^
        '�D�n�ݭn�Ψ쪺�ܮw���Ͳ��汵�ܥH�θ˰t�汵��---��������
        If Len(TextEdit3.Text) = 3 Then
            MsgBox("�п�ܥ��T���ܮw!")
            Exit Sub

        End If
        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If RadioButton1.Checked = True Then

                If ComboBoxEdit1.Text = "�h�f�渹" Or ComboBoxEdit1.Text = "�ɰh�f�渹" Then
                    tempValue = 1
                    tempValue2 = TextEdit1.Text
                End If

            End If
        Else
            '�ϥΦ۩w�q�d�߫H��

            If ds.Tables("SelectTbDel").Rows.Count = 0 Then
                MsgBox("�d�߱��󬰪�,�вK�[�Z�b�T�w!", , "����")
                Exit Sub
            End If
            tempValue = 2
            tempValue2 = ""
            For i As Integer = 0 To ds.Tables("SelectTbDel").Rows.Count - 1
                tempValue2 = tempValue2 & ds.Tables("SelectTbDel").Rows(i)("DelConditon").ToString
            Next


            '�Ȯɫ̽���e�o�X�ܮw�H��  2012/9/5
            'tempValue2 = tempValue2 & " and WH_OutID='" & TextEdit3.Text & "'"
        End If

        Me.Close()
    End Sub

    Sub CreateTable()

        ds.Tables.Clear()

        With ds.Tables.Add("SelectTb")
            .Columns.Add("AutoID", GetType(Integer))
            .Columns.Add("Category", GetType(String))
            .Columns.Add("Type", GetType(String))
            .Columns.Add("FieldName", GetType(String))
        End With

        Grid1.DataSource = ds.Tables("SelectTb")

        With ds.Tables.Add("SelectTbDel")
            .Columns.Add("DelConditon", GetType(String))
            .Columns.Add("DelDisplay", GetType(String))
        End With
        Grid2.DataSource = ds.Tables("SelectTbDel")

    End Sub

    '�ɤJ���w�Ҷ����ݭn�d�ߪ��r�q�H��
    Private Sub LoadTable()

        Dim Row As DataRow
        On Error Resume Next

        If TempA = "�˰t�h�f��" Then
            StrA = "�h�f�渹,�Ͳ��u��,���~�s��,�t��W��,���J�ܮw,�ާ@���,�f��"
            StrB = "��r,��r,��r,��r,��r,���,��r"
            StrC = "R_NO,Pro_Type,PM_M_Code,PM_Type,WH_InID,R_Date,R_Check"
        End If
        If TempA = "�Ͳ��ɰh�f��" Then
            StrA = "�ɰh�f�渹,�h�f�渹,�Ͳ��u��,���~�s��,�t��W��,���J�ܮw,�ާ@���,�f��"
            StrB = "��r,��r,��r,��r,��r,��r,���,��r"
            StrC = "AR_NO,R_NO,Pro_Type,PM_M_Code,PM_Type,WH_InID,AR_Date,AR_Check"
        End If

        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")
        Dim StrCarray As Array = Split(StrC, ",")

        For i As Integer = 0 To UBound(StrAarray)

            Row = ds.Tables("SelectTb").NewRow
            Row("AutoID") = i
            Row("Category") = StrAarray(i)
            Row("Type") = StrBarray(i)
            Row("FieldName") = StrCarray(i)
            ds.Tables("SelectTb").Rows.Add(Row)
        Next i
    End Sub

    '���J���~�s���H��
    Sub LoadPM_M_Code()

        Dim mc As New ProcessMainControl
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)

    End Sub

    '�h�^��e�d�߼Ҷ� 
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    '�K�[�s����r�������d�߱���
    Private Sub SimpleButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton7.Click

        Dim row As DataRow

        Row = ds.Tables("SelectTbDel").NewRow

        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            If GridView1.GetFocusedRowCellValue("Category") = "�Ͳ��u��" Then
                row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Mid(Trim(ComboBoxEdit4.Text), 1, 4) + "'" + " "
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Mid(Trim(ComboBoxEdit4.Text), 1, 4) + "'"
            ElseIf GridView1.GetFocusedRowCellValue("Category") = "���~�s��" Then
                row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(PM_M_Code.EditValue) + "'" + " "
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(PM_M_Code.EditValue) + "'"
            ElseIf GridView1.GetFocusedRowCellValue("Category") = "�f��" Then
                row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(ComboBoxEdit5.Text) + "'" + " "
                Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(ComboBoxEdit5.Text) + "'"

            End If

        Else
            If GridView1.GetFocusedRowCellValue("Category") = "�Ͳ��u��" Then
                row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Mid(Trim(ComboBoxEdit4.Text), 1, 4) + " " + "'"
                row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Mid(Trim(ComboBoxEdit4.Text), 1, 4) + "'"
            ElseIf GridView1.GetFocusedRowCellValue("Category") = "���~�s��" Then
                row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(PM_M_Code.EditValue) + " " + "'"
                row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(PM_M_Code.EditValue) + "'"
            ElseIf GridView1.GetFocusedRowCellValue("Category") = "�f��" Then
                row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(ComboBoxEdit5.Text) + " " + "'"
                row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(ComboBoxEdit5.Text) + "'"
 
            End If
        End If
        ds.Tables("SelectTbDel").Rows.Add(Row)
        TempB = ""
    End Sub

    '�K�[�s���d�߱���H��
    Private Sub SimpleButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton5.Click
        If TextEdit2.Text = "" Then
            MsgBox("�ƾڬ���,����K�[!")
            Exit Sub
        End If
        Dim Row As DataRow
        Row = ds.Tables("SelectTbDel").NewRow
        If GridView1.GetFocusedRowCellValue("Category") = "���ƦW��" Or GridView1.GetFocusedRowCellValue("Category") = "���ƳW��" Or GridView1.GetFocusedRowCellValue("Category") = "��������" Then
            Row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " like " + "'%" + Trim(TextEdit2.Text) + "%'" + " "
        Else
            Row("DelConditon") = " " & Mid(Trim(ComboBoxEdit3.Text), 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + " = " + "'" + Trim(TextEdit2.Text) + "'" + " "
        End If

        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(TextEdit2.Text) + "'"
        Else
            Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " = " + "'" + Trim(TextEdit2.Text) + "'"
        End If

        ds.Tables("SelectTbDel").Rows.Add(Row)
        TextEdit2.Text = ""
    End Sub
    '�R����eGrid2��e�襤�檺�d�߱���
    Private Sub SimpleButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton6.Click
        If GridView2.RowCount = 0 Then Exit Sub

        Dim i As Integer = ds.Tables("SelectTbDel").Rows.Count - 1
        ds.Tables("SelectTbDel").Rows.RemoveAt(i)

    End Sub
    '�K�[����d��
    Private Sub SimpleButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton4.Click
        Dim Row As DataRow

        Row = ds.Tables("SelectTbDel").NewRow
        Row("DelConditon") = " " & Mid(ComboBoxEdit3.Text, 1, 3) + " " + GridView1.GetFocusedRowCellValue("FieldName") + "  " + Mid(Trim(ComboBoxEdit2.Text), 1, 3) + " " + "'" + DateEdit1.Text + "'" + " "
        If ds.Tables("SelectTbDel").Rows.Count = 0 Then
            Row("DelDisplay") = GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ComboBoxEdit2.Text), 3) + " " + "'" + DateEdit1.Text + "'"
        Else
            Row("DelDisplay") = Mid(Trim(ComboBoxEdit3.Text), 1, 4) + GridView1.GetFocusedRowCellValue("Category") + " " + Mid(Trim(ComboBoxEdit2.Text), 3) + " " + "'" + DateEdit1.Text + "'"
        End If
        ds.Tables("SelectTbDel").Rows.Add(Row)
    End Sub

    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        On Error Resume Next

        If GridView1.GetFocusedRowCellValue("Type") = "���" Then
            XtraTabControl2.SelectedTabPageIndex = 1
            XtraTabControl2.TabPages(0).PageEnabled = False
            XtraTabControl2.TabPages(1).PageEnabled = True
            LabelControl3.Text = ""
        Else
            Dim tempC As String = "�h�f�渹,�ɰh�f�渹,���J�ܮw,�t��W��"
            Dim Strarray As Array = Split(tempC, ",")
            For i As Integer = 0 To UBound(Strarray)
                If GridView1.GetFocusedRowCellValue("Category").ToString = Strarray(i) Then
                    SimpleButton7.Enabled = False
                    SimpleButton5.Enabled = True
                    TextEdit2.Enabled = True
                    TextEdit2.Focus()
                    ComboBoxEdit4.Visible = False
                    ComboBoxEdit5.Visible = False
                    PM_M_Code.Visible = False

                    Exit For
                Else

                    If GridView1.GetFocusedRowCellValue("Category").ToString = "�Ͳ��u��" Then
                        ComboBoxEdit4.Visible = True
                        ComboBoxEdit5.Visible = False
                        PM_M_Code.Visible = False
                    ElseIf GridView1.GetFocusedRowCellValue("Category").ToString = "���~�s��" Then
                        ComboBoxEdit4.Visible = False
                        ComboBoxEdit5.Visible = False
                        PM_M_Code.Visible = True
                    ElseIf GridView1.GetFocusedRowCellValue("Category").ToString = "�f��" Then
                        ComboBoxEdit4.Visible = False
                        ComboBoxEdit5.Visible = True
                        PM_M_Code.Visible = False
                    End If

                    TextEdit2.Enabled = False
                    SimpleButton5.Enabled = False
                    SimpleButton7.Enabled = True
                End If
            Next

            XtraTabControl2.SelectedTabPageIndex = 0
            XtraTabControl2.TabPages(0).PageEnabled = True
            XtraTabControl2.TabPages(1).PageEnabled = False
            LabelControl3.Text = "(" + GridView1.GetFocusedRowCellValue("Category") + ")"
        End If

    End Sub
End Class