Imports LFERP.Library.ProductProcess
Imports LFERP.Library.SampleManager.SampleProcess
Imports LFERP.Library.SampleManager.SamplePace
Imports LFERP.Library.SampleManager.SampleOrdersMain
Imports LFERP.Library.SampleManager.SampleCollection
Imports LFERP.Library.SampleManager.SampleWareInventory
Imports LFERP.Library.PieceProcess
Imports LFERP.Library.SampleManager.SampleTransaction
Imports LFERP.Library.ProductionController
Imports LFERP.SystemManager
Imports LFERP.Library.SampleManager.SamplePacking

Public Class frmSamplePaceInsert

#Region "属性"
    Dim ds As New DataSet
    Dim pmcon As New ProcessMainControl
    Dim spCon As New SamplePaceControler
    Dim prcon As New SampleProcessControl
    Dim pncon As New PersonnelControl
    Dim SwCon As New SampleWareInventoryControler
    Dim pacon As New SamplePaceControler
    Dim Sccon As New SampleCollectionControler
    Dim stcon As New SampleTransactionControler
    Dim pfcon As New ProductionFieldControl
    Dim spkcon As New SamplePackingController

    Private boolSE_Check As Boolean
    Private boolSE_InCheck As Boolean
    Private _EditBooL As Boolean = True
    Private _EditItem As String '属性栏位
    Private _GetEditValue As String
    Private _EditSE_ID As String
    Private _EditDep As String
    Private StrSE_OutCardID As String '借出人员
    Dim pmlist As New List(Of PersonnelInfo) '部門分享

    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property EditSE_ID() As String '属性
        Get
            Return _EditSE_ID
        End Get
        Set(ByVal value As String)
            _EditSE_ID = value
        End Set
    End Property

    Property GetEditValue() As String '属性
        Get
            Return _GetEditValue
        End Get
        Set(ByVal value As String)
            _GetEditValue = value
        End Set
    End Property

    Property EditDep() As String '属性
        Get
            Return _EditDep
        End Get
        Set(ByVal value As String)
            _EditDep = value
        End Set
    End Property

    Property EditBooL() As Boolean  '属性
        Get
            Return _EditBooL
        End Get
        Set(ByVal value As Boolean)
            _EditBooL = value
        End Set
    End Property
#End Region

#Region "窗体载入"
    Private Sub frmSamplePaceInsert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateTable()
        pmlist = pncon.FacBriSearch_GetList(Nothing, Nothing, Nothing, Nothing)
        'gluSE_InD_ID.Properties.DisplayMember = "DepName"
        'gluSE_InD_ID.Properties.ValueMember = "DepID"
        gluSE_InD_ID.Properties.DataSource = pmlist

        'gluType.Properties.ValueMember = "SE_Type"
        'gluType.Properties.DisplayMember = "SE_TypeName"
        gluType.Properties.DataSource = spCon.SamplePaceType_Getlist(Nothing)
        '----------------------------------------------------------------------------------------
        '載入訂單編號
        Dim mtd As New SampleOrdersMainControler
        'gluSO_ID.Properties.DisplayMember = "SO_ID"
        'gluSO_ID.Properties.ValueMember = "SO_ID"
        gluSO_ID.Properties.DataSource = mtd.SampleOrdersMain_GetListItem(Nothing, Nothing, Nothing, True)

        If EditItem <> EditEnumType.OUT Then
            Dim mc As New SampleOrdersMainControler
            'gluPM_M_Code.Properties.DisplayMember = "PM_M_Code"
            'gluPM_M_Code.Properties.ValueMember = "PM_M_Code"
            gluPM_M_Code.Properties.DataSource = mc.SampleOrdersMain_GetList(gluSO_ID.EditValue, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        Select Case EditItem
            Case EditEnumType.OUT
                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.OUT)
                CreateDepNameTable(True)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")

                dateAddDate.EditValue = Format(Now, "yyyy/MM/dd")
                gluSE_OutD_ID.EditValue = EditDep
                Panel1.Visible = True
                lblCheckName.Text = InUserID
                lblCheckDate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
                cmdAdd.Visible = True
                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False
                'GetObjectWeight("COM1", 9600) '称重程序
            Case EditEnumType.INCHECK
                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.INCHECK)
                CreateDepNameTable(False)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")

                LoadDataSource()
                gluSO_ID.Enabled = False
                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluType.Enabled = False

                gluSE_InD_ID.Enabled = False
                gluSE_OutD_ID.Enabled = False
                gluOutPS_NO.Enabled = False
                gluInPS_NO.Enabled = False
                cboCodeType.Enabled = False
                CheckEdit1.Visible = True
                Label4.Visible = False
                gluLoan.Visible = False
                gluReason.Enabled = False
                txtTime.Enabled = False
                txtWeighing.Enabled = False
                txtSE_Remark.Enabled = False
                cmdAdd.Visible = False
                txtPK_CodeID.Enabled = True
                txtSealCode_ID.Enabled = True
                cmdDelSub.Enabled = False
                cobSE_BarCodeType.Enabled = False
                txtOutInQty.Enabled = False
                LoadData(GetEditValue)

                Dim sptlist As New List(Of SamplePaceInfo)
                sptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
                If sptlist.Count > 0 Then
                    If EditItem = EditEnumType.INCHECK And sptlist(0).SE_InCheckBarcode = False Then
                        txtPK_CodeID.Enabled = False
                        txtSealCode_ID.Enabled = False
                    End If
                End If
            Case EditEnumType.CHECK

                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.CHECK)
                CreateDepNameTable(False)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")

                LoadDataSource()
                gluSO_ID.Enabled = False

                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluType.Enabled = False

                gluSE_InD_ID.Enabled = False
                gluSE_OutD_ID.Enabled = False
                gluOutPS_NO.Enabled = False
                gluInPS_NO.Enabled = False

                txtM_Code.Enabled = False
                txtSE_Remark.Enabled = False
                cmdAdd.Visible = False
                Panel1.Visible = True
                cmdDelSub.Enabled = False
                gluReason.Enabled = False
                txtTime.Enabled = False
                txtWeighing.Enabled = False
                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False

                cobSE_BarCodeType.Enabled = False
                txtOutInQty.Enabled = False
                LoadData(GetEditValue)

            Case EditEnumType.EDIT

                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.EDIT)
                CreateDepNameTable(True)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")

                LoadDataSource()
                gluSO_ID.Enabled = False
                gluPM_M_Code.Enabled = False
                gluM_Code.Enabled = False
                gluType.Enabled = False
                cmdAdd.Visible = False
                gluReason.Enabled = False
                txtTime.Enabled = False
                txtWeighing.Enabled = False
                Panel1.Visible = True
                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False
                cobSE_BarCodeType.Enabled = False
                LoadData(GetEditValue)

            Case EditEnumType.VIEW

                Me.Lbl_Title.Text = Me.Text + EditEnumValue(EditEnumType.VIEW)
                CreateDepNameTable(False)
                gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")
                LoadDataSource()
                txtM_Code.Enabled = False
                Panel1.Visible = True
                CheckEdit1.Visible = True
                cmdDelSub.Enabled = False
                LoadData(GetEditValue)
                cmdSave.Visible = False
                cmdAdd.Visible = False
                gluReason.Enabled = False
                txtTime.Enabled = False
                txtWeighing.Enabled = False
                txtPK_CodeID.Enabled = False
                txtSealCode_ID.Enabled = False

        End Select
        Me.Text = Lbl_Title.Text
        Reason()
        Try
            '李超
            Dim reset As New ResetPassWords.SetPassWords
            reset.SetPassWords()
        Catch
        End Try
    End Sub
#End Region

    Sub Reason() '借出理由
        If gluType.EditValue = "P06" Then
            lblReason.Visible = True
            gluReason.Visible = True
            lblTime.Visible = True
            txtTime.Visible = True
        Else
            lblReason.Visible = False
            gluReason.Visible = False
            lblTime.Visible = False
            txtTime.Visible = False
        End If
    End Sub

    Sub LoadDataSource()
        Dim splist As New List(Of SampleProcessInfo)
        splist = prcon.SampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, Nothing)
        'gluOutPS_NO.Properties.ValueMember = "PS_NO"
        'gluOutPS_NO.Properties.DisplayMember = "PS_Name"
        gluOutPS_NO.Properties.DataSource = splist
    End Sub

#Region "载入数据"
    Sub LoadData(ByVal StrSE_ID As String)
        Dim som As New List(Of SamplePaceInfo)
        som = spCon.SamplePace_Getlist(StrSE_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If som.Count = 0 Then
            Exit Sub
        Else
            gluSO_ID.EditValue = som(0).SO_ID
            txtSS_Edition.Text = som(0).SS_Edition
            gluPM_M_Code.EditValue = som(0).PM_M_Code
            gluM_Code.EditValue = som(0).M_Code
            gluType.EditValue = som(0).SE_Type
            gluSE_OutD_ID.EditValue = som(0).SE_OutD_ID
            gluOutPS_NO.EditValue = som(0).SE_OutPS_NO
            dateAddDate.EditValue = som(0).SE_AddDate
            txtOutInQty.Text = som(0).SE_Qty
            gluSE_InD_ID.EditValue = som(0).SE_InD_ID
            gluInPS_NO.EditValue = som(0).SE_InPS_NO
            LabSE_NO.Text = som(0).SE_ID
            boolSE_Check = som(0).SE_Check
            boolSE_InCheck = som(0).SE_InCheck
            CheckA.Checked = som(0).SE_Check
            CheckEdit1.Checked = som(0).SE_InCheck
            gluLoan.EditValue = som(0).SE_LoanID
            gluReason.EditValue = som(0).SE_BorrowType
            txtSE_Remark.Text = som(0).SE_Remark
            txtMaterialType.Text = som(0).MaterialTypeName
            cobSE_BarCodeType.EditValue = som(0).SE_BarCodeType
            txtTime.Text = som(0).SE_BorrowTime
            txtWeighing.Text = som(0).Weighing

            If Me.EditItem <> EditEnumType.INCHECK Then '显示刷卡人
                If som(0).PK_Code_ID = String.Empty Then
                    txtPK_CodeID.Enabled = False
                    txtSealCode_ID.Enabled = False
                Else
                    txtPK_CodeID.Text = som(0).PK_Code_ID
                    txtSealCode_ID.Text = som(0).SealCode_ID
                End If
            Else
                If som(0).PK_Code_ID = String.Empty Then
                    txtPK_CodeID.Enabled = False
                Else
                    txtM_Code.Enabled = False
                End If
                If som(0).SealCode_ID = String.Empty Then
                    txtSealCode_ID.Enabled = False
                End If
            End If

            If som(0).SE_CheckAction = Nothing Then
                lblCheckName.Text = InUserID
            Else
                lblCheckName.Text = som(0).SE_CheckAction
            End If

            If som(0).SE_CheckDate = Nothing Then
                lblCheckDate.Text = Format(Now, "yyyy/MM/dd HH:mm:ss")
            Else
                lblCheckDate.Text = som(0).SE_CheckDate
            End If
            '------------------------------------------------------
            If Me.EditItem = EditEnumType.VIEW Then '显示刷卡人
                If som(0).SE_OutCardID <> String.Empty Then
                    txtCardID.Text = "发:" + som(0).SE_OutCardID
                Else
                    txtCardID.Text = String.Empty
                End If

                If som(0).SE_InCardID <> String.Empty Then
                    txtCardID.Text = txtCardID.Text + " 收:" + som(0).SE_InCardID
                Else
                    txtCardID.Text = txtCardID.Text + String.Empty
                End If
                If EditBooL = False Then
                    txtSealCode_ID.Text = String.Empty
                End If
            End If
            '------------------------------------------------------自动条码是否需要确认
            If som(0).SE_BarCodeType = "自动采集" And EditItem = EditEnumType.INCHECK Then
                Dim sptlist As New List(Of SamplePaceInfo)
                sptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
                If sptlist.Count > 0 Then
                    If sptlist(0).SE_InCheckBarcode Then
                        cmdAutoCode.Enabled = True
                        cmdAutoCode.Text = "确认条码数量"
                    End If
                End If
            End If
            '-----------------------------------------条码采集数据
            Dim somlist As New List(Of SamplePaceInfo)
            somlist = spCon.SamplePaceBarCode_Getlist(Nothing, Nothing, EditSE_ID)
            If somlist.Count = 0 Then
                Exit Sub
            Else
                ds.Tables("SamplePaceBarCode").Clear()
                Dim i As Integer
                For i = 0 To somlist.Count - 1
                    Dim row As DataRow
                    row = ds.Tables("SamplePaceBarCode").NewRow
                    row("AutoID") = somlist(i).AutoID
                    row("Code_ID") = somlist(i).Code_ID
                    row("ClientBarcode") = somlist(i).ClientBarcode

                    If i = 0 Then '李超修改
                        If somlist(i).ClientBarcode <> String.Empty Then
                            cboCodeType.SelectedIndex = 1
                        Else
                            cboCodeType.SelectedIndex = 0
                        End If
                    End If

                    row("SPID") = somlist(i).SPID
                    row("Qty") = somlist(i).Qty
                    row("SE_AddDate") = Format(CDate(somlist(i).SE_AddDate), "yyyy/MM/dd HH:mm:ss")
                    row("InCheck") = somlist(i).InCheck
                    If somlist(i).InCheck = False Then
                        row("InCheckDate") = Nothing
                    Else
                        row("InCheckDate") = Format(CDate(somlist(i).InCheckDate), "yyyy/MM/dd HH:mm:ss")
                    End If
                    ds.Tables("SamplePaceBarCode").Rows.Add(row)
                Next
            End If
            '-----------------------------------------
        End If
    End Sub
#End Region

#Region "收料确认"
    Sub UpdateInCheck()
        '1-----------------------------------------审核处理
        If CheckEdit1.Checked = boolSE_InCheck Then
            MsgBox("收料确认状态没有改变，提示！")
            Exit Sub
        End If

        Dim SPPI As New SamplePaceInfo
        SPPI.SE_ID = LabSE_NO.Text
        SPPI.SE_InCheck = CheckEdit1.Checked
        SPPI.SE_IncheckAction = InUserID
        SPPI.SE_InCardID = Me.txtCardID.Text

        If spCon.SamplePace_UpdateInCheck(SPPI) = False Then
            MsgBox("添加失敗，请檢查原因！")
            Exit Sub
        End If

        If (gluType.EditValue = "P06" Or gluType.EditValue = "P07") = False Then
            MsgBox("收料确认成功!")
        End If
        ''2-------------------------------------------修改条码收料时间
        Dim m As Integer

        'If EditItem = EditEnumType.INCHECK And gluType.EditValue <> "P06" And gluType.EditValue <> "P07" Then
        '    '是否需求输入确认条码
        '    Dim boolInQty As Boolean = False
        '    Dim sptlistB As New List(Of SamplePaceInfo)
        '    sptlistB = spCon.SamplePaceType_Getlist(gluType.EditValue)
        '    If sptlistB.Count > 0 Then
        '        If sptlistB(0).SE_OutVisible = False Then
        '            boolInQty = True
        '        End If
        '        If sptlistB(0).SE_InVisible = False Then
        '            boolInQty = True
        '        End If
        '    End If

        '    If boolInQty = False Then
        '        Dim strBarCode = True
        '        For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
        '            With ds.Tables("SamplePaceBarCode")
        '                Dim spBInfo As New SamplePaceInfo
        '                spBInfo.InCheck = IIf(IsDBNull(.Rows(m)("InCheck")), False, .Rows(m)("InCheck"))
        '                spBInfo.AutoID = IIf(IsDBNull(.Rows(m)("AutoID")), 0, .Rows(m)("AutoID"))
        '                spBInfo.InCheckDate = IIf(IsDBNull(.Rows(m)("InCheckDate")), Nothing, .Rows(m)("InCheckDate"))
        '                If spCon.SamplePaceBarCode_Update(spBInfo) = False Then
        '                    MsgBox("添加失敗，请檢查原因！")
        '                    Exit Sub
        '                End If
        '            End With
        '        Next

        '    End If
        'End If

        '3.-----------------------------------------扣账处理---+條碼確認
        Dim sptlist As New List(Of SamplePaceInfo)
        sptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            '3.1是否要輸入確認條碼
            If sptlist(0).SE_InCheckBarcode Then
                For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                    With ds.Tables("SamplePaceBarCode")
                        Dim spBInfo As New SamplePaceInfo
                        spBInfo.InCheck = IIf(IsDBNull(.Rows(m)("InCheck")), False, .Rows(m)("InCheck"))
                        spBInfo.AutoID = IIf(IsDBNull(.Rows(m)("AutoID")), 0, .Rows(m)("AutoID"))
                        spBInfo.InCheckDate = IIf(IsDBNull(.Rows(m)("InCheckDate")), Nothing, .Rows(m)("InCheckDate"))
                        If spCon.SamplePaceBarCode_Update(spBInfo) = False Then
                            MsgBox("添加失敗，请檢查原因！")
                            Exit Sub
                        End If
                    End With
                Next
            End If

            '3.2扣账发料
            If sptlist(0).SE_OutVisible Then
                Dim SwInfo As New SampleWareInventoryInfo
                Dim SwList As New List(Of SampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                SwList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                End If

                If CheckEdit1.Checked = True Then
                    SwInfo.SWI_Qty = intinQty - CInt(txtOutInQty.Text)
                ElseIf CheckA.Checked = False Then
                    SwInfo.SWI_Qty = intinQty + CInt(txtOutInQty.Text)
                End If

                SwInfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                SwInfo.ModifyUserID = InUserID

                SwInfo.D_ID = gluSE_OutD_ID.EditValue
                SwInfo.PS_NO = gluOutPS_NO.EditValue
                If SwCon.SampleWareInventory_Update(SwInfo) = False Then
                    MsgBox("發料扣賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

                '3.3-----------------------------------------采集條碼修改部門
                If CheckEdit1.Checked = True Then
                    Dim strStatusType As String = String.Empty
                    Dim somlist As New List(Of SamplePaceInfo)
                    somlist = pacon.SamplePaceBarCode_Getlist(Nothing, Nothing, LabSE_NO.Text)
                    If somlist.Count = 0 Then
                        Exit Sub
                    Else
                        Dim sptlistB As New List(Of SamplePaceInfo)
                        sptlistB = spCon.SamplePaceType_Getlist(gluType.EditValue)
                        If sptlistB.Count > 0 Then
                            strStatusType = sptlistB(0).StatusType
                        End If
                        For m = 0 To somlist.Count - 1
                            '修改部門
                            If Sccon.SampleCollection_UpdateC(somlist(m).Code_ID, gluSE_OutD_ID.EditValue) = False Then
                                MsgBox("生產部門修改錯誤,請檢查原因!", MsgBoxStyle.Information, "提示")
                                Exit Sub
                            End If

                            '修改状态
                            If Sccon.SampleCollection_UpdateA(somlist(m).Code_ID, strStatusType) = False Then
                                MessageBox.Show("修改条码状态错误!", "提示")
                                Exit Sub
                            End If
                        Next
                    End If
                End If
            End If

            '3.3入账收料
            If sptlist(0).SE_InVisible Then
                Dim SwInfo As New SampleWareInventoryInfo
                Dim SwList As New List(Of SampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                SwList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, gluInPS_NO.EditValue, Nothing, False, gluSE_InD_ID.EditValue)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                End If

                If CheckEdit1.Checked = True Then
                    SwInfo.SWI_Qty = intinQty + CInt(txtOutInQty.Text)
                ElseIf CheckA.Checked = False Then
                    SwInfo.SWI_Qty = intinQty - CInt(txtOutInQty.Text)
                End If

                SwInfo.ModifyDate = Format(Now, "yyyy/MM/dd")
                SwInfo.ModifyUserID = InUserID

                SwInfo.D_ID = gluSE_InD_ID.EditValue
                SwInfo.PS_NO = gluInPS_NO.EditValue
                If SwCon.SampleWareInventory_Update(SwInfo) = False Then
                    MsgBox("收料入賬失敗,請檢查原因!", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

                '3.4-----------------------------------------采集條碼修改部門--與--修改条码状态
                If CheckEdit1.Checked = True Then
                    Dim strStatusType As String = String.Empty
                    Dim somlist As New List(Of SamplePaceInfo)
                    somlist = pacon.SamplePaceBarCode_Getlist(Nothing, Nothing, LabSE_NO.Text)
                    If somlist.Count = 0 Then
                        Exit Sub
                    Else
                        Dim sptlistB As New List(Of SamplePaceInfo)
                        sptlistB = spCon.SamplePaceType_Getlist(gluType.EditValue)
                        If sptlistB.Count > 0 Then
                            strStatusType = sptlistB(0).StatusType
                        End If
                        For m = 0 To somlist.Count - 1
                            '修改部門
                            If Sccon.SampleCollection_UpdateC(somlist(m).Code_ID, gluSE_InD_ID.EditValue) = False Then
                                MsgBox("生產部門修改錯誤,請檢查原因!", MsgBoxStyle.Information, "提示")
                                Exit Sub
                            End If
                            '修改状态
                            If Sccon.SampleCollection_UpdateA(somlist(m).Code_ID, strStatusType) = False Then
                                MessageBox.Show("修改条码状态错误!", "提示")
                                Exit Sub
                            End If
                        Next
                    End If
                End If
            End If

            '4產生新的客戶條碼
            If CheckEdit1.Checked = True Then
                Dim somlist As New List(Of SamplePaceInfo)
                somlist = pacon.SamplePaceBarCode_Getlist(Nothing, Nothing, LabSE_NO.Text)
                If somlist.Count = 0 Then
                    Exit Sub
                Else

                    For m = 0 To somlist.Count - 1
                        '4.1產生新的客戶條碼
                        If somlist(m).ClientBarcode <> String.Empty Then
                            Dim SetSO_ID As String = gluSO_ID.EditValue
                            Dim SetPM_M_Code As String = Me.gluPM_M_Code.EditValue
                            Dim SetSS_Edition As String = txtSS_Edition.Text
                            Dim StrStatusType As String = String.Empty

                            Dim objinfo As New SampleCollectionInfo

                            objinfo.Code_ID = somlist(m).ClientBarcode
                            objinfo.Qty = 1
                            '李超修改20140111
                            Dim sptxlist As New List(Of SamplePaceInfo)
                            sptxlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
                            If sptxlist.Count > 0 Then
                                objinfo.StatusType = sptxlist(0).StatusType
                                StrStatusType = sptxlist(0).StatusType

                                If sptxlist(0).SE_OutPSEnabled Then
                                    objinfo.D_ID = gluSE_InD_ID.EditValue
                                Else
                                    objinfo.D_ID = gluSE_OutD_ID.EditValue
                                End If
                            End If

                            objinfo.Remark = String.Empty
                            objinfo.PM_M_Code = SetPM_M_Code
                            objinfo.AddUserID = InUserID
                            objinfo.AddDate = Format(Now, "yyyy/MM/dd")
                            objinfo.PM_Type = String.Empty
                            objinfo.SO_ID = SetSO_ID
                            objinfo.SP_ID = String.Empty
                            objinfo.SS_Edition = SetSS_Edition
                            objinfo.BarcodeType = "[c].客户条码"
                            If Sccon.SampleCollection_Add(objinfo) = False Then
                                MessageBox.Show("新增错误!", "提示")
                                Exit Sub
                            End If
                            '厂内条码变为內-->客
                            If Sccon.SampleCollection_UpdateA(somlist(m).Code_ID, "H") = False Then
                                MessageBox.Show("修改条码状态错误!", "提示")
                                Exit Sub
                            End If
                            '厂内条码变为內-->客
                            If Sccon.SampleCollection_UpdateD(somlist(m).Code_ID, somlist(m).ClientBarcode) = False Then
                                MessageBox.Show("修改客户条码错误!", "提示")
                                Exit Sub
                            End If
                            '客户条码变为在产
                            'If Sccon.SampleCollection_UpdateA(somlist(m).ClientBarcode, "Z") = False Then
                            '    MessageBox.Show("修改条码状态错误!", "提示")
                            '    Exit Sub
                            'End If

                            If Sccon.SampleCollection_UpdateA(somlist(m).ClientBarcode, StrStatusType) = False Then
                                MessageBox.Show("修改条码状态错误!", "提示")
                                Exit Sub
                            End If
                        End If

                        ''4.2完工時,收料部門不入賬,但是條碼所在部門要改變20140307李超加
                        'If gluType.EditValue = "P08" Then '只有完成
                        '    Dim Tsptlist As New List(Of SamplePaceInfo)
                        '    Tsptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
                        '    If Tsptlist.Count > 0 Then
                        '        If Tsptlist(0).SE_EditInD_ID = True Then
                        '            '修改部門
                        '            If Sccon.SampleCollection_UpdateC(somlist(m).Code_ID, gluSE_InD_ID.EditValue) = False Then
                        '                MsgBox("生產部門修改錯誤,請檢查原因!", MsgBoxStyle.Information, "提示")
                        '                Exit Sub
                        '            End If
                        '        End If
                        '    End If
                        'End If
                    Next
                End If
            End If

            '5.裝箱單使用一次以后不可以再使用
            Dim strPK_CodeID As String = txtPK_CodeID.Text
            Dim pklist As New List(Of SamplePackingInfo)
            pklist = spkcon.SamplePacking_GetList(Nothing, Nothing, Nothing, strPK_CodeID, Nothing, Nothing, Nothing, Nothing)
            If pklist.Count > 0 Then
                If strPK_CodeID <> String.Empty Then
                    Dim pkinfo As New SamplePackingInfo
                    pkinfo.Code_ID = strPK_CodeID
                    pkinfo.BitNeed = True
                    pkinfo.UsePKCount = 1

                    'Select Case pklist(0).UseCount
                    '    Case Nothing
                    '        pkinfo.UsePKCount = 1
                    '    Case 0
                    '        pkinfo.UsePKCount = 1
                    '    Case 1
                    '        pkinfo.UsePKCount = 1
                    '    Case 2
                    '        If pklist(0).UsePKCount = 0 Then
                    '            pkinfo.BitNeed = False
                    '            pkinfo.UsePKCount = 1
                    '        ElseIf pklist(0).UsePKCount = 1 Then
                    '            pkinfo.UsePKCount = pklist(0).UsePKCount + 1
                    '        End If
                    'End Select
                    If spkcon.SamplePacking_InUpdateCheck(pkinfo) = False Then
                        MessageBox.Show("修改裝箱状态错误!", "提示")
                        Exit Sub
                    End If
                End If
            End If

        End If
        Me.Close()
    End Sub
#End Region

#Region "审核"
    Sub UpdateCheck(ByVal boolMess As Boolean)
        If boolMess Then
            If CheckA.Checked = boolSE_Check Then
                MsgBox("审核状态没有改变，提示！")
                Exit Sub
            End If
        End If

        '-------------------------------------------
        Dim SPPI As New SamplePaceInfo
        SPPI.SE_ID = LabSE_NO.Text
        SPPI.SE_Check = CheckA.Checked
        SPPI.SE_CheckDate = Format(Now, "yyyy-MM-dd HH:mm:ss")
        SPPI.SE_CheckAction = InUserID
        If spCon.SamplePace_UpdateCheck(SPPI) = False Then
            MsgBox("添加失敗，请檢查原因！")
            Exit Sub
        End If

        If boolMess Then
            If CheckA.Checked Then
                MsgBox("審核成功!")
            Else
                MsgBox("取消審核成功!")
            End If
            Me.Close()
        End If
    End Sub
#End Region

#Region "控件事件"
    Private Sub gluSO_ID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSO_ID.EditValueChanged
        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.INCHECK Or EditItem = EditEnumType.VIEW Or EditItem = EditEnumType.CHECK Or EditItem = EditEnumType.EDIT Then
            '1.给产品编号填值
            Dim strM As String = gluSO_ID.EditValue
            Dim mc As New SampleOrdersMainControler
            gluPM_M_Code.Properties.DataSource = mc.SampleOrdersMain_GetList(strM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            '2.其他填值
            Dim mtd As New SampleOrdersMainControler
            Dim somlist As New List(Of SampleOrdersMainInfo)
            somlist = mtd.SampleOrdersMain_GetListItem(strM, Nothing, Nothing, True)
            If somlist.Count > 0 Then
                txtSS_Edition.Text = somlist(0).SS_Edition
                gluPM_M_Code.EditValue = somlist(0).PM_M_Code
                txtMaterialType.Text = somlist(0).MaterialTypeName
            End If
        End If
    End Sub

    Private Sub gluPM_M_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluPM_M_Code.EditValueChanged
        On Error Resume Next
        'gluM_Code.Properties.DisplayMember = "Type3ID"
        'gluM_Code.Properties.ValueMember = "Type3ID"
        gluM_Code.Properties.DataSource = prcon.SampleProcessMain_GetList2(Nothing, sender.text)
    End Sub

    Private Sub gluType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluType.EditValueChanged
        'If gluType.EditValue = "P01" Then
        '    Dim pmws As New PermissionModuleWarrantSubController
        '    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890409")
        '    If pmwiL.Count > 0 Then
        '        If pmwiL.Item(0).PMWS_Value = "否" Then
        '            MessageBox.Show("此用戶沒有開料的權限")
        '            gluType.EditValue = String.Empty
        '        End If
        '    Else
        '        MessageBox.Show("此用戶沒有開料的權限")
        '        gluType.EditValue = String.Empty
        '    End If
        'End If

        'If gluType.EditValue = "P09" Then
        '    Dim pmws As New PermissionModuleWarrantSubController
        '    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890407")
        '    If pmwiL.Count > 0 Then
        '        If pmwiL.Item(0).PMWS_Value = "否" Then
        '            MessageBox.Show("此用戶沒有退料的權限")
        '            gluType.EditValue = String.Empty
        '        End If
        '    Else
        '        MessageBox.Show("此用戶沒有退料的權限")
        '        gluType.EditValue = String.Empty
        '    End If
        'End If

        'If gluType.EditValue = "P08" Then
        '    Dim pmws As New PermissionModuleWarrantSubController
        '    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        '    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890408")
        '    If pmwiL.Count > 0 Then
        '        If pmwiL.Item(0).PMWS_Value = "否" Then
        '            MessageBox.Show("此用戶沒有完工的權限")
        '            gluType.EditValue = String.Empty
        '        End If
        '    Else
        '        MessageBox.Show("此用戶沒有完工的權限")
        '        gluType.EditValue = String.Empty
        '    End If
        'End If

        'If gluType.EditValue = "P03" Then
        '    Dim pmws As New PermissionModuleWarrantSubController
        '    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        '    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890411")
        '    If pmwiL.Count > 0 Then
        '        If pmwiL.Item(0).PMWS_Value = "否" Then
        '            MessageBox.Show("此用戶沒有损坏的權限")
        '            gluType.EditValue = String.Empty
        '        End If
        '    Else
        '        MessageBox.Show("此用戶沒有损坏的權限")
        '        gluType.EditValue = String.Empty
        '    End If
        'End If

        '权限处理---------------------------------------------------
        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then

            Dim BoolPermission As Boolean = True
            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
            Select Case gluType.EditValue
                Case "P01"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890409")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
                Case "P03"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890411")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
                Case "P08"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890408")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
                Case "P09"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890407")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
            End Select
            If BoolPermission = False Then
                MessageBox.Show("此用戶沒有" + gluType.Text + "的權限")
                gluType.EditValue = String.Empty
                Exit Sub
            End If
        End If

        '1.控件可見
        Reason() '借出理由可见
        Dim sptlistA As New List(Of SamplePaceInfo)
        sptlistA = spCon.SamplePaceType_Getlist(gluType.EditValue)
        If sptlistA.Count > 0 Then
            If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then
                gluSE_OutD_ID.Enabled = sptlistA(0).SE_OutEnabled
                gluSE_InD_ID.Enabled = sptlistA(0).SE_OutPSEnabled
                gluOutPS_NO.Enabled = sptlistA(0).SE_InEnabled
                gluInPS_NO.Enabled = sptlistA(0).SE_InPSEnabled
                cboCodeType.Enabled = sptlistA(0).SE_BarcodeLink '是否可以条码关联
            End If
        End If
        ''-----------------------------------------------------------
        If gluType.EditValue = "P07" Then
            Dim som As New List(Of SamplePaceInfo)
            som = spCon.SamplePace_Getlist2(Nothing, gluSO_ID.EditValue, txtSS_Edition.Text, Nothing, gluPM_M_Code.EditValue, Nothing, Nothing, True, Nothing, EditDep, Nothing, True, True)
            gluLoan.Properties.DisplayMember = "SE_ID"
            gluLoan.Properties.ValueMember = "SE_ID"
            gluLoan.Properties.DataSource = som
            '-------------------------------------------------
            If EditItem = EditEnumType.INCHECK Or EditItem = EditEnumType.CHECK Or EditItem = EditEnumType.VIEW Then
                Label4.Visible = False
                gluLoan.Visible = False
            Else
                Label4.Visible = True
                gluLoan.Visible = True
            End If
            LoadDataSource()
        Else
            Dim sptlist As New List(Of SamplePaceInfo)
            sptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
            If sptlist.Count > 0 Then
                '-第一個發料區可否Enabled
                If sptlist(0).SE_OutVisible Then
                    GroupBox2.Enabled = sptlist(0).SE_OutVisible '李超修改
                    gluSE_OutD_ID.Properties.DataSource = ds.Tables("SampleDepName")
                Else
                    GroupBox2.Enabled = sptlist(0).SE_OutVisible '李超修改
                    gluSE_OutD_ID.EditValue = String.Empty
                    gluOutPS_NO.EditValue = String.Empty
                End If

                '-第二個收料區可否Enabled
                If sptlist(0).SE_InVisible Then
                    GroupBox3.Enabled = sptlist(0).SE_InVisible '李超修改
                    If sptlist(0).SE_OutVisible Then
                        If gluType.EditValue = "P06" Then
                            Dim pmlist As New List(Of PersonnelInfo) '部門分享
                            pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, Nothing, Nothing, "Loan")
                            gluSE_InD_ID.Properties.DataSource = pmlist
                            gluSE_InD_ID.EditValue = pmlist(0).DepID
                        Else
                            gluSE_InD_ID.Properties.DataSource = pmlist
                        End If
                        '--------------------------------------------------------------损坏固定部门
                        If gluType.EditValue = "P03" Then
                            Dim pmlist As New List(Of PersonnelInfo) '部門分享
                            pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, Nothing, Nothing, "BF")
                            gluSE_InD_ID.Properties.DataSource = pmlist
                            gluSE_InD_ID.EditValue = pmlist(0).DepID
                        Else
                            gluSE_InD_ID.Properties.DataSource = pmlist
                        End If
                    Else
                        If gluType.EditValue = "P06" Then
                            Dim pmlist As New List(Of PersonnelInfo) '部門分享
                            pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, Nothing, Nothing, "Loan")
                            gluSE_InD_ID.Properties.DataSource = pmlist
                            gluSE_InD_ID.EditValue = pmlist(0).DepID
                        Else
                            gluSE_InD_ID.Properties.DataSource = ds.Tables("SampleDepName")
                        End If
                        '--------------------------------------------------------------损坏固定部门
                        If gluType.EditValue = "P03" Then
                            Dim pmlist As New List(Of PersonnelInfo) '部門分享
                            pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, Nothing, Nothing, "BF")
                            gluSE_InD_ID.Properties.DataSource = pmlist
                            gluSE_InD_ID.EditValue = pmlist(0).DepID
                        Else
                            gluSE_InD_ID.Properties.DataSource = ds.Tables("SampleDepName")
                        End If

                    End If
                Else
                    GroupBox3.Enabled = sptlist(0).SE_InVisible '李超修改
                    gluSE_InD_ID.EditValue = String.Empty
                    gluInPS_NO.EditValue = String.Empty
                End If
            End If

            Label4.Visible = False
            gluLoan.Visible = False
            '清空表格数据
            If EditItem = EditEnumType.OUT Then
                ds.Tables("SamplePaceBarCode").Clear() '否清空表内数据
                txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
                cobSE_BarCodeType.Enabled = True
            End If

        End If
    End Sub

    Private Sub cboCodeType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCodeType.EditValueChanged
        'Select Case Mid(cboCodeType.EditValue, 1, 3)
        '    Case "[A]"
        '        Label2.Visible = False
        '        txtClientBarcode.Visible = False
        '    Case "[C]"
        '        Label2.Visible = True
        '        txtClientBarcode.Visible = True
        'End Select

        Select Case cboCodeType.SelectedIndex
            Case 0
                lblClientBarcode.Visible = False
                txtClientBarcode.Visible = False
            Case 1
                lblClientBarcode.Visible = True
                txtClientBarcode.Visible = True
        End Select

    End Sub
#End Region

#Region "新增程序"
    ''' <summary>
    ''' 新增程序
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataNew() '新增
        '1.主档新增-----------------------------------------------------
        Dim spinfo As New SamplePaceInfo
        Dim GetSE_IDItem As String
        GetSE_IDItem = GetSE_ID()

        spinfo.SE_ID = GetSE_IDItem '自动流水号
        spinfo.SO_ID = gluSO_ID.EditValue
        spinfo.SS_Edition = Me.txtSS_Edition.Text
        spinfo.PM_M_Code = gluPM_M_Code.EditValue
        spinfo.M_Code = gluM_Code.EditValue
        spinfo.SE_Type = gluType.EditValue
        spinfo.SE_OutD_ID = gluSE_OutD_ID.EditValue
        spinfo.SE_OutPS_NO = Me.gluOutPS_NO.EditValue
        spinfo.SE_AddDate = Me.dateAddDate.EditValue
        spinfo.SE_OutTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_InTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_OutInType = "發料"
        spinfo.SE_InD_ID = gluSE_InD_ID.EditValue
        spinfo.SE_InPS_NO = Me.gluInPS_NO.EditValue
        spinfo.SE_Qty = ds.Tables("SamplePaceBarCode").Rows.Count ' txtOutInQty.Text
        spinfo.SE_AddUserID = InUserID
        spinfo.SE_OutCardID = Me.txtCardID.Text

        spinfo.SE_LoanID = gluLoan.EditValue
        spinfo.SE_BorrowType = gluReason.EditValue
        spinfo.SE_BorrowTime = txtTime.EditValue
        spinfo.Weighing = Me.txtWeighing.EditValue



        spinfo.SE_Remark = txtSE_Remark.Text
        spinfo.SE_BarCodeType = Me.cobSE_BarCodeType.EditValue

        If spCon.SamplePace_Add(spinfo) = False Then
            MsgBox("添加失败!")
            Exit Sub
        End If

        '2.条码新增-----------------------------------------------------
        Dim M As Integer
        For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                Dim spBInfo As New SamplePaceInfo
                Dim strCode_ID As String = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                spBInfo.SE_ID = GetSE_IDItem
                spBInfo.Code_ID = strCode_ID
                spBInfo.ClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), Nothing, .Rows(M)("ClientBarcode"))
                spBInfo.Qty = 1
                spBInfo.SE_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                spBInfo.SE_AddUserID = InUserID

                If spCon.SamplePaceBarCode_Add(spBInfo) = False Then
                    MsgBox("添加失敗，请檢查原因！")
                    Exit Sub
                End If

                '----------------------------------------------------
                If gluType.EditValue = "P07" Then '還入
                    Dim objictInfo As New SamplePaceInfo
                    objictInfo.SE_ID = gluLoan.EditValue
                    objictInfo.Code_ID = strCode_ID
                    objictInfo.SE_Type = GetSE_IDItem
                    If spCon.SamplePaceBarCode_UpdateA(objictInfo) = False Then
                        MsgBox("添加失敗，请檢查原因！")
                        Exit Sub
                    End If
                End If

            End With
        Next
        LabSE_NO.Text = GetSE_IDItem
        UpdateCheck(False)

        MsgBox("保存成功!")
    End Sub
#End Region

#Region "修改程序"
    ''' <summary>
    '''修改
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataEdit()
        '1.删除条码
        If ds.Tables("DelSamplePaceBarCode").Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ds.Tables("DelSamplePaceBarCode").Rows.Count - 1
                spCon.SamplePaceBarCode_Delete(ds.Tables("DelSamplePaceBarCode").Rows(j)("AutoID"), Nothing, Nothing) '刪除当前选定的
            Next
        End If
        '1.修改主档
        Dim spinfo As New SamplePaceInfo
        spinfo.SO_ID = gluSO_ID.EditValue
        spinfo.SS_Edition = Me.txtSS_Edition.Text
        spinfo.PM_M_Code = gluPM_M_Code.EditValue
        spinfo.M_Code = gluM_Code.EditValue
        spinfo.SE_Type = gluType.EditValue
        spinfo.SE_OutD_ID = gluSE_OutD_ID.EditValue
        spinfo.SE_OutPS_NO = Me.gluOutPS_NO.EditValue
        spinfo.SE_AddDate = Me.dateAddDate.EditValue
        spinfo.SE_OutTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_InTime = Format(Now, "yyyy/MM/dd")
        spinfo.SE_OutInType = "發料"
        spinfo.SE_InD_ID = gluSE_InD_ID.EditValue
        spinfo.SE_InPS_NO = Me.gluInPS_NO.EditValue
        spinfo.SE_Qty = ds.Tables("SamplePaceBarCode").Rows.Count ' txtOutInQty.Text
        spinfo.SE_ID = LabSE_NO.Text
        spinfo.SE_ModifyDate = Format(Now, "yyyy/MM/dd")
        spinfo.SE_ModifyUserID = InUserID
        spinfo.AutoID = GetEditValue
        spinfo.SE_OutCardID = Me.txtCardID.Text
        spinfo.SE_LoanID = gluLoan.EditValue
        spinfo.SE_BorrowType = gluReason.EditValue
        spinfo.SE_BorrowTime = txtTime.EditValue
        spinfo.Weighing = Me.txtWeighing.EditValue

        spinfo.SE_Remark = txtSE_Remark.Text
        spinfo.SE_BarCodeType = Me.cobSE_BarCodeType.EditValue

        If spCon.SamplePace_Update(spinfo) = False Then
            MsgBox("修改失败!")
            Exit Sub
        End If
        '3.修改条码-----------------------------------------------------
        Dim M As Integer
        For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")

                If IIf(IsDBNull(.Rows(M)("AutoID")), 0, .Rows(M)("AutoID")) = 0 Then
                    Dim spBInfo As New SamplePaceInfo
                    Dim strCode_ID As String = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                    spBInfo.SE_ID = LabSE_NO.Text
                    spBInfo.Code_ID = strCode_ID
                    spBInfo.ClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), Nothing, .Rows(M)("ClientBarcode"))

                    spBInfo.Qty = 1
                    spBInfo.SE_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    spBInfo.SE_AddUserID = InUserID

                    If spCon.SamplePaceBarCode_Add(spBInfo) = False Then
                        MsgBox("修改失敗，请檢查原因！")
                        Exit Sub
                    End If
                    '----------------------------------------------------
                    If gluType.EditValue = "P07" Then '還入
                        Dim objictInfo As New SamplePaceInfo
                        objictInfo.SE_ID = gluLoan.EditValue
                        objictInfo.Code_ID = strCode_ID
                        objictInfo.SE_Type = LabSE_NO.Text
                        If spCon.SamplePaceBarCode_UpdateA(objictInfo) = False Then
                            MsgBox("添加失敗，请檢查原因！")
                            Exit Sub
                        End If
                    End If

                End If
            End With
        Next
        UpdateCheck(False)
        '---------------------------------------------------------------
        MsgBox("修改成功!")
        Me.Close()
    End Sub

#End Region

#Region "按键事件"
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case EditItem
            Case EditEnumType.OUT '发料保存
                If DataCheckEmpty() = 0 Then
                    Exit Sub
                End If
                DataNew()
                If gluType.EditValue = "P06" Or gluType.EditValue = "P07" Then
                    CheckEdit1.Checked = True
                    UpdateInCheck()
                End If
                cmdAdd.Visible = True
                cmdSave.Visible = False
            Case EditEnumType.INCHECK  '确认收料
                If DataCheckEmpty() = 0 Then
                    Exit Sub
                End If
                UpdateInCheck()
            Case EditEnumType.CHECK  '确认收料
                If CheckA.Checked Then
                    If DataCheckEmpty() = 0 Then
                        Exit Sub
                    End If
                End If
                UpdateCheck(True)
            Case EditEnumType.EDIT
                If DataCheckEmpty() = 0 Then
                    Exit Sub
                End If
                DataEdit()
                If gluType.EditValue = "P06" Or gluType.EditValue = "P07" Then
                    CheckEdit1.Checked = True
                    UpdateInCheck()
                End If
        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        gluType.EditValue = String.Empty
        gluSE_OutD_ID.EditValue = String.Empty
        gluSE_InD_ID.EditValue = String.Empty
        gluOutPS_NO.EditValue = String.Empty
        gluInPS_NO.EditValue = String.Empty
        lblQtyOut.Text = "0"
        lblQtyIn.Text = "0"
        ds.Tables("SamplePaceBarCode").Clear()
        cmdSave.Visible = True
        cmdAdd.Visible = False
        cobSE_BarCodeType.SelectedIndex = 0
        cobSE_BarCodeType.Enabled = True
    End Sub

#End Region

#Region "创建临时表"
    ''' <summary>
    ''' 创建临时表
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateTable()
        ds.Tables.Clear()
        With ds.Tables.Add("SamplePaceBarCode") '子配件表
            .Columns.Add("Code_ID", GetType(String))
            .Columns.Add("SPID", GetType(String))
            .Columns.Add("ClientBarcode", GetType(String))
            .Columns.Add("Qty", GetType(Int32))
            .Columns.Add("AutoID", GetType(Decimal))
            .Columns.Add("SE_AddDate", GetType(String))
            .Columns.Add("InCheck", GetType(Boolean))
            .Columns.Add("InCheckDate", GetType(String))
            .Columns.Add("InCheckQty", GetType(Int32))
        End With

        Grid1.DataSource = ds.Tables("SamplePaceBarCode")
        With ds.Tables.Add("DelSamplePaceBarCode")
            .Columns.Add("AutoID", GetType(Decimal))
        End With

        With ds.Tables.Add("SampleDepName") '子配件表
            .Columns.Add("DepID", GetType(String))
            .Columns.Add("DepName", GetType(String))
        End With

        With ds.Tables.Add("SamplePS") '子配件表
            .Columns.Add("PS_Num", GetType(String))
            .Columns.Add("PS_NO", GetType(String))
            .Columns.Add("PS_Name", GetType(String))
            .Columns.Add("PS_NoName", GetType(String))
        End With
    End Sub

    Sub CreateDepNameTable(ByVal boolUserID As Boolean)
        Dim strUserID As String = String.Empty
        If boolUserID = True Then
            strUserID = InUserID
        Else
            strUserID = Nothing
        End If

        Dim pflist As New List(Of ProductionFieldControlInfo)
        pflist = pfcon.ProductionFieldControl_GetList(strUserID, Nothing)
        ds.Tables("SampleDepName").Clear()
        Dim i As Integer = 0
        For i = 0 To pflist.Count - 1
            Dim row As DataRow
            row = ds.Tables("SampleDepName").NewRow
            row("DepID") = pflist(i).ControlDep
            row("DepName") = pflist(i).DepName
            ds.Tables("SampleDepName").Rows.Add(row)
        Next
    End Sub
#End Region

#Region "条码事件"

    Private Sub txtM_Code_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtM_Code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case EditItem
                Case EditEnumType.OUT
                    BarCode()
                Case EditEnumType.EDIT
                    BarCode()
                Case EditEnumType.INCHECK
                    SetBarCode()
            End Select
        End If
    End Sub

    Private Sub txtClientBarcode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClientBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case EditItem
                Case EditEnumType.OUT
                    If cobSE_BarCodeType.SelectedIndex = 0 Then
                        SetClientBarcode()
                    Else
                        SetClientBarcodeAuto()
                    End If
                Case EditEnumType.EDIT
                    If cobSE_BarCodeType.SelectedIndex = 0 Then
                        SetClientBarcode()
                    Else
                        SetClientBarcodeAuto()
                    End If
            End Select
        End If
    End Sub

    ''' <summary>
    '''     客户条码自动方式
    ''' </summary>
    Sub SetClientBarcodeAuto()
        '1.第二个客户条码输入是否重复
        Dim strClientBarcode As String = Trim(UCase(Me.txtClientBarcode.Text))
        Dim N As Integer
        For N = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                Dim strClientBarcodeA As String = IIf(IsDBNull(.Rows(N)("ClientBarcode")), String.Empty, .Rows(N)("ClientBarcode"))
                If strClientBarcode = strClientBarcodeA Then
                    txtClientBarcode.Text = String.Empty
                    txtClientBarcode.Focus()
                    lblCode.Text = "条码重复"
                    Exit Sub
                End If
            End With
        Next

        ''此客戶條碼<采集表>存在
        Dim sclcom As New SampleCollectionControler
        If sclcom.SampleCollection_GetID(UCase(Me.txtClientBarcode.Text)) = True Then
            lblCode.Text = "客戶条码重复"
            txtClientBarcode.Text = String.Empty
            txtClientBarcode.Focus()
            MessageBox.Show(UCase(Me.txtClientBarcode.Text) + "此客戶條碼<采集表>存在!", "提示")
            Exit Sub
        End If

        Dim m As Integer
        For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                Dim strCode_ID As String = IIf(IsDBNull(.Rows(m)("ClientBarcode")), String.Empty, .Rows(m)("ClientBarcode"))
                If strCode_ID = String.Empty Then
                    .Rows(m)("ClientBarcode") = strClientBarcode
                    txtClientBarcode.Text = String.Empty
                    txtClientBarcode.Focus()
                    Exit For
                End If
            End With
        Next

    End Sub
    ''' <summary>
    '''     客户条码
    ''' </summary>
    Sub SetClientBarcode()
        If txtM_Code.Text = String.Empty Then
            txtM_Code.Focus()
            Exit Sub
        End If

        '第二个客户条码输入是否重复
        Dim strCode As String = Trim(StrConv(UCase(Me.txtM_Code.Text), vbNarrow))
        Dim strClientBarcode As String = Trim(StrConv(UCase(Me.txtClientBarcode.Text), vbNarrow))
        Dim N As Integer
        For N = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            Dim strClientBarcodeA As String = IIf(IsDBNull(ds.Tables("SamplePaceBarCode").Rows(N)("ClientBarcode")), String.Empty, ds.Tables("SamplePaceBarCode").Rows(N)("ClientBarcode"))
            If strClientBarcode = strClientBarcodeA Then
                txtClientBarcode.Text = String.Empty
                txtClientBarcode.Focus()
                lblCode.Text = "条码重复"
                Exit Sub
            End If
        Next

        '此客戶條碼<采集表>存在
        Dim sclcom As New SampleCollectionControler
        If sclcom.SampleCollection_GetID(UCase(Me.txtClientBarcode.Text)) = True Then
            lblCode.Text = "客戶条码重复"
            txtClientBarcode.Text = String.Empty
            txtClientBarcode.Focus()
            MessageBox.Show(UCase(Me.txtClientBarcode.Text) + "此客戶條碼<采集表>存在!", "提示")
            Exit Sub
        End If

        'Dim strCode As String
        'Dim strQty As String
        'Dim intIn As Integer
        'Dim StrText As String
        'Dim strCtext As String

        'StrText = txtM_Code.Text
        'strCtext = Me.txtClientBarcode.Text

        'If txtClientBarcode.Text = String.Empty Then
        '    txtClientBarcode.Focus()
        '    Exit Sub
        'End If

        'intIn = InStr(StrText, ",", CompareMethod.Text)
        'If StrText = "" Then Exit Sub
        'If intIn <= 0 Then
        '    strCode = StrText
        '    strQty = "1"
        'Else
        '    strCode = Mid(StrText, 1, intIn - 1)
        '    strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
        'End If

        If strCode = String.Empty Then
            txtM_Code.Focus()
            lblCode.Text = "客戶条码重复"
        End If

        Dim row As DataRow
        row = ds.Tables("SamplePaceBarCode").NewRow
        row("Code_ID") = strCode
        row("ClientBarcode") = strClientBarcode
        row("SPID") = String.Empty
        row("Qty") = 1
        row("InCheck") = False
        row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
        ds.Tables("SamplePaceBarCode").Rows.Add(row)
        txtClientBarcode.Text = String.Empty

        '4.条码总数量---------------------------------------
        txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count

        '5.跳入客户条码输入---------------------------------
        'If Mid(cboCodeType.EditValue, 1, 3) = "[C]" Then
        If cboCodeType.SelectedIndex = 1 Then
            Me.txtM_Code.Text = String.Empty
            Me.txtM_Code.Focus()
        End If
    End Sub

    Sub BarCode()
        '1.条码以输入-重复-------------------------------
        lblCode.Text = String.Empty
        Dim strM_Code As String = Trim(StrConv(UCase(Me.txtM_Code.Text), vbNarrow))
        Dim i As Integer
        For i = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            If strM_Code = ds.Tables("SamplePaceBarCode").Rows(i)("Code_ID") Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "条码重复"
                Exit Sub
            End If
        Next

        '2.条码验证
        Dim sclist As New List(Of SampleCollectionInfo)
        sclist = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        If sclist.Count > 0 Then
            '2.1-----------------------------------------------存在客户条码
            If sclist(0).ClientBarcode <> String.Empty Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "存在客户条码"
                Exit Sub
            End If
            '2.2-----------------------------------------------订单不存在此条码
            If sclist(0).SO_ID <> gluSO_ID.EditValue Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "此订单不存在此条码"
                Exit Sub
            End If
            '2.3-----------------------------------------------条码是否可能再收发
            If sclist(0).StatusType <> String.Empty Then
                Dim stlist As New List(Of SampleTransactionInfo)
                stlist = stcon.SampleTransactionType_GetList(sclist(0).StatusType, Nothing)
                If (stlist(0).IsTransferred = False) And (gluType.EditValue <> "P07") Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "此条码已经" + stlist(0).StatusTypeName
                    Exit Sub
                End If
            End If
            '2.4-----------------------------------------------開料此條碼已经處理
            If gluType.EditValue = "P01" Then
                If sclist(0).StatusType <> String.Empty Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "此條碼狀態為" + sclist(0).StatusTypeName
                    Exit Sub
                End If
            End If
            '2.5-----------------------------------------------还入条码处理
            If gluType.EditValue = "P07" Then
                '2.5.1條碼是否存在這個單號里面
                Dim somlist As New List(Of SamplePaceInfo)
                somlist = pacon.SamplePaceBarCode_Getlist(Nothing, strM_Code, gluLoan.EditValue)
                If somlist.Count > 0 Then
                Else
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "此單號-条码不存在"
                    Exit Sub
                End If
                '2.5.2條碼是否存在這個單號里面
                If sclist(0).StatusType <> "N" Then
                    txtM_Code.Text = String.Empty
                    txtM_Code.Focus()
                    lblCode.Text = "此條碼不是借出狀態"
                    Exit Sub
                End If
            End If
            '2.6-----------------------------------------------采集表部门不存在"
            Dim saplist As New List(Of SamplePaceInfo)
            saplist = spCon.SamplePaceType_Getlist(gluType.EditValue)
            If saplist.Count > 0 Then
                If saplist(0).SE_OutVisible Then
                    If sclist(0).D_ID <> gluSE_OutD_ID.EditValue Then
                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        lblCode.Text = "采集表部门不存在此条码"
                        Exit Sub
                    End If
                End If
            End If
        Else
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            lblCode.Text = "条码不存在"
            Exit Sub
        End If


        ''2.采集表部门不存在"-------------------------
        'Dim sptlist As New List(Of SamplePaceInfo)
        'sptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
        'If sptlist.Count > 0 Then

        '    If sptlist(0).SE_OutVisible Then
        '        Dim scclist As New List(Of SampleCollectionInfo)
        '        scclist = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, gluSE_OutD_ID.EditValue, Nothing, Nothing, Nothing, Nothing)
        '        If scclist.Count > 0 Then
        '        Else
        '            txtM_Code.Text = String.Empty
        '            txtM_Code.Focus()
        '            lblCode.Text = "采集表部门不存在此条码"
        '            Exit Sub
        '        End If
        '    End If
        'End If

        ''3.条码是否存在采集表里面,是否存在客户条码------
        'Dim sclist As New List(Of SampleCollectionInfo)
        'sclist = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        'If sclist.Count > 0 Then
        '    If sclist(0).ClientBarcode <> String.Empty Then
        '        txtM_Code.Text = String.Empty
        '        txtM_Code.Focus()
        '        lblCode.Text = "存在客户条码"
        '        Exit Sub
        '    End If
        'Else
        '    txtM_Code.Text = String.Empty
        '    txtM_Code.Focus()
        '    lblCode.Text = "条码不存在"
        '    Exit Sub
        'End If

        ''4.条码是否是这个订单的--------------------------
        'Dim sclistm As New List(Of SampleCollectionInfo)
        'sclistm = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, gluSO_ID.EditValue, txtSS_Edition.Text, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        'If sclistm.Count > 0 Then
        'Else
        '    txtM_Code.Text = String.Empty
        '    txtM_Code.Focus()
        '    lblCode.Text = "此订单不存在此条码"
        '    Exit Sub
        'End If

        ''5.此条码是否可能再收发--------------------------
        'Dim scmlist As New List(Of SampleCollectionInfo)
        'scmlist = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        'If scmlist.Count > 0 Then
        '    If scmlist(0).StatusType <> String.Empty Then
        '        Dim stlist As New List(Of SampleTransactionInfo)
        '        stlist = stcon.SampleTransactionType_GetList(scmlist(0).StatusType, Nothing)
        '        If (stlist(0).IsTransferred = False) And (gluType.EditValue <> "P07") Then
        '            txtM_Code.Text = String.Empty
        '            txtM_Code.Focus()
        '            lblCode.Text = "此条码已经" + stlist(0).StatusTypeName
        '            Exit Sub
        '        End If
        '    End If
        'End If

        '6.開料此條碼已经處理---------------------------------
        'If gluType.EditValue = "P01" Then
        '    Dim scmlistm As New List(Of SampleCollectionInfo)
        '    scmlistm = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    If scmlistm.Count > 0 Then
        '        If scmlistm(0).StatusType <> String.Empty Then
        '            txtM_Code.Text = String.Empty
        '            txtM_Code.Focus()
        '            lblCode.Text = "此條碼狀態為" + scmlistm(0).StatusTypeName
        '            Exit Sub
        '        End If
        '    End If
        'End If
        '7.還入條碼處理---------------------------------
        'If gluType.EditValue = "P07" Then
        '    '7.1條碼是否存在這個單號里面
        '    Dim somlist As New List(Of SamplePaceInfo)
        '    somlist = pacon.SamplePaceBarCode_Getlist(Nothing, strM_Code, gluLoan.EditValue)
        '    If somlist.Count > 0 Then
        '    Else
        '        txtM_Code.Text = String.Empty
        '        txtM_Code.Focus()
        '        lblCode.Text = "此單號-条码不存在"
        '        Exit Sub
        '    End If
        '    '7.2條碼是否存在這個單號里面
        '    Dim scmlistm As New List(Of SampleCollectionInfo)
        '    scmlistm = Sccon.SampleCollection_Getlist(Nothing, strM_Code, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        '    If scmlistm.Count > 0 Then
        '        If scmlistm(0).StatusType <> "N" Then
        '            txtM_Code.Text = String.Empty
        '            txtM_Code.Focus()
        '            lblCode.Text = "此條碼不是借出狀態"
        '            Exit Sub
        '        End If
        '    End If
        'End If

        '8.条码是否正在使用中李超20140116加
        Dim somAlist As New List(Of SamplePaceInfo)
        somAlist = pacon.SamplePaceBarCode_Getlist2(strM_Code, Nothing)
        If somAlist.Count > 0 Then
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            lblCode.Text = "条码正在使用中,请查明原因"
            Exit Sub
        End If

        '9.跳入客户条码输入---------------------------------
        'If Mid(cboCodeType.EditValue, 1, 3) = "[C]" Then
        If cboCodeType.SelectedIndex = 1 Then
            Me.txtClientBarcode.Text = String.Empty
            Me.txtClientBarcode.Focus()
            Exit Sub
        End If

        '10.取入条码------------------------------------------
        'Dim strCode As String
        'Dim strQty As String
        'Dim intIn As Integer
        'Dim StrText As String
        'StrText = txtM_Code.Text
        'intIn = InStr(StrText, ",", CompareMethod.Text)
        'If StrText = "" Then Exit Sub

        'If intIn <= 0 Then
        '    strCode = StrText
        '    strQty = "1"
        'Else
        '    strCode = Mid(StrText, 1, intIn - 1)
        '    strQty = Mid(StrText, intIn + 1, Len(StrText) - intIn)
        'End If

        Dim row As DataRow
        row = ds.Tables("SamplePaceBarCode").NewRow
        row("Code_ID") = strM_Code
        row("SPID") = String.Empty
        row("Qty") = 1
        row("InCheck") = False
        row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
        ds.Tables("SamplePaceBarCode").Rows.Add(row)
        txtM_Code.Text = ""

        '11.条码输入总数量处理-------------------------------
        If ds.Tables("SamplePaceBarCode").Rows.Count > 0 Then
            txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
            cobSE_BarCodeType.Enabled = False
            'gluType.Enabled = False
        Else
            cobSE_BarCodeType.Enabled = True
            'gluType.Enabled = True
        End If
    End Sub


    '确认条码
    Sub SetBarCode()

        '1.不需要輸入確認條碼"-------------------------
        Dim sptlist As New List(Of SamplePaceInfo)
        sptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            If EditItem = EditEnumType.INCHECK And sptlist(0).SE_InCheckBarcode = False Then
                txtM_Code.Text = String.Empty
                txtM_Code.Focus()
                lblCode.Text = "不需要輸入確認條碼"
                Exit Sub
            End If
        End If

        Dim boolBarcode As Boolean = False
        Dim M As Integer
        Dim strBarCode As String
        Dim strClientBarcode As String
        Dim strCode As String = StrConv(UCase(txtM_Code.Text), vbNarrow)
        lblCode.Text = String.Empty
        For M = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
            With ds.Tables("SamplePaceBarCode")
                strClientBarcode = IIf(IsDBNull(.Rows(M)("ClientBarcode")), String.Empty, .Rows(M)("ClientBarcode"))
                If strClientBarcode <> String.Empty Then
                    If strCode = strClientBarcode Then
                        .Rows(M)("InCheck") = True
                        .Rows(M)("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                        .Rows(M)("InCheckQty") = 1

                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        boolBarcode = True
                        Exit Sub
                    End If
                Else
                    strBarCode = IIf(IsDBNull(.Rows(M)("Code_ID")), Nothing, .Rows(M)("Code_ID"))
                    If strCode = strBarCode Then
                        .Rows(M)("InCheck") = True
                        .Rows(M)("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                        .Rows(M)("InCheckQty") = 1
                        txtM_Code.Text = String.Empty
                        txtM_Code.Focus()
                        boolBarcode = True
                        Exit Sub
                    End If
                End If
            End With
        Next

        If boolBarcode = False Then
            lblCode.Text = "条码不存在"
            txtM_Code.Text = String.Empty
            txtM_Code.Focus()
            Exit Sub
        End If

    End Sub

#End Region

#Region "表格删除事件"
    Private Sub cmdDelSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelSub.Click
        If GridView9.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView9.GetRowCellDisplayText(GridView9.GetSelectedRows(0), "AutoID")

        If DelTemp <> String.Empty Then
            Dim row As DataRow = ds.Tables("DelSamplePaceBarCode").NewRow
            row("AutoID") = ds.Tables("SamplePaceBarCode").Rows(GridView9.FocusedRowHandle)("AutoID")
            ds.Tables("DelSamplePaceBarCode").Rows.Add(row)
        End If
        ds.Tables("SamplePaceBarCode").Rows.RemoveAt(GridView9.GetSelectedRows(0))
        If ds.Tables("SamplePaceBarCode").Rows.Count > 0 Then
            txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
            cobSE_BarCodeType.Enabled = False
            'gluType.Enabled = False
        Else
            cobSE_BarCodeType.Enabled = True
            'gluType.Enabled = True
        End If

    End Sub
#End Region

#Region "是否為空"
    ''' <summary>
    ''' 保存数据前处理
    ''' </summary>
    ''' <remarks></remarks>
    Function DataCheckEmpty() As Integer
        If gluSO_ID.Text = String.Empty Then
            MsgBox("订单编号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluSO_ID.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If
        If gluPM_M_Code.Text = String.Empty Then
            MsgBox("产品编号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluPM_M_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluM_Code.EditValue = String.Empty Or gluM_Code.Text = String.Empty Then
            MsgBox("產品類別不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluM_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If txtSS_Edition.Text = String.Empty Then
            MsgBox("版本号不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            txtSS_Edition.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluType.Text = String.Empty Then
            MsgBox("类型不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            gluType.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        If gluType.EditValue = "P06" Then '借出
            If gluReason.EditValue = String.Empty Then
                MsgBox("借出原因不能為空！", MsgBoxStyle.Information, "溫馨提示")
                gluReason.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
            If CInt(Me.txtTime.Value) <= 0 Then
                MsgBox("借出天数不能為0！", MsgBoxStyle.Information, "溫馨提示")
                txtTime.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        If gluType.EditValue = "PO7" Then '還入
            If gluLoan.EditValue = String.Empty Then
                MsgBox("借出單號不能為空！", MsgBoxStyle.Information, "溫馨提示")
                gluLoan.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If
        If gluPM_M_Code.Text <> gluM_Code.Text Then
            MsgBox("产品编号与产同类型不同！", MsgBoxStyle.Information, "溫馨提示")
            gluM_Code.Focus()
            DataCheckEmpty = 0
            Exit Function
        End If

        '確認收發料工序是否屬生這個產品的
        If gluOutPS_NO.EditValue <> String.Empty Then
            Dim Tsplist As New List(Of SampleProcessInfo)
            Tsplist = prcon.SampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, gluOutPS_NO.EditValue)
            If Tsplist.Count <= 0 Then
                MsgBox("此發料工序不屬于這個產品編號！", MsgBoxStyle.Information, "溫馨提示")
                gluOutPS_NO.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        If gluInPS_NO.EditValue <> String.Empty Then
            Dim TspAlist As New List(Of SampleProcessInfo)
            TspAlist = prcon.SampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, gluInPS_NO.EditValue)
            If TspAlist.Count <= 0 Then
                MsgBox("此收料工序不屬于這個產品編號！", MsgBoxStyle.Information, "溫馨提示")
                gluInPS_NO.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        ''權限控制
        'If gluType.EditValue = "P08" Then
        '    Dim pmws As New PermissionModuleWarrantSubController
        '    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
        '    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890408")
        '    If pmwiL.Count > 0 Then
        '        If pmwiL.Item(0).PMWS_Value = "否" Then
        '            MessageBox.Show("此用戶沒有完工的權限")
        '            gluType.EditValue = String.Empty
        '        End If
        '    Else
        '        MessageBox.Show("此用戶沒有完工的權限")
        '        gluType.EditValue = String.Empty
        '    End If
        'End If
        ''權限控制
        'If gluType.EditValue = "P09" Then
        '    Dim pmws As New PermissionModuleWarrantSubController
        '    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890407")
        '    If pmwiL.Count > 0 Then
        '        If pmwiL.Item(0).PMWS_Value = "否" Then
        '            MessageBox.Show("此用戶沒有退料的權限")
        '            gluType.EditValue = String.Empty
        '        End If
        '    Else
        '        MessageBox.Show("此用戶沒有退料的權限")
        '        gluType.EditValue = String.Empty
        '    End If
        'End If
        ''损坏控制
        'If gluType.EditValue = "P03" Then
        '    Dim pmws As New PermissionModuleWarrantSubController
        '    Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)

        '    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890411")
        '    If pmwiL.Count > 0 Then
        '        If pmwiL.Item(0).PMWS_Value = "否" Then
        '            MessageBox.Show("此用戶沒有损坏的權限")
        '            gluType.EditValue = String.Empty
        '        End If
        '    Else
        '        MessageBox.Show("此用戶沒有损坏的權限")
        '        gluType.EditValue = String.Empty
        '    End If
        'End If
        '权限处理---------------------------------------------------
        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then
            Dim BoolPermission As Boolean = True
            Dim pmws As New PermissionModuleWarrantSubController
            Dim pmwiL As List(Of PermissionModuleWarrantSubInfo)
            Select Case gluType.EditValue
                Case "P01"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890409")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
                Case "P03"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890411")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
                Case "P08"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890408")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
                Case "P09"
                    pmwiL = pmws.PermissionModuleWarrantSub_GetList(InUserID, "890407")
                    If pmwiL.Count > 0 Then
                        If pmwiL.Item(0).PMWS_Value = "否" Then
                            BoolPermission = False
                        End If
                    Else
                        BoolPermission = False
                    End If
            End Select
            If BoolPermission = False Then
                MessageBox.Show("此用戶沒有" + gluType.Text + "的權限", "权限提示")
                gluType.EditValue = String.Empty
                Exit Function
            End If
        End If

        Dim sptlistA As New List(Of SamplePaceInfo)
        sptlistA = spCon.SamplePaceType_Getlist(gluType.EditValue)
        If sptlistA.Count > 0 Then
            If sptlistA(0).SE_OutVisible Then
                If gluSE_OutD_ID.EditValue = String.Empty Then
                    MsgBox("发料部门不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluSE_OutD_ID.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If

                If gluOutPS_NO.EditValue = String.Empty Then
                    MsgBox("发料工序不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluOutPS_NO.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If

            If sptlistA(0).SE_InVisible Then
                If gluSE_InD_ID.EditValue = String.Empty Then
                    MsgBox("收料部门不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluSE_InD_ID.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If

                If gluInPS_NO.EditValue = String.Empty Then
                    MsgBox("收料工序不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluInPS_NO.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If

            If sptlistA(0).SE_OutVisible And sptlistA(0).SE_InVisible Then
                If (gluSE_OutD_ID.EditValue = gluSE_InD_ID.EditValue) And (gluOutPS_NO.EditValue = gluInPS_NO.EditValue) Then
                    MsgBox("部門與工序同時相同,请重新输入！", MsgBoxStyle.Information, "溫馨提示")
                    gluInPS_NO.EditValue = String.Empty
                    gluInPS_NO.Focus()
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If

        End If

        If ds.Tables("SamplePaceBarCode").Rows.Count = 0 Then
            MsgBox("子檔不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
            DataCheckEmpty = 0
            Exit Function
        End If

        ''是否刷卡
        If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.INCHECK Or EditItem = EditEnumType.EDIT Then
            If txtCardID.EditValue = String.Empty Then
                MsgBox("需要刷卡！", MsgBoxStyle.Information, "溫馨提示")
                txtCardID.Focus()
                DataCheckEmpty = 0
                Exit Function
            End If
        End If

        '确认收料是输入条码
        Dim m As Integer
        'If EditItem = EditEnumType.INCHECK And gluType.EditValue <> "P06" And gluType.EditValue <> "P07" Then
        '    '是否需求输入确认条码
        '    Dim boolInQty As Boolean = False
        '    Dim sptlistB As New List(Of SamplePaceInfo)
        '    sptlistB = spCon.SamplePaceType_Getlist(gluType.EditValue)
        '    If sptlistB.Count > 0 Then
        '        If sptlistB(0).SE_OutVisible = False Then
        '            boolInQty = True
        '        End If
        '        If sptlistB(0).SE_InVisible = False Then
        '            boolInQty = True
        '        End If
        '    End If

        '    If boolInQty = False Then
        '        Dim strBarCode = True
        '        For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
        '            With ds.Tables("SamplePaceBarCode")
        '                strBarCode = IIf(IsDBNull(.Rows(m)("InCheck")), False, .Rows(m)("InCheck"))
        '                If strBarCode = False Then
        '                    MsgBox("条码没输入,请输入收料条码！", MsgBoxStyle.Information, "溫馨提示")
        '                    Grid1.Focus()
        '                    GridView9.FocusedRowHandle = m
        '                    DataCheckEmpty = 0
        '                    Exit Function
        '                End If
        '            End With
        '        Next
        '    End If
        'End If

        If EditItem = EditEnumType.INCHECK Then
            Dim sptlistZ As New List(Of SamplePaceInfo)
            sptlistZ = spCon.SamplePaceType_Getlist(gluType.EditValue)
            If sptlistZ.Count > 0 Then
                '3.1是否要輸入確認條碼
                If sptlistZ(0).SE_InCheckBarcode Then
                    Dim strBarCode = True
                    For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                        With ds.Tables("SamplePaceBarCode")
                            strBarCode = IIf(IsDBNull(.Rows(m)("InCheck")), False, .Rows(m)("InCheck"))
                            If strBarCode = False Then
                                MsgBox("条码没输入,请输入收料条码！", MsgBoxStyle.Information, "溫馨提示")
                                Grid1.Focus()
                                GridView9.FocusedRowHandle = m
                                DataCheckEmpty = 0
                                Exit Function
                            End If
                        End With
                    Next
                End If
            End If
        End If


        '确认收料是输入条码
        Dim IntClientBarCode As Integer = 0
        Dim IntTableRowCount As Integer = ds.Tables("SamplePaceBarCode").Rows.Count
        For m = 0 To IntTableRowCount - 1
            With ds.Tables("SamplePaceBarCode")
                Dim strBarCode As String = IIf(IsDBNull(.Rows(m)("Code_ID")), String.Empty, .Rows(m)("Code_ID"))
                Dim strClientBarcode As String = IIf(IsDBNull(.Rows(m)("ClientBarcode")), String.Empty, .Rows(m)("ClientBarcode"))
                '1.--------------------------------------------------------条码验证
                Dim sclist As New List(Of SampleCollectionInfo)
                sclist = Sccon.SampleCollection_Getlist(Nothing, strBarCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                If sclist.Count > 0 Then
                    '1.1---------------------------------------------------条码是否存在采集表里面,是否存在客户条码
                    If sclist(0).ClientBarcode <> String.Empty Then
                        MsgBox("存在客户条码！", MsgBoxStyle.Information, "溫馨提示")
                        Grid1.Focus()
                        GridView9.FocusedRowHandle = m
                        Exit Function
                    End If
                    '1.2----------------------------------------------------此订单不存在此条码
                    If gluSO_ID.EditValue <> sclist(0).SO_ID Then
                        MsgBox("此订单不存在此条码！", MsgBoxStyle.Information, "溫馨提示")
                        Grid1.Focus()
                        GridView9.FocusedRowHandle = m
                        Exit Function
                    End If
                    '1.3----------------------------------------------------采集表部门,不存在此条码
                    Dim splist As New List(Of SamplePaceInfo)
                    splist = spCon.SamplePaceType_Getlist(gluType.EditValue)
                    If splist.Count > 0 Then
                        If splist(0).SE_OutVisible Then
                            If gluSE_OutD_ID.EditValue <> sclist(0).D_ID Then
                                MsgBox("集表部门不存在此条码！", MsgBoxStyle.Information, "溫馨提示")
                                Grid1.Focus()
                                GridView9.FocusedRowHandle = m
                                Exit Function
                            End If
                        End If
                    End If
                    '1.4----------------------------------------------------此条码已经使用
                    If sclist(0).StatusType <> String.Empty Then
                        Dim stlist As New List(Of SampleTransactionInfo)
                        stlist = stcon.SampleTransactionType_GetList(sclist(0).StatusType, Nothing)
                        If (stlist(0).IsTransferred = False) And (gluType.EditValue <> "P07") Then
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = m
                            lblCode.Text = "此条码已经" + stlist(0).StatusTypeName
                            Exit Function
                        End If
                    End If
                    '1.5--------------------------------------------------開料此條碼已经處理
                    If gluType.EditValue = "P01" Then
                        If sclist(0).StatusType <> String.Empty Then
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = m
                            lblCode.Text = "此條碼狀態為" + sclist(0).StatusTypeName
                            Exit Function
                        End If
                    End If
                    '1.6---------------------------------------------------'5.還入條碼處理---------------------------------
                    If gluType.EditValue = "P07" Then
                        '1.6.1條碼是否存在這個單號里面
                        Dim somlist As New List(Of SamplePaceInfo)
                        somlist = pacon.SamplePaceBarCode_Getlist(Nothing, strBarCode, gluLoan.EditValue)
                        If somlist.Count > 0 Then
                        Else
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = m
                            lblCode.Text = "此單號-条码不存在"
                            Exit Function
                        End If
                        '1.6.1條碼是否存在這個單號里面
                        If sclist(0).StatusType <> "N" Then
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = m
                            lblCode.Text = "此條碼不是借出狀態"
                            Exit Function
                        End If
                        '1.6.3借出人员和还入人员是否相同20140121新增
                        If txtCardID.Text <> StrSE_OutCardID Then
                            Grid1.Focus()
                            GridView9.FocusedRowHandle = m
                            MsgBox("借出人员和还入人员不相同！", MsgBoxStyle.Information, "溫馨提示")
                            Exit Function
                        End If
                    End If

                Else
                    MsgBox("条码不存在！", MsgBoxStyle.Information, "溫馨提示")
                    Grid1.Focus()
                    GridView9.FocusedRowHandle = m
                    Exit Function
                End If

                ''1.采集表部门,不存在此条码--------------------------
                'Dim splist As New List(Of SamplePaceInfo)
                'splist = spCon.SamplePaceType_Getlist(gluType.EditValue)
                'If splist.Count > 0 Then
                '    If splist(0).SE_OutVisible Then
                '        Dim scclist As New List(Of SampleCollectionInfo)
                '        scclist = Sccon.SampleCollection_Getlist(Nothing, strBarCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, gluSE_OutD_ID.EditValue, Nothing, Nothing, Nothing, Nothing)
                '        If scclist.Count > 0 Then
                '        Else
                '            MsgBox("集表部门不存在此条码！", MsgBoxStyle.Information, "溫馨提示")
                '            Grid1.Focus()
                '            GridView9.FocusedRowHandle = m
                '            Exit Function
                '        End If
                '    End If
                'End If

                ''2.条码是否存在采集表里面,是否存在客户条码----------
                'Dim sclist As New List(Of SampleCollectionInfo)
                'sclist = Sccon.SampleCollection_Getlist(Nothing, strBarCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                'If sclist.Count > 0 Then
                '    If sclist(0).ClientBarcode <> String.Empty Then
                '        MsgBox("存在客户条码！", MsgBoxStyle.Information, "溫馨提示")
                '        Grid1.Focus()
                '        GridView9.FocusedRowHandle = m
                '        Exit Function
                '    End If
                'Else
                '    MsgBox("条码不存在！", MsgBoxStyle.Information, "溫馨提示")
                '    Grid1.Focus()
                '    GridView9.FocusedRowHandle = m
                '    Exit Function
                'End If
                '3.条码是否是当前订单------------------------------
                'Dim sclistm As New List(Of SampleCollectionInfo)
                'sclistm = Sccon.SampleCollection_Getlist(Nothing, strBarCode, Nothing, gluSO_ID.EditValue, txtSS_Edition.Text, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                'If sclistm.Count > 0 Then
                'Else
                '    Grid1.Focus()
                '    GridView9.FocusedRowHandle = m
                '    lblCode.Text = "此订单不存在此条码"
                '    Exit Function
                'End If

                '2.此客戶條碼<采集表>存在---------------------------
                If strClientBarcode <> String.Empty Then
                    '2.1此客戶條碼<采集表>存在!
                    If Sccon.SampleCollection_GetID(UCase(strClientBarcode)) = True Then
                        MessageBox.Show(UCase(strClientBarcode) + "此客戶條碼<采集表>存在!", "提示")
                        Grid1.Focus()
                        GridView9.FocusedRowHandle = m
                        Exit Function
                    End If
                    '2.2客户条码是否全部有
                    IntClientBarCode = IntClientBarCode + 1
                End If
                ''5.還入條碼處理---------------------------------
                'If gluType.EditValue = "P07" Then
                '    '6.1條碼是否存在這個單號里面
                '    Dim somlist As New List(Of SamplePaceInfo)
                '    somlist = pacon.SamplePaceBarCode_Getlist(Nothing, strBarCode, gluLoan.EditValue)
                '    If somlist.Count > 0 Then
                '    Else
                '        Grid1.Focus()
                '        GridView9.FocusedRowHandle = m
                '        lblCode.Text = "此單號-条码不存在"
                '        Exit Function
                '    End If
                '    '5.1條碼是否存在這個單號里面
                '    Dim scmlistm As New List(Of SampleCollectionInfo)
                '    scmlistm = Sccon.SampleCollection_Getlist(Nothing, strBarCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                '    If scmlistm.Count > 0 Then
                '        If scmlistm(0).StatusType <> "N" Then
                '            Grid1.Focus()
                '            GridView9.FocusedRowHandle = m
                '            lblCode.Text = "此條碼不是借出狀態"
                '            Exit Function
                '        End If
                '    End If
                '    '5.3借出人员和还入人员是否相同20140121新增
                '    If txtCardID.Text <> StrSE_OutCardID Then
                '        Grid1.Focus()
                '        GridView9.FocusedRowHandle = m
                '        MsgBox("借出人员和还入人员不相同！", MsgBoxStyle.Information, "溫馨提示")
                '        Exit Function
                '    End If
                'End If


                ''6.此条码是否可能再收发----------------------------
                'Dim scmlist As New List(Of SampleCollectionInfo)
                'scmlist = Sccon.SampleCollection_Getlist(Nothing, strBarCode, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
                'If scmlist.Count > 0 Then
                '    If scmlist(0).StatusType <> String.Empty Then
                '        Dim stlist As New List(Of SampleTransactionInfo)
                '        stlist = stcon.SampleTransactionType_GetList(scmlist(0).StatusType, Nothing)
                '        If (stlist(0).IsTransferred = False) And (gluType.EditValue = "P07") Then
                '            Grid1.Focus()
                '            GridView9.FocusedRowHandle = m
                '            lblCode.Text = "此条码已经" + stlist(0).StatusTypeName
                '        End If
                '    End If
                'End If

                '4.条码是否正在使用中李超20140116加
                Dim somAlist As New List(Of SamplePaceInfo)
                somAlist = pacon.SamplePaceBarCode_Getlist2(strBarCode, LabSE_NO.Text)
                If somAlist.Count > 0 Then
                    Grid1.Focus()
                    GridView9.FocusedRowHandle = m
                    lblCode.Text = "条码正在使用中,请查明原因"
                    Exit Function
                End If

            End With
        Next

        '1.输入客户条码时一定要
        If (IntTableRowCount - IntClientBarCode = 0) Or IntClientBarCode = 0 Then
        Else
            MsgBox("有" + CStr(IntTableRowCount - IntClientBarCode) + "条客户条码没有输入！", MsgBoxStyle.Information, "溫馨提示")
            Exit Function
        End If

        '------------------------------------------------------------扣账库存查询---李超20131217修正
        Dim sptlist As New List(Of SamplePaceInfo)
        sptlist = spCon.SamplePaceType_Getlist(gluType.EditValue)
        If sptlist.Count > 0 Then
            If sptlist(0).SE_OutVisible Then
                Dim SwInfo As New SampleWareInventoryInfo
                Dim SwList As New List(Of SampleWareInventoryInfo)
                Dim intinQty As Integer = 0
                SwList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)
                If SwList.Count > 0 Then
                    intinQty = SwList(0).SWI_Qty
                End If
                If intinQty < CInt(txtOutInQty.Text) Then
                    MsgBox("发料時庫存数量小于發料數量！", MsgBoxStyle.Information, "溫馨提示")
                    DataCheckEmpty = 0
                    Exit Function
                End If
            End If
        End If

        DataCheckEmpty = 1
    End Function
#End Region

#Region "控件事件"

    Private Sub gluOutPS_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluOutPS_NO.EditValueChanged
        If gluOutPS_NO.EditValue <> String.Empty Then
            Dim SwList As New List(Of SampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)

            If SwList.Count > 0 Then
                lblQtyOut.Text = SwList(0).SWI_Qty
            Else
                lblQtyOut.Text = 0
            End If
            '-----------------------------------------
            If gluType.EditValue = "P06" Then
                gluInPS_NO.EditValue = gluOutPS_NO.EditValue
            End If

        End If
    End Sub

    Private Sub gluInPS_NO_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluInPS_NO.EditValueChanged
        If gluInPS_NO.EditValue <> String.Empty Then
            Dim SwList As New List(Of SampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, gluInPS_NO.EditValue, Nothing, False, gluSE_InD_ID.EditValue)

            If SwList.Count > 0 Then
                lblQtyIn.Text = SwList(0).SWI_Qty
            Else
                lblQtyIn.Text = 0
            End If
        End If
    End Sub

    Private Sub gluSE_OutD_ID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSE_OutD_ID.EditValueChanged
        If gluSE_OutD_ID.EditValue <> String.Empty Then

            Dim SwList As New List(Of SampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, gluOutPS_NO.EditValue, Nothing, False, gluSE_OutD_ID.EditValue)

            If SwList.Count > 0 Then
                lblQtyOut.Text = SwList(0).SWI_Qty
            Else
                lblQtyOut.Text = 0
            End If

        End If
    End Sub

    Private Sub gluSE_InD_ID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSE_InD_ID.EditValueChanged
        If gluSE_InD_ID.EditValue <> String.Empty Then

            Dim SwList As New List(Of SampleWareInventoryInfo)
            Dim intinQty As Integer = 0
            SwList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, gluInPS_NO.EditValue, Nothing, False, gluSE_InD_ID.EditValue)

            If SwList.Count > 0 Then
                lblQtyIn.Text = SwList(0).SWI_Qty
            Else
                lblQtyIn.Text = 0
            End If
        End If
    End Sub

    Private Sub gluM_Code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluM_Code.EditValueChanged

        If gluM_Code.EditValue <> String.Empty Then
            Dim splist As New List(Of SampleProcessInfo)
            splist = prcon.SampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, True, Nothing)

            'gluOutPS_NO.Properties.ValueMember = "PS_NO"
            'gluOutPS_NO.Properties.DisplayMember = "PS_Name"
            gluOutPS_NO.Properties.DataSource = splist

            'gluInPS_NO.Properties.ValueMember = "PS_NO"
            'gluInPS_NO.Properties.DisplayMember = "PS_Name"
            gluInPS_NO.Properties.DataSource = splist

            SetPSDataSource()
        End If
    End Sub

    Private Sub txtPK_CodeID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPK_CodeID.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim strPK_CodeID As String = txtPK_CodeID.Text
            Dim strSealCode_ID As String = txtSealCode_ID.Text
            Dim strPK_ID As String = String.Empty
            If strSealCode_ID = String.Empty Then
                MessageBox.Show("封条沒有不能为空", "提示")
                txtSealCode_ID.Text = String.Empty
                Exit Sub
            End If

            Dim pklist As New List(Of SamplePackingInfo)
            pklist = spkcon.SamplePacking_GetList(Nothing, Nothing, Nothing, strPK_CodeID, Nothing, Nothing, Nothing, GetEditValue)
            If pklist.Count > 0 Then
                If pklist(0).CheckBit = False Then
                    MessageBox.Show(strPK_CodeID + "：此装箱条码没有审核", "提示")
                    txtPK_CodeID.Text = String.Empty
                    Exit Sub
                End If
                If pklist(0).BitNeed = True Then
                    MessageBox.Show(strPK_CodeID + "：此装箱条码没有已经使用,不能再使用！", "提示")
                    txtPK_CodeID.Text = String.Empty
                    Exit Sub
                End If
                If pklist(0).SealCode_ID <> strSealCode_ID Then
                    MessageBox.Show(strSealCode_ID + "：此封条不属于此单", "提示")
                    txtSealCode_ID.Text = String.Empty
                    txtSealCode_ID.Focus()
                    Exit Sub
                End If
                strPK_ID = pklist(0).PK_ID
            Else
                MessageBox.Show("条码不存在或者此单没有装箱条码", "提示")
                txtPK_CodeID.Text = String.Empty
                Exit Sub
            End If

            If strPK_ID <> String.Empty Then
                '1.清空
                Dim m As Integer
                For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                    With ds.Tables("SamplePaceBarCode")
                        .Rows(m)("InCheck") = False
                        .Rows(m)("InCheckDate") = Nothing
                        .Rows(m)("InCheckQty") = 0
                    End With
                Next

                '2'填入確認條碼
                Dim spklist As New List(Of SamplePackingInfo)
                spklist = spkcon.SamplePackingSub_GetList(Nothing, strPK_ID, Nothing, Nothing)
                If spklist.Count > 0 Then
                    Dim i As Integer
                    Dim StrCode As String = String.Empty
                    For i = 0 To spklist.Count - 1
                        StrCode = spklist(i).Code_ID
                        Dim strBarCode As String = String.Empty
                        For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                            With ds.Tables("SamplePaceBarCode")
                                strBarCode = IIf(IsDBNull(.Rows(m)("Code_ID")), Nothing, .Rows(m)("Code_ID"))
                                If StrCode = strBarCode Then
                                    .Rows(m)("InCheck") = True
                                    .Rows(m)("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                                    .Rows(m)("InCheckQty") = 1
                                End If
                            End With
                        Next
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub gluLoan_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluLoan.EditValueChanged
        If gluType.EditValue = "P07" And (EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT) Then
            gluSE_InD_ID.EditValue = GridView10.GetFocusedRowCellValue("SE_OutD_ID")
            gluSE_OutD_ID.EditValue = GridView10.GetFocusedRowCellValue("SE_InD_ID")

            gluInPS_NO.EditValue = GridView10.GetFocusedRowCellValue("SE_OutPS_NO")
            gluOutPS_NO.EditValue = GridView10.GetFocusedRowCellValue("SE_InPS_NO")
            StrSE_OutCardID = GridView10.GetFocusedRowCellValue("SE_OutCardID") '借出人员
        End If
    End Sub

    Private Sub gluSE_OutD_ID_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gluSE_OutD_ID.Leave
        SetPSDataSource()
    End Sub

    Private Sub SetPSDataSource()
        '--------------------------------------------處理工序有數量的帶出
        If gluPM_M_Code.EditValue <> String.Empty And Me.gluM_Code.EditValue <> String.Empty And gluSE_OutD_ID.EditValue <> String.Empty Then
            Dim splist As New List(Of SampleProcessInfo)
            splist = prcon.SampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, Nothing, Nothing)
            If splist.Count > 0 Then
                Dim i As Integer = 0

                ds.Tables("SamplePS").Clear()
                For i = 0 To splist.Count - 1
                    Dim SwsList As New List(Of SampleWareInventoryInfo)
                    SwsList = SwCon.SampleWareInventory_Getlist(Nothing, Nothing, splist(i).PS_NO, Nothing, False, gluSE_OutD_ID.EditValue)
                    If SwsList.Count > 0 Then
                        If SwsList(0).SWI_Qty > 0 Then ''李超修改
                            Dim row As DataRow
                            row = ds.Tables("SamplePS").NewRow
                            row("PS_Num") = splist(i).PS_Num
                            row("PS_NO") = splist(i).PS_NO
                            row("PS_Name") = splist(i).PS_Name
                            row("PS_NoName") = splist(i).PS_NoName
                            ds.Tables("SamplePS").Rows.Add(row)
                        End If
                    End If
                Next
                gluOutPS_NO.Properties.DataSource = ds.Tables("SamplePS")
            End If
        End If
    End Sub
#End Region

#Region "自动流水单号"
    Function GetSE_ID() As String
        Dim oc As New SamplePaceControler
        Dim oi As New SamplePaceInfo
        Dim StrSE As String
        StrSE = "SE" & Format(Now, "yyMM")
        oi = oc.SamplePace_GetID(StrSE)
        If oi Is Nothing Then
            GetSE_ID = "SE" + Format(Now, "yyMM") + "0001"
        Else
            GetSE_ID = "SE" + Format(Now, "yyMM") + Mid(CStr(CInt(Mid(oi.SE_ID, 7)) + 1000000001), 6)
        End If
    End Function
#End Region

#Region "刷卡事件"
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        txtCardID.Text = ReadCard() '讀取卡號李超
        If txtCardID.Text <> "" Then
            'chkFP_OutOK.Checked = True
        End If
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Select Case InUserID
            Case "13021010"
                txtCardID.Text = "13021010"
                'Case "0907201"
                '    txtCardID.Text = "0907201"
                'Case "00097"
                '    txtCardID.Text = "00097"
        End Select
    End Sub
#End Region

    Private Sub cobSE_BarCodeType_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cobSE_BarCodeType.EditValueChanged
        Select Case cobSE_BarCodeType.SelectedIndex
            Case 0
                Me.txtOutInQty.Enabled = False
                Me.cmdAutoCode.Enabled = False
                Me.txtM_Code.Enabled = True
                Me.cmdAutoCode.Text = "可扫描条码输入"
            Case 1
                If EditItem = EditEnumType.OUT Or EditItem = EditEnumType.EDIT Then
                    Me.txtOutInQty.Enabled = True
                    Me.txtM_Code.Enabled = False
                    Me.cmdAutoCode.Enabled = True
                    Me.cmdAutoCode.Text = "自动产生条码.."
                End If
        End Select
    End Sub

    Private Sub cmdAutoCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAutoCode.Click
        If cmdAutoCode.Text = "确认条码数量" Then
            '1.清空确认条码
            Dim m As Integer
            For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                With ds.Tables("SamplePaceBarCode")
                    .Rows(m)("InCheck") = False
                    .Rows(m)("InCheckDate") = Nothing
                    .Rows(m)("InCheckQty") = 0
                End With
            Next

            '2'填入確認條碼
            Dim somlist As New List(Of SamplePaceInfo)
            somlist = spCon.SamplePaceBarCode_Getlist(Nothing, Nothing, EditSE_ID)
            If somlist.Count = 0 Then
                Exit Sub
            Else

                For m = 0 To ds.Tables("SamplePaceBarCode").Rows.Count - 1
                    With ds.Tables("SamplePaceBarCode")
                        Dim strBarCode As String = IIf(IsDBNull(.Rows(m)("ClientBarcode")), String.Empty, .Rows(m)("ClientBarcode"))
                        If strBarCode <> String.Empty Then
                            MsgBox("客户条码只能手动确认！", MsgBoxStyle.Information, "溫馨提示")
                            Exit Sub
                        End If
                        .Rows(m)("InCheck") = True
                        .Rows(m)("InCheckDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                        .Rows(m)("InCheckQty") = 1
                    End With
                Next
            End If

        Else
            '0.收发部门工序不能为空
            Dim sptlistA As New List(Of SamplePaceInfo)
            sptlistA = spCon.SamplePaceType_Getlist(gluType.EditValue)
            If sptlistA.Count > 0 Then
                If sptlistA(0).SE_OutVisible Then
                    If gluSE_OutD_ID.EditValue = String.Empty Then
                        MsgBox("发料部门不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                        gluSE_OutD_ID.Focus()
                        Exit Sub
                    End If

                    If gluOutPS_NO.EditValue = String.Empty Then
                        MsgBox("发料工序不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                        gluOutPS_NO.Focus()
                        Exit Sub
                    End If
                End If

                If sptlistA(0).SE_InVisible Then
                    If gluSE_InD_ID.EditValue = String.Empty Then
                        MsgBox("收料部门不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                        gluSE_InD_ID.Focus()
                        Exit Sub
                    End If

                    If gluInPS_NO.EditValue = String.Empty Then
                        MsgBox("收料工序不能为空,请输入！", MsgBoxStyle.Information, "溫馨提示")
                        gluInPS_NO.Focus()
                        Exit Sub
                    End If
                End If
                If sptlistA(0).SE_OutVisible And sptlistA(0).SE_InVisible Then
                    If (gluSE_OutD_ID.EditValue = gluSE_InD_ID.EditValue) And (gluOutPS_NO.EditValue = gluInPS_NO.EditValue) Then
                        MsgBox("部門與工序同時相同,请重新输入！", MsgBoxStyle.Information, "溫馨提示")
                        gluInPS_NO.EditValue = String.Empty
                        gluInPS_NO.Focus()
                        Exit Sub
                    End If
                End If
            End If

            '1数量为0不可以自动产生条码
            If CInt(txtOutInQty.Text) = 0 Then
                MsgBox("自动产生条码数量不能为0,请您输入数量！", MsgBoxStyle.Information, "提示")
                txtOutInQty.Focus()
                Exit Sub
            End If
            '2相关类型是否需要输入条码
            Dim sptylist As New List(Of SamplePaceInfo)
            sptylist = spCon.SamplePaceType_Getlist(gluType.EditValue)
            If sptylist.Count > 0 Then
                If sptylist(0).SE_BarCodeAuto = False Then
                    MsgBox(gluType.Text + "类型不可以自动产生条码！", MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If
            End If

            '3条码自动产生
            ds.Tables("SamplePaceBarCode").Clear()
            Select Case gluType.EditValue
                Case "P01" '开料--------------------------------------------------------------
                    '3.0此工序是否有权限自动产生条码
                    Dim splist As New List(Of SampleProcessInfo)
                    splist = prcon.SampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, Nothing, gluInPS_NO.EditValue)
                    If splist.Count > 0 Then
                        If splist(0).PS_BarCodeBit = False Then
                            MsgBox(gluInPS_NO.Text + "工序不可以自动产生条码！", MsgBoxStyle.Information, "权限提示")
                            gluInPS_NO.EditValue = String.Empty
                            gluInPS_NO.Focus()
                            Exit Sub
                        End If
                    End If
                    '3.1的开料的情况下条码是不需要部门与条码状态
                    Dim GetSO_ID As String = gluSO_ID.EditValue
                    Dim scmlistm As New List(Of SampleCollectionInfo)
                    Dim i As Integer = 0
                    Dim SetBarCodeCount As Integer = 0

                    scmlistm = Sccon.SampleCollection_Getlist(Nothing, Nothing, Nothing, GetSO_ID, Nothing, Nothing, False, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)
                    For i = 0 To scmlistm.Count - 1
                        '3.2是不是没有开过料
                        If scmlistm(i).StatusType = String.Empty Then
                            Dim somAlist As New List(Of SamplePaceInfo)
                            '3.3没有使用过的
                            somAlist = pacon.SamplePaceBarCode_Getlist2(scmlistm(i).Code_ID, Nothing)
                            If somAlist.Count = 0 Then
                                Dim row As DataRow
                                row = ds.Tables("SamplePaceBarCode").NewRow
                                row("Code_ID") = scmlistm(i).Code_ID
                                row("SPID") = String.Empty
                                row("Qty") = 1
                                row("InCheck") = False
                                row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                                ds.Tables("SamplePaceBarCode").Rows.Add(row)
                                SetBarCodeCount = SetBarCodeCount + 1
                                If SetBarCodeCount = CInt(txtOutInQty.Text) Then
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If SetBarCodeCount < CInt(txtOutInQty.Text) Then
                        If SetBarCodeCount = 0 Then
                            MsgBox("没有自动条码可以产生,请您采集自动条码！", MsgBoxStyle.Information, "提示")
                            Exit Sub
                        End If
                        If MsgBox("只能自动产生" + CStr(SetBarCodeCount) + "个,是否继续执行", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                            If ds.Tables("SamplePaceBarCode").Rows.Count > 0 Then
                                txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
                                cobSE_BarCodeType.Enabled = False
                                'gluType.Enabled = False
                            Else
                                cobSE_BarCodeType.Enabled = True
                                'gluType.Enabled = True
                            End If
                        Else
                            ds.Tables("SamplePaceBarCode").Clear() '否清空表内数据
                        End If
                    End If
                    gluSE_InD_ID.Enabled = False
                    gluInPS_NO.Enabled = False
                Case Else '其他--------------------------------------------------------------
                    If CInt(txtOutInQty.Text) > CInt(lblQtyOut.Text) Then
                        MsgBox("部门工序数据小于需要自动产生的数量！", MsgBoxStyle.Information, "提示")
                        txtOutInQty.Focus()
                        Exit Sub
                    End If
                    '2.0雷射部等特别处理-------------------------
                    Dim boolType As Boolean = False
                    If cboCodeType.SelectedIndex = 1 Then
                        Dim pmlist As New List(Of PersonnelInfo) '部門分享
                        pmlist = pncon.FacBriSearch_GetListA(Nothing, Nothing, gluSE_OutD_ID.EditValue, Nothing, "SF")
                        If pmlist.Count > 0 Then
                            boolType = True
                        End If
                    End If

                    '3.0此工序是否有权限自动产生条码
                    If boolType = False Then
                        Dim splist As New List(Of SampleProcessInfo)
                        splist = prcon.SampleProcessMain_GetList(Nothing, gluPM_M_Code.EditValue, Nothing, Me.gluM_Code.EditValue, Nothing, Nothing, gluOutPS_NO.EditValue)
                        If splist.Count > 0 Then
                            If splist(0).PS_BarCodeBit = False Then
                                MsgBox(gluOutPS_NO.Text + "工序不可以自动产生条码！", MsgBoxStyle.Information, "权限提示")
                                gluOutPS_NO.EditValue = String.Empty
                                gluOutPS_NO.Focus()
                                Exit Sub
                            End If
                        End If
                    End If


                    Dim GetSO_ID As String = gluSO_ID.EditValue
                    Dim GetD_ID As String = gluSE_OutD_ID.EditValue
                    Dim scmlistm As New List(Of SampleCollectionInfo)
                    Dim i As Integer = 0
                    Dim SetBarCodeCount As Integer = 0

                    scmlistm = Sccon.SampleCollection_Getlist(Nothing, Nothing, Nothing, GetSO_ID, Nothing, Nothing, False, Nothing, Nothing, Nothing, GetD_ID, Nothing, Nothing, Nothing, True)
                    For i = 0 To scmlistm.Count - 1
                        If scmlistm(i).StatusType = "Z" Then
                            Dim somAlist As New List(Of SamplePaceInfo)
                            '3.3没有使用过的
                            somAlist = pacon.SamplePaceBarCode_Getlist2(scmlistm(i).Code_ID, Nothing)
                            If somAlist.Count = 0 Then
                                Dim row As DataRow
                                row = ds.Tables("SamplePaceBarCode").NewRow
                                row("Code_ID") = scmlistm(i).Code_ID
                                row("SPID") = String.Empty
                                row("Qty") = 1
                                row("InCheck") = False
                                row("SE_AddDate") = Format(Now, "yyyy-MM-dd HH:mm:ss")
                                ds.Tables("SamplePaceBarCode").Rows.Add(row)
                                SetBarCodeCount = SetBarCodeCount + 1
                                If SetBarCodeCount = CInt(txtOutInQty.Text) Then
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If SetBarCodeCount < CInt(txtOutInQty.Text) Then
                        If SetBarCodeCount = 0 Then
                            MsgBox("没有自动条码可以产生,请您采集自动条码！", MsgBoxStyle.Information, "提示")
                            Exit Sub
                        End If
                        If MsgBox("此部门只能自动产生" + CStr(SetBarCodeCount) + "个条码,是否继续执行", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            If ds.Tables("SamplePaceBarCode").Rows.Count > 0 Then
                                txtOutInQty.Text = ds.Tables("SamplePaceBarCode").Rows.Count
                                cobSE_BarCodeType.Enabled = False
                                'gluType.Enabled = False
                            Else
                                cobSE_BarCodeType.Enabled = True
                                'gluType.Enabled = True
                            End If
                        Else
                            ds.Tables("SamplePaceBarCode").Clear() '否清空表内数据
                        End If
                    End If
                    gluSE_OutD_ID.Enabled = False
                    gluOutPS_NO.Enabled = False

            End Select
        End If
    End Sub

#Region "称重程序"
    Dim comm As New System.IO.Ports.SerialPort
    Private Timer1 As New Timer
    Dim buf = New Byte() {}
    Dim strBuf As String = String.Empty

    Function GetObjectWeight(ByVal PortName As String, ByVal BaudRate As Integer) As Boolean
        '1.时间控件
        AddHandler Timer1.Tick, AddressOf Timer1_Tick '时间控件
        Timer1.Enabled = True
        Timer1.Interval = 10
        '2.COM控件事件
        AddHandler comm.DataReceived, AddressOf comm_DataReceived 'COM控件事件
        If comm.IsOpen Then
            Try
                comm.Close()
            Catch
                MessageBox.Show("关闭错误")
            End Try
        Else
            'comm.PortName = "COM1"
            'comm.BaudRate = 9600
            comm.PortName = PortName
            comm.BaudRate = BaudRate
            comm.DataBits = 8
            comm.StopBits = System.IO.Ports.StopBits.One
            Try
                comm.Open()
                comm.NewLine = "/r/n"
            Catch
                MessageBox.Show("打开错误")
            End Try
        End If
        Return True
    End Function

    Function CommWeight(ByVal txtWeight As DevExpress.XtraEditors.TextEdit) As String
        Dim strCommWeight As String = String.Empty
        Try
            Me.Invoke(New InvokeDelegate(AddressOf InvokeMethod))

            Dim StrA As String = String.Empty
            Dim StrB As String = String.Empty
            Dim StrC As String = String.Empty
            Dim IntA As Integer = 0
            Dim IntB As Integer = 0
            Dim IntC As Integer = 0
            If strBuf.Split.Length > 5 Then
                For i As Integer = 0 To strBuf.Split.Length - 1
                    If strBuf.Split()(i) = "WTST" Then
                        IntA = 1
                        StrA = strBuf.Split()(i)
                    End If
                    If IntA = 1 Then
                        If strBuf.Split()(i).StartsWith("+") Then
                            StrB = strBuf.Split()(i)
                            IntB = 1
                        End If
                    End If
                    If IntB = 1 Then
                        If strBuf.Split()(i) = "g" Then
                            StrC = strBuf.Split()(i)
                            IntC = 1
                        End If
                    End If
                    If IntA + IntB + IntC = 3 Then
                        If CDbl(StrB.Substring(1)) = 0 Then
                            txtWeight.Text = "0"
                        Else
                            txtWeight.Text = StrB.Substring(1)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch

        End Try

        Return strCommWeight
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CommWeight(txtWeighing)

    End Sub

    Private Sub comm_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        Try
            Dim n As Integer = comm.BytesToRead
            buf = New Byte(n) {}
            If n > 20 Then
                comm.Read(buf, 0, n)
            End If
        Catch

        End Try
    End Sub

    Delegate Sub DelegateSetDataSource()
    Private Delegate Sub InvokeDelegate()

    Private Sub InvokeMethod()
        Try
            strBuf = String.Empty
            Dim DegDataSource As New DelegateSetDataSource(AddressOf SetControlDataSource)
            Me.Invoke(DegDataSource)
        Catch
        End Try
    End Sub

    Private Sub SetControlDataSource()
        Try
            If IsDBNull(buf) = False Then
                strBuf = System.Text.Encoding.ASCII.GetString(buf)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "称重错误")
        End Try
    End Sub

    Private Sub frmSamplePaceInsert_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If comm.IsOpen Then
            Try
                comm.Close()
                Me.Dispose(True)
            Catch
                MessageBox.Show("关闭错误")
            End Try
        End If
    End Sub
#End Region


End Class