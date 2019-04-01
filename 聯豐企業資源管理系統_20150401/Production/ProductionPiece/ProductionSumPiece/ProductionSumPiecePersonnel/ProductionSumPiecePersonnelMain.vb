Imports LFERP.Library.ProductionSumPiecePersonnel
Imports LFERP.SystemManager.SystemUser

Public Class ProductionSumPiecePersonnelMain
    Private Sub ProductionSumPiecePersonnelMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        PowerUser()
        popSumPersonnelRef_Click(Nothing, Nothing)
    End Sub
    Sub PowerUser()
        popSumPersonnelAdd.Enabled = False
        popSumPersonnelEdit.Enabled = False
        popSumPersonnelDel.Enabled = False

        popSumPersonnelPrint.Enabled = False
        popSumPersonnelCollectPrint.Enabled = False

        ExpotExcelToolStripMenuItem.Enabled = False

        Dim pmws As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim pmwiL As List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160501")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumPersonnelAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160502")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumPersonnelEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160503")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumPersonnelDel.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160504")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumPersonnelPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160505")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popSumPersonnelCollectPrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160506")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then ExpotExcelToolStripMenuItem.Enabled = True
        End If

    End Sub

    Private Sub popSumPersonnelRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelRef.Click
        ''���J�ƾ�
        Dim pcc As New ProductionSumPiecePersonnelControl
        Grid1.DataSource = pcc.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Nothing, Format(Now, "yyyy/MM/dd"), Nothing)

    End Sub

    Private Sub popSumPersonnelAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelAdd.Click
        MTypeName = "PPAdd"
        ProductionSumPiecePersonnel.ShowDialog()
        ProductionSumPiecePersonnel.Dispose()
    End Sub


    Private Sub popSumPersonnelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelEdit.Click
        If GridView1.RowCount <= 0 Then Exit Sub

        ''-------------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelInfo)
        Dim strDate As String
        Dim strPer_NO As String

        strDate = GridView1.GetFocusedRowCellValue("PP_Date").ToString
        strPer_NO = GridView1.GetFocusedRowCellValue("Per_NO").ToString

        pdcl = pdc.ProductionPiecePayPersonnel_GetList(Nothing, strPer_NO, Nothing, Format(CDate(strDate), "yyyy/MM"), Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pdcl.Count > 0 Then   ''���O��
            MsgBox("����ק�," & "�����u" & Format(CDate(strDate), "yyyy/MM") & "���p��u��w�f��!")
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




        MTypeName = "PPEdit" ''�ק�
        tempValue2 = GridView1.GetFocusedRowCellValue("PP_NO")
        ProductionSumPiecePersonnel.ShowDialog()
        ProductionSumPiecePersonnel.Dispose()
    End Sub

    Private Sub popSumPersonnelView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelView.Click
        If GridView1.RowCount <= 0 Then Exit Sub
        MTypeName = "PPView" ''�d��
        tempValue2 = GridView1.GetFocusedRowCellValue("PP_NO").ToString
        ProductionSumPiecePersonnel.ShowDialog()
        ProductionSumPiecePersonnel.Dispose()
    End Sub

    Private Sub popSumPersonnelDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelDel.Click
        ''�R���ƾ�
      

        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If
        ''-------------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelInfo)
        Dim strDate As String
        Dim strPer_NO As String

        strDate = GridView1.GetFocusedRowCellValue("PP_Date").ToString
        strPer_NO = GridView1.GetFocusedRowCellValue("Per_NO").ToString

        pdcl = pdc.ProductionPiecePayPersonnel_GetList(Nothing, strPer_NO, Nothing, Format(CDate(strDate), "yyyy/MM"), Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pdcl.Count > 0 Then   ''���O��
            MsgBox("����R��," & "�����u" & Format(CDate(strDate), "yyyy/MM") & "���p��u��w�f��!")
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

        Dim Pc As New ProductionSumPiecePersonnelControl
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("PP_NO")

        If MsgBox("�A�T�w�R���p��渹��:  '" & strA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If Pc.ProductionSumPiecePersonnel_Delete(strA) = True Then
                MsgBox("�R�����\")
                'popSumPersonnelRef_Click(Nothing, Nothing) '��s
            Else
                MsgBox("�R������")
            End If
        End If
    End Sub

    Private Sub popSumPersonnelSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelSeek.Click
        'Dim mc As New ProductionSumPiecePersonnelControl
        'Dim myfrm As New ProductionSumPieceFind
        'tempValue = "P"
        'myfrm.ShowDialog()

        'If tempValue = "F" Then
        '    Grid1.DataSource = mc.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, tempValue2, Nothing, tempValue9, tempValue8, tempValue3, tempValue4, tempValue5, Nothing, Nothing, tempValue6, Nothing, tempValue7, Nothing)
        'End If

        'ProductionSumPieceFind.Dispose()

        'tempValue = Nothing
        'tempValue2 = Nothing
        'tempValue3 = Nothing
        'tempValue4 = Nothing
        'tempValue5 = Nothing
        'tempValue6 = Nothing
        'tempValue7 = Nothing

        'tempValue8 = Nothing
        'tempValue9 = Nothing
        tempValue = "�ӤH�p��έp"
        ProductionPieceSelectAll.ShowDialog()

        Dim pcc As New ProductionSumPiecePersonnelControl



        Select Case tempValue
            Case "0" '�۩w�q
                Dim PPS As New LFERP.Library.ProductionPiece_Select.ProductionPiece_SelectControl
                Grid1.DataSource = PPS.ProductionSumPiecePersonnel_GetListSelect("�ӤH�p��έp", tempValue2)
            Case "1" '�T�w�Ҧ�
                If tempValue3 = "�t���s��" Then
                    Grid1.DataSource = pcc.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, tempValue2, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue4, Nothing, tempValue5, Nothing)

                ElseIf tempValue3 = "���~�s��" Then
                    Grid1.DataSource = pcc.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, tempValue4, Nothing, tempValue5, Nothing)
                End If

            Case "2" '�t�O ����

                Grid1.DataSource = pcc.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue4, Nothing, tempValue5, Nothing)

        End Select


        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing


        ProductionPieceSelectAll.Dispose()
    End Sub


    Private Sub popSumPersonnelPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelPrint.Click
        'Dim strA As String
        'If GridView1.RowCount > 0 Then
        '    strA = GridView1.GetFocusedRowCellValue("Per_NO")
        '    tempValue2 = strA
        'End If

        'tempValue = "P_PT"  ''���u�O��p�ɪ�

        'rptProductionSumPieceTime.ShowDialog()
        'rptProductionSumPieceTime.Dispose()
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("Per_NO")

        tempValue = "�ӤH�p��έp�C�L"
        tempValue8 = strA
        ProductionPieceSelectAll.ShowDialog()

        tempValue = Nothing
        tempValue8 = Nothing
        ProductionPieceSelectAll.Dispose()

    End Sub

    Private Sub popSumPersonnelCollectPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popSumPersonnelCollectPrint.Click
        'Dim strA As String
        'If GridView1.RowCount > 0 Then
        '    strA = GridView1.GetFocusedRowCellValue("Per_NO")
        '    tempValue2 = strA
        'End If

        'tempValue = "P_PTC"  ''���u�O��p�ɪ�

        'rptProductionSumPieceTime.ShowDialog()
        'rptProductionSumPieceTime.Dispose()  ''popSumWorkGroupCollectPrint

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("Per_NO")

        tempValue = "�ӤH�p��έp�C�L���`"
        tempValue8 = strA
        ProductionPieceSelectAll.ShowDialog()

        tempValue = Nothing
        tempValue8 = Nothing
        ProductionPieceSelectAll.Dispose()
    End Sub


    Private Sub PopSumPersonnelCollectPrintList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PopSumPersonnelCollectPrintList.Click
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("Per_NO")

        tempValue = "�ӤH�p��έp�C�L���`�M��"
        tempValue8 = strA
        ProductionPieceSelectAll.ShowDialog()

        tempValue = Nothing
        tempValue8 = Nothing
        ProductionPieceSelectAll.Dispose()
    End Sub

    Private Sub ExpotExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpotExcelToolStripMenuItem.Click
        tempValue = "�ӤH�p��"
        frmProductionSumPieceExportExcel.ShowDialog()
        frmProductionSumPieceExportExcel.Dispose()

    End Sub

    Private Sub PerNOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerNOToolStripMenuItem.Click
        tempValue2 = "�ӤH�p��"

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