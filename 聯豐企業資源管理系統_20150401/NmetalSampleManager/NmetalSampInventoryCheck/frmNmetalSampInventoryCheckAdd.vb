Imports LFERP.Library.NmetalSampleManager.NmetalSampleTransaction
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.NmetalSampleManager.NmetalSampInventoryCheck
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess



Public Class frmNmetalSampInventoryCheckAdd
#Region "属性"
    Public ds As New DataSet
    Dim Sicon As New NmetalSampInventoryCheckControl
    Dim stcon As New NmetalSampleTransactionControler

    Private _EditType As String '属性栏位
    Private _AutoID As String
    Private _EditValue As String
    Private boolAnalysis As Boolean = False
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
            Return _EditType
        End Get
        Set(ByVal value As String)
            _EditType = value
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

#Region "载入窗体"
    Private Sub frmSamplePace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        Dim pc As New NmetalSampleProcessControl

        Me.gluD_Dep.Properties.DataSource = pc.NmetalProDepartMent_GetList(Nothing, "生產加工")
        Me.gluD_Dep.Properties.DisplayMember = "D_Dep"
        Me.gluD_Dep.Properties.ValueMember = "D_ID"

        txtAddUserID.Text = InUser
        Select Case EditItem
            Case "Add"
                dateAddDate.EditValue = Format(Now, "yyyy/MM/dd")
                Me.XtraTabPage2.PageVisible = False
                Me.XtraTabPage3.PageVisible = False
                cmdAnalysis.Visible = False
                txtAddUserID.Text = InUser
                cmdAnalysisAll.Visible = False
                ToolStripSeparator1.Visible = False
                ToolStripSeparator3.Visible = False
            Case "Edit"
                Me.XtraTabPage2.PageVisible = False
                Me.XtraTabPage3.PageVisible = False
                cmdAnalysis.Visible = False
                LoadData(EditValue)
                cmdAnalysisAll.Visible = False
                ToolStripSeparator1.Visible = False
                ToolStripSeparator3.Visible = False
            Case "Analysis"
                Me.XtraTabPage2.PageVisible = False
                cmdAddFile.Visible = False
                XtraTabControl1.SelectedTabPage = XtraTabPage3
                Label24.Visible = False
                txtM_Code.Visible = False
                LabelControl1.Visible = False

                Me.GridView1.Columns("Code_ID").OptionsColumn.ReadOnly = True
                Me.GridView1.Columns("Qty").OptionsColumn.ReadOnly = True
                Me.GridView1.Columns("Remark").OptionsColumn.ReadOnly = True

                LoadData(EditValue)
            Case "Check"
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                LoadData(EditValue)
                Label24.Visible = False
                txtM_Code.Visible = False
                LabelControl1.Visible = False
                cmdAnalysis.Visible = False
                cmdAddFile.Visible = False
            Case "Look"
                LoadData(EditValue)
                Me.cmdSave.Visible = False
                cmdAnalysis.Visible = False
                cmdAddFile.Visible = False
        End Select
    End Sub
#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SampInventoryCheck")
            .Columns.Add("CheckNO", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("Qty", GetType(String))
            .Columns.Add("Remark", GetType(String))
            .Columns.Add("AutoID", GetType(Decimal))
        End With
        GridSampleTransaction.DataSource = ds.Tables("SampInventoryCheck")

        With ds.Tables.Add("SampAnalysis")
            .Columns.Add("CheckNO", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("Qty", GetType(String))
            .Columns.Add("Status", GetType(String))
            .Columns.Add("StatusValue", GetType(String))
            .Columns.Add("AutoID", GetType(Decimal))
        End With
        GridSampAnalysis.DataSource = ds.Tables("SampAnalysis")

        With ds.Tables.Add("DelSampInventoryCheck")
            .Columns.Add("AutoID", GetType(String))
        End With

        With ds.Tables.Add("DelSampAnalysis")
            .Columns.Add("AutoID", GetType(String))
        End With
    End Sub
#End Region

#Region "載入数据"
    ''' <summary>
    ''' 載入数据
    ''' </summary>
    Sub LoadData(ByVal strCheckNO As String) '返回数据

        Dim som As New List(Of NmetalSampInventoryCheckInfo)
        som = Sicon.NmetalSampInventoryCheck_GetList(Nothing, strCheckNO, Nothing, Nothing, Nothing, Nothing)
        If som.Count = 0 Then
            Exit Sub
        Else
            Me.txtCheckNO.Text = som(0).CheckNO
            Me.dateAddDate.Text = som(0).CheckDate
            Me.txtAddUserID.Text = som(0).CheckActionName
            Me.CheckEdit2.Checked = som(0).CCheck
            Me.Label25.Text = som(0).CCheckActionName
            Me.Label26.Text = som(0).CCheckDate
            Me.gluD_Dep.EditValue = som(0).D_ID
            '.....................................................
            Dim i As Integer
            ds.Tables("SampInventoryCheck").Clear()
            For i = 0 To som.Count - 1
                Dim row As DataRow
                row = ds.Tables("SampInventoryCheck").NewRow
                row("CheckNO") = som(i).CheckNO
                row("Code_ID") = som(i).Code_ID
                row("Qty") = som(i).Qty
                row("Remark") = som(i).Remark
                row("AutoID") = som(i).AutoID
                ds.Tables("SampInventoryCheck").Rows.Add(row)
            Next
        End If

        Dim somlist As New List(Of NmetalSampInventoryCheckInfo)
        somlist = Sicon.NmetalSampInventoryCheckChaYi_GetList(strCheckNO, Nothing)
        If somlist.Count = 0 Then
            Exit Sub
        Else
            Dim i As Integer
            ds.Tables("SampAnalysis").Clear()
            For i = 0 To somlist.Count - 1
                Dim row As DataRow
                row = ds.Tables("SampAnalysis").NewRow
                row("CheckNO") = somlist(i).CheckNO
                row("Code_ID") = somlist(i).Code_ID
                row("Qty") = somlist(i).Qty
                row("Status") = somlist(i).Status
                row("AutoID") = somlist(i).AutoID
                ds.Tables("SampAnalysis").Rows.Add(row)
            Next
        End If
    End Sub
#End Region

#Region "子表事件"
    ''' <summary>
    ''' 表格新增
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnalysis.Click
        ds.Tables("SampAnalysis").Clear()
        If Me.txtCheckNO.Text = "自動編號" Then
            MsgBox("沒有生成單號,無法分析,请檢查原因！")
            Exit Sub
        End If

        Dim som As New List(Of NmetalSampInventoryCheckInfo)
        som = Sicon.NmetalSampInventoryCheckChaYiFX_GetList1(txtCheckNO.Text, Nothing)

        Dim i As Integer
        For i = 0 To som.Count - 1
            Dim row As DataRow
            row = ds.Tables("SampAnalysis").NewRow
            row("CheckNO") = txtCheckNO.Text
            row("Code_ID") = som(i).Code_ID
            row("Qty") = 1
            row("Status") = IIf(som(i).Status = 1, "盤盈", "盤虧")
            row("StatusValue") = som(i).Status
            ds.Tables("SampAnalysis").Rows.Add(row)
        Next
        XtraTabControl1.SelectedTabPage = XtraTabPage3
        boolAnalysis = True
    End Sub


    Private Sub cmdAnalysisAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnalysisAll.Click
        ds.Tables("SampAnalysis").Clear()
        If Me.txtCheckNO.Text = "自動編號" Then
            MsgBox("沒有生成單號,無法分析,请檢查原因！")
            Exit Sub
        End If

        Dim som As New List(Of NmetalSampInventoryCheckInfo)
        som = Sicon.NmetalSampInventoryCheckChaYiFX_GetList1(txtCheckNO.Text, "All")

        Dim i As Integer
        For i = 0 To som.Count - 1
            Dim row As DataRow
            row = ds.Tables("SampAnalysis").NewRow
            row("CheckNO") = txtCheckNO.Text
            row("Code_ID") = som(i).Code_ID
            row("Qty") = 1
            row("Status") = IIf(som(i).Status = 1, "盤盈", "盤虧")
            row("StatusValue") = som(i).Status
            ds.Tables("SampAnalysis").Rows.Add(row)
        Next
        XtraTabControl1.SelectedTabPage = XtraTabPage3
        boolAnalysis = True
    End Sub
    ''' <summary>
    ''' 刪除事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If GridView1.RowCount = 0 Then Exit Sub


        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(GridView1.GetSelectedRows(0), "AutoID")
        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("DelSampleTransaction").NewRow
            row("AutoID") = ds.Tables("SampleTransaction").Rows(GridView1.FocusedRowHandle)("AutoID")
            ds.Tables("DelSampleTransaction").Rows.Add(row)
        End If
        ds.Tables("SampleTransaction").Rows.RemoveAt(GridView1.GetSelectedRows(0))
    End Sub
#End Region

#Region "按键事件"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

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
            Case "Add"
                DataNew()
            Case "Edit"
                DataEdit()
            Case "Analysis"
                If boolAnalysis = True Then
                    DataNewAnalysis()
                    UpdateAnalysis() '分析確認
                Else
                    MsgBox("沒有盤點分析,保存錯誤！")
                    Exit Sub
                End If
            Case "Check"
                UpdateCheck()
        End Select
        MsgBox("保存成功！")
    End Sub

    Private Sub cmdAddFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFile.Click
        OpenFileDialog1.InitialDirectory = "c:\"
        OpenFileDialog1.Filter = "txt files (*.txt))|*.txt;"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True

        Dim PathStr As String

        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ds.Tables("SampInventoryCheck").Clear()
            PathStr = OpenFileDialog1.FileName

            Dim str As IO.StreamReader = New IO.StreamReader(PathStr, System.Text.Encoding.Default)
            Do Until str.EndOfStream
                Dim StrTemp As String
                Dim InstrA, i As Integer
                Dim strM_Code As String
                Dim boolCheck As Boolean = False
                StrTemp = str.ReadLine()
                InstrA = InStr(StrTemp, ",", CompareMethod.Text)

                If InstrA > 0 Then
                    Dim row As DataRow
                    row = ds.Tables("SampInventoryCheck").NewRow
                    row("Code_ID") = UCase(Mid(StrTemp, 1, InstrA - 1))
                    row("Qty") = 1
                    '去掉重複
                    strM_Code = UCase(Mid(StrTemp, 1, InstrA - 1))
                    For i = 0 To ds.Tables("SampInventoryCheck").Rows.Count - 1
                        If strM_Code = ds.Tables("SampInventoryCheck").Rows(i)("Code_ID") Then
                            Label2.Text = strM_Code + "條碼重複"
                            boolCheck = True
                        End If
                    Next

                    Label2.Text = String.Empty
                    If boolCheck = False Then
                        ds.Tables("SampInventoryCheck").Rows.Add(row)
                    End If
                End If
            Loop
            str.Close()
        End If

    End Sub

    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim strM_Code As String
            Dim i As Integer
            strM_Code = UCase(Me.txtM_Code.Text)
            For i = 0 To ds.Tables("SampInventoryCheck").Rows.Count - 1
                If strM_Code = ds.Tables("SampInventoryCheck").Rows(i)("Code_ID") Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    Label2.Text = strM_Code + "條碼重複"
                    Exit Sub
                End If
            Next

            Dim strCode As String
            Dim strQty As String
            Dim intIn As Integer
            Dim StrText As String
            StrText = txtM_Code.Text
            intIn = InStr(StrText, ",", CompareMethod.Text)
            If StrText = "" Then Exit Sub

            If intIn <= 0 Then
                strCode = StrText
                strQty = "1"
            Else
                strCode = Mid(StrText, 1, intIn - 1)
                strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
            End If


            Dim row As DataRow
            row = ds.Tables("SampInventoryCheck").NewRow
            row("Code_ID") = UCase(strCode)
            row("Qty") = 1
            ds.Tables("SampInventoryCheck").Rows.Add(row)
            txtM_Code.Text = ""
        End If
    End Sub
#End Region

#Region "审核程序"
    ''' <summary>
    ''' 审核程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateCheck()
        Dim siinfo As New NmetalSampInventoryCheckInfo
        siinfo.CheckNO = txtCheckNO.Text
        siinfo.CCheck = CheckEdit2.Checked
        siinfo.CCheckDate = Format(Now, "yyyy/MM/dd").ToString
        siinfo.CCheckAction = InUserID
        If Sicon.NmetalSampInventoryCheck_UpdateCheck(siinfo) = False Then
            MsgBox("审核失敗，请檢查原因！")
            Exit Sub
        End If
        Me.Close()
    End Sub
#End Region

#Region "分析程序"
    ''' <summary>
    ''' 审核程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateAnalysis()
        Dim siinfo As New NmetalSampInventoryCheckInfo
        siinfo.CheckNO = txtCheckNO.Text
        siinfo.AnalysisCheck = True
        siinfo.AnalysisDate = Format(Now, "yyyy/MM/dd").ToString
        siinfo.AnalysisAction = InUserID
        If Sicon.NmetalSampInventoryCheck_UpdateAnalysis(siinfo) = False Then
            MsgBox("分析確認失敗，请檢查原因！")
            Exit Sub
        End If
        Me.Close()
    End Sub
#End Region

#Region "新增修改程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataNewAnalysis() '新增差異
        If Me.txtCheckNO.Text <> "自動編號" Then
            Sicon.NmetalSampInventoryCheckChaYi_Delete(Me.txtCheckNO.Text) '刪除当前选定的
        End If

        Dim siinfo As New NmetalSampInventoryCheckInfo
        Dim i As Integer
        For i = 0 To ds.Tables("SampAnalysis").Rows.Count - 1
            With ds.Tables("SampAnalysis")
                siinfo.CheckNO = IIf(IsDBNull(.Rows(i)("CheckNO")), Nothing, .Rows(i)("CheckNO"))
                siinfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                siinfo.Qty = IIf(IsDBNull(.Rows(i)("Qty")), 0, .Rows(i)("Qty"))
                siinfo.Status = IIf(IsDBNull(.Rows(i)("StatusValue")), 0, .Rows(i)("StatusValue"))

                If Sicon.NmetalSampInventoryCheckChaYi_Add(siinfo) = False Then
                    MsgBox("修改失敗，请檢查原因！")
                    Exit Sub
                End If
            End With
        Next
        Me.Close()
    End Sub
    Sub DataNew() '新增
        Dim stinfo As New NmetalSampInventoryCheckInfo
        stinfo.CheckNO = GetTR_ID()
        stinfo.CheckDate = dateAddDate.Text
        stinfo.CheckAction = InUserID
        stinfo.D_ID = Me.gluD_Dep.EditValue

        Dim i As Integer
        For i = 0 To ds.Tables("SampInventoryCheck").Rows.Count - 1
            With ds.Tables("SampInventoryCheck")
                stinfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                stinfo.Qty = IIf(IsDBNull(.Rows(i)("Qty")), 0, .Rows(i)("Qty"))
                stinfo.Remark = IIf(IsDBNull(.Rows(i)("Remark")), Nothing, .Rows(i)("Remark"))
                stinfo.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID"))

                If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) > 0 Then
                    If Sicon.NmetalSampInventoryCheck_Update(stinfo) = False Then
                        MsgBox("修改失敗，请檢查原因！")
                        Exit Sub
                    End If
                Else
                    If Sicon.NmetalSampInventoryCheck_Add(stinfo) = False Then
                        MsgBox("修改失敗，请檢查原因！")
                        Exit Sub
                    End If
                End If
            End With
        Next
        Me.Close()
    End Sub
    ''' <summary>
    '''修改
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '更新刪除列表記錄
        If ds.Tables("DelSampInventoryCheck").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSampInventoryCheck").Rows.Count - 1
                Sicon.NmetalSampInventoryCheck_Delete(ds.Tables("DelSampInventoryCheck").Rows(j)("AutoID"), Nothing) '刪除当前选定的
            Next
        End If

        Dim stinfo As New NmetalSampInventoryCheckInfo
        stinfo.CheckNO = Me.txtCheckNO.Text
        stinfo.CheckDate = dateAddDate.Text
        stinfo.CheckAction = InUserID
        stinfo.D_ID = Me.gluD_Dep.EditValue

        Dim i As Integer
        For i = 0 To ds.Tables("SampInventoryCheck").Rows.Count - 1
            With ds.Tables("SampInventoryCheck")
                stinfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                stinfo.Qty = IIf(IsDBNull(.Rows(i)("Qty")), 0, .Rows(i)("Qty"))
                stinfo.Remark = IIf(IsDBNull(.Rows(i)("Remark")), Nothing, .Rows(i)("Remark"))
                stinfo.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID"))

                If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) > 0 Then
                    If Sicon.NmetalSampInventoryCheck_Update(stinfo) = False Then
                        MsgBox("修改失敗，请檢查原因！")
                        Exit Sub
                    End If
                Else
                    If Sicon.NmetalSampInventoryCheck_Add(stinfo) = False Then
                        MsgBox("修改失敗，请檢查原因！")
                        Exit Sub
                    End If
                End If
            End With
        Next

        Me.Close()
    End Sub
#End Region

#Region "检查数据"
    ''' <summary>
    ''' 是否为空
    ''' </summary>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer
        If txtAddUserID.Text = String.Empty Then
            MsgBox("創建人員不能为空,请输入！")
            txtAddUserID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        Dim i, m As Integer
        Dim strCode_id As String

        For i = 0 To ds.Tables("SampInventoryCheck").Rows.Count - 1
            strCode_id = ds.Tables("SampInventoryCheck").Rows(i)("Code_ID")
            For m = 0 To ds.Tables("SampInventoryCheck").Rows.Count - 1
                If i <> m Then
                    If strCode_id = ds.Tables("SampInventoryCheck").Rows(m)("Code_ID") Then
                        MsgBox("輸入的" + strCode_id + "條碼編號,有相同的記錄", 64, "提示")
                        GridSampleTransaction.Focus()
                        GridView1.FocusedRowHandle = i
                        DataCheckEmpty = 0
                        Exit Function
                    End If
                End If
            Next
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
    Function GetTR_ID() As String
        Dim sicon As New NmetalSampInventoryCheckControl
        Dim siinfo As New NmetalSampInventoryCheckInfo
        Dim ndate As String = "PD" + Format(Now(), "yyMM")
        siinfo = sicon.NmetalSampInventoryCheck_GetID(ndate)
        If siinfo Is Nothing Then
            GetTR_ID = "PD" + Format(Now, "yyMM") + "0001"
        Else
            GetTR_ID = "PD" + Format(Now, "yyMM") + Mid(CInt(Mid(siinfo.CheckNO, 7)) + 1000000001, 7)
        End If
    End Function
#End Region

End Class