Imports LFERP.Library.SampleManager.SampleSend
Imports LFERP.Library.SampleManager.SampleOrders
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.SampleManager.SampleCollection
Imports LFERP.Library.SampleManager.SampleTransaction
Imports LFERP.Library.SampleManager.SampleProcess
Imports LFERP.SystemManager

Public Class frmSampleBarcode

#Region "属性"
    Private sscon As New SampleSendCodeControler
    Private socon As New SampleOrdersCodeControler
    Private scon As New SampleSendControler
    Private sclcom As New SampleCollectionControler
    Private Ncount As Integer
    Public TransactionList As New List(Of SampleTransactionInfo)

    Public ds As New DataSet
    Private _EditItem As String '属性栏位
    Private _EditValue As String
    Private _SS_Edition As String
    Private _SO_ID As String
    Private _SP_ID As String
    Private _SS_Qty As Integer
    Property EditValue() As String '属性
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property SS_Edition() As String '版本号
        Get
            Return _SS_Edition
        End Get
        Set(ByVal value As String)
            _SS_Edition = value
        End Set
    End Property

    Property SO_ID() As String '订单编号
        Get
            Return _SO_ID
        End Get
        Set(ByVal value As String)
            _SO_ID = value
        End Set
    End Property
    Property SP_ID() As String '寄送单号
        Get
            Return _SP_ID
        End Get
        Set(ByVal value As String)
            _SP_ID = value
        End Set
    End Property
    Property SS_Qty() As Integer  '寄送单号
        Get
            Return _SS_Qty
        End Get
        Set(ByVal value As Integer)
            _SS_Qty = value
        End Set
    End Property
#End Region

#Region "窗体载入事件"
    Private Sub frmSampleWareCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        txt_PM_M_Code.Text = EditValue
        'txt_SO_ID.Text = SO_ID
        'txtSS_Edition.Text = SS_Edition
        'txt_PM_M_Code.Enabled = False
        TextEdit2.Text = SS_Qty
        Me.txtM_Code.Select()
        '  TextEdit1.Select()
        'If Me.SP_ID = String.Empty Then
        '    Ncount = socon.SampleOrdersCode_GetCount(SO_ID, SS_Edition)
        '    Label6.Text = Ncount
        '    Me.XtraTabControl1.SelectedTabPage = XtraTabPage1
        'Else
        '    Ncount = sscon.SampleSendCode_GetCount(SO_ID, SS_Edition)
        '    Label6.Text = Ncount
        Me.XtraTabControl1.SelectedTabPage = XtraTabPage2
        'End If
        If EditItem = "SampleCollection" Then
            Dim sc As New SampleOrdersMainControler
            txtM_Code.Properties.DisplayMember = "PM_M_Code"
            txtM_Code.Properties.ValueMember = "PM_M_Code"
            txtM_Code.Properties.DataSource = sc.sampleProcess_GetPRONO(Nothing)

            txtM_Code.Visible = True
            Label5.Visible = True
            PM_M_Code.Visible = True

            gluM_Code.Visible = True
            Label13.Visible = True
        ElseIf EditItem = "SampleOrders" Then
            Dim sc As New SampleOrdersMainControler
            txtM_Code.Properties.DisplayMember = "PM_M_Code"
            txtM_Code.Properties.ValueMember = "PM_M_Code"
            txtM_Code.Properties.DataSource = sc.sampleProcess_GetPRONO(Nothing)
            txtM_Code.EditValue = EditValue
            txtM_Code.Visible = True
            txtM_Code.Enabled = False
            Label5.Visible = True
        End If
    End Sub
#End Region

#Region "方法"
    Sub CreateTable()
        With ds.Tables.Add("SampleWareA")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("Code_Qty", GetType(String))
            .Columns.Add("Remark", GetType(String))
            .Columns.Add("BitAuto", GetType(Boolean))
        End With
        GridControl1.DataSource = ds.Tables("SampleWareA")
    End Sub
#End Region

#Region "键盘事件"
    Private Sub TextEdit_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextEdit1.KeyUp, TextEdit2.KeyUp
        Dim m As New System.Text.RegularExpressions.Regex("^\+?[1-9][0-9]*$")  '顯示整數,正浮點數正則表達式
        If m.IsMatch(sender.Text) = True Then
        Else
            sender.Text = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub txtCode_ID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode_ID.KeyDown
        If e.KeyCode = Keys.Enter Then
        Else
            Exit Sub
        End If

        If SetBarcodeLeng(txtCode_ID.Text) = False Then
            MessageBox.Show("您输入的条码长度不正确", "提示")
            txtCode_ID.Focus()
            txtCode_ID.Text = String.Empty
            Exit Sub
        End If
        '-------------------------------------------------------
        Select Case EditItem
            Case "SampleCollection"
                If Me.txtM_Code.Text = String.Empty Then
                    MessageBox.Show("產品編號不能為空", "提示")
                    txtM_Code.Focus()
                    Exit Sub
                End If
            Case "SampleOrders"
                If Me.txtM_Code.Text = String.Empty Then
                    MessageBox.Show("產品編號不能為空", "提示")
                    txtM_Code.Focus()
                    Exit Sub
                End If
            Case "SampleSend"
                If socon.SampleOrdersCode_GetID(txtCode_ID.Text) = False Then
                    txtCode_ID.Text = String.Empty
                    txtCode_ID.Focus()
                    MsgBox(txtCode_ID.Text & "此編號订单条码已存在!,无法输入")
                    Exit Sub
                End If
        End Select
        '-------------------------------------------------------
        Dim I As Integer
        For I = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            If ds.Tables("SampleWareA").Rows(I)("Code_ID").ToString() = txtCode_ID.Text Then
                GridControl1.Focus()
                GridView2.FocusedRowHandle = I
                txtCode_ID.Text = String.Empty
                txtCode_ID.Focus()
                Label4.Text = txtCode_ID.Text + "條碼重複"
                '  MessageBox.Show("采集" + ds.Tables("SampleWareA").Rows(I)("Code_ID").ToString() + "条码存在，请重输入!", "提示")
                Exit Sub
            End If
        Next

        If txtCode_ID.Text <> String.Empty Then
            Dim row As DataRow = ds.Tables("SampleWareA").NewRow
            row("PM_M_Code") = txtM_Code.Text
            row("Code_ID") = Trim(txtCode_ID.Text)
            row("Code_Qty") = 1
            row("Remark") = String.Empty

            If chkBitAuto.Checked Then
                row("BitAuto") = True
            Else
                row("BitAuto") = False
            End If

            ds.Tables("SampleWareA").Rows.Add(row)
        End If
        Label4.Text = String.Empty
        txtCode_ID.Text = String.Empty
        txtCode_ID.Focus()
    End Sub
#End Region

    Function SetBarcodeLeng(ByVal Barcode As String) As Boolean
        '1------------------------------------
        SetBarcodeLeng = False
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "8901")
        If pmwiL.Count > 0 Then

            Dim n As Integer
            Dim arr(n) As String
            arr = Split(pmwiL.Item(0).PMWS_Value, ",")
            n = Len(Replace(pmwiL.Item(0).PMWS_Value, ",", "," & "*")) - Len(pmwiL.Item(0).PMWS_Value)

            Dim i As Integer
            For i = 0 To n
                If CInt(arr(i)) = Len(Barcode) Then
                    SetBarcodeLeng = True
                    Exit Function
                    'Else
                    '    SetBarcodeLeng = False
                End If
            Next
        Else
            SetBarcodeLeng = True
            Exit Function
        End If
    End Function



#Region "按键事件"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    ''' <summary>
    '''  产生自动条码   
    ''' </summary>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i, n, m, x As Integer
        Dim StrCode_ID As String
        ds.Tables("SampleWareA").Clear()
        If EditItem = "SampleCollection" Then
            If Me.txtM_Code.Text = String.Empty Then
                MessageBox.Show("產品編號不能為空", "提示")
                txtM_Code.Focus()
                Exit Sub
            End If
        End If

        If txt_PM_M_Code.Text = String.Empty Then
            MessageBox.Show("條碼前置碼不能為空!", "提示")
            txt_PM_M_Code.Focus()
            Exit Sub
        End If

        If TextEdit1.Text = String.Empty Then
            MessageBox.Show("起止流水號不能為空!", "提示")
            TextEdit1.Focus()
            Exit Sub
        End If
        If TextEdit2.Text = String.Empty Then
            MessageBox.Show("起止流水號不能為空!", "提示")
            TextEdit1.Focus()
            Exit Sub
        End If

        If Val(TextEdit1.Text) > Val(TextEdit2.Text) Then
            MessageBox.Show("起止流水號,輸入有誤碼!", "提示")
            TextEdit1.Focus()
            Exit Sub
        End If

        m = CInt(TextEdit1.Text)
        n = CInt(TextEdit2.Text)

        Select Case txtVersion.Value
            Case 2
                x = 10
            Case 3
                x = 9
            Case 4
                x = 8
            Case 5
                x = 7
            Case 6
                x = 6
            Case 7
                x = 5
            Case 8
                x = 4
            Case 9
                x = 3
        End Select
        If TextEdit1.Text.Length > txtVersion.Value Then
            MessageBox.Show("起止流水號大于流水号位数,輸入有誤碼!", "提示")
            TextEdit1.Focus()
            Exit Sub
        End If
        If TextEdit2.Text.Length > txtVersion.Value Then
            MessageBox.Show("截止流水號大于流水号位数,輸入有誤碼!", "提示")
            TextEdit2.Focus()
            Exit Sub
        End If

        For i = m To n
            StrCode_ID = txt_PM_M_Code.Text.Trim + Mid(CStr(10000000000 + i), x)
            Dim row As DataRow = ds.Tables("SampleWareA").NewRow
            row("AutoID") = i
            row("PM_M_Code") = Me.txtM_Code.Text.Trim
            row("Code_ID") = StrConv(StrCode_ID, vbNarrow)
            row("Code_Qty") = 1
            If chkBitAuto.Checked Then
                row("BitAuto") = True
            Else
                row("BitAuto") = False
            End If
            ds.Tables("SampleWareA").Rows.Add(row)
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ds.Tables("SampleWareA").Rows.Count > 0 Then
        Else
            Exit Sub
        End If
        Dim j As Integer
        '---------------------------寄送输入条码时，订单条码不存在-----------------------------
        Select Case EditItem
            Case "SampleSend"
                For j = 0 To ds.Tables("SampleWareA").Rows.Count - 1
                    Dim strCode_ID As String = ds.Tables("SampleWareA").Rows(j)("Code_ID").ToString
                    If socon.SampleOrdersCode_GetID(strCode_ID) = False Then
                        GridControl1.Focus()
                        GridView2.FocusedRowHandle = j
                        MessageBox.Show(strCode_ID + "此編號订单条码不已存在!", "提示")
                        Exit Sub
                    End If
                Next
            Case "SampleTransaction"
                For j = 0 To ds.Tables("SampleWareA").Rows.Count - 1
                    Dim strCode_ID As String = ds.Tables("SampleWareA").Rows(j)("Code_ID").ToString

                    If sclcom.SampleCollection_GetID(strCode_ID) = False Then
                        GridControl1.Focus()
                        GridView2.FocusedRowHandle = j
                        MessageBox.Show(strCode_ID + "此編號條碼采集表不存在!", "提示")
                        Exit Sub
                    End If
                Next
        End Select
        '--------------------------------------------------------------------------------------
        Select Case EditItem
            Case "SampleSend"
                If scon.SampleSend_GetQty(SO_ID, SS_Edition) > Ncount + ds.Tables("SampleWareA").Rows.Count Then
                    If MsgBox("寄送的条码数量大于订单数量?，是否还保存", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
            Case "SampleOrders"
                'If SS_Qty < Ncount + ds.Tables("SampleWareA").Rows.Count Then
                '    MsgBox("新条码数量大于订单数量!")
                '    Exit Sub
                'End If
        End Select
        '--------------------------------------------------------------------------------------
        For j = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            Dim strCode_ID As String = ds.Tables("SampleWareA").Rows(j)("Code_ID").ToString
            Select Case EditItem
                Case "SampleSend"
                    If sscon.SampleSendCode_GetID(strCode_ID) Then
                        GridControl1.Focus()
                        GridView2.FocusedRowHandle = j
                        MsgBox(strCode_ID & ",此編號已存在!")
                        Exit Sub
                    End If
                Case "SampleOrders"
                    If sclcom.SampleCollection_GetID(strCode_ID) Then
                        GridControl1.Focus()
                        GridView2.FocusedRowHandle = j
                        MsgBox(strCode_ID & ",此編號採集表中已存在!")
                        Exit Sub
                    End If
                    'Case "SampleCollection"
                    '    If sclcom.SampleCollection_GetID(strCode_ID) Then
                    '        GridControl1.Focus()
                    '        GridView2.FocusedRowHandle = j
                    '        MsgBox(strCode_ID & ",此編號已存在!")
                    '        Exit Sub
                    '    End If
            End Select
        Next
        '--------------------------------------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            With ds.Tables("SampleWareA")
                Select Case EditItem
                    Case "SampleSend"
                        Dim objinfo As New SampleSendCodeInfo
                        objinfo.SO_ID = SO_ID
                        objinfo.SS_Edition = SS_Edition
                        objinfo.SP_ID = SP_ID
                        objinfo.PM_M_Code = ds.Tables("SampleWareA").Rows(i)("PM_M_Code")
                        objinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        objinfo.Code_Qty = ds.Tables("SampleWareA").Rows(i)("Code_Qty")
                        objinfo.AddUserID = InUserID
                        objinfo.AddDate = Format(Now, "yyyy/MM/dd")

                        If sscon.SampleSendCode_Add(objinfo) = False Then
                            MessageBox.Show("新增错误!", "提示")
                            Exit Sub
                        End If
                        '----------------------------------------------------------------------
                        Dim ssinfo As New SampleOrdersCodeInfo
                        ssinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        ssinfo.SP_ID = SP_ID
                        If socon.SampleOrdersCode_UpdateA(ssinfo) = False Then
                            MessageBox.Show("修改订单上面寄送单号错误!", "提示")
                            Exit Sub
                        End If
                    Case "SampleOrders"
                        Dim objinfo As New SampleCollectionInfo
                        objinfo.Remark = IIf(IsDBNull(ds.Tables("SampleWareA").Rows(i)("Remark")), Nothing, ds.Tables("SampleWareA").Rows(i)("Remark"))
                        objinfo.StatusType = String.Empty
                        objinfo.PM_M_Code = ds.Tables("SampleWareA").Rows(i)("PM_M_Code")
                        objinfo.PM_Type = gluM_Code.EditValue
                        objinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        objinfo.Qty = ds.Tables("SampleWareA").Rows(i)("Code_Qty")
                        objinfo.BitAuto = ds.Tables("SampleWareA").Rows(i)("BitAuto")
                        objinfo.AddUserID = InUserID
                        objinfo.AddDate = Format(Now, "yyyy/MM/dd")
                        objinfo.SO_ID = SO_ID
                        objinfo.SS_Edition = SS_Edition
                        objinfo.BarcodeType = "[a].內部条码"
                        If sclcom.SampleCollection_GetID(ds.Tables("SampleWareA").Rows(i)("Code_ID")) Then
                            objinfo.ModifyUserID = InUserID
                            objinfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                            If sclcom.SampleCollection_Update(objinfo) = False Then
                                MessageBox.Show("修改错误!", "提示")
                                Exit Sub
                            End If
                        Else
                            If sclcom.SampleCollection_Add(objinfo) = False Then
                                MessageBox.Show("新增错误!", "提示")
                                Exit Sub
                            End If
                        End If
                    Case "SampleCollection"
                        Dim objinfo As New SampleCollectionInfo
                        objinfo.Remark = IIf(IsDBNull(ds.Tables("SampleWareA").Rows(i)("Remark")), Nothing, ds.Tables("SampleWareA").Rows(i)("Remark"))
                        objinfo.StatusType = String.Empty
                        objinfo.PM_M_Code = ds.Tables("SampleWareA").Rows(i)("PM_M_Code")
                        objinfo.PM_Type = gluM_Code.EditValue
                        objinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        objinfo.Qty = ds.Tables("SampleWareA").Rows(i)("Code_Qty")
                        objinfo.BitAuto = ds.Tables("SampleWareA").Rows(i)("BitAuto")
                        objinfo.AddUserID = InUserID
                        objinfo.AddDate = Format(Now, "yyyy/MM/dd")
                        objinfo.SO_ID = String.Empty
                        objinfo.SS_Edition = String.Empty
                        objinfo.BarcodeType = "[a].內部条码"
                        If sclcom.SampleCollection_GetID(ds.Tables("SampleWareA").Rows(i)("Code_ID")) Then
                            objinfo.ModifyUserID = InUserID
                            objinfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                            If sclcom.SampleCollection_Update(objinfo) = False Then
                                MessageBox.Show("修改错误!", "提示")
                                Exit Sub
                            End If
                        Else
                            If sclcom.SampleCollection_Add(objinfo) = False Then
                                MessageBox.Show("新增错误!", "提示")
                                Exit Sub
                            End If
                        End If
                    Case "SampleTransaction"
                        Dim objinfo As New SampleTransactionInfo
                        objinfo.Remark = IIf(IsDBNull(ds.Tables("SampleWareA").Rows(i)("Remark")), Nothing, ds.Tables("SampleWareA").Rows(i)("Remark"))
                        objinfo.StatusType = String.Empty
                        objinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        objinfo.Qty = ds.Tables("SampleWareA").Rows(i)("Code_Qty")
                        objinfo.AddUserID = InUserID
                        objinfo.AddDate = Format(Now, "yyyy/MM/dd")
                        TransactionList.Add(objinfo)
                End Select
            End With
        Next
        '----------------------修改订单入库数量
        Select Case EditItem
            Case "SampleSend"
                MsgBox("保存成功!")
            Case "SampleOrders"
                If socon.SampleOrdersCode_UpdateB(SO_ID, SS_Edition, ds.Tables("SampleWareA").Rows.Count) = False Then
                    MessageBox.Show("修改订单入库数量!", "提示")
                    Exit Sub
                End If
                MsgBox("保存成功!")
            Case "SampleCollection"
                MsgBox("保存成功!")
            Case "SampleTransaction"
                MsgBox("轉入成功!")
        End Select
        Me.Close()
    End Sub

    '删除条码
    Private Sub cmdCODEDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCODEDel.Click
        If GridView2.RowCount > 0 Then
            Dim deltemp As String = ""
            deltemp = ds.Tables("SampleWareA").Rows(GridView2.FocusedRowHandle)("Code_ID").ToString
            ds.Tables("SampleWareA").Rows.RemoveAt(GridView2.FocusedRowHandle)
        End If
    End Sub

    Private Sub txtM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtM_Code.EditValueChanged
        'If Me.XtraTabControl1.SelectedTabPageIndex = 0 Then
        '    Me.txt_PM_M_Code.Focus()
        'End If
        'If Me.XtraTabControl1.SelectedTabPageIndex = 1 Then
        '    txtCode_ID.Focus()
        'End If

        On Error Resume Next
        Dim Spcon As New SampleProcessControl
        gluM_Code.Properties.DisplayMember = "Type3ID"
        gluM_Code.Properties.ValueMember = "Type3ID"
        gluM_Code.Properties.DataSource = Spcon.SampleProcessMain_GetList2(Nothing, sender.text)
    End Sub


    Private Sub gluM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluM_Code.EditValueChanged
        If Me.XtraTabControl1.SelectedTabPageIndex = 0 Then
            Me.txt_PM_M_Code.Focus()
        End If
        If Me.XtraTabControl1.SelectedTabPageIndex = 1 Then
            txtCode_ID.Focus()
        End If
    End Sub
#End Region

    Private Sub chkBitAuto_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBitAuto.EditValueChanged
        Dim BoolAuto As Boolean = False
        If chkBitAuto.Checked = True Then
            BoolAuto = True
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            With ds.Tables("SampleWareA")
                .Rows(i)("BitAuto") = BoolAuto
            End With
        Next
    End Sub

End Class
