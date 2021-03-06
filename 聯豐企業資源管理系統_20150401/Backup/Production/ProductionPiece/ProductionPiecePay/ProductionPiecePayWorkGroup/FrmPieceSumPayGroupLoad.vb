Imports LFERP.Library.ProductionPieceWorkGroup
Imports LFERP.DataSetting
Imports LFERP.Library.ProductionPiecePayWGMain
Imports LFERP.Library.ProductionPiecePayWGSub
Imports LFERP.Library.ProductionPiecePersonnelDay
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionPiecePayWGAdjust

Public Class FrmPieceSumPayGroupLoad

    Dim ds As New DataSet

    Private Sub FrmPieceSumPayGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ''加載廠別名稱()
        'Dim fc As New FacControler
        'lueFacID.Properties.DisplayMember = "FacName"
        'lueFacID.Properties.ValueMember = "FacID"
        'lueFacID.Properties.DataSource = fc.GetFacList(strInFacID, Nothing)

        CreateTable()
        Load_Fac()

        Date_YYMM.EditValue = Format(Now, "yyyy年MM月")
        RadioModle.Checked = True

        Dim ppwg As New ProductionPieceWorkGroupControl
        G_NO.Properties.DisplayMember = "G_Name"
        G_NO.Properties.ValueMember = "G_NO"
        G_NO.Properties.DataSource = ppwg.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing)

        G_NO.Focus()
        G_NO.Select()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


    Private Sub lueFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueFacID.EditValueChanged
        ' Dim dc As New DepartmentControler
        If lueFacID.EditValue Is Nothing Then Exit Sub

        'lueDepID.Properties.DisplayMember = "DepName"
        'lueDepID.Properties.ValueMember = "DepID"
        'lueDepID.Properties.DataSource = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
        Load_Dep()
    End Sub

    Private Sub RadioModle1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioModle1.CheckedChanged
        If RadioModle1.Checked = False Then
            RadioModle.Checked = True
            G_NO.Enabled = False
            lueFacID.Enabled = True
            lueDepID.Enabled = True
        Else
            RadioModle.Checked = False
            G_NO.Enabled = False
            lueFacID.Enabled = True
            lueDepID.Enabled = True
        End If
    End Sub

    Private Sub RadioModle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioModle.CheckedChanged
        If RadioModle.Checked = False Then
            RadioModle1.Checked = True
            G_NO.Enabled = False
            lueFacID.Enabled = True
            lueDepID.Enabled = True
        Else
            RadioModle1.Checked = False
            lueFacID.Enabled = False
            lueDepID.Enabled = False
            G_NO.Enabled = True
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If RadioModle.Checked = True Then
            If G_NO.EditValue Is Nothing Then
                MsgBox("請輸入組別編號!")
                G_NO.Focus()
                Exit Sub
            End If
            If Load_PiecePayWorkGroup(G_NO.EditValue) = True Then
                MsgBox("保存成功")
                Me.Close()
            End If
        End If

        If RadioModle1.Checked = True Then
            If lueFacID.EditValue Is Nothing Then
                MsgBox("請輸入廠別編號!")
                lueFacID.Focus()
                Exit Sub
            End If

            If lueDepID.EditValue Is Nothing Then
                lueDepID.Focus()
                MsgBox("請輸入廠別編號!")
                Exit Sub
            End If

            If Load_PieceWGPayDepFac() = True Then
                '  MsgBox("保存成功!")
            End If
        End If

    End Sub

    Function Load_PiecePayWorkGroup(ByVal _G_NO As String) As Boolean
        'Dim _G_NO As String
        'Dim i As Integer

        Load_PiecePayWorkGroup = True

        Dim strStat_Date, strEnd_Date As String
        ''---------------------------------------------------------------------------------
        Dim intInputMonth, intInputYear As Integer '这是你输入的月份                                '|
        intInputMonth = Val(Format(CDate(Date_YYMM.EditValue), "MM"))                 '| 
        intInputYear = Val(Format(CDate(Date_YYMM.EditValue), "yyyy"))

        Dim dt As New DateTime(intInputYear, intInputMonth, 1)                 '|
        '计算该月份的天数
        Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear                '|
        If days <= 0 Or days > 31 Then
            days = 31
        End If

        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
        ''---------------------------------------------------------------------------------

        Dim pc As New ProductionPieceWorkGroupControl
        Dim pl As New List(Of ProductionPieceWorkGroupInfo)

        ''查詢出組別-----------------

        pl = pc.ProductionPieceWorkGroup_GetList(_G_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pl.Count > 0 Then
        Else
            Load_PiecePayWorkGroup = False
            MsgBox("組別表中無此組信息，請檢查!")
            Exit Function
        End If

        If Delete_PiecePay(Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _G_NO) = "N" Then
            GoTo AAA
        End If

        '-----------------------寫流水號--------------------------------------------
        Dim strGet_No As String
        strGet_No = ProductionPiecePayWorkGroup_GetNO()
        If strGet_No = "" Then
            Load_PiecePayWorkGroup = False
            MsgBox("獲取表號失敗!")
            Exit Function
        End If
        ''------------------------------------------------------------------
        Dim DoublePY_PieceAllSum As Double
        Dim DoublePY_TimeAllSum As Double

        Dim PayWGCSumP As New ProductionPiecePayWGMainControl
        Dim PayWGiSumP As New ProductionPiecePayWGMainInfo
        PayWGiSumP = PayWGCSumP.ProductionSumPieceWorkGroupView(_G_NO, strStat_Date, strEnd_Date)
        DoublePY_PieceAllSum = Math.Round(PayWGiSumP.PWGtotal_P, 2)  '查詢統計出指定組別某一月份計時總額/計件總額

        Dim PayWGiSumT As New ProductionPiecePayWGMainInfo
        Dim PayWGCSumT As New ProductionPiecePayWGMainControl
        PayWGiSumT = PayWGCSumT.ProductionSumTimeWorkGroupView(_G_NO, strStat_Date, strEnd_Date)
        DoublePY_TimeAllSum = Math.Round(PayWGiSumT.PWGtotal_T, 2)

        ''------------------------------------------------------------------

        Dim PayWGC As New ProductionPiecePayWGMainControl
        Dim PayWGI As New ProductionPiecePayWGMainInfo

        PayWGI.PY_ID = strGet_No
        PayWGI.G_NO = _G_NO

        PayWGI.FacID = pl(0).FacID
        PayWGI.DepID = pl(0).DepID

        ''---------------------------------
        PayWGI.PY_PieceAllSum = DoublePY_PieceAllSum
        PayWGI.PY_TimeAllSum = DoublePY_TimeAllSum

        ''------------------------------

        ''*****************************************************************************************
        '2013/03/26 修改 
        Dim DoubleG_NO_InSum As Double = 0
        Dim k As Integer
        '轉入本組金額
        Dim AC As New ProductionPiecePayWGAdjustControl
        Dim AL As New List(Of ProductionPiecePayWGAdjustInfo)

        AL = AC.ProductionPiecePayWGAdjust_GetList(Nothing, Nothing, Nothing, Nothing, _G_NO, Nothing, Nothing, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), Nothing, Nothing, "True")
        If AL.Count > 0 Then
            For k = 0 To AL.Count - 1
                DoubleG_NO_InSum = DoubleG_NO_InSum + AL(k).Amount
            Next
        End If
        PayWGI.G_NO_InSum = DoubleG_NO_InSum

        '轉出本組金額
        Dim DoubleG_NO_OUTSum As Double = 0
        Dim AC1 As New ProductionPiecePayWGAdjustControl
        Dim AL1 As New List(Of ProductionPiecePayWGAdjustInfo)

        AL1 = AC1.ProductionPiecePayWGAdjust_GetList(Nothing, _G_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), Nothing, Nothing, "True")
        If AL1.Count > 0 Then
            For k = 0 To AL1.Count - 1
                DoubleG_NO_OUTSum = DoubleG_NO_OUTSum + AL1(k).Amount
            Next
        End If
        PayWGI.G_NO_OUTSum = DoubleG_NO_OUTSum

        ''*****************************************************************************************




        PayWGI.PY_AddDate = Format(Now, "yyyy/MM/dd")
        PayWGI.PY_AddUserID = InUserID

        PayWGI.PY_YYMM = Format(CDate(Date_YYMM.EditValue), "yyyy/MM")

        If PayWGC.ProductionPiecePayWGMain_Add(PayWGI) = False Then
            Load_PiecePayWorkGroup = False
            MsgBox("組別信息保存失敗!")
            Exit Function
        End If

        ''-----------------------------------------------------------------------------------------------------------------------------
        If LoadDay_WorkGroup_Personnel(_G_NO, PayWGI.DepID, strGet_No, strStat_Date, strEnd_Date) = True Then
        Else
            Load_PiecePayWorkGroup = False
            'MsgBox("子表保存失敗，請檢查！")
            Exit Function
        End If

        ' Me.Close()

AAA:

    End Function

    '根據組別編號，從每日人員名單 中導入，員工信息(寫入子表)

    Function LoadDay_WorkGroup_Personnel(ByVal _G_NO As String, ByVal _DepID As String, ByVal _PY_ID As String, ByVal _strStat_Date As String, ByVal _strEnd_Date As String) As Boolean

        LoadDay_WorkGroup_Personnel = True
        Dim i As Integer

        Dim Pdayc As New ProductionPiecePersonnelDayControl
        Dim Pdayi As New ProductionPiecePersonnelDayInfo
        Dim PdayL As New List(Of ProductionPiecePersonnelDayInfo)

        '查詢出當月在本組的所有人員名單
        PdayL = Pdayc.ProductionPiecePersonnelDay_GetList1(_G_NO, Nothing, Nothing, _DepID, Nothing, _strStat_Date, Nothing, _strEnd_Date, "=")
        If PdayL.Count > 0 Then
        Else
            LoadDay_WorkGroup_Personnel = False
            'MsgBox("組別編號為：" & _G_NO & "本月無人員參與計件！")
            Exit Function
        End If

        For i = 0 To PdayL.Count - 1 '存入每個人員的基本 信息

            ''查詢出每個員工 的薪金類型-------基本表中-----------------------

            Dim Ppc As New ProductionPiecePersonnelControl
            Dim PpL As New List(Of ProductionPiecePersonnelInfo)

            '已辭工的不匯總2013-5-8

            PpL = Ppc.ProductionPiecePersonnel_GetList(Nothing, PdayL(i).Per_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "False", Nothing, Nothing)
            If PpL.Count > 0 Then

      

                Dim PaySubC As New ProductionPiecePayWGSubControl
                Dim PaySubI As New ProductionPiecePayWGSubInfo

                PaySubI.Per_PayType = PpL(0).Per_PayType '薪金類型----------------------2013-5-8  
                PaySubI.Per_DayPrice = PpL(0).Per_DayPrice '日薪------------------------2013-5-8

                PaySubI.Per_NO = PdayL(i).Per_NO
                PaySubI.Per_Name = PdayL(i).Per_Name

                PaySubI.PY_ID = _PY_ID '表號

                ' /在指定組工作的天數---------------------------------------
                Dim Pdc As New ProductionPiecePersonnelDayControl
                Dim PdL As New List(Of ProductionPiecePersonnelDayInfo)

                PdL = Pdc.ProductionPiecePersonnelDay_GetList(Nothing, Nothing, PdayL(i).Per_NO, Nothing, _G_NO, _DepID, Nothing, Nothing, _strStat_Date, Nothing, Nothing, _strEnd_Date, Nothing)
                PaySubI.PYS_WorkWGDay = PdL.Count

                '' 讀考勤表，加入 “上班天數” "平日加班"  "假日加班"
                ' LoadKQSumMonth(PdayL(i).Per_NO, Format(CDate(Date_YYMM.EditValue), "yyyMM"))

                LoadKQSumMonth(PdayL(i).Per_NO, Format(CDate(Date_YYMM.EditValue), "yyyyMM"))  ''暫時用三月份的 

                PaySubI.PYS_OnDutyDays = DoubleNormalDays '上班天數
                PaySubI.PYS_UsualOverTime = DoubleExtraHours '平時加班
                PaySubI.PYS_HolidayOVerTime = DoubleWeekTime '假日加班

                ''獎金 都設為0先
                PaySubI.PYS_Bonus = 0

                PaySubC.ProductionPiecePayWGSub_Add(PaySubI)
            End If

        Next

        ''其它要導入的信息從考勤表中導入    --------------------------------------------------------

    End Function

    ''' <summary>
    ''' 從組別表中 查詢本部門所有組別 再逐個導入
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Load_PieceWGPayDepFac() As Boolean

        Load_PieceWGPayDepFac = True

        Dim i As Integer
        Dim pc As New ProductionPieceWorkGroupControl
        Dim pl As New List(Of ProductionPieceWorkGroupInfo)

        Dim StrlueFacID As String
        Dim StrlueDepID As String
        If lueFacID.EditValue = "*" Then
            StrlueFacID = Nothing
        Else
            StrlueFacID = lueFacID.EditValue
        End If
        If lueDepID.EditValue = "*" Then
            StrlueDepID = Nothing
        Else
            StrlueDepID = lueDepID.EditValue
        End If

        ''查詢出組別-----------------

        pl = pc.ProductionPieceWorkGroup_GetList(Nothing, Nothing, Nothing, StrlueDepID, StrlueFacID, Nothing, Nothing, Nothing)

        If pl.Count > 0 Then
        Else
            Load_PieceWGPayDepFac = False
            MsgBox("此部門無組別信息存在，請檢查!")
            Exit Function
        End If

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = pl.Count

        For i = 0 To pl.Count - 1  '循環本部門存在的所有組別  Load_PiecePayWorkGroup
            ProgressBar1.Value = i
            ''組別編號
            If Load_PiecePayWorkGroup(pl(i).G_NO) = False Then
                Load_PieceWGPayDepFac = False
                ' Exit Function
            End If
        Next
        ProgressBar1.Visible = False

        MsgBox("保存成功！")

        Me.Close()
    End Function
    ''' <summary>
    ''' 刪除已審核或當月組別已匯總
    ''' </summary>
    ''' <param name="_Date_YYMM"></param>
    ''' <param name="_strdG_NO"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Delete_PiecePay(ByVal _Date_YYMM As String, ByVal _strdG_NO As String) As String
        Delete_PiecePay = "Y"

        Dim pdcs As New ProductionPiecePayWGSubControl

        Dim pdc As New ProductionPiecePayWGMainControl
        Dim pdl As New List(Of ProductionPiecePayWGMainInfo)

        Dim strPY_ID, strAutoID As String
        Dim strCheck As String

        If CheckEditWG.Checked = True Then  ''若勾選上“刪除已審核” 則要刪除 當月指定組，審核與未審核的
            strCheck = Nothing
        Else
            strCheck = "False"
        End If

        '判斷是否添加， 不勾選 and 已審核-------------------------------------------------------
        If strCheck = "False" Then
            Dim pdc1 As New ProductionPiecePayWGMainControl
            Dim pdl1 As New List(Of ProductionPiecePayWGMainInfo)

            pdl1 = pdc1.ProductionPiecePayWGMain_GetList(Nothing, Nothing, _strdG_NO, _Date_YYMM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, "True")

            If pdl1.Count > 0 Then
                Delete_PiecePay = "N"
            End If
        End If
        ''--------------------------------------------------------------------------------------


        pdl = pdc.ProductionPiecePayWGMain_GetList(Nothing, Nothing, _strdG_NO, _Date_YYMM, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, strCheck)

        If pdl.Count > 0 Then
        Else
            Exit Function
        End If

        strPY_ID = pdl(0).PY_ID
        strAutoID = pdl(0).AutoID

        If pdcs.ProductionPiecePayWGSub_Delete(Nothing, strPY_ID) = True Then
            If pdc.ProductionPiecePayWGMain_Delete(strAutoID) = True Then '再刪除主表 
            Else
                ' Delete_PiecePay = False
            End If
        Else
            'Delete_PiecePay = False
        End If
    End Function

    ''' <summary>
    ''' 獲取編號-------
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ProductionPiecePayWorkGroup_GetNO() As String

        ProductionPiecePayWorkGroup_GetNO = ""

        Dim str1, str2 As String
        Dim gc1 As New ProductionPiecePayWGMainControl
        Dim gi1 As New ProductionPiecePayWGMainInfo

        str1 = Mid(Year(Now), 3)
        If CInt(Month(Now)) < 10 Then
            str2 = "0" & Month(Now)
        Else
            str2 = Month(Now)
        End If

        Dim Stra As String
        Stra = Trim(str1 & str2)

        gi1 = gc1.ProductionPiecePayWGMain_GetNO(Stra) '' 讀取基數

        If gi1 Is Nothing Then
            ProductionPiecePayWorkGroup_GetNO = "PA" & str1 & str2 & "0001"
        Else
            ProductionPiecePayWorkGroup_GetNO = "PA" & str1 & str2 & Mid((CInt(Mid(gi1.PY_ID, 7)) + 10001), 2)
        End If
    End Function


    Sub Load_Fac()
        ' 加載廠別名稱() 
        Dim fc As New FacControler
        Dim fcl As New List(Of FacInfo)
        Dim i As Integer

        fcl = fc.GetFacList(strInFacID, Nothing)

        If fcl.Count <= 0 Then Exit Sub

        ds.Tables("TFac").Clear()

        If strInFacID = Nothing Then
            Dim row As DataRow
            row = ds.Tables("TFac").NewRow
            row("FacName") = "全部"
            row("FacID") = "*"
            ds.Tables("TFac").Rows.Add(row)
        End If

        For i = 0 To fcl.Count - 1
            Dim row1 As DataRow
            row1 = ds.Tables("TFac").NewRow
            row1("FacName") = fcl(i).FacName
            row1("FacID") = fcl(i).FacID
            ds.Tables("TFac").Rows.Add(row1)
        Next
    End Sub

    Sub Load_Dep()
        ' 加載廠別名稱()
        Dim dc As New DepartmentControler
        Dim dil As New List(Of DepartmentInfo)
        Dim i As Integer

        If lueFacID.EditValue = "*" Then
            dil = dc.BriName_GetList(strInDepID, Nothing, Nothing)
        Else
            dil = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
        End If
        If dil.Count <= 0 Then Exit Sub

        ds.Tables("TDep").Clear()

        If strInDepID = Nothing Then
            Dim row As DataRow
            row = ds.Tables("TDep").NewRow
            row("DepName") = "全部"
            row("DepID") = "*"
            ds.Tables("TDep").Rows.Add(row)
        End If

        For i = 0 To dil.Count - 1
            Dim row1 As DataRow
            row1 = ds.Tables("TDep").NewRow
            row1("DepName") = dil(i).DepName
            row1("DepID") = dil(i).DepID
            ds.Tables("TDep").Rows.Add(row1)
        Next

    End Sub

    Sub CreateTable()
        ds.Tables.Clear()

        With ds.Tables.Add("TFac")
            .Columns.Add("FacName", GetType(String))
            .Columns.Add("FacID", GetType(String))
        End With

        lueFacID.Properties.DisplayMember = "FacName"
        lueFacID.Properties.ValueMember = "FacID"
        lueFacID.Properties.DataSource = ds.Tables("TFac")

        With ds.Tables.Add("TDep")
            .Columns.Add("DepName", GetType(String))
            .Columns.Add("DepID", GetType(String))
        End With

        lueDepID.Properties.DisplayMember = "DepName"
        lueDepID.Properties.ValueMember = "DepID"
        lueDepID.Properties.DataSource = ds.Tables("TDep")

    End Sub

End Class