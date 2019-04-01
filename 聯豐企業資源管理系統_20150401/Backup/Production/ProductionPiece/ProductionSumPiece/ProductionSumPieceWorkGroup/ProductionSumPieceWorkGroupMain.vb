Imports LFERP.Library.ProductionSumPieceWorkGroup

Public Class ProductionSumPieceWorkGroupMain

    Private Sub ProductionSumPieceWorkGroupMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        Call popSumWorkGroupRef_Click(Nothing, Nothing)
    End Sub

    Sub PowerUser()
        popSumWorkGroupAdd.Enabled = False
        popSumWorkGroupEdit.Enabled = False
        popSumWorkGrouplDel.Enabled = False

        popSumWorkGroupPrint.Enabled = False
        popSumWorkGroupCollectPrint.Enabled = False

        ExpotExcelToolStripMenuItem.Enabled = False

        Dim pmws As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim pmwiL As List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160601")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGroupAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160602")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGroupEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160603")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGrouplDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160604")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGroupPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160605")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGroupCollectPrint.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160606")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ExpotExcelToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub popSumWorkGroupRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupRef.Click
        ''���J�ƾ�
        Dim pcc As New ProductionSumPieceWorkGroupControl
        Grid1.DataSource = pcc.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Nothing, Format(Now, "yyyy/MM/dd"), Nothing)

    End Sub

    Private Sub popSumWorkGroupAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupAdd.Click
        MTypeName = "GPAdd"
        ProductionSumPieceWorkGroup.ShowDialog()
        ProductionSumPieceWorkGroup.Dispose()
    End Sub


    Private Sub popSumWorkGroupEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        ''-------------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainInfo)
        Dim strDate As String
        Dim strG_NO As String

        strDate = GridView1.GetFocusedRowCellValue("GP_Date").ToString
        strG_NO = GridView1.GetFocusedRowCellValue("G_NO").ToString

        pdcl = pdc.ProductionPiecePayWGMain_GetList(Nothing, Nothing, strG_NO, Format(CDate(strDate), "yyyy/MM"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

        If pdcl.Count > 0 Then   ''���O��
            MsgBox("����ק�," & "���էO" & Format(CDate(strDate), "yyyy/MM") & "���p��u��w�f��!")
            Exit Sub
        End If
        ''---------------------------------------------------------------------------

        Dim DepID As String

        DepID = GridView1.GetFocusedRowCellValue("DepID").ToString

        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, DepID, Format(CDate(strDate), "yyyy/MM"))

        If plA.Count > 0 Then
            If plA(0).LockCheck = True Then
                MsgBox("��e�����O���w��w����ק�!")
                Exit Sub
            End If
        End If
        ''---------------------------------------------------------------------------


        MTypeName = "GPEdit" ''�ק�
        tempValue2 = GridView1.GetFocusedRowCellValue("GP_NO")
        ProductionSumPieceWorkGroup.ShowDialog()
        ProductionSumPieceWorkGroup.Dispose()
    End Sub

    Private Sub popSumWorkGroupView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupView.Click
        If GridView1.RowCount <= 0 Then Exit Sub
        MTypeName = "GPView" ''�d��
        tempValue2 = GridView1.GetFocusedRowCellValue("GP_NO").ToString
        ProductionSumPieceWorkGroup.ShowDialog()
        ProductionSumPieceWorkGroup.Dispose()
    End Sub


    Private Sub popSumWorkGroupSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupSeek.Click
        'Dim mc As New ProductionSumPieceWorkGroupControl
        'Dim myfrm As New ProductionSumPieceFind
        'tempValue = "G"
        'myfrm.ShowDialog()

        'If tempValue = "F" Then
        '    Grid1.DataSource = mc.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, tempValue2, tempValue9, tempValue8, tempValue3, tempValue4, tempValue5, Nothing, Nothing, tempValue6, Nothing, tempValue7, Nothing)
        'End If

        'ProductionSumPieceFind.Dispose()

        'tempValue = Nothing
        'tempValue2 = Nothing
        'tempValue3 = Nothing
        'tempValue4 = Nothing
        'tempValue5 = Nothing
        'tempValue6 = Nothing
        'tempValue7 = Nothing
        'tempValue9 = Nothing
        'tempValue8 = Nothing
        tempValue = "�էO�p��έp"
        ProductionPieceSelectAll.ShowDialog()

        Dim mc As New ProductionSumPieceWorkGroupControl

        Select Case tempValue
            Case "0" '�۩w�q
                Dim PPS As New LFERP.Library.ProductionPiece_Select.ProductionPiece_SelectControl
                Grid1.DataSource = PPS.ProductionSumPieceWorkGroup_GetListSelect("�էO�p��έp", tempValue2)

            Case "1" '�T�w�Ҧ�
                If tempValue3 = "�էO�s��" Then
                    Grid1.DataSource = mc.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, tempValue2, strInDepID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue4, Nothing, tempValue5, Nothing)

                ElseIf tempValue3 = "���~�s��" Then
                    Grid1.DataSource = mc.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, tempValue4, Nothing, tempValue5, Nothing)
                End If

            Case "2" '�t�O ����
                Grid1.DataSource = mc.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue4, Nothing, tempValue5, Nothing)

        End Select


        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing


        ProductionPieceSelectAll.Dispose()
    End Sub

    Private Sub popSumWorkGrouplDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGrouplDel.Click
        ''�R���ƾ�
      

        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        ''-------------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainInfo)
        Dim strDate As String
        Dim strG_NO As String

        strDate = GridView1.GetFocusedRowCellValue("GP_Date").ToString
        strG_NO = GridView1.GetFocusedRowCellValue("G_NO").ToString

        pdcl = pdc.ProductionPiecePayWGMain_GetList(Nothing, Nothing, strG_NO, Format(CDate(strDate), "yyyy/MM"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

        If pdcl.Count > 0 Then   ''���O��
            MsgBox("����R��," & "���էO" & Format(CDate(strDate), "yyyy/MM") & "���p��u��w�f��!")
            Exit Sub
        End If
        ''---------------------------------------------------------------------------


        Dim DepID As String

        DepID = GridView1.GetFocusedRowCellValue("DepID").ToString

        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, DepID, Format(CDate(strDate), "yyyy/MM"))

        If plA.Count > 0 Then
            If plA(0).LockCheck = True Then
                MsgBox("��e�����O���w��w����R��!")
                Exit Sub
            End If
        End If
        ''---------------------------------------------------------------------------


        Dim Pc As New ProductionSumPieceWorkGroupControl
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("GP_NO").ToString

        If MsgBox("�A�T�w�R���p��渹��:  '" & strA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If Pc.ProductionSumPieceWorkGroup_Delete(strA) = True Then
                MsgBox("�R�����\")
                ' popSumWorkGroupRef_Click(Nothing, Nothing) '��s
            Else
                MsgBox("�R������")
            End If
        End If
    End Sub

    Private Sub popSumWorkGroupPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupPrint.Click
        'Dim strA As String
        'If GridView1.RowCount > 0 Then
        '    strA = GridView1.GetFocusedRowCellValue("G_NO")
        '    tempValue2 = strA
        'End If

        'tempValue = "G_PT"  ''�էO�ӥ]

        'rptProductionSumPieceTime.ShowDialog()
        'rptProductionSumPieceTime.Dispose()

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("G_NO")

        tempValue = "�էO�p��έp�C�L"
        tempValue8 = strA
        ProductionPieceSelectAll.ShowDialog()

        tempValue = Nothing
        tempValue8 = Nothing
        ProductionPieceSelectAll.Dispose()
    End Sub

    Private Sub popSumWorkGroupCollectPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupCollectPrint.Click
        'Dim strA As String
        'If GridView1.RowCount > 0 Then
        '    strA = GridView1.GetFocusedRowCellValue("G_NO")
        '    tempValue2 = strA
        'End If

        'tempValue = "G_PTC"  ''�էO�ӥ]

        'rptProductionSumPieceTime.ShowDialog()
        'rptProductionSumPieceTime.Dispose()

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("G_NO")

        tempValue = "�էO�p��έp�C�L���`"
        tempValue8 = strA
        ProductionPieceSelectAll.ShowDialog()

        tempValue = Nothing
        tempValue8 = Nothing
        ProductionPieceSelectAll.Dispose()
    End Sub

    Private Sub popSumWorkGroupCollectPrintList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupCollectPrintList.Click
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("G_NO")

        tempValue = "�էO�p��έp�C�L���`�M��"
        tempValue8 = strA
        ProductionPieceSelectAll.ShowDialog()

        tempValue = Nothing
        tempValue8 = Nothing
        ProductionPieceSelectAll.Dispose()
    End Sub

    Private Sub ExpotExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpotExcelToolStripMenuItem.Click
        tempValue = "�էO�p��"
        frmProductionSumPieceExportExcel.ShowDialog()
        frmProductionSumPieceExportExcel.Dispose()
    End Sub

    Private Sub GNOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GNOToolStripMenuItem.Click
        tempValue2 = "�էO�p��"

        Dim fr As New Form
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionSumPieceLoadExcel Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionSumPieceLoadExcel


        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
End Class