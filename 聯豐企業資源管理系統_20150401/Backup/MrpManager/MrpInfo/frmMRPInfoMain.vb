Imports LFERP.SystemManager
Imports LFERP.Library.MrpManager.MrpInfo
Imports LFERP.Library.MrpManager.MrpSetting
Imports LFERP.Library.MrpManager.MrpSelect

Imports System.Threading
Imports DevExpress.XtraTreeList.Nodes

Public Class frmMrpInfoMain

#Region "属性"
    Dim frm As frmMrpInfo
    Dim micon As New MrpInfoController
    Dim mdcon As New MrpDestBillsController
    Dim mocon As New MrpOrderDestBillsController
    Dim mscon As New MrpSettingController
    Dim mrcon As New MrpIndReqController
    Dim mpcon As New MrpPurchaseController
    Dim mccon As New MrpPurchaseCalcRecordController
    Dim sc As New Select_Controller

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
        cmsMRPRefresh_Click(Nothing, Nothing)
        pbIndreq.BringToFront()
    End Sub
#End Region

#Region "用戶權限加載"
    Sub UserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480501")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsMRPAdd.Visible = True
                cmsMRPAdd.Enabled = True
            End If

        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480502")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsMRPEdit.Visible = True
                cmsMRPEdit.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480503")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsMRPDel.Visible = True
                cmsMRPDel.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "480504")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                cmsMRPCheck.Visible = True
                cmsMRPCheck.Enabled = True
            End If
        End If
    End Sub
#End Region

#Region "右擊菜單事件"
    Private Sub cmsMRPAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPAdd.Click
        frm = New frmMrpInfo
        frm.EditItem = EditEnumType.ADD
        frm.MdiParent = MDIMain
        frm.WindowState = FormWindowState.Maximized
        frm.Show()
    End Sub

    Private Sub cmsMRPEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPEdit.Click
        Dim mi As New MrpInfoInfo
        mi = micon.MrpInfo_GetList(GridView1.GetFocusedRowCellValue("MRP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)(0)
        If mi.MI_CheckBit.Equals(True) Then
            MsgBox("此資料行已審核，無法修改", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        frm = New frmMrpInfo
        frm.EditItem = EditEnumType.EDIT
        frm.MRPID = GridView1.GetFocusedRowCellValue("MRP_ID").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsMRPDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPDel.Click
 
        Dim bo As Boolean
        If MsgBox("是否確定刪除運算單號：" + GridView1.GetFocusedRowCellValue("MRP_ID").ToString, MsgBoxStyle.YesNo, "提示") = MsgBoxResult.Yes Then
            If micon.MrpInfo_PreDelete(GridView1.GetFocusedRowCellValue("MRP_ID").ToString) = False Then
                Exit Sub
            End If
            Dim MRP_ID As String = GridView1.GetFocusedRowCellValue("MRP_ID").ToString
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
        frm = New frmMrpInfo
        frm.EditItem = EditEnumType.VIEW
        frm.MRPID = GridView1.GetFocusedRowCellValue("MRP_ID").ToString
        frm.WindowState = FormWindowState.Maximized
        frm.MdiParent = MDIMain
        frm.Show()
    End Sub

    Private Sub cmsMRPSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPSelect.Click
        'Dim fr As New frmSelect
        ''fr.FormText = "MRP物料需求運算"
        ''fr.TableName = "MrpInfo"
        ''fr.ID = "MRP_ID"
        'fr.ShowDialog()
        'If String.IsNullOrEmpty(tempValue) = False Then
        '    grid1.DataSource = sc.MrpInfo_GetList(tempValue)
        'End If
 
    End Sub

    Private Sub cmsMRPCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPCheck.Click
        frm = New frmMrpInfo
        frm.EditItem = EditEnumType.CHECK
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
                grid1.DataSource = micon.MrpInfo_GetList(Nothing, msi(0).mrpInfoBeginDate, msi(0).mrpInfoCheckType, msi(0).mrpInfoMrpType, msi(0).mrpInfoCreateUserID, Nothing, Nothing, Nothing, Nothing, msi(0).mrpInfoDisplayNum)
            Else
                grid1.DataSource = micon.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
            GridView1_FocusedRowChanged(Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "cmsMRPRefresh_Click方法出錯")
        End Try

    End Sub

#Region "列印事件"
    Private Sub ReportPrint(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrintPurchase.Click, cmsPrintDestBills.Click, cmsPrintOrderBills.Click, cmsPrintIndReq.Click
        Dim dss As New DataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim MRPID As String
        MRPID = IIf(GridView1.GetFocusedRowCellValue("MRP_ID") = Nothing, "", GridView1.GetFocusedRowCellValue("MRP_ID").ToString)
        ltc1.CollToDataSet(dss, "MrpInfo", micon.MrpInfo_GetList(MRPID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        Select Case sender.name
            Case "cmsPrintPurchase"
                XtraTabControl1.SelectedTabPage = xtpPurchase
                Dim StrSend As String = String.Empty
                StrSend = InUser
                ltc2.CollToDataSet(dss, "MrpPurchase", mpcon.MrpPurchase_GetList(MRPID))
                PreviewRPT1(dss, "rptMrpInfo", "請購建議表", StrSend, StrSend, True, True)
            Case "cmsPrintDestBills"
                XtraTabControl1.SelectedTabPage = xtpDestBills
                ltc2.CollToDataSet(dss, "MrpDestBills", mdcon.MrpDestBills_GetList(MRPID))
                PreviewRPT1(dss, "rptMrpDestBills", "產品明細表", InUser, InUser, True, True)
            Case "cmsPrintOrderBills"
                XtraTabControl1.SelectedTabPage = xtpOrderBills
                ltc2.CollToDataSet(dss, "MrpOrderDestBills", mocon.MRPOrderDestBills_GetList(MRPID))
                PreviewRPT1(dss, "rptMrpOrderDestBills", "訂單物料明細表", InUser, InUser, True, True)
            Case "cmsPrintIndReq"
                XtraTabControl1.SelectedTabPage = xtpIndReq
                ltc2.CollToDataSet(dss, "MrpIndReq", GetTreeListTable)
                PreviewRPT1(dss, "rptMrpIndReq", "獨立需求表", InUser, InUser, True, True)
        End Select
        ltc1 = Nothing
        ltc2 = Nothing
    End Sub

#Region "將Treelist轉化為list"
    Private Function GetTreeListTable()
        Dim milist As New List(Of MrpIndReqInfo)
        For Each node As TreeListNode In TreeList1.Nodes
            TraverseNodes(node, milist)
        Next
        Return milist
    End Function

    Private Sub TraverseNodes(ByVal p_node As TreeListNode, ByVal milist As List(Of MrpIndReqInfo))
        Dim tempNode As TreeListNode
        NodeValueToList(p_node, milist)
        For Each tempNode In p_node.Nodes
            NodeValueToList(tempNode, milist)
            If tempNode.HasChildren = True Then
                TraverseNodes(tempNode, milist)
            End If
        Next
    End Sub

    Private Sub NodeValueToList(ByVal tempNode As TreeListNode, ByVal milist As List(Of MrpIndReqInfo))
        Dim mii As New MrpIndReqInfo
        mii.ForecastID = tempNode.GetValue("ForecastID")
        mii.CustomerName = tempNode.GetValue("CustomerName")
        mii.M_Code = tempNode.GetValue("M_Code")
        mii.M_Name = tempNode.GetValue("M_Name")
        mii.M_Gauge = tempNode.GetValue("M_Gauge")
        mii.M_Unit = tempNode.GetValue("M_Unit")
        mii.Source = tempNode.GetValue("Source")
        mii.NeedQty = tempNode.GetValue("NeedQty")
        mii.NeedDate = tempNode.GetValue("NeedDate")
        milist.Add(mii)
    End Sub
#End Region
#End Region

#Region "設置右擊菜單項是否可用"
    Private Sub grid1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grid1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            SetRightClickMenuEnable()
        End If
    End Sub
    Private Sub SetRightClickMenuEnable()
        Dim mi As New MrpInfoInfo
        Dim miList As New List(Of MrpInfoInfo)
        If GridView1.FocusedRowHandle >= 0 Then
            miList = micon.MrpInfo_GetList(GridView1.GetFocusedRowCellValue("MRP_ID"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If miList.Count > 0 Then
                mi = miList(0)
            Else
                MsgBox(GridView1.GetFocusedRowCellValue("MRP_ID") + "的運算單號已被其他用戶刪除", MsgBoxStyle.Information, "提示")
                cmsMRPRefresh_Click(Nothing, Nothing)
                Exit Sub
            End If

        End If
        Try
            Dim c As ToolStripItem
            If GridView1.FocusedRowHandle < 0 Then
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
            ElseIf mi.MI_MRPType.Equals("0") Then
                For Each c In cmsMRP.Items
                    If (c.Name = "cmsMRPCheck") Then
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
                If e.Value Is Nothing Then
                ElseIf e.Value = 1 Then
                    e.DisplayText = "正式"
                ElseIf e.Value = 0 Then
                    e.DisplayText = "模擬"
                End If
            End If
            '----------------當運算條件為1時則顯示按日期運算，否則顯示按單號運算----------------
            If e.Column.FieldName = "MI_CalcType" Then
                If e.Value Is Nothing Then
                ElseIf e.Value = 1 Then
                    e.DisplayText = "按日期運算"
                ElseIf e.Value = 0 Then
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
            If GridView1.FocusedRowHandle < 0 Then
                TreeList1.Nodes.Clear()
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
            Dim mobi As New List(Of MrpOrderDestBillsInfo)
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
            If control.GetType.Name.Equals("TreeList") Then
                TreeList1.ExpandAll()
            End If
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

#Region "鍵盤刪除快捷鍵事件"
    Private Sub grid1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grid1.KeyUp
        If e.KeyCode = Keys.Delete And GridView1.FocusedRowHandle >= 0 Then
            cmsMRPDel_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "獨立需求中失效日期為空時，設置其顯示文本為空"
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


    Private Sub tsm_PrintMrpInfoAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm_PrintMrpInfoAll.Click
        On Error Resume Next
        Dim fr As MrpReportSelect
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is MrpReportSelect Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New MrpReportSelect
        fr.intShowPage = 4
        'fr.MdiParent = MDIMain
        'fr.WindowState = FormWindowState.Maximized

        fr.ShowDialog()
        fr.Focus()
    End Sub

#Region "導出Excel"
    Private Sub cmsMRPExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMRPExcel.Click, cmsSubExcel.Click
        If sender.owner Is cmsMRP Then
            ConrotlExportExcel(grid1)
        ElseIf XtraTabControl1.SelectedTabPage Is xtpIndReq Then
            ConrotlExportExcel(TreeList1)
        ElseIf XtraTabControl1.SelectedTabPage Is xtpDestBills Then
            ConrotlExportExcel(grid2)
        ElseIf XtraTabControl1.SelectedTabPage Is xtpOrderBills Then
            ConrotlExportExcel(grid3)
        ElseIf XtraTabControl1.SelectedTabPage Is xtpPurchase Then
            ConrotlExportExcel(grid4)
        ElseIf XtraTabControl1.SelectedTabPage Is xtpPurchaseCalcRecord Then
            ConrotlExportExcel(Grid5)
        End If
    End Sub
#End Region
End Class
