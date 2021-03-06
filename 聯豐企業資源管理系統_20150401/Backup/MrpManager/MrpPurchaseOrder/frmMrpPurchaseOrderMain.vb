Imports LFERP.Library.MrpManager.MrpPurchaseOrder
Imports LFERP.Library.MrpManager.MrpSetting
Imports LFERP.Library.MrpManager.MrpSelect
Imports LFERP.SystemManager

Public Class frmMrpPurchaseOrderMain
#Region "属性"
    Dim frm As frmMrpPurchaseOrder
    Dim mpoc As New MrpPurchaseOrderController
    Dim mpoec As New MrpPurchaseOrderEntryController
    Dim msc As New MrpSettingController

#End Region

#Region "窗體加載"
    Private Sub frmMrpPurchaseOrderMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        cmsPurOrderRefresh_Click(Nothing, Nothing)
    End Sub
#End Region

#Region "右擊菜單事件"
    Private Sub cmsPurOrderAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderAdd.Click
        frm = New frmMrpPurchaseOrder
        frm.EditItem = EditEnumType.ADD
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsPurOrderEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderEdit.Click
        frm = New frmMrpPurchaseOrder
        frm.EditItem = EditEnumType.EDIT
        frm.PO = GridView1.GetFocusedRowCellValue("PO").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsPurOrderDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderDel.Click
        If MsgBox("是否確定刪除采購單號：" + GridView1.GetFocusedRowCellValue("PO").ToString, MsgBoxStyle.YesNo, "提示") = MsgBoxResult.Yes Then
            Dim PO As String = GridView1.GetFocusedRowCellValue("PO").ToString
            mpoc.MrpPurchaseOrder_Delete(PO)
            mpoec.MrpPurchaseOrderEntry_Delete(Nothing, PO)
            GridView1.DeleteRow(GridView1.FocusedRowHandle)
            MsgBox("刪除成功", MsgBoxStyle.Information, "提示")
        End If
    End Sub

    Private Sub cmsPurOrderView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderView.Click
        frm = New frmMrpPurchaseOrder
        frm.EditItem = EditEnumType.VIEW
        frm.PO = GridView1.GetFocusedRowCellValue("PO").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsPurOrderRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderRefresh.Click
        Try
            Dim msi As New List(Of MrpSettingInfo)
            msi = msc.MrpSetting_GetList(InUserID)
            If msi.Count > 0 Then
                grid1.DataSource = mpoc.MrpPurchaseOrder_GetList(Nothing, msi(0).purOrderBeginDate, Nothing, Nothing, msi(0).purOrderRequestDept, msi(0).purOrderCreateUserID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, msi(0).purOrderDisplayNum)
            Else
                grid1.DataSource = mpoc.MrpPurchaseOrder_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
            GridView1_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "cmsPurOrderRefresh_Click方法出錯")
        End Try

    End Sub

    Private Sub cmsPurOrderCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderCheck.Click
        frm = New frmMrpPurchaseOrder
        frm.EditItem = EditEnumType.CHECK
        frm.PO = GridView1.GetFocusedRowCellValue("PO").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsPurOrderReCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderReCheck.Click
        frm = New frmMrpPurchaseOrder
        frm.EditItem = EditEnumType.RECHECK
        frm.PO = GridView1.GetFocusedRowCellValue("PO").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsPurOrderSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderSelect.Click
        'Dim fr As New frmSelect
        ''fr.FormText = "MRP采購單"
        ''fr.TableName = "MrpPurchaseOrder"
        ''fr.ID = "PO"
        'fr.ShowDialog()
        'Dim sc As New Select_Controller
        'If String.IsNullOrEmpty(tempValue) = False Then
        '    grid1.DataSource = sc.MrpPurchaseOrder_GetList(tempValue)
        'End If
    End Sub

    Private Sub cmsPurOrderPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderPrint.Click

        'Dim dss As New DataSet
        'Dim ltc1 As New CollectionToDataSet
        'Dim ltc2 As New CollectionToDataSet
        'Dim PO As String
        'PO = IIf(GridView1.GetFocusedRowCellValue("PO") = Nothing, "", GridView1.GetFocusedRowCellValue("PO").ToString)
        'ltc1.CollToDataSet(dss, "MrpPurchaseOrder", mpoc.MrpPurchaseOrder_GetList(PO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        'Dim StrSend As String = String.Empty
        'StrSend = InUser
        'ltc2.CollToDataSet(dss, "MrpPurchaseOrderEntry", mpoec.MrpPurchaseOrderEntry_GetList(PO))
        'PreviewRPT1(dss, "rptMrpPurchaseOrder", "采購單", StrSend, StrSend, True, True)

        'ltc1 = Nothing
        'ltc2 = Nothing

    End Sub

#Region "設置右擊菜單項是否可用"
    Private Sub grid1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grid1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            SetRightClickMenuEnable()
        End If
    End Sub

    Private Sub SetRightClickMenuEnable()
        Dim mpoi As New MrpPurchaseOrderInfo
        Dim mpoiList As New List(Of MrpPurchaseOrderInfo)
        If GridView1.FocusedRowHandle >= 0 Then
            mpoiList = mpoc.MrpPurchaseOrder_GetList(GridView1.GetFocusedRowCellValue("PO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If mpoiList.Count > 0 Then
                mpoi = mpoiList(0)
            Else
                MsgBox(GridView1.GetFocusedRowCellValue("PO") + "的采購單號已被其他用戶刪除", MsgBoxStyle.Information, "提示")
                cmsPurOrderRefresh_Click(Nothing, Nothing)
                Exit Sub
            End If

        End If
        Try
            Dim c As ToolStripItem
            If GridView1.FocusedRowHandle < 0 Then
                For Each c In cmsPurOrder.Items
                    If (c.Name = "cmsPurOrderAdd" Or c.Name = "cmsPurOrderRefresh") Then
                        c.Enabled = True
                    Else
                        c.Enabled = False
                    End If
                Next
            ElseIf mpoi.ReCheckBit.Equals(True) Then
                For Each c In cmsPurOrder.Items
                    If (c.Name = "cmsPurOrderEdit" Or c.Name = "cmsPurOrderDel" Or c.Name = "cmsPurOrderCheck") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            ElseIf mpoi.CheckBit.Equals(True) Then
                For Each c In cmsPurOrder.Items
                    If (c.Name = "cmsPurOrderEdit" Or c.Name = "cmsPurOrderDel") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            ElseIf mpoi.CheckBit.Equals(False) Then
                For Each c In cmsPurOrder.Items
                    If (c.Name = "cmsPurOrderReCheck") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            Else
                For Each c In cmsPurOrder.Items
                    c.Enabled = True
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "SetRightClickMenuEnable方法出錯")
        End Try
    End Sub
#End Region

#End Region

#Region "表格的_FocusedRowChanged事件"
    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            SetRightClickMenuEnable()
            Dim PO As String = String.Empty
            If GridView1.FocusedRowHandle >= 0 Then
                PO = IIf(GridView1.GetFocusedRowCellValue("PO") = Nothing, "", GridView1.GetFocusedRowCellValue("PO").ToString)
                GridControl1.DataSource = mpoec.MrpPurchaseOrderEntry_GetList(PO)
            Else
                GridControl1.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "GridView1_FocusedRowChanged方法出錯")
        End Try
    End Sub
#End Region

#Region "設置合計數據"
    Private Sub viewPurchaseOrder_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles viewPurchaseOrder.CustomColumnDisplayText
        If e.Column.FieldName = "TotalPrice" Then
            e.DisplayText = viewPurchaseOrder.GetRowCellValue(e.RowHandle, "UnitPrice") * viewPurchaseOrder.GetRowCellValue(e.RowHandle, "PurchaseQty")
        End If
    End Sub
#End Region

    Private Sub tsm_PrintMrpPurchaseOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_PrintMrpPurchaseOrder.Click
        On Error Resume Next
        Dim fr As MrpReportSelect
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is MrpReportSelect Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New MrpReportSelect
        fr.intShowPage = 6
        'fr.MdiParent = MDIMain
        'fr.WindowState = FormWindowState.Maximized

        fr.ShowDialog()
        fr.Focus()
    End Sub

#Region "導出Excel"
    Private Sub cmsExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsExcel.Click, cmsSubExcel.Click
        If sender.Owner Is cmsPurOrder Then
            ConrotlExportExcel(grid1)
        Else
            ConrotlExportExcel(GridControl1)
        End If
    End Sub
#End Region
#Region "設置權限"
    '設置權限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480801")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsPurOrderAdd.Visible = True
                cmsPurOrderAdd.Enabled = True
            End If

        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480802")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsPurOrderEdit.Visible = True
                cmsPurOrderEdit.Enabled = True
            End If

        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480803") '審核
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsPurOrderDel.Visible = True
                cmsPurOrderDel.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480804") '確認審核
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsPurOrderCheck.Visible = True
                cmsPurOrderCheck.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480805") '供應商
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsPurOrderReCheck.Visible = True
                cmsPurOrderReCheck.Enabled = True
            End If

        End If
    End Sub
#End Region
End Class