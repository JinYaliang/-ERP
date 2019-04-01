Imports LFERP.Library.MrpManager.MrpMps
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.MrpForecastOrder

Public Class frmMrpMps

#Region "�r�q�ݩ�"
    Dim ds As New DataSet
    Dim mmc As New MrpMpsController
    Dim mmec As New MrpMpsEntryController
    Dim MrpCon As New MrpForecastOrderController
    Dim mbc As New Bom_MController
    Dim delAutoID As String = ""            '�O�����~���Ӫ�Q�R����AutoID
    Dim boCheck As Boolean = False          '�ΨӧP�_�f�֪��A�O�_������
    Private _EditItem As String
    Private _MO As String

    Property EditItem() As String '�ݩ�
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property MO() As String '�ݩ�
        Get
            Return _MO
        End Get
        Set(ByVal value As String)
            _MO = value
        End Set
    End Property

#End Region

#Region "�Ы��{�ɪ�"
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("MrpMps")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("MO", GetType(String))
            .Columns.Add("PlanQty", GetType(Decimal))
            .Columns.Add("ProducedQty", GetType(Decimal))
            .Columns.Add("NeedDate", GetType(Date))
            .Columns.Add("WeeksNumber", GetType(Int32))
            .Columns.Add("TimeSpan", GetType(String))
            .Columns.Add("Remarks", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("MrpMps")
    End Sub
#End Region

#Region "��R�{�ɪ�"
    Sub FillTable(ByVal mmeList As List(Of MrpMpsEntryInfo))
        Try
            Dim row As DataRow
            Dim i As Integer
            For i = 0 To mmeList.Count - 1
                row = ds.Tables("MrpMps").NewRow
                row("AutoID") = mmeList(i).AutoID
                row("MO") = mmeList(i).MO
                row("PlanQty") = mmeList(i).PlanQty
                row("ProducedQty") = mmeList(i).ProducedQty
                row("NeedDate") = mmeList(i).NeedDate
                row("WeeksNumber") = mmeList(i).WeeksNumber
                row("TimeSpan") = mmeList(i).TimeSpan
                row("Remarks") = mmeList(i).Remarks
                ds.Tables("MrpMps").Rows.Add(row)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "FillTable��k�X��")
        End Try
    End Sub
#End Region

#Region "����[���ƥ�"
    Private Sub frmMrpMps_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If EditItem = EditEnumType.ADD Then
            'gluM_Code.Properties.DataSource = mbc.Bom_M_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
        Else
            'gluM_Code.Properties.DataSource = mbc.Bom_M_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        gluCusterID.Properties.DataSource = MrpCon.CusterGetName(Nothing, Nothing)
        CreateTables()
        Select Case EditItem
            Case EditEnumType.ADD
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                Me.Text = lblTitle.Text
                txtMO.EditValue = "�۰ʽs��"
                SetControlEnable(True, False)
            Case EditEnumType.EDIT
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                Me.Text = lblTitle.Text
                SetControlEnable(True, False)
                LoadData(MO)
            Case EditEnumType.VIEW
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                Me.Text = lblTitle.Text
                SetControlEnable(False, False)
                LoadData(MO)
            Case EditEnumType.CHECK
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                Me.Text = lblTitle.Text
                SetControlEnable(False, True)
                LoadData(MO)
                XtraTabControl1.SelectedTabPage = xtpCheck
        End Select
    End Sub
#End Region

#Region "�]�m�U����Enable�ݩ�"
    Private Sub SetControlEnable(ByVal a As Boolean, ByVal b As Boolean)
        gluM_Code.Enabled = a
        gluCusterID.Enabled = a
        xtpCheck.PageVisible = Not a
        chkCheck.Enabled = b
        txtCheckRemark.Enabled = b
        lblCheckUser.Visible = Not a
        lblCheckDate.Visible = Not a
        View1.OptionsBehavior.Editable = a
        btnSave.Enabled = a Or b

        If a = False Then
            Grid1.ContextMenuStrip = Nothing
        End If
    End Sub
#End Region

#Region "�[���ƾ�"
    Private Sub LoadData(ByVal MO As String)
        Dim objInfo As List(Of MrpMpsInfo)
        objInfo = mmc.MrpMps_GetList(MO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If objInfo Is Nothing Then
            Exit Sub
        End If
        txtMO.EditValue = MO
        txtProductionQty.EditValue = objInfo(0).ProductionQty
        gluCusterID.EditValue = objInfo(0).CusterID
        txtCusterName.EditValue = objInfo(0).CusterName
        gluM_Code.EditValue = objInfo(0).M_Code
        txtM_Name.EditValue = objInfo(0).M_Name
        txtM_Unit.EditValue = objInfo(0).M_Unit
        txtM_Gauge.EditValue = objInfo(0).M_Gauge
        txtM_Source.EditValue = objInfo(0).M_Source
        boCheck = objInfo(0).CheckBit

        If objInfo(0).CheckBit = True Then
            chkCheck.Checked = True
            lblCheckUser.Text += objInfo(0).CheckUserName
            lblCheckDate.Text += IIf(objInfo(0).CheckDate = Nothing, "", Format(CDate(objInfo(0).CheckDate), "yyyy/MM/dd"))
            txtCheckRemark.EditValue = objInfo(0).CheckRemarks
        ElseIf EditItem = EditEnumType.CHECK Then
            lblCheckUser.Text += InUser
            lblCheckDate.Text += Format(Now, "yyyy/MM/dd")
        End If

        FillTable(mmec.MrpMpsEntry_GetList(MO))
    End Sub
#End Region

#Region "��ܲ��~�s�X�ɱa�X���~�W�ٵ��H��"
    Private Sub gluM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluM_Code.EditValueChanged, gluCusterID.EditValueChanged
        If sender.name = "gluM_Code" And gluM_Code.EditValue <> Nothing Then
            txtM_Name.EditValue = viewMaterial.GetFocusedRowCellValue("M_Name")
            txtM_Gauge.EditValue = viewMaterial.GetFocusedRowCellValue("M_Gauge")
            txtM_Unit.EditValue = viewMaterial.GetFocusedRowCellValue("M_Unit")
            txtM_Source.EditValue = viewMaterial.GetFocusedRowCellValue("M_Source")
        ElseIf sender.name = "gluCusterID" And gluCusterID.EditValue <> Nothing Then
            txtCusterName.EditValue = viewCuster.GetFocusedRowCellValue("MO_CusterName")
        End If
    End Sub
#End Region

#Region "�O�s�e�ˬd��J�ƾڬO�_���T"
    Private Function CheckSave() As Boolean
        Try
            CheckSave = True
            If gluCusterID.EditValue = Nothing Then
                MsgBox("�п�ܫȤ�", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
            If gluM_Code.EditValue = Nothing Then
                MsgBox("�п�ܲ��~�s��", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
            If EditItem = EditEnumType.CHECK And chkCheck.Checked = boCheck Then
                MsgBox("�f�֪��A�S������,�L�k�O�s", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("MrpMps").Rows.Count < 1 Then
                MsgBox("�ݨD���Ӥ����s�b�ƾڦ�,�L�k�O�s", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
            For i As Int16 = 0 To ds.Tables("MrpMps").Rows.Count - 1
                If IsDBNull(ds.Tables("MrpMps").Rows(i)("PlanQty")) Or IsDBNull(ds.Tables("MrpMps").Rows(i)("NeedDate")) Then
                    View1.FocusedRowHandle = i
                    MsgBox("�L�k�O�s�A�p���ͦ��ƶq�M�Ͳ���������ର��", MsgBoxStyle.Information, "����")
                    CheckSave = False
                    Exit Function
                End If
            Next
            Dim intYear, intWeekNum As Int16
            For i As Int16 = 0 To ds.Tables("MrpMps").Rows.Count - 1
                intYear = Year(ds.Tables("MrpMps").Rows(i)("NeedDate"))
                intWeekNum = ds.Tables("MrpMps").Rows(i)("WeeksNumber")
                For j As Int16 = i + 1 To ds.Tables("MrpMps").Rows.Count - 1
                    If intYear = Year(ds.Tables("MrpMps").Rows(j)("NeedDate")) And intWeekNum = ds.Tables("MrpMps").Rows(j)("WeeksNumber") Then
                        MsgBox(intYear.ToString + "�~��" + intWeekNum.ToString + "�P�s�b���_�A�L�k�O�s", MsgBoxStyle.Information, "����")
                        View1.FocusedRowHandle = i
                        CheckSave = False
                        Exit Function
                    End If
                Next

            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "����")
        End Try

    End Function
#End Region

#Region "�O�s�B�h�X�ƥ�"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (CheckSave() = False) Then
            Exit Sub
        End If
        Select Case EditItem
            Case EditEnumType.ADD
                SaveData()
            Case EditEnumType.EDIT
                SaveData()
            Case EditEnumType.CHECK
                Dim mmi As New MrpMpsInfo
                mmi.MO = txtMO.EditValue
                mmi.CheckBit = chkCheck.Checked
                mmi.CheckUserID = InUserID
                mmi.CheckRemarks = txtCheckRemark.Text
                If mmc.MrpMps_UpdateCheck(mmi) = True Then
                    If chkCheck.Checked = True Then
                        MsgBox("�f�֦��\", MsgBoxStyle.Information, "����")
                    Else
                        MsgBox("�����f�֦��\", MsgBoxStyle.Information, "����")
                    End If
                Else
                    MsgBox("�O�s����", MsgBoxStyle.Information, "����")
                End If
                Me.Close()
        End Select
    End Sub

#Region "�O�s�ƾ�"
    Private Sub SaveData()
        Dim ProductionQty As Decimal = 0
        Try
            If EditItem = EditEnumType.ADD Then
                txtMO.EditValue = mmc.MrpMps_GetID()
            ElseIf EditItem = EditEnumType.EDIT Then
                If String.IsNullOrEmpty(delAutoID) = False Then
                    Dim array1 As String()
                    array1 = delAutoID.Split(",")
                    For x As Int16 = 0 To array1.Length - 2
                        mmec.MrpMpsEntry_Delete(array1(x), Nothing)
                    Next
                End If
            End If
            If ds.Tables("MrpMps").Rows.Count > 0 Then
                Dim mmei As New MrpMpsEntryInfo
                Dim i As Integer
                For i = 0 To ds.Tables("MrpMps").Rows.Count - 1
                    ProductionQty += CDec(ds.Tables("MrpMps").Rows(i)("PlanQty"))
                    mmei.MO = txtMO.EditValue
                    mmei.AutoID = ds.Tables("MrpMps").Rows(i)("AutoID").ToString
                    mmei.PlanQty = ds.Tables("MrpMps").Rows(i)("PlanQty")
                    mmei.NeedDate = Format(CDate(ds.Tables("MrpMps").Rows(i)("NeedDate")), "yyyy/MM/dd")
                    mmei.ProducedQty = ds.Tables("MrpMps").Rows(i)("ProducedQty")
                    mmei.WeeksNumber = ds.Tables("MrpMps").Rows(i)("WeeksNumber")
                    mmei.TimeSpan = ds.Tables("MrpMps").Rows(i)("TimeSpan")
                    mmei.Remarks = ds.Tables("MrpMps").Rows(i)("Remarks").ToString
                    If EditItem = EditEnumType.ADD Then                 '�W�[
                        mmei.CreateUserID = InUserID
                        mmec.MrpMpsEntry_Add(mmei)
                    ElseIf EditItem = EditEnumType.EDIT Then                        '�ק�
                        mmei.ModifyUserID = InUserID
                        If mmei.AutoID = 0 Then
                            If mmec.MrpMpsEntry_Add(mmei) = False Then
                                MsgBox("�����O�s����")
                            End If
                        Else
                            If mmec.MrpMpsEntry_Update(mmei) = False Then
                                MsgBox("�����O�s����")
                            End If
                        End If
                    End If
                Next
            End If
            Dim mmi As New MrpMpsInfo
            mmi.CusterID = gluCusterID.EditValue
            mmi.M_Code = gluM_Code.EditValue
            mmi.CreateUserID = InUserID
            mmi.ModifyUserID = InUserID
            mmi.ProductionQty = ProductionQty
            mmi.MO = txtMO.EditValue
            If EditItem = EditEnumType.ADD Then
                If mmc.MrpMps_Add(mmi) = True Then
                    MsgBox("�K�[���\", MsgBoxStyle.Information, "����")
                Else
                    MsgBox("�K�[����", MsgBoxStyle.Information, "����")
                End If
            ElseIf EditItem = EditEnumType.EDIT Then
                If mmc.MrpMps_Update(mmi) = True Then
                    MsgBox("�ק令�\", MsgBoxStyle.Information, "����")
                Else
                    MsgBox("�ק異��", MsgBoxStyle.Information, "����")
                End If
            End If
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SaveData��k�X��")
        End Try
    End Sub
#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
#End Region

#Region "�k�����ƥ�"
    Private Sub cms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMpsAdd.Click, cmsMpsDel.Click
        Select Case sender.Name
            Case "cmsMpsAdd"
                Dim row As DataRow
                row = ds.Tables("MrpMps").NewRow
                row("ProducedQty") = 0
                row("AutoID") = 0
                ds.Tables("MrpMps").Rows.Add(row)
                View1.FocusedRowHandle = View1.RowCount - 1
            Case "cmsMpsDel"
                If (View1.GetFocusedRowCellValue("AutoID").ToString <> "0") Then
                    delAutoID += View1.GetFocusedRowCellValue("AutoID").ToString + ","
                End If
                ds.Tables("MrpMps").Rows.RemoveAt(View1.FocusedRowHandle)
                SetRightMenuEnable()
        End Select
    End Sub

    Private Sub grid_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Grid1.MouseDown
        If e.Button() = Windows.Forms.MouseButtons.Right Then
            SetRightMenuEnable()
        End If
    End Sub
    Private Sub SetRightMenuEnable()
        If View1.RowCount < 1 Then
            cmsMps.Items("cmsMpsDel").Enabled = False
        Else
            cmsMps.Items("cmsMpsDel").Enabled = True
        End If
    End Sub

    Private Sub cmsMpsBatchAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsMpsBatchAdd.Click
        Try
            Dim batchAddQty As Int16
            Dim beginDate As Date
            Dim planQty As Integer  '�p���Ͳ��ƶq
            Dim bFlag As Boolean = False  '�ΨӼаO�C�~�̦Z�@�P�O�_��~
            Dim fr As New frmMrpMpsBatchAdd
            fr.PlanQty = IIf(IsDBNull(View1.GetFocusedRowCellValue("PlanQty")), 0, View1.GetFocusedRowCellValue("PlanQty"))
            fr.BeginDate = IIf(IsDBNull(View1.GetFocusedRowCellValue("NeedDate")), Nothing, View1.GetFocusedRowCellValue("NeedDate"))
            fr.ShowDialog()
            beginDate = fr.BeginDate
            planQty = fr.PlanQty
            batchAddQty = fr.BatchAddQty

            For i As Int16 = 0 To batchAddQty - 1
                'If IsDBNull(View1.GetFocusedRowCellValue("NeedDate")) = False Then
                '    Continue For
                'End If
                Dim row As DataRow = ds.Tables("MrpMps").NewRow
                beginDate = GetNextWeekDate(beginDate, bFlag)
                row("AutoID") = 0
                row("ProducedQty") = 0
                If bFlag = True Then
                    bFlag = False
                    row("PlanQty") = CInt(planQty * DatePart(DateInterval.Weekday, beginDate) / 7)
                    row("NeedDate") = beginDate
                    row("WeeksNumber") = DatePart(DateInterval.WeekOfYear, beginDate)
                    row("TimeSpan") = GetTimeSpan(beginDate)
                    ds.Tables("MrpMps").Rows.Add(row)
                    Dim row1 As DataRow = ds.Tables("MrpMps").NewRow
                    row1("AutoID") = 0
                    row1("PlanQty") = planQty - row("PlanQty")
                    row1("ProducedQty") = 0
                    row1("NeedDate") = (Year(beginDate) + 1).ToString + "/01/01"
                    row1("WeeksNumber") = DatePart(DateInterval.WeekOfYear, row1("NeedDate"))
                    row1("TimeSpan") = GetTimeSpan(row1("NeedDate"))
                    ds.Tables("MrpMps").Rows.Add(row1)
                Else
                    row("PlanQty") = planQty
                    row("NeedDate") = beginDate
                    row("WeeksNumber") = DatePart(DateInterval.WeekOfYear, beginDate)
                    row("TimeSpan") = GetTimeSpan(beginDate)
                    ds.Tables("MrpMps").Rows.Add(row)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "����")
        End Try
    End Sub

#Region "���o���w������U�ӬP�������"
    Private Function GetNextWeekDate(ByVal paraDate As Date, ByRef bFlag As Boolean) As Date
        Dim nextWeekDate As Date = paraDate.AddDays(8 - DatePart(DateInterval.Weekday, paraDate))
        Dim i As Int16 = DatePart(DateInterval.Weekday, nextWeekDate) - 1
        Dim lastDayOfWeek As Date = nextWeekDate.AddDays(6 - i)

        If lastDayOfWeek.Date.Year <> Year(nextWeekDate) Then
            paraDate = CDate(Year(nextWeekDate).ToString + ".12.31")
            bFlag = True
        Else
            paraDate = paraDate.AddDays(7)
        End If
        Return paraDate
    End Function
#End Region
#End Region

    Private Sub ItemdteNeedDate_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ItemdteNeedDate.EditValueChanging
        Try
            Dim needDate As Date = CDate(e.NewValue)
            View1.SetFocusedRowCellValue(WeeksNumber, DatePart(DateInterval.WeekOfYear, needDate))
            View1.SetFocusedRowCellValue(TimeSpan, GetTimeSpan(needDate))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#Region "���o���w������P���ҥ]�t������q"
    Private Function GetTimeSpan(ByRef paraDate As Date) As String
        Dim i As Int16
        i = DatePart(DateInterval.Weekday, paraDate) - 1
        Dim firstDayOfWeek, lastDayOfWeek As String
        If Year(paraDate) <> Year(paraDate.AddDays(-i)) Then
            firstDayOfWeek = Year(paraDate).ToString + ".01.01"
            paraDate = CDate(Year(paraDate).ToString + ".01.01")
        Else
            firstDayOfWeek = Format(paraDate.AddDays(-i), "yyyy.MM.dd")
        End If

        If Year(paraDate) <> Year(paraDate.AddDays(6 - i)) Then
            lastDayOfWeek = Year(paraDate).ToString + ".12.31"
            paraDate = CDate(Year(paraDate).ToString + ".12.31")
        Else
            lastDayOfWeek = Format(paraDate.AddDays(6 - i), "yyyy.MM.dd")
        End If

        Return firstDayOfWeek + "-" + lastDayOfWeek
    End Function
#End Region

#Region "�ƾڭ�����"
    Private Sub RepositoryItemCalcEdit1_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEdit1.EditValueChanging
        If (CDec(e.NewValue) > 999999999 Or CDec(e.NewValue) < 0) Then
            e.Cancel = True
        End If
        If View1.FocusedColumn.FieldName = "PlanQty" Then
            View1.SetFocusedRowCellValue(PlanQty, e.NewValue)
        End If
        Dim qty As Decimal = 0
        Try
            For i As Int16 = 0 To View1.DataRowCount - 1
                qty += Convert.ToDecimal(IIf(IsDBNull(View1.GetDataRow(i)("PlanQty")), 0, View1.GetDataRow(i)("PlanQty")))
            Next
            txtProductionQty.EditValue = qty
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
#End Region

End Class