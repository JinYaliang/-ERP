Imports LFERP.DataSetting
Imports LFERP.SystemManager


Public Class frmBriName

    Dim dc As New DepartmentControler
    Dim sc As New SystemManager.SystemUser.SystemUserController


    Private Sub frmBriName_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Label4.Text = tempValue
        Label5.Text = tempValue2
        Label6.Text = tempValue3

        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
        Select Case Label5.Text

            Case "ID"
                TextEdit1.Text = Label4.Text
                TextEdit2.Text = Label6.Text
                TextEdit2.Enabled = False
            Case "Name"
                TextEdit1.Text = Label4.Text
                TextEdit2.Text = Label6.Text
                TextEdit1.Enabled = False

            Case Else

                TextEdit1.Select()

        End Select

    End Sub

    Sub DataNew()     '�s�W�����H��

        Dim di As New DepartmentInfo

        di.DepID = TextEdit1.Text.Trim
        di.DepName = TextEdit2.Text.Trim

        di.FacID = Mid(TextEdit1.Text.Trim, 1, 1)

        If dc.BriName_Add(di) = True Then

            MsgBox("�K�[�����H�����\�I", 60, "����")
        Else
            MsgBox("�K�[�����H�����ѡA���ˬd��]�I", 60, "����")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Sub DataEditID()   '�ܧ󳡪�ID�H���]�^

    End Sub

    Sub DataEditName()    '�ܧ󳡪��W�١]�ѩ��e�����줽�a�I���ʵ����p�^

        Dim di As New DepartmentInfo

        di.DepID = TextEdit1.Text.Trim
        di.DepName = TextEdit2.Text.Trim


        If dc.BriName_UpdateName(di) = True Then         '�ܧ�BriName���O��

            Dim sci As List(Of SystemUser.SystemUserInfo)
            Dim si As New SystemUser.SystemUserInfo

            sci = sc.SystemUser_GetList(TextEdit1.Text.Trim, Nothing, Nothing)

            If sci.Count > 0 Then

                Dim strID, strName, strPWD, strDID As String
                Dim strkey As Byte()

                strID = sci(0).U_ID
                strName = sci(0).U_Name
                strPWD = sci(0).U_PassWord
                strDID = sci(0).DPT_ID
                strkey = sci(0).U_KeyImage

                sc.SystemUser_Delete(TextEdit1.Text.Trim)

                si.U_ID = strID
                si.U_KeyImage = sci(0).U_KeyImage
                si.U_Name = strName
                si.U_PassWord = sci(0).U_PassWord
                si.DPT_ID = strDID

                sc.SystemUser_Add(si)    '�ק��e�Τ���ܦW�٫H��

            End If

            Dim sci1 As List(Of SystemUser.SystemUserInfo)

            sci1 = sc.UserPower_GetList(Nothing, Nothing, Nothing, TextEdit1.Text.Trim)

            Dim si1 As New SystemUser.SystemUserInfo

            If sci1.Count > 0 Then    '��e�ܧ󪺳����H���b�Ͳ��v���Τ���s�b����H�U�N�X--�ܧ��e�w�s�b�Τ᳡���ݩ�

                Dim strUserID, strUserName, strUserRank, strDepID, strUserType As String  'UserPower��H���ܧ�

                strUserID = sci1(0).UserID
                strUserName = sci1(0).UserName
                strUserRank = sci1(0).UserRank
                strDepID = sci1(0).DepID
                strUserType = sci1(0).UserType

                si1.UserID = strUserID    '�n��ID
                si1.UserName = TextEdit2.Text.Trim    '�����H��
                si1.UserRank = strUserRank   '�Ͳ��v������
                si1.DepID = strDepID       '�Ҧb����
                si1.UserType = strUserType  '�u������

                sc.UserPower_Update(si1)    '�ק��eUserPower�������H��UserName �H��

            End If

            MsgBox("�קﳡ���H�����\�I", 60, "����")
        Else
            MsgBox("�קﳡ���H�����ѡA���ˬd��]�I", 60, "����")
            Exit Sub
        End If

        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Select Case Label5.Text

            Case "ID"

            Case "Name"   '�����W���ܧ�
                If Edit = True Then
                    DataEditName()
                End If
            Case Else

                If CheckData() = True Then
                    If Edit = False Then
                        DataNew()
                    End If
                End If
        End Select

    End Sub

    '�P�_��e����ID�O�_�s�b�A�p�G�w�s�b�����\�A���K�[

    Function CheckData() As Boolean

        CheckData = True    '���l�� ��1

        Dim di As List(Of DepartmentInfo)
        di = dc.BriName_GetList(TextEdit1.Text.Trim, Nothing, Nothing)
        If di.Count > 0 Then
            MsgBox("�������s���w�s�b�A�Э��s�K�[�I", 60, "����")
            TextEdit1.Text = ""
            TextEdit1.Select()

            CheckData = False
            Exit Function
        End If

    End Function

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


End Class