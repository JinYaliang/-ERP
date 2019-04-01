Imports LFERP.Library.KnifeWare
Imports LFERP.SystemManager

Public Class frmWhiteUserSub

#Region "�r�q�ݩ�"

    Private _WHID As String             ''��������ܮw��ID
    Private _EditType As String         ''�P�_�O�s�W�B�ק��٬O�d��
    Private _userID As String           ''����զW�檺�u��
    Private _WareType As String         ''����ܮw����
    Private _isEdit As Boolean = False  ''�T�w�O�_��Y�@��Ʀ����
    Private _isNew As Boolean = False   ''�T�w�O�_�K�[���
    Private _AutoID As String

    Public Property AutoID() As String
        Get
            Return _AutoID
        End Get
        Set(ByVal value As String)
            _AutoID = value
        End Set
    End Property

    Public Property WHID() As String
        Get
            Return _WHID
        End Get
        Set(ByVal value As String)
            _WHID = value
        End Set
    End Property

    Public Property EditType() As String
        Get
            Return _EditType
        End Get
        Set(ByVal value As String)
            _EditType = value
        End Set
    End Property

    Public Property userID() As String
        Get
            Return _userID
        End Get
        Set(ByVal value As String)
            _userID = value
        End Set
    End Property

    Public Property WareType() As String
        Get
            Return _WareType
        End Get
        Set(ByVal value As String)
            _WareType = value
        End Set
    End Property

    Public Property isEdit() As Boolean
        Get
            Return _isEdit
        End Get
        Set(ByVal value As Boolean)
            _isEdit = value
        End Set
    End Property

    Public Property isNew() As Boolean
        Get
            Return _isNew
        End Get
        Set(ByVal value As Boolean)
            _isNew = value
        End Set
    End Property

    Dim FindKaoQing As Boolean = False

#End Region



    Private Sub frmWhiteUserSub_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510919")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then FindKaoQing = True
        End If

        If FindKaoQing = True Then
            txt_UserName.Enabled = True
            txt_FacName.Enabled = True
            txtDepName.Enabled = True
        End If

        ''------�H�W2014-02-14---------------------------------------------------------------------


        Dim wuc As New KnifeWhiteUserController
        Dim fr As New frmWhiteUser
        Dim winfo As List(Of KnifeWhiteUserInfo)

        If EditType = "newAdd" Then
            lblMain.Text = "�զW��--�s�W"
            txt_WH_ID.Text = WHID
            lblWType.Text = WareType
            txt_WMax.Text = 0
        Else
            lblMain.Text = "�զW��--�ק�"
            cmdNew.Enabled = False
            txt_UserID.Enabled = False
            winfo = wuc.WhiteUser_GetList(AutoID, Nothing, Nothing, Nothing, Nothing, False)


            txt_UserID.Text = winfo(0).W_UserID
            txt_UserName.Text = winfo(0).W_UserName
            txt_WMax.Text = winfo(0).WMax
            txt_FacName.Text = winfo(0).FacName
            txtDepName.Text = winfo(0).DepName
            txt_Remark.Text = winfo(0).W_Remark
            txt_WH_ID.Text = winfo(0).WH_ID
            lblWType.Text = winfo(0).WType
            txt_AutoID.Text = winfo(0).AutoID
        End If

    End Sub

    Private Sub txt_UserID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_UserID.KeyDown

        If e.KeyCode = Keys.Enter Then

            If txt_UserID.Text.Trim = Nothing Then
                MsgBox("�п�J�u���I", 64, "����")
                Exit Sub
            Else
                Dim wuc As New KnifeWhiteUserController
                Dim fr As New frmWhiteUser
                Dim winfo As List(Of KnifeWhiteUserInfo)

                winfo = wuc.WhiteUser_GetList(Nothing, txt_UserID.Text, Nothing, Nothing, Nothing, False)
                If winfo.Count = 0 Then
                    EditType = "newAdd"
                    txt_WMax.Text = 0
                    txt_Remark.Text = ""

                    If FindKaoQing = False Then '2014-02-14 ���s�Ҷ�
                        Call GetNameDep(txt_UserID.Text)


                        If strKQdata = "haveData" Then
                            txt_UserName.Text = strKQName
                            txt_FacName.Text = strKQFacName
                            txtDepName.Text = strKQDepName
                        Else
                            MsgBox("���s�b�����u�����Τ�A�Э��s��J�I", 64, "����")
                            txt_UserID.Text = ""
                            Exit Sub
                        End If

                    End If
                Else
                    MsgBox("�ӥΤ�b�ܮw: " + winfo(0).WH_Name + " ���w�g�s�b�I", 64, "����")
                    EditType = "update"
                    lblMain.Text = "�զW��--�ק�"
                    cmdNew.Enabled = False
                    txt_UserID.Enabled = False
                    txt_UserName.Text = winfo(0).W_UserName
                    txt_WMax.Text = winfo(0).WMax
                    txt_FacName.Text = winfo(0).FacName
                    txtDepName.Text = winfo(0).DepName
                    txt_Remark.Text = winfo(0).W_Remark
                    txt_WH_ID.Text = winfo(0).WH_ID
                    lblWType.Text = winfo(0).WH_Name
                    txt_AutoID.Text = winfo(0).AutoID
                End If
                txt_WMax.Focus()
                End If
        End If

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        isEdit = False
        Me.Close()

    End Sub

    Function CheckSave() As Boolean

        CheckSave = True

        If txt_UserID.Text.Trim = Nothing Then
            MsgBox("�п�J�u���I", 64, "����")
            CheckSave = False
            Exit Function
        ElseIf txt_UserName.Text.Trim = Nothing Then
            MsgBox("��J�u���Z�Ы��UEnter�I", 64, "����")
            CheckSave = False
            Exit Function
        ElseIf CDbl(txt_WMax.Text) < 0 Then
            MsgBox("�i��W�����~�A���ˬd�I", 64, "����")
            CheckSave = False
            Exit Function
        End If

        '2013-11-21
        Dim wuc As New KnifeWhiteUserController
        Dim winfo As New List(Of KnifeWhiteUserInfo)
        winfo = wuc.WhiteUser_GetList(Nothing, txt_UserID.Text, Nothing, "�M���", Nothing, Nothing)

        If winfo.Count > 0 Then
            MsgBox("��e���u�w�b," & winfo(0).WH_Name & "�s�b�զW��.", 64, "����")
            CheckSave = False
            Exit Function
        End If


    End Function

    Private Sub cmdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNew.Click

        Dim wuc As New KnifeWhiteUserController
        Dim winfo As New KnifeWhiteUserInfo

        If CheckSave() = True Then
            winfo.W_UserID = txt_UserID.Text
            winfo.W_UserName = txt_UserName.Text
            winfo.WMax = txt_WMax.Text
            winfo.FacName = txt_FacName.Text
            winfo.DepName = txtDepName.Text
            winfo.WType = "�M���"
            winfo.W_Remark = txt_Remark.Text
            winfo.DPT_ID = ""
            winfo.WH_ID = txt_WH_ID.Text
            winfo.GNO = txt_GNO.Text
            If wuc.WhiteUser_Add(winfo) = True Then
                MsgBox("�s�W���\�I", 64, "����")
                txt_UserID.Text = ""
                txt_WMax.Text = ""
                txt_Remark.Text = ""
                txt_UserName.Text = ""
                txt_FacName.Text = ""
                txtDepName.Text = ""
                txt_UserID.Focus()
                isNew = True
            Else
                MsgBox("�s�W���ѡA���ˬd��]�I", 64, "����")
            End If
        Else
            Exit Sub
        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim wuc As New KnifeWhiteUserController
        Dim winfo As New KnifeWhiteUserInfo

        Select Case EditType
            Case "newAdd"
                If CheckSave() = True Then

                    winfo.W_UserID = txt_UserID.Text
                    winfo.W_UserName = txt_UserName.Text
                    winfo.WMax = txt_WMax.Text
                    winfo.FacName = txt_FacName.Text
                    winfo.DepName = txtDepName.Text
                    winfo.WType = "�M���"
                    winfo.W_Remark = txt_Remark.Text
                    winfo.DPT_ID = ""
                    winfo.WH_ID = txt_WH_ID.Text
                    winfo.GNO = txt_GNO.Text
                    If wuc.WhiteUser_Add(winfo) = True Then
                        isNew = True
                    Else
                        MsgBox("�s�W���ѡA���ˬd��]�I", 64, "����")
                    End If
                Else
                    Exit Sub
                End If
            Case "update"
                If wuc.WhiteUser_Update(txt_AutoID.Text, txt_Remark.Text, txt_WMax.Text) = True Then
                    isEdit = True
                    MsgBox("�ק令�\!", 64, "����")
                Else
                    MsgBox("�O�s���ѡA���ˬd��]�I", 64, "����")
                End If
        End Select
        Me.Close()

    End Sub

    Private Sub txt_WMax_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_WMax.KeyDown

        If e.KeyCode = Keys.Enter Then
            txt_Remark.Focus()
        End If

    End Sub

    Private Sub txt_Remark_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_Remark.KeyDown

        If e.KeyCode = Keys.Enter Then
            If EditType = "newAdd" Then
                cmdNew.Focus()
            Else
                cmdSave.Focus()
            End If
        End If

    End Sub

    Private Sub FindButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindButton.Click
        If txt_UserID.Text = "" Then
            MsgBox("�п�J�u���I")
            txt_UserID.Select()
            Exit Sub
        End If

        Call GetNameDep(txt_UserID.Text)

        If strKQdata = "haveData" Then
            txt_UserName.Text = strKQName
            txt_FacName.Text = strKQFacName
            txtDepName.Text = strKQDepName
        Else
            MsgBox("���s�b�����u�����Τ�A�Э��s��J�I", 64, "����")
            txt_UserID.Text = ""
            txt_UserName.Text = ""
            txt_FacName.Text = ""
            txtDepName.Text = ""
            Exit Sub
        End If
    End Sub
End Class