Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.SampleManager.SampleOrdersSub
Imports LFERP.Library.SampleManager.SamplePlan
Imports LFERP.Library.SampleManager.SampleOrders
Imports LFERP.Library.SampleManager.SampleTransaction
Imports LFERP.Library.SampleManager.SampleCollection
Imports LFERP.Library.SampleManager.SampleWareInventory
Imports LFERP.Library.SampleManager.SampleProcess
Imports LFERP.Library.SampleManager.SampleSetting
Imports LFERP.Library.SampleManager.SampleSend
Imports System.Threading

Public Class frmSampleOrders

#Region "屬性"
    Dim threadOrdersA As Thread
    Dim threadOrdersB As Thread
    Dim threadOrdersC As Thread
    Dim threadOrdersD As Thread
    Dim threadOrdersE As Thread

    Dim strSO_No As String '
    Dim strSO_ID As String
    Dim strSS_Edition As String
    Dim strPM_M_Code As String
    Dim strM_Code As String

    Delegate Sub DelegateSetDataSource(ByVal dataSource As Object, ByVal control As Object)

    Dim cc As New SampleOrdersMainControler
    Dim sccon As New SampleOrdersCodeControler
    Dim stcon As New SampleCollectionControler
    Dim swcon As New SampleWareInventoryControler
    Dim ssbcon As New SampleSendBackControler
#End Region

#Region "窗體載入事件"
    Private Sub frmSampleOrders_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        cmdRef_Click(Nothing, Nothing)
    End Sub
#End Region

#Region "新增程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As frmSampleOrdersAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleOrdersAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleOrdersAdd
        fr.MdiParent = MDIMain
        'tempValue = "Add"
        fr.EditItem = EditEnumType.ADD
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region

#Region "修改事件"
    ''' <summary>
    '''修改事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        If cc.SampleOrdersMain_GetCheck(GridView1.GetFocusedRowCellValue("SO_No").ToString) Then
            MsgBox("审核后不能修改", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        Dim fr As frmSampleOrdersAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleOrdersAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleOrdersAdd
        fr.MdiParent = MDIMain
        'tempValue = "Edit"
        fr.EditItem = EditEnumType.EDIT
        tempValue2 = ""
        tempValue2 = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
#End Region


#Region "刪除事件"
    ''' <summary>
    ''' 刪除事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        If cc.SampleOrdersMain_GetCheck(GridView1.GetFocusedRowCellValue("SO_No").ToString) Then
            MsgBox("审核后不能修改", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If

        Dim SPC As New SamplePlanControler()
        If SPC.SamplePlan_Getlist(Nothing, GridView1.GetFocusedRowCellValue("SO_ID").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing).Count > 0 Then
            MsgBox("此订单已经有排期资料，不能刪除", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If


        If MsgBox("確定要刪除名称為： '" & GridView1.GetFocusedRowCellValue("SO_ID").ToString & "' 的記錄嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If cc.SampleOrdersMain_Delete(GridView1.GetFocusedRowCellValue("SO_No").ToString) = True Then
                MsgBox("刪除当前記錄信息成功！", 60, "提示")
                cmdRef_Click(Nothing, Nothing)
            Else
                MsgBox("刪除当前选定記錄失敗，请檢查原因！", 60, "提示")
                Exit Sub
            End If
        End If
    End Sub
#End Region

#Region "查询事件"
    ''' <summary>
    ''' 查询事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim fr As New frmSampleView
        fr = New frmSampleView
        fr.lbl_Title.Text = "样办查询--订单"
        fr.EditItem = "SampleOrders"
        fr.ShowDialog()
        If fr.SampleOrdersList.Count = 0 Then
            Grid1.DataSource = Nothing
            Exit Sub
        Else
            Grid1.DataSource = fr.SampleOrdersList
        End If
    End Sub
#End Region

#Region "审核事件"
    ''' <summary>
    ''' 审核事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        '1.是否采集过条码
        'Dim intCount As Integer = 0
        'Dim intSO_OrderQty As Integer = 0
        'Dim ssc As New SampleCollectionControler
        'intSO_OrderQty = GridView1.GetFocusedRowCellValue("SO_OrderQty").ToString
        'intCount = ssc.SampleCollection_Getlist(Nothing, Nothing, Nothing, GridView2.GetFocusedRowCellValue("SO_ID").ToString, GridView2.GetFocusedRowCellValue("SS_Edition").ToString, Nothing, False, Nothing, Nothing, "[a]", Nothing).Count
        'If intCount < intSO_OrderQty Then
        '    MsgBox("条码没有采集与或采集的条码小于订单数量,无法审核", MsgBoxStyle.OkOnly, "提示")
        '    Exit Sub
        'End If
        '2.
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strSO_ID = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        Dim solist As New List(Of SampleOrdersMainInfo) 
        solist = cc.SampleOrdersMain_GetList(strSO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, False)
        If solist.Count > 0 Then
            If solist(0).SO_Closed Then
                MsgBox("訂單結案后不能修改", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
            If solist(0).SO_State <> String.Empty Then
                MsgBox("此订单已经有排期资料不能修改", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        End If
        '----------------------------------------------------
        On Error Resume Next
        Dim fr As frmSampleOrdersAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleOrdersAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleOrdersAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.CHECK
        'tempValue = "Check"
        tempValue2 = ""
        tempValue2 = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        fr.Show()
    End Sub
#End Region

#Region "更新事件"
    ''' <summary>
    ''' 更新事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        ''相關參數設置
        Dim msi As New List(Of SampleSettingInfo)
        Dim msc As New SampleSettingController

        Dim StrCheck As String = Nothing
        Dim StrUser As String = Nothing
        Dim StrOrdersBeginDate As String = Nothing

        msi = msc.SampleSetting_GetList(InUserID)
        If msi.Count > 0 Then
            '1.審核類型
            Select Case msi(0).SampleOrdersCheck
                Case "0,1"
                    StrCheck = Nothing
                Case "1"
                    StrCheck = "True"
                Case "0"
                    StrCheck = "False"
            End Select

            '1.用戶選擇
            If msi(0).SampleOrdersCreateUserID = "All" Then
                StrUser = Nothing
            Else
                StrUser = msi(0).SampleOrdersCreateUserID
            End If
            Grid1.DataSource = cc.SampleOrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, StrCheck, msi(0).SampleOrdersBeginDate, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StrUser)
        Else
            Grid1.DataSource = cc.SampleOrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
        End If
    End Sub
#End Region

#Region "查看事件"
    ''' <summary>
    ''' 查看事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmSampleOrdersAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleOrdersAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleOrdersAdd
        fr.MdiParent = MDIMain
        'tempValue = "Look"
        fr.EditItem = EditEnumType.VIEW
        tempValue2 = ""
        tempValue2 = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        fr.WindowState = FormWindowState.Maximized

        fr.Show()


    End Sub
#End Region

#Region "列印事件"
    ''' <summary>
    ''' 列印事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim strSO_No As String
        strSO_No = GridView1.GetFocusedRowCellValue("SO_No").ToString

        ltc.CollToDataSet(dss, "SampleOrdersMain", cc.SampleOrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, strSO_No, False))
        ltc2.CollToDataSet(dss, "SampleOrdersSub", cc.SampleOrdersSub_GetList(strSO_No))

        PreviewRPT(dss, "rptSampleOrders", "订单资料表", True, True)
        ltc = Nothing
        ltc2 = Nothing
    End Sub
#End Region

#Region "子表数据填充"
    'Sub GridControl1show()

    '    GridControl2.DataSource = Nothing
    '    If GridView1.RowCount = 0 Then Exit Sub
    '    If GridView1.GetFocusedRowCellValue("SO_No").ToString IsNot Nothing Then
    '        ' strSO_No = GridView1.GetFocusedRowCellValue("SO_No").ToString
    '        'strSO_ID = GridView2.GetFocusedRowCellValue("SO_ID").ToString
    '        'strSS_Edition = GridView2.GetFocusedRowCellValue("SS_Edition").ToString
    '        'strPM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
    '        'strM_Code = GridView1.GetFocusedRowCellValue("M_Code").ToString

    '        'GridControl1.DataSource = cc.SampleOrdersSub_GetList(GridView1.GetFocusedRowCellValue("SO_No").ToString)

    '        'Dim ssc As New SampleCollectionControler
    '        'GridControl2.DataSource = ssc.SampleCollection_Getlist(Nothing, Nothing, Nothing, GridView2.GetFocusedRowCellValue("SO_ID").ToString, GridView2.GetFocusedRowCellValue("SS_Edition").ToString, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    '        'Dim prcon As New SampleProcessControl
    '        'GridControl4.DataSource = prcon.SampleProcessMain_GetList(Nothing, GridView1.GetFocusedRowCellValue("PM_M_Code").ToString, Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing)

    '        'Dim spw As New SampleWareInventoryControler
    '        'GridControl3.DataSource = spw.SampleProcessInventory_GetList("生產加工", GridView1.GetFocusedRowCellValue("PM_M_Code").ToString, GridView1.GetFocusedRowCellValue("M_Code").ToString)
    '        '---------------------------------------------------------------------
    '        'Try

    '        'If threadOrdersA Is Nothing = False Then
    '        '    threadOrdersA.Abort()
    '        'End If
    '        '    If threadOrdersB Is Nothing = False Then
    '        '        threadOrdersB.Abort()
    '        '    End If
    '        '    If threadOrdersC Is Nothing = False Then
    '        '        threadOrdersC.Abort()
    '        '    End If
    '        '    If threadOrdersD Is Nothing = False Then
    '        '        threadOrdersD.Abort()
    '        '    End If

    '        'threadOrdersA = New Thread(AddressOf LoadOrdersA)
    '        'threadOrdersA.Start()
    '        '    threadOrdersB = New Thread(AddressOf LoadOrdersB)
    '        '    threadOrdersB.Start()
    '        '    threadOrdersC = New Thread(AddressOf LoadOrdersC)
    '        '    threadOrdersC.Start()
    '        '    threadOrdersD = New Thread(AddressOf LoadOrdersD)
    '        '    threadOrdersD.Start()
    '        'Catch

    '        'End Try

    '    End If
    'End Sub

#Region "多線程"
    Private Sub LoadOrdersA()
        Try
            Dim npdlist As New List(Of SampleOrdersSubInfo)
            npdlist = cc.SampleOrdersSub_GetList(strSO_No)
            Dim DegDataSource As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(DegDataSource, npdlist, GridControl1)
            threadOrdersA.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "数据加载出错")
            End If
        End Try
    End Sub
    Private Sub LoadOrdersB()
        Try
            Dim sscon As New SampleCollectionControler
            Dim npdlist As New List(Of SampleCollectionInfo)
            npdlist = sscon.SampleCollection_Getlist(Nothing, Nothing, Nothing, strSO_ID, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim DegDataSource As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(DegDataSource, npdlist, GridControl2)
            threadOrdersB.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "数据加载出错")
            End If
        End Try
    End Sub
    Private Sub LoadOrdersC()
        Try
            Dim prcon As New SampleProcessControl
            Dim npdlist As New List(Of SampleProcessInfo)
            npdlist = prcon.SampleProcessMain_GetList(Nothing, strPM_M_Code, Nothing, Nothing, Nothing, True, Nothing)
            Dim DegDataSource As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(DegDataSource, npdlist, GridControl4)
            threadOrdersC.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "数据加载出错")
            End If
        End Try
    End Sub
    Private Sub LoadOrdersD()
        Try

            Dim spw As New SampleWareInventoryControler
            Dim npdlist As New List(Of SampleWareInventoryInfo)
            npdlist = spw.SampleProcessInventory_GetList("生產加工", strPM_M_Code, Nothing)

            Dim DegDataSource As New DelegateSetDataSource(AddressOf SetControlDataSource)

            Me.Invoke(DegDataSource, npdlist, GridControl3)
            threadOrdersD.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "数据加载出错")
            End If
        End Try
    End Sub
    Private Sub LoadOrdersE()
        Try
            Dim ssbcon As New SampleSendBackControler
            Dim ssblist As New List(Of SampleSendBackInfo)
            ssblist = ssbcon.SampleSendBack_GetList(strSO_ID, Nothing)
            Dim DegDataSource As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(DegDataSource, ssblist, GridControl5)
            threadOrdersE.Abort()
        Catch ex As Exception
            If ex.GetType.Name.Equals("ThreadAbortException") = False Then
                MsgBox(ex.Message, MsgBoxStyle.Critical, "数据加载出错")
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

#End Region


    ''' <summary>
    ''' 条码数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub GridControl2show()
        If GridView1.RowCount = 0 Then Exit Sub
        If GridView1.GetFocusedRowCellValue("SO_ID").ToString IsNot Nothing Then
            ' GridControl2.DataSource = sccon.SampleOrdersCode_Getlist(Nothing, GridView1.GetFocusedRowCellValue("SO_ID").ToString, Nothing, Nothing, Nothing, Nothing)
        End If
    End Sub
    ''' <summary>
    ''' 条码数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub GridControl3show()
        If GridView2.RowCount = 0 Then Exit Sub
        If GridView2.GetFocusedRowCellValue("SO_ID").ToString IsNot Nothing Then
            ' GridControl2.DataSource = sccon.SampleOrdersCode_Getlist(Nothing, GridView2.GetFocusedRowCellValue("SO_ID").ToString, GridView2.GetFocusedRowCellValue("SS_Edition").ToString, Nothing, Nothing, Nothing)
            Dim ssc As New SampleCollectionControler
            GridControl2.DataSource = ssc.SampleCollection_Getlist(Nothing, Nothing, Nothing, GridView2.GetFocusedRowCellValue("SO_ID").ToString, GridView2.GetFocusedRowCellValue("SS_Edition").ToString, Nothing, False, Nothing, Nothing, "[a]", Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "表格行調色"
    ''' <summary>
    ''' 調色彩
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
        If e.Column.FieldName = "SO_Rank" Then
            If e.CellValue = "B" Then
                e.Appearance.BackColor = Color.Yellow
            End If
            If e.CellValue = "C" Then
                e.Appearance.BackColor = Color.Orange
            End If
            If e.CellValue = "A" Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub
#End Region

#Region "设置权限"
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890104")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890105")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdPrint.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890106")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.cmdCODE.Enabled = True
                Me.cmdCODEA.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdCODEDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890108")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdEditQty.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890109")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdSO_SampleID.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890110")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.ToolStripWareChange.Visible = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890111")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdSendBack.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890112")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdEditType.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890113")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Me.cmdBack.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890114")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                Me.cmdOrderClose.Enabled = True
            End If
        End If

    End Sub
#End Region

#Region "表格觸發事件"
    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        'GridControl1show()
        GridControl2.DataSource = Nothing
        If GridView1.RowCount = 0 Then Exit Sub
        Try
            strSO_No = GridView1.GetFocusedRowCellValue("SO_No").ToString
            strSO_ID = GridView1.GetFocusedRowCellValue("SO_ID").ToString
            strPM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
            strM_Code = GridView1.GetFocusedRowCellValue("M_Code").ToString

            If threadOrdersA Is Nothing = False Then
                threadOrdersA.Abort()
            End If
            If threadOrdersB Is Nothing = False Then
                threadOrdersB.Abort()
            End If
            If threadOrdersC Is Nothing = False Then
                threadOrdersC.Abort()
            End If
            If threadOrdersD Is Nothing = False Then
                threadOrdersD.Abort()
            End If
            If threadOrdersE Is Nothing = False Then
                threadOrdersE.Abort()
            End If

            threadOrdersA = New Thread(AddressOf LoadOrdersA)
            threadOrdersA.Start()
            threadOrdersB = New Thread(AddressOf LoadOrdersB)
            threadOrdersB.Start()
            threadOrdersC = New Thread(AddressOf LoadOrdersC)
            threadOrdersC.Start()
            threadOrdersD = New Thread(AddressOf LoadOrdersD)
            threadOrdersD.Start()
            threadOrdersE = New Thread(AddressOf LoadOrdersE)
            threadOrdersE.Start()
        Catch
        End Try
        Me.GridView5.ActiveFilterString = "[StatusTypeName] <>'內-->客' and [StatusType]<>'T' "
    End Sub
    Private Sub GridView2_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        GridControl3show()
    End Sub
    Private Sub GridControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridControl1.Click
        GridControl3show()
    End Sub
#End Region

#Region "條碼事件處理"
    Private Sub 条码采集_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCODE.Click, cmdCODEA.Click
        Dim fr As New frmSampleBarcode
        fr = New frmSampleBarcode
        fr.Lbl_Title.Text = "订单条码采集"
        fr.EditItem = "SampleOrders"
        fr.EditValue = GridView1.GetFocusedRowCellValue("PM_M_Code")
        fr.SO_ID = GridView1.GetFocusedRowCellValue("SO_ID")
        fr.SS_Edition = GridView2.GetFocusedRowCellValue("SS_Edition")
        fr.SS_Qty = GridView1.GetFocusedRowCellValue("SO_OrderQty")
        fr.ShowDialog()
        'GridControl1show()
        'GridControl3show()
    End Sub


    Private Sub cmdCODEDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCODEDel.Click
        If GridView5.RowCount = 0 Then Exit Sub

        If GridView5.GetFocusedRowCellValue("SP_ID").ToString <> String.Empty Then
            MsgBox("存在样办寄送资料无法刪除", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        If GridView5.GetFocusedRowCellValue("StatusTypeName").ToString <> String.Empty Then
            MsgBox("条码存在交易无法刪除", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        Dim strCode_ID As String = GridView5.GetFocusedRowCellValue("Code_ID").ToString
        If MsgBox("確定要刪除名称為： '" & strCode_ID & "' 的記錄嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If stcon.SampleCollection_Delete(strCode_ID, Nothing) = True Then
                MsgBox("刪除当前記錄信息成功！", 60, "提示")
                'GridControl1show()
                GridView1_FocusedRowChanged(Nothing, Nothing)
                GridControl3show()
            Else
                MsgBox("刪除当前选定記錄失敗，请檢查原因！", 60, "提示")
                Exit Sub
            End If
        End If

    End Sub
#End Region

#Region "列印"
    Private Sub cmdPrintTotal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintTotal.Click

        Dim dss As New DataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet

        Dim StrSO_ID As String = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        Dim strSO_No As String = GridView1.GetFocusedRowCellValue("SO_No").ToString
        Dim strPM_M_Code As String = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        Dim strS_Edition As String = GridView2.GetFocusedRowCellValue("SS_Edition")

        Dim scon1 As New LFERP.Library.SampleManager.SampleWareInventory.SampleWareInventoryControler
        Dim scon2 As New LFERP.Library.SampleManager.SampleCollection.SampleCollectionControler
        ltc1.CollToDataSet(dss, "SampleOrdersMain", cc.SampleOrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, strSO_No, False))
        ltc2.CollToDataSet(dss, "SampleOrdersSub", cc.SampleOrdersSub_GetList(strSO_No))
        ltc3.CollToDataSet(dss, "SampleWareInventory", swcon.SampleWareInventory_Getlist(strPM_M_Code, Nothing, Nothing, Nothing, True, Nothing))
        ltc4.CollToDataSet(dss, "SampleCollection", stcon.SampleCollection_Getlist(Nothing, Nothing, Nothing, StrSO_ID, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

        PreviewRPT(dss, "rptSampleOrdersTotal", "订单漚總表", True, True)
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
    End Sub
#End Region

    Private Sub cmdEditQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditQty.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As New frmSampleOrdersSelect
        fr = New frmSampleOrdersSelect
        fr.SO_ID = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        fr.SS_Edition = GridView2.GetFocusedRowCellValue("SS_Edition").ToString
        'fr.PM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        'fr.SO_OrderQty = GridView1.GetFocusedRowCellValue("SO_OrderQty").ToString
        fr.ShowDialog()
        cmdRef_Click(Nothing, Nothing)
    End Sub

    Private Sub cmdSO_SampleID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSO_SampleID.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As New frmSampleOrdersVer
        fr = New frmSampleOrdersVer
        fr.SO_ID = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        fr.SS_Edition = GridView2.GetFocusedRowCellValue("SS_Edition").ToString
        'fr.PM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        'fr.SO_OrderQty = GridView1.GetFocusedRowCellValue("SO_OrderQty").ToString
        fr.ShowDialog()
    End Sub

    Private Sub cmdQueryInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQueryInventory.Click
        If GridView1.RowCount = 0 Then Exit Sub

        On Error Resume Next
        Dim fr As frmSampleOrdersQuery
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleOrdersQuery Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleOrdersQuery
        fr.MdiParent = MDIMain
        fr.PM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code").ToString
        fr.Show()
    End Sub

    Private Sub ToolStripWareChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripWareChange.Click
        If GridView3.RowCount <= 0 Then Exit Sub

        Dim fr As New frmSampleWareInventoryChange
        fr = New frmSampleWareInventoryChange

        fr.strPM_M_CodeA = GridView1.GetFocusedRowCellValue("PM_M_Code")
        fr.strD_ID = GridView3.GetFocusedRowCellValue("D_ID")
        fr.strPS_NO = GridView3.GetFocusedRowCellValue("PS_NO")

        fr.ShowDialog()
        fr.Dispose()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        D_ID.Visible = True : PS_NO.Visible = True
    End Sub

    Private Sub cmdExcelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelA.Click
        'If GridView1.RowCount < 1 Then
        '    MsgBox("沒有可導出的數據", MsgBoxStyle.Information, "提示")
        '    Exit Sub
        'End If
        'Dim sfd1 As New SaveFileDialog
        'sfd1.DefaultExt = ".xls"
        'sfd1.Filter = "Excel Files|*.xls|All Files|*.*"
        'If sfd1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    GridView1.ExportToXls(sfd1.FileName)
        '    MsgBox("已成功導出到：" + sfd1.FileName, MsgBoxStyle.Information, "提示")
        'End If

        If GridView1.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(Grid1, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If

    End Sub

#Region "订单退回处理"

    Private Sub cmdSendBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendBack.Click
        If GridView1.RowCount = 0 Then Exit Sub
        On Error Resume Next
        Dim fr As frmSampleSendBack
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendBack Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleSendBack
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.ADD
        fr.Text = "订单退回"
        fr.SO_CusterID = GridView1.GetFocusedRowCellValue("SO_CusterID")
        fr.SO_SampleID = GridView1.GetFocusedRowCellValue("SO_SampleID")
        fr.PM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code")
        fr.SO_ID = GridView1.GetFocusedRowCellValue("SO_ID")
        fr.SS_Edition = GridView1.GetFocusedRowCellValue("SS_Edition")
        fr.Show()
    End Sub

    Private Sub cmdSendBackLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendBackLook.Click
        If GridView7.RowCount = 0 Then Exit Sub
        On Error Resume Next
        Dim fr As frmSampleSendBack
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendBack Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmSampleSendBack
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.VIEW
        Dim strSE_TypeName As String = GridView7.GetFocusedRowCellValue("SE_TypeName")
        If strSE_TypeName = "订单退回" Then
            fr.Text = "订单退回"
        Else
            fr.Text = "完工退回"
        End If

        fr.SO_CusterID = GridView1.GetFocusedRowCellValue("SO_CusterID")
        fr.SO_SampleID = GridView1.GetFocusedRowCellValue("SO_SampleID")
        fr.PM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code")
        fr.SO_ID = GridView1.GetFocusedRowCellValue("SO_ID")
        fr.SB_IDItem = GridView7.GetFocusedRowCellValue("SB_ID")
        fr.Show()
    End Sub
#End Region

    Private Sub cmdEditType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditType.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As New frmSampleOrdersType
        fr = New frmSampleOrdersType
        fr.SO_ID = GridView1.GetFocusedRowCellValue("SO_ID").ToString
        fr.EditItem = EditEnumType.ADD
        fr.ShowDialog()
        cmdRef_Click(Nothing, Nothing)
    End Sub

    Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBack.Click

        If GridView1.RowCount = 0 Then Exit Sub
        On Error Resume Next
        Dim fr As frmSampleSendBack
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleSendBack Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleSendBack
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.ELSEONE
        fr.Text = "完工退回"
        fr.SO_CusterID = GridView1.GetFocusedRowCellValue("SO_CusterID")
        fr.SO_SampleID = GridView1.GetFocusedRowCellValue("SO_SampleID")
        fr.PM_M_Code = GridView1.GetFocusedRowCellValue("PM_M_Code")
        fr.SO_ID = GridView1.GetFocusedRowCellValue("SO_ID")
        fr.SS_Edition = GridView1.GetFocusedRowCellValue("SS_Edition")
        fr.Show()
    End Sub

    Private Sub cmdOrderClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOrderClose.Click
        If GridView1.RowCount = 0 Then Exit Sub
        If GridView1.FocusedRowHandle < 0 Then Exit Sub
        Dim fr As New frmSampleOrdersClose
        Dim strPM_M_Code As String = GridView1.GetFocusedRowCellValue("PM_M_Code")

        fr.PM_M_Code = strPM_M_Code
        fr.ShowDialog()
    End Sub

    Private Sub cmdSendBackQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendBackQuery.Click
        If GridView1.RowCount = 0 Then Exit Sub
        On Error Resume Next
        Dim fr As frmSampleOrdersBack
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSampleOrdersBack Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSampleOrdersBack
        fr.MdiParent = MDIMain
        'fr.EditItem = EditEnumType.ELSETWO
        fr.Show()
    End Sub
End Class