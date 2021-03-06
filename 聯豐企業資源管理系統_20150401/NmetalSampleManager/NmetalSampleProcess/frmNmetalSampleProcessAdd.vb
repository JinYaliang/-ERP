Imports System
Imports System.IO
Imports System.Windows.Forms
Imports LFERP.Library.Product
Imports LFERP.Library.ProductionField
Imports LFERP.SystemManager
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcessMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory

Public Class frmNmetalSampleProcessAdd

#Region "属性"
    Dim ds As New DataSet
    Dim sc As New NmetalSampleOrdersMainControler
    Dim ptcon As New ProcessTypeControl
    Dim swicon As New NmetalSampleWareInventoryControler
    Private fs As IO.FileStream
    Dim TempCode As String
    Dim TempType3ID As String
    Dim boolPermission As Boolean = False
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

    Private Sub ButtonEdit1_ButtonPressed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEdit1.ButtonPressed
        On Error Resume Next

        Dim pc As New ProductController
        Dim piL As List(Of ProductInfo)
        piL = pc.Product_GetList(ButtonEdit1.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If piL Is Nothing Then Exit Sub
        If ButtonEdit1.Text <> "" Then
            TextEdit1.Text = piL(0).PM_CusterID
            TextEdit3.Text = piL(0).PM_JiYu
        End If

        Dim pbc As New ProductBomController
        Dim pbiL As List(Of ProductBomInfo)
        pbiL = pbc.ProductBom_GetList(ButtonEdit1.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pbiL Is Nothing Then
            MsgBox("产品配件为空！", , "提示")
        End If
        SubRowAdd(pbiL)
    End Sub

    Private Sub SubRowAdd(ByVal pbil As List(Of ProductBomInfo))
        Dim pc As New ProductController
        Dim piL As List(Of ProductInfo)
        piL = pc.Product_GetList(GridLookUpEdit1.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        ds.Tables("ProductSub").Clear()
        Dim row As DataRow
        row = ds.Tables("ProductSub").NewRow

        row("M_Name") = GridLookUpEdit1.Text.ToString
        row("M_PID") = "0~"
        If pbil.Count > 0 Then
            row("M_Code") = piL(0).PM_M_Code
            row("M_KEY") = pbil(0).PM_PID
        Else
            row("M_Code") = GridLookUpEdit1.Text.ToString
            row("M_KEY") = ""
        End If
        ds.Tables("ProductSub").Rows.Add(row)
        For i As Integer = 0 To pbil.Count - 1
            row = ds.Tables("ProductSub").NewRow
            row("M_Name") = pbil(i).M_Name
            row("M_Code") = pbil(i).M_Code
            row("M_PID") = pbil(i).PM_PID
            row("M_KEY") = pbil(i).PM_Key
            ds.Tables("ProductSub").Rows.Add(row)
        Next
        Me.TreeList1.ExpandAll()
    End Sub

    Private Sub pupPDProductAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pupPDProductAdd.Click
        Dim row As DataRow
        row = ds.Tables("ProcessSub").NewRow
        row("PS_Check") = False
        If ds.Tables("ProcessSub").Rows.Count = 0 Then
            row("PS_Num") = 1
            row("PS_Name") = "开料"
            row("PS_OutPut") = 0
            row("PS_Type") = "大货"
            row("PS_Weight") = 0
            row("PS_Enable") = True
            row("PS_BarCodeBit") = False
            row("UseCount") = 0
            row("SWI_Qty") = 0

        Else
            row("PS_Num") = ds.Tables("ProcessSub").Rows(ds.Tables("ProcessSub").Rows.Count - 1)("PS_Num") + 1
            row("PS_OutPut") = 0
            row("PS_Type") = "大货"
            row("PS_Weight") = 0
            row("PS_Enable") = True
            row("PS_BarCodeBit") = False
            row("UseCount") = 0
            row("SWI_Qty") = 0
        End If
        ds.Tables("ProcessSub").Rows.Add(row)
    End Sub

    Private Sub frmPDProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        'Label24.Text = tempValue
        txtOMID.Text = tempValue2
        txtOMID.Enabled = False

        'tempValue = ""
        tempValue2 = ""
        CreateTable()

        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessMainInfo
        Dim ptc As New ProductController

        ImageInput.Properties.DataSource = ptc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        ImageInput.Properties.DisplayMember = "PM_M_Code"
        ImageInput.Properties.ValueMember = "PM_M_Code"

        GridLookUpEdit1.Properties.DisplayMember = "PM_M_Code"
        GridLookUpEdit1.Properties.ValueMember = "PM_M_Code"
        GridLookUpEdit1.Properties.DataSource = sc.NmetalSampleProcess_GetPRONO(Nothing)

        Me.RepositoryItemLookUpEdit1.DataSource = pc.NmetalProDepartMent_GetList(Nothing, tempValue3)
        Me.RepositoryItemLookUpEdit1.DisplayMember = "D_Dep"
        Me.RepositoryItemLookUpEdit1.ValueMember = "D_ID"

        If PalSetProcess.Visible = True Then
            gluProcessType.Properties.DataSource = ptcon.ProcessTypeA_GetList(Nothing, Nothing)
            gluProcessType.Properties.DisplayMember = "D_Type"
            gluProcessType.Properties.ValueMember = "D_Type"
        End If

        Select Case EditItem
            Case EditEnumType.ADD
                Me.Label1.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                GetEnable(True, False)
                ComboBoxEdit1.Enabled = False
                Me.popEnable.Visible = False
                ComboBoxEdit1.EditValue = tempValue3
                tempValue3 = ""
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                PS_OtherWeight.OptionsColumn.ReadOnly = False
            Case EditEnumType.EDIT
                Me.Label1.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                GetEnable(True, False)
                ButtonEdit1.Enabled = False
                GridLookUpEdit1.Enabled = False
                ComboBoxEdit1.Enabled = False
                PopupContainerEdit1.Enabled = False
                TextEdit1.Enabled = False
                TextEdit2.Enabled = False
                TextEdit3.Enabled = False
                LoadData(txtOMID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                PS_OtherWeight.OptionsColumn.ReadOnly = False
            Case EditEnumType.COPY
                Me.Label1.Text = Me.Text + EditEnumValue(EditEnumType.COPY)
                GetEnable(True, False)
                ComboBoxEdit1.Enabled = False
                Me.popEnable.Visible = False
                ComboBoxEdit1.EditValue = tempValue3
                tempValue3 = ""
                ComboBoxEdit1.Text = "生產加工"
                LoadSubData(pc.NmetalSampleProcessSub_GetList(txtOMID.Text, Nothing, Nothing, Nothing, Nothing, Nothing))

            Case EditEnumType.VIEW
                Me.Label1.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                GetEnable(False, False)
                TextEdit4.Enabled = False
                TextEdit5.Enabled = False
                LoadData(txtOMID.Text)
                txtOMID.Enabled = False
                cmdSave.Visible = False
                Me.GridControl1.ContextMenuStrip = Nothing
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage3.PageVisible = True
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            Case EditEnumType.CHECK

                Me.Label1.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                GetEnable(False, True)
                LoadData(txtOMID.Text)
                txtOMID.Enabled = False
                XtraTabPage3.PageVisible = True
                Me.GridControl1.ContextMenuStrip = Nothing
                XtraTabControl1.SelectedTabPage = XtraTabPage3
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            Case EditEnumType.ELSEONE  '2014-02-15 修改自動產生條碼

                Me.Label1.Text = Me.Text + "-自動編碼"
                GetEnable(True, False)
                ButtonEdit1.Enabled = False
                GridLookUpEdit1.Enabled = False
                ComboBoxEdit1.Enabled = False
                PopupContainerEdit1.Enabled = False
                TextEdit1.Enabled = False
                TextEdit2.Enabled = False
                TextEdit3.Enabled = False
                PS_NO.OptionsColumn.ReadOnly = True
                GridColumn2.OptionsColumn.ReadOnly = True
                GridColumn3.OptionsColumn.ReadOnly = True
                GridColumn4.OptionsColumn.ReadOnly = True
                PS_Type.OptionsColumn.ReadOnly = True
                GridColumn5.OptionsColumn.ReadOnly = True
                GridColumn6.OptionsColumn.ReadOnly = True
                GridColumn9.OptionsColumn.ReadOnly = True
                GridColumn10.OptionsColumn.ReadOnly = True
                GridColumn12.OptionsColumn.ReadOnly = True
                PS_BarCodeBit.OptionsColumn.ReadOnly = False
                Me.GridControl1.ContextMenuStrip = Nothing
                LoadData(txtOMID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case EditEnumType.ELSETWO  '張偉    2014-07-07

                Me.Label1.Text = Me.Text + "-新增重量"
                GetEnable(True, False)
                ButtonEdit1.Enabled = False
                GridLookUpEdit1.Enabled = False
                ComboBoxEdit1.Enabled = False
                PopupContainerEdit1.Enabled = False
                TextEdit1.Enabled = False
                TextEdit2.Enabled = False
                TextEdit3.Enabled = False
                PS_NO.OptionsColumn.ReadOnly = True
                GridColumn2.OptionsColumn.ReadOnly = True
                GridColumn3.OptionsColumn.ReadOnly = True
                GridColumn4.OptionsColumn.ReadOnly = True
                PS_Type.OptionsColumn.ReadOnly = True
                GridColumn5.OptionsColumn.ReadOnly = True
                GridColumn6.OptionsColumn.ReadOnly = True
                GridColumn9.OptionsColumn.ReadOnly = True
                GridColumn10.OptionsColumn.ReadOnly = True
                GridColumn12.OptionsColumn.ReadOnly = True
                PS_BarCodeBit.OptionsColumn.ReadOnly = False
                PS_OtherWeight.OptionsColumn.ReadOnly = False
                Me.GridControl1.ContextMenuStrip = Nothing
                LoadData(txtOMID.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case Else
                Exit Select
        End Select

        Me.Text = Label1.Text
    End Sub
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860212")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAddFile.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860213")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                boolPermission = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860215") '顯示模板工藝
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then
                PalSetProcess.Visible = True
            End If
        End If
    End Sub

    Private Sub GetEnable(ByVal a As Boolean, ByVal b As Boolean)
        ButtonEdit1.Enabled = a
        GridLookUpEdit1.Enabled = a
        TextEdit1.Enabled = a
        TextEdit3.Enabled = a
        PopupContainerEdit1.Enabled = a
        TextEdit2.Enabled = a
        ComboBoxEdit1.Enabled = a
        MemoEdit1.Enabled = a
        MemoEdit2.Enabled = a
        SimpleButton1.Enabled = a

        CheckEdit1.Enabled = b
        CheckRemark.Enabled = b
    End Sub
    Private Function LoadData(ByVal Pro_NO As String) As Boolean
        Dim pc As New NmetalSampleProcessControl
        Dim piL As List(Of NmetalSampleProcessInfo)

        piL = pc.NmetalSampleProcessMain_GetList1(Pro_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        ButtonEdit1.Text = piL(0).PM_M_Code
        GridLookUpEdit1.EditValue = piL(0).PM_M_Code
        TempCode = piL(0).M_Code

        PopupContainerEdit1.EditValue = piL(0).PM_Type
        TextEdit1.Text = piL(0).CustomerNO
        TextEdit2.Text = piL(0).Type3ID
        TextEdit3.Text = piL(0).PM_JiYu
        TextEdit4.Text = piL(0).Pro_Weight
        TextEdit5.Text = piL(0).Pro_Rate
        ComboBoxEdit1.Text = piL(0).Pro_Type
        MemoEdit1.Text = piL(0).Pro_Remark
        MemoEdit2.Text = piL(0).Pro_Describe

        pPhoto.Image = ByteToImage(piL(0).Pro_Photo)
        CheckEdit1.Checked = piL(0).Pro_Check

        If piL(0).Pro_CheckDate = Nothing Then
            CheckDate.Text = Format(Now, "yyyy/MM/dd")
        Else
            CheckDate.Text = piL(0).Pro_CheckDate
        End If

        If piL(0).Pro_CheckAction = Nothing Then
            CheckAction.Text = InUserID
        Else
            CheckAction.Text = piL(0).CheckActionName
        End If
        CheckRemark.Text = piL(0).Pro_CheckRemark
        LoadSubData(pc.NmetalSampleProcessSub_GetList(Pro_NO, Nothing, Nothing, Nothing, Nothing, Nothing))
    End Function
    Private Function LoadSubData(ByVal PsL As List(Of NmetalSampleProcessInfo)) As Boolean
        If PsL Is Nothing Then Exit Function
        Dim Row As DataRow
        Dim pc As New NmetalSampleProcessControl
        Me.RepositoryItemLookUpEdit1.DataSource = pc.NmetalProDepartMent_GetList(Nothing, ComboBoxEdit1.Text)
        Me.RepositoryItemLookUpEdit1.DisplayMember = "D_Dep"
        Me.RepositoryItemLookUpEdit1.ValueMember = "D_ID"

        For i As Integer = 0 To PsL.Count - 1
            Row = ds.Tables("ProcessSub").NewRow
            Row("PS_NO") = PsL(i).PS_NO
            Row("PS_Num") = PsL(i).PS_Num
            Row("D_Name") = PsL(i).D_ID
            Row("PS_Type") = PsL(i).PS_Type
            Row("PS_Name") = PsL(i).PS_Name
            Row("PS_Remark") = PsL(i).PS_Remark
            Row("Pro_NO") = PsL(i).Pro_NO
            Row("PS_Check") = PsL(i).PS_Check

            If EditItem <> EditEnumType.COPY Then
                Row("PS_File") = PsL(i).PS_File
            End If

            Row("PS_Enable") = PsL(i).PS_Enable
            Row("PS_OutPut") = PsL(i).PS_OutPut
            Row("PS_Weight") = PsL(i).PS_Weight
            Row("PS_BarCodeBit") = PsL(i).PS_BarCodeBit

            Row("PS_OtherWeight") = PsL(i).PS_OtherWeight

            If EditItem = EditEnumType.COPY Or EditItem = EditEnumType.ADD Then
                Row("UseCount") = 0
                Row("SWI_Qty") = 0
            Else
                Row("UseCount") = PsL(i).UseCount
                Row("SWI_Qty") = PsL(i).SWI_Qty
            End If

            Row("PS_IsAutoWeight") = PsL(i).PS_IsAutoWeight                    '張偉    2014-09-17      是否自動重量
            Row("PS_IsCompleteProcess") = PsL(i).PS_IsCompleteProcess          '張偉    2014-09-17      是否完工工序 

            ds.Tables("ProcessSub").Rows.Add(Row)
        Next
    End Function
    Private Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("ProcessSub") '子表
            .Columns.Add("Pro_M_NO", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Num", GetType(String))
            .Columns.Add("D_Name", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("PS_Type", GetType(String))
            .Columns.Add("PS_Remark", GetType(String))
            .Columns.Add("Pro_NO", GetType(String))
            .Columns.Add("PS_Check", GetType(Boolean))
            .Columns.Add("PS_Enable", GetType(Boolean))
            .Columns.Add("PS_OutPut", GetType(String))
            .Columns.Add("PS_Weight", GetType(Double))
            .Columns.Add("PS_File", GetType(String))
            .Columns.Add("PS_BarCodeBit", GetType(Boolean))
            .Columns.Add("UseCount", GetType(Integer))
            .Columns.Add("SWI_Qty", GetType(Integer))
            .Columns.Add("PS_OtherWeight", GetType(Decimal))

            .Columns.Add("PS_IsAutoWeight", GetType(Boolean))                   '2014-09-17         張偉           是否自動重量
            .Columns.Add("PS_IsCompleteProcess", GetType(Boolean))              '2014-09-17         張偉           是否完工工序
        End With
        '创建刪除数据表
        With ds.Tables.Add("DelData")
            .Columns.Add("PS_NO", GetType(String))
        End With
        With ds.Tables.Add("ProductSub") '子配件表
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_PID", GetType(String))
            .Columns.Add("M_KEY", GetType(String))
        End With

        '綁定表格
        GridControl1.DataSource = ds.Tables("ProcessSub")
        Me.TreeList1.DataSource = ds.Tables("ProductSub")
    End Sub

    Private Sub pupPDProductDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pupPDProductDel.Click
        If ds.Tables("ProcessSub").Rows.Count = 0 Then Exit Sub
        Dim pfc As New ProductionFieldControl
        Dim str As String

        If IsDBNull(ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_NO")) Then
            str = Nothing
        Else
            str = ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_NO")
            Dim spcon As New NmetalSamplePaceControler()
            If spcon.NmetalSamplePace_Getlist3(str).Count > 0 Then
                MsgBox("此工艺已经进入物料收發，不能刪除！", , "提示")
                Exit Sub
            End If

            Dim row As DataRow = ds.Tables("DelData").NewRow
            row("PS_NO") = ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_NO")
            ds.Tables("DelData").Rows.Add(row)
        End If
        For i As Integer = CInt(GridView1.GetFocusedRowCellValue("PS_Num")) To ds.Tables("ProcessSub").Rows.Count - 1 Step 1
            ds.Tables("ProcessSub").Rows(i)("PS_NUM") = i
        Next
        ds.Tables("ProcessSub").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub pupPDProductInt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pupPDProductInt.Click
        On Error Resume Next
        Dim row As DataRow = ds.Tables("ProcessSub").NewRow
        If ds.Tables("ProcessSub").Rows.Count = 0 Then
            row("PS_Num") = 1
            row("PS_Name") = "开料"
            row("PS_Check") = False
            row("PS_Type") = "大货"
            row("PS_OutPut") = 0
            row("PS_Weight") = 0
            row("PS_Enable") = True
            row("PS_BarCodeBit") = False
            row("UseCount") = 0
            row("SWI_Qty") = 0
        Else
            row("PS_Num") = ds.Tables("ProcessSub").Rows(ds.Tables("ProcessSub").Rows.Count - 1)("PS_Num") + 1
            row("PS_Check") = False
            row("PS_OutPut") = 0
            row("PS_Type") = "大货"
            row("PS_Weight") = 0
            row("PS_Enable") = True
            row("PS_BarCodeBit") = False
            row("UseCount") = 0
            row("SWI_Qty") = 0
        End If
        ds.Tables("ProcessSub").Rows.Add(row)

        For i As Integer = ds.Tables("ProcessSub").Rows.Count - 1 To CInt(GridView1.GetFocusedRowCellValue("PS_Num")) Step -1
            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_NO")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_NO") = Nothing
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_NO") = ds.Tables("ProcessSub").Rows(i - 1)("PS_NO").ToString.Trim
            End If

            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("D_Name")) Then
                ds.Tables("ProcessSub").Rows(i)("D_Name") = Nothing
            Else
                ds.Tables("ProcessSub").Rows(i)("D_Name") = ds.Tables("ProcessSub").Rows(i - 1)("D_Name").ToString.Trim
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_Name")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_Name") = Nothing
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_Name") = ds.Tables("ProcessSub").Rows(i - 1)("PS_Name").ToString.Trim
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_Remark")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_Remark") = Nothing
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_Remark") = ds.Tables("ProcessSub").Rows(i - 1)("PS_Remark")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_Weight")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_Weight") = 0
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_Weight") = ds.Tables("ProcessSub").Rows(i - 1)("PS_Weight")
            End If

            If ds.Tables("ProcessSub").Rows(i - 1)("PS_OutPut") = 0 Then
                ds.Tables("ProcessSub").Rows(i)("PS_OutPut") = 0
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_OutPut") = ds.Tables("ProcessSub").Rows(i - 1)("PS_OutPut")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_Enable")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_Enable") = False
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_Enable") = ds.Tables("ProcessSub").Rows(i - 1)("PS_Enable")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_Check")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_Check") = False
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_Check") = ds.Tables("ProcessSub").Rows(i - 1)("PS_Check")
            End If

            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_Type")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_Type") = Nothing
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_Type") = ds.Tables("ProcessSub").Rows(i - 1)("PS_Type")
            End If

            If IsDBNull(ds.Tables("ProcessSub").Rows(i - 1)("PS_BarCodeBit")) Then
                ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit") = False
            Else
                ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit") = ds.Tables("ProcessSub").Rows(i - 1)("PS_BarCodeBit")
            End If

            If ds.Tables("ProcessSub").Rows(i - 1)("UseCount") = 0 Then
                ds.Tables("ProcessSub").Rows(i)("UseCount") = 0
            Else
                ds.Tables("ProcessSub").Rows(i)("UseCount") = ds.Tables("ProcessSub").Rows(i - 1)("UseCount")
            End If
            If ds.Tables("ProcessSub").Rows(i - 1)("UseCount") = 0 Then
                ds.Tables("ProcessSub").Rows(i)("SWI_Qty") = 0
            Else
                ds.Tables("ProcessSub").Rows(i)("SWI_Qty") = ds.Tables("ProcessSub").Rows(i - 1)("SWI_Qty")
            End If
        Next

        Dim j As Integer = CInt(GridView1.GetFocusedRowCellValue("PS_Num")) - 1
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_NO")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_NO") = Nothing
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("D_Name")) = False Then
            ds.Tables("ProcessSub").Rows(j)("D_Name") = Nothing
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_Name")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_Name") = Nothing
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_Remark")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_Remark") = Nothing
        End If

        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_OutPut")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_OutPut") = 0
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_Weight")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_Weight") = 0
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_Enable")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_Enable") = True
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_Check")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_Check") = False
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_Type")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_Type") = Nothing
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("PS_BarCodeBit")) = False Then
            ds.Tables("ProcessSub").Rows(j)("PS_BarCodeBit") = False
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("UseCount")) = False Then
            ds.Tables("ProcessSub").Rows(j)("UseCount") = 0
        End If
        If IsDBNull(ds.Tables("ProcessSub").Rows(j)("SWI_Qty")) = False Then
            ds.Tables("ProcessSub").Rows(j)("SWI_Qty") = 0
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case EditItem
            Case EditEnumType.ADD
                If CheckData() = False Then Exit Sub
                SaveNew()
            Case EditEnumType.EDIT
                If CheckData() = False Then Exit Sub
                SaveEdit()
            Case EditEnumType.CHECK
                UpdateCheck()
            Case EditEnumType.COPY
                If CheckData() = False Then Exit Sub
                SaveNew()
            Case EditEnumType.ELSEONE
                SavePS_BarCodeBit()
            Case EditEnumType.ELSETWO       '張偉  2014-07-07
                SaveOtherWeight()
            Case Else
                Exit Select
        End Select
    End Sub
    ''' <summary>
    ''' 保存新增重量
    ''' 張偉
    ''' 2014-07-07
    ''' </summary>
    ''' <remarks></remarks>
    Sub SaveOtherWeight()
        If ds.Tables("ProcessSub").Rows.Count <= 0 Then
            MsgBox("無數據存在!")
            Exit Sub
        End If

        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        pi.Pro_NO = txtOMID.Text

        For i As Integer = 0 To ds.Tables("ProcessSub").Rows.Count - 1
            pi.PS_NO = ds.Tables("ProcessSub").Rows(i)("PS_NO").ToString

            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")) = True Then
                pi.PS_OtherWeight = 0
            Else
                pi.PS_OtherWeight = ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")
            End If

            If pc.NmetalSampleProcessSub_UpdateOtherWeight(pi) = False Then
                MsgBox("部分保存失敗,請檢查!")
                Exit Sub
            End If
        Next
        MsgBox("保存成功！")
        Me.Close()
    End Sub

    Private Sub UpdateCheck()
        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        pi.Pro_NO = txtOMID.Text
        pi.Pro_Check = CheckEdit1.Checked
        pi.Pro_CheckAction = InUserID
        pi.Pro_CheckDate = Format(Now, "yyyy/MM/dd")
        pi.Pro_CheckRemark = CheckRemark.Text

        If pc.NmetalSampleProcessMain_UpdateCheck(pi) Then
            If CheckEdit1.Checked = True Then
                MsgBox("审核成功!", , "提示")
            Else
                MsgBox("取消审核成功!", , "提示")
            End If
        Else
            MsgBox("审核失敗!", , "提示")
        End If
        Me.Close()
    End Sub
    Private Sub SaveNew()
        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        Dim pmi As New NmetalSampleProcessInfo

        txtOMID.Text = GetNO()     '单号
        If Len(txtOMID.Text) = 0 Then
            MsgBox("不能生成单号，无法保存", , "提示")
            Exit Sub
        End If
        pi.Pro_NO = txtOMID.Text
        pi.PM_M_Code = GridLookUpEdit1.Text
        pi.M_Code = TempCode
        pi.PM_Type = PopupContainerEdit1.Text
        pi.Pro_Type = ComboBoxEdit1.Text
        pi.Pro_Weight = TextEdit4.Text
        pi.Pro_Rate = Val(TextEdit5.Text)
        pi.Type3ID = TextEdit2.Text
        pi.Pro_Remark = MemoEdit1.Text
        pi.Pro_Describe = MemoEdit2.Text
        pi.Pro_Action = InUserID
        pi.Pro_Photo = ImageToByte(pPhoto.Image)
        pi.Pro_AddDate = Format(Now, "yyyy/MM/dd")
        pi.Pro_EditDate = Format(Now, "yyyy/MM/dd")

        If pc.NmetalSampleProcessMain_Add(pi) = False Then
            MsgBox("主表保存失敗!", , "提示")
            Exit Sub
        End If

        If GridView1.RowCount = 0 Then Exit Sub
        '---------------------------------------------------------------------李超20131226新增
        If boolPermission = False Then
            If IsDBNull(ds.Tables("ProcessSub").Rows(0)("PS_Name")) Then
                MsgBox("工序第一项必須是'开料'", , "提示")
                Exit Sub
            End If
            If ds.Tables("ProcessSub").Rows(0)("PS_Name") = "开料" Then
            Else
                MsgBox("工序第一项必須是'开料'", , "提示")
                Exit Sub
            End If
        End If
        '---------------------------------------------------------------------

        For i As Integer = 0 To ds.Tables("ProcessSub").Rows.Count - 1

            pmi.M_Code = GetNum()    '工艺编码

            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                pmi.M_Name = Nothing
            Else
                pmi.M_Name = PopupContainerEdit1.Text & "_" & ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
            End If

            pmi.Type3ID = TempType3ID
            pmi.M_Unit = "PCS"
            '...............................
            pi.PS_NO = pmi.M_Code    '流水号
            pi.Pro_NO = txtOMID.Text

            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Num")) Then
                pi.PS_Num = Nothing
            Else
                pi.PS_Num = ds.Tables("ProcessSub").Rows(i)("PS_Num")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("D_Name")) Then
                pi.D_Name = Nothing
            Else
                pi.D_Name = ds.Tables("ProcessSub").Rows(i)("D_Name").ToString.Trim
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                pi.PS_Name = Nothing
            Else
                pi.PS_Name = ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
            End If

            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Type")) Then
                pi.PS_Type = Nothing
            Else
                pi.PS_Type = ds.Tables("ProcessSub").Rows(i)("PS_Type")
            End If


            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Remark")) Then
                pi.PS_Remark = Nothing
            Else
                pi.PS_Remark = ds.Tables("ProcessSub").Rows(i)("PS_Remark")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Check")) Then
                pi.PS_Check = False
            Else
                pi.PS_Check = ds.Tables("ProcessSub").Rows(i)("PS_Check")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Enable")) Then
                pi.PS_Enable = False
            Else
                pi.PS_Enable = ds.Tables("ProcessSub").Rows(i)("PS_Enable")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OutPut")) Then
                pi.PS_OutPut = 0
            Else
                pi.PS_OutPut = 0
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Weight")) Then
                pi.PS_Weight = 0
            Else
                pi.PS_Weight = ds.Tables("ProcessSub").Rows(i)("PS_Weight")
            End If
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")) Then
                pi.PS_BarCodeBit = False
            Else
                pi.PS_BarCodeBit = ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")
            End If

            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")) Then
                pi.PS_OtherWeight = 0
            Else
                pi.PS_OtherWeight = ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")
            End If
            '2014-09-17     張偉         是否自動重量
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_IsAutoWeight")) Then
                pi.PS_IsAutoWeight = False
            Else
                pi.PS_IsAutoWeight = ds.Tables("ProcessSub").Rows(i)("PS_IsAutoWeight")
            End If

            '2014-09-17     張偉        是否完工工序
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_IsCompleteProcess")) Then
                pi.PS_IsCompleteProcess = False
            Else
                pi.PS_IsCompleteProcess = ds.Tables("ProcessSub").Rows(i)("PS_IsCompleteProcess")
            End If

            If pc.MaterialCode2_Add(pmi) = False Then
                MsgBox("编码表保存失敗!", , "提示")
                Exit Sub
            End If

            If pc.NmetalSampleProcessSub_Add(pi) = False Then
                MsgBox("子表保存失敗!", , "提示")
                Exit Sub
            End If
        Next
        Me.Close()
    End Sub

    ''' <summary>
    ''' 2014-6-30 加工藝單號從51表示貴金屬
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNum() As String
        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        Dim str As String

        If ComboBoxEdit1.Text = "裝配出貨" Then
            str = "51001"
        ElseIf ComboBoxEdit1.Text = "生產加工" Then
            str = "51002"
        ElseIf ComboBoxEdit1.Text = "胚部加工" Then
            str = "51003"
        ElseIf ComboBoxEdit1.Text = "品質檢查" Then
            str = "51004"
        Else
            MsgBox("物料编码生成出錯!", , "提示")
            GetNum = Nothing
            Exit Function
        End If
        TempType3ID = str & "001001"
        pi = pc.NmetalSampleProcessSub_GetNum(str)
        If pi Is Nothing Then
            GetNum = TempType3ID & "000001"
        Else
            GetNum = TempType3ID & CStr(Microsoft.VisualBasic.Right(CInt(Microsoft.VisualBasic.Right(pi.M_Code, 6)) + 1000001, 6))
        End If
    End Function
    Private Function GetNO() As String
        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        Dim str As String
        str = CStr(Format(Now, "yyMMdd"))
        pi = pc.NmetalSampleProcessMain_GetNO(str)
        If pi Is Nothing Then
            GetNO = "PRO" & str & "00001"
        Else
            GetNO = "PRO" & str & Mid((CInt(Mid(pi.Pro_NO, 10)) + 100001), 2)
        End If

    End Function
    Private Function CheckData() As Boolean
        CheckData = False
        If GridLookUpEdit1.Text = "" Then
            MsgBox("产品编号不能为空", , "提示")
            GridLookUpEdit1.Focus()
            Exit Function
        End If

        If Val(TextEdit4.Text) < 0 Then
            MsgBox("重量輸入有誤", , "提示")
            TextEdit4.Focus()
            Exit Function
        End If

        If Val(TextEdit5.Text) <= 0 Then
            MsgBox("比例輸入有誤", , "提示")
            TextEdit5.Focus()
            Exit Function
        End If


        '2013-11-05-----------------------------------------------------------
        'If Label24.Text = "产品资料工艺流程单" And Edit = False Then
        If EditItem = EditEnumType.ADD Then
            Dim pcA As New NmetalSampleProcessControl
            Dim piLA As New List(Of NmetalSampleProcessInfo)
            piLA = pcA.NmetalSampleProcessMain_GetList1(Nothing, GridLookUpEdit1.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If piLA.Count > 0 Then
                MsgBox("此产品编号已存在工序流程信息！", 0, "提示")
                Exit Function
            End If
        End If

        If TextEdit2.Text = "" Then
            MsgBox("类型不能为空", , "提示")
            TextEdit2.Focus()
            Exit Function
        End If

        If ComboBoxEdit1.Text = "" Then
            MsgBox("工艺分类不能为空", 64, "提示")
            ComboBoxEdit1.Focus()
            Exit Function
        End If

        '李超修改20140113
        If GridLookUpEdit1.EditValue <> PopupContainerEdit1.EditValue Then
            MsgBox("產品編號與类別要相同", , "提示")
            PopupContainerEdit1.Focus()
            Exit Function
        End If

        ''TextEdit2
        If GridLookUpEdit1.EditValue <> TextEdit2.EditValue Then
            MsgBox("產品編號與类型要相同", , "提示")
            TextEdit2.Focus()
            Exit Function
        End If

        Dim pc As New NmetalSampleProcessControl
        If EditItem = EditEnumType.ADD Then
            Dim pi As New NmetalSampleProcessMainInfo
            If pc.NmetalSampleProcessMain_GetList(Nothing, GridLookUpEdit1.Text, ComboBoxEdit1.Text, TextEdit2.Text, Nothing, Nothing, Nothing).Count > 0 Then
                MsgBox("类型命名重复,请重新命名!", 64, "提示")
                TextEdit2.Focus()
                Exit Function
            End If
        End If

        If ds.Tables("ProcessSub").Rows.Count <= 0 Then
            MsgBox("無工序記錄保存!", , "提示")
            Exit Function
        End If

        For i As Integer = 0 To ds.Tables("ProcessSub").Rows.Count - 1
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("D_Name")) Then
                MsgBox("工序部門不能为空!", 64, "提示")
                GridView1.FocusedRowHandle = i
                Exit Function
            ElseIf Trim(ds.Tables("ProcessSub").Rows(i)("D_Name")) = "" Then
                MsgBox("工序部門不能为空!", 64, "提示")
                GridView1.FocusedRowHandle = i
                Exit Function
            End If

            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                MsgBox("工序名称不能为空!", 64, "提示")
                GridView1.FocusedRowHandle = i
                Exit Function
            ElseIf Trim(ds.Tables("ProcessSub").Rows(i)("PS_Name")) = "" Then
                MsgBox("工序名称不能为空!", 64, "提示")
                GridView1.FocusedRowHandle = i
                Exit Function
            ElseIf ds.Tables("ProcessSub").Rows(i)("PS_Weight") < 0 Then
                MsgBox("工序重量輸入有誤!", 64, "提示")
                GridView1.FocusedRowHandle = i
                Exit Function
            End If
        Next
        CheckData = True
    End Function
    ''程序更改原因
    ''当工序编码存在物料收發記錄表中時的处理（
    Private Sub SaveAdd2()
        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo

        Dim pc1 As New NmetalSampleProcessControl
        Dim pi1 As New NmetalSampleProcessInfo

        Dim pc2 As New NmetalSampleProcessControl
        Dim pi2 As New NmetalSampleProcessInfo

        Dim pmi As New NmetalSampleProcessInfo

        Dim Sign_OK As String ''是否已进入有工艺已经进入物料收發

        If ds.Tables("DelData").Rows.Count > 0 Then '更新刪除子表記錄
            For i As Integer = 0 To ds.Tables("DelData").Rows.Count - 1
                pc.NmetalSampleProcessSub_Delete(Nothing, CStr(ds.Tables("DelData").Rows(i)("PS_NO")))
            Next
        End If

        If ds.Tables("DelData").Rows.Count > 0 Then '更新刪除物料编码表記錄
            For i As Integer = 0 To ds.Tables("DelData").Rows.Count - 1
                pc.MaterialCode2_Del(CStr(ds.Tables("DelData").Rows(i)("PS_NO")))
            Next
        End If

        For i As Integer = 0 To ds.Tables("ProcessSub").Rows.Count - 1
            Dim str As String
            Dim pfc As New ProductionFieldControl
            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_NO")) Then
                str = Nothing
            Else
                str = ds.Tables("ProcessSub").Rows(i)("PS_NO")
            End If

            Sign_OK = ""

            If pfc.ProductionField_GetList(Nothing, str, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Count > 0 And Not IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_NO")) Then
                Sign_OK = "B"
            Else
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_NO")) Then
                    Sign_OK = "A"
                Else
                    Sign_OK = "C"
                End If
            End If


            If Sign_OK = "A" Then
                pmi.M_Code = GetNum()    '工艺编码

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pmi.M_Name = Nothing
                Else
                    pmi.M_Name = PopupContainerEdit1.Text & "_" & ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If
                pmi.Type3ID = TempType3ID
                '...............................
                pi.PS_NO = pmi.M_Code    '流水号
                pi.Pro_NO = txtOMID.Text

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Num")) Then
                    pi.PS_Num = Nothing
                Else
                    pi.PS_Num = ds.Tables("ProcessSub").Rows(i)("PS_Num")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("D_Name")) Then
                    pi.D_Name = Nothing
                Else
                    pi.D_Name = ds.Tables("ProcessSub").Rows(i)("D_Name").ToString.Trim
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pi.PS_Name = Nothing
                Else
                    pi.PS_Name = ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Type")) Then
                    pi.PS_Type = Nothing
                Else
                    pi.PS_Type = ds.Tables("ProcessSub").Rows(i)("PS_Type")
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Remark")) Then
                    pi.PS_Remark = Nothing
                Else
                    pi.PS_Remark = ds.Tables("ProcessSub").Rows(i)("PS_Remark")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Check")) Then
                    pi.PS_Check = False
                Else
                    pi.PS_Check = ds.Tables("ProcessSub").Rows(i)("PS_Check")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Enable")) Then
                    pi.PS_Enable = False
                Else
                    pi.PS_Enable = ds.Tables("ProcessSub").Rows(i)("PS_Enable")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OutPut")) Then
                    pi.PS_OutPut = 0
                Else
                    pi.PS_OutPut = ds.Tables("ProcessSub").Rows(i)("PS_OutPut")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Weight")) Then
                    pi.PS_Weight = 0
                Else
                    pi.PS_Weight = ds.Tables("ProcessSub").Rows(i)("PS_Weight")
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")) Then
                    pi.PS_BarCodeBit = False
                Else
                    pi.PS_BarCodeBit = ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")
                End If
                If pc.MaterialCode2_Add(pmi) = False Then
                    MsgBox("编码表保存失敗!", , "提示")
                    ' Exit Sub
                End If
                If pc.NmetalSampleProcessSub_Add(pi) Then
                    MsgBox("此物料已经进入物料流轉,保存工艺新增部份成功!", , "提示")
                    'Exit Sub
                End If
            End If

            If Sign_OK = "B" Then   ''只更新一下
                pi1.PS_NO = ds.Tables("ProcessSub").Rows(i)("PS_NO")   '流水号
                pi1.Pro_NO = txtOMID.Text

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Num")) Then
                    pi1.PS_Num = Nothing
                Else
                    pi1.PS_Num = ds.Tables("ProcessSub").Rows(i)("PS_Num")
                End If

                If pc1.NmetalSampleProcessSub_Update3(pi1) = False Then
                    MsgBox("子表保存失敗!", , "提示")
                End If
            End If

            If Sign_OK = "C" Then
                pmi.M_Code = ds.Tables("ProcessSub").Rows(i)("PS_NO")    '工艺编码
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pmi.M_Name = Nothing
                Else
                    pmi.M_Name = PopupContainerEdit1.Text & "_" & ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If
                pmi.Type3ID = Nothing

                pi2.PS_NO = ds.Tables("ProcessSub").Rows(i)("PS_NO")
                pi2.Pro_NO = txtOMID.Text

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Num")) Then
                    pi2.PS_Num = Nothing
                Else
                    pi2.PS_Num = ds.Tables("ProcessSub").Rows(i)("PS_Num")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("D_Name")) Then
                    pi2.D_Name = Nothing
                Else
                    pi2.D_Name = ds.Tables("ProcessSub").Rows(i)("D_Name").ToString.Trim
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pi2.PS_Name = Nothing
                Else
                    pi2.PS_Name = ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Type")) Then
                    pi2.PS_Type = Nothing
                Else
                    pi2.PS_Type = ds.Tables("ProcessSub").Rows(i)("PS_Type")
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Remark")) Then
                    pi2.PS_Remark = Nothing
                Else
                    pi2.PS_Remark = ds.Tables("ProcessSub").Rows(i)("PS_Remark")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Check")) Then
                    pi2.PS_Check = False
                Else
                    pi2.PS_Check = ds.Tables("ProcessSub").Rows(i)("PS_Check")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Enable")) Then
                    pi2.PS_Enable = False
                Else
                    pi2.PS_Enable = ds.Tables("ProcessSub").Rows(i)("PS_Enable")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OutPut")) Then
                    pi2.PS_OutPut = 0
                Else
                    pi2.PS_OutPut = ds.Tables("ProcessSub").Rows(i)("PS_OutPut")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Weight")) Then
                    pi2.PS_Weight = 0
                Else
                    pi2.PS_Weight = ds.Tables("ProcessSub").Rows(i)("PS_Weight")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")) Then
                    pi2.PS_BarCodeBit = False
                Else
                    pi2.PS_BarCodeBit = ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")
                End If

                Dim StrPS_NO As String = ds.Tables("ProcessSub").Rows(i)("PS_NO")
                Dim spcon As New NmetalSamplePaceControler()
                If spcon.NmetalSamplePace_Getlist3(StrPS_NO).Count <= 0 Then
                    If pc2.MaterialCode2_Update(pmi) = False Then
                        MsgBox("编码表修改失敗!", , "提示")
                        Exit Sub
                    End If

                    If pc2.NmetalSampleProcessSub_Update(pi2) = False Then
                        MsgBox("子表保存失敗!", , "提示")
                        Exit Sub
                    End If
                End If
            End If

        Next

        Me.Close()
    End Sub
    Private Sub SaveEdit()
        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        Dim pmi As New NmetalSampleProcessInfo

        Dim pfc As New ProductionFieldControl

        pi.Pro_NO = txtOMID.Text
        pi.PM_M_Code = GridLookUpEdit1.Text
        pi.M_Code = TempCode
        pi.PM_Type = PopupContainerEdit1.Text
        pi.Pro_Weight = TextEdit4.Text
        pi.Pro_Rate = Val(TextEdit5.Text)
        pi.Pro_Type = ComboBoxEdit1.Text
        pi.Type3ID = TextEdit2.Text
        pi.Pro_Remark = MemoEdit1.Text
        pi.Pro_Describe = MemoEdit2.Text
        pi.Pro_Action = InUserID
        pi.Pro_Photo = ImageToByte(pPhoto.Image)
        pi.Pro_AddDate = Nothing
        pi.Pro_EditDate = Format(Now, "yyyy/MM/dd")

        If pc.NmetalSampleProcessMain_Update(pi) = False Then
            MsgBox("主表保存失敗!" , "提示")
            Exit Sub
        End If

        ' ''For i As Integer = 0 To ds.Tables("ProcessSub").Rows.Count - 1
        ' ''    If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_NO")) = False Then
        ' ''        If pfc.ProductionField_GetList(Nothing, ds.Tables("ProcessSub").Rows(i)("PS_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Count > 0 Then
        ' ''            Call SaveAdd2()
        ' ''            Exit Sub
        ' ''        End If
        ' ''    End If
        ' ''Next


        If GridView1.RowCount = 0 Then Exit Sub
        '----------------------------------------------------------------------------
        If boolPermission = False Then
            If ds.Tables("ProcessSub").Rows(0)("PS_Name") = "开料" Then
            Else
                MsgBox("工序第一项必須是'开料'", , "提示")
                Exit Sub
            End If
        End If
        '----------------------------------------------------------------------------
        If ds.Tables("DelData").Rows.Count > 0 Then '更新刪除子表記錄
            For i As Integer = 0 To ds.Tables("DelData").Rows.Count - 1
                Dim Str As String '2014-06-30
                Str = ds.Tables("DelData").Rows(i)("PS_NO").ToString
                Dim spcon As New NmetalSamplePaceControler()
                If spcon.NmetalSamplePace_Getlist3(Str).Count > 0 Then
                Else
                    '--------------------------------
                    pc.NmetalSampleProcessSub_Delete(Nothing, CStr(ds.Tables("DelData").Rows(i)("PS_NO")))
                    pc.MaterialCode2_Del(CStr(ds.Tables("DelData").Rows(i)("PS_NO")))
                End If
            Next
        End If


        For i As Integer = 0 To ds.Tables("ProcessSub").Rows.Count - 1

            If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_NO")) Then

                pmi.M_Code = GetNum()    '工艺编码

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pmi.M_Name = Nothing
                Else
                    pmi.M_Name = PopupContainerEdit1.Text & "_" & ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If
                pmi.Type3ID = TempType3ID
                '...............................
                pi.PS_NO = pmi.M_Code    '流水号
                pi.Pro_NO = txtOMID.Text

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Num")) Then
                    pi.PS_Num = Nothing
                Else
                    pi.PS_Num = ds.Tables("ProcessSub").Rows(i)("PS_Num")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("D_Name")) Then
                    pi.D_Name = Nothing
                Else
                    pi.D_Name = ds.Tables("ProcessSub").Rows(i)("D_Name").ToString.Trim
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pi.PS_Name = Nothing
                Else
                    pi.PS_Name = ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Type")) Then
                    pi.PS_Type = Nothing
                Else
                    pi.PS_Type = ds.Tables("ProcessSub").Rows(i)("PS_Type")
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Remark")) Then
                    pi.PS_Remark = Nothing
                Else
                    pi.PS_Remark = ds.Tables("ProcessSub").Rows(i)("PS_Remark")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Check")) Then
                    pi.PS_Check = False
                Else
                    pi.PS_Check = ds.Tables("ProcessSub").Rows(i)("PS_Check")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Enable")) Then
                    pi.PS_Enable = False
                Else
                    pi.PS_Enable = ds.Tables("ProcessSub").Rows(i)("PS_Enable")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OutPut")) Then
                    pi.PS_OutPut = 0
                Else
                    pi.PS_OutPut = ds.Tables("ProcessSub").Rows(i)("PS_OutPut")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Weight")) Then
                    pi.PS_Weight = 0
                Else
                    pi.PS_Weight = ds.Tables("ProcessSub").Rows(i)("PS_Weight")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")) Then
                    pi.PS_BarCodeBit = False
                Else
                    pi.PS_BarCodeBit = ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")) Then
                    pi.PS_OtherWeight = 0
                Else
                    pi.PS_OtherWeight = ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")
                End If

                '2014-09-17     張偉         是否自動重量
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_IsAutoWeight")) Then
                    pi.PS_IsAutoWeight = False
                Else
                    pi.PS_IsAutoWeight = ds.Tables("ProcessSub").Rows(i)("PS_IsAutoWeight")
                End If

                '2014-09-17     張偉        是否完工工序
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_IsCompleteProcess")) Then
                    pi.PS_IsCompleteProcess = False
                Else
                    pi.PS_IsCompleteProcess = ds.Tables("ProcessSub").Rows(i)("PS_IsCompleteProcess")
                End If


                If pc.MaterialCode2_Add(pmi) = False Then
                    MsgBox("编码表保存失敗!", , "提示")
                    Exit Sub
                End If
                If pc.NmetalSampleProcessSub_Add(pi) = False Then
                    MsgBox("子表保存失敗!", , "提示")
                    Exit Sub
                End If
            Else

                pmi.M_Code = ds.Tables("ProcessSub").Rows(i)("PS_NO")    '工艺编码
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pmi.M_Name = Nothing
                Else
                    pmi.M_Name = PopupContainerEdit1.Text & "_" & ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If
                pmi.Type3ID = Nothing

                pi.PS_NO = ds.Tables("ProcessSub").Rows(i)("PS_NO")
                pi.Pro_NO = txtOMID.Text

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Num")) Then
                    pi.PS_Num = Nothing
                Else
                    pi.PS_Num = ds.Tables("ProcessSub").Rows(i)("PS_Num")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("D_Name")) Then
                    pi.D_Name = Nothing
                Else
                    pi.D_Name = ds.Tables("ProcessSub").Rows(i)("D_Name").ToString.Trim
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Name")) Then
                    pi.PS_Name = Nothing
                Else
                    pi.PS_Name = ds.Tables("ProcessSub").Rows(i)("PS_Name").ToString.Trim
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Type")) Then
                    pi.PS_Type = Nothing
                Else
                    pi.PS_Type = ds.Tables("ProcessSub").Rows(i)("PS_Type")
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Remark")) Then
                    pi.PS_Remark = Nothing
                Else
                    pi.PS_Remark = ds.Tables("ProcessSub").Rows(i)("PS_Remark")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Check")) Then
                    pi.PS_Check = False
                Else
                    pi.PS_Check = ds.Tables("ProcessSub").Rows(i)("PS_Check")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Enable")) Then
                    pi.PS_Enable = False
                Else
                    pi.PS_Enable = ds.Tables("ProcessSub").Rows(i)("PS_Enable")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OutPut")) Then
                    pi.PS_OutPut = 0
                Else
                    pi.PS_OutPut = ds.Tables("ProcessSub").Rows(i)("PS_OutPut")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_Weight")) Then
                    pi.PS_Weight = 0
                Else
                    pi.PS_Weight = ds.Tables("ProcessSub").Rows(i)("PS_Weight")
                End If
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")) Then
                    pi.PS_BarCodeBit = False
                Else
                    pi.PS_BarCodeBit = ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit")
                End If

                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")) Then
                    pi.PS_OtherWeight = 0
                Else
                    pi.PS_OtherWeight = ds.Tables("ProcessSub").Rows(i)("PS_OtherWeight")
                End If

                '2014-09-17     張偉         是否自動重量
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_IsAutoWeight")) Then
                    pi.PS_IsAutoWeight = False
                Else
                    pi.PS_IsAutoWeight = ds.Tables("ProcessSub").Rows(i)("PS_IsAutoWeight")
                End If

                '2014-09-17     張偉        是否完工工序
                If IsDBNull(ds.Tables("ProcessSub").Rows(i)("PS_IsCompleteProcess")) Then
                    pi.PS_IsCompleteProcess = False
                Else
                    pi.PS_IsCompleteProcess = ds.Tables("ProcessSub").Rows(i)("PS_IsCompleteProcess")
                End If

                If pc.MaterialCode2_Update(pmi) = False Then
                    MsgBox("编码表修改失敗!", , "提示")
                    Exit Sub
                End If
                If pc.NmetalSampleProcessSub_Update(pi) = False Then
                    MsgBox("子表保存失敗!", , "提示")
                    Exit Sub
                End If

            End If
        Next

        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Dim ofd As New OpenFileDialog
        Dim i As Integer
        ofd.Filter = "图片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp"
        ofd.ShowDialog()
        If ofd.FileName.ToString = "" Then Exit Sub
        fs = New IO.FileStream(ofd.FileName.ToString, IO.FileMode.Open, IO.FileAccess.Read)
        pPhoto.Image = Image.FromFile(ofd.FileName.ToString)

        Select Case CInt(ofd.OpenFile.Length / 1024)
            Case Is < 80 : i = 0
            Case Is > 80 < 100 : i = 100
            Case Is > 100 < 150 : i = 85
            Case Is > 300 : i = 65
        End Select

    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        pPhoto.Image = Nothing
    End Sub

    Private Sub TreeList1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeList1.MouseDoubleClick
        PopupContainerEdit1.EditValue = TreeList1.FocusedNode.GetValue("M_Name")
        'PopupContainerEdit1.EditValue = TreeList1.FocusedNode.GetValue("M_Code")

        TempCode = TreeList1.FocusedNode.GetValue("M_Code")
        PopupContainerControl1.OwnerEdit.ClosePopup()
        If EditItem = EditEnumType.ADD Or EditItem = EditEnumType.COPY Then
            TextEdit2.Text = PopupContainerEdit1.Text
        End If
    End Sub
    Private Sub SimpleButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton4.Click
        pPhoto.Image = Nothing
    End Sub
    Private Sub ImageInput_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageInput.EditValueChanged
        On Error Resume Next
        Dim ptc As New ProductController
        Dim ptiL As New List(Of ProductInfo)
        ptiL = ptc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If ptiL.Count > 0 Then
            pPhoto.Image = ByteToImage(ptiL(0).PM_Image)
        Else
            MsgBox("产品编号图片为空！", , "提示")
        End If
    End Sub


    Private Sub popEnable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popEnable.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim strPS_NO = GridView1.GetFocusedRowCellValue("PS_NO")
        If strPS_NO <> String.Empty Then
            Dim swilist As List(Of NmetalSampleWareInventoryInfo)
            swilist = swicon.NmetalSampleWareInventoryPS_NO_GetList(strPS_NO)
            If swilist.Count > 0 Then
                If swilist(0).SWI_Qty Then
                    MsgBox("此工序已經有库存不能修正啟用!", , "提示")
                    Exit Sub
                End If
            End If
        End If

        If IsDBNull(ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_NO")) Then
            MsgBox("请保存后在启用!", , "提示")
            Exit Sub
        End If

        If ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_Enable") = True Then
            ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_Enable") = False
        Else
            ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_Enable") = True
        End If
        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        pi.PS_NO = ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_NO")
        pi.Pro_NO = txtOMID.Text
        pi.PS_Enable = ds.Tables("ProcessSub").Rows(GridView1.FocusedRowHandle)("PS_Enable")
        pc.NmetalSampleProcessSub_Update2(pi)
    End Sub

    Private Sub GridLookUpEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridLookUpEdit1.EditValueChanged
        TextEdit1.Text = sc.NmetalSampleProcess_GetCusterNo(GridLookUpEdit1.EditValue)
    End Sub

    Private Sub AddFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFile.Click
        '調用此产品资料的文件
        If GridView1.RowCount = 0 Then Exit Sub
        Dim open, update, down, Edit, del, detail As Boolean

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860207")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then update = True
            If pmwiL.Item(0).PMWS_Value = "否" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860208")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then down = True
            If pmwiL.Item(0).PMWS_Value = "否" Then down = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860209")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Edit = True
            If pmwiL.Item(0).PMWS_Value = "否" Then Edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860206")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then del = True
            If pmwiL.Item(0).PMWS_Value = "否" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860210")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "否" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "860211")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then open = True
            If pmwiL.Item(0).PMWS_Value = "否" Then open = False
        End If
        FileShow("8602", GridView1.GetFocusedRowCellValue("PS_NO").ToString, open, update, down, Edit, del, detail)
    End Sub

    Private Sub PopupContainerEdit1_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PopupContainerEdit1.QueryPopUp
        On Error Resume Next
        Dim pbc As New ProductBomController
        Dim pbiL As List(Of ProductBomInfo)
        pbiL = pbc.ProductBom_GetList(GridLookUpEdit1.Text.ToString, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pbiL Is Nothing Then
            MsgBox("产品配件为空！", , "提示")
        End If
        SubRowAdd(pbiL)
    End Sub
    ''' <summary>
    ''' 只修改只
    ''' </summary>
    ''' <remarks></remarks>
    Sub SavePS_BarCodeBit()
        If ds.Tables("ProcessSub").Rows.Count <= 0 Then
            MsgBox("無數據存在!")
            Exit Sub
        End If

        Dim pc As New NmetalSampleProcessControl
        Dim pi As New NmetalSampleProcessInfo
        pi.Pro_NO = txtOMID.Text

        For i As Integer = 0 To ds.Tables("ProcessSub").Rows.Count - 1
            pi.PS_NO = ds.Tables("ProcessSub").Rows(i)("PS_NO").ToString
            pi.PS_BarCodeBit = ds.Tables("ProcessSub").Rows(i)("PS_BarCodeBit").ToString
            If pc.NmetalSampleProcessSub_UpdatePS_BarCodeBit(pi) = False Then
                MsgBox("部分保存失敗,請檢查!")
                Exit Sub
            End If
        Next
        MsgBox("保存成功！")
        Me.Close()
    End Sub

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        If EditItem = EditEnumType.EDIT Then
            Dim strA As Integer = GridView1.GetFocusedRowCellValue("UseCount")
            If strA > 0 Then
                GridColumn3.OptionsColumn.ReadOnly = True
                GridColumn4.OptionsColumn.ReadOnly = True
                PS_OtherWeight.OptionsColumn.ReadOnly = True
            Else
                GridColumn3.OptionsColumn.ReadOnly = False
                GridColumn4.OptionsColumn.ReadOnly = False
                PS_OtherWeight.OptionsColumn.ReadOnly = False
            End If
        End If
    End Sub

#Region "工艺模板载入程序"
    Private Sub cmdProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcess.Click
        If gluProcessType.EditValue = String.Empty Then
            MsgBox("请选择工艺类型!")
            gluProcessType.Focus()
            Exit Sub
        End If
        Select Case EditItem
            Case EditEnumType.ADD
                Dim intRow = ds.Tables("ProcessSub").Rows.Count
                Dim i As Integer
                For i = 0 To intRow - 1
                    ds.Tables("ProcessSub").Rows.RemoveAt(0)
                Next

                Dim ptcon As New ProcessTypeControl
                Dim ptlist As New List(Of ProcessTypeInfo)
                ptlist = ptcon.ProcessType_GetList(Nothing, gluProcessType.Text, Nothing)
                For i = 0 To ptlist.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("ProcessSub").NewRow
                    row("PS_Check") = False
                    row("PS_Num") = i + 1
                    row("D_Name") = ptlist(i).D_ID
                    row("PS_Name") = ptlist(i).D_ProcessName
                    row("PS_OutPut") = 0
                    row("PS_Type") = "大貨"
                    row("PS_Weight") = 0
                    row("PS_Enable") = True
                    row("PS_Remark") = ptlist(i).Remarks
                    row("UseCount") = 0
                    row("SWI_Qty") = 0
                    ds.Tables("ProcessSub").Rows.Add(row)
                Next

            Case EditEnumType.EDIT
                Dim intRow = ds.Tables("ProcessSub").Rows.Count
                Dim i As Integer
                Dim ptcon As New ProcessTypeControl
                Dim ptlist As New List(Of ProcessTypeInfo)
                ptlist = ptcon.ProcessType_GetList(Nothing, gluProcessType.Text, Nothing)
                For i = 0 To ptlist.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("ProcessSub").NewRow
                    row("PS_Check") = False
                    row("PS_Num") = intRow + i + 1
                    row("D_Name") = ptlist(i).D_ID
                    row("PS_Name") = ptlist(i).D_ProcessName
                    row("PS_OutPut") = 0
                    row("PS_Type") = "大貨"
                    row("PS_Weight") = 0
                    row("PS_Enable") = True
                    row("PS_Remark") = ptlist(i).Remarks
                    row("UseCount") = 0
                    row("SWI_Qty") = 0
                    ds.Tables("ProcessSub").Rows.Add(row)
                Next
        End Select
    End Sub

    Private Sub ButtonSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSet.Click
        frmPDProductTypeSet.ShowDialog()
        frmPDProductTypeSet.Dispose()

        If PalSetProcess.Visible = True Then
            gluProcessType.Properties.DataSource = ptcon.ProcessTypeA_GetList(Nothing, Nothing)
            gluProcessType.Properties.DisplayMember = "D_Type"
            gluProcessType.Properties.ValueMember = "D_Type"
        End If

    End Sub

#End Region

    Private Sub PopupContainerEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopupContainerEdit1.EditValueChanged

    End Sub
End Class