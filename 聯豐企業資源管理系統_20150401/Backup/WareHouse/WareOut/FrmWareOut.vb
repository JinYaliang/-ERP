Imports LFERP.Library.WareHouse
Imports LFERP.Library.WareHouse.WareOut
Imports LFERP.Library
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.Shared
Imports LFERP.FileManager
Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.ProductionWareOutMain
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




Public Class frmWareOut
    Dim ds As New DataSet
    Dim oldcheck As Boolean
    Dim strWHID As String
    Dim strDPTID As String
    Public isProcess As Boolean '�P�_�O�_�O��o�u��
    Public isBarCode As Boolean '�P�_�O�_�O���X���y
    Dim strPM_M_Code As String
    Dim isShowLCD As Boolean = False  '�O�_�bLCD�p�̹��W��ܥX�w�H��

    Public strPdMachine As Boolean = False


    Dim MsgBZ As String = ""
    Dim LoadM_Code As String = ""

    Dim AllPlus As Boolean = False
    Dim AllowChageQty As Boolean = False  ''�i�ܧ�}�Ƽ�

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frmWareOut_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If My.Settings.strCOM = "" Or isShowLCD = False Then Exit Sub
        '@ 2013/3/13 �K�[
        ApiDisplay.display_wellcome()   'LCD��ܫ̫�_����l�ɭ�
        ApiDisplay.com_exit() '�_�}COM�s��
        isOpenCOM = False   'COM���}�аO��False
    End Sub

    '@ 2012/8/28 �K�[ �v������
    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '�bLCD�p��ܫ̤W��ܫH��
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500211")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then isShowLCD = True
        End If

        '��ܡ��ɤJ�ӻ�桨�k����
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500212")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                popApplyAdd.Visible = True
            Else
                popApplyAdd.Visible = False
            End If
        Else
            popApplyAdd.Visible = True
        End If

        ''---------��q�s�W2013-5-11---------------------------------------------------------------
        ToolStripBatchLoad.Visible = False
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500214")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripBatchLoad.Visible = True
        End If
        ''------------------------------------------------------------------------
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500217") '�[
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then AllPlus = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500218") '�[
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then AllowChageQty = True
        End If


    End Sub

    Private Sub frmWareOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mc As New Purchase.SharePurchase.SharePurchaseController

        '@ 2012/7/27 �K�[�@�[�����Ƴ���
        Dim pc As New PersonnelControl
        gluDepID.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        gluDepID.Properties.ValueMember = "DepID"
        gluDepID.Properties.DisplayMember = "DepName"

        LoadUserPower()

        LabelMsg.Text = ""
        LoadM_Code = tempValue5


        'If Mid(strInDPT_ID, 1, 4) = "1001" Then
        '    popApplyAdd.Visible = False
        'End If

        If isBarCode = True Then
            Label24.Visible = True
            txtM_Code.Visible = True
            LabelControl1.Visible = True
            popWareOutBarAdd.Visible = False
            txtM_Code.Select()

            If strPdMachine = True Then
                LabelControl1.Text = "(�L�I���Ҧ�)"
            End If

        End If

        Label3.Text = tempValue
        txtWIPID.EditValue = tempValue2
        tempValue = ""
        tempValue2 = ""

        CreateTables()

        ''���m�s��d��
        'Dim reset As New ResetPassWords.SetPassWords
        'reset.SetPassWords()
        'txtWIPID.Enabled = False

        If isProcess = True Then
            PS_Name.Visible = True
            gluDepID.Visible = True
            Label23.Visible = True
        End If

        '------------------------------------
        Select Case Label3.Text
            Case "�X�w��"
                If Edit = True Then
                    Me.Text = "�X�w��ק�"
                    loadedit(txtWIPID.EditValue)
                    'txtWH.Enabled = False
                    'ButtonEdit2.Enabled = False
                    'TextEdit1.Enabled = False
                    'DateEdit1.Enabled = False
                    txtWIPID.Enabled = False
                    'getenable(False, False)
                    DateEdit1.Enabled = False
                ElseIf Edit = False Then

                    WO_EndQty_Show.Caption = "��e�w�s"

                    Me.Text = "�X�w��s�W"
                    txtWIPID.EditValue = ""
                    DateEdit1.DateTime = Now
                    'getenable(True, False)
                    cbType.EditValue = "���`"
                    DateEdit1.Enabled = False

                    CheckEdit1.Checked = True  '�q�{���f�֪��A

                    ''---------------------------------------------------------------------------��d�P�_
                    strWHID = tempValue3
                    txtWH.EditValue = tempValue4
                    tempValue3 = ""
                    tempValue4 = ""

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
                    ''--------------------------------------
                    If LoadM_Code <> "" Then
                        AddRow(LoadM_Code, 0)
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
            Case "�f��"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                getenable(False, True)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False


            Case "�d��"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                getenable(False, False)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                cmdSave.Enabled = False
            Case "�_��"
                loadedit(txtWIPID.EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage3
                getenable(False, False)
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

            Case "�ק�ƪ`" '2013-3-5 �ק�ƪ`
                Me.Text = "�X�w��-�ק�ƪ`"
                loadedit(txtWIPID.EditValue)
                XtraTabPage1.PageVisible = True
                XtraTabPage2.PageVisible = False
                XtraTabPage3.PageVisible = False
                XtraTabPage4.PageVisible = False
                getenable(False, False)
                AP_NO.OptionsColumn.AllowEdit = False
                ButtonEdit2.Enabled = False
                cbType.Enabled = False
                txtOPNO.Enabled = False
                WO_Qty.OptionsColumn.AllowEdit = False

        End Select
        '�[����������
        GridFile.AutoGenerateColumns = False
        GridFile.RowHeadersWidth = 15
        Dim dt As New FileController
        GridFile.DataSource = dt.FileBond_GetList("5002", txtWIPID.EditValue, Nothing)
        GridFile.Refresh()

        XtraTabPage2.PageVisible = False

        '@ 2013/3/9 �K�[   �ܮw�����~�ܮɤ���ܥ��ʳ浥�H��
        If strWHID = "W1101" Then
            popApplyAdd.Visible = False
            WI_SafeQty_Show.Visible = False
            AP_NO.Visible = False
            M_SendQty.Visible = False
        End If


        If sk.Visible = False Then  ''��d���s ���i�� �N�����m�s 2013-4-9
        Else
            '���m�s��d��
            Dim reset As New ResetPassWords.SetPassWords
            reset.SetPassWords()
            txtWIPID.Enabled = False
        End If

       

    End Sub

    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("WareOut")

            .Columns.Add("WO_NUM", GetType(String))
            .Columns.Add("WO_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("WO_Qty", GetType(Double))
            .Columns.Add("OS_BatchID", GetType(String))
            .Columns.Add("WO_Remark", GetType(String))
            .Columns.Add("AP_NO", GetType(String))    '�ӻ�渹---���J��  2012/8/28
            .Columns.Add("M_ID", GetType(String))
            .Columns.Add("Temp_Code", GetType(String))
            .Columns.Add("M_SendQty", GetType(Double))         '@ 2012/8/29 �K�[
            .Columns.Add("C_Qty", GetType(Double))         '@ 2012/10/9 �K�[
            .Columns.Add("PM_M_Code", GetType(String))    '@ 2013/4/8 �K�[

            '2012-7-16
            .Columns.Add("WO_EndQty_Show", GetType(Double))
            .Columns.Add("WI_SafeQty_Show", GetType(Double))

            '@ 2012/7/27 
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With

        With ds.Tables.Add("DelDate")
            .Columns.Add("WO_NUM", GetType(String))
            .Columns.Add("WO_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
        End With

        Grid.DataSource = ds.Tables("WareOut")
    End Sub

    Sub AddRow(ByVal strCode As String, ByVal strQty As Double)

        Dim row As DataRow

        row = ds.Tables("WareOut").NewRow

        If strCode = "" Then

        Else

            Dim j As Integer

            For j = 0 To ds.Tables("WareOut").Rows.Count - 1
                If strCode = ds.Tables("WareOut").Rows(j)("M_Code") Then

                    If isBarCode = True Then
                        ds.Tables("WareOut").Rows(j)("WO_Qty") = ds.Tables("WareOut").Rows(j)("WO_Qty") + strQty

                        '@ 2013/3/13 �K�[ �ƶq���ܮɪ��ƫH���bLCD��ܫ̤W���s���
                        If isShowLCD = True Then
                            LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(j)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(j)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(j)("WO_Qty") & ds.Tables("WareOut").Rows(j)("M_Unit"), "")
                        End If

                        GridView1.FocusedRowHandle = j
                        Exit Sub
                    Else
                        If MsgBZ = "" Then
                            MsgBZ = "Y"
                            MsgBox("���Ƥw�s�b�A�P�@�i�X�w�椤�����\�s�b�ۦP���X�w���ơI")
                        End If

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
            If objInfo.M_IsEnabled = False Then
                MsgBox("��e���Ƥ��i�ΡA�нT�{�����ƫH���I", 64, "����")
                Exit Sub
            End If

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

            '2013-11-01
            If GetWH_Remark(strWHID) = "NG" Then
                row("M_Unit") = "KG"
            End If

            'row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge

            row("WO_Qty") = strQty
            row("AP_NO") = ""  '�ӻ�渹---���q�K�[���A�b�D�������
            row("M_ID") = ""  '����  ---�w��s�b�ӻ�渹
            row("Temp_Code") = "" '�{�ɪ��ƽs�X  --�w��s�b�ӻ�渹
            row("PS_NO") = ""


            '2012-7-16 �[�J��ܦw���w�s�P�`�E��-----------------------------
            If strWHID = "W1101" Then    '@ 203/3/9 �ק� ���ܪ��ܮw�O���~�ܮɡA��ܪ��O���~�w�s��
                Dim pic As New ProductInventoryController
                Dim piiGet As List(Of ProductInventoryInfo)
                piiGet = pic.ProductInventory_GetList(strPM_M_Code, strCode, strWHID, Nothing)

                If piiGet.Count <= 0 Then
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = 0
                Else
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = piiGet(0).PI_Qty
                End If
            Else
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(strCode, strWHID)

                If wi1 Is Nothing Then
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = 0
                Else
                    row("WI_SafeQty_Show") = wi1.WI_SafeQty
                    row("WO_EndQty_Show") = wi1.WI_Qty
                End If

                ''�w��i�M������W1609���˭ܥ����X�w---------------------
                If strWHID = "W1609" Or strWHID = "w1609" Then
                    row("WO_Qty") = wi1.WI_Qty
                End If


            End If






                ''------------------------------------------------------

                ds.Tables("WareOut").Rows.Add(row)

                '@ 2013/3/13 �K�[ �s�W�����ƫH���bLCD��ܫ̤W���
                If isShowLCD = True Then   '������v���ɤ~�i���
                    LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("WO_Qty") & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("M_Unit"), "")
                End If

        End If
        GridView1.MoveLast()
    End Sub

    Function GetWH_Remark(ByVal strWH_ID As String) As String
        GetWH_Remark = ""
        Dim mt As New WareHouseController
        Dim mtl As New List(Of WareHouseInfo)
        mtl = mt.WareHouse_Get(strWH_ID)

        If mtl.Count > 0 Then
            GetWH_Remark = mtl(0).WH_Remark
        End If
    End Function

    Function AddRowPD(ByVal strCode As String, ByVal strQty As Double) As String


        AddRowPD = ""

        Dim row As DataRow

        row = ds.Tables("WareOut").NewRow

        If strCode = "" Then

        Else

            Dim j As Integer

            For j = 0 To ds.Tables("WareOut").Rows.Count - 1
                If strCode = ds.Tables("WareOut").Rows(j)("M_Code") Then

                    If isBarCode = True Then
                        ds.Tables("WareOut").Rows(j)("WO_Qty") = ds.Tables("WareOut").Rows(j)("WO_Qty") + strQty

                        '@ 2013/3/13 �K�[ �ƶq���ܮɪ��ƫH���bLCD��ܫ̤W���s���
                        If isShowLCD = True Then
                            LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(j)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(j)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(j)("WO_Qty") & ds.Tables("WareOut").Rows(j)("M_Unit"), "")
                        End If

                        GridView1.FocusedRowHandle = j
                        Exit Function
                    Else
                        If MsgBZ = "" Then
                            MsgBZ = "Y"
                            'MsgBox("���Ƥw�s�b�A�P�@�i�X�w�椤�����\�s�b�ۦP���X�w���ơI")
                            AddRowPD = AddRowPD & strCode & "���Ƥw�s�b�A�P�@�i�X�w�椤�����\�s�b�ۦP���X�w���ơI" + vbCrLf
                        End If

                        Exit Function
                    End If

                End If
            Next

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            If objInfo Is Nothing Then
                'MsgBox("��e���Ƥ��s�b�A�нT�{�����ƫH���I", 64, "����")
                AddRowPD = AddRowPD & strCode & "��e���Ƥ��s�b�A�нT�{�����ƫH���I" + vbCrLf
                Exit Function
            End If
            If objInfo.M_IsEnabled = False Then
                AddRowPD = AddRowPD & strCode & "��e���Ƥ��i�ΡA�нT�{�����ƫH���I" + vbCrLf
                ' MsgBox("��e���Ƥ��i�ΡA�нT�{�����ƫH���I", 64, "����")
                Exit Function
            End If

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

            'row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge

            row("WO_Qty") = strQty
            row("AP_NO") = ""  '�ӻ�渹---���q�K�[���A�b�D�������
            row("M_ID") = ""  '����  ---�w��s�b�ӻ�渹
            row("Temp_Code") = "" '�{�ɪ��ƽs�X  --�w��s�b�ӻ�渹
            row("PS_NO") = ""


            '2012-7-16 �[�J��ܦw���w�s�P�`�E��-----------------------------
            If strWHID = "W1101" Then    '@ 203/3/9 �ק� ���ܪ��ܮw�O���~�ܮɡA��ܪ��O���~�w�s��
                Dim pic As New ProductInventoryController
                Dim piiGet As List(Of ProductInventoryInfo)
                piiGet = pic.ProductInventory_GetList(strPM_M_Code, strCode, strWHID, Nothing)

                If piiGet.Count <= 0 Then
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = 0
                Else
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = piiGet(0).PI_Qty
                End If
            Else
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(strCode, strWHID)

                If wi1 Is Nothing Then
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = 0
                Else
                    row("WI_SafeQty_Show") = wi1.WI_SafeQty
                    row("WO_EndQty_Show") = wi1.WI_Qty
                End If
            End If
            ''------------------------------------------------------

            ds.Tables("WareOut").Rows.Add(row)

            '@ 2013/3/13 �K�[ �s�W�����ƫH���bLCD��ܫ̤W���
            If isShowLCD = True Then   '������v���ɤ~�i���
                LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("WO_Qty") & ds.Tables("WareOut").Rows(GridView1.RowCount - 1)("M_Unit"), "")
            End If

        End If
        GridView1.MoveLast()




    End Function


    Sub getenable(ByVal a As Boolean, ByVal b As Boolean)

        txtWH.Enabled = a
        TextEdit1.Enabled = a
        DateEdit1.Enabled = a
        CheckEdit1.Enabled = b
        CheckRemark.Enabled = b
    End Sub

    Sub loadedit(ByVal WO_ID As String)
        ds.Tables("WareOut").Clear()

        Dim objInfo As List(Of WareOutInfo)
        Dim pc As New WareOutController
        Dim i As Integer
        Dim row As DataRow
        Try
            objInfo = pc.WareOut_Getlist5(WO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If objInfo Is Nothing Then
                '�S���ƾ�
                Exit Sub
            End If
            'txtAPNO.Text = objInfo(0).AP_NO
            cbType.EditValue = objInfo(0).WO_Type
            strWHID = objInfo(0).WH_ID

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

            Label15.Text = objInfo(0).WO_Action
            txtWH.EditValue = objInfo(0).WH_Name
            DateEdit1.EditValue = Format(objInfo(0).WO_AddDate, "yyyy/MM/dd")

            strDPTID = objInfo(0).DPT_ID
            ButtonEdit2.EditValue = objInfo(0).DPT_Name

            TextEdit1.EditValue = objInfo(0).WO_PerID
            Label4.Text = objInfo(0).WO_PerName
            CheckEdit1.Checked = objInfo(0).WO_Check
            CheckDate.Text = objInfo(0).WO_CheckDate
            CheckAction.Text = objInfo(0).WO_CheckActionName
            CheckRemark.Text = objInfo(0).WO_CheckRemark

            CheckEdit2.Checked = objInfo(0).WO_ReCheck
            txtrecheckdate.Text = objInfo(0).WO_ReCheckDate
            txtrecheckaction.Text = objInfo(0).WO_ReCheckAction
            txtrecheckremark.Text = objInfo(0).WO_ReCheckRemark

            oldcheck = objInfo(0).WO_Check

            For i = 0 To objInfo.Count - 1
                row = ds.Tables("WareOut").NewRow
                row("WO_NUM") = objInfo(i).WO_NUM
                row("WO_ID") = objInfo(i).WO_ID
                row("M_Code") = objInfo(i).M_Code
                row("M_Name") = objInfo(i).M_Name
                row("M_Gauge") = objInfo(i).M_Gauge
                row("M_Unit") = objInfo(i).M_Unit
                row("WO_Qty") = objInfo(i).WO_Qty
                row("OS_BatchID") = objInfo(i).OS_BatchID
                row("WO_Remark") = objInfo(i).WO_Remark
                row("AP_NO") = objInfo(i).AP_NO   '�ӻ�渹
                row("M_ID") = objInfo(i).M_ID
                row("Temp_Code") = objInfo(i).Temp_Code

                Dim api As List(Of ApplyInfo)
                Dim apc As New ApplyControl
                api = apc.Apply_GetList(objInfo(i).AP_NO, objInfo(i).M_ID, Nothing, Nothing)
                If api.Count > 0 Then
                    row("M_SendQty") = api(0).M_SendQty
                Else
                    row("M_SendQty") = objInfo(i).WO_Qty
                End If

                '@ 2012/10/9 �K�[ ��ܶ}�Ƽƶq
                If objInfo(0).WO_Type = "�Ͳ��}��" Then
                    txtOPNO.Text = objInfo(0).OP_NO

                    Dim pli As List(Of ProductionKaiLiaoInfo)
                    Dim plc As New ProductionKaiLiaoControl

                    pli = plc.ProductionKaiLiao_GetList(txtOPNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
                    If pli.Count > 0 Then
                        C_Qty.VisibleIndex = 6
                        C_Qty.Visible = True
                        row("C_Qty") = pli(0).C_Weight
                    End If
                End If

                '2012-7-16Ū���`�E��  �b�f�� �O�s���A-----------------------------
                row("WO_EndQty_Show") = objInfo(i).WO_EndQty
                ''Ū���w���w�s
                Dim wi2 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc2 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi2 = wc2.WareInventory_GetSub(objInfo(i).M_Code, strWHID)


                If wi2 Is Nothing Then
                    row("WI_SafeQty_Show") = 0
                Else
                    row("WI_SafeQty_Show") = wi2.WI_SafeQty
                End If
                '��--------------------------------------------------------------------


                ds.Tables("WareOut").Rows.Add(row)
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function GetWO_ID() As String
        '�ͦ��spm
        Dim pm As New WareOutController
        Dim pi As New WareOutInfo
        Dim ndate As String
        ndate = "WO" + Format(Now(), "yyMM")
        pi = pm.WareOut_GetID(ndate)
        If pi Is Nothing Then
            GetWO_ID = ndate + "00001"
        Else
            GetWO_ID = ndate + Mid((CInt(Mid(pi.WO_ID, 7)) + 100001), 2)
        End If



    End Function

    Function GetWO_NUM() As String
        '�ͦ��spS
        Dim pm As New WareOutController
        Dim pi As New WareOutInfo
        pi = pm.WareOut_GetNUM(Nothing)
        If pi Is Nothing Then
            GetWO_NUM = "O000000001"
        Else
            GetWO_NUM = "O" & Mid((CInt(Mid(pi.WO_NUM, 2)) + 1000000001), 2)
        End If

    End Function

    Private Sub popWareOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutAdd.Click
        tempCode = ""
        tempValue5 = strWHID
        tempValue6 = "�ܮw�޲z"
        tempValue7 = "�X�w�@�~"
        frmBOMSelect.ShowDialog()

        'AP_NO.OptionsColumn.AllowEdit = True
        'AP_NO.OptionsColumn.ReadOnly = True

        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            '�W�[�O��
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode, 0)
            End If
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then
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
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then
            Dim i, n As Integer
            Dim arr(n) As String
            strPM_M_Code = tempValue3
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
        tempValue3 = ""
    End Sub

    Private Sub popWareOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutDel.Click
        If ds.Tables("WareOut").Rows.Count = 0 Then Exit Sub


        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "M_Code")

        If DelTemp = "M_Code" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelDate").NewRow
            'row("M_CodeSub") = CodeSubData.Tables("CodeSub").Rows(GridView1.FocusedRowHandle)("M_CodeSub")
            row("WO_NUM") = ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_NUM")
            row("WO_ID") = ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_ID")
            row("M_Code") = DelTemp
            ds.Tables("DelDate").Rows.Add(row)
        End If
        ds.Tables("WareOut").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label3.Text
            Case "�X�w��"
                If Edit = False Then
                    If CheckSave() = True Then
                        SaveDataNew()
                        cmdSave.Enabled = False
                        cmdPrint.Visible = True
                    End If
                ElseIf Edit = True Then
                    SaveDataEdit()
                    cmdSave.Enabled = False
                    cmdPrint.Visible = True
                End If
            Case "�f��"
                UpdateCheck()
            Case "�_��"
                UpdateReCheck()

            Case "�ק�ƪ`"

                Update_Remark()
        End Select
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

    Sub SaveDataNew()
     

        Dim mc As New WareOutController
        Dim mi As New WareOutInfo

        Dim i, l As Integer

        txtWIPID.EditValue = GetWO_ID()

        If Len(txtWIPID.EditValue) = 0 Then
            MsgBox("����ͦ��渹�A�L�k�O�s�I")
            Exit Sub
        End If

        '@ 2012/7/27 �K�[
        '-------------------------------------------------------------------------���Ʀ��o�K�[
        If isProcess = True Then
            Dim pfi As New ProductionFieldInfo
            Dim pfc As New ProductionFieldControl

            Dim pic As New ProcessMainControl
            Dim pci As List(Of ProcessMainInfo)

            Dim pdi As List(Of ProductionDPTWareInventoryInfo)
            Dim pdc As New ProductionDPTWareInventoryControl

            Dim pai As New ProductionAffairInfo
            Dim pac As New ProductionAffairControl

            Dim pdsi As List(Of ProductionFieldDaySummaryInfo)
            Dim pdsc As New ProductionFieldDaySummaryControl

            For l = 0 To ds.Tables("WareOut").Rows.Count - 1
                pci = pic.ProcessSub_GetList(Nothing, ds.Tables("WareOut").Rows(l)("PS_NO").ToString, Nothing, Nothing, Nothing, Nothing)

                If pci.Count = 0 Then Exit Sub

                Dim AllWeight, strWeight, strG As Double

                strWeight = pci(0).PS_Weight  '�J/��  �歫
                strG = strWeight * ds.Tables("WareOut").Rows(l)("WO_Qty").ToString
                AllWeight = strG / 1000  '��e�ƶq�����q(KG)

                pdi = pdc.ProductionDPTWareInventory_GetList(gluDepID.EditValue, ds.Tables("WareOut").Rows(l)("PS_NO").ToString, Nothing)

                pfi.FP_NO = GetNO()
                pfi.FP_Num = GetNum()
                pfi.Pro_Type = pci(0).Pro_Type
                pfi.PM_M_Code = pci(0).PM_M_Code
                pfi.PM_Type = pci(0).Type3ID

                pfi.Pro_NO = ds.Tables("WareOut").Rows(l)("PS_NO").ToString
                pfi.FP_OutDep = gluDepID.EditValue
                pfi.FP_InDep = strWHID
                pfi.FP_Qty = ds.Tables("WareOut").Rows(l)("WO_Qty").ToString
                pfi.FP_Weight = Format(AllWeight, "0.00") '(��Ƭ����p��)

                pfi.FP_Date = Format(Now, "yyyy/MM/dd HH:mm:ss")
                pfi.FP_Detail = "PT03"
                pfi.FP_OutAction = InUserID
                pfi.FP_Remark = txtWIPID.EditValue
                pfi.IW_NO = ""
                pfi.CardID = TextEdit1.Text


                pfi.FP_Check = True
                pfi.FP_CheckAction = InUserID
                pfi.FP_CheckRemark = "�ܮw������o�u��"

                If pfc.ProductionField_InAdd(pfi) = True Then
                    pfc.ProductionField_UpdateCheck(pfi)

                    pai.FP_NO = pfi.FP_NO
                    pai.Pro_Type = pci(0).Pro_Type
                    pai.PM_M_Code = pci(0).PM_M_Code
                    pai.PM_Type = pci(0).Type3ID
                    pai.Pro_NO = ds.Tables("WareOut").Rows(l)("PS_NO").ToString

                    pai.FP_OutDep = gluDepID.EditValue
                    'pai.FP_InDep = gluDepID.EditValue
                    pai.FP_Detail = "PT03"
                    pai.FP_Type = "����"
                    pai.FP_InAction = InUserID

                    pai.FP_InCheck = True
                    pai.FP_InCheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    pai.CardID = TextEdit1.Text

                    pdsi = pdsc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, ds.Tables("WareOut").Rows(l)("PS_NO").ToString, gluDepID.EditValue, Nothing, DateEdit1.Text, DateEdit1.Text)

                    If pdsi.Count <= 0 Then
                        pai.ShouLiao = ds.Tables("WareOut").Rows(l)("WO_Qty").ToString
                    Else
                        pai.ShouLiao = pdsi(0).ShouLiao + ds.Tables("WareOut").Rows(l)("WO_Qty").ToString
                        pai.JiaCun = pdsi(0).JiaCun
                        pai.QuCun = pdsi(0).QuCun
                        pai.FaLiao = pdsi(0).FaLiao
                        pai.CunHuo = pdsi(0).CunHuo

                        pai.FanXiuIn = pdsi(0).FanXiuIn
                        pai.FanXiuOut = pdsi(0).FanXiuOut
                        pai.LiuBan = pdsi(0).LiuBan
                        pai.SunHuai = pdsi(0).SunHuai
                        pai.DiuShi = pdsi(0).DiuShi

                        pai.BuNiang = pdsi(0).BuNiang
                        pai.CunCang = pdsi(0).CunCang
                        pai.QuCang = pdsi(0).QuCang
                        pai.ChuHuo = pdsi(0).ChuHuo
                        pai.WaiFaIn = pdsi(0).WaiFaIn

                        pai.WaiFaOut = pdsi(0).WaiFaOut
                        pai.AccIn = pdsi(0).AccIn
                        pai.AccOut = pdsi(0).AccOut
                        pai.RePairOut = pdsi(0).RePairOut
                        pai.ZuheOut = pdsi(0).ZuheOut
                    End If

                    pai.PM_Date = Format(Now, "yyyy/MM/dd")

                    pai.WI_Qty = pdi(0).WI_Qty + ds.Tables("WareOut").Rows(l)("WO_Qty").ToString
                    pai.WI_ReQty = pdi(0).WI_ReQty

                    pac.UpdateProductionCheck_Qty(pai)
                End If
            Next
        End If



        mi.WO_ID = txtWIPID.EditValue
        mi.WO_Type = cbType.EditValue
        'mi.AP_NO = txtAPNO.Text
        mi.OP_NO = txtOPNO.Text
        mi.WH_ID = strWHID
        mi.WO_AddDate = Format(Now, "yyyy/MM/dd")
        mi.WO_Action = InUserID

        mi.DPT_ID = strDPTID

        'mi.DPT_ID = ButtonEdit2.EditValue

        If Len(TextEdit1.EditValue) = 0 Then
            mi.WO_PerID = Nothing

        Else
            mi.WO_PerID = TextEdit1.EditValue
        End If
        If Len(Label4.Text) = 0 Then
            mi.WO_PerName = Nothing

        Else
            mi.WO_PerName = Label4.Text
        End If

        '----------------------------------------------------
        mi.WO_Check = CheckEdit1.Checked
        mi.WO_CheckAction = InUserID
        mi.WO_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        mi.WO_CheckRemark = CheckRemark.Text
        '----------------------------------------------------

        For i = 0 To ds.Tables("WareOut").Rows.Count - 1
            mi.WO_NUM = GetWO_NUM()
            If IsDBNull(ds.Tables("WareOut").Rows(i)("M_Code")) Then
                mi.M_Code = Nothing
            Else
                mi.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")
            End If

            If IsDBNull(ds.Tables("WareOut").Rows(i)("OS_BatchID")) Then
                mi.OS_BatchID = Nothing
            Else
                mi.OS_BatchID = ds.Tables("WareOut").Rows(i)("OS_BatchID")
            End If


            mi.WO_Qty = CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))

            If IsDBNull(ds.Tables("WareOut").Rows(i)("WO_Remark")) Then
                mi.WO_Remark = Nothing
            Else
                mi.WO_Remark = ds.Tables("WareOut").Rows(i)("WO_Remark")
            End If

            '---------�ӻ�渹���J�A�L ���šA�s�b�h���J�ȩΪ̤�u��J

            If IsDBNull(ds.Tables("WareOut").Rows(i)("AP_NO")) Then

                mi.AP_NO = Nothing
            Else
                mi.AP_NO = ds.Tables("WareOut").Rows(i)("AP_NO")

            End If


            If IsDBNull(ds.Tables("WareOut").Rows(i)("M_ID")) Then

                mi.M_ID = Nothing
            Else
                mi.M_ID = ds.Tables("WareOut").Rows(i)("M_ID")

            End If

            If IsDBNull(ds.Tables("WareOut").Rows(i)("Temp_Code")) Then

                mi.Temp_Code = Nothing
            Else
                mi.Temp_Code = ds.Tables("WareOut").Rows(i)("Temp_Code")

            End If

            mc.WareOut_Add(mi)

        Next


        '-------------------------------------------------------------------------
        If CheckEdit1.Checked = True Then

            'Dim j As Integer
            Dim Qty As Double
            Dim dblWIP_EndQty As Double

            For i = 0 To ds.Tables("WareOut").Rows.Count - 1
                If strWHID = "W1101" Then            '@ 2013/3/9 �ק� ���ܪ��ܮw�O���~�ܮɡA��ܦ��~�w�s��
                    Dim pic As New ProductInventoryController
                    Dim piiSet As New ProductInventoryInfo
                    piiSet.WH_ID = strWHID
                    piiSet.PM_M_Code = ds.Tables("WareOut").Rows(i)("PM_M_Code")
                    piiSet.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")

                    Dim piiGet As List(Of ProductInventoryInfo)
                    piiGet = pic.ProductInventory_GetList(ds.Tables("WareOut").Rows(i)("PM_M_Code"), ds.Tables("WareOut").Rows(i)("M_Code"), strWHID, Nothing)

                    If piiGet.Count <= 0 Then
                        Qty = 0
                    Else
                        Qty = piiGet(0).PI_Qty
                    End If

                    piiSet.PI_Qty = Qty - CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))

                    dblWIP_EndQty = piiSet.PI_Qty

                    pic.ProductInventory_Update(piiSet)
                Else
                    Dim mt As New SharePurchaseController
                    Dim mm As New SharePurchaseInfo

                    mm.WH_ID = strWHID
                    mm.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")


                    Dim wi As New LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi = wc.WareInventory_GetSub(ds.Tables("WareOut").Rows(i)("M_Code"), strWHID)

                    If wi Is Nothing Then
                        Qty = 0
                    Else
                        Qty = wi.WI_Qty
                    End If

                    mm.WI_Qty = Qty - CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))

                    dblWIP_EndQty = mm.WI_Qty

                    mt.UpdateWareInventory_WIQty2(mm)   '���Ʒ�e�ܮw�w�s
                End If



                'Dim tii As New DataSetting.TransferInventoryInfo
                'Dim tic As New DataSetting.TransferInventoryControl

                'tii.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")
                'tii.M_Qty = mm.WI_Qty
                'tii.M_Bit = False

                'tic.TransferInventory_Add(tii)

                '-------------------�ܧ��e���l��-------------------------------
                Dim info As New WareOutInfo
                info.WO_ID = txtWIPID.Text
                info.WH_ID = strWHID
                info.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")

                info.WO_EndQty = dblWIP_EndQty

                mc.WareOut_UpdateEndQty(info)
                '--------------------------------------------------
            Next
            '--------------------------------------------------------------------�Ͳ��}�ưO���H��---�D�n�O���ܥͲ�����ƨϥΪ��p

            Dim pi As New ProductionMaterialInfo '����ƻ�ƼưO��
            Dim pc As New ProductionMaterialControl
            Dim pi1 As List(Of ProductionMaterialInfo)

            Dim pki As List(Of ProductionKaiLiaoInfo) '�}�ƫH��
            Dim pkc As New ProductionKaiLiaoControl

            If cbType.EditValue = "�Ͳ��}��" Then

                Dim m As Integer
                For m = 0 To ds.Tables("WareOut").Rows.Count - 1

                    pki = pkc.ProductionKaiLiao_GetList(txtOPNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)

                    If pki.Count > 0 Then
                        Dim k As Integer
                        For k = 0 To pki.Count - 1

                            '�P�_��e�O�_�s�b�����Ͳ��}�ƫH���O��
                            pi1 = pc.ProductionMaterial_GetList(pki(k).Pro_Type, pki(k).PM_M_Code, pki(k).PM_Type, pki(k).M_Code)
                            Dim MaterialQty As Single '�w�q��l�Ƽƶq/���q�H��

                            If pi1.Count = 0 Then
                                MaterialQty = 0
                            Else
                                MaterialQty = pi1(0).M_Qty  '��l�H��
                            End If

                            pi.Pro_Type = pki(k).Pro_Type
                            pi.PM_M_Code = pki(k).PM_M_Code
                            pi.PM_Type = pki(k).PM_Type
                            pi.M_Code = pki(k).M_Code
                            pi.M_Qty = MaterialQty + CDbl(ds.Tables("WareOut").Rows(m)("WO_Qty"))

                            pc.UpdateProductionMaterialQty(pi)  '�O����e�����~�Ͳ��}�Ʈw�s�H�����p

                        Next

                    End If

                Next

            End If



            '  --------------------------------------------------------------------�ӻ�渹�O���H��
            Dim ti As New TransferModuleInfo
            Dim tc As New TransferModuleControl


            Dim ai As List(Of ApplyInfo)
            Dim ac As New ApplyControl
            Dim ai1 As New ApplyInfo

            Dim j As Integer
            For j = 0 To ds.Tables("WareOut").Rows.Count - 1

                If ds.Tables("WareOut").Rows(j)("M_ID") = "" Then
                Else


                    ai = ac.Apply_GetList(ds.Tables("WareOut").Rows(j)("AP_NO"), ds.Tables("WareOut").Rows(j)("M_ID"), Nothing, Nothing)

                    If ai.Count > 0 Then

                        Dim SngA As Single

                        Dim m As Integer
                        For m = 0 To ai.Count - 1
                            SngA = ai(0).M_SendQty
                        Next

                        '-----------------------------�ܧ��e�ӻ�椤�o�X�ƶq�O��--------------------------------------------------
                        ai1.AM_ID = ds.Tables("WareOut").Rows(j)("AP_NO")
                        ai1.M_ID = ds.Tables("WareOut").Rows(j)("M_ID")
                        ai1.M_Code = ds.Tables("WareOut").Rows(j)("M_Code")
                        'ai1.M_SendQty = SngA + CSng(ds.Tables("WareOut").Rows(j)("WO_Qty"))

                        ''2012-9-4 �[�p�G���ફ�ƽs�X�����p�A���N�ӻ�椤�����ƶq �A�o�f�ƶq�i����

                        If ai(0).M_Code = ds.Tables("WareOut").Rows(j)("M_Code") Then
                            ai1.M_Qty = ai(0).M_Qty
                            ai1.M_SendQty = SngA + CDbl(ds.Tables("WareOut").Rows(j)("WO_Qty"))
                        Else
                            ai1.M_Qty = CDbl(ds.Tables("WareOut").Rows(j)("WO_Qty"))
                            ai1.M_SendQty = CDbl(ds.Tables("WareOut").Rows(j)("WO_Qty"))
                        End If



                        ac.Apply_UpdateQty(ai1)


                        '�ק����@ 2012-06-19  ��e�ϥηs���O����e���ƹ�ɮw�s�H�� ���Ҷ����A�ϥ� 
                        '-------------------------------------------------------------------------------
                        '-------------------------------------------------------------------------------
                        Dim SngB As Double

                        SngB = ai1.M_SendQty

                        ti.ModuleID = "�X�w�@�~"
                        ti.ModuleDetail = ds.Tables("WareOut").Rows(j)("AP_NO") & "," & ds.Tables("WareOut").Rows(j)("M_ID") & "," & ds.Tables("WareOut").Rows(j)("M_Code") & "," & SngB & "," & ds.Tables("WareOut").Rows(j)("Temp_Code") & "," & ds.Tables("WareOut").Rows(j)("WO_Remark")
                        ti.ModuleRemark = txtWIPID.Text & ds.Tables("WareOut").Rows(j)("AP_NO")
                        tc.TransferModule_Add(ti)

                    End If


                End If

            Next
            '----------------------------------------------------------------------

        End If


            '-------------------------------------------------------------------------
    End Sub

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
        For i = 0 To ds.Tables("WareOut").Rows.Count - 1

            mwi = mw.WareInventory_GetSub(ds.Tables("WareOut").Rows(i)("M_Code"), strWHID)
            If mwi Is Nothing Then
                MsgBox("����" & ds.Tables("WareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�����s�b�A����O�s�I")
                Exit Sub
            End If

            If mwi.WI_Qty < CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty")) Then
                MsgBox("����" & ds.Tables("WareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�������ơA����O�s�I")
                Exit Sub
            End If
        Next


        '��s�R�����O��
        If ds.Tables("DelDate").Rows.Count > 0 Then
            Dim ii As Integer
            For ii = 0 To ds.Tables("DelDate").Rows.Count - 1

                Dim mc2 As New WareOutController
                If Not IsDBNull(ds.Tables("DelDate").Rows(ii)("WO_NUM")) Then
                    mc2.WareOut_Delete(ds.Tables("DelDate").Rows(ii)("WO_NUM"), Nothing)
                End If
            Next ii
        End If




        Dim mc As New WareOutController
        Dim mi As New WareOutInfo



        mi.WO_ID = txtWIPID.EditValue
        mi.WO_Type = cbType.EditValue
        'mi.AP_NO = txtAPNO.Text
        mi.OP_NO = txtOPNO.Text
        mi.WH_ID = strWHID
        mi.WO_AddDate = DateEdit1.EditValue
        mi.WO_Action = Label15.Text

        mi.DPT_ID = strDPTID

        'mi.DPT_ID = ButtonEdit2.EditValue
        mi.WO_PerID = TextEdit1.EditValue
        mi.WO_PerName = Label4.Text


        '----------------------------------------------------  �ק�f�֫H��
        mi.WO_Check = CheckEdit1.Checked
        mi.WO_CheckAction = InUserID
        mi.WO_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        mi.WO_CheckRemark = CheckRemark.Text
        '----------------------------------------------------

        For i = 0 To ds.Tables("WareOut").Rows.Count - 1
            If IsDBNull(ds.Tables("WareOut").Rows(i)("WO_NUM")) Then   '�s�W
                mi.WO_NUM = GetWO_NUM()
                mi.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")

                mi.OS_BatchID = ds.Tables("WareOut").Rows(i)("OS_BatchID")
                mi.WO_Qty = CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))
                mi.WO_Remark = ds.Tables("WareOut").Rows(i)("WO_Remark")

                '---------�ӻ�渹���J�A�L ���šA�s�b�h���J�ȩΪ̤�u��J

                If IsDBNull(ds.Tables("WareOut").Rows(i)("AP_NO")) Then

                    mi.AP_NO = Nothing
                Else
                    mi.AP_NO = ds.Tables("WareOut").Rows(i)("AP_NO")

                End If

                If IsDBNull(ds.Tables("WareOut").Rows(i)("M_ID")) Then

                    mi.M_ID = Nothing
                Else
                    mi.M_ID = ds.Tables("WareOut").Rows(i)("M_ID")

                End If

                If IsDBNull(ds.Tables("WareOut").Rows(i)("Temp_Code")) Then

                    mi.Temp_Code = Nothing
                Else
                    mi.Temp_Code = ds.Tables("WareOut").Rows(i)("Temp_Code")

                End If

                mc.WareOut_Add(mi)
            ElseIf Not IsDBNull(ds.Tables("WareOut").Rows(i)("WO_NUM")) Then ' �ק�
                mi.WO_NUM = ds.Tables("WareOut").Rows(i)("WO_NUM")
                mi.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")

                mi.OS_BatchID = ds.Tables("WareOut").Rows(i)("OS_BatchID")
                mi.WO_Qty = CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))
                mi.WO_Remark = ds.Tables("WareOut").Rows(i)("WO_Remark")
                mi.WO_EditDate = Format(Now, "yyyy/MM/dd")


                '---------�ӻ�渹���J�A�L ���šA�s�b�h���J�ȩΪ̤�u��J

                If IsDBNull(ds.Tables("WareOut").Rows(i)("AP_NO")) Then

                    mi.AP_NO = Nothing
                Else
                    mi.AP_NO = ds.Tables("WareOut").Rows(i)("AP_NO")

                End If

                If IsDBNull(ds.Tables("WareOut").Rows(i)("M_ID")) Then

                    mi.M_ID = Nothing
                Else
                    mi.M_ID = ds.Tables("WareOut").Rows(i)("M_ID")

                End If

                If IsDBNull(ds.Tables("WareOut").Rows(i)("Temp_Code")) Then

                    mi.Temp_Code = Nothing
                Else
                    mi.Temp_Code = ds.Tables("WareOut").Rows(i)("Temp_Code")

                End If
                mc.WareOut_Update(mi)
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

                For i = 0 To ds.Tables("WareOut").Rows.Count - 1
                    mm.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")

                    Dim Qty As Double

                    Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi = wc.WareInventory_GetSub(ds.Tables("WareOut").Rows(i)("M_Code"), strWHID)

                    If wi Is Nothing Then
                        Qty = 0
                    Else
                        Qty = wi.WI_Qty
                    End If

                    mm.WI_Qty = Qty - CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))
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
                    For m = 0 To ds.Tables("WareOut").Rows.Count - 1

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
                                pi.M_Qty = MaterialQty + CDbl(ds.Tables("WareOut").Rows(m)("WO_Qty"))

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
                For j = 0 To ds.Tables("WareOut").Rows.Count - 1

                    If ds.Tables("WareOut").Rows(j)("M_ID") = "" Then
                    Else


                        ai = ac.Apply_GetList(ds.Tables("WareOut").Rows(j)("AP_NO"), ds.Tables("WareOut").Rows(j)("M_ID"), Nothing, Nothing)

                        If ai.Count > 0 Then

                            Dim SngA As Double

                            'Dim m As Integer
                            'For m = 0 To ai.Count - 1
                            SngA = ai(0).M_SendQty  '�Y�s�b--�h�u���@���O��
                            'Next
                            '-----------------------------�ܧ��e�ӻ�椤�o�X�ƶq�O��--------------------------------------------------
                            ai1.AM_ID = ds.Tables("WareOut").Rows(j)("AP_NO")
                            ai1.M_ID = ds.Tables("WareOut").Rows(j)("M_ID")
                            ai1.M_Code = ds.Tables("WareOut").Rows(j)("M_Code")
                            ai1.M_SendQty = SngA + CDbl(ds.Tables("WareOut").Rows(j)("WO_Qty"))

                            ac.Apply_UpdateQty(ai1)
                            '-------------------------------------------------------------------------------
                            '�ק����@ 2012-06-19  ��e�ϥηs���O����e���ƹ�ɮw�s�H��,
                            '���Ҷ���e�u��ӻ��A�o�f�ƶq�i��O��, �w�T�w��e�ӻ��O�_�w����
                            '-------------------------------------------------------------------------------
                            Dim SngB As Double

                            SngB = ai1.M_SendQty

                            ti.ModuleID = "�X�w�@�~"
                            ti.ModuleDetail = ds.Tables("WareOut").Rows(j)("AP_NO") & "," & ds.Tables("WareOut").Rows(j)("M_ID") & "," & ds.Tables("WareOut").Rows(j)("M_Code") & "," & SngB & "," & ds.Tables("WareOut").Rows(j)("Temp_Code") & "," & ds.Tables("WareOut").Rows(j)("WO_Remark")
                            ti.ModuleRemark = txtWIPID.Text & ds.Tables("WareOut").Rows(j)("AP_NO")

                            tc.TransferModule_Add(ti)

                        End If

                    End If

                Next
                '-----------------------------------------------------------------------------

            End If
        End If

    End Sub

    Sub UpdateCheck()

        Dim i As Integer

        If oldcheck = CheckEdit1.Checked Then
            MsgBox("�f�֪��A�����ܡA�Ч�窱�A��A�O�s�K�K")
            Exit Sub
        End If

        'If CheckEdit1.Checked = True Then
        '    Dim mw As New WareInventory.WareInventoryMTController
        '    Dim mwi As New WareInventory.WareInventoryInfo

        '    For i = 0 To ds.Tables("WareOut").Rows.Count - 1
        '        '�d�߬O�_�������ܮw������
        '        mwi = mw.WareInventory_GetSub(ds.Tables("WareOut").Rows(i)("M_Code"), strWHID)

        '        If mwi Is Nothing Then
        '            MsgBox("����" & ds.Tables("WareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�����s�b�A����O�s�I")
        '            Exit Sub
        '        End If

        '        If mwi.WI_Qty < CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty")) Then
        '            MsgBox("����" & ds.Tables("WareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�������ơA����O�s�I")
        '            Exit Sub
        '        End If
        '    Next
        'End If


        Dim mc As New WareOutController
        Dim mi As New WareOutInfo
        mi.WO_ID = txtWIPID.EditValue
        mi.WO_Check = CheckEdit1.Checked
        mi.WO_CheckAction = InUserID
        mi.WO_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        mi.WO_CheckRemark = CheckRemark.Text
        If mc.WareOut_UpdateCheck(mi) = False Then
            MsgBox("�����f�֥���,���ˬd��]!")
            Exit Sub
        End If

        Dim mt As New SharePurchaseController
        Dim mm As New SharePurchaseInfo

        mm.WH_ID = strWHID

        For i = 0 To ds.Tables("WareOut").Rows.Count - 1
            mm.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")

            Dim Qty As Double
            Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi = wc.WareInventory_GetSub(ds.Tables("WareOut").Rows(i)("M_Code"), strWHID)

            If wi Is Nothing Then
                Qty = 0
            Else
                Qty = wi.WI_Qty
            End If

            If CheckEdit1.Checked = False Then
                mm.WI_Qty = Qty + CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))
                'ElseIf CheckEdit1.Checked = True Then
                '    mm.WI_Qty = Qty - CSng(ds.Tables("WareOut").Rows(i)("WO_Qty"))
            End If
            mt.UpdateWareInventory_WIQty2(mm)

            '--------------------------------------------------------------------�Ͳ��}�ưO���H��---�D�n�O���ܥͲ�����ƨϥΪ��p

            Dim pi As New ProductionMaterialInfo '����ƻ�ƼưO��
            Dim pc As New ProductionMaterialControl
            Dim pi1 As List(Of ProductionMaterialInfo)

            Dim pki As List(Of ProductionKaiLiaoInfo) '�}�ƫH��
            Dim pkc As New ProductionKaiLiaoControl

            If cbType.EditValue = "�Ͳ��}��" Then


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
                        pi.M_Qty = MaterialQty - CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))

                        pc.UpdateProductionMaterialQty(pi)  '�ܧ��e�����~�Ͳ��}�Ʈw�s�H�����p

                    Next

                End If

            End If

            '-----------------------------------�ܧ�s�b���ӻ��O�����p----------------------
            Dim ai As New ApplyInfo
            Dim aii As List(Of ApplyInfo)
            Dim ac As New ApplyControl

            If ds.Tables("WareOut").Rows(i)("M_ID") = "" Then
            Else
                aii = ac.Apply_GetList(ds.Tables("WareOut").Rows(i)("AP_NO"), ds.Tables("WareOut").Rows(i)("M_ID"), Nothing, Nothing)

                If aii.Count = 0 Then

                Else
                    ai.AM_ID = ds.Tables("WareOut").Rows(i)("AP_NO")
                    ai.M_ID = ds.Tables("WareOut").Rows(i)("M_ID")
                    ai.M_Code = ds.Tables("WareOut").Rows(i)("M_Code")
                    ai.M_SendQty = aii(0).M_SendQty - CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty"))

                    ac.Apply_UpdateQty(ai)

                End If

            End If

            '-----------------------------------------------------------------------------------

        Next


        Dim fi As New TransferModuleInfo
        Dim fc As New TransferModuleControl

        fi.ModuleID = "�X�w�@�~"
        fi.ModuleRemark = txtWIPID.Text

        fc.TransferModule_Delete(fi.ModuleID, fi.ModuleRemark) '�R����e�����L���ӻ�渹�O���H��

        Me.Close()

    End Sub

    Sub UpdateReCheck()

        Dim mc As New WareOutController
        Dim mi As New WareOutInfo
        mi.WO_ID = txtWIPID.EditValue
        mi.WO_ReCheck = CheckEdit2.Checked
        mi.WO_ReCheckAction = InUserID
        mi.WO_ReCheckDate = Format(Now, "yyyy/MM/dd")
        mi.WO_ReCheckRemark = txtrecheckremark.Text
        If mc.WareOut_UpdateReCheck(mi) = False Then
            MsgBox("�_�֥���,���ˬd��]!")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub popWareOutBarAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareOutBarAdd.Click
        tempValue4 = Me.strWHID
        FrmBarCodeInput.ds.Tables.Clear()
        FrmBarCodeInput.ShowDialog()
        If FrmBarCodeInput.ds.Tables("BarCode").Rows.Count <> 0 Then
            Dim i As Integer
            For i = 0 To FrmBarCodeInput.ds.Tables("BarCode").Rows.Count - 1
                If IsDBNull(FrmBarCodeInput.ds.Tables("BarCode").Rows(i)("M_Code")) Then
                Else
                    AddRow(FrmBarCodeInput.ds.Tables("BarCode").Rows(i)("M_Code"), FrmBarCodeInput.ds.Tables("BarCode").Rows(i)("WIP_Qty"))
                End If
            Next
        End If

    End Sub

    'Private Sub TextEdit1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextEdit1.LostFocus
    'On Error Resume Next

    'Dim mt As New LFERP.DataSetting.EmployeControler
    'Dim mc As New LFERP.DataSetting.EmployeInfo
    'Dim uc As New LFERP.SystemManager.SystemUser.SystemUserController
    'If Trim(TextEdit1.EditValue) = "" Then      '@ 2012/2/14�K�[�P�_TextEdit1.EditValue�O�_����
    '    Exit Sub
    'End If

    'mc = mt.Employe_Get(TextEdit1.EditValue)
    'If mc Is Nothing Then
    '    Exit Sub
    'Else
    '    Label4.Text = mc.Employe_Name
    '    Dim wulc As New WhiteUserListController
    '    Dim wuliL As New List(Of WhiteuserListInfo)
    '    wuliL = wulc.WhiteUserList_GetList(TextEdit1.Text, Mid(strWHID, 1, 3))
    '    'If wuliL.Count = 0 Then
    '    '    cmdSave.Text = "�D�k�d��"
    '    '    cmdSave.Enabled = False
    '    '    Exit Sub
    '    'End If
    '    Label4.Text = wuliL.Item(0).W_UserName
    '    If wuliL.Item(0).DPT_ID = "" Then
    '        'strDPTID = ""
    '        'ButtonEdit2.Text = ""
    '        'ButtonEdit2.Enabled = True
    '        TextEdit1.Enabled = False
    '    Else
    '        strDPTID = wuliL.Item(0).DPT_ID
    '        ButtonEdit2.Text = wuliL.Item(0).DPT_Name
    '        ButtonEdit2.Enabled = False
    '        TextEdit1.Enabled = False
    '    End If
    'End If

    'End Sub

    Private Sub cmdBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBarCode.Click
        'Dim i, m As Integer
        'For i = 0 To ds.Tables("WareOut").Rows.Count - 1
        '    Dim str1, str2, str3, str4, str5, str6, str7 As String

        '    str1 = ds.Tables("WareOut").Rows(i)("M_Code")
        '    str2 = ds.Tables("WareOut").Rows(i)("M_Name")
        '    str3 = Nothing
        '    str4 = ds.Tables("WareOut").Rows(i)("M_Unit")
        '    str5 = ds.Tables("WareOut").Rows(i)("MO_Qty")
        '    str6 = Format(DateEdit1.DateTime, "Short Date")
        '    str7 = ds.Tables("WareOut").Rows(i)("OS_BatchID")


        '    Dim whc As New WareHouseController
        '    Dim whiL As New List(Of WareHouseInfo)
        '    whiL = whc.WareHouse_GetList(Nothing)
        '    For m = 0 To whiL.Count - 1
        '        If whiL.Item(m).WH_ID = txtWH.Text Then
        '            If whiL.Item(m).PrintBulk = "�j" Then
        '                PrintBar(str1, str2, str3, str4, str5, str6, str7)
        '            End If

        '            If whiL.Item(m).PrintBulk = "�p" Then
        '                PrintBar2(str1, str2)
        '            End If
        '            Exit For
        '        End If
        '    Next

        'Next
        tempValue3 = "�X�w�@�~"
        tempValue4 = txtWIPID.EditValue
        Dim myfrm As New frmBarCode
        myfrm.ShowDialog()
    End Sub

    Private Sub ButtonEdit2_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEdit2.ButtonClick
        '' ''frmDepartmentSelect.DptID = ""
        '' ''frmDepartmentSelect.DptName = ""

        '' ''frmDepartmentSelect.ShowDialog()

        '' ''If frmDepartmentSelect.DptID = "" Then

        '' ''Else

        '' ''    ButtonEdit2.Text = frmDepartmentSelect.DptName
        '' ''    strDPTID = frmDepartmentSelect.DptID
        '' ''    'ButtonEdit2.Tag = frmDepartmentSelect.DptID

        '' ''End If

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "500107")
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

    Private Sub sk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sk.Click

        TextEdit1.Text = ReadCard1() 'Ū���d��

        Dim wulc As New WhiteUserListController
        Dim wuliL As New List(Of WhiteuserListInfo)
        wuliL = wulc.WhiteUserList_GetList(TextEdit1.Text, Mid(strWHID, 1, 3))
        If wuliL.Count = 0 Then
            'cmdSave.Text = "�D�k�d��"
            MsgBox("����d�ΫD�k�d��")
            'cmdSave.Enabled = False
            Exit Sub
        Else
            cmdSave.Text = "�T�w"
            cmdSave.Enabled = True
        End If
        Label4.Text = wuliL.Item(0).W_UserName
        strDPTID = wuliL.Item(0).DPT_ID
        ButtonEdit2.Text = wuliL.Item(0).DPT_Name

        '-------------------------------------------------------------------------
        'Dim strtemp2 As String = Application.StartupPath & "\vb6\ReadIC.exe "
        ''��d�ø��J��d�H�H��

        'Shell(strtemp2, AppWinStyle.NormalFocus, True)

        'Dim tc As New TempController
        'Dim ti As New List(Of TempInfo)
        'ti = tc.Temp_GetList(Nothing, Nothing, Nothing)
        'TextEdit1.Text = ti.Item(0).Str1

        'Dim wulc As New WhiteUserListController
        'Dim wuliL As New List(Of WhiteuserListInfo)
        'wuliL = wulc.WhiteUserList_GetList(TextEdit1.Text, Mid(strWHID, 1, 3))
        'If wuliL.Count = 0 Then
        '    cmdSave.Text = "�D�k�d��"
        '    cmdSave.Enabled = False
        '    Exit Sub
        'End If
        'Label4.Text = wuliL.Item(0).W_UserName
        'strDPTID = wuliL.Item(0).DPT_ID
        'ButtonEdit2.Text = wuliL.Item(0).DPT_Name

        '-------------------------------------------------------------------------
        'TextEdit1.Text = "0403034"
        'Label4.Text = "�_�p��"            '
        'strDPTID = "10010104"             '*���ե�*��
        'ButtonEdit2.EditValue = "�q����"  '

        ButtonEdit2.Enabled = False
        TextEdit1.Enabled = False

    End Sub

    Private Function ReadCard1() As String

        'MsgBox(ReadComm)
        ReadComm = Val(GetIni("CommSet", "ER900"))
        If ReadComm = 0 Then
            ReadComm = 1
        End If

        Dim portptr As IntPtr = ReadWriteCard.readwriteDll.OpenCommPort(ReadComm, 9600)
        Dim port As Integer = Int32.Parse(portptr.ToString())
        Dim isclock As Boolean

        If port <> -1 AndAlso port <> 0 Then
            isclock = ReadWriteCard.readwriteDll.CallClock(portptr, Int32.Parse(0))
            If isclock Then


                Dim temp As New ReadWriteCard.info
                temp.CardNo = New Byte(16) {}
                temp.CardName = New Byte(16) {}
                temp.Money = 0
                temp.Times = 0
                temp.Ver = 0
                Try
                    Dim suc As Boolean = ReadWriteCard.readwriteDll.ReadICCard(portptr, temp.CardNo, temp.CardName, temp.Money, temp.Times, temp.Ver)

                    If suc Then

                        If Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 9, 1) = "2" Then
                            ReadCard1 = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 8)
                        Else
                            ReadCard1 = Mid(System.Text.Encoding.ASCII.GetString(temp.CardNo), 1, 7)
                        End If
                    Else
                        MessageBox.Show("�L�k�����u�d�Ψ�d�����s���I")
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                ReadWriteCard.readwriteDll.CloseCommPort(ReadComm)
            Else
                MessageBox.Show("�p������!")
                ReadCard1 = ""
            End If
        ElseIf port = 0 Then
            MessageBox.Show("�L�k���}�ݤf!")
        ElseIf port = -1 Then

            MessageBox.Show("�ݤf�L�ĩΥ��b�ϥ�!")

        End If

    End Function
    '�ɤJ�Y�妸����Ӱt��i��X�w�ާ@
    Private Sub popLoadBatchAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popLoadBatchAdd.Click

        tempValue4 = strWHID
        frmLoadingBatchID.ShowDialog()

        If tempValue = "" Or tempValue2 = "" Then
            Exit Sub
        Else
            AddRow1(tempValue, tempValue2, tempValue3)

            tempValue = ""
            tempValue2 = ""
            tempValue3 = Nothing

        End If

    End Sub

    Sub AddRow1(ByVal strBatchID As String, ByVal strCode As String, ByVal strQty As Single)

        Dim row As DataRow
        row = ds.Tables("WareOut").NewRow

        If strCode = "" Then

        Else

            Dim j As Integer

            For j = 0 To ds.Tables("WareOut").Rows.Count - 1
                If strBatchID = ds.Tables("WareOut").Rows(j)("OS_BatchID") And strCode = ds.Tables("WareOut").Rows(j)("M_Code") Then
                    MsgBox("�@�i��ۦP�妸�s���ۦP���Ƥ����\�s�b�h���O���P�P�P�P")
                    Exit Sub
                End If
            Next


            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            row("WO_NUM") = Nothing
            row("WO_ID") = Nothing
            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name

            Dim unit As New LFERP.DataSetting.UnitController
            Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

            unitinfo = unit.GetUnitList(objInfo.M_Unit)
            If unitinfo.Count > 0 Then
                row("M_Unit") = unitinfo(0).U_Name
            Else
                row("M_Unit") = ""
            End If

            row("M_Gauge") = objInfo.M_Gauge
            row("OS_BatchID") = strBatchID
            row("WO_Qty") = strQty
            row("M_ID") = ""
            row("Temp_Code") = ""

            '2012-7-16 �[�J��ܦw���w�s�P�`�E��-----------------------------
            Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi1 = wc1.WareInventory_GetSub(strCode, strWHID)

            If wi1 Is Nothing Then
                row("WI_SafeQty_Show") = 0
                row("WO_EndQty_Show") = 0
            Else
                row("WI_SafeQty_Show") = wi1.WI_SafeQty
                row("WO_EndQty_Show") = wi1.WI_Qty
            End If

            ''------------------------------------------------------

            ds.Tables("WareOut").Rows.Add(row)
        End If
        GridView1.MoveLast()
    End Sub

    '���}���ɤ��H��
    Private Sub popFileShowOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popFileShowOpen.Click
        Dim dt As New FileController
        If GridFile.Rows.Count = 0 Then Exit Sub
        dt.File_Open(Nothing, Nothing, GridFile.CurrentRow.Cells("F_No").Value.ToString)
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim ds As New DataSet
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet


        Dim pmc As New WareOutController
        Dim uc As New UnitController
        Dim suc As New SystemUser.SystemUserController
        Dim wh As New WareHouseController

        Dim pmc2 As New WhiteUserListController
        Dim uc2 As New DepartmentControler

        '    Dim omc As New OrdersMainController
        ds.Tables.Clear()
        Dim strA, strB As String
        strA = txtWIPID.Text
        strB = TextEdit1.Text

        Dim wc As New WareHouseController
        Dim wiL As New List(Of WareHouseInfo)
        wiL = wc.WareHouse_Get(strWHID)

        If wiL(0).NeedCheck = False Then

            ltc.CollToDataSet(ds, "WareOut", pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc2.CollToDataSet(ds, "SystemUser", suc.SystemUser_GetList(Nothing, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "WareHouse", wh.WareHouse_GetList(Nothing))
            ltc5.CollToDataSet(ds, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))

            Dim pmi As List(Of WareOutInfo)
            pmi = pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If pmi.Count = 0 Then Exit Sub
            If pmi(0).WO_Check = False Then

                PreviewRPT(ds, "rptWareOutNoCard", "�X�w��", False, False)
            Else

                PreviewRPT(ds, "rptWareOutNoCard", "�X�w��", True, False)
            End If


            ltc = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
            ltc5 = Nothing

        Else
            ltc.CollToDataSet(ds, "WareOut", pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc1.CollToDataSet(ds, "Unit", uc.GetUnitList(Nothing))
            ltc2.CollToDataSet(ds, "SystemUser", suc.SystemUser_GetList(Nothing, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "WareHouse", wh.WareHouse_GetList(Nothing))

            ltc4.CollToDataSet(ds, "WhiteUserList", pmc2.WhiteUserList_GetList(strB, Nothing))
            ltc5.CollToDataSet(ds, "Department", uc2.Department_GetList(Nothing, Nothing, Nothing))

            Dim poi As List(Of WareOutInfo)

            poi = pmc.WareOut_Getlist5(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            'If poi(0).WH_ID = "W0401" Then
            If GetKnifeWareHouseReport(poi(0).WH_ID) = True Then
                If poi(0).WO_Check = False Then
                    PreviewRPT(ds, "rptWareOut1", "�X�w��", False, False)
                Else
                    PreviewRPT(ds, "rptWareOut1", "�X�w��", True, False)
                End If

            Else
                If poi(0).WO_Check = False Then
                    PreviewRPT(ds, "rptWareOut", "�X�w��", False, False)
                Else
                    PreviewRPT(ds, "rptWareOut", "�X�w��", True, False)
                End If

            End If

            ltc = Nothing
            ltc1 = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
            ltc4 = Nothing
            ltc5 = Nothing
        End If
        Me.Close()

    End Sub

    Private Function GetKnifeWareHouseReport(ByVal StrWHID As String) As Boolean
        Dim strWHRemark As String
        GetKnifeWareHouseReport = False

        Dim wc As New WareHouseController
        Dim wl As New List(Of WareHouseInfo)
        wl = wc.WareHouse_Get(StrWHID)
        strWHRemark = wl(0).WH_Remark  '�p�G�O�]�Z��

        If strWHRemark = "KnifeReports" Or strWHRemark = "�M��ܳ���" Then
            GetKnifeWareHouseReport = True
        End If
    End Function

    '�w��t�󳡬Y�妸����X�w�ɾާ@
    Private Sub popLoadBatchAllAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popLoadBatchAllAdd.Click

        frmWareOutBatchID.ShowDialog()

        If tempValue2 = Nothing Then Exit Sub

        'If Label6.Text.Equals(tempValue2) = True Then
        '    MsgBox("���X�w�椤�w�s�b���妸���X�w�O���I")
        '    Exit Sub
        'End If

        'Label6.Text = tempValue2
        Dim j As Integer

        For j = 0 To ds.Tables("WareOut").Rows.Count - 1
            If tempValue2 = ds.Tables("WareOut").Rows(j)("OS_BatchID") Then
                MsgBox("�@�i�椣���\�����_�妸�s��....")
                Exit Sub
            End If
        Next

        Dim wic As New WareInput.WareInputContraller
        Dim woc As New WareOutController



        Dim wi1 As List(Of WareInput.WareInputInfo)
        wi1 = wic.WareInput_GetQty(tempValue2, Nothing, strWHID, True)

        If wi1.Count = 0 Then
            MsgBox("���妸�S���J�w�O���I")
            Exit Sub
        Else

            Dim i As Integer
            For i = 0 To wi1.Count - 1

                Dim InputQty As Double
                Dim OutputQty As Double

                Dim row As DataRow
                row = ds.Tables("WareOut").NewRow

                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo
                objInfo = mc.MaterialCode_Get(wi1(i).M_Code)

                row("WO_NUM") = Nothing
                row("WO_ID") = Nothing
                row("M_Code") = objInfo.M_Code
                row("M_Name") = objInfo.M_Name

                Dim unit As New LFERP.DataSetting.UnitController
                Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

                unitinfo = unit.GetUnitList(objInfo.M_Unit)

                row("M_Unit") = unitinfo(0).U_Name
                row("M_Gauge") = objInfo.M_Gauge
                row("OS_BatchID") = tempValue2

                Dim wi As List(Of WareInput.WareInputInfo)
                wi = wic.WareInput_GetQty(tempValue2, wi1(i).M_Code, strWHID, True)

                If wi.Count = 0 Then
                    InputQty = 0
                Else
                    InputQty = wi(0).GetQty
                End If
                Dim wo1 As List(Of WareOut.WareOutInfo)

                wo1 = woc.WareOut_GetQty(tempValue2, wi1(i).M_Code, strWHID, True)
                If wo1.Count = 0 Then
                    OutputQty = 0
                Else
                    OutputQty = wo1(0).OutGetQty
                End If
                Dim wii As List(Of WareInventory.WareInventoryInfo)
                Dim wiic As New WareInventory.WareInventoryMTController
                wii = wiic.WareInventory_GetList(wi1(i).M_Code, strWHID)
                If wii.Count = 0 Then
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = 0
                    MsgBox("��e�ܮw�L�����ƩΦ����Ʈw�s��0�I")
                    Exit Sub
                Else
                    If InputQty - OutputQty > wii(0).WI_Qty Then
                        MsgBox("���妸���l�ƶq�j��w�s,�ȸ��J��e�w�s�ơI")
                        row("WO_Qty") = wii(0).WI_Qty
                    Else
                        row("WO_Qty") = InputQty - OutputQty
                    End If

                    row("WI_SafeQty_Show") = wii(0).WI_SafeQty
                    row("WO_EndQty_Show") = wii(0).WI_Qty


                End If
                row("M_ID") = ""
                row("Temp_Code") = ""

                ds.Tables("WareOut").Rows.Add(row)
            Next
        End If

        tempValue2 = ""

    End Sub

    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        If cbType.EditValue = "�Ͳ��}��" Then
            Label16.Text = "�}�Ƴ渹:"
            popWareInput.Enabled = False
        ElseIf cbType.EditValue = "�Ͳ��X�f" Then
            Label16.Text = "�X�f�渹"
            popWareInput.Enabled = False
        Else
            Label6.Text = "�渹"
            popWareInput.Enabled = True
        End If
    End Sub

    Private Sub txtOPNO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOPNO.KeyPress
        Dim woc As New WareOutController
        Dim woi As List(Of WareOutInfo)

        If e.KeyChar = Chr(13) Then

            If cbType.EditValue = "�Ͳ��}��" Then
                Dim pli As List(Of ProductionKaiLiaoInfo)
                Dim plc As New ProductionKaiLiaoControl

                pli = plc.ProductionKaiLiao_GetList(txtOPNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
                If pli.Count = 0 Then
                    MsgBox("���s�b���}�Ƴ渹�Φ��}�Ƴ楼�f��!")
                    txtOPNO.EditValue = ""
                    txtOPNO.Enabled = True
                    Exit Sub
                End If

                If pli(0).WH_ID <> strWHID Then
                    MsgBox("���}�Ƴ渹,���O�b���ܻ��!")
                    txtOPNO.EditValue = ""
                    txtOPNO.Enabled = True
                    Exit Sub
                End If

                If AllowChageQty = True Then ''�i��ƶq
                    WO_Qty.OptionsColumn.AllowEdit = True
                Else
                    WO_Qty.OptionsColumn.AllowEdit = False
                End If

                '--------------------@ 2012/10/9 �K�[�@�P�_�}�Ƴ�O�_�w�����o��------
                woi = woc.WareOut_GetQty3(Nothing, Nothing, txtOPNO.Text.Trim)

                If woi.Count > 0 Then
                    If woi(0).AllQty >= pli(0).C_Weight Then
                        MsgBox("���}�Ƴ�w�����o�ơA����A�o�ơI", 64, "����")
                        txtOPNO.Focus()
                        Exit Sub
                    End If
                End If
                '---------------------------------------------------------------------------------------------------

                AP_NO.OptionsColumn.ReadOnly = True

                Dim j As Integer
                Dim row As DataRow

                ds.Tables("WareOut").Clear()

                For j = 0 To pli.Count - 1

                    row = ds.Tables("WareOut").NewRow

                    row("AP_NO") = txtOPNO.Text.Trim
                    row("WO_NUM") = Nothing
                    row("WO_ID") = Nothing
                    row("M_Code") = pli(j).M_Code
                    row("M_Name") = pli(j).M_Name

                    Dim mc As New LFERP.Library.Material.MaterialController
                    Dim objInfo As New LFERP.Library.Material.MaterialInfo
                    objInfo = mc.MaterialCode_Get(pli(j).M_Code)

                    Dim unit As New LFERP.DataSetting.UnitController
                    Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

                    unitinfo = unit.GetUnitList(objInfo.M_Unit)

                    row("M_Unit") = unitinfo(0).U_Name
                    row("M_Gauge") = pli(j).M_Gauge
                    row("OS_BatchID") = ""

                    '@ 2012/10/15 �ק�
                    row("WO_Qty") = pli(j).C_Weight

                    'If objInfo.M_Unit = "KG" Or objInfo.M_Unit = "����" Or objInfo.M_Unit = "�d�J" Or objInfo.M_Unit = "Catty" Or objInfo.M_Unit = "t" Or objInfo.M_Unit = "ʤ" Then
                    '    row("WO_Qty") = pli(j).C_Weight
                    'Else
                    '    row("WO_Qty") = pli(j).C_Qty
                    'End If

                    row("M_SendQty") = woi(0).AllQty
                    row("C_Qty") = pli(j).C_Weight

                    row("M_ID") = ""
                    row("Temp_Code") = ""


                    '2012-7-16 �[�J��ܦw���w�s�P�`�E��-----------------------------
                    Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi1 = wc1.WareInventory_GetSub(pli(j).M_Code, strWHID)


                    If wi1 Is Nothing Then
                        row("WI_SafeQty_Show") = 0
                        row("WO_EndQty_Show") = 0
                    Else
                        row("WI_SafeQty_Show") = wi1.WI_SafeQty
                        row("WO_EndQty_Show") = wi1.WI_Qty
                    End If

                    ds.Tables("WareOut").Rows.Add(row)
                    txtOPNO.Enabled = False
                    cbType.Enabled = False
                    C_Qty.VisibleIndex = 6
                    C_Qty.Visible = True
                    'WO_Qty.OptionsColumn.AllowEdit = False
                Next

                GridView1.MoveLast()


            ElseIf cbType.EditValue = "�Ͳ��X�f" Then

                Dim poi As List(Of Production.ProuctionWareOut.ProductionWareOutInfo)
                Dim poc As New Production.ProuctionWareOut.ProductionWareOutControl

                poi = poc.ProductionWareOut_GetList(txtOPNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If poi.Count = 0 Then
                    MsgBox("���s�b���X�f�渹,�нT�{!")
                    txtOPNO.EditValue = ""
                    txtOPNO.Enabled = True
                    Exit Sub
                End If

                Dim j As Integer
                Dim row As DataRow

                For j = 0 To poi.Count - 1

                    row = ds.Tables("WareOut").NewRow

                    row("WO_NUM") = Nothing
                    row("WO_ID") = Nothing
                    row("M_Code") = poi(j).M_Code
                    row("M_Name") = poi(j).M_Name

                    Dim unit As New LFERP.DataSetting.UnitController
                    Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

                    unitinfo = unit.GetUnitList(poi(j).M_Unit)

                    row("M_Unit") = unitinfo(0).U_ID
                    row("M_Gauge") = poi(j).M_Gauge
                    row("OS_BatchID") = ""
                    row("WO_Qty") = poi(j).PWO_Qty
                    row("M_ID") = ""
                    row("Temp_Code") = ""

                    '2012-7-16 �[�J��ܦw���w�s�P�`�E��-----------------------------
                    Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                    Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                    wi1 = wc1.WareInventory_GetSub(poi(j).M_Code, strWHID)


                    If wi1 Is Nothing Then
                        row("WI_SafeQty_Show") = 0
                        row("WO_EndQty_Show") = 0
                    Else
                        row("WI_SafeQty_Show") = wi1.WI_SafeQty
                        row("WO_EndQty_Show") = wi1.WI_Qty
                    End If

                    ds.Tables("WareOut").Rows.Add(row)
                    txtOPNO.Enabled = False
                Next

                GridView1.MoveLast()

            End If
        End If

    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        '        Dim api As List(Of ApplyInfo)
        '        Dim apc As New ApplyControl
        '        '-----------------------------------�ܧ�ӻ�O���H�e�P�_��e�ӻ�渹�O�_�s�b���[ñ�O��--------
        '        Dim ei As List(Of EndorsementInfo)
        '        Dim ec As New EndorsementControl

        '        ei = ec.Endorsement_GetList(txtAPNO.Text, Nothing, Nothing)
        '        If ei.Count = 0 Then
        '            GoTo Sucess
        '        Else
        '            If ei(0).AM_Type = "�f�ֳq�L" And ei(0).AM_Check = True Then
        '                GoTo Sucess
        '            ElseIf ei(0).AM_Type = "�f�ְh�^" And ei(0).AM_Check = True Then
        '                MsgBox("���ӻ��[ñ�O���Q�h�^,�����\�o�f!")
        '                Exit Sub
        '            Else
        '                MsgBox("���ӻ��s�b�[ñ�O���B���f��,�����\�o�f!")
        '                Exit Sub
        '            End If

        '        End If

        '        '-----------------------------------------------------------------------------------------------

        'Sucess: api = apc.Apply_GetList(txtAPNO.Text, Nothing, Nothing, Nothing)
        '        If api.Count = 0 Then
        '            MsgBox("��e�����t�Τ��s�b��e�ӻ�渹,�нT�{!")
        '            popWareInput.Enabled = True

        '            Exit Sub
        '        Else
        '            popWareInput.Enabled = False
        '            tempValue = txtAPNO.Text
        '            Dim frm As New frmWareOutLoadingApply

        '            frm.ShowDialog()

        '            Dim i, n As Integer
        '            Dim arr(n) As String
        '            arr = Split(tempValue, ",")
        '            n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)
        '            For i = 0 To n
        '                If arr(i) = "" Then
        '                    Exit Sub
        '                End If
        '                AddRowApply(txtAPNO.Text, arr(i))
        '            Next
        '            tempValue = ""
        '        End If

        If GridView1.RowCount = 0 Then
            'txtAPNO.Text = Nothing
        End If

        tempValue3 = strWHID   '�ǤJ�ܮw�s���H��
        Dim frm As New frmWareOutLoadingApply
        frm.ShowDialog()

        '2012-7-16
        If tempValue = "" Or tempValue = Nothing Then
            Exit Sub
        End If

        'popWareInput.Enabled = False
        popWareOutAdd.Enabled = False
        popWareOutBarAdd.Enabled = False
        popLoadBatchAdd.Enabled = False
        popLoadBatchAllAdd.Enabled = False

        popWareOutDel.Enabled = True

        'If Len(txtAPNO.Text.Trim) = 0 Then
        '    txtAPNO.Text = tempValue2
        'Else
        '    If txtAPNO.Text.Equals(tempValue2) = False Then
        '        MsgBox("�b�P�@�X�w�渹�������\�X�{���P�ӻ�渹,�Э��s�}�X�w��!")
        '        Exit Sub
        '    End If
        'End If
        'txtAPNO.Enabled = False

        '-----------------------------------�ܧ�ӻ�O���H�e�P�_��e�ӻ�渹�O�_�s�b���[ñ�O��--------
        Dim ei As List(Of EndorsementInfo)
        Dim ec As New EndorsementControl

        ei = ec.Endorsement_GetList(tempValue2, Nothing, Nothing)
        If ei.Count = 0 Then
            GoTo Sucess
        Else
            If ei(0).AM_Type = "�f�ֳq�L" And ei(0).AM_Check = True Then
                GoTo Sucess
            ElseIf ei(0).AM_Type = "�f�ְh�^" And ei(0).AM_Check = True Then
                MsgBox("���ӻ��[ñ�O���Q�h�^,�����\�o�f!")
                Exit Sub
            Else
                MsgBox("���ӻ��s�b�[ñ�O���B���f��,�����\�o�f!")
                Exit Sub
            End If

        End If

        '-----------------------------------------------------------------------------------------------

Sucess: Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(tempValue, ",")
        n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)
        For i = 0 To n
            If arr(i) = "" Then
                Exit Sub
            End If
            AddRowApply(tempValue2, arr(i))
        Next
        tempValue = ""
        tempValue2 = ""

    End Sub

  

    Sub AddRowApply(ByVal AM_ID As String, ByVal M_ID As String)
        Dim row As DataRow
        row = ds.Tables("WareOut").NewRow

        If M_ID = "" Then
        Else
            Dim api As List(Of ApplyInfo)
            Dim apc As New ApplyControl
            api = apc.Apply_GetList(AM_ID, M_ID, Nothing, Nothing)
            If api.Count = 0 Then
                Exit Sub
            Else

                Dim j As Integer

                For j = 0 To ds.Tables("WareOut").Rows.Count - 1
                    If api(0).M_Code = ds.Tables("WareOut").Rows(j)("M_Code") Then
                        MsgBox("����" & ds.Tables("WareOut").Rows(j)("M_Code") & " / " & ds.Tables("WareOut").Rows(j)("M_Name") & "�X�w�椤�w�s�b�C" & vbCrLf & "�P�@�i�X�w�椤�����\�s�b�ۦP���X�w���ơI", 64, "����")
                        Exit Sub
                    End If
                Next

                row = ds.Tables("WareOut").NewRow


                row("WO_NUM") = Nothing
                row("WO_ID") = Nothing
                row("M_Code") = api(0).M_Code
                row("M_Name") = api(0).M_Name
                row("M_Gauge") = api(0).M_Gauge
                row("M_Unit") = api(0).M_Unit
                row("WO_Qty") = api(0).M_Qty
                row("OS_BatchID") = ""
                row("WO_Remark") = ""
                row("AP_NO") = AM_ID
                row("M_ID") = api(0).M_ID
                row("Temp_Code") = api(0).Temp_Code
                row("M_SendQty") = api(0).M_SendQty
                row("PS_NO") = ""

                '2012-7-16 �[�J��ܦw���w�s�P�`�E��-----------------------------
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(api(0).M_Code, strWHID)


                If wi1 Is Nothing Then
                    row("WI_SafeQty_Show") = 0
                    row("WO_EndQty_Show") = 0
                Else
                    row("WI_SafeQty_Show") = wi1.WI_SafeQty
                    row("WO_EndQty_Show") = wi1.WI_Qty
                End If

                ds.Tables("WareOut").Rows.Add(row)

                GridView1.MoveLast()
            End If
        End If
    End Sub
    '�ܧ��e��ܦ檫�ƽs�X�H��
    Private Sub RepositoryItemButtonEdit1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEdit1.ButtonClick
        Dim strTemp, strCode As String
        strTemp = GridView1.GetFocusedRowCellValue("M_Code").ToString
        tempValue5 = strWHID
        tempValue6 = "�ܮw�޲z"
        tempValue7 = "�X�w�@�~"

        Dim frm As New frmBOMSelect
        frm.ShowDialog()

        If tempCode = "" Then
        Else
            'AddRow(tempCode, 0)

            Dim j As Integer

            For j = 0 To ds.Tables("WareOut").Rows.Count - 1
                If j <> GridView1.FocusedRowHandle Then
                    If tempCode = ds.Tables("WareOut").Rows(j)("M_Code") Then
                        MsgBox("���Ƥw�s�b�A�P�@�i�X�w�椤�����\�s�b�ۦP���X�w���ơI")
                        Exit Sub
                    End If
                End If
            Next

            strCode = tempCode  '�s�����ƽs�X

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)

            Dim unit As New LFERP.DataSetting.UnitController
            Dim unitinfo As List(Of LFERP.DataSetting.UnitInfo)

            unitinfo = unit.GetUnitList(objInfo.M_Unit)

            GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "M_Code", strCode) '�s���u��o�X�����ƽs�X
            GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "M_Name", objInfo.M_Name) '�s���W��
            GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "M_Gauge", objInfo.M_Gauge) '�s���W��
            GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "M_Unit", unitinfo(0).U_Name) '�s�����
            GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "M_SendQty", 0) '�w�o�ƶq
            GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "Temp_Code", strTemp) '�ӻ�椤�~�H�����u��s�X(�ܧ��{�ɽs�X�r�q���O�s)
            '-------------------------------------------------------
            '�o���e���Ʀb���w�ܮw���w���w�s�]�m�H���H�η�e��ɮw�s�H��

            Dim wi As List(Of WareInventory.WareInventoryInfo)
            Dim wic As New WareInventory.WareInventoryMTController

            wi = wic.WareInventory_GetList3(strCode, strWHID, "True")
            If wi.Count = 0 Then
                GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "WI_SafeQty_Show", 0) '�s���u��o�X�����ƽs�X
                GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "WO_EndQty_Show", 0) '�s���W��
            Else
                GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "WI_SafeQty_Show", wi(0).WI_SafeQty) '�s���u��X�w���Ʀw���w�s
                GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "WO_EndQty_Show", wi(0).WI_Qty) '�s�����ƭܮw�w�s
            End If


            '-------------------------------------------------------

        End If
        tempCode = ""

    End Sub

    Function CheckSave() As Boolean

        CheckSave = True

        ''--------------------2012-12-17--����N����d--------------------------------------
        If sk.Visible = False Then '�q���ܤ���
        Else

            Dim wulc As New WhiteUserListController
            Dim wuliL As New List(Of WhiteuserListInfo)
            wuliL = wulc.WhiteUserList_GetList(TextEdit1.Text, Mid(strWHID, 1, 3))
            If wuliL.Count <= 0 Or TextEdit1.Text = "" Then
                CheckSave = False
                MsgBox("����d�ΫD�k�d��")
                Exit Function
            End If

        End If
        ''------------------------------------------------------------

        If Len(ButtonEdit2.EditValue) = 0 Then

            MsgBox("�������ର��")
            CheckSave = False
            Exit Function
        End If
        If Len(TextEdit1.EditValue) = 0 Then
            MsgBox("��ƤH���ର��")
            CheckSave = False
            Exit Function
        End If
        If ds.Tables("WareOut").Rows.Count = 0 Then
            MsgBox("�п�ܪ���")
            CheckSave = False
            Exit Function
        End If

        If isProcess = True Then
            If gluDepID.Text.Trim = "" Then
                MsgBox("���Ƴ������ର��")
                CheckSave = False
                Exit Function
            End If

            Dim n%
            For n = 0 To ds.Tables("WareOut").Rows.Count - 1
                If ds.Tables("WareOut").Rows(n)("PS_NO") = "" Then
                    MsgBox("�п�J�u�ǦW�١I", 64, "����")
                    CheckSave = False
                    Exit Function
                End If
            Next
        End If
        Dim i As Integer

        '�d�߬O�_�������ܮw������
        Dim mw As New WareInventory.WareInventoryMTController
        Dim mwi As New WareInventory.WareInventoryInfo

        Dim pic As New ProductInventoryController
        Dim piiGet As List(Of ProductInventoryInfo)

        Dim dblInventoryQty As Double '�w�s��
        Dim intRecords As Integer '�O����

        For i = 0 To ds.Tables("WareOut").Rows.Count - 1

            If AllPlus = True Then
                If ds.Tables("WareOut").Rows(i)("WO_Qty") = 0 Then
                    MsgBox("�X�w�ƶq���ର0!")
                    CheckSave = False
                    Exit Function
                End If
            Else
                If ds.Tables("WareOut").Rows(i)("WO_Qty") <= 0 Then
                    MsgBox("�X�w�ƶq����p�_���_0!")
                    CheckSave = False
                    Exit Function
                End If

            End If




            If strWHID = "W1101" Then        '@ 2013/3/11 �ק�
                piiGet = pic.ProductInventory_GetList(ds.Tables("WareOut").Rows(i)("PM_M_Code"), ds.Tables("WareOut").Rows(i)("M_Code"), strWHID, Nothing)
                If piiGet.Count <= 0 Then
                    intRecords = 0
                    dblInventoryQty = 0
                Else
                    intRecords = piiGet.Count
                    dblInventoryQty = piiGet(0).PI_Qty
                End If
            Else
                mwi = mw.WareInventory_GetSub(ds.Tables("WareOut").Rows(i)("M_Code"), strWHID)
                If mwi Is Nothing Then
                    intRecords = 0
                    dblInventoryQty = 0
                Else
                    intRecords = 1
                    dblInventoryQty = mwi.WI_Qty
                End If
            End If

            If intRecords = 0 Then
                MsgBox("����" & ds.Tables("WareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�����s�b�A����O�s�I")
                CheckSave = False
                Exit Function
            End If

            '@ 2012/10/9 �K�[
            If cbType.EditValue = "�Ͳ��}��" Then
                'If (CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty")) + CDbl(ds.Tables("WareOut").Rows(i)("M_SendQty"))) > CDbl(ds.Tables("WareOut").Rows(i)("C_Qty")) Then
                '    MsgBox("�X�w�ƶq�P�w�o�ƶq���M����j��}�Ƽƶq�I", 64, "����")
                '    Exit Function
                'End If


                Dim pli As New List(Of ProductionKaiLiaoInfo)
                Dim plc As New ProductionKaiLiaoControl

                pli = plc.ProductionKaiLiao_GetList(txtOPNO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing)
                If pli.Count = 0 Then
                    MsgBox("���s�b���}�Ƴ渹�Φ��}�Ƴ楼�f��!")
                    CheckSave = False
                    Exit Function
                End If

                '--------------------@ 2012/10/9 �K�[�@�P�_�}�Ƴ�O�_�w�����o��------
                Dim woc As New WareOutController
                Dim woi As New List(Of WareOutInfo)
                woi = woc.WareOut_GetQty3(Nothing, Nothing, txtOPNO.Text.Trim)

                If woi.Count > 0 Then
                    If pli(0).C_Weight - woi(0).AllQty < CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty")) Then
                        MsgBox("���}�Ƴ�w�����o�ơA����A�o�ơI", 64, "����")
                        CheckSave = False
                        Exit Function
                    End If
                End If
                '---------------------------------------------------------------------------------------------------
            End If

            If dblInventoryQty = 0 Then

                If CSng(ds.Tables("WareOut").Rows(i)("WO_Qty")) < 0 Then
                    CheckSave = True
                ElseIf dblInventoryQty - CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty")) >= 0 Then
                    CheckSave = True
                Else
                    MsgBox("��e���Ʈw�s��0,�����\�X�w������,�u��i��|�p���t�ƨR�b�B�z")
                    CheckSave = False
                    Exit Function
                End If
            ElseIf dblInventoryQty > 0 And dblInventoryQty < ds.Tables("WareOut").Rows(i)("WO_Qty") Then
                MsgBox("����" & ds.Tables("WareOut").Rows(i)("M_Code") & " �b�ܮw" & txtWH.EditValue & "�������ơA����O�s�I")
                CheckSave = False
                Exit Function
            ElseIf dblInventoryQty < 0 Then

                If dblInventoryQty - CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty")) >= 0 Then
                    CheckSave = True
                Else
                    MsgBox("��e����" & ds.Tables("WareOut").Rows(i)("M_Code") & "�w�s���t��,���ˬd��]!")
                    CheckSave = False
                    Exit Function
                End If

                If isProcess = True Then
                    If ds.Tables("WareOut").Rows(i)("PS_NO") = "" Then
                        MsgBox("�п�J�u�ǦW�١I", 64, "����")
                        CheckSave = False
                        Exit Function
                    End If
                End If

            Else
                CheckSave = True
            End If

            ''2012-9-4 �P�_�H�ӻ��A�X�w�����p--------------------------------

            If ds.Tables("WareOut").Rows(i)("M_ID") = "" Then
            Else
                Dim ai2 As New List(Of ApplyInfo)
                Dim ac2 As New ApplyControl
                ai2 = ac2.Apply_GetList(ds.Tables("WareOut").Rows(i)("AP_NO"), ds.Tables("WareOut").Rows(i)("M_ID"), Nothing, Nothing)

                If ai2.Count > 0 Then
                    If ai2(0).M_Code = ds.Tables("WareOut").Rows(i)("M_Code") Then
                        If CDbl(ds.Tables("WareOut").Rows(i)("WO_Qty")) > ai2(0).M_Qty - ai2(0).M_SendQty Then
                            MsgBox("�X�w�ƶq����j�_�ӻ⤤���o�X�ƶq�I", 64, "����")
                            CheckSave = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            '��----------------------------------------


        Next
    End Function

    '@ 2012/7/27 �K�[
    Private Sub RepositoryItemButtonEdit3_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEdit3.ButtonClick
        tempCode = ""
        tempValue2 = "���ƥX�w��u��"
        Dim fr As New frmProductionSelect
        fr.ShowDialog()
        '�W�[�O��
        If tempCode = "" Then
            Exit Sub
        Else
            Dim i%
            For i = 0 To ds.Tables("WareOut").Rows.Count - 1
                If ds.Tables("WareOut").Rows(i)("PS_NO") = tempCode Then
                    MsgBox("�u�Ǥw�s�b�A�P�@�i�椤����s�b�ۦP���u�ǡI", 64, "����")
                    Exit Sub
                End If
            Next
            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("PS_NO") = tempCode
            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("PS_Name") = tempValue9
            sender.text = tempValue9
        End If
        tempCode = ""
        tempValue9 = ""
    End Sub

    '��e���\�b�P�@�i�ӻ�椤�K�[���P���ӻ��H��

    Private Sub popApplyAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popApplyAdd.Click


        'If GridView1.RowCount = 0 Then
        '    txtAPNO.Text = Nothing
        'End If
        arl1.Clear()
        arl2.Clear()

        tempValue3 = strWHID   '�ǤJ�ܮw�s���H��
        Dim frm As New frmWareOutLoadingApply
        frm.ShowDialog()

        'AP_NO.OptionsColumn.AllowEdit = False
        'AP_NO.OptionsColumn.ReadOnly = False

        'If tempValue = "" Or tempValue = Nothing Then
        '    Exit Sub
        'End If

        'popWareInput.Enabled = False
        'popWareOutAdd.Enabled = False
        'popWareOutBarAdd.Enabled = False
        'popLoadBatchAdd.Enabled = False
        'popLoadBatchAllAdd.Enabled = False

        'popWareOutDel.Enabled = True


        Dim i, j As Integer
        If arl1.Count > 0 Then
            For i = 0 To arl1.Count - 1
                AddRowApply(arl1(i), arl2(i))
            Next
        End If

 
        arl1.Clear()
        arl2.Clear()
    End Sub

    Private Sub rtxtAP_NO_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxtAP_NO.Enter
        If cbType.Text <> "�Ͳ��}��" Then
            If ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_ID").ToString <> "" Then
                'AP_NO.OptionsColumn.ReadOnly = True
                sender.Properties.ReadOnly = True
            Else
                'AP_NO.OptionsColumn.ReadOnly = False
                sender.Properties.ReadOnly = False
            End If
        End If
    End Sub

    Private Sub txtAPNO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAPNO.KeyPress

        If e.KeyChar = Chr(13) Then

            If GridView1.RowCount = 0 Then Exit Sub
            Dim i As Integer

            For i = 0 To ds.Tables("WareOut").Rows.Count - 1

                GridView1.SetRowCellValue(i, "AP_NO", txtAPNO.Text)

            Next

        End If

    End Sub

    '@ 2012/10/12 �K�[ �ӻ���J�奻�إ��h�J�I�ɡA�P�_�ӻ��O�_�s�b�A�s�b�h���������H��
    Private Sub rtxtAP_NO_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxtAP_NO.Leave
        If sender.text <> "" And ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Code") <> "" Then
            Dim api As List(Of ApplyInfo)
            Dim apc As New ApplyControl
            api = apc.Apply_GetList(sender.text, Nothing, GridView1.GetFocusedRowCellValue(M_Code), Nothing)
            If api.Count > 0 Then
                ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_ID") = api(0).M_ID
                ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("Temp_Code") = api(0).Temp_Code
                ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_SendQty") = api(0).M_SendQty
            End If
        End If
    End Sub

   
    'Private Sub RepositoryItemButtonEdit1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemButtonEdit1.Leave
    '    If sender.text <> "" And ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("AP_NO") <> "" Then
    '        Dim api As List(Of ApplyInfo)
    '        Dim apc As New ApplyControl
    '        api = apc.Apply_GetList(GridView1.GetFocusedRowCellValue(AP_NO), Nothing, sender.text, Nothing)
    '        If api.Count > 0 Then
    '            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_ID") = api(0).M_ID
    '            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("Temp_Code") = api(0).Temp_Code
    '            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_SendQty") = api(0).M_SendQty
    '        Else
    '            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_ID") = ""
    '            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("Temp_Code") = ""
    '            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_SendQty") = 0
    '        End If
    '    End If
    'End Sub


    '@ 2013/1/25 �K�[ ���U�^����եβK�[�ƾڦ�ƥ�,���X���y�����y�X�����X�۱a�^�����
    Private Sub txtM_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        ''If e.KeyCode = Keys.Enter Then

        ''    If strPdMachine = True Then


        ''        Dim strCode As String
        ''        Dim strQty As String
        ''        Dim intIn As Integer
        ''        Dim StrText As String

        ''        StrText = txtM_Code.Text

        ''        intIn = InStr(StrText, ",", CompareMethod.Text)
        ''        If intIn <= 0 Then
        ''            strCode = StrText
        ''            strQty = "1"
        ''        Else
        ''            strCode = Mid(StrText, 1, intIn - 1)
        ''            strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
        ''        End If


        ''        LabelMsg.Text = LabelMsg.Text + AddRowPD(strCode, strQty)

        ''        txtM_Code.Text = ""
        ''    Else
        ''        AddRow(txtM_Code.Text.Trim, 1)
        ''        txtM_Code.Text = ""
        ''    End If

        ''End If


        If e.KeyCode = Keys.Enter Then

            Dim strCode As String
            Dim strQty As String
            Dim intIn As Integer
            Dim StrText As String

            StrText = txtM_Code.Text

            intIn = InStr(StrText, ",", CompareMethod.Text)
            If intIn <= 0 Then
                strCode = StrText
                strQty = "1"
            Else
                strCode = Mid(StrText, 1, intIn - 1)
                strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
            End If


            LabelMsg.Text = LabelMsg.Text + AddRowPD(strCode, strQty)

            txtM_Code.Text = ""

        End If
    End Sub

    '@ 2013/1/25 �K�[
    Private Sub txtM_Code_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyUp
        If GridView1.RowCount <= 0 Then Exit Sub

       ' If strPdMachine = True Then Exit Sub ''�L�I���Ҧ�


        If e.KeyCode = Keys.Home Then   '���UHome��A�J�I��ƶq�椸����o�J�I
            GridView1.Focus()
            GridView1.FocusedColumn = GridView1.Columns("WO_Qty")

        End If

        If e.KeyCode = Keys.PageUp Then    '���UPageUp��A�J�I��ƶq�[�@
            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_Qty") = ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_Qty") + 1
            LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_Qty") & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Unit"), "")
        End If

        If e.KeyCode = Keys.PageDown Then    '���UPageDown��A�J�I��ƶq��@
            ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_Qty") = ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_Qty") - 1
            LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_Qty") & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Unit"), "")
        End If

        If e.KeyCode = Keys.Up Then    '���U�V�W��V��A�J�I��W���@��A�åB�J�I���b�ƶq�椸�椤
            GridView1.FocusedRowHandle = GridView1.FocusedRowHandle - 1
            GridView1.Focus()
            GridView1.FocusedColumn = GridView1.Columns("WO_Qty")
        End If

        If e.KeyCode = Keys.Down Then   '���U�V�U��V��A�J�I��U���@��A�åB�J�I���b�ƶq�椸�椤
            GridView1.FocusedRowHandle = GridView1.FocusedRowHandle + 1
            GridView1.Focus()
            GridView1.FocusedColumn = GridView1.Columns("WO_Qty")
        End If
    End Sub

    '@ 2013/1/25 �K�[
    'Private Sub GridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        txtM_Code.Focus()
    '    End If
    'End Sub

    ''2013-3-5 �u�ק�ƪ`
    Sub Update_Remark()
        Dim mcc As New WareOutController
        Dim mii As New WareOutInfo

        mii.WO_ID = txtWIPID.EditValue
       
        Dim i As Integer

        For i = 0 To ds.Tables("WareOut").Rows.Count - 1
            mii.WO_NUM = ds.Tables("WareOut").Rows(i)("WO_NUM")
            mii.WO_Remark = ds.Tables("WareOut").Rows(i)("WO_Remark")

            If mcc.WareOut_UpdateRemark(mii) = False Then
                Exit Sub
            End If
        Next

        MsgBox("�O�s���\!")
        Me.Close()
    End Sub

    '@ 2013/3/14 �ƶq�ק�ɡALCD�̹����s��ܪ��ƫH��
    Private Sub QtyItemCalcEdit_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles QtyItemCalcEdit.EditValueChanged
        If isShowLCD = True Then   '������v���ɤ~�i���
            LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Gauge"), "�ƶq�G" & CDbl(sender.text) & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Unit"), "")
        End If
    End Sub

    '@ 2013/1/25 �K�[
    Private Sub QtyItemCalcEdit_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles QtyItemCalcEdit.KeyUp
        If e.KeyCode = Keys.Enter Then
            txtM_Code.Focus()
        End If
    End Sub

    Private Sub txtM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtM_Code.EditValueChanged

    End Sub
    Private Sub txtM_Code_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtM_Code.Leave
        If LabelMsg.Text <> "" Then
            MsgBox(LabelMsg.Text)
        End If
    End Sub

    Private Sub ToolStripBatchLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBatchLoad.Click
        tempCode = ""
        tempValue12 = "B"
        '--------------------------------
        tempValue5 = strWHID
        tempValue6 = "�ܮw�޲z"
        tempValue7 = "�X�w�@�~"

        frmBOMSelect.ShowDialog()


        If tempValue = Nothing Or tempValue = "" Then
            Exit Sub
        End If

        Dim i, n As Integer
        Dim arr(n) As String
        arr = Split(tempValue, ",")
        n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)
        For i = 0 To n
            If arr(i) = "" Then
                Exit Sub
            End If
            AddRow(arr(i), 0)
        Next

        tempValue = Nothing
        tempValue12 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
    End Sub

    '@ 2013/6/8 �K�[ �J�I����ܮɡALCD�̹���ܵJ�I�檫�ƫH��
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If isShowLCD = True Then   '������v���ɤ~�i���
            LoadPingMU("�W�١G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Name"), "�W��G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Gauge"), "�ƶq�G" & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("WO_Qty") & ds.Tables("WareOut").Rows(GridView1.FocusedRowHandle)("M_Unit"), "")
        End If
    End Sub

 
End Class