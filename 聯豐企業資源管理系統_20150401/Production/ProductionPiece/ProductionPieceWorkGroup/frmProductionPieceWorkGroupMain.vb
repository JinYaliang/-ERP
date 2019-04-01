Imports LFERP.SystemManager.SystemUser
Imports LFERP.Library.ProductionPieceWorkGroup
Imports LFERP.Library.ProductionPiecePersonnel

Public Class frmProductionPieceWorkGroupMain
    Dim up As New ERPSafe
    Dim gc As New ProductionPieceWorkGroupControl

    Private Sub popPieceWorkGroupAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPieceWorkGroupAdd.Click
        '
        MTypeName = "WGAdd"
        Dim fr As New frmProductionPieceWorkGroup
        fr.ShowDialog()
    End Sub

    Private Sub ProductionPieceWorkGroupMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        PowerUser()
        ''���J�ƾ�
        popPieceWorkGroupRef_Click(Nothing, Nothing)
    End Sub


    Sub PowerUser()
        popPieceWorkGroupAdd.Enabled = False
        popPieceWorkGroupEdit.Enabled = False
        popPieceWorkGroupDel.Enabled = False

        Dim pmws As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim pmwiL As List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPieceWorkGroupAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPieceWorkGroupEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPieceWorkGroupDel.Enabled = True
        End If
    End Sub

    Private Sub popPieceWorkGroupRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPieceWorkGroupRef.Click
        ''��s
        Grid1.DataSource = gc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub popPieceWorkGroupEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPieceWorkGroupEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        MTypeName = "WGEdit" ''�ק�
        tempValue2 = GridView1.GetFocusedRowCellValue("G_NO").ToString
        tempValue3 = Nothing
        tempValue4 = Nothing
        Dim fr As New frmProductionPieceWorkGroup
        fr.ShowDialog()
    End Sub

    Private Sub popPieceWorkGroupView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPieceWorkGroupView.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        MTypeName = "WGView" ''�d��
        tempValue2 = GridView1.GetFocusedRowCellValue("G_NO").ToString
        tempValue3 = Nothing
        tempValue4 = Nothing
        Dim fr As New frmProductionPieceWorkGroup
        fr.ShowDialog()
    End Sub


    Private Sub popPieceWorkGroupDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPieceWorkGroupDel.Click

        ''�R���ƾ�
        Dim gi As New ProductionPieceWorkGroupInfo
        Dim strA As String

        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        strA = GridView1.GetFocusedRowCellValue("G_NO")

        ''���P�_�ӲէO-----�O�_�H���W�椤����----  

        Dim dc As New ProductionPiecePersonnelControl
        Dim dil As New List(Of ProductionPiecePersonnelInfo)

        dil = dc.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If dil.Count > 0 Then
            MsgBox("����R��,�H���W�椤�w�s�b���էO�s��")
            Exit Sub
        End If

        If MsgBox("�A�T�w�n�R���էO�s����:  '" & strA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            If gc.ProductionPieceWorkGroup_Delete(strA) = True Then
                MsgBox("�R�����\")
                popPieceWorkGroupRef_Click(Nothing, Nothing) '��s
            Else
                MsgBox("�R������")
            End If

        End If
    End Sub

    Private Sub popPieceWorkGroupSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPieceWorkGroupSeek.Click
        ''frmProductionPieceWorkGroupFind.ShowDialog()
        'Dim fr As New frmProductionPieceWorkGroupFind

        'fr.ShowDialog()
        'If fr.isClickbtnFind = True Then
        '    Grid1.DataSource = gc.ProductionPieceWorkGroup_GetList(tempValue, tempValue2, tempValue3, tempValue4, tempValue5, tempValue6, tempValue7, Nothing)
        '    tempValue = ""
        '    tempValue2 = ""
        '    tempValue3 = ""
        '    tempValue4 = ""
        '    tempValue5 = ""
        '    tempValue6 = ""
        '    tempValue7 = ""
        'End If
        tempValue = "�էO�W��"
        ProductionPieceSelectAll.ShowDialog()

        Dim gi As New ProductionPieceWorkGroupControl

        Select Case tempValue
            Case "0" '�۩w�q
                Dim PPS As New LFERP.Library.ProductionPiece_Select.ProductionPiece_SelectControl
                Grid1.DataSource = PPS.ProductionPieceWorkGroup_GetListSelect("�էO�W��", tempValue2)

            Case "1" '�T�w�Ҧ�
                If tempValue3 = "�էO�s��" Then
                    Grid1.DataSource = gi.ProductionPieceWorkGroup_GetList(tempValue2, Nothing, Nothing, strInDepID, Nothing, tempValue4, tempValue5, Nothing)

                ElseIf tempValue3 = "�էO�W��" Then
                    Grid1.DataSource = gi.ProductionPieceWorkGroup_GetList(Nothing, tempValue2, Nothing, strInDepID, Nothing, tempValue4, tempValue5, Nothing)

                ElseIf tempValue3 = "�t�d�H" Then
                    Grid1.DataSource = gi.ProductionPieceWorkGroup_GetList(Nothing, Nothing, tempValue2, strInDepID, Nothing, tempValue4, tempValue5, Nothing)
                End If

            Case "2" '�t�O ����

                Grid1.DataSource = gi.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, tempValue4, tempValue5, Nothing)

        End Select

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing

        ProductionPieceSelectAll.Dispose()

    End Sub
End Class