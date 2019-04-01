Imports LFERP.Library.ProductionKaiLiaoReturn
Imports LFERP.SystemManager
Imports LFERP.DataSetting


Public Class frmProductionKaiLiaoReturnMain
    Dim strDPT As String ''�u��ܥ��t�O

    Private Sub frmProductionKaiLiaoReturnMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ui As New List(Of UserPowerInfo)
        Dim uc As New UserPowerControl
        ui = uc.UserPower_GetList(InUserID, Nothing, Nothing, Nothing)

        If ui.Count = 0 Then
            Exit Sub
        Else
            If ui(0).UserRank = "�޲z" Then
                strDPT = Nothing
            Else
                strDPT = Mid(ui(0).DepID, 1, 1)
            End If
        End If

        PowerUser()
        cmsProductionRef_Click(Nothing, Nothing)
    End Sub

    Private Sub cmsProductionRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionRef.Click
        

        Dim rc As New ProductionKaiLiaoReturnControl
        Me.Grid.DataSource = rc.ProductionKaiLiaoReturn_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strDPT, Nothing, Nothing, Nothing)
    End Sub

    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "8802001")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionAdd.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "8802002")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionEdit.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "8802003")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionDel.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "8802004")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionCheck.Enabled = True
            End If
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "8802005")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then
                cmsProductionPrint.Enabled = True
            End If
        End If
        
    End Sub

    Private Sub cmsProductionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionAdd.Click
        ''�s�W
        On Error Resume Next
        Edit = False

        Dim fr As frmProductionKaiLiaoReturn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionKaiLiaoReturn Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionKaiLiaoReturn
        fr.EditItem = "Add"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsProductionEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionEdit.Click
        ''�ק�
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As New List(Of ProductionKaiLiaoReturnInfo)
        Dim pkc As New ProductionKaiLiaoReturnControl
        pi = pkc.ProductionKaiLiaoReturn_GetList(Nothing, GridView1.GetFocusedRowCellValue("R_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi.Count <= 0 Then
            Exit Sub
        End If

        If pi(0).R_ActQty > 0 Then
            MsgBox("���h�Ƴ�w�i��h�ƾާ@,�����\�ק�!")
            Exit Sub
        End If

        If pi(0).RCheck = True Then
            MsgBox("���h�Ƴ�w�Q�f��,�����\�ק�!")
            Exit Sub
        Else
            Dim fr As frmProductionKaiLiaoReturn
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionKaiLiaoReturn Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            fr = New frmProductionKaiLiaoReturn
            fr.EditItem = "Update"
            fr.EditValue = GridView1.GetFocusedRowCellValue("R_NO").ToString
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    Private Sub cmsProductionDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pi As New List(Of ProductionKaiLiaoReturnInfo)
        Dim pkc As New ProductionKaiLiaoReturnControl
        pi = pkc.ProductionKaiLiaoReturn_GetList(Nothing, GridView1.GetFocusedRowCellValue("R_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi.Count <= 0 Then
            Exit Sub
        End If

        If pi(0).R_ActQty > 0 Then
            MsgBox("���h�Ƴ�w�i��h�ƾާ@,�����\�R��!")
            Exit Sub
        End If

        If pi(0).RCheck = True Then
            MsgBox("���h�Ƴ�w�Q�f��,�����\�R��!")
            Exit Sub
        Else
            If MsgBox("�T�w�n�R���s����" & GridView1.GetFocusedRowCellValue("R_NO").ToString & "���h�Ƴ�ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                If pkc.ProductionKaiLiaoReturn_Delete(Nothing, GridView1.GetFocusedRowCellValue("R_NO").ToString) = True Then
                    MsgBox("�R����e�}�Ƴ榨�\!")
                Else
                    MsgBox("�R������,���ˬd��]!")
                End If

                cmsProductionRef_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub cmsProductionView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmProductionKaiLiaoReturn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionKaiLiaoReturn Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionKaiLiaoReturn
        fr.EditItem = "View"
        fr.EditValue = GridView1.GetFocusedRowCellValue("R_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub



    Private Sub cmsProductionCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionCheck.Click
        ''�ק�
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pi As New List(Of ProductionKaiLiaoReturnInfo)
        Dim pkc As New ProductionKaiLiaoReturnControl
        pi = pkc.ProductionKaiLiaoReturn_GetList(Nothing, GridView1.GetFocusedRowCellValue("R_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pi.Count <= 0 Then
            Exit Sub
        End If

        If pi(0).R_ActQty > 0 Then
            MsgBox("���h�Ƴ�w�i��h�ƾާ@,�����\�f��!")
            Exit Sub
        End If

        Dim fr As frmProductionKaiLiaoReturn
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionKaiLiaoReturn Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmProductionKaiLiaoReturn
        fr.EditItem = "Check"
        fr.EditValue = GridView1.GetFocusedRowCellValue("R_NO").ToString
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    Private Sub cmsProductionPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionPrint.Click
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        Dim pi As New List(Of ProductionKaiLiaoReturnInfo)
        Dim pkc As New ProductionKaiLiaoReturnControl
        Dim ltc As New CollectionToDataSet
        Dim dsPrint As New DataSet
        pi = pkc.ProductionKaiLiaoReturn_GetList(Nothing, GridView1.GetFocusedRowCellValue("R_NO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pi.Count <= 0 Then
            MsgBox("�h�Ƽƾڤ��s�b")
            Exit Sub
        End If

        ltc.CollToDataSet(dsPrint, "ProductionKaiLiaoReturn", pi)
        PreviewRPTDialog(dsPrint, "rptProductionKaiLiaoReturn", "�h�Ƴ�", True, True)
        ltc = Nothing
    End Sub

    Private Sub cmsProductionSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsProductionSelect.Click
        Dim rcc As New ProductionKaiLiaoReturnControl
        frmProductionKaiLiaoReturnSeek.ShowDialog()

        If tempValue = Nothing Then
            Exit Sub
        End If

        Me.Grid.DataSource = rcc.ProductionKaiLiaoReturn_GetList(Nothing, tempValue2, Nothing, tempValue3, tempValue4, tempValue6, tempValue7, tempValue5, Nothing, strDPT, Nothing, Nothing, Nothing)

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing
        tempValue4 = Nothing
        tempValue5 = Nothing
        tempValue6 = Nothing
        tempValue7 = Nothing

    End Sub
End Class