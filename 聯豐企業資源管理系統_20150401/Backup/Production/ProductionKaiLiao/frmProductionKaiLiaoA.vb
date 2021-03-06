Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.Library.ProductionSchedule
Imports LFERP.DataSetting
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.WareHouse
Imports LFERP.Library.ProductionWareInventory
Imports LFERP.Library.Orders

Public Class frmProductionKaiLiaoA
#Region "屬性"
    Dim ds As New DataSet
    Dim dc As New DepartmentControler
    Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim strWHID As String

    Dim pfc As New LFERP.Library.ProductionController.ProductionFieldControl
    Dim pic As New ProductInventoryController
#End Region

#Region "窗體啟動載入事件"
    Sub LoadProductNo()
        If Label8.Text = "PreView" Or Label8.Text = "Check" Then
            Dim oscon As New OrdersSubController
            gluOS_BatchID.Properties.DisplayMember = "OS_BatchID"
            gluOS_BatchID.Properties.ValueMember = "OS_BatchID"
            gluOS_BatchID.Properties.DataSource = oscon.OrdersSub_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            'gluOS_BatchID.Properties.DataSource = oscon.OrdersSub_GetList4(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        Dim mc As New ProcessMainControl
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)

        GluDep.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        GluDep.Properties.DisplayMember = "DepName"
        GluDep.Properties.ValueMember = "DepID"

        Dim whcon As New WareHouseController
        Me.gluWH_IDA.DataSource = whcon.WareHouse_GetList1(Nothing)
        Me.gluWH_IDA.DisplayMember = "WH_AllName"
        Me.gluWH_IDA.ValueMember = "WH_ID"
    End Sub
    Private Sub frmProductionKaiLiao_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%
        Label8.Text = tempValue2
        Label9.Text = tempValue3
        tempValue2 = ""
        tempValue3 = ""

        txtCNO.Enabled = False
        LoadProductNo()

        CreateTable()
        Select Case Label8.Text
            Case "開料單"
                If Edit = False Then
                    Me.Text = "開料單--新增"
                    Dim ui As List(Of UserPowerInfo)
                    Dim uc As New UserPowerControl
                    ui = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

                    If ui.Count = 0 Then
                        Exit Sub
                    Else
                        GluDep.EditValue = ui(0).DepID
                    End If
                    CBType.EditValue = "生產加工"
                Else
                    LoadData(Label9.Text)
                    Me.Text = "開料單--修改" & Label9.Text
                End If
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "PreView"
                Me.Text = "開料單--查看" & Label9.Text
                LoadData(Label9.Text)
                cmdSave.Visible = False
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "Check"
                Me.Text = "開料單--審核" & Label9.Text
                LoadData(Label9.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage2

                For i = 0 To XtraTabPage1.Controls.Count - 2
                    XtraTabPage1.Controls(i).Enabled = False
                Next
                GridView1.OptionsBehavior.Editable = False
                Grid.ContextMenuStrip.Enabled = False
        End Select
    End Sub
#End Region

#Region "創建臨時表"
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("KaiLiao")
            .Columns.Add("IndexNo", GetType(String))
            .Columns.Add("C_NO", GetType(String))
            .Columns.Add("WH_ID", GetType(String))
            .Columns.Add("WI_Qty", GetType(Double))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("C_Qty", GetType(Integer))
            .Columns.Add("C_Weight", GetType(Double))  '重量
            .Columns.Add("C_Cishu", GetType(Integer))
            .Columns.Add("C_Type", GetType(String))
        End With
        Grid.DataSource = ds.Tables("KaiLiao")

        With ds.Tables.Add("DelKaiLiao")
            .Columns.Add("IndexNO", GetType(String))
        End With

        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")
    End Sub
#End Region

#Region "返回數據"
    Public Function LoadData(ByVal C_Number As String) As Boolean
        LoadData = True
        ds.Tables("KaiLiao").Clear()
        Dim pi As List(Of ProductionKaiLiaoInfo)
        Dim pc As New ProductionKaiLiaoControl
        pi = pc.ProductionKaiLiaoA_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, C_Number, Nothing, Nothing, Nothing)
        Try
            If pi.Count = 0 Then
                MsgBox("沒有數據")
                LoadData = False
                Exit Function
            Else
                txtCNO.Text = pi(0).C_NO
                txtC_Number.Text = pi(0).C_Number
                CBType.EditValue = pi(0).Pro_Type
                PM_M_Code.EditValue = pi(0).PM_M_Code
                gluOS_BatchID.EditValue = pi(0).OS_BatchID
                gluType.EditValue = pi(0).PM_Type
                GluDep.EditValue = pi(0).PS_Dep
                txtAction.Text = pi(0).PS_Action
                txtRemark.Text = pi(0).C_Remark

                If pi(0).C_Check = False Then
                    CheckEdit1.Checked = False
                Else
                    CheckEdit1.Checked = True
                End If
                txtCheckAction.Text = pi(0).C_CheckAction
                txtCheckRemark.Text = pi(0).C_CheckRemark

                Dim i As Integer
                Dim row As DataRow
                For i = 0 To pi.Count - 1
                    row = ds.Tables("KaiLiao").NewRow
                    row("IndexNo") = pi(i).IndexNo
                    row("WH_ID") = pi(i).WH_ID
                    row("M_Code") = pi(i).M_Code
                    row("M_Name") = pi(i).M_Name
                    row("M_Gauge") = pi(i).M_Gauge
                    row("M_Unit") = pi(i).M_Unit
                    row("C_Qty") = pi(i).C_Qty
                    row("C_Weight") = pi(i).C_Weight
                    row("C_Cishu") = pi(i).C_Cishu
                    row("C_Type") = pi(i).C_Type
                    row("C_NO") = pi(i).C_NO
                    ds.Tables("KaiLiao").Rows.Add(row)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
#End Region

#Region "新增數據"
    Sub DataNew()
        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl
        txtC_Number.Text = GetKNO()
        pi.C_Number = txtC_Number.Text
        pi.Pro_Type = CBType.EditValue
        pi.PM_M_Code = PM_M_Code.Text
        pi.PM_Type = gluType.EditValue
        pi.PS_Dep = GluDep.EditValue
        pi.PS_Action = txtAction.Text
        pi.C_Remark = txtRemark.Text
        pi.C_Action = InUserID
        pi.C_AddDate = Format(Now, "yyyy/MM/dd")
        '李超新增------------------
        pi.OS_BatchID = gluOS_BatchID.EditValue
        If gluOS_BatchID.EditValue <> String.Empty Then
            Dim oslist As New List(Of OrdersSubInfo)
            Dim oscon As New OrdersSubController
            oslist = oscon.OrdersSub_GetList(Nothing, gluOS_BatchID.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If oslist.Count > 0 Then
                pi.OM_ID = oslist(0).OM_ID
            End If
        End If
        '------------------
        Dim i As Integer
        For i = 0 To ds.Tables("KaiLiao").Rows.Count - 1
            pi.C_NO = GetNO()
            pi.WH_ID = ds.Tables("KaiLiao").Rows(i)("WH_ID")
            pi.M_Code = ds.Tables("KaiLiao").Rows(i)("M_Code")
            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Qty")) Then
                pi.C_Qty = 0
            Else
                pi.C_Qty = ds.Tables("KaiLiao").Rows(i)("C_Qty")
            End If
            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Weight")) Then
                pi.C_Weight = 0
            Else
                pi.C_Weight = ds.Tables("KaiLiao").Rows(i)("C_Weight")
            End If
            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Cishu")) Then
                pi.C_Cishu = 0
            Else
                pi.C_Cishu = ds.Tables("KaiLiao").Rows(i)("C_Cishu")
            End If
            If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Type")) Then
                pi.C_Type = Nothing
            Else
                pi.C_Type = ds.Tables("KaiLiao").Rows(i)("C_Type")
            End If
            pc.ProductionKaiLiaoA_Add(pi)
        Next
        MsgBox("已保存,單號: " & txtC_Number.Text & " ")
        Me.Close()
    End Sub
#End Region

#Region "修改數據"
    Sub DataEdit()
        Dim i As Integer
        '更新刪除的記錄
        If ds.Tables("DelKaiLiao").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelKaiLiao").Rows.Count - 1

                Dim odc As New ProductionKaiLiaoControl

                If Not IsDBNull(ds.Tables("DelKaiLiao").Rows(j)("IndexNo")) Then
                    odc.ProductionKaiLiao_Delete(Nothing, ds.Tables("DelKaiLiao").Rows(j)("IndexNo"))
                End If
            Next j
        End If

        For i = 0 To ds.Tables("KaiLiao").Rows.Count - 1
            If Not IsDBNull(ds.Tables("KaiLiao").Rows(i)("IndexNo")) Then
                Dim pi As New ProductionKaiLiaoInfo
                Dim pc As New ProductionKaiLiaoControl
                pi.C_Number = Me.txtC_Number.Text
                pi.WH_ID = strWHID
                pi.Pro_Type = CBType.EditValue
                pi.PM_M_Code = PM_M_Code.Text
                pi.PM_Type = gluType.EditValue
                pi.PS_Dep = GluDep.EditValue
                pi.PS_Action = txtAction.Text
                pi.C_Remark = txtRemark.Text
                pi.C_Action = InUserID
                pi.C_AddDate = Format(Now, "yyyy/MM/dd")
                '李超新增-------------------------------------
                pi.OS_BatchID = gluOS_BatchID.EditValue
                If gluOS_BatchID.EditValue <> String.Empty Then
                    Dim oslist As New List(Of OrdersSubInfo)
                    Dim oscon As New OrdersSubController
                    oslist = oscon.OrdersSub_GetList(Nothing, gluOS_BatchID.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If oslist.Count > 0 Then
                        pi.OM_ID = oslist(0).OM_ID
                    End If
                End If
                '-----------------------------------------------
                pi.C_NO = ds.Tables("KaiLiao").Rows(i)("C_NO")
                pi.WH_ID = ds.Tables("KaiLiao").Rows(i)("WH_ID")
                pi.M_Code = ds.Tables("KaiLiao").Rows(i)("M_Code")
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Qty")) Then
                    pi.C_Qty = 0
                Else
                    pi.C_Qty = ds.Tables("KaiLiao").Rows(i)("C_Qty")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Weight")) Then
                    pi.C_Weight = 0
                Else
                    pi.C_Weight = ds.Tables("KaiLiao").Rows(i)("C_Weight")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Cishu")) Then
                    pi.C_Cishu = 0
                Else
                    pi.C_Cishu = ds.Tables("KaiLiao").Rows(i)("C_Cishu")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Type")) Then
                    pi.C_Type = Nothing
                Else
                    pi.C_Type = ds.Tables("KaiLiao").Rows(i)("C_Type")
                End If
                pc.ProductionKaiLiaoA_Update(pi)

            ElseIf IsDBNull(ds.Tables("KaiLiao").Rows(i)("IndexNo")) Then
                Dim pi As New ProductionKaiLiaoInfo
                Dim pc As New ProductionKaiLiaoControl
                pi.C_Number = Me.txtC_Number.Text

                pi.Pro_Type = CBType.EditValue
                pi.PM_M_Code = PM_M_Code.Text
                pi.PM_Type = gluType.EditValue
                pi.PS_Dep = GluDep.EditValue
                pi.PS_Action = txtAction.Text
                pi.C_Remark = txtRemark.Text
                pi.C_Action = InUserID
                pi.C_AddDate = Format(Now, "yyyy/MM/dd")
                pi.OS_BatchID = gluOS_BatchID.EditValue
                pi.OS_BatchID = gluOS_BatchID.EditValue
                '李超--------------------------------------
                If gluOS_BatchID.EditValue <> String.Empty Then
                    Dim oslist As New List(Of OrdersSubInfo)
                    Dim oscon As New OrdersSubController
                    oslist = oscon.OrdersSub_GetList(Nothing, gluOS_BatchID.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If oslist.Count > 0 Then
                        pi.OM_ID = oslist(0).OM_ID
                    End If
                End If
                '李超--------------------------------------
                pi.C_NO = GetNO()
                pi.WH_ID = ds.Tables("KaiLiao").Rows(i)("WH_ID")
                pi.M_Code = ds.Tables("KaiLiao").Rows(i)("M_Code")

                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Qty")) Then
                    pi.C_Qty = 0
                Else
                    pi.C_Qty = ds.Tables("KaiLiao").Rows(i)("C_Qty")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Weight")) Then
                    pi.C_Weight = 0
                Else
                    pi.C_Weight = ds.Tables("KaiLiao").Rows(i)("C_Weight")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Cishu")) Then
                    pi.C_Cishu = 0
                Else
                    pi.C_Cishu = ds.Tables("KaiLiao").Rows(i)("C_Cishu")
                End If
                If IsDBNull(ds.Tables("KaiLiao").Rows(i)("C_Type")) Then
                    pi.C_Type = Nothing
                Else
                    pi.C_Type = ds.Tables("KaiLiao").Rows(i)("C_Type")
                End If
                pc.ProductionKaiLiaoA_Add(pi)
            End If
        Next
        MsgBox("已保存,單號: " & Me.txtC_Number.Text & " ")
        Me.Close()
    End Sub
#End Region

#Region "審核事件"
    Sub UpdateCheck()
        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl

        pi.C_Number = txtC_Number.Text
        pi.C_Check = CheckEdit1.Checked
        pi.C_CheckAction = InUserID
        pi.C_CheckRemark = txtCheckRemark.Text

        If pc.ProductionKaiLiaoA_UpdateCheck(pi) = True Then
            MsgBox("審核狀態已改變!")
        Else
            MsgBox("審核失敗,請檢查原因!")
        End If
        Me.Close()
    End Sub
#End Region

#Region "按鍵事件"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label8.Text
            Case "開料單"
                If Edit = False Then
                    If CheckData() = True Then
                        DataNew()
                    End If

                Else
                    If CheckData() = True Then
                        DataEdit()
                    End If
                End If
            Case "Check"
                If CheckData() = True Then
                    UpdateCheck()
                End If
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

#End Region

#Region "子表新增刪除事件"
    Private Sub cmsCodeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeAdd.Click
        frmBOMWareSelect.XtraTabPage1.PageVisible = True
        frmBOMWareSelect.XtraTabPage2.PageVisible = False  '不顯示批次信息
        frmBOMWareSelect.XtraTabPage3.PageVisible = True

        frmBOMWareSelect.EditItem = "開料管理"
        frmBOMWareSelect.EditWH_IDValue = strWHID
        frmBOMWareSelect.EditValue = PM_M_Code.Text
        frmBOMWareSelect.ShowDialog()

        Dim StrEditCode As String = frmBOMWareSelect.EditCode
        Dim StrEditWH_ID As String = frmBOMWareSelect.EditWH_ID
        '增加記錄
        If frmBOMWareSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            AddRow(StrEditCode, StrEditWH_ID)
        ElseIf frmBOMWareSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then   '聯豐編號
            AddRow(StrEditCode, StrEditWH_ID)
        End If
    End Sub

    Sub AddRow(ByVal StrEditCode As String, ByVal StrEditWH_ID As String)
        Dim pii As List(Of ProductInventoryInfo)
        Dim i, n As Integer
        Dim arr(n), arrA(n), arrB(n) As String
        arr = Split(StrEditCode, ",")
        arrA = Split(StrEditWH_ID, ",")

        n = Len(Replace(StrEditCode, ",", "," & "*")) - Len(StrEditCode)
        For i = 0 To n
            Dim j As Integer
            For j = 0 To ds.Tables("KaiLiao").Rows.Count - 1
                If arr(i) = ds.Tables("KaiLiao").Rows(j)("M_Code") And arrA(i) = ds.Tables("KaiLiao").Rows(j)("WH_ID") Then
                    MsgBox("一張單不允許有重復物料編碼和倉庫....")
                    Exit Sub
                End If
            Next

            If arr(i) = "" Then
                Exit Sub
            End If

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(arr(i))
            Dim row As DataRow
            row = ds.Tables("KaiLiao").NewRow
            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge
            row("WH_ID") = arrA(i)

            If CBType.Text = "裝配出貨" Then
                pii = pic.ProductInventory_GetList(Nothing, arr(i), strWHID, Nothing)
                If pii.Count > 0 Then
                    row("C_Weight") = pii(0).PI_Qty
                Else
                    row("C_Weight") = 0
                End If
            Else
                Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi = wc.WareInventory_GetList3(arr(i), arrA(i), "True")
                If wi.Count > 0 Then
                    row("C_Weight") = wi(0).WI_Qty
                    row("WI_Qty") = wi(0).WI_Qty
                Else
                    row("C_Weight") = 0
                    row("WI_Qty") = 0
                End If
            End If
            row("C_Qty") = 0
            row("C_Cishu") = 1
            row("C_Type") = "正批"
            ds.Tables("KaiLiao").Rows.Add(row)
            GridView1.MoveLast()
        Next
    End Sub

    Private Sub cmsCodeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "IndexNo")

        If DelTemp = "IndexNo" Then
        Else
            '在刪除表中增加被刪除的記錄
            Dim row As DataRow = ds.Tables("DelKaiLiao").NewRow
            row("IndexNo") = DelTemp
            ds.Tables("DelKaiLiao").Rows.Add(row)
        End If
        ds.Tables("KaiLiao").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub
#End Region

#Region "控件數據列表"
    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        On Error Resume Next
        Dim pcc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()
        pci = pcc.ProcessMain_GetList1(Nothing, PM_M_Code.EditValue, CBType.EditValue, Nothing)
        If pci.Count = 0 Then Exit Sub
        Dim i As Integer
        For i = 0 To pci.Count - 1
            Dim row As DataRow
            row = ds.Tables("ProductType").NewRow
            row("PM_Type") = pci(i).Type3ID

            ds.Tables("ProductType").Rows.Add(row)
        Next

        Dim j As Integer
        For j = 0 To pci.Count - 1
            If PM_M_Code.EditValue = pci(j).Type3ID Then  '2013-8-9 若存在成品編號,先默認成品
                gluType.EditValue = pci(j).Type3ID
            End If
        Next
        '------------------------------------------
        If PM_M_Code.EditValue <> String.Empty Then
            Dim oscon As New OrdersSubController
            gluOS_BatchID.Properties.DisplayMember = "OS_BatchID"
            gluOS_BatchID.Properties.ValueMember = "OS_BatchID"

            If Label8.Text = "PreView" Or Label8.Text = "Check" Then '2013-11-14
                gluOS_BatchID.Properties.DataSource = oscon.OrdersSub_GetList(Nothing, Nothing, PM_M_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Else
                gluOS_BatchID.Properties.DataSource = oscon.OrdersSub_GetList4(Nothing, Nothing, PM_M_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            End If
        End If

    End Sub

    Private Sub CBType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBType.SelectedIndexChanged
        If PM_M_Code.Text <> "" Then
            PM_M_Code_EditValueChanged(Nothing, Nothing)
        End If
    End Sub

#End Region

#Region "數據檢查"
    Function CheckData() As Boolean
        CheckData = True
        Dim wi As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
        Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
        Dim i As Integer
        For i = 0 To ds.Tables("KaiLiao").Rows.Count - 1
            Dim strQty As Double

            wi = wc.WareInventory_GetList3(ds.Tables("KaiLiao").Rows(i)("M_Code"), strWHID, "True")
            If wi.Count = 0 Then
                CheckData = False
                strQty = 0
                MsgBox("當前倉庫不存在此物料！")
                Exit Function
            Else
                strQty = wi(0).WI_Qty
            End If
        Next
    End Function
#End Region
   
#Region "自動流水號-開料單號-單號"
    Public Function GetNO() As String
        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionKaiLiao_GetNO(strA)

        If pi Is Nothing Then
            GetNO = "C" & strA & "001"
        Else
            GetNO = "C" + strA + Mid((CInt(Mid(pi.C_NO, 6)) + 1001), 2)
        End If
    End Function
    Public Function GetKNO() As String
        Dim pi As New ProductionKaiLiaoInfo
        Dim pc As New ProductionKaiLiaoControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionKaiLiaoA_GetNO(strA)

        If pi Is Nothing Then
            GetKNO = "K" & strA & "001"
        Else
            GetKNO = "K" + strA + Mid((CInt(Mid(pi.C_Number, 6)) + 1001), 2)
        End If
    End Function
#End Region

    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged

    End Sub
End Class