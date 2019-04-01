Imports LFERP.Library.Purchase.WareQuality
Imports LFERP.SystemManager

Public Class frmWareQualityMain
    Dim wqc As New WareQualityController

    Private Sub frmWareQualityMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        popWQRef_Click(Nothing, Nothing)
    End Sub

    '�����k�䡧�K�[�����
    Private Sub popWQAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQAdd.Click
        'Dim frmWQAdd As New frmWareQualityAdd
        'frmWQAdd.MdiParent = MDIMain
        'frmWQAdd.WindowState = FormWindowState.Maximized
        'frmWQAdd.Show()

        frmWareQualityAdd.MdiParent = MDIMain
        frmWareQualityAdd.WindowState = FormWindowState.Maximized
        frmWareQualityAdd.Show()
    End Sub

    '�����k�䡧��s�����
    '���L�{�Q�H�U�L�{�եΡG
    'frmWareQualityMain_Load()
    'popWQDelete_Click()
    'cmdSave_Click.frmWareQualityAdd()
    Public Sub popWQRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQRef.Click
        Grid.DataSource = wqc.WareQuality_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    '�����k�䡧�ק���
    Private Sub popWQEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQEdit.Click

        If GridView1.RowCount = 0 Then Exit Sub

        Dim frmWQEdit As New frmWareQualityAdd
        frmWQEdit.MdiParent = MDIMain
        frmWQEdit.WindowState = FormWindowState.Maximized
        frmWQEdit.Text = "���ƫ~����X��-�ק�"
        frmWQEdit.lblTitle.Text = "���ƫ~����X��-�ק�"
        frmWQEdit.txtWQ_Code.Text = GridView1.GetFocusedRowCellValue("WQ_Code").ToString '��襤�檺���X��s����ܦb�ק�Ҷ������X��s���奻�ؤ�
        frmWQEdit.Show()

    End Sub

    '�����k�䡧�d�ݡ����
    Private Sub popWQView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQView.Click

        If GridView1.RowCount = 0 Then Exit Sub

        Dim frmWQView As New frmWareQualityAdd
        frmWQView.MdiParent = MDIMain
        frmWQView.WindowState = FormWindowState.Maximized
        frmWQView.Text = "���ƫ~����X��-�d��"
        frmWQView.lblTitle.Text = "���ƫ~����X��-�d��"
        frmWQView.txtWQ_Code.Text = GridView1.GetFocusedRowCellValue("WQ_Code").ToString '��襤�檺���X��s����ܦb�d�ݼҶ������X��s���奻�ؤ�
        frmWQView.Show()

    End Sub

    '�����k�䡧�d�ߡ����
    Private Sub popWQFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQFind.Click
        frmWareQualityFind.ShowDialog()
    End Sub

    '�����k�䡧�R�������
    Private Sub popWQDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQDelete.Click

        If GridView1.RowCount = 0 Then Exit Sub

        Dim wqi As List(Of WareQualityInfo)
        wqi = wqc.WareQuality_GetList(GridView1.GetFocusedRowCellValue("WQ_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If wqi.Count > 0 Then
            If MsgBox("�T�w�R�����X��s����" & GridView1.GetFocusedRowCellValue("WQ_Code").ToString & "���O���ܡH", MsgBoxStyle.YesNo + vbQuestion, "����") = MsgBoxResult.Yes Then
                If wqc.WareQuality_Delete(GridView1.GetFocusedRowCellValue("WQ_Code").ToString) = True Then
                    popWQRef_Click(Nothing, Nothing)
                    MsgBox("�O���R�����\!", 64, "����")
                End If
            End If
        Else
            MsgBox("���O���ƾڮw�����s�b!", 64, "����")
        End If
    End Sub
    '�����k�䡧���[��󡨵��
    Private Sub popWQAddFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQAddFile.Click
        'Dim open As Boolean
        'Dim update As Boolean
        'Dim down As Boolean
        'Dim edit As Boolean
        'Dim del As Boolean
        'Dim detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        'If GridView1.RowCount = 0 Then Exit Sub
        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400110")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then update = True Else update = False
        'End If

        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400111")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then down = True Else down = False
        'End If


        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400112")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True Else edit = False
        'End If

        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400113")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then del = True Else del = False
        'End If

        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400114")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True Else detail = False
        'End If

        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400115")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then open = True Else open = False

        'End If
        'FileShow("40081", GridView1.GetFocusedRowCellValue("WQ_Code").ToString, open, update, down, edit, del, detail)

        '�եιL�{FileShow(),��ܪ��[������
        FileShow("40081", GridView1.GetFocusedRowCellValue("WQ_Code").ToString, True, True, True, True, True, True)
    End Sub

    Private Sub popWQPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWQPrint.Click

    End Sub
End Class