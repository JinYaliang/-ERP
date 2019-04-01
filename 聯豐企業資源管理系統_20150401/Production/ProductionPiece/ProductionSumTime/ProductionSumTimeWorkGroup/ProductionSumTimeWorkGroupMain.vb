Imports LFERP.Library.ProductionSumTimeWorkGroup
Imports LFERP.SystemManager.SystemUser

Public Class ProductionSumTimeWorkGroupMain


    Private Sub ProductionSumTimeWorkGroupMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PowerUser()
        popSumWorkGroupRef_Click(Nothing, Nothing)
    End Sub


    Sub PowerUser()
        popSumWorkGroupAdd.Enabled = False
        popSumWorkGroupEdit.Enabled = False
        popSumWorkGrouplDel.Enabled = False

        PopSumWorkGroupPrint.Enabled = False
        popSumWorkGroupCollectPrint.Enabled = False

        ExpotExcelToolStripMenuItem.Enabled = False

        Dim pmws As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim pmwiL As List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160801")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGroupAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160802")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGroupEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160803")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGrouplDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160804")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then PopSumWorkGroupPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160805")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumWorkGroupCollectPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160806")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ExpotExcelToolStripMenuItem.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' ��s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popSumWorkGroupRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupRef.Click
        Dim pcc As New ProductionSumTimeWorkGroupControl
        Grid1.DataSource = pcc.ProductionSumTimeWorkGroup_GetList(Nothing, Nothing, Nothing, strInDepID, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Nothing, Format(Now, "yyyy/MM/dd"), Nothing, Nothing)
    End Sub
    ''' <summary>
    ''' �ƾڼW�[
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popSumWorkGroupAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupAdd.Click
        tempValue4 = strInDepID ''����
        '
        MTypeName = "GTAdd"
        ProductionSumTimeWorkGroup.ShowDialog()
        ProductionSumTimeWorkGroup.Dispose()
    End Sub
    ''' <summary>
    ''' �ק�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popSumWorkGroupEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        ''-------2012-8-13------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainInfo)
        Dim strDate As String
        Dim strG_NO As String

        strDate = GridView1.GetFocusedRowCellValue("GT_Date").ToString
        strG_NO = GridView1.GetFocusedRowCellValue("G_NO").ToString

        pdcl = pdc.ProductionPiecePayWGMain_GetList(Nothing, Nothing, strG_NO, Format(CDate(strDate), "yyyy/MM"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

        If pdcl.Count > 0 Then   ''���O��
            MsgBox("����ק�," & "���էO" & Format(CDate(strDate), "yyyy/MM") & "���p�ɤu��w�f��!")
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


        MTypeName = "GTEdit" ''�ק�
        tempValue2 = GridView1.GetFocusedRowCellValue("GT_NO")

        tempValue4 = strInDepID ''����
        ProductionSumTimeWorkGroup.ShowDialog()
        ProductionSumTimeWorkGroup.Dispose()
    End Sub

    ''' <summary>
    ''' �d��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popSumWorkGroupView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupView.Click
        If GridView1.RowCount <= 0 Then Exit Sub
        MTypeName = "GTView" ''�d��
        tempValue2 = GridView1.GetFocusedRowCellValue("GT_NO").ToString

        tempValue4 = strInDepID
        ProductionSumTimeWorkGroup.ShowDialog()
        ProductionSumTimeWorkGroup.Dispose()
    End Sub
    ''' <summary>
    ''' �R���ƾ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub popSumWorkGrouplDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGrouplDel.Click
        ''
 
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        ''-------2012-8-13------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainInfo)
        Dim strDate As String
        Dim strG_NO As String

        strDate = GridView1.GetFocusedRowCellValue("GT_Date").ToString
        strG_NO = GridView1.GetFocusedRowCellValue("G_NO").ToString

        pdcl = pdc.ProductionPiecePayWGMain_GetList(Nothing, Nothing, strG_NO, Format(CDate(strDate), "yyyy/MM"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

        If pdcl.Count > 0 Then   ''���O��
            MsgBox("����R��," & "���էO" & Format(CDate(strDate), "yyyy/MM") & "���p�ɤu��w�f��!")
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

        Dim Pc As New ProductionSumTimeWorkGroupControl
        Dim strA As String

        strA = GridView1.GetFocusedRowCellValue("GT_NO")

        If MsgBox("�A�T�w�R���p��渹��:  '" & strA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If Pc.ProductionSumTimeWorkGroup_Delete(strA) = True Then
                MsgBox("�R�����\")
                'popSumWorkGroupRef_Click(Nothing, Nothing) '��s
            Else
                MsgBox("�R������")
            End If
        End If
    End Sub

    Private Sub popSumWorkGroupSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumWorkGroupSeek.Click
        'Dim mc As New ProductionSumTimeWorkGroupControl
        'Dim myfrm As New ProductionSumTimeFind
        'tempValue = "G"
        'myfrm.ShowDialog()

        'If tempValue = "F" Then
        '    Grid1.DataSource = mc.ProductionSumTimeWorkGroup_GetList(Nothing, tempValue2, tempValue3, tempValue4, tempValue5, tempValue6, Nothing, tempValue7, Nothing, Nothing)
        'End If

        'ProductionSumTimeFind.Dispose()

        'tempValue = Nothing
        'tempValue2 = Nothing
        'tempValue3 = Nothing
        'tempValue4 = Nothing
        'tempValue5 = Nothing
        'tempValue6 = Nothing
        'tempValue7 = Nothing

        tempValue = "�էO�p�ɲέp"
        ProductionPieceSelectAll.ShowDialog()

        Dim mc As New ProductionSumTimeWorkGroupControl


        Select Case tempValue
            Case "0" '�۩w�q
                Dim PPS As New LFERP.Library.ProductionPiece_Select.ProductionPiece_SelectControl
                Grid1.DataSource = PPS.ProductionSumTimeWorkGroup_GetListSelect("�էO�p�ɲέp", tempValue2)

            Case "1" '�T�w�Ҧ�
                If tempValue3 = "�էO�s��" Then
                    Grid1.DataSource = mc.ProductionSumTimeWorkGroup_GetList(Nothing, tempValue2, Nothing, strInDepID, Nothing, tempValue4, Nothing, tempValue5, Nothing, Nothing)
                End If

            Case "2" '�t�O ����
                Grid1.DataSource = mc.ProductionSumTimeWorkGroup_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, tempValue4, Nothing, tempValue5, Nothing, Nothing)

        End Select


        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing

        ProductionPieceSelectAll.Dispose()
    End Sub

    Private Sub PopSumPersonnelPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopSumWorkGroupPrint.Click

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

        tempValue = "�էO�p�ɲέp�C�L"
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

        tempValue = "�էO�p�ɲέp�C�L���`"
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