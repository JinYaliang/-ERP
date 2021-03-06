Imports LFERP.Library.NmetalSampleManager.NmetalSampleSend
Public Class frmNmetalSampleSendShipFile
#Region "属性"
    Private _EditItem As String
    Private _EditAutoID As String
    Private _EditSP_ID As String
    Private _EditPM_M_Code As String
    Private _EditSP_Qty As Integer
    Property EditItem() As String
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property EditAutoID() As String
        Get
            Return _EditAutoID
        End Get
        Set(ByVal value As String)
            _EditAutoID = value
        End Set
    End Property
    Property EditSP_ID() As String
        Get
            Return _EditSP_ID
        End Get
        Set(ByVal value As String)
            _EditSP_ID = value
        End Set
    End Property
    Property EditPM_M_Code() As String
        Get
            Return _EditPM_M_Code
        End Get
        Set(ByVal value As String)
            _EditPM_M_Code = value
        End Set
    End Property
    Property EditSP_Qty() As Integer
        Get
            Return _EditSP_Qty
        End Get
        Set(ByVal value As Integer)
            _EditSP_Qty = value
        End Set
    End Property

    Dim ssfcon As New NmetalSampleSendShipFilesControl
#End Region

#Region "窗体载入"
    Private Sub frmSampleSendShipFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case EditItem
            Case EditEnumType.ADD
                Me.txtSP_ID.Text = EditSP_ID
                Me.txtPM_M_Code.Text = EditPM_M_Code
                Me.txtSP_Qty.Text = EditSP_Qty
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                GetObjectWeight("COM1", 9600)
            Case EditEnumType.EDIT
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                LoadData(EditAutoID)
                GetObjectWeight("COM1", 9600)
            Case EditEnumType.VIEW
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                LoadData(EditAutoID)
                Me.txtCO_ID.Enabled = False
                Me.txtPartName.Enabled = False
                Me.txtPM_M_Code.Enabled = False
                Me.txtBoxID.Enabled = False
                Me.txtCode_ID.Enabled = False
                Me.txtSP_Qty.Enabled = False
                Me.txtWeighing.Enabled = False
                Me.txtPalletID.Enabled = False
                Me.txtQPN.Enabled = False
                Me.txtLC.Enabled = False
                Me.cmdSave.Enabled = False
                Me.txtAddressee.Enabled = False
                Me.txtProject.Enabled = False
                Me.txtHandle.Enabled = False

            Case EditEnumType.CHECK
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                LoadData(EditAutoID)
        End Select
        Me.Text = Me.lblTitle.Text
    End Sub
#End Region

#Region "保存前檢查輸入數據是否正確"
    Private Function CheckSave() As Boolean
        Dim bo As Boolean = False
        CheckSave = True
        If Me.txtSP_ID.EditValue = String.Empty Then
            MsgBox("寄送单号不能為空,请输入！", MsgBoxStyle.Information, "提示")
            txtSP_ID.Focus()
            bo = True
        ElseIf Me.txtCO_ID.EditValue = Nothing Then
            MsgBox("公司代号不能为空,请输入！", MsgBoxStyle.Information, "提示")
            txtCO_ID.Focus()
            bo = True
        ElseIf Me.txtPartName.EditValue = Nothing Then
            MsgBox("产品名称不能为空,请输入！", MsgBoxStyle.Information, "提示")
            txtPartName.Focus()
            bo = True
        ElseIf Me.txtPM_M_Code.EditValue = Nothing Then
            MsgBox("产品编号不能为空,请输入！", MsgBoxStyle.Information, "提示")
            txtPM_M_Code.Focus()
            bo = True
        ElseIf Me.txtBoxID.EditValue = Nothing Then
            MsgBox("箱号不能為空,请输入！", MsgBoxStyle.Information, "提示")
            txtBoxID.Focus()
            bo = True
        ElseIf Me.txtCode_ID.EditValue = Nothing Then
            MsgBox("条码编号不能为空,请输入！", MsgBoxStyle.Information, "提示")
            txtCode_ID.Focus()
            bo = True
        ElseIf Me.txtAddressee.EditValue = Nothing Then
            MsgBox("客户收件人不能为空,请输入！", MsgBoxStyle.Information, "提示")
            txtAddressee.Focus()
            bo = True
        ElseIf Me.txtProject.EditValue = Nothing Then
            MsgBox("项目人不能为空,请输入！", MsgBoxStyle.Information, "提示")
            txtProject.Focus()
            bo = True
        ElseIf Me.txtHandle.EditValue = Nothing Then
            MsgBox("跟办人不能为空,请输入！", MsgBoxStyle.Information, "提示")
            txtHandle.Focus()
            bo = True
        ElseIf CDec(txtSP_Qty.EditValue).Equals(0) Then
            MsgBox("领料數量不能為0！", MsgBoxStyle.Information, "提示")
            txtSP_Qty.Focus()
            bo = True
        ElseIf CDec(txtWeighing.EditValue).Equals(0) Then
            MsgBox("可领料數量為0,不能领料！", MsgBoxStyle.Information, "提示")
            txtWeighing.Focus()
            bo = True
        End If

        If bo = True Then
            CheckSave = False
            Exit Function
        End If
    End Function
#End Region

#Region "加載數據"
    Private Sub LoadData(ByVal PO As String)
        Dim objlist As New List(Of NmetalSampleSendShipFilesInfo)
        objlist = ssfcon.NmetalSampleSendShipFiles_GetList(Nothing, EditAutoID, Nothing)
        If objlist.Count > 0 Then
            Me.txtSP_ID.EditValue = objlist(0).SP_ID
            Me.txtCO_ID.EditValue = objlist(0).CO_ID
            Me.txtPartName.EditValue = objlist(0).PartName
            Me.txtPM_M_Code.EditValue = objlist(0).PM_M_Code
            Me.txtBoxID.EditValue = objlist(0).BoxID
            Me.txtCode_ID.EditValue = objlist(0).Code_ID
            Me.txtSP_Qty.EditValue = objlist(0).SP_Qty
            Me.txtWeighing.EditValue = objlist(0).Weighing
            Me.txtPalletID.EditValue = objlist(0).PalletID
            Me.txtLC.EditValue = objlist(0).LC
            Me.txtQPN.EditValue = objlist(0).QPN
            Me.txtAddressee.EditValue = objlist(0).Addressee
            Me.txtProject.EditValue = objlist(0).Project
            Me.txtHandle.EditValue = objlist(0).Handle
        End If
    End Sub

    Private Sub SaveData(ByVal EditItem As String)
        Try
            Dim ssfinfo As New NmetalSampleSendShipFilesInfo
            ssfinfo.SP_ID = Me.txtSP_ID.EditValue
            ssfinfo.CO_ID = Me.txtCO_ID.EditValue
            ssfinfo.PartName = Me.txtPartName.EditValue
            ssfinfo.PM_M_Code = Me.txtPM_M_Code.EditValue
            ssfinfo.BoxID = Me.txtBoxID.EditValue
            ssfinfo.Code_ID = Me.txtCode_ID.EditValue
            ssfinfo.SP_Qty = CInt(Me.txtSP_Qty.EditValue)
            ssfinfo.Weighing = CDbl(Me.txtWeighing.EditValue)
            ssfinfo.PalletID = Me.txtPalletID.EditValue
            ssfinfo.LC = Me.txtLC.EditValue
            ssfinfo.QPN = Me.txtQPN.EditValue

            ssfinfo.Addressee = Me.txtAddressee.EditValue
            ssfinfo.Project = Me.txtProject.EditValue
            ssfinfo.Handle = Me.txtHandle.EditValue

            If editItem = EditEnumType.ADD Then
                ssfinfo.AddUserID = InUserID
                ssfinfo.AddDate = Format(Now, "yyyy/MM/dd")
                If ssfcon.NmetalSampleSendShipFiles_Add(ssfinfo) = True Then
                    MsgBox("添加成功", MsgBoxStyle.Information, "提示")
                Else
                    MsgBox("添加失敗", MsgBoxStyle.Information, "提示")
                End If
            ElseIf EditItem = EditEnumType.EDIT Then
                ssfinfo.AutoID = EditAutoID
                ssfinfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                ssfinfo.ModifyUserID = InUserID

                If ssfcon.NmetalSampleSendShipFiles_Update(ssfinfo) = True Then
                    MsgBox("修改成功", MsgBoxStyle.Information, "提示")
                Else
                    MsgBox("修改失敗", MsgBoxStyle.Information, "提示")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "按键事件"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If (CheckSave() = False) Then
            Exit Sub
        End If
        Select Case EditItem
            Case EditEnumType.ADD
                SaveData(EditEnumType.ADD)
            Case EditEnumType.EDIT
                SaveData(EditEnumType.EDIT)
            Case EditEnumType.CHECK
                'Dim sbi As New SampleBorrowInfo
                'sbi.BorrowID = txt_BorrowID.EditValue
                'sbi.CheckBit = ce_IsCheck.Checked
                'sbi.CheckUserID = InUserID
                'If sbc.SampleBorrow_UpdateCheck(sbi) = True Then
                '    MsgBox("審核狀態修改成功", MsgBoxStyle.Information, "提示")
                'Else
                '    MsgBox("審核狀態修改失敗", MsgBoxStyle.Information, "提示")
                'End If
        End Select
        Me.Close()
    End Sub
#End Region

#Region "称重程序"
    Dim comm As New System.IO.Ports.SerialPort
    Private Timer1 As New Timer
    Dim buf = New Byte() {}
    Dim strBuf As String = String.Empty

    Function GetObjectWeight(ByVal PortName As String, ByVal BaudRate As Integer) As Boolean
        '1.时间控件
        AddHandler Timer1.Tick, AddressOf Timer1_Tick '时间控件
        Timer1.Enabled = True
        Timer1.Interval = 10
        '2.COM控件事件
        AddHandler comm.DataReceived, AddressOf comm_DataReceived 'COM控件事件
        If comm.IsOpen Then
            Try
                comm.Close()
            Catch
                MessageBox.Show("关闭错误")
            End Try
        Else
            'comm.PortName = "COM1"
            'comm.BaudRate = 9600
            comm.PortName = PortName
            comm.BaudRate = BaudRate
            comm.DataBits = 8
            comm.StopBits = System.IO.Ports.StopBits.One
            Try
                comm.Open()
                comm.NewLine = "/r/n"
            Catch
                'MessageBox.Show("打开错误")
            End Try
        End If
        Return True
    End Function

    Function CommWeight(ByVal txtWeight As DevExpress.XtraEditors.TextEdit) As String
        Dim strCommWeight As String = String.Empty
        Try
            Me.Invoke(New InvokeDelegate(AddressOf InvokeMethod))

            Dim StrA As String = String.Empty
            Dim StrB As String = String.Empty
            Dim StrC As String = String.Empty
            Dim IntA As Integer = 0
            Dim IntB As Integer = 0
            Dim IntC As Integer = 0
            If strBuf.Split.Length > 5 Then
                For i As Integer = 0 To strBuf.Split.Length - 1
                    If strBuf.Split()(i) = "WTST" Then
                        IntA = 1
                        StrA = strBuf.Split()(i)
                    End If
                    If IntA = 1 Then
                        If strBuf.Split()(i).StartsWith("+") Then
                            StrB = strBuf.Split()(i)
                            IntB = 1
                        End If
                    End If
                    If IntB = 1 Then
                        If strBuf.Split()(i) = "g" Then
                            StrC = strBuf.Split()(i)
                            IntC = 1
                        End If
                    End If
                    If IntA + IntB + IntC = 3 Then
                        If CDbl(StrB.Substring(1)) = 0 Then
                            txtWeight.Text = "0"
                        Else
                            txtWeight.Text = StrB.Substring(1)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch

        End Try

        Return strCommWeight
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommWeight(txtWeighing)

    End Sub

    Private Sub comm_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        Try
            Dim n As Integer = comm.BytesToRead
            buf = New Byte(n) {}
            If n > 20 Then
                comm.Read(buf, 0, n)
            End If
        Catch

        End Try
    End Sub

    Delegate Sub DelegateSetDataSource()
    Private Delegate Sub InvokeDelegate()

    Private Sub InvokeMethod()
        Try
            strBuf = String.Empty
            Dim DegDataSource As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(DegDataSource)
        Catch
        End Try
    End Sub

    Private Sub SetControlDataSource()
        Try
            If IsDBNull(buf) = False Then
                strBuf = System.Text.Encoding.ASCII.GetString(buf)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "称重错误")
        End Try
    End Sub
#End Region

    Private Sub frmSampleSendShipFile_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If comm.IsOpen Then
            Try
                comm.Close()
                Me.Dispose(True)
            Catch
                MessageBox.Show("关闭错误")
            End Try
        End If
    End Sub

End Class