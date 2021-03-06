Imports LFERP.Library.PieceProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleOrdersMain
Imports LFERP.Library.NmetalSampleManager.NmetalSampleProcess
Imports LFERP.Library.NmetalSampleManager.NmetalSampleWareInventory
Imports LFERP.Library.NmetalSampleManager.NmetalSampleReWaste
Imports LFERP.Library.ProductionController

Public Class frmNmetalSampleWaste

    Private _EditItem As String '属性栏位
    Private _EditDep As String  '部門
    Private _ReNOs As String  '部門

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property EditDep() As String '部門
        Get
            Return _EditDep
        End Get
        Set(ByVal value As String)
            _EditDep = value
        End Set
    End Property


    Property ReNOs() As String '單號
        Get
            Return _ReNOs
        End Get
        Set(ByVal value As String)
            _ReNOs = value
        End Set
    End Property

    Dim nsrwc As New NmetalSampleReWasteControler
    Dim pfcon As New ProductionFieldControl
    Dim prcon As New NmetalSampleProcessControl
    Dim SwCon As New NmetalSampleWareInventoryControler
    Dim mc As New NmetalSampleOrdersMainControler
    Dim pncon As New PersonnelControl
    Dim pmlist As New List(Of PersonnelInfo) '部門分享
    Dim ds As New DataSet

    ''' <summary>
    ''' 創建臨時表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("Material")
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
        End With
        glu_PS_NO.Properties.DataSource = ds.Tables("Material")
    End Sub
    ''' <summary>
    ''' 初始化加載事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNmetalSampleWaste_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        pmlist = pncon.FacBriSearch_GetList("V", Nothing, Nothing, Nothing)

        glu_ReDepID.Properties.DataSource = pmlist          '登記部門

        glu_OutDepID.Properties.DisplayMember = "DepName"
        glu_OutDepID.Properties.ValueMember = "DepID"
        glu_OutDepID.Properties.DataSource = pmlist         '廢料來源

        glu_ReDepID.EditValue = EditDep
        glu_ReDepID.Enabled = False


        glu_PM_M_Code.Properties.DataSource = mc.NmetalSampleOrdersMain_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Select Case EditItem
            Case EditEnumType.ADD
                Me.Text = "廢料/尾料登記—新增"
                dtp_ReDate.EditValue = Format(Now, "yyyy/MM/dd")
                txt_ReNo.Enabled = False
                XtraTabPage2.PageVisible = False
            Case EditEnumType.EDIT
                Me.Text = "廢料/尾料登記—修改"
                txt_ReNo.Enabled = False
                XtraTabPage2.PageVisible = False
                LoadData(ReNOs)
            Case EditEnumType.CHECK
                Me.Text = "廢料/尾料登記—審核"
                XtraTabControl1.SelectedTabPage = Me.XtraTabPage2
                XtraTabPage2.PageVisible = True
                lbl_checkuser.Text = InUserID
                lbl_checkdate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
                LoadData(ReNOs)
            Case EditEnumType.VIEW
                Me.Text = "廢料/尾料登記—查看"
                XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
                XtraTabPage2.PageVisible = True
                Savebutton.Visible = False
                Me.GroupBox1.Enabled = False
                Me.GroupBox2.Enabled = False
                LoadData(ReNOs)
        End Select
    End Sub
    ''' <summary>
    ''' 加載數據
    ''' </summary>
    ''' <param name="Num"></param>
    ''' <remarks></remarks>
    Sub LoadData(ByVal Num As String)
        Dim nsri As New List(Of NmetalSampleReWasteInfo)
        nsri = nsrwc.NmetalSampleReWaste_GetList(Nothing, Num, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If nsri.Count <= 0 Then
            Exit Sub
        End If
        txt_ReNo.Text = nsri(0).ReNO
        glu_ReDepID.EditValue = nsri(0).ReDepID
        dtp_ReDate.EditValue = nsri(0).ReDate
        glu_OutDepID.EditValue = nsri(0).OutDepID
        txt_ReType.Text = nsri(0).ReType
        glu_PM_M_Code.EditValue = nsri(0).PM_M_Code
        glu_PM_Type.EditValue = nsri(0).PM_Type
        glu_PS_NO.EditValue = nsri(0).PS_NO
        txt_ReWeight.Text = nsri(0).ReWeight
        txt_Remark.Text = nsri(0).Remark
        ReCheck.Checked = nsri(0).ReCheck

        If nsri(0).CheckDate <> "" Then lbl_checkdate.Text = nsri(0).CheckDate
        If nsri(0).CheckUserID <> "" Then lbl_checkuser.Text = nsri(0).CheckUserID


        txt_checkremark.Text = nsri(0).CheckRemark

    End Sub
    ''' <summary>
    ''' 保存按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Savebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Savebutton.Click
        Select Case EditItem
            Case EditEnumType.ADD
                If CheckDateEmpty() = False Then
                    Exit Sub
                End If
                Savedata()
            Case EditEnumType.EDIT
                If CheckDateEmpty() = False Then
                    Exit Sub
                End If
                Savedata()
            Case EditEnumType.CHECK
                If CheckDateEmpty() = False Then
                    Exit Sub
                End If
                UpdateCheck()
        End Select
    End Sub
    ''' <summary>
    ''' 判斷是否為空
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckDateEmpty() As Boolean
        CheckDateEmpty = True
        If glu_ReDepID.EditValue = Nothing Then
            CheckDateEmpty = False
            MsgBox("登記部門不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_ReDepID.Focus()
            Exit Function
        End If
        If dtp_ReDate.Text = String.Empty Then
            CheckDateEmpty = False
            MsgBox("請選擇日期！", MsgBoxStyle.OkOnly, "提示")
            dtp_ReDate.Focus()
            Exit Function
        End If
        If glu_OutDepID.EditValue = Nothing Then
            CheckDateEmpty = False
            MsgBox("廢料來源不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_OutDepID.Focus()
            Exit Function
        End If
        If txt_ReType.Text.Trim = String.Empty Then
            CheckDateEmpty = False
            MsgBox("類型不能為空，請輸入！", MsgBoxStyle.OkOnly, "提示")
            txt_ReType.Focus()
            Exit Function
        End If
        If glu_PM_M_Code.EditValue = Nothing Then
            CheckDateEmpty = False
            MsgBox("產品編號不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_PM_M_Code.Focus()
            Exit Function
        End If
        If glu_PM_Type.EditValue = Nothing Then
            CheckDateEmpty = False
            MsgBox("產品類型不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_PM_Type.Focus()
            Exit Function
        End If
        If glu_PS_NO.EditValue = Nothing Then
            CheckDateEmpty = False
            MsgBox("收料工序不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_PS_NO.Focus()
            Exit Function
        End If

        If glu_PM_M_Code.Text.Trim = String.Empty Then
            CheckDateEmpty = False
            MsgBox("產品編號不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_PM_M_Code.Focus()
            Exit Function
        End If
        If glu_PM_Type.Text.Trim = String.Empty Then
            CheckDateEmpty = False
            MsgBox("產品類型不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_PM_Type.Focus()
            Exit Function
        End If
        If glu_PS_NO.Text.Trim = String.Empty Then
            CheckDateEmpty = False
            MsgBox("收料工序不能為空，請選擇！", MsgBoxStyle.OkOnly, "提示")
            glu_PS_NO.Focus()
            Exit Function
        End If


        If Val(txt_ReWeight.Text) <= 0 Then
            CheckDateEmpty = False
            MsgBox("重量不能小於等於0，請重新輸入！", MsgBoxStyle.OkOnly, "提示")
            txt_ReWeight.Focus()
            Exit Function
        End If
    End Function
    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Savedata()
        Dim objinfo As New NmetalSampleReWasteInfo()
        objinfo.ReDepID = glu_ReDepID.EditValue
        objinfo.ReDate = CDate(dtp_ReDate.EditValue.ToString)
        objinfo.OutDepID = glu_OutDepID.EditValue
        objinfo.ReType = txt_ReType.Text
        objinfo.PM_M_Code = glu_PM_M_Code.EditValue
        objinfo.PM_Type = glu_PM_Type.EditValue
        objinfo.PS_NO = glu_PS_NO.EditValue
        objinfo.ReWeight = txt_ReWeight.Text
        objinfo.Remark = txt_Remark.Text

        If EditItem = EditEnumType.ADD Then
            objinfo.ReNO = NmetalSampleReWaste_GetID()
        End If
        Select Case EditItem
            Case EditEnumType.ADD
                objinfo.AddUserID = InUserID
                objinfo.AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                If nsrwc.NmetalSampleReWaste_Add(objinfo) = False Then
                    MsgBox("保存失敗，请检查原因！", "提示")
                    Exit Sub
                End If
            Case EditEnumType.EDIT
                objinfo.ModifyUserID = InUserID
                objinfo.ModifyDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                objinfo.ReNO = ReNOs
                If nsrwc.NmetalSampleReWaste_Update(objinfo) = False Then
                    MsgBox("更改失敗，请检查原因！", "提示")
                    Exit Sub
                End If
        End Select
        MsgBox("保存成功!")
        Me.Close()
    End Sub
    ''' <summary>
    ''' 審核
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCheck()
        Dim objinfo As New NmetalSampleReWasteInfo()
        objinfo.ReNO = txt_ReNo.Text
        objinfo.ReCheck = ReCheck.Checked
        objinfo.CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
        objinfo.CheckUserID = InUserID
        objinfo.CheckRemark = txt_checkremark.Text

        If nsrwc.NmetalSampleReWaste_Check(objinfo) = False Then
            MsgBox(txt_ReNo.Text & "，请检查原因！", 60, "提示")
        End If
        MsgBox("保存成功!")
        Me.Close()
    End Sub
    ''' <summary>
    ''' 獲取單號
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NmetalSampleReWaste_GetID() As String
        Dim DepWeightCheckNo As String = ""
        Dim SoyyMM As String
        SoyyMM = "RE" & Format(Now, "yyMM")

        Dim ndi As New NmetalSampleReWasteInfo
        ndi = nsrwc.NmetalSampleReWaste_GetID(SoyyMM)

        If ndi.ReNO = "" Then
            DepWeightCheckNo = Trim(SoyyMM & "0001")
        Else
            DepWeightCheckNo = SoyyMM & Format(Val(Microsoft.VisualBasic.Right(ndi.ReNO, 4)) + 1, "0000")
        End If

        NmetalSampleReWaste_GetID = DepWeightCheckNo
    End Function
    ''' <summary>
    ''' 關閉窗體
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 類型
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub glu_PM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles glu_PM_M_Code.EditValueChanged
        On Error Resume Next
        glu_PM_Type.Properties.DataSource = prcon.NmetalSampleProcessMain_GetList2(Nothing, glu_PM_M_Code.EditValue)
    End Sub

    Private Sub glu_PM_Type_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles glu_PM_Type.EditValueChanged
        Dim pc As New NmetalSampleProcessControl
        Dim pci As New List(Of NmetalSampleProcessInfo)
        pci = pc.NmetalSampleProcessMain_GetList(Nothing, glu_PM_M_Code.EditValue, Nothing, glu_PM_Type.EditValue, Nothing, "True", Nothing)

        glu_PS_NO.Properties.ValueMember = "PS_NO"
        glu_PS_NO.Properties.DisplayMember = "PS_Name"
        glu_PS_NO.Properties.DataSource = pci
    End Sub
End Class