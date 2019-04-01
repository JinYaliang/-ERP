Imports LFERP.Library.Purchase.ApplyPurchase
Imports LFERP.SystemManager


Public Class frmApplyPurchaseMain

    Dim apc As New ApplyPurchaseControl

    Private Sub frmApplyPurchaseMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Grid1.DataSource = apc.ApplyPurchase_GetList(Nothing, Nothing, Nothing)
    End Sub

    Private Sub frmApplyPurchaseMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Grid1.DataSource = apc.ApplyPurchase_GetList(Nothing, Nothing, Nothing)
        LoadUserPower()
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400601")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400602")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdEdit.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400603")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400604")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400605")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdReCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400606")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then cmdHandle.Enabled = True
        End If

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmApplyPurchase
        'Dim fr As Form
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmApplyPurchase Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "���ʳ�"
        fr = New frmApplyPurchase
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim ai As List(Of ApplyPurchaseInfo)
        Dim ac As New ApplyPurchaseControl
        ai = ac.ApplyPurchase_GetList(GridView1.GetFocusedRowCellValue("AP_ID").ToString, Nothing, Nothing)
        If ai(0).AP_Check = True Then
            MsgBox("�����ʳ�w�f�֡A�����\�ק�!", 64, "����")
            Exit Sub
        Else
            Edit = True
            Dim fr As frmApplyPurchase
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmApplyPurchase Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue = "���ʳ�"
            tempValue3 = GridView1.GetFocusedRowCellValue("AP_ID").ToString
            fr = New frmApplyPurchase
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If

    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim api As New ApplyPurchaseInfo
        Dim apc As New ApplyPurchaseControl
        api = apc.ApplyPurchase_Get(GridView1.GetFocusedRowCellValue("AP_Num").ToString)

        If api.AP_Check = False Then
            If MsgBox("�T�w�R���渹��" & GridView1.GetFocusedRowCellValue("AP_ID").ToString & "�������O���H", MsgBoxStyle.YesNo + vbQuestion, "����") = MsgBoxResult.Yes Then
                If apc.ApplyPurchase_Delete(api.AP_ID, Nothing) = True Then
                    MsgBox("�R�����\!", 64, "����")
                    Grid1.DataSource = apc.ApplyPurchase_GetList(Nothing, Nothing, Nothing)

                End If
            End If
        Else
            MsgBox("�����ʳ�w�f�֡A�����\�R��!", 64, "����")
        End If

    End Sub

    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub

        Dim fr As frmApplyPurchase
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmApplyPurchase Then
                fr.Activate()
                Exit Sub
            End If
        Next
        tempValue = "�d��"
        tempValue3 = GridView1.GetFocusedRowCellValue("AP_ID").ToString
        fr = New frmApplyPurchase
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim ai As List(Of ApplyPurchaseInfo)
        Dim ac As New ApplyPurchaseControl
        ai = ac.ApplyPurchase_GetList(GridView1.GetFocusedRowCellValue("AP_ID").ToString, Nothing, Nothing)
        If ai(0).AP_Check = True Then
            MsgBox("�����ʳ�w�f�֡A���ݦA�f�֡I", 64, "����")
            Exit Sub
        Else
            Dim fr As frmApplyPurchase
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmApplyPurchase Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue = "�f��"
            tempValue3 = GridView1.GetFocusedRowCellValue("AP_ID").ToString
            fr = New frmApplyPurchase
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If

    End Sub

    Private Sub cmdReCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReCheck.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim ai As List(Of ApplyPurchaseInfo)
        Dim ac As New ApplyPurchaseControl
        ai = ac.ApplyPurchase_GetList(GridView1.GetFocusedRowCellValue("AP_ID").ToString, Nothing, Nothing)
        If ai(0).AP_Check = False Then
            MsgBox("�����ʳ��٥��f�֡A�����\�_�֡I", 64, "����")
            Exit Sub
        ElseIf ai(0).AP_ReCheck = True Then
            MsgBox("�����ʳ��٤w�_�֡A���ݦA�_�֡I", 64, "����")
            Exit Sub
        Else

            Dim fr As frmApplyPurchase
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmApplyPurchase Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            tempValue = "�_��"
            tempValue3 = GridView1.GetFocusedRowCellValue("AP_ID").ToString
            fr = New frmApplyPurchase
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If
    End Sub

    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        Grid1.DataSource = apc.ApplyPurchase_GetList(Nothing, Nothing, Nothing)
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim api As New ApplyPurchaseInfo
        Dim apc As New ApplyPurchaseControl
        tempAPID = ""
        tempAPCode = ""
        tempAPName = ""
        tempAPGauge = ""
        tempAPDPTID = ""
        tempAPDateStart = ""
        tempAPDateEnd = ""
        frmApplyPurchaseSelect.ShowDialog()
        If tempAPID <> "" Or tempAPCode <> "" Or tempAPName <> "" Or tempAPGauge <> "" Or tempAPDPTID <> "" Or tempAPDateStart <> "" Or tempAPDateEnd <> "" Then
            Grid1.DataSource = apc.ApplyPurchase_GetList1(tempAPID, tempAPCode, tempAPName, tempAPGauge, tempAPDPTID, tempAPDateStart, tempAPDateEnd)
            tempAPID = ""
            tempAPCode = ""
            tempAPName = ""
            tempAPGauge = ""
            tempAPDPTID = ""
            tempAPDateStart = ""
            tempAPDateEnd = ""
        End If

    End Sub

    Private Sub cmdHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHandle.Click
        On Error Resume Next

        If GridView1.RowCount = 0 Then Exit Sub
        Dim ai As List(Of ApplyPurchaseInfo)
        Dim ac As New ApplyPurchaseControl
        Dim i As Integer
        ai = ac.ApplyPurchase_GetList(GridView1.GetFocusedRowCellValue("AP_ID").ToString, Nothing, Nothing)
        For i = 0 To ai.Count - 1
            If ai(i).AP_M_Code = "" And ai(i).AP_Check = True And ai(i).AP_CheckWare = True Then
                Dim fr As frmApplyPurchase
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmApplyPurchase Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                tempValue = "���ʳ�B�z"
                tempValue3 = GridView1.GetFocusedRowCellValue("AP_ID").ToString
                fr = New frmApplyPurchase
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
                Exit Sub
            End If
        Next
        MsgBox("�����ʳ椣�ݳB�z!", 64, "����")
    End Sub

    Private Sub GridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Click

        If GridView1.RowCount = 0 Then Exit Sub

        Dim pmc As New LFERP.Library.Purchase.Purchase.PurchaseMainController

        If GridView1.GetFocusedRowCellValue("AP_M_Code").ToString() <> "" Then
            Grid2.DataSource = pmc.PurchaseSub_GetList("", GridView1.GetFocusedRowCellValue("AP_ID").ToString, GridView1.GetFocusedRowCellValue("AP_M_Code").ToString)
        Else
            Grid2.DataSource = Nothing
        End If
    End Sub

End Class