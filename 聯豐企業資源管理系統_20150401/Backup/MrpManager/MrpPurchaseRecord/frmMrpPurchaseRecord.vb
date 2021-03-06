Imports LFERP.Library.MrpManager.MrpPurchaseRecord
Imports LFERP.Library.MrpManager.MrpSupplierQuotation
Imports LFERP.DataSetting
Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.MrpManager.MrpInfo
Imports LFERP.Library.WareHouse
Imports LFERP.Library.MrpManager.MrpMaterialCode

Public Class frmMrpPurchaseRecord
#Region "屬性"
    Dim ds As New DataSet
    Private _EditItem As String
    Private _MrpPurchaseID As String
    Private boolChcek As Boolean

    Private _Type As String
    Dim micon As New MrpInfoController
    Dim mpcon As New MrpPurchaseRecordController
    Dim mpecon As New MrpPurchaseRecordEntryController
    Dim dptCon As New DepartmentControler
    Dim suCon As New SystemUserController
    Dim mrpRecordInfo As New MrpPurchaseRecordInfo
    Dim wmc As New WareHouseController
    Dim mmc As New MrpMaterialCodeController

    Public Property EditItem() As String
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Public Property MrpPurchaseID() As String
        Get
            Return _MrpPurchaseID
        End Get
        Set(ByVal value As String)
            _MrpPurchaseID = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

#End Region

#Region "窗本載入"
    Private Sub frmMrpPurchaseRecord_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If EditItem = EditEnumType.ADD Then
            GridMaterial1.DataSource = mmc.MrpMaterialCode_GetList(Nothing, Nothing, "True", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Else
            GridMaterial1.DataSource = mmc.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        CreateTable()

        '來源別
        GLU_MRPID.Properties.DisplayMember = "MRP_ID"    'txt
        GLU_MRPID.Properties.ValueMember = "MRP_ID"   'EditValue
        GLU_MRPID.Properties.DataSource = micon.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
        '請購部門
        glu_PurchaseDepartment.Properties.DisplayMember = "DPT_Name" 'txt
        glu_PurchaseDepartment.Properties.ValueMember = "DPT_ID"  'EditValue
        glu_PurchaseDepartment.Properties.DataSource = dptCon.Department_GetList(Nothing, Nothing, Nothing)
        '來源別
        glu_PurchaseUserID.Properties.DisplayMember = "U_Name"    'txt
        glu_PurchaseUserID.Properties.ValueMember = "U_ID"   'EditValue
        glu_PurchaseUserID.Properties.DataSource = suCon.SystemUser_GetList(Nothing, Nothing, Nothing)

        gluWareHouse.Properties.DataSource = wmc.WareHouse_GetDataTable(Nothing)

        Select Case EditItem
            Case EditEnumType.ADD
                Lbl_Title.Text = Lbl_Title.Text + EditEnumValue(EditEnumType.ADD)
                Me.Text = Lbl_Title.Text

                txt_MPP_Remark.Enabled = True
                xtpCheck.PageVisible = False
                txt_MP_CreateUserName.Text = InUserID
                txt_MrpPurchID.Text = mpcon.MrpPurchaseRecord_GetNewMrpID()

                det_PurchaseDate.Text = Format(Now, "yyyy/MM/dd")
                SupplierName.Visible = False
                UnitPrice.Visible = False
            Case EditEnumType.EDIT
                Lbl_Title.Text = Lbl_Title.Text + EditEnumValue(EditEnumType.EDIT)
                Me.Text = Lbl_Title.Text
                LoadTable()

                txt_MPP_Remark.Enabled = True
                GLU_MRPID.Enabled = False
                xtpCheck.PageVisible = False
                Me.Remark.OptionsColumn.AllowEdit = True
                SupplierName.Visible = False
                UnitPrice.Visible = False
            Case EditEnumType.ELSEONE               '供應商報價
                Lbl_Title.Text = Lbl_Title.Text + EditEnumValue(EditEnumType.ELSEONE)
                Me.Text = Lbl_Title.Text
                LoadTable()
                txt_MPP_Remark.Enabled = True
                GLU_MRPID.Enabled = False
                xtpCheck.PageVisible = False
                Me.MRP_ID.OptionsColumn.AllowEdit = False
                Me.M_Code.OptionsColumn.AllowEdit = False
                Me.MPI_NeedQty.OptionsColumn.AllowEdit = False
                Me.MPI_NeedDate.OptionsColumn.AllowEdit = False
                Me.ForecastDate.OptionsColumn.AllowEdit = False
                Me.SupplierName.OptionsColumn.AllowEdit = True
                SupplierName.Visible = True

                Grid1.ContextMenuStrip = Nothing
                txt_MPP_Remark.Enabled = False
                Me.Remark.OptionsColumn.AllowEdit = True
                GroupBox1.Enabled = False

            Case EditEnumType.CHECK
                Lbl_Title.Text = Lbl_Title.Text + EditEnumValue(EditEnumType.CHECK)
                Me.Text = Lbl_Title.Text
                LoadTable()

                xtpCheck.PageVisible = True
                xtcTable.SelectedTabPageIndex = 1
                SetReadOnly()
            Case EditEnumType.VIEW
                Lbl_Title.Text = Lbl_Title.Text + EditEnumValue(EditEnumType.VIEW)
                Me.Text = Lbl_Title.Text
                LoadTable()
                SetReadOnly()
                cmdSave.Enabled = False
                chkCheckBit.Enabled = False
                txtCheckRemark.Enabled = False
        End Select
    End Sub
#End Region

#Region "創建臨時表"
    Private Sub CreateTable()
        ds.Tables.Clear()
        '創建臨時子表
        With ds.Tables.Add("MrpPurchaseRecordEntry")
            .Columns.Add("MRP_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("MPI_NeedQty", GetType(Double))
            .Columns.Add("MPI_CreateUserID", GetType(String))
            .Columns.Add("MPI_CreateDate", GetType(Date))
            .Columns.Add("MPI_ModifyUserID", GetType(String))
            .Columns.Add("MPI_ModifyDate", GetType(Date))
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("MrpPurchaseID", GetType(String))
            .Columns.Add("MPI_CreateUserName", GetType(String))
            .Columns.Add("MPI_ModifyUserName", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Source", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("MPI_NeedDate", GetType(Date))
            .Columns.Add("S_Supplier", GetType(String))
            .Columns.Add("S_SupplierName", GetType(String))
            .Columns.Add("MPI_Remark", GetType(String))
            .Columns.Add("ForecastDate", GetType(Date))
            .Columns.Add("UnitPrice", GetType(Decimal))
        End With

        Grid1.DataSource = ds.Tables("MrpPurchaseRecordEntry")
        '創建臨時刪除表
        With ds.Tables.Add("DelTable")
            .Columns.Add("AutoID", GetType(Decimal))
        End With
    End Sub
#End Region

#Region "返回數據"
    Private Sub LoadTable()
        '1.主表數據返回
        Dim mprList As New List(Of MrpPurchaseRecordInfo)
        mprList = mpcon.MrpPurchaseRecord_GetList(MrpPurchaseID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If mprList.Count > 0 Then
            txt_MPP_Remark.Text = mprList(0).MPP_Remark
            txt_MP_CreateUserName.Text = mprList(0).MPP_CreateUserID
            GLU_MRPID.Text = mprList(0).MRP_ID
            txt_MrpPurchID.Text = mprList(0).MrpPurchaseID

            boolChcek = mprList(0).CheckBit
            chkCheckBit.Checked = mprList(0).CheckBit
            txtCheckRemark.Text = mprList(0).CheckRemark
            If mprList(0).CheckBit = True Then
                lblCheckDate.Text = Format(mprList(0).MPP_CheckDate, "yyyy/MM/dd")
                lblCheckUserID.Text = mprList(0).MPP_CheckUserName
            ElseIf EditItem = EditEnumType.CHECK Then
                lblCheckDate.Text = Format(Now, "yyyy/MM/dd")
                lblCheckUserID.Text = InUser
            End If

            det_PurchaseDate.EditValue = IIf(mprList(0).PurchaseDate.Year = 1, Nothing, mprList(0).PurchaseDate)
            glu_PurchaseUserID.EditValue = mprList(0).PurchaseUserID
            glu_PurchaseDepartment.EditValue = mprList(0).PurchaseDepartment
            gluWareHouse.EditValue = mprList(0).WareHouseID
            CheckUrgent.Checked = mprList(0).IsUrgent
        End If

        '2.子表數據返回
        ds.Tables("MrpPurchaseRecordEntry").Clear()
        Dim mprelist As New List(Of MrpPurchaseRecordEntryInfo)
        If MrpPurchaseID <> String.Empty Then
            mprelist = mpecon.MrpPurchaseRecordEntry_GetList(MrpPurchaseID)
            If mprelist.Count > 0 Then
            Else
                Exit Sub
            End If
        End If

        Dim i As Integer
        For i = 0 To mprelist.Count - 1
            Dim row As DataRow
            row = ds.Tables("MrpPurchaseRecordEntry").NewRow
            row("AutoID") = mprelist(i).AutoID
            row("M_Code") = mprelist(i).M_Code
            row("MPI_CreateDate") = mprelist(i).MPI_CreateDate
            row("MPI_CreateUserID") = mprelist(i).MPI_CreateUserID
            row("MPI_CreateUserName") = mprelist(i).MPI_CreateUserName
            row("MPI_ModifyDate") = mprelist(i).MPI_ModifyDate
            row("MPI_ModifyUserID") = mprelist(i).MPI_ModifyUserID
            row("MPI_ModifyUserName") = mprelist(i).MPI_ModifyUserName
            row("MPI_NeedQty") = mprelist(i).MPI_NeedQty
            row("MRP_ID") = mprelist(i).MRP_ID
            row("MrpPurchaseID") = mprelist(i).MrpPurchaseID
            row("M_Gauge") = mprelist(i).M_Gauge
            row("M_Name") = mprelist(i).M_Name
            row("M_Source") = mprelist(i).M_Source
            row("M_Unit") = mprelist(i).M_Unit
            row("MPI_NeedDate") = mprelist(i).MPI_NeedDate
            row("S_Supplier") = mprelist(i).S_Supplier
            row("S_SupplierName") = mprelist(i).S_SupplierName
            row("MPI_Remark") = mprelist(i).MPI_Remark
            row("ForecastDate") = IIf(mprelist(i).ForecastDate.Year = 1, DBNull.Value, mprelist(i).ForecastDate)
            row("UnitPrice") = mprelist(i).UnitPrice
            ds.Tables("MrpPurchaseRecordEntry").Rows.Add(row)
        Next

    End Sub
#End Region

#Region "添加方法"
    Private Sub SaveAdd()
        '1.主表數據新增
        Dim MrpInfo As New MrpPurchaseRecordInfo
        MrpInfo.MrpPurchaseID = mpcon.MrpPurchaseRecord_GetNewMrpID()
        MrpInfo.MRP_ID = GLU_MRPID.Text
        MrpInfo.MPP_Remark = txt_MPP_Remark.Text
        MrpInfo.MPP_CreateUserID = InUserID

        MrpInfo.PurchaseDate = det_PurchaseDate.EditValue
        MrpInfo.PurchaseUserID = glu_PurchaseUserID.EditValue
        MrpInfo.PurchaseDepartment = glu_PurchaseDepartment.EditValue
        MrpInfo.WareHouseID = gluWareHouse.EditValue
        MrpInfo.IsUrgent = CheckUrgent.Checked

        If mpcon.MrpPurchaseRecord_Add(MrpInfo) = False Then
            MsgBox("保存失敗,請檢查原因！")
            Exit Sub
        End If

        '2.子表數據新增
        For i As Integer = 0 To ds.Tables("MrpPurchaseRecordEntry").Rows.Count - 1
            With ds.Tables("MrpPurchaseRecordEntry")
                Dim mrpeInfo As New MrpPurchaseRecordEntryInfo
                mrpeInfo.M_Code = .Rows(i)("M_Code").ToString
                mrpeInfo.MPI_NeedQty = .Rows(i)("MPI_NeedQty")
                mrpeInfo.MRP_ID = .Rows(i)("MRP_ID").ToString
                mrpeInfo.MrpPurchaseID = .Rows(i)("MrpPurchaseID").ToString
                If IsDBNull(.Rows(i)("MPI_NeedDate")) Then
                    mrpeInfo.MPI_NeedDate = Nothing
                Else
                    mrpeInfo.MPI_NeedDate = .Rows(i)("MPI_NeedDate")
                End If

                mrpeInfo.S_Supplier = .Rows(i)("S_Supplier").ToString
                mrpeInfo.MPI_Remark = .Rows(i)("MPI_Remark").ToString
                If IsDBNull(.Rows(i)("ForecastDate")) Then
                    mrpeInfo.ForecastDate = Nothing
                Else
                    mrpeInfo.ForecastDate = .Rows(i)("ForecastDate")
                End If

                mrpeInfo.MPI_CreateUserID = InUserID
                If mpecon.MrpPurchaseRecordEntry_Add(mrpeInfo) = False Then
                    MsgBox("保存失敗,請檢查原因！")
                    Exit Sub
                End If
            End With
        Next
        MsgBox("保存成功！")
        Me.Close()
    End Sub
#End Region

#Region "修改方法"
    Private Sub SaveEdit()
        '1.刪除數據表里面的值
        For i As Integer = 0 To ds.Tables("DelTable").Rows.Count - 1
            If ds.Tables("DelTable").Rows.Count = 0 Then
                Exit For
            End If
            If IsDBNull(ds.Tables("DelTable").Rows(i)("AutoID")) = False Then
                mpecon.MrpPurchaseRecordEntry_Delete(Nothing, ds.Tables("DelTable").Rows(i)("AutoID"))
            End If
        Next
        '2.新增主表數據
        Dim mpInfo As New MrpPurchaseRecordInfo
        mpInfo.MrpPurchaseID = txt_MrpPurchID.Text
        mpInfo.MRP_ID = GLU_MRPID.Text
        mpInfo.MPP_Remark = txt_MPP_Remark.Text
        mpInfo.MPP_ModifyUserID = InUserID

        mpInfo.PurchaseDate = det_PurchaseDate.EditValue
        mpInfo.PurchaseUserID = glu_PurchaseUserID.EditValue
        mpInfo.PurchaseDepartment = glu_PurchaseDepartment.EditValue
        mpInfo.WareHouseID = gluWareHouse.EditValue
        mpInfo.IsUrgent = CheckUrgent.Checked


        If mpcon.MrpPurchaseRecord_Update(mpInfo) = False Then
            MsgBox("修改失敗,請檢查原因！")
            Exit Sub
        End If
        '3.新增子表數據
        For i As Integer = 0 To ds.Tables("MrpPurchaseRecordEntry").Rows.Count - 1
            With ds.Tables("MrpPurchaseRecordEntry")
                Dim mrpeInfo As New MrpPurchaseRecordEntryInfo
                mrpeInfo.M_Code = .Rows(i)("M_Code").ToString
                mrpeInfo.MPI_NeedQty = .Rows(i)("MPI_NeedQty").ToString
                mrpeInfo.MRP_ID = .Rows(i)("MRP_ID").ToString
                mrpeInfo.MrpPurchaseID = .Rows(i)("MrpPurchaseID").ToString
                mrpeInfo.MPI_NeedDate = .Rows(i)("MPI_NeedDate").ToString
                mrpeInfo.S_Supplier = .Rows(i)("S_Supplier").ToString
                mrpeInfo.MPI_Remark = .Rows(i)("MPI_Remark").ToString
                If IsDBNull(.Rows(i)("ForecastDate")) Then
                    mrpeInfo.ForecastDate = Nothing
                Else
                    mrpeInfo.ForecastDate = .Rows(i)("ForecastDate")
                End If
                mrpeInfo.MPI_ModifyUserID = InUserID

                If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) = 0 Then
                    mrpeInfo.MPI_CreateUserID = InUserID
                    If mpecon.MrpPurchaseRecordEntry_Add(mrpeInfo) = False Then
                        MsgBox("修改失敗,請檢查原因！")
                        Exit Sub
                    End If
                Else
                    mrpeInfo.AutoID = .Rows(i)("AutoID")
                    If mpecon.MrpPurchaseRecordEntry_Update(mrpeInfo) = False Then
                        MsgBox("修改失敗,請檢查原因！")
                        Exit Sub
                    End If
                End If
            End With
        Next

        MsgBox("保存成功！")
        Me.Close()
    End Sub
#End Region

#Region "審核方法"
    '保存按鈕功能—審核
    Private Sub DateCheck()
        If chkCheckBit.Checked = boolChcek Then
            MsgBox("審核狀態沒有改變", 64, "提示")
            Exit Sub
        End If

        Dim bminfo As New MrpPurchaseRecordInfo
        bminfo.CheckBit = chkCheckBit.Checked
        bminfo.MPP_CheckUserID = InUserID
        bminfo.MrpPurchaseID = txt_MrpPurchID.Text
        bminfo.CheckRemark = txtCheckRemark.Text

        Try
            If mpcon.MrpPurchaseRecord_Check(bminfo) Then
                If chkCheckBit.Checked = True Then
                    MsgBox("審核成功", 64, "提示")
                Else
                    MsgBox("取消審核成功", 64, "提示")
                End If
            End If
        Catch ex As Exception
            MsgBox("審核失敗", 64, "提示")
        End Try
        Me.Close()
    End Sub
#End Region

#Region "更新報價信息"
    Private Sub UpdateQuotation()
        For i As Integer = 0 To ds.Tables("MrpPurchaseRecordEntry").Rows.Count - 1
            With ds.Tables("MrpPurchaseRecordEntry")
                Dim mrpeInfo As New MrpPurchaseRecordEntryInfo
                mrpeInfo.MrpPurchaseID = .Rows(i)("MrpPurchaseID").ToString
                mrpeInfo.M_Code = .Rows(i)("M_Code").ToString
                mrpeInfo.S_Supplier = .Rows(i)("S_Supplier").ToString
                mrpeInfo.UnitPrice = .Rows(i)("UnitPrice")
                If mpecon.MrpPurchaseRecordEntry_UpdateQuotation(mrpeInfo) = False Then
                    MsgBox("修改失敗,請檢查原因！")
                    Exit Sub
                End If

            End With
        Next
        MsgBox("保存成功！")
        Me.Close()
    End Sub
#End Region

#Region "是否為空"
    Private Function CheckDateEmpty() As Boolean
        CheckDateEmpty = False
        If glu_PurchaseDepartment.EditValue = Nothing Then
            MsgBox("請購部門不能為空", 64, "提示")
            glu_PurchaseDepartment.Focus()
            Exit Function
        ElseIf glu_PurchaseUserID.EditValue = Nothing Then
            MsgBox("請購人員不能為空", 64, "提示")
            glu_PurchaseUserID.Focus()
            Exit Function
        ElseIf det_PurchaseDate.EditValue = Nothing Then
            MsgBox("請購日期不能為空", 64, "提示")
            det_PurchaseDate.Focus()
            Exit Function
        ElseIf gluWareHouse.EditValue = Nothing Then
            MsgBox("進貨倉庫不能為空", 64, "提示")
            gluWareHouse.Focus()
            Exit Function
        End If
        If GridView.RowCount < 1 Then
            MsgBox("物料明細不能為空", 64, "提示")
            Exit Function
        End If

        Dim bo As Boolean = False
        For i As Integer = 0 To GridView.RowCount - 1
            If IsDBNull(GridView.GetRowCellValue(i, "M_Code")) Then
                MsgBox("物料編號不能為空", 64, "提示")
                GridView.FocusedColumn = GridView.Columns("M_Code")
                bo = True
            ElseIf IsDBNull(GridView.GetRowCellValue(i, "MPI_NeedQty")) Then
                MsgBox("需求數量不能為空", 64, "提示")
                GridView.FocusedColumn = GridView.Columns("MPI_NeedQty")
                bo = True
            ElseIf IsDBNull(GridView.GetRowCellValue(i, "MPI_NeedDate")) Then
                MsgBox("需求日期不能為空", 64, "提示")
                GridView.FocusedColumn = GridView.Columns("MPI_NeedDate")
                bo = True
            ElseIf IsDBNull(GridView.GetRowCellValue(i, "ForecastDate")) Then
                MsgBox("預定交期不能為空", 64, "提示")
                GridView.FocusedColumn = GridView.Columns("ForecastDate")
                bo = True
            End If
            'If EditItem = EditEnumType.ELSEONE And IsDBNull(GridView.GetRowCellValue(i, "UnitPrice")) = True Then
            '    MsgBox("單價不能為空", 64, "提示")
            '    GridView.FocusedColumn = GridView.Columns("ForecastDate")
            '    bo = True
            'End If
            If bo = True Then
                GridView.FocusedRowHandle = i
                Exit Function
            End If
        Next
        CheckDateEmpty = True
    End Function
#End Region


#Region "表格菜單事件"
    Private Sub tsmNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNew.Click
        Dim row As DataRow = ds.Tables("MrpPurchaseRecordEntry").NewRow
        row("MRP_ID") = GLU_MRPID.Text
        row("MrpPurchaseID") = txt_MrpPurchID.Text
        ds.Tables("MrpPurchaseRecordEntry").Rows.Add(row)
        GridView.FocusedRowHandle = GridView.RowCount - 1
    End Sub

    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If GridView.RowCount <= 0 Then
            Exit Sub
        End If
        Dim row As DataRow
        row = ds.Tables("DelTable").NewRow
        row("AutoID") = GridView.GetFocusedRowCellValue("AutoID")
        ds.Tables("DelTable").Rows.Add(row)
        GridView.DeleteRow(GridView.FocusedRowHandle())
        GridView.SelectRow(GridView.RowCount - 1)
        GridView.FocusedRowHandle = GridView.RowCount - 1
    End Sub
#End Region

#Region "物料信息賦值"
    Private Sub GridViewMaterial1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridViewMaterial1.DoubleClick
        GridView.SetFocusedRowCellValue(GridView.Columns("M_Code"), GridViewMaterial1.GetFocusedRowCellValue("M_Code").ToString)
        GridView.SetFocusedRowCellValue(GridView.Columns("M_Name"), GridViewMaterial1.GetFocusedRowCellValue("M_Name").ToString)
        GridView.SetFocusedRowCellValue(GridView.Columns("M_Gauge"), GridViewMaterial1.GetFocusedRowCellValue("M_Gauge").ToString)
        GridView.SetFocusedRowCellValue(GridView.Columns("M_Unit"), GridViewMaterial1.GetFocusedRowCellValue("M_Unit").ToString)
        GridView.SetFocusedRowCellValue(GridView.Columns("M_Source"), GridViewMaterial1.GetFocusedRowCellValue("MC_Source").ToString)
    End Sub
#End Region

    Private Sub GridView8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView8.Click
        GridView.SetFocusedRowCellValue(GridView.Columns("S_SupplierName"), GridView8.GetFocusedRowCellValue("SupplierName"))
        GridView.SetFocusedRowCellValue(GridView.Columns("S_Supplier"), GridView8.GetFocusedRowCellValue("S_Supplier"))
        GridView.SetFocusedRowCellValue(GridView.Columns("UnitPrice"), GridView8.GetFocusedRowCellValue("UnitPrice"))
    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemPopupContainerEdit1.Click
        GridControl3.DataSource = mmc.MrpMaterialCode_GetAllTable(GridView.GetFocusedRowCellValue("M_Code").ToString, True)
    End Sub

    Private Sub SetReadOnly()
        GroupBox1.Enabled = False
        For i As Integer = 0 To GridView.Columns.Count - 1
            GridView.Columns(i).OptionsColumn.ReadOnly = True
            GridView.Columns(i).OptionsColumn.AllowEdit = False
        Next
        cmsMenuStrip.Enabled = False
        tsmNew.Visible = False
        tsmDelete.Visible = False
        det_PurchaseDate.Enabled = False
        glu_PurchaseUserID.Enabled = False
        glu_PurchaseDepartment.Enabled = False
        CheckUrgent.Enabled = False
        txt_MPP_Remark.Enabled = False
    End Sub

    Private Sub GridView_CustomColumnDisplayText(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles GridView.CustomColumnDisplayText
        '----------------當請購日期為空時，則不顯示----------------
        If e.Column.FieldName = "PurchaseDate" Then
            If e.Value = Nothing Then e.DisplayText = ""
        End If
    End Sub

#Region "按鍵事件"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckDateEmpty() = False Then
            Exit Sub
        End If

        Select Case EditItem
            Case EditEnumType.ADD
                SaveAdd()
            Case EditEnumType.EDIT
                SaveEdit()
            Case EditEnumType.CHECK
                DateCheck()
            Case EditEnumType.ELSEONE
                UpdateQuotation()
        End Select

    End Sub
#End Region

#Region "數據值驗證"
    Private Sub RepositoryItemCalcEdit1_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEdit1.EditValueChanging
        If (CDec(e.NewValue) > 999999999 Or CDec(e.NewValue) < 0) Then
            e.Cancel = True
        End If
    End Sub
#End Region

End Class