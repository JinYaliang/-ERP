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

Public Class frmProductionFieldWare

    Dim pc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim ds As New DataSet
    Dim mc As New ProductionDataSettingControl

    Dim upi As List(Of UserPowerInfo)
    Dim upc As New UserPowerControl

    Dim strGluDep As String
    Dim strCaoType As String
    Dim strFP_Detail As String ''����pt19,pt18
    Dim OldCheck As Boolean
    Dim ViewType As String '�P�_���Ƭd��/�o�Ƭd��
    Dim strPO_NO As String

    Dim LoadBZ As String = ""
    Dim strFP_Type As String = "" '����/�o��
    Dim GroupBoxAX, GroupBoxAY, GroupBoxBH As Double


    Private Sub frmProductionFieldWare_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PM_JiYu1.Caption = strJIYU

        If strRefCard = "�O" Then
        Else
            SimpleButton1.Visible = False
            TextEdit1.Visible = False
            SimpleButton2.Visible = False
        End If

        strGluDep = tempValue3
        strCaoType = tempValue
        strFP_Detail = tempValue2
        ViewType = tempValue6
        strPO_NO = tempValue4

        tempValue = Nothing
        tempValue3 = Nothing
        tempValue2 = Nothing
        tempValue4 = Nothing
        tempValue6 = Nothing
        '--------------------------------
        GroupBoxAX = GroupBoxA.Location.X : GroupBoxAY = GroupBoxA.Location.Y : GroupBoxBH = GroupBoxB.Height
        If Edit <> True Then
            LoadEnabled(strFP_Detail)
        End If
        '---------------------------------
        LoadProductionDetail()

        CreateTable()

        '-------------------------------
        Select Case strCaoType
            Case "CodeOut" '�o��
                '---------------------------------------------------------------
                If Edit = False Then

                    LoadDep(strGluDep, strFP_Detail)
                    LoadDep1(strGluDep, strFP_Detail)
                    LoadPM_M_Code(strGluDep)

                    upi = upc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)
                    If upi.Count = 0 Then Exit Sub
                    cbType1.EditValue = upi(0).UserType
                    '---------------------------------------------------------------
                    GluDetail.EditValue = strFP_Detail
                    DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")

                    If strFP_Detail = "PT18" Then
                        GluDep.EditValue = strGluDep
                        Me.Text = upi(0).UserType & "-�o��"
                        LabCaption.Text = upi(0).UserType & "-�o��"
                    ElseIf strFP_Detail = "PT19" Then
                        GluDep1.EditValue = strGluDep
                        Me.Text = "�~�o-�o�˰t"
                        LabCaption.Text = "�~�o-�o�˰t"
                    ElseIf strFP_Detail = "PT20" Then
                        GluDep1.EditValue = strGluDep
                        Me.Text = "�o�ˤ��}-�o�˰t��"
                        LabCaption.Text = "�o�ˤ��}-�o�˰t��"
                    End If

                    txtNo.Text = GetNO()
                Else
                    LoadData(strPO_NO)
                    LoadEnabled(strFP_Detail)
                End If
            Case "PreView"
                Me.GroupBoxA.Enabled = False
                Me.GroupBoxB.Enabled = False
                Me.GroupBoxC.Enabled = False
                cmdSave.Visible = False
                LoadData(strPO_NO)
                LoadEnabled(GluDetail.EditValue)
            Case "InCheck"

                Me.GroupBoxA.Enabled = False
                Me.GroupBoxB.Enabled = False
                Me.GroupBoxC.Enabled = False
                CheckEdit1.Enabled = True

                LoadData(strPO_NO)
                LoadEnabled(GluDetail.EditValue)
        End Select

        LoadBZ = "Y"


        Dim reset As New ResetPassWords.SetPassWords
        reset.SetPassWords()
    End Sub

#Region "���J�򥻫H��"

    ''' <summary>
    ''' �w�藍�P�����[�����󪺥i�Ϊ��A
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadEnabled(ByVal Type As String) As Boolean
        Select Case Type
            Case "PT18"
                GroupBoxA.Text = "�o��"
                GroupBoxB.Text = "����"
                GluDep.Enabled = False
                PM_M_Code.Enabled = True
                gluType.Enabled = True

                cbType1.Enabled = False
                PM_M_Code1.Enabled = True

            Case "PT19", "PT20"
                GroupBoxA.Text = "����"
                GroupBoxB.Text = "�o��"

                GluDep.Enabled = True
                GluDep1.Enabled = False

                PM_M_Code1.Enabled = True
                PM_M_Code.Enabled = True


                GroupBoxB.Location = New Point(GroupBoxAX, GroupBoxAY)
                GroupBoxA.Location = New Point(GroupBoxAX, GroupBoxAY + GroupBoxBH + 2)
        End Select
    End Function

    Sub LoadProductionDetail() ' �ݩ�
        Dim mc As New ProductionFieldTypeControl
        GluDetail.Properties.DisplayMember = "PT_Type"
        GluDetail.Properties.ValueMember = "PT_NO"
        GluDetail.Properties.DataSource = mc.ProductionFieldType_GetList(Nothing, Nothing, "�o��/����")
    End Sub


    Sub LoadDep(ByVal GluDepA As String, ByVal TypeA As String)   '�����H��
        If TypeA = "PT18" Then
            GluDep.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)  '�ܧ󳡪�
            GluDep.Properties.DisplayMember = "DepName"
            GluDep.Properties.ValueMember = "DepID"
        End If

        If TypeA = "PT19" Or TypeA = "PT20" Then
            Dim mi As List(Of ProductionDataSettingInfo)
            Dim fc As New FacControler
            Dim fi As List(Of FacInfo)
            Dim row As DataRow

            mi = mc.ProductionIncome_GetList(GluDepA, Nothing)
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
                Dim pi As New List(Of WorkGroupInfo)
                Dim pc As New WorkGroupControl
                pi = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing, "�ܮw")
                Dim j%
                For j = 0 To pi.Count - 1

                    row = ds.Tables("Dep").NewRow
                    row("DepID") = pi(j).DepID
                    row("DepName") = pi(j).DepName
                    row("FacName") = pi(j).FacName
                    ds.Tables("Dep").Rows.Add(row)
                Next
            End If
        End If

    End Sub

    '@ 2012/2/22 �K�[
    '�[�����Ƴ����ƾ�
    '���L�{�Q�H�U�L�{�եΡG
    'frmProductionFieldCode_Load()
    'LoadData()
    Sub LoadDep1(ByVal GluDepA As String, ByVal Type As String)
        If Type = "PT19" Or Type = "PT20" Then
            GluDep1.Properties.DataSource = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)  '�ܧ󳡪�
            GluDep1.Properties.DisplayMember = "DepName"
            GluDep1.Properties.ValueMember = "DepID"
        End If

        If Type = "PT18" Then
            Dim mi As List(Of ProductionDataSettingInfo)
            Dim fc As New FacControler
            Dim fi As List(Of FacInfo)
            Dim row As DataRow

            mi = mc.ProductionIncome_GetList(GluDepA, Nothing)
            ds.Tables("Dep1").Clear()

            If mi.Count > 0 Then        '�P�_�O�_���v������
                Dim j%
                For j = 0 To mi.Count - 1
                    fi = fc.GetFacList(Microsoft.VisualBasic.Left(mi(j).FP_InDep, 1), Nothing)
                    If fi.Count > 0 Then
                        row = ds.Tables("Dep1").NewRow
                        row("DepID") = mi(j).FP_InDep
                        row("DepName") = mi(j).FP_InName
                        row("FacName") = fi(0).FacName
                        ds.Tables("Dep1").Rows.Add(row)
                    End If
                Next
            Else
                Dim pi As List(Of PersonnelInfo)
                pi = pc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
                Dim j%
                For j = 0 To pi.Count - 1
                    row = ds.Tables("Dep1").NewRow
                    row("DepID") = pi(j).DepID
                    row("DepName") = pi(j).DepName
                    row("FacName") = pi(j).FacName
                    ds.Tables("Dep1").Rows.Add(row)
                Next
            End If
        End If

    End Sub

    Function LoadPM_TypeA(ByVal cbType As String, ByVal PM_M_Code As String) As String
        LoadPM_TypeA = Nothing

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()

        ppi = ppc.ProcessMain_GetList2(cbType, PM_M_Code)
        If ppi.Count = 0 Then
        Else
            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("PM_Type") = ppi(i).Type3ID
                ds.Tables("ProductType").Rows.Add(row)
            Next
            LoadPM_TypeA = ds.Tables("ProductType").Rows(0)("PM_Type").ToString
        End If

    End Function

    Function LoadPM_TypeA1(ByVal cbType As String, ByVal PM_M_Code As String) As String
        LoadPM_TypeA1 = Nothing

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)

        ds.Tables("ProductType1").Clear()

        ppi = ppc.ProcessMain_GetList2(cbType, PM_M_Code)
        If ppi.Count = 0 Then
        Else

            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row1 As DataRow
                row1 = ds.Tables("ProductType1").NewRow
                row1("PM_Type") = ppi(i).Type3ID          '---�q�{���p�U,�o�������P���������ۦP
                ds.Tables("ProductType1").Rows.Add(row1)
                '-------------------------------------------------
            Next
            LoadPM_TypeA1 = ds.Tables("ProductType1").Rows(0)("PM_Type").ToString
        End If

    End Function

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
        With ds.Tables.Add("Process1")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        GluEdit1.Properties.ValueMember = "PS_NO"
        GluEdit1.Properties.DisplayMember = "PS_Name"
        GluEdit1.Properties.DataSource = ds.Tables("Process1")


        '@ 2012/2/22 �K�[
        '�Ыئ��~�s����
        With ds.Tables.Add("PM_M_Code")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_JiYu", GetType(String))
        End With
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = ds.Tables("PM_M_Code")


        PM_M_Code1.Properties.ValueMember = "PM_M_Code"
        PM_M_Code1.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code1.Properties.DataSource = ds.Tables("PM_M_Code")

        '@ 2012/2/22 �K�[
        '�Ыئ��Ƴ�����
        With ds.Tables.Add("Dep1")
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("FacName", GetType(String))
            .Columns.Add("DepName", GetType(String))
        End With
        GluDep1.Properties.ValueMember = "DepID"
        GluDep1.Properties.DisplayMember = "DepName"
        GluDep1.Properties.DataSource = ds.Tables("Dep1")

        ''---------------------------------------------------------------------
        With ds.Tables.Add("Dep")
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("FacName", GetType(String))
            .Columns.Add("DepName", GetType(String))
        End With
        GluDep.Properties.ValueMember = "DepID"
        GluDep.Properties.DisplayMember = "DepName"
        GluDep.Properties.DataSource = ds.Tables("Dep")

    End Sub

    

    '@ 2012/2/22 �K�[
    '�[�����~�s���ƾ�
    '���L�{�Q�H�U�L�{�եΡG
    'frmProductionFieldCode_Load()
    'LoadData()

    Sub LoadPM_M_Code(ByVal GluDepA As String)
        Dim mi As List(Of ProductionDataSettingInfo)
        mi = mc.ProductionUser_GetList(GluDepA, Nothing)

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
#End Region

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        If PM_M_Code.EditValue = "" Or PM_M_Code.EditValue Is Nothing Or LoadBZ = "" Then Exit Sub
        gluType.EditValue = Nothing
        gluType.EditValue = LoadPM_TypeA(cbType1.EditValue, PM_M_Code.EditValue)

        If strFP_Detail = "PT18" Then
            PM_M_Code1.EditValue = Nothing
            GluEdit1.EditValue = Nothing
            gluType1.EditValue = Nothing
            PM_M_Code1.EditValue = PM_M_Code.EditValue
        End If

    End Sub

    Private Sub PM_M_Code1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code1.EditValueChanged
        If PM_M_Code1.EditValue = "" Or PM_M_Code1.EditValue Is Nothing Or LoadBZ = "" Then Exit Sub

        GluEdit1.EditValue = Nothing
        gluType1.EditValue = Nothing
        gluType1.EditValue = LoadPM_TypeA1(cbType1.EditValue, PM_M_Code1.EditValue)

        If strFP_Detail = "PT19" Or strFP_Detail = "PT20" Then
            gluType.EditValue = Nothing
            PM_M_Code.EditValue = Nothing
            PM_M_Code.EditValue = PM_M_Code1.EditValue
        End If
    End Sub


    Sub LoadInPs_Name()
        Dim mi As List(Of ProductionDataSettingInfo)
        Dim row As DataRow
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        mi = mc.ProductionIssue_GetList(GluDep1.EditValue, GluDep1.EditValue, cbType1.Text, PM_M_Code1.Text, gluType1.Text, Nothing)
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

        Else
            pci = pc.ProcessMain_GetList(Nothing, PM_M_Code1.EditValue, cbType1.EditValue, gluType1.EditValue, Nothing, True)
            If pci.Count = 0 Then Exit Sub
            Dim i As Integer
            For i = 0 To pci.Count - 1
                row = ds.Tables("Process1").NewRow
                row("PS_NO") = pci(i).PS_NO
                row("PS_Name") = pci(i).PS_Name

                ds.Tables("Process1").Rows.Add(row)
            Next

        End If


    End Sub


    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged
        If gluType.EditValue = "" Or gluType.EditValue Is Nothing Or GluDep.EditValue = "" Then Exit Sub

        ''�o��Ͳ��ܰt��s��
        Dim PL As New List(Of ProcessMainInfo)
        Dim PC As New ProcessMainControl
        PL = PC.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType1.EditValue, gluType.EditValue, Nothing, Nothing)

        If PL.Count > 0 Then
            LabM_Code.Text = PL(0).M_Code
            ''�d�ߥXProductInventory �����w�s
            Dim pcw As New ProductInventoryController
            Dim pcl As New List(Of ProductInventoryInfo)
            '  pcl = pcw.ProductInventory_GetList(PM_M_Code.EditValue, PL(0).M_Code, GluDep.EditValue, Nothing)
            pcl = pcw.ProductInventory_GetList(PM_M_Code.EditValue, Nothing, GluDep.EditValue, Nothing)

            If pcl.Count <= 0 Then
                LabWareInvent.Text = 0
            Else
                LabWareInvent.Text = pcl(0).PI_Qty
            End If
        End If
        '-----------------------------------------------------------
    End Sub

    Private Sub GluEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluEdit1.EditValueChanged
        If GluEdit1.EditValue = "" Or GluDep1.EditValue = "" Then Exit Sub

        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)

        fdi = fdc.ProductionDPTWareInventory_GetList(GluDep1.EditValue, GluEdit1.EditValue, Nothing)
        If fdi.Count = 0 Then
            LabelPSQty.Text = 0
        Else
            LabelPSQty.Text = fdi(0).WI_Qty
        End If
    End Sub

    Private Sub GluDep1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluDep1.EditValueChanged
        If (strFP_Detail = "PT18" And LoadBZ <> "") Then
            GluEdit1_EditValueChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub GluDep_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluDep.EditValueChanged
        If (strFP_Detail = "PT19" Or strFP_Detail = "PT20") And LoadBZ <> "" Then
            gluType_EditValueChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        If (e.KeyValue > 47 And e.KeyValue < 58) Or (e.KeyValue > 95 And e.KeyValue < 106) Or (e.KeyValue = 8) Or (e.KeyValue = 45) Or (e.KeyValue = 46) Then
            If GluEdit1.EditValue <> "" Then
                Dim pc As New ProcessMainControl
                Dim pci As List(Of ProcessMainInfo)

                pci = pc.ProcessSub_GetList(Nothing, GluEdit1.EditValue, Nothing, Nothing, Nothing, Nothing)
                If pci.Count = 0 Then Exit Sub

                Dim AllWeight, strWeight, strG As Single

                strWeight = pci(0).PS_Weight  '�J/��  �歫
                strG = strWeight * Val(txtQty.Text)
                AllWeight = strG / 1000  '��e�ƶq�����q(KG)
                txtWeight.Text = Format(AllWeight, "0.00") '(��Ƭ����p��)
            End If
        Else
            'MsgBox("�u���J��ƼƦr�I")
            txtQty.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub gluType1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType1.EditValueChanged
        If gluType1.EditValue = "" Or gluType1.EditValue Is Nothing Then Exit Sub
        LoadInPs_Name()
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

#Region "�O�s�ƾ�-PT18"
    Function CheckDate(ByVal PtType As String) As Boolean
        CheckDate = True

        If Val(txtQty.Text) <= 0 Then
            MsgBox("�ƶq�������j��0�����")
            CheckDate = False
            txtQty.Select()
            Exit Function
        End If

        If Val(txtWeight.Text) <= 0 Then
            MsgBox("���q�������j��0�����")
            CheckDate = False
            txtQty.Select()
            Exit Function
        End If

        If TextEdit1.Text = "" And TextEdit1.Visible = True And chkFP_OutOK.Checked = True Then     '@2012/7/12 �ק�
            MsgBox("��d�H�H�����ର�šI")
            CheckDate = False
            SimpleButton1.Focus()
            Exit Function
        End If

        If GluDep.EditValue = "" Or GluDep.Text = "" Then
            MsgBox("�������ର�šI")
            CheckDate = False
            GluDep.Select()
            Exit Function
        End If

        If GluDep1.EditValue = "" Or GluDep1.Text = "" Then
            MsgBox("�������ର�šI")
            CheckDate = False
            GluDep1.Select()
            Exit Function
        End If

        If GluEdit1.EditValue = "" Then
            MsgBox("�o�X�u�Ǥ��ର�šI")
            CheckDate = False
            Exit Function
        End If

        ''2013-5-20 ������u�ǽs��,�L�u�ǦW��
        If GluEdit1.Text = "" Then
            MsgBox("�o�X�u�Ǥ��ର��.")
            CheckDate = False
            Exit Function
        End If
        If GluEdit1.Text = "" Then
            MsgBox("���Ƥu�Ǥ��ର��.")
            CheckDate = False
            Exit Function
        End If

        If TextEdit1.Text <> "" And TextEdit1.Visible = True And chkFP_OutOK.Checked = False Then   '@2012/7/12 �K�[
            MsgBox("�п�ܽT�{�o�X�I")
            CheckDate = False
            Exit Function
        End If

        If GluDep.EditValue = GluDep1.EditValue Then
            MsgBox("�����ܬۦP�����H���I")
            CheckDate = False
            Exit Function
        End If

        ''---------------------------------------------------------------------------------------------------------------
        ''1.���ޤ����������n���ܪ����~�s��,����,�P�_�u�ǬO�_�s�b
        ''2.���~�s��-���� �������t��s��(���ƽs�X�O�_�s�b)  
        ''����bTEXTChange�ܤƮɸ��J���H�����~ 
        Dim pcc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pcc.ProcessMain_GetList(Nothing, PM_M_Code1.EditValue, cbType1.EditValue, gluType1.EditValue, Nothing, True)
        Dim i As Integer
        Dim BZ As String = ""
        For i = 0 To pci.Count - 1
            If GluEdit1.EditValue = pci(i).PS_NO Then
                BZ = "Y"
                GoTo AA
            End If
        Next
AA:
        If BZ = "" Then
            MsgBox("[" & PM_M_Code1.EditValue & "  " & gluType1.EditValue & "]" & "���s�b:" & GluEdit1.Text & "�u��!")
            CheckDate = False
            Exit Function
        End If

        ''---------------------------------------------------------------------------------------------------------------

        Dim PL As New List(Of ProcessMainInfo)
        Dim PCA As New ProcessMainControl
        PL = PCA.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType1.EditValue, gluType.EditValue, Nothing, Nothing)

        If PL.Count > 0 Then
            If LabM_Code.Text = PL(0).M_Code Then
            Else
                MsgBox("[" & PM_M_Code.EditValue & "  " & gluType.EditValue & "]" & "�s�����s�b:" & LabM_Code.Text & "�s�X!")
                CheckDate = False
                Exit Function
            End If
        Else
            MsgBox("[" & PM_M_Code.EditValue & "  " & gluType.EditValue & "]" & ",���إߥͲ��u���y�{!")
            CheckDate = False
            Exit Function
        End If
        ''---------------------------------------------------------------------------------------------------------------

        If PtType = "PT18" Or PtType = "PT20" Then
            ''------------------------------------------------------------------
            ''[�Ͳ���]����o��[�˰t�u��]
            ''[�˰t��]����o��[�Ͳ��u��]
            If InStr(GluDep.Text, "�Ͳ�", CompareMethod.Text) > 0 Then
                If InStr(cbType1.Text, "�Ͳ�", CompareMethod.Text) > 0 Then
                Else
                    MsgBox("[" & GluDep.Text & "]�P[" & cbType1.Text & "���ǰt!")
                    CheckDate = False
                    Exit Function
                End If
            ElseIf InStr(GluDep.Text, "�˰t", CompareMethod.Text) > 0 Then
                If InStr(cbType1.Text, "�˰t", CompareMethod.Text) > 0 Then
                Else
                    MsgBox("[" & GluDep.Text & "]�P[" & cbType1.Text & "]���ǰt!")
                    CheckDate = False
                    Exit Function
                End If
            End If

        End If

        If PtType = "PT19" Then
            If InStr(GluDep.Text, "�˰t", CompareMethod.Text) > 0 Then
            Else
                MsgBox("[" & GluDetail.Text & "],�п�ܸ˰t�����ܮw!")
                GluDep.Focus()
                CheckDate = False
                Exit Function
            End If
        End If



        If PtType = "PT18" Then  '�Ͳ���---->�u�Ǽƶq-
            '�n�P�_ProductInventory�w�s�O�_����
            Dim pcw As New ProductInventoryController
            Dim pcl As New List(Of ProductInventoryInfo)
            pcl = pcw.ProductInventory_GetList(PM_M_Code.EditValue, LabM_Code.Text, GluDep.EditValue, Nothing)

            If pcl.Count <= 0 Then
                LabWareInvent.Text = 0
            Else
                LabWareInvent.Text = pcl(0).PI_Qty
            End If

            If Val(txtQty.Text) <= Val(LabWareInvent.Text) Then
            Else
                MsgBox("��e[" & GluDep.Text & "],�w�s�ƶq�p��o�X�ƶq!")
                CheckDate = False
                Exit Function
            End If

        End If

        If PtType = "PT19" Or PtType = "PT20" Then '�~�o�u�Ǽƶq----->�˰t��(�n�l�[�Ͳ��p�E)--------------------------------------------------------

            If PtType = "PT19" Then
                Dim psi As List(Of LFERP.Library.ProductionSchedule.ProductionScheduleInfo)
                Dim psc As New LFERP.Library.ProductionSchedule.ProductionScheduleControl

                psi = psc.ProductionSchedule_GetList(Nothing, cbType1.EditValue, Nothing, PM_M_Code1.EditValue, gluType1.EditValue, DateEdit1.Text, DateEdit1.Text, Nothing)
                If psi.Count = 0 Then
                    MsgBox("��e�Ͳ������s�b��w������Ͳ��p��,�Х��K�[�Ͳ��p���I")
                    CheckDate = False
                    Exit Function
                Else
                    CheckDate = True
                End If
            End If

            '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Dim fdc As New ProductionDPTWareInventoryControl
            Dim fdi As List(Of ProductionDPTWareInventoryInfo)

            fdi = fdc.ProductionDPTWareInventory_GetList(GluDep1.EditValue, GluEdit1.EditValue, Nothing)
            If fdi.Count = 0 Then
                LabelPSQty.Text = 0
            Else
                LabelPSQty.Text = fdi(0).WI_Qty
            End If

            If Val(txtQty.Text) <= Val(LabelPSQty.Text) Then
            Else
                MsgBox("��e[" & GluDep1.Text & "],�u�Ǯw�s�ƶq�p��o�X�ƶq!")
                CheckDate = False
                Exit Function
            End If
        End If

        Dim pi As List(Of ProductionFieldInfo)
        Dim pc As New ProductionFieldControl
        pi = pc.ProductionField_GetList(txtNo.Text, Nothing, "����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)
        If pi.Count > 0 Then
            If pi(0).FP_InCheck = True Then
                MsgBox("����w�f��,����O�s!")
                CheckDate = False
                Exit Function
            End If
        End If

    End Function

    Function DataNew() As Boolean   '�Ͳ���----------�u�Ǽƶq

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        pi.FP_NO = txtNo.Text
        pi.FP_Num = GetNum()

        If GluDetail.EditValue = "PT18" Then

            pi.Pro_Type = cbType1.EditValue
            pi.PM_M_Code = PM_M_Code.EditValue
            pi.PM_Type = gluType.EditValue
            pi.Pro_NO = LabM_Code.Text
            pi.FP_OutDep = GluDep.EditValue

            pi.Pro_Type1 = cbType1.EditValue
            pi.PM_M_Code1 = PM_M_Code1.EditValue
            pi.PM_Type1 = gluType1.EditValue
            pi.Pro_NO1 = GluEdit1.EditValue
            pi.FP_InDep = GluDep1.EditValue
        ElseIf GluDetail.EditValue = "PT19" Or GluDetail.EditValue = "PT20" Then
            pi.Pro_Type1 = cbType1.EditValue
            pi.PM_M_Code1 = PM_M_Code.EditValue
            pi.PM_Type1 = gluType.EditValue
            pi.Pro_NO1 = LabM_Code.Text
            pi.FP_InDep = GluDep.EditValue

            pi.Pro_Type = cbType1.EditValue
            pi.PM_M_Code = PM_M_Code1.EditValue
            pi.PM_Type = gluType1.EditValue
            pi.Pro_NO = GluEdit1.EditValue
            pi.FP_OutDep = GluDep1.EditValue

        End If

        pi.FP_Qty = txtQty.Text
        pi.FP_Weight = txtWeight.Text

        Labeltime.Text = Format(Now, "HH:mm:ss")
        pi.FP_Date = CDate(DateEdit1.EditValue & " " & Labeltime.Text)

        pi.FP_Detail = GluDetail.EditValue
        pi.FP_Remark = txtRemark.Text
        pi.FP_OutAction = InUserID
        pi.CardID = TextEdit1.Text   '��d�H

        pi.FP_OutType = Nothing
        pi.FP_SubtractReQty = Nothing
        pi.FP_OutOK = chkFP_OutOK.Checked       '@ 2012/7/12 �K�[

        pi.FP_Weight = Val(txtWeight.Text)


        If pc.ProductionField_Add(pi) = True Then
            MsgBox("�O�s���\")
            DataNew = True
            Me.Close()
        Else
            MsgBox("�O�s����,���ˬd��]!")
            DataNew = False
        End If

    End Function


    Sub DataEdite()    '�Ͳ���----------�u�Ǽƶq �ק�

        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        pi.FP_NO = txtNo.Text
        pi.FP_Num = labNum.Text

        If GluDetail.EditValue = "PT18" Then
            pi.Pro_Type = cbType1.EditValue
            pi.PM_M_Code = PM_M_Code.EditValue
            pi.PM_Type = gluType.EditValue
            pi.Pro_NO = LabM_Code.Text
            pi.FP_OutDep = GluDep.EditValue

            pi.Pro_Type1 = cbType1.EditValue
            pi.PM_M_Code1 = PM_M_Code1.EditValue
            pi.PM_Type1 = gluType1.EditValue
            pi.Pro_NO1 = GluEdit1.EditValue
            pi.FP_InDep = GluDep1.EditValue
        ElseIf GluDetail.EditValue = "PT19" Or GluDetail.EditValue = "PT20" Then
            pi.Pro_Type1 = cbType1.EditValue
            pi.PM_M_Code1 = PM_M_Code.EditValue
            pi.PM_Type1 = gluType.EditValue
            pi.Pro_NO1 = LabM_Code.Text
            pi.FP_InDep = GluDep.EditValue

            pi.Pro_Type = cbType1.EditValue
            pi.PM_M_Code = PM_M_Code1.EditValue
            pi.PM_Type = gluType1.EditValue
            pi.Pro_NO = GluEdit1.EditValue
            pi.FP_OutDep = GluDep1.EditValue

        End If

        pi.FP_Qty = txtQty.Text
        pi.FP_Weight = txtWeight.Text

        Labeltime.Text = Format(Now, "HH:mm:ss")
        pi.FP_Date = CDate(DateEdit1.EditValue & " " & Labeltime.Text)

        pi.FP_Detail = GluDetail.EditValue
        pi.FP_Remark = txtRemark.Text
        pi.FP_OutAction = InUserID
        pi.CardID = TextEdit1.Text   '��d�H

        pi.FP_OutType = Nothing
        pi.FP_SubtractReQty = Nothing
        pi.FP_OutOK = chkFP_OutOK.Checked       '@ 2012/7/12 �K�[

        pi.FP_Weight = Val(txtWeight.Text)

        If pc.ProductionField_Update(pi) = True Then
            MsgBox("�ק令�\")
            Me.Close()
        Else
            MsgBox("�ק令�\,���ˬd��]!")
        End If

    End Sub

#End Region



    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case strCaoType
            Case "CodeOut" '�o��

                If Edit = False Then

                    Dim fpi As List(Of ProductionFieldInfo)
                    Dim fpc As New ProductionFieldControl

                    fpi = fpc.ProductionField_GetList(txtNo.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                    If fpi.Count = 0 Then
                        If CheckDate(strFP_Detail) = False Then
                            Exit Sub
                        End If

                        DataNew()

                    Else
                        MsgBox("�渹�w�s�b�A" & vbCr & "�нT�w���s�ͦ��渹!", 64, "����")
                        txtNo.Text = GetNO()
                    End If
                Else
                    '�ק�
                    If CheckDate(GluDetail.EditValue) = False Then
                        Exit Sub
                    End If
                    DataEdite()
                End If

            Case "InCheck"
                If CheckEdit1.Checked = OldCheck Then
                    MsgBox("�����ܽT�{���A,�����\�O�s!")
                    Exit Sub
                End If

                If CheckDate(GluDetail.EditValue) = True Then
                    UpdateIncheck()
                    If GluDetail.EditValue = "PT19" Then
                        UpdateDaySchedule() '�Ͳ��p�E
                    End If
                End If
        End Select


    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        TextEdit1.Text = ReadCard() 'Ū���d��
        If TextEdit1.Text <> "" Then
            chkFP_OutOK.Checked = True
        End If

        '  TextEdit1.Text = "12022419-SASA"

    End Sub


    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Dim frm As New frmException
        frm.ShowDialog()

        TextEdit1.Text = tempValue
        tempValue = ""
    End Sub


    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Function LoadData(ByVal FP_NO As String) As Boolean
        Dim pi As List(Of ProductionFieldInfo)
        Dim pc As New ProductionFieldControl

        pi = pc.ProductionField_GetList(FP_NO, Nothing, "����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)

        If pi.Count = 0 Then
            MsgBox("�S���ƾ�")
            LoadData = False
            Exit Function
        Else
            txtNo.Text = pi(0).FP_NO
            labNum.Text = pi(0).FP_Num

            strFP_Detail = pi(0).FP_Detail
            strFP_Type = pi(0).FP_Type

            If pi(0).FP_Detail = "PT19" Or pi(0).FP_Detail = "PT20" Then
                '------------------------------------------------------------
                LoadDep(pi(0).FP_InDep, pi(0).FP_Detail)
                LoadDep1(pi(0).FP_InDep, pi(0).FP_Detail)
                LoadPM_M_Code(pi(0).FP_InDep)
                LoadPM_TypeA(pi(0).Pro_Type, pi(0).PM_M_Code1)
                LoadPM_TypeA1(pi(0).Pro_Type, pi(0).PM_M_Code)
                '------------------------------------------------------------
                PM_M_Code.EditValue = pi(0).PM_M_Code1
                gluType.EditValue = pi(0).PM_Type1
                LabM_Code.Text = pi(0).Pro_NO
                GluDep.EditValue = pi(0).FP_OutDep

                cbType1.EditValue = pi(0).Pro_Type
                PM_M_Code1.EditValue = pi(0).PM_M_Code
                gluType1.EditValue = pi(0).PM_Type
                GluEdit1.EditValue = pi(0).Pro_NO1
                GluDep1.EditValue = pi(0).FP_InDep
            End If

            If pi(0).FP_Detail = "PT18" Then
                '------------------------------------------------------------
                LoadDep(pi(0).FP_InDep, pi(0).FP_Detail)
                LoadDep1(pi(0).FP_InDep, pi(0).FP_Detail)
                LoadPM_M_Code(pi(0).FP_InDep)
                LoadPM_TypeA(pi(0).Pro_Type, pi(0).PM_M_Code)
                LoadPM_TypeA1(pi(0).Pro_Type, pi(0).PM_M_Code1)
                '------------------------------------------------------------

                PM_M_Code.EditValue = pi(0).PM_M_Code
                gluType.EditValue = pi(0).PM_Type
                LabM_Code.Text = pi(0).Pro_NO1
                GluDep.EditValue = pi(0).FP_InDep

                cbType1.EditValue = pi(0).Pro_Type1
                PM_M_Code1.EditValue = pi(0).PM_M_Code1
                gluType1.EditValue = pi(0).PM_Type1
                GluEdit1.EditValue = pi(0).Pro_NO
                GluDep1.EditValue = pi(0).FP_OutDep
            End If

            DateEdit1.EditValue = Format(pi(0).FP_Date, "yyyy/MM/dd")
            Labeltime.Text = Format(pi(0).FP_Date, "HH:mm:ss")
            LabFP_Type.Text = pi(0).FP_Type
            chkFP_OutOK.Checked = pi(0).FP_OutOK       '@ 2012/7/12 �K�[

            txtQty.Text = pi(0).FP_Qty
            txtWeight.Text = pi(0).FP_Weight
            '------------------------------------------------------------------
            GluDetail.EditValue = pi(0).FP_Detail

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

            If ViewType = "���J����" Then
                TextEdit1.Text = pi(0).CardID
            ElseIf ViewType = "�o�X����" Then
                Dim pi1 As List(Of ProductionFieldInfo)
                Dim pc1 As New ProductionFieldControl
                pi1 = pc1.ProductionField_GetList(FP_NO, Nothing, "�o��", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "2", Nothing, Nothing)
                If pi1.Count > 0 Then
                    TextEdit1.Text = pi(0).CardID
                End If
            End If

            Label14.Text = pi(0).FP_CheckActionName
            txtCheckremark.Text = pi(0).FP_CheckRemark

            ''���J�ɹ�w�s
            gluType_EditValueChanged(Nothing, Nothing)
            GluEdit1_EditValueChanged(Nothing, Nothing)

        End If
    End Function
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
#Region " ���ƽT�{"
    ''' <summary>
    ''' ��s�f�֪��A
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateIncheck()
        '�n�P�_�@�U,�y�����O�_�w�s�b,�s�b�N���f��
        If GetTypeData() = False Then
            MsgBox("����w�s�b����,�ФήɻP�q�����p�t!")
            Exit Sub
        End If
        '----------------------------------------------------------
        Dim pfc As New ProductionFieldControl
        Dim pfi As New ProductionFieldInfo
        pfi.FP_InCheck = CheckEdit1.Checked
        ' pfi.FP_InCheckDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
        pfi.CardID = TextEdit1.Text
        pfi.FP_Type = strFP_Type
        pfi.FP_NO = txtNo.Text

        pfi.FP_Check = True
        pfi.FP_CheckAction = InUserID

        If pfc.ProductionField_UpdateInCheck1(pfi) = True Then
            '����-------------------------------------------
            'UpdateCheck()             �{��Ĳ�o������
            'UpdateWarePSNOInvent()
            UpdateDaySummary()

            MsgBox("���ƽT�{���\�I")
            Me.Close()
        Else
            MsgBox("�ƾګO�s����,���ˬd!")
            Exit Sub
        End If

    End Sub

    Sub UpdateCheck()
        Dim pi As New ProductionFieldInfo
        Dim pc As New ProductionFieldControl

        pi.FP_NO = txtNo.Text
        pi.FP_Check = True
        pi.FP_CheckAction = InUserID
        pi.FP_CheckRemark = txtCheckremark.Text

        pc.ProductionField_UpdateCheck(pi)
    End Sub

    Sub UpdateWarePSNOInvent()

        Dim strGluDetail As String
        strGluDetail = GluDetail.EditValue

        Dim pcw As New ProductInventoryController
        Dim pcl As New List(Of ProductInventoryInfo)
        pcl = pcw.ProductInventory_GetList(PM_M_Code.EditValue, LabM_Code.Text, GluDep.EditValue, Nothing)

        Dim IntWareInvent, IntPI_Qty As Int32
        If pcl.Count <= 0 Then
            IntPI_Qty = 0
        Else
            IntPI_Qty = pcl(0).PI_Qty
        End If

        If strGluDetail = "PT18" Then  '�bcheckData�����P�_
            IntWareInvent = IntPI_Qty - Val(txtQty.Text) '��
        ElseIf strGluDetail = "PT19" Or strGluDetail = "PT20" Then
            IntWareInvent = IntPI_Qty + Val(txtQty.Text) '+
        End If

        Dim pii As New ProductInventoryInfo
        pii.PM_M_Code = PM_M_Code.EditValue
        pii.M_Code = LabM_Code.Text
        pii.PI_Qty = IntWareInvent
        pii.WH_ID = GluDep.EditValue
        If pcw.ProductInventory_Update(pii) = True Then
        End If
        ''--------------------------------------------------------------------
        Dim fdc As New ProductionDPTWareInventoryControl
        Dim fdi As List(Of ProductionDPTWareInventoryInfo)
        fdi = fdc.ProductionDPTWareInventory_GetList(GluDep1.EditValue, GluEdit1.EditValue, Nothing)
        Dim PS_Qty As Int32
        Dim PS_RQty As Int32
        Dim IntWI_Qty As Int32
        If fdi.Count <= 0 Then
            IntWI_Qty = 0
            PS_RQty = 0
        Else
            IntWI_Qty = fdi(0).WI_Qty
            PS_RQty = fdi(0).WI_ReQty
        End If

        If strGluDetail = "PT18" Then
            PS_Qty = IntWI_Qty + Val(txtQty.Text) '�[
        ElseIf strGluDetail = "PT19" Or strGluDetail = "PT20" Then
            PS_Qty = IntWI_Qty - Val(txtQty.Text) '-'�bcheckData�����P�_
        End If

        Dim pfcA As New ProductionDPTWareInventoryControl
        Dim pfiA As New ProductionDPTWareInventoryInfo
        pfiA.M_Code = GluEdit1.EditValue
        pfiA.DPT_ID = GluDep1.EditValue
        pfiA.WI_Qty = PS_Qty
        pfiA.WI_ReQty = PS_RQty
        If pfcA.UpdateProductionField_Qty(pfiA) = True Then
        End If
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    End Sub

    ''' <summary>
    ''' '��s�C���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateDaySummary() As Boolean
        Dim pc As New ProductionFieldDaySummaryControl
        Dim pl As New List(Of ProductionFieldDaySummaryInfo)
        pl = pc.ProductionFieldDaySummary_GetList(Nothing, Nothing, Nothing, GluEdit1.EditValue, GluDep1.EditValue, Nothing, Format(CDate(DateEdit1.EditValue), "yyyy-MM-dd"), Format(CDate(DateEdit1.EditValue), "yyyy-MM-dd"))

        Dim IntWaiFaASS As Int32
        Dim IntWareFaLiao As Int32
        Dim IntASSBuNiang As Int32

        If pl.Count <= 0 Then
            IntWaiFaASS = 0
            IntWareFaLiao = 0
            IntASSBuNiang = 0
        Else
            IntWaiFaASS = pl(0).WaiFaASS
            IntWareFaLiao = pl(0).WareFaLiao
            IntASSBuNiang = pl(0).ASSBuNiang
        End If
        ''--------------------------------------------------------------
        Dim pi As New ProductionFieldDaySummaryInfo
        pi.Pro_Type = cbType1.EditValue
        pi.PM_M_Code = PM_M_Code1.EditValue
        pi.PM_Type = gluType1.EditValue
        pi.Pro_NO = GluEdit1.EditValue
        pi.FP_OutDep = GluDep1.EditValue
        pi.PM_Date = Format(CDate(DateEdit1.EditValue), "yyyy-MM-dd")

        'Pro_NO=@Pro_NO and FP_OutDep=@FP_OutDep and CONVERT(varchar(100), PM_Date, 23)=@PM_Date
        If GluDetail.EditValue = "PT18" Then
            pi.WaiFaASS = IntWaiFaASS
            pi.WareFaLiao = IntWareFaLiao + Val(txtQty.Text)
            pi.ASSBuNiang = IntASSBuNiang
        ElseIf GluDetail.EditValue = "PT19" Then
            pi.WaiFaASS = IntWaiFaASS + Val(txtQty.Text)
            pi.WareFaLiao = IntWareFaLiao
            pi.ASSBuNiang = IntASSBuNiang
        ElseIf GluDetail.EditValue = "PT20" Then
            pi.WaiFaASS = IntWaiFaASS
            pi.WareFaLiao = IntWareFaLiao
            pi.ASSBuNiang = IntASSBuNiang + Val(txtQty.Text)
        End If

        If pc.UpdateProductionDaySummary_QtyASS(pi) = True Then

        End If

    End Function

    '�Ͳ��p��
    Function UpdateDaySchedule() As String
        UpdateDaySchedule = Nothing
        '--------------------
        Dim psi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl
        Dim psi1 As List(Of ProductionScheduleInfo)

        psi1 = psc.ProductionSchedule_GetList1(Nothing, cbType1.EditValue, PM_M_Code1.EditValue, Transfer(GluEdit1.EditValue), Nothing, Nothing, DateEdit1.Text, DateEdit1.Text, gluType1.EditValue)

        If psi1.Count = 0 Then
        Else
            If psi1(0).PS_DayNumber > 0 Then

                Dim strNumber As Integer

                strNumber = psi1(0).PS_ActualNumber

                psi.PS_NO = psi1(0).PS_NO '�o���e�渹
                psi.Pro_Type = cbType1.EditValue
                psi.PM_M_Code = PM_M_Code1.EditValue

                psi.PM_Type = gluType1.EditValue
                psi.PS_Dep = psi1(0).PS_Dep
                psi.PS_Date = CDate(DateEdit1.Text)

                psi.PS_ActualNumber = strNumber + Val(txtQty.Text)

                psc.ProductionSchedule_UpdateActualNumber(psi)
            End If
        End If
        '-------------------'�ܧ��Ѧ����~���Ͳ��p����ڧ����q 
    End Function

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

#End Region


  

End Class
