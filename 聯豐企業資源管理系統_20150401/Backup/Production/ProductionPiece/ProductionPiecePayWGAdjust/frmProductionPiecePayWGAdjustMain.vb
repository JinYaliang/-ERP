Imports LFERP.Library.ProductionPiecePayWGAdjust
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.SystemManager


Public Class frmProductionPiecePayWGAdjustMain

    Private Sub frmProductionPiecePayWGAdjustMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdRef_Click(Nothing, Nothing)
        PowerUser()
    End Sub

    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Dim pc As New ProductionPiecePayWGAdjustControl
        Me.Grid.DataSource = pc.ProductionPiecePayWGAdjust_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


    End Sub

    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88161401")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88161402")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88161403")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88161404")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdCheck.Enabled = True
        End If

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        MTypeName = "AdjustAdd" ''�ק�
        Dim fr As New frmProductionPiecePayWGAdjust
        fr.ShowDialog()
        fr.Dispose()
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("AutoID")
        Dim gc1 As New ProductionPiecePayWGAdjustControl
        Dim gilist As New List(Of ProductionPiecePayWGAdjustInfo)
        gilist = gc1.ProductionPiecePayWGAdjust_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If gilist.Count <= 0 Then
            Exit Sub
        End If

        If gilist(0).Ad_Check = True Then
            MsgBox("����w�f��,����ק�")
            Exit Sub
        End If

        MTypeName = "AdjustEdit" ''�ק�
        tempValue2 = GridView1.GetFocusedRowCellValue("AutoID")
        Dim fr As New frmProductionPiecePayWGAdjust
        fr.ShowDialog()
        fr.Dispose()
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("AutoID")
        Dim gc1 As New ProductionPiecePayWGAdjustControl
        Dim gilist As New List(Of ProductionPiecePayWGAdjustInfo)
        gilist = gc1.ProductionPiecePayWGAdjust_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If gilist.Count <= 0 Then
            Exit Sub
        End If

        If gilist(0).Ad_Check = True Then
            MsgBox("����w�f��,����R��")
            Exit Sub
        End If

        Dim MsgStr As String
        MsgStr = GridView1.GetFocusedRowCellValue("Ad_YYMM") & "��X�էO��:" & GridView1.GetFocusedRowCellValue("OUT_G_Name") + vbCrLf + "��J�էO���G" + GridView1.GetFocusedRowCellValue("IN_G_Name")

        Dim Pc As New ProductionPiecePayWGAdjustControl

        If MsgBox("�A�T�w�R��" & MsgStr & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If Pc.ProductionPiecePayWGAdjust_Delete(strA) = True Then
                MsgBox("�R�����\")
                cmdRef_Click(Nothing, Nothing) '��s
            Else
                MsgBox("�R������")
            End If
        End If
    End Sub

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        MTypeName = "AdjustCheck" ''�ק�
        tempValue2 = GridView1.GetFocusedRowCellValue("AutoID")
        Dim fr As New frmProductionPiecePayWGAdjust
        fr.ShowDialog()
        fr.Dispose()
    End Sub

    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        MTypeName = "AdjustView" ''�ק�
        tempValue2 = GridView1.GetFocusedRowCellValue("AutoID")
        Dim fr As New frmProductionPiecePayWGAdjust
        fr.ShowDialog()
        fr.Dispose()
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        tempValue = "�էO�~���վ�"
        ProductionPieceSelectAll.ShowDialog()

        Dim pc As New ProductionPiecePayWGAdjustControl

        Select Case tempValue
            Case "0" '�۩w�q

                Dim PPS As New LFERP.Library.ProductionPiece_Select.ProductionPiece_SelectControl
                Grid.DataSource = PPS.ProductionSumPieceWorkGroup_GetListSelect("�էO�~���վ�", tempValue2)
            Case "1" '�T�w�Ҧ�
                If tempValue3 = "��X�էO�s��" Then  ''�H��X�������ݩ���Ӽt�O����
                    Me.Grid.DataSource = pc.ProductionPiecePayWGAdjust_GetList(Nothing, tempValue2, Nothing, strInDepID, Nothing, Nothing, Nothing, tempValue4, Nothing, Nothing, tempValue6)
                ElseIf tempValue3 = "��J�էO�s��" Then
                    Me.Grid.DataSource = pc.ProductionPiecePayWGAdjust_GetList(Nothing, Nothing, Nothing, strInDepID, tempValue2, Nothing, Nothing, tempValue4, Nothing, Nothing, tempValue6)
                End If
            Case "2" '�t�O ����
                Me.Grid.DataSource = pc.ProductionPiecePayWGAdjust_GetList(Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing)
        End Select


        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing


        ProductionPieceSelectAll.Dispose()
    End Sub
End Class