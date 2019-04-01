Imports LFERP.Library.ProductionReturn
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionRetrocede
Imports LFERP.Library.Product

Public Class frmProductionReturn
#Region "�ݩ�"
    Dim ds As New DataSet
    Dim prc As New ProductionReturnControl
    Dim uc As New UserPowerControl
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

#Region "����Ұʸ��J�ƥ�"
    Private Sub frmProductionReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()

        strWHInID = ""
        txtNO.Enabled = False
        cbType.Enabled = False

        Select Case EditItem
            Case "ReturnADD"
                If Edit = False Then
                    DateEdit1.EditValue = Format(Now, "yyyy/MM/dd")
                    cbType.EditValue = "�ɰh�f"
                    Me.Text = "�Ͳ�-�e�f��"
                Else
                    LoadData(EditValue)
                    Me.Text = "�ק�-�e�f��" & EditValue
                End If
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage3.PageVisible = False
            Case "PreView"
                LoadData(EditValue)
                cmdSave.Visible = False
                Me.Text = "�d��--" & "�e�f��" & "-" & EditValue
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "Check"
                LoadData(EditValue)
                Me.Text = "�f��--" & "�e�f��" & "-" & EditValue
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                XtraTabPage3.PageVisible = False
                cmdSave.Enabled = False
                GroupBox1.Enabled = False
                GridView1.OptionsBehavior.Editable = False
                Grid.ContextMenuStrip.Enabled = False


            Case "InCheck"
                LoadData(EditValue)
                Me.Text = "���Ƽf��--" & "�e�f��" & "-" & EditValue
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

        With ds.Tables.Add("Return")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("R_NO", GetType(String))    '��榩���ƶq
            .Columns.Add("Pro_Type", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("AR_Qty", GetType(Integer))
            .Columns.Add("R_NoSendQty", GetType(Integer))
            .Columns.Add("PI_Qty", GetType(Integer))
            .Columns.Add("AR_Detail", GetType(String))
        End With
        Grid.DataSource = ds.Tables("Return")

        With ds.Tables.Add("DelReturn")
            .Columns.Add("AR_NO", GetType(String))
            .Columns.Add("AutoID", GetType(String))
        End With
    End Sub
#End Region

#Region "��^���J�ƾڦC��"
    Function LoadData(ByVal AR_NO As String) As Boolean
        LoadData = True

        Try
            Dim pri As List(Of ProductionReturnInfo)
            pri = prc.ProductionReturn_GetList(AR_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If pri.Count = 0 Then
                LoadData = False
                Exit Function
            Else
                Dim i As Integer
                txtNO.Text = pri(0).AR_NO
                txtWH.Text = pri(0).WH_Name
                strWHOutID = pri(0).WH_OutID
                txtWHInID.Text = pri(0).WH_InName
                strWHInID = pri(0).WH_InID
                cbType.EditValue = pri(0).AR_Type
                DateEdit1.EditValue = Format(pri(0).AR_Date, "yyyy/MM/dd")
                txtRemark.EditValue = pri(0).AR_Remark

                For i = 0 To pri.Count - 1
                    Dim row As DataRow

                    row = ds.Tables("Return").NewRow
                    row("AutoID") = pri(i).AutoID
                    row("R_NO") = pri(i).R_NO    '�������h�f�渹
                    row("Pro_Type") = pri(i).Pro_Type
                    row("PM_M_Code") = pri(i).PM_M_Code
                    row("PM_Type") = pri(i).PM_Type
                    row("AR_Qty") = pri(i).AR_Qty
                    row("AR_Detail") = pri(i).AR_Detail

                    Dim prrc As New ProductionRetrocedeControl
                    Dim prri As New List(Of ProductionRetrocedeInfo)
                    'prri = prrc.ProductionRetrocede_GetList(pri(i).R_NO, Nothing, Nothing, Nothing, Nothing, pri(i).Pro_Type, pri(i).PM_M_Code, pri(i).PM_Type, Nothing)
                    prri = prrc.ProductionRetrocede_GetList(pri(i).R_NO, Nothing, Nothing, Nothing, Nothing, Nothing, pri(i).PM_M_Code, pri(i).PM_Type, Nothing)
                    If prri.Count > 0 Then
                        row("R_NoSendQty") = prri(0).R_NoSendQty
                    Else
                        row("R_NoSendQty") = 0
                    End If

                    Dim pii As New List(Of ProductInventoryInfo)
                    pii = pic.ProductInventory_GetList(pri(0).PM_M_Code, GetM_Code("�˰t�X�f", pri(0).PM_M_Code, pri(0).PM_Type), strWHOutID, Nothing)
                    If pii.Count > 0 Then
                        row("PI_Qty") = pii(0).PI_Qty
                    Else
                        row("PI_Qty") = 0
                    End If
                    ds.Tables("Return").Rows.Add(row)

                    CheckEdit1.Checked = pri(i).AR_Check
                    CheckDate.Text = Format(pri(i).AR_CheckDate, "yyyy/MM/dd")
                    CheckAction.Text = pri(i).CheckActionName
                    CheckRemark.EditValue = pri(i).AR_CheckRemark

                    ''����
                    CheckEdit2.Checked = pri(i).AR_InCheck
                    Label5.Text = Format(pri(i).AR_InCheckDate, "yyyy/MM/dd")
                    Label3.Text = pri(i).AR_InCheckActionName
                    MemoEdit1.EditValue = pri(i).AR_InCheckRemark

                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
#End Region

#Region "�۰ʬy����"
    Function GetRNO() As String '�ͦ�-�h�f-�e�f��渹
        Dim pri As New ProductionReturnInfo
        Dim strA As String
        strA = Format(Now, "yyMM")
        pri = prc.ProductionReturn_GetRNO(strA)
        If pri Is Nothing Then
            GetRNO = "AR" + strA + "0001"
        Else
            GetRNO = "AR" + strA + Mid((CInt(Mid(pri.AR_NO, 7)) + 10001), 2)
        End If
    End Function
    Function GetMRNO() As String '�ͦ�-�ɰh�f-�e�f�渹
        Dim pri As New ProductionReturnInfo
        Dim strA As String
        strA = Format(Now, "yyMM")
        pri = prc.ProductionReturn_GetMRNO(strA)
        If pri Is Nothing Then
            GetMRNO = "AMR" + strA + "0001"
        Else
            GetMRNO = "AMR" + strA + Mid((CInt(Mid(pri.AR_NO, 8)) + 10001), 2)
        End If
    End Function
#End Region

#Region "�s�W�ƥ��k"
    Sub DataNew()
        Dim pri As New ProductionReturnInfo
        txtNO.Text = GetRNO()
        pri.AR_NO = txtNO.Text
        pri.WH_OutID = strWHOutID
        pri.WH_InID = strWHInID
        pri.AR_Type = cbType.EditValue
        pri.AR_Date = DateEdit1.EditValue
        pri.AR_Remark = txtRemark.Text
        pri.AR_Action = InUserID

        Dim i As Integer
        For i = 0 To ds.Tables("Return").Rows.Count - 1
            pri.R_NO = ds.Tables("Return").Rows(i)("R_NO")
            pri.Pro_Type = ds.Tables("Return").Rows(i)("Pro_Type")
            pri.PM_M_Code = ds.Tables("Return").Rows(i)("PM_M_Code")
            pri.PM_Type = ds.Tables("Return").Rows(i)("PM_Type")
            pri.AR_Qty = CSng(ds.Tables("Return").Rows(i)("AR_Qty"))

            If IsDBNull(ds.Tables("Return").Rows(i)("AR_Detail")) Then
                pri.AR_Detail = Nothing
            Else
                pri.AR_Detail = ds.Tables("Return").Rows(i)("AR_Detail")
            End If
            prc.ProductionReturn_Add(pri)
        Next
        MsgBox("�O�s�渹" & txtNO.Text & "�H�����\!")
        Me.Close()
    End Sub
#End Region

#Region "�ק�ƥ��k"
    Sub DataEdit()
        Dim pri As New ProductionReturnInfo
        pri.AR_NO = txtNO.Text
        pri.WH_OutID = strWHOutID
        pri.WH_InID = strWHInID
        pri.AR_Type = cbType.EditValue
        pri.AR_Date = DateEdit1.EditValue
        pri.AR_Remark = txtRemark.Text
        pri.AR_Action = InUserID

        Dim i As Integer
        If ds.Tables("DelReturn").Rows.Count > 0 Then
            For i = 0 To ds.Tables("DelReturn").Rows.Count - 1
                Dim mc2 As New ProductionReturnControl
                If Not IsDBNull(ds.Tables("DelReturn").Rows(i)("AutoID")) Then
                    mc2.ProductionReturn_Delete(ds.Tables("DelReturn").Rows(i)("AutoID"), Nothing)
                End If
            Next i
        End If

        For i = 0 To ds.Tables("Return").Rows.Count - 1
            If IsDBNull(ds.Tables("Return").Rows(i)("AutoID")) Then   '�s�W]
                pri.R_NO = ds.Tables("Return").Rows(i)("R_NO")
                pri.Pro_Type = ds.Tables("Return").Rows(i)("Pro_Type")
                pri.PM_M_Code = ds.Tables("Return").Rows(i)("PM_M_Code")
                pri.PM_Type = ds.Tables("Return").Rows(i)("PM_Type")
                pri.AR_Qty = CSng(ds.Tables("Return").Rows(i)("AR_Qty"))
                If IsDBNull(ds.Tables("Return").Rows(i)("AR_Detail")) Then
                    pri.AR_Detail = Nothing
                Else
                    pri.AR_Detail = ds.Tables("Return").Rows(i)("AR_Detail")
                End If
                prc.ProductionReturn_Add(pri)
            ElseIf Not IsDBNull(ds.Tables("Return").Rows(i)("AutoID")) Then   '�ק�
                pri.R_NO = ds.Tables("Return").Rows(i)("R_NO")
                pri.AutoID = ds.Tables("Return").Rows(i)("AutoID")
                pri.Pro_Type = ds.Tables("Return").Rows(i)("Pro_Type")
                pri.PM_M_Code = ds.Tables("Return").Rows(i)("PM_M_Code")
                pri.PM_Type = ds.Tables("Return").Rows(i)("PM_Type")
                pri.AR_Qty = CSng(ds.Tables("Return").Rows(i)("AR_Qty"))

                If IsDBNull(ds.Tables("Return").Rows(i)("AR_Detail")) Then
                    pri.AR_Detail = Nothing
                Else
                    pri.AR_Detail = ds.Tables("Return").Rows(i)("AR_Detail")
                End If
                prc.ProductionReturn_Update(pri)
            End If
        Next
        MsgBox("�O�s�渹" & txtNO.Text & "�H�����\!")
        Me.Close()
    End Sub
#End Region

#Region "�f�֨ƥ��k"
    Sub UpdateCheck()
        Dim pri As New ProductionReturnInfo
        pri.AR_Check = CheckEdit1.Checked
        pri.AR_CheckAction = InUserID
        pri.AR_CheckDate = Format(Now, "yyyy/MM/dd")
        pri.AR_CheckRemark = CheckRemark.Text

        Dim i As Integer
        For i = 0 To ds.Tables("Return").Rows.Count - 1
            pri.AutoID = ds.Tables("Return").Rows(i)("AutoID")
            If prc.ProductionReturn_UpdateCheck(pri) = False Then
                GridView1.FocusedRowHandle = i
                MsgBox("�O���G" & ds.Tables("Return").Rows(i)("PM_M_Code").ToString & " / " & ds.Tables("Return").Rows(i)("PM_Type").ToString & " �f�֥���,���ˬd��]!")
                GridView1.Focus()
                Exit Sub
            End If
        Next
        MsgBox("�f�֧����I", 64, "����")
        '--------------------------------------------
        '------------�ܮw���ƥ�Ĳ�o������------------
        '-----------ProductionReturn_UpdateQty1------
        '--------------------------------------------
        Me.Close()
    End Sub


    Sub UpdateInCheck()
        Dim pri As New ProductionReturnInfo
        pri.AR_InCheck = CheckEdit2.Checked
        pri.AR_InCheckAction = InUserID
        pri.AR_InCheckDate = Format(Now, "yyyy/MM/dd")
        pri.AR_InCheckRemark = MemoEdit1.Text

        Dim i As Integer
        For i = 0 To ds.Tables("Return").Rows.Count - 1
            pri.AutoID = ds.Tables("Return").Rows(i)("AutoID")
            If prc.ProductionReturn_UpdateInCheck(pri) = False Then
                GridView1.FocusedRowHandle = i
                MsgBox("�O���G" & ds.Tables("Return").Rows(i)("PM_M_Code").ToString & " / " & ds.Tables("Return").Rows(i)("PM_Type").ToString & " �f�֥���,���ˬd��]!")
                GridView1.Focus()
                Exit Sub
            End If
        Next
        MsgBox("�f�֧����I", 64, "����")
        '--------------------------------------------
        '------------�ܮw���ƥ�Ĳ�o������------------
        '-----------ProductionReturn_UpdateQty1------
        '--------------------------------------------
        Me.Close()
    End Sub

#End Region

#Region "����ƥ�"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim i, j, sum%
        Dim strM_Code1, strM_Code2 As String

        If txtWH.EditValue = "" Then
            MsgBox("�e�f�ܮw���ର�šA�п�J�e�f�ܮw!", 64, "����")
            txtWH.Focus()
            Exit Sub
        End If
        If txtWHInID.EditValue = "" Then
            MsgBox("�����ܮw���ର�šA�п�J�����ܮw!", 64, "����")
            txtWHInID.Focus()
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

        For i = 0 To ds.Tables("Return").Rows.Count - 1
            If ds.Tables("Return").Rows(i)("PM_M_Code").ToString = "" Then
                MsgBox("���~�s�����ର�šA�п�J���~�s���I", 64, "����")
                Grid.Focus()
                Exit Sub
            End If
            If ds.Tables("Return").Rows(i)("Pro_Type").ToString = "" Then
                MsgBox("�������ର�šA�п�J�����I", 64, "����")
                Grid.Focus()
                Exit Sub
            End If
            If CInt(ds.Tables("Return").Rows(i)("AR_Qty")) <= 0 Then
                MsgBox("�e�f�ƶq����p�󵥩�s�A�Э��s��J�e�f�ƶq�I", 64, "����")
                Grid.Focus()
                Exit Sub
            End If
            '2013-11-06
            If EditItem = "Check" And CheckEdit1.Checked = False Then
            Else
                If CInt(ds.Tables("Return").Rows(i)("AR_Qty")) > CInt(ds.Tables("Return").Rows(i)("R_NoSendQty")) Then
                    MsgBox("�e�f�ƶq����j�󥼥�ơA�Э��s��J�e�f�ƶq�I", 64, "����")
                    Grid.Focus()
                    Exit Sub
                End If
            End If

            strM_Code1 = GetM_Code(ds.Tables("Return").Rows(i)("Pro_Type"), ds.Tables("Return").Rows(i)("PM_M_Code"), ds.Tables("Return").Rows(i)("PM_Type"))

            ''���n�P�_�˰t------>>>�Ͳ�(�Ͳ������L�إߦ��u������)
            '--------------------------------------------------------------------------
            Dim pc1 As New ProcessMainControl
            Dim pci1 As New List(Of ProcessMainInfo)
            pci1 = pc1.ProcessMain_GetList1(Nothing, ds.Tables("Return").Rows(i)("PM_M_Code"), "�Ͳ��[�u", strM_Code1)

            If pci1.Count <= 0 Then
                MsgBox("�S���إ߷�e[�Ͳ��[�u]�u��" + ds.Tables("Return").Rows(i)("PM_M_Code") + ",�нT�{�I")
            End If
            '---------------------------------------------------------------------------

            sum = CInt(ds.Tables("Return").Rows(i)("AR_Qty"))
            For j = 0 To ds.Tables("Return").Rows.Count - 1
                strM_Code2 = GetM_Code(ds.Tables("Return").Rows(j)("Pro_Type"), ds.Tables("Return").Rows(j)("PM_M_Code"), ds.Tables("Return").Rows(j)("PM_Type"))
                If i <> j Then
                    If ds.Tables("Return").Rows(i)("PM_M_Code") = ds.Tables("Return").Rows(j)("PM_M_Code") And strM_Code1 = strM_Code2 Then
                        sum = sum + CInt(ds.Tables("Return").Rows(j)("AR_Qty"))
                    End If
                End If
                '------------------------------------------------
                Dim intPI_Qty As Int32
                Dim pii1 As New List(Of ProductInventoryInfo)
                pii1 = pic.ProductInventory_GetList(ds.Tables("Return").Rows(i)("PM_M_Code"), strM_Code1, strWHOutID, Nothing)
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
                '-----------------------------------------------
            Next
        Next

        Select Case EditItem
            Case "ReturnADD"
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
    Private Sub ReturnSubAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnSubAdd.Click
        tempCode = ""
        frmProductionSelect.ShowDialog()
        '�W�[�O��
        If tempCode = "" Then
            Exit Sub
        Else
            AddRow(tempCode)
        End If
    End Sub

    Sub AddRow(ByVal M_Code As String) '�q�L�u�ǽs���ɤJ�����H��(�u������,���~�s��,����,�u�ǦW�ٵ�)
        ds.Tables("Return").Clear()
        If M_Code = "" Then
        Else
            Dim pc As New ProcessMainControl
            Dim pci As List(Of ProcessMainInfo)
            pci = pc.ProcessSub_GetList(Nothing, M_Code, Nothing, Nothing, Nothing, Nothing)
            If pci.Count = 0 Then Exit Sub
            Dim i As Integer

            For i = 0 To pci.Count - 1
                Dim row As DataRow
                row = ds.Tables("Return").NewRow

                row("R_NO") = ""     '------�h�f�渹
                row("Pro_Type") = pci(i).Pro_Type
                row("PM_M_Code") = pci(i).PM_M_Code
                row("PM_Type") = pci(i).PM_Type
                row("AR_Qty") = 0
                ds.Tables("Return").Rows.Add(row)
            Next
        End If
        GridView1.MoveLast()
    End Sub
    '�ɤJ��J���h�f��ԲӫH��
    Private Sub LoadReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadReturn.Click
        If strWHOutID = Nothing Then
            MsgBox("�п�ܵo�f�ܮw!")
            txtWH.Select()
            Exit Sub
        End If

        If strWHInID = Nothing Then
            MsgBox("�п�ܱ����ܮw!")
            txtWHInID.Select()
            Exit Sub
        End If


        tempValue = ""
        'StrINWare = tempValue2
        'StrOutWare = tempValue3
        tempValue2 = strWHOutID
        tempValue3 = strWHInID

        frmLoadRetrocede.ShowDialog()

        Dim i, n As Integer
        Dim YanZeng As Boolean    '���,�ݸ��J�U������ƬO�_��0�έt��
        YanZeng = True
        If RefreshT = True Then
            Dim arr(n) As String
            arr = Split(tempValue, ",")
            n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)

            For i = 0 To n
                Dim prrc As New ProductionRetrocedeControl
                Dim prri As New List(Of ProductionRetrocedeInfo)

                prri = prrc.ProductionRetrocede_GetList2(Nothing, arr(i), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, Nothing, Nothing, Nothing)

                If prri.Count = 1 Then
                    Dim j As Integer

                    If strWHOutID <> "" And strWHOutID <> prri(0).WH_InID Then
                        MsgBox("���h�f�椣�ݩ�w��w�e�f�ܮw�A����ɰh�f�I", 64, "����")
                        Grid.Focus()
                        Exit Sub
                    End If

                    If strWHInID <> "" And strWHInID <> prri(0).WH_OutID Then
                        MsgBox("���h�f�椤�������ܮw�P�w��w�������ܮw���@�P�I", 64, "����")
                        Grid.Focus()
                        Exit Sub
                    End If

                    For j = 0 To ds.Tables("Return").Rows.Count - 1

                        Dim strA, strB As String

                        strA = prri(0).R_NO & prri(0).Pro_Type & prri(0).PM_M_Code & prri(0).PM_Type
                        strB = ds.Tables("Return").Rows(j)("R_NO") & ds.Tables("Return").Rows(j)("Pro_Type") & ds.Tables("Return").Rows(j)("PM_M_Code") & ds.Tables("Return").Rows(j)("PM_Type")

                        If strA = strB Then
                            MsgBox("�@�i��ۦP�t�󪫮ƫH�������\�s�b�h���O���P�P�P�P")
                            Exit Sub
                        End If
                    Next

                    Dim row As DataRow
                    row = ds.Tables("Return").NewRow

                    row("R_NO") = prri(0).R_NO
                    row("Pro_Type") = "�˰t�X�f"
                    row("PM_M_Code") = prri(0).PM_M_Code
                    row("PM_Type") = prri(0).PM_Type
                    row("AR_Qty") = 0
                    row("R_NoSendQty") = prri(0).R_NoSendQty

                    Dim pii As New List(Of ProductInventoryInfo)
                    pii = pic.ProductInventory_GetList(prri(0).PM_M_Code, GetM_Code("�˰t�X�f", prri(0).PM_M_Code, prri(0).PM_Type), strWHOutID, Nothing)
                    If pii.Count > 0 Then
                        row("PI_Qty") = pii(0).PI_Qty
                    Else
                        row("PI_Qty") = 0
                    End If
                    ds.Tables("Return").Rows.Add(row)
                End If
            Next
        End If
        tempValue = ""
        RefreshT = False
    End Sub

    Private Sub ReturnSubDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnSubDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "AutoID")

        If DelTemp = "AutoID" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelReturn").NewRow
            row("AutoID") = ds.Tables("Return").Rows(GridView1.FocusedRowHandle)("AutoID")
            row("AR_NO") = txtNO.Text
            ds.Tables("DelReturn").Rows.Add(row)
        End If
        ds.Tables("Return").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        tempCode = "�Ͳ��ܮw"
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "880906"
        tempValue2 = "880906"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWH.Left + 15
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + txtWH.Top + txtWH.Height + 143
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHOutID = frmWareHouseSelect.SelectWareID
            txtWH.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub

    Private Sub txtWHInID_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWHInID.ButtonClick
        tempCode = "�Ͳ��ܮw"
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "880907"
        tempValue2 = "880907"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWHInID.Left + 15
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + txtWHInID.Top + txtWHInID.Height + 143
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHInID = frmWareHouseSelect.SelectWareID
            txtWHInID.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub
#End Region

#Region "������J�ƥ��k"
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
    Private Sub CheckEdit1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit1.CheckedChanged
        cmdSave.Enabled = Not cmdSave.Enabled
    End Sub

    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        cmdSave.Enabled = Not cmdSave.Enabled
    End Sub
#End Region

  

End Class