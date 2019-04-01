Imports DevExpress.XtraGrid.Views.BandedGrid
Imports LFERP.Library.MrpManager.MrpMps
Imports LFERP.Library.MrpManager.Bom_M
Imports LFERP.Library.MrpManager.MrpSetting

Public Class frmMrpMpsBrowse
    Dim mmc As New MrpMpsController
    Dim bc As New Bom_MController
    Dim msc As New MrpSettingController

    Private Sub frmMrpMpsBrowse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim bi As New Bom_MInfo
        Dim biList As New List(Of Bom_MInfo)
        bi.ParentGroup = "ALL"
        bi.M_Name = "����"
        'biList = bc.Bom_M_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        biList.Insert(0, bi)
        gluM_Code.Properties.DataSource = biList

        Dim msiList As List(Of MrpSettingInfo)
        msiList = msc.MrpSetting_GetList(InUserID)
        If msiList.Count > 0 Then
            dteBeginDate.EditValue = msiList(0).mrpMpsBrowserBeginDate
            dteEndDate.EditValue = msiList(0).mrpMpsBrowserEndDate
        Else
            dteBeginDate.EditValue = CDate(Now.Year.ToString + "/01/01")
            dteEndDate.EditValue = CDate(Now.Year.ToString + "/12/31")
        End If

    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim GroupType As String = String.Empty
        Dim M_Code As String = String.Empty
        Dim CusterFlag As Boolean = False
        If CDate(dteBeginDate.EditValue).CompareTo(CDate(dteEndDate.EditValue)) > 0 Then
            MsgBox("�}�l�������ߤ_�������", MsgBoxStyle.Information, "����")
            Exit Sub
        End If
        GroupType = IIf(cboGroupType.SelectedIndex = 0 Or cboGroupType.SelectedIndex = 2, "week", "month")
        CusterFlag = IIf(cboGroupType.SelectedIndex = 2 Or cboGroupType.SelectedIndex = 3, True, False)
        M_Code = IIf(gluM_Code.EditValue = "ALL", Nothing, gluM_Code.EditValue.ToString)
        Dim dt As DataTable = mmc.MrpMps_GetGroupList(CDate(dteBeginDate.EditValue), CDate(dteEndDate.EditValue), GroupType, M_Code, CusterFlag)
        LoadBandedGrid(dt, BandedView1, GroupType, CusterFlag)
    End Sub

    Private Sub LoadBandedGrid(ByVal dt As DataTable, ByVal BandedView As BandedGridView, ByVal GroupType As String, ByVal CusterFlag As Boolean)
        BandedView.Bands.Clear()      '�M��BandedGridView 
        BandedView.Columns.Clear()
        BandedView.OptionsBehavior.Editable = False
        Dim strYear As String = String.Empty     '�[���~����band�ɫO�s�e�@��band����ܤ�r
        If dt Is Nothing Then
            Exit Sub
        End If
        Try
            BandedView.Bands.AddBand("���~�H��").Name = "ProductionInfo"
            BandedView.Bands("ProductionInfo").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            Grid1.DataSource = dt

            BandedView.Bands.AddBand("�Ȥ�H��").Name = "CusterInfo"
            BandedView.Bands("CusterInfo").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            For Each dc As DataColumn In dt.Columns
                If dc.DataType Is GetType(String) Then
                    If dc.ColumnName.StartsWith("Custer") Then
                        BandedView.Bands("CusterInfo").Columns.Add(BandedView.Columns(dc.ColumnName))
                        BandedView.Bands("CusterInfo").Visible = CusterFlag
                    Else
                        BandedView.Bands("ProductionInfo").Columns.Add(BandedView.Columns(dc.ColumnName))
                    End If
                End If
                If Int32.TryParse(dc.ColumnName, 1) = True Then
                    If dc.ColumnName.Remove(4) <> strYear Then
                        BandedView.Bands.AddBand(dc.ColumnName.Remove(4) + "�~").Name = dc.ColumnName.Remove(4)
                        BandedView.Bands(dc.ColumnName.Remove(4)).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End If
                    If GroupType = "week" Then
                        BandedView.Bands(dc.ColumnName.Remove(4)).Children.AddBand(GetWeekRange(dc.ColumnName)).Name = GetWeekRange(dc.ColumnName)
                        BandedView.Bands(dc.ColumnName.Remove(4)).Children(GetWeekRange(dc.ColumnName)).Columns.Add(BandedView.Columns(dc.ColumnName))
                        BandedView.Bands(dc.ColumnName.Remove(4)).Children(GetWeekRange(dc.ColumnName)).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        BandedView.Columns(dc.ColumnName).Caption = (dc.ColumnName + "�P").Insert(4, "�~")
                    Else
                        BandedView.Bands(dc.ColumnName.Remove(4)).Children.AddBand(dc.ColumnName.Remove(0, 4) + "��").Name = dc.ColumnName.Remove(0, 4)
                        BandedView.Bands(dc.ColumnName.Remove(4)).Children(dc.ColumnName.Remove(0, 4)).Columns.Add(BandedView.Columns(dc.ColumnName))
                        BandedView.Bands(dc.ColumnName.Remove(4)).Children(dc.ColumnName.Remove(0, 4)).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        BandedView.Columns(dc.ColumnName).Caption = (dc.ColumnName + "��").Insert(4, "�~")
                    End If
                    BandedView.Columns(dc.ColumnName).Width = 90
                    strYear = dc.ColumnName.Remove(4)   '�O�s�~��
                End If
                If dc.DataType Is GetType(Decimal) Then
                    BandedView.Columns(dc.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    BandedView.Columns(dc.ColumnName).DisplayFormat.FormatString = " {0:N0}"
                End If
            Next
            BandedView.Bands.AddBand("�`�p").Name = "PlanQtySum"
            BandedView.Bands("PlanQtySum").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            BandedView.Bands("PlanQtySum").Columns.Add(BandedView.Columns("PlanQtySum"))
            BandedView.Columns("PlanQtySum").Width = 95

            SetBandedViewStyle(BandedView)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "����")
        End Try

    End Sub

#Region "��J�~�P��^���w�P�ƪ�����S��"
    Private Function GetWeekRange(ByVal yearWeek As String) As String
        Dim firstDayOfYear As Date = CDate(yearWeek.Remove(4) + ".01.01")
        Dim endDayofYear As Date = CDate(yearWeek.Remove(4) + ".12.31")
        Dim week As Int16 = CInt(yearWeek.Remove(0, 4))
        Dim sencondWeekBeginDate As Date          '���w�~���ĤG�P���}�l���

        Dim i As Int16 = Microsoft.VisualBasic.DatePart(DateInterval.Weekday, firstDayOfYear) - 1
        sencondWeekBeginDate = firstDayOfYear.AddDays(6 - i + 1)
        Dim beginDayOfWeek As Date = sencondWeekBeginDate.AddDays((week - 2) * 7)
        Dim endDayofWeek As Date = beginDayOfWeek.AddDays(6)
        If endDayofWeek > endDayofYear Then
            endDayofWeek = endDayofYear
        End If
        Return Format(beginDayOfWeek, "MM.dd") + "-" + Format(endDayofWeek, "MM.dd")
    End Function
#End Region

#Region "�]�mBandedView���˦�"
    Private Sub SetBandedViewStyle(ByVal BandedView As BandedGridView)
        BandedView.Bands("ProductionInfo").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        BandedView.Bands("PlanQtySum").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

        BandedView.Columns("M_Code").Caption = "���~�s�X"
        BandedView.Columns("M_Code").Width = 135
        BandedView.Columns("M_Gauge").Caption = "���~�W��"
        BandedView.Columns("M_Gauge").Width = 170
        BandedView.Columns("M_Name").Caption = "���~�W��"
        BandedView.Columns("M_Name").Width = 135
        BandedView.Columns("M_Unit").Caption = "���"
        BandedView.Columns("M_Unit").Width = 45
        BandedView.Columns("M_Source").Caption = "�ӷ��X"
        BandedView.Columns("M_Source").Width = 60

        BandedView.Columns("CusterID").Caption = "�Ȥ�s��"
        BandedView.Columns("CusterID").Width = 70
        BandedView.Columns("CusterName").Caption = "�Ȥ�W��"
        BandedView.Columns("CusterName").Width = 80

        BandedView.Columns("PlanQtySum").Caption = "�ƶq"
        BandedView.Columns("PlanQtySum").Width = 50

        Dim ItemMemoex As New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
        ItemMemoex.ShowIcon = False
        BandedView.Columns("M_Gauge").ColumnEdit = ItemMemoex
    End Sub
#End Region

#Region "�ɥXExcel"
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If BandedView1.RowCount < 1 Then
            MsgBox("�S���i�ɥX���ƾ�", MsgBoxStyle.Information, "����")
            Exit Sub
        End If
        Dim sfd As New SaveFileDialog
        sfd.DefaultExt = ".xls"
        sfd.Filter = "Excel Files|*.xls|All Files|*.*"
        If sfd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Grid1.ExportToXls(sfd.FileName)
            MsgBox("�w���\�ɥX", MsgBoxStyle.Information, "����")
        End If
    End Sub
#End Region

End Class