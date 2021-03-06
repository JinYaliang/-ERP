Imports LFERP.Library.Purchase.ApplyPurchase
Imports LFERP.SystemManager.SystemUser
Imports LFERP.DataSetting
Imports LFERP.Library.WareHouse.WareInventory

Public Class frmApplyPurchase

    Dim ds As New DataSet
    Dim PreCheck As Boolean
    Dim strDPTID As String '存儲部門ID
    Dim strDPTName As String '存儲部門名稱
    Dim CodeCheckNull As Boolean '記錄物料編號(AP_M_Code)是否爲空
    Dim RowCode As Integer '存儲行號

    'Private Sub frmApplyPurchase_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
    '    If Label3.Text = "審核" Then
    '        frmShowWareQty.Close()
    '    End If
    'End Sub

    Private Sub frmApplyPurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = tempValue
        Label3.Visible = False
        txtapID.Text = tempValue3
        tempValue = ""
        tempValue3 = ""
        CreateTable()
        RowCode = 1000

        Select Case Label3.Text
            Case "申購單"
                If Edit = True Then
                    Me.Text = "申購單--修改"
                    Label1.Text = "申購單--修改"
                    LoadData(txtapID.Text)
                    AP_CheckWare.Visible = False
                ElseIf Edit = False Then
                    Me.Text = "申購單--新增"
                    Label1.Text = "申購單--新增"
                    txtapID.Text = ""
                    txtapDate.DateTime = Format(Now, "yyyy/MM/dd")
                    txtApplyPersonName.Text = UserName
                    AP_CheckWare.Visible = False
                End If

                XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage2.PageVisible = False
                XtraTabPage3.PageVisible = False
                cmdAdd.Visible = True
                cmdAddNoCode.Visible = True
                cmdDel.Visible = True
                ToolStripSeparator1.Visible = True
                txtapDpt.Focus()
            Case "申購單處理"
                Me.Text = "申購單--處理"
                Label1.Text = "申購單--處理"
                LoadData(txtapID.Text)
                txtapDpt.Enabled = False
                txtapDate.Enabled = False
                txtapReason.Enabled = False
                txtApplyPersonName.Enabled = False
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage2.PageVisible = False
                XtraTabPage3.PageVisible = False
                cmdAdd.Visible = False
                cmdAddNoCode.Visible = False
                cmdDel.Visible = False
                ToolStripSeparator1.Visible = False

            Case "審核"
                Me.Text = "申購單--審核"
                Label1.Text = "申購單--審核"
                LoadData(txtapID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                txtapDpt.Enabled = False
                txtapDate.Enabled = False
                txtapReason.Enabled = False
                txtApplyPersonName.Enabled = False
                XtraTabPage3.PageVisible = False
                cmdSave.Enabled = False
                cmdAdd.Visible = False
                cmdAddNoCode.Visible = False
                cmdDel.Visible = False
                ToolStripSeparator1.Visible = False

            Case "復核"
                Me.Text = "申購單--復核"
                Label1.Text = "申購單--復核"
                LoadData(txtapID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage3
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                CheckEdit1.Enabled = False
                ComboBoxEdit1.Enabled = False
                apCheckdate.Enabled = False
                apCheckAction.Enabled = False
                MemoEdit2.Enabled = False
                txtapDpt.Enabled = False
                txtapDate.Enabled = False
                txtapReason.Enabled = False
                txtApplyPersonName.Enabled = False
                cmdSave.Enabled = False
                cmdAdd.Visible = False
                cmdAddNoCode.Visible = False
                cmdDel.Visible = False
                ToolStripSeparator1.Visible = False

            Case "查看"
                Me.Text = "申購單--查看"
                Label1.Text = "申購單--查看"
                LoadData(txtapID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                GridView1.OptionsBehavior.Editable = False
                AP_CheckWare.Visible = True
                CheckEdit1.Enabled = False
                ComboBoxEdit1.Enabled = False
                apCheckdate.Enabled = False
                apCheckAction.Enabled = False
                CheckEdit2.Enabled = False
                ComboBoxEdit2.Enabled = False
                apRecheckdate.Enabled = False
                apRecheckAction.Enabled = False
                MemoEdit2.Enabled = False
                MemoEdit3.Enabled = False
                cmdSave.Visible = False
                MenuStrip.Enabled = False
                txtApplyPersonName.Enabled = False
                txtapDpt.Enabled = False
                txtapDate.Enabled = False
                txtapReason.Enabled = False
        End Select

    End Sub

    '獲取上一級部門名稱
    'Function GetDptName(ByVal DptID As String) As String

    '    Dim dc As New DepartmentControler
    '    Dim di As New DepartmentInfo
    '    Dim DID As String

    '    DID = Microsoft.VisualBasic.Left(DptID, 6)
    '    di = dc.Department_Get(DID, Nothing, Nothing)
    '    GetDptName = di.DPT_Name

    'End Function

    Sub LoadData(ByVal AP_ID As String)

        Dim objInfo As List(Of ApplyPurchaseInfo)
        Dim apc As New ApplyPurchaseControl
       
        Dim i As Integer
        Dim row As DataRow

        Try
            objInfo = apc.ApplyPurchase_GetList(AP_ID, Nothing, Nothing)

            If objInfo Is Nothing Then
                '沒有數據
                Exit Sub
            End If
            '

            txtapDpt.EditValue = objInfo(0).AP_ApplyDptName 'GetDptName(objInfo(0).AP_ApplyDptID) & "-" & 
            strDPTID = objInfo(0).AP_ApplyDptID

            txtApplyPersonName.Text = objInfo(0).AP_ApplyPersonName
            txtapDate.DateTime = objInfo(0).AP_ApplyDate
            txtapReason.EditValue = objInfo(0).AP_Applyreason

            For i = 0 To objInfo.Count - 1
                row = ds.Tables("ApplyPurchase").NewRow
                row("AP_Num") = objInfo(i).AP_Num
                row("AP_ApplyID") = objInfo(i).AP_ApplyID
                row("AP_M_Code") = objInfo(i).AP_M_Code
                row("AP_M_Gauge") = objInfo(i).AP_M_Gauge
                row("AP_M_Name") = objInfo(i).AP_M_Name
                row("AP_M_Unit") = objInfo(i).AP_M_Unit
                row("AP_Qty") = objInfo(i).AP_Qty
                row("AP_CheckWare") = objInfo(i).AP_CheckWare

                ds.Tables("ApplyPurchase").Rows.Add(row)

                If objInfo(i).AP_Check = False Then
                    CheckEdit1.Checked = False
                Else
                    CheckEdit1.Checked = True
                End If
                apCheckdate.Text = objInfo(i).AP_CheckDate
                apCheckAction.Text = objInfo(i).AP_CheckAction
                ComboBoxEdit1.EditValue = objInfo(i).AP_CheckType
                MemoEdit2.Text = objInfo(i).AP_CheckRemark
                If objInfo(i).AP_ReCheck = False Then
                    CheckEdit2.Checked = False
                Else
                    CheckEdit2.Checked = True
                End If

                apRecheckdate.Text = objInfo(i).AP_ReCheckDate
                apRecheckAction.Text = objInfo(i).AP_ReCheckAction
                ComboBoxEdit2.EditValue = objInfo(i).AP_ReCheckType
                MemoEdit3.Text = objInfo(i).AP_ReCheckRemark
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("ApplyPurchase")

            .Columns.Add("AP_ID", GetType(String))
            .Columns.Add("AP_Num", GetType(String))
            .Columns.Add("AP_ApplyID", GetType(String))
            .Columns.Add("AP_M_Code", GetType(String))
            .Columns.Add("AP_M_Gauge", GetType(String))
            .Columns.Add("AP_M_Name", GetType(String))
            .Columns.Add("AP_M_Unit", GetType(String))
            .Columns.Add("AP_Qty", GetType(String))
            .Columns.Add("AP_CheckWare", GetType(Boolean))
            .Columns.Add("AP_Applyreason", GetType(String))
            .Columns.Add("AP_ApplyDate", GetType(String))
            .Columns.Add("AP_ApplyDptID", GetType(String))
            .Columns.Add("AP_ApplyPerson", GetType(String))
            .Columns.Add("AP_ApplyPersonName", GetType(String))
            .Columns.Add("AP_Action", GetType(String))

        End With


        '創建刪除數據表
        With ds.Tables.Add("DelData")
            .Columns.Add("AP_ID", GetType(String))
            .Columns.Add("AP_Num", GetType(String))
            .Columns.Add("AP_M_Code", GetType(String))
        End With

        Grid.DataSource = ds.Tables("ApplyPurchase")
    End Sub
    Sub AddRow(ByVal strCode As String)
        Dim row As DataRow
        row = ds.Tables("ApplyPurchase").NewRow

        If strCode = "" Then
        Else
            Dim i As Integer

            For i = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
                If strCode = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Code") Then
                    MsgBox("此物料已添加，不需再添加!", 64, "提示")
                    Exit Sub
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            If objInfo Is Nothing Then Exit Sub
            If objInfo.M_IsEnabled = False Then  '判斷當前物料是否可用 2012-2-20，不可用不允許報價！
                MsgBox("當前物料不可用，不允許申購！")
                Exit Sub
            End If

            row("AP_Num") = Nothing
            row("AP_ID") = Nothing

            row("AP_M_Code") = objInfo.M_Code
            row("AP_M_Name") = objInfo.M_Name
            row("AP_M_Unit") = objInfo.M_Unit
            row("AP_M_Gauge") = objInfo.M_Gauge
            row("AP_ApplyID") = Nothing
            row("AP_Qty") = 0
            row("AP_CheckWare") = False

            ds.Tables("ApplyPurchase").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Sub
    Sub SaveDataNew()
        On Error Resume Next
        Dim api As New ApplyPurchaseInfo
        Dim apc As New ApplyPurchaseControl
        Dim sc As New SystemUserController

        If Trim(txtapDpt.EditValue) = "" Then
            MsgBox("申購部門不能為空,請選擇部門!", 64, "提示")
            txtapDpt.Focus()
            Exit Sub
        End If
        If Trim(txtApplyPersonName.Text) = "" Then
            MsgBox("申購人不能為空,請輸入申購人!", 64, "提示")
            txtApplyPersonName.Focus()
            Exit Sub
        End If
        If ds.Tables("ApplyPurchase").Rows.Count = 0 Then
            MsgBox("請添加申購物料!", 64, "提示")
            Exit Sub
        End If
        Dim j As Integer
        For j = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
            If Trim(ds.Tables("ApplyPurchase").Rows(j)("AP_M_Name")) = "" Then
                MsgBox("物料名稱不能爲空!", 64, "提示")
                Exit Sub
            End If
            If Trim(ds.Tables("ApplyPurchase").Rows(j)("AP_M_Gauge")) = "" Then
                MsgBox("物料規格不能爲空!", 64, "提示")
                Exit Sub
            End If
            If ds.Tables("ApplyPurchase").Rows(j)("AP_Qty") <= 0 Then
                MsgBox("申購數量不能小於或等於零!", 64, "提示")
                Exit Sub
            End If
            If Trim(ds.Tables("ApplyPurchase").Rows(j)("AP_M_Unit")) = "" Then
                MsgBox("單位不能爲空!", 64, "提示")
                Exit Sub
            End If
        Next

        Dim i As Integer
        Dim GetID, GetNum As String
        txtapID.Text = GetApplyID()
        GetID = txtapID.Text

        api.AP_ApplyDate = txtapDate.EditValue
        api.AP_ApplyDptID = strDPTID
        api.AP_Applyreason = txtapReason.Text

        api.AP_ApplyPersonName = txtApplyPersonName.Text
        api.AP_Action = InUserID
        '-------------------------------------------------------------------
        api.AP_Check = False
        api.AP_CheckAction = ""
        api.AP_CheckDate = Nothing
        api.AP_CheckRemark = ""
        api.AP_CheckType = ""
        api.AP_ReCheck = False
        api.AP_ReCheckDate = Nothing
        api.AP_ReCheckAction = Nothing
        api.AP_ReCheckType = Nothing
        api.AP_ReCheckRemark = ""
        '--------------------------------------------------------------------

        For i = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
            GetNum = GetApplyNum()
            api.AP_ID = GetID
            ds.Tables("ApplyPurchase").Rows(i)("AP_Num") = GetNum
            api.AP_Num = ds.Tables("ApplyPurchase").Rows(i)("AP_Num")
            api.AP_M_Code = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Code")
            api.AP_M_Name = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Name")
            api.AP_M_Gauge = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Gauge")

            If IsDBNull(ds.Tables("ApplyPurchase").Rows(i)("AP_Qty")) Then
                api.AP_Qty = "0"
            Else
                api.AP_Qty = ds.Tables("ApplyPurchase").Rows(i)("AP_Qty")
            End If

            api.AP_M_Unit = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Unit")

            If IsDBNull(ds.Tables("ApplyPurchase").Rows(i)("AP_ApplyID")) Then
                api.AP_ApplyID = Nothing
            Else
                api.AP_ApplyID = ds.Tables("ApplyPurchase").Rows(i)("AP_ApplyID")
            End If
            api.AP_CheckWare = ds.Tables("ApplyPurchase").Rows(i)("AP_CheckWare")
            apc.ApplyPurchase_Add(api)
        Next
        MsgBox("添加成功!", 64, "提示")
        Me.Close()

    End Sub

    Sub SaveDataEdit()
        On Error Resume Next
        Dim dpm As New DepartmentControler
        Dim di As New DepartmentInfo

        If Trim(txtapDpt.EditValue) = "" Then
            MsgBox("申購部門不能為空，請選擇部門!", 64, "提示")
            txtapDpt.Focus()
            Exit Sub
        End If
        If Trim(txtApplyPersonName.Text) = "" Then
            MsgBox("申購人不能為空,請輸入申購人!", 64, "提示")
            txtApplyPersonName.Focus()
            Exit Sub
        End If
        If ds.Tables("ApplyPurchase").Rows.Count = 0 Then
            MsgBox("請添加申購物料!", 64, "提示")
            Exit Sub
        End If

        Dim j As Integer
        For j = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
            If Trim(ds.Tables("ApplyPurchase").Rows(j)("AP_M_Name")) = "" Then
                MsgBox("物料名稱不能爲空!", 64, "提示")
                Exit Sub
            End If
            If Trim(ds.Tables("ApplyPurchase").Rows(j)("AP_M_Gauge")) = "" Then
                MsgBox("物料規格不能爲空!", 64, "提示")
                Exit Sub
            End If
            If ds.Tables("ApplyPurchase").Rows(j)("AP_Qty") <= 0 Then
                MsgBox("申購數量不能小於或等於零!", 64, "提示")
                Exit Sub
            End If
            If Trim(ds.Tables("ApplyPurchase").Rows(j)("AP_M_Unit")) = "" Then
                MsgBox("單位不能爲空!", 64, "提示")
                Exit Sub
            End If
        Next

        Dim ai As New ApplyPurchaseInfo
        Dim ac As New ApplyPurchaseControl
        Dim sc As New SystemUserController

        If ds.Tables("DelData").Rows.Count > 0 Then
            Dim k As Integer
            For k = 0 To ds.Tables("DelData").Rows.Count - 1

                Dim apc As New ApplyPurchaseControl
                If Not IsDBNull(ds.Tables("DelData").Rows(k)("AP_Num")) Then
                    apc.ApplyPurchase_Delete(Nothing, ds.Tables("DelData").Rows(k)("AP_Num"))

                End If
            Next
        End If

        ai.AP_ID = txtapID.Text
        ai.AP_ApplyDptID = strDPTID
        ai.AP_ApplyDate = txtapDate.EditValue
        'ai.AP_ApplyDate = Format(Now, "yyyy/MM/dd")
        ai.AP_Applyreason = txtapReason.Text
        ai.AP_ApplyPersonName = txtApplyPersonName.Text
        ai.AP_Action = InUserID

        Dim i As Integer

        For i = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1

            If IsDBNull(ds.Tables("ApplyPurchase").Rows(i)("AP_Num")) Then  '新增物料詳細信息

                ai.AP_Num = GetApplyNum()

                If IsDBNull(ds.Tables("ApplyPurchase").Rows(i)("AP_ApplyID")) Then
                    ai.AP_ApplyID = Nothing
                Else
                    ai.AP_ApplyID = ds.Tables("ApplyPurchase").Rows(i)("AP_ApplyID")
                End If
                ai.AP_M_Code = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Code")
                ai.AP_M_Name = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Name")
                ai.AP_M_Gauge = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Gauge")
                ai.AP_M_Unit = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Unit")

                ai.AP_Qty = CSng(ds.Tables("ApplyPurchase").Rows(i)("AP_Qty"))
                ac.ApplyPurchase_Add(ai)

            ElseIf Not IsDBNull(ds.Tables("ApplyPurchase").Rows(i)("AP_Num")) Then ' 修改物料詳細信息

                If IsDBNull(ds.Tables("ApplyPurchase").Rows(i)("AP_ApplyID")) Then
                    ai.AP_ApplyID = Nothing
                Else
                    ai.AP_ApplyID = ds.Tables("ApplyPurchase").Rows(i)("AP_ApplyID")
                End If
                ai.AP_M_Code = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Code")
                ai.AP_M_Name = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Name")
                ai.AP_M_Gauge = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Gauge")
                ai.AP_M_Unit = ds.Tables("ApplyPurchase").Rows(i)("AP_M_Unit")
                ai.AP_Num = ds.Tables("ApplyPurchase").Rows(i)("AP_Num")
                ai.AP_Qty = CSng(ds.Tables("ApplyPurchase").Rows(i)("AP_Qty"))
                ac.ApplyPurchase_Update(ai)

            End If

        Next
        If Label3.Text = "申購單處理" Then
            MsgBox("申購單處理成功!", 64, "提示")
        Else
            MsgBox("申購單修改成功!", 64, "提示")
        End If
        Me.Close()
    End Sub
    ''' <summary>
    ''' 審核
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateCheck()

        Dim api As New ApplyPurchaseInfo
        Dim apc As New ApplyPurchaseControl
        Dim sc As New SystemUserController
        Dim notCheckWare As Integer  '記錄可採購物料數，以確定是否所有物料都不採購
        Dim AvbYesNo As Integer
        Dim i, j As Integer
        notCheckWare = 0
        For i = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
            If ds.Tables("ApplyPurchase").Rows(i)("AP_CheckWare") = False Then
                notCheckWare = notCheckWare + 1
            End If
        Next
        If notCheckWare = ds.Tables("ApplyPurchase").Rows.Count Then
            AvbYesNo = MsgBox("確定此申購單中所有的物料都不可採購？", vbYesNo + vbQuestion, "提示")
            If AvbYesNo = vbNo Then
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                Exit Sub
            End If
        End If

        api.AP_ID = txtapID.Text
        api.AP_Check = CheckEdit1.Checked
        api.AP_CheckAction = InUser
        api.AP_CheckDate = Format(Now, "yyyy/MM/dd")
        api.AP_CheckRemark = MemoEdit2.Text
        api.AP_CheckType = ComboBoxEdit1.EditValue

        For j = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
            api.AP_CheckWare = ds.Tables("ApplyPurchase").Rows(j)("AP_CheckWare")
            api.AP_Num = ds.Tables("ApplyPurchase").Rows(j)("AP_Num")
            apc.ApplyPurchase_UpdateCheck(api)
        Next
        MsgBox("已保存審核信息!", 64, "提示")

        Me.Close()

    End Sub
    ''' <summary>
    ''' 復核
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateReCheck()

        Dim api As New ApplyPurchaseInfo
        Dim apc As New ApplyPurchaseControl
        Dim sc As New SystemUserController
        Dim notCheckWare As Integer  '記錄可採購物料數，以確定是否所有物料都不採購
        Dim AvbYesNo As Integer
        Dim i, j As Integer
        notCheckWare = 0
        For i = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
            If ds.Tables("ApplyPurchase").Rows(i)("AP_CheckWare") = False Then
                notCheckWare = notCheckWare + 1
            End If
        Next
        If notCheckWare = ds.Tables("ApplyPurchase").Rows.Count Then
            AvbYesNo = MsgBox("確定此申購單中所有的物料都不可採購？", vbYesNo + vbQuestion, "提示")
            If AvbYesNo = vbNo Then
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                Exit Sub
            End If
        End If
        api.AP_ID = txtapID.Text
        api.AP_ReCheck = CheckEdit2.Checked
        api.AP_ReCheckAction = InUser
        api.AP_ReCheckDate = Format(Now, "yyyy/MM/dd")
        api.AP_ReCheckRemark = MemoEdit3.Text
        api.AP_ReCheckType = ComboBoxEdit2.EditValue

        For j = 0 To ds.Tables("ApplyPurchase").Rows.Count - 1
            api.AP_CheckWare = ds.Tables("ApplyPurchase").Rows(j)("AP_CheckWare")
            api.AP_Num = ds.Tables("ApplyPurchase").Rows(j)("AP_Num")

            apc.ApplyPurchase_UpdateReCheck(api)
        Next
        MsgBox("已保存復核信息!", 64, "提示")

        Me.Close()

    End Sub
    ''' <summary>
    ''' 自動申購單號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetApplyID() As String
        Dim str As String
        str = CStr(Format(Now, "yyMM"))
        Dim ai As New ApplyPurchaseInfo
        Dim ac As New ApplyPurchaseControl

        ai = ac.ApplyPurchase_GetID(str)
        If ai Is Nothing Then
            GetApplyID = "AP" & str & "0001"
        Else
            GetApplyID = "AP" & str & Mid((CInt(Mid(ai.AP_ID, 7)) + 10001), 2)
        End If
    End Function
    ''' <summary>
    ''' 自動申購編號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetApplyNum() As String
        Dim ai1 As New ApplyPurchaseInfo
        Dim ac1 As New ApplyPurchaseControl

        ai1 = ac1.ApplyPurchase_GetNum("A")
        If ai1 Is Nothing Then
            GetApplyNum = "A" & "000000001"
        Else
            GetApplyNum = "A" & Mid((CInt(Mid(ai1.AP_Num, 2)) + 1000000001), 2)
        End If

    End Function

    ''' <summary>
    ''' 物料信息的添加
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        tempCode = ""
        frmBOMSelect.ShowDialog()
        '增加記錄
        If tempCode = "" Then
            Exit Sub
        Else
            AddRow(tempCode)
        End If
    End Sub
 
    ''' <summary>
    '''  物料信息的刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If ds.Tables("ApplyPurchase").Rows.Count = 0 Then Exit Sub

        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "AP_M_Code")

        If DelTemp = "AP_M_Code" Then
        Else
            '在刪除表中增加被刪除的記錄
            Dim row As DataRow = ds.Tables("DelData").NewRow

            row("AP_ID") = ds.Tables("ApplyPurchase").Rows(GridView1.FocusedRowHandle)("AP_ID")
            row("AP_Num") = ds.Tables("ApplyPurchase").Rows(GridView1.FocusedRowHandle)("AP_Num")
            row("AP_M_Code") = DelTemp
            ds.Tables("DelData").Rows.Add(row)
        End If
        ds.Tables("ApplyPurchase").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label3.Text
            Case "申購單"
                If Edit = False Then
                    SaveDataNew()
                Else
                    SaveDataEdit()
                End If
            Case "審核"
                UpdateCheck()
            Case "復核"
                UpdateReCheck()
            Case "申購單處理"
                SaveDataEdit()
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 選擇部門代號
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub txtapDpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtapDpt.Click
        frmDepartmentSelect.DptID = ""
        frmDepartmentSelect.DptName = ""
        'frmDepartmentSelect.Location = New Point(Me.Left, Me.Top)
        frmDepartmentSelect.StartPosition = FormStartPosition.Manual
        frmDepartmentSelect.Left = Me.Left + txtapDpt.Left + 222
        frmDepartmentSelect.Top = Me.Top + 166
        frmDepartmentSelect.Size = New Size(213, 245)

        frmDepartmentSelect.ShowDialog()

        If frmDepartmentSelect.DptID = "" Then

        Else
            strDPTID = frmDepartmentSelect.DptID
            txtapDpt.Text = frmDepartmentSelect.UpDptName & "-" & frmDepartmentSelect.DptName
        End If
    End Sub

    Private Sub cmdAddNoCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddNoCode.Click
        Dim row As DataRow
        row = ds.Tables("ApplyPurchase").NewRow

        row("AP_Num") = Nothing
        row("AP_ID") = Nothing
        row("AP_M_Code") = ""
        row("AP_M_Name") = ""
        row("AP_M_Unit") = Nothing
        row("AP_M_Gauge") = Nothing
        row("AP_ApplyID") = Nothing
        row("AP_Qty") = 0

        AP_CheckWare.Visible = False
        ds.Tables("ApplyPurchase").Rows.Add(row)
        GridView1.MoveLast()

    End Sub

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        If Label3.Text = "審核" Or Label3.Text = "復核" Then
            AP_M_Code.OptionsColumn.ReadOnly = True
            AP_M_Name.OptionsColumn.ReadOnly = True
            AP_M_Gauge.OptionsColumn.ReadOnly = True
            AP_M_Unit.OptionsColumn.ReadOnly = True
            AP_Qty.OptionsColumn.ReadOnly = True
            AP_ApplyID.OptionsColumn.ReadOnly = True
            AP_CheckWare.OptionsColumn.ReadOnly = False
        ElseIf Label3.Text = "申購單處理" Then
            Dim objInfo As New ApplyPurchaseInfo
            Dim apc As New ApplyPurchaseControl
            objInfo = apc.ApplyPurchase_Get(GridView1.GetFocusedRowCellValue("AP_Num").ToString)
            If objInfo.AP_M_Code = "" And objInfo.AP_CheckWare = True Then
                AP_M_Code.OptionsColumn.ReadOnly = True
                AP_M_Name.OptionsColumn.ReadOnly = True
                AP_M_Gauge.OptionsColumn.ReadOnly = True
                AP_M_Unit.OptionsColumn.ReadOnly = True
                AP_Qty.OptionsColumn.ReadOnly = True
                AP_ApplyID.OptionsColumn.ReadOnly = True
                AP_CheckWare.OptionsColumn.ReadOnly = True
                CodeCheckNull = True
            Else
                CodeCheckNull = False
            End If
        Else
            Dim sCode As String
            sCode = GridView1.GetFocusedRowCellValue("AP_M_Code")
            If sCode = "" Then
                AP_M_Code.OptionsColumn.ReadOnly = True
                AP_M_Name.OptionsColumn.ReadOnly = False
                AP_M_Gauge.OptionsColumn.ReadOnly = False
                AP_M_Unit.OptionsColumn.ReadOnly = False
            Else
                AP_M_Code.OptionsColumn.ReadOnly = True
                AP_M_Name.OptionsColumn.ReadOnly = True
                AP_M_Gauge.OptionsColumn.ReadOnly = True
                AP_M_Unit.OptionsColumn.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub CheckEdit1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit1.CheckedChanged
        If Label3.Text = "審核" Then cmdSave.Enabled = Not cmdSave.Enabled
    End Sub

    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        If Label3.Text = "復核" Then cmdSave.Enabled = Not cmdSave.Enabled
    End Sub

    Private Sub rbeM_Code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rbeM_Code.ButtonClick
        If Label3.Text = "申購單處理" And CodeCheckNull = True Then
            tempCode = ""
            frmBOMSelect.ShowDialog()
            If tempCode = "" Then
            Else
                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo
                objInfo = mc.MaterialCode_Get(tempCode)

                GridView1.SetFocusedRowCellValue(AP_M_Code, objInfo.M_Code)
                GridView1.SetFocusedRowCellValue(AP_M_Name, objInfo.M_Name)
                GridView1.SetFocusedRowCellValue(AP_M_Unit, objInfo.M_Unit)
                GridView1.SetFocusedRowCellValue(AP_M_Gauge, objInfo.M_Gauge)
            End If
        ElseIf Label3.Text = "審核" And GridView1.GetFocusedRowCellValue("AP_M_Code").ToString <> "" Then
            If RowCode = GridView1.FocusedRowHandle And Grid1.Visible = True Then '再次單擊rbeM_Code控件，隱藏已顯示的Grid1控件
                Grid1.Visible = False
            Else
                Grid1.Top = 44
                Dim wtc As New WareInventoryMTController
                Grid1.DataSource = wtc.WareInventory_GetList3(GridView1.GetFocusedRowCellValue("AP_M_Code").ToString, Nothing, "True")
                Grid1.Top = GridView1.FocusedRowHandle * 20 + Grid1.Top '設置Grid1控件顯示在焦點行下方
                Grid1.Visible = True
                RowCode = GridView1.FocusedRowHandle '記錄焦點行號，用於判斷單擊時是否爲原焦點行
            End If
        End If
    End Sub

    Private Sub Grid1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid1.Leave
        Grid1.Visible = False
    End Sub

    Private Sub rbeM_Code_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbeM_Code.Leave
        If Label3.Text = "審核" Then
            If Grid1.Focus = True Then

            Else
                Grid1.Visible = False
            End If
        End If
    End Sub

    Private Sub Grid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid.Click

    End Sub
End Class