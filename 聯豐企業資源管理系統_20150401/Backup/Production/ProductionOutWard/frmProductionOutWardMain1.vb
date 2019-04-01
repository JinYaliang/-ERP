Imports LFERP.Library.Production.ProductionOutWard


Public Class frmProductionOutWardMain

    Dim poc As New ProductionOutWardControl

    '@ 2012/2/21 �ק� �u��ܷ�e15�Ѥ��O��
    Private Sub frmProductionOutWardMain1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Grid.DataSource = poc.ProductionOutWard_GetList(Nothing, Nothing, Nothing, "�o��", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -15, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        Grid2.DataSource = poc.ProductionOutWard_GetList(Nothing, Nothing, Nothing, "����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -15, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))

        cmsInAdd.Visible = False   '�q�{��ܬ��~�o
        cmsOutAdd.Visible = True

        UserPower()
    End Sub
    '�b��ާ@���q�v���]�m�������v��
    Sub UserPower() '�]�m�v��()

    End Sub

    Private Sub cmsInAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsInAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmProductionOutWard
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionOutWard Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionOutWard
        tempValue = "OutWard"
        tempValue2 = "����"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsOutAdd.Click
        On Error Resume Next
        Edit = False
        Dim fr As frmProductionOutWard
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionOutWard Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionOutWard
        tempValue = "OutWard"
        tempValue2 = "�o��"
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsEdit.Click


        Dim strID As String
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            If GridView1.RowCount = 0 Then Exit Sub
            strID = GridView1.GetFocusedRowCellValue("OW_ID").ToString
        Else
            If GridView2.RowCount = 0 Then Exit Sub
            strID = GridView2.GetFocusedRowCellValue("OW_ID").ToString
        End If

        Dim poi As List(Of ProductionOutWardInfo)
        poi = poc.ProductionOutWard_GetList(strID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If poi.Count = 0 Then
            Exit Sub
        Else
            If poi(0).OW_Check = True Then
                MsgBox("����w�f��,�����\�ק�!")
                Exit Sub
            Else
                On Error Resume Next
                Edit = True
                Dim fr As frmProductionOutWard
                For Each fr In MDIMain.MdiChildren
                    If TypeOf fr Is frmProductionOutWard Then
                        fr.Activate()
                        Exit Sub
                    End If
                Next
                fr = New frmProductionOutWard
                tempValue = "OutWard"
                tempValue2 = poi(0).OW_Detail
                tempValue3 = strID
                fr.MdiParent = MDIMain
                fr.WindowState = FormWindowState.Maximized
                fr.Show()
            End If
        End If
    End Sub

    Private Sub cmsDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsDel.Click
        Dim strID As String
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            If GridView1.RowCount = 0 Then Exit Sub
            strID = GridView1.GetFocusedRowCellValue("OW_ID").ToString
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            If GridView2.RowCount = 0 Then Exit Sub
            strID = GridView2.GetFocusedRowCellValue("OW_ID").ToString
        End If

        Dim poi As List(Of ProductionOutWardInfo)
        poi = poc.ProductionOutWard_GetList(strID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If poi.Count = 0 Then
            Exit Sub
        Else
            If poi(0).OW_Check = True Then
                MsgBox("����w�f��,�����\�ק�!")
                Exit Sub
            Else
                If MsgBox("�A�T�w�R�����Ʀ��o�渹��  '" & strID & "'  ���O����?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    If poc.ProductionOutWard_Delete(strID, Nothing) = True Then
                        MsgBox("�R�����\!")
                        If XtraTabControl1.SelectedTabPageIndex = 0 Then    '@ 2012/2/21 �ק� �u��ܷ�e15�Ѥ��O��
                            Grid.DataSource = poc.ProductionOutWard_GetList(Nothing, Nothing, Nothing, "�o��", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -15, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
                        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
                            Grid2.DataSource = poc.ProductionOutWard_GetList(Nothing, Nothing, Nothing, "����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -15, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
                        End If

                    Else
                        MsgBox("�R�����渹�H������,���ˬd��]!")
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub cmsPreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPreView.Click
        On Error Resume Next

        Dim strID As String
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            If GridView1.RowCount = 0 Then Exit Sub
            strID = GridView1.GetFocusedRowCellValue("OW_ID").ToString
        Else
            If GridView2.RowCount = 0 Then Exit Sub
            strID = GridView2.GetFocusedRowCellValue("OW_ID").ToString
        End If
        Dim poi As List(Of ProductionOutWardInfo)
        poi = poc.ProductionOutWard_GetList(strID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If poi.Count = 0 Then
            Exit Sub
        End If
        Dim fr As frmProductionOutWard
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmProductionOutWard Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmProductionOutWard
        tempValue = "PreView"
        tempValue2 = poi(0).OW_Detail
        tempValue3 = strID
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    '�~�o��d�߫H��
    Private Sub cmsQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsQuery.Click

        Dim frm As New frmProductionOutWardSelect
        frm.ShowDialog()
        If frm.isClickcmdSave = True Then
            If XtraTabControl1.SelectedTabPageIndex = 0 Then
                Grid.DataSource = poc.ProductionOutWard_GetList(tempValue, tempValue2, tempValue3, "�o��", tempValue4, tempValue6, tempValue5, tempValue7, tempValue8, tempValue9, tempValue10, tempCode)
            ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
                Grid2.DataSource = poc.ProductionOutWard_GetList(tempValue, tempValue2, tempValue3, "����", tempValue4, tempValue6, tempValue5, tempValue7, tempValue8, tempValue9, tempValue10, tempCode)
            End If
        End If
        tempValue = ""
        tempValue2 = ""
        tempValue3 = ""
        tempValue4 = ""
        tempValue5 = ""
        tempValue6 = ""
        tempValue7 = ""
        tempValue8 = ""
        tempValue9 = ""
        tempValue10 = ""
        tempCode = ""
    End Sub

    '@ 2012/2/21 �ק� �u��ܷ�e15�Ѥ��O��
    Private Sub cmsRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsRef.Click
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            Grid.DataSource = poc.ProductionOutWard_GetList(Nothing, Nothing, Nothing, "�o��", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -15, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            Grid2.DataSource = poc.ProductionOutWard_GetList(Nothing, Nothing, Nothing, "����", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -15, CDate(Format(Now, "yyyy/MM/dd"))), CDate(Format(Now, "yyyy/MM/dd")))
        End If

    End Sub

    Private Sub cmsPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrint.Click

        Dim strA As String = ""

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            If GridView1.RowCount = 0 Then Exit Sub
            strA = GridView1.GetFocusedRowCellValue("OW_ID").ToString
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            If GridView2.RowCount = 0 Then Exit Sub
            strA = GridView2.GetFocusedRowCellValue("OW_ID").ToString
        End If

        Dim ds As New DataSet

        Dim ltc As New CollectionToDataSet
        Dim pfc As New ProductionOutWardControl    '���Ʀ��o�H��
        ds.Tables.Clear()

        Dim pfi As List(Of ProductionOutWardInfo)

        pfi = pfc.ProductionOutWard_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pfi.Count = 0 Then
            MsgBox("���s�b��e�渹�O��,���ˬd!")
            Exit Sub
        End If
        ltc.CollToDataSet(ds, "ProductionOutWard", pfc.ProductionOutWard_GetList(strA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))

        If pfi(0).OW_Detail = "�o��" Then
            PreviewRPT(ds, "rptProductionOutWard", "�����~�o��", True, True)
        ElseIf pfi(0).OW_Detail = "����" Then
            PreviewRPT(ds, "rptProductionOutWard1", "�����e�f��", True, True)
        End If

        ltc = Nothing

    End Sub

    Private Sub cmsPrintAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsPrintAll.Click

    End Sub

    Private Sub cmsCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsCheck.Click
        Dim strID As String = ""
        If XtraTabControl1.SelectedTabPageIndex = 0 Then

            If GridView1.RowCount = 0 Then Exit Sub
            strID = GridView1.GetFocusedRowCellValue("OW_ID").ToString
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then

            If GridView2.RowCount = 0 Then Exit Sub
            strID = GridView2.GetFocusedRowCellValue("OW_ID").ToString
        End If

        Dim poi As List(Of ProductionOutWardInfo)
        poi = poc.ProductionOutWard_GetList(strID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If poi.Count = 0 Then
            Exit Sub
        Else
            'If poi(0).OW_Check = True Then
            '    MsgBox("����w�f��,�����\�A���f�־ާ@!")
            '    Exit Sub
            'Else
            On Error Resume Next

            Dim fr As frmProductionOutWard
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmProductionOutWard Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            fr = New frmProductionOutWard
            tempValue = "Check"
            tempValue2 = poi(0).OW_Detail
            tempValue3 = strID
            fr.MdiParent = MDIMain
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
            'End If
        End If
    End Sub

    Private Sub XtraTabControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            cmsInAdd.Visible = False
            cmsOutAdd.Visible = True
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            cmsInAdd.Visible = True
            cmsOutAdd.Visible = False
        End If

    End Sub


End Class