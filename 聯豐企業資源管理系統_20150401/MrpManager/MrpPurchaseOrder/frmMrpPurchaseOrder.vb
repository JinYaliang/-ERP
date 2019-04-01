Imports LFERP.Library.MrpManager.MrpPurchaseOrder
Imports LFERP.Library.MrpManager.MrpPurchaseRecord
Imports LFERP.DataSetting
Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.MrpManager.MrpMaterialCode
Imports LFERP.Library.WareHouse

Public Class frmMrpPurchaseOrder

#Region "�r�q�ݩ�"
    Dim ds As New DataSet
    Dim mpoc As New MrpPurchaseOrderController
    Dim mpoec As New MrpPurchaseOrderEntryController
    Dim msuc As New SystemUserController
    Dim mprc As New MrpPurchaseRecordController
    Dim sc As New SuppliersControler
    Dim whc As New WareHouseController
    Dim mmcc As New MrpMaterialCodeController
    Dim DelAutoID As String = ""            '�O�����Ӫ�Q�R����AutoID
    Dim boCheck As Boolean = False         '�ΨӧP�_�f�֪��A�O�_������
    Private _EditItem As String
    Private _PO As String

    Property EditItem() As String '�ݩ�
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property PO() As String '�ݩ�
        Get
            Return _PO
        End Get
        Set(ByVal value As String)
            _PO = value
        End Set
    End Property
#End Region

#Region "�ЫءB��R�{�ɪ�"
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("PurchaseOrder")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("PO", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("Source", GetType(String))
            .Columns.Add("PurchaseQty", GetType(Decimal))
            .Columns.Add("UnitPrice", GetType(Decimal))
            .Columns.Add("NeedDate", GetType(Date))
            .Columns.Add("DeliveryDate", GetType(Date))
            .Columns.Add("Remarks", GetType(String))
            .Columns.Add("TotalPrice", GetType(String))
            '.Columns.Add("TotalPrice", GetType(String), "PurchaseQty*UnitPrice")
        End With
        GridPurchaseOrder.DataSource = ds.Tables("PurchaseOrder")
    End Sub

    Sub FillTable(ByVal infoList As Object)
        Try
            Dim row As DataRow
            Dim i As Integer
            Dim mpoi As New List(Of MrpPurchaseOrderEntryInfo)
            mpoi = infoList
            For i = 0 To mpoi.Count - 1
                row = ds.Tables("PurchaseOrder").NewRow
                row("AutoID") = mpoi(i).AutoID
                row("PO") = mpoi(i).PO
                row("M_Code") = mpoi(i).M_Code
                row("M_Name") = mpoi(i).M_Name
                row("M_Gauge") = mpoi(i).M_Gauge
                row("M_Unit") = mpoi(i).M_Unit
                row("Source") = mpoi(i).Source
                row("PurchaseQty") = mpoi(i).PurchaseQty
                row("UnitPrice") = mpoi(i).UnitPrice
                row("NeedDate") = mpoi(i).NeedDate
                row("DeliveryDate") = mpoi(i).DeliveryDate
                row("Remarks") = mpoi(i).Remarks
                row("TotalPrice") = row("PurchaseQty") * row("UnitPrice")
                ds.Tables("PurchaseOrder").Rows.Add(row)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "FillTable��k�X��")
        End Try
    End Sub
#End Region

#Region "����[��"
    Private Sub frmMrpPurchaseOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadDataToLookUpEdit()
        CreateTables()
        Select Case EditItem
            Case EditEnumType.ADD
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                Me.Text = lblTitle.Text
                txtPO.EditValue = "�۰ʽs��"
                dteRequestDate.EditValue = Now
                SetControlEnable(True, True)
                gluPR.Enabled = True
            Case EditEnumType.EDIT
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                Me.Text = lblTitle.Text
                SetControlEnable(True, True)
                LoadData(PO)
            Case EditEnumType.VIEW
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                Me.Text = lblTitle.Text
                SetControlEnable(False, False)
                LoadData(PO)
            Case EditEnumType.CHECK
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                Me.Text = lblTitle.Text
                SetControlEnable(False, True)
                XtraTabControl1.SelectedTabPage = xtpCheck
                LoadData(PO)
            Case EditEnumType.RECHECK
                lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.RECHECK)
                Me.Text = lblTitle.Text            
                SetControlEnable(False, True)
                XtraTabControl1.SelectedTabPage = xtpCheck
                LoadData(PO)
        End Select
    End Sub

    Private Sub LoadDataToLookUpEdit()   
        gluPR.Properties.DataSource = mprc.MrpPurchaseRecord_GetPRList()
        gluSupplier.Properties.DataSource = sc.Suppliers_GetDataTable(Nothing)
        gluDept.Properties.DataSource = mpoec.GetDeptInfo()
        gluUserID.Properties.DataSource = msuc.SystemUser_GetList(Nothing, Nothing, Nothing)
        gluWareHouse.Properties.DataSource = whc.WareHouse_GetDataTable(Nothing)

        If EditItem = EditEnumType.ADD Then
            GridMaterial.DataSource = mmcc.MrpMaterialCode_GetList(Nothing, Nothing, "True", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Else
            GridMaterial.DataSource = mmcc.MrpMaterialCode_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If
    End Sub

#End Region

#Region "�]�m����O�_�i��"
    Private Sub SetControlEnable(ByVal a As Boolean, ByVal b As Boolean)
        gluSupplier.Enabled = a
        gluDept.Enabled = a
        gluUserID.Enabled = a
        gluWareHouse.Enabled = a
        dteRequestDate.Enabled = a
        chkIsUrgency.Enabled = a
        txtRemarks.Enabled = a
        viewPurchaseOrder.OptionsBehavior.Editable = a
        xtpCheck.PageVisible = Not a
        chkCheck.Enabled = b
        txtCheckRemark.Enabled = b
        btnSave.Enabled = b

        If a = False Then
            GridPurchaseOrder.ContextMenuStrip = Nothing
        End If
    End Sub
#End Region

#Region "�[���ƾ�"
    Private Sub LoadData(ByVal PO As String)
        Dim objInfo As New MrpPurchaseOrderInfo
        Dim mpolist As New List(Of MrpPurchaseOrderEntryInfo)
        objInfo = mpoc.MrpPurchaseOrder_GetList(PO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)(0)
        txtPO.EditValue = objInfo.PO
        gluPR.EditValue = objInfo.PR
        gluDept.EditValue = objInfo.DeptID
        gluSupplier.EditValue = objInfo.SupplierID
        gluUserID.EditValue = objInfo.RequestUserID
        dteRequestDate.EditValue = objInfo.RequestDate
        txtRemarks.EditValue = objInfo.Remarks
        chkIsUrgency.Checked = objInfo.IsUrgency
        gluWareHouse.EditValue = objInfo.WareHouseID

        chkCheck.Checked = objInfo.CheckBit
        boCheck = chkCheck.Checked
        txtCheckRemark.EditValue = objInfo.CheckRemark
        If chkCheck.Checked = True Then
            lblCheckUserName.Text += objInfo.CheckUserName
            lblCheckDate.Text += Format(objInfo.CheckDate, "yyyy/MM/dd")
        Else
            lblCheckUserName.Text += InUser
            lblCheckDate.Text += Format(Now, "yyyy/MM/dd")
        End If
        If EditItem = EditEnumType.VIEW And objInfo.ReCheckBit = True Or EditItem = EditEnumType.RECHECK Then
            lblCheck.Text = "�_�f�G"
            xtpCheck.Text = "�_�f�H��"
            chkCheck.Checked = objInfo.ReCheckBit
            boCheck = chkCheck.Checked
            txtCheckRemark.EditValue = objInfo.ReCheckRemark
            If chkCheck.Checked = True Then
                lblCheckUserName.Text = "�_�֤H���G" + objInfo.ReCheckUserName
                lblCheckDate.Text = "�_�֤���G" + Format(objInfo.ReCheckDate, "yyyy/MM/dd")
            Else
                lblCheckUserName.Text = "�_�֤H���G" + InUser
                lblCheckDate.Text = "�_�֤���G" + Format(Now, "yyyy/MM/dd")
            End If
        End If
        mpolist = mpoec.MrpPurchaseOrderEntry_GetList(PO)
        If mpolist.Count > 0 Then
            FillTable(mpolist)
        End If
    End Sub
#End Region

#Region "�k�����ƥ�"
    Private Sub cms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPurOrderAdd.Click, cmsPurOrderDel.Click
        Select Case sender.Name
            Case "cmsPurOrderAdd"
                Dim row As DataRow
                row = ds.Tables("PurchaseOrder").NewRow
                row("AutoID") = 0
                row("PurchaseQty") = 0
                row("UnitPrice") = 0
                ds.Tables("PurchaseOrder").Rows.Add(row)
                viewPurchaseOrder.FocusedRowHandle = viewPurchaseOrder.RowCount - 1
            Case "cmsPurOrderDel"
                If (viewPurchaseOrder.GetFocusedRowCellValue("AutoID").ToString <> "0") Then
                    DelAutoID += viewPurchaseOrder.GetFocusedRowCellValue("AutoID").ToString + ","
                End If
                ds.Tables("PurchaseOrder").Rows.RemoveAt(viewPurchaseOrder.FocusedRowHandle)
                SetRightMenuEnable()
        End Select
    End Sub

    Private Sub GridPurchaseOrder_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridPurchaseOrder.MouseDown
        If e.Button() = Windows.Forms.MouseButtons.Right Then
            SetRightMenuEnable()
        End If
    End Sub

    Private Sub SetRightMenuEnable()
        If viewPurchaseOrder.RowCount < 1 Then
            cmsPurOrder.Items("cmsPurOrderDel").Enabled = False
        Else
            cmsPurOrder.Items("cmsPurOrderDel").Enabled = True
        End If
    End Sub
#End Region

#Region "���ƿ�ܨƥ�"
    Private Sub PopupControlSelect(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewMaterial.DoubleClick
        Dim i As Int32 = viewPurchaseOrder.FocusedRowHandle
        With ds.Tables("PurchaseOrder")
            .Rows(i)("M_Code") = ViewMaterial.GetFocusedRowCellValue("M_Code").ToString
            .Rows(i)("M_Name") = ViewMaterial.GetFocusedRowCellValue("M_Name").ToString
            .Rows(i)("M_Gauge") = ViewMaterial.GetFocusedRowCellValue("M_Gauge").ToString
            .Rows(i)("M_Unit") = ViewMaterial.GetFocusedRowCellValue("M_Unit").ToString
            .Rows(i)("Source") = ViewMaterial.GetFocusedRowCellValue("MC_Source").ToString
        End With
        pccMaterial.OwnerEdit.ClosePopup()
        btnSave.Focus()
    End Sub
#End Region

#Region "�O�s�B�h�X�ƥ�"

#Region "�O�s�e�ˬd��J�ƾڬO�_���T"
    Private Function CheckSave() As Boolean
        Dim bo As Boolean = False
        CheckSave = True
        If gluPR.EditValue = Nothing Then
            MsgBox("�п�ܪ��ʳ渹", MsgBoxStyle.Information, "����")
            gluPR.Focus()
            bo = True
        ElseIf gluSupplier.EditValue = Nothing Then
            MsgBox("�п�ܨ�����", MsgBoxStyle.Information, "����")
            gluSupplier.Focus()
            bo = True
        ElseIf gluDept.EditValue = Nothing Then
            MsgBox("�п�ܳ���", MsgBoxStyle.Information, "����")
            gluDept.Focus()
            bo = True
        ElseIf gluUserID.EditValue = Nothing Then
            MsgBox("�п�ܽ��ʤH��", MsgBoxStyle.Information, "����")
            gluUserID.Focus()
            bo = True
        ElseIf dteRequestDate.EditValue = Nothing Then
            MsgBox("�п�ܽ��ʤ��", MsgBoxStyle.Information, "����")
            dteRequestDate.Focus()
            bo = True
        ElseIf gluWareHouse.EditValue = Nothing Then
            MsgBox("�п�ܶi�f�ܮw", MsgBoxStyle.Information, "����")
            gluWareHouse.Focus()
            bo = True
        ElseIf ds.Tables("PurchaseOrder").Rows.Count < 1 Then
            MsgBox("���Ʃ��Ӭ��šA�L�k�O�s", MsgBoxStyle.Information, "����")
            GridPurchaseOrder.Focus()
            bo = True
        ElseIf EditItem = EditEnumType.CHECK And boCheck = chkCheck.Checked Then
            MsgBox("�f�֪��A�S���ܤơA�L�k�O�s", MsgBoxStyle.Information, "����")
            chkCheck.Focus()
            bo = True
        End If
        If bo = True Then
            CheckSave = False
            Exit Function
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("PurchaseOrder").Rows.Count - 1
            If IsDBNull(ds.Tables("PurchaseOrder").Rows(i)("M_Code")) Then
                MsgBox("�L�k�O�s�A���Ʃ��Ӥ��s�b���~�s�X���Ū���Ʀ�", MsgBoxStyle.Information, "����")
                bo = True
            ElseIf ds.Tables("PurchaseOrder").Rows(i)("PurchaseQty") = 0 Then
                MsgBox("�L�k�O�s�A���Ʃ��Ӥ��s�b���ʼƶq��0����Ʀ�", MsgBoxStyle.Information, "����")
                bo = True
            ElseIf ds.Tables("PurchaseOrder").Rows(i)("UnitPrice") = 0 Then
                MsgBox("�L�k�O�s�A���Ʃ��Ӥ��s�b�����0����Ʀ�", MsgBoxStyle.Information, "����")
                bo = True
            ElseIf IsDBNull(ds.Tables("PurchaseOrder").Rows(i)("NeedDate")) Then
                MsgBox("�L�k�O�s�A���Ʃ��Ӥ��s�b�ݨD������Ū���Ʀ�", MsgBoxStyle.Information, "����")
                bo = True
            ElseIf IsDBNull(ds.Tables("PurchaseOrder").Rows(i)("DeliveryDate")) Then
                MsgBox("�L�k�O�s�A���Ʃ��Ӥ��s�b�w�w������Ū���Ʀ�", MsgBoxStyle.Information, "����")
                bo = True
            ElseIf ds.Tables("PurchaseOrder").Rows(i)("NeedDate") > ds.Tables("PurchaseOrder").Rows(i)("DeliveryDate") Then
                MsgBox("�L�k�O�s�A���Ʃ��Ӥ��ݨD����j��w�w���", MsgBoxStyle.Information, "����")
                bo = True
            End If

            If bo = True Then
                viewPurchaseOrder.FocusedRowHandle = i
                CheckSave = False
                Exit Function
            End If

        Next
    End Function
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (CheckSave() = False) Then
            Exit Sub
        End If

        Select Case EditItem
            Case EditEnumType.ADD
                SaveData(EditEnumType.ADD)
            Case EditEnumType.EDIT
                SaveData(EditEnumType.EDIT)
            Case EditEnumType.CHECK
                Dim mpoi As New MrpPurchaseOrderInfo
                mpoi.PO = txtPO.EditValue
                mpoi.CheckBit = chkCheck.Checked
                mpoi.CheckUserID = InUserID
                mpoi.CheckRemark = txtCheckRemark.Text
                If mpoc.MrpPurchaseOrder_UpdateCheck(mpoi) = True Then
                    MsgBox("�f�֪��A�ק令�\", MsgBoxStyle.Information, "����")
                Else
                    MsgBox("�f�֪��A�ק異��", MsgBoxStyle.Information, "����")
                End If
            Case EditEnumType.RECHECK
                Dim mpoi As New MrpPurchaseOrderInfo
                mpoi.PO = txtPO.EditValue
                mpoi.ReCheckBit = chkCheck.Checked
                mpoi.ReCheckUserID = InUserID
                mpoi.ReCheckRemark = txtCheckRemark.Text
                If mpoc.MrpPurchaseOrder_UpdateCheck(mpoi) = True Then
                    MsgBox("�_�֪��A�ק令�\", MsgBoxStyle.Information, "����")
                Else
                    MsgBox("�_�֪��A�ק異��", MsgBoxStyle.Information, "����")
                End If
        End Select
        Me.Close()
    End Sub

    Private Sub SaveData(ByVal editItem As String)
        Try
            Dim result As Boolean = True
            Dim mpoi As New MrpPurchaseOrderInfo
            mpoi.PR = gluPR.EditValue.ToString
            mpoi.DeptID = gluDept.EditValue.ToString
            mpoi.SupplierID = gluSupplier.EditValue.ToString
            mpoi.RequestUserID = gluUserID.EditValue
            mpoi.RequestDate = dteRequestDate.EditValue
            mpoi.Remarks = txtRemarks.EditValue
            mpoi.WareHouseID = gluWareHouse.EditValue
            mpoi.IsUrgency = chkIsUrgency.Checked
            If editItem = EditEnumType.ADD Then
                txtPO.EditValue = mpoc.MrpPurchaseOrder_GetID()
                mpoi.PO = txtPO.EditValue.ToString
                mpoi.CreateUserID = InUserID
                result = mpoc.MrpPurchaseOrder_Add(mpoi) And result
            ElseIf editItem = EditEnumType.EDIT Then
                mpoi.PO = txtPO.EditValue.ToString
                mpoi.ModifyUserID = InUserID
                result = mpoc.MrpPurchaseOrder_Update(mpoi) And result
            End If

            If editItem = EditEnumType.EDIT Then
                If String.IsNullOrEmpty(DelAutoID) = False Then
                    Dim array1 As String()
                    array1 = DelAutoID.Split(",")
                    For x As Int16 = 0 To array1.Length - 2
                        mpoec.MrpPurchaseOrderEntry_Delete(array1(x), Nothing)
                    Next
                End If
            End If
            If ds.Tables("PurchaseOrder").Rows.Count > 0 Then
                Dim mpoei As New MrpPurchaseOrderEntryInfo
                Dim i As Integer
                For i = 0 To ds.Tables("PurchaseOrder").Rows.Count - 1
                    mpoei.AutoID = ds.Tables("PurchaseOrder").Rows(i)("AutoID").ToString
                    mpoei.PO = txtPO.EditValue.ToString
                    mpoei.M_Code = ds.Tables("PurchaseOrder").Rows(i)("M_Code").ToString
                    mpoei.PurchaseQty = ds.Tables("PurchaseOrder").Rows(i)("PurchaseQty").ToString
                    mpoei.UnitPrice = ds.Tables("PurchaseOrder").Rows(i)("UnitPrice").ToString
                    mpoei.NeedDate = CDate(ds.Tables("PurchaseOrder").Rows(i)("NeedDate"))
                    mpoei.DeliveryDate = CDate(ds.Tables("PurchaseOrder").Rows(i)("DeliveryDate"))
                    mpoei.Remarks = ds.Tables("PurchaseOrder").Rows(i)("Remarks").ToString
                    If editItem = EditEnumType.ADD Then                 '�W�[
                        mpoei.CreateUserID = InUserID
                        result = mpoec.MrpPurchaseOrderEntry_Add(mpoei) And result
                    ElseIf editItem = EditEnumType.EDIT Then                        '�ק�
                        mpoei.ModifyUserID = InUserID
                        If mpoei.AutoID = 0 Then
                            result = mpoec.MrpPurchaseOrderEntry_Add(mpoei) And result
                        Else
                            result = mpoec.MrpPurchaseOrderEntry_Update(mpoei) And result
                        End If
                    End If
                Next
            End If
            If editItem = EditEnumType.ADD And result = True Then
                MsgBox("�O�s���\", MsgBoxStyle.Information, "����")
            ElseIf editItem = EditEnumType.EDIT And result = True Then
                MsgBox("�ק令�\", MsgBoxStyle.Information, "����")
            ElseIf result = False Then
                MsgBox("�ƾګO�s����", MsgBoxStyle.Information, "����")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SaveMrpPurchaseOrder��k�X��")
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#End Region

#Region "�ƾڭ�����"
    Private Sub RepositoryItemCalcEdit1_EditValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEdit1.EditValueChanging
        If (CDec(e.NewValue) > 999999999 Or CDec(e.NewValue) < 0) Then
            e.Cancel = True
            Exit Sub
        End If
        Dim purchaseQty As Decimal = viewPurchaseOrder.GetFocusedRowCellValue("PurchaseQty")
        Dim unitPrice As Decimal = viewPurchaseOrder.GetFocusedRowCellValue("UnitPrice")
        If viewPurchaseOrder.FocusedColumn.FieldName = "PurchaseQty" Then
            purchaseQty = e.NewValue
        ElseIf viewPurchaseOrder.FocusedColumn.FieldName = "UnitPrice" Then
            unitPrice = e.NewValue
        End If
        viewPurchaseOrder.SetFocusedRowCellValue(TotalPrice, purchaseQty * unitPrice)
    End Sub
#End Region

#Region "��ܨ����ӱa�X�����Ӫ��䥦�򥻫H��"
    Private Sub gluSupplier_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSupplier.EditValueChanged
        txtPaymentType.EditValue = viewSupplier.GetFocusedRowCellValue("S_ToFrom")
        txtTel.EditValue = viewSupplier.GetFocusedRowCellValue("S_Tel")
        txtContacts.EditValue = viewSupplier.GetFocusedRowCellValue("S_Associate")
        txtCurrencyName.EditValue = viewSupplier.GetFocusedRowCellValue("CurrencyName")
        txtEmail.EditValue = viewSupplier.GetFocusedRowCellValue("S_Email")
        txtFax.EditValue = viewSupplier.GetFocusedRowCellValue("S_Fax")
    End Sub
#End Region

End Class