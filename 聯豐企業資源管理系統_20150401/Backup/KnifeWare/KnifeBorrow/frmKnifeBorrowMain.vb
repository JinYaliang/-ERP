Imports LFERP.Library.WareHouse
Imports LFERP.Library.KnifeWare
Imports LFERP.SystemManager

Public Class frmKnifeBorrowMain
    Dim ds As New DataSet
    'Dim LsPerNO As String

    Private Sub frmKnifeBorrowMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        XtraTabControl1.SelectedTabPage = XtraTabPage1
        CreateTables()

        Dim mt As New WareHouseController

        ''mt.WareHouse_LoadToTreeView(TreeView1, WareSelect("12022419", "50100101"))
        'Dim str As String = "'W40','W50'"
        'mt.WareHouse_LoadToTreeView(tv1, str)
        'ToolStripRefresh_Click(Nothing, Nothing)

        LabMsg.Text = ""
        LabPerNO.Text = ""
        labLsPerNO.Text = ""
        labLsPerName.Text = ""

        LoadBar()

    End Sub

    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("KnifeBorrow")
            .Columns.Add("Type3ID", GetType(String))
            .Columns.Add("Type3Name", GetType(String))
            .Columns.Add("Type2Name", GetType(String))
            .Columns.Add("Type1Name", GetType(String))
            .Columns.Add("B_Qty", GetType(Double))
            .Columns.Add("WI_SumQty", GetType(Double))
            .Columns.Add("WI_SQty", GetType(Double))
            .Columns.Add("WI_SReQty", GetType(Double))
            .Columns.Add("WH_ID", GetType(String))
            .Columns.Add("NO_ReturnSum", GetType(Double))
        End With
        Grid4.DataSource = ds.Tables("KnifeBorrow")


        With ds.Tables.Add("KnifeGroup")
            .Columns.Add("G_NO", GetType(String))
            .Columns.Add("G_Name", GetType(String))
            .Columns.Add("WH_ID", GetType(String))
        End With
        Grid1.DataSource = ds.Tables("KnifeGroup")

    End Sub

#Region "�էO�H��"
    Private Sub Grid2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid1.Click
        If Me.GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        Dim strA, StrB As String
        strA = Me.GridView1.GetFocusedRowCellValue("G_NO").ToString
        StrB = Me.GridView1.GetFocusedRowCellValue("WH_ID").ToString

        Dim GW As New KnifeWhiteUserController
        If strA = "*" Then
            Grid2.DataSource = GW.WhiteUser_GetListAll(Nothing, Nothing, StrB, Nothing, Nothing, False)
        Else
            Grid2.DataSource = GW.WhiteUser_GetListAll(Nothing, Nothing, StrB, Nothing, strA, False)
        End If

    End Sub

    ''' <summary>
    ''' �o��էO�H��
    ''' </summary>
    ''' <param name="_WH_ID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FreshGrid2(ByVal _WH_ID As String) As Boolean

        ds.Tables("KnifeGroup").Clear()

        Dim row1 As DataRow
        row1 = ds.Tables("KnifeGroup").NewRow
        row1("G_NO") = "*"
        row1("G_Name") = "����"
        row1("WH_ID") = _WH_ID
        ds.Tables("KnifeGroup").Rows.Add(row1)

        row1 = ds.Tables("KnifeGroup").NewRow
        row1("G_NO") = "init"
        row1("G_Name") = "����Ϥ�"
        row1("WH_ID") = _WH_ID
        ds.Tables("KnifeGroup").Rows.Add(row1)

        Dim gc As New KnifeGroupControl
        Dim gl As New List(Of KnifeGroupInfo)
        gl = gc.KnifeGroup_GetList(Nothing, Nothing, _WH_ID)

        Dim j As Integer
        If gl.Count > 0 Then
            For j = 0 To gl.Count - 1
                Dim row As DataRow
                row = ds.Tables("KnifeGroup").NewRow
                row("G_NO") = gl(j).G_NO
                row("G_Name") = gl(j).G_Name
                row("WH_ID") = _WH_ID
                ds.Tables("KnifeGroup").Rows.Add(row)
            Next
        End If

    End Function
#End Region

    ''' <summary>
    ''' �o��Grid4���ƾ�
    ''' </summary>
    ''' <param name="_WH_ID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function FreshGrid4(ByVal _WH_ID As String) As Boolean

        ds.Tables("KnifeBorrow").Clear()

        If _WH_ID = "" Then
            Exit Function
        End If

        Dim kcA As New KnifeWareInventorySubControl
        Dim kcL As New List(Of LFERP.Library.KnifeWare.KnifeWareInventorySubInfo)

        kcL = kcA.KnifeWareInventorySubTypeGroup_GetList1(_WH_ID, Nothing, Nothing)
        If kcL.Count <= 0 Then
            Exit Function
        End If

        Dim j As Integer

        For j = 0 To kcL.Count - 1
            Dim row As DataRow
            row = ds.Tables("KnifeBorrow").NewRow

            row("Type3ID") = kcL(j).Type3ID
            row("Type3Name") = kcL(j).Type3Name
            row("Type2Name") = kcL(j).Type2Name
            row("Type1Name") = kcL(j).Type1Name
            row("WI_SumQty") = kcL(j).WI_SQty + kcL(j).WI_SReQty
            row("WI_SQty") = kcL(j).WI_SQty
            row("WI_SReQty") = kcL(j).WI_SReQty
            row("WH_ID") = kcL(j).WH_ID

            row("NO_ReturnSum") = kcL(j).NO_ReturnSum

            Dim kc As New KnifeBorrowControl
            Dim kl As New List(Of KnifeBorrowInfo)
            kl = kc.KnifeBorrowSumB_Qty_GetList(_WH_ID, kcL(j).Type3ID, Nothing, Nothing, Nothing)
            row("B_Qty") = kl(0).B_Qty

            ds.Tables("KnifeBorrow").Rows.Add(row)
        Next
        '2014-03-17

        Me.GridView4.ActiveFilterString = "[WI_SumQty] > 0"

    End Function
    ''' <summary>
    ''' ��s�O��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ToolStripRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRefresh.Click
        Dim kc As New LFERP.Library.KnifeWare.KnifeBorrowControl
        Me.Grid3.DataSource = kc.KnifeBorrow_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

    Private Sub ToolStripRefreshOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRefreshOnly.Click
        Dim kc As New LFERP.Library.KnifeWare.KnifeBorrowControl
        Me.Grid3.DataSource = kc.KnifeBorrow_GetList(Nothing, Nothing, Nothing, tv1.SelectedNode.Tag, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv1.AfterSelect
        If e.Node.Level = 0 Then
            Dim kc As New LFERP.Library.KnifeWare.KnifeBorrowControl
            Dim GW As New KnifeWhiteUserController

            FreshGrid4(tv1.SelectedNode.Tag)

            Me.Grid3.DataSource = kc.KnifeBorrow_GetList(Nothing, Nothing, Nothing, tv1.SelectedNode.Tag, Nothing, DateAdd(DateInterval.Day, -7, CDate(Format(Now, "yyyy/MM/dd"))), Format(Now, "yyyy/MM/dd"), Nothing, Nothing, Nothing, Nothing, Nothing)
            Grid5.DataSource = Nothing
            LabMsg.Text = tv1.SelectedNode.Tag & "-" & tv1.SelectedNode.Text
            LabPerNO.Text = ""
            labLsPerNO.Text = ""
            labLsPerName.Text = ""
            FreshGrid2(tv1.SelectedNode.Tag) '�էO

            If GetNightWareHouse(tv1.SelectedNode.Tag) = True Then ''�Y�O�]�Z��
                Grid2.DataSource = GW.WhiteUser_NightGetList(tv1.SelectedNode.Tag, Nothing)
            Else
                Grid2.DataSource = GW.WhiteUser_GetListAll(Nothing, Nothing, tv1.SelectedNode.Tag, Nothing, Nothing, False)
            End If
        End If
    End Sub

    '''''' <summary>
    '''''' �P�_�O�_�O�]�Z��
    '''''' </summary>
    '''''' <param name="StrWHID"></param>
    '''''' <returns></returns>
    '''''' <remarks></remarks>
    ' ''Function GetNightWareHouse(ByVal StrWHID As String) As Boolean
    ' ''    Dim strWHRemark As String
    ' ''    GetNightWareHouse = False

    ' ''    Dim wc As New WareHouseController
    ' ''    Dim wl As New List(Of WareHouseInfo)
    ' ''    wl = wc.WareHouse_Get(StrWHID)
    ' ''    strWHRemark = wl(0).WH_Remark  '�p�G�O�]�Z��

    ' ''    If strWHRemark = "�]�Z��" Or strWHRemark = "Night" Then
    ' ''        GetNightWareHouse = True
    ' ''    End If
    ' ''End Function



    Function WHIDList(ByVal PMS_ID As String) As String()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        Dim strWHIDList As String
        Dim ArrWHID() As String
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, PMS_ID)
        If pmwiL.Count > 0 Then
            strWHIDList = pmwiL.Item(0).PMWS_Value
            ArrWHID = strWHIDList.Split(",")
        Else
            ArrWHID = Nothing
        End If
        Return ArrWHID
    End Function

    Function CheckWHID(ByVal strSelectWHID As String) As Boolean
        CheckWHID = False

        Dim strID As String
        ''-------------------------
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "510501")
        If pmwiL.Count > 0 Then
            strID = pmwiL.Item(0).PMWS_Value
        Else
            Exit Function
        End If
        ''-------------------------
        Dim arrWHID() As String
        arrWHID = strID.Split(",")
        Dim i As Integer
        For i = 0 To arrWHID.Length - 1
            If strSelectWHID = arrWHID(i) Then
                CheckWHID = True
                Exit Function
            Else

            End If
        Next
    End Function

    Private Sub ToolStripGetNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripGetNew.Click, ToolStripGetOld.Click
        If CheckWHID(Me.tv1.SelectedNode.Tag) = False Then
            MsgBox("�L���ܮw�v��", 64, "����")
            Exit Sub
        End If
        If Me.GridView5.RowCount <= 0 Then
            Exit Sub
        End If

        If tv1.SelectedNode Is Nothing Then
            MsgBox("�Х��T��ܭܮw!", 64, "����")
            Exit Sub
        Else
        End If

        'If labLsPerNO.Text = "" Then
        '    MsgBox("�п�ܭɤM�H�u��!", 64, "����")
        '    Exit Sub
        'End If

        '���d�ߥX�ܮw�����O�_���]�Z��,�d�ߤH���զW��
        ''Dim strWHRemark As String

        ''Dim wc As New WareHouseController
        ''Dim wl As New List(Of WareHouseInfo)
        ''wl = wc.WareHouse_Get(tv1.SelectedNode.Tag)
        ''strWHRemark = wl(0).WH_Remark  '�p�G�O�]�Z��

        ''Dim knl As New List(Of KnifeWhiteUserInfo)
        ''Dim knc As New KnifeWhiteUserController

        ''If GetNightWareHouse(tv1.SelectedNode.Tag) = True Then '�]�Z��
        ''    knl = knc.WhiteUser_NightGetList(tv1.SelectedNode.Tag, labLsPerNO.Text)
        ''Else
        ''    knl = knc.WhiteUser_GetListAll(Nothing, labLsPerNO.Text, tv1.SelectedNode.Tag, Nothing, Nothing, False)
        ''End If

        ''If knl.Count <= 0 Then
        ''    MsgBox(labLsPerNO.Text & " �L�b[" & tv1.SelectedNode.Text & "]��M���v��!", 64, "����")
        ''    Exit Sub
        ''End If

        'tempValue6 = tv1.SelectedNode.Tag
        'tempValue7 = tv1.SelectedNode.Text

        tempValue3 = sender.tag
        tempValue4 = Nothing
        tempValue5 = Me.GridView5.GetFocusedRowCellValue("M_Code").ToString
        '�s�W
        'frmKnifeBorrow.StrBNo = labLsPerNO.Text
        frmKnifeBorrow.strWHID = tv1.SelectedNode.Tag
        frmKnifeBorrow.strWH_Name = tv1.SelectedNode.Text

        frmKnifeBorrow.ShowDialog()
        frmKnifeBorrow.Dispose()

        tempValue6 = Nothing
        tempValue7 = Nothing
    End Sub


    Private Sub Grid4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid4.Click
        If GridView4.RowCount <= 0 Then Exit Sub

        Dim bc As New KnifeWareInventorySubControl

        Dim StrType3ID As String
        Dim StrWHID As String 'WH_ID

        StrType3ID = GridView4.GetFocusedRowCellValue("Type3ID").ToString
        StrWHID = GridView4.GetFocusedRowCellValue("WH_ID").ToString
        'Grid5.DataSource = bc.KnifeWareInventorySubType_GetList(StrWHID, Nothing, StrType3ID, Nothing, Nothing, "Zero")
        Grid5.DataSource = bc.KnifeWareInventorySubType_GetList(StrWHID, Nothing, StrType3ID, Nothing, Nothing, Nothing)
        Me.GridView5.ActiveFilterString = "[WI_QtyAll] > 0"

        LabMsg.Text = tv1.SelectedNode.Tag & "-" & tv1.SelectedNode.Text & "[" & GridView4.GetFocusedRowCellValue("Type3Name").ToString & "]"
    End Sub

    Private Sub ToolStripView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripView.Click
        If Me.GridView3.RowCount <= 0 Then
            Exit Sub
        End If

        tempValue4 = Me.GridView3.GetFocusedRowCellValue("B_NO")
        tempValue3 = "View"

        frmKnifeBorrow.ShowDialog()
        frmKnifeBorrow.Dispose()

    End Sub

    Private Sub Grid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid2.Click
        If Me.GridView2.RowCount <= 0 Then
            Exit Sub
        End If

        labLsPerNO.Text = Me.GridView2.GetFocusedRowCellValue("W_UserID").ToString
        labLsPerName.Text = Me.GridView2.GetFocusedRowCellValue("W_UserName").ToString
        LabPerNO.Text = "��M�H: "

    End Sub

    'Public Sub LoadBar()

    '    Dim WH_Name, WH_ID As String
    '    Dim i As Integer
    '    For i = 0 To 1
    '        If (i = 0) Then
    '            WH_Name = "�`��"
    '            WH_ID = "W4001"
    '        Else
    '            WH_Name = "����"
    '            WH_ID = "W50"
    '        End If

    '        Dim BarLargeButtonItem As New DevExpress.XtraBars.BarLargeButtonItem
    '        BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {BarLargeButtonItem})
    '        Dim bar1 As DevExpress.XtraBars.Bar = BarManager1.Bars.Item(0)
    '        bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(BarLargeButtonItem)})
    '        BarLargeButtonItem.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    '        BarLargeButtonItem.Id = i
    '        BarLargeButtonItem.Glyph = ImageList1.Images(0)
    '        BarLargeButtonItem.Caption = WH_Name
    '        BarLargeButtonItem.Tag = WH_ID
    '        BarLargeButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
    '        BarLargeButtonItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003

    '        AddHandler BarLargeButtonItem.ItemClick, AddressOf KnifeWareLoadEvent
    '    Next
    '    If BarManager1.Items.Count > 0 Then
    '        Loadtree(BarManager1.Items(0).Tag)
    '    End If
    'End Sub

    Public Sub LoadBar()

        Dim mt As New WareHouseController
        Dim mtl As New List(Of WareHouseInfo)
        mtl = mt.WareHouse_GetList(WareSelect(InUserID, "510503"))

        If mtl.Count <= 0 Then
            Exit Sub
        End If

        Dim i As Integer
        For i = 0 To mtl.Count - 1
            ''-------------------------------------------
            If mtl(i).WH_PID = "" Then
                Dim BarLargeButtonItem As New DevExpress.XtraBars.BarLargeButtonItem
                BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {BarLargeButtonItem})
                Dim bar1 As DevExpress.XtraBars.Bar = BarManager1.Bars.Item(0)
                bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(BarLargeButtonItem)})
                BarLargeButtonItem.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
                BarLargeButtonItem.Id = i
                BarLargeButtonItem.Glyph = ImageList1.Images(0)
                BarLargeButtonItem.Caption = mtl(i).WH_Name
                BarLargeButtonItem.Tag = mtl(i).WH_ID
                BarLargeButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
                BarLargeButtonItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003

                AddHandler BarLargeButtonItem.ItemClick, AddressOf KnifeWareLoadEvent
            End If
        Next

        If BarManager1.Items.Count > 0 Then
            Loadtree(BarManager1.Items(0).Tag)
        End If

    End Sub


    Private Sub Loadtree(ByVal sTag As String)
        Dim i As Integer
        Dim mc As New WareHouseController
        Dim mi As New List(Of WareHouseInfo)
        mi = mc.WareHouse_GetList("'" & sTag & "'")
        tv1.Nodes.Clear()
        If mi.Count < 1 Then
            Exit Sub
        End If

        For i = 0 To mi.Count - 1
            If mi(i).WH_PID = "" Then
            Else
                tv1.Nodes.Add(mi(i).WH_Name).Tag = mi(i).WH_ID
            End If
        Next
        tv1.SelectedNode = tv1.Nodes(0)
    End Sub


    Private Sub KnifeWareLoadEvent(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim i As Integer
        Dim sTag As String = CType(sender.PressedLink.Item, DevExpress.XtraBars.BarLargeButtonItem).Tag
        For i = 0 To BarManager1.Bars(0).ItemLinks.Count - 1
            If CType(sender.PressedLink.Item, DevExpress.XtraBars.BarLargeButtonItem).Id = i Then
                CType(sender.PressedLink.Item, DevExpress.XtraBars.BarLargeButtonItem).Down = True
            Else
                CType(BarManager1.Bars(0).ItemLinks(i).Item, DevExpress.XtraBars.BarLargeButtonItem).Down = False
            End If
        Next
        Dim mc As New WareHouseController
        Dim mi As New List(Of WareHouseInfo)
        mi = mc.WareHouse_GetList("'" & sTag & "'")
        tv1.Nodes.Clear()
        If mi.Count < 1 Then
            Exit Sub
        End If

        For i = 0 To mi.Count - 1
            If mi(i).WH_PID = "" Then
            Else
                tv1.Nodes.Add(mi(i).WH_Name).Tag = mi(i).WH_ID
            End If
        Next
        tv1.SelectedNode = tv1.Nodes(0)
    End Sub

    Private Sub ToolStripFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripFind.Click
        Dim kc As New LFERP.Library.KnifeWare.KnifeBorrowControl
        Dim fr As New frmKnifeSelect
        fr.ComboBoxEdit1.Properties.Items.Remove("�M��W��")
        fr.ComboBoxEdit1.Properties.Items.Add("�ɤM�渹")
        fr.ComboBoxEdit1.Properties.Items.Add("�ɤM�y����")
        tempValue = "�M��ɤM"
        Try
            tempValue4 = tv1.SelectedNode.Tag
        Catch ex As Exception
        End Try
        fr.ShowDialog()
        If RefreshT = True Then
            Select Case tempValue
                Case 1
                    Grid3.DataSource = kc.KnifeBorrow_GetList(Nothing, tempValue2, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 2
                    Grid3.DataSource = kc.KnifeBorrow_GetList(Nothing, Nothing, tempValue2, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 3
                    Grid3.DataSource = kc.KnifeBorrow_GetList(Nothing, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2)
                Case 4
                    Grid3.DataSource = kc.KnifeBorrow_GetList(tempValue2, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Case 5
                    Grid3.DataSource = kc.KnifeBorrow_GetList(Nothing, Nothing, Nothing, tempValue4, Nothing, Nothing, Nothing, Nothing, Nothing, tempValue2, Nothing, Nothing)
                Case 6
                    Dim ws As New KnifeWareSelectController
                    Grid3.DataSource = ws.WareBorrow_Getlist("�M��ɤM", tempValue2)
                    RefreshT = False
            End Select
            tempValue = ""
            tempValue2 = ""
            tempValue4 = ""
        End If
    End Sub

 
    Private Sub OToolStripExel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OToolStripExel.Click

        If GridView3.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "�ɥXExcel"
        saveFileDialog.Filter = "Excel2003���(*.xls)|*.xls"
        '|Excel2007�ΥH�W���(*.xlsx)|*.xlsx  '��e����2007 �H�ΥH�W�������~�I
        Dim dialogResult__1 As DialogResult = saveFileDialog.ShowDialog(Me)
        If dialogResult__1 = Windows.Forms.DialogResult.OK Then
            Grid3.ExportToExcelOld(saveFileDialog.FileName)
            DevExpress.XtraEditors.XtraMessageBox.Show("�O�s���\�I", "����", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ToolStripUseList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripUseList.Click

        If Me.GridView2.RowCount <= 0 Then
            Exit Sub
        End If

        frmBrrowList.ReportPerID = Me.GridView2.GetFocusedRowCellValue("W_UserID").ToString
        frmBrrowList.ReportTypeID = "UserKnifeList"
        frmBrrowList.ReportTypeName = "�ΤM�M��"
        frmBrrowList.ShowDialog()

    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        If Me.GridView3.RowCount <= 0 Then
            Exit Sub
        End If
        Dim bc As New KnifeBorrowControl
        Dim objInfo As New List(Of KnifeBorrowInfo)
        Dim ltc As New CollectionToDataSet
        Dim dsBrrowKnife As New DataSet
        objInfo = bc.KnifeBorrow_GetList(Nothing, Me.GridView3.GetFocusedRowCellValue("B_NO"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If objInfo.Count <= 0 Then
            MsgBox("�L�ƾڦs�b!", 64, "����")
            Exit Sub
        End If

        ltc.CollToDataSet(dsBrrowKnife, "KnifeBorrow", objInfo)
        PreviewRPTDialog(dsBrrowKnife, "rptBrrowKnifeRecord", "��M�O��", True, True)
        ltc = Nothing
    End Sub

    Private Sub ToolStripBorrowSamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBorrowSamp.Click
        If (Me.GridView2.GetFocusedRowCellValue("W_UserID")) Is Nothing Then
            frmBrrowList.ReportPerID = Nothing
        Else
            frmBrrowList.ReportPerID = Me.GridView2.GetFocusedRowCellValue("W_UserID")
        End If
        frmBrrowList.ReportWHID = tv1.SelectedNode.Tag
        frmBrrowList.ReportTypeID = "KnifeBrrowByGNO"
        frmBrrowList.ReportTypeName = "���u��M���p-��²��"
        frmBrrowList.ShowDialog()
    End Sub

    Private Sub ToolStripFenBu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripFenBu.Click
        If GridView4.RowCount <= 0 Then
            Exit Sub
        End If

        If tv1.SelectedNode.Tag = "" Then
            MsgBox("�Х���ܭܮw!", 64, "����")
            Exit Sub
        End If

        If GridView4.GetFocusedRowCellValue("Type3ID").ToString = "" Then
            MsgBox("�Х��������!", 64, "����")
            Exit Sub
        End If


        Dim bc As New KnifeBorrowControl
        Dim objInfo As New List(Of KnifeBorrowInfo)
        Dim ltc As New CollectionToDataSet
        Dim dsBrrowKnife As New DataSet
        objInfo = bc.KnifeBorrow_GetList(Nothing, Nothing, Nothing, tv1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, Nothing, GridView4.GetFocusedRowCellValue("Type3ID"), "Zero", Nothing)
        If objInfo.Count <= 0 Then
            MsgBox("�L�ƾڦs�b!", 64, "����")
            Exit Sub
        End If

        ltc.CollToDataSet(dsBrrowKnife, "UseKnifeScatter", objInfo)
        PreviewRPTDialog(dsBrrowKnife, "15rptUseKnifeScatter", "��M�O��", True, True)
        ltc = Nothing
    End Sub

    Private Sub ToolStripBrrowRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripBrrowRecord.Click

        If tv1.SelectedNode.Tag = "" Then
            MsgBox("�Х���ܭܮw!", 64, "����")
            Exit Sub
        End If

        frmBrrowList.ReportWHID = tv1.SelectedNode.Tag
        frmBrrowList.ReportWHName = tv1.SelectedNode.Text
        frmBrrowList.ReportTypeID = "KnifeBorrow"
        frmBrrowList.ReportTypeName = "�M��ɤM�O��"
        frmBrrowList.ShowDialog()
        frmBrrowList.Dispose()
    End Sub
End Class