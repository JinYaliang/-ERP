Imports LFERP.Library.Outward
Imports LFERP.SystemManager

Public Class FormRetrocedeMain

    Private Sub FormRetrocedeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim orc As New OutwardRetrocedeControl
        Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        LoadUserPower()
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popRetrocedeAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700302")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popRetrocedeEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700303")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popRetrocedeDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700304")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popRetrocedeCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700305")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popRetrocedePrint.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700306")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popRetrocedeAccCheck.Enabled = True
        End If
    End Sub

    Private Sub popRetrocedeAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeAdd.Click
        On Error Resume Next
        Edit = False

        Dim fr As FormRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is FormRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        MTypeName = "RetrocedeAddEdit"
        fr = New FormRetrocede
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub popRetrocedeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeEdit.Click
        On Error Resume Next
        Dim orc As New OutwardRetrocedeControl
        Dim oriL As List(Of OutwardRetrocedeInfo)
        oriL = orc.OutwardRetrocede_GetList(GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)
        If oriL(0).R_AccountCheck = True Or oriL(0).R_Check = True Then
            MsgBox("此退貨單已審核或復核，不允許修改！")
            Exit Sub
        End If
        Edit = True
        Dim fr As FormRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is FormRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        MTypeName = "RetrocedeAddEdit"

        fr = New FormRetrocede
        fr.MdiParent = MDIMain
        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        fr.Show()
    End Sub

    Private Sub popRetrocedeDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeDel.Click
        On Error Resume Next
        Dim orc As New OutwardRetrocedeControl
        Dim oriL As List(Of OutwardRetrocedeInfo)
        If GridView1.RowCount = 0 Then Exit Sub

        oriL = orc.OutwardRetrocede_GetList(GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)
        If oriL(0).R_Check Then
            MsgBox("已審核單據，不能刪除！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
        If MsgBox("確定刪除表單 '" & GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString & " ' 嗎?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If orc.OutwardRetrocede_Delete(GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, Nothing) Then
                MsgBox("已刪除表單！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Else
                MsgBox("刪除失敗！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            End If
        End If
        oriL = orc.OutwardRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub popRetrocedeView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
     
        Dim fr As FormRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is FormRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        MTypeName = "RetrocedeView"

        fr = New FormRetrocede
        fr.MdiParent = MDIMain
        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        fr.Show()
    End Sub

    Private Sub popRetrocedeRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeRef.Click
        Dim orc As New OutwardRetrocedeControl
        Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub popRetrocedeCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeCheck.Click
        On Error Resume Next
        Dim orc As New OutwardRetrocedeControl
        Dim oriL As List(Of OutwardRetrocedeInfo)
        If GridView1.RowCount = 0 Then Exit Sub

        oriL = orc.OutwardRetrocede_GetList(GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)
        If oriL(0).R_AccountCheck = True Then
            MsgBox("已復核單據，不能修改", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
     
        'Dim fr As FormRetrocede
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is FormRetrocede Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        MTypeName = "RetrocedeCheck"
        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        Dim fr As New FormRetrocede
        'fr.MdiParent = MDIMain
        fr.ShowDialog()
    End Sub

    Private Sub popRetrocedeAccCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeAccCheck.Click
        On Error Resume Next
        Dim orc As New OutwardRetrocedeControl
        Dim oriL As List(Of OutwardRetrocedeInfo)
        If GridView1.RowCount = 0 Then Exit Sub

        oriL = orc.OutwardRetrocede_GetList(GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, Nothing, Nothing, Nothing, Nothing, Nothing)
        If oriL(0).R_Check = False Then
            MsgBox("未審核單據，不能復核", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If
       
        Dim fr As FormRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is FormRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        MTypeName = "RetrocedeAccountCheck"
        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        fr = New FormRetrocede
        fr.MdiParent = MDIMain
        fr.Show()
    End Sub

    Private Sub popRetrocedeSeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeSeek.Click
        Dim orc As New OutwardRetrocedeControl
        Dim fr As New FormRetrocedeSelect2
        fr.ShowDialog()
        Select Case tempValue
            Case "1"
                '驗收單號
                Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing)
            Case "2"
                '送貨單號
                Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing)
            Case "3"
                '外發單號
                Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)
            Case "4"
                '批次
                Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2)
            Case "5"
                '退貨單號
                Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)
            Case "6"
                '物料編碼
                Grid1.DataSource = orc.OutwardRetrocede_GetList(Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing)

        End Select

        tempValue = ""
        tempValue2 = ""
    End Sub
    ''附加文件
    Private Sub popRetrocedeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeFile.Click

        Dim open, update, down, edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700308")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then update = True
            If pmwiL.Item(0).PMWS_Value = "否" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700309")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then down = True
            If pmwiL.Item(0).PMWS_Value = "否" Then down = False
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700310")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then edit = True
            If pmwiL.Item(0).PMWS_Value = "否" Then edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700311")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then del = True
            If pmwiL.Item(0).PMWS_Value = "否" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700312")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "否" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "700313")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then open = True
            If pmwiL.Item(0).PMWS_Value = "否" Then open = False
        End If

        FileShow("7003", GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, open, update, down, edit, del, detail)
    End Sub

    '退貨清單
    Private Sub popRetrocedePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedePrint.Click

    End Sub
End Class