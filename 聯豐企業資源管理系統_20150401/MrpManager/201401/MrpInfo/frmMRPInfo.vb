Imports CrystalDecisions.Shared
Imports LFERP.Library.MrpManager.MrpInfo
Imports LFERP.Library.MrpManager.MrpSetting
Imports LFERP.Library.MrpManager.MrpForecastOrder
Imports LFERP.Library.MrpManager.MrpWareHouseInfo
Imports LFERP.Library.MrpManager.MrpPurchaseRecord
Imports System.Threading

Public Class frmMRPInfo

#Region "�r�q�ݩ�"
    Dim ds As New DataSet
    Dim MRPBill_DelAutoID As String = ""            '�O�����~���Ӫ�Q�R����AutoID
    Dim MRPOrderBill_DelAutoID As String = ""       '�O���q����Ӫ�Q�R����AutoID
    Dim MRPPurchase_DelAutoID As String = ""        '�O����ĳ���ʪ�Q�R����AutoID
    Dim MRPAction As Boolean = False                '�аO�O�_�I���L����q��H�����s
    Dim bMRPGetPurchase As Boolean = False           '�аO�O�_�I���L�ͦ����ʫ�ĳ���s
    Dim tempbo As Boolean = False                   'CheckEdit_CheckedChanged�ƥ󤤤ޥ�
    Private tempMrpID As String = ""
    Private _EditItem As String
    Private _MRPID As String
    Dim mri As New MrpCalcRecordInfo

    Property EditItem() As String '�ݩ�
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property

    Property MRPID() As String '�ݩ�
        Get
            Return _MRPID
        End Get
        Set(ByVal value As String)
            _MRPID = value
        End Set
    End Property

#End Region

#Region "�Ы��{�ɪ�"
    Sub CreateTables()
        ds.Tables.Clear()
        With ds.Tables.Add("MRPDestBills")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("MRP_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("Source", GetType(String))
            .Columns.Add("MRPQty", GetType(Decimal))
            .Columns.Add("MP_InventoryQty", GetType(Decimal))
        End With

        With ds.Tables.Add("MRPOrderDestBills")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("MRP_ID", GetType(String))
            .Columns.Add("OD_ID", GetType(String))
            .Columns.Add("MOB_ForecastID", GetType(String))
            .Columns.Add("MOB_NeedDate", GetType(Date))
            .Columns.Add("customerName", GetType(String))
            .Columns.Add("MRPQty", GetType(Decimal))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("Source", GetType(String))
        End With

        With ds.Tables.Add("MRPPurchase")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("MRP_ID", GetType(String))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
            .Columns.Add("Source", GetType(String))
            .Columns.Add("MP_NeedQty", GetType(Decimal))
            .Columns.Add("MP_MRPQty", GetType(Decimal))
            .Columns.Add("MP_InventoryQty", GetType(Decimal))
            .Columns.Add("MP_InTransitQty", GetType(Decimal))
            .Columns.Add("MP_Inspection", GetType(Decimal))
            .Columns.Add("MP_NoCollar", GetType(Decimal))
            .Columns.Add("MP_RetreatQty", GetType(Decimal))
            .Columns.Add("MP_RelatedQty", GetType(Decimal))
            .Columns.Add("MP_SecInv", GetType(Decimal))
            .Columns.Add("MP_LowLimit", GetType(Decimal))
            .Columns.Add("MP_BatchQty", GetType(Decimal))
            .Columns.Add("MP_BatFixEconomy", GetType(Decimal))
            .Columns.Add("MP_OrderMax", GetType(Decimal))
            .Columns.Add("MP_OrderMin", GetType(Decimal))
        End With

        With ds.Tables.Add("MRPIndReq")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("MRP_ID", GetType(String))
            .Columns.Add("PID", GetType(String))
            .Columns.Add("ID", GetType(String))
            .Columns.Add("sonsNum", GetType(Int16))
            .Columns.Add("ForecastID", GetType(String))
            .Columns.Add("NeedDate", GetType(Date))
            .Columns.Add("InvalidDate", GetType(Date))
            .Columns.Add("M_Code", GetType(String))
            .Columns.Add("NeedQty", GetType(Decimal))
            .Columns.Add("Source", GetType(String))
            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))
        End With

        grid1.DataSource = ds.Tables("MRPDestBills")
        grid2.DataSource = ds.Tables("MRPOrderDestBills")
        grid3.DataSource = ds.Tables("MRPPurchase")
        'TreeList1.DataSource = ds.Tables("MRPIndReq")
    End Sub
#End Region

#Region "��R�{�ɪ�"
    Sub FillTable(ByVal tableName As String, ByVal infoList As Object)
        Try
            Dim row As DataRow
            Dim i As Integer
            Select Case tableName
                Case "MRPDestBills"
                    Dim mbi As New List(Of MrpDestBillsInfo)
                    mbi = infoList
                    For i = 0 To mbi.Count - 1
                        row = ds.Tables("MRPDestBills").NewRow
                        row("AutoID") = mbi(i).AutoID
                        row("MRP_ID") = mbi(i).MRP_ID
                        row("M_Code") = mbi(i).M_Code
                        row("M_Name") = mbi(i).M_Name
                        row("M_Gauge") = mbi(i).M_Gauge
                        row("M_Unit") = mbi(i).M_Unit
                        row("Source") = mbi(i).Source
                        row("MRPQty") = mbi(i).MB_MRPQty
                        row("MP_InventoryQty") = mbi(i).MP_InventoryQty
                        ds.Tables("MRPDestBills").Rows.Add(row)
                    Next
                Case "MRPOrderDestBills"
                    Dim mobi As New List(Of MRPOrderDestBillsInfo)
                    mobi = infoList
                    For i = 0 To mobi.Count - 1
                        row = ds.Tables("MRPOrderDestBills").NewRow
                        row("AutoID") = mobi(i).AutoID
                        row("MRP_ID") = mobi(i).MRP_ID
                        row("OD_ID") = mobi(i).OD_ID
                        row("MOB_ForecastID") = mobi(i).MOB_ForecastID
                        row("MOB_NeedDate") = mobi(i).MOB_NeedDate
                        row("customerName") = mobi(i).customerName
                        row("MRPQty") = mobi(i).MOB_MRPQty
                        row("M_Code") = mobi(i).M_Code
                        row("M_Name") = mobi(i).M_Name
                        row("M_Gauge") = mobi(i).M_Gauge
                        row("M_Unit") = mobi(i).M_Unit
                        row("Source") = mobi(i).Source
                        ds.Tables("MRPOrderDestBills").Rows.Add(row)
                    Next
                Case "MRPPurchase"
                    Dim mpi As New List(Of MrpPurchaseInfo)
                    mpi = infoList
                    For i = 0 To mpi.Count - 1
                        row = ds.Tables("MRPPurchase").NewRow
                        row("AutoID") = mpi(i).AutoID
                        row("MRP_ID") = mpi(i).MRP_ID
                        row("M_Code") = mpi(i).M_Code
                        row("M_Name") = mpi(i).M_Name
                        row("M_Gauge") = mpi(i).M_Gauge
                        row("M_Unit") = mpi(i).M_Unit
                        row("Source") = mpi(i).Source
                        row("MP_NeedQty") = mpi(i).MP_NeedQty
                        row("MP_MRPQty") = mpi(i).MP_MRPQty
                        row("MP_InventoryQty") = mpi(i).MP_InventoryQty
                        row("MP_InTransitQty") = mpi(i).MP_InTransitQty
                        row("MP_Inspection") = mpi(i).MP_Inspection
                        row("MP_NoCollar") = mpi(i).MP_NoCollar
                        row("MP_RetreatQty") = mpi(i).MP_RetreatQty
                        row("MP_RelatedQty") = mpi(i).MP_RelatedQty
                        row("MP_SecInv") = mpi(i).MP_SecInv
                        row("MP_LowLimit") = mpi(i).MP_LowLimit
                        row("MP_BatchQty") = mpi(i).MP_BatchQty
                        row("MP_BatFixEconomy") = mpi(i).MP_BatFixEconomy
                        row("MP_OrderMax") = mpi(i).MP_OrderMax
                        row("MP_OrderMin") = mpi(i).MP_OrderMin
                        ds.Tables("MRPPurchase").Rows.Add(row)
                    Next
                Case "MRPIndReq"
                    Dim mii As New List(Of MrpIndReqInfo)
                    mii = infoList
                    For i = 0 To mii.Count - 1
                        row = ds.Tables("MRPIndReq").NewRow
                        row("AutoID") = mii(i).AutoID
                        row("MRP_ID") = mii(i).MRP_ID
                        row("PID") = mii(i).PID
                        row("ID") = mii(i).ID
                        row("sonsNum") = mii(i).sonsNum
                        row("ForecastID") = mii(i).ForecastID
                        row("NeedDate") = mii(i).NeedDate
                        row("InvalidDate") = mii(i).InvalidDate
                        row("M_Code") = mii(i).M_Code
                        row("NeedQty") = mii(i).NeedQty
                        row("Source") = mii(i).Source
                        row("M_Name") = mii(i).M_Name
                        row("M_Gauge") = mii(i).M_Gauge
                        row("M_Unit") = mii(i).M_Unit
                        ds.Tables("MRPIndReq").Rows.Add(row)
                    Next
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "FillTable��k�X��")
        End Try
    End Sub
#End Region

#Region "����[���ƥ�"

#Region "����[��"
    Private Sub frmMRP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadDataToLookUpEdit()
        CreateTables()
        chkDate.Checked = True
        pceForecastID.Properties.PopupControl = Nothing
        Select Case EditItem
            Case "MRPAdd"
                lblTitle.Text = "MRP���ƻݨD�B��X�X�s�W"
                Me.Text = "MRP���ƻݨD�B��X�X�s�W"
                txtMRP_ID.EditValue = "�۰ʽs��"
                xtpCheck.PageVisible = False
                dteCreateDate.EditValue = Format(Now(), "yyyy/MM/dd")
                SetControlEnable(True, True)
            Case "MRPEdit"
                lblTitle.Text = "MRP���ƻݨD�B��X�X�ק�"
                Me.Text = "MRP���ƻݨD�B��X�X�ק�"
                xtpCheck.PageVisible = False
                chkDate.Enabled = False
                chkForecastID.Enabled = False
                SetControlEnable(True, True)
                LoadData(MRPID)
            Case "MRPWatch"
                lblTitle.Text = "MRP���ƻݨD�B��X�X�d��"
                Me.Text = "MRP���ƻݨD�B��X�X�d��"
                SetControlEnable(False, False)
                LoadData(MRPID)
            Case "MRPCheck"
                lblTitle.Text = "MRP���ƻݨD�B��X�X�f��"
                Me.Text = "MRP���ƻݨD�B��X�X�f��"
                XtraTabControl1.SelectedTabPage = xtpCheck
                SetControlEnable(False, True)
                LoadData(MRPID)
        End Select
        TreeList1.ExpandAll()
    End Sub
#End Region

#Region "�⪫�ư򥻫H���[����LookUpEdit����"
    Private Sub LoadDataToLookUpEdit()
        Dim mc As New MrpInfoController
        GridMaterial1.DataSource = mc.GetMaterialInfo()
        GridMaterial2.DataSource = mc.GetMaterialInfo()
        GridMaterial3.DataSource = mc.GetMaterialInfo()

        Dim mfc As New MrpForecastOrderController
        chklbForecastID.DisplayMember = "ForecastID"
        chklbForecastID.ValueMember = "ForecastID"
        chklbForecastID.DataSource = mfc.MrpForecastOrder_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
    End Sub

#End Region

#Region "CheckEdit_CheckedChanged"
    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkForecastID.CheckedChanged, chkDate.CheckedChanged
        If tempbo = True Then
            Exit Sub
        End If
        Select Case sender.Name
            Case "chkDate"
                dteNeedBeginDate.Enabled = True
                dteNeedEndDate.Enabled = True
                pceForecastID.Properties.PopupControl = Nothing
                chkDate.Checked = True
                tempbo = True
                chkForecastID.Checked = False
                tempbo = False
                lblDescrip.Text = "���q�����B��"
            Case "chkForecastID"
                dteNeedBeginDate.Enabled = False
                dteNeedEndDate.Enabled = False
                pceForecastID.Properties.PopupControl = pccForecastID
                chkForecastID.Checked = True
                tempbo = True
                chkDate.Checked = False
                tempbo = False
                lblDescrip.Text = "���w���q��B��"
        End Select
    End Sub
#End Region

#End Region

#Region "�[���ƾ�"
    Dim threadIndReq As Thread
    Delegate Sub DelegateSetDataSource(ByVal dataSource As Object, ByVal control As Object)
    Delegate Sub DelegateSetPictureBox()
    Private Sub LoadData(ByVal MRPID As String)
        Dim objInfo As List(Of MRPInfoInfo)
        Dim mc As New MrpInfoController
        Try
            '-----------------MRP�B��O����-----------------------
            objInfo = mc.MrpInfo_GetList(MRPID, Nothing, Nothing, Nothing, Nothing)
            If objInfo Is Nothing Then
                Exit Sub
            End If
            If objInfo(0).MI_CalcType = 1 Then
                chkDate.Checked = True
                dteNeedBeginDate.EditValue = objInfo(0).MI_NeedBeginDate
                dteNeedEndDate.EditValue = objInfo(0).MI_NeedEndDate
                pceForecastID.EditValue = Nothing
            Else
                chkForecastID.Checked = True
                dteNeedBeginDate.EditValue = Nothing
                dteNeedEndDate.EditValue = Nothing
                pceForecastID.EditValue = objInfo(0).MI_ForecastID
            End If
            tempMrpID = MRPID
            txtMRP_ID.EditValue = MRPID
            lblWareID.Text = objInfo(0).MI_WareID
            btnMrp.Tag = objInfo(0).MI_MRPDate
            dteCreateDate.EditValue = objInfo(0).MI_CreateDate

            txtCreateUserName.Text = objInfo(0).MI_CreateUserName
            txtRemarks.Text = objInfo(0).MI_Remarks
            If (objInfo(0).MI_MRPType = "0") Then
                cboMRPType.SelectedIndex = 0
            Else
                cboMRPType.SelectedIndex = 1
            End If
            txtlogtxt.EditValue = objInfo(0).MI_LogTxt
            chkCheck.Checked = objInfo(0).MI_CheckBit
            lblCheckUser.Text += objInfo(0).MI_CheckUserName
            If objInfo(0).MI_CheckDate <> Nothing Then
                lblCheckDate.Text += objInfo(0).MI_CheckDate
            End If

            txtCheckRemark.Text = objInfo(0).MI_CheckRemark

            '-----------------MRP���~���Ӫ�-----------------------
            Dim mbi As New List(Of MrpDestBillsInfo)
            Dim mbc As New MrpDestBillsController
            mbi = mbc.MrpDestBills_GetList(MRPID)
            If mbi.Count > 0 Then
                FillTable("MRPDestBills", mbi)
            End If

            '-----------------MRP�q�檫�Ʃ��Ӫ�--------------------
            Dim mobi As New List(Of MRPOrderDestBillsInfo)
            Dim mobc As New MRPOrderDestBillsController
            mobi = mobc.MRPOrderDestBills_GetList(MRPID)
            If mobi.Count > 0 Then
                FillTable("MRPOrderDestBills", mobi)
            End If

            '-----------------MRP���ʫ�ĳ��--------------------------
            Dim mpi As New List(Of MrpPurchaseInfo)
            Dim mpc As New MrpPurchaseController
            mpi = mpc.MrpPurchase_GetList(MRPID)
            If mpi.Count > 0 Then
                FillTable("MRPPurchase", mpi)
            End If

            pbIndreq.Visible = True
            pbIndreq.BringToFront()
            If threadIndReq Is Nothing = False Then
                threadIndReq.Abort()
            End If
            threadIndReq = New Thread(AddressOf LoadIndReq)
            threadIndReq.Start()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "LoadData��k�X��")
        End Try
    End Sub

    Private Sub LoadIndReq()
        On Error Resume Next
        '-----------------MRP�W�߻ݨD--------------------------
        Dim mii As New List(Of MrpIndReqInfo)
        Dim mic As New MrpIndReqController
        mii = mic.MrpIndReq_GetList(MRPID)
        If mii.Count > 0 Then
            FillTable("MRPIndReq", mii)
        End If
        Dim s As New DelegateSetDataSource(AddressOf SetControlDataSource)
        Dim p As New DelegateSetPictureBox(AddressOf SetPictureBox)
        Me.Invoke(s, ds.Tables("MRPIndReq"), TreeList1)
        pbIndreq.Invoke(p)
        threadIndReq.Abort()
    End Sub

    Private Sub SetControlDataSource(ByVal dataSource As Object, ByVal control As Object)
        Try
            control.DataSource = dataSource
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "����")
        End Try
    End Sub

    Private Sub SetPictureBox()
        Try
            pbIndreq.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "����")
        End Try
    End Sub
#End Region

#Region "�]�m�U����Enable�ݩ�"
    Private Sub SetControlEnable(ByVal a As Boolean, ByVal b As Boolean)
        dteNeedBeginDate.Enabled = a
        dteNeedEndDate.Enabled = a
        cboMRPType.Enabled = a
        pceForecastID.Enabled = a
        btnSave.Enabled = b
        btnMrp.Enabled = a
        btnInsertWareHouse.Enabled = a
        btnGetPurchase.Enabled = a
        chkCheck.Enabled = b
        txtCheckRemark.Enabled = b
        lblCheckUser.Visible = IIf(a Or b, False, True)
        lblCheckDate.Visible = IIf(a Or b, False, True)
        GridView2.OptionsBehavior.Editable = b
        GridView3.OptionsBehavior.Editable = b
        BandedGridView1.OptionsBehavior.Editable = b

        Dim i As Integer
        For i = 0 To cmsBill.Items.Count - 1
            cmsBill.Items(i).Visible = a
        Next
        For i = 0 To cmsOrderBill.Items.Count - 1
            cmsOrderBill.Items(i).Visible = a
        Next
        For i = 0 To cmsPurchase.Items.Count - 1
            cmsPurchase.Items(i).Visible = a
        Next
    End Sub
#End Region

#Region "���MRP�B���y����"
    Private Function GetMRP_ID() As String
        Dim mc As New MrpInfoController
        Dim ndate, id As String
        ndate = "MI" + Format(Now(), "yyMM")
        id = mc.MrpInfo_GetID()
        If id Is Nothing Then
            GetMRP_ID = ndate + "0001"
        Else
            GetMRP_ID = ndate + Mid((CInt(Mid(id, 7)) + 10001), 2)
        End If
    End Function
#End Region

#Region "�k�����ƥ�"
    Private Sub cms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsBillAdd.Click, cmdBillDel.Click, cmsOrderBillAdd.Click, cmsOrderBillDel.Click, cmsPurchaseAdd.Click, cmsPurchaseDel.Click, cmsPurchasePrint.Click
        Select Case sender.Name
            Case "cmsBillAdd"
                Dim row As DataRow
                row = ds.Tables("MRPDestBills").NewRow
                row("AutoID") = 0
                ds.Tables("MRPDestBills").Rows.Add(row)
                GridView2.FocusedRowHandle = GridView2.RowCount - 1
            Case "cmdBillDel"
                If (GridView2.GetFocusedRowCellValue("AutoID").ToString <> "0") Then
                    MRPBill_DelAutoID += GridView2.GetFocusedRowCellValue("AutoID").ToString + ","
                End If
                ds.Tables("MRPDestBills").Rows.RemoveAt(GridView2.FocusedRowHandle)

            Case "cmsOrderBillAdd"
                Dim row As DataRow
                row = ds.Tables("MRPOrderDestBills").NewRow
                row("AutoID") = 0
                row("MOB_NeedDate") = Format(Now, "yyyy/MM/dd")
                ds.Tables("MRPOrderDestBills").Rows.Add(row)
                GridView3.FocusedRowHandle = GridView3.RowCount - 1
            Case "cmsOrderBillDel"
                If (GridView3.GetFocusedRowCellValue("AutoID").ToString <> "0") Then
                    MRPOrderBill_DelAutoID += GridView3.GetFocusedRowCellValue("AutoID").ToString + ","
                End If
                ds.Tables("MRPOrderDestBills").Rows.RemoveAt(GridView3.FocusedRowHandle)

            Case "cmsPurchaseAdd"
                Dim row As DataRow
                row = ds.Tables("MRPPurchase").NewRow
                row("AutoID") = 0
                row("MP_NeedQty") = 0
                row("MP_MRPQty") = 0
                row("MP_InventoryQty") = 0
                row("MP_InTransitQty") = 0
                row("MP_Inspection") = 0
                row("MP_NoCollar") = 0
                row("MP_RetreatQty") = 0
                row("MP_SecInv") = 0
                row("MP_LowLimit") = 0
                row("MP_BatchQty") = 0
                row("MP_BatFixEconomy") = 0
                row("MP_OrderMax") = 0
                row("MP_OrderMin") = 0
                ds.Tables("MRPPurchase").Rows.Add(row)
                BandedGridView1.FocusedRowHandle = BandedGridView1.RowCount - 1
            Case "cmsPurchaseDel"
                If (BandedGridView1.GetFocusedRowCellValue("AutoID").ToString <> "0") Then
                    MRPPurchase_DelAutoID += BandedGridView1.GetFocusedRowCellValue("AutoID").ToString + ","
                End If
                ds.Tables("MRPPurchase").Rows.RemoveAt(BandedGridView1.FocusedRowHandle)

        End Select
    End Sub

    Private Sub grid_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grid1.MouseDown, grid2.MouseDown, grid3.MouseDown
        If e.Button() = Windows.Forms.MouseButtons.Right Then
            Select Case sender.name
                Case "grid1"
                    If GridView2.RowCount < 1 Then
                        cmsBill.Items("cmdBillDel").Enabled = False
                    Else
                        cmsBill.Items("cmdBillDel").Enabled = True
                    End If
                Case "grid2"
                    If GridView3.RowCount < 1 Then
                        cmsOrderBill.Items("cmsOrderBillDel").Enabled = False
                    Else
                        cmsOrderBill.Items("cmsOrderBillDel").Enabled = True
                    End If
                Case "grid3"
                    If BandedGridView1.RowCount < 1 Then
                        cmsPurchase.Items("cmsPurchaseDel").Enabled = False
                    Else
                        cmsPurchase.Items("cmsPurchaseDel").Enabled = True
                    End If
            End Select
        End If
    End Sub
#End Region

#Region "��ܪ��ƽs�X�ɱa�X���ƦW�ٵ��H��"
    Private Sub GridViewMateria_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridViewMaterial1.DoubleClick, GridViewMaterial2.DoubleClick, GridViewMaterial3.DoubleClick
        Dim gridView, gridViewParent As New DevExpress.XtraGrid.Views.Grid.GridView
        Dim popc As New DevExpress.XtraEditors.PopupContainerControl
        Dim tableName As String = ""
        Dim M_Code, M_Name, M_Gauge, M_Unit As String
        gridView = sender
        M_Code = gridView.GetFocusedRowCellValue("M_Code").ToString
        M_Name = gridView.GetFocusedRowCellValue("M_Name").ToString
        M_Gauge = gridView.GetFocusedRowCellValue("M_Gauge").ToString
        M_Unit = gridView.GetFocusedRowCellValue("M_Unit").ToString

        Select Case sender.Name
            Case "GridViewMaterial1"
                tableName = "MRPDestBills"
                gridViewParent = GridView2
                popc = PopupContainerControl1
            Case "GridViewMaterial2"
                tableName = "MRPOrderDestBills"
                gridViewParent = GridView3
                popc = PopupContainerControl2
            Case "GridViewMaterial3"
                tableName = "MRPPurchase"
                gridViewParent = BandedGridView1
                popc = PopupContainerControl3
        End Select

        ds.Tables(tableName).Rows(gridViewParent.FocusedRowHandle)("M_Code") = M_Code
        ds.Tables(tableName).Rows(gridViewParent.FocusedRowHandle)("M_Name") = M_Name
        ds.Tables(tableName).Rows(gridViewParent.FocusedRowHandle)("M_Gauge") = M_Gauge
        ds.Tables(tableName).Rows(gridViewParent.FocusedRowHandle)("M_Unit") = M_Unit
        popc.OwnerEdit.ClosePopup()
        btnSave.Focus()
    End Sub
#End Region

#Region "Mrp�B��"
    Private Sub btnMrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMrp.Click
        If CheckHaveSetting() = False Then
            Exit Sub
        End If
        If CheckSave() = False Then
            Exit Sub
        End If
        prb.Visible = True                '��ܶi�ױ�
        prb.Position = 0
        txtlogtxt.Visible = False

        btnMrp.Tag = Now.ToString("yyyy/MM/dd")        'MRP�B����
        MRPAction = True

        Dim msi As New MrpSettingInfo
        Dim msc As New MrpSettingController
        msi = msc.MrpSetting_GetList(InUserID)(0)

        Dim mc As New MrpInfoController
        mc.MRP_ID = txtMRP_ID.EditValue
        mc.userID = InUserID
        Dim tempTable As New DataTable

        '----------------���~���Ӫ�--------------------
        mc.dt = ds.Tables("MRPDestBills")
        If chkDate.Checked Then
            mc.GetBills(dteNeedBeginDate.EditValue, dteNeedEndDate.EditValue, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, Nothing)
        ElseIf chkForecastID.Checked Then
            mc.GetBills(Nothing, Nothing, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, pceForecastID.EditValue.ToString)
        End If

        prb.PerformStep()
        Application.DoEvents()

        '----------------�q�檫�Ʃ���----------------
        mc.dt = ds.Tables("MRPOrderDestBills")
        If chkDate.Checked Then
            mc.GetOrderBills(dteNeedBeginDate.EditValue, dteNeedEndDate.EditValue, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, Nothing)
        ElseIf chkForecastID.Checked Then
            mc.GetOrderBills(Nothing, Nothing, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, pceForecastID.EditValue.ToString)
        End If

        '-------------�W�߻ݨD----------------------
        mc.dt = ds.Tables("MRPIndReq")
        If chkDate.Checked Then
            mc.GetIndReq(dteNeedBeginDate.EditValue, dteNeedEndDate.EditValue, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, Nothing)
        ElseIf chkForecastID.Checked Then
            mc.GetIndReq(Nothing, Nothing, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, pceForecastID.EditValue.ToString)
        End If
        TreeList1.DataSource = ds.Tables("MRPIndReq")
        TreeList1.ExpandAll()
        prb.PerformStep()
        Application.DoEvents()
        pnlDeclare.Visible = True
        btnInsertWareHouse.Focus()
    End Sub

#Region "��w�s�O��"
    Private Sub btnInsertWareHouse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertWareHouse.Click
        If CheckSave() = False Then
            Exit Sub
        End If
        If ds.Tables("MRPOrderDestBills").DefaultView.Count < 1 Then
            MsgBox("�S�������s���w�s�O��", MsgBoxStyle.Information, "����")
            Exit Sub
        End If
        GridView2.OptionsBehavior.Editable = False   '���~����
        GridView3.OptionsBehavior.Editable = False   '�q�檫�Ʃ���

        Dim mc As New MrpInfoController
        Dim tb As New DataTable

        Dim msi As New MrpSettingInfo
        Dim msc As New MrpSettingController
        msi = msc.MrpSetting_GetList(InUserID)(0)
        If chkDate.Checked Then
            tb = mc.Mrp_GetMrpInfoWareHouse(dteNeedBeginDate.EditValue, dteNeedEndDate.EditValue, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, Nothing)
        ElseIf chkForecastID.Checked Then
            tb = mc.Mrp_GetMrpInfoWareHouse(Nothing, Nothing, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, pceForecastID.EditValue.ToString)
        End If

        Dim mlist As New List(Of MrpWareHouseInfoEntryInfo)
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim mi As New MrpWareHouseInfoEntryInfo
            mi.M_Code = tb.Rows(i)("Code")
            mi.M_Name = IIf(IsDBNull(tb.Rows(i)("M_Name")), Nothing, tb.Rows(i)("M_Name"))
            mi.M_Gauge = IIf(IsDBNull(tb.Rows(i)("M_Gauge")), Nothing, tb.Rows(i)("M_Gauge"))
            mi.M_Unit = IIf(IsDBNull(tb.Rows(i)("M_Unit")), Nothing, tb.Rows(i)("M_Unit"))
            mi.Source = IIf(IsDBNull(tb.Rows(i)("Source")), Nothing, tb.Rows(i)("Source"))
            mi.SourceID = IIf(IsDBNull(tb.Rows(i)("SourceID")), Nothing, tb.Rows(i)("SourceID"))
            mlist.Add(mi)
        Next
        Dim fr As New frmMrpWareHouseInfoAdd
        fr.EditItem = "Add"
        fr.DataList = mlist
        tempMrpID = System.Guid.NewGuid.ToString
        fr.MrpID = tempMrpID
        fr.MdiParent = MDIMain
        fr.Show()
        btnGetPurchase.Focus()

    End Sub
#End Region

#Region "�ͦ����ʫ�ĳ"
    Private Sub btnGetPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetPurchase.Click
        If CheckSave() = False Then
            Exit Sub
        End If

        Dim mc As New MrpInfoController
        If Len(tempMrpID) = 36 Then
            lblWareID.Text = mc.GetMrpWareHouseID(tempMrpID)
        End If

        Dim mwc As New MrpWareHouseInfoController
        If mwc.MrpWareHouseInfo_WareIDExists(lblWareID.Text) = False Then
            MsgBox("�w�s�O�������s�b�P���������w�s�O���A�Х���s���O��", MsgBoxStyle.Information, "����")
            Exit Sub
        End If
        pnlDeclare.Visible = False
        bMRPGetPurchase = True
        Dim tempTable As New DataTable
        Dim beginDateTime, endDateTime As DateTime
        Dim logString, usedTime As String
        beginDateTime = Now

        Dim msi As New MrpSettingInfo
        Dim msc As New MrpSettingController
        msi = msc.MrpSetting_GetList(InUserID)(0)

        mc.dt = ds.Tables("MRPPurchase")
        If chkDate.Checked Then
            mc.GetPurchaseMaterial(dteNeedBeginDate.EditValue, dteNeedEndDate.EditValue, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, Nothing, lblWareID.Text)
        ElseIf chkForecastID.Checked Then
            mc.GetPurchaseMaterial(Nothing, Nothing, msi.mrpInfoForecastCheckType, msi.mrpInfoForecastCancellation, pceForecastID.EditValue.ToString, lblWareID.Text)
        End If

        endDateTime = Now
        logString = "�}�l�ɶ�:" + beginDateTime.ToString("yyyy/MM/dd HH:mm:ss")
        logString += ";�����ɶ�:" + endDateTime.ToString("yyyy/MM/dd HH:mm:ss")
        usedTime = endDateTime.Subtract(beginDateTime).TotalMinutes.ToString
        txtlogtxt.EditValue = logString + ";�`�Ӯ�:" + usedTime.Remove(usedTime.LastIndexOf(".") + 3) + "����"

        For i As Integer = 0 To 2
            prb.PerformStep()
            Application.DoEvents()
        Next
        prb.Visible = False
        txtlogtxt.Visible = True
        mri.MrpCalcBeginDate = beginDateTime
        mri.MrpCalcEndDate = endDateTime
        mri.UsedTime = usedTime.Remove(usedTime.LastIndexOf(".") + 3)
        mri.MrpCalcDateTime = Now
        XtraTabControl1.SelectedTabPage = xtpPurchase
    End Sub
#End Region

#Region "�ˬd�Τ�O�_���]�mMRP�ѼƳ]�m"
    Private Function CheckHaveSetting() As Boolean
        Dim msi As New List(Of MrpSettingInfo)
        Dim msc As New MrpSettingController
        msi = msc.MrpSetting_GetList(InUserID)
        If msi.Count > 0 Then
            CheckHaveSetting = True
        Else
            MsgBox("�г]�mMRP�����ѼƳ]�m", MsgBoxStyle.Information, "����")
            CheckHaveSetting = False
        End If
    End Function
#End Region

#End Region

#Region "�O�s�B�h�X�ƥ�"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (CheckSave() = False) Then
            Exit Sub
        End If
        SaveData()
        Me.Close()
    End Sub

#Region "�O�s�ƾ�"
    Private Sub SaveData()
        Select Case EditItem
            Case "MRPAdd"
                SaveDataSub("MRPAdd")
                'SavePurchaseAdd()
            Case "MRPEdit"
                SaveDataSub("MRPEdit")
                'SavePurchaseEdit()
            Case "MRPCheck"
                Dim mi As New MRPInfoInfo
                Dim mc As New MrpInfoController
                mi.MRP_ID = txtMRP_ID.EditValue
                mi.MI_CheckBit = chkCheck.Checked
                mi.MI_CheckUserID = InUserID
                mi.MI_CheckRemark = txtCheckRemark.Text
                mc.MrpInfo_UpdateCheck(mi)
        End Select
    End Sub
    Private Sub SavePurchaseAdd()
        Dim MrpPInfo As New MrpPurchaseRecordInfo
        Dim MrpPCon As New MrpPurchaseRecordController
        Dim MrpPEInfo As New MrpPurchaseRecordEntryInfo
        Dim MrpPECon As New MrpPurchaseRecordEntryController
        MrpPInfo.MPP_CreateDate = Now
        MrpPInfo.MPP_CreateUserID = InUserID
        MrpPInfo.MRP_ID = txtMRP_ID.Text
        MrpPInfo.MrpPurchaseID = pceForecastID.Text
        MrpPCon.MrpPurchaseRecord_Add(MrpPInfo)
        For i As Integer = 0 To BandedGridView1.RowCount - 1
            MrpPEInfo.MRP_ID = BandedGridView1.GetRow(i)("MRP_ID").ToString
            MrpPEInfo.MrpPurchaseID = pceForecastID.Text
            MrpPEInfo.M_Code = BandedGridView1.GetRow(i)("M_Code").ToString
            MrpPEInfo.MPI_CreateUserID = InUserID
            MrpPECon.MrpPurchaseRecordEntry_Add(MrpPEInfo)
        Next
    End Sub
    Private Sub SavePurchaseEdit()
        Dim MrpPInfo As New MrpPurchaseRecordInfo
        Dim MrpPCon As New MrpPurchaseRecordController
        Dim MrpPEInfo As New MrpPurchaseRecordEntryInfo
        Dim MrpPECon As New MrpPurchaseRecordEntryController
        MrpPInfo.MRP_ID = txtMRP_ID.Text
        MrpPInfo.MrpPurchaseID = pceForecastID.Text
        MrpPCon.MrpPurchaseRecord_Update(MrpPInfo)

        For i As Integer = 0 To BandedGridView1.RowCount - 1
            If BandedGridView1.GetRow(i)("AutoID").ToString = 0 Or IsDBNull(BandedGridView1.GetRow(i)("AutoID").ToString) = True Then
                MrpPEInfo.MRP_ID = BandedGridView1.GetRow(i)("MRP_ID").ToString
                MrpPEInfo.MrpPurchaseID = pceForecastID.Text
                MrpPEInfo.M_Code = BandedGridView1.GetRow(i)("M_Code").ToString
                MrpPEInfo.MPI_CreateUserID = InUserID
                MrpPEInfo.MPI_NeedQty = BandedGridView1.GetRow(i)("MP_MRPQty").ToString
                MrpPECon.MrpPurchaseRecordEntry_Add(MrpPEInfo)
            Else
                MrpPEInfo.MRP_ID = BandedGridView1.GetRow(i)("MRP_ID").ToString
                MrpPEInfo.MrpPurchaseID = pceForecastID.Text
                MrpPEInfo.M_Code = BandedGridView1.GetRow(i)("M_Code").ToString
                MrpPEInfo.MPI_ModifyUserID = InUserID
                MrpPEInfo.MPI_NeedQty = BandedGridView1.GetRow(i)("MP_MRPQty").ToString
                MrpPECon.MrpPurchaseRecordEntry_Update(MrpPEInfo)
            End If
            Dim array1 As String()
            array1 = MRPBill_DelAutoID.Split(",")
            For x As Integer = 0 To array1.Length - 2
                MrpPECon.MrpPurchaseRecordEntry_Delete(Nothing, array1(x))
            Next
        Next

    End Sub
    Private Sub SaveDataSub(ByVal editItem As String)

        SaveMrpInfo(editItem)
        SaveMrpDestBill(editItem)
        SaveMrpOrderDestBill(editItem)
        SavePurchaseCalcRecord()
        SaveMrpPurchase(editItem)
        SaveMrpIndReq(editItem)

        '----------------�O�s�B��O����----------------
        If MRPAction = True Then
            Dim mrc As New MrpCalcRecordController
            mri.Mrp_ID = txtMRP_ID.EditValue
            mri.CreateUserID = InUserID
            mri.ProductNum = ds.Tables("MRPDestBills").Rows.Count
            mri.PurchaseNum = ds.Tables("MRPPurchase").Rows.Count
            mrc.MrpCalcRecord_Add(mri)
        End If

        '---------------MRP��w�s�Z�ק�w�s�O������MRP_ID
        Dim mwc As New MrpWareHouseInfoController
        If tempMrpID.Length = 36 Then
            mwc.MrpWareHouseInfo_updateMRPID(tempMrpID, txtMRP_ID.EditValue.ToString)
        End If
    End Sub

#Region "�O�sMRP�B��O����"
    Private Sub SaveMrpInfo(ByVal editItem As String)
        Try
            '-----------------MRP�B��O����-------------------------
            Dim mi As New MRPInfoInfo
            Dim mc As New MrpInfoController
            If chkDate.Checked Then
                mi.MI_NeedBeginDate = dteNeedBeginDate.EditValue
                mi.MI_NeedEndDate = dteNeedEndDate.EditValue
                mi.MI_ForecastID = Nothing
            Else
                mi.MI_NeedBeginDate = Nothing
                mi.MI_NeedEndDate = Nothing
                mi.MI_ForecastID = pceForecastID.EditValue
            End If
            mi.MI_CalcType = IIf(chkDate.Checked, 1, 0)          '1��������B��,0�����q�渹�B��
            mi.MI_MRPType = cboMRPType.SelectedIndex
            mi.MI_MRPDate = btnMrp.Tag
            mi.MI_LogTxt = txtlogtxt.EditValue
            mi.MI_Remarks = txtRemarks.Text
            If Len(tempMrpID) = 36 Then
                lblWareID.Text = mc.GetMrpWareHouseID(tempMrpID)
            End If
            mi.MI_WareID = lblWareID.Text
            If editItem = "MRPAdd" Then
                txtMRP_ID.EditValue = GetMRP_ID()
                If Len(txtMRP_ID.EditValue) = 0 Then
                    MsgBox("�ͦ��渹�X���A�L�k�O�s", MsgBoxStyle.Information)
                    Exit Sub
                End If
                mi.MRP_ID = txtMRP_ID.EditValue
                mi.MI_CreateUserID = InUserID
                mc.MrpInfo_Add(mi)
            End If
            If editItem = "MRPEdit" Then
                mi.MRP_ID = txtMRP_ID.EditValue
                mi.MI_ModifyUserID = InUserID
                mc.MrpInfo_Update(mi)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SaveMrpInfo��k�X��")
        End Try
    End Sub
#End Region

#Region "�O�sMRP���~���Ӫ�"
    Private Sub SaveMrpDestBill(ByVal editItem As String)
        Try
            '-----------------MRP���~���Ӫ�--------------------------
            If ds.Tables("MRPDestBills").Rows.Count > 0 Then
                Dim mbi As New MrpDestBillsInfo
                Dim mbc As New MrpDestBillsController
                Dim i, x As Integer
                Dim array1 As String()
                If MRPAction = True Then
                    mbc.MrpDestBills_Delete(Nothing, txtMRP_ID.EditValue)
                End If
                For i = 0 To ds.Tables("MRPDestBills").Rows.Count - 1
                    mbi.AutoID = ds.Tables("MRPDestBills").Rows(i)("AutoID").ToString
                    mbi.MRP_ID = txtMRP_ID.EditValue
                    mbi.M_Code = ds.Tables("MRPDestBills").Rows(i)("M_Code").ToString
                    mbi.MB_MRPQty = ds.Tables("MRPDestBills").Rows(i)("MRPQty")
                    If editItem = "MRPAdd" Or MRPAction = True Then                 '�W�[
                        mbi.MB_CreateUserID = InUserID
                        mbc.MrpDestBills_Add(mbi)

                    ElseIf editItem = "MRPEdit" Then                        '�ק�
                        mbi.MB_ModifyUserID = InUserID
                        If String.IsNullOrEmpty(MRPBill_DelAutoID) = False Then
                            array1 = MRPBill_DelAutoID.Split(",")
                            For x = 0 To array1.Length - 2
                                mbc.MrpDestBills_Delete(array1(x), Nothing)
                            Next
                        End If
                        If mbi.AutoID = 0 Then
                            mbc.MrpDestBills_Add(mbi)
                        Else
                            mbc.MrpDestBills_Update(mbi)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SaveMrpDestBill��k�X��")
        End Try
    End Sub
#End Region

#Region "�O�sMRP�q�檫�Ʃ��Ӫ�"
    Private Sub SaveMrpOrderDestBill(ByVal editItem As String)
        Try
            '-----------------MRP�q�檫�Ʃ��Ӫ�----------------------
            If ds.Tables("MRPOrderDestBills").DefaultView.ToTable.Rows.Count > 0 Then
                Dim mobi As New MRPOrderDestBillsInfo
                Dim mobc As New MRPOrderDestBillsController
                Dim j, y As Integer
                Dim array2 As String()
                If MRPAction = True Then
                    mobc.MRPOrderDestBills_Delete(Nothing, txtMRP_ID.EditValue)
                End If
                Dim table As New DataTable
                table = ds.Tables("MRPOrderDestBills").DefaultView.ToTable
                For j = 0 To table.Rows.Count - 1
                    mobi.AutoID = table.Rows(j)("AutoID").ToString
                    mobi.MRP_ID = txtMRP_ID.EditValue
                    mobi.OD_ID = table.Rows(j)("OD_ID").ToString
                    mobi.MOB_ForecastID = table.Rows(j)("MOB_ForecastID").ToString
                    mobi.MOB_NeedDate = table.Rows(j)("MOB_NeedDate")
                    mobi.M_Code = table.Rows(j)("M_Code").ToString
                    mobi.MOB_MRPQty = table.Rows(j)("MRPQty").ToString
                    If editItem = "MRPAdd" Or MRPAction = True Then
                        mobi.MOB_CreateUserID = InUserID
                        mobc.MRPOrderDestBills_Add(mobi)

                    ElseIf editItem = "MRPEdit" Then
                        mobi.MOB_ModifyUserID = InUserID
                        If String.IsNullOrEmpty(MRPOrderBill_DelAutoID) = False Then
                            array2 = MRPOrderBill_DelAutoID.Split(",")
                            For y = 0 To array2.Length - 2
                                mobc.MRPOrderDestBills_Delete(array2(y), Nothing)
                            Next
                        End If
                        If mobi.AutoID = 0 Then
                            mobc.MRPOrderDestBills_Add(mobi)
                        Else
                            mobc.MRPOrderDestBills_Update(mobi)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SaveMrpOrderDestBill��k�X��")
        End Try
    End Sub
#End Region

#Region "�O�sMRP�B����ʫ�ĳ����ʰO����"
#Region "����y����"
    Private Function GetPurchaseCalcRecord_ID() As String
        Dim mc As New MrpPurchaseCalcRecordController
        Dim ndate, id As String
        ndate = "CR" + Format(Now(), "yyMM")
        id = mc.MrpPurchaseCalcRecord_GetID()
        If id Is Nothing Then
            GetPurchaseCalcRecord_ID = ndate + "0001"
        Else
            GetPurchaseCalcRecord_ID = ndate + Mid((CInt(Mid(id, 7)) + 10001), 2)
        End If
    End Function
#End Region

    Private Sub SavePurchaseCalcRecord()
        Try
            Dim mpcri As New MrpPurchaseCalcRecordInfo
            Dim mpcrc As New MrpPurchaseCalcRecordController
            If ds.Tables("MRPPurchase").Rows.Count > 0 Then
                For x As Integer = 0 To ds.Tables("MRPPurchase").Rows.Count - 1
                    mpcri.SN = GetPurchaseCalcRecord_ID()
                    mpcri.MRP_ID = txtMRP_ID.EditValue
                    mpcri.MRPType = cboMRPType.SelectedIndex
                    If chkDate.Checked Then
                        mpcri.NeedBeginDate = dteNeedBeginDate.EditValue
                        mpcri.NeedEndDate = dteNeedEndDate.EditValue
                        mpcri.ForecastID = Nothing
                    Else
                        mpcri.NeedBeginDate = Nothing
                        mpcri.NeedEndDate = Nothing
                        mpcri.ForecastID = pceForecastID.EditValue
                    End If
                    mpcri.CalcType = IIf(chkDate.Checked, 1, 0)
                    mpcri.Ware_ID = lblWareID.Text
                    mpcri.CreateUserID = InUserID
                    With ds.Tables("MRPPurchase")
                        mpcri.M_Code = .Rows(x)("M_Code").ToString
                        mpcri.NeedQty = CDec(.Rows(x)("MP_NeedQty"))
                        mpcri.MRPQty = CDec(.Rows(x)("MP_MRPQty"))
                        mpcri.InventoryQty = CDec(.Rows(x)("MP_InventoryQty"))
                        mpcri.InTransitQty = CDec(.Rows(x)("MP_InTransitQty"))
                        mpcri.Inspection = CDec(.Rows(x)("MP_Inspection"))
                        mpcri.NoCollar = CDec(.Rows(x)("MP_NoCollar"))
                        mpcri.RetreatQty = CDec(.Rows(x)("MP_RetreatQty"))
                        mpcri.RelatedQty = CDec(.Rows(x)("MP_RelatedQty"))
                    End With
                    mpcrc.MrpPurchaseCalcRecord_Add(mpcri)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SavePurchaseCalcRecord")
        End Try
    End Sub
#End Region

#Region "�O�sMRP���ʫ�ĳ��"
    Private Sub SaveMrpPurchase(ByVal editItem As String)
        Try
            '-----------------MRP���ʫ�ĳ��--------------------------
            If ds.Tables("MRPPurchase").Rows.Count > 0 Then
                Dim mpi As New MrpPurchaseInfo
                Dim mpc As New MrpPurchaseController
                Dim n, z As Integer
                Dim array3 As String()
                If bMRPGetPurchase = True Then
                    mpc.MrpPurchase_Delete(Nothing, txtMRP_ID.EditValue)
                End If
                Dim table As New DataTable
                table = ds.Tables("MRPPurchase")
                For n = 0 To table.Rows.Count - 1
                    mpi.AutoID = table.Rows(n)("AutoID").ToString
                    mpi.MRP_ID = txtMRP_ID.EditValue
                    mpi.M_Code = table.Rows(n)("M_Code").ToString
                    mpi.MP_NeedQty = CDec(table.Rows(n)("MP_NeedQty"))
                    mpi.MP_MRPQty = CDec(table.Rows(n)("MP_MRPQty"))
                    mpi.MP_InventoryQty = CDec(table.Rows(n)("MP_InventoryQty"))
                    mpi.MP_InTransitQty = CDec(table.Rows(n)("MP_InTransitQty"))
                    mpi.MP_Inspection = CDec(table.Rows(n)("MP_Inspection"))
                    mpi.MP_NoCollar = CDec(table.Rows(n)("MP_NoCollar"))
                    mpi.MP_RetreatQty = CDec(table.Rows(n)("MP_RetreatQty"))
                    mpi.MP_RelatedQty = CDec(table.Rows(n)("MP_RelatedQty"))
                    mpi.MP_SecInv = CDec(table.Rows(n)("MP_SecInv"))
                    mpi.MP_LowLimit = CDec(table.Rows(n)("MP_LowLimit"))
                    mpi.MP_BatchQty = CDec(table.Rows(n)("MP_BatchQty"))
                    mpi.MP_BatFixEconomy = CDec(table.Rows(n)("MP_BatFixEconomy"))
                    mpi.MP_OrderMax = CDec(table.Rows(n)("MP_OrderMax"))
                    mpi.MP_OrderMin = CDec(table.Rows(n)("MP_OrderMin"))
                    If editItem = "MRPAdd" Or MRPAction = True Then
                        mpi.MP_CreateUserID = InUserID
                        mpc.MrpPurchase_Add(mpi)
                    ElseIf editItem = "MRPEdit" Then
                        mpi.MP_ModifyUserID = InUserID
                        If String.IsNullOrEmpty(MRPPurchase_DelAutoID) = False Then
                            array3 = MRPPurchase_DelAutoID.Split(",")
                            For z = 0 To array3.Length - 2
                                mpc.MrpPurchase_Delete(array3(z), Nothing)
                            Next
                        End If
                        If mpi.AutoID = 0 Then
                            mpc.MrpPurchase_Add(mpi)
                        Else
                            mpc.MrpPurchase_Update(mpi)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SaveMrpPurchase��k�X��")
        End Try
    End Sub
#End Region

#Region "�O�s�W�߻ݨD"
    Private Sub SaveMrpIndReq(ByVal editItem As String)
        Try
            '----------------�O�s�W�߻ݨD----------------
            Dim mii As New MrpIndReqInfo
            Dim mic As New MrpIndReqController
            If MRPAction = True Then
                mic.MrpIndReq_Delete(txtMRP_ID.EditValue.ToString)
                Try
                    If TypeOf (TreeList1.DataSource) Is DataTable Then
                        Dim temptb As New DataTable
                        temptb = TreeList1.DataSource
                        For i As Integer = 0 To temptb.Rows.Count - 1
                            mii.MRP_ID = txtMRP_ID.EditValue.ToString
                            mii.PID = temptb.Rows(i)("PID").ToString
                            mii.ID = temptb.Rows(i)("ID").ToString
                            mii.ForecastID = temptb.Rows(i)("ForecastID").ToString
                            mii.M_Code = temptb.Rows(i)("M_Code").ToString
                            mii.M_Name = temptb.Rows(i)("M_Name").ToString
                            mii.M_Gauge = temptb.Rows(i)("M_Gauge").ToString
                            mii.M_Unit = temptb.Rows(i)("M_Unit").ToString
                            mii.Source = temptb.Rows(i)("Source").ToString
                            mii.NeedDate = CDate(temptb.Rows(i)("NeedDate"))
                            If IsDBNull(temptb.Rows(i)("InvalidDate")) Then
                                mii.InvalidDate = Nothing
                            Else
                                mii.InvalidDate = CDate(temptb.Rows(i)("InvalidDate"))
                            End If
                            mii.NeedQty = CDec(temptb.Rows(i)("NeedQty"))
                            mii.sonsNum = temptb.Rows(i)("sonsNum").ToString
                            mii.CreateUserID = InUserID
                            mic.MrpIndReq_Add(mii)
                        Next
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information, "����")
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "SaveMrpIndReq��k�X��")
        End Try
    End Sub
#End Region

    Private Function CheckSave() As Boolean
        CheckSave = True
        If chkForecastID.Checked Then
            If pceForecastID.EditValue Is Nothing Then
                MsgBox("�п�ܹw���q�渹", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            ElseIf pceForecastID.EditValue.ToString.Length < 2 Then
                MsgBox("�п�ܹw���q�渹", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
        ElseIf chkDate.Checked Then
            If dteNeedBeginDate.EditValue = Nothing Then
                MsgBox("�п�ܮi����}�l���", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            ElseIf dteNeedEndDate.EditValue = Nothing Then
                MsgBox("�п�ܮi����������", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            ElseIf Date.Compare(CDate(dteNeedBeginDate.EditValue), CDate(dteNeedEndDate.EditValue)) > 0 Then
                MsgBox("�i����}�l�������ߤ_�������", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("MRPDestBills").Rows.Count - 1
            If ds.Tables("MRPDestBills").Rows(i)("M_Code") Is Nothing Then
                MsgBox("�L�k�O�s�A���~���Ӥ��s�b���~�s�X���Ū���Ʀ�", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
        Next

        For i = 0 To ds.Tables("MRPOrderDestBills").Rows.Count - 1
            With ds.Tables("MRPOrderDestBills")
                If .Rows(i)("M_Code").ToString.Length < 1 Then
                    MsgBox("�L�k�O�s�A�q�檫�Ʃ��Ӥ��s�b���~�s�X����", MsgBoxStyle.Information, "����")
                    CheckSave = False
                    Exit Function
                End If
            End With
        Next

        For i = 0 To ds.Tables("MrpPurchase").Rows.Count - 1
            If ds.Tables("MrpPurchase").Rows(i)("M_Code") Is Nothing Then
                MsgBox("�L�k�O�s�A���ʫ�ĳ���s�b���ƽs�X���Ū���Ʀ�", MsgBoxStyle.Information, "����")
                CheckSave = False
                Exit Function
            End If
        Next
    End Function
#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmMRPInfo_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Dim mwc As New MrpWareHouseInfoController
            Dim mwec As New MrpWareHouseInfoEntryController
            Dim wareID As String
            If Len(tempMrpID) = 36 Then
                wareID = mwc.MrpWareHouseInfo_MRPIDExists(tempMrpID)
                If wareID <> Nothing Then
                    mwc.MrpWareHouseInfo_DeleteByMRPID(tempMrpID)
                    mwec.MrpWareHouseInfoEntry_DeleteAll(wareID)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "frmMRPInfo_FormClosed��k�X��")
        End Try
    End Sub
#End Region

#Region "�d�ݾ��v�B��O��"
    Private Sub txtlogtxt_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtlogtxt.DoubleClick
        Dim fr As New frmMrpCalcOrder
        fr.ShowDialog()
    End Sub
#End Region

#Region "��ܹw���q��ƥ�"
    Private Sub pceForecastID_QueryResultValue(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.QueryResultValueEventArgs) Handles pceForecastID.QueryResultValue
        Dim str As String = ""
        For i As Integer = 0 To chklbForecastID.ItemCount - 1
            If chklbForecastID.GetItemChecked(i) = True Then
                str += chklbForecastID.GetItemText(i) + ","
            End If
        Next
        If str.Length > 1 Then
            str = str.Remove(str.LastIndexOf(","), 1)
        End If
        e.Value = str
    End Sub
#End Region

#Region "�d�ݮw�s�O����"
    Private Sub lblWareID_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWareID.DoubleClick
        If lblWareID.Text.Length > 2 Then
            Dim fr As New frmMrpWareHouseInfoAdd
            fr.EditItem = "Edit"
            fr.EditValue = lblWareID.Text
            fr.MdiParent = MDIMain
            fr.Show()
        End If
    End Sub

    Private Sub lblWareID_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWareID.MouseHover
        If lblWareID.Text.Length > 2 Then
            lblWareID.ForeColor = Color.DarkRed
            lblWareID.Cursor = Cursors.Hand
            Dim tip As New ToolTip
            tip.SetToolTip(lblWareID, "�����d�ݮw�s�O����")
        End If     
    End Sub

    Private Sub lblWareID_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWareID.MouseLeave
        lblWareID.ForeColor = Color.Red
        lblWareID.Cursor = Cursors.Default
    End Sub
#End Region

#Region "���Ĥ�����ŮɡA�]�m����ܤ奻����"
    Private Sub TreeList1_CustomDrawNodeCell(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs) Handles TreeList1.CustomDrawNodeCell
        If e.Column.Name = "InvalidDate" Then
            If IsDBNull(e.CellValue) Then
                e.CellText = ""
            ElseIf CDate(e.CellValue).Year = 1 Then
                e.CellText = ""
            ElseIf CDate(e.CellValue) <= Now Then
                e.Appearance.BackColor = Color.Red
                MsgBox("�������ƲM�椤�s�b���Ī�����", MsgBoxStyle.Information, "�`�N")
            End If

        End If
    End Sub
#End Region

End Class
