Imports LFERP.Library
Imports LFERP.Library.Material
Imports LFERP.SystemManager
Imports LFERP.Library.Purchase.Purchase
Imports LFERP.Library.Purchase.Quotation
Imports LFERP.Library.WareHouse
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core

Public Class frmMaterialMain
    Dim mc As New MaterialController
    Dim mtc As New Material.MaterialTypeController
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        M_Currency.Visible = False
        M_Price.Visible = False


        LoadUserPower()
        'Me.Grid.AutoGenerateColumns = False
        '                         mtc.LoadNodes(tv1, ErpUser.MaterialType)
        'tv1.Nodes(2).Expand()
        'tv1.Nodes(1).Expand()
        'tv1.Nodes(0).Expand()
        'mtc.LoadNodes(tv1, "10,20,30")
        'tv1.ExpandAll()
        'Grid.RowHeadersWidth = 15


        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100113")
        If pmwiL.Count > 0 Then
            If pmwiL(0).PMWS_Value.ToString <> "" Then
                mtc.LoadNodes(tv1, pmwiL(0).PMWS_Value.ToString)    '只選擇成品類
            Else
                mtc.LoadNodes(tv1, ErpUser.MaterialType)
            End If
        Else
            mtc.LoadNodes(tv1, ErpUser.MaterialType)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'MsgBox(tv1.SelectedNode.Tag)

    End Sub

    Private Sub tv1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    End Sub

    Private Sub tv1_AfterSelect_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv1.AfterSelect

        If Mid(tv1.SelectedNode.Tag, 1, 5) = "20024" Then

            '***判斷用戶是擁有特殊類的權限
            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

            pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100115")
            If pmwiL.Item(0).PMWS_Value = "否" Then
                ' If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainAdd.Enabled = True
                MsgBox("你沒有特殊類的相關權限")
                tv1.CollapseAll()

                Exit Sub
            End If
        End If
        '************
        If e.Node.Level = 3 Then
            Grid1.DataSource = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, tv1.SelectedNode.Tag, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "是")
            GridSub.Columns.Clear()

        End If


    End Sub

    Private Sub popMaterialMainAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainAdd.Click
        On Error Resume Next
        tempCode = "AddOrEdit"
        Edit = False
        Dim fr As frmBiaMai
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmBiaMai Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmBiaMai
        fr.MdiParent = MDIMain
        fr.WindowState = FormWindowState.Maximized
        fr.cmdAddSub.Visible = False
        fr.cmdCopy.Visible = True
        fr.cmdNew.Visible = True
        fr.Show()


    End Sub

    Private Sub popMaterialMainEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainEdit.Click
        If GridView1.RowCount > 0 Then
            Dim objInfo As New MaterialInfo
            objInfo = mc.MaterialCode_Get(GridView1.GetFocusedRowCellValue("M_Code").ToString)
            If objInfo.M_AccountCheck = True Then
                MsgBox("會計部已審核,無法再修改或刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Sub
            End If

            '暫時不考慮是否有庫存及是否有採購單，如需考慮直接用下面的即可。2010-1-7
            Dim pmc As New PurchaseMainController
            Dim pmiL As New List(Of PurchaseMainInfo)
            pmiL = pmc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If pmiL.Count > 0 Then
                MsgBox("此物料已經存在採購記錄,無法再修改或刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim qmc As New QuotationController
            Dim qmil As New List(Of QuotationInfo)
            qmil = qmc.Quotation_Getlist(Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, True, Nothing)
            If qmil.Count > 0 Then
                MsgBox("此物料已經存在報價記錄,無法刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim mw As New WareInventory.WareInventoryMTController
            Dim mwi As New List(Of WareInventory.WareInventoryInfo)

            mwi = mw.WareInventory_GetMaterial(Nothing, Nothing, "'" & GridView1.GetFocusedRowCellValue("M_Code").ToString & "'")
            If mwi.Count > 0 Then
                MsgBox("此物料已經經過倉庫,無法再修改或刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Sub
            End If

            Dim wii As List(Of WareInput.WareInputInfo)
            Dim wic As New WareInput.WareInputContraller
            wii = wic.WareInput_Getlist(Nothing, Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If wii.Count > 0 Then
                MsgBox("此物料已經進行過入庫，無法再修改或刪除！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Sub
            End If
            Dim woi As List(Of WareOut.WareOutInfo)
            Dim woc As New WareOut.WareOutController
            woi = woc.WareOut_Getlist(Nothing, Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If woi.Count > 0 Then
                MsgBox("此物料已經進行過出庫，無法再修改或刪除！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
                Exit Sub
            End If

            tempCode = "AddOrEdit"
            Edit = True
            On Error Resume Next
            Dim fr As frmMaterialCode
            For Each fr In MDIMain.MdiChildren
                If TypeOf fr Is frmMaterialCode Then
                    fr.Activate()
                    Exit Sub
                End If
            Next
            fr = New frmMaterialCode
            fr.MdiParent = MDIMain
            tempValue5 = GridView1.GetFocusedRowCellValue("M_Code").ToString
            fr.WindowState = FormWindowState.Maximized
            fr.Show()
        End If


    End Sub
    Private Sub Grid1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid1.Click
        If GridView1.RowCount = 0 Then
            Exit Sub
        Else
            ' SetSelectTypeGrid(Grid.CurrentRow.Cells("M_Code").Value.ToString, "產品資料共用", GridSub)
            SetSelectTypeGrid(GridView1.GetFocusedRowCellValue("M_Code").ToString, tcList.SelectedTabPage.Text, GridSub)
        End If
    End Sub
    Private Sub Grid1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If GridView1.RowCount = 0 Then
            Exit Sub
        Else
            ' SetSelectTypeGrid(Grid.CurrentRow.Cells("M_Code").Value.ToString, "產品資料共用", GridSub)
            SetSelectTypeGrid(GridView1.GetFocusedRowCellValue("M_Code").ToString, tcList.SelectedTabPage.Text, GridSub)
        End If
    End Sub

    Private Sub Grid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)



    End Sub

    Private Sub popMaterialMainDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainDel.Click
        Dim objInfo As New MaterialInfo
        objInfo = mc.MaterialCode_Get(GridView1.GetFocusedRowCellValue("M_Code").ToString)
        If objInfo.M_AccountCheck = True Then
            MsgBox("會計部已審核,無法再修改或刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim pmc As New PurchaseMainController
        Dim pmiL As New List(Of PurchaseMainInfo)
        pmiL = pmc.PurchaseMain_Getlist(Nothing, Nothing, Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If pmiL.Count > 0 Then
            MsgBox("此物料已經存在採購記錄,無法刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim qmc As New QuotationController
        Dim qmil As New List(Of QuotationInfo)
        qmil = qmc.Quotation_Getlist(Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If qmil.Count > 0 Then
            MsgBox("此物料已經存在報價記錄,無法刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If


        '暫時不考慮是否有庫存，如需考慮直接用下面的即可。2010-1-7
        Dim mw As New WareInventory.WareInventoryMTController
        Dim mwi As New List(Of WareInventory.WareInventoryInfo)

        mwi = mw.WareInventory_GetMaterial(Nothing, Nothing, "'" & GridView1.GetFocusedRowCellValue("M_Code").ToString & "'")
        If mwi.Count > 0 Then
            MsgBox("此物料已經經過倉庫,無法再修改或刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim wii As List(Of WareInput.WareInputInfo)
        Dim wic As New WareInput.WareInputContraller
        wii = wic.WareInput_Getlist(Nothing, Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If wii.Count > 0 Then
            MsgBox("此物料已經進行過入庫，無法再修改或刪除！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End If
        Dim woi As List(Of WareOut.WareOutInfo)
        Dim woc As New WareOut.WareOutController
        woi = woc.WareOut_Getlist5(Nothing, Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If woi.Count > 0 Then
            MsgBox("此物料已經進行過出庫，無法再修改或刪除！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
        End If


        Dim pc As New LFERP.Library.Product.ProductBomController
        Dim pci As List(Of LFERP.Library.Product.ProductBomInfo)

        pci = pc.ProductBom_GetList(Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing, Nothing, Nothing, Nothing)
        If pci.Count > 0 Then
            MsgBox("此物料已經建立過產品資料信息,無法再修改或刪除!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
            Exit Sub
        End If

        If MsgBox("你確定要刪除 " & GridView1.GetFocusedRowCellValue("M_Code").ToString & " 這個物料嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub

        Dim mi As New MaterialSubInfo
        mi = mc.MaterialCodeSub_Get(Nothing, GridView1.GetFocusedRowCellValue("M_Code").ToString)
        If mi Is Nothing Then
            mc.MaterialCode_Delete(GridView1.GetFocusedRowCellValue("M_Code").ToString) '刪除物料編碼主表
            mc.MaterialPrice_Delete(GridView1.GetFocusedRowCellValue("M_Code").ToString) '刪除物料價格表信息
            Dim mi1 As New MaterialSubInfo
            mi1 = mc.MaterialCodeSub_Get(GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing)
            If mi1 Is Nothing Then
                GoTo A
            Else
                mc.MaterialCodeSub_Delete(GridView1.GetFocusedRowCellValue("M_Code").ToString, Nothing) '刪除物料編碼子表
            End If
A:          Grid1.DataSource = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, tv1.SelectedNode.Tag)
        Else
            MsgBox(GridView1.GetFocusedRowCellValue("M_Code").ToString & "  已作為其他產品的子配件,無法刪除!", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
    End Sub

    Private Sub popMaterialMainRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainRef.Click
        On Error Resume Next
        If tv1.SelectedNode.Level = 3 Then
            Grid1.DataSource = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, tv1.SelectedNode.Tag)
        End If
    End Sub

    Private Sub popMaterialTypeRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialTypeRef.Click
        On Error Resume Next
        '刷新類別
        mtc.LoadNodes(tv1, ErpUser.MaterialType)
        'tv1.Nodes(2).Expand()
        'tv1.Nodes(1).Expand()
        'tv1.Nodes(0).Expand()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub


    Private Sub tcList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcList.Click
        If GridView1.RowCount = 0 Then Exit Sub

        SetSelectTypeGrid(GridView1.GetFocusedRowCellValue("M_Code").ToString, tcList.SelectedTabPage.Text, GridSub)

        If tcList.SelectedTabPage.Text = "報價單" Or tcList.SelectedTabPage.Text = "採購單" Then

            GridSub.ContextMenuStrip = popReport

            popReportOpen.Visible = True
            cmsQualityPreview.Visible = False


        ElseIf tcList.SelectedTabPage.Text = "物料品質反饋" Then

            'GridSub.ContextMenuStrip.Name = "cmsQuality"
            GridSub.ContextMenuStrip = cmsQuality
            cmsQualityPreview.Visible = True
            popReportOpen.Visible = False

        Else
            cmsQualityPreview.Visible = False
            popReportOpen.Visible = False
        End If

    End Sub

    Private Sub tv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tv1.Click

    End Sub

    Private Sub popMaterialMainFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainFile.Click
        '調用此產品資料的文件
        If GridView1.RowCount = 0 Then Exit Sub
        Dim open, update, down, Edit, del, detail As Boolean
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If GridView1.RowCount = 0 Then Exit Sub
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100107")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then update = True
            If pmwiL.Item(0).PMWS_Value = "否" Then update = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100108")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then down = True
            If pmwiL.Item(0).PMWS_Value = "否" Then down = False
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100109")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then Edit = True
            If pmwiL.Item(0).PMWS_Value = "否" Then Edit = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100110")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then del = True
            If pmwiL.Item(0).PMWS_Value = "否" Then del = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100111")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then detail = True
            If pmwiL.Item(0).PMWS_Value = "否" Then detail = False
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100112")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then open = True
            If pmwiL.Item(0).PMWS_Value = "否" Then open = False
        End If

        FileShow("1001", GridView1.GetFocusedRowCellValue("M_Code").ToString, open, update, down, Edit, del, detail)
    End Sub
    Private Sub popMaterialMainSeek_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popMaterialMainSeek.Click
        tempCode = ""

        frmBOMSelect.XtraTabPage1.PageVisible = True
        frmBOMSelect.XtraTabPage2.PageVisible = False
        frmBOMSelect.XtraTabPage3.PageVisible = False
        frmBOMSelect.ShowDialog()
        '增加記錄
        If tempCode = "" Then

            Exit Sub
        Else

            Dim mc As New MaterialController
            Grid1.DataSource = mc.MaterialCode_GetList(tempCode)
        End If

        tempCode = ""
    End Sub

    Private Sub popMaterialMainCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popMaterialMainCheck.Click
        On Error Resume Next
        tempCode = "Check"
        Edit = True

        Dim fr As frmMaterialCode
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMaterialCode Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMaterialCode
        fr.MdiParent = MDIMain
        tempValue5 = GridView1.GetFocusedRowCellValue("M_Code").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()



    End Sub

    Sub LoadUserPower()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100101")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainAdd.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100103")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainDel.Enabled = True
        End If


        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100114")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainCheck.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100102")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainPhoto.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100116")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainExport.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100117")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popMaterialMainAudit.Enabled = True
        End If


        ''單價相關權限
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100118")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then M_Currency.Visible = True : M_Price.Visible = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100119")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then popToolStripPrice.Visible = True
        End If
    End Sub

    Private Sub popMaterialMainView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainView.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        tempCode = "OnlyPreView"
        Dim fr As frmMaterialCode
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMaterialCode Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMaterialCode
        fr.MdiParent = MDIMain
        tempValue5 = GridView1.GetFocusedRowCellValue("M_Code").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub GridSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridSub.Click
        If GridSub.RowCount = 0 Then Exit Sub
        PowerReport()

    End Sub

    '針對報價單以及採購單載入附檔
    Private Sub GridSub_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridSub.DoubleClick

        If tcList.SelectedTabPage.Text = "報價單" Then

            Dim pc As New Purchase.Quotation.QuotationController
            Dim pi As New Purchase.Quotation.QuotationInfo

            pi = pc.Quotation_Get(GridSub.CurrentRow.Cells("Q_QuoID").Value.ToString)
            Dim open As Boolean
            Dim update As Boolean
            Dim down As Boolean
            Dim edit As Boolean
            Dim del As Boolean
            Dim detail As Boolean

            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

            If GridSub.RowCount = 0 Then Exit Sub

            If pi.Q_Type = "物料" Then

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010410")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010411")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010412")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010413")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010414")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010415")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False

                End If
            ElseIf pi.Q_Type = "配件批次" Then

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010310")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010311")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010312")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010313")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010314")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010315")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False

                End If
            ElseIf pi.Q_Type = "大貨批次" Then

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010110")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010111")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010112")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010113")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010114")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010115")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False

                End If
            ElseIf pi.Q_Type = "樣辦" Then

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010210")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010211")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010212")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010213")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010214")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010215")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False

                End If
            End If

            FileShow("4001", GridSub.CurrentRow.Cells("Q_QuoID").Value.ToString, open, update, down, edit, del, detail)

        ElseIf tcList.SelectedTabPage.Text = "採購單" Then

            Dim pc As New Purchase.Purchase.PurchaseMainController
            Dim pi As New Purchase.Purchase.PurchaseMainInfo

            pi = pc.PurchaseMain_Get(GridSub.CurrentRow.Cells("PM_NO").Value.ToString)

            Dim open As Boolean
            Dim update As Boolean
            Dim down As Boolean
            Dim edit As Boolean
            Dim del As Boolean
            Dim detail As Boolean
            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)
            If GridSub.RowCount = 0 Then Exit Sub

            If pi.PM_PurchaseType = "物料" Then

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020410")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020411")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020412")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020413")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020414")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020415")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False
                End If
            ElseIf pi.PM_PurchaseType = "配件批次" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020310")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020311")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020312")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020313")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020314")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020315")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False
                End If
            ElseIf pi.PM_PurchaseType = "大貨批次" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020110")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020111")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020112")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020113")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020114")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020115")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False
                End If
            ElseIf pi.PM_PurchaseType = "樣辦" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020210")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then update = True Else update = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020211")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then down = True Else down = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020212")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then edit = True Else edit = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020213")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then del = True Else del = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020214")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then detail = True Else detail = False
                End If

                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020215")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then open = True Else open = False
                End If
            End If

            FileShow("4002", GridSub.CurrentRow.Cells("PM_NO").Value.ToString, open, update, down, edit, del, detail)

        End If

    End Sub
    '載入子模塊的權限
    Sub PowerReport()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As New List(Of PermissionModuleWarrantSubInfo)

        If tcList.SelectedTabPage.Text = "報價單" Then

            Dim pc As New Purchase.Quotation.QuotationController 
            Dim pi As New Purchase.Quotation.QuotationInfo

            pi = pc.Quotation_Get(GridSub.CurrentRow.Cells("Q_QuoID").Value.ToString)
            If pi.Q_Type = "物料" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010408")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If

            ElseIf pi.Q_Type = "配件批次" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010308")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If

            ElseIf pi.Q_Type = "樣辦" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010208")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If

            ElseIf pi.Q_Type = "大貨批次" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40010108")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If
            End If
        ElseIf tcList.SelectedTabPage.Text = "採購單" Then

            Dim pc As New Purchase.Purchase.PurchaseMainController
            Dim pi As New Purchase.Purchase.PurchaseMainInfo

            pi = pc.PurchaseMain_Get(GridSub.CurrentRow.Cells("PM_NO").Value.ToString)

            If pi.PM_PurchaseType = "物料" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020407")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If
            ElseIf pi.PM_PurchaseType = "配件批次" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020307")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If
            ElseIf pi.PM_PurchaseType = "樣辦" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020207")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If
            ElseIf pi.PM_PurchaseType = "大貨批次" Then
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "40020107")
                If pmwiL.Count > 0 Then
                    If pmwiL.Item(0).PMWS_Value = "是" Then popReportOpen.Enabled = True Else popReportOpen.Enabled = False
                End If
            End If
        End If
    End Sub
    '在物料表中直接導入某批次某物料的報價或採購報表
    Private Sub popReportOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popReportOpen.Click

        If tcList.SelectedTabPage.Text = "報價單" Then
            Dim pc As New Purchase.Quotation.QuotationController
            Dim pi As New Purchase.Quotation.QuotationInfo

            pi = pc.Quotation_Get(GridSub.CurrentRow.Cells("Q_QuoID").Value.ToString)

            Dim ds As New DataSet
            If GridSub.RowCount = 0 Then Exit Sub

            Dim ltc As New CollectionToDataSet
            Dim ltc1 As New CollectionToDataSet
            Dim ltc2 As New CollectionToDataSet
            Dim ltc3 As New CollectionToDataSet
            Dim ltc4 As New CollectionToDataSet
            Dim mcCompany As New LFERP.DataSetting.CompanyControler
            Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
            Dim mcQuotation As New LFERP.Library.Purchase.Quotation.QuotationController
            Dim mcUnit As New LFERP.DataSetting.UnitController
            Dim mctarriff As New LFERP.DataSetting.TarriffController
            ds.Tables.Clear()
            ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, pi.CO_ID, Nothing, Nothing))
            ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(pi.Q_Supplier, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc2.CollToDataSet(ds, "Quotation", mcQuotation.Quotation_Getlist(pi.Q_QuoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "Unit", mcUnit.GetUnitList(Nothing))
            ltc4.CollToDataSet(ds, "Tarriff", mctarriff.TarriffGetList(Nothing))
    
            PreviewRPT(ds, "rptQuotation", " 報價單--" & pi.Q_QuoID, True, True)
            ltc = Nothing
            ltc1 = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
        ElseIf tcList.SelectedTabPage.Text = "採購單" Then

            Dim pc As New Purchase.Purchase.PurchaseMainController
            Dim pi As New Purchase.Purchase.PurchaseMainInfo

            pi = pc.PurchaseMain_Get(GridSub.CurrentRow.Cells("PM_NO").Value.ToString)
            Dim ds As New DataSet
            If GridSub.RowCount = 0 Then Exit Sub

            Dim ltc As New CollectionToDataSet
            Dim ltc1 As New CollectionToDataSet
            Dim ltc2 As New CollectionToDataSet
            Dim ltc3 As New CollectionToDataSet
            Dim ltc4 As New CollectionToDataSet
            Dim ltc5 As New CollectionToDataSet
            Dim mcCompany As New LFERP.DataSetting.CompanyControler
            Dim mcSupplier As New LFERP.DataSetting.SuppliersControler
            Dim mcPurchase As New LFERP.Library.Purchase.Purchase.PurchaseMainController
            Dim mcsysuser As New LFERP.SystemManager.SystemUser.SystemUserController
            Dim mctarriff As New LFERP.DataSetting.TarriffController
            Dim mcunit As New LFERP.DataSetting.UnitController

            ds.Tables.Clear()
            ltc.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(Nothing, pi.CO_ID, Nothing, Nothing))
            ltc1.CollToDataSet(ds, "Suppliers", mcSupplier.GetSuppliersList(pi.S_Supplier, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc2.CollToDataSet(ds, "PurchaseMain", mcPurchase.PurchaseMain_Getlist(pi.PM_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            ltc3.CollToDataSet(ds, "SystemUser", mcsysuser.SystemUser_GetList(Nothing, Nothing, Nothing))
            ltc4.CollToDataSet(ds, "Tarriff", mctarriff.TarriffGetList(Nothing))
            ltc5.CollToDataSet(ds, "Unit", mcunit.GetUnitList(Nothing))
          
            PreviewRPT(ds, "rptPurchaseChs", " 採購單--" & pi.PM_NO, True, True)
            ltc = Nothing
            ltc1 = Nothing
            ltc2 = Nothing
            ltc3 = Nothing
            ltc4 = Nothing
        End If

    End Sub

    Private Sub popMaterialMainPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainPhoto.Click
        On Error Resume Next
        If GridView1.RowCount = 0 Then Exit Sub
        tempCode = "Photo"
        Dim fr As frmMaterialCode
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMaterialCode Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMaterialCode
        fr.MdiParent = MDIMain
        tempValue5 = GridView1.GetFocusedRowCellValue("M_Code").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub popMaterialMainExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainExport.Click
        'On Error Resume Next
        'If GridView1.RowCount = 0 Then Exit Sub
        'If MsgBox("你確定要當前所有的物料嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub

        'Dim exapp As New Excel.Application
        'Dim exbook As Excel.Workbook
        'Dim exsheet As Excel.Worksheet

        'Dim i As Integer = 0, ii As Integer = 0

        'exapp = CreateObject("Excel.Application")
        'exbook = exapp.Workbooks.Add
        'exsheet = exapp.Worksheets(1)

        'exsheet.Cells(1, 1) = "物料編碼"
        'exsheet.Cells(1, 2) = "名稱"
        'exsheet.Cells(1, 3) = "單位"
        'exsheet.Cells(1, 4) = "規格"
        'exsheet.Cells(1, 5) = "啟用"
        'exsheet.Cells(1, 6) = "會計部審核"
        'exsheet.Cells(1, 7) = "建立日期"
        'exsheet.Cells(1, 8) = "修改日期"
        'exsheet.Cells(1, 9) = "操作員ID"
        'exsheet.Cells(1, 10) = "操作員"

        'For i = 0 To GridView1.RowCount - 1
        '    ii = i + 2
        '    exsheet.Cells(ii, 1) = "#" & GridView1.GetRowCellValue(i, "M_Code") & "#"
        '    exsheet.Cells(ii, 2) = GridView1.GetRowCellValue(i, "M_Name")
        '    exsheet.Cells(ii, 3) = GridView1.GetRowCellValue(i, "M_Unit")
        '    exsheet.Cells(ii, 4) = GridView1.GetRowCellValue(i, "M_Gauge")
        '    exsheet.Cells(ii, 5) = GridView1.GetRowCellValue(i, "M_IsEnabled")
        '    exsheet.Cells(ii, 6) = GridView1.GetRowCellValue(i, "M_AccountCheck")
        '    exsheet.Cells(ii, 7) = GridView1.GetRowCellValue(i, "M_AddDate")
        '    exsheet.Cells(ii, 8) = GridView1.GetRowCellValue(i, "M_EditDate")
        '    exsheet.Cells(ii, 9) = GridView1.GetRowCellValue(i, "InUser")
        '    exsheet.Cells(ii, 10) = GridView1.GetRowCellValue(i, "U_Name")

        'Next
        'exapp.Visible = True

        Dim saveFileDialog As New SaveFileDialog()

        saveFileDialog.Title = "導出Excel"

        saveFileDialog.Filter = "Excel文件(*.xls)|*.xls"

        Dim dialogResult__1 As DialogResult = saveFileDialog.ShowDialog(Me)

        If dialogResult__1 = Windows.Forms.DialogResult.OK Then

            Grid1.ExportToXls(saveFileDialog.FileName)

            DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    '匯總呆滯物料
    Private Sub popMaterialMainNouse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainNouse.Click
        Dim ds As New DataSet

        Dim ltc As New CollectionToDataSet
     
        Dim mcc As New MaterialController

        ds.Tables.Clear()
       
        ltc.CollToDataSet(ds, "MaterialCode", mcc.MaterialCode_NoUseGetList(Nothing))
     
        PreviewRPT(ds, "rptMaterialCode", " 呆滯物料", True, True)

        ltc = Nothing
    
    End Sub
    '審計部修改--變更物料名稱,規格,備註信息.子物料信息等
    Private Sub popMaterialMainAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popMaterialMainAudit.Click
        On Error Resume Next
        tempCode = "AuditEdit"

        Dim fr As frmMaterialCode
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmMaterialCode Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmMaterialCode
        fr.MdiParent = MDIMain
        tempValue5 = GridView1.GetFocusedRowCellValue("M_Code").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmsQualityPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsQualityPreview.Click

        If GridSub.RowCount = 0 Then Exit Sub

        Dim frmWQView As New frmWareQualityAdd
        frmWQView.MdiParent = MDIMain
        frmWQView.WindowState = FormWindowState.Maximized
        frmWQView.Text = "物料品質反饋單-查看"
        frmWQView.lblTitle.Text = "物料品質反饋單-查看"
        frmWQView.txtWQ_Code.Text = GridSub.CurrentRow.Cells("WQ_Code").Value.ToString '把選中行的反饋單編號顯示在查看模塊的反饋單編號文本框中
        frmWQView.Show()
    End Sub

    Private Sub popMaterialMainPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles popMaterialMainPrint.Click

    End Sub

    Private Sub popToolStripPrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popToolStripPrice.Click
        If GridView1.RowCount <= 0 Then
            Exit Sub
        End If

        tempValue = GridView1.GetFocusedRowCellValue("M_Code").ToString
        frmMaterialPrice.ShowDialog()
        frmMaterialPrice.Dispose()

    End Sub
End Class
