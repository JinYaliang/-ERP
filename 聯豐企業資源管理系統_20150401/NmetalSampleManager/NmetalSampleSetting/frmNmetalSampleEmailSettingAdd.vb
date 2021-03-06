Imports System.Text.RegularExpressions
Imports LFERP.Library.NmetalSampleManager.NmetalSampleEmailSetting
Imports LFERP.SystemManager.SystemUser
Public Class frmNmetalSampleEmailSettingAdd
    Dim pmeiCon As New NmetalSampleEmailSettingController
    Dim pmeiList As New List(Of NmetalSampleEmailSettingInfo)
    ''' <summary>
    ''' 屬性
    ''' </summary>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' 加載
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub EmailSettingAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim suc As New SystemUserController
        Dim suiList As New List(Of SystemUserInfo)
        Dim sui As New SystemUserInfo
        suiList = suc.SystemUser_GetList(Nothing, Nothing, Nothing)
        suiList.Insert(0, sui)
        gluU_ID.Properties.DisplayMember = "U_Name"
        gluU_ID.Properties.ValueMember = "U_ID"
        gluU_ID.Properties.DataSource = suiList


        If EditItem = "Add" Then
            gluU_ID.Enabled = True
            speCheckOrder.Enabled = True
        Else
            gluU_ID.Enabled = False
            speCheckOrder.Enabled = False

            pmeiList = pmeiCon.NmetalSampleEmailSetting_GetList(EditValue, Nothing, Nothing, Nothing, Nothing)
            gluU_ID.Text = pmeiList(0).Email_UserID
            txtEmail.Text = pmeiList(0).Email
            speCheckOrder.Text = pmeiList(0).CheckOrder
            txtStateAlarmTime.Text = pmeiList(0).StateAlarmTime
            txtEndAlarmTime.Text = pmeiList(0).EndAlarmTime
            memoRemark.Text = pmeiList(0).Remark

            '2014-04-09  姚駿
            txtStoreTime.Text = pmeiList(0).StoreTime

            '2014-08-13     Mark
            txt_StoreCyCleTime.Text = pmeiList(0).StoreCycleTime
            txtCypleTime.Text = pmeiList(0).CycleTime


        End If

        '操作日志 2014.4.21 劉祥松
        'G_LogInfo.FormName = Me.Text
        'G_LogInfo.OperationType = "進入"
        'InsertOperationLog()
    End Sub

    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If CheckDate() Then
        Else
            Exit Sub
        End If
        Dim pmei As New NmetalSampleEmailSettingInfo
        If EditItem = "Add" Then
            pmei.Email_UserID = gluU_ID.EditValue
            pmei.Email_UserName = gluU_ID.Text
            pmei.Email = txtEmail.Text
            pmei.CheckOrder = CInt(speCheckOrder.Text)
            pmei.StateAlarmTime = txtStateAlarmTime.Text
            pmei.EndAlarmTime = txtEndAlarmTime.Text
            pmei.CreateUserID = InUserID
            pmei.CreateDate = Format(System.DateTime.Now, "yyyy/MM/dd")
            pmei.Remark = memoRemark.Text


            '2014-04-18 姚駿
            pmei.StoreTime = txtStoreTime.Text
            pmei.CycleTime = txtCypleTime.Text

            '2014-08-13     Mark
            pmei.StoreCycleTime = txt_StoreCyCleTime.Text

            If pmeiCon.NmetalSampleEmailSetting_Add(pmei) Then
                MsgBox("保存成功!", , "提示")
                Me.Close()
            Else
                MsgBox("保存失敗，請檢查原因!", , "提示")
                Me.Close()
            End If

        Else
            pmei.AutoID = EditValue
            pmei.Email_UserID = gluU_ID.EditValue
            pmei.Email_UserName = gluU_ID.Text
            pmei.Email = txtEmail.Text
            pmei.CheckOrder = CInt(speCheckOrder.Text)
            pmei.StateAlarmTime = txtStateAlarmTime.Text
            pmei.EndAlarmTime = txtEndAlarmTime.Text
            pmei.ModifyUserID = InUserID
            pmei.ModifyDate = Format(System.DateTime.Now, "yyyy/MM/dd")
            pmei.Remark = memoRemark.Text

            '2014-04-18 姚駿
            pmei.StoreTime = txtStoreTime.Text
            pmei.CycleTime = txtCypleTime.Text

            '2014-08-13     Mark
            pmei.StoreCycleTime = txt_StoreCyCleTime.Text

            If pmeiCon.NmetalSampleEmailSetting_Update(pmei) Then
                MsgBox("保存成功!", , "提示")
                Me.Close()
            Else
                MsgBox("保存失敗，請檢查原因!", , "提示")
                Me.Close()
            End If
        End If
    End Sub
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Me.Close()
    End Sub

    'Private Sub chkIsFinalUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If sender.checked Then
    '        lblInfo.Visible = True
    '    Else
    '        lblInfo.Visible = False
    '    End If
    'End Sub
    ''' <summary>
    ''' 檢查是否有數據錯誤
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckDate() As Boolean
        'If gluU_ID.Text = String.Empty Then
        '    MsgBox("用戶編號不能為空!", , "提示")
        '    gluU_ID.Focus()
        '    Return False
        'End If

        If txtEmail.Text = String.Empty Then
            MsgBox("郵箱地址不能為空!", , "提示")
            txtEmail.Focus()
            Return False
        End If

        If EditItem = "Add" Then
            If CheckUserOrder() Then
                MsgBox("該郵件与邮件顺序已經存在，請檢查!", , "提示")
                txtEmail.Focus()
                Return False
            End If
        End If

        If Val(txtStateAlarmTime.Text) >= Val(txtEndAlarmTime.Text) Then
            MsgBox("开始报警不能大于等於截止报警!", , "提示")
            txtEndAlarmTime.Focus()
            Return False
        End If

        If ValidateEmail(txtEmail.Text) = False Then
            MsgBox("郵箱格式不對!", , "提示")
            txtEmail.Focus()
            Return False
        End If


        '2014-04-30  姚駿
        If txtEmail.Text = "mis@mail.megapt.com.cn" Then
            If String.IsNullOrEmpty(memoRemark.Text) Then
                MsgBox("手机号码不能为空!", , "提示")
                memoRemark.Focus()
                Return False
            End If
        End If

        Return True
    End Function
    ''' <summary>
    ''' 驗證審核順序是否重複
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckUserOrder() As Boolean
        Dim strEmail As String = Me.txtEmail.EditValue
        Dim intCheckOrder As Integer = CInt(speCheckOrder.Text)
        pmeiList = pmeiCon.NmetalSampleEmailSetting_GetList(Nothing, Nothing, strEmail, intCheckOrder, Nothing)
        If pmeiList.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 驗證郵箱格式
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateEmail(ByVal str As String) As Boolean
        Return Regex.IsMatch(str.Trim, "^([a-z0-9A-Z]+[-|\.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\.)+[a-zA-Z]{2,}$")
    End Function

    Private Sub frmSampleEmailSettingAdd_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        '操作日志 2014.4.21 劉祥松
        'G_LogInfo.FormName = Me.Text
        'UpdateOperationLog()
    End Sub

    ''' <summary>
    ''' 2014-04-23
    ''' 姚      駿
    ''' 手機號碼限制只能輸入數字
    ''' 最大位數(11)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub memoRemark_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles memoRemark.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class