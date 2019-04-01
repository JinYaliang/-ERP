Imports LFERP.Library.WareHouse
Imports LFERP.Library.Shared
Imports LFERP.SystemManager
Imports LFERP.DataSetting
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.BarCode
Imports LFERP.FileManager
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Core
Imports LFERP.Library.Product
Imports LFERP.Library.KnifeWare

Public Class frmKnifeWareInventoryMain

    Dim VisibleGrid1 As Boolean
    Dim LoadBZ As String = ""
    Dim LableText As String

    Private Sub frmWareInventoryMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.GridShelves.AutoGenerateColumns = False
        'GridShelves.RowHeadersWidth = 15

        Dim mt As New WareHouseController
        Dim wic As New WareInventory.WareInventoryMTController
        Dim wil As New List(Of WareInventory.WareInventoryInfo)
        '  mt.WareHouse_LoadToTreeView(twWare, ErpUser.WareHouseList)

        'mt.WareHouse_LoadToTreeView(twWare, WareSelect(InUserID, "510401"))
        KnifeWareTreeView = twWare
        KnifeWareBarManager = BarManager1
        KnifeWareLoad(ImageList1, "510401")

        VisibleGrid1 = False

        PowerUser()
    End Sub
    Sub PowerUser() '�]�m�v���P�X�w�s�W�ۦP�]�p�G�Τ�֦��X�w���v�����\���w���w�s�ƶq�^
        'Dim pmws As New PermissionModuleWarrantSubController
        'Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510402")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInventorySafe.Enabled = True '
        'End If
        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510403")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then popChangePrice.Enabled = True '
        'End If

        ''@2013/1/11 �K�[
        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510404")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then VisibleGrid1 = True
        'End If

        ' ''2013-2-22 �i�M�����X���L
        'popWareInventoryBarCode.Enabled = False
        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510405")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then popWareInventoryBarCode.Enabled = True
        'End If

        'ToolStripDetail.Visible = False
        'pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510407")
        'If pmwiL.Count > 0 Then
        '    If pmwiL.Item(0).PMWS_Value = "�O" Then ToolStripDetail.Visible = True
        'End If
    End Sub

    Private Sub twWare_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles twWare.AfterSelect
        '
        Dim mt As New KnifeWareInventorySubControl
        Dim pt As New ProductInventoryController
        Dim mtt As New WareInventory.WareInventoryMTController

        If twWare.SelectedNode.Level = 0 Then
            mtt.LoadNodes(twType, twWare.SelectedNode.Tag())
            Grid1.DataSource = mt.KnifeWareInventorySub_GetListItem(Nothing, twWare.SelectedNode.Tag)
            GridControl1.DataSource = Nothing


            ''---------------------------------------------------------------------------------------------------
            If twType.Nodes.Count > 0 Then
                Dim k, l As Integer
                For k = 0 To twType.Nodes.Count - 1
                    twType.Nodes(k).Expand()
                    For l = 0 To twType.Nodes(k).Nodes.Count - 1
                        twType.Nodes(k).Nodes(l).Expand()
                    Next
                Next
                twType.SelectedNode = twType.Nodes(0)
            End If
            ''---------------------------------------------------------------------------------------------------

        End If
    End Sub

    Private Sub twType_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles twType.AfterSelect
        Dim mt As New KnifeWareInventorySubControl
        If twType.SelectedNode.Level = 1 Then
            Grid1.DataSource = mt.KnifeWareInventory_GetListType123(twType.SelectedNode.Tag, Nothing, Nothing, twWare.SelectedNode.Tag, Nothing, Nothing, Nothing)
        ElseIf twType.SelectedNode.Level = 2 Then
            Grid1.DataSource = mt.KnifeWareInventory_GetListType123(Nothing, twType.SelectedNode.Tag, Nothing, twWare.SelectedNode.Tag, Nothing, Nothing, Nothing)
        ElseIf twType.SelectedNode.Level = 3 Then
            Grid1.DataSource = mt.KnifeWareInventory_GetListType123(Nothing, Nothing, twType.SelectedNode.Tag, twWare.SelectedNode.Tag, Nothing, Nothing, Nothing)
        End If
    End Sub

    Private Sub GridView2_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        GridControl1.DataSource = Nothing

        If GridView2.RowCount <= 0 Then Exit Sub
        If GridControl1.Visible = True Then
            Dim wic As New KnifeWareInventorySubControl
            GridControl1.DataSource = wic.KnifeWareInventorySub_GetListItem(GridView2.GetFocusedRowCellValue("M_Code").ToString, Nothing)
        End If
    End Sub

    Private Sub popWareInventoryPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInventoryPrint.Click
        Dim dss As New DataSet

        Dim ltc As New CollectionToDataSet

        Dim mc As New KnifeWareInventorySubControl
        '********************************************************************************
        '*****************************2013-11-13 ���@�s�W********************************
        If (twWare.SelectedNode.Tag Is Nothing) Or (twWare.SelectedNode.Tag = "") Then
            MessageBox.Show("�п�ܭܮw")
            Return
        End If

        '********************************************************************************

        'ltc.CollToDataSet(dss, "KnifeWareInventory", mc.KnifeWareInventory_GetList(Nothing, Nothing, Nothing, Nothing, Nothing))   --Old
        ltc.CollToDataSet(dss, "KnifeWareInventory", mc.KnifeWareInventory_GetList(Nothing, twWare.SelectedNode.Tag, Nothing, Nothing, Nothing))
        PreviewRPT(dss, "rptKnifeWareInventory", "�M��w�s��", True, True)

        ltc = Nothing
    End Sub

    Private Sub frmWareInventoryMain_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        KnifeWareTreeView = twWare
        KnifeWareBarManager = BarManager1
        KnifeWareLoad(ImageList1, "510401")
    End Sub

    Private Sub popWareInventorySeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareInventorySeek.Click
        Dim mc As New KnifeWareInventorySubControl
        Dim fr As New frmKnifeSelect
        fr.ComboBoxEdit1.EditValue = "���ƦW��"
        tempValue = "�M��w�s"
        Try
            tempValue4 = twWare.SelectedNode.Tag
        Catch ex As Exception
        End Try
        fr.ShowDialog()
        If RefreshT = True Then
            Select Case tempValue
                Case 1
                    Grid1.DataSource = mc.KnifeWareInventory_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 2
                    Grid1.DataSource = mc.KnifeWareInventory_GetList(Nothing, tempValue4, tempValue2, Nothing, Nothing)
                Case 3
                    Grid1.DataSource = mc.KnifeWareInventory_GetList(Nothing, tempValue4, Nothing, Nothing, tempValue2)
                Case 4
                    Grid1.DataSource = mc.KnifeWareInventory_GetList(Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 5
                    Grid1.DataSource = mc.KnifeWareInventory_GetList(tempValue2, tempValue4, Nothing, Nothing, Nothing)
                Case 6
                    Dim ws As New KnifeWareSelectController
                    Grid1.DataSource = ws.KnifeWareInventorySub_GetList("�M��w�s", tempValue2)
                    RefreshT = False
            End Select
            tempValue = ""
            tempValue2 = ""
            tempValue4 = ""
        End If
    End Sub



    Private Sub ToolStripDull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDull.Click
        frmBrrowList.ReportTypeID = "DullKnife"
        frmBrrowList.ReportTypeName = "�b���M��"
        frmBrrowList.ShowDialog()
    End Sub

    Private Sub ToolStripWareInvent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripWareInvent.Click
        frmBrrowList.ReportTypeID = "KnifeInventory"
        frmBrrowList.ReportTypeName = "�M��ܮw�w�s"
        frmBrrowList.ShowDialog()
        frmBrrowList.Dispose()
    End Sub

    Private Sub popKnifeMatriCodeRec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popKnifeMatriCodeRec.Click
        frmBrrowList.ReportTypeID = "KnifeWareHouseSearch"
        frmBrrowList.ReportTypeName = "�M�㦬�o����"
        frmBrrowList.ReportWHID = twWare.SelectedNode.Tag
        frmBrrowList.ReportWHName = twWare.SelectedNode.Text

        frmBrrowList.ShowDialog()
        frmBrrowList.Dispose()
    End Sub

    Private Sub popKnifeMatriCodeBackUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popKnifeMatriCodeBackUp.Click
        frmBrrowList.ReportTypeID = "KnifeInventoryBackUp"
        frmBrrowList.ReportTypeName = "�w�s�ƥ�"
        frmBrrowList.ReportWHID = "*"
        frmBrrowList.ReportWHName = "����"
        frmBrrowList.ShowDialog()
        frmBrrowList.Dispose()
    End Sub
End Class