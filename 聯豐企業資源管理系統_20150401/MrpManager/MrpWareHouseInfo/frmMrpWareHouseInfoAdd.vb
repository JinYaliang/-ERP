Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.MrpManager.MrpWareHouseInfo
Imports LFERP.Library.MrpManager.MrpMaterialCode
Imports LFERP.Library.MrpManager.MrpInfo
Imports DevExpress.XtraEditors.Controls

Public Class frmMrpWareHouseInfoAdd

#Region "�ݩ�"
    Dim MWHIcon As New MrpWareHouseInfoController
    Dim MWHIEcon As New MrpWareHouseInfoEntryController
    Dim MMICcon As New MrpMaterialCodeController
    Dim sms As New SystemUserController
    Dim ds As New DataSet

    Private boolCheck As Boolean
    Private _EditItem As String '�ݩ����
    Private _EditValue As String
    Private _DataList As List(Of MrpWareHouseInfoEntryInfo)    'MRP�B����w�s�O��
    Private _MrpID As String
    Property EditItem() As String '�ݩ�
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property EditValue() As String '�ݩ�
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property
    Property DataList() As List(Of MrpWareHouseInfoEntryInfo) '�ݩ�
        Get
            Return _DataList
        End Get
        Set(ByVal value As List(Of MrpWareHouseInfoEntryInfo))
            _DataList = value
        End Set
    End Property
    Property MrpID() As String '�ݩ�
        Get
            Return _MrpID
        End Get
        Set(ByVal value As String)
            _MrpID = value
        End Set
    End Property
#End Region

#Region "���J�ƥ�"
    ''' <summary>
    ''' �[��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMrpWareHouseInfoAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        gueMRPID.Properties.DisplayMember = "MRP_ID"    'txt
        gueMRPID.Properties.ValueMember = "MRP_ID"   'EditValue
        Dim mc As New MrpInfoController
        gueMRPID.Properties.DataSource = mc.MrpInfo_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Select Case EditItem
            Case EditEnumType.ADD
                txtWare_ID.Text = GetWareID()
                gueMRPID.Enabled = True
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.ADD)
                dteRecordCreateDate.Text = Format(System.DateTime.Now, "yyyy/MM/dd")
                GridView1.OptionsBehavior.Editable = True
                If (DataList Is Nothing = False) Then
                    FillTable(DataList)
                    gueMRPID.Enabled = False
                    ContextMenuStrip2.Visible = False
                    GridView1.Columns("M_Code").OptionsColumn.AllowEdit = False
                    gueMRPID.EditValue = MrpID
                    chkMD_ISMrp.Checked = True
                    Exit Sub
                End If
                Grid3.DataSource = MMICcon.MrpMaterialCode_GetList(Nothing, Nothing, "True", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case EditEnumType.EDIT
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.EDIT)
                LoadData(EditValue) '���J�ƾ�
                Grid3.DataSource = MMICcon.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                'txtWare_ID.Enabled = True
                gueMRPID.Enabled = True
                dteRecordCreateDate.Enabled = True
                GridView1.OptionsBehavior.Editable = True
            Case EditEnumType.VIEW
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.VIEW)
                LoadData(EditValue)
                GridView1.OptionsBehavior.Editable = False
                Grid1.ContextMenuStrip = Nothing
                cmdSave.Enabled = False
                XtraTabPage2.PageVisible = True
            Case EditEnumType.CHECK
                lblinfo.Text = lblinfo.Text + EditEnumValue(EditEnumType.CHECK)
                LoadData(EditValue)
                chkCheckBit.Enabled = True
                meeCheckRemark.Enabled = True
                Grid1.ContextMenuStrip = Nothing
                XtraTabPage2.PageVisible = True
                XtraTabControl1.SelectedTabPage = XtraTabPage2
        End Select
    End Sub
#End Region

#Region "�P��"
    ''' <summary>
    ''' �P��
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer
        If Me.gueMRPID.EditValue = String.Empty Then
            MsgBox("�B��渹���ର��,�п�J�I", MsgBoxStyle.Information, "����")
            gueMRPID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If Me.dteRecordCreateDate.Text = String.Empty Then
            MsgBox("�ЫذO��������ର��,�п�J�I", MsgBoxStyle.Information, "����")
            dteRecordCreateDate.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If ds.Tables("MrpWareHouseInfoEntry").Rows.Count = 0 Then
            MsgBox("�вK�[�w�s�H�����ӡI", MsgBoxStyle.Information, "����")
            Exit Function
        End If
        Dim i, j As Integer
        For i = 0 To ds.Tables("MrpWareHouseInfoEntry").Rows.Count - 1
            If ds.Tables("MrpWareHouseInfoEntry").Rows(i)("M_Code").ToString.Trim = String.Empty Then
                MsgBox("�вK�[�w�s�H�����ӡI", MsgBoxStyle.Information, "����")
                Exit Function
            End If
        Next
        For i = 0 To ds.Tables("MrpWareHouseInfoEntry").Rows.Count - 2
            For j = i + 1 To ds.Tables("MrpWareHouseInfoEntry").Rows.Count - 1
                With ds.Tables("MrpWareHouseInfoEntry")
                    If .Rows(i)("M_Code").ToString = .Rows(j)("M_Code").ToString Then
                        MsgBox("����s�b���_����:" + .Rows(i)("M_Code").ToString, MsgBoxStyle.Information, "����")
                        DataCheckEmpty = 0
                        Exit Function
                    End If
                End With
            Next
        Next
        DataCheckEmpty = 1
    End Function
#End Region

#Region "�K�[�ƾ�"
    ''' <summary>
    ''' �K�[�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataAdd()
        'If MWHIcon.MrpWareHouseInfo_MRPIDExists(gueMRPID.EditValue.ToString) = True Then
        '    MsgBox(gueMRPID.EditValue.ToString + "���B��渹�w�s�b,�L�k�O�s", MsgBoxStyle.Information, "����")
        '    Exit Sub
        'End If
        Dim fr As New frmMrpInfo
        fr.btnInsertWareHouse.Tag = True

        Dim MWHII As New MrpWareHouseInfoInfo
        Dim MWHIEI As New MrpWareHouseInfoEntryInfo

        MWHII.Ware_ID = txtWare_ID.Text
        MWHII.MRP_ID = gueMRPID.EditValue
        MWHII.TempMrpID = lblMRP_ID.Tag
        MWHII.MD_ISMrp = chkMD_ISMrp.Checked
        MWHII.MD_RecordCreateDate = dteRecordCreateDate.EditValue
        MWHII.CreateDate = Format(System.DateTime.Now, "yyyy/MM/dd")
        MWHII.CreateUserID = InUserID
        If MWHIcon.MrpWareHouseInfo_Insert(MWHII) = True Then
            '--------------------------------------------------------------------------------
            Dim i As Integer
            For i = 0 To ds.Tables("MrpWareHouseInfoEntry").Rows.Count - 1
                With ds.Tables("MrpWareHouseInfoEntry")
                    MWHIEI.Ware_ID = txtWare_ID.Text
                    MWHIEI.M_Code = IIf(IsDBNull(.Rows(i)("M_Code")), Nothing, .Rows(i)("M_Code"))
                    MWHIEI.MP_InventoryQty = IIf(IsDBNull(.Rows(i)("MP_InventoryQty")), 0, .Rows(i)("MP_InventoryQty"))
                    MWHIEI.MP_InTransitQty = IIf(IsDBNull(.Rows(i)("MP_InTransitQty")), 0, .Rows(i)("MP_InTransitQty"))
                    MWHIEI.MP_Inspection = IIf(IsDBNull(.Rows(i)("MP_Inspection")), 0, .Rows(i)("MP_Inspection"))
                    MWHIEI.MP_NoCollar = IIf(IsDBNull(.Rows(i)("MP_NoCollar")), 0, .Rows(i)("MP_NoCollar"))
                    MWHIEI.MP_RetreatQty = IIf(IsDBNull(.Rows(i)("MP_RetreatQty")), 0, .Rows(i)("MP_RetreatQty"))
                    MWHIEI.CreateUserID = InUserID
                    MWHIEI.CreateDate = System.DateTime.Now
                    MWHIEI.MP_Remark = IIf(IsDBNull(.Rows(i)("MP_Remark")), "", .Rows(i)("MP_Remark"))
                    If IIf(IsDBNull(.Rows(i)("M_Code")), String.Empty, .Rows(i)("M_Code")) <> String.Empty Then
                        If MWHIEcon.MrpWareHouseInfoEntry_Insert(MWHIEI) = False Then
                            MsgBox("�O�s���ѡA���ˬd��]", 60, "����")
                            Exit Sub
                        End If
                    End If
                End With
            Next
        Else
            MsgBox("�O�s���ѡA���ˬd��]", 60, "����")
            Exit Sub
        End If
        MsgBox("�O�s���\!", 60, "����")
        Me.Close()
    End Sub
#End Region

#Region "�s��ƾ�"
    ''' <summary>
    ''' �s��ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '�R���{�ɪ�O��
        Dim j As Integer
        For j = 0 To ds.Tables("MrpWareHouseInfoEntryDel").Rows.Count - 1
            If MWHIEcon.MrpWareHouseInfoEntry_Delete(ds.Tables("MrpWareHouseInfoEntryDel").Rows(j)("AutoID")) = False Then
                MsgBox("�O�s���ѡA���ˬd��]�I", 60, "����")
                Exit Sub
            End If
        Next
        '------------------------------------------
        Dim MWHII As New MrpWareHouseInfoInfo
        Dim MWHIEI As New MrpWareHouseInfoEntryInfo
        MWHII.Ware_ID = txtWare_ID.Text
        MWHII.MRP_ID = gueMRPID.Text
        MWHII.MD_ISMrp = chkMD_ISMrp.Checked
        MWHII.MD_RecordCreateDate = dteRecordCreateDate.EditValue
        MWHII.ModifyDate = Format(System.DateTime.Now, "yyyy/MM/dd")
        MWHII.ModifyUserID = InUserID
        '-----------------------------------------------------------------------
        If MWHIcon.MrpWareHouseInfo_Update(MWHII) = True Then
            Dim i As Integer
            For i = 0 To ds.Tables("MrpWareHouseInfoEntry").Rows.Count - 1
                With ds.Tables("MrpWareHouseInfoEntry")
                    MWHIEI.Ware_ID = txtWare_ID.Text
                    MWHIEI.M_Code = IIf(IsDBNull(.Rows(i)("M_Code")), Nothing, .Rows(i)("M_Code"))
                    MWHIEI.MP_InventoryQty = IIf(IsDBNull(.Rows(i)("MP_InventoryQty")), 0, .Rows(i)("MP_InventoryQty"))
                    MWHIEI.MP_InTransitQty = IIf(IsDBNull(.Rows(i)("MP_InTransitQty")), 0, .Rows(i)("MP_InTransitQty"))
                    MWHIEI.MP_Inspection = IIf(IsDBNull(.Rows(i)("MP_Inspection")), 0, .Rows(i)("MP_Inspection"))
                    MWHIEI.MP_NoCollar = IIf(IsDBNull(.Rows(i)("MP_NoCollar")), 0, .Rows(i)("MP_NoCollar"))
                    MWHIEI.MP_RetreatQty = IIf(IsDBNull(.Rows(i)("MP_RetreatQty")), 0, .Rows(i)("MP_RetreatQty"))
                    MWHIEI.CreateUserID = InUserID
                    MWHIEI.CreateDate = System.DateTime.Now
                    MWHIEI.ModifyUserID = InUserID
                    MWHIEI.ModifyDate = System.DateTime.Now
                    MWHIEI.MP_Remark = IIf(IsDBNull(.Rows(i)("MP_Remark")), 0, .Rows(i)("MP_Remark"))
                    MWHIEI.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), Nothing, .Rows(i)("AutoID"))
                    '�P�_�l��O�s�W�٬O�ק�
                    If ds.Tables("MrpWareHouseInfoEntry").Rows(i)("AutoID").ToString <> "" Then
                        If MWHIEcon.MrpWareHouseInfoEntry_Update(MWHIEI) = False Then
                            MsgBox("�O�s���ѡA���ˬd��]", 60, "����")
                            Exit Sub
                        End If
                    Else
                        If MWHIEcon.MrpWareHouseInfoEntry_Insert(MWHIEI) = False Then
                            MsgBox("�O�s���ѡA���ˬd��]", 60, "����")
                            Exit Sub
                        End If
                    End If
                End With
            Next
        Else
            MsgBox("�O�s���ѡA���ˬd��]", 60, "����")
            Exit Sub
        End If
        MsgBox("�O�s���\!", 60, "����")
        Me.Close()
    End Sub
#End Region

#Region "�f�ּƾ�"
    ''' <summary>
    ''' �f�ּƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataCheck()
        If chkCheckBit.Checked = boolCheck Then
            MsgBox("�f�֪��A�S������,���ˬd��]�I", 60, "����")
            Exit Sub
        End If

        Dim MWHII As New MrpWareHouseInfoInfo
        MWHII.Ware_ID = txtWare_ID.Text
        MWHII.CheckUserID = InUserID
        MWHII.CheckBit = chkCheckBit.Checked
        MWHII.CheckRemark = meeCheckRemark.Text

        If MWHIcon.MrpWareHouseInfo_UpdateCheck(MWHII) = False Then
            MsgBox("�f�֥��ѡA���ˬd��]�I", 60, "����")
            Exit Sub
        End If

        If chkCheckBit.Checked Then
            MsgBox("�f�֦��\!", 60, "����")
        Else
            MsgBox("�����f�֦��\!", 60, "����")
        End If
        Me.Close()
    End Sub
#End Region

#Region "���J�ƾ�"
    ''' <summary>
    ''' ���J�ƾ�
    ''' </summary>
    ''' <param name="StrWare_ID"></param>
    ''' <remarks></remarks>
    Sub LoadData(ByVal StrWare_ID As String)
        '-----------------------------------------------
        Dim MWHI_list As New List(Of MrpWareHouseInfoInfo)
        MWHI_list = MWHIcon.MrpWareHouseInfo_GetList(StrWare_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If MWHI_list.Count = 0 Then
            Exit Sub
        Else
            txtWare_ID.Text = MWHI_list(0).Ware_ID
            gueMRPID.Text = MWHI_list(0).MRP_ID
            dteRecordCreateDate.Text = MWHI_list(0).MD_RecordCreateDate
            chkMD_ISMrp.Checked = MWHI_list(0).MD_ISMrp
            chkCheckBit.Checked = MWHI_list(0).CheckBit
            boolCheck = MWHI_list(0).CheckBit
            meeCheckRemark.Text = MWHI_list(0).CheckRemark
        End If
        If MWHI_list(0).CheckBit = True Then
            lblCheckUserID.Text = MWHI_list(0).CheckUserName
            lblCheckDate.Text = Format(MWHI_list(0).CheckDate, "yyyy/MM/dd")
        ElseIf EditItem = EditEnumType.CHECK Then
            lblCheckUserID.Text = InUser
            lblCheckDate.Text = Format(Now, "yyyy/MM/dd")
        End If
        '-----------------------------------------------
        Dim MWHIE_list As New List(Of MrpWareHouseInfoEntryInfo)
        MWHIE_list = MWHIEcon.MrpWareHouseInfoEntry_GetList(MWHI_list(0).Ware_ID)
        FillTable(MWHIE_list)
    End Sub
#End Region

#Region "�Ы��{�ɪ�"
    Sub CreateTable() '�Ы��{�ɪ�
        ds.Tables.Clear()
        With ds.Tables.Add("MrpWareHouseInfoEntryDel") '�{�ɪ�
            .Columns.Add("AutoID", GetType(String))
        End With

        With ds.Tables.Add("MrpWareHouseInfoEntry") '�l��
            .Columns.Add("Ware_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("SourceID", GetType(String))
            .Columns.Add("Source", GetType(String))
            .Columns.Add("MP_InventoryQty", GetType(Decimal))
            .Columns.Add("MP_InTransitQty", GetType(Decimal))
            .Columns.Add("MP_Inspection", GetType(Decimal))
            .Columns.Add("MP_NoCollar", GetType(Decimal))
            .Columns.Add("MP_RetreatQty", GetType(Decimal))
            .Columns.Add("CreateUserID", GetType(String))
            .Columns.Add("CreateDate", GetType(String))
            .Columns.Add("ModifyUserID", GetType(String))
            .Columns.Add("ModifyDate", GetType(String))
            .Columns.Add("MP_Remark", GetType(String))
            .Columns.Add("AutoID", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("MrpWareHouseInfoEntry")
    End Sub
#End Region

#Region "��R�{�ɪ�"
    Sub FillTable(ByVal MWHIE_list As List(Of MrpWareHouseInfoEntryInfo))

        Dim i As Integer
        For i = 0 To MWHIE_list.Count - 1
            Dim row As DataRow
            row = ds.Tables("MrpWareHouseInfoEntry").NewRow
            row("AutoID") = MWHIE_list(i).AutoID
            row("Ware_ID") = MWHIE_list(i).Ware_ID
            row("M_Code") = MWHIE_list(i).M_Code
            row("M_Name") = MWHIE_list(i).M_Name
            row("M_Gauge") = MWHIE_list(i).M_Gauge
            row("M_Unit") = MWHIE_list(i).M_Unit
            row("SourceID") = MWHIE_list(i).SourceID
            row("Source") = MWHIE_list(i).Source
            row("MP_InventoryQty") = MWHIE_list(i).MP_InventoryQty
            row("MP_InTransitQty") = MWHIE_list(i).MP_Inspection
            row("MP_Inspection") = MWHIE_list(i).MP_Inspection
            row("MP_NoCollar") = MWHIE_list(i).MP_NoCollar
            row("MP_RetreatQty") = MWHIE_list(i).MP_RetreatQty
            row("CreateUserID") = MWHIE_list(i).CreateUserID
            row("CreateDate") = MWHIE_list(i).CreateDate
            row("ModifyUserID") = MWHIE_list(i).ModifyUserID
            row("ModifyDate") = MWHIE_list(i).ModifyDate
            row("MP_Remark") = MWHIE_list(i).MP_Remark
            ds.Tables("MrpWareHouseInfoEntry").Rows.Add(row)
        Next
    End Sub
#End Region

#Region "�O�s/�����ƥ�"
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �O�s
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
                DataAdd()
            Case EditEnumType.EDIT
                DataEdit()
            Case EditEnumType.CHECK
                DataCheck()
        End Select
    End Sub
#End Region

#Region "�s�W/�R���l��"
    ''' <summary>
    ''' �R���l��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsDelSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDelSub.Click
        If GridView1.RowCount > 0 Then
            Dim deltemp As String = ""
            deltemp = ds.Tables("MrpWareHouseInfoEntry").Rows(GridView1.FocusedRowHandle)("AutoID").ToString
            ds.Tables("MrpWareHouseInfoEntry").Rows.RemoveAt(GridView1.FocusedRowHandle)
            Dim row As DataRow
            If deltemp <> "" Then
                row = ds.Tables("MrpWareHouseInfoEntryDel").NewRow
                row("AutoID") = deltemp
                ds.Tables("MrpWareHouseInfoEntryDel").Rows.Add(row)
            End If
        End If
    End Sub
    ''' <summary>
    ''' �s�W�l��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsAddSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAddSub.Click
        'Dim MrpWareHouseInfoEntryItem As New MrpWareHouseInfoEntryInfo
        Dim row As DataRow
        row = ds.Tables("MrpWareHouseInfoEntry").NewRow
        row("Ware_ID") = Nothing
        row("M_Code") = Nothing
        row("M_Name") = Nothing
        row("M_Gauge") = Nothing
        row("M_Unit") = Nothing
        row("Source") = Nothing
        row("MP_InventoryQty") = 0
        row("MP_InTransitQty") = 0
        row("MP_Inspection") = 0
        row("MP_NoCollar") = 0
        row("MP_RetreatQty") = 0
        row("CreateUserID") = Nothing
        row("CreateDate") = Nothing
        row("ModifyUserID") = Nothing
        row("ModifyDate") = Nothing
        row("MP_Remark") = Nothing
        row("AutoID") = Nothing
        ds.Tables("MrpWareHouseInfoEntry").Rows.Add(row)
    End Sub
#End Region

#Region "�۰ʬy����"
    ''' <summary>
    ''' �۰ʬy����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWareID() As String
        Dim WareID As String
        Dim WIyyMM As String
        WIyyMM = "WI" & Format(Now, "yyMM")
        WareID = MWHIcon.MrpWareHouseInfo_GetWareID(WIyyMM)
        If WareID = "" Then
            WareID = Trim(WIyyMM & "00001")
        Else
            WareID = WIyyMM & Format(Val(Microsoft.VisualBasic.Right(WareID, 5)) + 1, "00000")
        End If
        Return WareID
    End Function
#End Region

    '#Region "�f�֪��A���ܮɤ~��O�s�f�֫H��"
    '    ''' <summary>
    '    ''' ��f�֪��A���ܮɤ~��O�s�f�֫H��
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub chkCheckBit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckBit.CheckedChanged
    '        cmdSave.Enabled = True
    '    End Sub

    '#End Region

#Region "�q�L���ƽs�X��o���ƫH��"
    ''' <summary>
    ''' ��ܪ��ƽs�X��o���ƦW�١B�W�浥�H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView4.Click
        GridView1.SetFocusedRowCellValue(Me.M_Code, GridView4.GetFocusedRowCellValue("M_Code"))
        GridView1.SetFocusedRowCellValue(Me.M_Gauge, GridView4.GetFocusedRowCellValue("M_Gauge"))
        GridView1.SetFocusedRowCellValue(Me.M_Name, GridView4.GetFocusedRowCellValue("M_Name"))
        GridView1.SetFocusedRowCellValue(Me.M_Unit, GridView4.GetFocusedRowCellValue("M_Unit"))
        GridView1.SetFocusedRowCellValue(Me.Source, GridView4.GetFocusedRowCellValue("MC_Source"))
    End Sub
#End Region

    Private Sub GridView1_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If GridView1.GetFocusedRowCellValue(e.Column).GetType().Name = "Decimal" Then
            Dim SourceID As String = GridView1.GetFocusedRowCellValue("SourceID").ToString
            Dim Source As String = GridView1.GetFocusedRowCellValue("Source").ToString
            'Dim M_Unit As String = GridView1.GetFocusedRowCellValue("M_Unit").ToString.Trim
            Dim value As Decimal = CDec(GridView1.GetFocusedRowCellValue(e.Column))
            If SourceID = "C" Or SourceID = "T" Then
                If GridView1.FocusedColumn.Name <> "MP_InventoryQty" And value <> 0 Then
                    MsgBox(Source + "���ݭn��g" + GridView1.FocusedColumn.Caption, MsgBoxStyle.Information, "����")
                    GridView1.SetFocusedRowCellValue(e.Column, 0)
                    Exit Sub
                End If
                If Integer.TryParse(value, 1) = False And value <> 0 Then
                    MsgBox(Source + "���w�s�ƶq�u�ର���", MsgBoxStyle.Information, "����")
                    GridView1.SetFocusedRowCellValue(e.Column, 0)
                    Exit Sub
                End If
            End If
            If value < 0 Then
                MsgBox(GridView1.FocusedColumn.Caption + "���ର�t��", MsgBoxStyle.Information, "����")
                GridView1.SetFocusedRowCellValue(e.Column, 0)
                Exit Sub
            End If
            'If "�Ӥ�⥻".Contains(M_Unit) And Integer.TryParse(value, 1) = False And value <> 0 Then
            'End If
        End If
    End Sub

#Region "�ƾڭ�����"
    Private Sub RepositoryItemCalcEdit15_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEdit15.EditValueChanging
        If (CDec(e.NewValue) > 999999999 Or CDec(e.NewValue) < 0) Then
            e.Cancel = True
        End If
    End Sub
#End Region

End Class