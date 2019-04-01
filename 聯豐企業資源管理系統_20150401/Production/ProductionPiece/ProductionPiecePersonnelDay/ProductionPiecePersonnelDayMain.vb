Imports lferp.Library.ProductionPiecePersonnelDay
Imports LFERP.Library.ProductionSumPiecePersonnel
Imports LFERP.Library.ProductionSumPieceWorkGroup
Imports LFERP.Library.ProductionSumTimePersonnel
Imports LFERP.Library.ProductionSumTimeWorkGroup
Imports LFERP.SystemManager.SystemUser

Public Class ProductionPiecePersonnelDayMain

    Private Sub popPiecePersonnelAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPiecePersonnelAdd.Click
        On Error Resume Next

        Dim fr As ProductionPiecePersonnelDaySub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionPiecePersonnelDaySub Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue2 = Nothing
        MTypeName = "PD_add"

        fr = New ProductionPiecePersonnelDaySub
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub


    Private Sub popPiecePersonnelRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPiecePersonnelRef.Click
        Dim pdc As New ProductionPiecePersonnelDayControl
        Grid1.DataSource = pdc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Format(Now, "yyyy/MM/dd"), Nothing)
    End Sub

    Private Sub ProductionPiecePersonnelDayMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        popPiecePersonnelRef_Click(Nothing, Nothing)

        Dim pmws As New LFERP.SystemManager.PermissionModuleWarrantSubController
        Dim pmwiL As List(Of LFERP.SystemManager.PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160401")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPiecePersonnelAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160402")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPiecePersonnelEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "88160403")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popPiecePersonnelDel.Enabled = True
        End If
    End Sub

    Private Sub popPiecePersonnelDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPiecePersonnelDel.Click
        ''�R���ƾ�
        Dim pdc As New ProductionPiecePersonnelDayControl
        Dim pdi As New ProductionPiecePersonnelDayInfo
        Dim strA, strB As String

        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        Dim strDate1, strDep1, strPer_NO1, strG_NO1 As String

        strDate1 = GridView1.GetFocusedRowCellValue("Per_Date").ToString
        strDep1 = GridView1.GetFocusedRowCellValue("DepID").ToString
        strPer_NO1 = GridView1.GetFocusedRowCellValue("Per_NO").ToString
        strG_NO1 = GridView1.GetFocusedRowCellValue("G_NO").ToString

        If Check_Delete(strDate1, strDep1, strPer_NO1, strG_NO1, "����R��,  ") = False Then Exit Sub

        strA = GridView1.GetFocusedRowCellValue("AutoID").ToString
        strB = GridView1.GetFocusedRowCellValue("Per_Name").ToString

        If MsgBox("�A�T�w�R�����u�m�W��:  '" & strB & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If pdc.ProductionPiecePersonnelDay_Delete(strA) = True Then
                MsgBox("�R�����\")
                popPiecePersonnelRef_Click(Nothing, Nothing) '��s
            Else
                MsgBox("�R������")
            End If
        End If
    End Sub


    ''' <summary>
    ''' ��ӤH�s���b�ӤH�p��A�ӤH�p�ɤ��s�b�ɤ���R�� ,�էO�s���s����էO�p��A�էO�p�ɤ��ɤ���R��
    ''' </summary>
    ''' <param name="strDate"></param>
    ''' <param name="strDep"></param>
    ''' <param name="strPer_NO"></param>
    ''' <param name="strG_NO"></param>
    ''' <param name="MsgStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Check_Delete(ByVal strDate As String, ByVal strDep As String, ByVal strPer_NO As String, ByVal strG_NO As String, ByVal MsgStr As String) As Boolean


        Check_Delete = True

        If strG_NO = "�L" Then
            ''�ˬd���w����A�����A�ӤH�s���O�_�w�i��A�ӤH�p��A�ӤH�p�ɡA�]�L�էO�^
            Dim dec As New ProductionSumPiecePersonnelControl
            Dim dcil As New List(Of ProductionSumPiecePersonnelInfo)

            dcil = dec.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, strPer_NO, Nothing, strDep, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDate, Nothing, strDate, Nothing)

            If dcil.Count > 0 Then
                MsgBox(MsgStr & "�����u�s����Ѥw�i��ӤH�p��ާ@")
                Check_Delete = False
                Exit Function
            End If

            Dim dec1 As New ProductionSumTimePersonnelControl
            Dim dcil1 As New List(Of ProductionSumTimePersonnelInfo)

            dcil1 = dec1.ProductionSumTimePersonnel_GetList(Nothing, strPer_NO, Nothing, strDep, Nothing, strDate, Nothing, strDate, Nothing, Nothing, Nothing)

            If dcil1.Count > 0 Then
                MsgBox(MsgStr & "�����u�s����Ѥw�i��ӤH�p�ɾާ@")
                Check_Delete = False
                Exit Function
            End If

        Else
            ''�ˬd���w����A�����A�էO�s���O�_�w�i��A�էO�p��A�էO�p�ɡA�]���էO�^

            Dim dec As New ProductionSumPieceWorkGroupControl
            Dim dcil As New List(Of ProductionSumPieceWorkGroupInfo)

            dcil = dec.ProductionSumPieceWorkGroup_GetList(Nothing, Nothing, Nothing, strG_NO, strDep, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDate, Nothing, strDate, Nothing)

            If dcil.Count > 0 Then
                MsgBox(MsgStr & "���էO�s����Ѥw�s�b��էO�p��")
                Check_Delete = False
                Exit Function
            End If

            Dim dec1 As New ProductionSumTimeWorkGroupControl
            Dim dcil1 As New List(Of ProductionSumTimeWorkGroupInfo)

            dcil1 = dec1.ProductionSumTimeWorkGroup_GetList(Nothing, strPer_NO, strG_NO, strDep, Nothing, strDate, Nothing, strDate, Nothing, Nothing)

            If dcil1.Count > 0 Then
                MsgBox(MsgStr & "�����u�s��,�b���շ�Ѥw�i��էO�p�ɾާ@")
                Check_Delete = False
                Exit Function
            End If

        End If
    End Function

    Private Sub popPiecePersonnelEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPiecePersonnelEdit.Click
        On Error Resume Next
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If


        Dim strDate1, strDep1, strPer_NO1, strG_NO1 As String

        strDate1 = GridView1.GetFocusedRowCellValue("Per_Date").ToString
        strDep1 = GridView1.GetFocusedRowCellValue("DepID").ToString
        strPer_NO1 = GridView1.GetFocusedRowCellValue("Per_NO").ToString
        strG_NO1 = GridView1.GetFocusedRowCellValue("G_NO").ToString

        If Check_Delete(strDate1, strDep1, strPer_NO1, strG_NO1, "����ק�,") = False Then Exit Sub


        Dim fr As ProductionPiecePersonnelDaySub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionPiecePersonnelDaySub Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue2 = GridView1.GetFocusedRowCellValue("Per_Num").ToString
        MTypeName = "PD_edit"

        fr = New ProductionPiecePersonnelDaySub
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popPiecePersonnelView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPiecePersonnelView.Click
        On Error Resume Next

        Dim fr As ProductionPiecePersonnelDaySub
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is ProductionPiecePersonnelDaySub Then
                fr.Activate()
                Exit Sub
            End If
        Next

        tempValue2 = GridView1.GetFocusedRowCellValue("Per_Num").ToString
        MTypeName = "PD_view"

        fr = New ProductionPiecePersonnelDaySub
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popPiecePersonnelSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPiecePersonnelSeek.Click
        Dim PC As New ProductionPiecePersonnelDayControl

        ProductionPiecePersonnelDayFind.ShowDialog()

        If tempValue = "F" Then
            Grid1.DataSource = PC.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, tempValue2, tempValue3, tempValue6, tempValue5, tempValue4, tempValue7, tempValue8, Nothing, Nothing, tempValue9, Nothing)
        End If

        ProductionPiecePersonnelDayFind.Dispose()

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing
        tempValue8 = Nothing
        tempValue9 = Nothing



    End Sub

    Private Sub popPiecePersonnelPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popPiecePersonnelPrint.Click
        tempValue = "D"
        rptProductionPiecePersonnel.ShowDialog()
        rptProductionPiecePersonnel.Dispose()

    End Sub
End Class