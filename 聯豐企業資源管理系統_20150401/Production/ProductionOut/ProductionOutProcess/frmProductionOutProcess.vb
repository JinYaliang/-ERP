Imports LFERP.Library.Production.ProductionType
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.ProductionDPTWareInventory
Imports LFERP.DataSetting
Imports LFERP.Library.Material
Imports LFERP.Library.ProductionOutProcess

Public Class frmProductionOutProcess
    Dim poc As New ProductionOutProcessControl
    Dim pdc As New ProductionDPTWareInventoryControl
    Dim ds As New DataSet
    Dim strError As String
    Public strPO_Type As String
    Dim strPM_M_Code As String
    Dim strPM_Type As String

    Private Sub frmProductionOutProcess_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�[���~�o�����ӫH��
        Dim mtd As New SuppliersControler
        gluS_SupplierName.Properties.DisplayMember = "S_SupplierName"
        gluS_SupplierName.Properties.ValueMember = "S_Supplier"
        gluS_SupplierName.Properties.DataSource = mtd.GetSuppliersList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")

        CreateTable()       '�եγЫت�L�{

        Select Case Microsoft.VisualBasic.Right(lblTittle.Text, 2)
            Case "�s�W"
                If strPO_Type = "�~�o���" Then
                    lblPO_ID.Text = "�~�o��׳渹(&I):"
                End If
                Me.Text = strPO_Type & "�� - -�s�W"
                txtPO_UserName.Text = UserName
                dtePO_OutDate.DateTime = Format(Now, "yyyy/MM/dd")
                txtPO_ID.Text = GetID()     '�եΦ۰ʥͦ��~�o�[�u�渹�L�{
                XtraTabPage2.PageVisible = False
            Case "�ק�"
                If strPO_Type = "�~�o���" Then
                    lblPO_ID.Text = "�~�o��׳渹(&I):"
                End If
                Me.Text = strPO_Type & "��--�ק�"
                XtraTabPage2.PageVisible = False
                LoadData()      '�եΥ[���ƾڹL�{
            Case "�d��"
                If strPO_Type = "�~�o���" Then
                    lblPO_ID.Text = "�~�o��׳渹(&I):"
                End If
                Me.Text = strPO_Type & "��--�d��"
                cmdOK.Visible = False       '���ýT�w���s
                LoadData()      '�եΥ[���ƾڹL�{
                Grid.ContextMenuStrip.Enabled = False    '�k����L��
            Case "�f��"
                If strPO_Type = "�~�o���" Then
                    lblPO_ID.Text = "�~�o��׳渹(&I):"
                End If
                Me.Text = strPO_Type & "��--�f��"
                XtraTabPage2.PageVisible = True
                XtraTabControl1.SelectedTabPageIndex = 1

                '�Ҧ��ƾڳ����uŪ
                gluS_SupplierName.Properties.ReadOnly = True
                txtPO_UserName.Properties.ReadOnly = True
                dtePO_OutDate.Properties.ReadOnly = True
                txtPO_Remark.Properties.ReadOnly = True

                OW_Do.OptionsColumn.ReadOnly = True
                PO_ProAttribute.OptionsColumn.ReadOnly = True
                PO_Qty.OptionsColumn.ReadOnly = True
                PO_PM_Remark.OptionsColumn.ReadOnly = True

                Grid.ContextMenuStrip.Enabled = False       '�k����L��

                LoadData()      '�եΥ[���ƾڹL�{

                lblPO_CheckUserName.Text = UserName
                lblPO_CheckDate.Text = Format(Now, "yyyy/MM/dd")

                cmdOK.Enabled = False   '�T�w���s�L��
        End Select

    End Sub

    '�Ыت�
    '���L�{�Q�H�U�L�{�եΡG
    'frmProductionOutProcess_Load()
    Sub CreateTable()
        ds.Tables.Clear()

        '�Ыإ~�o�[�u��H����
        With ds.Tables.Add("ProductionOutProcess")

            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))

            .Columns.Add("OW_Do", GetType(String))
            .Columns.Add("PO_ProAttribute", GetType(String))
            .Columns.Add("PO_Qty", GetType(Integer))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("PO_PM_Remark", GetType(String)) '
            .Columns.Add("PM_JiYu", GetType(String)) 'PM_JiYu

        End With
        Grid.DataSource = ds.Tables("ProductionOutProcess")     '�j�w���Grid����

        '�ЫاR���H����A�Ω�ק�ɧR���ƾڥ�
        With ds.Tables.Add("DelOutProcess")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("PO_ID", GetType(String))
        End With
    End Sub

    '�b��ProductionOutProcess���K�[�ƾ�
    '���L�{�Q�H�U�L�{�եΡG
    'MenuAdd_Click()
    Sub AddRow(ByVal M_Code As String) '�q�L�u�ǽs���ɤJ�����H��(�u������,���~�s��,����,�u�ǦW�ٵ�)

        If M_Code <> "" Then

            '2012-7-27
            Dim pdi1 As List(Of ProductionDPTWareInventoryInfo)
            Dim pdc1 As New ProductionDPTWareInventoryControl
            pdi1 = pdc1.ProductionDPTWareInventory_GetList("F101", M_Code, Nothing)
            If pdi1.Count <= 0 Then
                MsgBox("�~�o���w�s�L���u�Ǧs�b!")
                Exit Sub
            End If


            Dim pic As New ProcessMainControl
            Dim pci As List(Of ProcessMainInfo)

            pci = pic.ProcessSub_GetList(Nothing, M_Code, Nothing, Nothing, Nothing, Nothing)
            If pci.Count = 0 Then Exit Sub
            Dim i As Integer

            ''�@�i�椣���\�����_�ۦP�u�ǽs�X�H��
            'For i = 0 To ds.Tables("ProductionOutProcess").Rows.Count - 1
            '    If M_Code = ds.Tables("ProductionOutProcess").Rows(i)("PS_NO") Then
            '        MsgBox("�@�i�椣���\�����_�ۦP�u�ǽs�X�H��....")
            '        Exit Sub
            '    End If

            'Next

            For i = 0 To pci.Count - 1
                Dim row As DataRow
                row = ds.Tables("ProductionOutProcess").NewRow

                row("AutoID") = Nothing
                row("PM_M_Code") = pci(i).PM_M_Code
                row("PM_Type") = pci(i).Type3ID  '�������

                row("PM_JiYu") = pci(i).PM_JiYu

                If LoadProductionType(pci(i).PM_M_Code, pci(i).Type3ID) = True Then
                    GoTo A
                Else
                    Exit Sub
                End If
A:              If strPO_Type = "�~�o���" Then
                    RepositoryItemComboBox2.Items.Clear()
                    RepositoryItemComboBox2.Items.Add("���")
                    RepositoryItemComboBox2.Items.Add("���-���")
                    RepositoryItemComboBox2.Items.Add("���-��q")
                End If


                row("PS_NO") = M_Code
                row("PS_Name") = pci(i).PS_Name
                row("OW_Do") = RepositoryItemComboBox1.Items.Item(0)
                row("PO_ProAttribute") = RepositoryItemComboBox2.Items.Item(0)

                Dim pdi As List(Of ProductionDPTWareInventoryInfo)
                pdi = pdc.ProductionDPTWareInventory_GetList("F101", M_Code, Nothing)

                If pdi.Count > 0 Then
                    row("PO_Qty") = pdi(0).WI_Qty
                Else
                    row("PO_Qty") = 0
                End If

                row("PO_PM_Remark") = ""
                ds.Tables("ProductionOutProcess").Rows.Add(row)

            Next
        End If
        GridView1.MoveLast()
    End Sub

    '�[���[�u�n�D�H��
    '���L�{�Q�H�U�L�{�եΡG
    'AddRow()
    'LoadData()
    'RepositoryItemComboBox1_Enter()
    Function LoadProductionType(ByVal strCode As String, ByVal strType As String) As Boolean
        Dim ptc As New ProductionOutWardTypeControl
        Dim pti As List(Of ProductionOutWardTypeInfo)

        pti = ptc.ProductionOutWardType_GetList(Nothing, strCode, strType, Nothing, Nothing)
        RepositoryItemComboBox1.Items.Clear()

        If pti.Count = 0 Then
            MsgBox("��e���~���������إߥ[�u�n�D,�Х��K�[�I", 64, "����")
            LoadProductionType = False
            Exit Function
        Else
            LoadProductionType = True
            Dim i As Integer
            For i = 0 To pti.Count - 1
                RepositoryItemComboBox1.Items.Add(pti(i).OW_Do)
            Next
        End If


    End Function

    '�����k���桧�s�W��,�K�[���~�H��
    Private Sub MenuAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuAdd.Click
        tempCode = ""
        tempValue2 = "�~�o��޲z"
        tempValue3 = "�o��"
        frmProductionSelect.ShowDialog()
        '�W�[�O��
        If tempCode = "" Then
            Exit Sub
        Else
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempCode, ",")      '��r�Ŧ���ζ�R��Ʋդ�
            n = Len(Replace(tempCode, ",", "," & "*")) - Len(tempCode)      '�p��Ʋժ���
            For i = 0 To n
                If arr(i) = "" Then
                    Exit Sub
                End If
                AddRow(arr(i))      '�եβK�[�ƾڹL�{
            Next

        End If
        tempCode = ""
    End Sub

    '�����������s�A�h�X����
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    '�����T�w���s,�O�s�ƾ�
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim i, j, k%

        If gluS_SupplierName.Text.Trim = "" Then
            MsgBox("�~�o�����Ӥ��ର�šA�п�ܥ~�o������!", 64, "����")
            gluS_SupplierName.Focus()
            Exit Sub
        ElseIf txtPO_UserName.Text.Trim = "" Then
            MsgBox("�o�f�H���ର�šA�п�J�o�f�H!", 64, "����")
            txtPO_UserName.Focus()
            Exit Sub
        ElseIf dtePO_OutDate.Text.Trim = "" Then
            MsgBox("�o�f������ର�šA�п�J�o�f���!", 64, "����")
            dtePO_OutDate.Focus()
            Exit Sub
        End If

        For i = 0 To GridView1.RowCount - 1
            '��J�ƶq����p�󵥩�0
            If ds.Tables("ProductionOutProcess").Rows(i)("PO_Qty") <= 0 Then
                MsgBox("�ƶq��J���~�A�п�J����Ƽƶq!", 64, "����")
                Grid.Focus()
                GridView1.FocusedRowHandle = i
                Exit Sub
            End If

            Dim pdi As List(Of ProductionDPTWareInventoryInfo)
            Dim intPO_Qty As Integer

            pdi = pdc.ProductionDPTWareInventory_GetList("F101", ds.Tables("ProductionOutProcess").Rows(i)("PS_NO"), Nothing)

            If pdi.Count <= 0 Then
                MsgBox("�u�Ǥ��s�b!", 64, "����")
                Exit Sub
            Else
                '@2012/7/2 �ק�@�P�@�i�椤���\�s�b�ۦP�u�ǡA���[�u�n�D����ۦP
                intPO_Qty = ds.Tables("ProductionOutProcess").Rows(i)("PO_Qty")
                For k = 0 To GridView1.RowCount - 1
                    If k <> i Then
                        If ds.Tables("ProductionOutProcess").Rows(i)("PS_NO") = ds.Tables("ProductionOutProcess").Rows(k)("PS_NO") And ds.Tables("ProductionOutProcess").Rows(i)("OW_DO") = ds.Tables("ProductionOutProcess").Rows(k)("OW_DO") Then
                            MsgBox("�P�@�i�椤����s�b�u�ǬۦP�B�[�u�n�D�]�ۦP���O���I", 64, "����")
                            GridView1.FocusedRowHandle = i
                            Exit Sub
                        End If

                        ' ''�֥[�ۦP�u�Ǫ��~�o�ƶq
                        ''If ds.Tables("ProductionOutProcess").Rows(i)("PS_NO") = ds.Tables("ProductionOutProcess").Rows(k)("PS_NO") Then
                        ''    intPO_Qty = intPO_Qty + ds.Tables("ProductionOutProcess").Rows(k)("PO_Qty")
                        ''End If

                        ' ''�ۦP�u�ǧڵo�`�ƶq����j��w�s��
                        ''If pdi(0).WI_Qty < intPO_Qty Then
                        ''    MsgBox("�O��" & ds.Tables("ProductionOutProcess").Rows(i)("PM_M_Code") & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PM_Type") _
                        ''     & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PS_Name") & vbCrLf & "��J�ƶq�j��w�s�ƶq�A����~�o!", 64, "����")
                        ''    GridView1.FocusedRowHandle = i
                        ''    Exit Sub
                        ''End If

                    End If
                Next
            End If
        Next

        Select Case Microsoft.VisualBasic.Right(lblTittle.Text, 2)
            Case "�s�W"
                If DataNew() = True Then        '�եηs�W�ƾڨ��
                    MsgBox("�K�[�O�����\!", 64, "����")
                    Me.Close()
                End If
            Case "�ק�"
                If DataEdit() = True Then       '�եέק�ƾڨ��
                    MsgBox("�ק�O�����\!", 64, "����")
                    Me.Close()
                End If
            Case "�f��"
                Dim poi As New ProductionOutProcessInfo
                poi.PO_ID = txtPO_ID.Text
                poi.PO_Check = True
                poi.PO_CheckUserID = InUserID
                poi.PO_CheckDate = Format(Now, "yyyy/MM/dd")
                poi.PO_CheckRemark = txtPO_CheckRemark.Text.Trim

                If poc.ProductionOutProcess_Check(poi) = True Then      '�P�_�f�֫H���O�_��s����
                    Dim pdi As List(Of ProductionDPTWareInventoryInfo)
                    Dim pdiUpdate As New ProductionDPTWareInventoryInfo

                    '����w�s�ƶq
                    For j = 0 To GridView1.RowCount - 1
                        pdi = pdc.ProductionDPTWareInventory_GetList("F101", ds.Tables("ProductionOutProcess").Rows(j)("PS_NO"), Nothing)

                        If pdi.Count > 0 Then
                            pdiUpdate.M_Code = ds.Tables("ProductionOutProcess").Rows(j)("PS_NO")
                            pdiUpdate.DPT_ID = "F101"
                            pdiUpdate.WI_Qty = pdi(0).WI_Qty - ds.Tables("ProductionOutProcess").Rows(j)("PO_Qty")
                            If pdc.UpdateProductionField_Qty(pdiUpdate) = False Then
                                MsgBox("����w�s�ƶq����!", 64, "����")
                                Exit Sub
                            End If
                        End If
                    Next

                    MsgBox("�f�֧���!", 64, "����")
                    Me.Close()
                Else
                    MsgBox("�f�֥���!", 64, "����")
                End If
        End Select
    End Sub

    ''' <summary>
    ''' �۰ʥ~�o�[�u�渹
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' DataNew()
    ''' frmProductionOutProcess_Load()
    Function GetID() As String
        Dim str As String = ""
        If strPO_Type = "�~�o���" Then
            str = "PR" & CStr(Format(Now, "yyMM"))
        ElseIf strPO_Type = "�~�o�[�u" Then
            str = "PO" & CStr(Format(Now, "yyMM"))
        End If

        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, str, strPO_Type, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If poi.Count <= 0 Then
            GetID = str & "0001"
        Else
            GetID = str & Mid((CInt(Mid(poi(0).PO_ID, 7)) + 10001), 2)
        End If
    End Function

    ''' <summary>
    ''' �s�W�ƾ�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' cmdOK_Click()
    Function DataNew() As Boolean
        Dim poi As New ProductionOutProcessInfo
        Dim i As Integer

        Try
            Dim popi As List(Of ProductionOutProcessInfo)
            popi = poc.ProductionOutProcess_GetList(Nothing, txtPO_ID.Text.Trim, strPO_Type, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            '�P�_�~�o�[�u�渹�O�_�w�s�b
            If popi.Count > 0 Then
                MsgBox(strPO_Type & "�渹�w�s�b�A�ݭ��s�ͦ��A�нT�w���s�ͦ�" & strPO_Type & "�渹!", 64, "����")
                txtPO_ID.Text = GetID()     '���s�ͦ��~�o�[�u�渹
                MsgBox(strPO_Type & "�渹�w���s�ͦ��A�нT�w�O�s�H��!", 64, "����")
            End If

            poi.PO_ID = txtPO_ID.Text.Trim
            poi.PO_Type = strPO_Type
            poi.S_Supplier = gluS_SupplierName.EditValue
            poi.PO_UserName = txtPO_UserName.Text.Trim
            poi.PO_OutDate = dtePO_OutDate.EditValue

            poi.PO_Remark = txtPO_Remark.Text.Trim
            poi.AddUserID = InUserID
            poi.AddDate = Now

            For i = 0 To ds.Tables("ProductionOutProcess").Rows.Count - 1
                poi.PM_M_Code = ds.Tables("ProductionOutProcess").Rows(i)("PM_M_Code")
                poi.PM_Type = ds.Tables("ProductionOutProcess").Rows(i)("PM_Type")
                poi.PS_NO = ds.Tables("ProductionOutProcess").Rows(i)("PS_NO")
                poi.PS_Name = ds.Tables("ProductionOutProcess").Rows(i)("PS_Name")
                poi.OW_Do = ds.Tables("ProductionOutProcess").Rows(i)("OW_Do")

                poi.PO_ProAttribute = ds.Tables("ProductionOutProcess").Rows(i)("PO_ProAttribute")
                poi.PO_Qty = ds.Tables("ProductionOutProcess").Rows(i)("PO_Qty")
                poi.PO_PM_Remark = ds.Tables("ProductionOutProcess").Rows(i)("PO_PM_Remark")

                If poc.ProductionOutProcess_Add(poi) = False Then   '�P�_�ƾڬO�_�K�[����
                    MsgBox("�O��" & ds.Tables("ProductionOutProcess").Rows(i)("PM_M_Code") & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PM_Type") _
                     & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PS_Name") & "�K�[����!", 64, "����")
                    DataNew = False
                    Exit Function
                Else
                    DataNew = True
                End If
            Next
        Catch ex As Exception
            DataNew = False
            MsgBox("�K�[�O������!" & vbCrLf & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' �ק�ƾ�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' cmdOK_Click()
    Function DataEdit() As Boolean
        Dim poi As New ProductionOutProcessInfo
        Dim i, j As Integer

        Try
            '�P�_�R�����O�_���ݭn�R�����H���A���h�R���ƾڮw�������H��
            If ds.Tables("DelOutProcess").Rows.Count > 0 Then
                For j = 0 To ds.Tables("DelOutProcess").Rows.Count - 1
                    poc.ProductionOutProcess_Delete(ds.Tables("DelOutProcess").Rows(i)("AutoID"), Nothing)
                Next
            End If

            poi.PO_ID = txtPO_ID.Text.Trim
            poi.PO_Type = strPO_Type
            poi.S_Supplier = gluS_SupplierName.EditValue
            poi.PO_UserName = txtPO_UserName.Text.Trim
            poi.PO_OutDate = dtePO_OutDate.DateTime

            poi.PO_Remark = txtPO_Remark.Text.Trim
            poi.ModifyUserID = InUserID
            poi.ModifyDate = Now

            For i = 0 To ds.Tables("ProductionOutProcess").Rows.Count - 1
                '�P�_�ק襤�O�_���s�W
                If IsDBNull(ds.Tables("ProductionOutProcess").Rows(i)("AutoID")) Then
                    poi.PM_M_Code = ds.Tables("ProductionOutProcess").Rows(i)("PM_M_Code")
                    poi.PM_Type = ds.Tables("ProductionOutProcess").Rows(i)("PM_Type")
                    poi.PS_NO = ds.Tables("ProductionOutProcess").Rows(i)("PS_NO")
                    poi.PS_Name = ds.Tables("ProductionOutProcess").Rows(i)("PS_Name")
                    poi.OW_Do = ds.Tables("ProductionOutProcess").Rows(i)("OW_Do")

                    poi.PO_ProAttribute = ds.Tables("ProductionOutProcess").Rows(i)("PO_ProAttribute")
                    poi.PO_Qty = ds.Tables("ProductionOutProcess").Rows(i)("PO_Qty")
                    poi.PO_NoSendQty = ds.Tables("ProductionOutProcess").Rows(i)("PO_Qty")
                    poi.PO_PM_Remark = ds.Tables("ProductionOutProcess").Rows(i)("PO_PM_Remark")
                    poi.AddUserID = InUserID

                    poi.AddDate = Now

                    If poc.ProductionOutProcess_Add(poi) = False Then
                        MsgBox("�O��" & ds.Tables("ProductionOutProcess").Rows(i)("PM_M_Code") & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PM_Type") _
                         & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PS_Name") & "�K�[����!", 64, "����")
                        DataEdit = False
                        Exit Function
                    Else
                        DataEdit = True
                    End If
                Else
                    poi.AutoID = ds.Tables("ProductionOutProcess").Rows(i)("AutoID")
                    poi.PM_M_Code = ds.Tables("ProductionOutProcess").Rows(i)("PM_M_Code")
                    poi.PM_Type = ds.Tables("ProductionOutProcess").Rows(i)("PM_Type")
                    poi.PS_NO = ds.Tables("ProductionOutProcess").Rows(i)("PS_NO")
                    poi.PS_Name = ds.Tables("ProductionOutProcess").Rows(i)("PS_Name")

                    poi.OW_Do = ds.Tables("ProductionOutProcess").Rows(i)("OW_Do")
                    poi.PO_ProAttribute = ds.Tables("ProductionOutProcess").Rows(i)("PO_ProAttribute")
                    poi.PO_Qty = ds.Tables("ProductionOutProcess").Rows(i)("PO_Qty")
                    poi.PO_NoSendQty = ds.Tables("ProductionOutProcess").Rows(i)("PO_Qty")
                    poi.PO_PM_Remark = ds.Tables("ProductionOutProcess").Rows(i)("PO_PM_Remark")

                    If poc.ProductionOutProcess_Update(poi) = False Then
                        MsgBox("�O��" & ds.Tables("ProductionOutProcess").Rows(i)("PM_M_Code") & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PM_Type") _
                         & "/" & ds.Tables("ProductionOutProcess").Rows(i)("PS_Name") & "�ק異��!", 64, "����")
                        DataEdit = False
                        Exit Function
                    Else
                        DataEdit = True
                    End If
                End If

            Next
        Catch ex As Exception
            DataEdit = False
            MsgBox("�K�[�O������!" & vbCrLf & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' �[���ƾ�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' frmProductionOutProcess_Load()
    Sub LoadData()
        Dim i%
        Dim row As DataRow
        Dim poi As List(Of ProductionOutProcessInfo)
        poi = poc.ProductionOutProcess_GetList(Nothing, txtPO_ID.Text, strPO_Type, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '�P�_�ƾڮw���O�_�s�b���~�o�[�u��(�~�o�[�u�榳�i��Q�H�R����)
        If poi.Count <= 0 Then
            MsgBox("�~�o�[�u��:" & txtPO_ID.Text & "�w�Q�R��!", 64, "����")
            Exit Sub
        End If

        gluS_SupplierName.EditValue = poi(0).S_Supplier
        txtPO_UserName.Text = poi(0).PO_UserName
        dtePO_OutDate.DateTime = poi(0).PO_OutDate
        txtPO_Remark.Text = poi(0).PO_Remark
        chkPO_Check.Checked = poi(0).PO_Check

        lblPO_CheckUserName.Text = poi(0).PO_CheckUserName
        lblPO_CheckDate.Text = poi(0).PO_CheckDate
        txtPO_CheckRemark.Text = poi(0).PO_CheckRemark

        ds.Tables("ProductionOutProcess").Clear()       '�M�Ū�
        For i = 0 To poi.Count - 1

            row = ds.Tables("ProductionOutProcess").NewRow

            row("AutoID") = poi(i).AutoID
            row("PM_M_Code") = poi(i).PM_M_Code
            row("PM_Type") = poi(i).PM_Type
            row("PS_No") = poi(i).PS_NO
            row("PS_Name") = poi(i).PS_Name
            row("PM_JiYu") = poi(i).PM_JiYu

            LoadProductionType(poi(i).PM_M_Code, poi(i).PM_Type)
            If strPO_Type = "�~�o���" Then
                RepositoryItemComboBox2.Items.Clear()
                RepositoryItemComboBox2.Items.Add("���")
                RepositoryItemComboBox2.Items.Add("���-���")
                RepositoryItemComboBox2.Items.Add("���-��q")
            End If
            row("OW_Do") = poi(i).OW_Do
            row("PO_ProAttribute") = poi(i).PO_ProAttribute
            row("PO_Qty") = poi(i).PO_Qty
            row("PO_PM_Remark") = poi(i).PO_PM_Remark

            ds.Tables("ProductionOutProcess").Rows.Add(row)
        Next
    End Sub

    ''' <summary>
    ''' �����k�䡧�R�������A�R����椤�ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuDel.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "AutoID")

        If DelTemp = "AutoID" Then
        Else
            '�b�R�����W�[�Q�R�����O���A�H�K�T�w�ɧR���ƾڮw���O��
            Dim row As DataRow = ds.Tables("DelOutProcess").NewRow

            row("AutoID") = ds.Tables("ProductionOutProcess").Rows(GridView1.FocusedRowHandle)("AutoID")

            ds.Tables("DelOutProcess").Rows.Add(row)
        End If
        ds.Tables("ProductionOutProcess").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    ''' <summary>
    ''' �T�{�_��حȧ���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkPO_Check_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPO_Check.CheckedChanged
        cmdOK.Enabled = Not cmdOK.Enabled       '�_��سQ�襤�ɡA�T�w���s�~�i��
    End Sub

    '���Ů�����ܥ~�o�����ӿﶵ
    Private Sub gluS_SupplierName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluS_SupplierName.KeyDown
        If e.KeyCode = Keys.Space Then
            gluS_SupplierName.ShowPopup()
        End If
    End Sub

    'Private Sub RepositoryItemComboBox1_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemComboBox1.EditValueChanged
    '    Dim i%
    '    Dim strPS_NO, strOW_DO As String
    '    strPS_NO = GridView1.GetFocusedRowCellValue("PS_NO").ToString
    '    strOW_DO = GridView1.GetFocusedRowCellValue("OW_DO").ToString
    '    For i = 0 To GridView1.RowCount - 1
    '        If GridView1.FocusedRowHandle <> i Then
    '            If ds.Tables("ProductionOutProcess").Rows(i)("PS_NO") = strPS_NO And ds.Tables("ProductionOutProcess").Rows(i)("OW_DO") = strOW_DO Then
    '                MsgBox("�P�@�i�椤����s�b�ۦP�u�ǥB�[�u�n�D�]�ۦP���O���I", 64, "����")
    '            End If
    '        End If
    '    Next
    'End Sub

    '�[�u�n�D�C�����o�J�I�ɡA���s�[������
    Private Sub RepositoryItemComboBox1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemComboBox1.Enter
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        Dim strA, strB As String

        strA = GridView1.GetFocusedRowCellValue("PM_M_Code")
        strB = GridView1.GetFocusedRowCellValue("PM_Type")

        If strPM_M_Code <> strA Or strPM_Type <> strB Then
            strPM_M_Code = strA
            strPM_Type = strB
            LoadProductionType(strA, strB)
        End If
    End Sub

    '���Ů�����ܥ[�u�n�D/�[�u�ݩʿﶵ
    Private Sub RepositoryItemComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RepositoryItemComboBox1.KeyDown, RepositoryItemComboBox2.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.showpopup()
        End If
    End Sub

End Class