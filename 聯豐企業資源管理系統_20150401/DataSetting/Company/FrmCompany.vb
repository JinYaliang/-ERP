Imports System.IO
Imports System.Windows.Forms
Imports LFERP.Library.MaterialParam
Imports LFERP.Library.Material
Imports LFERP.Library
Imports LFERP.DataSetting


Public Class FrmCompany
    Private fs As FileStream
    'Private Sub cmdDelPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelPhoto.Click
    '    Dim objinfo As New LFERP.DataSetting.CompanyInfo
    '    Dim objct As New LFERP.DataSetting.CompanyControler
    '    objinfo.CO_ID = "CB"
    '    objinfo.CO_ChsName = "�ķצ������q"
    '    objinfo.CO_EngName = "CB"
    '    objinfo.CO_ChsAddress = "�`�`���_�w���s����Ĥ��u�~�ϤW��F�G��"
    '    objinfo.CO_ChsTel = "0755-27748020"
    '    objinfo.CO_ChsFax = "0755-27749753"
    '    objinfo.CO_Logo = ImageToByte(pPhoto.Image)
    '    If objct.Company_Update(objinfo) = True Then
    '        MsgBox("CB-OK")
    '    End If

    'End Sub


    Private Sub FrmCompanyAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Clr_text()

        Dim CC As New CompanyControler
        Grid1.DataSource = CC.Company_Getlist(Nothing, Nothing, Nothing, Nothing)
    End Sub


    Sub Clr_text()
        txtCO_ID.Text = ""
        txtCO_ChsName.Text = ""
        txtCO_EngName.Text = ""
        txtCO_ChsAddress.Text = ""
        txtCO_EngAddress.Text = ""

        txtCO_ChsTel.Text = ""
        txtCO_EngTel.Text = ""
        txtCO_ChsFax.Text = ""
        txtCO_EngFax.Text = ""

        pPhoto.Image = Nothing


        txtCO_ID.Focus()
    End Sub

    ' Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'Dim objinfo As New LFERP.DataSetting.CompanyInfo
    'Dim objct As New LFERP.DataSetting.CompanyControler
    'objinfo.CO_ID = "LF"
    'objinfo.CO_ChsName = "�p�ת�߼t�������q"
    'objinfo.CO_EngName = "LuenFungWatch"
    'objinfo.CO_ChsAddress = "�`�`���_�w���s����Ĥ��u�~�ϤW��F�G��"
    'objinfo.CO_ChsTel = "0755-27748020"
    'objinfo.CO_ChsFax = "0755-27749753"
    'objinfo.CO_Logo = ImageToByte(pPhoto.Image)
    'If objct.Company_Update(objinfo) = True Then
    '    MsgBox("LF-OK")
    'End If
    ' End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    Dim objinfo As New LFERP.DataSetting.CompanyInfo
    '    Dim objct As New LFERP.DataSetting.CompanyControler
    '    objinfo.CO_ID = "MG"
    '    objinfo.CO_ChsName = "�̨Ⱥ�K���ݦ������q"
    '    objinfo.CO_EngName = "MeGa"
    '    objinfo.CO_ChsAddress = "�F�𥫻�^��ɬu�u�~��"
    '    objinfo.CO_ChsTel = "0769-86802888"
    '    objinfo.CO_ChsFax = "0769-86803888"
    '    objinfo.CO_Logo = ImageToByte(pPhoto.Image)
    '    If objct.Company_Update(objinfo) = True Then
    '        MsgBox("MG-OK")
    '    End If
    'End Sub

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
    '    Dim objinfo As New LFERP.DataSetting.CompanyInfo
    '    Dim objct As New LFERP.DataSetting.CompanyControler


    '    objinfo.DPT_ID = "1002"
    '    objinfo.CO_ID = "DGMG"
    '    objinfo.CO_ChsName = "�̨Ⱥ�K���ݬ��(�F��)�������q"
    '    objinfo.CO_EngName = "MeGa"
    '    objinfo.CO_ChsAddress = "�F�𥫻�^��ɬu�u�~��"
    '    objinfo.CO_ChsTel = "0769-86802888"
    '    objinfo.CO_ChsFax = "0769-86803888"
    '    objinfo.CO_Logo = ImageToByte(pPhoto.Image)
    '    If objct.Company_Update(objinfo) = True Then
    '        MsgBox("DGMG-OK")
    '    End If
    'End Sub

    ''' <summary>
    ''' ���J�ƾ�
    ''' </summary>
    ''' <param name="_CO_ID"></param>
    ''' <remarks></remarks>
    Sub LoadData(ByVal _CO_ID As String)

        Clr_text()

        Dim objList As New List(Of LFERP.DataSetting.CompanyInfo)
        Dim objct As New LFERP.DataSetting.CompanyControler
        objList = objct.Company_Getlist(Nothing, _CO_ID, Nothing, Nothing)

        If objList.Count <= 0 Then Exit Sub

        txtCO_ID.Text = objList(0).CO_ID

        txtCO_ChsName.Text = objList(0).CO_ChsName
        txtCO_EngName.Text = objList(0).CO_EngName

        txtCO_ChsAddress.Text = objList(0).CO_ChsAddress
        txtCO_EngAddress.Text = objList(0).CO_EngAddress

        txtCO_ChsTel.Text = objList(0).CO_ChsTel
        txtCO_EngTel.Text = objList(0).CO_EngTel

        txtCO_ChsFax.Text = objList(0).CO_ChsFax
        txtCO_EngFax.Text = objList(0).CO_EngFax

        If objList(0).CO_Logo Is Nothing Then
            pPhoto.Image = Nothing
        Else
            pPhoto.Image = ByteToImage(objList(0).CO_Logo)
        End If

        txtCO_ID.SelectionStart = Len(txtCO_ID.Text)

    End Sub
    ''' <summary>
    ''' �O�s�ƾ�
    ''' </summary>
    ''' <remarks></remarks>
    Sub Save_Edit()
        Dim Save_State As String

        ''���P�_���O���O�_�s�b�A�Y�s�b�h��s�@�U,�Y���s�b�h�W�[�@���O��
        If txtCO_ID.Text = "" Then
            MsgBox("���q�N�����ର�šA���ˬd�I")
            Exit Sub
        End If

        Dim Fc As New CompanyControler
        Dim Fil As New List(Of CompanyInfo)

        Fil = Fc.Company_Getlist(Nothing, txtCO_ID.Text.Trim, Nothing, Nothing)

        If Fil.Count > 0 Then
            Save_State = "Update"
            If MsgBox("�����q�N���H���w�s�b,�O�_�~��O�s?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Else
                Exit Sub
            End If
        Else
            Save_State = "Add"
        End If


        Dim objct As New CompanyControler
        Dim objinfo As New CompanyInfo

        objinfo.CO_ID = txtCO_ID.Text

        objinfo.CO_ChsName = txtCO_ChsName.Text
        objinfo.CO_EngName = txtCO_EngName.Text

        objinfo.CO_ChsAddress = txtCO_ChsAddress.Text
        objinfo.CO_EngAddress = txtCO_EngAddress.Text

        objinfo.CO_ChsTel = txtCO_ChsTel.Text
        objinfo.CO_EngTel = txtCO_EngTel.Text

        objinfo.CO_ChsFax = txtCO_ChsFax.Text
        objinfo.CO_EngFax = txtCO_EngFax.Text


        objinfo.CO_Logo = ImageToByte(pPhoto.Image) '�O�s�϶H(�G�i��)

        If Save_State = "Update" Then
            If objct.Company_Update(objinfo) = True Then
                MsgBox("�ƾڧ�s���\!")
                'Clr_text()
            End If
        Else
            If objct.Company_Add(objinfo) = True Then
                MsgBox("�ƾڲK�[���\!")
                Dim CC As New CompanyControler
                Grid1.DataSource = CC.Company_Getlist(Nothing, Nothing, Nothing, Nothing)
                Clr_text()
            End If
        End If
    End Sub


    ''' <summary>
    ''' ��ܤ��e
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Click
        Dim strA As String

        If GridView1.RowCount <= 0 Then Exit Sub
        strA = GridView1.GetFocusedRowCellValue("CO_ID")

        LoadData(strA)

    End Sub
    ''' <summary>
    ''' ���JLOGO�Ϥ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoad.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "�Ϥ����(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp"
        ofd.ShowDialog()


        If ofd.FileName.ToString = "" Then Exit Sub
        fs = New FileStream(ofd.FileName.ToString, FileMode.Open, FileAccess.Read)
        pPhoto.Image = Image.FromFile(ofd.FileName.ToString)
    End Sub
    ''' <summary>
    ''' �R���O��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DelToolStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DelToolStrip.Click

        Dim strA As String

        If GridView1.RowCount <= 0 Then Exit Sub

        strA = GridView1.GetFocusedRowCellValue("CO_ID")

        If strA = "" Then Exit Sub

        If MsgBox("�A�T�w�R�����q�N����:  '" & strA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim dc As New CompanyControler
            Dim di As New CompanyInfo
            di.CO_ID = strA
            If dc.Company_Del(di) = True Then
                MsgBox("�R�����\�I")

                Clr_text()
                Dim CC As New CompanyControler
                Grid1.DataSource = CC.Company_Getlist(Nothing, Nothing, Nothing, Nothing)

            Else
                MsgBox("�R�����ѡI")
            End If
        End If

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Save_Edit()
    End Sub

    Private Sub BottonClr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BottonClr.Click
        pPhoto.Image = Nothing
    End Sub


End Class