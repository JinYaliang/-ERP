Imports LFERP.Library.Positive

Public Class frmPositiveDeliverSelect
#Region "緩存參數"
    Private m_DataSet As New DataSet                '數據集

    Private m_PoCtrl As New PositiveOrdersController
    Private m_PsListInfo As New List(Of PositiveOrdersInfo)

    Private Shared m_ListTemp As New List(Of String)

    Public Property ListTemp() As List(Of String)
        Get
            Return m_ListTemp
        End Get
        Set(ByVal value As List(Of String))
            m_ListTemp = value
        End Set
    End Property

    Private m_CusterNo As String                   '客户代号

    Public Property CusterNo() As String
        Get
            Return m_CusterNo
        End Get
        Set(ByVal value As String)
            m_CusterNo = value
        End Set
    End Property

#End Region

#Region "加載訂單編號"
    ''' <summary>
    ''' 加載訂單編號
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadgluCuster()
        gluP_OM_ID.Properties.DisplayMember = "P_OM_ID"
        gluP_OM_ID.Properties.ValueMember = "P_OM_ID"
        ' gluP_OM_ID.Properties.DataSource = m_PoCtrl.PositiveOrders_GetPOMIDList(m_CusterNo)

    End Sub
#End Region

#Region "創建臨時表"
    Private Sub CreateTables()

        m_DataSet.Tables.Clear()
        With m_DataSet.Tables.Add("PositiveOrders")
            .Columns.Add("Input", GetType(Boolean))
            .Columns.Add("P_OM_Num", GetType(String))
            .Columns.Add("P_OM_ID", GetType(String))

            .Columns.Add("P_OM_CusterPO", GetType(String))
            .Columns.Add("P_M_Code", GetType(String))
            .Columns.Add("PartNumber", GetType(String))
            .Columns.Add("Qty", GetType(String))
            .Columns.Add("NoSendQty", GetType(String))
        End With
        dgPositiveOrders.DataSource = m_DataSet.Tables("PositiveOrders")

    End Sub

#End Region

#Region "根據單號導入數據"
    Private Sub cmdInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInput.Click

        m_DataSet.Tables("PositiveOrders").Rows.Clear()

        If gluP_OM_ID.EditValue = String.Empty Then
            MsgBox("請輸入訂單編號!", 64, "提示")
            Exit Sub
        End If

        '  m_PsListInfo = m_PoCtrl.PositiveOrders_GetList(gluP_OM_ID.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If m_PsListInfo Is Nothing Then
            MsgBox("此張單不存在!", 64, "提示")
            Exit Sub
        End If

        If m_PsListInfo.Count <= 0 Then
            MsgBox("此張單不存在!", 64, "提示")
            Exit Sub
        End If

        Dim nIndex As Integer

        For nIndex = 0 To m_PsListInfo.Count - 1

            Dim row As DataRow

            row = m_DataSet.Tables("PositiveOrders").NewRow

            row("Input") = False

            row("P_OM_CusterPO") = m_PsListInfo(nIndex).P_OM_CusterPO

            row("P_OM_Num") = m_PsListInfo(nIndex).P_OM_Num

            row("P_OM_ID") = m_PsListInfo(nIndex).P_OM_ID

            row("P_M_Code") = m_PsListInfo(nIndex).P_M_Code

            row("PartNumber") = m_PsListInfo(nIndex).PartNumber

            row("Qty") = m_PsListInfo(nIndex).Qty

            row("NoSendQty") = m_PsListInfo(nIndex).NoSendQty

            m_DataSet.Tables("PositiveOrders").Rows.Add(row)

        Next

    End Sub
#End Region

#Region "保存數據"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click


        If dgvPositiveOrders.RowCount <= 0 Then
            MsgBox("請選擇送貨產品!", 64, "提示")
            Exit Sub
        End If
     

        If Not (m_ListTemp Is Nothing) Then
            m_ListTemp.Clear()
        End If


        Dim nIndex As Integer
        For nIndex = 0 To m_DataSet.Tables("PositiveOrders").Rows.Count - 1



            If Convert.ToBoolean(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("Input")) Then

                ' m_PsListInfo = m_PoCtrl.PositiveOrders_GetList(Nothing, m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_OM_Num"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

                If m_PsListInfo Is Nothing Then
                    MsgBox("訂單有改變請重新導入!", 64, "提示")
                    Exit Sub
                End If

                If m_PsListInfo.Count <= 0 Then
                    MsgBox("訂單有改變請重新導入!", 64, "提示")
                    Exit Sub
                End If

                m_ListTemp.Add(m_DataSet.Tables("PositiveOrders").Rows(nIndex)("P_OM_Num").ToString())


            End If
        Next

        Me.Close()

    End Sub
#End Region

#Region "關閉窗體程序"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        If Not (m_ListTemp Is Nothing) Then
            m_ListTemp.Clear()
        End If
        Me.Close()
    End Sub
#End Region

#Region "初始化加載程序"
    Private Sub frmPositiveDeliverSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateTables()
        LoadgluCuster()
    End Sub
#End Region

End Class