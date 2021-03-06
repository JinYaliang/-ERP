Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.Product
Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory
Imports LFERP.Library.PieceProcess



Public Class frmNmetalSampleDepWeightCheck
    Dim ndc As New NmetalSampleDepWeightCheckControler
    Dim nsdc As New NmetalSampleDepWeightControler
    Dim mc As New ProductController
    Dim nsdw As List(Of NmetalSampleDepWeightCheckInfo)
    Dim sms As New SystemUserController
    Dim pmlist As New List(Of PersonnelInfo) '部門分享
    Dim pncon As New PersonnelControl
    Private ds As New DataSet
    Private boolSE_Check As Boolean '審核狀態有沒有改變
    Private _EditItem As String '属性栏位
    Private _EditDep As String  '部門
    Private _AutoNo As String '单号
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
    Property AutoNo() As String '单号
        Get
            Return _AutoNo
        End Get
        Set(ByVal value As String)
            _AutoNo = value
        End Set
    End Property

    ''' <summary>
    ''' 初始化加載
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNmetalSampleDepWeightCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTables()
        pmlist = pncon.FacBriSearch_GetList("V", Nothing, Nothing, Nothing)
        txtSO_dep.Properties.DataSource = pmlist
        txtSO_dep.EditValue = EditDep
        txtSO_dep.Enabled = False

        'Grid2.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Select Case EditItem
            Case EditEnumType.ADD
                Lbl_Title.Text = "部门庫存重量—新增"
                'LabDepID.Text = tempValue2      '部門編號
                'tempValue2 = Nothing

                dtp_date.EditValue = Format(Now, "yyyy/MM/dd")

                txtSO_dep.Enabled = False
                txt_OrderNum.Enabled = False
                XtraTabPage2.PageVisible = False

            Case EditEnumType.EDIT
                Lbl_Title.Text = "部门庫存重量—修改"
                txt_OrderNum.Enabled = False
                txtSO_dep.Enabled = False
                dtp_date.Enabled = False
                XtraTabPage2.PageVisible = False
                moe_remark.Enabled = False
                'Label2.Text = tempValue3
                'tempValue3 = Nothing

                LoadData(AutoNo)

            Case EditEnumType.CHECK
                Lbl_Title.Text = "部门庫存重量—審核"
                'Label2.Text = tempValue3
                'tempValue3 = Nothing
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

                txtSO_dep.Enabled = False
                dtp_date.Enabled = False
                txt_OrderNum.Enabled = False
                moe_remark.Enabled = False
                LoadData(AutoNo)
                'lbl_checkdate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
                'lbl_checkuser.Text = InUserID
                Me.Grid1.ContextMenuStrip = Nothing
            Case EditEnumType.VIEW
                Lbl_Title.Text = "部门庫存重量—查看"
                'Label2.Text = tempValue3
                'tempValue3 = Nothing
                'lbl_checkdate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
                'lbl_checkuser.Text = InUserID

                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

                txtSO_dep.Enabled = False
                dtp_date.Enabled = False
                txt_OrderNum.Enabled = False
                moe_remark.Enabled = False
                CheckEdit1.Enabled = False
                txt_checkremark.Enabled = False
                Savebutton.Visible = False

                LoadData(AutoNo)
                Me.Grid1.ContextMenuStrip = Nothing
        End Select
    End Sub
    ''' <summary>
    ''' 傳回值
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadData(ByVal c_no As String)
        Dim cl As New List(Of NmetalSampleDepWeightCheckInfo)
        cl = ndc.NmetalSampleDepWeightCheck_GetList(Nothing, c_no, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If cl.Count <= 0 Then

            Exit Sub
        End If

        txtSO_dep.EditValue = cl(0).D_ID
        'LabDepID.Text = cl(0).D_ID
        dtp_date.EditValue = cl(0).AddDate
        txt_OrderNum.Text = cl(0).ChangeNO
        moe_remark.Text = cl(0).Remark
        CheckEdit1.Checked = cl(0).CheckStatus
        txt_checkremark.Text = cl(0).CheckRemark

        'lbl_checkdate.Text = Format(CDate(cl(0).CheckDate), "yyyy/MM/dd HH:mm:ss")
        'lbl_checkuser.Text = cl(0).CheckAction


        Dim i As Integer
        For i = 0 To cl.Count - 1
            Dim row As DataRow
            row = ds.Tables("NmetalSampleDepWeightCheck").NewRow

            row("AutoID") = cl(i).AutoID
            row("ChangeNO") = cl(i).ChangeNO
            row("PM_M_Code") = cl(i).PM_M_Code
            row("PM_Type") = cl(i).PM_Type
            row("PS_NO") = cl(i).PS_NO
            row("DepWightOld") = cl(i).DepWightOld
            row("PS_Name") = cl(i).PS_Name

            row("DepWightNew") = cl(i).DepWightNew
 
            row("AddAction") = cl(i).AddAction
            row("AddDate") = cl(i).AddDate
            row("Remark") = cl(i).Remark
            row("SubRemark") = cl(i).SubRemark

            If cl(i).CheckDate = Nothing Then
                lbl_checkdate.Text = Format(Now, "yyyy/MM/dd  HH:mm:ss")
            Else
                lbl_checkdate.Text = Format(CDate(cl(i).CheckDate), "yyyy/MM/dd HH:mm:ss")
            End If
            If cl(i).CheckAction = Nothing Then
                lbl_checkuser.Text = InUserID
            Else
                lbl_checkuser.Text = cl(i).CheckAction
            End If
            row("TWeights") = cl(i).ProductWightOld
            ds.Tables("NmetalSampleDepWeightCheck").Rows.Add(row)
        Next
    End Sub

    ''' <summary>
    ''' 保存按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Savebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Savebutton.Click
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
    ''' 審核
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCheck()
        If CheckEdit1.Checked = boolSE_Check Then
            MsgBox("审核状态没有改变，提示！")
            Exit Sub
        End If
        Dim nswic As New NmetalSampleWareInventoryControler
        Dim nswi As New NmetalSampleWareInventoryInfo
        Dim objinfo As New NmetalSampleDepWeightCheckInfo

        objinfo.ChangeNO = txt_OrderNum.Text
        objinfo.CheckStatus = CheckEdit1.Checked
        objinfo.CheckDate = lbl_checkdate.Text
        objinfo.CheckAction = lbl_checkuser.Text
        objinfo.CheckRemark = txt_checkremark.Text

        If ndc.NmetalSampleDepWeightCheck_Check(objinfo) = False Then
            MsgBox(txt_OrderNum.Text & "，请检查原因！", 60, "提示")
        End If
        Dim j As Integer
        For j = 0 To ds.Tables("NmetalSampleDepWeightCheck").Rows.Count - 1
            nswi.D_ID = txtSO_dep.EditValue
            nswi.PM_M_Code = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PM_M_Code").ToString
            nswi.PS_NO = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PS_NO")
            nswi.SWI_Qty = GetQty(ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PS_NO"), EditDep)
            nswi.SWI_Weight = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("DepWightNew")
            nswi.ModifyUserID = InUserID
            nswi.ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            If nswic.NmetalSampleWareInventory_Update(nswi) = False Then
                MsgBox(txt_OrderNum.Text & "號更新庫存失敗，请检查原因！", 60, "提示")
            End If
        Next

        MsgBox("保存成功!")
        Me.Close()
    End Sub

    Function GetQty(ByVal _PS_NO As String, ByVal _D_ID As String) As Integer
        Dim nswic As New NmetalSampleWareInventoryControler
        Dim nl As New List(Of NmetalSampleWareInventoryInfo)
        nl = nswic.NmetalSampleWareInventory_Getlist(Nothing, Nothing, _PS_NO, Nothing, Nothing, _D_ID)
        If nl.Count <= 0 Then
            GetQty = 0
        Else
            GetQty = nl(0).SWI_Qty
        End If
    End Function

    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Savedata()
        Dim objinfo As New NmetalSampleDepWeightCheckInfo()

        objinfo.ChangeNO = txt_OrderNum.Text
        'objinfo.D_ID = LabDepID.Text
        objinfo.D_ID = txtSO_dep.EditValue
        objinfo.AddDate = dtp_date.Text
        objinfo.Remark = moe_remark.Text

        If EditItem = EditEnumType.ADD Then
            objinfo.ChangeNO = DepWeightCheck_GetID()
        End If


        Dim j As Integer
        For j = 0 To ds.Tables("NmetalSampleDepWeightCheck").Rows.Count - 1
            objinfo.PM_M_Code = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PM_M_Code").ToString
            objinfo.PM_Type = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PM_Type").ToString
            objinfo.PS_NO = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PS_NO").ToString
            objinfo.DepWightOld = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("DepWightOld")
            objinfo.DepWightNew = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("DepWightNew")
            objinfo.SubRemark = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("SubRemark").ToString
            objinfo.ProductWightOld = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("TWeights")
            objinfo.AddAction = InUserID

            Select Case EditItem
                Case EditEnumType.ADD
                    If ndc.NmetalSampleDepWeightCheck_Add(objinfo) = False Then
                        MsgBox(ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PM_M_Code") & "，请检查原因！", 60, "提示")
                        Exit Sub
                    End If
                Case EditEnumType.EDIT
                    objinfo.AddAction = InUserID
                    objinfo.ModifyUserID = InUserID
                    objinfo.ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

                    If IsDBNull(ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("AutoID")) = True Then
                        If ndc.NmetalSampleDepWeightCheck_Add(objinfo) = False Then
                            MsgBox(ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("PM_M_Code") & "，请检查原因！", 60, "提示")
                            Exit Sub
                        End If
                    Else
                        objinfo.AutoID = ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("AutoID")
                        If ndc.NmetalSampleDepWeightCheck_Update(objinfo) = False Then
                            MsgBox(ds.Tables("NmetalSampleDepWeightCheck").Rows(j)("AutoID") & "，请检查原因！", 60, "提示")
                            Exit Sub
                        End If
                    End If
            End Select
        Next
        '刪除子表記錄
        For j = 0 To ds.Tables("NmetalSampleDepWeightCheckDel").Rows.Count - 1
            If ndc.NmetalSampleDepWeightCheck_Delete(ds.Tables("NmetalSampleDepWeightCheckDel").Rows(j)("AutoID").ToString, Nothing) = False Then
                MsgBox("刪除当前选定记录失敗，请检查原因！", 60, "提示")
                Exit Sub
            End If
        Next
        MsgBox("保存成功!")
        Me.Close()

    End Sub

    ''' <summary>
    ''' 判斷不能為空
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckDateEmpty() As Boolean
        CheckDateEmpty = True
        If txtSO_dep.Text = String.Empty Then
            CheckDateEmpty = False
            MsgBox("您沒有输入部門！", MsgBoxStyle.OkOnly, "提示")
            txtSO_dep.Focus()
            Exit Function
        End If
        If dtp_date.Text = "" Then
            CheckDateEmpty = False
            MsgBox("日期不能為空!", MsgBoxStyle.OkOnly, "提示")
            dtp_date.Focus()
            Exit Function
        End If

        If ds.Tables("NmetalSampleDepWeightCheck").Rows.Count <= 0 Then
            CheckDateEmpty = False
            MsgBox("子表無數據，請添加數據!", MsgBoxStyle.OkOnly, "提示")
            dtp_date.Focus()
            Exit Function
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("NmetalSampleDepWeightCheck").Rows.Count - 1

            If IsDBNull(ds.Tables("NmetalSampleDepWeightCheck").Rows(i)("PM_M_Code")) = True Then
                CheckDateEmpty = False
                Grid1.Focus()
                GridView1.FocusedRowHandle = i
                MsgBox("产品编号不能为空！", MsgBoxStyle.OkOnly, "提示")
                Exit Function
            End If

            If IsDBNull(ds.Tables("NmetalSampleDepWeightCheck").Rows(i)("PM_Type")) = True Then
                CheckDateEmpty = False
                MsgBox("產品類型不能为空！", MsgBoxStyle.OkOnly, "提示")
                Grid1.Focus()
                GridView1.FocusedRowHandle = i
                Exit Function
            End If
            If IsDBNull(ds.Tables("NmetalSampleDepWeightCheck").Rows(i)("DepWightOld")) = True Then
                CheckDateEmpty = False
                MsgBox("更改前重量不能為空！", MsgBoxStyle.OkOnly, "提示")
                Grid1.Focus()
                GridView1.FocusedRowHandle = i
                Exit Function
            End If
            If IsDBNull(ds.Tables("NmetalSampleDepWeightCheck").Rows(i)("DepWightNew")) = True Then
                CheckDateEmpty = False
                MsgBox("更改後重量不能為空！", MsgBoxStyle.OkOnly, "提示")
                Grid1.Focus()
                GridView1.FocusedRowHandle = i
                Exit Function
            End If
            If CDbl(ds.Tables("NmetalSampleDepWeightCheck").Rows(i)("TWeights")) > CDbl(ds.Tables("NmetalSampleDepWeightCheck").Rows(i)("DepWightNew")) Then
                If MsgBox("更改后重量比更改前产品重量小，是否继续操作！", MsgBoxStyle.OkCancel, "提示") = MsgBoxResult.Cancel Then
                    CheckDateEmpty = False
                    ds.Tables("NmetalSampleDepWeightCheck").Rows(i)("DepWightNew") = Nothing
                    Exit Function
                    Me.Close()
                End If

            End If
        Next
        '子表
        Dim j As Integer
        With ds.Tables("NmetalSampleDepWeightCheck")
            For i = 0 To .Rows.Count - 1
                For j = 0 To .Rows.Count - 1
                    If .Rows(i)("PS_NO") = .Rows(j)("PS_NO") And i <> j Then
                        CheckDateEmpty = False
                        MsgBox("记录中存在相同的'" & .Rows(i)("PS_NO") & "工序编号!", MsgBoxStyle.OkOnly, "提示")
                        Exit Function
                    End If
                Next
            Next
        End With
    End Function

    ''' <summary>
    ''' 退出按鈕 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 獲取單號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DepWeightCheck_GetID() As String
        Dim DepWeightCheckNo As String = ""
        Dim SoyyMM As String
        SoyyMM = "CH" & Format(Now, "yyMM")

        Dim ndi As New NmetalSampleDepWeightCheckInfo
        ndi = ndc.NmetalSampleDepWeightCheck_GetID(SoyyMM)

        If ndi.ChangeNO = "" Then
            DepWeightCheckNo = Trim(SoyyMM & "0001")
        Else
            DepWeightCheckNo = SoyyMM & Format(Val(Microsoft.VisualBasic.Right(ndi.ChangeNO, 4)) + 1, "0000")
        End If

        DepWeightCheck_GetID = DepWeightCheckNo
    End Function
    ''' <summary>
    ''' 創建臨時表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("NmetalSampleDepWeightCheck")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("ChangeNO", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("DepWightOld", GetType(String))

            .Columns.Add("DepWightNew", GetType(String))
            .Columns.Add("AddAction", GetType(String))
            .Columns.Add("AddDate", GetType(Date))
            .Columns.Add("Remark", GetType(String))

            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("Pro_NO", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("SubRemark", GetType(String))
            .Columns.Add("TWeights", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("NmetalSampleDepWeightCheck")

        With ds.Tables.Add("NmetalSampleDepWeightCheckDel")
            .Columns.Add("AutoID", GetType(String))
        End With
        With ds.Tables.Add("ProductSub") '子配件表
            .Columns.Add("M_Code", GetType(String))
        End With
    End Sub
    ''' <summary>
    ''' 新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        'If ds.Tables("NmetalSampleDepWeightCheck").Rows.Count < 10 Then
        '    Dim row As DataRow
        '    row = ds.Tables("NmetalSampleDepWeightCheck").NewRow
        '    ds.Tables("NmetalSampleDepWeightCheck").Rows.Add(row)
        'End If
        Dim fr As New frmNmetalSampleDepWeightCheckView
        fr = New frmNmetalSampleDepWeightCheckView
        fr.DepName = txtSO_dep.Text
        fr.DepNameID = txtSO_dep.EditValue
        fr.ShowDialog()
       
        If tempCode = "" Then
            Exit Sub
        Else
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempCode, ",")
            n = Len(Replace(tempCode, ",", "," & "*")) - Len(tempCode)
            Dim m, j As Integer
            For m = 0 To ds.Tables("NmetalSampleDepWeightCheck").Rows.Count - 1
                For j = 0 To n
                    If ds.Tables("NmetalSampleDepWeightCheck").Rows(m)("PS_NO") = arr(j) Then
                        MsgBox("记录中存在相同的'" & ds.Tables("NmetalSampleDepWeightCheck").Rows(m)("PS_NO") & "工序编号!", MsgBoxStyle.OkOnly, "提示")
                        Exit Sub
                    End If
                Next
            Next
            For i = 0 To n
                If arr(i) = "" Then
                    Exit Sub
                End If
                AddRow(arr(i))
            Next
        End If
        tempCode = ""

    End Sub
    Sub AddRow(ByVal M_Code As String) '通過工序編號導入相應信息(工藝類型,產品編號,類型,工序名稱等)
        Dim pic As New NmetalSampleProcessControl
        Dim pci As List(Of NmetalSampleProcessInfo)
        pci = pic.NmetalSampleProcessSub_GetList(Nothing, M_Code, Nothing, Nothing, Nothing, Nothing)
        If pci.Count = 0 Then Exit Sub
        '----------------------
        Dim row As DataRow
        row = ds.Tables("NmetalSampleDepWeightCheck").NewRow

        row("PM_M_Code") = pci(0).PM_M_Code
        row("PM_Type") = pci(0).Type3ID
        row("PS_NO") = pci(0).PS_NO
        row("PS_Name") = pci(0).PS_Name

        Dim msw As New NmetalSampleWareInventoryControler
        Dim nswi As List(Of NmetalSampleWareInventoryInfo)
        nswi = msw.NmetalSampleWareInventory_Getlist(Nothing, Nothing, M_Code, Nothing, Nothing, txtSO_dep.EditValue)
        If nswi.Count <= 0 Then
            row("DepWightOld") = 0
        Else
            row("DepWightOld") = nswi(0).SWI_Weight
        End If

        '2014-07-30
        Dim nscc As New NmetalSampleCollectionControler
        Dim nsci As List(Of NmetalSampleCollectionInfo)
        nsci = nscc.NmetalSampleCollection_GetListProductWightOld(EditDep, M_Code)
        If nsci.Count <= 0 Then
            row("TWeights") = 0
        Else
            row("TWeights") = nsci(0).TWeights
        End If
        

        ds.Tables("NmetalSampleDepWeightCheck").Rows.Add(row)


        GridView1.MoveLast()
    End Sub

    'Private Sub Grid2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'If GridView3.RowCount > 0 Then
    '    GridView1.SetFocusedRowCellValue(Me.PM_M_Code, GridView3.GetFocusedRowCellValue("PM_M_Code"))
    '    Dim pbiL As List(Of ProductBomInfo)
    '    Dim pbc As New ProductBomController
    '    pbiL = pbc.ProductBom_GetList(GridView3.GetFocusedRowCellValue("PM_M_Code"), Nothing, Nothing, Nothing, Nothing, Nothing)
    '    SubRowAdd(pbiL)
    'End If
    'GridView1.Focus()

    'End Sub
    Private Sub SubRowAdd(ByVal pbil As List(Of ProductBomInfo))
        Dim pc As New ProductController
        Dim piL As List(Of ProductInfo)
        Dim pmmcode As String
        pmmcode = ds.Tables("NmetalSampleDepWeightCheck").Rows(GridView1.FocusedRowHandle)("PM_M_Code").ToString
        piL = pc.Product_GetList(pmmcode, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        ds.Tables("ProductSub").Clear()
        Dim row As DataRow
        row = ds.Tables("ProductSub").NewRow

        'row("M_Name") = pmmcode

        If pbil.Count > 0 Then
            row("M_Code") = piL(0).PM_M_Code
        Else
            row("M_Code") = pmmcode
        End If

        ds.Tables("ProductSub").Rows.Add(row)
        For i As Integer = 0 To pbil.Count - 1
            row = ds.Tables("ProductSub").NewRow
            'row("M_Name") = pbil(i).PM_M_Code
            row("M_Code") = pbil(i).M_Code
            ds.Tables("ProductSub").Rows.Add(row)
        Next
        'Me.TreeList1.ExpandAll()
       
    End Sub

    'Private Sub TreeList1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim num As List(Of NmetalSampleDepWeightInfo)
    '        Dim StrM_Code As String
    '        Dim test As Decimal
    '        'StrM_Code = TreeList1.FocusedNode.GetValue("M_Code")
    '        ' GridView1.SetFocusedRowCellValue(Me.gclMode_Cd, StrM_Code)
    '        num = nsdc.NmetalSampleDepWeight_GetList(Nothing, StrM_Code)

    '        If num.Count <= 0 Then
    '            MsgBox("當前庫存不存在此編號！", MsgBoxStyle.OkOnly, "提示")
    '            Exit Sub
    '        End If

    '        test = num(0).DepWight
    '        GridView1.SetFocusedRowCellValue(Me.SWI_Weight, test)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    ''' <summary>
    ''' 刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        If GridView1.RowCount > 0 Then

            Dim deltemp As String = ""
            deltemp = ds.Tables("NmetalSampleDepWeightCheck").Rows(GridView1.FocusedRowHandle)("AutoID").ToString
            ds.Tables("NmetalSampleDepWeightCheck").Rows.RemoveAt(GridView1.FocusedRowHandle)

            Dim row As DataRow
            If deltemp <> "" Then
                row = ds.Tables("NmetalSampleDepWeightCheckDel").NewRow
                row("AutoID") = deltemp
                ds.Tables("NmetalSampleDepWeightCheckDel").Rows.Add(row)
            End If
        End If
    End Sub

End Class