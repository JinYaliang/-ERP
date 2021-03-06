Imports LFERP.Library.Product
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain


Public Class frmNmetalSampleDepWeightCheckView
    Dim ds As New DataSet
    Dim strCode As String
    Dim prcon As New NmetalSampleProcessControl
    Dim sc As New NmetalSampleOrdersMainControler
    Dim _DepName As String
    Dim _DepNameID As String
    Property DepName() As String '部門
        Get
            Return _DepName
        End Get
        Set(ByVal value As String)
            _DepName = value
        End Set
    End Property
    Property DepNameID() As String '部門ID
        Get
            Return _DepNameID
        End Get
        Set(ByVal value As String)
            _DepNameID = value
        End Set
    End Property


    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Material")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("SWI_Weight", GetType(String))
            .Columns.Add("GoIn", GetType(Boolean))
            .Columns.Add("SWI_Qty", GetType(String))
        End With
        Grid.DataSource = ds.Tables("Material")
        With ds.Tables.Add("ProductType")
            .Columns.Add("M_Name", GetType(String))
        End With
        txt_gluType.Properties.DisplayMember = "M_Name"
        txt_gluType.Properties.ValueMember = "M_Name"
        txt_gluType.Properties.DataSource = ds.Tables("ProductType")
    End Sub
    ''' <summary>
    ''' 讀取產品編號
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadProductNo()
        'Dim mc As New ProductController
        PM_M_Code.Properties.DisplayMember = "PM_M_Code"
        PM_M_Code.Properties.ValueMember = "PM_M_Code"
        'PM_M_Code.Properties.DataSource = mc.Product_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        PM_M_Code.Properties.DataSource = sc.NmetalSampleProcess_GetPRONO(Nothing)
    End Sub
    ''' <summary>
    ''' 窗體加載事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNmetalSampleDepWeightCheckView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cbType.EditValue = "生產加工"
        PM_M_Code.EditValue = ""
        txt_gluType.EditValue = ""
        txt_DepName.Text = DepName
        CreateTable()
        LoadProductNo()
        PM_M_Code.Focus()
        PM_M_Code.Select()
    End Sub
    ''' <summary>
    ''' 產品編碼變更
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PM_M_Code.EditValueChanged
        On Error Resume Next
        'Dim pc As New ProductController
        'Dim piL As List(Of ProductInfo)NmetalSampleProcessInfo
        'piL = pc.Product_GetList(PM_M_Code.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Dim piL As List(Of NmetalSampleProcessInfo)
        piL = prcon.NmetalSampleProcessMain_GetList2(Nothing, PM_M_Code.EditValue)

        ds.Tables("ProductType").Clear()
        Dim i As Integer
        Dim row As DataRow
        For i = 0 To piL.Count - 1
            row = ds.Tables("ProductType").NewRow
            row("M_Name") = piL(i).Type3ID
            ds.Tables("ProductType").Rows.Add(row)
        Next
        ds.Tables("Material").Clear()
    End Sub
    ''' <summary>
    ''' 查找
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        If PM_M_Code.EditValue = "" Then
            MsgBox("產品編號不能為空!")
            Exit Sub
        End If
        If txt_gluType.EditValue = "" Then
            MsgBox("類型不能為空!")
            Exit Sub
        End If
        Dim pc As New NmetalSampleProcessControl
        Dim pci As List(Of NmetalSampleProcessInfo)
        pci = pc.NmetalSampleProcessMain_GetList(Nothing, PM_M_Code.EditValue, cbType.EditValue, txt_gluType.EditValue, Nothing, "True", Nothing)
        Try
            If pci.Count = 0 Then
                ds.Tables("Material").Clear()
                Exit Sub
            End If
            ds.Tables("Material").Clear()
            Dim i As Integer
            For i = 0 To pci.Count - 1
                Dim pdi As List(Of NmetalSampleWareInventoryInfo)
                Dim pdc As New NmetalSampleWareInventoryControler
                pdi = pdc.NmetalSampleWareInventory_Getlist(Nothing, Nothing, pci(i).PS_NO, Nothing, Nothing, DepNameID)
                If pdi.Count > 0 Then
                    Dim row As DataRow
                    row = ds.Tables("Material").NewRow

                    row("PS_NO") = pci(i).PS_NO
                    row("PS_Name") = pci(i).PS_Name

                    row("SWI_Weight") = pdi(0).SWI_Weight
                    row("GoIn") = False
                    '2014-07-30   Mark  新增数量
                    row("SWI_Qty") = pdi(0).SWI_Qty

                    ds.Tables("Material").Rows.Add(row)
                End If


            Next
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' 關閉窗體
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 保存按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If ds.Tables("Material").Rows.Count <= 0 Then
            MsgBox("子表无数据，请添加数据!", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
        GridView1.Columns("GoIn").OptionsColumn.AllowEdit = True
        tempValue = ""
        Dim i, n As Integer
        n = 0
        For i = 0 To ds.Tables("Material").Rows.Count - 1

            If ds.Tables("Material").Rows(i)("GoIn") = True Then
                If n = 0 Then
                    tempValue = ds.Tables("Material").Rows(i)("PS_NO")
                    n = n + 1
                Else
                    tempValue = tempValue & "," & ds.Tables("Material").Rows(i)("PS_NO")
                    n = n + 1
                End If

            End If
        Next
        tempCode = tempValue
        tempValue = ""

        If tempCode = "" Then
            MsgBox("未选择数据，无法加入到产品信息表中请重新选择或取消！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If


        Me.Close()
    End Sub
    ''' <summary>
    ''' 加入按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemCheckEdit1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepositoryItemCheckEdit1.EditValueChanged

        If sender.checked = False Then
            If sender.checked = True Then
                Dim i%
                For i = 0 To GridView1.RowCount - 1
                    'If i <> GridView1.FocusedRowHandle Then
                    If GridView1.GetRowCellValue(i, "GoIn") = True Then
                        MsgBox("只能選擇一個工序！", 64, "提示")
                        sender.checked = False
                        Exit Sub
                        'End If
                    End If
                Next
            End If
        End If
       
    End Sub
    ''' <summary>
    ''' 點擊類型變更
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txt_gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_gluType.EditValueChanged
        If cbType.EditValue = "" Then
            Exit Sub
        End If
        If PM_M_Code.EditValue = "" Then
            Exit Sub
        End If
        cmdSelect_Click(Nothing, Nothing)
    End Sub
End Class