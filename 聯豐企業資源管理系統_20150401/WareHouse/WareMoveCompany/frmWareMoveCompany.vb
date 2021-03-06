Imports System.Data.SqlClient
Imports System.Data.Common
Imports LFERP.Library.WareHouse.WareInventory
Imports LFERP.Library.WareHouse.WareMoveCompany

Public Class frmWareMoveCompany
#Region "字段屬性"
    Dim JS As Integer

    Dim CenterIP As String
    Dim strCenter As String
    Dim RunstrCenter As String
    Dim DSmove As New DataSet


    Dim oldCheck As Boolean
    Dim oldInCheck As Boolean

    Private _EditItem As String '屬性欄  'Add ,Edit,Check ,InCheck 
    Private _EditID As String
    Private _Company As String
    Private _WH_ID As String
    Private _WH_Name As String
    Private _TypeInorOut As String

    Property WH_Name() As String '屬性
        Get
            Return _WH_Name
        End Get
        Set(ByVal value As String)
            _WH_Name = value
        End Set
    End Property
    Property WH_ID() As String '屬性
        Get
            Return _WH_ID
        End Get
        Set(ByVal value As String)
            _WH_ID = value
        End Set
    End Property
    Property Company() As String '屬性
        Get
            Return _Company
        End Get
        Set(ByVal value As String)
            _Company = value
        End Set
    End Property
    Property EditItem() As String '屬性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property EditID() As String '屬性
        Get
            Return _EditID
        End Get
        Set(ByVal value As String)
            _EditID = value
        End Set
    End Property

    Property TypeInorOut() As String '屬性
        Get
            Return _TypeInorOut
        End Get
        Set(ByVal value As String)
            _TypeInorOut = value
        End Set
    End Property

#End Region


#Region "取得中心數據庫連接"
    Function GetCenter() As String
        GetCenter = ""

        Dim CC As New WareMoveCompanyController
        Dim CL As New List(Of WareMoveCompanyInfo)
        CL = CC.WareMoveCompanyCenterSetting_GetList()
        If CL.Count <= 0 Then
            Exit Function
        End If

        GetCenter = "server=" & CL(0).ServerIP & ";database=" & CL(0).DataBaseName & ";uid=" & CL(0).UserID & ";pwd=" & CL(0).PassWord & ""
        CenterIP = CL(0).ServerIP

        RunstrCenter = ""

    End Function


    Sub AddCompany()
        Dim dsAdd As New DataSet
        Dim strSql As String
        Dim myConn As New SqlConnection(GetCenter)
        Dim da As SqlDataAdapter

        myConn.Open()
        dsAdd.Tables.Clear()



        If EditItem = "InCheck" Or EditItem = "View" Then
            strSql = "SELECT distinct CO_ID,CO_Name FROM  WareMoveCompanyHouse "
        Else
            strSql = "SELECT distinct CO_ID,CO_Name FROM  WareMoveCompanyHouse  where CO_ID<>'" & Company & "' "
        End If

        da = New SqlDataAdapter(strSql, myConn)
        da.Fill(dsAdd, "CO_IDName")
        myConn.Close()
        '-------------------------------------------------------------------
        ComMC_IN_Company.Properties.ValueMember = "CO_ID"
        ComMC_IN_Company.Properties.DisplayMember = "CO_Name"
        ComMC_IN_Company.Properties.DataSource = dsAdd.Tables("CO_IDName")
    End Sub


    Sub AddWareHouse(ByVal _CO_ID As String)
        Dim dsAdd As New DataSet
        Dim strSql As String
        Dim myConn As New SqlConnection(GetCenter)
        Dim da As SqlDataAdapter

        myConn.Open()
        dsAdd.Tables.Clear()
        strSql = "SELECT WH_ID,WH_Name FROM  WareMoveCompanyHouse where CO_ID='" & _CO_ID & "'   "
        da = New SqlDataAdapter(strSql, myConn)
        da.Fill(dsAdd, "WareHouse")
        myConn.Close()
        '-------------------------------------------------------------------
        ComMC_IN_WHName.Properties.ValueMember = "WH_ID"
        ComMC_IN_WHName.Properties.DisplayMember = "WH_Name"
        ComMC_IN_WHName.Properties.DataSource = dsAdd.Tables("WareHouse")
    End Sub


    Function ExecuteNonQuery(ByVal cmdText As String) As Integer

        Dim result As Integer
        Dim cn As SqlConnection = New SqlConnection(GetCenter)
        cn.Open()
        Dim cmd = New SqlCommand(cmdText, cn)
        result = cmd.ExecuteNonQuery()
        cn.Dispose()
        Return result
    End Function

#End Region

#Region "加載送貨類型"
    Private Sub LoadPositiveDeliverType()
        Dim mvc As New WareMoveCompanyController

        ResLookIp.DisplayMember = "WTypeName"
        ResLookIp.ValueMember = "WTypeName"
        ResLookIp.DataSource = mvc.WareMoveCompanyType_GetList(Nothing, Nothing)
    End Sub

#End Region


#Region "建表"
    Sub CreateTables()
        DSmove.Tables.Clear()
        With DSmove.Tables.Add("WareMove")
            .Columns.Add("AutoID", GetType(String))
            .Columns.Add("MC_Num", GetType(String))

            .Columns.Add("M_Name", GetType(String))
            .Columns.Add("M_Gauge", GetType(String))
            .Columns.Add("M_Unit", GetType(String))

            .Columns.Add("M_Code", GetType(String))

            .Columns.Add("MC_Qty", GetType(Double))
            .Columns.Add("MC_Remark", GetType(String))
            .Columns.Add("MC_OUT_EndQty", GetType(Double))
            .Columns.Add("MC_IN_EndQty", GetType(Double))


            .Columns.Add("WI_Qty", GetType(Double))

            '2014-04-01      姚駿
            .Columns.Add("WI_RelQty", GetType(Double))
            .Columns.Add("WTypeName", GetType(String))


        End With
        '創建刪除數據表
        With DSmove.Tables.Add("DelDataWareMove")
            .Columns.Add("AutoID", GetType(String))
        End With

        Grid2.DataSource = DSmove.Tables("WareMove")
    End Sub
#End Region






    Private Sub frmWareMoveCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        JS = 0

        CreateTables()

        '2014-04-01   姚駿
        LoadPositiveDeliverType()

        strCenter = GetCenter()
        If strCenter = "" Then
            MsgBox("中心數據庫設置錯誤請檢查！")
            Me.Close()
        End If
        '------------------------------------------
        AddCompany()

        Select Case EditItem
            Case "Add"
                ComMC_OUT_Company.Text = Company
                ComMC_OUT_Company.EditValue = Company
                ComMC_OUT_WHName.Tag = WH_ID
                ComMC_OUT_WHName.Text = WH_Name

                DateMC_AddDate.EditValue = Format(Now, "yyyy/MM/dd")
                Me.XtraTabPage2.PageVisible = False
                WI_Qty.Visible = True

                LabCaption.Text = "調撥單-發出"
                CheckMC_InCheck.Visible = False
            Case "Edit"
                Me.XtraTabPage2.PageVisible = False
                CheckMC_InCheck.Visible = False
                LabCaption.Text = "調撥單-修改"
                WI_Qty.Visible = True

                txtMC_NO.Text = EditID
                ComMC_OUT_Company.Text = Company

                LoadData(EditItem)

            Case "Check"
                Panel1.Enabled = True
                Me.XtraTabPage2.PageVisible = True
                XtraTabControl1.SelectedTabPage = Me.XtraTabPage2
                txtMC_NO.Text = EditID
                ComMC_OUT_Company.Text = Company
                LoadData(EditItem)

                LabCaption.Text = "調撥單-審核"
                WI_Qty.Visible = True

                GridView2.Columns("MC_Qty").OptionsColumn.AllowEdit = False
                GridView2.Columns("MC_Remark").OptionsColumn.AllowEdit = False
                GridView2.Columns("WTypeName").OptionsColumn.AllowEdit = False

                ComMC_IN_Company.Enabled = False
                ComMC_IN_WHName.Enabled = False
                CheckMC_InCheck.Visible = False

                popWareMoveOut.Enabled = False

            Case "InCheck"
                Panel1.Enabled = False
                txtMC_NO.Text = EditID
                LoadData(EditItem)

                LabCaption.Text = "調撥單-收料確認"
                WI_Qty.Visible = False

                GridView2.Columns("MC_Qty").OptionsColumn.AllowEdit = False
                GridView2.Columns("MC_Remark").OptionsColumn.AllowEdit = False
                GridView2.Columns("WTypeName").OptionsColumn.AllowEdit = False

                ComMC_IN_Company.Enabled = False
                ComMC_IN_WHName.Enabled = False

                popWareMoveOut.Enabled = False
                CheckMC_InCheck.Enabled = True
            Case "View"
                Panel1.Enabled = False
                WI_Qty.Visible = False
                cmdSave.Visible = False
                txtMC_NO.Text = EditID

                LoadData(EditItem)

                LabCaption.Text = "調撥單-查看"

                ComMC_IN_Company.Enabled = False
                ComMC_IN_WHName.Enabled = False

                GridView2.OptionsBehavior.Editable = False

                popWareMoveOut.Enabled = False

        End Select
    End Sub

#Region "加載事件"
    Private Sub ComMC_IN_Company_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComMC_IN_Company.EditValueChanged
        If ComMC_IN_Company.Text <> "" Then
        Else
            Exit Sub
        End If
        AddWareHouse(ComMC_IN_Company.EditValue)
        'ComMC_IN_Company.Enabled = False
    End Sub


#End Region


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Select Case EditItem
            Case "Add"
                If CheckData() = True Then
                    SaveData()
                End If
            Case "Edit"
                If CheckData() = True Then
                    SaveUpdate()
                End If
            Case "Check"
                If My.Computer.Network.Ping(CenterIP) Then
                    If CheckData() = True Then
                        If JS > 0 Then
                            Exit Sub
                        End If
                        JS = JS + 1

                        CheckUpdateData()
                    End If
                Else
                    MsgBox("遠程數據庫不通,請檢查!")
                End If

            Case "InCheck"
                If My.Computer.Network.Ping(CenterIP) Then
                    If CheckData() = True Then

                        If JS > 0 Then
                            Exit Sub
                        End If
                        JS = JS + 1

                        InCheckUpdata()
                    End If
                Else
                    MsgBox("遠程數據庫不通,請檢查!")
                End If

        End Select
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub



#Region "操作函數"
    Function CheckData() As Boolean
        CheckData = True
        If ComMC_OUT_Company.Text = "" Then
            MsgBox("發出公司不能為空！", MsgBoxStyle.Information, "提示")
            CheckData = False
            ComMC_OUT_Company.Select()
            Exit Function
        End If

        If ComMC_OUT_WHName.Text = "" Then
            MsgBox("發出倉庫不能為空！", MsgBoxStyle.Information, "提示")
            CheckData = False
            ComMC_OUT_WHName.Select()
            Exit Function
        End If

        If ComMC_IN_Company.EditValue = "" Then
            MsgBox("接收公司不能為空！", MsgBoxStyle.Information, "提示")
            CheckData = False
            ComMC_IN_Company.Select()
            Exit Function
        End If

        If ComMC_IN_WHName.EditValue = "" Then
            MsgBox("接收倉庫不能為空！", MsgBoxStyle.Information, "提示")
            CheckData = False
            ComMC_IN_WHName.Select()
            Exit Function
        End If

        If DSmove.Tables("WareMove").Rows.Count <= 0 Then
            MsgBox("無數據加載！", MsgBoxStyle.Information, "提示")
            CheckData = False
            Exit Function
        End If

        If ComMC_OUT_Company.EditValue = ComMC_IN_Company.EditValue Then
            MsgBox("發料公司不能與接收公司相同！", MsgBoxStyle.Information, "提示")
            CheckData = False
            Exit Function
        End If

        If EditItem <> "InCheck" Then
            Dim i As Integer

            For i = 0 To DSmove.Tables("WareMove").Rows.Count - 1

                If IsDBNull(DSmove.Tables("WareMove").Rows(i)("MC_Qty")) = True Then
                    MsgBox("數量不能為空！", MsgBoxStyle.Information, "提示")
                    CheckData = False
                    Exit Function
                End If

                If DSmove.Tables("WareMove").Rows(i)("MC_Qty") <= 0 Then
                    MsgBox("數量必須大于0！", MsgBoxStyle.Information, "提示")
                    CheckData = False
                    Exit Function
                End If

                '2014-04-01      姚駿----------------------------------------------------------------------

                If IsDBNull(DSmove.Tables("WareMove").Rows(i)("WTypeName")) = True Then
                    MsgBox("調撥類型不能為空！", MsgBoxStyle.Information, "提示")
                    CheckData = False
                    Exit Function
                End If

                If DSmove.Tables("WareMove").Rows(i)("WTypeName") = String.Empty Then
                    MsgBox("調撥類型不能為空！", MsgBoxStyle.Information, "提示")
                    CheckData = False
                    Exit Function
                End If
                '------------------------------------------------------------------------------------------

                '判斷當前庫存是否足夠----------------------------------------------------------------------
                Dim wic As New WareInventoryMTController
                Dim wii As New WareInventoryInfo
                wii = wic.WareInventory_GetSub(DSmove.Tables("WareMove").Rows(i)("M_Code").ToString, ComMC_OUT_WHName.Tag)

                Dim Qty As Double
                If wii Is Nothing Then
                    MsgBox(DSmove.Tables("WareMove").Rows(i)("M_Code").ToString & ":  當前發出倉庫不存在此物料信息")
                    CheckData = False
                    Exit Function
                Else
                    Qty = wii.WI_Qty
                End If

                If Qty < CDbl(DSmove.Tables("WareMove").Rows(i)("MC_Qty").ToString) Then
                    MsgBox(DSmove.Tables("WareMove").Rows(i)("M_Code").ToString & ":  發出倉沒有此庫存或庫存不足!")
                    CheckData = False
                    Exit Function
                End If

                '2014-04-01   姚駿-----------------------------------------------------------------------------------------
                Dim relQty As Double
                relQty = RealQty(Qty, DSmove.Tables("WareMove").Rows(i)("M_Code").ToString)

                If relQty < CDbl(DSmove.Tables("WareMove").Rows(i)("MC_Qty").ToString) Then
                    MsgBox(DSmove.Tables("WareMove").Rows(i)("M_Code").ToString & ":  發出倉實際可用庫存不足!")
                    CheckData = False
                    Exit Function
                End If
                '----------------------------------------------------------------------------------------------------------

            Next

        End If

        ''審核時
        If EditItem = "Check" Then
            If oldCheck = CheckMC_Check.Checked Then
                MsgBox("審核狀態未改變,不能保存!")
                CheckData = False
                Exit Function
            End If
        End If

        '收料審核
        If EditItem = "InCheck" Then
            If oldInCheck = CheckMC_InCheck.Checked Then
                MsgBox("收料審核狀態未改變,不能保存!")
                CheckData = False
                Exit Function
            End If
        End If

        '2014-03-28   姚駿------------------------------------------------------------------------------------------------------------------------------
        Dim mvc As New WareMoveCompanyController
        Dim mvl As New List(Of WareMoveCompanyInfo)
      


        mvl = mvc.WareMoveCompany_GetList(Nothing, txtMC_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        

        If mvl.Count > 0 Then
            If mvl(0).MC_InCheck Then
                MsgBox("收料失敗,收料狀態已經確認!")
                CheckData = False
                Exit Function
            End If
        End If

        '-----------------------------------------------------------------------------------------------------------------------------------------------

    End Function


    Function GetMC_Num() As String
        Dim str1 As String
        Dim ac As New WareMoveCompanyController
        Dim ai As New List(Of WareMoveCompanyInfo)
        str1 = Format(Now, "yyMM")

        Dim Stra As String
        Stra = ComMC_OUT_Company.Text & str1

        ai = ac.WareMoveCompany_GetNum(Stra, ComMC_OUT_Company.Text)
        If ai.Count = 0 Then
            GetMC_Num = Stra & "00001"
        Else
            GetMC_Num = Stra & Mid((CInt(Mid(ai.Item(0).MC_Num, Len(Stra) + 1)) + 100001), 2)
        End If
    End Function

    Function GetMC_NO() As String
        Dim str1 As String
        Dim ac As New WareMoveCompanyController
        Dim ai As New List(Of WareMoveCompanyInfo)
        str1 = Format(Now, "yyMM")

        Dim Stra As String
        Stra = "MV" & Trim(ComMC_OUT_Company.Text) & str1

        ai = ac.WareMoveCompany_GetNO(Stra, ComMC_OUT_Company.Text)
        If ai.Count = 0 Then
            GetMC_NO = Stra & "00001"
        Else
            GetMC_NO = Stra & Mid((CInt(Mid(ai.Item(0).MC_NO, Len(Stra) + 1)) + 100001), 2)
        End If
    End Function

    Sub SaveData()
        Dim mc As New WareMoveCompanyController
        Dim mi As New WareMoveCompanyInfo

        mi.MC_NO = GetMC_NO()

        mi.MC_OUT_Company = ComMC_OUT_Company.Text
        mi.MC_OUT_WHID = ComMC_OUT_WHName.Tag
        mi.MC_OUT_WHName = ComMC_OUT_WHName.Text

        mi.MC_IN_Company = ComMC_IN_Company.EditValue
        mi.MC_IN_WHID = ComMC_IN_WHName.EditValue
        mi.MC_IN_WHName = ComMC_IN_WHName.Text

        Dim i As Integer
        For i = 0 To DSmove.Tables("WareMove").Rows.Count - 1
            mi.MC_Num = GetMC_Num()
            mi.M_Code = DSmove.Tables("WareMove").Rows(i)("M_Code").ToString
            mi.MC_Qty = DSmove.Tables("WareMove").Rows(i)("MC_Qty")

            mi.MC_AddAction = InUserID
            mi.MC_AddActionName = UserName
            mi.MC_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            mi.MC_Remark = DSmove.Tables("WareMove").Rows(i)("MC_Remark").ToString


            mi.WTypeName = DSmove.Tables("WareMove").Rows(i)("WTypeName").ToString

            If mc.WareMoveCompany_AddOne(mi) = False Then
                MsgBox("部分數據保存失敗,請檢查!")
                Exit Sub
            End If
        Next


        MsgBox("數據增加成功！")
        Me.Close()

    End Sub



    Sub SaveUpdate()
        Dim mc As New WareMoveCompanyController
        Dim mi As New WareMoveCompanyInfo

        mi.MC_NO = txtMC_NO.Text

        mi.MC_OUT_Company = ComMC_OUT_Company.Text
        mi.MC_OUT_WHID = ComMC_OUT_WHName.Tag
        mi.MC_OUT_WHName = ComMC_OUT_WHName.Text

        mi.MC_IN_Company = ComMC_IN_Company.EditValue
        mi.MC_IN_WHID = ComMC_IN_WHName.EditValue
        mi.MC_IN_WHName = ComMC_IN_WHName.Text

        Dim i As Integer
        For i = 0 To DSmove.Tables("WareMove").Rows.Count - 1


            mi.MC_Num = DSmove.Tables("WareMove").Rows(i)("MC_Num").ToString
            mi.M_Code = DSmove.Tables("WareMove").Rows(i)("M_Code").ToString
            mi.MC_Qty = DSmove.Tables("WareMove").Rows(i)("MC_Qty")

            mi.MC_AddAction = InUserID
            mi.MC_AddActionName = UserName
            mi.MC_AddDate = Format(Now, "yyyy/MM/dd HH:mm:ss")
            mi.MC_Remark = DSmove.Tables("WareMove").Rows(i)("MC_Remark").ToString

            mi.WTypeName = DSmove.Tables("WareMove").Rows(i)("WTypeName").ToString

            If IsDBNull(DSmove.Tables("WareMove").Rows(i)("AutoID")) = True Then
                mi.MC_Num = GetMC_Num()
                If mc.WareMoveCompany_AddOne(mi) = False Then
                    MsgBox("部分數據保存失敗,請檢查!")
                    Exit Sub
                End If
            Else
                mi.AutoID = DSmove.Tables("WareMove").Rows(i)("AutoID")
                If mc.WareMoveCompany_UpdateOne(mi) = False Then
                    MsgBox("部分數據保存失敗,請檢查!")
                    Exit Sub
                End If
            End If

        Next

        Dim j As Integer
        ''刪除一些歷史記錄---------------------------------------------------------------------------------
        If DSmove.Tables("DelDataWareMove").Rows.Count > 0 Then
            For j = 0 To DSmove.Tables("DelDataWareMove").Rows.Count - 1
                If IsDBNull(DSmove.Tables("WareMove").Rows(j)("AutoID")) = False Then
                    Dim mcd As New WareMoveCompanyController
                    Dim strAutoID As String
                    strAutoID = DSmove.Tables("WareMove").Rows(j)("AutoID")

                    If mcd.WareMoveCompany_Delete(strAutoID, Nothing, Nothing, Nothing) = False Then
                        MsgBox("部分數據刪除失敗,請檢查!")
                        Exit Sub
                    End If
                End If
            Next
        End If


        MsgBox("修改成功！")

        Me.Close()

    End Sub

    Sub CheckUpdateData()
        '----------------------------------------------------------------
        Dim mc As New WareMoveCompanyController
        Dim mi As New WareMoveCompanyInfo

        mi.MC_OUT_Company = ComMC_OUT_Company.Text
        mi.MC_CheckRemark = txtMC_CheckRemark.Text

        Dim i As Integer
        For i = 0 To DSmove.Tables("WareMove").Rows.Count - 1
            mi.AutoID = DSmove.Tables("WareMove").Rows(i)("AutoID")
            mi.MC_Num = DSmove.Tables("WareMove").Rows(i)("MC_Num").ToString
            mi.MC_Check = CheckMC_Check.Checked
            mi.MC_CheckAction = InUserID
            mi.MC_CheckActionName = UserName
            mi.MC_CheckDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

            If mc.WareMoveCompany_Check(mi) = False Then
                MsgBox("數據審核成功!")
                Exit Sub
            End If
        Next

        '要傳 數據至中央數據庫-----------------------------------------------
        Dim SQL As String
        SQL = "insert into WareMoveCompanyCenter(MC_NO,IN_Company,OUT_Company,MC_Type,RSign) values('" & txtMC_NO.Text & "','" & ComMC_IN_Company.EditValue & "','" & ComMC_OUT_Company.Text & "','Send',0)"
        If ExecuteNonQuery(SQL) = 0 Then
            MsgBox("遠端數據保存失敗!")
            Exit Sub
        End If

        MsgBox("保存成功!")
        Me.Close()

    End Sub

    Sub InCheckUpdata()
        Dim mc As New WareMoveCompanyController
        Dim mi As New WareMoveCompanyInfo

        mi.MC_OUT_Company = ComMC_OUT_Company.Text

        Dim i As Integer
        For i = 0 To DSmove.Tables("WareMove").Rows.Count - 1
            mi.AutoID = DSmove.Tables("WareMove").Rows(i)("AutoID")
            mi.MC_Num = DSmove.Tables("WareMove").Rows(i)("MC_Num").ToString

            mi.MC_InCheck = CheckMC_Check.Checked
            mi.MC_InAction = InUserID
            mi.MC_InActionName = UserName
            mi.MC_InDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

            If mc.WareMoveCompany_INCheck(mi) = False Then
                MsgBox("部分數據保存失敗,請檢查!")
                Exit Sub
            End If
        Next

        ''要傳中間表,進行扣數處理--------------------
        For i = 0 To DSmove.Tables("WareMove").Rows.Count - 1
            Dim SQL As String
            SQL = "insert into WareMoveCompanyCenter(MC_NO,M_Code,Qty,IN_Company,OUT_Company,IN_WareHouse,OUT_WareHouse,MC_Type,RSign,MC_InAction,MC_InActionName) values('" _
            & txtMC_NO.Text & "','" & DSmove.Tables("WareMove").Rows(i)("M_Code") & "'," & DSmove.Tables("WareMove").Rows(i)("MC_Qty") & ",'" & ComMC_IN_Company.EditValue & _
            "','" & ComMC_OUT_Company.Text & "','" & ComMC_IN_WHName.EditValue & "','" & ComMC_OUT_WHName.Tag & "','InCheck','False','" & InUserID & "','" & UserName & "')"
            If ExecuteNonQuery(SQL) = 0 Then
                MsgBox("遠端數據保存失敗!")
                Exit Sub
            End If
        Next

        MsgBox("保存成功!")
        Me.Close()

    End Sub


    '加載數據
    Sub LoadData(ByVal CaoType As String)
        Dim mvc As New WareMoveCompanyController
        Dim mvl As New List(Of WareMoveCompanyInfo)
        'ComMC_OUT_Company     txtMC_NO

        'TypeInorOut

        If (CaoType = "View" And TypeInorOut = "收入項目") Or CaoType = "InCheck" Then
            mvl = mvc.WareMoveCompany_GetList(Nothing, txtMC_NO.Text, Nothing, Nothing, Company, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Else
            mvl = mvc.WareMoveCompany_GetList(Nothing, txtMC_NO.Text, Company, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        End If

        If mvl.Count <= 0 Then
            MsgBox("無數據存在！")
            Exit Sub
        End If
        '--------------------------------------------

        ComMC_OUT_Company.Text = mvl(0).MC_OUT_Company
        ComMC_OUT_Company.EditValue = mvl(0).MC_OUT_Company


        ComMC_OUT_WHName.Text = mvl(0).MC_OUT_WHName
        ComMC_OUT_WHName.Tag = mvl(0).MC_OUT_WHID

        ComMC_IN_Company.EditValue = mvl(0).MC_IN_Company

        ComMC_IN_WHName.EditValue = mvl(0).MC_IN_WHID

        DateMC_AddDate.EditValue = Format(CDate(mvl(0).MC_AddDate), "yyyy/MM/dd")

        ''審核相關
        CheckMC_Check.Checked = mvl(0).MC_Check
        oldCheck = mvl(0).MC_Check
        labMC_CheckActionName.Text = mvl(0).MC_CheckActionName
        txtMC_CheckRemark.Text = mvl(0).MC_CheckRemark

        ''收料確認
        CheckMC_InCheck.Checked = mvl(0).MC_InCheck
        oldInCheck = mvl(0).MC_InCheck

        DSmove.Tables("WareMove").Rows.Clear()

        Dim i As Integer
        Dim row As DataRow
        For i = 0 To mvl.Count - 1
            row = DSmove.Tables("WareMove").NewRow
            row("AutoID") = mvl(i).AutoID
            row("MC_Num") = mvl(i).MC_Num
            row("M_Name") = mvl(i).M_Name
            row("M_Gauge") = mvl(i).M_Gauge
            row("M_Unit") = mvl(i).M_Unit
            '--------------------------------------------------
            row("M_Code") = mvl(i).M_Code
            row("MC_Qty") = mvl(i).MC_Qty
            row("MC_Remark") = mvl(i).MC_Remark
            row("MC_OUT_EndQty") = mvl(i).MC_OUT_EndQty

            row("MC_IN_EndQty") = mvl(i).MC_IN_EndQty

            '@2013/1/28 添加 顯示庫存
            Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi1 = wc1.WareInventory_GetSub(mvl(i).M_Code, ComMC_OUT_WHName.Tag)

            If wi1 Is Nothing Then
                row("WI_Qty") = 0
            Else
                row("WI_Qty") = wi1.WI_Qty
            End If

            '2014-04-01   姚駿-------------------------------
            row("WI_RelQty") = RealQty(CDbl(row("WI_Qty")), row("M_Code"))
            row("WTypeName") = mvl(i).WTypeName
            '------------------------------------------------

            DSmove.Tables("WareMove").Rows.Add(row)

        Next

    End Sub

#End Region

#Region "添加數據"

    Private Sub popWareMoveOutAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveOutAdd.Click
        If ComMC_IN_Company.Text = "" Then
            ComMC_IN_Company.Select()
            MsgBox("請選擇接收公司!")
            Exit Sub
        End If

        If ComMC_IN_WHName.Text = "" Then
            ComMC_IN_WHName.Select()
            MsgBox("請選擇接收倉庫!")
            Exit Sub
        End If

        '---------------------------------------------

        tempCode = ""
        tempValue5 = ComMC_OUT_WHName.Tag
        tempValue6 = "倉庫管理"
        frmBOMSelect.ShowDialog()
        '增加記錄
        If tempCode = "" Then

            Exit Sub
        Else
            AddRow(tempCode, 0)
        End If
    End Sub

    Sub AddRow(ByVal strCode As String, ByVal Qty As Double)
        If strCode = "" Then
        Else
            Dim i As Integer
            For i = 0 To DSmove.Tables("WareMove").Rows.Count - 1
                If strCode = DSmove.Tables("WareMove").Rows(i)("M_Code") Then
                    MsgBox("一張單不允許有重復物料編碼....")
                    Exit Sub
                End If
            Next

            Dim mc As New LFERP.Library.Material.MaterialController
            Dim objInfo As New LFERP.Library.Material.MaterialInfo
            objInfo = mc.MaterialCode_Get(strCode)
            If objInfo Is Nothing Then
                MsgBox("不存在此物料編碼信息!")
                Exit Sub
            End If
            Dim row As DataRow
            row = DSmove.Tables("WareMove").NewRow

            row("M_Code") = objInfo.M_Code
            row("MC_Qty") = Qty
            row("M_Name") = objInfo.M_Name
            row("M_Unit") = objInfo.M_Unit
            row("M_Gauge") = objInfo.M_Gauge

            '@2013/1/28 添加 顯示庫存
            Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
            Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
            wi1 = wc1.WareInventory_GetSub(strCode, ComMC_OUT_WHName.Tag)

            If wi1 Is Nothing Then
                row("WI_Qty") = 0
            Else
                row("WI_Qty") = wi1.WI_Qty
            End If

            '2014-04-01   姚駿-------------------------------
            row("WI_RelQty") = RealQty(CDbl(row("WI_Qty")), row("M_Code"))
            '------------------------------------------------


            DSmove.Tables("WareMove").Rows.Add(row)
            GridView2.MoveLast()
        End If
    End Sub


    ''' <summary>
    ''' 返回實際可用庫存數量
    ''' 2014-04-01   姚駿
    ''' </summary>
    ''' <param name="wiQty">庫存數量</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RealQty(ByVal wiQty As Double, ByVal mCode As String) As Double

        Dim mvc As New WareMoveCompanyController
        Dim mvl As New List(Of WareMoveCompanyInfo)

        mvl = mvc.WareMoveCompany_GetList(Nothing, Nothing, Company, Nothing, Nothing, Nothing, mCode, Nothing, Nothing, Nothing, False)

        If mvl.Count <= 0 Then
            Return wiQty
        Else
            Dim nCount As Double

            For nIndex As Integer = 0 To mvl.Count - 1
                If txtMC_NO.Text <> mvl(nIndex).MC_NO Then
                    nCount = nCount + CDbl(mvl(nIndex).MC_Qty)
                End If
            Next
            Return (CDbl(wiQty) - nCount)
        End If

    End Function


    Private Sub popWareMoveOutDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveOutDel.Click
        If GridView2.RowCount = 0 Then Exit Sub
        Dim DelTemp As String
        DelTemp = GridView2.GetRowCellDisplayText(ArrayToString(GridView2.GetSelectedRows()), "AutoID")
        If DelTemp = "AutoID" Then
        Else
            '在刪除表中增加被刪除的記錄
            Dim row As DataRow = DSmove.Tables("DelDataWareMove").NewRow
            row("AutoID") = DelTemp
            DSmove.Tables("DelDataWareMove").Rows.Add(row)
        End If
        DSmove.Tables("WareMove").Rows.RemoveAt(CInt(ArrayToString(GridView2.GetSelectedRows())))
    End Sub
    Private Sub popWareMoveCenterAcc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles popWareMoveCenterAcc.Click

        If ComMC_IN_Company.Text = "" Then
            ComMC_IN_Company.Select()
            MsgBox("請選擇接收公司!")
            Exit Sub
        End If

        If ComMC_IN_WHName.Text = "" Then
            ComMC_IN_WHName.Select()
            MsgBox("請選擇接收倉庫!")
            Exit Sub
        End If
        '---------------------------------------------
        Dim i, n As Integer
        Dim myfrm As New frmRetrocedeSelect
        tempValue4 = "調撥作業-中央驗收"
        myfrm.ShowDialog()
        If RefreshT = True Then
            Dim arr(n) As String
            arr = Split(tempValue, ",")
            n = Len(Replace(tempValue, ",", "," & "*")) - Len(tempValue)

            For i = 0 To n

                Dim wic As New LFERP.Library.WareHouse.WareInput.WareInputContraller
                Dim wii As New LFERP.Library.WareHouse.WareInput.WareInputInfo

                wii = wic.WareInput_GetNUM(arr(i))

                Dim j As Integer
                For j = 0 To DSmove.Tables("WareMove").Rows.Count - 1
                    If wii.M_Code = DSmove.Tables("WareMove").Rows(j)("M_Code") Then
                        MsgBox("一張單不允許有重復物料編碼....")
                        Exit Sub
                    End If
                Next

                Dim row As DataRow = DSmove.Tables("WareMove").NewRow

                row("M_Code") = wii.M_Code
                row("M_Unit") = wii.M_Unit
                row("M_Name") = wii.M_Name
                row("M_Gauge") = wii.M_Gauge
                'row("OS_BatchID") = wii.OS_BatchID

                'row("MV_Qty") = wii.WIP_Qty
                'row("MV_Property") = "物料轉移"
                'row("MV_Remark") = Mid(wii.WIP_Remark, 1, 12) '記錄當前入庫單號對應物料的採購單號記錄

                '@2013/1/28 添加 顯示庫存
                Dim wi1 As LFERP.Library.WareHouse.WareInventory.WareInventoryInfo
                Dim wc1 As New LFERP.Library.WareHouse.WareInventory.WareInventoryMTController
                wi1 = wc1.WareInventory_GetSub(wii.M_Code, ComMC_OUT_WHName.Tag)

                If wi1 Is Nothing Then
                    row("WI_Qty") = 0
                Else
                    row("WI_Qty") = wi1.WI_Qty
                End If

                '2014-04-01   姚駿-------------------------------
                row("WI_RelQty") = RealQty(CDbl(row("WI_Qty")), row("M_Code"))
                '------------------------------------------------

                DSmove.Tables("WareMove").Rows.Add(row)
                GridView2.MoveLast()

            Next
        End If
        tempValue = ""
        RefreshT = False
    End Sub

#End Region

End Class