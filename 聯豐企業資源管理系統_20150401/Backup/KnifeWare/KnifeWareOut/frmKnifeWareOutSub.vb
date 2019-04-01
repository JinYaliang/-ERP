Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.Library
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.Shared
Imports LFERP.FileManager
Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.Library.Purchase.Apply
Imports LFERP.Library.ProductionMaterial
Imports System.Threading
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.ProductionField
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.Library.Production.ProductionAffair
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports LFERP.Library.Product
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.KnifeWare

Public Class frmKnifeWareOutSub
#Region "�r�q�ݩ�"
    Dim ds As New DataSet
    Dim oldcheck As Boolean
    Dim strWHID As String
    Dim strDPTID As String

    Public isBarCode As Boolean '�P�_�O�_�O���X���y
    Dim strPM_M_Code As String
    Dim isShowLCD As Boolean = False  '�O�_�bLCD�p�̹��W��ܥX�w�H��

    Private _EditItem As String
    Private _EditValue As String
    Private _EditID As String
    Private _NodeTag As String
    Private _NodeText As String

    Public Property EditItem() As String
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Public Property EditValue() As String
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property
    Public Property EditID() As String
        Get
            Return _EditID
        End Get
        Set(ByVal value As String)
            _EditID = value
        End Set
    End Property
    Public Property NodeTag() As String
        Get
            Return _NodeTag
        End Get
        Set(ByVal value As String)
            _NodeTag = value
        End Set
    End Property
    Public Property NodeText() As String
        Get
            Return _NodeText
        End Get
        Set(ByVal value As String)
            _NodeText = value
        End Set
    End Property

#End Region


    Private Sub frmKnifeWareOutAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        popApplyAdd.Visible = False
        RepositoryItemComboBox1.Items.Clear()


        Dim mc As New Purchase.SharePurchase.SharePurchaseController
        LoadUserPower()
        CreateTables()
        If isBarCode = True Then
            Label24.Visible = True
            txtM_Code.Visible = True
            LabelControl1.Visible = True
            popWareOutBarAdd.Visible = False
            txtM_Code.Select()
        End If


        txtWIPID.EditValue = EditID

        ''���m�s��d��
        'Dim reset As New ResetPassWords.SetPassWords
        'reset.SetPassWords()
        'txtWIPID.Enabled = Fals

        '------------------------------------
        Select Case EditItem
            Case "popWareOutAdd"
                If Edit = True Then
                    Me.Text = "�M��X�w--�ק�"
                    loadedit(txtWIPID.EditValue)
                    txtWIPID.Enabled = False
                    DateEdit1.Enabled = False
                ElseIf Edit = False Then
                    Select Case EditValue
                        Case "popWareOutAddNew"
                            cbKnifeType.Text = "�s�M"
                        Case "popWareOutAddOld"
                            cbKnifeType.Text = "�ݳB�z"
                    End Select
                    WO_EndQty_Show.Caption = "��e�w�s"
                    Me.Text = "�M��X�w--�s�W"
                    txtWIPID.EditValue = ""
                    DateEdit1.DateTime = Now
                    'getenable(True, False)
                    cbType.EditValue = "���`"
                    DateEdit1.Enabled = False

                    ' CheckEdit1.Checked = True  '�q�{���f�֪��A

                    ''---------------------------------------------------------------------------��d�P�_
                    strWHID = NodeTag
                    txtWH.EditValue = NodeText

                    Dim wc As New WareHouseController
                    Dim wiL As New List(Of WareHouseInfo)
                    wiL = wc.WareHouse_Get(strWHID)
                    If wiL.Item(0).NeedCheck = False Then
                        TextEdit1.Enabled = False
                        ButtonEdit2.Enabled = False
                        sk.Visible = True
                    Else
                        TextEdit1.Enabled = False
                        ButtonEdit2.Enabled = False
                    End If

                End If

                XtraTabControl1.SelectedTabPage = XtraTabPage1

                If strInCompany = "1001" Then
                    Label13.Visible = True
                    txtAPNO.Visible = True
                Else
                    Label13.Visible = False
                    txtAPNO.Visible = False

                End If
            Case "popWareOutCheck"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                SetObjectEnable(False, True)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                popWareInput.Enabled = False
            Case "popwareOutView"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                SetObjectEnable(False, False)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                cmdSave.Enabled = False
                popWareInput.Enabled = False
            Case "popWareOutReCheck"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage3
                SetObjectEnable(False, False)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                popWareInput.Enabled = False
            Case "ModifyRemarkTool" '2013-3-5 �ק�ƪ`
                Me.Text = "�M��X�w-�ק�ƪ`"
                loadedit(txtWIPID.EditValue)
                XtraTabPage1.PageVisible = True
                XtraTabPage2.PageVisible = False
                XtraTabPage3.PageVisible = False
                XtraTabPage4.PageVisible = False
                SetObjectEnable(False, False)
                AP_NO.OptionsColumn.AllowEdit = False
                ButtonEdit2.Enabled = False
                cbType.Enabled = False
                txtOPNO.Enabled = False
                WO_Qty.OptionsColumn.AllowEdit = False
                popWareInput.Enabled = False
        End Select
        '�[����������
        GridFile.AutoGenerateColumns = False
        GridFile.RowHeadersWidth = 15
        Dim dt As New FileController
        GridFile.DataSource = dt.FileBond_GetList("5002", txtWIPID.EditValue, Nothing)
        GridFile.Refresh()

        ' �Ȯɤ��Ψ�d��
        'If sk.Visible = False Then  ''��d���s ���i�� �N�����m�s 2013-4-9
        'Else
        '    '���m�s��d��
        Dim reset As New ResetPassWords.SetPassWords
        reset.SetPassWords()
        '    txtWIPID.Enabled = False
        'End If
    End Sub

    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("KnifeWareOut")
            .Columns.Add("WO_NUM", GetType(String))
            .Columns.Add("WO_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("WO_Qty", GetType(Int32))
            .Columns.Add("OS_BatchID", GetType(String))
            .Columns.Add("WO_Remark", GetType(String))
            .Columns.Add("AP_NO", GetType(String))    '�ӻ�渹---���J��  2012/8/28
            .Columns.Add("M_ID", GetType(String))
            .Columns.Add("Temp_Code", GetType(String))
            .Columns.Add("M_SendQty", GetType(Double))         '@ 2012/8/29 �K�[
            .Columns.Add("C_Qty", GetType(Double))         '@ 2012/10/9 �K�[
            .Columns.Add("PM_M_Code", GetType(String))    '@ 2013/4/8 �K�[
            .Columns.Add("WO_EndQty_Show", GetType(Double))
            .Columns.Add("WI_SafeQty_Show", GetType(Double))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("KnifeType", GetType(String))
            .Columns.Add("Enable_Qty_Show", GetType(Double))
            .Columns.Add("WI_SQty", GetType(String))         '�s�M�ƶq
            .Columns.Add("WI_SReQty", GetType(String))       '�ݳB�z�M�ƶq
        End With

        With ds.Tables.Add("DelDate")
            .Columns.Add("WO_NUM", GetType(String))
            .Columns.Add("WO_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
        End With

        Grid.DataSource = ds.Tables("KnifeWareOut")
    End Sub

    Sub AddRow(ByVal strCode As String, ByVal strQty As Double)
        Dim row As DataRow

        row = ds.Tables("KnifeWareOut").NewRow

        If strCode = "" Then

        Else
            Dim j As Integer
            For j = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1
                If strCode = ds.Tables("KnifeWareOut").Rows(j)("M_Code") Then
                    If isBarCode = True Then
                        ds.Tables("KnifeWareOut").Rows(j)("WO_Qty") = ds.Tables("KnifeWareOut").Rows(j)("WO_Qty") + strQty
                        '@ 2013/3/13 �K�[ �ƶq���ܮɪ��ƫH���bLCD��ܫ̤W���s���
                        'If isShowLCD = True Then
                        '    LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(j)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(j)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(j)("WO_Qty") & ds.Tables("WareOut").Rows(j)("M_Unit"), "")
                        'End If
                        GridView1.FocusedRowHandle = j
                        Exit Sub
                    Else
                        MsgBox("���Ƥw�s�b�A�P�@�i�X�w�椤�����\�s�b�ۦP���X�w���ơI", 64, "����")
                        Exit Sub
                    End If
                End If
            Next

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            If objInfo Is Nothing Then
                MsgBox("��e���Ƥ��s�b�A�нT�{�����ƫH���I", 64, "����")
                Exit Sub
            End If
            'If objInfo.M_IsEnabled = False Then
            '    MsgBox("��e���Ƥ��i�ΡA�нT�{�����ƫH���I", 64, "����")
            '    Exit Sub
            'End If

            row("WO_NUM") = Nothing
            row("WO_ID") = Nothing
            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("PM_M_Code") = strPM_M_Code

            Dim unit As New LFERP.DataSetting.UnitController
            Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

            unitinfo = unit.GetUnitList(objInfo.M_Unit)
            If unitinfo.Count > 0 Then
                row("M_Unit") = unitinfo(0).U_Name
            Else
                row("M_Unit") = ""
            End If
            row("M_Gauge") = objInfo.M_Gauge
            row("WO_Qty") = strQty
            row("AP_NO") = ""  '�ӻ�渹---���q�K�[���A�b�D�������
            row("M_ID") = ""  '����  ---�w��s�b�ӻ�渹
            row("Temp_Code") = "" '�{�ɪ��ƽs�X  --�w��s�b�ӻ�渹
            row("PS_NO") = ""


            Dim WareH As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim WareC As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            WareH = WareC.WareInventory_GetSub(strCode, strWHID)

            If WareH Is Nothing Then
                row("WI_SafeQty_Show") = 0
                row("WO_EndQty_Show") = 0
            Else
                row("WI_SafeQty_Show") = WareH.WI_SafeQty
                row("WO_EndQty_Show") = WareH.WI_Qty
            End If
            ''''''''''''''''''''2013-11-6���@�s�W

            ''''''''''''''''''''''''''''''''''''''''''''
            'row("WI_SQty") = frmKnifeBOMSelect.BOWI_SQty
            'row("WI_SReQty") = frmKnifeBOMSelect.BOWI_SReQty

            '--------------------------------------------
            Dim KwiOut As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
            Dim KwsOut As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
            KwiOut = KwsOut.KnifeWareInventorySub_GetList(strCode, strWHID)
            If KwiOut Is Nothing Then
                row("Enable_Qty_Show") = 0
                row("WI_SQty") = 0
                row("WI_SReQty") = 0
            Else
                Select Case EditValue
                    Case "popWareOutAddNew" ''''�s�M
                        row("Enable_Qty_Show") = KwiOut.WI_SQty
                    Case "popWareOutAddOld" '�ݳB�z
                        row("Enable_Qty_Show") = KwiOut.WI_SReQty
                End Select

                row("WI_SQty") = KwiOut.WI_SQty
                row("WI_SReQty") = KwiOut.WI_SReQty
            End If

        End If
        ''------------------------------------------------------
        ds.Tables("KnifeWareOut").Rows.Add(row)
        GridView1.MoveLast()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case EditItem
            Case "popWareOutAdd"
                If Edit = False Then            ''Edit�Ω�Ϥ��O�s�W�٬O�ק�
                    If CheckSave() = True Then
                        SaveDataNew()
                        cmdSave.Enabled = False
                        cmdPrint.Visible = True
                        MsgBox("�s�W���\", 64, "����")
                    End If
                ElseIf Edit = True Then
                    SaveDataEdit()
                    cmdSave.Enabled = False
                    cmdPrint.Visible = True
                    MsgBox("�ק令�\", 64, "����")
                End If
            Case "popWareOutCheck"
                UpdateCheck()
            Case "popWareOutReCheck"
                UpdateReCheck()
            Case "ModifyRemarkTool" ''�ק�ƪ`
                Update_Remark()
        End Select
    End Sub

    Function GetWO_ID() As String
        '�ͦ��spm
        Dim kwo As New KnifeWareOutController
        Dim kinfo As New KnifeWareOutInfo

        Dim ndate As String
        ndate = "WO" + Format(Now(), "yyMM")
        kinfo = kwo.KnifeWareOut_GetID(ndate)
        If kinfo Is Nothing Then
            GetWO_ID = ndate + "00001"
        Else
            GetWO_ID = ndate + Mid((CInt(Mid(kinfo.WO_ID, 7)) + 100001), 2)
        End If
    End Function

    Function GetWO_NUM() As String
        '�ͦ��spS
        Dim kwo As New KnifeWareOutController
        Dim kinfo As New KnifeWareOutInfo
        kinfo = kwo.KnifeWareOut_GetNUM(Nothing)
        If kinfo Is Nothing Then
            GetWO_NUM = "O000000001"
        Else
            GetWO_NUM = "O" & Mid((CInt(Mid(kinfo.WO_NUM, 2)) + 1000000001), 2)
        End If

    End Function

    Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click
        tempCode = ""
        tempValue5 = strWHID
        tempValue6 = "�ܮw�޲z"
        tempValue7 = "�M��X�w�@�~"
        'frmKnifeBOMSelect.KnifeOut = Label1.Text
        frmKnifeBOMSelect.ShowDialog()
        'AP_NO.OptionsColumn.AllowEdit = True
        'AP_NO.OptionsColumn.ReadOnly = True
        If frmKnifeBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            '�W�[�O��
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode, 0)
            End If
        ElseIf frmKnifeBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue7, ",")
            n = Len(Replace(tempValue7, ",", "," & "*")) - Len(tempValue7)
            For i = 0 To n
                If arr(i) = "" Then
                    Exit Sub
                End If
                AddRow(arr(i), 0)
            Next
        ElseIf frmKnifeBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then
            Dim i, n As Integer
            Dim arr(n) As String
            strPM_M_Code = NodeTag
            arr = Split(tempValue8, ",")
            n = Len(Replace(tempValue8, ",", "," & "*")) - Len(tempValue8)
            For i = 0 To n

                If arr(i) = "" Then
                    Exit Sub
                End If
                AddRow(arr(i), 0)
            Next
        End If
        tempValue7 = ""
        tempValue8 = ""
    End Sub

    Function CheckSave() As Boolean
        CheckSave = True
        If Len(TextEdit1.EditValue) = 0 Then
            MsgBox("�Ш�d", 64, "����")
            CheckSave = False
            Exit Function
        End If
        ''--------------------2012-12-17--����N����d--------------------------------------
        If sk.Visible = False Then '�q���ܤ���
        Else
            Dim wulc As New WhiteUserListController
            Dim wuliL As New List(Of WhiteuserListInfo)
            wuliL = wulc.WhiteUserListSub_GetList(TextEdit1.Text, strWHID)

            If wuliL.Count <= 0 Then
                CheckSave = False
                MsgBox("�S��d�ΫD�k�d�����\�O�s", 64, "����")
                Exit Function
            End If

            Dim j As Integer
            For j = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1
                If ds.Tables("KnifeWareOut").Rows(j)("WO_Qty") > 0 Then
                    If wuliL.Item(0).WareOut = False Then
                        CheckSave = False
                        MsgBox("�S���X�w�v��", 64, "����")
                        Exit Function
                    End If
                Else
                    If wuliL.Item(0).WareChange = False Then
                        CheckSave = False
                        MsgBox("�S���R�P�v��", 64, "����")
                        Exit Function
                    End If
                End If
            Next

        End If
        ''------------------------------------------------------------
        If Len(ButtonEdit2.EditValue) = 0 Then
            MsgBox("�������ର��", 64, "����")
            CheckSave = False
            Exit Function
        End If
        If Len(cbKnifeType.EditValue) = 0 Then
            MsgBox("�M���������ର��", 64, "����")
            CheckSave = False
            Exit Function
        End If
        If ds.Tables("KnifeWareOut").Rows.Count = 0 Then
            MsgBox("�п�ܪ���", 64, "����")
            CheckSave = False
            Exit Function
        End If

        Dim i As Integer


        For i = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1
            If ds.Tables("KnifeWareOut").Rows(i)("M_Code").ToString.Trim = "" Then
                MsgBox("���ƽs�X���ର��", 64, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("KnifeWareOut").Rows(i)("M_Name").ToString.Trim = "" Then
                MsgBox("���ƦW�٤��ର��", 64, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("KnifeWareOut").Rows(i)("M_Gauge").ToString.Trim = "" Then
                MsgBox("���ƳW�椣�ର��", 64, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("KnifeWareOut").Rows(i)("M_Unit").ToString.Trim = "" Then
                MsgBox("���Ƴ�줣�ର��", 64, "����")
                CheckSave = False
                Exit Function
            End If
            If ds.Tables("KnifeWareOut").Rows(i)("WO_Qty") = 0 Then
                MsgBox("�X�w�ƶq���ର0!", 64, "����")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                CheckSave = False
                Exit Function
            End If
            '--------------------------------------------------------�P�_�O�_���D�ɮw�s
            Dim wic As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            Dim wii As New LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            wii = wic.WareInventory_GetSub(ds.Tables("KnifeWareOut").Rows(i)("M_Code").ToString, strWHID)
            Dim Qty As Double = 0
            If wii Is Nothing Then
                MsgBox("��e�o�X�ܮw���s�b�����ƫH��", 64, "����")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                CheckSave = False
                Exit Function
            Else
                Qty = wii.WI_Qty
            End If

            If Qty < CDbl(ds.Tables("KnifeWareOut").Rows(i)("WO_Qty").ToString) Then
                MsgBox("�o�X�ܨS�����w�s�ήw�s����!", 64, "����")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                CheckSave = False
                Exit Function
            End If
            '--------------------------------------------------------�P�_�O�_���l�ɮw�s
            Dim WkOut As LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
            Dim WcOut As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
            WkOut = WcOut.KnifeWareInventorySub_GetList(ds.Tables("KnifeWareOut").Rows(i)("M_Code"), strWHID)
            If WkOut Is Nothing Then
                MsgBox("��e�o�X�ܮw�M���ݩʭܮw���s�b�����ƫH��", 64, "����")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                CheckSave = False
                Exit Function
            Else
                Select Case EditValue
                    Case "popWareOutAddNew" ''''�s�M
                        If WkOut.WI_SQty < CDbl(ds.Tables("KnifeWareOut").Rows(i)("WO_Qty").ToString) Then
                            MsgBox("�o�X���ݩʤM��S�����w�s�ήw�s����!", 64, "����")
                            Grid.Focus()
                            GridView1.FocusedRowHandle = i
                            CheckSave = False
                            Exit Function
                        End If
                    Case "popWareOutAddOld" '�ݳB�z
                        If WkOut.WI_SReQty < CDbl(ds.Tables("KnifeWareOut").Rows(i)("WO_Qty").ToString) Then
                            MsgBox("�o�X���ݩʤM��S�����w�s�ήw�s����!", 64, "����")
                            Grid.Focus()
                            GridView1.FocusedRowHandle = i
                            CheckSave = False
                            Exit Function
                        End If
                End Select
            End If
        Next
    End Function

    Sub SaveDataNew()
        Dim kwo As New KnifeWareOutController
        Dim kinfo As New KnifeWareOutInfo

        txtWIPID.EditValue = GetWO_ID()
        If Len(txtWIPID.EditValue) = 0 Then
            MsgBox("����ͦ��渹�A�L�k�O�s�I", 64, "����")
            Exit Sub
        End If

        kinfo.WO_ID = txtWIPID.EditValue
        kinfo.WO_Type = cbType.EditValue
        kinfo.OP_NO = txtOPNO.Text
        kinfo.WH_ID = strWHID
        kinfo.WO_AddDate = Format(Now, "yyyy/MM/dd")
        kinfo.WO_Action = InUserID
        kinfo.DPT_ID = strDPTID
        kinfo.KnifeType = cbKnifeType.Text
        kinfo.WO_PerID = IIf(Len(TextEdit1.EditValue) = 0, Nothing, TextEdit1.EditValue)
        '----------------------------------------------------
        kinfo.WO_Check = CheckEdit1.Checked
        kinfo.WO_CheckAction = InUserID
        kinfo.WO_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        kinfo.WO_CheckRemark = CheckRemark.Text
        '----------------------------------------------------
        Dim i As Integer
        With ds.Tables("KnifeWareOut")
            For i = 0 To .Rows.Count - 1
                Dim DouQty As Double = CDbl(.Rows(i)("WO_Qty"))
                kinfo.WO_NUM = GetWO_NUM()
                kinfo.M_Code = IIf(IsDBNull(.Rows(i)("M_Code")), Nothing, .Rows(i)("M_Code"))
                kinfo.OS_BatchID = IIf(IsDBNull(.Rows(i)("OS_BatchID")), Nothing, .Rows(i)("OS_BatchID"))
                kinfo.WO_Qty = CDbl(.Rows(i)("WO_Qty"))
                kinfo.WO_Remark = IIf(IsDBNull(.Rows(i)("WO_Remark")), Nothing, .Rows(i)("WO_Remark"))
                kinfo.AP_NO = IIf(IsDBNull(.Rows(i)("AP_NO")), Nothing, .Rows(i)("AP_NO"))
                kinfo.M_ID = IIf(IsDBNull(.Rows(i)("M_ID")), Nothing, .Rows(i)("M_ID"))
                kinfo.Temp_Code = IIf(IsDBNull(.Rows(i)("Temp_Code")), Nothing, .Rows(i)("Temp_Code"))
                If kwo.KnifeWareOut_Add(kinfo) = True Then
                    ''--------------------------------------------------------
                    Dim KwOut As New LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
                    Dim wi As New LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi = wc.WareInventory_GetSub(.Rows(i)("M_Code"), strWHID)
                    If wi Is Nothing Then
                        KwOut.WI_All = 0 - CDbl(.Rows(i)("WO_Qty"))
                    Else
                        KwOut.WI_All = wi.WI_Qty - CDbl(.Rows(i)("WO_Qty"))
                    End If
                    '---------------------�w�s�l��------------------------
                    Dim wiinfo As New LFERP.Library.KnifeWare.KnifeWareInventorySubInfo
                    Dim wcco As New LFERP.Library.KnifeWare.KnifeWareInventorySubControl
                    wiinfo = wcco.KnifeWareInventorySub_GetList(.Rows(i)("M_Code"), strWHID)
                    If wiinfo Is Nothing Then
                        Select Case EditValue
                            Case "popWareOutAddNew" ''''�s�M
                                KwOut.WI_SQty = (0 - DouQty)
                                KwOut.WI_SReQty = 0
                            Case "popWareOutAddOld" '�ݳB�z
                                KwOut.WI_SReQty = (0 - DouQty)
                                KwOut.WI_SQty = 0
                        End Select
                    Else
                        Select Case EditValue
                            Case "popWareOutAddNew" ''''�s�M
                                KwOut.WI_SQty = (wiinfo.WI_SQty - DouQty)
                                KwOut.WI_SReQty = wiinfo.WI_SReQty
                            Case "popWareOutAddOld" '�ݳB�z
                                KwOut.WI_SReQty = (wiinfo.WI_SReQty - DouQty)
                                KwOut.WI_SQty = wiinfo.WI_SQty
                        End Select
                    End If
                    KwOut.M_Code = .Rows(i)("M_Code")
                    KwOut.WH_ID = strWHID
                    If wcco.KnifeWareInventorySub_Update(KwOut) = False Then
                        MsgBox("�O�s����", MsgBoxStyle.Information, "����")
                        Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Function GetNO() As String
        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionField_GetNO(strA)
        If pi Is Nothing Then
            GetNO = "PF" & strA & "000001"
        Else
            GetNO = "PF" + strA + Mid((CInt(Mid(pi.FP_NO, 7)) + 1000001), 2)
        End If
    End Function

    Function GetNum() As String

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionField_GetNO(strA)

        If pi Is Nothing Then
            GetNum = strA & "000001"
        Else
            GetNum = strA + Mid((CInt(Mid(pi.FP_Num, 5)) + 1000001), 2)
        End If

    End Function

#Region "���ϥε{��"
    Sub SaveDataEdit()
        On Error Resume Next

        If Len(txtWH.EditValue) = 0 Then
            MsgBox("�п�ܭܮw")
            Exit Sub
        End If
        If Len(ButtonEdit2.EditValue) = 0 Then
            MsgBox("�п�ܳ���")
            Exit Sub
        End If

        If ds.Tables("WareOut").Rows.Count = 0 Then
            MsgBox("�п�ܪ���")
            Exit Sub
        End If

        If Len(txtWIPID.EditValue) = 0 Then
            MsgBox("�渹���šA�L�k�O�s�I")
            Exit Sub
        End If

        Dim i As Integer
        '�d�߬O�_�������ܮw������
        Dim mw As New WareInventory.WareInventoryMTController
        Dim mwi As New WareInventory.WareInventoryInfo
        For i = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1

            mwi = mw.WareInventory_GetSub(ds.Tables("KnifeWareOut").Rows(i)("M_Code"), strWHID)
            If mwi Is Nothing Then
                MsgBox("����" & ds.Tables("KnifeWareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�����s�b�A����O�s�I")
                Exit Sub
            End If

            If mwi.WI_Qty < CDbl(ds.Tables("KnifeWareOut").Rows(i)("WO_Qty")) Then
                MsgBox("����" & ds.Tables("KnifeWareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�������ơA����O�s�I")
                Exit Sub
            End If
        Next

        '��s�R�����O��
        If ds.Tables("DelDate").Rows.Count > 0 Then
            Dim ii As Integer
            For ii = 0 To ds.Tables("DelDate").Rows.Count - 1

                Dim kwo1 As New KnifeWareOutController
                If Not IsDBNull(ds.Tables("DelDate").Rows(ii)("WO_NUM")) Then
                    kwo1.KnifeWareOut_Delete(ds.Tables("DelDate").Rows(ii)("WO_NUM"), Nothing)
                End If
            Next ii
        End If

        Dim kwo As New KnifeWareOutController
        Dim kinfo As New KnifeWareOutInfo

        kinfo.WO_ID = txtWIPID.EditValue
        kinfo.WO_Type = cbType.EditValue
        'mi.AP_NO = txtAPNO.Text
        kinfo.OP_NO = txtOPNO.Text
        kinfo.WH_ID = strWHID
        kinfo.WO_AddDate = DateEdit1.EditValue
        kinfo.WO_Action = Label15.Text

        kinfo.DPT_ID = strDPTID
        kinfo.KnifeType = cbKnifeType.Text

        'mi.DPT_ID = ButtonEdit2.EditValue
        kinfo.WO_PerID = TextEdit1.EditValue
        'kinfo.WO_PerName = Label4.Text ��d��d ��d ��d

        '----------------------------------------------------  �ק�f�֫H��
        kinfo.WO_Check = CheckEdit1.Checked
        kinfo.WO_CheckAction = InUserID
        kinfo.WO_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        kinfo.WO_CheckRemark = CheckRemark.Text
        '----------------------------------------------------

        For i = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1
            If IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("WO_NUM")) Then   '�s�W
                kinfo.WO_NUM = GetWO_NUM()
                kinfo.M_Code = ds.Tables("KnifeWareOut").Rows(i)("M_Code")

                kinfo.OS_BatchID = ds.Tables("KnifeWareOut").Rows(i)("OS_BatchID")
                kinfo.WO_Qty = CDbl(ds.Tables("KnifeWareOut").Rows(i)("WO_Qty"))
                kinfo.WO_Remark = ds.Tables("KnifeWareOut").Rows(i)("WO_Remark")

                '---------�ӻ�渹���J�A�L ���šA�s�b�h���J�ȩΪ̤�u��J

                If IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("AP_NO")) Then

                    kinfo.AP_NO = Nothing
                Else
                    kinfo.AP_NO = ds.Tables("KnifeWareOut").Rows(i)("AP_NO")

                End If

                If IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("M_ID")) Then

                    kinfo.M_ID = Nothing
                Else
                    kinfo.M_ID = ds.Tables("KnifeWareOut").Rows(i)("M_ID")

                End If

                If IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("Temp_Code")) Then

                    kinfo.Temp_Code = Nothing
                Else
                    kinfo.Temp_Code = ds.Tables("KnifeWareOut").Rows(i)("Temp_Code")

                End If

                kwo.KnifeWareOut_Add(kinfo)
            ElseIf Not IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("WO_NUM")) Then ' �ק�
                kinfo.WO_NUM = ds.Tables("KnifeWareOut").Rows(i)("WO_NUM")
                kinfo.M_Code = ds.Tables("KnifeWareOut").Rows(i)("M_Code")

                kinfo.OS_BatchID = ds.Tables("KnifeWareOut").Rows(i)("OS_BatchID")
                kinfo.WO_Qty = CDbl(ds.Tables("KnifeWareOut").Rows(i)("WO_Qty"))
                kinfo.WO_Remark = ds.Tables("KnifeWareOut").Rows(i)("WO_Remark")
                kinfo.WO_EditDate = Format(Now, "yyyy/MM/dd")

                '---------�ӻ�渹���J�A�L ���šA�s�b�h���J�ȩΪ̤�u��J

                If IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("AP_NO")) Then

                    kinfo.AP_NO = Nothing
                Else
                    kinfo.AP_NO = ds.Tables("KnifeWareOut").Rows(i)("AP_NO")

                End If

                If IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("M_ID")) Then

                    kinfo.M_ID = Nothing
                Else
                    kinfo.M_ID = ds.Tables("KnifeWareOut").Rows(i)("M_ID")

                End If

                If IsDBNull(ds.Tables("KnifeWareOut").Rows(i)("Temp_Code")) Then

                    kinfo.Temp_Code = Nothing
                Else
                    kinfo.Temp_Code = ds.Tables("KnifeWareOut").Rows(i)("Temp_Code")

                End If
                kwo.KnifeWareOut_Update(kinfo)
            End If
        Next

        If oldcheck = True Then
            MsgBox("����w�f��!")
            Exit Sub
        Else
            '-------------------------------------------------------------------------
            If CheckEdit1.Checked = True Then

                Dim mt As New SharePurchaseController
                Dim mm As New SharePurchaseInfo

                mm.WH_ID = strWHID

                For i = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1
                    mm.M_Code = ds.Tables("KnifeWareOut").Rows(i)("M_Code")

                    Dim Qty As Double

                    Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi = wc.WareInventory_GetSub(ds.Tables("KnifeWareOut").Rows(i)("M_Code"), strWHID)

                    If wi Is Nothing Then
                        Qty = 0
                    Else
                        Qty = wi.WI_Qty
                    End If

                    mm.WI_Qty = Qty - CDbl(ds.Tables("KnifeWareOut").Rows(i)("WO_Qty"))
                    mt.UpdateWareInventory_WIQty2(mm)

                Next
                '--------------------------------------------------------------------�Ͳ��}�ưO���H��---�D�n�O���ܥͲ�����ƨϥΪ��p
                Dim pi As New ProductionMaterialInfo '����ƻ�ƼưO��
                Dim pc As New ProductionMaterialControl
                Dim pi1 As List(Of ProductionMaterialInfo)

                Dim pki As List(Of ProductionKaiLiaoInfo) '�}�ƫH��
                Dim pkc As New ProductionKaiLiaoControl

                If cbType.EditValue = "�Ͳ��}��" Then

                    Dim m As Integer
                    For m = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1

                        pki = pkc.ProductionKaiLiao_GetList(txtOPNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)

                        If pki.Count > 0 Then
                            Dim k As Integer
                            For k = 0 To pki.Count - 1

                                '�P�_��e�O�_�s�b�����Ͳ��}�ƫH���O��
                                pi1 = pc.ProductionMaterial_GetList(pki(k).Pro_Type, pki(k).PM_M_Code, pki(k).PM_Type, pki(k).M_Code)
                                Dim MaterialQty As Double '�w�q��l�Ƽƶq/���q�H��

                                If pi1.Count = 0 Then
                                    MaterialQty = 0
                                Else
                                    MaterialQty = pi1(0).M_Qty  '��l�H��
                                End If

                                pi.Pro_Type = pki(k).Pro_Type
                                pi.PM_M_Code = pki(k).PM_M_Code
                                pi.PM_Type = pki(k).PM_Type
                                pi.M_Code = pki(k).M_Code
                                pi.M_Qty = MaterialQty + CDbl(ds.Tables("KnifeWareOut").Rows(m)("WO_Qty"))

                                pc.UpdateProductionMaterialQty(pi)  '�O����e�����~�Ͳ��}�Ʈw�s�H�����p

                            Next

                        End If

                    Next

                End If

                '-----------------------------------------------------------------------------�ӻ�渹�O���H��
                Dim ti As New TransferModuleInfo
                Dim tc As New TransferModuleControl

                Dim ai As List(Of ApplyInfo)
                Dim ac As New ApplyControl
                Dim ai1 As New ApplyInfo

                Dim j As Integer
                For j = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1

                    If ds.Tables("KnifeWareOut").Rows(j)("M_ID") = "" Then
                    Else

                        ai = ac.Apply_GetList(ds.Tables("KnifeWareOut").Rows(j)("AP_NO"), ds.Tables("KnifeWareOut").Rows(j)("M_ID"), Nothing, Nothing)

                        If ai.Count > 0 Then

                            Dim SngA As Double

                            'Dim m As Integer
                            'For m = 0 To ai.Count - 1
                            SngA = ai(0).M_SendQty  '�Y�s�b--�h�u���@���O��
                            'Next
                            '-----------------------------�ܧ��e�ӻ�椤�o�X�ƶq�O��--------------------------------------------------
                            ai1.AM_ID = ds.Tables("KnifeWareOut").Rows(j)("AP_NO")
                            ai1.M_ID = ds.Tables("KnifeWareOut").Rows(j)("M_ID")
                            ai1.M_Code = ds.Tables("KnifeWareOut").Rows(j)("M_Code")
                            ai1.M_SendQty = SngA + CDbl(ds.Tables("KnifeWareOut").Rows(j)("WO_Qty"))

                            ac.Apply_UpdateQty(ai1)
                            '-------------------------------------------------------------------------------
                            '�ק����@ 2012-06-19  ��e�ϥηs���O����e���ƹ�ɮw�s�H��,
                            '���Ҷ���e�u��ӻ��A�o�f�ƶq�i��O��, �w�T�w��e�ӻ��O�_�w����
                            '-------------------------------------------------------------------------------
                            Dim SngB As Double

                            SngB = ai1.M_SendQty

                            ti.ModuleID = "�X�w�@�~"
                            ti.ModuleDetail = ds.Tables("KnifeWareOut").Rows(j)("AP_NO") & "," & ds.Tables("KnifeWareOut").Rows(j)("M_ID") & "," & ds.Tables("KnifeWareOut").Rows(j)("M_Code") & "," & SngB & "," & ds.Tables("KnifeWareOut").Rows(j)("Temp_Code") & "," & ds.Tables("KnifeWareOut").Rows(j)("WO_Remark")
                            ti.ModuleRemark = txtWIPID.Text & ds.Tables("KnifeWareOut").Rows(j)("AP_NO")

                            tc.TransferModule_Add(ti)

                        End If

                    End If

                Next
                '-----------------------------------------------------------------------------
            End If
        End If

    End Sub
#End Region
    Sub UpdateCheck()
        If oldcheck = CheckEdit1.Checked Then
            MsgBox("�f�֪��A�����ܡA�Ч�窱�A��A�O�s�K�K", 64, "����")
            Exit Sub
        End If

        Dim kwo As New KnifeWareOutController
        Dim kinfo As New KnifeWareOutInfo
        kinfo.WO_ID = txtWIPID.EditValue
        kinfo.WO_Check = CheckEdit1.Checked
        kinfo.WO_CheckAction = InUserID
        kinfo.WO_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        kinfo.WO_CheckRemark = CheckRemark.Text
        kinfo.KnifeType = cbKnifeType.Text
        If kwo.KnifeWareOut_UpdateCheck(kinfo) = False Then
            MsgBox("�����f�֥���,���ˬd��]!", 64, "����")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Sub UpdateReCheck()
        Dim kwo As New KnifeWareOutController
        Dim kinfo As New KnifeWareOutInfo

        kinfo.WO_ID = txtWIPID.EditValue
        kinfo.WO_ReCheck = CheckEdit2.Checked
        kinfo.WO_ReCheckAction = InUserID
        kinfo.WO_ReCheckDate = Format(Now, "yyyy/MM/dd")
        kinfo.WO_ReCheckRemark = txtrecheckremark.Text
        kinfo.KnifeType = cbKnifeType.Text
        If kwo.KnifeWareOut_UpdateReCheck(kinfo) = False Then
            MsgBox("�_�֥���,���ˬd��]!", 64, "����")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Sub Update_Remark()

        Dim kwo As New KnifeWareOutController
        Dim kinfo As New KnifeWareOutInfo

        kinfo.WO_ID = txtWIPID.EditValue

        Dim i As Integer

        For i = 0 To ds.Tables("KnifeWareOut").Rows.Count - 1
            kinfo.WO_NUM = ds.Tables("KnifeWareOut").Rows(i)("WO_NUM")
            kinfo.WO_Remark = ds.Tables("KnifeWareOut").Rows(i)("WO_Remark")

            If kwo.KnifeWareOut_UpdateRemark(kinfo) = False Then
                Exit Sub
            End If
        Next

        MsgBox("�O�s���\!", 64, "����")
        Me.Close()
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '�bLCD�p��ܫ̤W��ܫH��
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510211")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then isShowLCD = True
        End If
    End Sub

    Sub loadedit(ByVal WO_ID As String)
        ds.Tables("KnifeWareOut").Clear()

        Dim kwo As New LFERP.Library.KnifeWare.KnifeWareOutController
        Dim kwinfo As New List(Of LFERP.Library.KnifeWare.KnifeWareOutInfo)
        Dim i As Integer
        Dim row As DataRow
        Try
            kwinfo = kwo.KnifeWareOut_GetList5(WO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If kwinfo.Count <= 0 Then
                '�S���ƾ�
                Exit Sub
            End If
            'txtAPNO.Text = objInfo(0).AP_NO
            cbType.EditValue = kwinfo(0).WO_Type
            strWHID = kwinfo(0).WH_ID

            Dim wc As New WareHouseController
            Dim wiL As New List(Of WareHouseInfo)
            wiL = wc.WareHouse_Get(strWHID)

            If wiL.Item(0).NeedCheck = False Then
                TextEdit1.Enabled = True
                ButtonEdit2.Enabled = True
                sk.Visible = False
            Else
                TextEdit1.Enabled = False
                ButtonEdit2.Enabled = False
            End If

            Label15.Text = kwinfo(0).WO_Action
            txtWH.EditValue = kwinfo(0).WH_Name
            DateEdit1.EditValue = Format(kwinfo(0).WO_AddDate, "yyyy/MM/dd")
            strDPTID = kwinfo(0).DPT_ID
            ButtonEdit2.EditValue = kwinfo(0).DPT_Name
            TextEdit1.EditValue = kwinfo(0).WO_PerID
            'Label4.Text = kwinfo(0).WO_PerName��d��d��d
            CheckEdit1.Checked = kwinfo(0).WO_Check
            CheckDate.Text = kwinfo(0).WO_CheckDate
            CheckAction.Text = kwinfo(0).WO_CheckActionName
            CheckRemark.Text = kwinfo(0).WO_CheckRemark
            cbKnifeType.Text = kwinfo(0).KnifeType
            CheckEdit2.Checked = kwinfo(0).WO_ReCheck
            txtrecheckdate.Text = kwinfo(0).WO_ReCheckDate
            txtrecheckaction.Text = kwinfo(0).WO_ReCheckAction
            txtrecheckremark.Text = kwinfo(0).WO_ReCheckRemark
            oldcheck = kwinfo(0).WO_Check
            For i = 0 To kwinfo.Count - 1
                row = ds.Tables("KnifeWareOut").NewRow
                row("WO_NUM") = kwinfo(i).WO_NUM
                row("WO_ID") = kwinfo(i).WO_ID
                row("M_Code") = kwinfo(i).M_Code
                row("M_Name") = kwinfo(i).M_Name
                row("M_Gauge") = kwinfo(i).M_Gauge
                row("M_Unit") = kwinfo(i).M_Unit
                row("WO_Qty") = kwinfo(i).WO_Qty
                row("OS_BatchID") = kwinfo(i).OS_BatchID
                row("WO_Remark") = kwinfo(i).WO_Remark
                row("AP_NO") = kwinfo(i).AP_NO   '�ӻ�渹
                row("M_ID") = kwinfo(i).M_ID
                row("Temp_Code") = kwinfo(i).Temp_Code
                row("KnifeType") = kwinfo(i).KnifeType

                

                ''Ū���w���w�s
                Dim wi2 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc2 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi2 = wc2.WareInventory_GetSub(kwinfo(i).M_Code, strWHID)
                row("WI_SafeQty_Show") = IIf(IsDBNull(wi2), 0, wi2.WI_SafeQty)
                '��--------------------------------------------------------------------
                ds.Tables("KnifeWareOut").Rows.Add(row)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub SetObjectEnable(ByVal a As Boolean, ByVal b As Boolean)
        txtWH.Enabled = a
        TextEdit1.Enabled = a
        DateEdit1.Enabled = a
        CheckEdit1.Enabled = b
        CheckRemark.Enabled = b
        ButtonEdit2.Enabled = a
        txtOPNO.Enabled = a
        cbKnifeType.Enabled = a
        cbType.Enabled = a
    End Sub

    Private Sub ButtonEdit2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEdit2.Click
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value <> "" Then
                frmDepartmentSelect1.DptID = ""
                frmDepartmentSelect1.DptName = ""
                tempValue = pmwiL.Item(0).PMWS_Value

                frmDepartmentSelect1.ShowDialog()
                If frmDepartmentSelect1.DptID = "" Then
                Else
                    ButtonEdit2.Text = frmDepartmentSelect1.DptName
                    strDPTID = frmDepartmentSelect1.DptID
                End If

                Exit Sub

            End If
        End If

        frmDepartmentSelect.DptID = ""
        frmDepartmentSelect.DptName = ""
        frmDepartmentSelect.ShowDialog()

        If frmDepartmentSelect.DptID = "" Then
        Else
            ButtonEdit2.Text = frmDepartmentSelect.DptName
            strDPTID = frmDepartmentSelect.DptID
            'ButtonEdit2.Tag = frmDepartmentSelect.DptID
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If ds.Tables("KnifeWareOut").Rows.Count = 0 Then Exit Sub


        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "M_Code")

        If DelTemp = "M_Code" Then
        Else
            Dim row As DataRow = ds.Tables("DelDate").NewRow
            row("WO_NUM") = ds.Tables("KnifeWareOut").Rows(GridView1.FocusedRowHandle)("WO_NUM")
            row("WO_ID") = ds.Tables("KnifeWareOut").Rows(GridView1.FocusedRowHandle)("WO_ID")
            row("M_Code") = DelTemp
            ds.Tables("DelDate").Rows.Add(row)
        End If
        ds.Tables("KnifeWareOut").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim DsA As New DataSet
        If GridView1.RowCount = 0 Then Exit Sub
        Dim ltc, ltc1, ltc2, ltc3, ltc4, ltc5 As New CollectionToDataSet

        Dim pmc As New KnifeWareOutController
        Dim uc As New UnitController
        Dim suc As New SystemUser.SystemUserController
        Dim wh As New WareHouseController

        Dim pmc2 As New WhiteUserListController
        Dim uc2 As New DepartmentControler

        DsA.Tables.Clear()
        Dim strA, strB As String
        strA = txtWIPID.Text
        strB = TextEdit1.Text

        Dim wc As New WareHouseController
        Dim wiL As New List(Of WareHouseInfo)
        wiL = wc.WareHouse_Get(strWHID)

        ltc.CollToDataSet(DsA, "KnifeWareOut", pmc.KnifeWareOut_GetList5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc1.CollToDataSet(DsA, "Unit", uc.GetUnitList(Nothing))
        ltc2.CollToDataSet(DsA, "SystemUser", suc.SystemUser_GetList(Nothing, Nothing, Nothing))
        ltc3.CollToDataSet(DsA, "WareHouse", wh.WareHouse_GetList(Nothing))
        ltc4.CollToDataSet(DsA, "WhiteUserList", pmc2.WhiteUserList_GetList(strB, Nothing))
        ltc5.CollToDataSet(DsA, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))

        PreviewRPT(DsA, "rptKnifeWareOut0", "�X�w��", True, True)

        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
        ltc5 = Nothing
    End Sub

    Private Sub sk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sk.Click
        TextEdit1.Text = ReadCard1()
        Dim wulc As New WhiteUserListController
        Dim wuliL As New List(Of WhiteuserListInfo)
        wuliL = wulc.WhiteUserListSub_GetList(TextEdit1.Text, strWHID)
        If wuliL.Count <= 0 Or TextEdit1.Text = "" Then
            MsgBox("�D�k�d��", 64, "����")
            Exit Sub
        Else
            ButtonEdit2.Text = wuliL.Item(0).DPT_Name
            strDPTID = wuliL.Item(0).DPT_ID
        End If
    End Sub
End Class