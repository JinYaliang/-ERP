Imports LFERP.Library.KnifeWare
Imports LFERP.Library.Material
Imports LFERP.SystemManager
Imports LFERP.Library

Public Class frmBrrowList


#Region "屬性"
    Dim meleft As Integer

    Private _ReportTypeID As String
    Private _ReportTypeName As String
    Private _ReportPerID As String
    Private _ReportWHID As String
    Private _ReportWHName As String

    Public Property ReportWHName() As String ''倉庫編號
        Get
            Return _ReportWHName
        End Get
        Set(ByVal value As String)
            _ReportWHName = value
        End Set
    End Property

    Public Property ReportWHID() As String ''倉庫編號
        Get
            Return _ReportWHID
        End Get
        Set(ByVal value As String)
            _ReportWHID = value
        End Set
    End Property

    Public Property ReportPerID() As String ''員工工號
        Get
            Return _ReportPerID
        End Get
        Set(ByVal value As String)
            _ReportPerID = value
        End Set
    End Property

    Public Property ReportTypeID() As String
        Get
            Return _ReportTypeID
        End Get
        Set(ByVal value As String)
            _ReportTypeID = value
        End Set
    End Property

    Public Property ReportTypeName() As String
        Get
            Return _ReportTypeName
        End Get
        Set(ByVal value As String)
            _ReportTypeName = value
        End Set
    End Property
#End Region

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Select Case ReportTypeID
            Case "UserKnifeList" ''用刀清單
                If dgEditWorkNumberA.Text = Nothing Then
                    dgEditWorkNumberA.Select()
                    MsgBox("請選擇工號", 64, "提示")
                    Exit Sub
                End If

                Dim strWHID As String
                If ButtonEditA.Tag = Nothing Then
                    strWHID = Nothing
                Else
                    strWHID = ButtonEditA.Tag
                End If

                '-------------------------------------------------------------------------------
                Dim knl As New List(Of KnifeWhiteUserInfo)
                Dim knc As New KnifeWhiteUserController
                knl = knc.WhiteUser_GetListAll(Nothing, dgEditWorkNumberA.EditValue, strWHID, "刀具倉", Nothing, False)

                '-------------------------------------------------------------------------------
                Dim kc As New Library.KnifeWare.KnifeBorrowControl
                Dim cw As New List(Of Library.KnifeWare.KnifeBorrowInfo)

                Dim ltc As New CollectionToDataSet
                Dim ltc1 As New CollectionToDataSet
                Dim ds As New DataSet
                cw = kc.KnifeBorrow_GetList(Nothing, Nothing, Nothing, Nothing, dgEditWorkNumberA.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, "Zero", Nothing)
                If cw.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If
                '-------------------------------------------------------------------------------
                ltc1.CollToDataSet(ds, "WhiteUser", knl)
                ltc.CollToDataSet(ds, "KnifeBorrow", cw)
                PreviewRPTShow(ds, "rptUseBrrowKnifeReport", "刀具清單", True, True)
                ltc = Nothing
                ltc1 = Nothing
            Case "DullKnife" ''呆滯物料
                Dim ds As New DataSet
                Dim DullC As New KnifeWareInventorySubControl
                Dim DullL As New List(Of KnifeWareInventorySubInfo)
                DullL = DullC.KnifeWareInventoryDull_GetList(Format(CDate(DateEditDullS.EditValue), "yyyy/MM/dd"), Format(CDate(DateEditDullE.EditValue), "yyyy/MM/dd"))
                If DullL.Count <= 0 Then
                    MsgBox("無數據存在！")
                    Exit Sub
                End If
                Dim ltc As New CollectionToDataSet

                ltc.CollToDataSet(ds, "vwKnifeMaterial", DullL)
                PreviewRPTShow(ds, "rptKnifeWareInventoryDull", "呆滯刀具信息", True, True)
                ltc = Nothing
            Case "KnifeBrrowByGNO" ''員工欠刀情況精簡表
                Dim kc As New Library.KnifeWare.KnifeBorrowControl
                Dim cw As New List(Of Library.KnifeWare.KnifeBorrowInfo)

                Dim ltc As New CollectionToDataSet
                Dim ltc1 As New CollectionToDataSet
                Dim ds As New DataSet
                ds.Clear()

                '2014-3-24  姚駿
                Dim strTempWHID As String
                strTempWHID = ReportWHID
                '----------------------------------------------------------------------------
                If chkWHID.Checked Then
                    strTempWHID = Nothing
                End If
                '----------------------------------------------------------------------------


                If rbnWorkNumber.Checked = True Then '按工號
                    If txtWorkNumberD.Text = Nothing Then
                        txtWorkNumberD.Select()
                        MsgBox("請選擇工號", 64, "提示")
                        Exit Sub
                    End If
                    cw = kc.KnifeBorrow_GetListByGNO(Nothing, Nothing, Nothing, strTempWHID, txtWorkNumberD.Text, Nothing, Nothing, Nothing, Nothing, Nothing, "Zero", Nothing)
                    '-------------------------------------------------------------------------------
                ElseIf rbnGroup.Checked = True Then '按組別
                    If (dgGroupD.Text = "") Then
                        txtWorkNumberD.Select()
                        MsgBox("請選擇组號", 64, "提示")
                        Exit Sub
                    End If
                    cw = kc.KnifeBorrow_GetListByGNO(Nothing, Nothing, Nothing, strTempWHID, Nothing, Nothing, Nothing, dgGroupD.EditValue, Nothing, Nothing, "Zero", Nothing)
                    '-------------------------------------------------------------------------------
                Else
                    '全部
                    cw = kc.KnifeBorrow_GetListByGNO(Nothing, Nothing, Nothing, strTempWHID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "Zero", Nothing)
                End If

                If cw.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If

                'ltc.CollToDataSet(ds, "KnifeBorrow", cw)
                'PreviewRPTShow(ds, "rptBrrowReportCombo", "員工欠刀情況-精簡表", True, True)
                'ltc = Nothing

                '2014-3-24  姚駿
                '----------------------------------------------------------------------------
                ltc.CollToDataSet(ds, "KnifeBorrow", cw)
                If chkWHID.Checked Then
                    PreviewRPTDialog(ds, "rptWHBrrowReportCombo", "員工欠刀情況-所有倉庫精簡表", True, True)
                Else
                    PreviewRPTDialog(ds, "rptBrrowReportCombo", "員工欠刀情況-精簡表", True, True)
                End If
                ltc = Nothing
                '----------------------------------------------------------------------------

            Case "KnifeBorrow"              '領刀記錄
             
                Dim kc As New Library.KnifeWare.KnifeBorrowControl
                Dim cw As New List(Of Library.KnifeWare.KnifeBorrowInfo)
                Dim ds As New DataSet
                Dim ltc As New CollectionToDataSet

                Dim themonthStart As Date = dtEditStartB.Text
                Dim themonthEnd As Date = dtEditEndB.Text

                Dim strWareHouse As String = Nothing
                Dim strM_Code As String = Nothing
                Dim strPerNOBroow As String = Nothing

                If txtPerNOBroow.Text <> "" Then
                    strPerNOBroow = txtPerNOBroow.Text
                End If

                If btnWareHouseB.Text <> "全部" Then
                    strWareHouse = btnWareHouseB.Tag
                End If

                If txtM_CodeB.Text <> "" Then
                    strM_Code = txtM_CodeB.Text
                End If
                cw = kc.KnifeBorrow_GetList(Nothing, Nothing, strM_Code, strWareHouse, strPerNOBroow, dtEditStartB.Text, dtEditEndB.Text, Nothing, Nothing, Nothing, Nothing, Nothing)
                If cw.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If
                ltc.CollToDataSet(ds, "KnifeBorrow", cw)
                PreviewRPTShow1(ds, "12rptKnifeBorrowAll", "刀具領取記錄", dtEditStartB.Text, dtEditEndB.Text, True, True)
                ltc = Nothing


            Case "KnifeReturn"

                Dim themonthStart As Date = dtEditStartC.Text
                Dim themonthEnd As Date = dtEditEndC.Text

                Dim wbc As New KnifeReturnControl
                Dim objInfo As New List(Of KnifeReturnInfo)
                Dim ltc As New CollectionToDataSet
                Dim dsReturnKnife As New DataSet


                Dim strWareHouse As String = Nothing
                Dim strM_Code As String = Nothing
                Dim strPerNOReturn As String = Nothing

                If txtPerNOReturn.Text <> "" Then
                    strPerNOReturn = txtPerNOReturn.Text
                End If

                If btnWareHouseC.Text <> "全部" Then
                    strWareHouse = btnWareHouseC.Tag
                End If

                If txtM_CodeC.Text <> "" Then
                    strM_Code = txtM_CodeC.Text
                End If

                objInfo = wbc.KnifeReturn_GetList(Nothing, Nothing, strM_Code, strWareHouse, strPerNOReturn, dtEditStartC.Text, dtEditEndC.Text, Nothing, Nothing, Nothing)
                If objInfo.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If
                ltc.CollToDataSet(dsReturnKnife, "KnifeReturn", objInfo)
                PreviewRPTShow1(dsReturnKnife, "13rptKnifeReturnAll", "刀具歸還記錄", dtEditStartC.Text, dtEditEndC.Text, True, True)
                ltc = Nothing

            Case "KnifeInventory"  '刀具庫存
     
                Dim wbc As New KnifeWareInventorySubControl
                Dim objInfo As New List(Of KnifeWareInventorySubInfo)
                Dim ltc As New CollectionToDataSet
                Dim dsInventory As New DataSet
                Dim strKnifeInventory As String = Nothing
                Dim strpopTypeID As String = Nothing

                strpopTypeID = popTypeID.Tag

                strKnifeInventory = btnKnifeInventoryE.Tag

                If Len(strpopTypeID) = 11 Then
                    objInfo = wbc.KnifeWareInventory_GetListType123(Nothing, Nothing, strpopTypeID, strKnifeInventory, Nothing, Nothing, Nothing)
                ElseIf Len(strpopTypeID) = 8 Then
                    objInfo = wbc.KnifeWareInventory_GetListType123(Nothing, strpopTypeID, Nothing, strKnifeInventory, Nothing, Nothing, Nothing)
                Else
                    objInfo = wbc.KnifeWareInventory_GetListType123(Nothing, Nothing, strpopTypeID, strKnifeInventory, Nothing, Nothing, Nothing)
                End If


                If objInfo.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If

                ltc.CollToDataSet(dsInventory, "KnifeInventory", objInfo)
                If CheckEditPD.Checked = True Then
                    PreviewRPTShow(dsInventory, "01rptKnifeInventoryPD", "刀具倉庫庫存(盤點表)", True, True)
                Else
                    PreviewRPTShow(dsInventory, "01rptKnifeInventory", "刀具倉庫庫存", True, True)
                End If

                ltc = Nothing

            Case "KnifeWareMove"

                If btnIssueWareHourse.Tag = Nothing Then
                    MsgBox("請選擇發出倉庫！")
                    btnIssueWareHourse.Select()
                    Exit Sub
                End If


                Dim StrM_Code As String = Nothing
                Dim StrInWare As String = Nothing
                Dim StrOutWare As String = Nothing
                Dim StrStartDate As String = Nothing
                Dim StrEndDate As String = Nothing
                Dim Strprint As String = ""

                If txt_MrpCode.Text <> "" Then
                    StrM_Code = txt_MrpCode.Text
                End If

                If btnIssueWareHourse.Text <> "" Then
                    StrOutWare = btnIssueWareHourse.Tag
                End If

                If btnRevWareHouse.Text <> "" Then
                    StrInWare = btnRevWareHouse.Tag
                End If

                If dtIssueStart.Text <> "" Then
                    StrStartDate = Format(CDate(dtIssueStart.EditValue), "yyyy/MM/dd") & " 00:00:00"
                End If

                If dtIssueEnd.Text <> "" Then
                    StrEndDate = Format(CDate(dtIssueEnd.EditValue), "yyyy/MM/dd") & " 23:59:59"
                End If
                If StrStartDate <> Nothing And StrEndDate <> Nothing Then
                    Strprint = Format(CDate(dtIssueStart.EditValue), "yyyy/MM/dd") & "至" & Format(CDate(dtIssueEnd.EditValue), "yyyy/MM/dd")
                End If

                '--------------------

                Dim wmc As New KnifeWareMoveController
                Dim wmi As New List(Of KnifeWareMoveInfo)
                Dim ltc As New CollectionToDataSet
                Dim dsKnifeWareMove As New DataSet

                wmi = wmc.KnifeWareMove_GetList1(Nothing, StrM_Code, Nothing, StrOutWare, StrInWare, Nothing, Nothing, "True", "2", StrStartDate, StrEndDate)

                If wmi.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If

                ltc.CollToDataSet(dsKnifeWareMove, "KnifeWareMove", wmi)
                PreviewRPTShow1(dsKnifeWareMove, "rpt2KnifeWareMove", "刀具調撥", Strprint, "", True, True)
                ltc = Nothing

            Case "KnifeWareHouseSearch"
                Dim wmc As New KnifeWareInventorySubControl
                Dim wmi As New List(Of KnifeWareInventorySubInfo)
                Dim ltc As New CollectionToDataSet
                Dim dsKnifeWareHouseSearch As New DataSet

                Dim strType As String = Nothing
                Dim strMCode As String = Nothing
                Dim strArrtibute As String


                If cbTypeF.Text <> "" Then
                    If Trim(Mid(cbTypeF.Text, 1, 3)) = "All" Then
                        strType = Nothing
                    Else
                        strType = Trim(Mid(cbTypeF.Text, 1, 3))
                    End If

                End If

                'If (cbTypeF.Text = "Out—出") Then
                '    strType = "Out"
                'ElseIf cbTypeF.Text = "In—入" Then
                '    strType = "In"
                'Else
                '    strType = Nothing
                'End If

                If txtM_CodeF.Text = "" Then
                    strMCode = Nothing
                Else
                    strMCode = txtM_CodeF.Text
                End If

                If cboArrtibute.Text = "" Or cboArrtibute.Text = "全部" Then
                    strArrtibute = Nothing
                Else
                    strArrtibute = cboArrtibute.Text
                End If

                Dim strTimeStart As String = Format(CDate(dtStartF.EditValue), "yyyy/MM/dd") & " 00:00:00"
                Dim strTimeEnd As String = Format(CDate(dtEndF.EditValue), "yyyy/MM/dd") & " 23:59:59"
                wmi = wmc.KnifeWareInventorySearch_GetList(btnWareHouseF.Tag, strType, strArrtibute, strMCode, strTimeStart, strTimeEnd)

                If wmi.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If

                ltc.CollToDataSet(dsKnifeWareHouseSearch, "KnifeWareHouseSearch", wmi)
                PreviewRPTShow(dsKnifeWareHouseSearch, "rptKnifeWareHouseSearch", "刀具收發", True, True)
                ltc = Nothing
            Case "KnifeInventoryBackUp"

                'If (btnTypeG.Text = "") Then
                '    MsgBox("請選擇類別", 64, "提示")
                '    Exit Sub
                'End If
                Dim wbc As New KnifeWareInventorySubControl
                Dim objInfo As New List(Of KnifeWareInventorySubInfo)
                Dim ltc As New CollectionToDataSet
                Dim dsInventoryBackUp As New DataSet
                Dim strKnifeInventoryBackUp As String = Nothing
                Dim strpopTypeID As String = Nothing


                Dim strWHID As String = Nothing
                Dim strTimeStart As String = dtYearMothly.DateTime.AddMonths(1).AddDays(1 - dtYearMothly.DateTime.Day).ToString("yyyy-MM") + "-01 00:00:00"
                Dim strTimeEnd As String = dtYearMothly.DateTime.AddMonths(1).AddDays(1 - dtYearMothly.DateTime.Day).ToString("yyyy-MM") + "-01 23:59:59"

                strpopTypeID = btnTypeG.Tag

                If btnWareHourseG.Text <> "全部" Then
                    strWHID = btnWareHourseG.Tag
                End If


                If Len(strpopTypeID) = 11 Then
                    objInfo = wbc.WareInventoryBackUp_GetListType123(strWHID, Nothing, Nothing, strpopTypeID, strTimeStart, strTimeEnd)
                ElseIf Len(strpopTypeID) = 8 Then
                    objInfo = wbc.WareInventoryBackUp_GetListType123(strWHID, Nothing, strpopTypeID, Nothing, strTimeStart, strTimeEnd)
                ElseIf Len(strpopTypeID) = 5 Then
                    objInfo = wbc.WareInventoryBackUp_GetListType123(strWHID, strpopTypeID, Nothing, Nothing, strTimeStart, strTimeEnd)
                Else
                    objInfo = wbc.WareInventoryBackUp_GetListType123(strWHID, Nothing, Nothing, Nothing, strTimeStart, strTimeEnd)
                End If


                If objInfo.Count <= 0 Then
                    MsgBox("查詢數據為空", 64, "提示")
                    Exit Sub
                End If

                ltc.CollToDataSet(dsInventoryBackUp, "KnifeWareInventory", objInfo)
                PreviewRPTShow(dsInventoryBackUp, "rptKnifeWareInventoryBackUp", "庫存備份", True, True)
                ltc = Nothing

        End Select
    End Sub

    Private Sub canButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles canButton.Click
        Me.Close()
    End Sub

    Private Sub frmBrrowList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        XtraTabPage1.PageVisible = False
        XtraTabPage2.PageVisible = False
        XtraTabPage3.PageVisible = False
        XtraTabPage4.PageVisible = False
        XtraTabPage5.PageVisible = False
        XtraTabPage6.PageVisible = False
        XtraTabPage7.PageVisible = False
        XtraTabPage8.PageVisible = False
        XtraTabPage9.PageVisible = False


        Me.Width = 300
        meleft = Me.Left
        Me.Left = meleft + 150

        Select Case ReportTypeID
            Case "UserKnifeList" ''用刀清單
                XtraTabPage1.PageVisible = True
                tab.SelectedTabPage = Me.XtraTabPage1
                XtraTabPage1.Text = ReportTypeName
                ButtonEditA.Tag = Nothing
                ButtonEditA.Text = "全部"


                Dim kwu As New Library.KnifeWare.KnifeWhiteUserController
                dgEditWorkNumberA.Properties.DataSource = kwu.WhiteUserSeting_GetListAll(Nothing, Nothing, Nothing, "刀具倉", Nothing, Nothing, Nothing)
                dgEditWorkNumberA.Properties.DisplayMember = "Per_Name"
                dgEditWorkNumberA.Properties.ValueMember = "Per_ID"

                dgEditWorkNumberA.EditValue = Me.ReportPerID

            Case "DullKnife" ''呆滯刀具

                XtraTabPage2.PageVisible = True
                XtraTabPage2.Text = ReportTypeName
                DateEditDullS.EditValue = Format(Now, "yyyy/MM") & "/01"
                DateEditDullE.EditValue = Format(Now, "yyyy/MM/dd")

            Case "KnifeBrrowByGNO" ''員工欠刀情況精簡表
                XtraTabPage3.PageVisible = True
                XtraTabPage3.Text = ReportTypeName
                txtWorkNumberD.Text = ReportPerID

                Dim gc As New KnifeGroupControl
                Dim gl As New List(Of KnifeGroupInfo)
                dgGroupD.Properties.DataSource = gc.KnifeGroup_GetList(Nothing, Nothing, ReportWHID)
                dgGroupD.Properties.DisplayMember = "G_Name"
                dgGroupD.Properties.ValueMember = "G_NO"

            Case "KnifeBorrow"              '領刀記錄
                XtraTabPage4.PageVisible = True
                XtraTabPage4.Text = ReportTypeName
                btnWareHouseB.Tag = ReportWHID
                btnWareHouseB.Text = ReportWHName
                InitTime(dtEditStartB, dtEditEndB)

                Dim mc As New MaterialController
                Me.txtM_CodeB.Properties.DataSource = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, "60", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Me.txtM_CodeB.Properties.ValueMember = "M_Code"
                Me.txtM_CodeB.Properties.DisplayMember = "M_Code"

            Case "KnifeReturn"              '還刀記錄
                XtraTabPage5.PageVisible = True
                XtraTabPage5.Text = ReportTypeName
                btnWareHouseC.Tag = ReportWHID
                btnWareHouseC.Text = ReportWHName
                InitTime(dtEditStartC, dtEditEndC)


                Dim mc As New MaterialController
                Me.txtM_CodeC.Properties.DataSource = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, "60", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Me.txtM_CodeC.Properties.ValueMember = "M_Code"
                Me.txtM_CodeC.Properties.DisplayMember = "M_Code"

            Case "KnifeInventory"
                XtraTabPage6.PageVisible = True
                XtraTabPage6.Text = ReportTypeName
                btnKnifeInventoryE.Tag = Nothing
                btnKnifeInventoryE.Text = "全部"

                popTypeID.Tag = Nothing
                popTypeID.Text = "全部"

                popTypeID.Properties.PopupControl = PopupContainerControl1

                Dim mtc As New Material.MaterialTypeController

                '' 為磨刀部
                Dim pmws As New PermissionModuleWarrantSubController
                Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100113")
                If pmwiL.Count > 0 Then
                    If pmwiL(0).PMWS_Value.ToString <> "" Then
                        mtc.LoadNodes(tv1, pmwiL(0).PMWS_Value.ToString)    '只選擇成品類
                    Else
                        mtc.LoadNodes(tv1, "60")    '只選擇成品類
                    End If
                Else
                    mtc.LoadNodes(tv1, "60")    '只選擇成品類
                End If

             

            Case "KnifeWareMove"           '刀具撥發
                XtraTabPage7.PageVisible = True
                XtraTabPage7.Text = ReportTypeName
                InitTime(dtIssueStart, dtIssueEnd)
                ''載入刀具編碼
                Dim mc As New MaterialController
                Me.txt_MrpCode.Properties.DataSource = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, "60", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Me.txt_MrpCode.Properties.ValueMember = "M_Code"
                Me.txt_MrpCode.Properties.DisplayMember = "M_Code"

                txt_MrpCode.Select()

            Case "KnifeWareHouseSearch"
                XtraTabPage8.PageVisible = True
                XtraTabPage8.Text = ReportTypeName
                btnWareHouseF.Tag = ReportWHID
                btnWareHouseF.Text = ReportWHName

                dtStartF.Text = Format(Now, "yyyy/MM") & "/01"
                dtEndF.Text = Format(Now, "yyyy/MM/dd")
                Dim mc As New MaterialController
                Me.txtM_CodeF.Properties.DataSource = mc.MaterialCode_GetList(Nothing, Nothing, Nothing, "60", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                Me.txtM_CodeF.Properties.ValueMember = "M_Code"
                Me.txtM_CodeF.Properties.DisplayMember = "M_Code"
                btnWareHouseF.Select()
            Case "KnifeInventoryBackUp"
                XtraTabPage9.PageVisible = True
                XtraTabPage9.Text = ReportTypeName

                btnWareHourseG.Tag = ReportWHID
                btnWareHourseG.Text = ReportWHName

                btnTypeG.Properties.PopupControl = PopupContainerControl1

                dtYearMothly.Text = Format(DateTime.Now, "yyyy年MM月")

                Dim mtc As New Material.MaterialTypeController

                '' 為磨刀部
                Dim pmws As New PermissionModuleWarrantSubController
                Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
                pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "100113")
                If pmwiL.Count > 0 Then
                    If pmwiL(0).PMWS_Value.ToString <> "" Then
                        mtc.LoadNodes(tv1, pmwiL(0).PMWS_Value.ToString)    '只選擇成品類
                    Else
                        mtc.LoadNodes(tv1, "60")    '只選擇成品類
                    End If
                Else
                    mtc.LoadNodes(tv1, "60")    '只選擇成品類
                End If


        End Select
    End Sub

#Region "員工還刀記錄"
    Private Sub btnWareHouseC_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnWareHouseC.ButtonClick
        GetIdAndName(btnWareHouseC, "510603")
    End Sub


#End Region

#Region "員工領刀記錄"

    Private Sub btnWareHouseB_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnWareHouseB.ButtonClick
        GetIdAndName(btnWareHouseB, "510503")
    End Sub

#End Region


#Region "初始化時間"
    Private Sub InitTime(ByRef dtEditStart As DevExpress.XtraEditors.DateEdit, ByRef dtEditEnd As DevExpress.XtraEditors.DateEdit)
        dtEditStart.Text = Format(DateTime.Now, "yyyy/MM") & "/01"
        ' Dim dt As DateTime = DateTime.Now.AddDays(30)
        dtEditEnd.Text = Format(DateTime.Now, "yyyy/MM/dd")
    End Sub
#End Region

#Region "員工欠刀-精簡表"
    Private Sub rbnWorkNumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnWorkNumber.CheckedChanged
        If rbnWorkNumber.Checked = True Then
            txtWorkNumberD.Enabled = True
            dgGroupD.Enabled = False
        End If
    End Sub

    Private Sub rbnGroup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnGroup.CheckedChanged
        If rbnGroup.Checked = True Then
            txtWorkNumberD.Enabled = False
            dgGroupD.Enabled = True
        End If
    End Sub

    Private Sub rbnAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnAll.CheckedChanged
        If rbnAll.Checked = True Then
            txtWorkNumberD.Enabled = False
            dgGroupD.Enabled = False
        End If
    End Sub
#End Region

#Region "用刀清單"


    Private Sub ButtonEdit1_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ButtonEditA.ButtonClick
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = "510503"
        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + ButtonEditA.Left + 2
        frmWareHouseSelect.Top = Me.Top + ButtonEditA.Top + ButtonEditA.Height + 21
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            ButtonEditA.Tag = frmWareHouseSelect.SelectWareID
            ButtonEditA.Text = frmWareHouseSelect.SelectWareName
        End If

    End Sub
#End Region

#Region "獲取倉庫ID和名稱"
    Private Sub GetIdAndName(ByRef btnControl As DevExpress.XtraEditors.ButtonEdit, ByVal strValue As String)
        frmWareHouseSelect.SelectWareID = ""
        tempValue3 = strValue
        frmWareHouseSelect.SelectWareID = ""
        frmWareHouseSelect.StartPosition = FormStartPosition.Manual
        frmWareHouseSelect.Left = Me.Left + ButtonEditA.Left + 2
        frmWareHouseSelect.Top = Me.Top + ButtonEditA.Top + ButtonEditA.Height + 21
        frmWareHouseSelect.ShowDialog()
        If frmWareHouseSelect.SelectWareID = "" Then
        Else
            btnControl.Tag = frmWareHouseSelect.SelectWareID
            btnControl.Text = frmWareHouseSelect.SelectWareName
        End If
    End Sub
#End Region

#Region "庫存記錄"
    Private Sub tv1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tv1.MouseDoubleClick
        '獲取選擇的類別編號
        If tv1.SelectedNode.Level = 3 Or tv1.SelectedNode.Level = 2 Then
            popTypeID.Tag = tv1.SelectedNode.Tag
            popTypeID.EditValue = tv1.SelectedNode.Text

            btnTypeG.Tag = tv1.SelectedNode.Tag
            btnTypeG.EditValue = tv1.SelectedNode.Text

            PopupContainerControl1.OwnerEdit.ClosePopup()
        End If
    End Sub

    Private Sub btnKnifeInventoryE_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnKnifeInventoryE.ButtonClick
        GetIdAndName(btnKnifeInventoryE, "510401")
    End Sub

#End Region

#Region "調撥作業"
    Private Sub btnIssueWareHourse_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnIssueWareHourse.ButtonClick
        GetIdAndName(btnIssueWareHourse, "510306")
    End Sub

    Private Sub btnRevWareHouse_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnRevWareHouse.ButtonClick
        GetIdAndName(btnRevWareHouse, "510306")
    End Sub
#End Region
#Region "刀具收發"
    Private Sub btnWareHouseF_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnWareHouseF.ButtonClick
        GetIdAndName(btnWareHouseF, "510503")
    End Sub
#End Region

#Region "庫存記錄備份"
    Private Sub btnWareHourseG_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles btnWareHourseG.ButtonClick
        GetIdAndName(btnWareHourseG, "510401")
    End Sub
#End Region


End Class