Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.Library.WareHouse.WareInput
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports LFERP.FileManager
Imports LFERP.Library.WareHouse.WareMove


Public Class FrmWareExport

    Dim tempSTR As String, tempA As String



    Private Sub FrmWareExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tempSTR = tempValue
        tempA = tempValue2
        If tempSTR = "WareInput" Then
            CheckBox1.Text = "�J�w���"
        ElseIf tempSTR = "WareOut" Then
            CheckBox1.Text = "�X�w���"
        ElseIf tempSTR = "WareMove" Then
            CheckBox1.Text = "�ռ����"
        End If
        tempValue = ""
        tempValue2 = ""
        DateEdit1.Text = Format(Now, "yyyy/MM/dd")
        DateEdit2.Text = Format(Now, "yyyy/MM/dd")
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If CheckBox1.Checked Then
            If DateEdit1.Text = "" Or DateEdit2.Text = "" Then
                MsgBox("������ର��!")
                Exit Sub
            End If
            tempValue = DateEdit1.EditValue
            tempValue2 = DateEdit2.EditValue
            Me.Close()
        End If

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            DateEdit1.Enabled = True
            DateEdit2.Enabled = True
            DateEdit1.Focus()
        Else
            DateEdit1.Enabled = False
            DateEdit2.Enabled = False
        End If
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim ds As New DataSet
        Dim ctd As New CollectionToDataSet
        Dim woc As New WareOutController
        Dim wip As New WareInputContraller
        Dim wvc As New WareMoveController

        ds.Tables.Clear()
        If CheckBox1.Checked Then
            If DateEdit1.Text = "" Or DateEdit2.Text = "" Then
                MsgBox("������ର��!")
                Exit Sub
            End If

        End If

        If tempSTR = "WareOut" Then

            If ctd.CollToDataSet(ds, "WareOutExport", woc.WareOutExport_Getlist(tempA, Nothing, DateEdit1.Text, DateEdit2.Text)) Then


                Dim exapp As New Excel.Application
                Dim exbook As Excel.Workbook
                Dim exsheet As Excel.Worksheet

                Dim i As Integer = 0, ii As Integer = 0

                exapp = CreateObject("Excel.Application")
                exbook = exapp.Workbooks.Add
                exsheet = exapp.Worksheets(1)

                exsheet.Cells(1, 1) = "�X�w�渹"
                exsheet.Cells(1, 2) = "���ƽs�X"
                exsheet.Cells(1, 3) = "���ƦW��"
                exsheet.Cells(1, 4) = "�W��"
                exsheet.Cells(1, 5) = "�X�w�ƶq"
                exsheet.Cells(1, 6) = "���"
                exsheet.Cells(1, 7) = "�妸"
                exsheet.Cells(1, 8) = "�Ƶ�"
                exsheet.Cells(1, 9) = "��ƤH"
                exsheet.Cells(1, 10) = "�ާ@��"
                exsheet.Cells(1, 11) = "�f��"
                exsheet.Cells(1, 12) = "�X�w���"
                exsheet.Cells(1, 13) = "�f�ֳƵ�"
                exsheet.Cells(1, 14) = "��ƤH�u��"
                exsheet.Cells(1, 15) = "�ӻ�渹"

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    ii = i + 2
                    exsheet.Cells(ii, 1) = ds.Tables(0).Rows(i)("WO_ID")
                    exsheet.Cells(ii, 2) = "'" & ds.Tables(0).Rows(i)("M_Code")
                    exsheet.Cells(ii, 3) = ds.Tables(0).Rows(i)("M_Name")
                    exsheet.Cells(ii, 4) = ds.Tables(0).Rows(i)("M_Gauge")
                    exsheet.Cells(ii, 5) = ds.Tables(0).Rows(i)("WO_Qty")
                    exsheet.Cells(ii, 6) = ds.Tables(0).Rows(i)("M_Unit")
                    exsheet.Cells(ii, 7) = ds.Tables(0).Rows(i)("OS_BatchID")
                    exsheet.Cells(ii, 8) = ds.Tables(0).Rows(i)("WO_Remark")
                    exsheet.Cells(ii, 9) = ds.Tables(0).Rows(i)("WO_PerName")
                    exsheet.Cells(ii, 10) = ds.Tables(0).Rows(i)("WO_ActionName")
                    exsheet.Cells(ii, 11) = ds.Tables(0).Rows(i)("WO_Check")
                    exsheet.Cells(ii, 12) = CDate(ds.Tables(0).Rows(i)("WO_AddDate"))
                    exsheet.Cells(ii, 13) = ds.Tables(0).Rows(i)("WO_CheckRemark")
                    exsheet.Cells(ii, 14) = "'" & ds.Tables(0).Rows(i)("WO_PerID")
                    exsheet.Cells(ii, 15) = "'" & ds.Tables(0).Rows(i)("AP_NO")
                Next
                exapp.Visible = True
            Else
                MsgBox("�S���ƾ�!")
            End If
        ElseIf tempSTR = "WareInput" Then

            If ctd.CollToDataSet(ds, "WareInputExport", wip.WareInputExport_Getlist(tempA, Nothing, DateEdit1.Text, DateEdit2.Text)) Then


                Dim exapp As New Excel.Application
                Dim exbook As Excel.Workbook
                Dim exsheet As Excel.Worksheet

                Dim i As Integer = 0, ii As Integer = 0

                exapp = CreateObject("Excel.Application")
                exbook = exapp.Workbooks.Add
                exsheet = exapp.Worksheets(1)

                exsheet.Cells(1, 1) = "�J�w�渹"
                exsheet.Cells(1, 2) = "���ƽs�X"
                exsheet.Cells(1, 3) = "���ƦW��"
                exsheet.Cells(1, 4) = "�W��"
                exsheet.Cells(1, 5) = "�J�w�ƶq"
                exsheet.Cells(1, 6) = "���"
                exsheet.Cells(1, 7) = "�妸"
                exsheet.Cells(1, 8) = "�Ƶ�"
                exsheet.Cells(1, 9) = "�ާ@��"
                exsheet.Cells(1, 10) = "�f��"
                exsheet.Cells(1, 11) = "�J�w���"
                exsheet.Cells(1, 12) = "�f�ֳƵ�"


                For i = 0 To ds.Tables(0).Rows.Count - 1
                    ii = i + 2
                    exsheet.Cells(ii, 1) = ds.Tables(0).Rows(i)("WIP_ID")
                    exsheet.Cells(ii, 2) = "$" & ds.Tables(0).Rows(i)("M_Code") & "$"
                    exsheet.Cells(ii, 3) = ds.Tables(0).Rows(i)("M_Name")
                    exsheet.Cells(ii, 4) = ds.Tables(0).Rows(i)("M_Gauge")
                    exsheet.Cells(ii, 5) = ds.Tables(0).Rows(i)("WIP_Qty")
                    exsheet.Cells(ii, 6) = ds.Tables(0).Rows(i)("M_Unit")
                    exsheet.Cells(ii, 7) = ds.Tables(0).Rows(i)("OS_BatchID")
                    exsheet.Cells(ii, 8) = ds.Tables(0).Rows(i)("WIP_Remark")
                    exsheet.Cells(ii, 9) = ds.Tables(0).Rows(i)("WIP_ActionName")
                    exsheet.Cells(ii, 10) = ds.Tables(0).Rows(i)("WIP_Check")
                    exsheet.Cells(ii, 11) = CDate(ds.Tables(0).Rows(i)("WIP_AddDate"))
                    exsheet.Cells(ii, 12) = ds.Tables(0).Rows(i)("WIP_CheckRemark")
                Next
                exapp.Visible = True
            Else
                MsgBox("�S���ƾ�!")
            End If
        ElseIf tempSTR = "WareMove" Then

            If ctd.CollToDataSet(ds, "WareMove", wvc.WareMove_GetList1(Nothing, Nothing, Nothing, tempA, Nothing, Nothing, Nothing, Nothing, "2", DateEdit1.Text, DateEdit2.Text)) Then

                Dim exapp As New Excel.Application
                Dim exbook As Excel.Workbook
                Dim exsheet As Excel.Worksheet

                Dim i As Integer = 0, ii As Integer = 0

                exapp = CreateObject("Excel.Application")
                exbook = exapp.Workbooks.Add
                exsheet = exapp.Worksheets(1)

                exsheet.Cells(1, 1) = "�ռ��渹"
                exsheet.Cells(1, 2) = "���ƽs�X"
                exsheet.Cells(1, 3) = "���ƦW��"
                exsheet.Cells(1, 4) = "�W��"
                exsheet.Cells(1, 5) = "�ռ��ƶq"
                exsheet.Cells(1, 6) = "���"
                exsheet.Cells(1, 7) = "�ܮw�N��"
                exsheet.Cells(1, 8) = "�ܧ�ܮw�N��"
                exsheet.Cells(1, 9) = "�ާ@��"
                exsheet.Cells(1, 10) = "���o�ʽ�"
                exsheet.Cells(1, 11) = "�ռ����"
                exsheet.Cells(1, 12) = "�Ƶ�"
                exsheet.Cells(1, 13) = "�T�{"
                exsheet.Cells(1, 14) = "���ƤH"
                exsheet.Cells(1, 15) = "�f��"
                exsheet.Cells(1, 16) = "�f�ֳƵ�"


                For i = 0 To ds.Tables(0).Rows.Count - 1
                    ii = i + 2
                    exsheet.Cells(ii, 1) = ds.Tables(0).Rows(i)("MV_NO")
                    exsheet.Cells(ii, 2) = "$" & ds.Tables(0).Rows(i)("M_Code") & "$"
                    exsheet.Cells(ii, 3) = ds.Tables(0).Rows(i)("M_Name")
                    exsheet.Cells(ii, 4) = ds.Tables(0).Rows(i)("M_Gauge")
                    exsheet.Cells(ii, 5) = ds.Tables(0).Rows(i)("MV_Qty")
                    exsheet.Cells(ii, 6) = ds.Tables(0).Rows(i)("M_Unit")
                    exsheet.Cells(ii, 7) = ds.Tables(0).Rows(i)("DepotNO")
                    exsheet.Cells(ii, 8) = ds.Tables(0).Rows(i)("MV_ChangeDepotNO")
                    exsheet.Cells(ii, 9) = ds.Tables(0).Rows(i)("MV_OutActionName")
                    exsheet.Cells(ii, 10) = ds.Tables(0).Rows(i)("MV_InOrOut")
                    exsheet.Cells(ii, 11) = CDate(ds.Tables(0).Rows(i)("MV_Date"))
                    exsheet.Cells(ii, 12) = ds.Tables(0).Rows(i)("MV_Remark")
                    exsheet.Cells(ii, 13) = ds.Tables(0).Rows(i)("MV_Ack")
                    exsheet.Cells(ii, 14) = ds.Tables(0).Rows(i)("MV_CheckActionName")
                    exsheet.Cells(ii, 15) = ds.Tables(0).Rows(i)("MV_Check")
                    exsheet.Cells(ii, 16) = ds.Tables(0).Rows(i)("MV_CheckRemark")


                Next
                exapp.Visible = True
            Else
                MsgBox("�S���ƾ�!")
            End If

        End If
        Me.Close()
        '�qexcel�ɤJ��dataGridView1 
        'Dim excel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()

        'Dim xlBook As Microsoft.Office.Interop.Excel.Workbook
        'Dim xlSheet As Microsoft.Office.Interop.Excel.Worksheet




        'Dim fileDialog As OpenFileDialog = New OpenFileDialog()
        'Dim FileName As String
        'fileDialog.Filter = "Microsoft Excel files (*.xls)|*.xls"
        'If fileDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub

        'If fileDialog.FileName = Nothing Then
        '    MsgBox("�п�ܭn�ɤJ��excel���", , "����")
        'End If
        'FileName = fileDialog.FileName


        'xlBook = excel.Application.Workbooks.Open(FileName)

        'xlSheet = xlBook.Application.Worksheets(1)

        'Dim col As Integer = 0
        'Dim i As Integer = 2

        'Me.dataGridView1.Rows.Clear()

        'Do While i < 5
        '    dataGridView1.Rows.Add()

        '    dataGridView1.Rows(col).Cells(0).Value = xlSheet.Cells(i, 1).value
        '    dataGridView1.Rows(col).Cells(1).Value = xlSheet.Cells(i, 2).value
        '    dataGridView1.Rows(col).Cells(2).Value = xlSheet.Cells(i, 3).value

        '    i = i + 1
        '    col = col + 1

        'Loop
        'excel.ActiveWorkbook.Close(False)
        'xlSheet = Nothing
        'xlBook = Nothing
        'excel = Nothing

        'If dataGridView1.Rows.Count >= 1 Then
        '    MessageBox.Show("�ɤJ���\")
        'Else
        '    MessageBox.Show("�ɤJ����")
        'End If
    End Sub

End Class