Imports System.IO
Imports System.Data.SqlClient
Imports system.data.oledb
Imports LFERPDB

Imports System
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SamplePlan
Imports LFERP.Library.SampleManager.SamplePace

Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraEditors.Repository
Imports LFERP.Library.SampleManager.SampleSetting
Imports LFERP.Library.SampleManager.SampleOrdersMain

Public Class frmSamplePlan
#Region "属性"
    Dim ds As New DataSet
    Dim SPC As New SamplePlanControler
    Dim SPE As New SamplePaceControler
#End Region

#Region "窗体载入"
    Private Sub frmSamplePlan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' CreateTable()
        PowerUser()
        LoadDate()
        DateEdit1.Text = Format(DateAdd("m", -1, Now()), "yyyy/MM/dd")
        DateEdit2.Text = Format(Now, "yyyy/MM/dd")
    End Sub
#End Region

#Region "載入数据"
    ''' <summary>
    ''' 載入数据
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadDate()
        Dim msi As New List(Of SampleSettingInfo)
        Dim msc As New SampleSettingController

        Dim StrCheck As String = Nothing
        Dim StrUser As String = Nothing
        Dim StrOrdersBeginDate As String = Nothing
        msi = msc.SampleSetting_GetList(InUserID)
        If msi.Count > 0 Then
            '1.審核類型
            Select Case msi(0).SamplePlanCheck
                Case "0,1"
                    StrCheck = Nothing
                Case "1"
                    StrCheck = "True"
                Case "0"
                    StrCheck = "False"
            End Select

            '1.用戶選擇
            If msi(0).SamplePlanCreateUserID = "All" Then
                StrUser = Nothing
            Else
                StrUser = msi(0).SamplePlanCreateUserID
            End If

            gridSamplePlan.DataSource = SPC.SamplePlan_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, msi(0).SamplePlanBeginDate, Nothing, StrUser)
        Else
            gridSamplePlan.DataSource = SPC.SamplePlan_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing)
        End If
    End Sub
#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SamplePlan")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("SP_ID", GetType(String))
            .Columns.Add("SO_ID", GetType(String))
            .Columns.Add("SS_Edition", GetType(String))
            .Columns.Add("SP_StartDate", GetType(Date))
            .Columns.Add("SP_EndDate", GetType(Date))
            .Columns.Add("SP_Remark", GetType(String))
            .Columns.Add("SP_AddUserID", GetType(String))
            .Columns.Add("SP_AddDate", GetType(Date))
            .Columns.Add("SP_AddUserName", GetType(String))
            .Columns.Add("SP_ModifyUserID", GetType(String))
            .Columns.Add("SP_ModifyDate", GetType(Date))
            .Columns.Add("SP_ModifyUserName", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("M_Code", GetType(String))
        End With
        gridSamplePlan.DataSource = ds.Tables("SamplePlan")

        With ds.Tables.Add("SamplePace")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("SO_ID", GetType(String))
            .Columns.Add("SS_Edition", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("SE_PaceDescribe", GetType(String))
            .Columns.Add("SE_AddUserID", GetType(String))
            .Columns.Add("SE_AddDate", GetType(Date))
            .Columns.Add("SE_AddUserName", GetType(String))
            .Columns.Add("SE_ModifyUserID", GetType(String))
            .Columns.Add("SE_ModifyDate", GetType(Date))
            .Columns.Add("SE_ModifyUserName", GetType(String))
        End With
        gridSamplePace.DataSource = ds.Tables("SamplePace")
    End Sub
#End Region

    Private Sub gridSamplePlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridSamplePlan.Click
        Try
            PaceDetail()
        Catch
        End Try
    End Sub

    ''' <summary>
    '''修改事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        On Error Resume Next
        Dim strSP_ID As String = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        If strSP_ID = String.Empty Or strSP_ID = Nothing Then
            Exit Sub
        End If
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSamplePlanAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePlanAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePlanAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.EDIT
        fr.txtSP_ID.EditValue = strSP_ID

        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    ''' 新增事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        On Error Resume Next
        Dim fr As frmSamplePlanAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePlanAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePlanAdd
        fr.MdiParent = MDIMain
        fr.txtSP_ID.Text = "自動编号"
        fr.EditItem = EditEnumType.ADD
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    ''' <summary>
    '''刪除事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        '刪除产品资料
        If GridView3.RowCount = 0 Then Exit Sub

        Dim som As New List(Of SamplePaceInfo)
        Dim StrSO_ID, StrSS_Edition As String
        StrSO_ID = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        StrSS_Edition = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
        som = SPE.SamplePace_Getlist(Nothing, StrSO_ID, StrSS_Edition, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If som.Count > 0 Then
            MsgBox("存在样办进度无法刪除", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        If MsgBox("你確定要刪除 " & GridView3.GetFocusedRowCellValue("SP_ID").ToString & " 这个样办排期资料嗎?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.No Then Exit Sub

        Dim smcon As New SampleOrdersMainControler
        Dim SampleMainInfo As New SampleOrdersMainInfo
        SampleMainInfo.SO_ID = StrSO_ID
        SampleMainInfo.SO_State = ""
        smcon.SampleOrdersMain_UpdateState(SampleMainInfo)

        If SPC.SamplePlan_Delete(GridView3.GetFocusedRowCellValue("AutoID").ToString) = True Then
            LoadDate()
        End If
    End Sub
    ''' <summary>
    ''' 查询事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim fr As New frmSampleView
        fr = New frmSampleView
        fr.lbl_Title.Text = "样办查询--排期"
        fr.chk_ID.Text = "排期单号(&E)"
        fr.EditItem = "SamplePlan"
        fr.ShowDialog()
        If fr.SamplePlanList.Count = 0 Then
            gridSamplePlan.DataSource = Nothing
            Exit Sub
        Else
            gridSamplePlan.DataSource = fr.SamplePlanList
        End If
    End Sub
    ''' <summary>
    ''' 审核事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        On Error Resume Next
        Dim fr As frmSamplePlanAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePlanAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePlanAdd
        fr.MdiParent = MDIMain
        fr.EditItem = EditEnumType.CHECK
        fr.gluSO_ID.EditValue = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        fr.XtraTabControl1.SelectedTabPage = fr.XtraTabPage2
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub
    '设置权限
    Sub PowerUser()
        Dim pmws As New PermissionModuleWarrantSubController
        Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890301")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdAdd.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890302")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdEdit.Enabled = True
        End If

        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890303")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdDel.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890304")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdCheck.Enabled = True
        End If
        pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890305")
        If pmwiL.Count > 0 Then
            If pmwiL.Item(0).PMWS_Value = "是" Then cmdPrint.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' 刷新事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click
        LoadDate()
    End Sub
    ''' <summary>
    ''' 查看事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdLook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLook.Click
        On Error Resume Next
        If GridView3.RowCount = 0 Then Exit Sub
        Dim fr As frmSamplePlanAdd
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePlanAdd Then
                fr.Activate()
                Exit Sub
            End If
        Next
        fr = New frmSamplePlanAdd
        fr.MdiParent = MDIMain

        fr.EditItem = EditEnumType.VIEW
        fr.txtSP_ID.EditValue = GridView3.GetFocusedRowCellValue("SP_ID").ToString
        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        '1.正確判斷
        Dim strStratDate, StrEndDate As String
        strStratDate = Format(CDate(DateEdit1.Text), "yyyy/MM/dd")
        StrEndDate = Format(CDate(DateEdit2.Text), "yyyy/MM/dd")
        If strStratDate = String.Empty Then
            MsgBox("开始日期不能为空,请输入", MsgBoxStyle.Information, "提示")
            Exit Sub
            DateEdit1.Focus()
        End If
        If StrEndDate = String.Empty Then
            MsgBox("结束日期不能为空,请输入", MsgBoxStyle.Information, "提示")
            Exit Sub
            DateEdit2.Focus()
        End If
        gridSamplePlan.DataSource = SPC.SamplePlan_Getlist(Nothing, Nothing, Nothing, strStratDate, StrEndDate, Nothing, Nothing, False, Nothing, Nothing, Nothing)
    End Sub

    Sub PaceDetail()
        If GridView3.RowCount = 0 Then Exit Sub
        Dim StrSO_ID, StrSS_Edition As String
        StrSO_ID = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        StrSS_Edition = GridView3.GetFocusedRowCellValue("SS_Edition").ToString
        gridSamplePace.DataSource = SPE.SamplePace_Getlist(Nothing, StrSO_ID, StrSS_Edition, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        '李超顯示進度隱
        'Dim StrWhere As String
        'StrWhere = "where SO_ID='" + StrSO_ID + "' and SS_Edition =" + StrSS_Edition + ""
        'Ds_ProductionDay(StrWhere)
    End Sub

    Private Sub GridView3_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        Try
            PaceDetail()
        Catch
        End Try
    End Sub

    Private Sub GridView3_CustomDrawCell(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView3.CustomDrawCell
        Try
            If e.Column.FieldName = "SP_EndDate" Then
                If CDate(e.CellValue) = CDate(Format(Now(), "yyyy/MM/dd")) Then
                    If GridView3.GetRowCellValue(e.RowHandle, "SO_Closed") = False Then
                        e.Appearance.BackColor = Color.Tomato
                    End If
                End If
                If CDate(e.CellValue) < CDate(Format(Now(), "yyyy/MM/dd")) And e.CellValue <> Nothing Then
                    If GridView3.GetRowCellValue(e.RowHandle, "SO_Closed") = False Then
                        e.Appearance.BackColor = Color.Red
                    End If
                End If
                If CDate(e.CellValue) > CDate(Format(Now(), "yyyy/MM/dd")) And CDate(e.CellValue) <= CDate(Format(DateAdd("d", +3, Now()), "yyyy/MM/dd")) Then
                    If GridView3.GetRowCellValue(e.RowHandle, "SO_Closed") = False Then
                        e.Appearance.BackColor = Color.DarkOrange
                    End If
                End If
            End If
        Catch

        End Try
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim SPC As New SamplePlanControler
        '''''''''''''''''''''''''''''''''''''''\
        Dim som As New List(Of SamplePlanInfo)
        Dim a As Integer
        For a = 0 To GridView3.RowCount - 1
            Dim Spinfo As New SamplePlanInfo
            Spinfo.AutoID = GridView3.GetRowCellValue(a, "AutoID")
            Spinfo.M_Code = GridView3.GetRowCellValue(a, "M_Code")
            Spinfo.PM_M_Code = GridView3.GetRowCellValue(a, "PM_M_Code")
            Spinfo.SO_ID = GridView3.GetRowCellValue(a, "SO_ID")
            Spinfo.SP_ID = GridView3.GetRowCellValue(a, "SP_ID")
            Spinfo.SP_StartDate = GridView3.GetRowCellValue(a, "SP_StartDate")
            Spinfo.SP_EndDate = GridView3.GetRowCellValue(a, "SP_EndDate")
            Spinfo.SS_Edition = GridView3.GetRowCellValue(a, "SS_Edition")
            Spinfo.SP_Remark = GridView3.GetRowCellValue(a, "SP_Remark")
            Spinfo.SP_AddDate = GridView3.GetRowCellValue(a, "SP_AddDate")
            Spinfo.SP_AddUserName = GridView3.GetRowCellValue(a, "SP_AddUserName")
            Spinfo.M_Name = GridView3.GetRowCellValue(a, "M_Name")
            som.Add(Spinfo)
        Next

        ltc.CollToDataSet(dss, "SamplePlan", som)
        '  ltc.CollToDataSet(dss, "SamplePlan", SPC.SamplePlan_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        PreviewRPT(dss, "rptSamplePlan", "样办排期表", True, True)
        ltc = Nothing
        ' Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim StrEndDate As String
        StrEndDate = Format(DateAdd("d", -1, Now()), "yyyy/MM/dd")
        gridSamplePlan.DataSource = SPC.SamplePlan_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, StrEndDate, True, Nothing, Nothing, Nothing)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim StrEndDate, strStratDate As String
        strStratDate = Format(Now(), "yyyy/MM/dd")
        StrEndDate = Format(Now(), "yyyy/MM/dd")
        gridSamplePlan.DataSource = SPC.SamplePlan_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, strStratDate, StrEndDate, True, Nothing, Nothing, Nothing)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim strStratDate, StrEndDate As String
        strStratDate = Format(DateAdd("d", +1, Now()), "yyyy/MM/dd")
        StrEndDate = Format(DateAdd("d", +3, Now()), "yyyy/MM/dd")
        gridSamplePlan.DataSource = SPC.SamplePlan_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, strStratDate, StrEndDate, True, Nothing, Nothing, Nothing)
    End Sub


    Function Ds_ProductionDay(ByVal StrWhere As String) As Boolean
        Dim view As BandedGridView = TryCast(Me.AdvBandedGridView1, BandedGridView)
        view.Bands.Clear()
        view.Columns.Clear()
        'view.BeginUpdate();
        view.BeginUpdate()
        '开始视图的编辑，防止触发其他事件
        view.BeginDataUpdate()
        '开始数据的编辑            
        '添加列标题
        view.OptionsView.ShowColumnHeaders = False
        view.OptionsView.ColumnAutoWidth = False
        view.OptionsView.EnableAppearanceEvenRow = False  '';                   //是否启用偶数行外观
        view.OptionsView.EnableAppearanceOddRow = False  '';                     //是否启用奇数行外观
        view.OptionsCustomization.AllowColumnResizing = False ';              //是否允许调整列宽
        view.OptionsView.ShowFooter = False
        view.OptionsView.AllowCellMerge = False
        ' view.OptionsView.ShowFooter = True

        view.OptionsSelection.EnableAppearanceFocusedCell = False
        view.OptionsSelection.EnableAppearanceFocusedRow = False

        Dim DSHead As New DataSet
        DSHead.Tables.Clear()
        DSHead.Clear()

        Dim SQLStr As String = ""
        Dim SumSQLStr As String = ""
        '++++
        Dim dsFile(1000, 1000) As String
        Dim MaxTypes As Integer = 0
        Dim JS As Integer
        '++++
        Dim Sql As String = ""

        view.Bands.AddBand("工序编号")
        view.Bands.AddBand("工序名称")

        '--------------------------------------------------
        Dim i, j As Integer
        Dim days As Integer
        Dim dsDate As New DataSet
        dsDate = DataSet("SELECT distinct SE_AddDate FROM  VVSamplePace " + StrWhere)
        days = dsDate.Tables(0).Rows.Count
        If days <= 0 Then
            GoTo aa
        End If
        '---------------------------------------------------
        For i = 1 To days
            Dim DayName As String
            DayName = dsDate.Tables(0).Rows(i - 1)("SE_AddDate")
            view.Bands.AddBand(Format(CDate(dsDate.Tables(0).Rows(i - 1)("SE_AddDate")), "MM月dd日"))
            view.Bands(i + 1).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim dsDateType As New DataSet
            Dim TypeS As Integer
            Dim SE_TypeName As String
            dsDateType = DataSet("SELECT distinct SE_Type,SE_TypeName FROM  VVSamplePace " + StrWhere + " and SE_AddDate ='" + DayName + "'")
            TypeS = dsDateType.Tables(0).Rows.Count
            If TypeS <= 0 Then
                Exit For
            End If

            If MaxTypes <= TypeS Then
                MaxTypes = TypeS
            End If

            For j = 1 To TypeS
                SE_TypeName = dsDateType.Tables(0).Rows(j - 1)("SE_TypeName").ToString
                view.Bands(i + 1).Children.AddBand(SE_TypeName)
                view.Bands(i + 1).Children(j - 1).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                dsFile(i, j) = Trim(Format(CDate(dsDate.Tables(0).Rows(i - 1)("SE_AddDate")), "yyyy-MM-dd") & dsDateType.Tables(0).Rows(j - 1)("SE_TypeName").ToString)
            Next

            JS = JS + 1
        Next

        view.Bands.AddBand("合计")
        '-----------------表頭----------------------------------------------------------------------------------------------------
        Dim DsContent As New DataSet
        Dim Sqlcontent As String = ""
        Dim Sqlcontent1 As String = ""
        Dim Sqlcontent2 As String = ""
        Dim DsContentJS As Integer
        DsContent = DataSet("select distinct SE_TypeN FROM VVSamplePace " + StrWhere)
        DsContentJS = DsContent.Tables(0).Rows.Count
        If DsContentJS <= 0 Then
            Exit Function
        End If
        Dim k As Integer

        For k = 0 To DsContentJS - 1
            If k = DsContentJS - 1 Then
                Sqlcontent1 = Sqlcontent1 + "sum([" + DsContent.Tables(0).Rows(k)("SE_TypeN") + "]) as [" + DsContent.Tables(0).Rows(k)("SE_TypeN") + "]"
                Sqlcontent2 = Sqlcontent2 + "[" + DsContent.Tables(0).Rows(k)("SE_TypeN") + "]"
            Else
                Sqlcontent1 = Sqlcontent1 + "sum([" + DsContent.Tables(0).Rows(k)("SE_TypeN") + "]) as [" + DsContent.Tables(0).Rows(k)("SE_TypeN") + "],"
                Sqlcontent2 = Sqlcontent2 + "[" + DsContent.Tables(0).Rows(k)("SE_TypeN") + "],"
            End If
        Next

        Sqlcontent = "select PS_NO,PS_Name," + Sqlcontent1 + " into #temp FROM VVSamplePace PIVOT(SUM(SE_Qty) FOR SE_TypeN in (" + Sqlcontent2 + "))" + "  AS P " + StrWhere + "  group by PS_NO,PS_Name "
        Sqlcontent = Sqlcontent + "  select *, (select SUM(SE_Qty ) from VVSamplePace  " + StrWhere + " and PS_NO =#temp.PS_NO ) as SumQty  from #temp order by PS_NO   drop table #temp"
        'SELECT  PS_NO,SUM([2013-09-05大貨]) as [2013-09-05大貨] ,SUM([2013-09-05損壞]) as [2013-09-05損壞] 
        'FROM VVSamplePace PIVOT(SUM(SE_Qty) FOR SE_TypeN IN([2013-09-05大貨],[2013-09-05損壞])) AS P  group by PS_NO 
        ' FileOpen(1, "c:\log1.log", OpenMode.Output)
        'Write(1, Sqlcontent)
        'FileClose(1)


        Me.GridControlExcel.DataSource = DataSet(Sqlcontent).Tables(0)

        view.Columns("PS_NO").OwnerBand = view.Bands(0)
        view.Columns("PS_Name").OwnerBand = view.Bands(1)
        view.Columns("SumQty").OwnerBand = view.Bands(JS + 2)
        view.Bands(0).Fixed = FixedStyle.Left
        view.Bands(1).Fixed = FixedStyle.Left

        view.Columns("PS_NO").Width = 65
        view.Columns("PS_NO").OptionsColumn.ReadOnly = True
        view.Columns("PS_NO").Visible = False
        view.Bands(0).Visible = False

        view.Columns("PS_Name").Width = 120
        view.Columns("PS_Name").OptionsColumn.ReadOnly = True
        view.Columns("PS_Name").OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

        'On Error Resume Next
        For i = 1 To days
            For j = 1 To MaxTypes
                If dsFile(i, j) <> "" Then
                    view.Columns(dsFile(i, j)).OwnerBand = view.Bands(i + 1).Children(j - 1)
                    view.Columns(dsFile(i, j)).OptionsColumn.ReadOnly = True
                    view.Columns(dsFile(i, j)).Width = 65
                    view.Columns(dsFile(i, j)).SummaryItem.FieldName = dsFile(i, j)
                    'view.Columns(dsFile(i, j)).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                End If
            Next
        Next

        view.Bands(JS + 2).Fixed = FixedStyle.Right
        view.Columns("SumQty").SummaryItem.FieldName = "SumQty"
        'view.Columns("SumQty").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min
        view.Columns("SumQty").OptionsColumn.ReadOnly = True
aa:
        view.EndDataUpdate()
        '结束数据的编辑
        view.EndUpdate()
        '结束视图的编辑

    End Function

    Private Sub AdvBandedGridView1_CustomDrawCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles AdvBandedGridView1.CustomDrawCell
        Dim strTemp As String = "0"
        If IsDBNull(AdvBandedGridView1.GetRowCellDisplayText(e.RowHandle, e.Column)) = False Then
            strTemp = AdvBandedGridView1.GetRowCellDisplayText(e.RowHandle, e.Column)
            If Val(strTemp) > 0 Then
                e.Appearance.BackColor = Color.DeepSkyBlue
            End If

            If InStr(e.Column.FieldName, "損壞", CompareMethod.Text) > 0 Or InStr(e.Column.FieldName, "损坏", CompareMethod.Text) > 0 Then
                If Val(strTemp) > 0 Then
                    e.Appearance.BackColor = Color.Red
                End If
            ElseIf InStr(e.Column.FieldName, "Sum", CompareMethod.Text) > 0 Then
                If Val(strTemp) > 0 Then
                    e.Appearance.BackColor = Color.LightGreen
                End If
            End If
        End If
    End Sub

    Function DataSet(ByVal sql As String) As DataSet
        Dim strCon As String
        Dim ai As New LFERPDataBase
        strCon = ai.LoadConnStr

        Dim ds As New DataSet
        Dim conn As SqlConnection
        Dim da As SqlDataAdapter
        conn = New SqlConnection
        conn.ConnectionString = strCon
        conn.Open()
        da = New SqlDataAdapter(sql, conn)
        da.Fill(ds)

        da.Dispose()
        conn.Close()
        Return ds
    End Function


    Private Sub cmdPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        Dim fr As frmSamplePaceInsert
        For Each fr In MDIMain.MdiChildren
            If TypeOf fr Is frmSamplePaceInsert Then
                fr.Activate()
                Exit Sub
            End If
        Next

        fr = New frmSamplePaceInsert
        fr.MdiParent = MDIMain
        fr.Lbl_Title.Text = "样办进度--新增"
        fr.EditItem = "Out"

        fr.gluSO_ID.EditValue = GridView3.GetFocusedRowCellValue("SO_ID").ToString
        fr.gluPM_M_Code.EditValue = GridView3.GetFocusedRowCellValue("PM_M_Code").ToString
        fr.txtSS_Edition.Text = GridView3.GetFocusedRowCellValue("SS_Edition").ToString

        fr.WindowState = FormWindowState.Maximized
        fr.Show()
    End Sub

    Private Sub cmdExcelA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcelA.Click
        If GridView3.RowCount = 0 Then Exit Sub
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Title = "導出Excel"
        saveFileDialog.Filter = "Excel2003文件(*.xls)|*.xls"
        Dim FiledialogResult As DialogResult = saveFileDialog.ShowDialog(Me)
        If FiledialogResult = Windows.Forms.DialogResult.OK Then
            If ExportToExcelOld(gridSamplePlan, saveFileDialog.FileName) Then
                MsgBox("已成功導出到：" + saveFileDialog.FileName, MsgBoxStyle.Information, "提示")
            End If
        End If

    End Sub
End Class