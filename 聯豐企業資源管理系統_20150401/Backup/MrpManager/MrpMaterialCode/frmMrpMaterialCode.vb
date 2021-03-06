Imports LFERP.Library.MrpManager.MrpSupplierQuotation
Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.MrpManager.MrpMaterialCode
Imports LFERP.Library.MrpManager.MrpWareHouseInfo
Imports LFERP.DataSetting
Public Class frmMrpMaterialCode

#Region "屬性"
    Dim ds As New DataSet
    Dim mrpSQcon As New MrpSupplierQuotationController
    Dim MWHIEcon As New MrpWareHouseInfoEntryController
    Dim MMICcon As New MrpMaterialCodeController
    Dim sms As New SystemUserController
    Dim sccon As New SuppliersControler
    Dim dt As New DataTable

    Private boolOld As Boolean
    Private _EditItem As String '屬性欄位
    Private _EditValue As String
    Private _M_Code_List As List(Of String)
    Property EditItem() As String '屬性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property EditValue() As String '屬性
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property
#End Region

#Region "載入事件"
    ''' <summary>
    ''' 載入事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMrpMaterialCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        dt = sccon.Suppliers_GetDataTable(Nothing)
        GridControl1.DataSource = dt

        gluS_Supplier.Properties.DataSource = dt
        '產品信息
        gueM_Code.Properties.DataSource = MMICcon.MaterialCode_GetList(Nothing)
        '倉庫信息
        gueMC_WH_ID.Properties.DataSource = MMICcon.MrpMaterialCode_GetWareHouseInfo(Nothing)
        '來源別
        gueSource.Properties.DataSource = MMICcon.MrpSource_GetList(Nothing, Nothing)
        '請購人員
        txtMC_OrderMan.Properties.DataSource = sms.SystemUser_GetList(Nothing, Nothing, Nothing)

        Select Case EditItem
            Case EditEnumType.ADD
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.ADD)
                Me.Text = lblinfo.Text
                gueM_Code.Enabled = True
                EnableOrDisable(True)
                gueSource.EditValue = "P"
            Case EditEnumType.EDIT
                EnableOrDisable(True)
                gueM_Code.Enabled = False
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.EDIT)
                Me.Text = lblinfo.Text
                LoadData(EditValue) '載入數據
            Case EditEnumType.VIEW
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.VIEW)
                Me.Text = lblinfo.Text
                EnableOrDisable(False)
                LoadData(EditValue)
                cmdSave.Visible = False
                xtpCheck.PageVisible = True
                xtcTable.SelectedTabPage = xtpGridView
                '-----------------------------------------------
                GridView.OptionsBehavior.AutoSelectAllInEditor = False
                GridView.OptionsBehavior.Editable = False
                GridView.OptionsSelection.EnableAppearanceFocusedCell = False
                Grid1.ContextMenuStrip = Nothing
                Panel4.Enabled = False
            Case EditEnumType.CHECK

                xtpCheck.PageVisible = True
                xtcTable.SelectedTabPage = xtpCheck
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.CHECK)
                Me.Text = lblinfo.Text
                EnableOrDisable(False)
                LoadData(EditValue)
                cmdSave.Enabled = True
                GridView.OptionsBehavior.AutoSelectAllInEditor = False
                GridView.OptionsBehavior.Editable = False
                GridView.OptionsSelection.EnableAppearanceFocusedCell = False
                Grid1.ContextMenuStrip = Nothing
            Case EditEnumType.ELSEONE
                EnableOrDisable(True)
                gueM_Code.Enabled = False
                lblinfo.Text = lblinfo.Text + "選擇供應商"
                Me.Text = lblinfo.Text
                LoadData(EditValue) '載入數據
                gueSource.Enabled = False
                chkMC_MRPCon.Enabled = False
                meeMC_Remark.Enabled = False
                GroupBox3.Enabled = True
                GroupBox1.Enabled = True
                Grid1.Enabled = True
            Case EditEnumType.ELSETWO
                EnableOrDisable(True)
                gueM_Code.Enabled = False
                lblinfo.Text = lblinfo.Text + "選擇倉儲"
                Me.Text = lblinfo.Text
                LoadData(EditValue) '載入數據
                gueSource.Enabled = False
                chkMC_MRPCon.Enabled = False
                meeMC_Remark.Enabled = False
                GroupBox2.Enabled = True
        End Select
    End Sub
#End Region

#Region "創建臨時子表"
    Private Sub CreateTable()
        ds.Tables.Clear()
        '創建臨時子表
        With ds.Tables.Add("MrpMaterialCodeQuotation")

            .Columns.Add("DefaultBit", GetType(Boolean))
            .Columns.Add("MCode", GetType(String))
            .Columns.Add("S_Supplier", GetType(String))
            .Columns.Add("S_SupplierName", GetType(String))
            .Columns.Add("EconomicQty", GetType(String))
            .Columns.Add("OrderQty", GetType(String))
            .Columns.Add("DeliveryDate", GetType(String))
            .Columns.Add("MaxQty", GetType(String))
            .Columns.Add("MinQty", GetType(String))
            .Columns.Add("ResponsibleUserID", GetType(String))
            .Columns.Add("UnitPrice", GetType(String))
            .Columns.Add("Remark", GetType(String))
            .Columns.Add("CurrencyName", GetType(String))
            .Columns.Add("AutoID", GetType(Decimal))

        End With
        Grid1.DataSource = ds.Tables("MrpMaterialCodeQuotation")
        '創建臨時刪除表
        With ds.Tables.Add("MrpMaterialCodeQuotationDel")
            .Columns.Add("AutoID", GetType(Decimal))
        End With
    End Sub
#End Region

#Region "添加數據"
    ''' <summary>
    ''' 添加數據
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataAdd()
        '1.主檔-------------新增
        Dim MMCI As New MrpMaterialCodeInfo
        MMCI.M_Code = gueM_Code.EditValue
        MMCI.MC_Source = gueSource.EditValue

        MMCI.MC_BatchQty = caeMC_BatchQty.EditValue
        MMCI.MC_QtyMax = caeMC_QtyMax.EditValue
        MMCI.MC_QtyMin = caeMC_QtyMin.EditValue
        MMCI.MC_BatFixEconomy = caeMC_BatFixEconomy.EditValue
        MMCI.MC_WH_ID = gueMC_WH_ID.EditValue
        MMCI.MC_SecInv = caeMC_SecInv.EditValue
        MMCI.MC_OrderInterVal = caeMC_OrderInterVal.EditValue
        MMCI.MC_LowLimit = caeMC_LowLimit.EditValue
        MMCI.MC_OrderMan = txtMC_OrderMan.EditValue

        MMCI.M_Supplier = gluS_Supplier.EditValue
        MMCI.MC_MRPCon = chkMC_MRPCon.Checked
        MMCI.MC_Remark = meeMC_Remark.EditValue
        MMCI.CreateUserID = InUserID
        MMCI.CreateDate = Format(System.DateTime.Now, "yyyy/MM/dd")

        If MMICcon.MrpMaterialCode_Insert(MMCI) = False Then
            MsgBox("保存失敗，請檢查原因！", 60, "提示")
            Me.Close()
            Exit Sub
        End If
        '2.子檔-------------新增

        For i As Integer = 0 To ds.Tables("MrpMaterialCodeQuotation").Rows.Count - 1
            With ds.Tables("MrpMaterialCodeQuotation")

                Dim msqInfo As New MrpSupplierQuotationInfo
                msqInfo.CreateUserID = InUserID
                msqInfo.MCode = gueM_Code.EditValue
                msqInfo.DefaultBit = IIf(IsDBNull(.Rows(i)("DefaultBit")), False, .Rows(i)("DefaultBit"))
                msqInfo.DeliveryDate = IIf(IsDBNull(.Rows(i)("DeliveryDate")), Nothing, .Rows(i)("DeliveryDate"))

                msqInfo.EconomicQty = IIf(IsDBNull(.Rows(i)("EconomicQty")), Nothing, .Rows(i)("EconomicQty"))
                msqInfo.MaxQty = IIf(IsDBNull(.Rows(i)("MaxQty")), Nothing, .Rows(i)("MaxQty"))
                msqInfo.MinQty = IIf(IsDBNull(.Rows(i)("MinQty")), Nothing, .Rows(i)("MinQty"))
                msqInfo.OrderQty = IIf(IsDBNull(.Rows(i)("OrderQty")), Nothing, .Rows(i)("OrderQty"))
                msqInfo.Remark = IIf(IsDBNull(.Rows(i)("Remark")), Nothing, .Rows(i)("Remark"))
                msqInfo.S_Supplier = IIf(IsDBNull(.Rows(i)("S_Supplier")), Nothing, .Rows(i)("S_Supplier"))
                msqInfo.UnitPrice = IIf(IsDBNull(.Rows(i)("UnitPrice")), Nothing, .Rows(i)("UnitPrice"))
                msqInfo.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID"))

                If mrpSQcon.MRPSupplierQuotation_Add(msqInfo) = False Then
                    MsgBox("保存失敗，請檢查原因！", 60, "提示")
                    Me.Close()
                    Exit Sub
                End If
            End With
        Next

        MsgBox("保存成功！", 60, "提示")
        Me.Close()
        Exit Sub

    End Sub
#End Region

#Region "編輯數據"
    ''' <summary>
    ''' 編輯數據
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '1.刪除臨時數據

        For i As Integer = 0 To ds.Tables("MrpMaterialCodeQuotationDel").Rows.Count - 1
            Dim intAutoID As Integer = ds.Tables("MrpMaterialCodeQuotationDel").Rows(i)("AutoID")
            If mrpSQcon.MRPSupplierQuotation_Delete(Nothing, intAutoID) = False Then
                MsgBox("1.刪除子表出錯！")
                Exit Sub
            End If
        Next

        '2.主檔-------------修改
        Dim MMCI As New MrpMaterialCodeInfo
        MMCI.M_Code = gueM_Code.Text
        MMCI.MC_Source = gueSource.EditValue

        MMCI.MC_BatchQty = caeMC_BatchQty.EditValue
        MMCI.MC_QtyMax = caeMC_QtyMax.EditValue
        MMCI.MC_QtyMin = caeMC_QtyMin.EditValue
        MMCI.MC_BatFixEconomy = caeMC_BatFixEconomy.EditValue
        MMCI.MC_WH_ID = gueMC_WH_ID.EditValue
        MMCI.MC_SecInv = caeMC_SecInv.EditValue
        MMCI.MC_OrderInterVal = caeMC_OrderInterVal.EditValue
        MMCI.MC_LowLimit = caeMC_LowLimit.EditValue
        MMCI.M_Supplier = gluS_Supplier.EditValue

        MMCI.MC_OrderMan = txtMC_OrderMan.EditValue
        MMCI.MC_MRPCon = chkMC_MRPCon.Checked
        MMCI.ModifyUserID = InUserID
        MMCI.ModifyDate = Format(System.DateTime.Now, "yyyy/MM/dd")
        MMCI.MC_Remark = meeMC_Remark.EditValue

        If MMICcon.MrpMaterialCode_Update(MMCI) = False Then
            MsgBox("修改失敗，請檢查原因！", 60, "提示")
            Me.Close()
            Exit Sub
        End If

        '3.子檔-------------修改
        For i As Integer = 0 To ds.Tables("MrpMaterialCodeQuotation").Rows.Count - 1
            With ds.Tables("MrpMaterialCodeQuotation")
                Dim msqInfo As New MrpSupplierQuotationInfo
                msqInfo.MCode = gueM_Code.Text
                msqInfo.CreateUserID = InUserID
                msqInfo.DeliveryDate = IIf(IsDBNull(.Rows(i)("DeliveryDate")), Nothing, .Rows(i)("DeliveryDate"))
                msqInfo.DefaultBit = IIf(IsDBNull(.Rows(i)("DefaultBit")), False, .Rows(i)("DefaultBit"))
                msqInfo.EconomicQty = IIf(IsDBNull(.Rows(i)("EconomicQty")), Nothing, .Rows(i)("EconomicQty"))
                msqInfo.MaxQty = IIf(IsDBNull(.Rows(i)("MaxQty")), Nothing, .Rows(i)("MaxQty"))
                msqInfo.MinQty = IIf(IsDBNull(.Rows(i)("MinQty")), Nothing, .Rows(i)("MinQty"))
                msqInfo.OrderQty = IIf(IsDBNull(.Rows(i)("OrderQty")), Nothing, .Rows(i)("OrderQty"))
                msqInfo.Remark = IIf(IsDBNull(.Rows(i)("Remark")), Nothing, .Rows(i)("Remark"))
                msqInfo.S_Supplier = IIf(IsDBNull(.Rows(i)("S_Supplier")), Nothing, .Rows(i)("S_Supplier"))
                msqInfo.UnitPrice = IIf(IsDBNull(.Rows(i)("UnitPrice")), Nothing, .Rows(i)("UnitPrice"))
                msqInfo.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID"))
                If .Rows(i)("AutoID") = 0 Then
                    If mrpSQcon.MRPSupplierQuotation_Add(msqInfo) = False Then
                        MsgBox("修改失敗，請檢查原因！", 60, "提示")
                        Exit Sub
                    End If
                Else
                    If mrpSQcon.MRPSupplierQuotation_Update(msqInfo) = False Then
                        MsgBox("修改失敗，請檢查原因！", 60, "提示")
                        Exit Sub
                    End If
                End If

            End With
        Next

        MsgBox("保存成功！", 60, "提示")
        Me.Close()
    End Sub
#End Region

#Region "審核數據"
    Sub DataCheck()
        If chkCheckBit.Checked = boolOld Then
            MsgBox("審核狀態沒有改變,請檢查原因！", 60, "提示")
            Exit Sub
        End If

        Dim MMCI As New MrpMaterialCodeInfo
        MMCI.M_Code = gueM_Code.Text
        MMCI.CheckUserID = InUserID
        MMCI.CheckBit = chkCheckBit.Checked
        MMCI.CheckRemark = txtCheckRemark.Text

        If MMICcon.MrpMaterialCode_UpdateCheck(MMCI) = True Then
            If chkCheckBit.Checked = True Then
                MsgBox("審核成功!", 60, "提示")
            Else
                MsgBox("取消審核成功!", 60, "提示")
            End If
            Me.Close()
            Exit Sub
        Else
            MsgBox("審核失敗，請檢查原因！", 60, "提示")
            Me.Close()
            Exit Sub
        End If
    End Sub
#End Region

#Region "判空"
    Function DataCheckEmpty() As Integer
        '1.主檔
        If gueM_Code.Text = String.Empty Then
            MsgBox("產品編號不能為空,請輸入！", MsgBoxStyle.Information, "提示")
            gueM_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If Me.gueSource.Text = String.Empty Then
            MsgBox("來源別不能為空,請輸入！", MsgBoxStyle.Information, "提示")
            gueSource.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        '1.子表
        Dim boolDefaultBit As Boolean = False
        For i As Integer = 0 To GridView.RowCount - 1
            If IsDBNull(GridView.GetRowCellValue(i, "S_Supplier")) Then
                MsgBox(" 第 " & i + 1 & " 行的供應商編號不能為空！")
                GridView.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If GridView.GetRowCellValue(i, "S_Supplier") = String.Empty Then
                MsgBox(" 第 " & i + 1 & " 行的供應商編號不能為空！")
                GridView.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(GridView.GetRowCellValue(i, "UnitPrice")) Then
                MsgBox(" 第 " & i + 1 & " 行的單價不能為空！")
                GridView.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If GridView.GetRowCellValue(i, "UnitPrice") = String.Empty Then
                MsgBox(" 第 " & i + 1 & " 行的單價不能為空！")
                GridView.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            Dim minQty, maxQty As Integer
            minQty = IIf(IsDBNull(ds.Tables("MrpMaterialCodeQuotation").Rows(i)("MinQty")), Nothing, ds.Tables("MrpMaterialCodeQuotation").Rows(i)("MinQty"))  '最小訂購量
            maxQty = IIf(IsDBNull(ds.Tables("MrpMaterialCodeQuotation").Rows(i)("MaxQty")), Nothing, ds.Tables("MrpMaterialCodeQuotation").Rows(i)("MaxQty"))  '最大訂購量
            If minQty > maxQty Then
                MsgBox(" 第 " & i + 1 & " 行的最小訂購量大于最大訂購量！")
                GridView.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If ds.Tables("MrpMaterialCodeQuotation").Rows(i)("DefaultBit") = True Then
                Dim strS_Supplier = GridView.GetFocusedRowCellValue("S_Supplier")
                If strS_Supplier <> String.Empty Then
                    gluS_Supplier.EditValue = strS_Supplier
                    caeMC_BatFixEconomy.Text = IIf(IsDBNull(ds.Tables("MrpMaterialCodeQuotation").Rows(i)("EconomicQty")), Nothing, ds.Tables("MrpMaterialCodeQuotation").Rows(i)("EconomicQty")) '經濟批量
                    caeMC_BatchQty.Text = IIf(IsDBNull(ds.Tables("MrpMaterialCodeQuotation").Rows(i)("OrderQty")), Nothing, ds.Tables("MrpMaterialCodeQuotation").Rows(i)("OrderQty"))  '訂貨批量
                    caeMC_OrderInterVal.Text = IIf(IsDBNull(ds.Tables("MrpMaterialCodeQuotation").Rows(i)("DeliveryDate")), Nothing, ds.Tables("MrpMaterialCodeQuotation").Rows(i)("DeliveryDate")) '交貨天數
                    caeMC_QtyMax.Text = maxQty.ToString  '最大訂購量
                    caeMC_QtyMin.Text = minQty.ToString
                End If
                boolDefaultBit = True
            End If
        Next
        If boolDefaultBit = False And GridView.RowCount > 0 Then
            MsgBox("您沒有選擇默認廠商！")
            GridView.FocusedRowHandle = 0
            DataCheckEmpty = 0
            Exit Function
        End If

        DataCheckEmpty = 1
    End Function
#End Region

#Region "保存/取消事件"
    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If DataCheckEmpty() = 0 Then
            Exit Sub
        End If
        Select Case EditItem
            Case EditEnumType.ADD
                DataAdd()
            Case EditEnumType.EDIT
                DataEdit()
            Case EditEnumType.CHECK
                DataCheck()
            Case EditEnumType.ELSEONE
                DataSupplier()
            Case EditEnumType.ELSETWO
                DataStock()
        End Select
    End Sub
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region

#Region "表格菜單事件"

    Private Sub tsmNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNew.Click
        Dim row As DataRow = ds.Tables("MrpMaterialCodeQuotation").NewRow
        row("DefaultBit") = False
        row("MCode") = String.Empty
        row("S_Supplier") = String.Empty
        row("AutoID") = 0
        ds.Tables("MrpMaterialCodeQuotation").Rows.Add(row)
        GridView.FocusedRowHandle = GridView.RowCount - 1
    End Sub

    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If GridView.RowCount > 0 Then

            Dim deltemp As Integer = 0
            deltemp = ds.Tables("MrpMaterialCodeQuotation").Rows(GridView.FocusedRowHandle)("AutoID").ToString
            ds.Tables("MrpMaterialCodeQuotation").Rows.RemoveAt(GridView.FocusedRowHandle)

            Dim row As DataRow
            If deltemp <> 0 Then
                row = ds.Tables("MrpMaterialCodeQuotationDel").NewRow
                row("AutoID") = deltemp
                ds.Tables("MrpMaterialCodeQuotationDel").Rows.Add(row)
            End If
        Else
            Exit Sub
        End If
    End Sub
#End Region

#Region "載入數據"
    Sub LoadData(ByVal StrM_Code As String)
        '主表數據返回-----------------------------------------------
        Dim MWHI_list As New List(Of MrpMaterialCodeInfo)
        MWHI_list = MMICcon.MrpMaterialCode_GetList(StrM_Code, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If MWHI_list.Count = 0 Then
            Exit Sub
        Else
            gueM_Code.Text = MWHI_list(0).M_Code
            caeMC_BatchQty.Text = MWHI_list(0).MC_BatchQty
            caeMC_QtyMax.Text = MWHI_list(0).MC_QtyMax
            caeMC_QtyMin.Text = MWHI_list(0).MC_QtyMin
            caeMC_BatFixEconomy.Text = MWHI_list(0).MC_BatFixEconomy
            gueMC_WH_ID.EditValue = MWHI_list(0).MC_WH_ID
            caeMC_SecInv.Text = MWHI_list(0).MC_SecInv
            caeMC_OrderInterVal.Text = MWHI_list(0).MC_OrderInterVal
            caeMC_LowLimit.Text = MWHI_list(0).MC_LowLimit
            gueSource.EditValue = MWHI_list(0).MC_SourceID
            txtMC_OrderMan.EditValue = MWHI_list(0).MC_OrderMan
            chkMC_MRPCon.Checked = MWHI_list(0).MC_MRPCon

            If MWHI_list(0).CheckBit = True Then
                chkCheckBit.Checked = True
                lblCheckUserID.Text = MWHI_list(0).CheckUserID
                lblCheckDate.Text = IIf(MWHI_list(0).CheckDate = Nothing, "", Format(CDate(MWHI_list(0).CheckDate), "yyyy/MM/dd"))
                txtCheckRemark.Text = MWHI_list(0).CheckRemark
            ElseIf EditItem = EditEnumType.CHECK Then
                lblCheckUserID.Text = InUser
                lblCheckDate.Text = Format(Now, "yyyy/MM/dd")
            End If

            boolOld = MWHI_list(0).CheckBit
            gluS_Supplier.EditValue = MWHI_list(0).M_Supplier

            txtM_Gauge.Text = MWHI_list(0).M_Gauge
            txtM_Name.Text = MWHI_list(0).M_Name
            txtM_Unit.Text = MWHI_list(0).M_Unit
        End If
        '2.子表數據返回----------------------------------------------

        Dim msList As New List(Of MrpSupplierQuotationInfo)
        ds.Tables("MrpMaterialCodeQuotation").Clear()

        msList = mrpSQcon.MRPSupplierQuotation_GetList(StrM_Code, Nothing, Nothing)
        If msList.Count <= 0 Then
            Exit Sub
        Else
            Dim i As Integer
            For i = 0 To msList.Count - 1
                Dim row As DataRow
                row = ds.Tables("MrpMaterialCodeQuotation").NewRow

                row("DefaultBit") = msList(i).DefaultBit
                row("AutoID") = msList(i).AutoID
                row("DeliveryDate") = msList(i).DeliveryDate
                row("EconomicQty") = msList(i).EconomicQty
                row("MaxQty") = msList(i).MaxQty
                row("MCode") = msList(i).MCode
                row("MinQty") = msList(i).MinQty
                row("OrderQty") = msList(i).OrderQty
                row("Remark") = msList(i).Remark
                row("S_Supplier") = msList(i).S_Supplier
                row("S_SupplierName") = msList(i).S_SupplierName
                row("UnitPrice") = msList(i).UnitPrice
                row("CurrencyName") = msList(i).CurrencyName

                ds.Tables("MrpMaterialCodeQuotation").Rows.Add(row)
            Next
        End If
    End Sub
#End Region

#Region "控制控件enable屬性--ture/false"
    ''' <summary>
    ''' 控制控件enable屬性--ture/false
    ''' </summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Sub EnableOrDisable(ByVal state As Boolean)
        gueM_Code.Enabled = state
        gueSource.Enabled = state
        chkMC_MRPCon.Enabled = state
        gueMC_WH_ID.Enabled = state
        caeMC_LowLimit.Enabled = state
        caeMC_SecInv.Enabled = state
        caeMC_BatFixEconomy.Enabled = state
        caeMC_BatchQty.Enabled = state
        caeMC_OrderInterVal.Enabled = state
        caeMC_QtyMax.Enabled = state
        caeMC_QtyMin.Enabled = state
        txtMC_OrderMan.Enabled = state
        meeMC_Remark.Enabled = state
        gluS_Supplier.Enabled = state
    End Sub

#End Region

#Region "通過M_Code獲得其他信息"

    Private Sub GridView3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gueM_Code.EditValueChanged
        txtM_Name.Text = GridView3.GetFocusedRowCellValue("M_Name")
        txtM_Gauge.Text = GridView3.GetFocusedRowCellValue("M_Gauge")
        txtM_Unit.Text = GridView3.GetFocusedRowCellValue("M_Unit")
    End Sub
    Private Sub gueM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gueM_Code.EditValueChanged
        'MsgBox("該產品編碼已添加，請重新選擇！")
        If EditItem = EditEnumType.ADD Then
            If gueM_Code.EditValue = String.Empty Then
            Else
                Select Case Mid(gueM_Code.EditValue, 1, 2)
                    Case "MG"
                        gueSource.EditValue = "C"
                    Case "30"
                        gueSource.EditValue = "T"
                    Case Else
                        gueSource.EditValue = "P"
                End Select

                Dim MWHI_list As New List(Of MrpMaterialCodeInfo)
                MWHI_list = MMICcon.MrpMaterialCode_GetList(gueM_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If MWHI_list.Count > 0 Then
                    MsgBox("該產品編碼已添加，請重新選擇！", MsgBoxStyle.Information, "提示")
                    gueM_Code.EditValue = String.Empty
                    gueM_Code.Focus()
                End If

            End If
        End If
    End Sub

    Private Sub GridView4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView4.Click
        Dim Str As String = GridView4.GetFocusedRowCellValue("S_Supplier")

        For i As Integer = 0 To ds.Tables("MrpMaterialCodeQuotation").Rows.Count - 1
            If IsDBNull(ds.Tables("MrpMaterialCodeQuotation").Rows(i)("S_Supplier")) Then
                Continue For
            End If
            If ds.Tables("MrpMaterialCodeQuotation").Rows(i)("S_Supplier") = Str Then
                MsgBox("此供應商報價已經添加！")
                Exit Sub
            End If
        Next

        GridView.SetFocusedRowCellValue(GridView.Columns("S_Supplier"), GridView4.GetFocusedRowCellValue("S_Supplier"))
        GridView.SetFocusedRowCellValue(GridView.Columns("S_SupplierName"), GridView4.GetFocusedRowCellValue("S_SupplierName"))
        GridView.SetFocusedRowCellValue(GridView.Columns("CurrencyName"), GridView4.GetFocusedRowCellValue("CurrencyName"))
    End Sub
#End Region

#Region "數據值驗證"
    Private Sub caeMC_BatFixEconomy_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles caeMC_BatFixEconomy.EditValueChanging, caeMC_BatchQty.EditValueChanging, caeMC_OrderInterVal.EditValueChanging, caeMC_QtyMax.EditValueChanging, caeMC_QtyMin.EditValueChanging, caeMC_LowLimit.EditValueChanging, caeMC_SecInv.EditValueChanging, RepositoryItemCalcEdit1.EditValueChanging
        If (CDec(e.NewValue) > 999999999 Or CDec(e.NewValue) < 0) Then
            e.Cancel = True
        End If
        If sender.name.Equals("caeMC_OrderInterVal") And CDec(e.NewValue) > 1000 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub chkDefaultBit_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDefaultBit.EditValueChanged
        With ds.Tables("MrpMaterialCodeQuotation")
            For i As Integer = 0 To ds.Tables("MrpMaterialCodeQuotation").Rows.Count - 1
                If GridView.FocusedRowHandle = i Then
                    gluS_Supplier.EditValue = GridView.GetFocusedRowCellValue("S_Supplier")
                    caeMC_BatFixEconomy.Text = IIf(IsDBNull(.Rows(i)("EconomicQty")), Nothing, .Rows(i)("EconomicQty")) '經濟批量
                    caeMC_BatchQty.Text = IIf(IsDBNull(.Rows(i)("OrderQty")), Nothing, .Rows(i)("OrderQty"))  '訂貨批量
                    caeMC_OrderInterVal.Text = IIf(IsDBNull(.Rows(i)("DeliveryDate")), Nothing, .Rows(i)("DeliveryDate")) '交貨天數
                    caeMC_QtyMax.Text = IIf(IsDBNull(.Rows(i)("MaxQty")), Nothing, .Rows(i)("MaxQty")) '最大訂購量
                    caeMC_QtyMin.Text = IIf(IsDBNull(.Rows(i)("MinQty")), Nothing, .Rows(i)("MinQty"))
                    ds.Tables("MrpMaterialCodeQuotation").Rows(i)("DefaultBit") = True
                Else
                    ds.Tables("MrpMaterialCodeQuotation").Rows(i)("DefaultBit") = False
                End If
            Next
        End With
    End Sub
#End Region

    Sub DataSupplier()
        '2.主檔-------------修改
        Dim MMCI As New MrpMaterialCodeInfo
        MMCI.M_Code = gueM_Code.Text
        MMCI.MC_BatchQty = caeMC_BatchQty.EditValue
        MMCI.MC_QtyMax = caeMC_QtyMax.EditValue
        MMCI.MC_QtyMin = caeMC_QtyMin.EditValue
        MMCI.MC_BatFixEconomy = caeMC_BatFixEconomy.EditValue
        MMCI.MC_OrderInterVal = caeMC_OrderInterVal.EditValue
        MMCI.M_Supplier = gluS_Supplier.EditValue
        MMCI.MC_OrderMan = txtMC_OrderMan.EditValue
        MMCI.ModifyUserID = InUserID
        If MMICcon.MrpMaterialCode_Supplier(MMCI) = False Then
            MsgBox("保存失敗，請檢查原因！", 60, "提示")
            Exit Sub
        End If

        '3.子檔-------------修改
        For i As Integer = 0 To ds.Tables("MrpMaterialCodeQuotation").Rows.Count - 1
            With ds.Tables("MrpMaterialCodeQuotation")
                Dim msqInfo As New MrpSupplierQuotationInfo
                msqInfo.MCode = gueM_Code.Text
                msqInfo.CreateUserID = InUserID
                msqInfo.DeliveryDate = IIf(IsDBNull(.Rows(i)("DeliveryDate")), Nothing, .Rows(i)("DeliveryDate"))
                msqInfo.DefaultBit = IIf(IsDBNull(.Rows(i)("DefaultBit")), False, .Rows(i)("DefaultBit"))
                msqInfo.EconomicQty = IIf(IsDBNull(.Rows(i)("EconomicQty")), Nothing, .Rows(i)("EconomicQty"))
                msqInfo.MaxQty = IIf(IsDBNull(.Rows(i)("MaxQty")), Nothing, .Rows(i)("MaxQty"))
                msqInfo.MinQty = IIf(IsDBNull(.Rows(i)("MinQty")), Nothing, .Rows(i)("MinQty"))
                msqInfo.OrderQty = IIf(IsDBNull(.Rows(i)("OrderQty")), Nothing, .Rows(i)("OrderQty"))
                msqInfo.Remark = IIf(IsDBNull(.Rows(i)("Remark")), Nothing, .Rows(i)("Remark"))
                msqInfo.S_Supplier = IIf(IsDBNull(.Rows(i)("S_Supplier")), Nothing, .Rows(i)("S_Supplier"))
                msqInfo.UnitPrice = IIf(IsDBNull(.Rows(i)("UnitPrice")), Nothing, .Rows(i)("UnitPrice"))
                msqInfo.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID"))
                If .Rows(i)("AutoID") = 0 Then
                    If mrpSQcon.MRPSupplierQuotation_Add(msqInfo) = False Then
                        MsgBox("保存失敗，請檢查原因！", 60, "提示")
                        Exit Sub
                    End If
                Else
                    If mrpSQcon.MRPSupplierQuotation_Update(msqInfo) = False Then
                        MsgBox("保存失敗，請檢查原因！", 60, "提示")
                        Exit Sub
                    End If
                End If
            End With
        Next
        MsgBox("供應商設置成功！", 60, "提示")
        Me.Close()
    End Sub

    Sub DataStock()
        '2.主檔-------------修改
        Dim MMCI As New MrpMaterialCodeInfo
        MMCI.M_Code = gueM_Code.Text
        MMCI.MC_WH_ID = gueMC_WH_ID.EditValue
        MMCI.MC_LowLimit = caeMC_LowLimit.EditValue
        MMCI.MC_SecInv = caeMC_SecInv.EditValue


        If MMICcon.MrpMaterialCode_Stock(MMCI) = False Then
            MsgBox("修改倉儲失敗，請檢查原因！", 60, "提示")
            Me.Close()
            Exit Sub
        Else
            MsgBox("倉儲設置成功！", 60, "提示")
            Me.Close()
        End If

    End Sub

End Class