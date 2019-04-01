
Imports LFERP.SystemManager
Imports LFERP.Library.MaterialParam
Imports LFERP.Library.Material
Imports LFERP.Library
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports LFERP.FileManager

Public Class FrmPurchaseReport
    Dim mtc As New Material.MaterialTypeController


    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub FrmPurchaseReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mtd As New LFERP.DataSetting.SuppliersControler


        gluSupplier.Properties.DisplayMember = "S_SupplierName"
        gluSupplier.Properties.ValueMember = "S_Supplier"
        gluSupplier.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")
        mtc.LoadNodes(tv1, ErpUser.MaterialType)
        DateEdit1.EditValue = Now
        DateEdit2.EditValue = Now


    End Sub

    Private Sub tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv1.AfterSelect
        PopupContainerEdit1.EditValue = tv1.SelectedNode.Tag
        PopupContainerControl1.OwnerEdit.ClosePopup()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim ds As New DataSet
        'If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
        Dim mcsysuser As New LFERP.SystemManager.SystemUser.SystemUserController
        Dim mctarriff As New LFERP.DataSetting.TarriffController
        Dim mcunit As New LFERP.DataSetting.UnitController

        '***�P�_�Τ�O�֦��S�������v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim tesuvalue As String
        Dim temp1, temp2, temp3, temp4 As String


        temp1 = Nothing
        temp2 = Nothing
        temp3 = Nothing
        temp4 = Nothing
        tesuvalue = "�_"

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        If pmwiL.Item(0).PMWS_Value = "�O" Then
            tesuvalue = "�O"
        End If

        '************

        ds.Tables.Clear()

        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ' ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_GetReportList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc5.CollToDataSet(ds, "Unit", mcunit.GetUnitList(Nothing))

        If Len(PopupContainerEdit1.EditValue) <> 0 Then temp1 = PopupContainerEdit1.EditValue
        If Len(gluSupplier.EditValue) <> 0 Then temp2 = gluSupplier.EditValue
        If CheckBox1.Checked = True Then
            temp3 = Format(DateEdit1.EditValue, "yyyy/MM/dd")
            temp4 = Format(DateEdit2.EditValue, "yyyy/MM/dd")
        End If

        ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_GetReportList(Nothing, temp1, temp3, temp4, Nothing, Nothing, temp2, Nothing, Nothing, "1", tesuvalue))

        'If RadioButton1.Checked = True Then
        '    ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_GetReportList(Nothing, PopupContainerEdit1.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tesuvalue))
        'End If

        'If RadioButton2.Checked = True Then
        '    ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_GetReportList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, gluSupplier.EditValue, Nothing, Nothing, tesuvalue))

        'End If
        'If RadioButton3.Checked = True Then
        '    ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_GetReportList(Nothing, Nothing, Format(DateEdit1.EditValue, "yyyy/MM/dd"), Format(DateEdit2.EditValue, "yyyy/MM/dd"), Nothing, Nothing, Nothing, Nothing, Nothing, tesuvalue))
        'End If


        ltc3.CollToDataSet(ds, "SystemUser", mcsysuser.SystemUser_GetList(Nothing, Nothing, Nothing))
        ltc4.CollToDataSet(ds, "Tarriff", mctarriff.TarriffGetList(Nothing))

        PreviewRPT(ds, "Purchaseschedule", " ���ʳ�--", True, True)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing


    End Sub

    Private Sub PopupContainerEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopupContainerEdit1.EditValueChanged

    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim ds As New DataSet
        'If GridView2.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController

        '***�P�_�Τ�O�֦��S�������v��
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim tesuvalue As String
        Dim temp1, temp2, temp3, temp4 As String
        temp1 = Nothing
        temp2 = Nothing
        temp3 = Nothing
        temp4 = Nothing
        tesuvalue = "�_"
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
        If pmwiL.Item(0).PMWS_Value = "�O" Then
            tesuvalue = "�O"
        End If

        ds.Tables.Clear()
        If Len(PopupContainerEdit1.EditValue) <> 0 Then temp1 = PopupContainerEdit1.EditValue
        If Len(gluSupplier.EditValue) <> 0 Then temp2 = gluSupplier.EditValue
        If CheckBox1.Checked = True Then
            temp3 = Format(DateEdit1.EditValue, "yyyy/MM/dd")
            temp4 = Format(DateEdit2.EditValue, "yyyy/MM/dd")
        End If

        ltc.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_GetReportList(Nothing, temp1, temp3, temp4, Nothing, Nothing, temp2, Nothing, Nothing, "1", tesuvalue))

        Dim i As Integer
        Dim ii As Integer
        Dim exapp As New Excel.Application   '�w�q�@��excel��H
        Dim exbook As Excel.Workbook     '�w�q�@��excel����
        Dim exsheet As Excel.Worksheet   '�w�q�@��excel�u�@��

        i = 0
        ii = 0
        exapp = CreateObject("Excel.Application")   '�ͦ��@��excel��H

        exbook = exapp.Workbooks.Add
        exsheet = exbook.Worksheets(1)


        exsheet.Cells(1, 1) = "���ʳ渹"
        exsheet.Cells(1, 2) = "�妸"
        exsheet.Cells(1, 3) = "�p�׽s��"
        exsheet.Cells(1, 4) = "������"
        exsheet.Cells(1, 5) = "�����ӽs��"
        exsheet.Cells(1, 6) = "���ƦW��"
        exsheet.Cells(1, 7) = "���ƳW��"
        exsheet.Cells(1, 8) = "���ʤ��"
        exsheet.Cells(1, 9) = "�n�D���"
        exsheet.Cells(1, 10) = "�_�^���"
        exsheet.Cells(1, 11) = "�q����"
        exsheet.Cells(1, 12) = "�ƶq"
        exsheet.Cells(1, 13) = "�����"
        exsheet.Cells(1, 14) = "�ƪ`"

        For i = 0 To ds.Tables("PurchaseMain").Rows.Count - 1
            'ds.Tables("Quotation").Rows(i)("Q_ID") = i + 1
            'If ds.Tables("PurchaseMain").Rows(i)("PS_Price") <= 0 Then '����ܥ����>0��

            '    GoTo txt3
            'Else
            '    ii = i + 2
            'End If

            ii = i + 2


            exsheet.Cells(ii, 1) = ds.Tables("PurchaseMain").Rows(i)("PM_NO")
            exsheet.Cells(ii, 2) = ds.Tables("PurchaseMain").Rows(i)("OS_BatchID")
            exsheet.Cells(ii, 3) = ds.Tables("PurchaseMain").Rows(i)("PM_M_Code")
            exsheet.Cells(ii, 4) = ds.Tables("PurchaseMain").Rows(i)("S_SupplierName")
            exsheet.Cells(ii, 5) = ds.Tables("PurchaseMain").Rows(i)("S_SupplierNo")
            exsheet.Cells(ii, 6) = ds.Tables("PurchaseMain").Rows(i)("M_Name")
            exsheet.Cells(ii, 7) = ds.Tables("PurchaseMain").Rows(i)("M_Gauge")
            exsheet.Cells(ii, 8) = CDate(ds.Tables("PurchaseMain").Rows(i)("PM_PurchaseDate"))
            exsheet.Cells(ii, 9) = CDate(ds.Tables("PurchaseMain").Rows(i)("PS_SendDate").ToString)
            exsheet.Cells(ii, 10) = ds.Tables("PurchaseMain").Rows(i)("PSs_Date")
            exsheet.Cells(ii, 11) = ds.Tables("PurchaseMain").Rows(i)("PM_OS_SendDate")
            exsheet.Cells(ii, 12) = ds.Tables("PurchaseMain").Rows(i)("PS_Qty")
            exsheet.Cells(ii, 13) = ds.Tables("PurchaseMain").Rows(i)("PS_NoSendQty")
            exsheet.Cells(ii, 14) = ds.Tables("PurchaseMain").Rows(i)("PSs_Remark")


        Next
        exapp.Visible = True

        'exsheet.Visible = Excel.XlSheetVisibility.xlSheetVisible

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            DateEdit1.Enabled = True
            DateEdit2.Enabled = True
            DateEdit1.Focus()
        Else
            DateEdit1.Enabled = False
            DateEdit2.Enabled = False
        End If
    End Sub
End Class