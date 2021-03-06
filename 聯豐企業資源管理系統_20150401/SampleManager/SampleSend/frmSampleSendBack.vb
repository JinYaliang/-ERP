Imports LFERP.DataSetting
Imports LFERP.Library.SampleManager.SampleProcess
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.SampleManager.SampleCollection
Imports LFERP.Library.SampleManager.SampleSend
Imports LFERP.Library.SampleManager.SampleWareInventory
Imports LFERP.Library.PieceProcess

Public Class frmSampleSendBack
#Region "属性"

    Dim somcon As New SampleOrdersMainControler
    Dim ssbcon As New SampleSendBackControler
    Dim sclcom As New SampleCollectionControler
    Dim prcon As New SampleProcessControl
    Dim SwCon As New SampleWareInventoryControler
    Dim mtd As New CusterControler
    Dim pncon As New PersonnelControl

    Private ds As New DataSet
    Private boolSE_Check As Boolean '審核狀態有沒有改變
    Private _EditItem As String '属性栏位
    Private _AutoID As String
    Private _SO_CusterID As String
    Private _SO_ID As String
    Private _PM_M_Code As String
    Private _SO_SampleID As String
    Private _SB_IDItem As String
    Private _SS_Edition As String

    Property AutoID() As String '属性
        Get
            Return _AutoID
        End Get
        Set(ByVal value As String)
            _AutoID = value
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
    Property SO_CusterID() As String '客户代号
        Get
            Return _SO_CusterID
        End Get
        Set(ByVal value As String)
            _SO_CusterID = value
        End Set
    End Property
    Property SO_ID() As String '订单编号
        Get
            Return _SO_ID
        End Get
        Set(ByVal value As String)
            _SO_ID = value
        End Set
    End Property
    Property PM_M_Code() As String '产品编号
        Get
            Return _PM_M_Code
        End Get
        Set(ByVal value As String)
            _PM_M_Code = value
        End Set
    End Property
    Property SO_SampleID() As String '样办单号
        Get
            Return _SO_SampleID
        End Get
        Set(ByVal value As String)
            _SO_SampleID = value
        End Set
    End Property
    Property SB_IDItem() As String '样办单号
        Get
            Return _SB_IDItem
        End Get
        Set(ByVal value As String)
            _SB_IDItem = value
        End Set
    End Property
    Property SS_Edition() As Integer  '样办单号
        Get
            Return _SS_Edition
        End Get
        Set(ByVal value As Integer)
            _SS_Edition = value
        End Set
    End Property

#End Region

#Region "载入窗体"
    Private Sub frmSamplePace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        '控件載入数据2
        gluCuster.Properties.DataSource = mtd.GetCusterList(Nothing, Nothing, Nothing)
        Select Case EditItem
            Case EditEnumType.ADD
                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                Me.DateSendDate.EditValue = Format(Now, "yyyy/MM/dd")
                Me.gluCuster.EditValue = SO_CusterID
                Me.txtPM_M_Code.Text = PM_M_Code

                SetSO_IDDataSource() '显示订单编号
                SetD_IDDataSource() '显示部门
                SetPSNODataSource() '显示工序
                Me.gluSO_ID.EditValue = SO_ID
                Me.DateSendDate.Enabled = False
                Me.gluCuster.Enabled = False
                Me.gluSO_ID.Enabled = False
                Me.txtPM_M_Code.Enabled = False
                Me.XtraTabPage2.PageVisible = False
            Case EditEnumType.ELSEONE
                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                Me.DateSendDate.EditValue = Format(Now, "yyyy/MM/dd")
                Me.gluCuster.EditValue = SO_CusterID
                Me.txtPM_M_Code.Text = PM_M_Code

                SetSO_IDDataSource() '显示订单编号
                SetD_IDDataSource() '显示部门
                SetPSNODataSource() '显示工序
                Me.gluSO_ID.EditValue = SO_ID
                Me.DateSendDate.Enabled = False
                Me.gluCuster.Enabled = False
                Me.gluSO_ID.Enabled = False
                Me.txtPM_M_Code.Enabled = False
                Me.XtraTabPage2.PageVisible = False

            Case EditEnumType.VIEW
                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                SetSO_IDDataSource() '显示订单编号
                SetD_IDDataSource() '显示部门
                LoadData(SB_IDItem)
                SetPSNODataSource() '显示工序

                Me.Grid1.ContextMenuStrip = Nothing
                Me.txtM_Code.Enabled = False
                Me.DateSendDate.Enabled = False
                Me.gluCuster.Enabled = False
                Me.gluSO_ID.Enabled = False
                Me.txtPM_M_Code.Enabled = False
                Me.gluD_ID.Enabled = False
                Me.gluPS_NO.Enabled = False
                Me.txt_Remark.Enabled = False
                Me.cmdSave.Visible = False
                Me.XtraTabPage2.PageVisible = False
                GridEnable()
        End Select

    End Sub

#Region "控件载入数据"
    Sub SetD_IDDataSource() '载入部门
        Dim pmlist As New List(Of PersonnelInfo) '部門分享
        If EditItem = EditEnumType.ADD Then
            pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, Nothing, Nothing, "SB")
        ElseIf EditItem = EditEnumType.ELSEONE Then
            pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, Nothing, Nothing, "ZP")
        Else
            pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
        gluD_ID.Properties.DataSource = pmlist
        gluD_ID.EditValue = pmlist(0).DepID
    End Sub
    Sub SetSO_IDDataSource() '載入訂單編號
        gluSO_ID.Properties.DataSource = somcon.SampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)
    End Sub
    Sub SetPSNODataSource() '载入工序
        Dim splist As New List(Of SampleProcessInfo)
        splist = prcon.SampleProcessMain_GetList(Nothing, txtPM_M_Code.Text, Nothing, Nothing, Nothing, Nothing, Nothing)
        gluPS_NO.Properties.DataSource = splist
        'gluPS_NO.EditValue = splist(splist.Count - 1).PS_NO
    End Sub
    Sub GridEnable()
        GridView9.OptionsBehavior.AutoSelectAllInEditor = False
        GridView9.OptionsBehavior.Editable = False
        GridView9.OptionsSelection.EnableAppearanceFocusedCell = False
    End Sub
#End Region

#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SampleSendBack")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("SB_ID", GetType(String))
            .Columns.Add("SB_Qty", GetType(Integer))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("AddDate", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("SampleSendBack")
    End Sub
#End Region

#Region "載入数据"
    Sub LoadData(ByVal strSB_ID As String) '返回数据
        Dim som As New List(Of SampleSendBackInfo)
        som = ssbcon.SampleSendBack_GetList(Nothing, strSB_ID)
        If som.Count = 0 Then
            Exit Sub
        Else
            Me.txtSB_ID.Text = som(0).SB_ID
            Me.DateSendDate.Text = Format(CDate(som(0).SB_SendBackDate), "yyyy/MM/dd")
            Me.gluCuster.EditValue = som(0).SB_CusterID
            Me.gluSO_ID.EditValue = som(0).SO_ID
            Me.txtPM_M_Code.Text = som(0).PM_M_Code
            Me.gluD_ID.EditValue = som(0).D_ID
            Me.gluPS_NO.EditValue = som(0).PS_NO
            Me.txt_Remark.Text = som(0).SB_Remark
            '.....................................................
            Dim i As Integer
            ds.Tables("SampleSendBack").Clear()
            For i = 0 To som.Count - 1
                Dim row As DataRow
                row = ds.Tables("SampleSendBack").NewRow
                row("AutoID") = som(i).AutoID
                row("Code_ID") = som(i).Code_ID
                row("SB_ID") = som(i).SB_ID
                row("SB_Qty") = som(i).SB_Qty
                row("AddDate") = Format(CDate(som(i).SB_AddDate), "yyyy-MM-dd HH:mm:ss")
                ds.Tables("SampleSendBack").Rows.Add(row)
            Next

        End If
    End Sub
#End Region

#Region "子表刪除事件"
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If GridView9.RowCount = 0 Then Exit Sub
        ds.Tables("SampleSendBack").Rows.RemoveAt(GridView9.GetSelectedRows(0))
    End Sub
#End Region

#Region "按键事件"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If DataCheckEmpty() = 0 Then
            Exit Sub
        End If
        Select Case EditItem
            Case EditEnumType.ADD
                OrdersBackNew()
            Case EditEnumType.ELSEONE
                FinishBackNew()
        End Select
    End Sub
#End Region

#Region "订单退回新增修改程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub OrdersBackNew() '新增
        Dim ssbinfo As New SampleSendBackInfo
        '1.主檔控件' ------------------------------------------------------------------------
        Dim strSB_ID As String = GetSB_ID()
        ssbinfo.SB_ID = strSB_ID
        ssbinfo.SB_SendBackDate = DateSendDate.Text
        ssbinfo.SB_CusterID = gluCuster.EditValue

        ssbinfo.SO_ID = Me.gluSO_ID.EditValue
        ssbinfo.SS_Edition = SS_Edition
        ssbinfo.PM_M_Code = Me.txtPM_M_Code.Text
        ssbinfo.D_ID = Me.gluD_ID.EditValue
        ssbinfo.PS_NO = Me.gluPS_NO.EditValue
        ssbinfo.SB_Remark = Me.txt_Remark.Text
        ssbinfo.SE_TypeName = "订单退回"


        '2.子表數據新增' --------------------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("SampleSendBack").Rows.Count - 1
            With ds.Tables("SampleSendBack")
                ssbinfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                ssbinfo.SB_Qty = IIf(IsDBNull(.Rows(i)("SB_Qty")), Nothing, .Rows(i)("SB_Qty"))
                ssbinfo.SB_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                ssbinfo.SB_AddUserID = InUserID

                If ssbcon.SampleSendBack_Add(ssbinfo) = False Then
                    MsgBox("添加失败，请检查原因！")
                    Exit Sub
                End If
            End With
        Next

        '3.修改条码状态为在产
        Dim strCode_ID As String = String.Empty
        For i = 0 To ds.Tables("SampleSendBack").Rows.Count - 1
            With ds.Tables("SampleSendBack")
                strCode_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                '厂内条码变为內-->客
                If sclcom.SampleCollection_UpdateA(strCode_ID, "Z") = False Then
                    MessageBox.Show("修改条码状态错误!", "提示")
                    Exit Sub
                End If
                If sclcom.SampleCollection_UpdateC(strCode_ID, Me.gluD_ID.EditValue) = False Then
                    MessageBox.Show("修改条码所在部门错误!", "提示")
                    Exit Sub
                End If
            End With
        Next

        '4 库存入账----------------------------------------------------
        Dim SwInInfo As New SampleWareInventoryInfo
        Dim SwInList As New List(Of SampleWareInventoryInfo)
        Dim intInQty As Integer = 0
        SwInList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, Me.gluPS_NO.EditValue, Nothing, False, Me.gluD_ID.EditValue)
        If SwInList.Count > 0 Then
            intInQty = SwInList(0).SWI_Qty
        End If
        SwInInfo.SWI_Qty = intInQty + ds.Tables("SampleSendBack").Rows.Count
        SwInInfo.ModifyDate = Format(Now, "yyyy/MM/dd")
        SwInInfo.ModifyUserID = InUserID
        SwInInfo.D_ID = Me.gluD_ID.EditValue
        SwInInfo.PS_NO = Me.gluPS_NO.EditValue
        If SwCon.SampleWareInventory_Update(SwInInfo) = False Then
            MsgBox("发料入账失败,请检查原因!", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        '5.修改未交数量-------------------------------------------------.
        Dim soinfo As New SampleOrdersMainInfo
        Dim solist As New List(Of SampleOrdersMainInfo)
        solist = somcon.SampleOrdersMain_GetList(SO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, False)
        If solist.Count > 0 Then
            soinfo.SO_OrderQty = solist(0).SO_OrderQty
            soinfo.SO_NoSendQty = solist(0).SO_NoSendQty + ds.Tables("SampleSendBack").Rows.Count '處理未交數量
        End If
        soinfo.SO_ID = SO_ID
        soinfo.SS_Edition = SS_Edition
        If somcon.SampleOrdersMain_UpdateQty(soinfo) = False Then
            MsgBox("更改未交数量错误！", MsgBoxStyle.Information, "溫馨提示")
            Exit Sub
        End If

        MsgBox("订单退料成功", 60, "提示")
        Me.Close()

    End Sub
#End Region

#Region "新增修改程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub FinishBackNew() '新增
        Dim ssbinfo As New SampleSendBackInfo
        '1.主檔控件' ------------------------------------------------------------------------
        Dim strSB_ID As String = GetSB_ID()
        ssbinfo.SB_ID = strSB_ID
        ssbinfo.SB_SendBackDate = DateSendDate.Text
        ssbinfo.SB_CusterID = gluCuster.EditValue

        ssbinfo.SO_ID = Me.gluSO_ID.EditValue
        ssbinfo.SS_Edition = SS_Edition
        ssbinfo.PM_M_Code = Me.txtPM_M_Code.Text
        ssbinfo.D_ID = Me.gluD_ID.EditValue
        ssbinfo.PS_NO = Me.gluPS_NO.EditValue
        ssbinfo.SB_Remark = Me.txt_Remark.Text
        ssbinfo.SE_TypeName = "完工退回"

        '2.子表數據新增' --------------------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("SampleSendBack").Rows.Count - 1
            With ds.Tables("SampleSendBack")
                ssbinfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                ssbinfo.SB_Qty = IIf(IsDBNull(.Rows(i)("SB_Qty")), Nothing, .Rows(i)("SB_Qty"))
                ssbinfo.SB_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                ssbinfo.SB_AddUserID = InUserID

                If ssbcon.SampleSendBack_Add(ssbinfo) = False Then
                    MsgBox("添加失败，请检查原因！")
                    Exit Sub
                End If
            End With
        Next

        '3.修改条码状态为在产
        Dim strCode_ID As String = String.Empty
        For i = 0 To ds.Tables("SampleSendBack").Rows.Count - 1
            With ds.Tables("SampleSendBack")
                strCode_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                '厂内条码变为內-->客
                If sclcom.SampleCollection_UpdateA(strCode_ID, "Z") = False Then
                    MessageBox.Show("修改条码状态错误!", "提示")
                    Exit Sub
                End If
                If sclcom.SampleCollection_UpdateC(strCode_ID, Me.gluD_ID.EditValue) = False Then
                    MessageBox.Show("修改条码所在部门错误!", "提示")
                    Exit Sub
                End If
            End With
        Next

        '4 库存入账----------------------------------------------------
        Dim SwInInfo As New SampleWareInventoryInfo
        Dim SwInList As New List(Of SampleWareInventoryInfo)
        Dim intInQty As Integer = 0
        SwInList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, Me.gluPS_NO.EditValue, Nothing, False, Me.gluD_ID.EditValue)
        If SwInList.Count > 0 Then
            intInQty = SwInList(0).SWI_Qty
        End If
        SwInInfo.SWI_Qty = intInQty + ds.Tables("SampleSendBack").Rows.Count
        SwInInfo.ModifyDate = Format(Now, "yyyy/MM/dd")
        SwInInfo.ModifyUserID = InUserID
        SwInInfo.D_ID = Me.gluD_ID.EditValue
        SwInInfo.PS_NO = Me.gluPS_NO.EditValue
        If SwCon.SampleWareInventory_Update(SwInInfo) = False Then
            MsgBox("发料入账失败,请检查原因!", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        MsgBox("完工退回成功", 60, "提示")
        Me.Close()

    End Sub
#End Region

#Region "检查数据"
    ''' <summary>
    ''' 是否为空
    ''' </summary>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer
        If gluCuster.Text = String.Empty Then
            MsgBox("客戶代号不能为空,请输入！")
            gluCuster.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If DateSendDate.Text = String.Empty Then
            MsgBox("寄送日期不能为空,请输入！")
            DateSendDate.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluSO_ID.EditValue = String.Empty Then
            MsgBox("样办单号不能为空,请输入！")
            gluSO_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If txtPM_M_Code.EditValue = String.Empty Then
            MsgBox("产品编号不能为空,请输入！")
            txtPM_M_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluD_ID.EditValue = String.Empty Then
            MsgBox("转入部门不能为空,请输入！")
            gluD_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluPS_NO.EditValue = String.Empty Then
            MsgBox("转入工序不能为空,请输入！")
            gluPS_NO.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        Dim i As Integer
        If ds.Tables("SampleSendBack").Rows.Count <= 0 Then
            MsgBox("沒有輸入条码,不能保存！", 64, "提示")
            Grid1.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        For i = 0 To ds.Tables("SampleSendBack").Rows.Count - 1
            Dim strM_Code As String = IIf(IsDBNull(ds.Tables("SampleSendBack").Rows(i)("Code_ID")), String.Empty, ds.Tables("SampleSendBack").Rows(i)("Code_ID"))

            '4.條碼是不是寄送狀態
            Dim scmlistm As New List(Of SampleCollectionInfo)
            scmlistm = sclcom.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scmlistm.Count > 0 Then
                Select Case EditItem
                    Case EditEnumType.ADD
                        If scmlistm(0).StatusType <> "C" Then
                            MsgBox("此条码不是寄送状态！", 64, "提示")
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = i
                            DataCheckEmpty = 0
                            Exit Function
                        End If
                    Case EditEnumType.ELSEONE
                        If scmlistm(0).StatusType <> "M" Then
                            MsgBox("此条码不是完工状态！", 64, "提示")
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = i
                            DataCheckEmpty = 0
                            Exit Function
                        End If
                End Select

                If Me.txtPM_M_Code.Text <> scmlistm(0).PM_M_Code Then
                    MsgBox("此条码不是产品编号不相同！", 64, "提示")
                    Grid1.Focus()
                    GridView9.FocusedRowHandle = i
                    DataCheckEmpty = 0
                    Exit Function
                End If
            Else
                '5.采集表是否存在
                MsgBox("<采集表>不存在！", 64, "提示")
                Grid1.Focus()
                GridView9.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
        Next

        DataCheckEmpty = 1
    End Function
#End Region

#Region "自动流水号"
    ''' <summary>
    ''' 自動流水号
    ''' </summary>
    ''' <remarks></remarks>
    Function GetSB_ID() As String
        Dim oc As New SampleSendBackControler
        Dim oi As New SampleSendBackInfo
        Dim ndate As String = "SB" + Format(Now(), "yyMM")
        oi = oc.SampleSendBack_GetID(ndate)
        If oi Is Nothing Then
            GetSB_ID = "SB" + Format(Now, "yyMM") + "0001"
        Else
            GetSB_ID = "SB" + Format(Now, "yyMM") + Mid(CStr(CInt(Mid(oi.SB_ID, 7)) + 1000000001), 7)
        End If
    End Function
#End Region

#Region "條碼事件"
    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            lblCode.Text = String.Empty
            Dim strM_Code As String
            '1.客戶不能為空
            If gluCuster.EditValue = String.Empty Then
                MsgBox("客戶代号不能为空,请输入！")
                gluCuster.Focus()
                Exit Sub
            End If
            '2.條碼不能重複
            Dim i As Integer
            strM_Code = UCase(Me.txtM_Code.Text)
            For i = 0 To ds.Tables("SampleSendBack").Rows.Count - 1
                If strM_Code = ds.Tables("SampleSendBack").Rows(i)("Code_ID") Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = strM_Code + "条码重复"
                    Exit Sub
                End If
            Next

            '3.此條碼<采集表>是否存在
            If sclcom.SampleCollection_GetID(strM_Code) = False Then
                lblCode.Text = "<采集表>不存在"
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                Exit Sub
            End If

            '4.條碼是不是寄送狀態
            Dim scmlistm As New List(Of SampleCollectionInfo)
            scmlistm = sclcom.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scmlistm.Count > 0 Then

                Select Case EditItem
                    Case EditEnumType.ADD
                        If scmlistm(0).StatusType <> "C" Then
                            txtM_Code.Text = String.Empty
                            txtM_Code.Focus()
                            lblCode.Text = "此条码不是寄送状态"
                            Exit Sub
                        End If
                    Case EditEnumType.ELSEONE
                        If scmlistm(0).StatusType <> "M" Then
                            txtM_Code.Text = String.Empty
                            txtM_Code.Focus()
                            lblCode.Text = "此条码不是完工状态"
                            Exit Sub
                        End If
                End Select

                If Me.txtPM_M_Code.Text <> scmlistm(0).PM_M_Code Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "此条码与产品编号不相同"
                    Exit Sub
                End If
            End If

            Dim row As DataRow
            row = ds.Tables("SampleSendBack").NewRow
            row("Code_ID") = Trim(StrConv(UCase(strM_Code), vbNarrow))
            row("SB_Qty") = 1
            row("AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
            ds.Tables("SampleSendBack").Rows.Add(row)
            txtM_Code.Text = String.Empty
        End If
    End Sub
#End Region

End Class