Imports LFERP.Library.ProductionPiecePayPersonnel
Imports System.Data.SqlClient
Imports LFERPDB

Public Class frmProductionPiecePayPersonnel

    Dim ds As New DataSet
    Dim strCaoType As String  '載入的操作類型
    Dim strAutoID As String  ''修改、查看的唯一
    Dim strPayFacID As String
    Dim strPayDepID As String

    Dim Load_OK As String ''確定Load事件是否已載入完畢 

    Dim strPer_Class As String

    Private Sub frmProductionPiecePayPersonnel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True

        strCaoType = tempValue
        strAutoID = tempValue2

        tempValue = Nothing
        tempValue2 = Nothing

        Select Case strCaoType

            Case "add"
                LabelCaption.Text = "個人計件薪金-增加"

                chkPL_Check.Visible = False

            Case "edit"
                LoadData(strAutoID)
                LabelCaption.Text = "個人計件薪金-修改"

                chkPL_Check.Visible = False
            Case "check"
                LoadData(strAutoID)
                LabelCaption.Text = "個人計件薪金-審核"


            Case "view"

                LabelCaption.Text = "個人計件薪金-查看"

                btnOK.Visible = False
                chkPL_Check.Visible = False
                LoadData(strAutoID)

        End Select

        Me.Text = "個人計件薪金"

        txtPL_OnDutyDays.Focus()
        txtPL_OnDutyDays.Select()

        Load_OK = "OK"

    End Sub


    Function LoadData(ByVal _AutoID As String) As Boolean

        Dim objInfo As New ProductionPiecePayPersonnelInfo
        Dim objList As New List(Of ProductionPiecePayPersonnelInfo)
        Dim oc As New ProductionPiecePayPersonnelContol
        objList = oc.ProductionPiecePayPersonnel_GetList(_AutoID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        LoadData = False

        If objList.Count = 1 Then
        Else
            MsgBox("沒有數據.")
            Exit Function
        End If

        LoadData = True


        txtPer_NO.Text = objList(0).Per_NO.ToString  '廠證編號
        txtPer_Name.Text = objList(0).Per_Name.ToString  '姓名

        strPayFacID = objList(0).FacID.ToString  '廠別
        txtFacID.Text = objList(0).PL_FacName.ToString

        strPayDepID = objList(0).DepID.ToString  '部門
        txtDepID.Text = objList(0).PL_DepName.ToString


        txtPer_DayPrice.Text = objList(0).Per_DayPrice  'Per_DayPrice             * real                /日薪  

        txtPL_OnDutyDays.Text = objList(0).PL_OnDutyDays 'PL_OnDutyDays            * real                  /上班天數

        txtPL_UsualOverHours.Text = objList(0).PL_UsualOverHours 'PL_UsualOverHours        * real                  /平時加班小時數

        txtPL_HolidayOVerHours.Text = objList(0).PL_HolidayOVerHours 'PL_HolidayOVerHours      * real                  /節假日加班小時數

        txtPL_TimeHours.Text = objList(0).PL_TimeHours 'PL_TimeHours             * real                  /計時工時

        txtPL_CompensateSum.Text = objList(0).PL_CompensateSum 'PL_CompensateSum         * real                  /應補金額

        txtPL_SubtractSum.Text = objList(0).PL_SubtractSum 'PL_SubtractSum           * real                /應扣金額

        txtPL_TimePay.Text = objList(0).PL_TimePay 'PL_TimePay               * real                /計時工資

        txtPL_PiecePay.Text = objList(0).PL_PiecePay 'PL_PiecePay              * real                /計件工資

        txtPL_MeritedPay.Text = objList(0).PL_MeritedPay 'PL_MeritedPay            * real                /應得工資

        txtPL_Remark.Text = objList(0).PL_Remark 'PL_Remark                * nvarchar(MAX)       /備注

        chkPL_Check.Checked = objList(0).PL_Check 'PL_Check                 * bit                   /審核

        DatePL_YYMM.EditValue = Format(CDate(objList(0).PL_YYMM.ToString), "yyyy年MM月")

        strPer_Class = objList(0).Per_Class


    End Function


    Private Sub SaveEdit()
        Dim pi As New ProductionPiecePayPersonnelInfo
        Dim pc As New ProductionPiecePayPersonnelContol

        If strAutoID <> "" Then
        Else
            MsgBox("自編號為空，請檢查.")
            Exit Sub
        End If

        pi.AutoID = strAutoID

        ''-------------------------------
        pi.Per_NO = txtPer_NO.Text
        pi.Per_Name = txtPer_Name.Text

        pi.FacID = strPayFacID   ''廠別編號
        pi.DepID = strPayDepID   ''部門編號

        pi.PL_YYMM = Format(CDate(DatePL_YYMM.EditValue), "yyyy/MM") '年/月
        ''-----------------------------------------------------------------

        pi.Per_DayPrice = Val(txtPer_DayPrice.Text) 'Per_DayPrice             * real                /日薪  

        pi.PL_OnDutyDays = Val(txtPL_OnDutyDays.Text) 'PL_OnDutyDays            * real                  /上班天數

        pi.PL_UsualOverHours = Val(txtPL_UsualOverHours.Text) 'PL_UsualOverHours        * real                  /平時加班小時數

        pi.PL_HolidayOVerHours = Val(txtPL_HolidayOVerHours.Text) 'PL_HolidayOVerHours      * real                  /節假日加班小時數

        pi.PL_TimeHours = Val(txtPL_TimeHours.Text) 'PL_TimeHours             * real                  /計時工時

        pi.PL_CompensateSum = Val(txtPL_CompensateSum.Text) 'PL_CompensateSum         * real                  /應補金額

        pi.PL_SubtractSum = Val(txtPL_SubtractSum.Text) 'PL_SubtractSum           * real                /應扣金額

        pi.PL_TimePay = Val(txtPL_TimePay.Text) 'PL_TimePay               * real                /計時工資

        pi.PL_PiecePay = Val(txtPL_PiecePay.Text) 'PL_PiecePay              * real                /計件工資

        pi.PL_MeritedPay = Val(txtPL_MeritedPay.Text) 'PL_MeritedPay            * real                /應得工資

        pi.PL_Remark = txtPL_Remark.Text 'PL_Remark                * nvarchar(MAX)       /備注

        pi.PL_ModifyUserID = InUserID 'PL_ModifyUserID          * nvarchar(20)         /修改人

        pi.PL_ModifyDate = Format(Now, "yyyy/MM/dd") 'PL_ModifyDate            * datetime             /修改日期

        pi.Per_Class = strPer_Class

        If pc.ProductionPiecePayPersonnel_Update(pi) = True Then
            If strCaoType = "check" Then
            Else
                MsgBox("保存成功!")
                Me.Close()
            End If
        Else
            MsgBox("保存失敗!")
        End If

    End Sub

    Private Sub SaveNew()
        Dim pi1 As New ProductionPiecePayPersonnelInfo
        Dim pc1 As New ProductionPiecePayPersonnelContol

        ''-------------------------------
        pi1.Per_NO = txtPer_NO.Text
        pi1.Per_Name = txtPer_Name.Text

        pi1.FacID = strPayFacID   ''廠別編號
        pi1.DepID = strPayDepID   ''部門編號

        pi1.PL_YYMM = Format(CDate(DatePL_YYMM.EditValue), "yyyy/MM")
        ''-----------------------------------------------------------------

        pi1.Per_DayPrice = Val(txtPer_DayPrice.Text) 'Per_DayPrice             * real                /日薪  

        pi1.PL_OnDutyDays = Val(txtPL_OnDutyDays.Text) 'PL_OnDutyDays            * real                  /上班天數

        pi1.PL_UsualOverHours = Val(txtPL_UsualOverHours.Text) 'PL_UsualOverHours        * real                  /平時加班小時數

        pi1.PL_HolidayOVerHours = Val(txtPL_HolidayOVerHours.Text) 'PL_HolidayOVerHours      * real                  /節假日加班小時數

        pi1.PL_TimeHours = Val(txtPL_TimeHours.Text) 'PL_TimeHours             * real                  /計時工時

        pi1.PL_CompensateSum = Val(txtPL_CompensateSum.Text) 'PL_CompensateSum         * real                  /應補金額

        pi1.PL_SubtractSum = Val(txtPL_SubtractSum.Text) 'PL_SubtractSum           * real                /應扣金額

        pi1.PL_TimePay = Val(txtPL_TimePay.Text) 'PL_TimePay               * real                /計時工資

        pi1.PL_PiecePay = Val(txtPL_PiecePay.Text) 'PL_PiecePay              * real                /計件工資

        pi1.PL_MeritedPay = Val(txtPL_MeritedPay.Text) 'PL_MeritedPay            * real                /應得工資

        pi1.PL_Remark = txtPL_Remark.Text 'PL_Remark                * nvarchar(MAX)       /備注

        pi1.PL_AddUserID = InUserID 'PL_AddUserID             * nvarchar(20)          /添加人(操作人)編號

        pi1.PL_AddDate = Format(Now, "yyyy/MM/dd") 'PL_AddDate               * datetime             /添加日期

        pi1.PL_Check = False

        If pc1.ProductionPiecePayPersonnel_Add(pi1) = True Then
            MsgBox("保存成功!")
            Me.Close()
        Else
            MsgBox("保存失敗!")
        End If

    End Sub


    Private Sub SaveCheck()
        Dim pi2 As New ProductionPiecePayPersonnelInfo
        Dim pc2 As New ProductionPiecePayPersonnelContol

        If strAutoID <> "" Then
        Else
            MsgBox("自編號為空，請檢查.")
            Exit Sub
        End If

        pi2.AutoID = strAutoID
        pi2.PL_Check = chkPL_Check.Checked  'PL_Check                 * bit                   /審核
        pi2.PL_CheckUserID = InUserID 'PL_CheckUserID           * nvarchar(20)          /審核編號
        pi2.PL_CheckDate = Format(Now, "yyyy/MM/dd") 'PL_CheckDate             * datetime              /審核日期

        If pc2.ProductionPiecePayPersonnel_Updatecheck(pi2) = True Then
            MsgBox("審核成功!")
            Me.Close()
        Else
            MsgBox("審核失敗!")
        End If

    End Sub






    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If strCaoType = "check" Then  ''審核
            SaveEdit()  '同事可以保存
            SaveCheck()
        End If

        'If strCaoType = "add" Then  '增加
        '    SaveNew()
        'End If

        If strCaoType = "edit" Then  ''修改
            SaveEdit()
        End If

    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Function SumData() As String   ''計算數據
        SumData = ""

        ''計時額
        'If Val(txtPL_TimeHours.Text) <> 0 Then
        '    txtPL_TimePay.Text = Math.Round(Val(txtPer_DayPrice.Text) / 8 * Val(txtPL_TimeHours.Text), 2)
        'End If

        '計件額
        txtPL_MeritedPay.Text = Math.Round(Val(txtPL_TimePay.Text) + Val(txtPL_PiecePay.Text) + Val(txtPL_CompensateSum.Text) - Val(txtPL_SubtractSum.Text))

    End Function


    Private Sub txtPer_DayPrice_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPer_DayPrice.EditValueChanged, txtPL_TimeHours.EditValueChanged, txtPL_TimePay.EditValueChanged, txtPL_PiecePay.EditValueChanged, txtPL_CompensateSum.EditValueChanged, txtPL_SubtractSum.EditValueChanged

        If Load_OK = "OK" Then
            SumData()
        End If

    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click

        Dim strStat_Date, strEnd_Date As String
        ''---------------------------------------------------------------------------------
        Dim intInputMonth, intInputYear As Integer '这是你输入的月份                                '|
        intInputMonth = Val(Format(CDate(DatePL_YYMM.EditValue), "MM"))                 '| 
        intInputYear = Val(Format(CDate(DatePL_YYMM.EditValue), "yyyy"))

        Dim dt As New DateTime(intInputYear, intInputMonth, 1)                 '|
        '计算该月份的天数
        Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear                '|
        If days <= 0 Or days > 31 Then
            days = 31
        End If

        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
        ''---------------------------------------------------------------------------------


        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
        Dim PPT As New LFERP.Library.ProductionSumTimePersonnel.ProductionSumTimePersonnelControl
        Dim PPPS As New LFERP.Library.ProductionPieceProcess.ProductionPieceProcessControl
        Dim PPPa As New LFERP.Library.ProductionPiecePersonnel.ProductionPiecePersonnelControl

        Dim strCompany As String
        strCompany = Mid(strInDPT_ID, 1, 4)   '獲得登錄者所屬公司ID,以返回公司名稱，LOGO
        ds.Clear()
        ds.Tables.Clear()
        Dim ltc1 As New CollectionToDataSet
        Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet
        Dim ltc4 As New CollectionToDataSet
        Dim ltc5 As New CollectionToDataSet

        'If PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, txtPer_NO.Text, Nothing, strPayDepID, strPayFacID, Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, UserName).Count <= 0 Then
        '    ltc1.CollToDataSet(ds, "ProductionSumPiecePersonnel", PPP.NothingNew)
        'Else
        '    ltc1.CollToDataSet(ds, "ProductionSumPiecePersonnel", PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, txtPer_NO.Text, Nothing, strPayDepID, strPayFacID, Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, UserName))
        'End If
        Dim bz As String = ""


        If PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, txtPer_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, UserName).Count <= 0 Then
            ltc1.CollToDataSet(ds, "ProductionSumPiecePersonnel", PPP.NothingNew)

            'MsgBox("1")

            Dim PPPSL As New List(Of LFERP.Library.ProductionPieceProcess.ProductionPieceProcessInfo)
            PPPSL = PPPS.ProductionPieceProcess_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)


            ds.Tables("ProductionSumPiecePersonnel").Rows(0)("Per_NO") = txtPer_NO.Text
            ds.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_Per_Name") = Me.txtPer_Name.Text
            ds.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_DateEnd") = Format(CDate(strEnd_Date), "yyyy/MM/dd")
            ds.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_DateStart") = Format(CDate(strStat_Date), "yyyy/MM/dd")
            ds.Tables("ProductionSumPiecePersonnel").Rows(0)("PP_AutoID") = PPPSL(0).AutoID
            ds.Tables("ProductionSumPiecePersonnel").Rows(0)("DepID") = ""
            ds.Tables("ProductionSumPiecePersonnel").Rows(0)("FacID") = ""
        Else
            ltc1.CollToDataSet(ds, "ProductionSumPiecePersonnel", PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, txtPer_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, UserName))
        End If

        '' MsgBox(ds.Tables("ProductionSumPiecePersonnel").Rows.Count)

        If PPT.ProductionSumTimePersonnel_GetList(Nothing, txtPer_NO.Text, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, Nothing, Nothing, Nothing).Count <= 0 Then
            ltc2.CollToDataSet(ds, "ProductionSumTimePersonnel", PPT.NothingNew)
        Else
            ltc2.CollToDataSet(ds, "ProductionSumTimePersonnel", PPT.ProductionSumTimePersonnel_GetList(Nothing, txtPer_NO.Text, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, Nothing, Nothing, Nothing))
        End If

        ltc3.CollToDataSet(ds, "ProductionPieceProcess", PPPS.ProductionPieceProcess_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        ltc4.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strCompany, Nothing, Nothing, Nothing))

        ltc5.CollToDataSet(ds, "ProductionPiecePersonnel", PPPa.ProductionPiecePersonnel_GetList(Nothing, txtPer_NO.Text, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
     
        PreviewRPTDialog1(ds, "rptProductionPersonnelSumPieceTime", "個人計件表打印", strInUserRank, strInUserRank, True, True)

        ltc1 = Nothing
        ltc2 = Nothing
        ltc3 = Nothing
        ltc4 = Nothing
        ltc5 = Nothing

        '  Me.Close()
    End Sub

    '@ 2012/11/7 添加
    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn.Click
        'Dim myCommand As SqlCommand
        Dim strSql As String
        Dim myConn As New SqlConnection(KaoQinConn)
        Dim da As SqlDataAdapter

        Dim cn As New LFERPDataBase
        Dim strCn As New SqlConnection(cn.LoadConnStr)

        Dim strStarEndDate As String
        Dim strStat_Date As Date
        Dim strEnd_Date As Date

        Dim mcCompany As New LFERP.DataSetting.CompanyControler
        Dim PPP As New LFERP.Library.ProductionSumPiecePersonnel.ProductionSumPiecePersonnelControl
        Dim PPT As New LFERP.Library.ProductionSumTimePersonnel.ProductionSumTimePersonnelControl

        'Dim ltc1 As New CollectionToDataSet
        'Dim ltc2 As New CollectionToDataSet
        Dim ltc3 As New CollectionToDataSet

        strStat_Date = Format(DatePL_YYMM.DateTime, "yyyy-MM") & "-1"
        strEnd_Date = DateAdd("d", -1, DateAdd("M", 1, strStat_Date))
        strStarEndDate = "BETWEEN '" & strStat_Date & "' AND '" & strEnd_Date & "'"

        myConn.Open()

        ds.Tables.Clear()
        strSql = "SELECT * FROM  vwCheckin WHERE BadgeID ='" & txtPer_NO.Text & "'  AND " _
                & " CheckInDate " & strStarEndDate & " ORDER BY BadgeID,CheckInDate"
        da = New SqlDataAdapter(strSql, myConn)
        da.Fill(ds, "vwCheckin")

        strSql = "SELECT * FROM  KQDetail WHERE BadgeID ='" & txtPer_NO.Text & "'  AND " _
                & " KQDate " & strStarEndDate & " ORDER BY BadgeID,KQDate"
        da = New SqlDataAdapter(strSql, myConn)
        da.Fill(ds, "KQDetail")
        myConn.Close()


        strCn.Open()

        'strSql = "select pt.per_no ,pt_date ,SUM(pt_total) as pt_total,SUM(pt_total*per_dayprice/8) as pt_sum " & _
        '              "from ProductionSumTimePersonnel pt inner join ProductionPiecePersonnel pp on pt.Per_NO =pp.Per_NO " & _
        '              "group by pt.per_no,pt_date having pt_date " & strStarEndDate & " and pt.per_no='" & txtPer_NO.Text & "'"
        strSql = "select pt.per_no ,pt_date ,SUM(pt_total) as pt_total,sum(PT_Total * SampPrice)  as pt_sum " & _
                      "from ProductionSumTimePersonnel pt inner join ProductionPiecePersonnel pp on pt.Per_NO =pp.Per_NO " & _
                      "group by pt.per_no,pt_date having pt_date " & strStarEndDate & " and pt.per_no='" & txtPer_NO.Text & "'"

        da = New SqlDataAdapter(strSql, strCn)
        da.Fill(ds, "ProductionSumTimePersonnel")

        strSql = "select per_no,pp_date,SUM(pp_qty) as pp_qty,SUM(pp_qty*pp_price) as pp_sum from ViewSumPiecePersonnel" & _
                     " group by per_no,pp_date having pp_date " & strStarEndDate & " and per_no='" & txtPer_NO.Text & "'"

        da = New SqlDataAdapter(strSql, strCn)
        da.Fill(ds, "ProductionSumPiecePersonnel")

        strCn.Close()
        'If PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, txtPer_NO.Text, Nothing, strPayDepID, strPayFacID, Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, UserName).Count <= 0 Then
        '    ltc1.CollToDataSet(ds, "ProductionSumPiecePersonnel", PPP.NothingNew)
        'Else
        '    ltc1.CollToDataSet(ds, "ProductionSumPiecePersonnel", PPP.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, txtPer_NO.Text, Nothing, strPayDepID, strPayFacID, Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, UserName))
        'End If

        'If PPT.ProductionSumTimePersonnel_GetList(Nothing, txtPer_NO.Text, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, Nothing, Nothing).Count <= 0 Then
        '    ltc2.CollToDataSet(ds, "ProductionSumTimePersonnel", PPT.NothingNew)
        'Else
        '    ltc2.CollToDataSet(ds, "ProductionSumTimePersonnel", PPT.ProductionSumTimePersonnel_GetList(Nothing, txtPer_NO.Text, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, Nothing, Nothing))
        'End If

        ltc3.CollToDataSet(ds, "Company", mcCompany.Company_Getlist(strInCompany, Nothing, Nothing, Nothing))

        PreviewRPTDialog1(ds, "rptProductionPiecePersonnelDaySum", "個人日計件匯總表", txtFacID.Text, txtDepID.Text, True, True)

        'ltc1 = Nothing
        'ltc2 = Nothing
        ltc3 = Nothing

        '   Me.Close()
    End Sub

    Private Sub UpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
        LoadKQSumMonth(txtPer_NO.Text, Format(CDate(DatePL_YYMM.EditValue), "yyyMM"))


        txtPL_OnDutyDays.Text = DoubleNormalDays '上班天數
        txtPL_UsualOverHours.Text = DoubleExtraHours '平時加班
        txtPL_HolidayOVerHours.Text = DoubleWeekTime '假日加班
    End Sub

    Private Sub txtPer_DayPrice_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPer_DayPrice.KeyUp, txtPL_PiecePay.KeyUp, txtPL_CompensateSum.KeyUp, txtPL_SubtractSum.KeyUp
        Dim Str As String = ""
        If (e.KeyValue > 47 And e.KeyValue < 58) Or (e.KeyValue > 95 And e.KeyValue < 106) Or (e.KeyValue = 8) Or (e.KeyValue = 45) Or (e.KeyValue = 46) Or (e.KeyValue = 110) Then
            Str = sender.Text
        Else
            sender.Text = Str
            sender.Focus()
        End If
    End Sub
End Class