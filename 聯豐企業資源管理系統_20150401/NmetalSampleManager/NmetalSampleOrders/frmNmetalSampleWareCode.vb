Imports LFERP.Library.NmetalSampleManager.NmetalSampleSend
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrders

Public Class frmNmetalSampleWareCode
    Private sscon As New NmetalSampleSendCodeControler
    Private socon As New NmetalSampleOrdersCodeControler
    Private scon As New NmetalSampleSendControler
    Private Ncount As Integer

#Region "属性"
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
        txt_SO_ID.Text = SO_ID
        txtSS_Edition.Text = SS_Edition
        txt_PM_M_Code.Enabled = False
        TextEdit2.Text = SS_Qty
        TextEdit1.Focus()
        TextEdit1.Select()
        If Me.SP_ID = String.Empty Then
            Ncount = socon.NmetalSampleOrdersCode_GetCount(SO_ID, SS_Edition)
            Label6.Text = Ncount
            Me.XtraTabControl1.SelectedTabPage = XtraTabPage1
        Else
            Ncount = sscon.NmetalSampleSendCode_GetCount(SO_ID, SS_Edition)
            Label6.Text = Ncount
            Me.XtraTabControl1.SelectedTabPage = XtraTabPage2
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
        '-------------------------------------------------------
        Select Case EditItem
            Case "SampleSend"
                If socon.NmetalSampleOrdersCode_GetID(txtCode_ID.Text) = False Then
                    txtCode_ID.Text = String.Empty
                    txtCode_ID.Focus()
                    MsgBox(txtCode_ID.Text & "此編號订单条码不已存在!,无法输入")
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
                MessageBox.Show("采集" + ds.Tables("SampleWareA").Rows(I)("Code_ID").ToString() + "条码存在，请重输入!", "提示")
                Exit Sub
            End If
        Next

        If txtCode_ID.Text <> String.Empty Then
            Dim row As DataRow = ds.Tables("SampleWareA").NewRow
            row("PM_M_Code") = txt_PM_M_Code.Text
            row("Code_ID") = txtCode_ID.Text
            row("Code_Qty") = 1
            ds.Tables("SampleWareA").Rows.Add(row)
        End If

        txtCode_ID.Text = String.Empty
        txtCode_ID.Focus()
    End Sub
#End Region

#Region "按键事件"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    ''' <summary>
    '''  产生自动条码   
    ''' </summary>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i, n, m As Integer
        Dim StrCode_ID As String
        ds.Tables("SampleWareA").Clear()

        If TextEdit1.Text = String.Empty Then
            MessageBox.Show("起止條碼不能為空!")
            TextEdit1.Focus()
            Exit Sub
        End If
        If TextEdit2.Text = String.Empty Then
            MessageBox.Show("起止條碼不能為空!")
            TextEdit1.Focus()
            Exit Sub
        End If

        If Val(TextEdit1.Text) > Val(TextEdit2.Text) Then
            MessageBox.Show("起止條碼,輸入有誤碼!")
            TextEdit1.Focus()
            Exit Sub
        End If

        m = CInt(TextEdit1.Text)
        n = CInt(TextEdit2.Text)
        For i = m To n
            StrCode_ID = txt_PM_M_Code.Text.Trim + "-" + Mid(CStr(100000 + i), 2)
            Dim row As DataRow = ds.Tables("SampleWareA").NewRow
            row("AutoID") = i
            row("PM_M_Code") = txt_PM_M_Code.Text.Trim
            row("Code_ID") = StrCode_ID
            row("Code_Qty") = 1
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
                    If socon.NmetalSampleOrdersCode_GetID(strCode_ID) = False Then
                        GridControl1.Focus()
                        GridView2.FocusedRowHandle = j
                        MessageBox.Show(strCode_ID + "此編號订单条码不已存在!", "提示")
                        Exit Sub
                    End If
                Next
        End Select
        '--------------------------------------------------------------------------------------
        Select Case EditItem
            Case "SampleSend"
                If scon.NmetalSampleSend_GetQty(SO_ID, SS_Edition) > Ncount + ds.Tables("SampleWareA").Rows.Count Then
                    If MsgBox("寄送的条码数量大于订单数量?，是否还保存", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Exit Sub
                    End If
                End If
            Case "SampleOrders"
                If SS_Qty < Ncount + ds.Tables("SampleWareA").Rows.Count Then
                    MsgBox("新条码数量大于订单数量!")
                    Exit Sub
                End If
        End Select
        '--------------------------------------------------------------------------------------
        For j = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            Dim strCode_ID As String = ds.Tables("SampleWareA").Rows(j)("Code_ID").ToString
            Select Case EditItem
                Case "SampleSend"
                    If sscon.NmetalSampleSendCode_GetID(strCode_ID) Then
                        GridControl1.Focus()
                        GridView2.FocusedRowHandle = j
                        MsgBox(strCode_ID & ",此編號已存在!")
                        Exit Sub
                    End If
                Case "SampleOrders"
                    If socon.NmetalSampleOrdersCode_GetID(strCode_ID) Then
                        GridControl1.Focus()
                        GridView2.FocusedRowHandle = j
                        MsgBox(strCode_ID & ",此編號已存在!")
                        Exit Sub
                    End If
            End Select
        Next
        '--------------------------------------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            With ds.Tables("SampleWareA")
                Select Case EditItem
                    Case "SampleSend"
                        Dim objinfo As New NmetalSampleSendCodeInfo
                        objinfo.SO_ID = SO_ID
                        objinfo.SS_Edition = SS_Edition
                        objinfo.SP_ID = SP_ID
                        objinfo.PM_M_Code = ds.Tables("SampleWareA").Rows(i)("PM_M_Code")
                        objinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        objinfo.Code_Qty = ds.Tables("SampleWareA").Rows(i)("Code_Qty")
                        objinfo.AddUserID = InUserID
                        objinfo.AddDate = Format(Now, "yyyy/MM/dd")

                        If sscon.NmetalSampleSendCode_Add(objinfo) = False Then
                            MessageBox.Show("新增错误!", "提示")
                            Exit Sub
                        End If
                        '----------------------------------------------------------------------
                        Dim ssinfo As New NmetalSampleOrdersCodeInfo
                        ssinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        ssinfo.SP_ID = SP_ID
                        If socon.NmetalSampleOrdersCode_UpdateA(ssinfo) = False Then
                            MessageBox.Show("修改订单上面寄送单号错误!", "提示")
                            Exit Sub
                        End If
                    Case "SampleOrders"
                        Dim objinfo As New NmetalSampleOrdersCodeInfo
                        objinfo.SO_ID = SO_ID
                        objinfo.SS_Edition = SS_Edition
                        objinfo.SP_ID = SP_ID
                        objinfo.PM_M_Code = ds.Tables("SampleWareA").Rows(i)("PM_M_Code")
                        objinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
                        objinfo.Code_Qty = ds.Tables("SampleWareA").Rows(i)("Code_Qty")
                        objinfo.AddUserID = InUserID
                        objinfo.AddDate = Format(Now, "yyyy/MM/dd")

                        If socon.NmetalSampleOrdersCode_Add(objinfo) = False Then
                            MessageBox.Show("新增错误!", "提示")
                            Exit Sub
                        End If
                End Select
            End With
        Next
        '----------------------修改订单入库数量
        Select Case EditItem
            Case "SampleOrders"
                If socon.NmetalSampleOrdersCode_UpdateB(SO_ID, SS_Edition, ds.Tables("SampleWareA").Rows.Count) = False Then
                    MessageBox.Show("修改订单入库数量!", "提示")
                    Exit Sub
                End If
        End Select
        MsgBox("保存成功!")
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
#End Region
End Class
