Imports LFERP.Library.KnifeWare
Public Class frmGNOdistribute

    Dim strW_UserID As String
    Dim strWHID As String
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frmGNOdistribute_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strW_UserID = tempValue
        strWHID = tempValue2
        tempValue = Nothing
        tempValue2 = Nothing

        Dim wc As New KnifeGroupControl
        GridLookGNO.Properties.DisplayMember = "G_Name"
        GridLookGNO.Properties.ValueMember = "G_NO"
        GridLookGNO.Properties.DataSource = wc.KnifeGroup_GetList(Nothing, Nothing, strWHID)

        LoadData()

    End Sub
    ''' <summary>
    ''' ���J�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadData()
        Dim GC As New KnifeWhiteUserController
        Dim gl As New List(Of KnifeWhiteUserInfo)
        gl = GC.WhiteUser_GetList(Nothing, strW_UserID, strWHID, Nothing, Nothing, False)

        If gl.Count <= 0 Then
            MsgBox("�L�ƾڦs�b!", 64, "����")
            Me.Close()
        ElseIf gl.Count > 1 Then
            MsgBox("��e�զW��b�P�@�ܤ��s�b,�h���O��!", 64, "����")
            Me.Close()
        End If

        txtWH.Text = gl(0).WH_Name
        txtWH.Tag = gl(0).WH_ID

        txtW_UserID.Text = gl(0).W_UserID
        txtW_UserName.Text = gl(0).W_UserName
        GridLookGNO.EditValue = gl(0).GNO

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If GridLookGNO.Text = "" Then
            MsgBox("�էO�s������,���ˬd�I", 64, "����")
            Exit Sub
        End If

        SaveData()

    End Sub

    Sub SaveData()
        Dim GC As New KnifeWhiteUserController
        Dim gl As New KnifeWhiteUserInfo

        If GC.WhiteUser_UpdateGNOA(strW_UserID, strWHID, GridLookGNO.EditValue) = True Then
            MsgBox("�O�s���\!", 64, "����")
            Me.Close()
        Else
            MsgBox("�O�s����,���ˬd!", 64, "����")
        End If


    End Sub


End Class