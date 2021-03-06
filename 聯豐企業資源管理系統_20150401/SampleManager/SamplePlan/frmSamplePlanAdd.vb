'Imports LFERP.DataSetting
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.SampleManager.SampleOrdersSub
Imports LFERP.Library.SampleManager.SamplePlan

Public Class frmSamplePlanAdd
#Region "属性"
    Public ds As New DataSet
    Dim SampleSub As New SampleOrdersSubControler
    Dim SampleSubInfo As New SampleOrdersSubInfo
    Dim SampleMainInfo As New SampleOrdersMainInfo
    Dim SampleMain As New SampleOrdersMainControler
    Dim SamplePlan As New SamplePlanControler
    Dim SamplePlanInfo As New SamplePlanInfo

    Private _EditItem As String '属性栏位
    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
#End Region

#Region "窗体载入"
    Private Sub frmSampleOrders_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        Me.txtCreateName.Text = InUserID
        Select Case EditItem
            Case EditEnumType.ADD
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                SetListValue()
                Me.DateAdd.EditValue = Format(Now, "yyyy/MM/dd")
            Case EditEnumType.EDIT
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                SetEditListValue()
                gluSO_ID.Enabled = False
                DateAdd.Enabled = False
                LoadData(txtSP_ID.Text)
            Case EditEnumType.CHECK
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                SetEditListValue()
                Me.Grid.ContextMenuStrip = Nothing
                LoadData(txtSP_ID.Text)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
            Case EditEnumType.VIEW
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                SetEditListValue()
                gluSO_ID.Enabled = False
                txtSP_ID.Enabled = False
                DateAdd.Enabled = False
                cmdSave.Visible = False
                Me.Grid.ContextMenuStrip = Nothing
                LoadData(txtSP_ID.Text)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        End Select
        Me.Text = lblTitle.Text
    End Sub
#End Region

#Region "设置数据值"
    Public Sub SetListValue()
        Dim mtd As New SampleOrdersMainControler
        Dim SP As New SamplePlanControler
        Dim som As New List(Of SampleOrdersMainInfo)
        Dim spi As New List(Of SamplePlanInfo)

        som = mtd.SampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)
        Dim i As Integer
        For i = 0 To som.Count - 1
            If SP.SamplePlan_GetItem(som.Item(i).SO_ID, som.Item(i).SS_Edition).Count = 0 Then
                Dim Samnfo As New SamplePlanInfo
                Samnfo.SO_ID = som.Item(i).SO_ID
                Samnfo.SS_Edition = som.Item(i).SS_Edition
                Samnfo.SO_SampleID = som.Item(i).SO_SampleID
                spi.Add(Samnfo)
            End If
        Next
        gluSO_ID.Properties.DisplayMember = "SO_ID"
        gluSO_ID.Properties.ValueMember = "SO_ID"
        gluSO_ID.Properties.DataSource = spi
    End Sub
    Public Sub SetEditListValue()
        Dim mtd As New SampleOrdersMainControler
        Dim som As New List(Of SampleOrdersMainInfo)
        som = mtd.SampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)
        gluSO_ID.Properties.DisplayMember = "SO_ID"
        gluSO_ID.Properties.ValueMember = "SO_ID"
        gluSO_ID.Properties.DataSource = som
    End Sub
#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SamplePlan")
            .Columns.Add("SP_ID", GetType(String))
            .Columns.Add("SO_ID", GetType(String))
            .Columns.Add("SS_Edition", GetType(String))
            .Columns.Add("SP_StartDate", GetType(Date))
            .Columns.Add("SP_EndDate", GetType(Date))
            .Columns.Add("SP_Remark", GetType(String))
            .Columns.Add("SP_AddUserID", GetType(String))
            .Columns.Add("SP_AddDate", GetType(Date))
            .Columns.Add("SP_AddUserName", GetType(String))
            .Columns.Add("SP_ModifyUserID", GetType(String))
            .Columns.Add("SP_ModifyDate", GetType(Date))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("M_Name", GetType(String))
        End With

        With ds.Tables.Add("DelSamplePlan")
            .Columns.Add("AutoID", GetType(String))
        End With
        Grid.DataSource = ds.Tables("SamplePlan")
    End Sub
#End Region

#Region "载入数据"
    ''' <summary>
    ''' 載入数据
    ''' </summary>
    ''' <param name="strID"></param>
    ''' <remarks></remarks>
    Sub LoadData(ByVal strID As String) '返回数据
        '主檔操作
        Dim somlist As New List(Of SamplePlanInfo)
        somlist = SamplePlan.SamplePlan_Getlist(strID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing)
        If somlist.Count = 0 Then
            Exit Sub
        Else
            txtSP_ID.Text = somlist(0).SP_ID
            gluSO_ID.EditValue = somlist(0).SO_ID
            DateAdd.Text = somlist(0).SP_AddDate
            txtCreateName.Text = somlist(0).SP_AddUserName
        End If
        ''''''''''''''''''''''''''''''''''''
        '子檔操作
        Dim ci As New List(Of SamplePlanInfo)
        ci = SamplePlan.SamplePlan_Getlist(strID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing)

        If ci.Count = 0 Then
            Exit Sub
        Else
            Dim i As Integer
            For i = 0 To ci.Count - 1
                Dim row As DataRow
                row = ds.Tables("SamplePlan").NewRow
                row("SS_Edition") = ci(i).SS_Edition
                row("SP_StartDate") = ci(i).SP_StartDate
                row("SP_EndDate") = ci(i).SP_EndDate
                row("SP_Remark") = ci(i).SP_Remark
                row("AutoID") = ci(i).AutoID
                row("PM_M_Code") = ci(i).PM_M_Code
                row("M_Code") = ci(i).M_Code
                row("M_Name") = ci(i).M_Name
                ds.Tables("SamplePlan").Rows.Add(row)
            Next
        End If
    End Sub
#End Region

#Region "按键事件"
    ''' <summary>
    ''' 保存事件
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
                DataNew()
            Case EditEnumType.EDIT
                DataEdit()
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
#End Region

#Region "新增事件"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Sub DataNew()
        '主檔新增
        SamplePlanInfo.SO_ID = gluSO_ID.EditValue
        SamplePlanInfo.SP_ID = GetSP_ID()
        SamplePlanInfo.SP_AddDate = DateAdd.Text
        SamplePlanInfo.SP_AddUserID = Me.txtCreateName.Text

        Dim i As Integer
        For i = 0 To ds.Tables("SamplePlan").Rows.Count - 1
            With ds.Tables("SamplePlan")
                SamplePlanInfo.SS_Edition = IIf(IsDBNull(.Rows(i)("SS_Edition")), Nothing, .Rows(i)("SS_Edition"))
                SamplePlanInfo.SP_StartDate = .Rows(i)("SP_StartDate").ToString
                SamplePlanInfo.SP_EndDate = .Rows(i)("SP_EndDate").ToString
                SamplePlanInfo.SP_AddUserID = InUserID
                SamplePlanInfo.SP_Remark = IIf(IsDBNull(.Rows(i)("SP_Remark")), Nothing, .Rows(i)("SP_Remark"))
                SamplePlanInfo.M_Code = IIf(IsDBNull(.Rows(i)("M_Code")), Nothing, .Rows(i)("M_Code"))
                SamplePlanInfo.PM_M_Code = IIf(IsDBNull(.Rows(i)("PM_M_Code")), Nothing, .Rows(i)("PM_M_Code"))
            End With
            If SamplePlan.SamplePlan_Add(SamplePlanInfo) = False Then
                MsgBox("添加失敗，请檢查原因！")
                Exit Sub
            Else
                Dim SampleMainInfo As New SampleOrdersMainInfo
                SampleMainInfo.SO_ID = gluSO_ID.EditValue
                SampleMainInfo.SO_State = "E.样办排期"
                SampleMain.SampleOrdersMain_UpdateState(SampleMainInfo)
            End If
        Next
        MsgBox("保存成功!")
        Me.Close()
    End Sub
#End Region

#Region "修改程序"
    ''' <summary>
    '''修改程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '更新刪除列表記錄
        If ds.Tables("DelSamplePlan").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSamplePlan").Rows.Count - 1
                SamplePlan.SamplePlan_Delete(ds.Tables("DelSamplePlan").Rows(j)("AutoID")) '刪除当前选定的
            Next
        End If
        '修改子檔
        Dim i As Integer
        For i = 0 To ds.Tables("SamplePlan").Rows.Count - 1
            With ds.Tables("SamplePlan")
                SamplePlanInfo.SP_ID = txtSP_ID.Text
                SamplePlanInfo.SO_ID = gluSO_ID.EditValue
                SamplePlanInfo.SS_Edition = IIf(IsDBNull(.Rows(i)("SS_Edition")), Nothing, .Rows(i)("SS_Edition"))
                SamplePlanInfo.SP_StartDate = .Rows(i)("SP_StartDate").ToString
                SamplePlanInfo.SP_EndDate = .Rows(i)("SP_EndDate").ToString
                SamplePlanInfo.SP_Remark = IIf(IsDBNull(.Rows(i)("SP_Remark")), Nothing, .Rows(i)("SP_Remark"))
                SamplePlanInfo.M_Code = IIf(IsDBNull(.Rows(i)("M_Code")), Nothing, .Rows(i)("M_Code"))
                SamplePlanInfo.PM_M_Code = IIf(IsDBNull(.Rows(i)("PM_M_Code")), Nothing, .Rows(i)("PM_M_Code"))
                SamplePlanInfo.SP_ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                SamplePlanInfo.SP_ModifyUserID = InUserID
                SamplePlanInfo.AutoID = .Rows(i)("AutoID").ToString

                If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) > 0 Then
                    If SamplePlan.SamplePlan_Update(SamplePlanInfo) = False Then
                        MsgBox("修改失敗，请檢查原因！")
                        Exit Sub
                    End If
                Else
                    If SamplePlan.SamplePlan_Add(SamplePlanInfo) = False Then
                        MsgBox("添加失敗，请檢查原因！")
                        Exit Sub
                    End If
                End If
            End With
        Next
        MsgBox("修改成功!")
        Me.Close()
    End Sub
#End Region

#Region "保存数据前处理"
    ''' <summary>
    ''' 保存数据前处理
    ''' </summary>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer
        If gluSO_ID.EditValue = String.Empty Then
            MsgBox("样办订单号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluSO_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If ds.Tables("SamplePlan").Rows.Count = 0 Then
            MsgBox("表格数据不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            DataCheckEmpty = 0
            Exit Function
        End If
        Dim i As Integer
        For i = 0 To ds.Tables("SamplePlan").Rows.Count - 1
            If IsDBNull(ds.Tables("SamplePlan").Rows(i)("SS_Edition")) Then
                MsgBox("版本号不能为空，请输入版本号！", 64, "提示")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SamplePlan").Rows(i)("PM_M_Code")) Then
                MsgBox("产品编号不能为空，请输入产品编号！", 64, "提示")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SamplePlan").Rows(i)("M_Code")) Then
                MsgBox("配件编号不能为空，请输入配件编号！", 64, "提示")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SamplePlan").Rows(i)("SP_StartDate")) Then
                MsgBox("开始時間不能为空，请输入开始時間！", 64, "提示")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SamplePlan").Rows(i)("SP_EndDate")) Then
                MsgBox("完成時間不能为空，请输入完成日期！", 64, "提示")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If ds.Tables("SamplePlan").Rows(i)("SP_StartDate") > ds.Tables("SamplePlan").Rows(i)("SP_EndDate") Then
                MsgBox("开始時間不能大于完成時間，请输入日期！", 64, "提示")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If

            Dim dblDay As Double
            dblDay = DateDiff("d", Format(CDate(ds.Tables("SamplePlan").Rows(i)("SP_StartDate")), "short date"), CDate(ds.Tables("SamplePlan").Rows(i)("SP_EndDate")))
            If dblDay > 365 Then
                MsgBox("排期不能大于1年，请输入日期！", 64, "提示")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
        Next
        DataCheckEmpty = 1
    End Function
#End Region

#Region "表格事件"
    ''' <summary>
    ''' 子檔刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(GridView1.GetSelectedRows(0), "AutoID")
        If DelTemp <> String.Empty Then
            '在刪除表中增加被刪除的記錄
            Dim row As DataRow = ds.Tables("DelSamplePlan").NewRow
            row("AutoID") = ds.Tables("SamplePlan").Rows(GridView1.FocusedRowHandle)("AutoID")
            ds.Tables("DelSamplePlan").Rows.Add(row)
        End If
        ds.Tables("SamplePlan").Rows.RemoveAt(GridView1.GetSelectedRows(0))
    End Sub
    ''' <summary>
    ''' 子檔新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNew.Click
        If gluSO_ID.EditValue = String.Empty Then
            MessageBox.Show("请输入订单编号", "提示")
            gluSO_ID.Focus()
            Exit Sub
        End If
        Dim fr As New frmSamplePlanAddItem
        fr = New frmSamplePlanAddItem
        fr.txtSO_ID.Text = gluSO_ID.EditValue
        fr.txtSO_ID.Enabled = False
        fr.ShowDialog()
        '''''''''''''''''''是否存在''''''''''''''''''''''''''
        Dim m As Integer
        For m = 0 To fr.SampleList.Count - 1
            Dim BoolEdit As Boolean
            BoolEdit = True
            Dim x As Integer
            For x = 0 To ds.Tables("SamplePlan").Rows.Count - 1
                If fr.SampleList.Item(m).SS_Edition = ds.Tables("SamplePlan").Rows(x)("SS_Edition").ToString And _
                   fr.SampleList.Item(m).PM_M_Code = ds.Tables("SamplePlan").Rows(x)("PM_M_Code").ToString And _
                   fr.SampleList.Item(m).SO_ID = ds.Tables("SamplePlan").Rows(x)("SO_ID").ToString Then
                    BoolEdit = False
                End If
            Next

            If BoolEdit = True Then
                Dim row As DataRow = ds.Tables("SamplePlan").NewRow
                row("SS_Edition") = fr.SampleList.Item(m).SS_Edition
                row("PM_M_Code") = fr.SampleList.Item(m).PM_M_Code
                row("M_Code") = fr.SampleList.Item(m).M_Code
                row("M_Name") = fr.SampleList.Item(m).M_Name
                ds.Tables("SamplePlan").Rows.Add(row)
            End If
        Next
    End Sub
#End Region

#Region "自动流水号"
    ''' <summary>
    ''' 自動新增流水号排期单号
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSP_ID() As String
        Dim oc As New SamplePlanControler
        Dim oi As New SamplePlanInfo
        Dim ndate As String = "SP" + Format(Now(), "yyMM")
        oi = oc.SamplePlan_Get(ndate)
        If oi Is Nothing Then
            GetSP_ID = "SP" + Format(Now, "yyMM") + "0001"
        Else
            GetSP_ID = "SP" + Format(Now, "yyMM") + Mid(CStr(CInt(Mid(oi.SP_ID, 7)) + 1000000001), 7)
        End If
    End Function
#End Region
End Class