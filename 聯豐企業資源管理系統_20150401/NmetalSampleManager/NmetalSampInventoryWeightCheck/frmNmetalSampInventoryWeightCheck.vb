Imports LFERP.Library.PieceProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampInventoryWeightCheck

Public Class frmNmetalSampInventoryWeightCheck
    Dim ds As New DataSet

    Private _EditItem As String '属性栏位
    Private _EditDep As String  '部門
    Private _CNumb As String   '单号
    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property EditDep() As String '部門
        Get
            Return _EditDep
        End Get
        Set(ByVal value As String)
            _EditDep = value
        End Set
    End Property
    Property CNumb() As String '单号
        Get
            Return _CNumb
        End Get
        Set(ByVal value As String)
            _CNumb = value
        End Set
    End Property


    Dim pmlist As New List(Of PersonnelInfo) '部門分享
    Dim pncon As New PersonnelControl
    Dim ndc As New NmetalSampInventoryWeightCheckMainControler
    Dim nswc As New NmetalSampInventoryWeightCheckSubControler
    ''' <summary>
    ''' 窗体加载
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNmetalSampInventoryWeightCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        pmlist = pncon.FacBriSearch_GetList("V", Nothing, Nothing, Nothing)
        glu_DepID.Properties.DataSource = pmlist
        glu_DepID.EditValue = EditDep
        glu_DepID.Enabled = False
        Select Case EditItem
            Case EditEnumType.ADD
                Me.Text = "盘点作业—新增"
                dtp_podate.EditValue = Format(Now, "yyyy/MM/dd")
                XtraTabPage2.PageVisible = False
            Case EditEnumType.EDIT
                Me.Text = "盘点作业—修改"
                LabSE_NO.Enabled = False
                XtraTabPage2.PageVisible = False
                LoadData(CNumb)
            Case EditEnumType.CHECK
                Me.Text = "盘点作业—审核"
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                lblCheckName.Text = InUserID
                lblCheckDate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
                LoadData(CNumb)
            Case EditEnumType.VIEW
                Me.Text = "盘点作业—查看"
                XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
                XtraTabPage2.PageVisible = True
                cmdSave.Visible = False
                LoadData(CNumb)
        End Select
    End Sub
    ''' <summary>
    ''' 傳回值
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadData(ByVal c_no As String)
        Dim cl As New List(Of NmetalSampInventoryWeightCheckMainInfo)
        Dim cls As New List(Of NmetalSampInventoryWeightCheckSubInfo)
        cl = ndc.NmetalSampInventoryWeightCheckMain_GetList(Nothing, c_no, Nothing, Nothing, Nothing, Nothing, Nothing)
        If cl.Count <= 0 Then
            Exit Sub
        End If
        LabSE_NO.Text = cl(0).CH_NO
        glu_DepID.EditValue = cl(0).DepID
        dtp_podate.EditValue = cl(0).CH_Date
        txt_CH_Action.Text = cl(0).CH_Action
        txtRemark.Text = cl(0).CH_Remark
        CheckA.Checked = cl(0).CheckStatus
        lblCheckName.Text = cl(0).CheckAction
        lblCheckDate.Text = cl(0).CheckDate
        txt_checkremark.Text = cl(0).CheckRemark

        cls = nswc.NmetalSampInventoryWeightCheckSub_GetList(Nothing, c_no, Nothing)

        If cls.Count <= 0 Then
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To cl.Count - 1
            Dim row As DataRow
            row = ds.Tables("WeightCheckSub").NewRow

            row("CH_NO") = cls(i).CH_NO
            row("Code_ID") = cls(i).Code_ID
            row("SO_SampleID") = cls(i).SO_SampleID
            row("CH_QQty") = cls(i).CH_QQty
            row("CH_QWeight") = cls(i).CH_QWeight
            row("CH_Qty") = cls(i).CH_Qty

            row("CH_Weight") = cls(i).CH_Weight
            row("ErrorRate") = cls(i).ErrorRate
            row("Remark") = cls(i).Remark

            ds.Tables("WeightCheckSub").Rows.Add(row)
        Next
    End Sub
    ''' <summary>
    ''' 保存按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case EditItem
            Case EditEnumType.ADD
                If CheckDateEmpty() = False Then
                    Exit Sub
                End If
                Savedata()
            Case EditEnumType.EDIT
                If CheckDateEmpty() = False Then
                    Exit Sub
                End If
                Savedata()
            Case EditEnumType.CHECK
                If CheckDateEmpty() = False Then
                    Exit Sub
                End If
                UpdateCheck()

        End Select
    End Sub
    ''' <summary>
    ''' 判断是否为空
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckDateEmpty() As Boolean
        CheckDateEmpty = True
        If dtp_podate.Text = String.Empty Then
            CheckDateEmpty = False
            MsgBox("盘点日期不能为空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            dtp_podate.Focus()
            Exit Function
        End If
        If txt_CH_Action.Text.Trim = String.Empty Then
            CheckDateEmpty = False
            MsgBox("盘点人不能为空，请输入！", MsgBoxStyle.OkOnly, "提示")
            txt_CH_Action.Focus()
            Exit Function
        End If
        Dim i As Integer
        For i = 0 To ds.Tables("WeightCheckSub").Rows.Count - 1
            If ds.Tables("WeightCheckSub").Rows(i)("Code_ID").ToString() = txtCodeID.Text Then
                Grid2.Focus()
                GridView2.FocusedRowHandle = i
                txtCodeID.Text = String.Empty
                txtCodeID.Focus()
                MsgBox("表中存在相同的條碼編號'" & ds.Tables("WeightCheckSub").Rows(i)("Code_ID") & "'!", MsgBoxStyle.OkOnly, "提示")
                CheckDateEmpty = False
                Exit Function
            End If
        Next
    End Function
    ''' <summary>
    ''' 審核
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCheck()
        Dim objinfo As New NmetalSampInventoryWeightCheckMainInfo
        objinfo.CH_NO = LabSE_NO.Text
        objinfo.CheckStatus = CheckA.Checked
        objinfo.CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        objinfo.CheckAction = InUserID
        objinfo.CheckWastWeight = txtCheckWastWeight.Text
        objinfo.CheckRemark = txt_checkremark.Text

        If ndc.NmetalSampInventoryWeightCheckMain_Check(objinfo) = False Then
            MsgBox(LabSE_NO.Text & "，请检查原因！", 60, "提示")
        End If
        MsgBox("审核成功!")
        Me.Close()
    End Sub
    ''' <summary>
    ''' 保存数据
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Savedata()

        Dim objinfo As New NmetalSampInventoryWeightCheckMainInfo
        Dim objinfos As New NmetalSampInventoryWeightCheckSubInfo
        objinfo.CH_NO = LabSE_NO.Text
        objinfo.DepID = glu_DepID.EditValue
        objinfo.CH_Date = dtp_podate.EditValue
        objinfo.CH_Action = txt_CH_Action.Text
        objinfo.CH_Remark = txtRemark.Text

        If EditItem = EditEnumType.ADD Then
            objinfo.CH_NO = SampInventoryWeightCheckMain_GetID()
        End If

        Dim j As Integer
        For j = 0 To ds.Tables("WeightCheckSub").Rows.Count - 1
            objinfos.CH_NO = SampInventoryWeightCheckMain_GetID()
            objinfos.Code_ID = ds.Tables("WeightCheckSub").Rows(j)("Code_ID").ToString
            objinfos.SO_SampleID = ds.Tables("WeightCheckSub").Rows(j)("SO_SampleID").ToString
            objinfos.CH_QQty = ds.Tables("WeightCheckSub").Rows(j)("CH_QQty")
            objinfos.CH_QWeight = ds.Tables("WeightCheckSub").Rows(j)("CH_QWeight")
            objinfos.CH_Qty = ds.Tables("WeightCheckSub").Rows(j)("CH_Qty")
            objinfos.CH_Weight = ds.Tables("WeightCheckSub").Rows(j)("CH_Weight")
            objinfos.ErrorRate = ds.Tables("WeightCheckSub").Rows(j)("ErrorRate")
            objinfos.Remark = ds.Tables("WeightCheckSub").Rows(j)("Remark")


            Select Case EditItem
                Case EditEnumType.ADD
                    objinfo.AddAction = InUserID
                    objinfo.AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    If ndc.NmetalSampInventoryWeightCheckMain_Add(objinfo) = False Then
                        MsgBox(ds.Tables("WeightCheckSub").Rows(j)("CH_NO") & "，请检查原因！", 60, "提示")
                        Exit Sub
                    End If
                    If nswc.NmetalSampInventoryWeightCheckSub_Add(objinfos) = False Then
                        MsgBox(ds.Tables("WeightCheckSub").Rows(j)("CH_NO") & "，请检查原因！", 60, "提示")
                        Exit Sub
                    End If
                Case EditEnumType.EDIT
                    objinfo.ModifyUserID = InUserID
                    objinfo.ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

                    If ndc.NmetalSampInventoryWeightCheckMain_Update(objinfo) = False Then
                        MsgBox(ds.Tables("WeightCheckSub").Rows(j)("CH_NO") & "，请检查原因！", 60, "提示")
                        Exit Sub
                    End If

                    objinfos.CH_NO = ds.Tables("WeightCheckSub").Rows(j)("CH_NO")
                    If nswc.NmetalSampInventoryWeightCheckSub_Update(objinfos) = False Then
                        MsgBox(ds.Tables("WeightCheckSub").Rows(j)("CH_NO") & "，请检查原因！", 60, "提示")
                        Exit Sub
                    End If
            End Select
        Next
        MsgBox("保存成功!")
        Me.Close()
    End Sub
    ''' <summary>
    ''' 獲取單號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SampInventoryWeightCheckMain_GetID() As String
        Dim DepWeightCheckNo As String = ""
        Dim SoyyMM As String
        SoyyMM = "C" & Format(Now, "yyMM")

        Dim ndi As New NmetalSampInventoryWeightCheckMainInfo
        ndi = ndc.NmetalSampInventoryWeightCheckMain_GetID(SoyyMM)

        If ndi.CH_NO = "" Then
            DepWeightCheckNo = Trim(SoyyMM & "0001")
        Else
            DepWeightCheckNo = SoyyMM & Format(Val(Microsoft.VisualBasic.Right(ndi.CH_NO, 4)) + 1, "0000")
        End If

        SampInventoryWeightCheckMain_GetID = DepWeightCheckNo
    End Function
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("WeightCheckSub")
            .Columns.Add("CH_NO", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("SO_SampleID", GetType(String))
            .Columns.Add("CH_QQty", GetType(Integer))
            .Columns.Add("CH_QWeight", GetType(Decimal))

            .Columns.Add("CH_Qty", GetType(Integer))
            .Columns.Add("CH_Weight", GetType(Decimal))
            .Columns.Add("ErrorRate", GetType(Decimal))
            .Columns.Add("Remark", GetType(String))
        End With
        Grid2.DataSource = ds.Tables("WeightCheckSub")
    End Sub
    ''' <summary>
    ''' 加载行数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub AddRow()
        If AddRowCheckDateEmpty() = False Then
            Exit Sub
        End If
        If txtCodeID.Text <> String.Empty Then
            Dim row As DataRow = ds.Tables("WeightCheckSub").NewRow
            row("Code_ID") = Trim(txtCodeID.Text)
            row("CH_Qty") = Trim(txtQty.Text)
            row("CH_Weight") = Trim(txtWeight.Text)

            ds.Tables("WeightCheckSub").Rows.Add(row)
        End If
        txtCodeID.Text = String.Empty
        txtQty.Text = String.Empty
        txtWeight.Text = String.Empty
        txtCodeID.Focus()

    End Sub
    ''' <summary>
    ''' 加载行数据为空判断
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AddRowCheckDateEmpty() As Boolean
        AddRowCheckDateEmpty = True
        If txtCodeID.Text.Trim = String.Empty Then
            AddRowCheckDateEmpty = False
            MsgBox("条码录入不能为空，请输入！", MsgBoxStyle.OkOnly, "提示")
            txt_CH_Action.Focus()
            Exit Function
        End If
        If txtQty.Text.Trim = String.Empty Then
            AddRowCheckDateEmpty = False
            MsgBox("盘点数量不能为空，请输入！", MsgBoxStyle.OkOnly, "提示")
            txt_CH_Action.Focus()
            Exit Function
        End If
        If txtWeight.Text.Trim = String.Empty Then
            AddRowCheckDateEmpty = False
            MsgBox("盘点重量不能为空，请输入！", MsgBoxStyle.OkOnly, "提示")
            txtWeight.Focus()
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' 回车键事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodeID.KeyDown

        If e.KeyCode = Keys.Enter Then
            AddRow()
        End If

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class