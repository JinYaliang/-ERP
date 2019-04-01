Imports LFERP.Library.ProductionRetrocede
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.DataSetting
Imports LFERP.Library.Product
Public Class frmProductionRetrocede
#Region "�ݩ�"
    Dim ds As New DataSet
    Dim prc As New ProductionRetrocedeControl
    Dim uc As New UserPowerControl
    Dim mpc As New ProductController
    Dim ppc As New ProcessMainControl
    Dim pic As New ProductInventoryController

    Private strWHOutID As String
    Private strWHInID As String
    Private _EditItem As String
    Private _EditValue As String
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
#End Region

#Region "���J����ƥ�"
    Private Sub frmProductionRetrocede_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        txtNO.Enabled = False
        cbType.Enabled = False

        Select Case EditItem
            Case "ADD"
                If Edit = False Then
                    DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                    cbType.EditValue = "�h�f"
                    Me.Text = "�˰t-�h�f"
                Else
                    LoadData(EditValue)
                    Me.Text = "�ק�--" & EditItem & "-" & EditValue
                End If
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage2.PageVisible = False
                XtraTabPage3.PageVisible = False
            Case "PreView"
                LoadData(EditValue)
                cmdSave.Visible = False
                Me.Text = "�d��--" & EditItem & "-" & EditValue
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "Check"
                LoadData(EditValue)
                Me.Text = "�f��--" & EditItem & "-" & EditValue
                cmdSave.Enabled = False
                XtraTabControl1.SelectedTabPage = XtraTabPage2

                GroupBox1.Enabled = False
                GridView1.OptionsBehavior.Editable = False
                Grid.ContextMenuStrip.Enabled = False
                XtraTabPage3.PageVisible = False

            Case "InCheck"
                LoadData(EditValue)
                Me.Text = "���Ƽf��--" & EditItem & "-" & EditValue

                XtraTabControl1.SelectedTabPage = XtraTabPage3
                XtraTabPage2.PageVisible = False
                cmdSave.Enabled = False
                GroupBox1.Enabled = False
                GridView1.OptionsBehavior.Editable = False
                Grid.ContextMenuStrip.Enabled = False
        End Select
    End Sub
#End Region
 
#Region "�Ы��{�ɪ�"
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Retrocede")
            .Columns.Add("IndexNO", GetType(String))
            .Columns.Add("Pro_Type", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("R_Qty", GetType(Integer))
            .Columns.Add("PI_Qty", GetType(Integer))
            .Columns.Add("R_Detail", GetType(String))
        End With
        Grid.DataSource = ds.Tables("Retrocede")

        With ds.Tables.Add("DelRetrocede")
            .Columns.Add("R_NO", GetType(String))
            .Columns.Add("IndexNO", GetType(String))
        End With

        ''2013-10-18���~�ʺA�W�[
        With ds.Tables.Add("PM_M_Code")
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_JiYu", GetType(String))
        End With
        PM_M_CodeLook.ValueMember = "PM_M_Code"
        PM_M_CodeLook.DisplayMember = "PM_M_Code"
        PM_M_CodeLook.DataSource = ds.Tables("PM_M_Code")


    End Sub
#End Region

#Region "��^�ƾڦC��"
    Function LoadData(ByVal R_NO As String) As Boolean
        LoadData = True
        Try
            Dim pri As List(Of ProductionRetrocedeInfo)
            Dim pii As List(Of ProductInventoryInfo)
            pri = prc.ProductionRetrocede_GetList(R_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If pri.Count = 0 Then
                LoadData = False
                Exit Function
            Else
                Dim mc As New ProcessMainControl
                PM_M_CodeLook.DisplayMember = "PM_M_Code"
                PM_M_CodeLook.ValueMember = "PM_M_Code"
                PM_M_CodeLook.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)

                Dim i As Integer
                For i = 0 To pri.Count - 1
                    txtNO.Text = pri(i).R_NO
                    txtWH.EditValue = pri(i).WH_Name
                    strWHOutID = pri(i).WH_OutID
                    txtWHInID.EditValue = pri(i).WH_InName
                    strWHInID = pri(i).WH_InID
                    cbType.EditValue = pri(i).R_Type
                    DateEdit1.EditValue = Format(pri(i).R_Date, "yyyy/MM/dd")
                    txtRemark.EditValue = pri(i).R_Remark
                    Dim row As DataRow

                    row = ds.Tables("Retrocede").NewRow
                    row("IndexNO") = pri(i).IndexNO
                    row("Pro_Type") = pri(i).Pro_Type
                    row("PM_M_Code") = pri(i).PM_M_Code
                    row("PM_Type") = pri(i).PM_Type
                    row("R_Qty") = pri(i).R_Qty
                    row("R_Detail") = pri(i).R_Detail
                    '-----------------�[���w�s--------------------------
                    pii = pic.ProductInventory_GetList(pri(i).PM_M_Code, GetM_Code(pri(i).Pro_Type, pri(i).PM_M_Code, pri(i).PM_Type), strWHOutID, Nothing)
                    If pii.Count > 0 Then
                        row("PI_Qty") = pii(0).PI_Qty
                    Else
                        row("PI_Qty") = 0
                    End If
                    '----------------------------------------------------------
                    ds.Tables("Retrocede").Rows.Add(row)

                    CheckEdit1.Checked = pri(i).R_Check
                    CheckDate.Text = Format(pri(i).R_CheckDate, "yyyy/MM/dd")
                    CheckAction.Text = pri(i).CheckActionName
                    CheckRemark.EditValue = pri(i).R_CheckRemark

                    '����
                    CheckEdit2.Checked = pri(i).R_InCheck
                    Label5.Text = Format(pri(i).R_InCheckDate, "yyyy/MM/dd")
                    Label3.Text = pri(i).R_InCheckActionName
                    MemoEdit1.EditValue = pri(i).R_InCheckRemark

                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
#End Region

#Region "�۰ʬy����"
    Public Function GetRNO() As String '�h�f�渹
        Dim pri As New ProductionRetrocedeInfo
        Dim strA As String
        strA = Format(Now, "yyMM")
        pri = prc.ProductionRetrocede_GetRNO(strA)
        If pri Is Nothing Then
            GetRNO = "R" + strA + "0001"
        Else
            GetRNO = "R" + strA + Mid((CInt(Mid(pri.R_NO, 6)) + 10001), 2)
        End If
    End Function
#End Region

#Region "�s�W�ƥ�"
    Sub DataNew()
        Dim pri As New ProductionRetrocedeInfo

            txtNO.Text = GetRNO()
            pri.R_NO = txtNO.Text
            pri.WH_OutID = strWHOutID
            pri.WH_InID = strWHInID
            pri.R_Type = cbType.EditValue
            pri.R_Date = DateEdit1.EditValue
            pri.R_Remark = txtRemark.Text
            pri.R_Action = InUserID

            Dim i As Integer
            For i = 0 To ds.Tables("Retrocede").Rows.Count - 1
                pri.Pro_Type = ds.Tables("Retrocede").Rows(i)("Pro_Type")
                pri.PM_M_Code = ds.Tables("Retrocede").Rows(i)("PM_M_Code")
                pri.PM_Type = ds.Tables("Retrocede").Rows(i)("PM_Type")
                pri.R_Qty = CDbl(ds.Tables("Retrocede").Rows(i)("R_Qty"))
                pri.R_NoSendQty = CDbl(ds.Tables("Retrocede").Rows(i)("R_Qty"))
                If IsDBNull(ds.Tables("Retrocede").Rows(i)("R_Detail")) Then
                    pri.R_Detail = Nothing
                Else
                    pri.R_Detail = ds.Tables("Retrocede").Rows(i)("R_Detail")
                End If
                prc.ProductionRetrocede_Add(pri)
            Next
            MsgBox("�O�s�渹" & txtNO.Text & "�H�����\!")
            Me.Close()
    End Sub
#End Region

#Region "�ק�ƥ�"
    Sub DataEdit()
        Dim pri As New ProductionRetrocedeInfo
        pri.R_NO = txtNO.Text
        pri.WH_OutID = strWHOutID
        pri.WH_InID = strWHInID
        pri.R_Type = cbType.EditValue
        pri.R_Date = DateEdit1.EditValue
        pri.R_Remark = txtRemark.Text
        pri.R_Action = InUserID

        '��s�R�����O��
        Dim i As Integer
        If ds.Tables("DelRetrocede").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelRetrocede").Rows.Count - 1
                Dim mc2 As New ProductionRetrocedeControl
                If Not IsDBNull(ds.Tables("DelRetrocede").Rows(i)("IndexNO")) Then
                    mc2.ProductionRetrocede_Delete(ds.Tables("DelRetrocede").Rows(i)("IndexNO"), Nothing)
                End If
            Next i
        End If

        For i = 0 To ds.Tables("Retrocede").Rows.Count - 1
            If IsDBNull(ds.Tables("Retrocede").Rows(i)("IndexNO")) Then   '�s�W
                pri.Pro_Type = ds.Tables("Retrocede").Rows(i)("Pro_Type")
                pri.PM_M_Code = ds.Tables("Retrocede").Rows(i)("PM_M_Code")
                pri.PM_Type = ds.Tables("Retrocede").Rows(i)("PM_Type")
                pri.R_Qty = CDbl(ds.Tables("Retrocede").Rows(i)("R_Qty"))
                pri.R_NoSendQty = CSng(ds.Tables("Retrocede").Rows(i)("R_Qty"))
                If IsDBNull(ds.Tables("Retrocede").Rows(i)("R_Detail")) Then
                    pri.R_Detail = Nothing
                Else
                    pri.R_Detail = ds.Tables("Retrocede").Rows(i)("R_Detail")
                End If
                prc.ProductionRetrocede_Add(pri)
            ElseIf Not IsDBNull(ds.Tables("Retrocede").Rows(i)("IndexNO")) Then   '�ק�
                pri.IndexNO = ds.Tables("Retrocede").Rows(i)("IndexNO")
                pri.Pro_Type = ds.Tables("Retrocede").Rows(i)("Pro_Type")
                'MsgBox(pri.Pro_Type & "," & ds.Tables("Retrocede").Rows(i)("Pro_Type"))
                pri.PM_M_Code = ds.Tables("Retrocede").Rows(i)("PM_M_Code")
                pri.PM_Type = ds.Tables("Retrocede").Rows(i)("PM_Type")
                pri.R_Qty = CDbl(ds.Tables("Retrocede").Rows(i)("R_Qty"))
                pri.R_NoSendQty = CDbl(ds.Tables("Retrocede").Rows(i)("R_Qty"))
                If IsDBNull(ds.Tables("Retrocede").Rows(i)("R_Detail")) Then
                    pri.R_Detail = Nothing
                Else
                    pri.R_Detail = ds.Tables("Retrocede").Rows(i)("R_Detail")
                End If
                If prc.ProductionRetrocede_Update(pri) = False Then
                    MsgBox("�O�s����!")
                    Exit Sub
                End If
            End If
        Next
        MsgBox("�O�s�渹" & txtNO.Text & "�H�����\!")
        Me.Close()
    End Sub
#End Region

#Region "�f�֨ƥ�"
    Sub UpdateCheck()
        Dim i As Integer
        Dim pri As New ProductionRetrocedeInfo

        For i = 0 To ds.Tables("Retrocede").Rows.Count - 1
            pri.IndexNO = ds.Tables("Retrocede").Rows(i)("IndexNO")
            pri.R_Check = CheckEdit1.Checked
            pri.R_CheckAction = InUserID
            pri.R_CheckDate = Format(Now, "yyyy/MM/dd")
            pri.R_CheckRemark = CheckRemark.Text

            If prc.ProductionRetrocede_UpdateCheck(pri) = False Then
                GridView1.FocusedRowHandle = i
                MsgBox("�O���G" & ds.Tables("Retrocede").Rows(i)("PM_M_Code") & " / " & ds.Tables("Retrocede").Rows(i)("PM_Type") & "�f�֥���!")
                Exit Sub
            End If
        Next
        MsgBox("�f�֧���")
        '-----------����B�z��Ĳ�o��-----------
        '-------ProductionRetrocede_UpdateQty1
        '--------------------------------------
        Me.Close()
    End Sub


    Sub UpdateInCheck()
        Dim i As Integer
        Dim pri As New ProductionRetrocedeInfo

        For i = 0 To ds.Tables("Retrocede").Rows.Count - 1
            pri.IndexNO = ds.Tables("Retrocede").Rows(i)("IndexNO")
            pri.R_InCheck = CheckEdit2.Checked
            pri.R_InCheckAction = InUserID
            pri.R_InCheckDate = Format(Now, "yyyy/MM/dd")
            pri.R_InCheckRemark = MemoEdit1.Text

            If prc.ProductionRetrocede_UpdateInCheck(pri) = False Then
                GridView1.FocusedRowHandle = i
                MsgBox("�O���G" & ds.Tables("Retrocede").Rows(i)("PM_M_Code") & " / " & ds.Tables("Retrocede").Rows(i)("PM_Type") & "�f�֥���!")
                Exit Sub
            End If
        Next
        MsgBox("���Ƽf�֧���")
        '-----------����B�z��Ĳ�o��-----------
        '-------ProductionRetrocede_UpdateQty1
        '--------------------------------------
        Me.Close()
    End Sub


#End Region

#Region "����ƥ�ЫO�s�T�{�аh�X�ƥ�"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim i, j, sum%
        Dim strM_Code1, strM_Code2 As String

        If txtWH.EditValue = "" Then
            MsgBox("�h�f�ܮw���ର��!", 64, "����")
            txtWH.Focus()
            Exit Sub
        End If
        If txtWHInID.EditValue = "" Then
            MsgBox("�����ܮw���ର��!", 64, "����")
            txtWHInID.Focus()
            Exit Sub
        End If
        If txtWH.EditValue = txtWHInID.EditValue Then
            MsgBox("�h�f�ܮw�P�����ܮw����O�P�@�ӭܮw!", 64, "����")
            txtWH.Focus()
            Exit Sub
        End If
        If cbType.EditValue = "" Then
            MsgBox("�������ର��!", 64, "����")
            cbType.Focus()
            Exit Sub
        End If
        If GridView1.RowCount = 0 Then
            MsgBox("�вK�[���Ʃ���!", 64, "����")
            Grid.Focus()
            Exit Sub
        End If
        For i = 0 To ds.Tables("Retrocede").Rows.Count - 1
            If ds.Tables("Retrocede").Rows(i)("PM_M_Code").ToString = "" Then
                MsgBox("���~�s�����ର�šA�п�J���~�s���I", 64, "����")
                Grid.Focus()
                Exit Sub
            End If
            If ds.Tables("Retrocede").Rows(i)("Pro_Type").ToString = "" Then
                MsgBox("�������ର�šA�п�J�����I", 64, "����")
                Grid.Focus()
                Exit Sub
            End If
            If CInt(ds.Tables("Retrocede").Rows(i)("R_Qty")) <= 0 Then
                MsgBox("�ƶq����p�󵥩�s�A�Э��s��J�ƶq�I", 64, "����")
                Grid.Focus()
                Exit Sub
            End If
            If ds.Tables("Retrocede").Rows(i)("R_Detail").ToString = "" Then
                MsgBox("�h�f��]���ର�šA�п�J�h�f��]�I", 64, "����")
                Grid.Focus()
                Exit Sub
            End If
            '---------------------�P�_�ۦP���ƪ��w�s�O�_����------------------
            sum = CInt(ds.Tables("Retrocede").Rows(i)("R_Qty"))
            strM_Code1 = GetM_Code(ds.Tables("Retrocede").Rows(i)("Pro_Type"), ds.Tables("Retrocede").Rows(i)("PM_M_Code"), ds.Tables("Retrocede").Rows(i)("PM_Type"))


            ''���n�P�_�˰t------>>>�Ͳ�(�Ͳ������L�إߦ��u������)
            '--------------------------------------------------------------------------
            Dim pc1 As New ProcessMainControl
            Dim pci1 As New List(Of ProcessMainInfo)
            pci1 = pc1.ProcessMain_GetList1(Nothing, ds.Tables("Retrocede").Rows(i)("PM_M_Code"), "�Ͳ��[�u", strM_Code1)

            If pci1.Count <= 0 Then
                MsgBox("�S���إ߷�e[�Ͳ��[�u]�u��" + ds.Tables("Retrocede").Rows(i)("PM_M_Code") + ",�нT�{�I")
            End If
            '---------------------------------------------------------------------------

            For j = 0 To ds.Tables("Retrocede").Rows.Count - 1
                If i <> j Then
                    '������ƽs�X
                    strM_Code2 = GetM_Code(ds.Tables("Retrocede").Rows(j)("Pro_Type"), ds.Tables("Retrocede").Rows(j)("PM_M_Code"), ds.Tables("Retrocede").Rows(j)("PM_Type"))
                    If strM_Code1 = strM_Code2 And ds.Tables("Retrocede").Rows(i)("PM_M_Code") = ds.Tables("Retrocede").Rows(j)("PM_M_Code") Then
                        sum = sum + CInt(ds.Tables("Retrocede").Rows(j)("R_Qty"))     '�֥[�ۦP���Ƽƶq
                    End If
                End If
                '---------------------------------------------------------------------------
                Dim intPI_Qty As Int32
                Dim pii1 As New List(Of ProductInventoryInfo)
                pii1 = pic.ProductInventory_GetList(ds.Tables("Retrocede").Rows(i)("PM_M_Code"), strM_Code1, strWHOutID, Nothing)
                If pii1.Count > 0 Then
                    intPI_Qty = pii1(0).PI_Qty
                Else
                    intPI_Qty = 0
                End If

                '2013-11-06
                If EditItem = "Check" And CheckEdit1.Checked = False Then
                Else
                    If sum > intPI_Qty Then
                        MsgBox("�ۦP���ƪ��`�ƶq����j��w�s�ƶq�I", 64, "����")
                        Exit Sub
                    End If
                End If

            Next
        Next

        Select Case EditItem
            Case "ADD"
                If Edit = False Then
                    DataNew()
                Else
                    DataEdit()
                End If
            Case "Check"
                UpdateCheck()
            Case "InCheck"
                UpdateInCheck()
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ReSubAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReSubAdd.Click
        'tempCode = ""

        'frmProductionSelect.ShowDialog()
        ''�W�[�O��
        'If tempCode = "" Then
        '    Exit Sub
        'Else
        '    AddRow(tempCode)
        'End If
        'tempCode = ""

        If txtWH.Text = "" Then
            txtWH.Select()
            MsgBox("�Х���ܰh�f�ܮw!")
            Exit Sub
        End If

        If txtWHInID.Text = "" Then
            txtWHInID.Select()
            MsgBox("�п�ܦ��f�ܮw�I")
            Exit Sub
        End If
        '-------------------------------------------------
        '�ϥηs�K�[��O�����Ҧ��i��h�f�O���K�[�e
        '-------------------------------------------------

        Dim row As DataRow
        Dim mc As New ProcessMainControl

        row = ds.Tables("Retrocede").NewRow

        row("Pro_Type") = "�˰t�X�f" '�q�{���˰t�X�f
        row("PM_M_Code") = ""
        row("PM_Type") = ""
        row("R_Qty") = 0
        row("PI_Qty") = 0
        row("R_Detail") = ""

        ds.Tables("Retrocede").Rows.Add(row)


        'PM_M_CodeLook.DisplayMember = "PM_M_Code"
        'PM_M_CodeLook.ValueMember = "PM_M_Code" 
        'PM_M_CodeLook.DataSource = mc.ProcessMain_GetList3(Nothing, Nothing)

    End Sub
    Private Sub ReSubDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReSubDel.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "IndexNO")

        If DelTemp = "IndexNO" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelRetrocede").NewRow

            row("IndexNO") = ds.Tables("Retrocede").Rows(GridView1.FocusedRowHandle)("IndexNO")
            row("R_NO") = txtNO.Text

            ds.Tables("DelRetrocede").Rows.Add(row)
        End If
        ds.Tables("Retrocede").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub
    Private Sub txtWHInID_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWHInID.ButtonClick
        tempCode = "�Ͳ��ܮw"
        frmWareHouseSelect.SelectWareID = ""
        tempValue2 = "880807"
        tempValue3 = "880807"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWHInID.Left + 16
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + txtWHInID.Top + txtWHInID.Height + 143
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHInID = frmWareHouseSelect.SelectWareID
            txtWHInID.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub

    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        tempCode = "�Ͳ��ܮw"
        frmWareHouseSelect.SelectWareID = ""
        tempValue2 = "880806"
        tempValue3 = "880806"

        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWH.Left + 16
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + txtWH.Top + txtWH.Height + 143
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHOutID = frmWareHouseSelect.SelectWareID
            txtWH.Text = frmWareHouseSelect.SelectWareName
            '2013-10-18
            LoadPM_M_Code(strWHOutID)

        End If
    End Sub

    Sub AddRow(ByVal M_Code As String) '�q�L�u�ǽs���ɤJ�����H��(�u������,���~�s��,����,�u�ǦW�ٵ�)
        ds.Tables("Retrocede").Clear()
        If M_Code = "" Then
        Else
            Dim pc As New ProcessMainControl
            Dim pci As List(Of ProcessMainInfo)
            pci = pc.ProcessSub_GetList(Nothing, M_Code, Nothing, Nothing, Nothing, Nothing)
            If pci.Count = 0 Then Exit Sub
            Dim i As Integer

            For i = 0 To pci.Count - 1
                Dim row As DataRow
                row = ds.Tables("Retrocede").NewRow
                row("Pro_Type") = "�˰t�X�f" ''pci(i).Pro_Type
                row("PM_M_Code") = pci(i).PM_M_Code
                row("PM_Type") = pci(i).PM_Type
                row("R_Qty") = 0
                ds.Tables("Retrocede").Rows.Add(row)
            Next
        End If
        GridView1.MoveLast()

    End Sub
#End Region

#Region "������J��k"
    '---�q�L���~�u���H���o������[�u�����ƽs�X�H�� 2012/8/23
    Function GetM_Code(ByVal Pro_Type As String, ByVal PM_M_Code As String, ByVal PM_Type As String) As String

        Dim pc As New ProcessMainControl
        Dim pci As List(Of ProcessMainInfo)

        pci = pc.ProcessMain_GetList(Nothing, PM_M_Code, Pro_Type, PM_Type, Nothing, Nothing)

        If pci.Count = 0 Then
            MsgBox("�S���إ߷�e���~�u���H���A�нT�{�I")
            GetM_Code = Nothing
            Exit Function
        Else
            GetM_Code = pci(0).M_Code   '�o���e���~�u���H�����������ƽs�X
        End If
    End Function

    Private Sub PM_M_CodeLook_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PM_M_CodeLook.EditValueChanged
        ds.Tables("Retrocede").Rows(GridView1.FocusedRowHandle)("PM_Type") = ""
        ds.Tables("Retrocede").Rows(GridView1.FocusedRowHandle)("R_Qty") = 0
        ds.Tables("Retrocede").Rows(GridView1.FocusedRowHandle)("R_Detail") = ""
        GridView1.Focus()
    End Sub

    Private Sub GridView2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.Click
        On Error Resume Next
        If GridView2.RowCount > 0 Then
            GridView1.SetFocusedRowCellValue(PM_Type, GridView2.GetFocusedRowCellValue("Type3ID"))       '�ǭ�
            GridView1.Focus()
            Dim i As Integer
            For i = 0 To ds.Tables("Retrocede").Rows.Count - 1
                If IsDBNull(ds.Tables("Retrocede").Rows(i)("PM_Type")) = False And i <> GridView1.FocusedRowHandle Then

                    If ds.Tables("Retrocede").Rows(i)("PM_M_Code") = GridView1.GetFocusedRowCellValue("PM_M_Code") And ds.Tables("Retrocede").Rows(i)("PM_Type") = GridView1.GetFocusedRowCellValue("PM_Type") Then
                        PopupContainerControl1.OwnerEdit.ClosePopup()
                        MsgBox("�����~�����w�s�b,����K�[�ۦP������!", 64, "����")

                        GridView1.SetFocusedRowCellValue(PM_Type, "")  '�N��e��PM_Type��Ȭ���

                        Exit Sub
                    End If
                End If
            Next
            '------�[���w�s----
            Dim pii As List(Of ProductInventoryInfo)
            pii = pic.ProductInventory_GetList(GridView1.GetFocusedRowCellValue("PM_M_Code"), GetM_Code(GridView1.GetFocusedRowCellValue("Pro_Type"), GridView1.GetFocusedRowCellValue("PM_M_Code"), GridView1.GetFocusedRowCellValue("PM_Type")), strWHOutID, Nothing)
            If pii.Count > 0 Then
                GridView1.SetFocusedRowCellValue(PI_Qty, pii(0).PI_Qty)
            Else
                GridView1.SetFocusedRowCellValue(PI_Qty, 0)
            End If
            '-------------------------
        End If
        '���s���

        ds.Tables("Retrocede").Rows(GridView1.FocusedRowHandle)("R_Qty") = 0
        ds.Tables("Retrocede").Rows(GridView1.FocusedRowHandle)("R_Detail") = ""
    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemPopupContainerEdit1.Enter
        If GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString = "" Then   '���~�s�����Ů�,�������]����
            GridControl1.DataSource = Nothing
        Else
            GridControl1.DataSource = ppc.ProcessMain_GetList2(GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Pro_Type").ToString, GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "PM_M_Code").ToString)
        End If
    End Sub

    '�]�m�ֱ���
    Private Sub PM_M_CodeLook_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles PM_M_CodeLook.KeyDown, RepositoryItemPopupContainerEdit1.KeyDown, RepositoryItemComboBox1.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If

    End Sub
    ''' <summary>
    ''' ����ƥ�
    ''' </summary>
    Private Sub GridView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridView2_Click(Nothing, Nothing)
        End If
    End Sub
    ''' <summary>
    ''' �f�ֽT�{�_��حȧ��ܮɡA�T�w���s�~����
    ''' </summary>
    Private Sub CheckEdit1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit1.CheckedChanged
        cmdSave.Enabled = Not cmdSave.Enabled
    End Sub

    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        cmdSave.Enabled = Not cmdSave.Enabled
    End Sub


    Sub LoadPM_M_Code(ByVal strGluDep As String)
        Dim mi As New List(Of LFERP.Library.Production.Datasetting.ProductionDataSettingInfo)
        Dim mc As New LFERP.Library.Production.Datasetting.ProductionDataSettingControl
        mi = mc.ProductionUser_GetList(strGluDep, Nothing)

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

            Dim mpi As New List(Of ProcessMainInfo)
            Dim mpc As New ProcessMainControl
            mpi = mpc.ProcessMain_GetList3(Nothing, Nothing)

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


End Class