Imports LFERP.Library.Production.ProductionType
Imports LFERP.SystemManager

Public Class frmProductionOutWardTypeMain

    Dim ptc As New ProductionOutWardTypeControl

    Private Sub frmProductionOutWardTypeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Grid.DataSource = ptc.ProductionOutWardType_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)

        PowerUser()
    End Sub

    '�]�m�v��
    Sub PowerUser()

        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881201")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then menuAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881202")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then menuEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "881203")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then menuDel.Enabled = True
        End If

    End Sub

    Private Sub menuAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmProductionOutWardType
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmProductionOutWardType Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        fr = New frmProductionOutWardType
        tempValue = "OutWardType"
        'fr.MdiParent = MDIMain
        'fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub menuEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuEdit.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim pti As List(Of ProductionOutWardTypeInfo)
        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("AutoID").ToString
 

        pti = ptc.ProductionOutWardType_GetList(strA, Nothing, Nothing, Nothing, Nothing)
        If pti.Count = 0 Then
            Exit Sub
        Else
            If pti(0).OW_ReQty > 0 Then
                MsgBox("�����~�t��H���w�i��L�~�o�ާ@�A�����\�ק�A���pô�q�����I")
                Exit Sub
            Else
                On Error Resume Next
                Edit = True
                Dim fr As frmProductionOutWardType
                'For Each fr In MDIMain.MdiChildren
                '    If TypeOf fr Is frmProductionOutWardType Then
                '        fr.Activate()
                '        Exit Sub
                '    End If
                'Next
                fr = New frmProductionOutWardType
                tempValue = "OutWardType"
                tempValue2 = GridView1.GetFocusedRowCellValue("AutoID").ToString
                'fr.MdiParent = MDIMain
                'fr.WindowState = FormWindowState.Maximized
                fr.Show()
            End If
        End If
    End Sub

    Private Sub menuDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuDel.Click
        If GridView1.RowCount = 0 Then Exit Sub
        Dim pti As List(Of ProductionOutWardTypeInfo)

        Dim strA As String
        strA = GridView1.GetFocusedRowCellValue("AutoID").ToString
   

        pti = ptc.ProductionOutWardType_GetList(strA, Nothing, Nothing, Nothing, Nothing)
        If pti.Count = 0 Then
            Exit Sub
        Else
            If pti(0).OW_ReQty > 0 Then
                MsgBox("�����~�t��H���w�i��L�~�o�ާ@�A�����\�R���I")
                Exit Sub
            Else
                If MsgBox("�T�w�n�R����e���~�t��H���[�u�n�D�ܡH", MsgBoxStyle.YesNo, "�R������") = MsgBoxResult.Yes Then
                    If ptc.ProductionOutWardType_Delete(GridView1.GetFocusedRowCellValue("AutoID").ToString) = True Then
                        MsgBox("�R����e���~�t��H���[�u�n�D���\!")
                    Else
                        MsgBox("�R������,���ˬd��]!")
                    End If
                    Grid.DataSource = ptc.ProductionOutWardType_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
                End If
            End If

        End If

    End Sub

    Private Sub menuQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuQuery.Click
        Dim fr As New frmProductionOutWardFindS
        fr.ShowDialog()

        If tempValue = "F" Then
            Grid.DataSource = ptc.ProductionOutWardType_GetList(Nothing, tempValue2, tempValue3, Nothing, Nothing)
        End If

        tempValue = Nothing
        tempValue2 = Nothing
        tempValue3 = Nothing

    End Sub

    Private Sub menuRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRef.Click
        Grid.DataSource = ptc.ProductionOutWardType_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub
End Class