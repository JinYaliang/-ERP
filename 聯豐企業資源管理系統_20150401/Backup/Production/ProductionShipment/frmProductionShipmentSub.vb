
Imports lferp.Library.ProductionShipment

Public Class frmProductionShipmentSub
    Dim strWHOutID, strWHInID As String
    Dim ds As New DataSet
    Dim oldCheck As Boolean


    Private Sub ShipmentWareOutMainSub_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label5.Text = tempValue2
        Label3.Text = tempValue3
        tempValue2 = ""
        tempValue3 = ""
        CreateTable()

        txtWIPID.Enabled = False
        DateEdit1.Text = Format(Now, "yyyy/MM/dd")

        Select Case Label5.Text

            Case "�X�f��"
                If Edit = False Then
                    Me.Text = "�e�f��--�s�W"
                    Me.Label1.Text = "�e�f��--�s�W"
                    CheckEdit1.Enabled = False
                    CheckRemark.Enabled = False
                Else
                    txtWIPID.Text = Label3.Text
                    LoadData(Label3.Text)
                    CheckEdit1.Enabled = False
                    CheckRemark.Enabled = False
                    Me.Text = "�e�f��--�ק�" & Label3.Text
                    Me.Label1.Text = "�e�f��--�ק�"
                End If
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "PreView"
                Me.Text = "�e�f��--�d��" & Label3.Text
                Me.Label1.Text = "�e�f��--�d��"
                LoadData(Label3.Text)
                cmdSave.Visible = False
                XtraTabControl1.SelectedTabPage = XtraTabPage1
            Case "Check"
                Me.Text = "�e�f��--�f��" & Label3.Text
                Me.Label1.Text = "�e�f��--�f��"
                txtWH.Enabled = False

                cbType.Enabled = False
                DateEdit1.Enabled = False
                txtRemark.Enabled = False
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

                LoadData(Label3.Text)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
        End Select

        cmdGuideAdd.Visible = False

        txtWH.Focus()

    End Sub

    Sub CreateTable()

        ds.Tables.Clear()

        With ds.Tables.Add("ShipmentWareOutSub")

            .Columns.Add("SPM_M_Code", GetType(String))
            .Columns.Add("SM_Code", GetType(String))
            .Columns.Add("SM_Name", GetType(String))
            .Columns.Add("SM_Gauge", GetType(String))
            .Columns.Add("SPWO_Qty", GetType(Integer))
            .Columns.Add("SM_Unit", GetType(String))
            .Columns.Add("SPS_Type", GetType(String))
            .Columns.Add("SPS_SubRemark", GetType(String))
            .Columns.Add("SPS_NO_Sub", GetType(String))

        End With
        Grid.DataSource = ds.Tables("ShipmentWareOutSub")

        With ds.Tables.Add("DelShipmentWareOutSub")     '�Χ@�ק�A�w�O�s�F�h�Ӫ��ƪ���A�O���R�����Y�@�ӡA�ΤL�Ӫ���
            .Columns.Add("Del_PS_NO", GetType(String))
            .Columns.Add("Del_PS_SUB_NO", GetType(String))
        End With

        RepositoryItemComboBox1.Items.Clear()
        RepositoryItemComboBox1.Items.Add("���`")
        RepositoryItemComboBox1.Items.Add("���")


    End Sub


    Private Sub txtWH_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        tempCode = "�Ͳ��ܮw"         ''�ٻݭn�]�m�A�n�v����
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "881409"
        frmWareHouseSelect.ShowDialog()

        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHOutID = frmWareHouseSelect.SelectWareID
            txtWH.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub


    Private Sub cmsCodeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeAdd.Click
        tempCode = ""
        tempValue6 = "�X�f�޲z"

        frmBOMSelect.XtraTabPage1.PageVisible = True
        frmBOMSelect.XtraTabPage2.PageVisible = False
        frmBOMSelect.XtraTabPage3.PageVisible = True
        'frmBOMSelect.txtLFID.Text = "MG1020-2"
        frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2

        frmBOMSelect.ShowDialog()

        '�W�[�O��
        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode)
            End If
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then   '�p�׽s��
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue8, ",")
            n = Len(Replace(tempValue8, ",", "," & "*")) - Len(tempValue8)
            For i = 0 To n

                Dim j As Integer

                For j = 0 To ds.Tables("ShipmentWareOutSub").Rows.Count - 1
                    If arr(i) = ds.Tables("ShipmentWareOutSub").Rows(j)("SM_Code") Then
                        MsgBox("�@�i�椣���\�����_���ƽs�X....")
                        Exit Sub
                    End If
                Next
                If arr(i) = "" Then
                    Exit Sub
                End If

                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo

                objInfo = mc.MaterialCode_Get(arr(i))
                Dim row As DataRow
                row = ds.Tables("ShipmentWareOutSub").NewRow

                row("SPM_M_Code") = tempValue3
                row("SM_Code") = objInfo.M_Code
                row("SM_Name") = objInfo.M_Name
                row("SM_Gauge") = objInfo.M_Gauge
                row("SM_Unit") = objInfo.M_Unit
                row("SPWO_Qty") = 0
                row("SPS_Type") = "���`"

                ds.Tables("ShipmentWareOutSub").Rows.Add(row)
                GridView1.MoveLast()
            Next
        End If
        tempValue2 = ""
        tempValue7 = ""
        tempValue8 = ""
        tempValue3 = ""
    End Sub


    Sub AddRow(ByVal strCode As String)
        If strCode = "" Then
        Else

            Dim i As Integer

            For i = 0 To ds.Tables("ShipmentWareOutSub").Rows.Count - 1
                If strCode = ds.Tables("ShipmentWareOutSub").Rows(i)("SM_Code") Then
                    MsgBox("�@�i�椣���\�����_���ƽs�X....")
                    Exit Sub
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            Dim row As DataRow
            row = ds.Tables("ShipmentWareOutSub").NewRow

            If Mid(objInfo.M_Code, 1, 2) = "MG" Then
                row("SPM_M_Code") = objInfo.M_Code
            Else
                row("SPM_M_Code") = ""
            End If

            row("SM_Code") = objInfo.M_Code
            row("SM_Name") = objInfo.M_Name
            row("SM_Unit") = objInfo.M_Unit
            row("SM_Gauge") = objInfo.M_Gauge
            row("SPWO_Qty") = 0
            row("SPS_Type") = "���`"

            ds.Tables("ShipmentWareOutSub").Rows.Add(row)

            GridView1.MoveLast()
        End If
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case Label5.Text
            Case "�X�f��"
                If Edit = False Then
                    If CheckData() = True Then
                        DataNew()
                    End If
                Else
                    If CheckData() = True Then   ''�H�e�S�[
                        DataEdit()
                    End If
                End If
            Case "Check"  ''�f��
                If CheckData() = True Then
                    UpdateCheck()
                End If

        End Select
    End Sub


    Function CheckData() As Boolean  '�P�_��e�o�X�ܮw���ƬO�_����
        CheckData = True

        If txtWH.Text = "" Then
            MsgBox("�ܮw���ର��")
            CheckData = False
            txtWH.Focus()
            Exit Function
        End If

        If DateEdit1.Text = "" Then
            MsgBox("�X�f������ର��")
            CheckData = False
            DateEdit1.Focus()
            Exit Function
        End If


        'If cbType.Text = "" Then
        '    MsgBox("�������ର��")
        '    CheckData = False
        '    cbType.Focus()
        '    Exit Function
        'End If


        Dim i As Integer

        For i = 0 To ds.Tables("ShipmentWareOutSub").Rows.Count - 1

            If ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty") <= 0 Then
                MsgBox("�X�f�ƶq���ର0�I")
                CheckData = False
                GridView1.FocusedRowHandle = i '���B�ܿ��~�X��
                GridView1.FocusedColumn = GridView1.Columns("SPWO_Qty")
                Exit Function
            End If

            If ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_Type") = "" Then
                MsgBox("�п�ܥ]�˥X�f�����I")
                CheckData = False
                GridView1.FocusedRowHandle = i
                GridView1.FocusedColumn = GridView1.Columns("SPS_Type")
                Exit Function
            End If

            ''--------------------�ˬd�w�s�����w���ƶq---------------------------------------------------------------
            ''�ˬd�w�s��-------

            Dim Safe_Qty As Single
            Dim Qty As Single
            Dim wi As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi = wc.WareInventory_GetSub(ds.Tables("ShipmentWareOutSub").Rows(i)("SM_Code"), strWHOutID)

            If wi Is Nothing Then
                Qty = 0
                Safe_Qty = 0
            Else
                Qty = wi.WI_Qty
                Safe_Qty = wi.WI_SafeQty '�w���w�s
            End If

            If Qty >= ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty") Then

                If Qty - ds.Tables("ShipmentWareOutSub").Rows(0)("SPWO_Qty") < Safe_Qty Then
                    MsgBox("��e��w�ܮw�w�s�ƶq�p��w���w�s,�нT�{�I")
                    CheckData = False
                    GridView1.FocusedRowHandle = i
                    Exit Function

                Else
                    CheckData = True
                End If
            Else
                GridView1.FocusedRowHandle = i
                MsgBox("��e��w�ܮw�w�s�ƶq�p��o�X�ƶq,�нT�{�I")
                CheckData = False
                Exit Function
            End If
        Next

    End Function



    Sub DataNew()
        Dim pi As New ProductionShipmentInfo
        Dim pc As New ProductionShipmentControl

        txtWIPID.Text = GetPSNO()
        pi.PS_NO = txtWIPID.Text
        pi.PS_WareID = strWHOutID
        pi.PS_Date = DateEdit1.Text
        pi.PS_Remark = txtRemark.Text
        pi.PS_Action = InUserID
        pi.PM_Type = cbType.EditValue


        Dim i As Integer

        For i = 0 To ds.Tables("ShipmentWareOutSub").Rows.Count - 1
            pi.PS_NO_Sub = GetSubNO()

            pi.PM_M_Code = ds.Tables("ShipmentWareOutSub").Rows(i)("SPM_M_Code")

            pi.M_Code = ds.Tables("ShipmentWareOutSub").Rows(i)("SM_Code")

            If IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty")) Then
                pi.PS_Qty = 0
            Else
                pi.PS_Qty = ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty")
            End If

            If IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_Type")) Then
                pi.PS_Type = Nothing
            Else
                pi.PS_Type = ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_Type")
            End If


            If IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_SubRemark")) Then
                pi.PS_SubRemark = Nothing
            Else
                pi.PS_SubRemark = ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_SubRemark")
            End If

            pc.ProductionShipment_Add(pi)
        Next

        MsgBox("�w�O�s,�渹: " & txtWIPID.Text & " ")
        Me.Close()

    End Sub
    Private Function GetPSNO() As String
        Dim pi As New ProductionShipmentInfo
        Dim pc As New ProductionShipmentControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionShipment_GetNO(strA)
        If pi Is Nothing Then
            GetPSNO = "S" & strA & "0001"
        Else
            GetPSNO = "S" + strA + Mid((CInt(Mid(pi.PS_NO, 6)) + 10001), 2)
        End If

    End Function

    Private Function GetSubNO() As String
        Dim pi As New ProductionShipmentInfo
        Dim pc As New ProductionShipmentControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductionShipment_GetSubNO(strA)
        If pi Is Nothing Then
            GetSubNO = "SS" & strA & "0001"
        Else
            GetSubNO = "SS" + strA + Mid((CInt(Mid(pi.PS_NO_Sub, 7)) + 10001), 2)
        End If

    End Function

    Public Function LoadData(ByVal PS_NO As String) As Boolean
        LoadData = True

        ds.Tables("ShipmentWareOutSub").Clear()

        Dim piL As List(Of ProductionShipmentInfo)
        Dim pc As New ProductionShipmentControl
        piL = pc.ProductionShipment_GetList(PS_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Try
            If piL.Count = 0 Then
                MsgBox("�S���ƾ�")
                LoadData = False
                Exit Function
            Else
                strWHOutID = piL(0).PS_WareID
                txtWIPID.Text = piL(0).PS_NO
                txtWH.EditValue = piL(0).PS_OutName
                cbType.EditValue = piL(0).PM_Type
                DateEdit1.EditValue = piL(0).PS_Date
                txtRemark.Text = piL(0).PS_Remark
                CheckEdit1.Checked = piL(0).PS_Check
                CheckAction.Text = piL(0).PS_CheckAction_N

                If CheckEdit1.Checked = True Then
                    oldCheck = True
                Else
                    oldCheck = False
                End If

                CheckDate.Text = piL(0).PS_CheckDate
                CheckRemark.Text = piL(0).PS_CheckRemark

                Dim i As Integer
                Dim row As DataRow
                For i = 0 To piL.Count - 1

                    row = ds.Tables("ShipmentWareOutSub").NewRow
                    row("SPM_M_Code") = piL(i).PM_M_Code
                    row("SM_Code") = piL(i).M_Code
                    row("SM_Name") = piL(i).PS_M_Name
                    row("SPS_Type") = piL(i).PS_Type
                    row("SM_Gauge") = piL(i).PS_M_Gauge
                    row("SM_Unit") = piL(i).PS_M_Unit
                    row("SPWO_Qty") = piL(i).PS_Qty
                    row("SPS_NO_Sub") = piL(i).PS_NO_Sub
                    row("SPS_SubRemark") = piL(i).PS_SubRemark

                    ds.Tables("ShipmentWareOutSub").Rows.Add(row)
                Next

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Sub UpdateCheck()

        Dim pi As New ProductionShipmentInfo
        Dim pc As New ProductionShipmentControl

        If oldCheck = CheckEdit1.Checked Then
            MsgBox("�f�֪��A�����ܡA�Ч�窱�A��A�O�s�K")
            Exit Sub
        End If

        pi.PS_NO = txtWIPID.Text   ''�f�֪��@�ǰ򥻫H��
        pi.PS_Check = CheckEdit1.Checked
        pi.PS_CheckAction = InUserID
        pi.PS_CheckDate = Format(Now, "yyyy/MM/dd")
        pi.PS_CheckRemark = CheckRemark.Text

        If pc.ProductionShipment_UpdateCheck(pi) = True Then
            MsgBox("�f�֪��A�w����!")
        Else
            MsgBox("�f�֥���,���ˬd��]!")
            Exit Sub   ''�֬d�����������w�s
        End If
        '--------------------------------------------------------------------------�X�f�O�����ƫH��
        Dim ui As New LFERP.Library.Purchase.SharePurchase.SharePurchaseInfo
        Dim uc As New LFERP.Library.Purchase.SharePurchase.SharePurchaseController

        Dim i As Integer
        ui.WH_ID = strWHOutID ''�ܮw��

        For i = 0 To ds.Tables("ShipmentWareOutSub").Rows.Count - 1
            ui.M_Code = ds.Tables("ShipmentWareOutSub").Rows(i)("SM_Code")  ''���ƽs�X

            If CheckEdit1.Checked = False Then
                ui.WI_Qty = CSng(ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty"))
            ElseIf CheckEdit1.Checked = True Then
                ui.WI_Qty = -CSng(ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty"))
            End If

            uc.UpdateWareInventory_WIQty(ui)
        Next


        '--------------------------------------------------------------------------

    End Sub
    Private Sub cmsCodeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeDel.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "SPS_NO_Sub")   ''�O���R���O��

        If DelTemp = "SM_Code" Then
        Else
            '�b�R�����W�[�Q�R�����O��
            Dim row As DataRow = ds.Tables("DelShipmentWareOutSub").NewRow
            row("Del_PS_NO") = txtWIPID.Text
            row("Del_PS_SUB_NO") = DelTemp
            ds.Tables("DelShipmentWareOutSub").Rows.Add(row)
        End If
        ds.Tables("ShipmentWareOutSub").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Sub DataEdit()

        Dim i As Integer
        Dim pi As New ProductionShipmentInfo
        Dim pc As New ProductionShipmentControl

        '��s�R�����O��
        If ds.Tables("DelShipmentWareOutSub").Rows.Count > 0 Then   ''��s�R���w�ק諸�O��
            Dim j As Integer
            For j = 0 To ds.Tables("DelShipmentWareOutSub").Rows.Count - 1
                If Not IsDBNull(ds.Tables("DelShipmentWareOutSub").Rows(j)("Del_PS_SUB_NO")) Then
                    'MsgBox(ds.Tables("DelShipmentWareOutSub").Rows(j)("Del_PS_SUB_NO"))
                    pc.ProductionShipment_Delete(ds.Tables("DelShipmentWareOutSub").Rows(j)("Del_PS_NO"), ds.Tables("DelShipmentWareOutSub").Rows(j)("Del_PS_SUB_NO"))
                End If
            Next j
        End If

        For i = 0 To ds.Tables("ShipmentWareOutSub").Rows.Count - 1

            pi.PS_NO = txtWIPID.Text
            pi.PS_WareID = strWHOutID
            pi.PS_Date = DateEdit1.Text
            pi.PS_Remark = txtRemark.Text
            pi.PS_Action = InUserID
            pi.PM_Type = cbType.EditValue

            pi.PM_M_Code = ds.Tables("ShipmentWareOutSub").Rows(i)("SPM_M_Code")

            pi.M_Code = ds.Tables("ShipmentWareOutSub").Rows(i)("SM_Code")

            If IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty")) Then
                pi.PS_Qty = 0
            Else
                pi.PS_Qty = ds.Tables("ShipmentWareOutSub").Rows(i)("SPWO_Qty")
            End If

            If IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_Type")) Then
                pi.PS_Type = Nothing
            Else
                pi.PS_Type = ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_Type")
            End If

            If IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_SubRemark")) Then
                pi.PS_SubRemark = Nothing
            Else
                pi.PS_SubRemark = ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_SubRemark")
            End If

            'MsgBox(ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_SubRemark"))

            If IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_NO_Sub")) Then    ''�ק襤�W�[�e���S���[�����ƽs�X------------
                pi.PS_NO_Sub = GetSubNO()
                pc.ProductionShipment_Add(pi)          ''��s�μW�[����(�X�f�渹�A�l�渹(���ƽs�X))
            ElseIf Not IsDBNull(ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_NO_Sub")) Then
                pi.PS_NO_Sub = ds.Tables("ShipmentWareOutSub").Rows(i)("SPS_NO_Sub")
                pc.ProductionShipment_update(pi)
            End If
        Next

        MsgBox("�w�O�s,�渹: " & txtWIPID.Text & " ")
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class