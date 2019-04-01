Imports LFERP.DataSetting
Imports LFERP.Library.Product
Imports LFERP.Library.ProductProcess
Imports LFERP.Library.Production.ProductionWareShipped
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.WareHouse
Imports LFERP.Library.Purchase.SharePurchase
Imports LFERP.Library.ProductionSchedule
Imports LFERP.Library.Production.ProductInventoryOut
Imports LFERP.SystemManager



Public Class ProductInventoryOut
#Region "�ݩʳ]�m"
    Dim pic As New ProductInventoryController
    Dim upi As New List(Of UserPowerInfo)
    Dim upc As New UserPowerControl

    Dim ds As New DataSet
    Dim strWHOutID, strWHInID As String
    Dim oldCheck, oldInCheck As Boolean

    Private _EditItem As String
    Private _EditValue As String
    Private _EditID As String
    Private _EditName As String
    Private _EditM_Code As String
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
    Public Property EditID() As String
        Get
            Return _EditID
        End Get
        Set(ByVal value As String)
            _EditID = value
        End Set
    End Property
    Public Property EditName() As String
        Get
            Return _EditName
        End Get
        Set(ByVal value As String)
            _EditName = value
        End Set
    End Property
    Public Property EditM_Code() As String
        Get
            Return _EditM_Code
        End Get
        Set(ByVal value As String)
            _EditM_Code = value
        End Set
    End Property
#End Region

#Region "�s�W�ק�{��"
    ''' <summary>
    '''  '�s�W�{��   
    ''' </summary>
    Sub DataNew()
        Dim pi As New ProductInventoryOutInfo
        Dim pc As New ProductInventoryOutControl

        txtWIPID.Text = GetNO()
        pi.PO_NO = txtWIPID.Text
        pi.WH_ID = strWHOutID
        pi.Pro_Type = cbType.EditValue
        pi.PO_Action = InUserID
        pi.PO_Date = DateEdit1.Text
        Dim i As Integer
        With ds.Tables("ProductInventoryOut")
            For i = 0 To .Rows.Count - 1
                pi.PM_M_Code = .Rows(i)("PM_M_Code")
                pi.M_Code = .Rows(i)("M_Code")
                pi.PO_Qty = IIf(IsDBNull(.Rows(i)("PO_Qty")), 0, .Rows(i)("PO_Qty"))
                pi.PO_Remark = IIf(IsDBNull(.Rows(i)("PO_Remark")), String.Empty, .Rows(i)("PO_Remark"))
                pi.PM_Type = IIf(IsDBNull(.Rows(i)("PM_Type")), String.Empty, .Rows(i)("PM_Type"))
                '----------------------------------------------
                '2013-12-5
                '���@�s�W
                If Not pc.ProductInventoryOut_Add(pi) Then
                    MsgBox("�O�s����: " & txtWIPID.Text & " ")
                    Exit Sub
                End If
            Next
        End With
        MsgBox("�w�O�s,�渹: " & txtWIPID.Text & " ")
        Me.Close()
    End Sub
    ''' <summary>
    '''  �ק�{��  
    ''' </summary>
    Sub DataEdit()

     
        Dim i As Integer
        If ds.Tables("DelProductInventoryOut").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelProductInventoryOut").Rows.Count - 1
                If Not IsDBNull(ds.Tables("DelProductInventoryOut").Rows(j)("AutoID")) Then
                    Dim pc As New ProductInventoryOutControl
                    pc.ProductInventoryOut_Delete(ds.Tables("DelProductInventoryOut").Rows(j)("AutoID"))
                End If
            Next j
        End If

       

        With ds.Tables("ProductInventoryOut")
            For i = 0 To .Rows.Count - 1
                Dim pi As New ProductInventoryOutInfo
                Dim pc As New ProductInventoryOutControl
                If IsDBNull(.Rows(i)("AutoID")) Then
                    pi.PO_NO = txtWIPID.Text
                    pi.WH_ID = strWHOutID
                    pi.Pro_Type = cbType.EditValue
                    pi.PO_Action = InUserID
                    pi.PO_Date = DateEdit1.Text

                    pi.PM_M_Code = .Rows(i)("PM_M_Code")
                    pi.M_Code = .Rows(i)("M_Code")
                    pi.PO_Qty = IIf(IsDBNull(.Rows(i)("PO_Qty")), 0, .Rows(i)("PO_Qty"))
                    pi.PO_Remark = IIf(IsDBNull(.Rows(i)("PO_Remark")), String.Empty, .Rows(i)("PO_Remark"))
                    pi.PM_Type = IIf(IsDBNull(.Rows(i)("PM_Type")), String.Empty, .Rows(i)("PM_Type"))

                    If Not pc.ProductInventoryOut_Add(pi) Then
                        MsgBox("�O�s����: " & txtWIPID.Text & " ")
                        Exit Sub
                    End If
                Else
                    pi.PO_NO = txtWIPID.Text
                    pi.WH_ID = strWHOutID
                    pi.Pro_Type = cbType.EditValue
                    pi.PO_Date = DateEdit1.Text
                    pi.PO_CheckAction = InUserID

                    pi.AutoID = .Rows(i)("AutoID")
                    pi.PM_M_Code = .Rows(i)("PM_M_Code")
                    pi.M_Code = .Rows(i)("M_Code")
                    pi.PO_Action = InUserID
                    pi.PO_Qty = IIf(IsDBNull(.Rows(i)("PO_Qty")), 0, .Rows(i)("PO_Qty"))
                    pi.PO_Remark = IIf(IsDBNull(.Rows(i)("PO_Remark")), String.Empty, .Rows(i)("PO_Remark"))
                    pi.PM_Type = IIf(IsDBNull(.Rows(i)("PM_Type")), String.Empty, .Rows(i)("PM_Type"))
                    If Not pc.ProductInventoryOut_Update(pi) Then
                        MsgBox("�ק異��,�渹: " & txtWIPID.Text & " ")
                        Exit Sub
                    End If
                End If

            Next


        End With
        MsgBox("�w�O�s,�渹: " & txtWIPID.Text & " ")
        Me.Close()
    End Sub
#End Region

#Region "�Ы��{�ɪ�"
    ''' <summary>
    ''' �Ы��{�ɪ�
    ''' </summary>
    Sub CreateTable()
        ds.Tables.Clear()
      
        ''��G
        With ds.Tables.Add("DelProductInventoryOut")
            .Columns.Add("AutoID", GetType(String))
        End With
        '��T
        With ds.Tables.Add("ProductInventoryOut")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("PO_NO", GetType(String))
            .Columns.Add("PM_Type", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("WH_ID", GetType(String))
            .Columns.Add("PO_Qty", GetType(Integer))
            .Columns.Add("PO_Action", GetType(String))
            .Columns.Add("PO_Date", GetType(String))
            .Columns.Add("PO_Remark", GetType(String))
            .Columns.Add("PO_EndQty", GetType(Integer))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("PI_Qty", GetType(Integer))
            .Columns.Add("PM_JiYu", GetType(String))
        End With
        Grid.DataSource = ds.Tables("ProductInventoryOut")
    End Sub
#End Region

#Region "�f�ּƾ�"
    Public Function LoadDataChkeck(ByVal PWO_NO As String) As Boolean
        LoadDataChkeck = True
        ds.Tables("ProductInventoryOut").Clear()

        Dim piL As List(Of ProductInventoryOutInfo)
        Dim pc As New ProductInventoryOutControl
        Dim pii As List(Of ProductInventoryInfo)
        piL = pc.ProductInventoryOut_GetList(Nothing, PWO_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Try
            If piL.Count = 0 Then
                MsgBox("�S���ƾ�")
                LoadDataChkeck = False
                Exit Function
            Else
                strWHOutID = piL(0).WH_ID
                txtWIPID.Text = piL(0).PO_NO
                txtWIPID.Enabled = False
                txtWH.EditValue = piL(0).PWS_OutName
                txtWH.Enabled = False
                cbType.EditValue = piL(0).Pro_Type
                cbType.Enabled = False
                DateEdit1.EditValue = piL(0).PO_Date
                DateEdit1.Enabled = False
                CheckDate.Text = Format(DateTime.Now, "yyyy/MM/dd")
                CheckAction.Text = piL(0).PWS_ActionName
                CheckRemark.Text = piL(0).PO_CheckRemark
                Dim i As Integer
                Dim row As DataRow
                For i = 0 To piL.Count - 1
                    row = ds.Tables("ProductInventoryOut").NewRow
                    row("AutoID") = piL(i).AutoID
                    row("PM_M_Code") = piL(i).PM_M_Code
                    row("M_Code") = piL(i).M_Code
                    row("M_Name") = piL(i).M_Name
                    row("M_Gauge") = piL(i).M_Gauge
                    row("M_Unit") = piL(i).M_Unit
                    row("PO_Qty") = piL(i).PO_Qty
                    row("PO_Remark") = piL(i).PO_Remark
                    If piL(i).PM_Type <> "" Then
                        row("PM_Type") = piL(i).PM_Type
                    Else
                        row("PM_Type") = LoadProductionType(cbType.EditValue, piL(i).PM_M_Code, piL(i).M_Code)
                    End If
                    row("PM_JiYu") = piL(i).PM_JiYu

                    pii = pic.ProductInventory_GetList(piL(i).PM_M_Code, piL(i).M_Code, strWHOutID, Nothing)
                    If pii.Count > 0 Then
                        row("PI_Qty") = pii(0).PI_Qty
                    Else
                        row("PI_Qty") = 0
                    End If

                    ds.Tables("ProductInventoryOut").Rows.Add(row)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

#End Region


#Region "��^�d�߼ƾڨ챱��"
    Public Function LoadData(ByVal PWO_NO As String) As Boolean
        LoadData = True
        ds.Tables("ProductInventoryOut").Clear()

        Dim piL As List(Of ProductInventoryOutInfo)
        Dim pc As New ProductInventoryOutControl
        Dim pii As List(Of ProductInventoryInfo)
        piL = pc.ProductInventoryOut_GetList(PWO_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Try
            If piL.Count = 0 Then
                MsgBox("�S���ƾ�")
                LoadData = False
                Exit Function
            Else
                strWHOutID = piL(0).WH_ID
                txtWIPID.Text = piL(0).PO_NO
                txtWIPID.Enabled = False
                txtWH.EditValue = piL(0).PWS_OutName
                txtWH.Enabled = False
                cbType.EditValue = piL(0).Pro_Type
                cbType.Enabled = False
                DateEdit1.EditValue = piL(0).PO_Date
                DateEdit1.Enabled = False
                CheckDate.Text = Format(DateTime.Now, "yyyy/MM/dd")
                CheckAction.Text = piL(0).PWS_ActionName
                CheckRemark.Text = piL(0).PO_CheckRemark
                Dim i As Integer
                Dim row As DataRow
                For i = 0 To piL.Count - 1
                    row = ds.Tables("ProductInventoryOut").NewRow
                    row("AutoID") = piL(i).AutoID
                    row("PM_M_Code") = piL(i).PM_M_Code
                    row("M_Code") = piL(i).M_Code
                    row("M_Name") = piL(i).M_Name
                    row("M_Gauge") = piL(i).M_Gauge
                    row("M_Unit") = piL(i).M_Unit
                    row("PO_Qty") = piL(i).PO_Qty
                    row("PO_Remark") = piL(i).PO_Remark
                    If piL(i).PM_Type <> "" Then
                        row("PM_Type") = piL(i).PM_Type
                    Else
                        row("PM_Type") = LoadProductionType(cbType.EditValue, piL(i).PM_M_Code, piL(i).M_Code)
                    End If
                    row("PM_JiYu") = piL(i).PM_JiYu

                    pii = pic.ProductInventory_GetList(piL(i).PM_M_Code, piL(i).M_Code, strWHOutID, Nothing)
                    If pii.Count > 0 Then
                        row("PI_Qty") = pii(0).PI_Qty
                    Else
                        row("PI_Qty") = 0
                    End If
               
                    ds.Tables("ProductInventoryOut").Rows.Add(row)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

#End Region

#Region "�Ұʵ���ƥ�"
    Private Sub ProductionWareShipped_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        txtWIPID.Enabled = False
        DateEdit1.Text = Format(Now, "yyyy/MM/dd")
        Select Case EditItem
            Case "Shipped" '�X�f��

                Me.Text = "�Ͳ��ܥX�w��--�s�W"
                Me.Label1.Text = Me.Text

                cbType.EditValue = "�˰t�X�f"
                '--------------------------------------------------------
                Dim pmws As New PermissionModuleWarrantSubController
                Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881011")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "�O" Then cbType.EditValue = upi(0).UserType
                End If
                '---------------------------------------------------------



                cbType.Enabled = False
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage2.PageVisible = False
            Case "ShippedUpdate"

                txtWIPID.Text = EditValue
                LoadDataChkeck(EditValue)
                CheckEdit1.Enabled = False
                CheckRemark.Enabled = False
                Me.Text = "�Ͳ��ܥX�w��--�ק�" & EditValue
                Me.Label1.Text = Me.Text


                XtraTabControl1.SelectedTabPage = XtraTabPage1
                XtraTabPage2.PageVisible = False
            Case "PreView"
                Me.Text = "�Ͳ��ܥX�w��--�d��" & EditValue
                Me.Label1.Text = Me.Text
                'LoadData(EditValue)
                LoadDataChkeck(EditValue)
                cmdSave.Visible = False
                cmsCodeAdd.Visible = False
                cmsCodeDel.Visible = False
                PN_Qty.Visible = True
                XtraTabControl1.SelectedTabPage = XtraTabPage1
                '----------------------------------------------------------------------
                '2013-12-5 �s�W
                XtraTabPage2.PageVisible = False
                GridView1.Columns("PM_Type").OptionsColumn.AllowEdit = False
                GridView1.Columns("M_Gauge").OptionsColumn.AllowEdit = False
                GridView1.Columns("PO_Remark").OptionsColumn.AllowEdit = False
                GridView1.Columns("PO_Qty").OptionsColumn.AllowEdit = False
                GridView1.Columns("PN_Qty").Visible = False
                '----------------------------------------------------------------------
            Case "Check"
                Me.Text = "�Ͳ��ܥX�w��--�f��" & EditValue
                Me.Label1.Text = Me.Text
                txtWH.Enabled = False
                cbType.Enabled = False
                DateEdit1.Enabled = False

                PN_Qty.Visible = False

                cmsCodeAdd.Visible = False
                cmsCodeDel.Visible = False
                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

                LoadDataChkeck(EditValue)
                'LoadData(EditValue)
                XtraTabControl1.SelectedTabPage = XtraTabPage2
            Case "InCheck"
                Me.Text = "�Ͳ��ܥX�w��--���f�T�{" & EditValue
                Me.Label1.Text = Me.Text
                'txtWHIn.Enabled = False
                txtWH.Enabled = False
                cbType.Enabled = False
                DateEdit1.Enabled = False
                'txtRemark.Enabled = False
                XtraTabPage2.PageVisible = False
                'CheckEdit3.Enabled = True
                PN_Qty.Visible = True

                GridView1.OptionsBehavior.AutoSelectAllInEditor = False
                GridView1.OptionsBehavior.Editable = False
                GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
                LoadData(EditValue)
        End Select
        cmdGuideAdd.Visible = False
    End Sub
#End Region

#Region "�f�֡СнT�{���f"
    Sub UpdateCheck()
        Dim pi As New ProductInventoryOutInfo
        Dim pc As New ProductInventoryOutControl

        If Not CheckEdit1.Checked Then
            MsgBox("�нT�{�f�֪��A")
            Exit Sub
        End If

        '----------------�X�f����B�z----------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("ProductInventoryOut").Rows.Count - 1
            Dim pii As New ProductInventoryInfo
            pii.WH_ID = strWHOutID

            Dim piiGet As List(Of ProductInventoryInfo)

            piiGet = pic.ProductInventory_GetList(ds.Tables("ProductInventoryOut").Rows(i)("PM_M_Code").ToString, ds.Tables("ProductInventoryOut").Rows(i)("M_Code").ToString, strWHOutID, Nothing)
            '------------------------------------------------------------------------------------------
            '�s�W2013-12-5
            pii.PI_Qty = piiGet(0).PI_Qty - CInt(ds.Tables("ProductInventoryOut").Rows(i)("PO_Qty"))
            '------------------------------------------------------------------------------------------

            pii.PM_M_Code = ds.Tables("ProductInventoryOut").Rows(i)("PM_M_Code").ToString
            pii.M_Code = ds.Tables("ProductInventoryOut").Rows(i)("M_Code").ToString
            If pic.ProductInventory_Update(pii) = False Then
                MsgBox("�X�f���㥢��,���ˬd��]!", MsgBoxStyle.Information, "����")
                Exit Sub
            End If

            pi.AutoID = ds.Tables("ProductInventoryOut").Rows(i)("AutoID").ToString
            pi.PO_NO = txtWIPID.Text
            pi.PO_Check = CheckEdit1.Checked
            pi.PO_CheckAction = InUserID
            pi.PO_EndQty = pii.PI_Qty
            pi.PO_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            pi.PO_CheckRemark = CheckRemark.Text
            If Not pc.ProductInventoryOut_Check(pi) Then
                MsgBox("�f�֥���,���ˬd��]!")
                Exit Sub
            End If

            'If pc.ProductInventoryOut_Check(pi) = True Then
            '    MsgBox("�f�֪��A�w����!")
            'Else
            '    MsgBox("�f�֥���,���ˬd��]!")
            '    Exit Sub
            'End If
        Next
        MsgBox("�f�֪��A�w����!")
        Me.Close()
    End Sub

    Sub UpdateInCheck()
        
    End Sub
#End Region

#Region "�l��s�W�R���ƥ�"
    Private Sub cmsCodeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeAdd.Click
        tempCode = ""
        tempValue6 = "�X�f�޲z"
        frmBOMSelect.XtraTabPage1.PageVisible = False
        frmBOMSelect.XtraTabPage2.PageVisible = False
        frmBOMSelect.XtraTabPage3.PageVisible = True
        frmBOMSelect.ShowDialog()
        '�W�[�O��
        If frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 0 Then
            If tempCode = "" Then
                Exit Sub
            Else
                AddRow(tempCode)
            End If

        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 1 Then  '�妸
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue7, ",")
            n = Len(Replace(tempValue7, ",", "," & "*")) - Len(tempValue7)
            For i = 0 To n
                Dim j As Integer
                For j = 0 To ds.Tables("ProductInventoryOut").Rows.Count - 1
                    If arr(i) = ds.Tables("ProductInventoryOut").Rows(j)("M_Code") Then
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
                row = ds.Tables("ProductInventoryOut").NewRow
                row("PM_M_Code") = ""
                row("M_Code") = objInfo.M_Code
                row("M_Name") = objInfo.M_Name
                row("M_Unit") = objInfo.M_Unit
                row("M_Gauge") = objInfo.M_Gauge
                row("PO_Qty") = 0
                'row("PWS_SubType") = "����"
                ds.Tables("ProductInventoryOut").Rows.Add(row)
                GridView1.MoveLast()
            Next
        ElseIf frmBOMSelect.XtraTabControl1.SelectedTabPageIndex = 2 Then   '�p�׽s��
            Dim i, n As Integer
            Dim arr(n) As String
            arr = Split(tempValue8, ",")
            n = Len(Replace(tempValue8, ",", "," & "*")) - Len(tempValue8)
            For i = 0 To n
                If arr(i) = "" Then
                    Exit Sub
                End If
                Dim mc As New LFERP.Library.Material.MaterialController
                Dim objInfo As New LFERP.Library.Material.MaterialInfo
                objInfo = mc.MaterialCode_Get(arr(i))
                Dim row As DataRow
                'row = ds.Tables("ProductionWareShipped").NewRow
                row = ds.Tables("ProductInventoryOut").NewRow
                row("PM_M_Code") = tempValue3
                row("M_Code") = objInfo.M_Code
                row("M_Name") = objInfo.M_Name
                row("M_Gauge") = objInfo.M_Gauge
                row("M_Unit") = objInfo.M_Unit
                row("PO_Qty") = 0
                row("PM_Type") = LoadProductionType(cbType.EditValue, tempValue3, objInfo.M_Code)
                Dim ppc1 As New ProcessMainControl
                Dim ppi1 As New List(Of ProcessMainInfo)
                ppi1 = ppc1.ProcessMain_GetList1(Nothing, tempValue3, cbType.EditValue, objInfo.M_Code)
                If ppi1.Count > 0 Then
                    row("PM_JiYu") = ppi1(0).PM_JiYu
                End If
                Dim pii As List(Of ProductInventoryInfo)
                pii = pic.ProductInventory_GetList(tempValue3, objInfo.M_Code, strWHOutID, Nothing)
                If pii.Count > 0 Then
                    row("PI_Qty") = pii(0).PI_Qty
                Else
                    row("PI_Qty") = 0
                End If
                ds.Tables("ProductInventoryOut").Rows.Add(row)
                GridView1.MoveLast()
            Next
        End If
        tempValue7 = ""
        tempValue8 = ""
    End Sub

    Sub AddRow(ByVal strCode As String)
        If strCode = "" Then
        Else
            Dim i As Integer
            For i = 0 To ds.Tables("ProductionWareShipped").Rows.Count - 1
                If strCode = ds.Tables("ProductionWareShipped").Rows(i)("M_Code") Then
                    MsgBox("�@�i�椣���\�����_���ƽs�X....")
                    Exit Sub
                End If
            Next
            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            Dim row As DataRow
            row = ds.Tables("ProductionWareShipped").NewRow
            row("PM_M_Code") = IIf(Mid(objInfo.M_Code, 1, 2) = "MG", objInfo.M_Code, String.Empty)
            row("M_Code") = objInfo.M_Code
            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge
            row("PWS_Qty") = 0
            row("PWS_SubType") = "����"
            ds.Tables("ProductionWareShipped").Rows.Add(row)
            GridView1.MoveLast()
        End If
    End Sub
    ''' <summary>
    '''     �l��R��
    ''' </summary>
    Private Sub cmsCodeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCodeDel.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(ArrayToString(GridView1.GetSelectedRows()), "AutoID")
        If DelTemp = "AutoID" Then
        Else
            Dim row As DataRow = ds.Tables("DelProductInventoryOut").NewRow
            row("AutoID") = GridView1.GetFocusedRowCellValue("AutoID").ToString
            ds.Tables("DelProductInventoryOut").Rows.Add(row)
        End If
        ds.Tables("ProductInventoryOut").Rows.RemoveAt(CInt(ArrayToString(GridView1.GetSelectedRows())))
    End Sub

    Private Sub cmdGuideAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuideAdd.Click
        tempValue5 = "�Ͳ��ܥX�f"
        Dim fr As New frmProductionSelect
        fr.ShowDialog()
    End Sub

#End Region

#Region "����ƥ�"
    Private Sub txtWH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtWH.ButtonClick
        tempCode = "�Ͳ��ܮw"
        frmWareHouseSelect.SelectWareID = ""
        tempValue2 = "881005"
        tempValue3 = "881005"
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = MDIMain.Left + MDIMain.tvModule.Width + Me.Left + txtWH.Left + 15
        frmWareHouseSelect.Top = MDIMain.Top + Me.Top + txtWH.Top + txtWH.Height + 140
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            strWHOutID = frmWareHouseSelect.SelectWareID
            txtWH.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub

    Private Sub txtWHIn_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
       
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case EditItem
            Case "Shipped"
                If CheckData() = True Then
                    DataNew()
                End If

            Case "ShippedUpdate"
                If CheckData() = True Then
                    DataEdit()
                End If
                
            Case "Check"
                If CheckData() = True Then
                    UpdateCheck()
                End If
            Case "InCheck"
                If CheckData() = True Then
                    UpdateInCheck()
                End If
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

#End Region

#Region "�ˬd�ƥ�----�O�_����"
    Function CheckData() As Boolean  '�P�_��e�o�X�ܮw���ƬO�_����
       
        CheckData = True

        

        If txtWH.Text = "" Then
            MsgBox("�X�f�ܮw���ର��")
            CheckData = False
            Exit Function
        End If


        If DateEdit1.Text = "" Then
            MsgBox("�X�f������ର��")
            CheckData = False
            Exit Function
        End If

        If ds.Tables("ProductInventoryOut").Rows.Count <= 0 Then
            MsgBox("�S���K�[���󪫮�")
            CheckData = False
            Exit Function
        End If

        Dim i, j As Integer
        For i = 0 To ds.Tables("ProductInventoryOut").Rows.Count - 1

            If IsDBNull(ds.Tables("ProductInventoryOut").Rows(i)("PM_M_Code")) Then
                MsgBox("���~�s�����ରNULL!")
                CheckData = False
                Exit Function
            End If

            If ds.Tables("ProductInventoryOut").Rows(i)("PM_M_Code") = "" Then
                MsgBox("���~�s�����ର��!")
                CheckData = False
                Exit Function
            End If



            If ds.Tables("ProductInventoryOut").Rows(i)("PO_Qty") <= 0 Then
                MsgBox("�X�f�ƶq���ର0�I")
                CheckData = False
                Exit Function
            End If


            Dim pii As New List(Of ProductInventoryInfo)
            pii = pic.ProductInventory_GetList(ds.Tables("ProductInventoryOut").Rows(i)("PM_M_Code"), ds.Tables("ProductInventoryOut").Rows(i)("M_Code"), strWHOutID, Nothing)

            If pii.Count <= 0 Then
                MsgBox("�w�s�ƶq�����I")
                CheckData = False
                Exit Function
            End If
            If ds.Tables("ProductInventoryOut").Rows(i)("PO_Qty") > pii(0).PI_Qty Then
                MsgBox("�X�w�ƶq����j�_�w�s�ƶq�I")
                CheckData = False
                Exit Function
            End If

            '-----------------------�X�f�즨�~�ˬd�u��X���~-------------------------
            Dim whlist As List(Of WareHouseInfo)
            Dim wh As New WareHouseController
            whlist = wh.WareHouse_GetList("'" + strWHInID + "'")
            If whlist(0).WH_Remark = "���~��" Then
                If ds.Tables("ProductInventoryOut").Rows(i)("PM_M_Code").ToString <> ds.Tables("ProductInventoryOut").Rows(i)("M_Code").ToString Then
                    MsgBox("���~�X�f--���~�s���n���󪫮ƽs�X�I", MsgBoxStyle.Information, "����")
                    CheckData = False
                    Exit Function
                End If
            End If



        Next

        '�l����X�{�ۦP�����~
        With ds.Tables("ProductInventoryOut")
            If .Rows.Count > 1 Then
                For j = 0 To .Rows.Count - 1
                    For i = 0 To .Rows.Count - 1
                        If i <> j Then
                            If .Rows(i)("PM_M_Code") = .Rows(j)("PM_M_Code") And .Rows(i)("M_Code") = .Rows(j)("M_Code") And .Rows(i)("PM_Type") = .Rows(j)("PM_Type") Then
                                MsgBox("�@�i�椤����s�b�ۦP���~,�P����,�P���ƽs�X���O��!")
                                CheckData = False
                                Exit Function
                            End If
                        End If
                    Next
                Next
            End If
        End With

    End Function
#End Region

#Region "������J�C��"
    Private Sub cboPM_Type_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPM_Type.Click
        Dim Get_LoadP_YN As Boolean
        If GridView1.RowCount > 0 Then
        Else
            Exit Sub
        End If
        Dim strA, strB, strC As String
        strA = ds.Tables("ProductInventoryOut").Rows((GridView1.FocusedRowHandle)).Item("PM_M_Code")
        strB = cbType.EditValue
        strC = ds.Tables("ProductInventoryOut").Rows((GridView1.FocusedRowHandle)).Item("M_Code")

        If EditID <> strA Or EditName <> strB Or EditM_Code <> strC Then
            EditID = strA
            EditName = strB
            EditM_Code = strC
            Get_LoadP_YN = True
        Else
            Get_LoadP_YN = False
        End If
        If Get_LoadP_YN = True Then
            LoadProductionType(strB, strA, strC)
        End If
    End Sub
    Function LoadProductionType(ByVal _Pro_Type As String, ByVal _PM_M_Code As String, ByVal _M_Code As String) As String
        LoadProductionType = ""
        Dim ppc As New ProcessMainControl
        Dim ppi As New List(Of ProcessMainInfo)
        cboPM_Type.Items.Clear()
        ppi = ppc.ProcessMain_GetList1(Nothing, _PM_M_Code, _Pro_Type, _M_Code)
        If ppi.Count > 0 Then
            Dim k As Integer
            For k = 0 To ppi.Count - 1
                cboPM_Type.Items.Add(ppi(k).Type3ID)
            Next
            LoadProductionType = ppi(0).Type3ID
        End If
    End Function
#End Region

#Region "�۰ʬy����"
    '�y����
    Public Function GetNO() As String
        Dim pi As New ProductInventoryOutInfo
        Dim pc As New ProductInventoryOutControl
        Dim strA As String
        strA = Format(Now, "yyMM")
        pi = pc.ProductInventoryOut_GetNo(strA)
        If pi Is Nothing Then
            GetNO = "PWO" & strA & "0001"
        Else
            GetNO = "PWO" + strA + Mid((CInt(Mid(pi.PO_NO, 8)) + 10001), 2)
        End If
    End Function

  
   
#End Region

End Class