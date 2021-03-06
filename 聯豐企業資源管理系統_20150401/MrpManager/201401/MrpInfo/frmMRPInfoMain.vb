Imports LFERP.SystemManager
Imports LFERP.Library.MrpManager.MrpInfo
Imports LFERP.Library.MrpManager.MrpSetting
Imports System.Threading

Public Class frmMRPInfoMain
#Region "属性"
    Dim frm As frmMRPInfo
    Dim micon As New MrpInfoController
    Dim mdcon As New MrpDestBillsController
    Dim mocon As New MRPOrderDestBillsController
    Dim mscon As New MrpSettingController
    Dim mrcon As New MrpIndReqController
    Dim mpcon As New MrpPurchaseController
    Dim mccon As New MrpPurchaseCalcRecordController

    Dim threadIndReq As Thread
    Dim threadDestBills As Thread
    Dim threadOrderDestBills As Thread
    Dim threadPurchase As Thread
    Dim threadPurchaseCalcRecord As Thread

    Dim MRPID As String
    Delegate Sub DelegateSetDataSource(ByVal dataSource As Object, ByVal control As Object)
    Delegate Sub DelegateSetPictureBox()

#End Region

#Region "窗体载入"
    Private Sub frmMRPMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserPower()
        pbIndreq.BringToFront()
    End Sub
#End Region

#Region "用戶權限加載"
    Sub UserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480501")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmsMRPAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480502")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmsMRPEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480503")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmsMRPDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480504")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmsMRPCheck.Enabled = True
        End If
    End Sub
#End Region

#Region "右擊菜單事件"
    Private Sub cmsMRPAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPAdd.Click
        frm = New frmMRPInfo
        frm.EditItem = "MRPAdd"
        frm.MdiParent = MDIMain
        frm.WindowState = FormWindowState.Maximized
        frm.Show()
    End Sub

    Private Sub cmsMRPEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPEdit.Click
        Dim mi As New MRPInfoInfo
        mi = micon.MrpInfo_GetList(GridView1.GetFocusedRowCellValue("MRP_ID").ToString, Nothing, Nothing, Nothing, Nothing)(0)
        frm = New frmMRPInfo
        frm.EditItem = "MRPEdit"
        frm.MRPID = GridView1.GetFocusedRowCellValue("MRP_ID").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsMRPDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPDel.Click
        Dim bo As Boolean
        If MsgBox("確認刪除此運算記錄", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.Yes Then
            Dim MRP_ID As String = GridView1.GetFocusedRowCellValue("MRP_ID").ToString
            Dim mi As New MRPInfoInfo
            Dim mpc As New MrpPurchaseController
            bo = micon.MrpInfo_Delete(MRP_ID)
            bo = bo And mdcon.MrpDestBills_Delete(Nothing, MRP_ID)
            bo = bo And mocon.MRPOrderDestBills_Delete(Nothing, MRP_ID)
            bo = bo And mpc.MrpPurchase_Delete(Nothing, MRP_ID)
            If bo = True Then
                GridView1.DeleteRow(GridView1.FocusedRowHandle)
                MsgBox("刪除成功", MsgBoxStyle.Information, "提示")
            Else
                MsgBox("刪除失敗", MsgBoxStyle.Information, "提示")
            End If
        End If

    End Sub

    Private Sub cmsMRPWatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPWatch.Click
        frm = New frmMRPInfo
        frm.EditItem = "MRPWatch"
        frm.MRPID = GridView1.GetFocusedRowCellValue("MRP_ID").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsMRPCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPCheck.Click
        frm = New frmMRPInfo
        frm.EditItem = "MRPCheck"
        frm.MRPID = GridView1.GetFocusedRowCellValue("MRP_ID").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsMRPRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPRefresh.Click
        Try
            Dim msi As New List(Of MrpSettingInfo)
            msi = mscon.MrpSetting_GetList(InUserID)
            If msi.Count > 0 Then
                grid1.DataSource = micon.MrpInfo_GetList(Nothing, msi(0).mrpInfoBeginDate, msi(0).mrpInfoCheckType, msi(0).mrpInfoMrpType, msi(0).mrpInfoCreateUserID)
            Else
                grid1.DataSource = micon.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
            GridView1_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "cmsMRPRefresh_Click方法出錯")
        End Try

    End Sub

#Region "設置右擊菜單項是否可用"
    Private Sub SetRightClickMenuEnable()
        Dim mi As New MRPInfoInfo
        mi = micon.MrpInfo_GetList(GridView1.GetFocusedRowCellValue("MRP_ID"), Nothing, Nothing, Nothing, Nothing)(0)
        Try
            Dim c As ToolStripItem
            If GridView1.DataRowCount < 1 Then
                For Each c In cmsMRP.Items
                    If (c.Name = "cmsMRPAdd" Or c.Name = "cmsMRPRefresh") Then
                        c.Enabled = True
                    Else
                        c.Enabled = False
                    End If
                Next
            ElseIf mi.MI_CheckBit.Equals(True) Then
                For Each c In cmsMRP.Items
                    If (c.Name = "cmsMRPEdit" Or c.Name = "cmsMRPDel") Then
                        c.Enabled = False
                    Else
                        c.Enabled = True
                    End If
                Next
            Else
                For Each c In cmsMRP.Items
                    c.Enabled = True
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "SetRightClickMenuEnable方法出錯")
        End Try
    End Sub
#End Region

#End Region

#Region "對Grid中的審核日期和運算類型設置顯示格式"
    Private Sub GridView1_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView1.CustomColumnDisplayText
        Try
            '----------------當訂單開始日期為空時，則不顯示----------------
            If e.Column.FieldName = "MI_NeedBeginDate" Then
                If e.Value = Nothing Then e.DisplayText = ""
            End If
            '----------------當訂單結束日期為空時，則不顯示----------------
            If e.Column.FieldName = "MI_NeedEndDate" Then
                If e.Value = Nothing Then e.DisplayText = ""
            End If
            '----------------當審核日期為空時，則不顯示----------------
            If e.Column.FieldName = "MI_CheckDate" Then
                If e.Value = Nothing Then e.DisplayText = ""
            End If
            '----------------當運算日期為空時，則不顯示----------------
            If e.Column.FieldName = "MI_MRPDate" Then
                If e.Value = Nothing Then e.DisplayText = ""
            End If
            '----------------當類型為1時顯示正式，否則顯示模擬----------------
            If e.Column.FieldName = "MI_MRPType" Then
                If e.Value = 1 Then
                    e.DisplayText = "正式"
                Else
                    e.DisplayText = "模擬"
                End If
            End If
            '----------------當運算條件為1時則顯示按日期運算，否則顯示按單號運算----------------
            If e.Column.FieldName = "MI_CalcType" Then
                If e.Value = 1 Then
                    e.DisplayText = "按日期運算"
                Else
                    e.DisplayText = "按單號運算"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "GridView1_CustomColumnDisplayText方法出錯")
        End Try
    End Sub
#End Region

#Region "表格的_FocusedRowChanged事件"
    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            SetRightClickMenuEnable()
            If GridView1.RowCount < 1 Then
                TreeList1.DataSource = Nothing
                grid2.DataSource = Nothing
                grid3.DataSource = Nothing
                grid4.DataSource = Nothing
                Exit Sub
            End If

            MRPID = IIf(GridView1.GetFocusedRowCellValue("MRP_ID") = Nothing, "", GridView1.GetFocusedRowCellValue("MRP_ID").ToString)

            pbIndreq.Visible = True
            If threadIndReq Is Nothing = False Then
                threadIndReq.Abort()
            End If
            If threadDestBills Is Nothing = False Then
                threadDestBills.Abort()
            End If
            If threadOrderDestBills Is Nothing = False Then
                threadOrderDestBills.Abort()
            End If
            If threadPurchase Is Nothing = False Then
                threadPurchase.Abort()
            End If
            If threadPurchaseCalcRecord Is Nothing = False Then
                threadPurchaseCalcRecord.Abort()
            End If

            threadIndReq = New Thread(AddressOf LoadIndReq)
            threadIndReq.Start()

            threadDestBills = New Thread(AddressOf LoadDestBills)
            threadDestBills.Start()

            threadOrderDestBills = New Thread(AddressOf LoadOrderDestBills)
            threadOrderDestBills.Start()

            threadPurchase = New Thread(AddressOf LoadPurchase)
            threadPurchase.Start()

            threadPurchaseCalcRecord = New Thread(AddressOf LoadPurchaseCalcRecord)
            threadPurchaseCalcRecord.Start()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "GridView1_FocusedRowChanged方法出錯")
        End Try
    End Sub
    '-----------------MRP獨立需求--------------------------
    Private Sub LoadIndReq()
        Try
            Dim mii As New List(Of MrpIndReqInfo)
            mii = mrcon.MrpIndReq_GetList(MRPID)
            Dim s As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Dim p As New DelegateSetPictureBox(AddressOf SetPictureBox)
            Me.Invoke(s, mii, TreeList1)
            pbIndreq.Invoke(p)
            threadIndReq.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "LoadIndReq方法出錯")
            End If
        End Try
    End Sub
    '-----------------MRP產品明細表-----------------------
    Private Sub LoadDestBills()
        Try
            Dim mbi As New List(Of MrpDestBillsInfo)
            mbi = mdcon.MrpDestBills_GetList(MRPID)
            Dim s As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(s, mbi, grid2)
            threadDestBills.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "LoadDestBills方法出錯")
            End If
        End Try
    End Sub
    '-----------------MRP訂單物料明細表--------------------
    Private Sub LoadOrderDestBills()
        Try
            Dim mobi As New List(Of MRPOrderDestBillsInfo)
            mobi = mocon.MRPOrderDestBills_GetList(MRPID)
            Dim s As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(s, mobi, grid3)
            threadOrderDestBills.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "LoadOrderDestBills方法出錯")
            End If
        End Try
    End Sub
    '-----------------MRP請購建議表--------------------------
    Private Sub LoadPurchase()
        Try
            Dim mpi As New List(Of MrpPurchaseInfo)
            mpi = mpcon.MrpPurchase_GetList(MRPID)
            Dim s As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(s, mpi, grid4)
            threadPurchase.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "LoadPurchase方法出錯")
            End If
        End Try
    End Sub
    '-----------------MRP運算請購建議記錄表--------------------------
    Private Sub LoadPurchaseCalcRecord()
        Try
            Dim mpcri As New List(Of MrpPurchaseCalcRecordInfo)
            mpcri = mccon.MrpPurchaseCalcRecord_GetList(MRPID)

            Dim s As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(s, mpcri, Grid5)

            threadPurchaseCalcRecord.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "LoadPurchaseCalcRecord方法出錯")
            End If
        End Try
    End Sub

    Private Sub SetControlDataSource(ByVal dataSource As Object, ByVal control As Object)
        Try
            control.DataSource = dataSource
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "SetControlDataSource方法錯誤")
        End Try
    End Sub

    Private Sub SetPictureBox()
        Try
            pbIndreq.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "SetControlDataSource方法出錯")
        End Try
    End Sub

#End Region

#Region "列印事件"
    Private Sub tsm_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_Print.Click
        Dim dss As New DataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet

        Dim mpc As New MrpPurchaseController
        Dim MRPID As String
        MRPID = IIf(GridView1.GetFocusedRowCellValue("MRP_ID") = Nothing, "", GridView1.GetFocusedRowCellValue("MRP_ID").ToString)
        ltc1.CollToDataSet(dss, "MrpInfo", micon.MrpInfo_GetList(MRPID, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(dss, "MrpPurchase", mpc.MrpPurchase_GetList(MRPID))
        PreviewRPT(dss, "rptMrpInfo", "表", True, True)
        ltc1 = Nothing
        ltc2 = Nothing
    End Sub
#End Region

#Region "窗体返回载入刷新事件"
    Private Sub frmMRPInfoMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        cmsMRPRefresh_Click(Nothing, Nothing)
    End Sub
#End Region

#Region "鍵盤快捷鍵事件"
    Private Sub grid1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grid1.KeyUp
        If e.KeyCode = Keys.Delete Then
            cmsMRPDel_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "失效日期為空時，設置其顯示文本為空"
    Private Sub TreeList1_GetNodeDisplayValue(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.GetNodeDisplayValueEventArgs) Handles TreeList1.GetNodeDisplayValue
        Try
            If e.Column.Name = "InvalidDate" Then
                If e.Value = Nothing Then
                    e.Value = ""
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "TreeList1_GetNodeDisplayValue方法出錯")
        End Try
    End Sub
#End Region

End Class
