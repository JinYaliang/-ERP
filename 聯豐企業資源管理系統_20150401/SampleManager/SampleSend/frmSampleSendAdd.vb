Imports LFERP.DataSetting
Imports LFERP.Library.SampleManager.SamplePace
Imports LFERP.Library.SampleManager.SamplePlan
Imports LFERP.Library.SampleManager.SampleProcess
Imports LFERP.Library.SampleManager.SampleSend
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.SampleManager.SampleOrdersSub
Imports LFERP.Library.SampleManager.SampleCollection
Imports LFERP.Library.SampleManager.SamplePacking

Public Class frmSampleSendAdd
#Region "属性"
    Public ds As New DataSet
    Dim soscon As New SampleOrdersSubControler
    Dim somcon As New SampleOrdersMainControler
    Dim sscon As New SampleSendControler
    Dim ssccon As New SampleSendCodeControler
    Dim sclcom As New SampleCollectionControler
    Dim spkcon As New SamplePackingController
    Dim mtd As New CusterControler

    Private boolSE_Check As Boolean '審核狀態有沒有改變

    Private _EditType As String '属性栏位
    Private _AutoID As String
    Property AutoID() As String '属性
        Get
            Return _AutoID
        End Get
        Set(ByVal value As String)
            _AutoID = value
        End Set
    End Property

    Property EditItem() As String '属性
        Get
            Return _EditType
        End Get
        Set(ByVal value As String)
            _EditType = value
        End Set
    End Property
#End Region

#Region "载入窗体"
    Private Sub frmSamplePace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        Select Case EditItem
            Case EditEnumType.ADD
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.ADD)
                dateAddDate.EditValue = Format(Now, "yyyy/MM/dd")
                DateSendDate.EditValue = Format(Now, "yyyy/MM/dd")
                XtraTabPage2.PageVisible = False
                LoadData(txtSP_ID.Text)
            Case EditEnumType.EDIT
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                XtraTabPage2.PageVisible = False
                LoadData(txtSP_ID.Text)
                gluCuster.Enabled = False
                txtPK_CodeID.Enabled = False
            Case EditEnumType.CHECK
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                LoadData(txtSP_ID.Text)
                gridSampleSend.ContextMenuStrip = Nothing
                Grid1.ContextMenuStrip = Nothing

                XtraTabPage2.PageVisible = True
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                txtM_Code.Enabled = False
                GridEnable()
                gluCuster.Enabled = False
                txtPK_CodeID.Enabled = False
            Case EditEnumType.INCHECK
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.INCHECK)
                LoadData(txtSP_ID.Text)
                gridSampleSend.ContextMenuStrip = Nothing
                Grid1.ContextMenuStrip = Nothing
                XtraTabPage2.Text = "复审"
                Label13.Text = "复审(&C)："
                Label11.Text = "复审人:"
                Label10.Text = "复审日期:"
                XtraTabPage2.PageVisible = True
                XtraTabControl1.SelectedTabPage = XtraTabPage2
                txtM_Code.Enabled = False
                GridEnable()
                gluCuster.Enabled = False
                txtPK_CodeID.Enabled = False
            Case EditEnumType.VIEW
                Me.lblTitle.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                LoadData(txtSP_ID.Text)
                gridSampleSend.ContextMenuStrip = Nothing
                Grid1.ContextMenuStrip = Nothing
                txtSP_ID.Enabled = False
                gluCuster.Enabled = False
                dateAddDate.Enabled = False
                DateSendDate.Enabled = False
                XtraTabPage2.PageVisible = True
                txtM_Code.Enabled = False
                txt_SP_ExpCompany.Enabled = False
                txt_SP_ExpDeliveryID.Enabled = False
                GridEnable()
                txtPK_CodeID.Enabled = False
        End Select
        Me.Text = lblTitle.Text
        '控件載入数据2
        gluCuster.Properties.DisplayMember = "C_ChsName"
        gluCuster.Properties.ValueMember = "C_CusterID"
        gluCuster.Properties.DataSource = mtd.GetCusterList(Nothing, Nothing, Nothing)
    End Sub

    Sub GridEnable()
        GridView1.OptionsBehavior.AutoSelectAllInEditor = False
        GridView1.OptionsBehavior.Editable = False
        GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

        GridView9.OptionsBehavior.AutoSelectAllInEditor = False
        GridView9.OptionsBehavior.Editable = False
        GridView9.OptionsSelection.EnableAppearanceFocusedCell = False
    End Sub
#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SampleSend")
            .Columns.Add("SP_ID", GetType(String))
            .Columns.Add("SO_ID", GetType(String))
            .Columns.Add("SS_Edition", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("SO_SampleID", GetType(String))
            .Columns.Add("SP_SendDate", GetType(Date))
            .Columns.Add("SO_OrderQty", GetType(Integer))
            .Columns.Add("SP_Qty", GetType(Integer))
            .Columns.Add("Qty", GetType(Integer))
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("AutoItem", GetType(Decimal))
            .Columns.Add("SP_Remark", GetType(String))
            .PrimaryKey = New DataColumn() {.Columns("SO_ID"), .Columns("SS_Edition"), .Columns("PM_M_Code")} '主鍵
        End With

        gridSampleSend.DataSource = ds.Tables("SampleSend")
        With ds.Tables.Add("DelSampleSend")
            .Columns.Add("AutoID", GetType(String))
        End With

        '---寄送條碼
        With ds.Tables.Add("SampleSendCode")
            .Columns.Add("SendID", GetType(Decimal))
            .Columns("SendID").AutoIncrement = True
            .Columns("SendID").AutoIncrementSeed = 1
            .Columns("SendID").AutoIncrementStep = 1
            .Columns("SendID").ReadOnly = True
            .PrimaryKey = New DataColumn() {.Columns("SendID")} '自動ID號
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("SO_ID", GetType(String))
            .Columns.Add("SS_Edition", GetType(String))
            .Columns.Add("PM_M_Code", GetType(String))
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("Code_Qty", GetType(Integer))
            .Columns.Add("SP_ID", GetType(String))
            .Columns.Add("AddUserID", GetType(String))
            .Columns.Add("AddDate", GetType(String))
            .Columns.Add("CodeType", GetType(String))
        End With

        Grid1.DataSource = ds.Tables("SampleSendCode")
        With ds.Tables.Add("DelSampleSendCode")
            .Columns.Add("AutoID", GetType(String))
        End With
        '臨時存放
        With ds.Tables.Add("SampleSendID")
            .Columns.Add("SendID", GetType(Decimal))
        End With

    End Sub
#End Region

#Region "載入数据"
    ''' <summary>
    ''' 載入数据
    ''' </summary>
    ''' <param name="strSP_ID"></param>
    ''' <remarks></remarks>
    Sub LoadData(ByVal strSP_ID As String) '返回数据
        Dim som As New List(Of SampleSendInfo)
        som = sscon.SampleSend_Getlist(strSP_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing)
        If som.Count = 0 Then
            Exit Sub
        Else
            txtSP_ID.Text = som(0).SP_ID
            gluCuster.Text = som(0).SP_CusterID
            dateAddDate.Text = som(0).SP_AddDate
            DateSendDate.Text = som(0).SP_SendDate
            txtPK_CodeID.Text = som(0).PK_Code_ID

            Select Case EditItem
                Case EditEnumType.CHECK
                    CheckA.Checked = som(0).SP_Check
                    boolSE_Check = som(0).SP_Check
                    lblAction.Text = som(0).SP_CheckUserName
                    lblActionDate.Text = som(0).SP_CheckDate
                    txt_checkremark.Text = som(0).SP_CheckRemark
                Case EditEnumType.INCHECK
                    CheckA.Checked = som(0).SP_InCheck
                    boolSE_Check = som(0).SP_InCheck
                    lblAction.Text = som(0).SP_InCheckUserName
                    lblActionDate.Text = som(0).SP_InCheckDate
                    txt_checkremark.Text = som(0).SP_InCheckRemark
            End Select

            txt_SP_ExpCompany.Text = som(0).SP_ExpCompany
            txt_SP_ExpDeliveryID.Text = som(0).SP_ExpDeliveryID
            '.....................................................
            Dim i As Integer
            ds.Tables("SampleSend").Clear()
            For i = 0 To som.Count - 1
                Dim row As DataRow
                row = ds.Tables("SampleSend").NewRow
                row("SP_ID") = som(i).SP_ID
                row("SO_ID") = som(i).SO_ID
                row("SS_Edition") = som(i).SS_Edition
                row("PM_M_Code") = som(i).PM_M_Code
                row("M_Code") = som(i).M_Code
                row("M_Name") = som(i).M_Name
                row("SP_SendDate") = som(i).SP_SendDate
                row("SP_Qty") = som(i).SP_Qty
                row("SP_Remark") = som(i).SP_Remark
                row("SO_SampleID") = som(i).SO_SampleID

                '載入订单数量
                Dim somDate As New List(Of SampleOrdersMainInfo)
                somDate = somcon.SampleOrdersMain_GetList(som(i).SO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If somDate.Count > 0 Then
                    row("SO_OrderQty") = somDate(0).SO_OrderQty
                Else
                    row("SO_OrderQty") = 0
                End If

                Dim somSub As New List(Of SampleOrdersSubInfo)
                somSub = soscon.SampleOrdersSub_GetList(som(i).SO_ID, som(i).SS_Edition)
                If somSub.Count > 0 Then
                    row("AutoItem") = somSub(0).AutoID
                Else
                    row("AutoItem") = 0
                End If

                row("AutoID") = som(i).AutoID
                ds.Tables("SampleSend").Rows.Add(row)
            Next

            '-----------------------------------------条码采集数据
            Dim somlist As New List(Of SampleSendCodeInfo)
            somlist = ssccon.SampleSendCode_Getlist(strSP_ID, Nothing, Nothing, Nothing, Nothing, Nothing)
            If somlist.Count = 0 Then
                Exit Sub
            Else
                ds.Tables("SampleSendCode").Clear()
                For i = 0 To somlist.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("SampleSendCode").NewRow

                    row("AutoID") = somlist(i).AutoID
                    row("SO_ID") = somlist(i).SO_ID
                    row("SS_Edition") = somlist(i).SS_Edition
                    row("PM_M_Code") = somlist(i).PM_M_Code
                    row("Code_ID") = somlist(i).Code_ID
                    row("Code_Qty") = somlist(i).Code_Qty
                    row("SP_ID") = somlist(i).SP_ID
                    row("AddUserID") = somlist(i).AddUserID
                    row("CodeType") = somlist(i).CodeType
                    row("AddDate") = Format(CDate(somlist(i).AddDate), "yyyy-MM-dd HH:mm:ss")

                    ds.Tables("SampleSendCode").Rows.Add(row)
                Next
            End If


        End If
    End Sub
#End Region

#Region "子表刪除事件"

    ''' <summary>
    ''' 刪除事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsmDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmDelete.Click
        If GridView1.RowCount = 0 Then Exit Sub

        Dim strSO_ID As String = ds.Tables("SampleSend").Rows(GridView1.FocusedRowHandle)("SO_ID")
        Dim strSS_Edition As String = ds.Tables("SampleSend").Rows(GridView1.FocusedRowHandle)("SS_Edition")
        Dim strPM_M_Code As String = ds.Tables("SampleSend").Rows(GridView1.FocusedRowHandle)("PM_M_Code")

        Dim DelTemp As String
        DelTemp = GridView1.GetRowCellDisplayText(GridView1.GetSelectedRows(0), "AutoID")
        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("DelSampleSend").NewRow
            row("AutoID") = ds.Tables("SampleSend").Rows(GridView1.FocusedRowHandle)("AutoID")
            ds.Tables("DelSampleSend").Rows.Add(row)
        End If
        ds.Tables("SampleSend").Rows.RemoveAt(GridView1.GetSelectedRows(0))

        '刪除子檔數據條碼同樣刪除-----------------------------------------------------
        ds.Tables("SampleSendID").Clear()
        Dim i As Integer
        For i = 0 To ds.Tables("SampleSendCode").Rows.Count - 1
            Dim chrSO_ID As String = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("SO_ID")), Nothing, ds.Tables("SampleSendCode").Rows(i)("SO_ID"))
            Dim chrSS_Edition As String = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("SS_Edition")), Nothing, ds.Tables("SampleSendCode").Rows(i)("SS_Edition"))
            Dim chrPM_M_Code As String = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("PM_M_Code")), Nothing, ds.Tables("SampleSendCode").Rows(i)("PM_M_Code"))
            Dim chrAutoID As Decimal = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("AutoID")), 0, ds.Tables("SampleSendCode").Rows(i)("AutoID"))

            If strSO_ID = chrSO_ID And chrSS_Edition = strSS_Edition And strPM_M_Code = chrPM_M_Code Then
                If chrAutoID > 0 Then
                    Dim row As DataRow = ds.Tables("DelSampleSendCode").NewRow
                    row("AutoID") = ds.Tables("SampleSendCode").Rows(i)("AutoID")
                    ds.Tables("DelSampleSendCode").Rows.Add(row)
                End If
                '----------------------------------
                Dim rowa As DataRow
                rowa = ds.Tables("SampleSendID").NewRow
                rowa("SendID") = ds.Tables("SampleSendCode").Rows(i)("SendID")
                ds.Tables("SampleSendID").Rows.Add(rowa)
            End If
        Next

        '刪除行
        For i = 0 To ds.Tables("SampleSendID").Rows.Count - 1
            Dim strSendID As String = ds.Tables("SampleSendID").Rows(i)("SendID").ToString()
            Dim rowb As DataRow = ds.Tables("SampleSendCode").Rows.Find(strSendID)
            ds.Tables("SampleSendCode").Rows.Remove(rowb)
        Next
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If GridView9.RowCount = 0 Then Exit Sub

        Dim DelTemp As String
        Dim strSO_ID As String = ds.Tables("SampleSendCode").Rows(GridView9.FocusedRowHandle)("SO_ID")
        Dim strSS_Edition As String = ds.Tables("SampleSendCode").Rows(GridView9.FocusedRowHandle)("SS_Edition")
        Dim strPM_M_Code As String = ds.Tables("SampleSendCode").Rows(GridView9.FocusedRowHandle)("PM_M_Code")

        DelTemp = GridView9.GetRowCellDisplayText(GridView9.GetSelectedRows(0), "AutoID")
        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("DelSampleSendCode").NewRow
            row("AutoID") = ds.Tables("SampleSendCode").Rows(GridView9.FocusedRowHandle)("AutoID")
            ds.Tables("DelSampleSendCode").Rows.Add(row)
        End If
        ds.Tables("SampleSendCode").Rows.RemoveAt(GridView9.GetSelectedRows(0))

        '刪除條碼后寄送數量也刪除-----------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("SampleSend").Rows.Count - 1
            Dim chrSO_ID As String = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SO_ID")), Nothing, ds.Tables("SampleSend").Rows(i)("SO_ID"))
            Dim chrSS_Edition As String = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SS_Edition")), Nothing, ds.Tables("SampleSend").Rows(i)("SS_Edition"))
            Dim chrPM_M_Code As String = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("PM_M_Code")), Nothing, ds.Tables("SampleSend").Rows(i)("PM_M_Code"))
            Dim chrAutoID As Decimal = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("AutoID")), 0, ds.Tables("SampleSend").Rows(i)("AutoID"))

            If strSO_ID = chrSO_ID And chrSS_Edition = strSS_Edition And strPM_M_Code = chrPM_M_Code Then
                ds.Tables("SampleSend").Rows(i)("SP_Qty") = ds.Tables("SampleSend").Rows(i)("SP_Qty") - 1
                If ds.Tables("SampleSend").Rows(i)("SP_Qty") = 0 Then
                    If chrAutoID > 0 Then
                        Dim row As DataRow = ds.Tables("DelSampleSend").NewRow
                        row("AutoID") = ds.Tables("SampleSend").Rows(i)("AutoID")
                        ds.Tables("DelSampleSend").Rows.Add(row)
                    End If
                    ds.Tables("SampleSend").Rows.RemoveAt(i)
                    i = i + 1
                End If
            End If
        Next
    End Sub
    '“點查詢”
    Private Sub GridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Dim strSO_ID As String = GridView1.GetFocusedRowCellValue("SO_ID").ToString()
        Me.GridView9.ActiveFilterString = "[SO_ID] = '" & strSO_ID & "'"
    End Sub

#End Region

#Region "按键事件"
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 保存事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If DataCheckEmpty() = 0 Then
            Exit Sub
        End If
        Select Case EditItem
            Case EditEnumType.ADD
                DataNew()
            Case EditEnumType.EDIT
                DataEdit()
            Case EditEnumType.CHECK
                UpdateCheck()
            Case EditEnumType.INCHECK
                UpdateInCheck()
        End Select
    End Sub
#End Region

#Region "审核程序"
    ''' <summary>
    ''' 审核程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateInCheck()
        '1.是否修改審核狀態
        If CheckA.Checked = boolSE_Check Then
            MsgBox("审核状态没有改变，提示！")
            Exit Sub
        End If

        Dim SSI As New SampleSendInfo
        SSI.SP_ID = txtSP_ID.Text
        SSI.SP_InCheck = CheckA.Checked
        SSI.SP_InCheckDate = Format(Now, "yyyy/MM/dd").ToString
        SSI.SP_InCheckUserID = InUserID
        SSI.SP_InCheckRemark = String.Empty
        If sscon.SampleSend_UpdateInCheck(SSI) = False Then
            MsgBox("复审失敗，请檢查原因！")
            Exit Sub
        End If

        If CheckA.Checked Then
            MsgBox("复审成功", 60, "提示")
        Else
            MsgBox("取消复审成功", 60, "提示")
        End If


        ''2.改變采集表里面的狀態--------------------------------------------------
        'Dim i As Integer
        'For i = 0 To ds.Tables("SampleSendCode").Rows.Count - 1
        '    Dim strCode_ID As String = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("Code_ID")), Nothing, ds.Tables("SampleSendCode").Rows(i)("Code_ID"))
        '    '-----------------------------
        '    '2.1改變采集表里面的狀態--寄送單號
        '    If CheckA.Checked Then
        '        '2.1.1修改状态
        '        If sclcom.SampleCollection_UpdateA(strCode_ID, "C") = False Then
        '            MessageBox.Show("修改条码状态错误!", "提示")
        '            Exit Sub
        '        End If
        '        '2.1.2修改客戶條碼寄送單號
        '        If sclcom.SampleCollection_UpdateB(strCode_ID, txtSP_ID.Text) = False Then
        '            MessageBox.Show("修改寄送單號错误!", "提示")
        '            Exit Sub
        '        End If
        '        '2.1.3修改內部條碼寄送單號
        '        Dim scmlistm As New List(Of SampleCollectionInfo)
        '        scmlistm = sclcom.SampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, strCode_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '        If scmlistm.Count > 0 Then
        '            If scmlistm(0).Code_ID <> String.Empty Then
        '                If sclcom.SampleCollection_UpdateB(scmlistm(0).Code_ID, txtSP_ID.Text) = False Then
        '                    MessageBox.Show("修改寄送單號错误!", "提示")
        '                    Exit Sub
        '                End If
        '            End If
        '        End If
        '    Else
        '        '2.2.1修改状态
        '        If sclcom.SampleCollection_UpdateA(strCode_ID, "M") = False Then
        '            MessageBox.Show("修改条码状态错误!", "提示")
        '            Exit Sub
        '        End If
        '        '2.2.2修改客戶條碼寄送單號
        '        If sclcom.SampleCollection_UpdateB(strCode_ID, "") = False Then
        '            MessageBox.Show("修改寄送單號错误!", "提示")
        '            Exit Sub
        '        End If
        '        '2.2.3修改內部條碼寄送單號
        '        Dim scmlistm As New List(Of SampleCollectionInfo)
        '        scmlistm = sclcom.SampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, strCode_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '        If scmlistm.Count > 0 Then
        '            If scmlistm(0).Code_ID <> String.Empty Then
        '                If sclcom.SampleCollection_UpdateB(scmlistm(0).Code_ID, "") = False Then
        '                    MessageBox.Show("修改寄送單號错误!", "提示")
        '                    Exit Sub
        '                End If
        '            End If
        '        End If
        '    End If
        'Next

        ''3.改變訂單未交數量--------------------------------------------------
        'For i = 0 To ds.Tables("SampleSend").Rows.Count - 1
        '    Dim ssinfo As New SampleSendInfo
        '    ssinfo.SO_ID = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SO_ID")), Nothing, ds.Tables("SampleSend").Rows(i)("SO_ID"))
        '    ssinfo.SS_Edition = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SS_Edition")), Nothing, ds.Tables("SampleSend").Rows(i)("SS_Edition"))
        '    ssinfo.PM_M_Code = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("PM_M_Code")), Nothing, ds.Tables("SampleSend").Rows(i)("PM_M_Code"))
        '    ssinfo.SP_Qty = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SP_Qty")), 0, ds.Tables("SampleSend").Rows(i)("SP_Qty"))
        '    '-----------------------------
        '    '3.1.改變采集表里面的狀態
        '    If CheckA.Checked Then
        '        '3.1.1修改確認未交數量
        '        If sscon.SampleSend_UpdateNoSendQty(ssinfo) = False Then
        '            MessageBox.Show("修改未交數量错误!", "提示")
        '            Exit Sub
        '        End If
        '    Else
        '        '3.1.2修改取消未交數量-
        '        ssinfo.SP_Qty = -ssinfo.SP_Qty
        '        If sscon.SampleSend_UpdateNoSendQty(ssinfo) = False Then
        '            MessageBox.Show("修改未交數量错误!", "提示")
        '            Exit Sub
        '        End If
        '    End If
        'Next

        Me.Close()

    End Sub
#End Region

#Region "审核程序"
    ''' <summary>
    ''' 审核程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub UpdateCheck()
        '1.是否修改審核狀態
        If CheckA.Checked = boolSE_Check Then
            MsgBox("审核状态没有改变，提示！")
            Exit Sub
        End If

        Dim SSI As New SampleSendInfo
        SSI.SP_ID = txtSP_ID.Text
        SSI.SP_Check = CheckA.Checked
        SSI.SP_CheckDate = Format(Now, "yyyy/MM/dd").ToString
        SSI.SP_CheckUserID = InUserID
        SSI.SP_CheckRemark = String.Empty
        If sscon.SampleSend_UpdateCheck(SSI) = False Then
            MsgBox("审核失敗，请檢查原因！")
            Exit Sub
        End If

        If CheckA.Checked Then
            MsgBox("审核成功", 60, "提示")
        Else
            MsgBox("取消审核成功", 60, "提示")
        End If

        '2.改變采集表里面的狀態--------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("SampleSendCode").Rows.Count - 1
            Dim strCode_ID As String = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("Code_ID")), Nothing, ds.Tables("SampleSendCode").Rows(i)("Code_ID"))
            '-----------------------------
            '2.1改變采集表里面的狀態--寄送單號
            If CheckA.Checked Then
                '2.1.1修改状态
                If sclcom.SampleCollection_UpdateA(strCode_ID, "C") = False Then
                    MessageBox.Show("修改条码状态错误!", "提示")
                    Exit Sub
                End If
                '2.1.2修改客戶條碼寄送單號
                If sclcom.SampleCollection_UpdateB(strCode_ID, txtSP_ID.Text) = False Then
                    MessageBox.Show("修改寄送單號错误!", "提示")
                    Exit Sub
                End If
                '2.1.3修改內部條碼寄送單號
                Dim scmlistm As New List(Of SampleCollectionInfo)
                scmlistm = sclcom.SampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, strCode_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If scmlistm.Count > 0 Then
                    If scmlistm(0).Code_ID <> String.Empty Then
                        If sclcom.SampleCollection_UpdateB(scmlistm(0).Code_ID, txtSP_ID.Text) = False Then
                            MessageBox.Show("修改寄送單號错误!", "提示")
                            Exit Sub
                        End If
                    End If
                End If
            Else

                '2.2.1修改状态
                If sclcom.SampleCollection_UpdateA(strCode_ID, "M") = False Then
                    MessageBox.Show("修改条码状态错误!", "提示")
                    Exit Sub
                End If
                '2.2.2修改客戶條碼寄送單號
                If sclcom.SampleCollection_UpdateB(strCode_ID, "") = False Then
                    MessageBox.Show("修改寄送單號错误!", "提示")
                    Exit Sub
                End If
                '2.2.3修改內部條碼寄送單號
                Dim scmlistm As New List(Of SampleCollectionInfo)
                scmlistm = sclcom.SampleCollection_Getlist(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, strCode_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If scmlistm.Count > 0 Then
                    If scmlistm(0).Code_ID <> String.Empty Then
                        If sclcom.SampleCollection_UpdateB(scmlistm(0).Code_ID, "") = False Then
                            MessageBox.Show("修改寄送單號错误!", "提示")
                            Exit Sub
                        End If
                    End If
                End If

            End If
        Next

        '3.改變訂單未交數量--------------------------------------------------
        For i = 0 To ds.Tables("SampleSend").Rows.Count - 1
            Dim ssinfo As New SampleSendInfo
            ssinfo.SO_ID = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SO_ID")), Nothing, ds.Tables("SampleSend").Rows(i)("SO_ID"))
            ssinfo.SS_Edition = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SS_Edition")), Nothing, ds.Tables("SampleSend").Rows(i)("SS_Edition"))
            ssinfo.PM_M_Code = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("PM_M_Code")), Nothing, ds.Tables("SampleSend").Rows(i)("PM_M_Code"))
            ssinfo.SP_Qty = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SP_Qty")), 0, ds.Tables("SampleSend").Rows(i)("SP_Qty"))
            '-----------------------------
            '3.1.改變采集表里面的狀態
            If CheckA.Checked Then
                '3.1.1修改確認未交數量
                If sscon.SampleSend_UpdateNoSendQty(ssinfo) = False Then
                    MessageBox.Show("修改未交數量错误!", "提示")
                    Exit Sub
                End If
            Else
                '3.1.2修改取消未交數量-
                ssinfo.SP_Qty = -ssinfo.SP_Qty
                If sscon.SampleSend_UpdateNoSendQty(ssinfo) = False Then
                    MessageBox.Show("修改未交數量错误!", "提示")
                    Exit Sub
                End If
            End If
        Next

        '4.裝箱單使用一次以后不可以再使用
        Dim strPK_CodeID As String = txtPK_CodeID.Text
        If strPK_CodeID <> String.Empty Then
            Dim pkinfo As New SamplePackingInfo
            pkinfo.BitNeed = True
            pkinfo.Code_ID = strPK_CodeID
            If spkcon.SamplePacking_InUpdateCheck(pkinfo) = False Then
                MessageBox.Show("修改裝箱状态错误!", "提示")
                Exit Sub
            End If
        End If

        Me.Close()
    End Sub
#End Region

#Region "新增修改程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataNew() '新增
        Dim SSINFO As New SampleSendInfo
        '1.主檔控件' ------------------------------------------------------------------------
        Dim strSP_ID As String = GetSP_ID()
        SSINFO.SP_ID = strSP_ID
        SSINFO.SP_SendDate = DateSendDate.Text
        SSINFO.SP_AddDate = dateAddDate.Text
        SSINFO.SP_AddUserID = InUserID
        SSINFO.SP_CusterID = gluCuster.EditValue
        SSINFO.SP_ExpCompany = txt_SP_ExpCompany.Text
        SSINFO.SP_ExpDeliveryID = txt_SP_ExpDeliveryID.Text
        SSINFO.PK_Code_ID = txtPK_CodeID.Text
        '2.子表數據新增' ------------------------------------------------------------------------
        Dim i As Integer
        For i = 0 To ds.Tables("SampleSend").Rows.Count - 1
            With ds.Tables("SampleSend")
                SSINFO.SO_ID = IIf(IsDBNull(.Rows(i)("SO_ID")), Nothing, .Rows(i)("SO_ID"))
                SSINFO.SS_Edition = IIf(IsDBNull(.Rows(i)("SS_Edition")), Nothing, .Rows(i)("SS_Edition"))
                SSINFO.PM_M_Code = IIf(IsDBNull(.Rows(i)("PM_M_Code")), Nothing, .Rows(i)("PM_M_Code"))
                SSINFO.M_Code = IIf(IsDBNull(.Rows(i)("M_Code")), Nothing, .Rows(i)("M_Code"))
                SSINFO.SP_Qty = IIf(IsDBNull(.Rows(i)("SP_Qty")), Nothing, .Rows(i)("SP_Qty"))
                SSINFO.SP_Remark = IIf(IsDBNull(.Rows(i)("SP_Remark")), Nothing, .Rows(i)("SP_Remark"))

                If IIf(IsDBNull(.Rows(i)("SP_Qty")), 0, .Rows(i)("SP_Qty")) > 0 Then
                    If sscon.SampleSend_Add(SSINFO) = False Then
                        MsgBox("添加失敗，请檢查原因！")
                        Exit Sub
                    Else
                        Dim SampleMain As New SampleOrdersMainControler
                        Dim SampleMainInfo As New SampleOrdersMainInfo
                        SampleMainInfo.SO_ID = IIf(IsDBNull(.Rows(i)("SO_ID")), Nothing, .Rows(i)("SO_ID"))
                        SampleMainInfo.SO_State = "G.样办寄送"
                        SampleMain.SampleOrdersMain_UpdateState(SampleMainInfo)
                    End If
                End If
            End With
        Next
        '3.條碼數擾新增' ------------------------------------------------------------------------
        For i = 0 To ds.Tables("SampleSendCode").Rows.Count - 1
            With ds.Tables("SampleSendCode")
                Dim sscinfo As New SampleSendCodeInfo

                sscinfo.SO_ID = IIf(IsDBNull(.Rows(i)("SO_ID")), Nothing, .Rows(i)("SO_ID"))
                sscinfo.SS_Edition = IIf(IsDBNull(.Rows(i)("SS_Edition")), Nothing, .Rows(i)("SS_Edition"))
                sscinfo.PM_M_Code = IIf(IsDBNull(.Rows(i)("PM_M_Code")), Nothing, .Rows(i)("PM_M_Code"))
                sscinfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                sscinfo.Code_Qty = IIf(IsDBNull(.Rows(i)("Code_Qty")), Nothing, .Rows(i)("Code_Qty"))
                sscinfo.CodeType = IIf(IsDBNull(.Rows(i)("CodeType")), Nothing, .Rows(i)("CodeType"))
                sscinfo.SP_ID = strSP_ID
                sscinfo.AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                sscinfo.AddUserID = InUserID

                If ssccon.SampleSendCode_Add(sscinfo) = False Then
                    MsgBox("添加失敗，请檢查原因！")
                    Exit Sub
                End If

            End With
        Next

        MsgBox("添加成功", 60, "提示")
        Me.Close()
    End Sub

    ''' <summary>
    '''修改
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '更新刪除列表記錄
        If ds.Tables("DelSampleSend").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSampleSend").Rows.Count - 1
                sscon.SampleSend_DeleteAutoID(ds.Tables("DelSampleSend").Rows(j)("AutoID")) '刪除当前选定的
            Next
        End If
        '更新刪除列表條碼記錄
        If ds.Tables("DelSampleSendCode").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSampleSendCode").Rows.Count - 1
                ssccon.SampleSendCode_Delete(Nothing, ds.Tables("DelSampleSendCode").Rows(j)("AutoID")) '刪除当前选定的
            Next
        End If

        '修改数据
        Dim SSINFO As New SampleSendInfo
        SSINFO.SP_ID = txtSP_ID.Text
        SSINFO.SP_SendDate = DateSendDate.Text
        SSINFO.SP_AddDate = dateAddDate.Text
        SSINFO.SP_AddUserID = InUserID
        SSINFO.SP_CusterID = gluCuster.EditValue
        SSINFO.SP_ExpCompany = txt_SP_ExpCompany.Text
        SSINFO.SP_ExpDeliveryID = txt_SP_ExpDeliveryID.Text
        SSINFO.PK_Code_ID = txtPK_CodeID.Text

        Dim i As Integer
        For i = 0 To ds.Tables("SampleSend").Rows.Count - 1
            With ds.Tables("SampleSend")
                SSINFO.SO_ID = IIf(IsDBNull(.Rows(i)("SO_ID")), Nothing, .Rows(i)("SO_ID"))
                SSINFO.SS_Edition = IIf(IsDBNull(.Rows(i)("SS_Edition")), Nothing, .Rows(i)("SS_Edition"))
                SSINFO.PM_M_Code = IIf(IsDBNull(.Rows(i)("PM_M_Code")), Nothing, .Rows(i)("PM_M_Code"))
                SSINFO.M_Code = IIf(IsDBNull(.Rows(i)("M_Code")), Nothing, .Rows(i)("M_Code"))
                SSINFO.SP_Qty = IIf(IsDBNull(.Rows(i)("SP_Qty")), Nothing, .Rows(i)("SP_Qty"))
                SSINFO.SP_Remark = IIf(IsDBNull(.Rows(i)("SP_Remark")), Nothing, .Rows(i)("SP_Remark"))
                SSINFO.AutoID = IIf(IsDBNull(.Rows(i)("AutoID")), Nothing, .Rows(i)("AutoID"))
                If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) > 0 Then
                    If IIf(IsDBNull(.Rows(i)("SP_Qty")), 0, .Rows(i)("SP_Qty")) > 0 Then
                        If sscon.SampleSend_Update(SSINFO) = False Then
                            MsgBox("修改失敗，请檢查原因！")
                            Exit Sub
                        End If
                    End If
                Else
                    If IIf(IsDBNull(.Rows(i)("SP_Qty")), 0, .Rows(i)("SP_Qty")) > 0 Then
                        If sscon.SampleSend_Add(SSINFO) = False Then
                            MsgBox("修改失敗，请檢查原因！")
                            Exit Sub
                        End If
                    End If
                End If
            End With
        Next

        '3.條碼數擾新增' ------------------------------------------------------------------------
        For i = 0 To ds.Tables("SampleSendCode").Rows.Count - 1
            With ds.Tables("SampleSendCode")
                If IIf(IsDBNull(.Rows(i)("AutoID")), 0, .Rows(i)("AutoID")) = 0 Then

                    Dim sscinfo As New SampleSendCodeInfo
                    sscinfo.SO_ID = IIf(IsDBNull(.Rows(i)("SO_ID")), Nothing, .Rows(i)("SO_ID"))
                    sscinfo.SS_Edition = IIf(IsDBNull(.Rows(i)("SS_Edition")), Nothing, .Rows(i)("SS_Edition"))
                    sscinfo.PM_M_Code = IIf(IsDBNull(.Rows(i)("PM_M_Code")), Nothing, .Rows(i)("PM_M_Code"))
                    sscinfo.Code_ID = IIf(IsDBNull(.Rows(i)("Code_ID")), Nothing, .Rows(i)("Code_ID"))
                    sscinfo.Code_Qty = IIf(IsDBNull(.Rows(i)("Code_Qty")), Nothing, .Rows(i)("Code_Qty"))
                    sscinfo.CodeType = IIf(IsDBNull(.Rows(i)("CodeType")), Nothing, .Rows(i)("CodeType"))
                    sscinfo.SP_ID = txtSP_ID.Text
                    sscinfo.AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    sscinfo.AddUserID = InUserID

                    If ssccon.SampleSendCode_Add(sscinfo) = False Then
                        MsgBox("添加失敗，请檢查原因！")
                        Exit Sub
                    End If
                End If
            End With
        Next

        MsgBox("修改成功", 60, "提示")
        Me.Close()
    End Sub
#End Region

#Region "检查数据"
    ''' <summary>
    ''' 是否为空
    ''' </summary>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer
        If gluCuster.Text = String.Empty Then
            MsgBox("客戶代号不能为空,请输入！")
            gluCuster.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If DateSendDate.Text = String.Empty Then
            MsgBox("寄送日期不能为空,请输入！")
            DateSendDate.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If txt_SP_ExpDeliveryID.Text = String.Empty Then
            MsgBox("快递单号不能为空,请输入！")
            txt_SP_ExpDeliveryID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If ds.Tables("SampleSend").Rows.Count <= 0 Then
            MsgBox("子表沒有数据,不能保存！", 64, "提示")
            gridSampleSend.Focus()
            DataCheckEmpty = 0
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("SampleSend").Rows.Count - 1
            If IsDBNull(ds.Tables("SampleSend").Rows(i)("SO_ID")) Then
                MsgBox("样办订单号不能为空，请输入样办订单号！", 64, "提示")
                gridSampleSend.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SampleSend").Rows(i)("SS_Edition")) Then
                MsgBox("版本号不能为空，请输入版本号！", 64, "提示")
                gridSampleSend.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SampleSend").Rows(i)("PM_M_Code")) Then
                MsgBox("产品编号不能为空，请输入产品编号！", 64, "提示")
                gridSampleSend.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IsDBNull(ds.Tables("SampleSend").Rows(i)("M_Code")) Then
                MsgBox("配件编号不能为空，请输入配件编号！", 64, "提示")
                gridSampleSend.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            If IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SP_Qty")), 0, ds.Tables("SampleSend").Rows(i)("SP_Qty")) = 0 Then
                MsgBox("寄送数量不能为空，请输入寄送数量！", 64, "提示")
                gridSampleSend.Focus()
                GridView1.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            Dim IntSP_Qty As Integer = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SP_Qty")), 0, ds.Tables("SampleSend").Rows(i)("SP_Qty"))
            Dim IntSO_OrderQty As Integer = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SO_OrderQty")), 0, ds.Tables("SampleSend").Rows(i)("SO_OrderQty"))
            Dim IntQty As Integer = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("Qty")), 0, ds.Tables("SampleSend").Rows(i)("Qty"))
            'If IntSP_Qty > (IntSO_OrderQty - IntQty) Then
            '    MsgBox("寄送数量不能大于订单数量-已寄送数量，请重新输入寄送数量！", 64, "提示")
            '    gridSampleSend.Focus()
            '    GridView1.FocusedRowHandle = i
            '    DataCheckEmpty = 0
            '    Exit Function
            'End If
        Next

        If ds.Tables("SampleSendCode").Rows.Count <= 0 Then
            MsgBox("沒有輸入條碼,不能保存！", 64, "提示")
            Grid1.Focus()
            DataCheckEmpty = 0
        End If
        For i = 0 To ds.Tables("SampleSendCode").Rows.Count - 1
            Dim strM_Code As String = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("Code_ID")), String.Empty, ds.Tables("SampleSendCode").Rows(i)("Code_ID"))
            Dim strCodeType As String = IIf(IsDBNull(ds.Tables("SampleSendCode").Rows(i)("CodeType")), String.Empty, ds.Tables("SampleSendCode").Rows(i)("CodeType"))
            '1.此條碼<采集表>是否存在
            If sclcom.SampleCollection_GetID(strM_Code) = False Then
                MsgBox("<采集表>不存在！", 64, "提示")
                Grid1.Focus()
                GridView9.FocusedRowHandle = i
                DataCheckEmpty = 0
                Exit Function
            End If
            '2.條碼是不是完工狀態
            Dim strSO_ID As String = String.Empty
            Dim strSS_Edition As String = String.Empty
            Dim strPM_M_Code As String = String.Empty
            Dim scmlistm As New List(Of SampleCollectionInfo)
            scmlistm = sclcom.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scmlistm.Count > 0 Then
                If scmlistm(0).StatusType <> "M" Then
                    MsgBox(strM_Code + " 此条码不是完工状态！", 64, "提示")
                    Grid1.Focus()
                    GridView9.FocusedRowHandle = i
                    DataCheckEmpty = 0
                    Exit Function
                End If

                strSO_ID = scmlistm(0).SO_ID '訂單編號
                strSS_Edition = scmlistm(0).SS_Edition '版次
                strPM_M_Code = scmlistm(0).PM_M_Code '產品編號
            End If

            '3.客戶是否存在此訂單條碼
            If strSO_ID <> String.Empty And strSS_Edition <> String.Empty Then
                Dim somlist As New List(Of SampleOrdersMainInfo)
                somlist = somcon.SampleOrdersMain_GetList(strSO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, True)
                If somlist.Count > 0 Then
                    If somlist(0).SO_CusterID <> gluCuster.EditValue Then
                        MsgBox("客戶不存在此订单条码！", 64, "提示")
                        Grid1.Focus()
                        GridView9.FocusedRowHandle = i
                        DataCheckEmpty = 0
                        Exit Function
                    End If
                End If
            End If
            '4裝箱條碼不能為空
            If strCodeType = "裝箱" Then
                If Me.txtPK_CodeID.Text = String.Empty Then
                    MsgBox("装箱条码不能为空,请输入！")
                    txtPK_CodeID.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If
        Next

        DataCheckEmpty = 1
    End Function
#End Region

#Region "自动流水号"
    ''' <summary>
    ''' 自動流水号
    ''' </summary>
    ''' <remarks></remarks>
    Function GetSP_ID() As String
        Dim oc As New SampleSendControler
        Dim oi As New SampleSendInfo
        Dim ndate As String = "SF" + Format(Now(), "yyMM")
        oi = oc.SampleSend_Get(ndate)
        If oi Is Nothing Then
            GetSP_ID = "SF" + Format(Now, "yyMM") + "0001"
        Else
            GetSP_ID = "SF" + Format(Now, "yyMM") + Mid(CStr(CInt(Mid(oi.SP_ID, 7)) + 1000000001), 7)
        End If
    End Function

#End Region

#Region "条码事件"
    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            lblCode.Text = String.Empty
            Dim strM_Code As String
            '1.客戶不能為空
            If gluCuster.EditValue = String.Empty Then
                MsgBox("客戶代号不能为空,请输入！")
                gluCuster.Focus()
                Exit Sub
            End If

            '2.條碼不能重複
            Dim i As Integer
            strM_Code = UCase(Me.txtM_Code.Text)
            For i = 0 To ds.Tables("SampleSendCode").Rows.Count - 1
                If strM_Code = ds.Tables("SampleSendCode").Rows(i)("Code_ID") Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = strM_Code + "条码重复"
                    Exit Sub
                End If
            Next

            '3.此條碼<采集表>是否存在
            If sclcom.SampleCollection_GetID(strM_Code) = False Then
                lblCode.Text = "<采集表>不存在"
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                Exit Sub
            End If

            Dim strSO_ID As String = String.Empty
            Dim strSS_Edition As String = String.Empty
            Dim strPM_M_Code As String = String.Empty
            Dim strM_CodeItem As String = String.Empty
            Dim strSO_SampleID As String = String.Empty
            Dim IntSO_OrderQty As Integer = 0
            Dim strM_Name As String = String.Empty

            '4.條碼是不是完工狀態
            Dim scmlistm As New List(Of SampleCollectionInfo)
            scmlistm = sclcom.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            If scmlistm.Count > 0 Then
                If scmlistm(0).StatusType <> "M" Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "此条码不是完工状态"
                    Exit Sub
                End If
                strSO_ID = scmlistm(0).SO_ID '訂單編號
                strSS_Edition = scmlistm(0).SS_Edition '版次
                strPM_M_Code = scmlistm(0).PM_M_Code '產品編號
            End If

            '5.客戶是否存在此訂單條碼
            If strSO_ID <> String.Empty And strSS_Edition <> String.Empty Then
                Dim somlist As New List(Of SampleOrdersMainInfo)
                somlist = somcon.SampleOrdersMain_GetList(strSO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, True)
                If somlist.Count > 0 Then
                    strM_CodeItem = somlist(0).M_Code '配件編號
                    strM_Name = somlist(0).M_Name
                    strSO_SampleID = somlist(0).SO_SampleID

                    IntSO_OrderQty = somlist(0).SO_OrderQty '訂單數量
                    If somlist(0).SO_CusterID <> gluCuster.EditValue Then
                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        lblCode.Text = "客戶不存在此订单条码"
                        Exit Sub
                    End If
                End If
            End If

            Dim strCode As String
            Dim strQty As String
            Dim intIn As Integer
            Dim StrText As String
            StrText = txtM_Code.Text
            intIn = InStr(StrText, ",", CompareMethod.Text)
            If StrText = "" Then Exit Sub

            If intIn <= 0 Then
                strCode = StrText
                strQty = "0"
            Else
                strCode = Mid(StrText, 1, intIn - 1)
                strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
            End If

            Dim row As DataRow
            row = ds.Tables("SampleSendCode").NewRow
            row("Code_ID") = StrConv(UCase(strCode), vbNarrow)
            row("Code_Qty") = 1
            row("SO_ID") = strSO_ID
            row("SS_Edition") = strSS_Edition
            row("PM_M_Code") = strPM_M_Code
            row("CodeType") = String.Empty
            row("AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
            ds.Tables("SampleSendCode").Rows.Add(row)
            txtM_Code.Text = ""
            CreateSendTalbe(strSO_ID, strSS_Edition, strPM_M_Code, strM_CodeItem, strM_Name, IntSO_OrderQty, strSO_SampleID)
        End If
    End Sub

    Sub CreateSendTalbe(ByVal strSO_ID As String, ByVal strSS_Edition As String, ByVal strPM_M_Code As String, ByVal strM_CodeItem As String, ByVal strM_Name As String, ByVal IntSO_OrderQty As Integer, ByVal strSO_SampleID As String)
        Dim i As Integer
        Dim boolSend As Boolean = False
        For i = 0 To ds.Tables("SampleSend").Rows.Count - 1
            Dim chrSO_ID As String = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SO_ID")), Nothing, ds.Tables("SampleSend").Rows(i)("SO_ID"))
            Dim chrSS_Edition As String = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("SS_Edition")), Nothing, ds.Tables("SampleSend").Rows(i)("SS_Edition"))
            Dim chrPM_M_Code As String = IIf(IsDBNull(ds.Tables("SampleSend").Rows(i)("PM_M_Code")), Nothing, ds.Tables("SampleSend").Rows(i)("PM_M_Code"))
            If strSO_ID = chrSO_ID And chrSS_Edition = strSS_Edition And strPM_M_Code = chrPM_M_Code Then
                boolSend = True
                ds.Tables("SampleSend").Rows(i)("SP_Qty") = ds.Tables("SampleSend").Rows(i)("SP_Qty") + 1
            End If
        Next

        If boolSend = False Then
            Dim SampleSendInfo As New SampleSendInfo
            Dim rows As DataRow
            rows = ds.Tables("SampleSend").NewRow
            rows("SP_ID") = String.Empty
            rows("SO_ID") = strSO_ID
            rows("SS_Edition") = strSS_Edition
            rows("PM_M_Code") = strPM_M_Code
            rows("M_Code") = strM_CodeItem
            rows("M_Name") = strM_Name
            rows("SO_SampleID") = strSO_SampleID
            rows("SP_SendDate") = Format(Now, "yyyy-MM-dd")
            rows("SP_Qty") = 1
            rows("SO_OrderQty") = IntSO_OrderQty
            rows("SP_Remark") = String.Empty
            rows("Qty") = sscon.SampleSend_GetQty(strSO_ID, strSS_Edition)
            ds.Tables("SampleSend").Rows.Add(rows)
        End If
    End Sub
#End Region

#Region "裝箱條碼直接寄送"
    Private Sub txtPK_CodeID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPK_CodeID.KeyDown
        If e.KeyCode = Keys.Enter And EditItem = EditEnumType.ADD Then
            Dim strPK_CodeID As String = txtPK_CodeID.Text
            Dim strPK_ID As String = String.Empty

            If strPK_CodeID <> String.Empty Then
                Dim pklist As New List(Of SamplePackingInfo)
                pklist = spkcon.SamplePacking_GetList(Nothing, Nothing, Nothing, strPK_CodeID, Nothing, Nothing, Nothing, Nothing)
                If pklist.Count > 0 Then
                    If pklist(0).CheckBit = False Then
                        MessageBox.Show(strPK_CodeID + "：此装箱条码没有审核", "提示")
                        txtPK_CodeID.Text = String.Empty
                        Exit Sub
                    End If
                    If pklist(0).BitNeed = True Then
                        MessageBox.Show(strPK_CodeID + "：此装箱条码已经使用,不能再使用！", "提示")
                        txtPK_CodeID.Text = String.Empty
                        Exit Sub
                    End If
                    strPK_ID = pklist(0).PK_ID '裝箱單號
                Else
                    MessageBox.Show("条码不存在或者此单没有装箱条码", "提示")
                    txtPK_CodeID.Text = String.Empty
                    Exit Sub
                End If

                '1.清空表
                ds.Tables("SampleSendCode").Clear()
                ds.Tables("SampleSend").Clear()
                '2.轉入裝箱條碼
                If strPK_ID <> String.Empty Then
                    Dim spklist As New List(Of SamplePackingInfo)
                    spklist = spkcon.SamplePackingSub_GetList(Nothing, strPK_ID, Nothing, Nothing)
                    If spklist.Count > 0 Then
                        Dim i As Integer
                        Dim StrCode As String = String.Empty
                        Dim strSO_ID As String = String.Empty
                        Dim strSS_Edition As String = String.Empty
                        Dim strPM_M_Code As String = String.Empty
                        Dim strM_CodeItem As String = String.Empty
                        Dim strSO_SampleID As String = String.Empty
                        Dim IntSO_OrderQty As Integer = 0
                        Dim strM_Name As String = String.Empty

                        For i = 0 To spklist.Count - 1
                            StrCode = spklist(i).Code_ID

                            '3.條碼是不是完工狀態
                            Dim scmlistm As New List(Of SampleCollectionInfo)
                            scmlistm = sclcom.SampleCollection_Getlist(Nothing, StrCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                            If scmlistm.Count > 0 Then
                                If scmlistm(0).StatusType <> "M" Then

                                    'txtM_Code.Text = String.Empty
                                    'txtM_Code.Focus()

                                    lblCode.Text = "此条码不是完工状态"
                                    ds.Tables("SampleSendCode").Clear()
                                    Exit Sub
                                End If
                                strSO_ID = scmlistm(0).SO_ID '訂單編號
                                strSS_Edition = scmlistm(0).SS_Edition '版次
                                strPM_M_Code = scmlistm(0).PM_M_Code '產品編號
                            End If

                            '5.客戶是否存在此訂單條碼
                            If strSO_ID <> String.Empty And strSS_Edition <> String.Empty Then
                                Dim somlist As New List(Of SampleOrdersMainInfo)
                                somlist = somcon.SampleOrdersMain_GetList(strSO_ID, Nothing, Nothing, Nothing, Nothing, Nothing, True)
                                If somlist.Count > 0 Then
                                    strM_CodeItem = somlist(0).M_Code '配件編號
                                    strM_Name = somlist(0).M_Name
                                    strSO_SampleID = somlist(0).SO_SampleID
                                    IntSO_OrderQty = somlist(0).SO_OrderQty '訂單數量
                                    If somlist(0).SO_CusterID <> gluCuster.EditValue Then

                                        'txtM_Code.Text = String.Empty
                                        'txtM_Code.Focus()

                                        lblCode.Text = "客戶不存在此订单条码"
                                        ds.Tables("SampleSendCode").Clear()
                                        Exit Sub
                                    End If
                                End If
                            End If

                            Dim row As DataRow
                            row = ds.Tables("SampleSendCode").NewRow
                            row("Code_ID") = StrCode
                            row("Code_Qty") = 1
                            row("SO_ID") = strSO_ID
                            row("SS_Edition") = strSS_Edition
                            row("PM_M_Code") = strPM_M_Code
                            row("AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            row("CodeType") = "裝箱"
                            ds.Tables("SampleSendCode").Rows.Add(row)

                            CreateSendTalbe(strSO_ID, strSS_Edition, strPM_M_Code, strM_CodeItem, strM_Name, IntSO_OrderQty, strSO_SampleID)
                        Next
                    End If
                End If

            End If
        End If
    End Sub
#End Region

End Class