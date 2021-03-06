Imports LFERP.Library.MaterialParam
Imports LFERP.Library.Material
Imports LFERP.Library
Imports LFERP.Library.Product
Imports LFERP.SystemManager
Imports LFERP.Library.WareHouse.WareInventory

Public Class frmBOMWareSelect
#Region "屬性"
    Dim MPController As New MaterialParamController
    Dim mtc As New Material.MaterialTypeController
    Dim mc As New MaterialController

    Dim strCode As String
    Dim dl As New DataSet
    Dim ds As New DataSet
    Dim d2 As New DataSet

    Private _EditCode As String '產品編號
    Private _EditValue As String
    Private _EditItem As String '產品編號
    Private _EditWH_ID As String '傳回多個倉庫
    Private _EditWH_IDValue As String '傳入倉庫

    Property EditCode() As String '属性
        Get
            Return _EditCode
        End Get
        Set(ByVal value As String)
            _EditCode = value
        End Set
    End Property

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property EditValue() As String '属性
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property

    Property EditWH_ID() As String '属性
        Get
            Return _EditWH_ID
        End Get
        Set(ByVal value As String)
            _EditWH_ID = value
        End Set
    End Property
    Property EditWH_IDValue() As String '属性
        Get
            Return _EditWH_IDValue
        End Get
        Set(ByVal value As String)
            _EditWH_IDValue = value
        End Set
    End Property
#End Region

#Region "按鍵事件"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            Dim i, n As Integer
            For i = 0 To d2.Tables("WareItemA").Rows.Count - 1
                If d2.Tables("WareItemA").Rows(i)("GoIn") = True Then
                    If n = 0 Then
                        EditCode = d2.Tables("WareItemA").Rows(i)("M_Code")
                        EditWH_ID = d2.Tables("WareItemA").Rows(i)("WH_ID")
                        n = n + 1
                    Else
                        EditCode = EditCode & "," & d2.Tables("WareItemA").Rows(i)("M_Code")
                        EditWH_ID = EditWH_ID & "," & d2.Tables("WareItemA").Rows(i)("WH_ID")
                        n = n + 1
                    End If
                End If
            Next
        ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
            Dim i, n As Integer
            n = 0
            Select Case EditItem
                Case "開料管理"
                    For i = 0 To d2.Tables("WareItemB").Rows.Count - 1
                        If d2.Tables("WareItemB").Rows(i)("GoIn") = True Then
                            If n = 0 Then
                                EditCode = d2.Tables("WareItemB").Rows(i)("M_Code")
                                EditWH_ID = d2.Tables("WareItemB").Rows(i)("WH_ID")
                                n = n + 1
                            Else
                                EditCode = EditCode & "," & d2.Tables("WareItemB").Rows(i)("M_Code")
                                EditWH_ID = EditWH_ID & "," & d2.Tables("WareItemB").Rows(i)("WH_ID")
                                n = n + 1
                            End If
                        End If
                    Next
            End Select
        End If
        If EditCode = "" Then
            MsgBox("請選擇需要導入的資料!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
        Me.Close()
    End Sub
#Region "確認退出事件"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region
#End Region

#Region "窗體載入事件"
    Private Sub frmBOMWareSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mtc.LoadNodes(tv1, ErpUser.MaterialType)

        Dim pc As New ProductController
        txtLFID.Properties.DataSource = pc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        txtLFID.Properties.DisplayMember = "PM_M_Code"
        txtLFID.Properties.ValueMember = "PM_M_Code"

        Select Case EditItem
            Case "開料管理"
                XtraTabControl1.SelectedTabPage = XtraTabPage3
        End Select

        Grid.DataSource = Nothing
        Grid2.DataSource = Nothing
        TreeList1.DataSource = ""

        txtLFID.Text = EditValue
        EditCode = String.Empty
        EditWH_ID = String.Empty
        '------------------
        txtName.Select()
        CreateWareTable()
    End Sub
#End Region
 
#Region "創建臨時表"
    Sub CreateBatchTable()
        ds.Tables.Clear()
        With ds.Tables.Add("OrdersBom")
            .Columns.Add("OS_BatchID", GetType(Integer))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("OB_PID", GetType(String))
            .Columns.Add("OB_Key", GetType(String))
            .Columns.Add("OB_Qty", GetType(Double))
            .Columns.Add("OB_MainQty", GetType(Double))
            .Columns.Add("OB_Price", GetType(Double))
            .Columns.Add("OB_Supplier", GetType(String))
            .Columns.Add("OB_SupplierNo", GetType(String))
            .Columns.Add("OB_Product", GetType(String))
            .Columns.Add("OB_ProductNo", GetType(String))
            .Columns.Add("OB_ProductRemark", GetType(String))
            .Columns.Add("OB_AccountCheck", GetType(Boolean))
            .Columns.Add("OB_Make", GetType(Boolean))
            .Columns.Add("OB_MarkRemark", GetType(String))
            .Columns.Add("OB_ID", GetType(Integer))
            .Columns.Add("PM_LVL", GetType(Integer))
            .Columns.Add("M_CodeType", GetType(String))
            .Columns.Add("M_CodeMouldNO", GetType(String))
            .Columns.Add("OB_MakeDepartment", GetType(String))
            .Columns.Add("OB_MModeCusterNO", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("GoIn", GetType(Boolean))
        End With
        Grid2.DataSource = ds.Tables("OrdersBom") '批次查詢臨時表
    End Sub
    Sub CreateLFIDTable()
        dl.Tables.Clear()
        With dl.Tables.Add("Pro_Mounting_New")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_ID", GetType(Integer))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_CodePID", GetType(String))
            .Columns.Add("PM_Qty", GetType(Double))
            .Columns.Add("PM_MakeRemark", GetType(String))
            .Columns.Add("PM_Make", GetType(Boolean))
            .Columns.Add("PM_Check", GetType(Boolean))
            .Columns.Add("M_Supplier", GetType(String))
            .Columns.Add("M_SupplierNo", GetType(String))
            .Columns.Add("PM_Key", GetType(String))
            .Columns.Add("PM_PID", GetType(String))
            .Columns.Add("PM_LVL", GetType(Integer))
            .Columns.Add("M_CodeType", GetType(String))
            .Columns.Add("M_CodeMouldNO", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("GoIn", GetType(Boolean))
        End With
        TreeList1.DataSource = dl.Tables("Pro_Mounting_New") '產品編號查詢臨時表
    End Sub
    Sub CreateMaterialCode()
        ds.Tables.Clear()
        With ds.Tables.Add("MaterialCode")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("M_SupplierNo", GetType(String))
        End With
        Grid.DataSource = ds.Tables("MaterialCode") '普通查詢
    End Sub
    Sub CreateWareTable()
        d2.Tables.Clear()
        With d2.Tables.Add("WareItemA")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("WH_Name", GetType(String))
            .Columns.Add("WH_ID", GetType(String))
            .Columns.Add("WI_Qty", GetType(String))
            .Columns.Add("GoIn", GetType(Boolean))
        End With
        GridControl1.DataSource = d2.Tables("WareItemA") '批次查詢臨時表

        With d2.Tables.Add("WareItemB")
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("WH_Name", GetType(String))
            .Columns.Add("WH_ID", GetType(String))
            .Columns.Add("WI_Qty", GetType(String))
            .Columns.Add("GoIn", GetType(Boolean))
        End With
        GridControl2.DataSource = d2.Tables("WareItemB") '批次查詢臨時表
    End Sub
#End Region

#Region "數據載入"
    Sub LoadDataOrdersBom()
        Dim osi As List(Of Orders.OrdersSubInfo)
        Dim osc As New Orders.OrdersSubController
        osi = osc.OrdersSub_GetList(Nothing, txtMo.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If osi.Count = 0 Then Exit Sub
        Dim obc As New Orders.OrdersBomController
        Dim tlist As List(Of Orders.OrdersBomInfo)
        ds.Tables("OrdersBom").Rows.Clear()
        tlist = obc.OrdersBom_GetList(Nothing, txtMo.Text, Nothing, Nothing)
        If tlist Is Nothing Then Exit Sub

        On Error Resume Next
        Dim i As Integer
        Dim row As DataRow
        For i = 0 To tlist.Count - 1
            row = ds.Tables("OrdersBom").NewRow
            row("M_Code") = tlist(i).M_Code
            row("OB_Qty") = tlist(i).OB_Qty
            row("OB_MakeRemark") = tlist(i).OB_MakeRemark
            row("OB_Make") = tlist(i).OB_Make
            row("OB_Supplier") = tlist(i).OB_Supplier
            row("OB_SupplierNo") = tlist(i).OB_SupplierNo
            row("M_Name") = tlist(i).M_Name
            row("M_Gauge") = tlist(i).M_Gauge
            row("OB_AccountCheck") = tlist(i).OB_AccountCheck
            row("OB_Key") = tlist(i).OB_Key
            row("OB_PID") = tlist(i).OB_PID
            row("PM_LVL") = tlist(i).PM_LVL
            row("M_CodeType") = tlist(i).M_CodeType
            row("M_CodeMouldNO") = tlist(i).M_CodeMouldNO
            row("OB_MakeDepartment") = tlist(i).OB_MakeDepartment
            row("OB_MModeCusterNO") = tlist(i).OB_MModeCusterNO
            row("OB_Product") = tlist(i).OB_Product
            row("GoIn") = False
            ds.Tables("OrdersBom").Rows.Add(row)
        Next
        Grid2.ExpandAll()
        If ds.Tables("OrdersBom").Rows.Count = 0 Then
            MsgBox("未載入產品配件資料或產品資料配件未建立！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End If
    End Sub

    Sub LoadProductBomData()
        Dim pi As List(Of ProductBomInfo)
        Dim pbc As New ProductBomController
        dl.Tables("Pro_Mounting_New").Rows.Clear()
        If EditItem = "生產倉庫調撥" Then
            pi = pbc.Prod_Mounting_New_GetList(txtLFID.Text.Trim, Nothing, Nothing, 1, Nothing, Nothing, True)
        Else
            pi = pbc.ProductBom_GetList(txtLFID.Text, Nothing, Nothing, Nothing, Nothing, True)
        End If
        If pi.Count <= 0 Then
            MsgBox("產品資料配件未建立！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
        On Error Resume Next
        Dim i As Integer
        Dim row As DataRow
        row = dl.Tables("Pro_Mounting_New").NewRow
        row("M_Code") = txtLFID.Text
        row("M_CodePID") = pi(0).M_CodePID
        row("PM_Qty") = pi(0).PM_Qty
        row("PM_MakeRemark") = pi(0).PM_MakeRemark
        row("PM_Make") = pi(0).PM_Make
        row("M_Supplier") = pi(0).M_Supplier
        row("M_SupplierNo") = pi(0).M_SupplierNo
        row("M_Name") = txtLFID.Text
        row("PM_Check") = pi(0).PM_Check
        row("PM_Key") = pi(0).PM_PID
        row("PM_PID") = "dd"
        row("M_CodeType") = pi(0).M_CodeType
        row("M_CodeMouldNO") = pi(0).M_CodeMouldNO
        row("GoIn") = False
        dl.Tables("Pro_Mounting_New").Rows.Add(row)
        For i = 0 To pi.Count - 1
            row = dl.Tables("Pro_Mounting_New").NewRow
            row("M_Code") = pi(i).M_Code
            row("M_CodePID") = pi(i).M_CodePID
            row("PM_Qty") = pi(i).PM_Qty
            row("PM_MakeRemark") = pi(i).PM_MakeRemark
            row("PM_Make") = pi(i).PM_Make
            row("M_Supplier") = pi(i).M_Supplier
            row("M_SupplierNo") = pi(i).M_SupplierNo
            row("M_Name") = pi(i).M_Name
            row("M_Gauge") = pi(i).M_Gauge
            row("PM_Check") = pi(i).PM_Check
            row("PM_Key") = pi(i).PM_Key
            row("PM_PID") = pi(i).PM_PID
            row("PM_LVL") = pi(i).PM_LVL
            row("M_CodeType") = pi(i).M_CodeType
            row("M_CodeMouldNO") = pi(i).M_CodeMouldNO
            row("GoIn") = False
            dl.Tables("Pro_Mounting_New").Rows.Add(row)
        Next
        TreeList1.ExpandAll()
    End Sub

    Sub LoadMaterialCodeData()
        Dim mi As List(Of MaterialInfo)
        Dim mc As New MaterialController
        Dim strCode, strName, strTypeID, strGauge, strSupplierNo As String
        If CheckEdit7.Checked = True Then
            If PopupContainerEdit1.EditValue Is Nothing Then
                MsgBox("沒有選擇類別,請選擇!", , "提示")
                Exit Sub
            Else
                strTypeID = PopupContainerEdit1.EditValue
            End If
        Else
            strTypeID = Nothing 'ErpUser.MaterialType
        End If
        If CheckEdit4.Checked = True Then
            If Len(txtCode.EditValue) = 0 Then
                MsgBox("沒有輸入物料編碼,請輸入!", , "提示")
                Exit Sub
            Else
                strCode = txtCode.EditValue
            End If
        Else
            strCode = Nothing
        End If
        If CheckEdit6.Checked = True Then
            If Len(txtName.EditValue) = 0 Then
                MsgBox("沒有輸入物料名稱,請輸入!", , "提示")
                Exit Sub
            Else
                strName = txtName.EditValue
            End If
        Else
            strName = Nothing
        End If
        If CheckEdit2.Checked = True Then
            If Len(txtGauge.EditValue) = 0 Then
                MsgBox("沒有輸入物料規格,請輸入!", , "提示")
                Exit Sub
            Else
                strGauge = txtGauge.EditValue
            End If
        Else
            strGauge = Nothing
        End If

        If CheckEdit1.Checked = True Then
            If Len(TextEdit2.EditValue) = 0 Then
                MsgBox("沒有輸入供應商編號,請輸入!", , "提示")
                Exit Sub
            Else
                strSupplierNo = TextEdit2.EditValue
            End If
        Else
            strSupplierNo = Nothing
        End If

        '***判斷用戶是擁有特殊類的權限
        Dim pmws2 As New PermissionModuleWarrantSubController
        Dim pmwiL2 As List(Of PermissionModuleWarrantSubInfo)
        Dim Stra As String
        Stra = "否"
        pmwiL2 = pmws2.PermissionModuleWarrantSub_GetList(InUserID, "100115")

        If pmwiL2.Item(0).PMWS_Value = "是" Then
            Stra = "是"
        End If
        '************
        mi = mc.MaterialCode_GetList(strCode, strName, strGauge, strTypeID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strSupplierNo, Stra)
        If mi.Count = 0 Then Exit Sub

        Dim i As Integer
        Dim row As DataRow
        ds.Tables("MaterialCode").Clear()

        For i = 0 To mi.Count - 1
            row = ds.Tables("MaterialCode").NewRow
            row("M_Code") = mi(i).M_Code
            row("M_Name") = mi(i).M_Name
            row("M_Gauge") = mi(i).M_Gauge
            row("M_Unit") = mi(i).M_Unit
            row("M_SupplierNo") = mi(i).M_SupplierNo
            ds.Tables("MaterialCode").Rows.Add(row)
        Next
    End Sub

    Sub LoadInventoryData()
        Dim mi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
        Dim mc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
        Dim strCode, strName, strTypeID, strGauge As String
        If CheckEdit7.Checked = True Then
            If PopupContainerEdit1.EditValue Is Nothing Then
                MsgBox("沒有選擇類別,請選擇!", , "提示")
                Exit Sub
            Else
                strTypeID = PopupContainerEdit1.EditValue
            End If
        Else
            strTypeID = Nothing
        End If
        If CheckEdit4.Checked = True Then
            If Len(txtCode.EditValue) = 0 Then
                MsgBox("沒有輸入物料編碼,請輸入!", , "提示")
                Exit Sub
            Else
                strCode = txtCode.EditValue
            End If
        Else
            strCode = Nothing
        End If
        If CheckEdit6.Checked = True Then
            If Len(txtName.EditValue) = 0 Then
                MsgBox("沒有輸入物料名稱,請輸入!", , "提示")
                Exit Sub
            Else
                strName = txtName.EditValue
            End If
        Else
            strName = Nothing
        End If
        If CheckEdit2.Checked = True Then
            If Len(txtGauge.EditValue) = 0 Then
                MsgBox("沒有輸入物料規格,請輸入!", , "提示")
                Exit Sub
            Else
                strGauge = txtGauge.EditValue
            End If
        Else
            strGauge = Nothing
        End If
        mi = mc.WareInventory_SelectCode(strCode, strName, strGauge, EditWH_IDValue)
        If mi.Count = 0 Then Exit Sub

        Dim i As Integer
        Dim row As DataRow
        For i = 0 To mi.Count - 1
            row = ds.Tables("MaterialCode").NewRow
            row("M_Code") = mi(i).M_Code
            row("M_Name") = mi(i).M_Name
            row("M_Gauge") = mi(i).M_Gauge
            row("M_Unit") = mi(i).M_Unit
            row("M_SupplierNo") = mi(i).M_SupplierNo
            ds.Tables("MaterialCode").Rows.Add(row)
        Next
    End Sub

#End Region

#Region "表格行事件"
    'Private Sub Grid2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid2.MouseUp
    '    If Grid2.Nodes.Count = 0 Then Exit Sub
    '    If EditItem = "採購管理" Then
    '        If Grid2.FocusedNode("OB_Product").ToString = "不需採購" Or Grid2.FocusedNode("OB_Product").ToString = "待復" Then
    '            GoIn.OptionsColumn.AllowEdit = False
    '            MsgBox("此物料目前還不能被採購", , "提示")
    '            Exit Sub
    '        Else
    '            GoIn.OptionsColumn.AllowEdit = True
    '        End If
    '    Else
    '        GoIn.OptionsColumn.AllowEdit = True
    '    End If
    'End Sub

    Private Sub Grid_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid.MouseUp
        If GridView1.RowCount = 0 Then Exit Sub
        Dim intRow = d2.Tables("WareItemA").Rows.Count
        Dim i As Integer
        For i = 0 To intRow - 1
            d2.Tables("WareItemA").Rows.RemoveAt(0)
        Next

        Dim wtc As New WareInventoryMTController
        Dim wilist As New List(Of WareInventoryInfo)
        wilist = wtc.WareInventory_GetList3(GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, False)
        Dim row As DataRow
        For i = 0 To wilist.Count - 1
            row = d2.Tables("WareItemA").NewRow
            row("M_Code") = wilist(i).M_Code
            row("M_Name") = wilist(i).M_Name
            row("M_Gauge") = wilist(i).M_Gauge
            row("WH_Name") = wilist(i).WH_AllName
            row("WH_ID") = wilist(i).WH_ID
            row("WI_Qty") = wilist(i).WI_Qty
            row("GoIn") = False
            d2.Tables("WareItemA").Rows.Add(row)
        Next
    End Sub

    Private Sub TreeList1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeList1.MouseUp
        If TreeList1.VisibleNodesCount = 0 Then
            Exit Sub
        End If

        Dim strA As String = TreeList1.FocusedNode.Item("M_Code").ToString()
        If (strA = String.Empty) Then
            Exit Sub
        End If
        Dim intRow = d2.Tables("WareItemB").Rows.Count
        Dim i As Integer
        For i = 0 To intRow - 1
            d2.Tables("WareItemB").Rows.RemoveAt(0)
        Next
        '--------------------------------------------------
        Dim wtc As New WareInventoryMTController
        Dim wilist As New List(Of WareInventoryInfo)
        wilist = wtc.WareInventory_GetList3(strA, Nothing, False)

        Dim row As DataRow
        For i = 0 To wilist.Count - 1
            row = d2.Tables("WareItemB").NewRow
            row("M_Code") = wilist(i).M_Code
            row("M_Name") = wilist(i).M_Name
            row("M_Gauge") = wilist(i).M_Gauge
            row("WH_Name") = wilist(i).WH_AllName
            row("WH_ID") = wilist(i).WH_ID
            row("WI_Qty") = wilist(i).WI_Qty
            row("GoIn") = False
            d2.Tables("WareItemB").Rows.Add(row)
        Next
    End Sub

#End Region

#Region "查詢事件按鍵"
    Private Sub tv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tv1.DoubleClick
        PopupContainerEdit1.EditValue = tv1.SelectedNode.Tag
        PopupContainerControl1.OwnerEdit.ClosePopup()
    End Sub

    Private Sub cmdSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeek.Click
        If (GridView4.RowCount > 0) Then
            Dim intRow = d2.Tables("WareItemA").Rows.Count
            Dim i As Integer
            For i = 0 To intRow - 1
                d2.Tables("WareItemA").Rows.RemoveAt(0)
            Next
        End If

        If Label6.Text = "出庫作業" Then
            CreateMaterialCode()
            LoadInventoryData()
        Else
            CreateMaterialCode()
            LoadMaterialCodeData()
        End If
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim ob As New Orders.OrdersBomController
        If txtMo.Text = "" Then
            MsgBox("批次編號不能為空，請輸入！")
            Exit Sub
        End If
        CreateBatchTable()
        LoadDataOrdersBom()
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        If (GridView5.RowCount > 0) Then
            Dim intRow = d2.Tables("WareItemB").Rows.Count
            Dim i As Integer
            For i = 0 To intRow - 1
                d2.Tables("WareItemB").Rows.RemoveAt(0)
            Next
        End If

        Dim pbc As New ProductBomController

        If txtLFID.Text = "" Then
            MsgBox("產品編號不能為空，請輸入！")
            Exit Sub
        End If
        CreateLFIDTable()
        LoadProductBomData()
    End Sub
#End Region

#Region "特別控件事件"
    '指定物料集
    Private Sub cmdWare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWare.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim i As Integer
        Dim str As String
        str = Nothing
        For i = 0 To ds.Tables("MaterialCode").Rows.Count - 1
            str = str & "," & "'" & ds.Tables("MaterialCode").Rows(i)("M_Code") & "'"
        Next
        tempCode = str
        Me.Close()
    End Sub

    Private Sub txtName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown, txtCode.KeyDown, txtGauge.KeyDown, TextEdit2.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdSeek_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub CheckEdit7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit7.CheckedChanged
        If CheckEdit7.Checked = True Then
            PopupContainerEdit1.Focus()
        End If
    End Sub

    Private Sub CheckEdit4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit4.CheckedChanged
        If CheckEdit4.Checked = True Then
            txtCode.Focus()
        End If
    End Sub

    Private Sub CheckEdit6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit6.CheckedChanged
        If CheckEdit6.Checked = True Then
            txtName.Focus()
        End If
    End Sub

    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        If CheckEdit2.Checked = True Then
            txtGauge.Focus()
        End If
    End Sub

    Private Sub CheckEdit1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit1.CheckedChanged
        If CheckEdit1.Checked = True Then
            TextEdit2.Focus()
        End If
    End Sub

    Private Sub cmdAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAll.Click
        LoadMaterialCodeData()
    End Sub
#End Region

End Class