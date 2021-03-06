Imports LFERP.Library.ProductionException
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePaceTypeBriName

Public Class frmNmetalSampleException

    Dim ec As New ProductionExceptionControl
    Dim nsptb As New NmetalSamplePaceTypeBriNameControl
    Private _EditItem As String '属性栏位   2014-08-22  Mark
    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Private Sub frmException_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '2014-08-22     Mark
        Select Case EditItem
            Case EditEnumType.ELSEONE
                PanelControl1.Visible = True
                PanelControl2.Visible = False
            Case EditEnumType.ELSETWO               '2014-08-22     Mark
                PanelControl2.Visible = True
                PanelControl1.Visible = False
        End Select
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        Select Case EditItem
            Case EditEnumType.ELSEONE
                Dim ei As New ProductionExceptionInfo
                ei.PE_CardID = TextEdit1.Text
                ei.PE_CardName = TextEdit2.Text
                ei.PE_Date = Now
                ei.PE_User = InUserID
                ei.PE_Remark = MemoEdit1.Text
                If TextEdit1.Text.Trim = "" Or TextEdit2.Text.Trim = "" Then
                    MsgBox("工號，姓名不能為空！")
                    Exit Sub
                End If
                If ec.ProductionException_Add(ei) = True Then
                    tempValue = ei.PE_CardID & "-" & ei.PE_CardName
                End If
                Me.Close()
            Case EditEnumType.ELSETWO               '2014-08-22 Mark
                Dim bc As New NmetalSamplePaceTypeBriNameControl
                Dim bL As New List(Of NmetalSamplePaceTypeBriNameInfo)
                bL = nsptb.NmetalSampleExceptionUser_GetList(TextEdit1.Text, Nothing, Nothing)
                If bL.Count > 0 Then
                    If bL(0).PE_Type = "W" Then
                        tempValue = bL(0).PE_User & "-" & bL(0).PE_Name
                    Else
                        MessageBox.Show("此用户不属于称重异常的用户！", "提示")
                    End If
                Else
                    tempValue = ""
                End If
                Me.Close()
        End Select

    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub


    Private Sub TextEdit1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextEdit1.KeyPress
        Select Case EditItem
            Case EditEnumType.ELSEONE
                If e.KeyChar = Chr(13) Then
                    TextEdit2.Text = GetName(TextEdit1.Text)
                    TextEdit2.Enabled = False
                    MemoEdit1.Focus()
                End If
            Case EditEnumType.ELSETWO           '2014-08-22     Mark
                If e.KeyChar = Chr(13) Then
                    TextEdit2.Text = GetNames(TextEdit1.Text, Nothing, Nothing)
                    TextEdit2.Enabled = False
                    MemoEdit1.Focus()
                End If
        End Select
    End Sub
    ''' <summary>
    ''' 2014-08-22
    ''' 返回值 
    ''' </summary>
    ''' <param name="strCardID"></param>
    ''' <param name="PE_Name"></param>
    ''' <param name="PE_Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNames(ByVal strCardID As String, ByVal PE_Name As String, ByVal PE_Type As String) As String
        Dim bc As New NmetalSamplePaceTypeBriNameControl
        Dim bL As New List(Of NmetalSamplePaceTypeBriNameInfo)
        bL = nsptb.NmetalSampleExceptionUser_GetList(TextEdit1.Text, Nothing, Nothing)

        If bL.Count > 0 Then
            GetNames = bL(0).PE_Name
        Else
            GetNames = ""
        End If
    End Function

    '權限用戶登陸
    Private Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        'Dim ei As List(Of ProductionExceptionInfo)
        'ei = ec.ProductionExceptionUser_GetList(txt_User.Text, txt_password.Text)
        Dim ex As List(Of NmetalSamplePaceTypeBriNameInfo)
        ex = nsptb.NmetalSampleExceptionUser_GetList(txt_User.Text, txt_password.Text, Nothing)
        If ex.Count > 0 Then
            If ex(0).PE_Type = "C" Then         '2014-08-21 Mark
                PanelControl1.Visible = False
                PanelControl2.Visible = True
                TextEdit1.Focus()
            Else
                MessageBox.Show("此用户不属于刷卡异常的用户！", "提示")
            End If
        Else
            PanelControl1.Visible = True
            PanelControl2.Visible = False
            MsgBox("當前用戶不存在此權限或密碼錯誤，請重新輸入！")
            txt_password.Text = ""
            txt_password.Focus()
            Exit Sub
        End If

    End Sub

    Private Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub

    Private Sub MemoEdit1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MemoEdit1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SimpleButton1_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub txt_User_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_User.KeyPress
        If e.KeyChar = Chr(13) Then

            lbl_Name.Text = GetName(txt_User.Text)
            lbl_Name.Visible = True
            txt_password.Focus()
        End If
    End Sub


    Private Sub TextEdit4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_password.KeyPress
        If e.KeyChar = Chr(13) Then

            btn_save_Click(Nothing, Nothing)

        End If
    End Sub

End Class