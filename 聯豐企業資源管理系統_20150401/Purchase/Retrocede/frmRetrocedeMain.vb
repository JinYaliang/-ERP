Imports LFERP.Library.Purchase.Retrocede
Imports LFERP.Library.Purchase.Acceptance
Imports LFERP.DataSetting
Imports LFERP.SystemManager
Imports LFERP.Library.Purchase.purselect
Public Class frmRetrocedeMain

    Private Sub frmRetrocedeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mc As New RetrocedeController
   
        Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        LoadUserPower()
    End Sub

    Private Sub popRetrocedeAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeAdd.Click
        On Error Resume Next
        Edit = False
        MTypeName = "RetrocedeAddEdit"
        'Dim fr As frmRetrocede
        'For Each fr In MDIMain.MdiChildren
        '    If TypeOf fr Is frmRetrocede Then
        '        fr.Activate()
        '        Exit Sub
        '    End If
        'Next
        'fr = New frmRetrocede
        'fr.MdiParent = MDIMain
        'fr.WindowState = FormWindowState.Maximized
        'fr.Show()
        CheckForm(frmRetrocede, frmRetrocede.Name)
        'Dim myfrm As New frmRetrocede
        'myfrm.ShowDialog()
    End Sub

    Private Sub popRetrocedeRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeRef.Click
        Dim mc As New RetrocedeController
 
        Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)



    End Sub

    Private Sub popRetrocedeDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeDel.Click
        Dim osc As New RetrocedeController
        Dim osilist As New List(Of RetrocedeInfo)

        If GridView1.RowCount = 0 Then Exit Sub
        osilist = osc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString)
        If osilist.Count <> 0 Then

            MsgBox("�L�k�R��,���h�f��w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If



        Dim StrA, strB As String
        StrA = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        strB = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
        If MsgBox("�A�T�w�R���h�f�渹��  '" & StrA & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            Dim mc As New RetrocedeInfo
            Dim mt As New RetrocedeController
            mc.R_RetrocedeNO = StrA
            If mt.Retrocede_Delete(mc) = True Then

                Grid1.DataSource = mt.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                '����禬�檬�A
                Dim ac2 As New AcceptanceController
                Dim aciList As New List(Of AcceptanceInfo)
                Dim aci2 As New AcceptanceInfo
                aciList = ac2.Acceptance_GetList(Nothing, strB, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                '  Label1.Text = GridView1.GetFocusedRowCellValue("A_AcceptanceNO").ToString
                If aciList.Item(0).A_PayCheck = True Then
                    aci2.A_Detail = "�w����"
                Else
                    If aciList.Item(0).A_Check = True Then
                        aci2.A_Detail = "�禬"
                    Else
                        aci2.A_Detail = "�Ȧ�"
                    End If
                End If
                aci2.A_AcceptanceNO = strB
                ac2.Acceptance_UpdateDetail(aci2)
            Else
                MsgBox("�R������")
            End If

        End If

    End Sub

    Private Sub popRetrocedeEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeEdit.Click
        On Error Resume Next
        Dim osc As New RetrocedeController
        Dim osilist As New List(Of RetrocedeInfo)


        osilist = osc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString)
        If osilist.Count <> 0 Then



            MsgBox("�L�k�ק�,���h�f��w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        MTypeName = "RetrocedeAddEdit"
        Edit = True

        Dim fr As frmRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmRetrocede
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

    Private Sub popRetrocedePrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedePrint.Click
        On Error Resume Next
        Dim ds As New DataSet
        If GridView1.RowCount = 0 Then Exit Sub

        Dim ltc As New CollectionToDataSet
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
        Dim mcRetrocede As New LFERP.Library.Purchase.Retrocede.RetrocedeController
        Dim mcsysuser As New LFERP.SystemManager.SystemUser.SystemUserController

        ds.Tables.Clear()
        ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, GridView1.GetFocusedRowCellValue("CO_ID").ToString, Nothing, Nothing))
        ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(GridView1.GetFocusedRowCellValue("S_Supplier").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc2.CollToDataSet(ds, "Retrocede", mcRetrocede.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString))
        ltc3.CollToDataSet(ds, "SystemUser", mcsysuser.SystemUser_GetList(GridView1.GetFocusedRowCellValue("R_Action").ToString, Nothing, Nothing))

        PreviewRPT(ds, "rptRetrocede", " �h�f��--" & GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, True, False)
        ltc = Nothing
        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing

    End Sub

    Private Sub popRetrocedeCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeCheck.Click
        On Error Resume Next
        Dim osc As New RetrocedeController
        Dim osilist As New List(Of RetrocedeInfo)


        osilist = osc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString)
        If osilist.Count <> 0 Then



            MsgBox("�L�k�f��,���h�f��w�f�֡I", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If


        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        MTypeName = "RetrocedeCheck"

        Dim fr As frmRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmRetrocede
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
        'Dim myfrm As New frmRetrocede
        'myfrm.ShowDialog()
    End Sub

    Private Sub popRetrocedeAccCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeAccCheck.Click
        On Error Resume Next
        'Dim osc As New RetrocedeController
        'Dim osilist As New List(Of RetrocedeInfo)

        Dim ac As New RetrocedeController
        Dim ai As New List(Of RetrocedeInfo)

        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        ai = ac.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2)
        If ai.Item(0).R_Check = False Then
            MsgBox("�����������h�f�f��,�|�p���~��f��!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If


        MTypeName = "RetrocedeAccountCheck"
        '     Edit = True

        Dim fr As frmRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmRetrocede
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popRetrocedeFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeFile.Click
        '�եΦ��q��y���������
        '  If Grid.Rows.Count = 0 Then Exit Sub
        If GridView1.RowCount = 0 Then Exit Sub

        Dim open, update, down, edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        '  If Grid.Rows.Count = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400408")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then update = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400409")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then down = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then down = False
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400410")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then edit = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400411")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then del = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400412")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400413")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then open = True
            If pmwiL.Item(0).PMWS_Value = "�_" Then open = False
        End If

        FileShow("4001", GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString, open, update, down, edit, del, detail)
    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400401")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popRetrocedeAdd.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400402")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popRetrocedeEdit.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400403")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popRetrocedeDel.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400404")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popRetrocedeCheck.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400405")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popRetrocedePrint.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "400406")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "�O" Then popRetrocedeAccCheck.Enabled = True
        End If



    End Sub

    Private Sub popRetrocedeSeek_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeSeek.Click
        tempValue = "�h�f�޲z"
        Dim fr As New FrmpurSelect
        FrmpurSelect.ShowDialog()
        Dim mc As New RetrocedeController
        Dim pc As New PurSelectControl
        Select Case tempValue
            Case "1" '���ʳ渹
                Grid1.DataSource = mc.Retrocede_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "2"  '�h�f�渹
                Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2)
            Case "3" '���ƦW��
                Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Case "4" '���ƽs�X
                Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)
                'Case "5"  '�������O
            Case "5"  '������
                Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing)
            Case "6"
                Grid1.DataSource = pc.Retrocede_GetList("�h�f�޲z", tempValue2)
        End Select
        tempValue = ""
        tempValue2 = ""
        'Dim mc As New RetrocedeController
        'Dim myfrm As New frmRetrocedeSelect2
        'myfrm.ShowDialog()
        'Select Case tempValue
        '    Case "1"
        '        '�禬�渹
        '        Grid1.DataSource = mc.Retrocede_GetList(Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    Case "2"
        '        '�e�f�渹
        '        Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing)
        '    Case "3"
        '        '���ʳ渹
        '        Grid1.DataSource = mc.Retrocede_GetList(tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    Case "4"
        '        '�妸
        '        Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    Case "5"
        '        '�h�f�渹
        '        Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2)
        '    Case "6"
        '        '���ƽs�X
        '        Grid1.DataSource = mc.Retrocede_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing, Nothing)

        'End Select

        'tempValue = ""
        'tempValue2 = ""

        'tempValue = "�h�f�޲z"
        'Dim fr As New FrmpurSelect
        'fr.ShowDialog()
        'Dim pc As New PurSelectControl
        'Grid1.DataSource = pc.Retrocede_GetList("�h�f�޲z", tempValue2)
        'tempValue2 = ""
    End Sub

    Private Sub popRetrocedeView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popRetrocedeView.Click
        On Error Resume Next

        tempValue2 = GridView1.GetFocusedRowCellValue("R_RetrocedeNO").ToString
        MTypeName = "RetrocecdeView"


        Dim fr As frmRetrocede
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmRetrocede Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmRetrocede
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popRetrocedeNotAllPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popRetrocedeNotAllPrint.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        Dim fr As frmRetrocedeDate
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmRetrocedeDate Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmRetrocedeDate
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()

    End Sub

  
End Class