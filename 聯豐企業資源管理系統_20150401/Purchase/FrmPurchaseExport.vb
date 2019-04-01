Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports LFERP.FileManager
Imports LFERP.Library.Purchase
Imports LFERP.SystemManager

Public Class FrmPurchaseExport
    Dim tempSTR As String, tempA As String

    Private Sub FrmPurchaseExport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '"���ʺ޲z--���Ʊ���"
        tempSTR = tempValue2
        tempValue2 = ""
        DateEdit1.EditValue = Now
        DateEdit2.EditValue = Now
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckBox1.Checked Then
            If DateEdit1.Text = "" Or DateEdit2.Text = "" Then
                MsgBox("������ର��!")
                Exit Sub
            End If
            tempValue2 = DateEdit1.Text
            tempValue3 = DateEdit2.Text
            Me.Close()
        End If

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
        Dim Pmc As New Purchase.PurchaseMainController
        ds.Tables.Clear()
        If CheckBox1.Checked Then
            If DateEdit1.Text = "" Or DateEdit2.Text = "" Then
                MsgBox("������ର��!")
                Exit Sub
            End If

        End If

        If tempSTR = "���ʺ޲z--���Ʊ���" Then
            '***�P�_�Τ�O�֦��S�������v��
            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
            Dim tesuvalue As String
            tesuvalue = "�_"
            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                tesuvalue = "�O"
            End If
            Dim mc As New Purchase.PurchaseMainController

            If ctd.CollToDataSet(ds, "FrmPurchaseExport", Pmc.PurchaseMain_Getlist1(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "����", DateEdit1.Text, DateEdit2.Text, Nothing, Nothing, "���ʧ@�~", Nothing, Nothing, Nothing, tesuvalue)) Then


                Dim exapp As New Excel.Application
                Dim exbook As Excel.Workbook
                Dim exsheet As Excel.Worksheet

                Dim i As Integer = 0, ii As Integer = 0

                exapp = CreateObject("Excel.Application")
                exbook = exapp.Workbooks.Add
                exsheet = exapp.Worksheets(1)

                exsheet.Cells(1, 1) = "���ʳ渹"
                exsheet.Cells(1, 2) = "�����ӦW"
                exsheet.Cells(1, 3) = "���ƽs�X"
                exsheet.Cells(1, 4) = "�W��"
                exsheet.Cells(1, 5) = "�W��"
                exsheet.Cells(1, 6) = "�妸"
                exsheet.Cells(1, 7) = "�����ӽs��"
                exsheet.Cells(1, 8) = "���"
                exsheet.Cells(1, 9) = "���O"
                exsheet.Cells(1, 10) = "�ƶq"
                exsheet.Cells(1, 11) = "�����"
                exsheet.Cells(1, 12) = "���ʤ��"
                exsheet.Cells(1, 13) = "�����f��"
                exsheet.Cells(1, 14) = "�|�p�f��"
                exsheet.Cells(1, 15) = "�|�p�f������"
                exsheet.Cells(1, 16) = "�|�p���f�֭�"
                exsheet.Cells(1, 17) = "�ާ@��"

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    ii = i + 2
                    exsheet.Cells(ii, 1) = ds.Tables(0).Rows(i)("PM_NO")
                    exsheet.Cells(ii, 2) = ds.Tables(0).Rows(i)("S_SupplierName")
                    exsheet.Cells(ii, 3) = "#" & ds.Tables(0).Rows(i)("M_Code") & "#"
                    exsheet.Cells(ii, 4) = ds.Tables(0).Rows(i)("M_Name")
                    exsheet.Cells(ii, 5) = ds.Tables(0).Rows(i)("M_Gauge")
                    exsheet.Cells(ii, 6) = ds.Tables(0).Rows(i)("OS_BatchID")
                    exsheet.Cells(ii, 7) = ds.Tables(0).Rows(i)("S_SupplierNo")
                    exsheet.Cells(ii, 8) = ds.Tables(0).Rows(i)("PS_Price")
                    exsheet.Cells(ii, 9) = ds.Tables(0).Rows(i)("C_ID")
                    exsheet.Cells(ii, 10) = ds.Tables(0).Rows(i)("PS_QTY")
                    exsheet.Cells(ii, 11) = ds.Tables(0).Rows(i)("PS_NoSendQty")
                    exsheet.Cells(ii, 12) = ds.Tables(0).Rows(i)("PM_PurchaseDate")
                    exsheet.Cells(ii, 13) = ds.Tables(0).Rows(i)("PM_Check")

                    exsheet.Cells(ii, 14) = ds.Tables(0).Rows(i)("PM_AccountCheck")
                    exsheet.Cells(ii, 15) = ds.Tables(0).Rows(i)("PM_AccountCheckType")
                    exsheet.Cells(ii, 16) = ds.Tables(0).Rows(i)("PM_AccCheckActionName")
                    exsheet.Cells(ii, 17) = ds.Tables(0).Rows(i)("PM_ActionName")
                Next
                exapp.Visible = True
            Else
                MsgBox("�S���ƾ�!")
            End If
        ElseIf tempSTR = "���ʺ޲z--�j�f����" Then
        End If
        '    If ctd.CollToDataSet(ds, "WareInputExport", wip.WareInputExport_Getlist(tempA, Nothing, DateEdit1.Text, DateEdit2.Text)) Then


        '        Dim exapp As New Excel.Application
        '        Dim exbook As Excel.Workbook
        '        Dim exsheet As Excel.Worksheet

        '        Dim i As Integer = 0, ii As Integer = 0

        '        exapp = CreateObject("Excel.Application")
        '        exbook = exapp.Workbooks.Add
        '        exsheet = exapp.Worksheets(1)

        '        exsheet.Cells(1, 1) = "�J�w�渹"
        '        exsheet.Cells(1, 2) = "���ƽs�X"
        '        exsheet.Cells(1, 3) = "���ƦW��"
        '        exsheet.Cells(1, 4) = "�W��"
        '        exsheet.Cells(1, 5) = "�J�w�ƶq"
        '        exsheet.Cells(1, 6) = "���"
        '        exsheet.Cells(1, 7) = "�妸"
        '        exsheet.Cells(1, 8) = "�ާ@��"
        '        exsheet.Cells(1, 9) = "�f��"
        '        exsheet.Cells(1, 10) = "�J�w���"
        '        exsheet.Cells(1, 11) = "�f�ֳƵ�"


        '        For i = 0 To ds.Tables(0).Rows.Count - 1
        '            ii = i + 2
        '            exsheet.Cells(ii, 1) = ds.Tables(0).Rows(i)("WIP_ID")
        '            exsheet.Cells(ii, 2) = "#" & ds.Tables(0).Rows(i)("M_Code") & "#"
        '            exsheet.Cells(ii, 3) = ds.Tables(0).Rows(i)("M_Name")
        '            exsheet.Cells(ii, 4) = ds.Tables(0).Rows(i)("M_Gauge")
        '            exsheet.Cells(ii, 5) = ds.Tables(0).Rows(i)("WIP_Qty")
        '            exsheet.Cells(ii, 6) = ds.Tables(0).Rows(i)("M_Unit")
        '            exsheet.Cells(ii, 7) = ds.Tables(0).Rows(i)("OS_BatchID")
        '            exsheet.Cells(ii, 8) = ds.Tables(0).Rows(i)("WIP_ActionName")
        '            exsheet.Cells(ii, 9) = ds.Tables(0).Rows(i)("WIP_Check")
        '            exsheet.Cells(ii, 10) = CDate(ds.Tables(0).Rows(i)("WIP_AddDate"))
        '            exsheet.Cells(ii, 11) = ds.Tables(0).Rows(i)("WIP_CheckRemark")
        '        Next
        '        exapp.Visible = True
        '    Else
        '        MsgBox("�S���ƾ�!")
        '    End If


        Me.Close()
    End Sub
End Class