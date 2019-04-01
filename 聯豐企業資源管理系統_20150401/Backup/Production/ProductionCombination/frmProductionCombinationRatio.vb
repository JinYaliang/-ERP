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


Public Class frmProductionCombinationRatio

    Dim uc As New SystemUser.SystemUserController
    Dim dc As New DepartmentControler
    Dim fc As New LFERP.Library.PieceProcess.PersonnelControl
    Dim pc As New ProductionCombinationControl
    Dim prc As New ProductionRatioControl

    Dim ds As New DataSet
    Dim upi As List(Of UserPowerInfo)
    Dim upc As New UserPowerControl

    Dim OldCheck As Boolean
    Dim DeclareQty As Single
    Public strPro_NO As String
    Dim isAdd As Boolean        '�O���O�_�O�s�W

    Private Sub frmProductionCombination_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()       '�եγЫت�L�{

        '�[�����~�s��
        Dim mc As New ProcessMainControl
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        PM_M_Code.Properties.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)
        isAdd = False

        Select Case lblTittle.Text
            Case "�u�ǲզX���-�s�W"
                isAdd = True
            Case "�u�ǲզX���-�ק�"
                Me.Text = lblTittle.Text
                LoadData()      '�եΥ[���ƾڹL�{

            Case "�u�ǲզX���-�d��"
                Me.Text = lblTittle.Text
                cmdSave.Enabled = False
                LoadData()      '�եΥ[���ƾڹL�{
                Grid.ContextMenuStrip.Enabled = False
            Case "�u�ǲզX���-�f��"
                Me.Text = lblTittle.Text
                LoadData()      '�եΥ[���ƾڹL�{
                lblPR_CheckDate.Text = Format(Now, "yyyy/MM/dd")
                lblPR_CheckUserName.Text = UserName
                cbType.Enabled = False
                PM_M_Code.Enabled = False
                gluType.Enabled = False
                GluEdit1.Enabled = False
                MemoEdit1.Enabled = False
                PR_Ratio.OptionsColumn.ReadOnly = True
                Grid.ContextMenuStrip.Enabled = False
        End Select

    End Sub
    ''' <summary>
    ''' �Ыت�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' frmProductionCombination_Load()
    Sub CreateTable()
        ds.Tables.Clear()

        '�t��W�٪�
        With ds.Tables.Add("ProductType")
            .Columns.Add("PM_Type", GetType(String))
        End With
        gluType.Properties.ValueMember = "PM_Type"
        gluType.Properties.DisplayMember = "PM_Type"
        gluType.Properties.DataSource = ds.Tables("ProductType")

        '�u�ǦW�٪�
        With ds.Tables.Add("Process")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        GluEdit1.Properties.ValueMember = "PS_NO"
        GluEdit1.Properties.DisplayMember = "PS_Name"
        GluEdit1.Properties.DataSource = ds.Tables("Process")

        '��ҫH����
        With ds.Tables.Add("Combination")

            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("Pro_Type1", GetType(String))
            .Columns.Add("PM_M_Code1", GetType(String))
            .Columns.Add("PM_Type1", GetType(String))
            .Columns.Add("PS_Name1", GetType(String))
            .Columns.Add("Pro_NO1", GetType(String))
            .Columns.Add("PR_Ratio", GetType(Integer))

        End With
        Grid.DataSource = ds.Tables("Combination")

        '�R����(�Ω�ק�ɧR���ƾڥ�)
        With ds.Tables.Add("DelCombination")
            .Columns.Add("AutoID", GetType(String))
        End With
    End Sub
    ''' <summary>
    ''' �[���ƾڹL�{
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' frmProductionCombination_Load()
    Function LoadData() As Boolean
        LoadData = True

        ds.Tables("Combination").Clear()

        Dim pri As List(Of ProductionRatioInfo)

        pri = prc.ProductionRatio_GetList(Nothing, strPro_NO, Nothing, Nothing)
        '�P�_�u�ǽs���O�_�s�b
        If pri.Count = 0 Then
            LoadData = False
        Else
            cbType.EditValue = pri(0).Pro_Type
            PM_M_Code.EditValue = pri(0).PM_M_Code
            gluType.EditValue = pri(0).PM_Type
            GluEdit1.EditValue = pri(0).Pro_NO
            MemoEdit1.EditValue = pri(0).PR_Remark
            chkPR_Check.Checked = pri(0).PR_Check
            lblPR_CheckUserName.Text = pri(0).PR_CheckUserName
            lblPR_CheckDate.Text = Format(pri(0).PR_CheckDate, "yyyy/MM/dd")

            Dim row As DataRow
            Dim i As Integer
            For i = 0 To pri.Count - 1

                row = ds.Tables("Combination").NewRow

                row("AutoID") = pri(i).AutoID
                row("Pro_Type1") = pri(i).Pro_Type1
                row("PM_M_Code1") = pri(i).PM_M_Code1
                row("PM_Type1") = pri(i).PM_Type1
                row("Pro_NO1") = pri(i).Pro_NO1 '���ä����
                row("PS_Name1") = pri(i).PS_Name1
                row("PR_Ratio") = pri(i).PR_Ratio

                ds.Tables("Combination").Rows.Add(row)
            Next

            If chkPR_Check.Checked = True Or lblTittle.Text = "�u�ǲզX���-�f��" Then
                chkPR_Check.Visible = True
                Panel1.Visible = True
            End If
        End If

    End Function
    ''' <summary>
    ''' �O�s�s�W�ƾڹL�{
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' cmdSave_Click()
    Sub DataNew()
        Dim i, j As Integer
        Dim pri As New ProductionRatioInfo

        '�P�_�u�ǲզX�O�_�w�s�b
        For j = 0 To ds.Tables("Combination").Rows.Count - 1
            If prc.ProductionRatio_GetList(Nothing, GluEdit1.EditValue, ds.Tables("Combination").Rows(j)("Pro_NO1"), Nothing).Count > 0 Then
                GridView1.FocusedRowHandle = j
                MsgBox("�w�s�b���u�ǲզX!", 64, "����")
                Exit Sub
            End If
        Next

        pri.Pro_Type = cbType.EditValue
        pri.PM_M_Code = PM_M_Code.EditValue
        pri.PM_Type = gluType.EditValue
        pri.Pro_NO = GluEdit1.EditValue

        pri.PR_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        pri.PR_Remark = MemoEdit1.EditValue
        pri.PR_AddUserID = InUserID

        For i = 0 To ds.Tables("Combination").Rows.Count - 1

            pri.Pro_Type1 = ds.Tables("Combination").Rows(i)("Pro_Type1")
            pri.PM_M_Code1 = ds.Tables("Combination").Rows(i)("PM_M_Code1")
            pri.PM_Type1 = ds.Tables("Combination").Rows(i)("PM_Type1")
            pri.Pro_NO1 = ds.Tables("Combination").Rows(i)("Pro_NO1")
            pri.PS_Name1 = ds.Tables("Combination").Rows(i)("PS_Name1")
            pri.PR_Ratio = ds.Tables("Combination").Rows(i)("PR_Ratio")

            prc.ProductionRatio_Add(pri)

        Next

        MsgBox("�s�W����!", 64, "����")
        Me.Close()

    End Sub
    ''' <summary>
    ''' �O�s�ק�ƾڹL�{
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' cmdSave_Click()
    Sub DataEdit()
        Dim i, j As Integer
        Dim pri As New ProductionRatioInfo

        '�P�_�R�����O�_���ݭn�R�����H���A���h�R���ƾڮw�������H��
        If ds.Tables("DelCombination").Rows.Count > 0 Then
            For j = 0 To ds.Tables("DelCombination").Rows.Count - 1
                prc.ProductionRatio_Delete(ds.Tables("DelCombination").Rows(j)("AutoID"), Nothing)
            Next
        End If

        pri.Pro_Type = cbType.EditValue
        pri.PM_M_Code = PM_M_Code.EditValue
        pri.PM_Type = gluType.EditValue
        pri.Pro_NO = GluEdit1.EditValue

        pri.PR_ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        pri.PR_Remark = MemoEdit1.EditValue
        pri.PR_ModifyUserID = InUserID

        For i = 0 To ds.Tables("Combination").Rows.Count - 1
            If ds.Tables("Combination").Rows(i)("AutoID") Is DBNull.Value Then      '�P�_�O�_���s�W�ƾ�
                pri.Pro_Type1 = ds.Tables("Combination").Rows(i)("Pro_Type1")
                pri.PM_M_Code1 = ds.Tables("Combination").Rows(i)("PM_M_Code1")
                pri.PM_Type1 = ds.Tables("Combination").Rows(i)("PM_Type1")
                pri.Pro_NO1 = ds.Tables("Combination").Rows(i)("Pro_NO1")
                pri.PS_Name1 = ds.Tables("Combination").Rows(i)("PS_Name1")
                pri.PR_Ratio = ds.Tables("Combination").Rows(i)("PR_Ratio")

                pri.PR_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                pri.PR_AddUserID = InUserID

                prc.ProductionRatio_Add(pri)
            Else
                pri.AutoID = ds.Tables("Combination").Rows(i)("AutoID")
                pri.Pro_Type1 = ds.Tables("Combination").Rows(i)("Pro_Type1")
                pri.PM_M_Code1 = ds.Tables("Combination").Rows(i)("PM_M_Code1")
                pri.PM_Type1 = ds.Tables("Combination").Rows(i)("PM_Type1")
                pri.Pro_NO1 = ds.Tables("Combination").Rows(i)("Pro_NO1")
                pri.PS_Name1 = ds.Tables("Combination").Rows(i)("PS_Name1")
                pri.PR_Ratio = ds.Tables("Combination").Rows(i)("PR_Ratio")

                prc.ProductionRatio_Update(pri)
            End If
        Next

        MsgBox("�ק粒��!", 64, "����")
        Me.Close()

    End Sub
    ''' <summary>
    ''' �O�s�f�֫H���L�{
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' cmdSave_Click()
    Sub DataCheck()
        Dim pri As New ProductionRatioInfo

        pri.Pro_NO = GluEdit1.EditValue
        pri.PR_Check = chkPR_Check.Checked
        pri.PR_CheckUserID = InUserID
        pri.PR_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

        If prc.ProductionRatio_Check(pri) = True Then
            MsgBox("�f�֧���!", 64, "����")
            Me.Close()
        Else
            MsgBox("�f�֥���!", 64, "����")
        End If

    End Sub
    ''' <summary>
    ''' �����T�w���s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim i As Integer
        If GluEdit1.Text.Trim = "" Then
            MsgBox("�u�ǦW�٤��ର��!", 64, "����")
            GluEdit1.Focus()
            Exit Sub
        End If
        For i = 0 To ds.Tables("Combination").Rows.Count - 1
            If ds.Tables("Combination").Rows(i)("PR_Ratio") < 1 Then
                GridView1.FocusedRowHandle = i
                MsgBox("�u�Ǥ�Ҥ���p��1�I")
                Exit Sub
            End If
        Next

        If ds.Tables("Combination").Rows.Count = 1 Then
            If ds.Tables("Combination").Rows(0)("PR_Ratio") = 1 Then
                MsgBox("�u���@�Ӥu�ǲզX�ɡA�u�Ǥ�Ҥ��൥��1�I")
                Exit Sub
            End If
        End If

        Select Case lblTittle.Text
            Case "�u�ǲզX���-�s�W"
                DataNew()

            Case "�u�ǲզX���-�ק�"
                DataEdit()

            Case "�u�ǲզX���-�f��"
                DataCheck()

        End Select
    End Sub
    ''' <summary>
    ''' �����������s,��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �����k���桧�K�[��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        tempCode = ""
     
        tempValue2 = "�u�ǩ���޲z"

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
        End If
        tempCode = ""
    End Sub
    ''' <summary>
    ''' �V��ҫH�����R�ƾڹL�{
    ''' </summary>
    ''' <param name="M_Code"></param>
    '''���L�{�Q�H�U�L�{�եΡG
    ''' cmsAdd_Click()
    Sub AddRow(ByVal M_Code As String) '�q�L�u�ǽs���ɤJ�����H��(�u������,���~�s��,����,�u�ǦW�ٵ�)

        If M_Code = "" Then
        Else
            If GluEdit1.EditValue = M_Code Then   '@ 2012/7/25 �K�[�@
                MsgBox("�u�ǡG" & GluEdit1.Text & " �P�j�u�ǬۦP�A����K�[!", 64, "����")
                Exit Sub
            End If

            Dim pic As New ProcessMainControl
            Dim pci As List(Of ProcessMainInfo)
            pci = pic.ProcessSub_GetList(Nothing, M_Code, Nothing, Nothing, Nothing, Nothing)
            If pci.Count = 0 Then Exit Sub

            Dim i As Integer
            '�P�_�u�ǬO�_�w�s�b
            For i = 0 To ds.Tables("Combination").Rows.Count - 1
                If M_Code = ds.Tables("Combination").Rows(i)("Pro_NO1") Then
                    MsgBox("�u�ǡG" & ds.Tables("Combination").Rows(i)("PS_Name1") & " �w�s�b!", 64, "����")
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
                row("PR_Ratio") = 1

                ds.Tables("Combination").Rows.Add(row)
            Next
        End If
        GridView1.MoveLast()
    End Sub
    ''' <summary>
    ''' �����k���桧�R����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
    ''' <summary>
    ''' ���~�s�����ܮɡA�[���������t��W�٫H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' cbType_SelectedIndexChanged()
    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        On Error Resume Next

        Dim ppc As New ProcessMainControl
        Dim ppi As List(Of ProcessMainInfo)
        gluType.Text = ""
        ds.Tables("ProductType").Clear()
        ds.Tables("Process").Clear()
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
    ''' <summary>
    ''' �t��W�٧��ܮɡA�[���������u�ǦW�٫H��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged
        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)
        pci = pc.ProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType.EditValue, gluType.EditValue, Nothing, True)

        Try

            GluEdit1.Text = ""
            ds.Tables("Process").Clear()

            If pci.Count = 0 Then Exit Sub

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
    ''' <summary>
    ''' �u���������ܮɡA�եβ��~�s�����ܹL�{
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbType.SelectedIndexChanged
        PM_M_Code_EditValueChanged(Nothing, Nothing)    '�եβ��~�s�����ܹL�{
    End Sub
    ''' <summary>
    ''' �j�u�ǧ��ܮɡA�P�_�Ӥu�ǬO�_�w�s�b�զX�A�s�b�h�[��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' @ 2012/7/25 �K�[
    Private Sub GluEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GluEdit1.EditValueChanged
        If GluEdit1.EditValue Is Nothing Then Exit Sub

        If isAdd = True Then    '�P�_�O�_�O�s�W
            ds.Tables("Combination").Rows.Clear()

            chkPR_Check.Visible = False
            chkPR_Check.Enabled = True
            Panel1.Visible = False
            cmdSave.Enabled = True
            Grid.ContextMenuStrip.Enabled = True

            Dim pri As List(Of ProductionRatioInfo)
            pri = prc.ProductionRatio_GetList(Nothing, GluEdit1.EditValue, Nothing, Nothing)
            If pri.Count > 0 Then
                Dim i%

                chkPR_Check.Checked = pri(0).PR_Check
                lblPR_CheckUserName.Text = pri(0).PR_CheckUserName
                lblPR_CheckDate.Text = pri(0).PR_CheckDate

                For i = 0 To pri.Count - 1

                    Dim row As DataRow
                    row = ds.Tables("Combination").NewRow

                    row("AutoID") = pri(i).AutoID
                    row("Pro_Type1") = pri(i).Pro_Type1
                    row("PM_M_Code1") = pri(i).PM_M_Code1
                    row("PM_Type1") = pri(i).PM_Type1
                    row("Pro_NO1") = pri(i).Pro_NO1 '���ä����
                    row("PS_Name1") = pri(i).PS_Name1
                    row("PR_Ratio") = pri(i).PR_Ratio

                    ds.Tables("Combination").Rows.Add(row)
                Next

                If pri(0).PR_Check = True Then
                    MsgBox("�u�ǲզX�w�s�b�A�B�w�f�֡A�]������i��ާ@�I", 64, "����")

                    chkPR_Check.Visible = True
                    chkPR_Check.Enabled = False
                    Panel1.Visible = True
                    cmdSave.Enabled = False
                    Grid.ContextMenuStrip.Enabled = False
                Else
                    MsgBox("�u�ǲզX�w�s�b�I", 64, "����")
                    lblTittle.Text = "�u�ǲզX���-�ק�"
                    Me.Text = "�u�ǲզX���-�ק�"
                End If
            Else
                lblTittle.Text = "�u�ǲզX���-�s�W"
                Me.Text = "�u�ǲզX���-�s�W"
            End If
        End If
    End Sub
End Class