Imports System.IO
Imports System.Windows.Forms
Imports LFERP.Library.MaterialParam
Imports LFERP.Library.Material
Imports LFERP.Library
Imports LFERP.DataSetting


Public Class FrmCompanyAdd
    Private fs As FileStream
    Private Sub cmdDelPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelPhoto.Click
        Dim objinfo As New LFERP.DataSetting.CompanyInfo
        Dim objct As New LFERP.DataSetting.CompanyControler
        objinfo.CO_ID = "CB"
        objinfo.CO_ChsName = "�ķצ������q"
        objinfo.CO_EngName = "CB"
        objinfo.CO_ChsAddress = "�`�`���_�w���s����Ĥ��u�~�ϤW��F�G��"
        objinfo.CO_ChsTel = "0755-27748020"
        objinfo.CO_ChsFax = "0755-27749753"
        objinfo.CO_Logo = ImageToByte(pPhoto.Image)
        If objct.Company_Update(objinfo) = True Then
            MsgBox("CB-OK")
        End If

    End Sub

    Private Sub cmdOpenPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenPhoto.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "�Ϥ����(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp"
        ofd.ShowDialog()


        If ofd.FileName.ToString = "" Then Exit Sub
        fs = New FileStream(ofd.FileName.ToString, FileMode.Open, FileAccess.Read)
        pPhoto.Image = Image.FromFile(ofd.FileName.ToString)
    End Sub

    Private Sub FrmCompanyAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objinfo As New LFERP.DataSetting.CompanyInfo
        Dim objct As New LFERP.DataSetting.CompanyControler
        objinfo.CO_ID = "LF"
        objinfo.CO_ChsName = "�p�ת�߼t�������q"
        objinfo.CO_EngName = "LuenFungWatch"
        objinfo.CO_ChsAddress = "�`�`���_�w���s����Ĥ��u�~�ϤW��F�G��"
        objinfo.CO_ChsTel = "0755-27748020"
        objinfo.CO_ChsFax = "0755-27749753"
        objinfo.CO_Logo = ImageToByte(pPhoto.Image)
        If objct.Company_Update(objinfo) = True Then
            MsgBox("LF-OK")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim objinfo As New LFERP.DataSetting.CompanyInfo
        Dim objct As New LFERP.DataSetting.CompanyControler
        objinfo.CO_ID = "MG"
        objinfo.CO_ChsName = "�̨Ⱥ�K���ݦ������q"
        objinfo.CO_EngName = "MeGa"
        objinfo.CO_ChsAddress = "�F�𥫻�^��ɬu�u�~��"
        objinfo.CO_ChsTel = "0769-86802888"
        objinfo.CO_ChsFax = "0769-86803888"
        objinfo.CO_Logo = ImageToByte(pPhoto.Image)
        If objct.Company_Update(objinfo) = True Then
            MsgBox("MG-OK")
        End If
    End Sub
End Class