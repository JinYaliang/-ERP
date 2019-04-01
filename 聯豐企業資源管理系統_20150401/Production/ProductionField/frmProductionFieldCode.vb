
Imports LFERP.Library.ProductionField
Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.Product
Imports LFERP.Library.WareHouse
Imports LFERP.Library.ProductionWareHouse
Imports LFERP.Library.ProductionSchedule
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.ProductionFieldType
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.ProductionKaiLiao
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.Production.ProductionFieldDaySummary
Imports System.Threading
Imports LFERP.Library.Production.ProductionAffair
Imports LFERP.Library.Production.Datasetting
Imports LFERP.Library.Production.ProductionProductInner


Public Class frmProductionFieldCode

    Dim uc As New SystemUser.SystemUserController
    Dim dc As New DepartmentControler
    Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim mc As New ProductionDataSettingControl

    Dim ds As New DataSet
    Dim upi As List(Of UserPowerInfo)
    Dim upc As New UserPowerControl

    Dim OldCheck As Boolean
    Dim strSecond As String
    Dim DeclareQty As Double

    Dim DeclareQtyAll As Double ''2013-5-16�� �j�f+��ת������ƶq

    Dim strIndex As Integer
    Dim AutoSchedule As Boolean = False


    Sub LoadPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "880413")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then AutoSchedule = True

        End If
    End Sub

    '@ 2012/2/22 ���L�{�Ȯɤ���
    'Sub LoadProductNo()  '���~�s��
    '    Dim mi As List(Of ProductionDataSettingInfo)
    '    mi = mc.ProductionUser_GetList(GluDep.EditValue, Nothing)
    '    If mi.Count > 0 Then
    '        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
    '        PM_M_Code.Properties.ValueMember = "PM_M_Code"
    '        PM_M_Code.Properties.DataSource = mc.ProductionUser_GetList(GluDep.EditValue, Nothing)

    '        gluProduct.Properties.DisplayMember = "PM_M_Code"
    '        gluProduct.Properties.ValueMember = "PM_M_Code"
    '        gluProduct.Properties.DataSource = mc.ProductionUser_GetList(GluDep.EditValue, Nothing)
    '    Else
    '        Dim mpc As New ProductController
    '        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
    '        PM_M_Code.Properties.ValueMember = "PM_M_Code"
    '        PM_M_Code.Properties.DataSource = mpc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    '        gluProduct.Properties.DisplayMember = "PM_M_Code"
    '        gluProduct.Properties.ValueMember = "PM_M_Code"
    '        gluProduct.Properties.DataSource = mpc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    '    End If
    'End Sub

    Sub LoadProductionDetail() ' �ݩ�
        Dim mc As New ProductionFieldTypeControl
        GluDetail.Properties.DisplayMember = "PT_Type"
        GluDetail.Properties.ValueMember = "PT_NO"
        GluDetail.Properties.DataSource = mc.ProductionFieldType_GetList(Nothing, Nothing, "�o��/����")
    End Sub

    '@ 2012/2/22 �ק� ����Ƴ����ƾڥ[��
    Sub LoadDepartment()   '�����H��
        GluDep.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)  '�ܧ󳡪�
        GluDep.Properties.DisplayMember = "DepName"
        GluDep.Properties.ValueMember = "DepID"

        'Dim mi As List(Of ProductionDataSettingInfo)
        'mi = mc.ProductionIncome_GetList(GluDep.EditValue, Nothing)
        'If mi.Count > 0 Then
        '    gluChangeDep.Properties.DataSource = mc.ProductionIncome_GetList(GluDep.EditValue, Nothing)  '�ܧ󳡪�
        '    gluChangeDep.Properties.DisplayMember = "DepName"
        '    gluChangeDep.Properties.ValueMember = "DepID"
        'Else
        '    gluChangeDep.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)   '�ܧ󳡪�
        '    gluChangeDep.Properties.DisplayMember = "DepName"
        '    gluChangeDep.Properties.ValueMember = "DepID"
        'End If
    End Sub

    Private Sub frmProductionFieldCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%
        '@ 2012/5/14 �K�[ �P�_�O�_�ݭn��d
        If strRefCard = "�O" Then
        Else
            SimpleButton1.Visible = False
            TextEdit1.Visible = False
            SimpleButton2.Visible = False
        End If

        CreateTable()
        LoadPower()

        Label17.Text = tempValue4  '�渹
        Label18.Text = tempValue   '�ާ@����

        Label30.Text = tempValue5
        Label35.Text = tempValue6
        Label36.Text = tempValue10  '�O�_���~��

        'LoadProductNo()  '���~�s��
        LoadDepartment() '�o�Ƴ����H��
        GluDep.EditValue = Label30.Text
        GluDep.Enabled = False

        LoadPM_M_Code()     '@2012/2/22 �ק� ���~�s���ե�
        LoadDep()   '@2012/2/22 �K�[ ���Ƴ����H���ե�
        LoadProductionDetail()  '�ݩʫH��

        tempValue5 = ""
        tempValue6 = ""
        tempValue = ""
        tempValue4 = ""
        tempValue10 = ""
        cbType.Enabled = False

        XtraTabPage2.PageVisible = False '�Ȯɤ��ݼf��

        '���m�s��d��
        'Try
        If strRefCard = "�O" Then
            Dim reset As New ResetPassWords.SetPassWords
            reset.SetPassWords()
            'Catch
            'End Try
        End If

        If Label36.Text = "���~��" Then
            gluChangeDep.EditValue = "W1101"
            gluChangeDep.Enabled = False

            cboFP_OutType.Visible = True    '@ 2012/7/27 �K�[ �o���~�ܮ���ܥ~�o����
            Label37.Visible = True
            cboFP_OutType.SelectedIndex = 0
        Else
            gluChangeDep.EditValue = ""
            gluChangeDep.Enabled = True
        End If

        Select Case Label18.Text
            Case "CodeOut"  '���Ʀ��o
                If Edit = False Then
                    txtNo.Enabled = False
                    btnAdd.Enabled = True

                    Dim lockobj As New Object()
                    '�ݭn��w���N�X�� 
                    SyncLock lockobj
                        Thread.Sleep(Int(Rnd() * 400) + 100)
                        txtNo.Text = GetNO()
                    End SyncLock

                    upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

                    If upi.Count = 0 Then Exit Sub

                    GluDetail.EditValue = tempValue2

                    cbType.EditValue = upi(0).UserType
                    cbType.Enabled = False

                    If GluDetail.EditValue = "PT14" Then
                        If GluDep.EditValue = "F101" Then
                            gluChangeDep.Enabled = True
                            cbType.Enabled = True

                        Else
                            gluChangeDep.EditValue = "F101"
                            gluChangeDep.Enabled = False
                        End If
                        cboFP_OutType.Visible = True    '@ 2012/7/27 �ק� �~�o����ܥ~�o����
                        Label37.Visible = True
                        cboFP_OutType.SelectedIndex = 0

                        '2013-6-1
                        '  CheckEdit3.Visible = True

                    ElseIf GluDetail.EditValue = "PT13" And Label36.Text <> "���~��" Then '@ 2012/6/20 �K�[�@���u����ܤ��\����ק�ƴ_���
                        CheckEdit3.Visible = True
                        'Else
                        '    GluDep.EditValue = Label30.Text
                        '    GluDep.Enabled = False
                    End If

                    ComboBoxEdit1.EditValue = "���`"
                    tempValue2 = ""
                    DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                    Label23.Text = Format(Now, "HH:mm:ss")
                    Label23.Visible = False
                Else
                    LoadData(Label17.Text)
                    Me.Text = "�ק�--" & Label17.Text
                    Label23.Visible = True
                    If GluDetail.EditValue = "PT14" Then        '@ 2012/6/12 �K�[ �~�o�ɦ��Ƴ��������\�ק�
                        If gluChangeDep.EditValue = "F101" Then
                            gluChangeDep.Enabled = False
                        End If
                    End If
                End If
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "PreView"  '�d��
                LoadData(Label17.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                Label23.Visible = True
                cmdSave.Visible = False
                Me.Text = "�d��--" & Label17.Text
            Case "InCheck"  '���ƽT�{
                LoadData(Label17.Text)

                Button1.Visible = True
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                CheckEdit1.Enabled = True

                For i = 0 To XtraTabPage1.Controls.Count - 2
                    XtraTabPage1.Controls(i).Enabled = False
                Next
                Panel1.Enabled = False

                Label23.Visible = True
                Me.Text = "�T�{--" & Label17.Text
                CheckEdit3.Enabled = False    '@ 2012/6/20 �K�[
                chkFP_OutOK.Enabled = False    '@ 2012/7/12 �K�[
                'Case "CodeCheck"
                '    LoadData(Label17.Text)
                '    XtraTabControl1.SelectedTabPage = XtraTabPage2
        End Select
        GluDetail.Enabled = False
        'If (GluDep.EditValue = "F101" And GluDetail.EditValue = "PT14") Or GluDetail.EditValue = "PT13" Then
        If GluDetail.EditValue = "PT13" Then
            ' Button1.Visible = False '2013-10-28
        End If
        If GluDep.EditValue = "F101" Then
            chkFP_OutOK.Visible = False
        End If
        If GluDetail.EditValue = "PT02" Then
            LabelAll.Visible = True
            LabelAllQty.Visible = True
        End If
    End Sub
    '�Ыت�
    Sub CreateTable()
        '�Ы�������
        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With

        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")

        '�Ы�"�Բ�"���s��������
        With ds.Tables.Add("ProductType1")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType1.Properties.ValueMember = "PM_Type"
        gluType1.Properties.DisplayMember = "PM_Type"
        gluType1.Properties.DataSource = ds.Tables("ProductType1")

        '�Ыصo�Ƥu�ǽs����
        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        GluEdit1.Properties.ValueMember = "PS_NO"
        GluEdit1.Properties.DisplayMember = "PS_Name"
        GluEdit1.Properties.DataSource = ds.Tables("Process")

        '�Ыئ��Ƥu�ǽs����
        With ds.Tables.Add("Process1")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        GluEdit2.Properties.ValueMember = "PS_NO"
        GluEdit2.Properties.DisplayMember = "PS_Name"
        GluEdit2.Properties.DataSource = ds.Tables("Process1")

        '@ 2012/2/22 �K�[
        '�Ыئ��~�s����
        With ds.Tables.Add("PM_M_Code")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_JiYu", GetType(String))
        End With
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = ds.Tables("PM_M_Code")


        gluProduct.Properties.ValueMember = "PM_M_Code"
        gluProduct.Properties.DisplayMember = "PM_M_Code"
        gluProduct.Properties.DataSource = ds.Tables("PM_M_Code")

        '@ 2012/2/22 �K�[
        '�Ыئ��Ƴ�����
        With ds.Tables.Add("Dep")
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("FacName", GetType(String))
            .Columns.Add("DepName", GetType(String))
        End With
        gluChangeDep.Properties.ValueMember = "DepID"
        gluChangeDep.Properties.DisplayMember = "DepName"
        gluChangeDep.Properties.DataSource = ds.Tables("Dep")

    End Sub

    '@ 2012/2/22 �K�[
    '�[�����~�s���ƾ�
    '���L�{�Q�H�U�L�{�եΡG
    'frmProductionFieldCode_Load()
    'LoadData()

    Sub LoadPM_M_Code()
        Dim mi As List(Of ProductionDataSettingInfo)
        mi = mc.ProductionUser_GetList(GluDep.EditValue, Nothing)

        ds.Tables("PM_M_Code").Clear()

        If mi.Count > 0 Then    '�P�_�O�_���v������
            Dim row As DataRow
            Dim j As Integer
            For j = 0 To mi.Count - 1
                row = ds.Tables("PM_M_Code").NewRow
                row("PM_M_Code") = mi(j).PM_M_Code
                row("PM_JiYu") = mi(j).PM_JiYu '
                ds.Tables("PM_M_Code").Rows.Add(row)
            Next
        Else
            Dim row As DataRow
            Dim j As Integer
            'Dim mpi As List(Of ProductInfo)
            'Dim mpc As New ProductController

            Dim mpi As List(Of ProcessMainInfo)
            Dim mpc As New ProcessMainControl
            mpi = mpc.ProcessMain_GetList3(Nothing, Nothing)

            'mpi = mpc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If mpi.Count > 0 Then
                For j = 0 To mpi.Count - 1
                    row = ds.Tables("PM_M_Code").NewRow
                    row("PM_M_Code") = mpi(j).PM_M_Code
                    row("PM_JiYu") = mpi(j).PM_JiYu '
                    ds.Tables("PM_M_Code").Rows.Add(row)
                Next
            End If

        End If
    End Sub
    '@ 2012/2/22 �K�[
    '�[�����Ƴ����ƾ�
    '���L�{�Q�H�U�L�{�եΡG
    'frmProductionFieldCode_Load()
    'LoadData()
    Sub LoadDep()
        Dim mi As List(Of ProductionDataSettingInfo)
        Dim fc As New FacControler
        Dim fi As List(Of FacInfo)
        Dim row As DataRow

        mi = mc.ProductionIncome_GetList(GluDep.EditValue, Nothing)
        ds.Tables("Dep").Clear()

        If mi.Count > 0 Then        '�P�_�O�_���v������
            Dim j%
            For j = 0 To mi.Count - 1
                fi = fc.GetFacList(Microsoft.VisualBasic.Left(mi(j).FP_InDep, 1), Nothing)
                If fi.Count > 0 Then
                    row = ds.Tables("Dep").NewRow
                    row("DepID") = mi(j).FP_InDep
                    row("DepName") = mi(j).FP_InName
                    row("FacName") = fi(0).FacName
                    ds.Tables("Dep").Rows.Add(row)
                End If
            Next
        Else
            Dim pi As List(Of PersonnelInfo)
            pi = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
            Dim j%
            For j = 0 To pi.Count - 1
                row = ds.Tables("Dep").NewRow
                row("DepID") = pi(j).DepID
                row("DepName") = pi(j).DepName
                row("FacName") = pi(j).FacName
                ds.Tables("Dep").Rows.Add(row)
            Next
        End If

    End Sub

    '@ 2012/2/22 �K�[
    '�[���o�Ƥu�ǽs���ƾ�
    '���L�{�Q�H�U�L�{�եΡG
    'LoadData()
    'gluType_EditValueChanged()
    Sub LoadOutPs_Name()
        Dim mi As List(Of ProductionDataSettingInfo)
        Dim row As DataRow
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        mi = mc.ProductionIssue_GetList(GluDep.EditValue, GluDep.EditValue, cbType.Text, PM_M_Code.EditValue, gluType.EditValue, Nothing)
        ds.Tables("Process").Clear()

        If mi.Count > 0 Then        '�P�_�O�_���v������
            Dim i%
            For i = 0 To mi.Count - 1
                pci = pc.ProcessSub_GetList(Nothing, mi(i).Pro_NO, Nothing, Nothing, Nothing, True)
                If pci.Count > 0 Then
                    row = ds.Tables("Process").NewRow
                    row("PS_NO") = mi(i).Pro_NO
                    row("PS_Name") = pci(0).PS_Name

                    ds.Tables("Process").Rows.Add(row)
                End If
            Next
        Else
            pci = pc.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType.EditValue, gluType.EditValue, Nothing, True)
            Dim i As Integer
            For i = 0 To pci.Count - 1
                row = ds.Tables("Process").NewRow
                row("PS_NO") = pci(i).PS_NO
                row("PS_Name") = pci(i).PS_Name

                ds.Tables("Process").Rows.Add(row)
            Next
        End If
    End Sub

    '@ 2012/2/22 �K�[
    '�[�����Ƥu�ǽs���ƾ�
    '���L�{�Q�H�U�L�{�եΡG
    'LoadData()
    'gluChangeDep_EditValueChanged()
    Sub LoadInPs_Name()
        Dim mi As List(Of ProductionDataSettingInfo)
        Dim row As DataRow
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        mi = mc.ProductionIssue_GetList(gluChangeDep.EditValue, gluChangeDep.EditValue, cbType1.Text, gluProduct.Text, gluType1.Text, Nothing)
        ds.Tables("Process1").Clear()
        If mi.Count > 0 Then        '�P�_�O�_���v������

            Dim i%
            For i = 0 To mi.Count - 1
                pci = pc.ProcessSub_GetList(Nothing, mi(i).Pro_NO, Nothing, Nothing, Nothing, True)
                If pci.Count > 0 Then
                    row = ds.Tables("Process1").NewRow
                    row("PS_NO") = mi(i).Pro_NO
                    row("PS_Name") = pci(0).PS_Name

                    ds.Tables("Process1").Rows.Add(row)
                End If
            Next

            'MsgBox("1" & "--" & mi.Count & "--")
        Else
            pci = pc.ProcessMain_GetList(Nothing, gluProduct.EditValue, cbType1.EditValue, gluType1.EditValue, Nothing, True)
            'MsgBox(gluProduct.EditValue & cbType1.EditValue & gluType1.EditValue)
            If pci.Count = 0 Then Exit Sub
            Dim i As Integer
            For i = 0 To pci.Count - 1
                row = ds.Tables("Process1").NewRow
                row("PS_NO") = pci(i).PS_NO
                row("PS_Name") = pci(i).PS_Name

                ds.Tables("Process1").Rows.Add(row)
            Next
            ' MsgBox("2" & mi.Count & "-- " & pci.Count)
        End If

        If (GluDep.EditValue = "F101" And GluDetail.EditValue = "PT14") Or GluDetail.EditValue = "PT13" Then
            GluEdit2.EditValue = GluEdit1.EditValue

        Else

            If Edit = False Then
                Dim i%
                For i = 0 To ds.Tables("Process1").Rows.Count - 1
                    If ds.Tables("Process1").Rows(i)("PS_NO").ToString = GluEdit1.EditValue Then
                        If ds.Tables("Process1").Rows.Count = 1 Then
                            GluEdit2.EditValue = ds.Tables("Process1").Rows(0)("PS_NO").ToString
                        ElseIf ds.Tables("Process1").Rows.Count - 1 = i Then
                            GluEdit2.EditValue = ds.Tables("Process1").Rows(i)("PS_NO").ToString
                        Else
                            If gluChangeDep.EditValue = "F101" Then
                                GluEdit2.EditValue = ds.Tables("Process1").Rows(i)("PS_NO").ToString
                            Else
                                GluEdit2.EditValue = ds.Tables("Process1").Rows(i + 1)("PS_NO").ToString
                            End If
                        End If
                        Exit For
                    Else
                        GluEdit2.EditValue = ds.Tables("Process1").Rows(0)("PS_NO").ToString
                    End If
                Next

            End If

        End If

    End Sub

    Function LoadData(ByVal FP_NO As String) As Boolean
        LoadData = True
        LoadDep()   '@ 2012/2/22 �ק� ���Ƴ����ե�
        LoadPM_M_Code()     '@ 2012/2/22 �ק� ���~�s���ե�

        Dim pi As List(Of ProductionFieldInfo)
        Dim pc As New ProductionFieldControl

        pi = pc.ProductionField_GetList(FP_NO, Nothing, "����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)

        If pi.Count = 0 Then
            MsgBox("�S���ƾ�")
            LoadData = False
            Exit Function
        Else
            txtNo.Text = pi(0).FP_NO
            Label21.Text = pi(0).FP_Num

            cbType.EditValue = pi(0).Pro_Type
            PM_M_Code.EditValue = pi(0).PM_M_Code
            gluType.EditValue = pi(0).PM_Type

            cbType1.EditValue = pi(0).Pro_Type1
            gluProduct.EditValue = pi(0).PM_M_Code1
            gluType1.EditValue = pi(0).PM_Type1

            'cbType.EditValue = pi(0).Pro_Type1    ''2012-7-20 ���~�@��
            'PM_M_Code.EditValue = pi(0).PM_M_Code1
            'gluType.EditValue = pi(0).PM_Type1

            'cbType1.EditValue = pi(0).Pro_Type
            'gluProduct.EditValue = pi(0).PM_M_Code
            'gluType1.EditValue = pi(0).PM_Type

            DateEdit1.EditValue = Format(pi(0).FP_Date, "yyyy/MM/dd")
            Label23.Text = Format(pi(0).FP_Date, "HH:mm:ss")

            Label19.Text = pi(0).FP_Type

            GluDep.EditValue = pi(0).FP_InDep
            gluChangeDep.EditValue = pi(0).FP_OutDep
            cboFP_OutType.Text = pi(0).FP_OutType
            chkFP_OutOK.Checked = pi(0).FP_OutOK       '@ 2012/7/12 �K�[

            LoadOutPs_Name()
            LoadInPs_Name()

            GluEdit1.EditValue = pi(0).Pro_NO1  '�o�Ƥu��

            Button2_Click(Nothing, Nothing) '����button2�����ɶ�

            GluEdit2.EditValue = pi(0).Pro_NO '���Ƥu��

            GluDetail.EditValue = pi(0).FP_Detail

            CheckEdit3.Checked = pi(0).FP_SubtractReQty   '@2012/6/20 �K�[

            Dim di As List(Of ProductionDPTWareInventoryInfo)
            Dim dic As New ProductionDPTWareInventoryControl

            Dim strQty, strQty1 As Single

            di = dic.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)

            If di.Count > 0 Then
                strQty = di(0).WI_Qty
                strQty1 = di(0).WI_ReQty

            Else
                strQty = 0
                strQty1 = 0
            End If

            If GluDetail.EditValue = "PT02" Then
                If strQty1 = 0 Then
                    Label32.Text = strQty  '��׼ƶq��0 �ɤJ�j�f�ƶq
                    Label29.Text = "�j�f���l�ơG"
                Else
                    Label32.Text = strQty1  '��׼ƶq
                    Label29.Text = "��׵��l�ơG"
                End If

                LabelAllQty.Text = strQty + strQty1

            ElseIf GluDetail.EditValue = "PT16" Then
                Label32.Text = strQty1  '��׼ƶq
                Label29.Text = "��׵��l�ơG"
            Else
                Label32.Text = strQty    '�j�f�ƶq
                Label29.Text = "�j�f���l�ơG"

                If GluDetail.EditValue = "PT13" And gluChangeDep.EditValue <> "W1101" Then     '@ 2012/6/20 �K�[�@���u����ܤ��\����׼ƴ_���
                    CheckEdit3.Visible = True
                    gluChangeDep.Enabled = False
                    If CheckEdit3.Checked = True Then
                        Label32.Text = strQty1    '��׼ƶq
                        Label29.Text = "��׵��l�ơG"
                    End If
                End If

            End If

            txtQty.Text = pi(0).FP_Qty
            txtWeight.Text = pi(0).FP_Weight
            txtRemark.Text = pi(0).FP_Remark

            If pi(0).FP_InCheck = True Then
                CheckEdit1.Checked = True
                OldCheck = True
            Else
                CheckEdit1.Checked = False
                OldCheck = False
            End If

            If pi(0).FP_Check = True Then
                CheckEdit2.Checked = True
            Else
                CheckEdit2.Checked = False
            End If

            If Label35.Text = "���J����" Then

                TextEdit1.Text = pi(0).CardID

            ElseIf Label35.Text = "�o�X����" Then

                Dim pi1 As List(Of ProductionFieldInfo)
                Dim pc1 As New ProductionFieldControl

                pi1 = pc1.ProductionField_GetList(FP_NO, Nothing, "�o��", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)

                If pi1.Count > 0 Then
                    TextEdit1.Text = pi(0).CardID
                End If

            End If

            Label14.Text = pi(0).FP_CheckActionName
            txtCheckremark.Text = pi(0).FP_CheckRemark

            ComboBoxEdit1.EditValue = pi(0).FP_SendType
            txtZuhe.Text = pi(0).FP_Qty1

            If cboFP_OutType.Text.Trim <> "" Then     '@ 2012/6/12 �K�[ �~�o���������ŮɡA��ܥ~�o����
                cboFP_OutType.Visible = True
                Label37.Visible = True
            End If


        End If
    End Function

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
    

    Function GetPMNO() As String  '�w��զX���Ʊ��p�s�x�O��

        Dim pmi As New LFERP.Library.ProductionMerge.ProductionMergeInfo
        Dim pmc As New LFERP.Library.ProductionMerge.ProductionMergeControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pmi = pmc.ProductionMerge_GetNO(strA)
        If pmi Is Nothing Then
            GetPMNO = "M" + strA + "000001"
        Else
            GetPMNO = "M" + strA + Mid((CInt(Mid(pmi.PM_NO, 6)) + 1000001), 2)
        End If

    End Function

    Function DataNew() As Boolean   '���`���������������o����


        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl



        'Dim lockobj As New Object()

        ''�ݭn��w���N�X�� 
        'SyncLock lockobj
        '    Thread.Sleep(Int(Rnd() * 400) + 100)

        '    txtNo.Text = GetNO()
        'End SyncLock

        'txtNo.Text = GetNO()

        pi.FP_NO = txtNo.Text
        pi.FP_Num = GetNum()

        pi.Pro_Type = cbType.EditValue
        pi.PM_M_Code = PM_M_Code.EditValue
        pi.PM_Type = gluType.EditValue


        pi.Pro_Type1 = cbType1.EditValue
        pi.PM_M_Code1 = gluProduct.EditValue
        pi.PM_Type1 = gluType1.EditValue

        pi.Pro_NO = GluEdit1.EditValue
        pi.Pro_NO1 = GluEdit2.EditValue

        If txtQty.Text.Trim = "" Then
            MsgBox("�ƶq���ର��!")
            Exit Function
        End If
        If txtWeight.Text.Trim = "" Then
            MsgBox("���q���ର��!")
            Exit Function
        End If
        If CInt(txtQty.Text) < 0 Then
            MsgBox("�ƶq�������j��0�����")
            Exit Function
        End If
        If CInt(txtWeight.Text) < 0 Then
            MsgBox("���q�������j��0�����")
            Exit Function
        End If

        If CInt(txtQty.Text) > CInt(Label32.Text) Then
            'MsgBox("��e�o�X�ƶq�j���e���������l��!", , "����")
            If MsgBox("��e�o�X�ƶq�j���e���������l��,�O�_�T�w�~��?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If GluDetail.EditValue = "PT02" Then
                    'DeclareQtyAll
                    ' If Label32.Text = 0 And CInt(txtQty.Text) <= DeclareQty Then '
                    If CInt(txtQty.Text) <= DeclareQtyAll Then 'DeclareQtyAll �bchekData���w���s���   2013-5-16�[���P��
                        GoTo A
                    Else
                        MsgBox("��e�j�f+��׵��l�ƶq�p��o�X�ƶq,�еo�p���׵��l���ƶq!")
                        Exit Function
                    End If
                ElseIf GluDetail.EditValue = "PT16" Then

                    MsgBox("��e��׵��l�`�Ƥp��ɪ�׼�,�еo�p���׵��l���ƶq!")
                    Exit Function
                Else
                    MsgBox("�j�f�o�X��,�o�X�ƶq����j���e�����w�s!")
                    Exit Function
                End If
            Else
                Exit Function
            End If
        End If


A:      pi.FP_Qty = txtQty.Text
        pi.FP_Weight = txtWeight.Text

        Label23.Text = Format(Now, "HH:mm:ss") ''2013-10-9
        pi.FP_Date = CDate(DateEdit1.EditValue & " " & Label23.Text)
        pi.FP_Detail = GluDetail.EditValue
        pi.FP_OutDep = GluDep.EditValue
        pi.FP_InDep = gluChangeDep.EditValue
        pi.FP_Remark = txtRemark.Text
        pi.FP_OutAction = InUserID
        pi.CardID = TextEdit1.Text   '��d�H
        pi.FP_OutType = cboFP_OutType.EditValue
        pi.FP_SubtractReQty = CheckEdit3.Checked       '@ 2012/6/20 �K�[
        pi.FP_OutOK = chkFP_OutOK.Checked              '@ 2012/7/12 �K�[

        If ComboBoxEdit1.EditValue = "���`" Then
            pi.FP_SendType = "���`"

            If txtZuhe.Text.Trim = "" Then
                pi.FP_Qty1 = 0
            Else
                pi.FP_Qty1 = txtZuhe.Text
            End If

        Else
            pi.FP_SendType = "�զX"

            If txtZuhe.Text.Trim = "" Then
                pi.FP_Qty1 = 0
            Else
                pi.FP_Qty1 = txtZuhe.Text
            End If

        End If


        If pc.ProductionField_Add(pi) = True Then
            MsgBox("�O�s���\")
            DataNew = True
        Else
            MsgBox("�O�s����,���ˬd��]!")
            DataNew = False
        End If

    End Function

    Sub DataEdit()

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        pi.FP_Num = Label21.Text


        pi.Pro_Type = cbType.EditValue
        pi.PM_M_Code = PM_M_Code.EditValue
        pi.PM_Type = gluType.EditValue


        pi.Pro_Type1 = cbType1.EditValue
        pi.PM_M_Code1 = gluProduct.EditValue
        pi.PM_Type1 = gluType1.EditValue

        pi.Pro_NO = GluEdit1.EditValue
        pi.Pro_NO1 = GluEdit2.EditValue

        If txtQty.Text.Trim = "" Then
            MsgBox("�ƶq���ର��!")
            Exit Sub
        End If
        If txtWeight.Text = "" Then
            MsgBox("���q���ର��!")
            Exit Sub
        End If
        If CInt(txtQty.Text) < 0 Then
            MsgBox("�ƶq�������j��0�����")
        End If
        If CInt(txtWeight.Text) < 0 Then
            MsgBox("�ƶq�������j��0�����")
        End If

        If CInt(txtQty.Text) > CInt(Label32.Text) Then
            'MsgBox("��e�o�X�ƶq�j���e���������l��!", , "����")
            If MsgBox("��e�o�X�ƶq�j���e���������l��,�O�_�T�w�~��?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If GluDetail.EditValue = "PT02" Then
                    ' If Label32.Text = 0 And CInt(txtQty.Text) < DeclareQty Then
                    If CInt(txtQty.Text) <= DeclareQtyAll Then 'DeclareQtyAll �bchekData���w���s���   2013-5-16�[���P��
                        GoTo A
                    Else
                        MsgBox("��e��׼ƶq�Τj�f�ƶq�p��o�X�ƶq!")
                        Exit Sub
                    End If

                ElseIf GluDetail.EditValue = "PT16" Then
                    MsgBox("��e��׵��l�`�Ƥp��ɪ�׼�,�����\�o��!�нT�{")
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If

A:      pi.FP_Qty = txtQty.Text
        pi.FP_Weight = txtWeight.Text

        Label23.Text = Format(Now, "HH:mm:ss") ''2013-10-9
        pi.FP_Date = CDate(DateEdit1.EditValue & " " & Label23.Text)
        pi.FP_OutDep = GluDep.EditValue
        pi.FP_InDep = gluChangeDep.EditValue
        pi.FP_OutAction = InUserID
        pi.FP_Remark = txtRemark.Text
        pi.CardID = TextEdit1.Text
        pi.FP_OutType = cboFP_OutType.EditValue
        pi.FP_SubtractReQty = CheckEdit3.Checked   '@ 2012/6/20 �K�[
        pi.FP_OutOK = chkFP_OutOK.Checked       '@ 2012/7/12 �K�[

        If ComboBoxEdit1.EditValue = "���`" Then
            pi.FP_SendType = "���`"

            If txtZuhe.Text = "" Then
                pi.FP_Qty1 = 0
            Else
                pi.FP_Qty1 = txtZuhe.Text
            End If
        Else
            pi.FP_SendType = "�զX"

            If txtZuhe.Text = "" Then
                pi.FP_Qty1 = 0
            Else
                pi.FP_Qty1 = txtZuhe.Text
            End If
        End If

        If pc.ProductionField_Update(pi) = True Then
            MsgBox("�O�s���\")
            Me.Close()
        Else
            MsgBox("�O�s����,���ˬd��]!")
        End If

    End Sub

    Function GetTypeData() As Boolean
        GetTypeData = True
        '1<<<<<<<<<<<<-----------�d�XProductionFieldType���\�i���
        Dim SetDataVal, SetDataPTF As Integer
        Dim pfcon As New ProductionFieldTypeControl
        Dim pflist As New List(Of ProductionFieldTypeInfo)
        pflist = pfcon.ProductionFieldType_GetList(GluDetail.EditValue, Nothing, Nothing)
        If pflist.Count > 0 Then
            SetDataVal = pflist(0).PT_DataValue
        Else
            SetDataVal = 0
        End If
        '2-----------------------�d�XProductionField���浧��ڦ��
        Dim piL As New List(Of ProductionFieldInfo)
        Dim pc As New ProductionFieldControl
        piL = pc.ProductionField_GetList(txtNo.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)
        If piL.Count > 0 Then
            SetDataPTF = piL.Count
        Else
            SetDataPTF = 0
        End If
        '3-------------���`���浧��Ƥ���j�_��ڦ��-------------
        If SetDataPTF = SetDataVal Or SetDataVal = 0 Then

        Else
            GetTypeData = False
        End If
        '---------------------------------------------->>>>>>>>>>>>>
    End Function

    Sub UpdateInCheck()
        '�n�P�_�@�U,�y�����O�_�w�s�b,�s�b�N���f��
        If GetTypeData() = False Then
            MsgBox("����w�s�b����,�ФήɻP�q�����p�t!")
            Exit Sub
        End If

        'Dim pi As New ProductionFieldInfo
        'Dim pc As New ProductionFieldControl

        'pi.FP_NO = txtNo.Text
        'pi.FP_Type = "����"
        'If CheckEdit1.Checked = True Then
        '    pi.FP_InCheck = True
        'Else
        '    pi.FP_InCheck = False
        'End If
        'pi.FP_InAction = InUserID
        'pi.FP_InCheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

        'If CheckEdit1.Checked = OldCheck Then
        '    MsgBox("�����ܽT�{���A,�����\�O�s!")
        '    Exit Sub
        'End If

        'If pc.ProductionField_UpdateInCheck(pi) = True Then
        '    MsgBox("�O�s���\")

        '    '--------�w���禬�T�{�Z�����������զX�����Ʀ��o�O��-------------------------------------------------------------------
        '    If ComboBoxEdit1.EditValue = "�զX" Then
        '        Dim pmi As List(Of LFERP.Library.ProductionMerge.ProductionMergeInfo)
        '        Dim pmc As New LFERP.Library.ProductionMerge.ProductionMergeControl

        '        Dim pmi1 As New LFERP.Library.ProductionMerge.ProductionMergeInfo

        '        pmi = pmc.ProductionMerge_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, txtNo.Text)
        '        If pmi.Count = 0 Then

        '            pmi1.PM_NO = GetPMNO()
        '            pmi1.Pro_MNO = GluEdit2.EditValue
        '            pmi1.Pro_MQty = txtZuhe.Text
        '            pmi1.Pro_SNO = GluEdit1.EditValue
        '            pmi1.Pro_SQty = txtQty.Text
        '            pmi1.PM_Action = InUserID
        '            pmi1.PM_AddDate = Format(Now, "yyyy/MM/dd")
        '            pmi1.PM_Check = True
        '            pmi1.FP_NO = txtNo.Text

        '            pmc.ProductionMerge_Add(pmi1)

        '        Else

        '            pmi1.PM_NO = pmi(0).PM_NO
        '            pmi1.Pro_MNO = GluEdit2.EditValue
        '            pmi1.Pro_SNO = GluEdit1.EditValue

        '            If CheckEdit1.Checked = False Then  '�����f�֮ɪ��Ƨ���0
        '                pmi1.Pro_MQty = 0
        '                pmi1.Pro_SQty = 0
        '            Else
        '                pmi1.Pro_MQty = txtZuhe.Text
        '                pmi1.Pro_SQty = txtQty.Text
        '            End If
        '            pmi1.PM_Action = InUserID
        '            pmi1.PM_AddDate = Format(Now, "yyyy/MM/dd")
        '            pmi1.PM_Check = True
        '            pmi1.FP_NO = txtNo.Text

        '            pmc.ProductionMerge_Update(pmi1)

        '        End If

        '        '-----------------------------------------------------------------------

        '    End If
        'Else
        '    MsgBox("�O�s����,���ˬd��]!")
        '    Exit Sub
        'End If

        ''----------------------------------------------------------------------
        ''�Ƶ�:�s�W��e������e���ƪ��u�ǽs�X�ƶq
        ''�Ƶ�:��ַ�e������e�o�ƪ��u�ǽs�X�ƶq
        ''�Ƶ�:�ھڵo�ƪ������i�����(�j�f,��׵�)
        ''---------------------------------------------------------


        ''1 �o�Ƴ������
        'Dim pii As List(Of ProductionDPTWareInventoryInfo)
        'Dim pic As New ProductionDPTWareInventoryControl
        'Dim pii2 As New ProductionDPTWareInventoryInfo

        'Dim Qty, ReQty As Single

        'pii = pic.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)

        'If pii.Count = 0 Then
        '    Qty = 0
        '    ReQty = 0
        'Else
        '    Qty = pii(0).WI_Qty
        '    ReQty = pii(0).WI_ReQty
        'End If

        'pii2.M_Code = GluEdit1.EditValue
        'pii2.DPT_ID = GluDep.EditValue

        'If CheckEdit1.Checked = True Then

        '    If GluDetail.EditValue = "PT02" Then  '��׫H��
        '        If ReQty = 0 Then
        '            pii2.WI_Qty = Qty - CSng(txtQty.Text)   '�T�{�o��
        '            pii2.WI_ReQty = 0
        '        Else
        '            pii2.WI_Qty = Qty
        '            pii2.WI_ReQty = ReQty - CSng(txtQty.Text)
        '        End If
        '    ElseIf GluDetail.EditValue = "PT16" Then  '�ɪ�׫H��

        '        pii2.WI_Qty = Qty
        '        pii2.WI_ReQty = ReQty - CSng(txtQty.Text)
        '    Else
        '        pii2.WI_Qty = Qty - CSng(txtQty.Text)  '�T�{�o��
        '        pii2.WI_ReQty = ReQty
        '    End If

        '    'pii2.WI_Qty = Qty - CSng(txtQty.Text)  '�T�{�o��

        'ElseIf CheckEdit1.Checked = False Then

        '    If GluDetail.EditValue = "PT02" Then  '��׫H��

        '        pii2.WI_Qty = Qty
        '        pii2.WI_ReQty = ReQty + CSng(txtQty.Text)

        '    ElseIf GluDetail.EditValue = "PT16" Then  '�ɪ�׫H��

        '        pii2.WI_Qty = Qty
        '        pii2.WI_ReQty = ReQty + CSng(txtQty.Text)
        '    Else
        '        pii2.WI_Qty = Qty + CSng(txtQty.Text)  '�T�{�o��
        '        pii2.WI_ReQty = ReQty
        '    End If

        '    'pii2.WI_Qty = Qty + CSng(txtQty.Text)  '����--���ƥ[�J
        'End If


        'pic.UpdateProductionField_Qty(pii2) '�o�X���Ƽƶq�ܧ�H��

        ''-----------------
        ''�ܧ��e������e�u�ǽT�{�f�֦Z--�����o�渹�����l�ƶq(�O�s���ɦ������w�s��)

        'Dim pi1 As New ProductionFieldInfo

        'pi1.FP_NO = txtNo.Text
        'pi1.FP_OutDep = GluDep.EditValue
        'pi1.Pro_NO = GluEdit1.EditValue
        'pi1.FP_Type = "�o��"
        'pi1.FP_EndQty = pii2.WI_Qty
        'pi1.FP_EndReQty = pii2.WI_ReQty

        'pc.ProductionField_UpdateEndQty(pi1)
        ''-----------------

        'If Mid(GluDep.EditValue, 1, 1) = "P" Or Mid(GluDep.EditValue, 1, 1) = "W" Then  '�����ܮw

        '    Dim wii As List(Of WareInventory.WareInventoryInfo)
        '    Dim wic As New WareInventory.WareInventoryMTController

        '    Dim Qty3 As Single

        '    wii = wic.WareInventory_GetList3(Transfer(GluEdit1.EditValue), GluDep.EditValue)
        '    If wii.Count = 0 Then
        '        Qty3 = 0
        '    Else
        '        Qty3 = wii(0).WI_Qty
        '    End If

        '    Dim spc As New SharePurchaseController
        '    Dim spi As New SharePurchaseInfo

        '    spi.M_Code = Transfer(GluEdit1.EditValue) '�u�ǽs����Ƭ����������ƽs�X�H��
        '    spi.WH_ID = GluDep.EditValue

        '    If CheckEdit1.Checked = True Then
        '        spi.WI_Qty = Qty3 - CSng(txtQty.Text) '�T�{�o��

        '    ElseIf CheckEdit1.Checked = False Then

        '        spi.WI_Qty = Qty3 + CSng(txtQty.Text) '����--���ƥ[�J

        '    End If
        '    spc.UpdateWareInventory_WIQty2(spi)  '���Ʀb��e�ܮw���ܧ�
        'End If
        ''--------------------------------------------------------------------------------------------------

        'Dim pii3 As New ProductionDPTWareInventoryInfo
        'Dim pii1 As List(Of ProductionDPTWareInventoryInfo)

        'Dim Qty1, ReQty1 As Single

        'pii3.DPT_ID = gluChangeDep.EditValue
        'pii3.M_Code = GluEdit2.EditValue

        'pii1 = pic.ProductionDPTWareInventory_GetList(gluChangeDep.EditValue, GluEdit2.EditValue, Nothing)   '�P�_���Ƥu�Ǽƶq�ܧ�H��
        'If pii1.Count = 0 Then
        '    Qty1 = 0
        '    ReQty1 = 0
        'Else
        '    Qty1 = pii1(0).WI_Qty
        '    ReQty1 = pii1(0).WI_ReQty

        'End If
        'If CheckEdit1.Checked = True Then

        '    If ComboBoxEdit1.EditValue = "���`" Then

        '        If GluDetail.EditValue = "PT02" Then  '���
        '            pii3.WI_Qty = Qty1
        '            pii3.WI_ReQty = ReQty1 + CSng(txtQty.Text)

        '        ElseIf GluDetail.EditValue = "PT16" Then '�ɪ��
        '            pii3.WI_Qty = Qty1 + CSng(txtQty.Text)
        '            pii3.WI_ReQty = ReQty1
        '        Else
        '            pii3.WI_Qty = Qty1 + CSng(txtQty.Text) '���`���o
        '            pii3.WI_ReQty = ReQty1
        '        End If

        '        'pii3.WI_Qty = Qty1 + CSng(txtQty.Text)  '��l���p
        '    Else

        '        If GluDetail.EditValue = "PT02" Then
        '            pii3.WI_Qty = Qty1
        '            pii3.WI_ReQty = ReQty1 + CSng(txtZuhe.Text)

        '        ElseIf GluDetail.EditValue = "PT16" Then

        '            pii3.WI_Qty = Qty1 + CSng(txtZuhe.Text)
        '            pii3.WI_ReQty = ReQty1
        '        Else
        '            pii3.WI_Qty = Qty1 + CSng(txtZuhe.Text)
        '            pii3.WI_ReQty = ReQty1
        '        End If

        '        'pii3.WI_Qty = Qty1 + CSng(txtZuhe.Text) '��l���p
        '    End If

        'ElseIf CheckEdit1.Checked = False Then

        '    'If ComboBoxEdit1.EditValue = "���`" Then

        '    '    pii3.WI_Qty = Qty1 - CSng(txtQty.Text)
        '    'Else

        '    '    pii3.WI_Qty = Qty1 - CSng(txtZuhe.Text)
        '    'End If
        '    If ComboBoxEdit1.EditValue = "���`" Then

        '        If GluDetail.EditValue = "PT02" Then  '��׫H��

        '            pii3.WI_Qty = Qty1
        '            pii3.WI_ReQty = ReQty1 - CSng(txtQty.Text)

        '        ElseIf GluDetail.EditValue = "PT16" Then  '�ɪ�׫H��

        '            pii3.WI_Qty = Qty1
        '            pii3.WI_ReQty = ReQty1 - CSng(txtQty.Text)
        '        Else
        '            pii3.WI_Qty = Qty1 - CSng(txtQty.Text)  '�T�{�o��
        '            pii3.WI_ReQty = ReQty1
        '        End If
        '    Else

        '        If GluDetail.EditValue = "PT02" Then  '��׫H��

        '            pii3.WI_Qty = Qty1
        '            pii3.WI_ReQty = ReQty1 - CSng(txtZuhe.Text)

        '        ElseIf GluDetail.EditValue = "PT16" Then  '�ɪ�׫H��

        '            pii3.WI_Qty = Qty1
        '            pii3.WI_ReQty = ReQty1 - CSng(txtZuhe.Text)
        '        Else
        '            pii3.WI_Qty = Qty1 - CSng(txtZuhe.Text)  '�T�{�o��
        '            pii3.WI_ReQty = ReQty1
        '        End If
        '    End If

        'End If

        'pic.UpdateProductionField_Qty(pii3) '���J���Ƽƶq�ܧ�H��

        ''-----------------
        ''�ܧ��e������e�u�ǽT�{�f�֦Z--�����o�渹�����l�ƶq(�O�s���ɦ������w�s��)

        'Dim pi11 As New ProductionFieldInfo

        'pi11.FP_NO = txtNo.Text
        'pi11.FP_OutDep = gluChangeDep.EditValue
        'pi11.Pro_NO = GluEdit2.EditValue
        'pi11.FP_Type = "����"
        'pi11.FP_EndQty = pii3.WI_Qty
        'pi11.FP_EndReQty = pii3.WI_ReQty

        'pc.ProductionField_UpdateEndQty(pi11)
        ''-----------------

        ''--------------------------------------------------------------------
        ''������ѸӤu�ǽs�X�ӳ����ƶq�Ҧ����o�O�����`�H��
        ''--------------------------------------------------------------------

        'Dim pdi As List(Of ProductionFieldDaySummaryInfo)
        'Dim pdc As New ProductionFieldDaySummaryControl

        'Dim udi As New ProductionFieldDaySummaryInfo


        'Dim StrType As String  '����
        'Dim IntQty As Integer  '�ƶq

        ''�w��o�ƾާ@
        ''-------------------------------------------------------------------------
        'pdi = pdc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, GluEdit1.EditValue, GluDep.EditValue, Nothing, DateEdit1.Text, DateEdit1.Text)

        'If GluDetail.EditValue = "PT01" Then
        '    StrType = "�o��"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).FaLiao
        '    End If

        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = GluEdit1.EditValue
        '    udi.FP_OutDep = GluDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.CunCang = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.ShouLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FanXiuOut = 0
        '    udi.JiaCun = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.FaLiao = IntQty + CInt(txtQty.Text)


        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.FaLiao = IntQty - CInt(txtQty.Text)

        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '�ܧ�o�Ƽƶq(�j�f����)

        'ElseIf GluDetail.EditValue = "PT02" Then
        '    StrType = "��׵o�X"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).FanXiuOut
        '    End If

        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = GluEdit1.EditValue
        '    udi.FP_OutDep = GluDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.CunCang = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.ShouLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FaLiao = 0
        '    udi.JiaCun = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.FanXiuOut = IntQty + CInt(txtQty.Text)


        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.FanXiuOut = IntQty - CInt(txtQty.Text)


        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '�ܧ��׵o�Ƽƶq(��ײ���)
        'ElseIf GluDetail.EditValue = "PT12" Then
        '    StrType = "�s��"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).CunCang
        '    End If

        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = GluEdit1.EditValue
        '    udi.FP_OutDep = GluDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.FanXiuOut = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.ShouLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FaLiao = 0
        '    udi.JiaCun = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.CunCang = IntQty + CInt(txtQty.Text)

        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.CunCang = IntQty - CInt(txtQty.Text)

        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '�ܧ�s�ܼƶq(�s�ܲ���--�s�J�����ܮw(�����_�s�ܦ��J))
        'ElseIf GluDetail.EditValue = "PT05" Then
        '    StrType = "����"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).CunCang
        '    End If

        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = GluEdit2.EditValue
        '    udi.FP_OutDep = gluChangeDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.FanXiuOut = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.ShouLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FaLiao = 0
        '    udi.JiaCun = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.QuCang = IntQty + CInt(txtQty.Text)

        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.QuCang = IntQty - CInt(txtQty.Text)

        '    End If
        '    udi.CunCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '�ܧ�s�ܼƶq(�s�ܲ���--�s�J�����ܮw(�����_�s�ܦ��J))


        'ElseIf GluDetail.EditValue = "PT13" Then
        '    StrType = "���u"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).ChuHuo
        '    End If

        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = GluEdit1.EditValue
        '    udi.FP_OutDep = GluDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.CunCang = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.ShouLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FanXiuOut = 0
        '    udi.JiaCun = 0

        '    If CheckEdit1.Checked = True Then

        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi.ChuHuo = IntQty + CInt(txtQty.Text)
        '        Else
        '            udi.ChuHuo = IntQty + CInt(txtZuhe.Text)
        '        End If

        '    ElseIf CheckEdit1.Checked = False Then

        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi.ChuHuo = IntQty - CInt(txtQty.Text)
        '        Else
        '            udi.ChuHuo = IntQty - CInt(txtZuhe.Text)
        '        End If

        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.FaLiao = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '�ܧ�o�Ƽƶq(�j�f����)


        '    If Mid(gluChangeDep.EditValue, 1, 1) = "P" Or Mid(gluChangeDep.EditValue, 1, 1) = "W" Then  '�����ܮw

        '        Dim wii As List(Of WareInventory.WareInventoryInfo)
        '        Dim wic As New WareInventory.WareInventoryMTController

        '        Dim Qty3 As Single

        '        wii = wic.WareInventory_GetList3(Transfer(GluEdit2.EditValue), gluChangeDep.EditValue)
        '        If wii.Count = 0 Then
        '            Qty3 = 0
        '        Else
        '            Qty3 = wii(0).WI_Qty
        '        End If

        '        Dim spc As New SharePurchaseController
        '        Dim spi As New SharePurchaseInfo

        '        spi.M_Code = Transfer(GluEdit2.EditValue) '�N�u�ǽs����Ƭ����ƽs�X�H��
        '        spi.WH_ID = gluChangeDep.EditValue

        '        If CheckEdit1.Checked = True Then
        '            If ComboBoxEdit1.EditValue = "���`" Then
        '                spi.WI_Qty = Qty3 + CSng(txtQty.Text) '�T�{�o��
        '            Else
        '                spi.WI_Qty = Qty3 + CSng(txtZuhe.Text) '�T�{�o��
        '            End If

        '        ElseIf CheckEdit1.Checked = False Then
        '            If ComboBoxEdit1.EditValue = "���`" Then
        '                spi.WI_Qty = Qty3 - CSng(txtQty.Text) '����--���ƥ[�J
        '            Else
        '                spi.WI_Qty = Qty3 - CSng(txtZuhe.Text) '����--���ƥ[�J
        '            End If

        '        End If
        '        spc.UpdateWareInventory_WIQty2(spi)  '���Ʀb��e�ܮw���ܧ�
        '    End If
        'ElseIf GluDetail.EditValue = "PT14" Then

        '    StrType = "�~�o�o�X"

        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).WaiFaOut
        '    End If

        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = GluEdit1.EditValue
        '    udi.FP_OutDep = GluDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.FanXiuOut = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.ShouLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FaLiao = 0
        '    udi.JiaCun = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.WaiFaOut = IntQty + CInt(txtQty.Text)

        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.WaiFaOut = IntQty - CInt(txtQty.Text)

        '    End If
        '    udi.CunCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.QuCang = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.RePairOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '�ܧ�~�o�o�X�ƶq

        'ElseIf GluDetail.EditValue = "PT16" Then
        '    StrType = "�ɪ�׵o�X"
        '    If pdi.Count = 0 Then
        '        IntQty = 0
        '    Else
        '        IntQty = pdi(0).RePairOut
        '    End If

        '    udi.Pro_Type = cbType.EditValue
        '    udi.PM_M_Code = PM_M_Code.EditValue
        '    udi.PM_Type = gluType.EditValue
        '    udi.Pro_NO = GluEdit1.EditValue
        '    udi.FP_OutDep = GluDep.EditValue
        '    udi.PM_Date = DateEdit1.EditValue

        '    udi.BuNiang = 0
        '    udi.CunCang = 0
        '    udi.CunHuo = 0
        '    udi.DiuShi = 0
        '    udi.ShouLiao = 0
        '    udi.FanXiuIn = 0
        '    udi.FanXiuOut = 0
        '    udi.JiaCun = 0
        '    udi.FaLiao = 0
        '    If CheckEdit1.Checked = True Then
        '        udi.RePairOut = IntQty + CInt(txtQty.Text)
        '    ElseIf CheckEdit1.Checked = False Then
        '        udi.RePairOut = IntQty - CInt(txtQty.Text)

        '    End If
        '    udi.QuCang = 0
        '    udi.QuCun = 0
        '    udi.LiuBan = 0
        '    udi.SunHuai = 0
        '    udi.ChuHuo = 0
        '    udi.WaiFaIn = 0
        '    udi.WaiFaOut = 0
        '    udi.AccIn = 0
        '    udi.AccOut = 0
        '    udi.Type = StrType

        '    pdc.UpdateProductionDaySummary_Qty(udi)   '�ܧ�o�Ƽƶq(�j�f����)

        'End If
        ''------------------------------------------------ '�o�ƹL�{
        'If pdi.Count > 0 Then

        '    udi.Pro_NO = GluEdit1.EditValue
        '    udi.FP_OutDep = GluDep.EditValue
        '    udi.PM_Date = DateEdit1.Text

        '    pdc.ProductionFieldDaySummary_Delete(udi) '�P�_��e�u�ǬO�_�Ҧ��ƶq����0 Yes�R�������O��,NO�~��O�d!

        'End If
        ''------------------------------------------------
        ''------------------------------------------------------
        ''�w�怜�o�L�{��������
        ''------------------------------------------------------
        'Dim udi1 As New ProductionFieldDaySummaryInfo
        'Dim pdi1 As List(Of ProductionFieldDaySummaryInfo)
        'Dim strType2 As String
        'Dim IntQty2 As Integer

        'pdi1 = pdc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, GluEdit2.EditValue, gluChangeDep.EditValue, Nothing, DateEdit1.Text, DateEdit1.Text)

        'If GluDetail.EditValue = "PT01" Or GluDetail.EditValue = "PT16" Then
        '    strType2 = "����"
        '    If pdi1.Count = 0 Then
        '        IntQty2 = 0
        '    Else
        '        IntQty2 = pdi1(0).ShouLiao
        '    End If

        '    udi1.Pro_Type = cbType1.EditValue
        '    udi1.PM_M_Code = gluProduct.EditValue
        '    udi1.PM_Type = gluType1.EditValue
        '    udi1.Pro_NO = GluEdit2.EditValue
        '    udi1.FP_OutDep = gluChangeDep.EditValue
        '    udi1.PM_Date = DateEdit1.EditValue

        '    udi1.BuNiang = 0
        '    udi1.CunCang = 0
        '    udi1.CunHuo = 0
        '    udi1.DiuShi = 0
        '    udi1.FaLiao = 0
        '    udi1.FanXiuIn = 0
        '    udi1.FanXiuOut = 0
        '    udi1.JiaCun = 0
        '    If CheckEdit1.Checked = True Then
        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi1.ShouLiao = IntQty2 + CInt(txtQty.Text)
        '        Else
        '            udi1.ShouLiao = IntQty2 + CInt(txtZuhe.Text)
        '        End If

        '    ElseIf CheckEdit1.Checked = False Then
        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi1.ShouLiao = IntQty2 - CInt(txtQty.Text)
        '        Else
        '            udi1.ShouLiao = IntQty2 - CInt(txtZuhe.Text)
        '        End If

        '    End If
        '    udi1.QuCang = 0
        '    udi1.QuCun = 0
        '    udi1.LiuBan = 0
        '    udi1.SunHuai = 0
        '    udi1.WaiFaIn = 0
        '    udi1.WaiFaOut = 0
        '    udi1.AccIn = 0
        '    udi1.AccOut = 0
        '    udi1.RePairOut = 0
        '    udi1.Type = strType2

        '    pdc.UpdateProductionDaySummary_Qty(udi1)   '�ܧ�o�Ƽƶq(�j�f����)

        '    If Mid(gluChangeDep.EditValue, 1, 1) = "P" Or Mid(gluChangeDep.EditValue, 1, 1) = "W" Then  '�����ܮw

        '        Dim wii As List(Of WareInventory.WareInventoryInfo)
        '        Dim wic As New WareInventory.WareInventoryMTController

        '        Dim Qty3 As Single

        '        wii = wic.WareInventory_GetList3(Transfer(GluEdit2.EditValue), gluChangeDep.EditValue)
        '        If wii.Count = 0 Then
        '            Qty3 = 0
        '        Else
        '            Qty3 = wii(0).WI_Qty
        '        End If

        '        Dim spc As New SharePurchaseController
        '        Dim spi As New SharePurchaseInfo

        '        spi.M_Code = Transfer(GluEdit2.EditValue)
        '        spi.WH_ID = gluChangeDep.EditValue

        '        If CheckEdit1.Checked = True Then
        '            If ComboBoxEdit1.EditValue = "���`" Then
        '                spi.WI_Qty = Qty3 + CSng(txtQty.Text) '�T�{�o��
        '            Else
        '                spi.WI_Qty = Qty3 + CSng(txtZuhe.Text) '�T�{�o��
        '            End If

        '        ElseIf CheckEdit1.Checked = False Then
        '            If ComboBoxEdit1.EditValue = "���`" Then
        '                spi.WI_Qty = Qty3 - CSng(txtQty.Text) '����--���ƥ[�J
        '            Else
        '                spi.WI_Qty = Qty3 - CSng(txtZuhe.Text) '����--���ƥ[�J
        '            End If

        '        End If
        '        spc.UpdateWareInventory_WIQty2(spi)  '���Ʀb��e�ܮw���ܧ�
        '    End If

        'ElseIf GluDetail.EditValue = "PT02" Then
        '    strType2 = "��צ��J"

        '    If pdi1.Count = 0 Then
        '        IntQty2 = 0
        '    Else
        '        IntQty2 = pdi1(0).FanXiuIn
        '    End If

        '    udi1.Pro_Type = cbType1.EditValue
        '    udi1.PM_M_Code = gluProduct.EditValue
        '    udi1.PM_Type = gluType1.EditValue
        '    udi1.Pro_NO = GluEdit2.EditValue
        '    udi1.FP_OutDep = gluChangeDep.EditValue
        '    udi1.PM_Date = DateEdit1.EditValue

        '    udi1.BuNiang = 0
        '    udi1.CunCang = 0
        '    udi1.CunHuo = 0
        '    udi1.DiuShi = 0
        '    udi1.FaLiao = 0
        '    udi1.ShouLiao = 0
        '    udi1.FanXiuOut = 0
        '    udi1.JiaCun = 0

        '    If CheckEdit1.Checked = True Then
        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi1.FanXiuIn = IntQty2 + CInt(txtQty.Text)
        '        Else
        '            udi1.FanXiuIn = IntQty2 + CInt(txtZuhe.Text)
        '        End If

        '    ElseIf CheckEdit1.Checked = False Then
        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi1.FanXiuIn = IntQty2 - CInt(txtQty.Text)
        '        Else
        '            udi1.FanXiuIn = IntQty2 - CInt(txtZuhe.Text)
        '        End If

        '    End If
        '    udi1.QuCang = 0
        '    udi1.QuCun = 0
        '    udi1.LiuBan = 0
        '    udi1.SunHuai = 0
        '    udi1.WaiFaIn = 0
        '    udi1.WaiFaOut = 0
        '    udi1.AccIn = 0
        '    udi1.AccOut = 0
        '    udi1.RePairOut = 0
        '    udi1.Type = strType2

        '    pdc.UpdateProductionDaySummary_Qty(udi1)   '�ܧ��צ��J�ƶq(��ײ���)
        'ElseIf GluDetail.EditValue = "PT14" Then

        '    strType2 = "�~�o���J"

        '    If pdi1.Count = 0 Then
        '        IntQty2 = 0
        '    Else
        '        IntQty2 = pdi1(0).WaiFaIn
        '    End If

        '    udi1.Pro_Type = cbType1.EditValue
        '    udi1.PM_M_Code = gluProduct.EditValue
        '    udi1.PM_Type = gluType1.EditValue
        '    udi1.Pro_NO = GluEdit2.EditValue
        '    udi1.FP_OutDep = gluChangeDep.EditValue
        '    udi1.PM_Date = DateEdit1.EditValue

        '    udi1.BuNiang = 0
        '    udi1.CunCang = 0
        '    udi1.CunHuo = 0
        '    udi1.DiuShi = 0
        '    udi1.FaLiao = 0
        '    udi1.ShouLiao = 0
        '    udi1.FanXiuOut = 0
        '    udi1.JiaCun = 0

        '    If CheckEdit1.Checked = True Then
        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi1.WaiFaIn = IntQty2 + CInt(txtQty.Text)
        '        Else
        '            udi1.WaiFaIn = IntQty2 + CInt(txtZuhe.Text)
        '        End If

        '    ElseIf CheckEdit1.Checked = False Then
        '        If ComboBoxEdit1.EditValue = "���`" Then
        '            udi1.WaiFaIn = IntQty2 - CInt(txtQty.Text)
        '        Else
        '            udi1.WaiFaIn = IntQty2 - CInt(txtZuhe.Text)
        '        End If

        '    End If
        '    udi1.QuCang = 0
        '    udi1.QuCun = 0
        '    udi1.LiuBan = 0
        '    udi1.SunHuai = 0
        '    udi1.FanXiuIn = 0
        '    udi1.WaiFaOut = 0
        '    udi1.AccIn = 0
        '    udi1.AccOut = 0
        '    udi1.RePairOut = 0
        '    udi1.Type = strType2

        '    pdc.UpdateProductionDaySummary_Qty(udi1)   '�ܧ��צ��J�ƶq(��ײ���)


        '    If Mid(gluChangeDep.EditValue, 1, 1) = "P" Or Mid(gluChangeDep.EditValue, 1, 1) = "W" Then  '�����ܮw

        '        Dim wii As List(Of WareInventory.WareInventoryInfo)
        '        Dim wic As New WareInventory.WareInventoryMTController

        '        Dim Qty3 As Single

        '        wii = wic.WareInventory_GetList3(Transfer(GluEdit2.EditValue), gluChangeDep.EditValue)

        '        If wii Is Nothing Then
        '            Qty3 = 0
        '        Else
        '            Qty3 = wii(0).WI_Qty
        '        End If

        '        Dim spc As New SharePurchaseController
        '        Dim spi As New SharePurchaseInfo

        '        spi.M_Code = Transfer(GluEdit2.EditValue) '�N�u�ǽs����Ƭ����ƽs�X�H��
        '        spi.WH_ID = gluChangeDep.EditValue

        '        If CheckEdit1.Checked = True Then
        '            If ComboBoxEdit1.EditValue = "���`" Then
        '                spi.WI_Qty = Qty3 + CSng(txtQty.Text) '�T�{�o��
        '            Else
        '                spi.WI_Qty = Qty3 + CSng(txtZuhe.Text) '�T�{�o��
        '            End If

        '        ElseIf CheckEdit1.Checked = False Then
        '            If ComboBoxEdit1.EditValue = "���`" Then
        '                spi.WI_Qty = Qty3 - CSng(txtQty.Text) '����--���ƥ[�J
        '            Else
        '                spi.WI_Qty = Qty3 - CSng(txtZuhe.Text) '����--���ƥ[�J
        '            End If

        '        End If
        '        spc.UpdateWareInventory_WIQty2(spi)  '���Ʀb��e�ܮw���ܧ�
        '    End If

        'End If


        ''-------------------------------------------------���Ƴ���
        'If pdi1.Count > 0 Then

        '    udi1.Pro_NO = GluEdit2.EditValue
        '    udi1.FP_OutDep = gluChangeDep.EditValue
        '    udi1.PM_Date = DateEdit1.Text

        '    pdc.ProductionFieldDaySummary_Delete(udi1) '�P�_��e�u�ǬO�_�Ҧ��ƶq����0 Yes�R�������O��,NO�~��O�d!

        'End If
        ''------------------------------------------------


        ''----------------------------------------------�P�_��e�����Ҧb�Ͳ����O�_�s�b�����u�ǹ������Ͳ��p��.�p�G�S���N�K�[!
        'Dim psi1 As List(Of ProductionScheduleInfo)
        'Dim psc1 As New ProductionScheduleControl
        'Dim psi2 As New ProductionScheduleInfo
        'Dim psc2 As New ProductionScheduleControl

        'Dim strFacID As String
        'Dim strCode As String

        'If Mid(gluChangeDep.EditValue, 1, 1) <> "W" And Mid(gluChangeDep.EditValue, 1, 1) <> "F" And Mid(gluChangeDep.EditValue, 1, 1) <> "P" Then

        '    strFacID = Mid(gluChangeDep.EditValue, 1, 1)

        '    strCode = Transfer(GluEdit2.EditValue)

        '    psi1 = psc1.ProductionSchedule_GetList1(Nothing, cbType.EditValue, PM_M_Code.EditValue, strCode, strFacID, Nothing, DateEdit1.Text, DateEdit1.Text)

        '    If psi1.Count = 0 Then

        '        psi2.PS_NO = GetPSNO()
        '        psi2.Pro_Type = cbType.EditValue
        '        psi2.PM_M_Code = PM_M_Code.EditValue
        '        psi2.PM_Type = gluType.EditValue
        '        psi2.M_Code = strCode
        '        psi2.PS_KaiLiao = False
        '        psi2.PS_Detail = ""
        '        psi2.PS_Action = InUserID
        '        psi2.PS_Dep = strFacID  '�Ͳ��p���t�O
        '        psi2.PS_Remark = txtRemark.Text
        '        psi2.PS_AddDate = Format(Now, "yyyy/MM/dd")

        '        psi2.PS_Num = GetPSNum()
        '        psi2.PS_DayNumber = 0
        '        psi2.PS_Date = DateEdit1.Text

        '        If psc2.ProductionSchedule_Add(psi2) = False Then
        '            MsgBox("�K�[����,���ˬd��]!")
        '            Exit Sub
        '        End If

        '    Else
        '        Exit Sub
        '    End If

        'End If

        '-----------------------------------------------
        If CheckEdit1.Checked = OldCheck Then
            MsgBox("�����ܽT�{���A,�����\�O�s!")
            Exit Sub
        End If

        Dim pai As New ProductionAffairInfo
        Dim pac As New ProductionAffairControl

        '------------------------------------------------------�o�X�����w�q��l�ƫH��
        Dim pdi As List(Of ProductionDPTWareInventoryInfo)
        Dim pdc As New ProductionDPTWareInventoryControl

        Dim pdsi As List(Of ProductionFieldDaySummaryInfo)
        Dim pdsc As New ProductionFieldDaySummaryControl

        Dim strQty, strReQty As Integer  '�o�X�������l�ƫH��
        Dim strReQtyAll As Double  ''2013 �j�f+��׸`�E��

        Dim strShouLiao, strJiaCun, strQuCun, strFaLiao, strCunHuo, strFanXiuIn, strFanXiuOut, strLiuBan, strSunHuai, strDiuShi, strBuNiang, strCunCang, strQuCang, strChuHuo, strWaiFaIn, strWaiFaOut, strAccIn, strAccOut, strRePairOut, strZuheOut As Integer '�o�X����

        pdi = pdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)

        If pdi.Count = 0 Then
            strQty = 0
            strReQty = 0
            strReQtyAll = 0
        Else
            strQty = pdi(0).WI_Qty
            strReQty = pdi(0).WI_ReQty
            strReQtyAll = pdi(0).WI_Qty + pdi(0).WI_ReQty
        End If

        pdsi = pdsc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, GluEdit1.EditValue, GluDep.EditValue, Nothing, DateEdit1.Text, DateEdit1.Text)
        If pdsi.Count = 0 Then
            strShouLiao = 0
            strJiaCun = 0
            strQuCun = 0
            strFaLiao = 0
            strCunHuo = 0
            strFanXiuIn = 0
            strFanXiuOut = 0
            strLiuBan = 0
            strSunHuai = 0
            strDiuShi = 0
            strBuNiang = 0
            strCunCang = 0
            strQuCang = 0
            strChuHuo = 0
            strWaiFaIn = 0
            strWaiFaOut = 0
            strAccIn = 0
            strAccOut = 0
            strRePairOut = 0
            strZuheOut = 0
        Else
            strShouLiao = pdsi(0).ShouLiao
            strJiaCun = pdsi(0).JiaCun
            strQuCun = pdsi(0).QuCun
            strFaLiao = pdsi(0).FaLiao
            strCunHuo = pdsi(0).CunHuo
            strFanXiuIn = pdsi(0).FanXiuIn
            strFanXiuOut = pdsi(0).FanXiuOut
            strLiuBan = pdsi(0).LiuBan
            strSunHuai = pdsi(0).SunHuai
            strDiuShi = pdsi(0).DiuShi
            strBuNiang = pdsi(0).BuNiang
            strCunCang = pdsi(0).CunCang
            strQuCang = pdsi(0).QuCang
            strChuHuo = pdsi(0).ChuHuo
            strWaiFaIn = pdsi(0).WaiFaIn
            strWaiFaOut = pdsi(0).WaiFaOut
            strAccIn = pdsi(0).AccIn
            strAccOut = pdsi(0).AccOut
            strRePairOut = pdsi(0).RePairOut
            strZuheOut = pdsi(0).ZuheOut
        End If
        '------------------------------------------------------���J�����w�q��l�ƫH��

        Dim pdi1 As List(Of ProductionDPTWareInventoryInfo)
        Dim pdc1 As New ProductionDPTWareInventoryControl
        Dim pdsi1 As List(Of ProductionFieldDaySummaryInfo)
        Dim pdsc1 As New ProductionFieldDaySummaryControl
        '------------------------------------------------------
        Dim strQty1, strReQty1 As Integer  '���J�������l�ƫH��

        Dim strShouLiao1, strJiaCun1, strQuCun1, strFaLiao1, strCunHuo1, strFanXiuIn1, strFanXiuOut1, strLiuBan1, strSunHuai1, strDiuShi1, strBuNiang1, strCunCang1, strQuCang1, strChuHuo1, strWaiFaIn1, strWaiFaOut1, strAccIn1, strAccOut1, strRePairOut1, strZuheOut1 As Integer '���J����

        If Mid(gluChangeDep.EditValue, 1, 1) = "W" Or Mid(gluChangeDep.EditValue, 1, 1) = "P" Then
            'Dim wii As List(Of WareInventory.WareInventoryInfo)
            'Dim wic As New WareInventory.WareInventoryMTController

            'Dim Qty3 As Single

            'wii = wic.WareInventory_GetList3(Transfer(GluEdit2.EditValue), gluChangeDep.EditValue)

            'If wii.Count = 0 Then
            '    Qty3 = 0
            'Else
            '    Qty3 = wii(0).WI_Qty
            'End If


            'Dim spc As New SharePurchaseController
            'Dim spi As New SharePurchaseInfo

            'spi.M_Code = Transfer(GluEdit2.EditValue) '�N�u�ǽs����Ƭ����ƽs�X�H��
            'spi.WH_ID = gluChangeDep.EditValue

            'If CheckEdit1.Checked = True Then
            '    If ComboBoxEdit1.EditValue = "���`" Then
            '        spi.WI_Qty = Qty3 + CSng(txtQty.Text) '�T�{�o��
            '    Else
            '        spi.WI_Qty = Qty3 + CSng(txtZuhe.Text) '�T�{�o��
            '    End If

            'ElseIf CheckEdit1.Checked = False Then
            '    If ComboBoxEdit1.EditValue = "���`" Then
            '        spi.WI_Qty = Qty3 - CSng(txtQty.Text) '����--���ƥ[�J
            '    Else
            '        spi.WI_Qty = Qty3 - CSng(txtZuhe.Text) '����--���ƥ[�J
            '    End If

            'End If
            'spc.UpdateWareInventory_WIQty2(spi)  '���Ʀb��e�ܮw���ܧ�

            If CheckEdit1.Checked = True Then
                If ComboBoxEdit1.EditValue = "���`" Then
                    Dim pic As New ProductInventoryController
                    Dim pii As List(Of ProductInventoryInfo)
                    Dim pii1 As New ProductInventoryInfo
                    Dim strM_Code As String

                    strM_Code = Transfer(GluEdit2.EditValue)

                    ' pii = pic.ProductInventory_GetList(PM_M_Code.EditValue, strM_Code, gluChangeDep.EditValue, Nothing)'2013-10-29
                    pii = pic.ProductInventory_GetList(gluProduct.EditValue, strM_Code, gluChangeDep.EditValue, Nothing)

                    'pii1.PM_M_Code = PM_M_Code.EditValue
                    pii1.PM_M_Code = gluProduct.EditValue
                    pii1.M_Code = strM_Code
                    pii1.WH_ID = gluChangeDep.EditValue

                    If pii.Count > 0 Then
                        pii1.PI_Qty = pii(0).PI_Qty + CInt(txtQty.Text.Trim)
                    Else
                        pii1.PI_Qty = CInt(txtQty.Text.Trim)
                    End If
                    pic.ProductInventory_Update(pii1)
                End If
            End If

            strShouLiao1 = 0
            strJiaCun1 = 0
            strQuCun1 = 0
            strFaLiao1 = 0
            strCunHuo1 = 0
            strFanXiuIn1 = 0
            strFanXiuOut1 = 0
            strLiuBan1 = 0
            strSunHuai1 = 0
            strDiuShi1 = 0
            strBuNiang1 = 0
            strCunCang1 = 0
            strQuCang1 = 0
            strChuHuo1 = 0
            strWaiFaIn1 = 0
            strWaiFaOut1 = 0
            strAccIn1 = 0
            strAccOut1 = 0
            strRePairOut1 = 0
            strZuheOut1 = 0

        Else
            pdi1 = pdc1.ProductionDPTWareInventory_GetList(gluChangeDep.EditValue, GluEdit2.EditValue, Nothing)
            If pdi1.Count = 0 Then
                strQty1 = 0
                strReQty1 = 0
            Else
                strQty1 = pdi1(0).WI_Qty
                strReQty1 = pdi1(0).WI_ReQty
            End If

            pdsi1 = pdsc1.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, GluEdit2.EditValue, gluChangeDep.EditValue, Nothing, DateEdit1.Text, DateEdit1.Text)
            If pdsi1.Count = 0 Then
                strShouLiao1 = 0
                strJiaCun1 = 0
                strQuCun1 = 0
                strFaLiao1 = 0
                strCunHuo1 = 0
                strFanXiuIn1 = 0
                strFanXiuOut1 = 0
                strLiuBan1 = 0
                strSunHuai1 = 0
                strDiuShi1 = 0
                strBuNiang1 = 0
                strCunCang1 = 0
                strQuCang1 = 0
                strChuHuo1 = 0
                strWaiFaIn1 = 0
                strWaiFaOut1 = 0
                strAccIn1 = 0
                strAccOut1 = 0
                strRePairOut1 = 0
                strZuheOut1 = 0
            Else
                strShouLiao1 = pdsi1(0).ShouLiao
                strJiaCun1 = pdsi1(0).JiaCun
                strQuCun1 = pdsi1(0).QuCun
                strFaLiao1 = pdsi1(0).FaLiao
                strCunHuo1 = pdsi1(0).CunHuo
                strFanXiuIn1 = pdsi1(0).FanXiuIn
                strFanXiuOut1 = pdsi1(0).FanXiuOut
                strLiuBan1 = pdsi1(0).LiuBan
                strSunHuai1 = pdsi1(0).SunHuai
                strDiuShi1 = pdsi1(0).DiuShi
                strBuNiang1 = pdsi1(0).BuNiang
                strCunCang1 = pdsi1(0).CunCang
                strQuCang1 = pdsi1(0).QuCang
                strChuHuo1 = pdsi1(0).ChuHuo
                strWaiFaIn1 = pdsi1(0).WaiFaIn
                strWaiFaOut1 = pdsi1(0).WaiFaOut
                strAccIn1 = pdsi1(0).AccIn
                strAccOut1 = pdsi1(0).AccOut
                strRePairOut1 = pdsi1(0).RePairOut
                strZuheOut1 = pdsi1(0).ZuheOut

            End If
        End If

        pai.FP_NO = txtNo.Text
        pai.FP_Type = "����"
        pai.FP_InAction = InUserID
        pai.CardID = TextEdit1.Text '��d�H�H��
        If CheckEdit1.Checked = True Then
            pai.FP_InCheck = True
        Else
            pai.FP_InCheck = False
        End If

        pai.FP_InCheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        '-------------------------------------------------------------------
        pai.Pro_Type = cbType.EditValue
        pai.PM_M_Code = PM_M_Code.EditValue
        pai.PM_Type = gluType.EditValue

        pai.Pro_Type1 = cbType1.EditValue
        pai.PM_M_Code1 = gluProduct.EditValue
        pai.PM_Type1 = gluType1.EditValue

        pai.Pro_NO = GluEdit1.EditValue
        pai.Pro_NO1 = GluEdit2.EditValue
        pai.FP_OutDep = GluDep.EditValue
        pai.FP_InDep = gluChangeDep.EditValue

        pai.FP_Detail = GluDetail.EditValue
        pai.Type = Nothing

        '------------------------------------------------------�ܧ󳡪����l�ƫH��   strReQtyAll
        If GluDetail.EditValue = "PT02" Then

            If CInt(txtQty.Text) <= strReQtyAll Then
                If CInt(txtQty.Text) >= strReQty Then '�o���ƶq �j�� ��׼ƶq ,��׼ƥ���,���j�f��
                    pai.WI_Qty = strQty - (CInt(txtQty.Text) - strReQty)
                    pai.WI_ReQty = 0
                Else
                    pai.WI_Qty = strQty
                    pai.WI_ReQty = strReQty - CInt(txtQty.Text)
                End If
            Else
                MsgBox("A���+�j�f�ƶq����!" & CInt(txtQty.Text) - strReQtyAll)
                Exit Sub
            End If

            '+++++++++++++++++===============

            pai.WI_Qty1 = strQty1
            If ComboBoxEdit1.EditValue = "���`" Then
                pai.WI_ReQty1 = strReQty1 + CInt(txtQty.Text)
            Else
                pai.WI_ReQty1 = strReQty1 + CInt(txtZuhe.Text)
            End If

            '----------------------------------------------------------

            'If strReQty = 0 Then
            '    pai.WI_Qty = strQty - CInt(txtQty.Text)
            '    pai.WI_ReQty = strReQty
            'Else
            '    pai.WI_Qty = strQty
            '    pai.WI_ReQty = strReQty - CInt(txtQty.Text)
            'End If

            'pai.WI_Qty1 = strQty1
            'If ComboBoxEdit1.EditValue = "���`" Then
            '    pai.WI_ReQty1 = strReQty1 + CInt(txtQty.Text)
            'Else
            '    pai.WI_ReQty1 = strReQty1 + CInt(txtZuhe.Text)
            'End If
        ElseIf GluDetail.EditValue = "PT16" Then
            pai.WI_Qty = strQty
            pai.WI_ReQty = strReQty - CInt(txtQty.Text)
            pai.WI_ReQty1 = strReQty1
            If ComboBoxEdit1.EditValue = "���`" Then
                pai.WI_Qty1 = strQty1 + CInt(txtQty.Text)
            Else
                pai.WI_Qty1 = strQty1 + CInt(txtZuhe.Text)
            End If
        ElseIf GluDetail.EditValue = "PT01" Or GluDetail.EditValue = "PT14" Then
            ''ElseIf GluDetail.EditValue = "PT01" Then
            pai.WI_Qty = strQty - CInt(txtQty.Text)
            pai.WI_ReQty = strReQty
            pai.WI_ReQty1 = strReQty1
            If ComboBoxEdit1.EditValue = "���`" Then
                pai.WI_Qty1 = strQty1 + CInt(txtQty.Text)
            Else
                pai.WI_Qty1 = strReQty1 + CInt(txtZuhe.Text)
            End If

            'ElseIf GluDetail.EditValue = "PT14" Then

            '    If CheckEdit3.Checked = True Then '����׼�
            '        pai.WI_Qty = strQty
            '        pai.WI_ReQty = strReQty - CInt(txtQty.Text)

            '        pai.WI_Qty1 = strQty1
            '        If ComboBoxEdit1.EditValue = "���`" Then
            '            pai.WI_ReQty1 = strReQty1 + CInt(txtQty.Text)
            '        Else
            '            pai.WI_ReQty1 = strReQty1 + CInt(txtZuhe.Text)
            '        End If
            '    Else
            '        pai.WI_Qty = strQty - CInt(txtQty.Text)
            '        pai.WI_ReQty = strReQty
            '        pai.WI_ReQty1 = strReQty1
            '        If ComboBoxEdit1.EditValue = "���`" Then
            '            pai.WI_Qty1 = strQty1 + CInt(txtQty.Text)
            '        Else
            '            pai.WI_Qty1 = strReQty1 + CInt(txtZuhe.Text)
            '        End If
            '    End If

        ElseIf GluDetail.EditValue = "PT13" Then  '���u
            If CheckEdit3.Checked = False Then     '@ 2012/6/20 �K�[�@�P�_�O�_����׼�
                pai.WI_Qty = strQty - CInt(txtQty.Text)
                pai.WI_ReQty = strReQty
            Else
                pai.WI_Qty = strQty
                pai.WI_ReQty = strReQty - CInt(txtQty.Text)
            End If
            pai.WI_ReQty1 = 0
            pai.WI_Qty1 = 0
        End If


        '--------------------------------------------------------------------
        If GluDetail.EditValue = "PT01" Then '�j�f
            pai.FaLiao = strFaLiao + CInt(txtQty.Text)
            pai.FanXiuOut = strFanXiuOut
            pai.WaiFaOut = strWaiFaOut
            pai.RePairOut = strRePairOut
            pai.ChuHuo = strChuHuo

            If ComboBoxEdit1.EditValue = "���`" Then
                pai.ShouLiao1 = strShouLiao1 + CInt(txtQty.Text)
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1

            Else
                pai.ShouLiao1 = strShouLiao1 + CInt(txtZuhe.Text)
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1
            End If


        ElseIf GluDetail.EditValue = "PT02" Then '���
            pai.FaLiao = strFaLiao
            pai.FanXiuOut = strFanXiuOut + CInt(txtQty.Text)
            pai.WaiFaOut = strWaiFaOut
            pai.RePairOut = strRePairOut
            pai.ChuHuo = strChuHuo
            If ComboBoxEdit1.EditValue = "���`" Then
                pai.ShouLiao1 = strShouLiao1
                pai.FanXiuIn1 = strFanXiuIn1 + CInt(txtQty.Text)
                pai.WaiFaIn1 = strWaiFaIn1

            Else
                pai.ShouLiao1 = strShouLiao1
                pai.FanXiuIn1 = strFanXiuIn1 + CInt(txtZuhe.Text)
                pai.WaiFaIn1 = strWaiFaIn1
            End If

        ElseIf GluDetail.EditValue = "PT14" Then '�~�o
            pai.FaLiao = strFaLiao
            pai.FanXiuOut = strFanXiuOut
            pai.WaiFaOut = strWaiFaOut + CInt(txtQty.Text)
            pai.RePairOut = strRePairOut
            pai.ChuHuo = strChuHuo
            If ComboBoxEdit1.EditValue = "���`" Then
                pai.ShouLiao1 = strShouLiao1
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1 + CInt(txtQty.Text)

            Else
                pai.ShouLiao1 = strShouLiao1
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1 + CInt(txtZuhe.Text)
            End If

        ElseIf GluDetail.EditValue = "PT13" Then  '���u
            pai.FaLiao = strFaLiao
            pai.FanXiuOut = strFanXiuOut
            pai.WaiFaOut = strWaiFaOut
            pai.RePairOut = strRePairOut

            If ComboBoxEdit1.EditValue = "���`" Then
                pai.ShouLiao1 = strShouLiao1
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1
                pai.ChuHuo = strChuHuo + CInt(txtQty.Text)
            Else
                pai.ShouLiao1 = strShouLiao1
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1
                pai.ChuHuo = strChuHuo + CInt(txtZuhe.Text)
            End If
        ElseIf GluDetail.EditValue = "PT16" Then  '�ɪ��
            pai.FaLiao = strFaLiao
            pai.FanXiuOut = strFanXiuOut
            pai.WaiFaOut = strWaiFaOut
            pai.ChuHuo = strChuHuo
            pai.RePairOut = strRePairOut + CInt(txtQty.Text)

            If ComboBoxEdit1.EditValue = "���`" Then
                pai.ShouLiao1 = strShouLiao1 + CInt(txtQty.Text)
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1

            Else
                pai.ShouLiao1 = strShouLiao1 + CInt(txtZuhe.Text)
                pai.FanXiuIn1 = strFanXiuIn1
                pai.WaiFaIn1 = strWaiFaIn1
            End If
        End If
        pai.ShouLiao = strShouLiao
        pai.JiaCun = strJiaCun
        pai.QuCun = strQuCun
        pai.CunHuo = strCunHuo
        pai.FanXiuIn = strFanXiuIn
        pai.LiuBan = strLiuBan
        pai.SunHuai = strSunHuai
        pai.DiuShi = strDiuShi
        pai.BuNiang = strBuNiang
        pai.CunCang = strCunCang
        pai.QuCang = strQuCang
        pai.WaiFaIn = strWaiFaIn
        pai.AccIn = strAccIn
        pai.AccOut = strAccOut
        pai.ZuheOut = strZuheOut


        '------------------------------------------�s�b�������o���p�U
        pai.JiaCun1 = strJiaCun1
        pai.QuCun1 = strQuCun1
        pai.FaLiao1 = strFaLiao1
        pai.CunHuo1 = strCunHuo1
        pai.LiuBan1 = strLiuBan1
        pai.SunHuai1 = strSunHuai1
        pai.DiuShi1 = strDiuShi1
        pai.BuNiang1 = strBuNiang1
        pai.CunCang1 = strCunCang1
        pai.QuCang1 = strQuCang1
        pai.ChuHuo1 = strChuHuo1
        pai.AccIn1 = strAccIn1
        pai.AccOut1 = strAccOut1
        pai.FanXiuOut1 = strFanXiuOut1
        pai.WaiFaOut1 = strWaiFaOut1
        pai.RePairOut1 = strRePairOut1
        pai.ZuheOut1 = strZuheOut1

        '------------------------------------------
        pai.PM_Date = DateEdit1.Text

        If pac.UpdateProductionCheck_Qty(pai) = True Then

            UpdateCheck()
            MsgBox("�T�{��e���Ʀ��o�w�����f��!")

        Else
            MsgBox("��e�T�{�ާ@����,���ˬd��]!")
            Exit Sub
        End If

        '----------------------------------------------�P�_��e�����Ҧb�Ͳ����O�_�s�b�����u�ǹ������Ͳ��p��.�p�G�S���N�K�[!
        Dim psi1 As List(Of ProductionScheduleInfo)
        Dim psc1 As New ProductionScheduleControl
        Dim psi2 As New ProductionScheduleInfo
        Dim psc2 As New ProductionScheduleControl

        Dim strFacID As String
        Dim strCode As String

        If AutoSchedule = True Or (Mid(gluChangeDep.EditValue, 1, 1) <> "W" And Mid(gluChangeDep.EditValue, 1, 1) <> "F" And Mid(gluChangeDep.EditValue, 1, 1) <> "P") Then



            strFacID = Mid(gluChangeDep.EditValue, 1, 1)

            strCode = Transfer(GluEdit2.EditValue)

            psi1 = psc1.ProductionSchedule_GetList1(Nothing, cbType.EditValue, PM_M_Code.EditValue, strCode, strFacID, Nothing, DateEdit1.Text, DateEdit1.Text, gluType.EditValue)

            If psi1.Count = 0 Then

                psi2.PS_NO = GetPSNO()
                psi2.Pro_Type = cbType.EditValue
                psi2.PM_M_Code = PM_M_Code.EditValue
                psi2.PM_Type = gluType.EditValue
                psi2.M_Code = strCode
                psi2.PS_KaiLiao = False
                psi2.PS_Detail = ""
                psi2.PS_Action = InUserID
                psi2.PS_Dep = strFacID  '�Ͳ��p���t�O
                psi2.PS_Remark = txtRemark.Text
                psi2.PS_AddDate = Format(Now, "yyyy/MM/dd")

                psi2.PS_Num = GetPSNum()
                psi2.PS_DayNumber = 0
                psi2.PS_Date = DateEdit1.Text

                If psc2.ProductionSchedule_Add(psi2) = False Then
                    MsgBox("�K�[����,���ˬd��]!")
                    Exit Sub
                End If


            Else
                Exit Sub
            End If

        End If
        If Label36.Text <> "���~��" Then
            Me.Close()
        End If
    End Sub
    '����̷s���ظ�
    Public Function GetPSNO() As String
        Dim psi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl
        Dim strName As String
        strName = Format(Now, "yyMM")
        psi = psc.ProductionSchedule_GetNO(strName)
        If psi Is Nothing Then
            GetPSNO = "PS" + strName + "0001"
        Else
            GetPSNO = "PS" + strName + Mid((CInt(Mid(psi.PS_NO, 7)) + 10001), 2)
        End If

    End Function

    '����̷s������جy����
    Public Function GetPSNum() As String
        Dim psi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl
        Dim strName As String
        strName = "P" + Format(Now, "yyMM")
        psi = psc.ProductionSchedule_GetNum(strName)
        If psi Is Nothing Then
            GetPSNum = strName + "00001"
        Else
            GetPSNum = strName + Mid((CInt(Mid(psi.PS_Num, 6)) + 100001), 2)
        End If
    End Function
    Sub UpdateCheck()

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        pi.FP_NO = txtNo.Text
        pi.FP_Check = True

        pi.FP_CheckAction = InUserID
        pi.FP_CheckRemark = txtCheckremark.Text

        If pc.ProductionField_UpdateCheck(pi) = False Then
            MsgBox("�f�֥���,�ФήɻP�q�����p�t!")
        End If

        'If pc.ProductionField_UpdateCheck(pi) = True Then
        '    MsgBox("�f�֦��\")
        'Else
        '    MsgBox("�f�֦��\,���ˬd��]!")
        'End If
        'Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label18.Text
            Case "CodeOut"
                If Edit = False Then

                    Dim fpi As List(Of ProductionFieldInfo)
                    Dim fpc As New ProductionFieldControl

                    fpi = fpc.ProductionField_GetList(txtNo.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If fpi.Count = 0 Then

                        If CheckData() = True Then
                            If DataNew() = True Then

                                'If Label36.Text = "���~��" Then

                                '    CheckEdit1.Checked = True

                                '    UpdateInCheck()   '��������T�{--1.�˰t����--�u�Ǽƶq��֡F2.���~��--�t��ƶq�W�[

                                '    '--------------------
                                '    Dim psi As New ProductionScheduleInfo
                                '    Dim psc As New ProductionScheduleControl
                                '    Dim psi1 As List(Of ProductionScheduleInfo)

                                '    psi1 = psc.ProductionSchedule_GetList1(Nothing, cbType.EditValue, PM_M_Code.EditValue, Transfer(GluEdit1.EditValue), Nothing, Nothing, DateEdit1.Text, DateEdit1.Text, gluType.EditValue)

                                '    If psi1.Count = 0 Then

                                '    Else


                                '        If psi1(0).PS_DayNumber > 0 Then

                                '            Dim strNumber As Integer

                                '            strNumber = psi1(0).PS_ActualNumber

                                '            psi.PS_NO = psi1(0).PS_NO '�o���e�渹
                                '            psi.Pro_Type = cbType.EditValue
                                '            psi.PM_M_Code = PM_M_Code.EditValue

                                '            psi.PM_Type = gluType.EditValue
                                '            psi.PS_Dep = psi1(0).PS_Dep
                                '            psi.PS_Date = CDate(DateEdit1.Text)

                                '            psi.PS_ActualNumber = strNumber + txtQty.Text

                                '            psc.ProductionSchedule_UpdateActualNumber(psi)

                                '        End If

                                '    End If

                                '    '-------------------'�ܧ��Ѧ����~���Ͳ��p����ڧ����q 
                                '    '------------------ 

                                'End If
                                Me.Close()
                            End If
                        End If
                    Else
                        MsgBox("�渹�w�s�b�A" & vbCr & "�нT�w���s�ͦ��渹!", 64, "����")
                        txtNo.Text = GetNO()

                    End If

                Else
                    If CheckData() = True Then

                        DataEdit()
                    End If

                End If
            Case "InCheck"
                If CheckData() = True Then
                    UpdateInCheck()
                    'UpdateCheck()
                    Me.Close()
                End If
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        If (e.KeyValue > 47 And e.KeyValue < 58) Or (e.KeyValue > 95 And e.KeyValue < 106) Or (e.KeyValue = 8) Or (e.KeyValue = 45) Or (e.KeyValue = 46) Then
            Dim pc As New ProcessMainControl
            Dim pci As List(Of ProcessMainInfo)

            pci = pc.ProcessSub_GetList(Nothing, GluEdit1.EditValue, Nothing, Nothing, Nothing, Nothing)
            If pci.Count = 0 Then Exit Sub

            Dim AllWeight, strWeight, strG As Single

            strWeight = pci(0).PS_Weight  '�J/��  �歫
            strG = strWeight * txtQty.Text
            AllWeight = strG / 1000  '��e�ƶq�����q(KG)
            txtWeight.Text = Format(AllWeight, "0.00") '(��Ƭ����p��)
        Else
            'MsgBox("�u���J��ƼƦr�I")
            txtQty.Text = Nothing
            Exit Sub
        End If
    End Sub

    '�q�L���~�s���o������������H��
    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged

        On Error Resume Next
        If PM_M_Code.EditValue = "" Or PM_M_Code.EditValue Is Nothing Then Exit Sub

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()
        ds.Tables("ProductType1").Clear()
        ppi = ppc.ProcessMain_GetList2(cbType.EditValue, PM_M_Code.EditValue)
        If ppi.Count = 0 Then
        Else

            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("PM_Type") = ppi(i).Type3ID
                ds.Tables("ProductType").Rows.Add(row)

                '-------------------------------------------------
                Dim row1 As DataRow
                row1 = ds.Tables("ProductType1").NewRow
                row1("PM_Type") = ppi(i).Type3ID          '---�q�{���p�U,�o�������P���������ۦP
                ds.Tables("ProductType1").Rows.Add(row1)
                '-------------------------------------------------
            Next
        End If
        'gluProduct.Properties.DataSource = PM_M_Code.Properties.DataSource
        gluProduct.EditValue = PM_M_Code.EditValue   '�q�{���p�U,�o�Ʋ��~�s���P���Ʋ��~�s���ۦP
        gluType.EditValue = ds.Tables("ProductType").Rows(0)("PM_Type").ToString
        'If Len(Now.Second) = 1 Then
        'strSecond = "0" & CStr(Now.Second)
        'Else
        '    strSecond = Now.Second
        'End If


    End Sub

    '��������Z--�ھڲ��~�s��+�����ӾɤJ���������~�u���u�ǫH��
    '@ 2012/2/22 �ק� ���ե�LoadOutPs_Name()�L�{
    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged


        'Dim pc As New ProcessMainControl
        'Dim pci As List(Of ProcessMainInfo)
        'pci = pc.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType.EditValue, gluType.EditValue, Nothing, True)
        'gluChangeDep.EditValue = Nothing
        If gluType.EditValue = "" Or gluType.EditValue Is Nothing Then Exit Sub
        Try
            gluType1.EditValue = gluType.EditValue
            LoadOutPs_Name()
            GluEdit1.EditValue = ds.Tables("Process").Rows(0)("PS_NO").ToString
            'GluEdit2.EditValue = Nothing
            'LoadDep()
            '    If pci.Count = 0 Then Exit Sub
            '    ds.Tables("Process").Clear()
            '    ds.Tables("Process1").Clear()
            '    Dim i As Integer
            '    For i = 0 To pci.Count - 1
            '        Dim row As DataRow
            '        row = ds.Tables("Process").NewRow

            '        row("PS_NO") = pci(i).PS_NO
            '        row("PS_Name") = pci(i).PS_Name

            '        ds.Tables("Process").Rows.Add(row)

            '        '------------------------------------------------
            '        Dim row1 As DataRow
            '        row1 = ds.Tables("Process1").NewRow

            '        row1("PS_NO") = pci(i).PS_NO
            '        row1("PS_Name") = pci(i).PS_Name        '---�q�{���p�U--���Ƥu�ǻP�o�Ƥu�ǬۦP

            '        ds.Tables("Process1").Rows.Add(row1)

            '        '------------------------------------------------
            '    Next


            LoadDep()

            If GluDetail.EditValue = "PT13" Then '���u�ݩ�

                '���P�_�O�_�O�˰t--���~�ܡ@�Y�@Ok �h�q�{���ƭܮw�����~��  �����f�֧���
                '�Y���O�˰t--�e���~��   �h�e�ܥͲ��� If GluDetail.EditValue = "PT13" and Label36.Text <> "���~��" Then 

                If Label36.Text = "���~��" Then

                    gluChangeDep.EditValue = "W1101"  '���~��
                    gluChangeDep.Enabled = False

                Else

                    Dim psi As List(Of LFERP.Library.ProductionSchedule.ProductionScheduleInfo)
                    Dim psc As New LFERP.Library.ProductionSchedule.ProductionScheduleControl
                    ' psi = psc.ProductionSchedule_GetList(Nothing, cbType.EditValue, Nothing, PM_M_Code.EditValue, gluType.EditValue, Nothing, Nothing, Nothing)
                    psi = psc.ProductionSchedule_GetList(Nothing, cbType.EditValue, Nothing, PM_M_Code.EditValue, gluType.EditValue, DateEdit1.EditValue, DateEdit1.EditValue, Nothing)
                    If psi.Count = 0 Then
                        gluChangeDep.Enabled = True
                    Else

                        ''''''''''''''''''''���W�ק�''''''''''''''''''''''''
                        'Dim row As DataRow
                        'row = ds.Tables("Dep").NewRow
                        'If psi(0).PS_Dep = "A" Then
                        '    row("DepID") = "W0801"
                        '    row("DepName") = "�Ĥ@�Ͳ���"
                        '    row("FacName") = "�ܮw"
                        '    ds.Tables("Dep").Rows.Add(row)
                        '    gluChangeDep.EditValue = "W0801"  '�Ĥ@�Ͳ���
                        'ElseIf psi(0).PS_Dep = "B" Then
                        '    row("DepID") = "W0802"
                        '    row("DepName") = "�ĤG�Ͳ���"
                        '    row("FacName") = "�ܮw"
                        '    ds.Tables("Dep").Rows.Add(row)
                        '    gluChangeDep.EditValue = "W0802"  '�ĤG�Ͳ���
                        'ElseIf psi(0).PS_Dep = "C" Then
                        '    row("DepID") = "W0803"
                        '    row("DepName") = "�ĤT�Ͳ���"
                        '    row("FacName") = "�ܮw"
                        '    ds.Tables("Dep").Rows.Add(row)
                        '    gluChangeDep.EditValue = "W0803" '�ĤT�Ͳ���
                        'ElseIf psi(0).PS_Dep = "D" Then
                        '    row("DepID") = "W0921"
                        '    row("DepName") = "�˰t��(���~)"
                        '    row("FacName") = "�ܮw"
                        '    ds.Tables("Dep").Rows.Add(row)
                        '    gluChangeDep.EditValue = "W0921" '�˰t��(���~)
                        'End If
                        Dim row As DataRow
                        row = ds.Tables("Dep").NewRow
                        Dim fcon As New FacControler
                        Dim flist As New List(Of FacInfo)
                        flist = fcon.GetFacListA(psi(0).PS_Dep, Nothing, Nothing)
                        If flist.Count > 0 Then
                            row("DepID") = flist(0).FacRemark
                            row("DepName") = flist(0).FacName
                            row("FacName") = "�ܮw"
                            ds.Tables("Dep").Rows.Add(row)
                            gluChangeDep.EditValue = flist(0).FacRemark
                        End If
                        ''''''''''''''''''''''''''''''''''''''''''''''''''''

                        gluChangeDep.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception

        End Try


    End Sub

    '�q�{���p�U---��e���o�Ƥu�������P���Ƥu�������ۦP
    Private Sub cbType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbType.SelectedValueChanged
        cbType1.EditValue = cbType.EditValue
    End Sub

    Private Sub gluProduct_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluProduct.EditValueChanged

        On Error Resume Next
        If gluProduct.EditValue = "" Or gluProduct.EditValue Is Nothing Then Exit Sub

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType1").Clear()
        ppi = ppc.ProcessMain_GetList2(cbType1.EditValue, gluProduct.EditValue)
        If ppi.Count = 0 Then
        Else
            Dim i As Integer
            For i = 0 To ppi.Count - 1
                '-------------------------------------------------
                Dim row1 As DataRow
                row1 = ds.Tables("ProductType1").NewRow
                row1("PM_Type") = ppi(i).Type3ID          '---���J��������
                ds.Tables("ProductType1").Rows.Add(row1)
                '-------------------------------------------------
            Next
            gluType1.EditValue = ds.Tables("ProductType1").Rows(0)("PS_NO").ToString
        End If
        gluType1_EditValueChanged(Nothing, Nothing)
    End Sub

    '����Panel�H��
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Panel1.Visible = False
        strIndex = 0

    End Sub
    '���J�s���u�ǽs�X�H��
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pc.ProcessMain_GetList(Nothing, gluProduct.EditValue, cbType1.EditValue, gluType1.EditValue, Nothing, True)

        Try
            If pci.Count = 0 Then Exit Sub
            ds.Tables("Process1").Clear()

            Dim i As Integer
            For i = 0 To pci.Count - 1
                '------------------------------------------------
                Dim row1 As DataRow
                row1 = ds.Tables("Process1").NewRow

                row1("PS_NO") = pci(i).PS_NO
                row1("PS_Name") = pci(i).PS_Name        '---���J���Ƥu�������u�ǽs���H��

                ds.Tables("Process1").Rows.Add(row1)

                '------------------------------------------------
            Next
            'Panel1.Visible = False
            strIndex = 1
            'GluEdit2.EditValue = ds.Tables("Process1").Rows(0)("PS_NO").ToString
            LoadInPs_Name()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel1.Visible = Not Panel1.Visible
    End Sub
    '�O���u������,���~�s��,����,�u�ǤU���`���J�ƶq,�`�o�X�ƶq,���l�ƶq(��ڵ��l��)
    Private Sub GluEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluEdit1.EditValueChanged

        If GluEdit1.EditValue = "" Or GluEdit1.EditValue Is Nothing Then Exit Sub

        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)

        If (GluDep.EditValue = "F101" And GluDetail.EditValue = "PT14") Or GluDetail.EditValue = "PT13" Then
            ' GluEdit2.Enabled = False  '2013-10-28
            LoadInPs_Name()
            GluEdit2.EditValue = GluEdit1.EditValue
        ElseIf GluDetail.EditValue = "PT14" Then
            LoadInPs_Name()
        Else
            GluEdit2.Enabled = True
        End If

        If GluEdit1.Text = "" Then     '@ 2012/7/27 �K�[
            Label32.Text = 0
            Exit Sub
        End If

        fdi = fdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)
        If fdi.Count = 0 Then
            Label32.Text = 0
            DeclareQtyAll = 0
        Else
            If GluDetail.EditValue = "PT16" Or GluDetail.EditValue = "PT02" Then

                Label29.Text = "��׵��l�ơG"
                Label32.Text = fdi(0).WI_ReQty
                DeclareQty = fdi(0).WI_Qty  '��e�j�f�ƶq


                '2013
                DeclareQtyAll = fdi(0).WI_ReQty + fdi(0).WI_Qty
                LabelAllQty.Text = DeclareQtyAll


            Else
                Label29.Text = "�j�f���l�ơG"
                Label32.Text = fdi(0).WI_Qty
                CheckEdit3.Checked = False    '@ 2012/6/20 �K�[
            End If
        End If

        'txtQty.Text = Label32.Text

    End Sub

    Private Sub txtWeight_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeight.KeyUp
        Dim m As New System.Text.RegularExpressions.Regex("^+?(\d+(\.\d*)?|\.\d+)$")  '��ܾ��,���B�I�ƥ��h��F��

        If m.IsMatch(txtWeight.Text) = True Then

        Else

            txtWeight.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub txtZuhe_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtZuhe.KeyUp
        If (e.KeyValue > 47 And e.KeyValue < 58) Or (e.KeyValue > 95 And e.KeyValue < 106) Or (e.KeyValue = 8) Or (e.KeyValue = 45) Or (e.KeyValue = 46) Then

        Else
            'MsgBox("�u���J��ƼƦr�I")
            txtZuhe.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub ComboBoxEdit1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxEdit1.SelectedValueChanged
        If ComboBoxEdit1.EditValue = "���`" Then
            Label28.Visible = False
            txtZuhe.Visible = False
        ElseIf ComboBoxEdit1.EditValue = "�զX" Then
            Label28.Visible = True
            txtZuhe.Visible = True
        End If
    End Sub

    Function Transfer(ByVal PS_NO As String) As String  '�N�u�ǽs����Ƭ����~�u���H��������l�s�X(���ƽs�X,���ƦW��)
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        pci = pc.ProcessSub_GetList(Nothing, PS_NO, Nothing, Nothing, Nothing, Nothing)
        If pci.Count = 0 Then
            MsgBox("��e���s�b���u�ǽs�X,�нT�{��J���T!")
            Transfer = Nothing
            Exit Function
        Else
            Transfer = pci(0).M_Code    '���������ƽs�X�H��
        End If

    End Function

    Function CheckData() As Boolean
        CheckData = True

        If TextEdit1.Text = "" And TextEdit1.Visible = True And chkFP_OutOK.Checked = True Then     '@2012/7/12 �ק�
            MsgBox("��d�H�H�����ର�šI")
            CheckData = False
            Exit Function
        End If
        If GluDep.EditValue = "" Then
            MsgBox("�o�X�������ର�šI")
            CheckData = False
            Exit Function
        End If
        If gluChangeDep.EditValue = "" Then
            MsgBox("���J�������ର�šI")
            CheckData = False
            Exit Function
        End If

        If Len(txtQty.Text.Trim) = 0 Then
            MsgBox("�ƶq�H�����ର�šI")
            CheckData = False
            Exit Function
        End If
        If Len(txtWeight.Text.Trim) = 0 Then
            MsgBox("���q�H�����ର�šI")
            CheckData = False
            Exit Function
        End If

        If GluEdit1.EditValue = "" Then
            MsgBox("�o�X�u�Ǥ��ର�šI")
            CheckData = False
            Exit Function
        End If
        If GluEdit2.EditValue = "" Then
            MsgBox("���Ƥu�Ǥ��ର�šI")
            CheckData = False
            Exit Function
        End If

        '2013-10-28
        If GluDep.EditValue = gluChangeDep.EditValue And GluEdit1.EditValue = GluEdit2.EditValue Then
            MsgBox("�P�@����,���ব�o�P�@�u�ǫH���I")
            CheckData = False
            Exit Function
        End If



        ''2013-5-20 ������u�ǽs��,�L�u�ǦW��
        If GluEdit1.Text = "" Then
            MsgBox("�o�X�u�Ǥ��ର��.")
            CheckData = False
            Exit Function
        End If
        If GluEdit2.Text = "" Then
            MsgBox("���Ƥu�Ǥ��ର��.")
            CheckData = False
            Exit Function
        End If

        If TextEdit1.Text <> "" And TextEdit1.Visible = True And chkFP_OutOK.Checked = False Then   '@2012/7/12 �K�[
            MsgBox("�п�ܽT�{�o�X�I")
            CheckData = False
            Exit Function
        End If

        If AutoSchedule = False Then '�۰ʥͦ��Ͳ��p�E
            Dim psi As List(Of LFERP.Library.ProductionSchedule.ProductionScheduleInfo)
            Dim psc As New LFERP.Library.ProductionSchedule.ProductionScheduleControl

            psi = psc.ProductionSchedule_GetList(Nothing, cbType.EditValue, Nothing, PM_M_Code.EditValue, gluType.EditValue, DateEdit1.Text, DateEdit1.Text, Nothing)
            If psi.Count = 0 Then
                MsgBox("��e�Ͳ������s�b��w������Ͳ��p���A�Х��K�[�Ͳ��p���I.")
                CheckData = False
                Exit Function
            Else
                CheckData = True
            End If
        End If


        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)

        fdi = fdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)

        If fdi.Count = 0 Then
            MsgBox("��e�o�X�����u�Ǽƶq����,�����\�f�֡I")
            CheckData = False
            Exit Function
        Else
            ' If GluDetail.EditValue = "PT16" Or GluDetail.EditValue = "PT02" Then '2013-5-16
            If GluDetail.EditValue = "PT16" Then
                Label32.Text = fdi(0).WI_ReQty
                ''If fdi(0).WI_ReQty = 0 Then
                ''    If fdi(0).WI_Qty >= txtQty.Text Then
                ''        CheckData = True
                ''    Else
                ''        MsgBox("��e�j�f�ƶq�p��o�X�ƶq�I")
                ''        CheckData = False
                ''        Exit Function
                ''    End If
                ''Else
                ''    If fdi(0).WI_ReQty >= txtQty.Text Then
                ''        CheckData = True
                ''    Else
                ''        MsgBox("��e��׼ƶq�p��o�X�ƶq�I")
                ''        CheckData = False
                ''        Exit Function
                ''    End If
                ''End If

                ''MsgBox(fdi(0).WI_Qty & "-----" & fdi(0).WI_ReQty & "--" & txtQty.Text)

                If fdi(0).WI_ReQty >= txtQty.Text Then '2013-5-21 �ɪ�׮�,�u�P�_��׼ƶq�O�_����
                    CheckData = True
                Else
                    MsgBox("��e��׼ƶq�p��o�X�ƶq�I")
                    CheckData = False
                    Exit Function
                End If

            ElseIf GluDetail.EditValue = "PT02" Then '2013-5-16

                LabelAllQty.Text = fdi(0).WI_ReQty + fdi(0).WI_Qty
                DeclareQtyAll = fdi(0).WI_ReQty + fdi(0).WI_Qty

                If DeclareQtyAll >= Val(txtQty.Text) Then  '��צ^ �P�_�j�f+��׸`�E�ƶq
                    CheckData = True
                Else
                    MsgBox("��e�j�f+��׼ƶq�p��o�X�ƶq�I")
                    CheckData = False
                    Exit Function
                End If
            Else
                If CheckEdit3.Checked = False Then    '@ 2012/6/20 �K�[�@�P�_����׼ƮɡA��׮w�s�O�_����
                    If fdi(0).WI_Qty >= txtQty.Text Then
                        CheckData = True
                    Else
                        MsgBox("��e�j�f�ƶq�p��o�X�ƶq�I")
                        CheckData = False
                        Exit Function
                    End If
                Else
                    If fdi(0).WI_ReQty >= txtQty.Text Then
                        CheckData = True
                    Else
                        MsgBox("��e��׼ƶq�p��o�X�ƶq�I")
                        CheckData = False
                        Exit Function
                    End If
                End If
            End If

        End If



        ''----------------------�P�_�b���~�@�ή�(���Ƥu�ǻP���Ʋ��~�O�_�@��)2012-7-20------------------------------
        Dim pc1 As New ProcessMainControl
        Dim pci1 As List(Of ProcessMainInfo)

        pci1 = pc1.ProcessMain_GetList(Nothing, gluProduct.EditValue, cbType1.EditValue, gluType1.EditValue, Nothing, True)
        Dim i As Integer
        Dim bz = ""
        For i = 0 To pci1.Count - 1
            If GluEdit2.EditValue = pci1(i).PS_NO Then
                bz = "Y"
            End If
        Next

        If bz <> "Y" Then
            MsgBox("���Ʋ��~���s�b��w�����Ƥu�ǽs��,�Э��s[���]�I")
            CheckData = False
            Exit Function
        End If



        ''2013-10-28
        '�b�~�o/���u��,�ಣ�~��-�[�W�P�_ProductionProductInner
        If GluDetail.EditValue = "PT13" Or (GluDetail.EditValue = "PT14" And GluDep.EditValue = "F101") Then

            If GluDetail.EditValue = "PT13" And cbType.EditValue <> cbType1.EditValue Then
                MsgBox("�����u��,�o��" & "[" & GluDetail.Text & "]")
                CheckData = False
                Exit Function
            End If

            If PM_M_Code.EditValue <> gluProduct.EditValue Then
                Dim pcn As New ProductionProductInnerControl
                Dim pcl As New List(Of ProductionProductInnerInfo)

                pcl = pcn.ProductionProductInner_GetList(cbType.EditValue, PM_M_Code.EditValue, gluType.EditValue, cbType1.EditValue, gluProduct.EditValue, gluType1.EditValue)
                If pcl.Count <= 0 Then
                    MsgBox("�����ಣ�~���o,�г]�m�I")
                    CheckData = False
                    Exit Function
                End If
            End If
        End If
        '------------�ק�ɭn�A�P�_�@�U--------------------------------------------------------------------
        If Edit = True Then
            Dim pfi1 As New List(Of ProductionFieldInfo)
            Dim pfc As New Library.ProductionField.ProductionFieldControl
            pfi1 = pfc.ProductionField_GetList(txtNo.Text, Nothing, "����", Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, "1", Nothing, Nothing)
            If pfi1.Count <> 0 Then
                MsgBox("����w�Q�T�{,�����\�ק�!")
                CheckData = False
                Exit Function
            End If
        End If


    End Function

    Private Sub cbType1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType1.SelectedIndexChanged
        If gluProduct.Text <> "" Then
            gluProduct_EditValueChanged(Nothing, Nothing)

            '2013
            gluType1_EditValueChanged(Nothing, Nothing)

        End If
    End Sub

    '@ 2012/2/22 �K�[ �ƥ�
    Private Sub gluChangeDep_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluChangeDep.EditValueChanged
        'On Error Resume Next
        If gluChangeDep.EditValue = "" Or gluChangeDep.EditValue Is Nothing Then
            'ds.Tables("Process1").Clear()
            Exit Sub
        End If

        LoadInPs_Name()

    End Sub

    Private Sub GluEdit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GluEdit2.Click
        'If strIndex <> 1 Then
        '    LoadInPs_Name()
        'End If

    End Sub
    '2012/3/28�O����e��d�H�H��
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click

        TextEdit1.Text = ReadCard() 'Ū���d��
        If TextEdit1.Text <> "" Then
            chkFP_OutOK.Checked = True
        End If
        'Label31.Visible = True
        'Label31.Text = strCardName

    End Sub

    '2012-5-2 �w���d���`�B�z��k�]��ʸɥd�O���^
    '���i�J�n�������ЬݬO�_���i���ʸɥd�v��  Y --�O���u���A�m�W;N--���ܡF�h�X
    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Dim frm As New frmNmetalSampleException
        frm.ShowDialog()

        TextEdit1.Text = tempValue
        tempValue = ""
    End Sub

    '@ 2012/6/20 �K�[�@����׼ƴ_��حȧ��ܮɡA��ܬ������w�s
    Private Sub CheckEdit3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit3.CheckedChanged
        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)

        If GluEdit1.Text = "" Or GluEdit1.EditValue Is Nothing Then       '@ 2012/7/27 �K�[
            Label32.Text = 0
            Exit Sub
        End If

        fdi = fdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)
        If fdi.Count = 0 Then
            Label32.Text = 0
            Exit Sub
        End If

        If CheckEdit3.Checked = False Then
            Label32.Text = fdi(0).WI_Qty
            Label29.Text = "�j�f���l�ơG"
        Else
            Label32.Text = fdi(0).WI_ReQty
            Label29.Text = "��׵��l�ơG"
        End If

    End Sub

    Private Sub DateEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateEdit1.EditValueChanged
        ''2012-7-16 ���q�N�X�D�n�w�� ���u�A�B���e���~�ܮw �������Ͳ��p�������ܮw����
        If gluType.EditValue Is Nothing Or gluType.Text = "" Then Exit Sub

        If GluDetail.EditValue = "PT13" And Label36.Text <> "���~��" Then
            gluType_EditValueChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub gluType1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType1.EditValueChanged
        If gluType1.EditValue = "" Or gluType1.EditValue Is Nothing Then Exit Sub

        If Panel1.Visible = True Then
            LoadInPs_Name()
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Select Case Label18.Text
            Case "CodeOut"
                If Edit = False Then

                    Dim fpi As List(Of ProductionFieldInfo)
                    Dim fpc As New ProductionFieldControl

                    fpi = fpc.ProductionField_GetList(txtNo.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                    If fpi.Count = 0 Then

                        If CheckData() = True Then
                            If DataNew() = True Then

                                If Label36.Text = "���~��" Then

                                    CheckEdit1.Checked = True

                                    UpdateInCheck()   '��������T�{--1.�˰t����--�u�Ǽƶq��֡F2.���~��--�t��ƶq�W�[

                                    '--------------------
                                    Dim psi As New ProductionScheduleInfo
                                    Dim psc As New ProductionScheduleControl
                                    Dim psi1 As List(Of ProductionScheduleInfo)

                                    psi1 = psc.ProductionSchedule_GetList1(Nothing, cbType.EditValue, PM_M_Code.EditValue, Transfer(GluEdit1.EditValue), Nothing, Nothing, DateEdit1.Text, DateEdit1.Text, gluType.EditValue)

                                    If psi1.Count = 0 Then

                                    Else
                                        If psi1(0).PS_DayNumber > 0 Then

                                            Dim strNumber As Integer

                                            strNumber = psi1(0).PS_ActualNumber

                                            psi.PS_NO = psi1(0).PS_NO '�o���e�渹
                                            psi.Pro_Type = cbType.EditValue
                                            psi.PM_M_Code = PM_M_Code.EditValue

                                            psi.PM_Type = gluType.EditValue
                                            psi.PS_Dep = psi1(0).PS_Dep
                                            psi.PS_Date = CDate(DateEdit1.Text)

                                            psi.PS_ActualNumber = strNumber + txtQty.Text

                                            psc.ProductionSchedule_UpdateActualNumber(psi)

                                        End If

                                    End If

                                    '-------------------'�ܧ��Ѧ����~���Ͳ��p����ڧ����q 
                                    '------------------ 

                                End If
                                PM_M_Code.EditValue = ""
                                ds.Tables("ProductType").Rows.Clear()
                                gluType.Text = ""
                                If GluDetail.EditValue <> "PT14" Then
                                    gluChangeDep.EditValue = Nothing
                                End If
                                ds.Tables("Process").Rows.Clear()
                                GluEdit1.Text = ""
                                ds.Tables("Process1").Rows.Clear()
                                GluEdit2.Text = ""
                                txtQty.Text = ""
                                txtWeight.Text = ""
                                txtRemark.Text = ""
                            End If
                        End If
                    Else
                        MsgBox("�渹�w�s�b�A" & vbCr & "�нT�w���s�ͦ��渹!", 64, "����")
                        txtNo.Text = GetNO()

                    End If

                Else
                    If CheckData() = True Then

                        DataEdit()
                    End If

                End If
            Case "InCheck"
                If CheckData() = True Then
                    UpdateInCheck()
                    'UpdateCheck()
                    Me.Close()
                End If
        End Select
    End Sub




    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        Dim reset As New ResetPassWords.SetPassWords
        reset.SetPassWords()
    End Sub

    Private Sub txtQty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.EditValueChanged

    End Sub
End Class