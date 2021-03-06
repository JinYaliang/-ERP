Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Public Class frmNmetalSampleOrdersClose

#Region "属性"
    Private _PM_M_Code As String '部门编号
    Property PM_M_Code() As String
        Get
            Return _PM_M_Code
        End Get
        Set(ByVal value As String)
            _PM_M_Code = value
        End Set
    End Property
#End Region

#Region "按键事件"
    Private Sub sbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sbClose.Click
        Me.Close()
    End Sub

    Private Sub sbOrderClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sbOrderClose.Click
        Dim strPM_M_Code As String = Me.txtPM_M_Code.Text
        If strPM_M_Code = String.Empty Then Exit Sub
        If MsgBox("确定要对产品为： '" & strPM_M_Code & "' 的记录进行结案吗?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim somCon As New NmetalSampleOrdersMainControler
            Dim ds As New DataSet
            ds = somCon.NmetalSampleOrdersMain_UpdateClose(strPM_M_Code, InUserID)

            If ds.Tables.Count <= 0 Then
                MsgBox("对当前选定记录结案失财,请检查原因！", 60, "提示")
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count <= 0 Then
                MsgBox("对当前选定记录结案失财,请检查原因！", 60, "提示")
                Exit Sub
            End If

            If CBool(ds.Tables(0).Rows(0)("SO_Closed").ToString) Then
                MsgBox("当前选 定记录已结案，无法重复操作！", 60, "提示")
                Exit Sub
            End If

            If CInt(ds.Tables(0).Rows(0)("WareCount").ToString) > 0 Then
                MsgBox("当前选定记录库存为" + ds.Tables(0).Rows(0)("WareCount").ToString + "，无法结案！", 60, "提示")
                Exit Sub
            End If

            MsgBox("结案成功！", 60, "提示")
            Me.Close()

        End If
    End Sub
#End Region

#Region "窗体载入"
    Private Sub frmSampleOrdersClose_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim somCon As New NmetalSampleOrdersMainControler
            Dim soList As New List(Of NmetalSampleOrdersMainInfo)
            soList = somCon.NmetalSampleOrdersMain_GetList(Nothing, Nothing, PM_M_Code, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If soList.Count <= 0 Then
                Exit Sub
            End If

            Me.txtSO_ID.Text = soList(0).SO_ID
            Me.txtSO_SampleID.Text = soList(0).SO_SampleID
            Me.txtPM_M_Code.Text = soList(0).PM_M_Code
            Dim m_count As Int32 = somCon.NmetalSampleOrdersMain_GetWareCount(soList(0).PM_M_Code)
            Me.txtWareCount.Text = m_count

            Me.txtSO_SampleID.ForeColor = Color.Black
            Me.txtPM_M_Code.ForeColor = Color.Black
            Me.txtSO_ID.ForeColor = Color.Black
            Me.txtWareCount.ForeColor = Color.Black

            If m_count > 0 Then
                txtWareCount.ForeColor = Color.Red
                sbOrderClose.Enabled = False
            End If

            If m_count = -1 Then
                txtWareCount.ForeColor = Color.Yellow
            End If

            If soList(0).SO_Closed Then
                txtClosed.Text = "已结案"
                txtClosed.ForeColor = Color.Red
                sbOrderClose.Enabled = False
            Else
                txtClosed.Text = "未结案"
                txtClosed.ForeColor = Color.Black
            End If
        Catch

        End Try
    End Sub
#End Region
End Class