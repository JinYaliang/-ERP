Imports LFERP.DataSetting
Imports LFERP.Library.Outwards
Imports LFERP.Library.Orders
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Product

Public Class frmOutWards
    Dim owc As New OutWardsController
    Dim osc As New OrdersSubController
    Dim pmc As New ProcessMainControl
    Dim pic As New ProductInventoryController
    Dim ds As New DataSet
    Dim isPressEnter As Boolean         '�O���O�_���U�^����
    Dim LoadBZ As String
    Dim StrgluOM_CusterID As String

    Private Sub frmOutWards_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mtd As New CusterControler
        gluOM_CusterID.Properties.DisplayMember = "C_CusterID"
        gluOM_CusterID.Properties.ValueMember = "C_CusterID"
        gluOM_CusterID.Properties.DataSource = mtd.GetCusterList(Nothing, Nothing, Nothing)

        isPressEnter = False

        CreateTable()      '�եγЫت�L�{

        Select Case Me.lblTittle.Text
            Case "�e�f--�s�W"
                dteOW_Date.EditValue = Format(Now, "yyyy/MM/dd")
                txtOW_NO.Text = GetOW_NO()       '�եΥͦ��e�f�渹���
            Case "�e�f--�ק�"
                LoadData()                                          '�եΥ[���ƾڹL�{

            Case "�e�f--�d��"
                btnOK.Enabled = False
                LoadData()                                          '�եΥ[���ƾڹL�{

            Case "�e�f--�f��"
                LoadData()                                          '�եΥ[���ƾڹL�{
                lblOW_CheckUserName.Text = InUser
                lblOW_CheckDate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
                Panel1.Visible = True

                cboOW_Detail.Enabled = False
                dteOW_Date.Enabled = False
                gluOM_CusterID.Enabled = False
                txtOW_Address.Enabled = False
                GridView1.OptionsBehavior.Editable = False
                btnOK.Enabled = False
        End Select

        LoadBZ = "Y"

    End Sub
    ''' <summary>
    ''' �Ыت�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' frmOutWards_Load()
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("OutWards")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("OM_ID", GetType(String))
            .Columns.Add("OS_BatchID", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("OS_Plate", GetType(String))
            .Columns.Add("OW_Qty", GetType(Integer))
            .Columns.Add("OS_Sprace", GetType(Single))
            .Columns.Add("OS_NoSendQty", GetType(Integer))
            .Columns.Add("PI_Qty", GetType(Integer))
            .Columns.Add("OW_Remark", GetType(String))
            .Columns.Add("OW_Sprace", GetType(Int32))
        End With
        Grid.DataSource = ds.Tables("OutWards")

        '�ЫاR���ƾڪ�
        With ds.Tables.Add("DelData")
            .Columns.Add("AutoID", GetType(String))
        End With

        With ds.Tables.Add("Out_PM_Type")
            .Columns.Add("M_Name", GetType(String))
        End With
        GridControl1.DataSource = ds.Tables("Out_PM_Type")
    End Sub
    ''' <summary>
    '''  '�ͦ��e�f�渹
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    '''  frmOutWards_Load()
    ''' btnOK_Click()
    Function GetOW_NO() As String
        Dim owi As List(Of OutWardsInfo)
        Dim strOW_NO As String

        strOW_NO = "OW" & Format(Now, "yyMM")
        owi = owc.OutWards_GetList(strOW_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If owi.Count <= 0 Then
            GetOW_NO = strOW_NO & "001"
        Else
            GetOW_NO = strOW_NO & Mid((CInt(Mid(owi(0).OW_NO, 7)) + 1001), 2)
        End If

    End Function
    ''' <summary>
    ''' �[���ƾ�
    ''' </summary>
    ''' ���L�{�Q�H�U�L�{�եΡG
    '''  frmOutWards_Load()
    Sub LoadData()
        Dim owi As List(Of OutWardsInfo)
        Dim i%

        owi = owc.OutWards_GetList(txtOW_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If owi.Count > 0 Then      '�P�_�渹�O�_�s�b
            gluOM_CusterID.EditValue = owi(0).OM_CusterID
            txtOW_Address.Text = owi(0).OW_Address
            cboOW_Detail.Text = owi(0).OW_Detail
            dteOW_Date.Text = Format(owi(0).OW_Date, "yyyy/MM/dd")

            If owi(0).OW_Check = True Then              '�P�_�渹�O�_�w�f��
                chkOW_Check.Checked = True
                lblOW_CheckUserName.Text = owi(0).OW_CheckUserName
                lblOW_CheckDate.Text = owi(0).OW_CheckDate
                Panel1.Visible = True                              '��ܼf�֫H��
            End If

            ds.Tables("OutWards").Clear()

            For i = 0 To owi.Count - 1
                Dim row As DataRow
                row = ds.Tables("OutWards").NewRow

                row("AutoID") = owi(i).AutoID
                row("OM_ID") = owi(i).OM_ID
                row("OS_BatchID") = owi(i).OS_BatchID
                row("PM_M_Code") = owi(i).PM_M_Code
                row("M_Code") = owi(i).M_Code

                row("PM_Type") = owi(i).PM_Type
                row("OS_Plate") = owi(i).OS_Plate
                row("OW_Qty") = owi(i).OW_Qty
                row("OS_Sprace") = owi(i).OS_Sprace
                row("OW_Sprace") = owi(i).OW_Sprace
                row("OS_NoSendQty") = owi(i).OS_NoSendQty

                'row("PI_Qty") = owi(i).PI_Qty
                row("OW_Remark") = owi(i).OW_Remark

                ''-------------2013-7-9------------------------------------------------------------
                Dim DoubleQty As Double = 0
                Dim wi1 As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetList(owi(i).PM_M_Code, "W2201")
                If wi1.Count <= 0 Then
                Else
                    DoubleQty = wi1(0).WI_Qty
                End If

                row("PI_Qty") = DoubleQty
                ''-------------------------------------------------------------------------

                ds.Tables("OutWards").Rows.Add(row)
            Next
        End If
    End Sub
    ''' <summary>
    ''' �O�s�s�W�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' btnOK_Click()
    Function AddData() As Boolean
        Dim owi As New OutWardsInfo
        Dim i%

        owi.OW_NO = txtOW_NO.Text
        owi.WH_ID = "W2201"
        owi.OW_Address = txtOW_Address.Text.Trim
        owi.OW_Detail = cboOW_Detail.Text
        owi.OW_Date = dteOW_Date.Text
        owi.OW_AddUserID = InUserID
        owi.OW_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

        For i = 0 To ds.Tables("OutWards").Rows.Count - 1
            owi.OS_BatchID = ds.Tables("OutWards").Rows(i)("OS_BatchID").ToString
            owi.OM_ID = ds.Tables("OutWards").Rows(i)("OM_ID").ToString
            owi.OW_Remark = ds.Tables("OutWards").Rows(i)("OW_Remark").ToString
            owi.PM_M_Code = ds.Tables("OutWards").Rows(i)("PM_M_Code").ToString
            owi.M_Code = ds.Tables("OutWards").Rows(i)("M_Code").ToString
            owi.PM_Type = ds.Tables("OutWards").Rows(i)("PM_Type").ToString
            owi.OW_Qty = ds.Tables("OutWards").Rows(i)("OW_Qty")
            owi.OW_Sprace = ds.Tables("OutWards").Rows(i)("OW_Sprace")

            If owc.OutWards_Add(owi) = False Then
                MsgBox("�妸�渹���G " & ds.Tables("OutWards").Rows(i)("OS_BatchID").ToString & "  ���O���s�W���ѡI", 64, "����")
                AddData = False
                Exit Function
            Else
                AddData = True
            End If
        Next
    End Function
    ''' <summary>
    ''' �O�s�ק�ƾ�
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' btnOK_Click()
    Function EditData() As Boolean
        Dim owi As New OutWardsInfo
        Dim i, j%

        '�R���ק襤�R�����ƾ�
        For j = 0 To ds.Tables("DelData").Rows.Count - 1
            If IsDBNull(ds.Tables("DelData").Rows(j)("AutoID").ToString) = False Then                    '�u�R���ƾڮw���s�b���ƾ�
                owc.OutWards_Delete(ds.Tables("DelData").Rows(j)("AutoID").ToString, Nothing)
            End If
        Next

        owi.OW_NO = txtOW_NO.Text
        owi.WH_ID = "W2201"
        owi.OW_Address = txtOW_Address.Text.Trim
        owi.OW_Detail = cboOW_Detail.Text
        owi.OW_Date = dteOW_Date.Text
        owi.OW_AddUserID = InUserID
        owi.OW_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        owi.OW_ModifyUserID = InUserID
        owi.OW_ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

        For i = 0 To ds.Tables("OutWards").Rows.Count - 1

            owi.AutoID = ds.Tables("OutWards").Rows(i)("AutoID").ToString
            owi.OS_BatchID = ds.Tables("OutWards").Rows(i)("OS_BatchID").ToString
            owi.OM_ID = ds.Tables("OutWards").Rows(i)("OM_ID").ToString
            owi.OW_Remark = ds.Tables("OutWards").Rows(i)("OW_Remark").ToString
            owi.PM_M_Code = ds.Tables("OutWards").Rows(i)("PM_M_Code").ToString
            owi.M_Code = ds.Tables("OutWards").Rows(i)("M_Code").ToString
            owi.PM_Type = ds.Tables("OutWards").Rows(i)("PM_Type").ToString
            owi.OW_Qty = ds.Tables("OutWards").Rows(i)("OW_Qty")

            owi.OW_Sprace = ds.Tables("OutWards").Rows(i)("OW_Sprace")

            If IsDBNull(ds.Tables("OutWards").Rows(i)("AutoID")) = True Then         '�P�_�O�_�O�ק襤�s�W���ƾ�
                If owc.OutWards_Add(owi) = False Then
                    MsgBox("�妸�渹���G " & ds.Tables("OutWards").Rows(i)("OS_BatchID").ToString & "  ���O���s�W���ѡI", 64, "����")
                    EditData = False
                    Exit Function
                End If
            Else
                If owc.OutWards_Update(owi) = False Then
                    MsgBox("�妸�渹���G " & ds.Tables("OutWards").Rows(i)("OS_BatchID").ToString & "  ���O���ק異�ѡI", 64, "����")
                    EditData = False
                    Exit Function
                End If
            End If
        Next

        EditData = True
    End Function
    ''' <summary>
    ''' �O�s�f�֫H��
    ''' </summary>
    ''' <returns></returns>
    ''' ���L�{�Q�H�U�L�{�եΡG
    ''' btnOK_Click()
    Function UpdateCheck() As Boolean
        Try
            Dim owi As New OutWardsInfo
            Dim i%

            owi.OW_Check = chkOW_Check.Checked
            owi.OW_CheckUserID = InUserID
            owi.OW_CheckDate = lblOW_CheckDate.Text
            For i = 0 To ds.Tables("OutWards").Rows.Count - 1
                owi.AutoID = ds.Tables("OutWards").Rows(i)("AutoID").ToString
                If owc.OutWards_UpdateCheck(owi) = False Then
                    UpdateCheck = False
                    Exit Function
                Else
                    UpdateCheck = True
                End If
            Next
        Catch ex As Exception
            UpdateCheck = False
        End Try

    End Function
    Sub LoadPM_Type()
        Dim j%
        Dim pmi As List(Of ProcessMainInfo)
        If IsDBNull(ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code")) Then Exit Sub

        pmi = pmc.ProcessMain_GetList1(Nothing, ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code"), Nothing, ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("M_Code"))
        rcboPM_Type.Items.Clear()
        rcboPM_Type.Items.Add(ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code"))
        If pmi.Count > 0 Then
            For j = 0 To pmi.Count - 1
                rcboPM_Type.Items.Add(pmi(j).M_Name)
                'rcboPM_Type.Items.Add("SSS")
            Next
        End If

        'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_Type") = rcboPM_Type.Items.Item(0)
    End Sub
    ''' <summary>
    ''' �妸�s�����奻�ؤ����U�^����A�[�������ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rtxtOS_BatchID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles rtxtOS_BatchID.KeyDown
        'Dim i%

        'If e.KeyCode = Keys.Enter Then
        '    If gluOM_CusterID.Text.Trim = "" Or gluOM_CusterID.Text Is Nothing Then Exit Sub
        '    If sender.text = "" Then Exit Sub

        '    isPressEnter = True

        '    Dim osi As New OrdersSubInfo
        '    osi = osc.OrdersSub_Get(sender.text)
        '    If osi Is Nothing Then
        '        MsgBox("���妸�渹���s�b�A�Э��s��J�I", 64, "����")
        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        '        sender.text = ""
        '        Exit Sub
        '    Else
        '        If osi.OS_Check = False Then
        '            MsgBox("���妸�渹�|���f�֡A����X�f�I", 64, "����")
        '            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        '            sender.text = ""
        '            Exit Sub
        '        End If
        '        If osi.OM_CusterID <> gluOM_CusterID.EditValue Then
        '            MsgBox("���妸�渹���ݤ_�w��w�Ȥ�A�u���J�ݤ_�w��w�Ȥ᪺�妸�渹�I", 64, "����")
        '            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        '            sender.text = ""
        '            Exit Sub
        '        End If
        '        If osi.OS_NoSendQty <= 0 Then
        '            MsgBox("���妸�渹�w�����e�f�A����A�e�f�I", 64, "����")
        '            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        '            sender.text = ""
        '            Exit Sub
        '        End If

        '        For i = 0 To ds.Tables("OutWards").Rows.Count - 1
        '            If i <> GridView1.FocusedRowHandle Then
        '                If IsDBNull(ds.Tables("OutWards").Rows(i)("OS_BatchID")) = False Then
        '                    If ds.Tables("OutWards").Rows(i)("OS_BatchID") = sender.text Then
        '                        MsgBox("�P�@�i�X�f�椤����s�b�ۦP���妸�渹�I", 64, "����")
        '                        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        '                        sender.text = ""
        '                        Exit Sub
        '                    End If
        '                End If
        '            End If
        '        Next

        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OM_ID") = osi.OM_ID
        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = osi.PM_M_Code
        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("M_Code") = osi.M_Code
        '        'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_Type") = ""
        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_Plate") = osi.OS_Plate
        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_Sprace") = osi.OS_Sprace
        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_NoSendQty") = osi.OS_NoSendQty

        '        RepositoryItemPopupContainerEdit1_Enter(Nothing, Nothing)
        '        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_Type") = ds.Tables("Out_PM_Type").Rows(0)("M_Name").ToString

        '        '�[���w�s�ƾ�


        '        ''Dim pii As List(Of ProductInventoryInfo)
        '        ''pii = pic.ProductInventory_GetList(osi.PM_M_Code, osi.M_Code, "W1101", Nothing)
        '        ''If pii.Count > 0 Then
        '        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = pii(0).PI_Qty
        '        ''Else
        '        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = 0
        '        ''End If

        '        '�d�߬O�_�������ܮw������  2013-5-24
        '        Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
        '        Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
        '        wi1 = wc1.WareInventory_GetSub(osi.PM_M_Code, "W1101")
        '        If wi1 Is Nothing Then
        '            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = 0
        '        Else
        '            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = wi1.WI_Qty
        '        End If




        '    End If
        '    GridView1.SetFocusedRowCellValue(OS_BatchID, sender.text)
        'End If
    End Sub
    ''' <summary>
    ''' �����k���桧�s�W��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsAdd.Click
        Dim row As DataRow
        'Dim osi As List(Of OrdersSubInfo)
        'Dim i%

        If gluOM_CusterID.EditValue Is Nothing Then
            MsgBox("�п�J�Ȥ�N���I", 64, "����")
            gluOM_CusterID.Focus()
            Exit Sub
        End If

        row = ds.Tables("OutWards").NewRow

        row("OW_Qty") = 0
        row("OS_Sprace") = 0
        row("OW_Sprace") = 0

        ds.Tables("OutWards").Rows.Add(row)

        'rlueOS_BatchID.DisplayMember = "OS_BatchID"
        'rlueOS_BatchID.ValueMember = "OS_BatchID"
        'rlueOS_BatchID.DataSource = osc.OrdersSub_GetList3(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, gluOM_CusterID.EditValue, Nothing, Nothing, DateAdd(DateInterval.Day, -900, CDate(Format(Now, "yyyy/MM/dd"))), Nothing, "True")

        'osi = osc.OrdersSub_GetList3(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, gluOM_CusterID.EditValue, Nothing, Nothing, DateAdd(DateInterval.Day, -900, CDate(Format(Now, "yyyy/MM/dd"))), Nothing, "True")
        'If osi.Count > 0 Then
        '    For i = 0 To osi.Count - 1
        '        rcboOS_BatchID.Items.Add(osi(i).OS_BatchID)
        '    Next
        'End If

        '���Ĥ@�C��o�J�I
        GridView1.Focus()
        GridView1.FocusedColumn = OS_BatchID
    End Sub
    ''' <summary>
    ''' �����k���桧�R����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmsDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmsDelete.Click
        If ds.Tables("OutWards").Rows.Count = 0 Then Exit Sub
        '�b�R�����W�[�Q�R�����O���A�H�K�T�w�ɡA�b�ƾڮw���R�����O��
        Dim row As DataRow = ds.Tables("DelData").NewRow

        row("AutoID") = ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("AutoID")
        ds.Tables("DelData").Rows.Add(row)

        ds.Tables("OutWards").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub
    ''' <summary>
    ''' �����������s�A����������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCanecl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanecl.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' �ƪ`�奻�ؤ����U�Ů���A��ܤU�Ԥ奻��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemMemoExEdit1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RepositoryItemMemoExEdit1.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub
    ''' <summary>
    ''' �����T�w���s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim i, j, sum%

        If Me.lblTittle.Text <> "�e�f--�f��" Then
            If GridView1.RowCount <= 0 Then
                MsgBox("�мW�[�ƾګH���I", 64, "����")
                GridView1.Focus()
                Exit Sub
            End If
            If dteOW_Date.Text = "" Then
                MsgBox("�п�J�e�f����I", 64, "����")
                dteOW_Date.Focus()
                Exit Sub
            End If
            If txtOW_Address.Text.Trim = "" Then
                MsgBox("�п�J�e�f�a�I�I", 64, "����")
                txtOW_Address.Focus()
                Exit Sub
            End If

            For i = 0 To ds.Tables("OutWards").Rows.Count - 1
                If IsDBNull(ds.Tables("OutWards").Rows(i)("OS_BatchID")) = True Or ds.Tables("OutWards").Rows(i)("OS_BatchID") = "" Then
                    GridView1.FocusedRowHandle = i
                    MsgBox("�妸�渹���ର�šA�п�J�妸�渹�I", 64, "����")
                    Exit Sub
                End If
                If IsDBNull(ds.Tables("OutWards").Rows(i)("PM_M_Code")) = True Then
                    GridView1.FocusedRowHandle = i
                    MsgBox("���~�s�����ର�šA�п�J���~�s���I", 64, "����")
                    Exit Sub
                End If
                If IsDBNull(ds.Tables("OutWards").Rows(i)("PM_Type")) = True Then
                    GridView1.FocusedRowHandle = i
                    MsgBox("�t��W�٤��ର�šA�п�J�t��W�١I", 64, "����")
                    Exit Sub
                End If
                If ds.Tables("OutWards").Rows(i)("OW_Qty") <= 0 Then
                    GridView1.FocusedRowHandle = i
                    MsgBox("�e�f�ƶq����p�󵥩�s�A�Э��s��J�e�f�ƶq�I", 64, "����")
                    Exit Sub
                End If
                If ds.Tables("OutWards").Rows(i)("OW_Qty") > ds.Tables("OutWards").Rows(i)("OS_NoSendQty") Then
                    GridView1.FocusedRowHandle = i
                    MsgBox("�e�f�ƶq����j�󥼥�f�ƶq�A�Э��s��J�e�f�ƶq�I", 64, "����")
                    Exit Sub
                End If

                sum = ds.Tables("OutWards").Rows(i)("OW_Qty")
                For j = 0 To ds.Tables("OutWards").Rows.Count - 1
                    If i <> j Then
                        If ds.Tables("OutWards").Rows(i)("PM_M_Code").ToString = ds.Tables("OutWards").Rows(j)("PM_M_Code").ToString And ds.Tables("OutWards").Rows(i)("M_Code").ToString = ds.Tables("OutWards").Rows(j)("M_Code").ToString Then
                            sum = sum + ds.Tables("OutWards").Rows(j)("OW_Qty")
                        End If
                    End If
                Next

                '�Ȯɥh
                Dim DoubleQty As Double = 0
                Dim wi1 As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetList(ds.Tables("OutWards").Rows(i)("PM_M_Code").ToString, "W2201")
                If wi1.Count <= 0 Then
                Else
                    DoubleQty = wi1(0).WI_Qty
                End If

                If sum > DoubleQty Then
                    GridView1.FocusedRowHandle = i
                    MsgBox("�P�@�ڲ��~�e�f�`�ƶq����j��w�s�ƶq�A�Э��s��J�e�f�ƶq�I", 64, "����")
                    Exit Sub
                End If


            Next
        End If

        Select Case Me.lblTittle.Text
            Case "�e�f--�s�W"
                If owc.OutWards_GetList(txtOW_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing).Count > 0 Then
                    MsgBox("�e�f�渹�w�s�b�A�нT�w���s�ͦ��e�f�渹�I", 64, "����")
                    txtOW_NO.Text = GetOW_NO()
                    MsgBox("�w���s�ͦ��e�f�渹�A�Э��s�O�s�ƾڡI", 64, "����")
                    Exit Sub
                End If

                If AddData() = True Then
                    MsgBox("�ƾڷs�W���\�I", 64, "����")
                    Me.Close()
                End If
            Case "�e�f--�ק�"
                If EditData() = True Then
                    MsgBox("�ƾڭק令�\�I", 64, "����")
                    Me.Close()
                End If
            Case "�e�f--�f��"
                If UpdateCheck() = True Then
                    MsgBox("�f�֧����I", 64, "����")
                    Me.Close()
                End If
        End Select
    End Sub
    ''' <summary>
    ''' ��Ȥ�N�����ܦZ�A��w�Ȥ�N���A�����\�A�ק�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluOM_CusterID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluOM_CusterID.EditValueChanged
        If IsDBNull(gluOM_CusterID.EditValue) = True And LoadBZ <> "" Then Exit Sub

        If ds.Tables("OutWards").Rows.Count <= 0 Or StrgluOM_CusterID = "" Or (gluOM_CusterID.EditValue = StrgluOM_CusterID And StrgluOM_CusterID <> "") Then
            StrgluOM_CusterID = gluOM_CusterID.EditValue
        Else
            gluOM_CusterID.EditValue = StrgluOM_CusterID
            MsgBox("����ק�,�w�s�b�䥦�Ȥ�e�f�H��!")
            Exit Sub
        End If

        Dim mtd As New CusterControler
        Dim mtdl As New List(Of CusterInfo)
        mtdl = mtd.GetCusterList(gluOM_CusterID.EditValue, Nothing, Nothing)
        txtOW_Address.Text = mtdl(0).C_Adder1

        Dim osc As New OrdersSubController
        Me.GridControl2.DataSource = osc.OrdersSub_GetList4(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, gluOM_CusterID.EditValue, Nothing, Nothing, Nothing, Nothing, True)


        ' gluOM_CusterID.Enabled = False
    End Sub
    ''' <summary>
    ''' �T�{�L�~�_��ا��ܮɡA�T�w���s�]��۬�������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkOW_Check_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOW_Check.CheckedChanged
        If Me.lblTittle.Text = "�e�f--�f��" Then            '�u���f�֮ɧ@���]�m
            btnOK.Enabled = Not btnOK.Enabled
        End If
    End Sub
    ''' <summary>
    ''' �妸���s�奻�إ��h�J�I�ɡA�[�������ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rtxtOS_BatchID_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxtOS_BatchID.Leave
        ''Dim i%

        ''If isPressEnter = True Then       '�b�奻�ؤ����U�L�^����A�h�����榹�L�{
        ''    isPressEnter = False
        ''    Exit Sub
        ''End If

        ''If gluOM_CusterID.EditValue = "" Or gluOM_CusterID.EditValue Is Nothing Then Exit Sub

        ''Dim osi As New OrdersSubInfo
        ''osi = osc.OrdersSub_Get(sender.text)
        ''If osi Is Nothing Then          '�P�_�妸�O�_�s�b
        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        ''    'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_BatchID") = ""
        ''    sender.text = ""
        ''    Exit Sub
        ''Else
        ''    If osi.OS_Check = False Then               '�P�_�妸�O�_�w�f��
        ''        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        ''        'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_BatchID") = ""
        ''        sender.text = ""
        ''        Exit Sub
        ''    End If

        ''    If osi.OM_CusterID <> gluOM_CusterID.EditValue Then          '�P�_�妸�O�_�ݤ_��w�Ȥ�
        ''        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        ''        'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_BatchID") = ""
        ''        sender.text = ""
        ''        Exit Sub
        ''    End If

        ''    If osi.OS_NoSendQty <= 0 Then        '�P�_�妸����ƶq�O�_�p�󵥩�0�A�Y�O�h���妸����A�X�f
        ''        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        ''        'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_BatchID") = ""
        ''        sender.text = ""
        ''        Exit Sub
        ''    End If

        ''    '�P�_�O�_�s�b�ۦP�妸
        ''    For i = 0 To ds.Tables("OutWards").Rows.Count - 1
        ''        If i <> GridView1.FocusedRowHandle Then
        ''            If IsDBNull(ds.Tables("OutWards").Rows(i)("OS_BatchID")) = False Then
        ''                If ds.Tables("OutWards").Rows(i)("OS_BatchID") = sender.text Then
        ''                    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
        ''                    'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_BatchID") = ""
        ''                    sender.text = ""
        ''                    Exit Sub
        ''                End If
        ''            End If
        ''        End If
        ''    Next

        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OM_ID") = osi.OM_ID
        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = osi.PM_M_Code
        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("M_Code") = osi.M_Code
        ''    'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_Type") = ""
        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_Plate") = osi.OS_Plate
        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_Sprace") = osi.OS_Sprace
        ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_NoSendQty") = osi.OS_NoSendQty

        ''    'LoadPM_Type()

        ''    '�[���w�s�ƾ�
        ''    ''Dim pii As List(Of ProductInventoryInfo)
        ''    ''pii = pic.ProductInventory_GetList(osi.PM_M_Code, osi.M_Code, "W1101", Nothing)
        ''    ''If pii.Count > 0 Then
        ''    ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = pii(0).PI_Qty
        ''    ''Else
        ''    ''    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = 0
        ''    ''End If

        ''    '2013-5-24
        ''    Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
        ''    Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
        ''    wi1 = wc1.WareInventory_GetSub(osi.PM_M_Code, "W1101")
        ''    If wi1 Is Nothing Then
        ''        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = 0
        ''    Else
        ''        ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = wi1.WI_Qty
        ''    End If

        ''End If
    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles RepositoryItemPopupContainerEdit1.Enter
        Dim j%
        Dim pmi As List(Of ProcessMainInfo)
        Dim row As DataRow

        ds.Tables("Out_PM_Type").Clear()

        If IsDBNull(ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code")) Then Exit Sub

        pmi = pmc.ProcessMain_GetList1(Nothing, ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code"), "�Ͳ��[�u", ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("M_Code"))

        If pmi.Count > 0 Then
            For j = 0 To pmi.Count - 1
                row = ds.Tables("Out_PM_Type").NewRow
                row("M_Name") = pmi(j).Type3ID
                ds.Tables("Out_PM_Type").Rows.Add(row)
            Next
        Else
            row = ds.Tables("Out_PM_Type").NewRow
            row("M_Name") = ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code")
            ds.Tables("Out_PM_Type").Rows.Add(row)
        End If

    End Sub

    Private Sub GridView2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.Click
        GridView1.SetFocusedRowCellValue(PM_Type, GridView2.GetFocusedRowCellValue("M_Name"))
        GridView1.Focus()
    End Sub

    Private Sub gluOM_CusterID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gluOM_CusterID.KeyDown, cboOW_Detail.KeyDown
        If e.KeyCode = Keys.Space Then
            sender.ShowPopup()
        End If
    End Sub

    Private Sub GridView3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView3.Click
        Dim i%

        If GridView3.GetFocusedRowCellValue("OS_BatchID") Is Nothing Then
            Exit Sub
        End If


        Dim StrOS_Batch As String
        StrOS_Batch = GridView3.GetFocusedRowCellValue("OS_BatchID").ToString

        If gluOM_CusterID.Text.Trim = "" Or gluOM_CusterID.Text Is Nothing Then Exit Sub

        Dim osi As New OrdersSubInfo
        osi = osc.OrdersSub_Get(StrOS_Batch)
        If osi Is Nothing Then
            MsgBox("���妸�渹���s�b�A�Э��s��J�I", 64, "����")
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
            sender.text = ""
            Exit Sub
        Else
            If osi.OS_Check = False Then
                MsgBox("���妸�渹�|���f�֡A����X�f�I", 64, "����")
                ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing

                Exit Sub
            End If
            If osi.OM_CusterID <> gluOM_CusterID.EditValue Then
                MsgBox("���妸�渹���ݤ_�w��w�Ȥ�A�u���J�ݤ_�w��w�Ȥ᪺�妸�渹�I", 64, "����")
                ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing
                sender.text = ""
                Exit Sub
            End If
            If osi.OS_NoSendQty <= 0 Then
                MsgBox("���妸�渹�w�����e�f�A����A�e�f�I", 64, "����")
                ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing

                Exit Sub
            End If

            For i = 0 To ds.Tables("OutWards").Rows.Count - 1
                If i <> GridView1.FocusedRowHandle Then
                    If IsDBNull(ds.Tables("OutWards").Rows(i)("OS_BatchID")) = False Then
                        If ds.Tables("OutWards").Rows(i)("OS_BatchID") = StrOS_Batch Then
                            MsgBox("�P�@�i�X�f�椤����s�b�ۦP���妸�渹�I", 64, "����")
                            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = Nothing

                            Exit Sub
                        End If
                    End If
                End If
            Next

            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OM_ID") = osi.OM_ID
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_M_Code") = osi.PM_M_Code
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("M_Code") = osi.M_Code
            'ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_Type") = ""
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_Plate") = osi.OS_Plate
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_Sprace") = osi.OS_Sprace
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OW_Sprace") = osi.OS_Sprace
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("OS_NoSendQty") = osi.OS_NoSendQty

            RepositoryItemPopupContainerEdit1_Enter(Nothing, Nothing)
            ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PM_Type") = ds.Tables("Out_PM_Type").Rows(0)("M_Name").ToString

            '�d�߬O�_�������ܮw������  2013-5-24
            'Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            'Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            'wi1 = wc1.WareInventory_GetSub(osi.PM_M_Code, "W1101")
            'If wi1 Is Nothing Then
            '    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = 0
            'Else
            '    ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = wi1.WI_Qty
            'End If

            Dim wi1 As List(Of LFERP.Library.WareHouse.WareInventory.WareInventoryInfo)
            Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi1 = wc1.WareInventory_GetList(osi.PM_M_Code, "W2201")
            If wi1.Count <= 0 Then
                ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = 0
            Else
                ds.Tables("OutWards").Rows(GridView1.FocusedRowHandle)("PI_Qty") = wi1(0).WI_Qty
            End If

        End If
        GridView1.SetFocusedRowCellValue(OS_BatchID, StrOS_Batch)

    End Sub
End Class