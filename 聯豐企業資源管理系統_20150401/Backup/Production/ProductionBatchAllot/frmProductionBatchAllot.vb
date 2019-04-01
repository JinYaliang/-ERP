' =============================================
' Author:		<wwq>
' Create date: <2013/4/23>
' Description:	<���u��s�W�B�ק�B�R���B�f�֡B�d��>
' =============================================

Imports LFERP.Library.ProductionBatchAllot
Imports LFERP.Library.Orders
Imports LFERP.DataSetting
Imports LFERP.Library.Material

Public Class frmProductionBatchAllot
    Dim pac As New ProductionBatchAllotControl
    Dim onc As New OrdersSubNeedController
    Dim fac As New FacControler
    Dim mc As New MaterialController
    Dim ds As New DataSet

    Private Sub frmProductionBatchAllot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�[���妸�s���A�u�[������Ƶ���Ͳ��ƥB�w�f�֪��妸
        gluOS_BatchID.Properties.DisplayMember = "OS_BatchID"
        gluOS_BatchID.Properties.ValueMember = "OS_BatchID"
        gluOS_BatchID.Properties.DataSource = pac.ProductionBatchAllot_GetOS_BatchID

        '�[���t�O
        rgluFacName.DisplayMember = "FacName"
        rgluFacName.ValueMember = "FacID"
        rgluFacName.DataSource = fac.GetFacList(Nothing, Nothing)

        CreateTable()

        If lblTittle.Text = "�Ͳ����u��-�s�W" Then
            txtPBA_ID.Text = GetID()
            dtePBA_AddDate.DateTime = Now
        ElseIf lblTittle.Text = "�Ͳ����u��-�ק�" Then
            Me.Text = "�Ͳ����u��-�ק�"
            gluOS_BatchID.Enabled = False
            dtePBA_AddDate.Enabled = False
            LoadData()
        ElseIf lblTittle.Text = "�Ͳ����u��-�d��" Then
            GroupBox1.Enabled = False
            FacName.OptionsColumn.ReadOnly = True
            PBA_Qty.OptionsColumn.ReadOnly = True
            PBA_Remark.OptionsColumn.ReadOnly = True
            PBA_Check.OptionsColumn.ReadOnly = True
            'PBA_Check.OptionsColumn.ReadOnly = True
            GridControl1.ContextMenuStrip.Enabled = False
            PBA_Check.Visible = True
            btnOK.Enabled = False
            LoadData()
        ElseIf lblTittle.Text = "�Ͳ����u��-�f��" Then
            GroupBox1.Enabled = False
            FacName.OptionsColumn.ReadOnly = True
            PBA_Qty.OptionsColumn.ReadOnly = True
            PBA_Remark.OptionsColumn.ReadOnly = True
            'PBA_Check.OptionsColumn.ReadOnly = False
            'PBA_Check.OptionsColumn.ReadOnly = True
            btnSelectAll.Visible = True
            GridControl1.ContextMenuStrip.Enabled = False
            PBA_Check.Visible = True
            LoadData()
        End If

    End Sub
    ''' <summary>
    '''  '�ͦ����u�渹
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    '''  frmProductionBatchAllot_Load()
    ''' btnOK_Click()
    Public Function GetID() As String

        Dim pai As New ProductionBatchAllotInfo
        Dim strID As String
        strID = "PBA" & Format(Now, "yyMM")
        pai = pac.ProductionBatchAllot_GetID(strID)
        If pai Is Nothing Then
            GetID = strID & "0001"
        Else
            GetID = strID + Mid((CInt(Mid(pai.PBA_ID, 7)) + 10001), 2)
        End If
    End Function

    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("ProductionBatchAllot").Columns
            .Add("AutoID", GetType(String))
            .Add("M_Code", GetType(String))
            .Add("M_Name", GetType(String))
            .Add("M_Gauge", GetType(String))
            .Add("M_Unit", GetType(String))

            .Add("ON_NeedQty", GetType(Double))
            .Add("FacID", GetType(String))
            .Add("PBA_Qty", GetType(String))
            .Add("PBA_Remark", GetType(String))
            .Add("PBA_Check", GetType(Boolean))
        End With
        GridControl1.DataSource = ds.Tables("ProductionBatchAllot")

        With ds.Tables.Add("ProductionBatchAllot_M_Code").Columns
            .Add("M_Code", GetType(String))
            .Add("M_Name", GetType(String))
            .Add("M_Gauge", GetType(String))
            .Add("M_Unit", GetType(String))

            .Add("ON_NeedQty", GetType(Double))
        End With
        rgluM_Code.DisplayMember = "M_Code"
        rgluM_Code.ValueMember = "M_Code"
        rgluM_Code.DataSource = ds.Tables("ProductionBatchAllot_M_Code")

        With ds.Tables.Add("ProductionBatchAllotDelete").Columns
            .Add("AutoID", GetType(String))
        End With

    End Sub

    Function SaveNewData() As Boolean
        Dim pai As New ProductionBatchAllotInfo
        Dim i%

        pai.PBA_ID = GetID()
        pai.OS_BatchID = gluOS_BatchID.Text
        pai.PBA_AddDate = dtePBA_AddDate.DateTime
        pai.PBA_AddUserID = InUserID

        For i = 0 To ds.Tables("ProductionBatchAllot").Rows.Count - 1
            pai.M_Code = ds.Tables("ProductionBatchAllot").Rows(i)("M_Code")
            pai.FacID = ds.Tables("ProductionBatchAllot").Rows(i)("FacID")
            pai.PBA_Qty = ds.Tables("ProductionBatchAllot").Rows(i)("PBA_Qty")
            pai.PBA_Remark = ds.Tables("ProductionBatchAllot").Rows(i)("PBA_Remark")

            If pac.ProductionBatchAllot_Add(pai) = False Then
                SaveNewData = False
                MsgBox("�s�W���ѡI", 64, "����")
                Exit Function
            Else
                SaveNewData = True
            End If
        Next

        MsgBox("�s�W�����I", 64, "����")

        Me.Close()
    End Function

    Function SaveEditData() As Boolean
        Dim pai As New ProductionBatchAllotInfo
        Dim i, j%

        '�R���ק襤�R�����ƾ�
        For j = 0 To ds.Tables("ProductionBatchAllotDelete").Rows.Count - 1
            If IsDBNull(ds.Tables("ProductionBatchAllotDelete").Rows(j)("AutoID").ToString) = False Then                    '�u�R���ƾڮw���s�b���ƾ�
                pac.ProductionBatchAllot_Delete(ds.Tables("ProductionBatchAllotDelete").Rows(j)("AutoID").ToString, Nothing)
            End If
        Next

        pai.PBA_ID = txtPBA_ID.Text
        pai.OS_BatchID = gluOS_BatchID.Text
        pai.PBA_ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        pai.PBA_ModifyUserID = InUserID
        pai.PBA_AddDate = dtePBA_AddDate.DateTime
        pai.PBA_AddUserID = InUserID

        For i = 0 To ds.Tables("ProductionBatchAllot").Rows.Count - 1
            pai.AutoID = ds.Tables("ProductionBatchAllot").Rows(i)("AutoID").ToString
            pai.M_Code = ds.Tables("ProductionBatchAllot").Rows(i)("M_Code").ToString
            pai.FacID = ds.Tables("ProductionBatchAllot").Rows(i)("FacID").ToString
            pai.PBA_Qty = ds.Tables("ProductionBatchAllot").Rows(i)("PBA_Qty")
            pai.PBA_Remark = ds.Tables("ProductionBatchAllot").Rows(i)("PBA_Remark").ToString

            If IsDBNull(ds.Tables("ProductionBatchAllot").Rows(i)("AutoID")) = True Then         '�P�_�O�_�O�ק襤�s�W���ƾ�
                If pac.ProductionBatchAllot_Add(pai) = False Then
                    SaveEditData = False
                    MsgBox("�ק異�ѡI", 64, "����")
                    Exit Function
                End If
            Else
                If pac.ProductionBatchAllot_Update(pai) = False Then
                    SaveEditData = False
                    MsgBox("�ק異�ѡI", 64, "����")
                    Exit Function
                End If
            End If

        Next

        SaveEditData = True
        MsgBox("�ק粒���I", 64, "����")

        Me.Close()
    End Function

    Function SaveCheckData() As Boolean
        Dim pai As New ProductionBatchAllotInfo
        Dim i%

        pai.PBA_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        pai.PBA_CheckUserID = InUserID

        For i = 0 To ds.Tables("ProductionBatchAllot").Rows.Count - 1
            pai.AutoID = ds.Tables("ProductionBatchAllot").Rows(i)("AutoID").ToString
            pai.PBA_Check = ds.Tables("ProductionBatchAllot").Rows(i)("PBA_Check")

            If pac.ProductionBatchAllot_UpdateCheck(pai) = False Then
                SaveCheckData = False
                MsgBox("�f�֥��ѡI", 64, "����")
                Exit Function
            End If
        Next

        SaveCheckData = True
        MsgBox("�f�֧����I", 64, "����")

        Me.Close()
    End Function

    Sub LoadData()
        Dim pai As List(Of ProductionBatchAllotInfo)
        Dim row As DataRow
        Dim i%

        pai = pac.ProductionBatchAllot_GetList(txtPBA_ID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        GridLookUpEdit1View.AddNewRow()
        GridLookUpEdit1View.SetFocusedRowCellValue(Me.OS_BatchID, pai(0).OS_BatchID)
        gluOS_BatchID.Text = pai(0).OS_BatchID

        dtePBA_AddDate.DateTime = pai(0).PBA_AddDate

        For i = 0 To pai.Count - 1
            row = ds.Tables("ProductionBatchAllot").NewRow

            row("AutoID") = pai(i).AutoID
            row("M_Code") = pai(i).M_Code
            row("M_Name") = pai(i).M_Name
            row("M_Gauge") = pai(i).M_Gauge
            row("M_Unit") = pai(i).M_Unit

            row("ON_NeedQty") = pai(i).ON_NeedQty
            row("FacID") = pai(i).FacID
            row("PBA_Qty") = pai(i).PBA_Qty
            row("PBA_Remark") = pai(i).PBA_Remark
            row("PBA_Check") = pai(i).PBA_Check

            ds.Tables("ProductionBatchAllot").Rows.Add(row)
        Next
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim i, j%
        Dim sum As Double
        Dim pai As List(Of ProductionBatchAllotInfo)

        If gluOS_BatchID.Text = "" Then
            MsgBox("�п�ܧ妸�s���I", 64, "����")
            Exit Sub
        End If
        If dtePBA_AddDate.Text = "" Then
            MsgBox("�п�J�إߤ���I", 64, "����")
            Exit Sub
        End If

        pai = pac.ProductionBatchAllot_GetList(Nothing, gluOS_BatchID.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pai.Count > 0 Then
            MsgBox("�ӧ妸�w�s�b���u��A���ݭn�A�s�W���u��!", 64, "����")
            Exit Sub
        End If

        For i = 0 To ds.Tables("ProductionBatchAllot").Rows.Count - 1
            If ds.Tables("ProductionBatchAllot").Rows(i)("FacID").ToString = "" Then
                GridView1.FocusedRowHandle = i
                MsgBox("�п�ܼt�O�I", 64, "����")
                Exit Sub
            End If

            If ds.Tables("ProductionBatchAllot").Rows(i)("PBA_Qty") < 0 Then
                GridView1.FocusedRowHandle = i
                MsgBox("�����ƶq����p��s�I", 64, "����")
                Exit Sub
            End If

            For j = 0 To ds.Tables("ProductionBatchAllot").Rows.Count - 1
                If i <> j Then
                    If ds.Tables("ProductionBatchAllot").Rows(i)("M_Code") = ds.Tables("ProductionBatchAllot").Rows(j)("M_Code") _
                                   And ds.Tables("ProductionBatchAllot").Rows(i)("FacID") = ds.Tables("ProductionBatchAllot").Rows(j)("FacID") Then
                        GridView1.FocusedRowHandle = i
                        MsgBox("�P�@�i���u�椤����s�b���ƽs�X�ۦP�A�t�O�]�ۦP���O���I", 64, "����")
                        Exit Sub
                    End If
                End If

            Next

        Next

        If lblTittle.Text = "�Ͳ����u��-�s�W" Then
            SaveNewData()
        ElseIf lblTittle.Text = "�Ͳ����u��-�ק�" Then
            SaveEditData()
        ElseIf lblTittle.Text = "�Ͳ����u��-�f��" Then
            SaveCheckData()
        End If
    End Sub

    Private Sub gluOS_BatchID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluOS_BatchID.EditValueChanged
        Dim oni As List(Of OrdersSubNeedInfo)
        Dim oni1 As List(Of OrdersSubNeedInfo)
        Dim pai As List(Of ProductionBatchAllotInfo)
        Dim row, row1 As DataRow
        Dim i, j, k, n%
        Dim isM_codeExist As Boolean = False

        If gluOS_BatchID.EditValue = "" Then Exit Sub

        oni = onc.OrdersSubNeed_AddGetList(gluOS_BatchID.Text, "W0301", "'�����','���U����'")

        '=====================================================================================
        '�妸���t���ƻP�ݨD�椤���t���Ʀ��i��s�b�t��(���ʫH����^�ɡA���i��ɭP�����ܧ�)�A
        '�]���ݭn���Ӫ�X��, �åB�h�����ưO���C
        oni1 = onc.OrdersSubNeed_GetList(Nothing, Nothing, gluOS_BatchID.Text, Nothing, Nothing, "W0301", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        ds.Tables("ProductionBatchAllot_M_Code").Clear()

        For j = 0 To oni.Count - 1
            row1 = ds.Tables("ProductionBatchAllot_M_Code").NewRow
            row1("M_Code") = oni(j).M_Code
            row1("M_Name") = oni(j).M_Name
            row1("M_Gauge") = oni(j).M_Gauge
            row1("M_Unit") = oni(j).M_Unit
            row1("ON_NeedQty") = Val(oni(j).ON_NeedQty)
            ds.Tables("ProductionBatchAllot_M_Code").Rows.Add(row1)
        Next

        For k = 0 To oni1.Count - 1
            isM_codeExist = False
            For n = 0 To oni.Count - 1
                If oni(n).M_Code = oni1(k).M_Code Then      '�P�_�O�_�s�b�ۦP�O��
                    isM_codeExist = True
                    Exit For
                End If
            Next
            If isM_codeExist = False Then       '���s�b�ۦP�O���A�h�K�[�ӰO��
                row1 = ds.Tables("ProductionBatchAllot_M_Code").NewRow
                row1("M_Code") = oni1(k).M_Code
                row1("M_Name") = oni1(k).M_Name
                row1("M_Gauge") = oni1(k).M_Gauge
                row1("M_Unit") = oni1(k).M_Unit
                row1("ON_NeedQty") = Val(oni1(k).ON_NeedQty)
                ds.Tables("ProductionBatchAllot_M_Code").Rows.Add(row1)
            End If
        Next
        '========================================================================================

        If lblTittle.Text = "�Ͳ����u��-�s�W" Then

            ds.Tables("ProductionBatchAllot").Clear()

            pai = pac.ProductionBatchAllot_GetList(Nothing, sender.text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If pai.Count > 0 Then
                MsgBox("�ӧ妸�w�s�b���u��A���ݭn�A�s�W���u��!", 64, "����")
                sender.text = ""
                Exit Sub
            End If

            For i = 0 To oni.Count - 1
                row = ds.Tables("ProductionBatchAllot").NewRow

                row("M_Code") = oni(i).M_Code
                row("M_Name") = oni(i).M_Name
                row("M_Gauge") = oni(i).M_Gauge
                row("M_Unit") = oni(i).M_Unit
                row("ON_NeedQty") = Val(oni(i).ON_NeedQty)

                row("PBA_Qty") = 0
                row("PBA_Remark") = ""

                ds.Tables("ProductionBatchAllot").Rows.Add(row)
            Next
        End If
    End Sub

    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        Dim row As DataRow

        row = ds.Tables("ProductionBatchAllot").NewRow
        row("PBA_Qty") = 0
        row("PBA_Remark") = ""
        ds.Tables("ProductionBatchAllot").Rows.Add(row)
    End Sub

    'Private Sub rgluM_Code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgluM_Code.EditValueChanged
    '    Dim i%
    '    For i = 0 To GridView2.RowCount - 1
    '        If GridView2.GetRowCellValue(i, "M_Code") = sender.text Then
    '            ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Name") = GridView2.GetRowCellValue(i, "M_Name")
    '            ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Gauge") = GridView2.GetRowCellValue(i, "M_Gauge")
    '            ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Unit") = GridView2.GetRowCellValue(i, "M_Unit")
    '            ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("ON_NeedQty") = GridView2.GetRowCellValue(i, "ON_NeedQty")
    '            ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("PBA_Qty") = 0
    '        End If
    '    Next
    'End Sub

    Private Sub cmsDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDelete.Click
        If ds.Tables("ProductionBatchAllot").Rows.Count = 0 Then Exit Sub
        '�b�R�����W�[�Q�R�����O���A�H�K�T�w�ɡA�b�ƾڮw���R�����O��
        Dim row As DataRow = ds.Tables("ProductionBatchAllotDelete").NewRow

        row("AutoID") = ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("AutoID")
        ds.Tables("ProductionBatchAllotDelete").Rows.Add(row)

        ds.Tables("ProductionBatchAllot").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub rbteM_Code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rbteM_Code.ButtonClick
        Dim mi As New MaterialInfo

        tempValue5 = "W0301"
        tempValue6 = "�ܮw�w�s�d��"

        frmBOMSelect.ShowDialog()

        If tempCode = "" Then Exit Sub

        mi = mc.MaterialCode_Get(tempCode)
        sender.text = mi.M_Code
        ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Name") = mi.M_Name
        ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Gauge") = mi.M_Gauge
        ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Unit") = mi.M_Unit
        If IsDBNull(ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("AutoID")) = True Then
            ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("ON_NeedQty") = 0
        End If
        ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("PBA_Qty") = 0

    End Sub

    Private Sub rbteM_Code_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rbteM_Code.KeyDown
        Dim mi As New MaterialInfo
        Dim i%
        If e.KeyCode = Keys.Enter Then

            mi = mc.MaterialCode_Get(sender.text)

            If mi Is Nothing Then       '�P�_��J���ƬO�_�s�b
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Name") = ""
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Gauge") = ""
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Unit") = ""
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("ON_NeedQty") = 0
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("PBA_Qty") = 0
            Else
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Name") = mi.M_Name
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Gauge") = mi.M_Gauge
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("M_Unit") = mi.M_Unit
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("PBA_Qty") = 0

                '�P�_��J���ƬO�_���ݨD��
                For i = 0 To ds.Tables("ProductionBatchAllot_M_Code").Rows.Count - 1
                    If ds.Tables("ProductionBatchAllot_M_Code").Rows(i)("M_Code").ToString = sender.text Then
                        ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("ON_NeedQty") = ds.Tables("ProductionBatchAllot_M_Code").Rows(i)("ON_NeedQty")
                        Exit Sub
                    End If
                Next
                ds.Tables("ProductionBatchAllot").Rows(GridView1.FocusedRowHandle)("ON_NeedQty") = 0
            End If

        End If
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Dim i%
        Dim bln As Boolean

        If sender.text = "����(&A)" Then
            bln = True
            sender.text = "������(&A)"
        ElseIf sender.text = "������(&A)" Then
            bln = False
            sender.text = "����(&A)"
        End If

        For i = 0 To ds.Tables("ProductionBatchAllot").Rows.Count - 1
            ds.Tables("ProductionBatchAllot").Rows(i)("PBA_Check") = bln
        Next
    End Sub
End Class