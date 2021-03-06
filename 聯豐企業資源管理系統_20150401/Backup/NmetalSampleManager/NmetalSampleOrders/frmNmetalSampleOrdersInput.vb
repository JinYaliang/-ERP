Imports LFERP.Library.NmetalSampleManager.NmetalSampleCollection
Imports LFERP.Library.KnifeWareInventoryCheck

Public Class frmNmetalSampleOrdersInput
    Private sclcom As New NmetalSampleCollectionControler
    Dim objinfo As New NmetalSampleCollectionInfo
#Region "属性"
    Private _EditItem As String '属性栏位
    Private _EditValue As String
    Private _SS_Edition As String
    Private _SO_ID As String
    Private _SO_Type As String
    Private _SS_Qty As Integer
    Private ds As New DataSet
    Property EditValue() As String '属性
        Get
            Return _EditValue
        End Get
        Set(ByVal value As String)
            _EditValue = value
        End Set
    End Property

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property SS_Edition() As String '版本号
        Get
            Return _SS_Edition
        End Get
        Set(ByVal value As String)
            _SS_Edition = value
        End Set
    End Property

    Property SO_ID() As String '订单编号
        Get
            Return _SO_ID
        End Get
        Set(ByVal value As String)
            _SO_ID = value
        End Set
    End Property
    Property SO_Type() As String '寄送单号
        Get
            Return _SO_Type
        End Get
        Set(ByVal value As String)
            _SO_Type = value
        End Set
    End Property
    Property SS_Qty() As Integer  '寄送单号
        Get
            Return _SS_Qty
        End Get
        Set(ByVal value As Integer)
            _SS_Qty = value
        End Set
    End Property
#End Region
    ''' <summary>
    ''' 窗體加載事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNmetalSampleOrdersInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        txt_PM_M_Code.Text = EditValue      '產品編號
        txt_SO_SampleID.Text = SO_ID      '板單號



    End Sub

#Region "方法"
    Sub CreateTable()
        With ds.Tables.Add("SampleWareA")
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("Code_Qty", GetType(String))
            .Columns.Add("Remark", GetType(String))

            .Columns.Add("IWeight", GetType(Decimal))
            .Columns.Add("RWeight", GetType(Decimal))
            .Columns.Add("TWeight", GetType(Decimal))

            .Columns.Add("SO_Type", GetType(String))

        End With
        Grid1.DataSource = ds.Tables("SampleWareA")
    End Sub
#End Region
    ''' <summary>
    ''' 確認按鈕事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Savebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Savebutton.Click
        If ds.Tables("SampleWareA").Rows.Count <= 0 Then
            MsgBox("表中無數據，無法保存！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
        SaveData()
    End Sub
    ''' <summary>
    ''' 判斷是否為空
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckDataEmpty() As Boolean
        CheckDataEmpty = True
        If txtCode_ID.Text = String.Empty Then
            CheckDataEmpty = False
            MsgBox("條碼編號不能為空，請輸入！", MsgBoxStyle.OkOnly, "提示")
            txtCode_ID.Focus()
            Exit Function
        End If
        If txtWeight.Text = String.Empty Then
            CheckDataEmpty = False
            MsgBox("來料重量不能為空,請輸入！", MsgBoxStyle.OkOnly, "提示")
            txtWeight.Focus()
            Exit Function
        End If
        If txtWeight.Text <= 0 Then
            CheckDataEmpty = False
            MsgBox("來料重量需要大於0,請重新輸入！", MsgBoxStyle.OkOnly, "提示")
            txtWeight.Focus()
            Exit Function
        End If
        If sclcom.NmetalSampleCollection_GetID(txtCode_ID.Text) = True Then
            txtCode_ID.Focus()
            MsgBox("此編號採集表中已存在!", MsgBoxStyle.OkOnly, "提示")
            CheckDataEmpty = False
            Exit Function
        End If
        Dim I As Integer
        For I = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            If ds.Tables("SampleWareA").Rows(I)("Code_ID").ToString() = txtCode_ID.Text Then
                Grid1.Focus()
                GridView2.FocusedRowHandle = I
                txtCode_ID.Text = String.Empty
                txtCode_ID.Focus()
                MsgBox("表中存在相同的條碼編號'" & ds.Tables("SampleWareA").Rows(I)("Code_ID") & "'!", MsgBoxStyle.OkOnly, "提示")
                CheckDataEmpty = False
                Exit Function
            End If
        Next
    End Function
    Sub SaveData()
        Dim i As Integer

        For i = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            Dim strCode_ID As String
            strCode_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")

            If sclcom.NmetalSampleCollection_GetID(strCode_ID) = True Then
                MsgBox("編號 & ‘" & strCode_ID & "’ &, 在採集表中已存在!", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            End If
        Next
        '-------------------------------------------------------------------------------------
        For i = 0 To ds.Tables("SampleWareA").Rows.Count - 1
            objinfo.PM_M_Code = txt_PM_M_Code.Text
            objinfo.SO_SampleID = txt_SO_SampleID.Text
            objinfo.Code_ID = ds.Tables("SampleWareA").Rows(i)("Code_ID")
            objinfo.Qty = ds.Tables("SampleWareA").Rows(i)("Code_Qty")
            objinfo.IWeight = ds.Tables("SampleWareA").Rows(i)("IWeight")
            objinfo.AddUserID = InUserID
            objinfo.AddDate = Format(Now, "yyyy/MM/dd")
            objinfo.SO_ID = SO_ID
            objinfo.SS_Edition = SS_Edition
            objinfo.BarcodeType = "[a].來料編號"


            If sclcom.NmetalSampleCollection_Add(objinfo) = False Then
                MsgBox("保存失敗!", MsgBoxStyle.OkOnly, "提示")
                Exit Sub
            Else
            End If
        Next

        MsgBox("保存成功!", MsgBoxStyle.OkOnly, "提示")
    End Sub



    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 載入按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoad.Click
        If CheckDataEmpty() = False Then
            Exit Sub
        End If
        If txtCode_ID.Text <> String.Empty Then
            Dim row As DataRow = ds.Tables("SampleWareA").NewRow
            row("Code_ID") = Trim(txtCode_ID.Text)
            row("Code_Qty") = 1
            row("IWeight") = txtWeight.Text

            ds.Tables("SampleWareA").Rows.Add(row)
        End If
        txtCode_ID.Text = String.Empty
        txtWeight.Text = String.Empty
        txtCode_ID.Focus()
    End Sub
    ''' <summary>
    ''' 刪除條碼按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdCODEDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCODEDel.Click
        If GridView2.RowCount > 0 Then
            Dim deltemp As String = ""
            deltemp = ds.Tables("SampleWareA").Rows(GridView2.FocusedRowHandle)("Code_ID").ToString
            ds.Tables("SampleWareA").Rows.RemoveAt(GridView2.FocusedRowHandle)
        End If
    End Sub

    ''' <summary>
    ''' 條碼編號回車事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCode_ID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode_ID.KeyDown
        If e.KeyCode = Keys.Enter Then
            ButtonLoad_Click(sender, e)
        Else
            Exit Sub
        End If
    End Sub
    ''' <summary>
    ''' 來料重量回車事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtWeight_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWeight.KeyDown
        If e.KeyCode = Keys.Enter Then
            ButtonLoad_Click(sender, e)
        Else
            Exit Sub
        End If
    End Sub

End Class