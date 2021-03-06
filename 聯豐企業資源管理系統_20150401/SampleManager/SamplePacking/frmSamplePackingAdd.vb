Imports LFERP.Library.SampleManager.SamplePacking
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.ProductionController
Imports LFERP.Library.SampleManager.SampleCollection
Imports LFERP.Library.SampleManager.SampleTransaction
Public Class frmSamplePackingAdd

#Region "屬性"
    Private spcon As New SamplePackingController
    Private pfcon As New ProductionFieldControl
    Private Sccon As New SampleCollectionControler
    Private boolCheck As Boolean = False
    Private ds As New DataSet
    Private strPK_ID As String
    Private _EditItem As String
    Private _EditValue As String
    Private _EditAutoID As String
    Property EditAutoID() As String '属性
        Get
            Return _EditAutoID
        End Get
        Set(ByVal value As String)
            _EditAutoID = value
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
#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SamplePackingSub")
            .Columns.Add("PK_ID", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("PB_ID", GetType(String))
            .Columns.Add("Qty", GetType(Integer))
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("Remark", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("SamplePackingSub")

        'With ds.Tables.Add("SamplePackingSubB")
        '    .Columns.Add("PK_ID", GetType(String))
        '    .Columns.Add("Code_ID", GetType(String))
        '    .Columns.Add("PB_ID", GetType(String))
        '    .Columns.Add("Qty", GetType(Integer))
        '    .Columns.Add("AutoID", GetType(Decimal))
        '    .Columns.Add("Remark", GetType(String))
        'End With
        'Grid2.DataSource = ds.Tables("SamplePackingSubB")

        With ds.Tables.Add("DelSamplePackingSub")
            .Columns.Add("AutoID", GetType(String))
        End With

        'With ds.Tables.Add("DelSamplePackingSubB")
        '    .Columns.Add("AutoID", GetType(String))
        'End With
    End Sub
#End Region

#Region "載入窗體"
    Private Sub frmSamplePackingAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sc As New SampleOrdersMainControler
        gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
        gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
        gluPM_M_Code.Properties.DataSource = sc.sampleProcess_GetPRONO(Nothing)

        DatePackingDate.Text = Format(Now, "yyyy/MM/dd")
        txtAddUserID.Text = InUserID
        Me.cbPackingType.Text = "成品"
        CreateTable()
        Dim pflist As New List(Of ProductionFieldControlInfo)
        pflist = pfcon.ProductionFieldControl_GetList(Nothing, Nothing)
        Me.gluD_ID.Properties.DataSource =pflist
        Select Case EditItem
            Case "Add"
                Grid1.ContextMenuStrip = Me.cmsMenuStrip
            Case "Edit"
                LoadData(EditValue)
                Grid1.ContextMenuStrip = Me.cmsMenuStrip
            Case "Look"
                LoadData(EditValue)
                cmdSave.Visible = False
                Grid1.ContextMenuStrip = Nothing
            Case "Check"
                LoadData(EditValue)
                GroupBox1.Enabled = False
                GroupBox2.Enabled = False
                'GroupBox3.Enabled = False
                Grid1.ContextMenuStrip = Nothing
                XtraTabControl1.SelectedTabPage = XtraTabPage2
        End Select
    End Sub
#End Region

#Region "載入数据"
    Sub LoadData(ByVal strField As String) '返回数据
        Dim som As New List(Of SamplePackingInfo)
        som = spcon.SamplePacking_GetList(Nothing, strField, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If som.Count = 0 Then
            Exit Sub
        Else
            Me.txtPK_ID.Text = som(0).PK_ID
            Me.txt_remark.Text = som(0).Remark
            Me.txtAddUserID.Text = som(0).AddUserName
            Me.txtCode_ID.Text = som(0).Code_ID
            Me.gluPM_M_Code.Text = som(0).PM_M_Code
            Me.txtShelves_ID.Text = som(0).Shelves_ID
            Me.DatePackingDate.Text = Format(som(0).PackingDate, "yyyy/MM/dd")
            Me.txtPackingUserID.Text = som(0).PackingUserID
            Me.cbPackingType.Text = som(0).PackingType
            Me.txtQty.Text = som(0).Qty
            Me.gluD_ID.EditValue = som(0).D_ID
            EditAutoID = som(0).AutoID
            Me.CheckEdit2.Checked = som(0).CheckBit
            Me.txtSE_ID.Text = som(0).SE_ID

            boolCheck = som(0).CheckBit
            '.....................................................
            Dim pklist As New List(Of SamplePackingInfo)
            pklist = spcon.SamplePackingSub_GetList(Nothing, strField, Nothing, Nothing)
            If pklist.Count > 0 Then
                Dim i As Integer
                ds.Tables("SamplePackingSub").Clear()
                For i = 0 To pklist.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("SamplePackingSub").NewRow
                    row("PK_ID") = pklist(i).PK_ID
                    row("Code_ID") = pklist(i).Code_ID
                    row("PB_ID") = pklist(i).PB_ID
                    row("Qty") = pklist(i).Qty

                    row("AutoID") = pklist(i).AutoID
                    row("Remark") = pklist(i).Remark
                    ds.Tables("SamplePackingSub").Rows.Add(row)
                Next
            End If

            '.....................................................
            'Dim SpSublist As New List(Of SamplePackingInfo)
            'SpSublist = spcon.SamplePackingSubB_GetList(Nothing, strField, Nothing, Nothing)
            'If SpSublist.Count > 0 Then
            '    Dim i As Integer
            '    ds.Tables("SamplePackingSubB").Clear()
            '    For i = 0 To SpSublist.Count - 1
            '        Dim row As DataRow
            '        row = ds.Tables("SamplePackingSubB").NewRow
            '        row("PK_ID") = SpSublist(i).PK_ID
            '        row("Code_ID") = SpSublist(i).Code_ID
            '        row("PB_ID") = SpSublist(i).PB_ID
            '        row("Qty") = SpSublist(i).Qty
            '        row("AutoID") = SpSublist(i).AutoID
            '        row("Remark") = SpSublist(i).Remark
            '        ds.Tables("SamplePackingSubB").Rows.Add(row)
            '    Next
            'End If

        End If
    End Sub
#End Region

#Region "审核程序"
    ''' <summary>
    ''' 审核程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateCheck()
        If Me.CheckEdit2.Checked = boolCheck Then
            MsgBox("审核状态没有改变，请檢查原因！")
            Exit Sub
        End If

        Dim SSI As New SamplePackingInfo
        SSI.PK_ID = txtPK_ID.Text
        SSI.CheckBit = CheckEdit2.Checked
        SSI.CheckDate = Format(Now, "yyyy/MM/dd").ToString
        SSI.CheckUserID = InUserID
        SSI.CheckRemark = MemoEdit2.Text
        If spcon.SamplePacking_UpdateCheck(SSI) = False Then
            MsgBox("审核失敗，请檢查原因！")
            Exit Sub
        End If
        If CheckEdit2.Checked Then
            MsgBox("审核成功", 60, "提示")
        Else
            MsgBox("取消审核成功", 60, "提示")
        End If

        Me.Close()
    End Sub
#End Region

#Region "新增修改程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataNew() '新增
        Dim SSI As New SamplePackingInfo
        strPK_ID = GetPK_ID()
        SSI.PK_ID = strPK_ID
        SSI.Remark = Me.txt_remark.Text
        SSI.AddUserID = InUserID
        SSI.Code_ID = Me.txtCode_ID.Text
        SSI.PM_M_Code = Me.gluPM_M_Code.Text
        SSI.Shelves_ID = Me.txtShelves_ID.Text
        SSI.PackingDate = Me.DatePackingDate.Text
        SSI.PackingUserID = Me.txtPackingUserID.Text
        SSI.PackingType = Me.cbPackingType.Text
        SSI.Qty = Me.txtQty.Text
        SSI.D_ID = Me.gluD_ID.EditValue
        SSI.AddDate = Format(Now, "yyyy/MM/dd")
        SSI.SE_ID = Me.txtSE_ID.Text


        If spcon.SamplePacking_Add(SSI) = False Then
            MsgBox("添加失敗，请檢查原因！")
            Exit Sub
        Else
            '----------------------------------------------
            Dim i As Integer
            For i = 0 To ds.Tables("SamplePackingSub").Rows.Count - 1
                With ds.Tables("SamplePackingSub")
                    Dim strPB_ID As String = GetPB_ID()
                    Dim strPB As String = IIf(IsDBNull(.Rows(i)("PB_ID")), Nothing, .Rows(i)("PB_ID"))
                    Dim spInfo As New SamplePackingInfo
                    spInfo.PK_ID = strPK_ID
                    spInfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                    spInfo.PB_ID = strPB_ID
                    spInfo.Qty = IIf(IsDBNull(.Rows(i)("Qty")), Nothing, .Rows(i)("Qty"))
                    spInfo.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), Nothing, .Rows(i)("AutoID"))
                    spInfo.Remark = IIf(IsDBNull(.Rows(i)("Remark")), Nothing, .Rows(i)("Remark"))

                    If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) > 0 Then
                        If spcon.SamplePackingSub_Update(spInfo) = False Then
                            MsgBox("添加失敗，请檢查原因！")
                            Exit Sub
                        End If
                    Else
                        If spcon.SamplePackingSub_Add(spInfo) = False Then
                            MsgBox("添加失敗，请檢查原因！")
                            Exit Sub
                        End If
                    End If
                End With
            Next
            '----------------------------------------------
        End If
        MsgBox("添加成功", 60, "提示")
        Me.Close()
    End Sub

    ''' <summary>
    '''修改
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '更新刪除列表記錄
        If ds.Tables("DelSamplePackingSub").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSamplePackingSub").Rows.Count - 1
                spcon.SamplePackingSub_Delete(ds.Tables("DelSamplePackingSub").Rows(j)("AutoID"), Nothing, Nothing) '刪除当前选定的
            Next
        End If
        '更新刪除列表記錄
        'If ds.Tables("DelSamplePackingSubB").Rows.Count > 0 Then
        '    Dim j As Integer
        '    For j = 0 To ds.Tables("DelSamplePackingSubB").Rows.Count - 1
        '        spcon.SamplePackingSubB_Delete(ds.Tables("DelSamplePackingSubB").Rows(j)("AutoID"), Nothing, Nothing) '刪除当前选定的
        '    Next
        'End If
        '修改数据
        Dim SSI As New SamplePackingInfo
        strPK_ID = Me.txtPK_ID.Text
        SSI.PK_ID = strPK_ID
        SSI.Remark = Me.txt_remark.Text
        SSI.ModifyUserID = InUserID
        SSI.Code_ID = Me.txtCode_ID.Text
        SSI.PM_M_Code = Me.gluPM_M_Code.Text
        SSI.Shelves_ID = Me.txtShelves_ID.Text
        SSI.PackingDate = Me.DatePackingDate.Text
        SSI.PackingUserID = Me.txtPackingUserID.Text
        SSI.PackingType = Me.cbPackingType.Text
        SSI.Qty = Me.txtQty.Text
        SSI.D_ID = Me.gluD_ID.EditValue
        SSI.ModifyDate = Format(Now, "yyyy/MM/dd")
        SSI.AutoID = EditAutoID
        SSI.SE_ID = Me.txtSE_ID.Text

        If spcon.SamplePacking_Update(SSI) = False Then
            MsgBox("修改失敗，请檢查原因！")
            Exit Sub
        Else
            '---------------------------------------內箱子表
            Dim i As Integer
            Dim strPB_ID As String = String.Empty

            For i = 0 To ds.Tables("SamplePackingSub").Rows.Count - 1
                With ds.Tables("SamplePackingSub")
                    Dim strPB As String = IIf(IsDBNull(.Rows(i)("PB_ID")), Nothing, .Rows(i)("PB_ID"))

                    Dim spInfo As New SamplePackingInfo
                    spInfo.PK_ID = Me.txtPK_ID.Text
                    spInfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                    spInfo.PB_ID = IIf(IsDBNull(.Rows(i)("PB_ID")), Nothing, .Rows(i)("PB_ID"))
                    spInfo.Qty = IIf(IsDBNull(.Rows(i)("Qty")), Nothing, .Rows(i)("Qty"))
                    spInfo.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), Nothing, .Rows(i)("AutoID"))
                    spInfo.Remark = IIf(IsDBNull(.Rows(i)("Remark")), Nothing, .Rows(i)("Remark"))

                    If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) > 0 Then
                        If spcon.SamplePackingSub_Update(spInfo) = False Then
                            MsgBox("添加失敗，请檢查原因！")
                            Exit Sub
                            'Else
                            '    '---------------------------------------修改的情况-产品条码
                            '    Dim strGetPB As String = GetPB_ID()
                            '    spInfo.PB_ID = strGetPB
                            '    Dim M As Integer
                            '    For M = 0 To ds.Tables("SamplePackingSubB").Rows.Count - 1
                            '        With ds.Tables("SamplePackingSubB")
                            '            Dim stPBID As String = IIf(IsDBNull(.Rows(M)("PB_ID")), Nothing, .Rows(M)("PB_ID"))
                            '            If strPB = stPBID And IIf(IsDBNull(.Rows(M)("AutoID")), 0, .Rows(M)("AutoID")) = 0 Then
                            '                Dim spBInfo As New SamplePackingInfo
                            '                spBInfo.PK_ID = strPK_ID
                            '                spBInfo.Code_ID = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                            '                spBInfo.PB_ID = strPB
                            '                spBInfo.Qty = IIf(IsDBNull(.Rows(M)("Qty")), Nothing, .Rows(M)("Qty"))
                            '                spBInfo.AutoID = IIf(IsDBNull(.Rows(M)("AutoID")), Nothing, .Rows(M)("AutoID"))
                            '                spBInfo.Remark = IIf(IsDBNull(.Rows(M)("Remark")), Nothing, .Rows(M)("Remark"))
                            '                If spcon.SamplePackingSubB_Add(spBInfo) = False Then
                            '                    MsgBox("添加失敗，请檢查原因！")
                            '                    Exit Sub
                            '                End If
                            '            End If
                            '        End With
                            '    Next

                        End If
                    Else
                        '-------------------------------------------新增的情况-产品条码
                        Dim strGetPB As String = GetPB_ID()
                        spInfo.PB_ID = strGetPB
                        If spcon.SamplePackingSub_Add(spInfo) = False Then
                            MsgBox("添加失敗，请檢查原因！")
                            Exit Sub
                            'Else
                            '    Dim M As Integer
                            '    For M = 0 To ds.Tables("SamplePackingSubB").Rows.Count - 1
                            '        With ds.Tables("SamplePackingSubB")
                            '            Dim stPBID As String = IIf(IsDBNull(.Rows(M)("PB_ID")), Nothing, .Rows(M)("PB_ID"))
                            '            If strPB = stPBID Then
                            '                Dim spBInfo As New SamplePackingInfo
                            '                spBInfo.PK_ID = strPK_ID
                            '                spBInfo.Code_ID = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                            '                spBInfo.PB_ID = strGetPB
                            '                spBInfo.Qty = IIf(IsDBNull(.Rows(M)("Qty")), Nothing, .Rows(M)("Qty"))
                            '                spBInfo.AutoID = IIf(IsDBNull(.Rows(M)("AutoID")), Nothing, .Rows(M)("AutoID"))
                            '                spBInfo.Remark = IIf(IsDBNull(.Rows(M)("Remark")), Nothing, .Rows(M)("Remark"))
                            '                If spcon.SamplePackingSubB_Add(spBInfo) = False Then
                            '                    MsgBox("添加失敗，请檢查原因！")
                            '                    Exit Sub
                            '                End If
                            '            End If
                            '        End With
                            '    Next
                        End If

                    End If
                End With
            Next
        End If
        MsgBox("修改成功", 60, "提示")
        Me.Close()
    End Sub
#End Region

#Region "检查数据"
    ''' <summary>
    ''' 是否为空
    ''' </summary>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer
        If gluPM_M_Code.Text = String.Empty Then
            MsgBox("產品編號不能为空,请输入！")
            gluPM_M_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If gluD_ID.Text = String.Empty Then
            MsgBox("部门不能为空,请输入！")
            gluD_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If txtCode_ID.Text = String.Empty Then
            MsgBox("條碼編號不能为空,请输入！")
            txtCode_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        'If txtShelves_ID.Text = String.Empty Then
        '    MsgBox("貨架號不能为空,请输入！")
        '    txtShelves_ID.Focus()
        '    DataCheckEmpty = 0
        '    Exit Function
        'End If

        If ds.Tables("SamplePackingSub").Rows.Count <= 0 Then
            MsgBox("子表不能为空,请输入！")
            DataCheckEmpty = 0
            Exit Function
        End If


        '產品唯一處理
        Dim i As Integer
        For i = 0 To ds.Tables("SamplePackingSub").Rows.Count - 1
            Dim splist As New List(Of SamplePackingInfo)
            Dim strCode_ID As String = ds.Tables("SamplePackingSub").Rows(i)("Code_ID")
            '2.条码数据检查
            Dim scclist As New List(Of SampleCollectionInfo)
            scclist = Sccon.SampleCollection_Getlist(Nothing, strCode_ID, Nothing, Nothing, Nothing, Nothing, False, Nothing, gluPM_M_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scclist.Count > 0 Then
            Else
                MsgBox("采集表不存在此条码或此产品不存在此条码")
                DataCheckEmpty = 0
                Exit Function
            End If
            '3.此条码是否可能再收发--------------------------
            Dim scmlist As New List(Of SampleCollectionInfo)
            scmlist = Sccon.SampleCollection_Getlist(Nothing, strCode_ID, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scmlist.Count > 0 Then
                If scmlist(0).StatusType <> "Z" Then

                    MsgBox("此条码不是为在产状态")
                    DataCheckEmpty = 0
                    Exit Function
                End If
                If gluD_ID.EditValue <> scmlist(0).D_ID Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    MsgBox("此条码不在此部门")
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If
        Next
        DataCheckEmpty = 1
    End Function
#End Region

#Region "自动流水号"
    ''' <summary>
    ''' 自動流水号
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPK_ID() As String
        Dim oc As New SamplePackingController
        Dim oi As New SamplePackingInfo
        Dim ndate As String = "PK" + Format(Now(), "yyMMdd")
        oi = oc.SamplePacking_Get(ndate)
        If oi Is Nothing Then
            GetPK_ID = "PK" + Format(Now, "yyMMdd") + "0001"
        Else
            GetPK_ID = "PK" + Format(Now, "yyMMdd") + Mid(CInt(Mid(oi.PK_ID, 9)) + 100000000001, 9)
        End If
    End Function
    ''' <summary>
    ''' 內箱單號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPB_ID() As String
        Dim oc As New SamplePackingController
        Dim oi As New SamplePackingInfo
        Dim ndate As String = "PB" + Format(Now(), "yyMMdd")
        oi = oc.SamplePackingSub_Get(ndate)
        If oi Is Nothing Then
            GetPB_ID = "PB" + Format(Now, "yyMMdd") + "0001"
        Else
            GetPB_ID = "PB" + Format(Now, "yyMMdd") + Mid(CInt(Mid(oi.PB_ID, 9)) + 100000000001, 9)
        End If
    End Function


#End Region

#Region "按鍵事件"

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If DataCheckEmpty() = 0 Then
            Exit Sub
        End If

        Select Case EditItem
            Case "Add"
                DataNew()
            Case "Edit"
                DataEdit()
            Case "Check"
                UpdateCheck()
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            '0.条码数据检查
            If gluPM_M_Code.EditValue = String.Empty Then
                MsgBox("產品編號不能为空,请输入！")
                gluPM_M_Code.Focus()
                Exit Sub
            End If
            If Me.gluD_ID.EditValue = String.Empty Then
                MsgBox("部门不能为空,请输入！")
                gluD_ID.Focus()
                Exit Sub
            End If
            '1.條碼重複
            Label9.Text = String.Empty
            Dim strM_Code As String
            Dim i As Integer
            strM_Code = UCase(Me.txtM_Code.Text)
            For i = 0 To ds.Tables("SamplePackingSub").Rows.Count - 1
                If strM_Code = ds.Tables("SamplePackingSub").Rows(i)("Code_ID") Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    Label9.Text = strM_Code + "條碼重複"
                    Exit Sub
                End If
            Next
            '2.条码数据检查
            Dim scclist As New List(Of SampleCollectionInfo)
            scclist = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, gluPM_M_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scclist.Count > 0 Then
            Else
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                Label9.Text = "采集表不存在此条码或此产品不存在此条码"
                Exit Sub
            End If
            '3.此条码是否可能再收发--------------------------
            Dim scmlist As New List(Of SampleCollectionInfo)
            scmlist = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scmlist.Count > 0 Then
                If scmlist(0).StatusType <> "Z" Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    Label9.Text = "此条码不是为在产"
                    Exit Sub
                End If
                If gluD_ID.EditValue <> scmlist(0).D_ID Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    Label9.Text = "此条码不在此部门"
                    Exit Sub
                End If
            End If


            Dim strCode As String
            Dim strQty As String
            Dim intIn As Integer
            Dim StrText As String
            StrText = txtM_Code.Text
            intIn = InStr(StrText, ",", CompareMethod.Text)
            If StrText = "" Then Exit Sub

            If intIn <= 0 Then
                strCode = StrText
                strQty = "0"
            Else
                strCode = Mid(StrText, 1, intIn - 1)
                strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
            End If
            Dim row As DataRow
            row = ds.Tables("SamplePackingSub").NewRow

            row("Code_ID") = Trim(StrConv(UCase(strCode), vbNarrow))
            'row("PB_ID") = Mid(CStr(GetPB_IDDS() + 100001), 2)

            row("Qty") = 1
            ds.Tables("SamplePackingSub").Rows.Add(row)
            txtM_Code.Text = ""
            '----------------------------------------
            Dim intSub As Integer = 0
            intSub = ds.Tables("SamplePackingSub").Rows.Count
            txtQty.Text = CStr(intSub)
        End If
    End Sub

    Function GetPB_IDDS() As Integer
        Dim strPB_ID As Integer = 0
        If ds.Tables("SamplePackingSub").Rows.Count = 0 Then
            GetPB_IDDS = 1
        Else
            Dim i As Integer
            For i = 0 To ds.Tables("SamplePackingSub").Rows.Count - 1
                Dim strTA As String = String.Empty
                If Len(ds.Tables("SamplePackingSub").Rows(i)("PB_ID")) > 6 Then
                    strTA = Mid(ds.Tables("SamplePackingSub").Rows(i)("PB_ID"), 7)
                Else
                    strTA = ds.Tables("SamplePackingSub").Rows(i)("PB_ID")
                End If

                If strPB_ID < CInt(strTA) Then
                    strPB_ID = CInt(strTA)
                End If
            Next
        End If
        GetPB_IDDS = strPB_ID
    End Function

    'Private Sub txtM_CodeSub_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If Me.lbl_PB_ID.Text = String.Empty Then
    '        Label10.Text = "請選擇內箱"
    '        Exit Sub
    '    End If
    '    If e.KeyCode = Keys.Enter Then
    '        Label10.Text = String.Empty
    '        Dim strM_Code As String
    '        Dim i As Integer
    '        strM_Code = UCase(Me.txtM_CodeSub.Text)
    '        For i = 0 To ds.Tables("SamplePackingSubB").Rows.Count - 1
    '            If strM_Code = ds.Tables("SamplePackingSubB").Rows(i)("Code_ID") Then
    '                txtM_CodeSub.Text = String.Empty
    '                txtM_CodeSub.Focus()
    '                Label10.Text = strM_Code + "條碼重複"
    '                Exit Sub
    '            End If
    '        Next

    '        Dim strCode As String
    '        Dim strQty As String
    '        Dim intIn As Integer
    '        Dim StrText As String
    '        StrText = txtM_CodeSub.Text
    '        intIn = InStr(StrText, ",", CompareMethod.Text)
    '        If StrText = "" Then Exit Sub

    '        If intIn <= 0 Then
    '            strCode = StrText
    '            strQty = "1"
    '        Else
    '            strCode = Mid(StrText, 1, intIn - 1)
    '            strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
    '        End If


    '        Dim row As DataRow
    '        row = ds.Tables("SamplePackingSubB").NewRow

    '        row("Code_ID") = StrConv(UCase(strCode), vbNarrow)
    '        row("PB_ID") = Me.lbl_PB_ID.Text
    '        row("Qty") = 1
    '        ds.Tables("SamplePackingSubB").Rows.Add(row)
    '        txtM_CodeSub.Text = ""
    '        '------------------------------------------
    '        Dim m As Integer = 0
    '        For i = 0 To ds.Tables("SamplePackingSubB").Rows.Count - 1
    '            If Me.lbl_PB_ID.Text = ds.Tables("SamplePackingSubB").Rows(i)("PB_ID") Then
    '                m = m + 1
    '            End If
    '        Next
    '        GridView1.SetFocusedRowCellValue(Me.QtyA, m)
    '    End If
    'End Sub

    Private Sub tsmNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNew.Click

    End Sub

    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If GridView1.RowCount = 0 Then Exit Sub
        '-------------------------------------------------
        'Dim m As Integer = 0
        'Dim strPB_ID = GridView1.GetFocusedRowCellValue("PB_ID").ToString
        'For m = 0 To ds.Tables("SamplePackingSubB").Rows.Count - 1
        '    If strPB_ID = ds.Tables("SamplePackingSubB").Rows(m)("PB_ID") Then
        '        MsgBox("不能刪除存在產品資料！")
        '        Exit Sub
        '    End If
        'Next
        '----------------------------- --------------------
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(GridView1.GetSelectedRows(0), "AutoID")
        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("DelSamplePackingSub").NewRow
            row("AutoID") = ds.Tables("SamplePackingSub").Rows(GridView1.FocusedRowHandle)("AutoID")
            ds.Tables("DelSamplePackingSub").Rows.Add(row)
        End If
        ds.Tables("SamplePackingSub").Rows.RemoveAt(GridView1.GetSelectedRows(0))
        '-------------------------------------------------保存內箱數量
        Dim intSub As Integer = ds.Tables("SamplePackingSub").Rows.Count
        txtQty.Text = intSub
    End Sub

    'Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
    '    Me.lbl_PB_ID.Text = GridView1.GetFocusedRowCellValue("PB_ID").ToString()
    '    Me.GridView2.ActiveFilterString = "[PB_ID] = '" & Me.lbl_PB_ID.Text & "'"

    'End Sub

    'Private Sub tsmNewSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmNewSub.Click

    'End Sub

    'Private Sub tsmDelSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelSub.Click
    '    If GridView2.RowCount = 0 Then Exit Sub
    '    Dim DelTemp As String
    '    DelTemp = GridView2.GetRowCellDisplayText(GridView2.GetSelectedRows(0), "AutoID")
    '    If DelTemp <> String.Empty Then
    '        Dim row As DataRow = ds.Tables("DelSamplePackingSubB").NewRow
    '        row("AutoID") = ds.Tables("SamplePackingSubB").Rows(GridView2.FocusedRowHandle)("AutoID")
    '        ds.Tables("DelSamplePackingSubB").Rows.Add(row)
    '    End If
    '    ds.Tables("SamplePackingSubB").Rows.RemoveAt(GridView2.GetSelectedRows(0))
    '    '------------------------------------------
    '    Dim i As Integer = 0
    '    Dim m As Integer = 0
    '    For i = 0 To ds.Tables("SamplePackingSubB").Rows.Count - 1
    '        If Me.lbl_PB_ID.Text = ds.Tables("SamplePackingSubB").Rows(i)("PB_ID") Then
    '            m = m + 1
    '        End If
    '    Next
    '    GridView1.SetFocusedRowCellValue(Me.QtyA, m)
    'End Sub
#End Region

End Class

