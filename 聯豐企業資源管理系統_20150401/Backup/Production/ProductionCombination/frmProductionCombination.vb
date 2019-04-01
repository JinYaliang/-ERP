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
Imports LFERP.Library.ProductionCombination
Imports LFERP.Library.Production.ProductionRatio




Public Class frmProductionCombination

    Dim Load_OK As String

    Dim uc As New SystemUser.SystemUserController
    Dim dc As New DepartmentControler
    Dim fc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim pc As New ProductionCombinationControl


    Dim ds As New DataSet
    Dim upi As List(Of UserPowerInfo)
    Dim upc As New UserPowerControl

    Dim OldCheck As Boolean
    Dim DeclareQty As Single

    Sub LoadProductNo()  '���~�s��
        Dim mc As New ProcessMainControl
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)

    End Sub
    Sub LoadDepartment()  '�����H���@�@--�q�{���J���N�k��

        GluDep.Properties.DataSource = fc.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)  '�ܧ󳡪�
        GluDep.Properties.DisplayMember = "DepName"
        GluDep.Properties.ValueMember = "DepID"

    End Sub

    Private Sub frmProductionCombination_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '@ 2012/5/14 �K�[ �P�_�O�_�ݭn��d
        If strRefCard = "�O" Then
        Else
            btnReadCard.Visible = False
            txtCardID.Visible = False
            SimpleButton2.Visible = False
        End If
        LoadProductNo()
        LoadDepartment()
        CreateTable()
        Label10.Text = tempValue2
        txtID.Text = tempValue3
        tempValue2 = ""
        tempValue3 = ""

        '���m�s��d��
        Dim reset As New ResetPassWords.SetPassWords
        reset.SetPassWords()

        Select Case Label10.Text
            Case "�զX"
                cbType.EditValue = "�Ͳ��[�u"
                GluDep.EditValue = tempValue5
                tempValue5 = ""
                cmsAdd.Visible = False
                cmsDel.Visible = False
            Case "���"
                cbType.EditValue = "�Ͳ��[�u"
                GluDep.EditValue = tempValue5
                tempValue5 = ""
                lblTittle.Text = "����u��"
                Me.Text = "����u��"
                lblQty.Text = "�o�X�ƶq(&S)�G"
                M_OutQty.Caption = "���J�ƶq"

                cmsAdd.Visible = True
                cmsDel.Visible = True

            Case "PreView"
                LoadData(txtID.Text)
                cmdSave.Visible = False
        End Select

        Load_OK = "OK"

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")

        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With

        GluEdit1.Properties.ValueMember = "PS_NO"
        GluEdit1.Properties.DisplayMember = "PS_Name"
        GluEdit1.Properties.DataSource = ds.Tables("Process")

        With ds.Tables.Add("Combination")

            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("Pro_Type1", GetType(String))
            .Columns.Add("PM_M_Code1", GetType(String))
            .Columns.Add("PM_Type1", GetType(String))
            .Columns.Add("PS_Name1", GetType(String))
            .Columns.Add("Pro_NO1", GetType(String))
            .Columns.Add("M_OutQty", GetType(Integer))

            .Columns.Add("PR_Ratio", GetType(Integer)) '

        End With
        Grid.DataSource = ds.Tables("Combination")
        With ds.Tables.Add("DelCombination")
            .Columns.Add("AutoID", GetType(String))
        End With
    End Sub

    Function LoadData(ByVal M_ID As String) As Boolean
        LoadData = True

        ds.Tables("Combination").Clear()

        Dim pci As List(Of ProductionCombinationInfo)

        pci = pc.ProductionCombination_GetList(M_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pci.Count = 0 Then
            LoadData = False
            Exit Function
        Else
            txtID.Text = pci(0).M_ID
            cbType.EditValue = pci(0).Pro_Type
            PM_M_Code.EditValue = pci(0).PM_M_Code
            gluType.EditValue = pci(0).PM_Type

            GluDep.EditValue = pci(0).M_Dep
            GluEdit1.EditValue = pci(0).Pro_NO
            txtQty.Text = pci(0).M_InQty
            MemoEdit1.EditValue = pci(0).M_Remark
            txtCardID.Text = pci(0).CardID

            Dim row As DataRow
            Dim i As Integer
            For i = 0 To pci.Count - 1

                row = ds.Tables("Combination").NewRow

                row("AutoID") = pci(i).AutoID
                row("Pro_Type1") = pci(i).Pro_Type1
                row("PM_M_Code1") = pci(i).PM_M_Code1
                row("PM_Type1") = pci(i).PM_Type1
                row("Pro_NO1") = pci(i).Pro_NO1 '���ä����
                row("PS_Name1") = pci(i).PS_Name1
                row("M_OutQty") = pci(i).M_OutQty

                ds.Tables("Combination").Rows.Add(row)
            Next

        End If

    End Function

    Function GetNO() As String

        Dim pi As New ProductionCombinationInfo

        Dim strA As String
        If Label10.Text = "�զX" Then
            strA = "M" & Format(Now, "yyMM")
        ElseIf Label10.Text = "���" Then
            strA = "D" & Format(Now, "yyMM")
        End If

        pi = pc.ProductionCombination_GetNO(strA)

        If pi Is Nothing Then
            GetNO = strA & "0001"
        Else
            GetNO = strA + Mid((CInt(Mid(pi.M_ID, 6)) + 10001), 2)
        End If
    End Function

    Sub DataNew()

        Dim pi As New ProductionCombinationInfo

        txtID.Text = GetNO()

        pi.M_ID = txtID.Text
        pi.M_Type = Label10.Text  '--������PT17
        pi.Pro_Type = cbType.EditValue
        pi.PM_M_Code = PM_M_Code.EditValue
        pi.PM_Type = gluType.EditValue

        pi.M_Dep = GluDep.EditValue
        pi.M_Date = Format(Now, "yyyy/MM/dd HH:mm:ss")
        pi.M_InQty = txtQty.Text
        pi.M_Remark = MemoEdit1.EditValue
        pi.M_Action = InUserID
        pi.CardID = txtCardID.Text

        Dim i As Integer

        Dim strFac As String
        strFac = Mid(GluDep.EditValue, 1, 1)
        Dim strDate As String
        strDate = CStr(Format(Now, "yyyy/MM/dd"))

        For i = 0 To ds.Tables("Combination").Rows.Count - 1

            pi.Pro_Type1 = ds.Tables("Combination").Rows(i)("Pro_Type1")
            pi.PM_M_Code1 = ds.Tables("Combination").Rows(i)("PM_M_Code1")
            pi.PM_Type1 = ds.Tables("Combination").Rows(i)("PM_Type1")

            If Label10.Text = "�զX" Then
                pi.Pro_NO = GluEdit1.EditValue
                pi.Pro_NO1 = ds.Tables("Combination").Rows(i)("Pro_NO1")
            ElseIf Label10.Text = "���" Then
                pi.Pro_NO = ds.Tables("Combination").Rows(i)("Pro_NO1")
                pi.Pro_NO1 = GluEdit1.EditValue
            End If

            pi.PS_Name1 = ds.Tables("Combination").Rows(i)("PS_Name1")
            pi.M_OutQty = ds.Tables("Combination").Rows(i)("M_OutQty")

            pc.ProductionCombination_Add(pi) '�O�s�O���ܲզX�޲z--�ۦP��ڬd��


            '-----------�q�LĲ�o���ӳB�z----------------------�A�Ψ쪫�Ʀ��o���O��
            '-----------�q�LĲ�o���ӳB�z----------------------�ܧ󳡪��w�s
            '-----------�q�LĲ�o���ӳB�z----------------------�O���C�鵲�l�ƾ�


            '-------------------------------------------------���`��e�o�X�u�Ǫ���ڧ����q�]�έp�ܥͲ��p�����^

            If Label10.Text = "�զX" Then
                Dim psi As New ProductionScheduleInfo
                Dim psc As New ProductionScheduleControl
                Dim psi1 As List(Of ProductionScheduleInfo)

                psi1 = psc.ProductionSchedule_GetList1(Nothing, ds.Tables("Combination").Rows(i)("Pro_Type1"), ds.Tables("Combination").Rows(i)("PM_M_Code1"), Transfer(ds.Tables("Combination").Rows(i)("Pro_NO1")), strFac, Nothing, strDate, strDate, ds.Tables("Combination").Rows(i)("PM_Type1"))

                If psi1.Count = 0 Then
                Else

                    Dim m As Single

                    m = psi1(0).PS_ActualNumber


                    psi.PS_NO = psi1(0).PS_NO '�o���e�渹
                    psi.Pro_Type = cbType.EditValue
                    psi.PM_M_Code = psi1(0).PM_M_Code

                    psi.PM_Type = psi1(0).PM_Type
                    psi.PS_Dep = strFac
                    psi.PS_Date = Format(Now, "yyyy/MM/dd")

                    psi.PS_ActualNumber = m + CSng(ds.Tables("Combination").Rows(i)("M_OutQty"))

                    psc.ProductionSchedule_UpdateActualNumber(psi)

                End If

            End If

        Next

        MsgBox("�ާ@����!", 64, "����")
        Me.Close()

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim i As Integer

        If txtQty.Text.Trim = "" Then
            MsgBox(Label10.Text & M_OutQty.Caption & "���ର�šI")
            Exit Sub
        ElseIf CInt(txtQty.Text.Trim) = 0 Then
            MsgBox(Label10.Text & M_OutQty.Caption & "���ର0�I")
            Exit Sub
        End If
        If txtCardID.Text.Trim = "" And txtCardID.Visible = True Then
            MsgBox("��d�H�H�����ର��,�Ш�d!", 64, "����")
            btnReadCard.Focus()
            Exit Sub
        End If
        For i = 0 To ds.Tables("Combination").Rows.Count - 1
            If ds.Tables("Combination").Rows(i)("M_OutQty") <= 0 Then
                GridView1.FocusedRowHandle = i
                MsgBox(Label10.Text & M_OutQty.Caption & "���ର0�I")
                Exit Sub
            End If
        Next
        If Label10.Text = "�զX" Then
            If CheckData() = True Then
                DataNew()
            End If
        ElseIf Label10.Text = "���" Then

            Dim pdi As List(Of ProductionDPTWareInventoryInfo)
            Dim pdc As New ProductionDPTWareInventoryControl

            pdi = pdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, GluEdit1.EditValue, Nothing)
            If pdi(0).WI_Qty < CInt(txtQty.Text.Trim) Then
                MsgBox("��e�����u�Ǽƶq�p��o�X�ƶq!", 64, "����")
                Exit Sub
            End If
            DataNew()
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        tempCode = ""
        If Label10.Text = "�զX" Then
            tempValue2 = "�u�ǲզX�޲z"
        Else
            tempValue2 = "�u�ǩ���޲z"
        End If

        tempValue4 = GluDep.EditValue '�����H��

        frmProductionSelect.ShowDialog()
        '�W�[�O��
        If tempCode = "" Then
            Exit Sub
        Else
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempCode, ",")
            n = Len(Replace(tempCode, ",", "," & "*")) - Len(tempCode)
            For i = 0 To n
                If arr(i) = "" Then
                    Exit Sub
                End If
                AddRow(arr(i))
            Next

            'AddRow(tempCode)
        End If
        tempCode = ""
    End Sub
    Sub AddRow(ByVal M_Code As String) '�q�L�u�ǽs���ɤJ�����H��(�u������,���~�s��,����,�u�ǦW�ٵ�)

        'ds.Tables("ProductionChange").Clear()

        If M_Code = "" Then
        Else
            Dim pic As New ProcessMainControl
            Dim pci As List(Of ProcessMainInfo)
            pci = pic.ProcessSub_GetList(Nothing, M_Code, Nothing, Nothing, Nothing, Nothing)
            If pci.Count = 0 Then Exit Sub
            Dim i As Integer

            For i = 0 To ds.Tables("Combination").Rows.Count - 1
                If M_Code = ds.Tables("Combination").Rows(i)("Pro_NO1") Then
                    MsgBox("�@�i�椣���\�����_�ۦP�u�ǽs�X�H��....")
                    Exit Sub
                End If
            Next

            For i = 0 To pci.Count - 1

                Dim row As DataRow
                row = ds.Tables("Combination").NewRow

                row("Pro_Type1") = pci(i).Pro_Type
                row("PM_M_Code1") = pci(i).PM_M_Code
                row("PM_Type1") = pci(i).Type3ID  '�������
                row("Pro_NO1") = M_Code
                row("PS_Name1") = pci(i).PS_Name

                Dim pdi As List(Of ProductionDPTWareInventoryInfo)
                Dim pdc As New ProductionDPTWareInventoryControl
                pdi = pdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, M_Code, Nothing)
                If pdi.Count = 0 Then
                    row("M_OutQty") = 0
                Else
                    row("M_OutQty") = pdi(0).WI_Qty
                End If

                ds.Tables("Combination").Rows.Add(row)
            Next
        End If
        GridView1.MoveLast()
    End Sub
    Private Sub cmsDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDel.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "AutoID")

        If DelTemp = "AutoID" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelCombination").NewRow

            row("AutoID") = ds.Tables("Combination").Rows(GridView1.FocusedRowHandle)("AutoID")

            ds.Tables("DelCombination").Rows.Add(row)
        End If
        ds.Tables("Combination").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Function CheckData() As Boolean '�P�_��e�զX�H���O�_�i�i��

        CheckData = True

        Dim i As Integer

        For i = 0 To ds.Tables("Combination").Rows.Count - 1

            Dim Qty, ReQty As Integer '�w�q�ƶq(�o���e�������u�Ǽƶq)
            Dim pdi As List(Of ProductionDPTWareInventoryInfo)
            Dim pdc As New ProductionDPTWareInventoryControl

            pdi = pdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, ds.Tables("Combination").Rows(i)("Pro_NO1"), Nothing)

            If pdi.Count = 0 Then
                Qty = 0
                ReQty = 0
            Else
                Qty = pdi(0).WI_Qty  '�j�f���l��
                ReQty = pdi(0).WI_ReQty '��׵��l��
            End If
            'If ds.Tables("Combination").Rows(i)("M_OutQty") = 0 Then
            '    MsgBox("��e�զX�ƶq���ର0�I")
            '    CheckData = False
            '    Exit Function
            'End If

            If Qty < ds.Tables("Combination").Rows(i)("M_OutQty") Then
                MsgBox("��e�����u�Ǽƶq�p��o�X�ƶq!")
                CheckData = False
                Exit Function
            End If

            Dim psi1 As List(Of LFERP.Library.ProductionSchedule.ProductionScheduleInfo)
            Dim psc1 As New LFERP.Library.ProductionSchedule.ProductionScheduleControl


            psi1 = psc1.ProductionSchedule_GetList(Nothing, ds.Tables("Combination").Rows(i)("Pro_Type1"), Nothing, ds.Tables("Combination").Rows(i)("PM_M_Code1"), ds.Tables("Combination").Rows(i)("PM_Type1"), CStr(Format(Now, "yyyy/MM/dd")), CStr(Format(Now, "yyyy/MM/dd")), Nothing)

            If psi1.Count = 0 Then
                MsgBox("��e�Ͳ������s�b��w������Ͳ��p���A�Х��K�[�Ͳ��p���I")
                CheckData = False
                Exit Function
            Else
                CheckData = True
            End If
        Next

        Dim psi As List(Of LFERP.Library.ProductionSchedule.ProductionScheduleInfo)
        Dim psc As New LFERP.Library.ProductionSchedule.ProductionScheduleControl

        psi = psc.ProductionSchedule_GetList(Nothing, cbType.EditValue, Nothing, PM_M_Code.EditValue, gluType.EditValue, CStr(Format(Now, "yyyy/MM/dd")), CStr(Format(Now, "yyyy/MM/dd")), Nothing)

        If psi.Count = 0 Then
            MsgBox("��e�Ͳ������s�b��w������Ͳ��p���A�Х��K�[�Ͳ��p���I")
            CheckData = False
            Exit Function
        Else
            CheckData = True
        End If


    End Function

    Private Sub txtQty_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyUp
        If (e.KeyValue > 47 And e.KeyValue < 58) Or (e.KeyValue > 95 And e.KeyValue < 106) Or (e.KeyValue = 8) Or (e.KeyValue = 45) Or (e.KeyValue = 46) Then
      
        Else
            'MsgBox("�u���J��ƼƦr�I")
            txtQty.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        On Error Resume Next

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ds.Tables("ProductType").Clear()
        ppi = ppc.ProcessMain_GetList2(cbType.EditValue, PM_M_Code.EditValue)
        If ppi.Count = 0 Then
        Else

            Dim i As Integer
            For i = 0 To ppi.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductType").NewRow
                row("PM_Type") = ppi(i).Type3ID
                ds.Tables("ProductType").Rows.Add(row)
            Next
            GluEdit1.EditValue = Nothing
        End If

    End Sub

    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pc.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType.EditValue, gluType.EditValue, Nothing, True)

        Try
            If pci.Count = 0 Then Exit Sub
            ds.Tables("Process").Clear()

            Dim i As Integer
            For i = 0 To pci.Count - 1
                Dim row As DataRow
                row = ds.Tables("Process").NewRow

                row("PS_NO") = pci(i).PS_NO
                row("PS_Name") = pci(i).PS_Name

                ds.Tables("Process").Rows.Add(row)

            Next

        Catch ex As Exception

        End Try
    End Sub

    '������e�զX�u�Ǥ��o�Ƥu�Ǫ���ڧ����ƶq
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
    Public Function GetNum() As String
        Dim psi As New ProductionScheduleInfo
        Dim psc As New ProductionScheduleControl
        Dim strName As String
        strName = "P" + Format(Now, "yyMM")
        psi = psc.ProductionSchedule_GetNum(strName)
        If psi Is Nothing Then
            GetNum = strName + "00001"
        Else
            GetNum = strName + Mid((CInt(Mid(psi.PS_Num, 6)) + 100001), 2)
        End If
    End Function


    Public Function GetCode(ByVal Pro_Type As String, ByVal PM_M_Code As String, ByVal PM_Type As String) As String

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        ppi = ppc.ProcessMain_GetList(Nothing, PM_M_Code, Pro_Type, PM_Type, Nothing, Nothing)
        If ppi.Count = 0 Then
            GetCode = Nothing
            Exit Function
        Else
            GetCode = ppi(0).M_Code
        End If

    End Function
    '�ھڿ�ܪ��u�ǡ]�ɤJ������ݭn�զX���o�Ƥu�ǫH���^���ɳ̦n�O���L�զX���}�Ƥu��--������զX�Z��ڤW���u��
    Private Sub GluEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluEdit1.EditValueChanged

        If GluEdit1.EditValue = Nothing Then Exit Sub

        If Label10.Text = "PreView" Then

        Else
            Dim fi As List(Of ProductionRatioInfo)
            Dim fc As New ProductionRatioControl

            ds.Tables("Combination").Clear()

            fi = fc.ProductionRatio_GetList(Nothing, GluEdit1.EditValue, Nothing, "True")

            If fi.Count = 0 Then
                MsgBox("���u�ǨS���]�m�զX�u�ǫH���I")
                Exit Sub
            Else
                Dim i As Integer
                For i = 0 To fi.Count - 1


                    Dim row As DataRow
                    row = ds.Tables("Combination").NewRow

                    row("Pro_Type1") = fi(i).Pro_Type1
                    row("PM_M_Code1") = fi(i).PM_M_Code1
                    row("PM_Type1") = fi(i).PM_Type1  '�������
                    row("Pro_NO1") = fi(i).Pro_NO1
                    row("PS_Name1") = fi(i).PS_Name1

                    row("PR_Ratio") = fi(i).PR_Ratio '�u�Ǥ�� 2012-6-2

                    If Label10.Text = "�զX" Then
                        Dim pdi As List(Of ProductionDPTWareInventoryInfo)
                        Dim pdc As New ProductionDPTWareInventoryControl
                        pdi = pdc.ProductionDPTWareInventory_GetList(GluDep.EditValue, fi(i).Pro_NO1, Nothing)
                        If pdi.Count = 0 Then
                            row("M_OutQty") = 0
                        Else
                            row("M_OutQty") = pdi(0).WI_Qty
                        End If
                    Else   '�p�G�O��������ݩʤ��P�_�l�u�Ǽƶq---�u�ݽT�O�D�u�Ǧb�N�k���s�b��������ƶq�Y�i
                        row("M_OutQty") = 0
                    End If



                    ds.Tables("Combination").Rows.Add(row)

                Next
            End If

        End If

    End Sub

    Private Sub btnReadCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadCard.Click
        txtCardID.Text = ReadCard()
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Dim frm As New frmException
        frm.ShowDialog()

        txtCardID.Text = tempValue
        tempValue = ""
    End Sub
    ''' <summary>
    ''' �ھڤ�Ҧ۰ʥ[��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtQty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.EditValueChanged
        Dim i As Integer
        If ds.Tables("Combination").Rows.Count <= 0 Or Load_OK <> "OK" Then
            Exit Sub
        End If

        For i = 0 To ds.Tables("Combination").Rows.Count - 1

            If ds.Tables("Combination").Rows(i)("PR_Ratio") Is DBNull.Value Then
            Else
                ds.Tables("Combination").Rows(i)("M_OutQty") = Val(ds.Tables("Combination").Rows(i)("PR_Ratio")) * Val(txtQty.Text)
            End If

        Next
    End Sub
End Class