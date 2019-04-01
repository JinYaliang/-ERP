Imports LFERP.Library.MaterialParam
Public Class frmMaterialParamTypeSub
    Dim mtp As New MaterialParamController
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()

    End Sub

    Private Sub frmMaterialParamTypeSub_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtName.Text = ""
        txtGauge.Text = ""

        If Edit = False Then
            Label1.Text = "�W��Ѽ�-�s�W"
        Else
            Label1.Text = "�W��Ѽ�-�ק�"
            LoadData()

        End If
        Me.Text = Label1.Text

    End Sub
    Sub SaveDataNew()
        Dim objInfo As New MaterialParamTypeInfo
        objInfo.ParamTypeName = txtName.Text
        objInfo.ParamTypeGauge = txtGauge.Text

        If mtp.MaterialParamType_Add(objInfo) = True Then
            Me.Close()
        Else
            MsgBox("�O�s�X��", , "����")
            Exit Sub

        End If

    End Sub

    Sub SaveDataEdit()
        Dim objInfo As New MaterialParamTypeInfo
        objInfo.ParamTypeID = CInt(lblID.Text)
        objInfo.ParamTypeName = txtName.Text
        objInfo.ParamTypeGauge = txtGauge.Text
        If mtp.MaterialParamType_Update(objInfo) = True Then
            Me.Close()
        Else
            MsgBox("�O�s�X��", , "����")
            Exit Sub
        End If

    End Sub

    Sub LoadData()
        '�ɤJ�ƾ�
        Dim objInfo As New MaterialParamTypeInfo
        objInfo = mtp.MaterialParamType_Get(CInt(lblID.Text))

        txtName.Text = objInfo.ParamTypeName
        txtGauge.Text = objInfo.ParamTypeGauge


    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If txtName.Text = "" Then
            MsgBox("�S����J�W��,�п�J!", , "����")
            Exit Sub

        End If
        If Edit = False Then
            SaveDataNew()

        Else
            SaveDataEdit()

        End If
    End Sub
End Class