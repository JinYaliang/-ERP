Imports LFERP.Library.PieceProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleTransaction
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.NmetalSampleManager.NmetalSamplePace

Public Class frmNmetalSampleCollectionQuery
    Dim pncon As New PersonnelControl
    Dim m_SamTransCtrl As New NmetalSampleTransactionControler
    Dim somcon As New NmetalSampleOrdersMainControler
    Private strD_ID As String       '部门ID
    Private strTID As String        '类型ID
    Private Sub btn_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Dim pncon As New NmetalSamplePaceQueryController
        Dim dss As New DataSet
        Dim ltc As New CollectionToDataSet
        Dim strSampleID As String = Nothing                  '样办单号
        Dim strDep As String = Nothing                       '部門
        Dim strStatus As String = Nothing                    '狀態
        Dim startDate As String = Nothing
        Dim endDate As String = Nothing

        If gluPM_M_Code.Text = String.Empty Then
            MsgBox("样办单号不能为空,请选择！", MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        If gluPM_M_Code.Text <> String.Empty Then
            strSampleID = gluPM_M_Code.EditValue
        End If

        If strD_ID <> String.Empty Then
            strDep = strD_ID
        End If
        If strTID <> String.Empty Then
            strStatus = strTID
        End If

        If dateAddDate.Text = String.Empty Then
            MsgBox("起始日期不能为空,请选择！", MsgBoxStyle.Information, "提示")
            Exit Sub
            dateAddDate.Focus()
        Else
            startDate = Format(CDate(dateAddDate.EditValue), "yyyy/MM/dd")
        End If

        If Dateenddate.Text = String.Empty Then
            MsgBox("结束日期不能为空,请选择！", MsgBoxStyle.Information, "提示")
            Exit Sub
            Dateenddate.Focus()
        Else
            endDate = Format(CDate(Dateenddate.EditValue), "yyyy/MM/dd")
        End If
        ltc.CollToDataSet(dss, "NmetalSamplePace", pncon.NmetalSampleCollection_GetListQueryCondition(strSampleID, strDep, strStatus, startDate, endDate))
        PreviewRPT(dss, "NmetalColleciton_Select", "条码收发一览表", True, True)
        ltc = Nothing
    End Sub

    Private Sub frmNmetalSampleCollectionQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '1加载样办单号
        Dim solist As New List(Of NmetalSampleOrdersMainInfo)
        Dim soinfo As New NmetalSampleOrdersMainInfo
        'soinfo.PM_M_Code = "ALL"
        'soinfo.SO_ID = "全部"
        'soinfo.SO_SampleID = "全部"
        'soinfo.M_Code_Type = "全部"
        solist = somcon.NmetalSampleOrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False)
        'solist.Insert(0, soinfo)
        gluPM_M_Code.Properties.DataSource = solist
        gluPM_M_Code.Select()
        'gluPM_M_Code.EditValue = "ALL"

        '2加载部门
        Dim pmlist As New List(Of PersonnelInfo)
        pmlist = pncon.FacBriSearch_GetList("V", Nothing, Nothing, Nothing)
        chkD_ID.DataSource = pmlist
        '3加载状态
        Dim samTransInfo As New List(Of NmetalSampleTransactionInfo)
        samTransInfo = m_SamTransCtrl.NmetalSampleTransactionType_GetList(Nothing, Nothing)
        chkStatusType.DataSource = samTransInfo


    End Sub
    ''' <summary>
    ''' 关闭窗体
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_exit.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 部门控件全选
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkD_IDAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkD_IDAll.CheckedChanged
        For i As Integer = 0 To Me.chkD_ID.ItemCount - 1
            If chkD_IDAll.Checked Then
                chkD_ID.SetItemChecked(i, True)
            Else
                chkD_ID.SetItemChecked(i, False)
            End If
        Next
    End Sub
    ''' <summary>
    ''' 状态控件全选
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkPaceType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPaceType.CheckedChanged
        For i As Integer = 0 To Me.chkStatusType.ItemCount - 1
            If chkPaceType.Checked Then
                chkStatusType.SetItemChecked(i, True)
            Else
                chkStatusType.SetItemChecked(i, False)
            End If
        Next
    End Sub
    ''' <summary>
    ''' 部门控件返回值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluD_ID_QueryResultValue(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.QueryResultValueEventArgs) Handles gluD_ID.QueryResultValue
        Dim str As String = String.Empty
        strD_ID = String.Empty
        For i As Integer = 0 To chkD_ID.ItemCount - 1
            If chkD_ID.GetItemChecked(i) = True Then
                str += chkD_ID.GetItemText(i) + ","
                strD_ID += "'" + chkD_ID.GetItemValue(i) + "',"
            End If
        Next
        If str.Length > 1 Then
            str = str.Remove(str.LastIndexOf(","), 1)
            strD_ID = strD_ID.Remove(strD_ID.LastIndexOf(","), 1)
        End If
        gluD_ID.ToolTipTitle = lblD_ID.Text
        gluD_ID.ToolTip = str
        e.Value = str
    End Sub
    ''' <summary>
    ''' 类型控件返回值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gluStatusType_QueryResultValue(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.QueryResultValueEventArgs) Handles gluStatusType.QueryResultValue
        Dim str As String = String.Empty
        strTID = String.Empty
        For i As Integer = 0 To chkStatusType.ItemCount - 1
            If chkStatusType.GetItemChecked(i) = True Then
                str += chkStatusType.GetItemText(i) + ","
                strTID += "'" + chkStatusType.GetItemValue(i) + "',"
            End If
        Next
        If str.Length > 1 Then
            str = str.Remove(str.LastIndexOf(","), 1)
            strTID = strTID.Remove(strTID.LastIndexOf(","), 1)
        End If
        gluStatusType.ToolTipTitle = chkStatusType.Text
        gluStatusType.ToolTip = str
        e.Value = str
    End Sub
End Class