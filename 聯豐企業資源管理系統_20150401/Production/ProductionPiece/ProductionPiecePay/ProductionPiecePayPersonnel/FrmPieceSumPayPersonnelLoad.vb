Imports LFERP.DataSetting
Imports LFERP.Library.ProductionPiecePersonnel
Imports LFERP.Library.ProductionPiecePayPersonnel

Imports LFERP.Library.ProductionSumTimePersonnel
Imports LFERP.Library.ProductionSumPiecePersonnel
Imports LFERP.Library.ProductionPiecePersonnelMothClass

Public Class FrmPieceSumPayPersonnelLoad

    Dim ds As New DataSet

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub lueFacID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lueFacID.EditValueChanged
        'Dim dc As New DepartmentControler
        If lueFacID.EditValue Is Nothing Then Exit Sub

        'lueDepID.Properties.DisplayMember = "DepName"
        'lueDepID.Properties.ValueMember = "DepID"
        'lueDepID.Properties.DataSource = dc.BriName_GetList(strInDepID, Nothing, lueFacID.EditValue)
        Load_Dep()

    End Sub
    ''' <summary>
    ''' 按人員導入---
    ''' </summary>
    ''' <param name="_Per_NO"></param>
    ''' <param name="_DepID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Load_PiecePayPersonnel(ByVal _Per_NO As String, ByVal _DepID As String, ByVal _FacID As String) As Boolean

        Dim strStat_Date, strEnd_Date As String
        ''-----------------------------------------------------------------------------|
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
        ''-----------------------------------------------------------------------------|
        Dim i As Integer

        Load_PiecePayPersonnel = True
        Dim PPPC As New ProductionPiecePersonnelControl
        Dim PPPL As List(Of ProductionPiecePersonnelInfo)

        PPPL = PPPC.ProductionPiecePersonnel_GetList(Nothing, _Per_NO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If PPPL.Count > 0 Then
        Else
            MsgBox("人員表記錄中不存在本人信息，請檢查!")
            Exit Function
        End If

        ''要判斷一下，本月有無 個人計時，計件記錄以。
        ''統計參與個人計件，但又在組別名單中的

        If SumPieceTimePersonnel(_Per_NO, _DepID, strStat_Date, strEnd_Date) = False Then
            Exit Function
        End If
        '------------------------------------------------------

        For i = 0 To PPPL.Count - 1
            ''--------------------------------------------------------------------------
            '刪除當月已匯總數據   若有審核則 不加入了
            'If Delete_PiecePayPer(Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _Per_NO, _DepID) = "N" Then
            If Delete_PiecePayPer(Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _Per_NO, _DepID) = "N" Then
                GoTo AAA
            End If

            ''查詢統計出指定員工某一月份計時總額---------------------------------------------------------------
            Dim DoublePL_PiecePay As Double
            Dim DoublePL_TimePay As Double

            Dim DoublePer_DayPrice As Double
            Dim DoublicPL_TimeHours As Double

            Dim PayPerCSumP As New ProductionPiecePayPersonnelContol
            Dim PayPeriSumP As New ProductionPiecePayPersonnelInfo
            PayPeriSumP = PayPerCSumP.ProductionSumPiecePersonnelView(_Per_NO, strStat_Date, strEnd_Date, Nothing, Nothing)  ''2013-5-6 去部門

            DoublePL_PiecePay = Math.Round(PayPeriSumP.PPGtotal_P, 2)  '查詢統計出指定組別某一月份計時總額/計件總額

            Dim PayWGiSumT As New ProductionPiecePayPersonnelInfo
            Dim PayWGCSumT As New ProductionPiecePayPersonnelContol
            PayWGiSumT = PayWGCSumT.ProductionSumTimePersonnelView(_Per_NO, strStat_Date, strEnd_Date, Nothing, Nothing) ''2013-5-6 去部門

            '2013-5-18 改為做辦直接返回
            ' ''DoublePer_DayPrice = PPPL(0).Per_DayPrice ''日薪   
            ' ''DoublicPL_TimeHours = Math.Round(PayWGiSumT.PPGtotal_T, 2)  ''總工時

            '' ''計時額=員工日薪/8*正常止班
            ' ''DoublePL_TimePay = Math.Round(PPPL(0).Per_DayPrice / 8 * DoublicPL_TimeHours, 2)


            DoublePL_TimePay = Math.Round(PayWGiSumT.PPGtotal_T, 2)
            DoublePer_DayPrice = PPPL(0).Per_DayPrice ''日薪   
            DoublicPL_TimeHours = Math.Round(PayWGiSumT.PT_Total_Sum, 2)  ''總工時

            ''--------------------------------------------------------------------------------------------------------

            Dim PayC As New ProductionPiecePayPersonnelContol
            Dim PayI As New ProductionPiecePayPersonnelInfo

            ''由視圖匯總出來的
            PayI.PL_PiecePay = DoublePL_PiecePay '計件額
            PayI.PL_TimePay = DoublePL_TimePay '計時額

            PayI.PL_MeritedPay = Math.Round(DoublePL_PiecePay + DoublePL_TimePay)  ''應得總工資

            PayI.Per_DayPrice = DoublePer_DayPrice '日薪
            PayI.PL_TimeHours = DoublicPL_TimeHours '總小時數
            ''++++++++++++++++++++++++++++++++++++++++++++++++++

            PayI.Per_NO = PPPL(i).Per_NO.ToString '廠証編號號
            PayI.Per_Name = PPPL(i).Per_Name.ToString '姓名
            PayI.FacID = _FacID
            PayI.DepID = _DepID

            PayI.PL_AddDate = Format(Now, "yyyy/MM/dd")
            PayI.PL_AddUserID = InUserID

            PayI.PL_YYMM = Format(CDate(Date_YYMM.EditValue), "yyyy/MM")
            ''--------------------------------------------------------------------------------------------------------
            '' 讀考勤表，加入 “上班天數” "平日加班"  "假日加班"
            LoadKQSumMonth(PPPL(i).Per_NO, Format(CDate(Date_YYMM.EditValue), "yyyMM"))
            ' LoadKQSumMonth(PPPL(i).Per_NO, "201204")  ''暫時用三月份的 

            PayI.PL_OnDutyDays = DoubleNormalDays '上班天數
            PayI.PL_UsualOverHours = DoubleExtraHours '平時加班
            PayI.PL_HolidayOVerHours = DoubleWeekTime '假日加班
            ''-----------------------------------------------------------------------

            ' PayI.Per_Class = Find_Per_Class(_Per_NO)
            ' PayI.Per_Class = PPPL(i).Per_Class  '現改為從基本表中 2013-5-7
            PayI.Per_Class = PPPL(i).KQClass  '改為從基本表中考勤班制 2013-8-18
            If PayC.ProductionPiecePayPersonnel_Add(PayI) = False Then
                MsgBox("部分人員保存失敗!")
            End If
AAA:


        Next
    End Function
    ''' <summary>
    ''' 查詢出指定員工指定月所屬班制
    ''' </summary>
    ''' <param name="Per_NOA"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Find_Per_Class(ByVal Per_NOA As String) As String   '查詢
        Find_Per_Class = ""

        Dim Pec As New ProductionPiecePersonnelMothClassControl
        Dim pel As New List(Of ProductionPiecePersonnelMothClassInfo)

        pel = Pec.ProductionPiecePersonnelMothClass_GetList(Per_NOA, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), Nothing)

        If pel.Count <= 0 Then

            Dim ptil As New List(Of ProductionPiecePersonnelInfo)  '載入指定員工
            Dim ptc As New ProductionPiecePersonnelControl
            ptil = ptc.ProductionPiecePersonnel_GetList(Nothing, Per_NOA, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If ptil.Count > 0 Then
                Find_Per_Class = ptil(0).Per_Class
            Else
                Find_Per_Class = ""
            End If

        Else
            Find_Per_Class = pel(0).Per_Class
        End If
    End Function

    ''' <summary>
    ''' 由部門導入 流程為 在個人計件,個人計時表是查詢出，當前部門在當月有作業記錄的 人員列表 ，生成本人在本部門的個人計件，計時薪金
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Load_PiecePayDepFac() As Boolean

        Load_PiecePayDepFac = True

        Dim strStat_Date, strEnd_Date As String
        ''-----------------------------------------------------------------------------|
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
        ''-----------------------------------------------------------------------------|
        Dim k, j As Integer

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

        '2013-8-18
        Dim StrKQClass As String
        If cboPer_Class.EditValue = "全部" Then
            StrKQClass = Nothing
        Else
            StrKQClass = cboPer_Class.EditValue
        End If
        ''----------------------------------------------------------------------------------

        Dim PayPc1 As New ProductionPiecePayPersonnelContol
        Dim PayPl1 As New List(Of ProductionPiecePayPersonnelInfo)

        'PayPl1 = PayPc1.ProductionSumPieceTimePersonnelDepView(StrlueFacID, StrlueDepID, strStat_Date, strEnd_Date)
        PayPl1 = PayPc1.ProductionSumPieceTimePersonnelDepViewKQClass(StrlueFacID, StrlueDepID, strStat_Date, strEnd_Date, StrKQClass, Format(CDate(Date_YYMM.EditValue), "yyyy-MM"))

        ' 1).在由廠別/部門組合導入,員工匯總個人薪金(個人計件.個人計時)，時查詢出當月有個人計時作業，個人計件作業的人員.
        If PayPl1.Count <= 0 Then
            MsgBox("無數據存在,請檢查!")
            Exit Function
        End If

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = PayPl1.Count

        For k = 0 To PayPl1.Count - 1

            ProgressBar1.Value = k

            Dim PayPc As New ProductionPiecePayPersonnelContol
            Dim PayPl As New List(Of ProductionPiecePayPersonnelInfo)
            PayPl = PayPc.ProductionSumPieceTimePersonnelView(PayPl1(k).Per_NO, strStat_Date, strEnd_Date)
            '1).在由指定員工導入,員工匯總個人薪金(個人計件.個人計時)，查詢出指定員工作業的所有部門.
            If PayPl.Count > 0 Then
                For j = 0 To PayPl.Count - 1
                    If Load_PiecePayPersonnel(PayPl1(k).Per_NO, PayPl(j).DepID, PayPl(j).FacID) = False Then
                        MsgBox(PayPl1(k).Per_NO.ToString & "保存失敗！")
                        'Exit Function
                    End If
                Next
            End If
        Next

        ProgressBar1.Visible = False

        MsgBox("保存成功！")
        Me.Close()

    End Function

    ''Function Load_PiecePayDepFac() As Boolean

    ''    Load_PiecePayDepFac = True

    ''    Dim strStat_Date, strEnd_Date As String
    ''    ''-----------------------------------------------------------------------------|
    ''    Dim intInputMonth As Integer '这是你输入的月份                                '|
    ''    intInputMonth = Val(Format(CDate(Date_YYMM.EditValue), "MM"))                 '| 

    ''    Dim dt As New DateTime(DateTime.Today.Year, intInputMonth, 1)                 '|
    ''    '计算该月份的天数
    ''    Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear                '|
    ''    strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
    ''    strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|
    ''    ''-----------------------------------------------------------------------------|
    ''    Dim k As Integer

    ''    Dim PayPc1 As New ProductionPiecePayPersonnelContol
    ''    Dim PayPl1 As New List(Of ProductionPiecePayPersonnelInfo)

    ''    PayPl1 = PayPc1.ProductionSumPieceTimePersonnelDepView(lueFacID.EditValue, lueDepID.EditValue, strStat_Date, strEnd_Date)

    ''    If PayPl1.Count <= 0 Then Exit Function

    ''    For k = 0 To PayPl1.Count - 1
    ''        If Load_PiecePayPersonnel(PayPl1(k).Per_NO, lueDepID.EditValue, lueFacID.EditValue) = False Then
    ''            Exit Function
    ''            MsgBox("部分人員保存失敗!")
    ''        End If
    ''    Next

    ''    MsgBox("保存成功！")
    ''    Me.Close()

    ''End Function

    ''' <summary>
    ''' 判斷是否刪除已審核
    ''' </summary>
    ''' <param name="_Date_YYMM"></param>
    ''' <param name="_strPer_NO"></param>
    ''' <param name="_strDepID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Delete_PiecePayPer(ByVal _Date_YYMM As String, ByVal _strPer_NO As String, ByVal _strDepID As String) As String

        Delete_PiecePayPer = "Y"

        Dim pdc As New ProductionPiecePayPersonnelContol
        Dim pdl As New List(Of ProductionPiecePayPersonnelInfo)

        Dim strAutoID As String
        Dim strCheck As String

        If CheckEditPer.Checked = True Then  ''若勾選上“刪除已審核” 則要刪除 當月指定組，審核與未審核的
            strCheck = Nothing
        Else
            strCheck = "False"
        End If


        '判斷是否添加， 不勾選 and 已審核---
        If strCheck = "False" Then
            Dim pdc1 As New ProductionPiecePayPersonnelContol
            Dim pdl1 As New List(Of ProductionPiecePayPersonnelInfo)

            ' pdl1 = pdc1.ProductionPiecePayPersonnel_GetList(Nothing, _strPer_NO, Nothing, _Date_YYMM, _strDepID, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            '去部門
            pdl1 = pdc1.ProductionPiecePayPersonnel_GetList(Nothing, _strPer_NO, Nothing, _Date_YYMM, Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

            If pdl1.Count > 0 Then
                Delete_PiecePayPer = "N"
            End If
        End If
        ''----------------------------------------------------------
        '去部門
        'pdl = pdc.ProductionPiecePayPersonnel_GetList(Nothing, _strPer_NO, Nothing, _Date_YYMM, _strDepID, strCheck, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        pdl = pdc.ProductionPiecePayPersonnel_GetList(Nothing, _strPer_NO, Nothing, _Date_YYMM, Nothing, strCheck, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pdl.Count > 0 Then
        Else
            Exit Function
        End If

        Dim KK As Integer

        For KK = 0 To pdl.Count - 1
            strAutoID = pdl(KK).AutoID

            If pdc.ProductionPiecePayPersonnel_Delete(strAutoID) = True Then
            End If
        Next


        

    End Function

    ''' <summary>
    ''' 判斷有無個人計時，計件記錄
    ''' </summary>
    ''' <param name="_Per_NO1"></param>
    ''' <param name="_DepID1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SumPieceTimePersonnel(ByVal _Per_NO1 As String, ByVal _DepID1 As String, ByVal _StrStat_Date As String, ByVal _StrEnd_Date As String) As Boolean

        Dim PPSPC As New ProductionSumPiecePersonnelControl
        Dim PPSPL As New List(Of ProductionSumPiecePersonnelInfo)
        PPSPL = PPSPC.ProductionSumPiecePersonnel_GetList(Nothing, Nothing, _Per_NO1, Nothing, _DepID1, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, _StrStat_Date, Nothing, _StrEnd_Date, Nothing)

        Dim PPSTC As New ProductionSumTimePersonnelControl
        Dim PPSTL As New List(Of ProductionSumTimePersonnelInfo)
        PPSTL = PPSTC.ProductionSumTimePersonnel_GetList(Nothing, _Per_NO1, Nothing, _DepID1, Nothing, _StrStat_Date, Nothing, _StrEnd_Date, Nothing, Nothing, Nothing)

        If PPSPL.Count <= 0 And PPSTL.Count <= 0 Then
            SumPieceTimePersonnel = False
        Else
            SumPieceTimePersonnel = True
        End If

    End Function

    'Function Check_data(ByVal _Per_NO As String, ByVal _Dep_ID As String) As Boolean
    '    ''主要檢查，某一員工，在當月，本部門只存在一條匯總信息
    '    Check_data = True

    '    Dim PayC2 As New ProductionPiecePayPersonnelContol
    '    Dim PayL2 As New List(Of ProductionPiecePayPersonnelInfo)

    '    PayL2 = PayC2.ProductionPiecePayPersonnel_GetList(Nothing, _Per_NO, Nothing, Format(CDate(Date_YYMM.EditValue), "yyyy/MM"), _Dep_ID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    '    If PayL2.Count > 0 Then
    '        Check_data = False
    '    End If

    'End Function


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If RadioModle.Checked = True Then
            If txtPer_NO.EditValue Is Nothing Then
                MsgBox("請輸入員工編號!")
                Exit Sub
            End If
            Load_PieceTimeDepID()
        End If

        If RadioModle1.Checked = True Then
            If lueFacID.EditValue Is Nothing Then
                MsgBox("請輸入廠別編號!")
                Exit Sub
            End If

            If lueDepID.EditValue Is Nothing Then
                MsgBox("請輸入部門編號!")
                Exit Sub
            End If

            Load_PiecePayDepFac()
        End If

    End Sub

    ''' <summary>
    ''' 從人個計件 個人計時 信息中查詢出 指定員工所工作了的部門
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Load_PieceTimeDepID() As Boolean
        Load_PieceTimeDepID = True

        Dim strStat_Date, strEnd_Date As String
        ''-----------------------------------------------------------------------------|
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

        ''-----------------------------------------------------------------------------|
        Dim k As Integer

        Dim PayPc As New ProductionPiecePayPersonnelContol
        Dim PayPl As New List(Of ProductionPiecePayPersonnelInfo)
        PayPl = PayPc.ProductionSumPieceTimePersonnelView(txtPer_NO.EditValue, strStat_Date, strEnd_Date)

        If PayPl.Count <= 0 Then Exit Function

        For k = 0 To PayPl.Count - 1
            If Load_PiecePayPersonnel(txtPer_NO.EditValue, PayPl(k).DepID, PayPl(k).FacID) = False Then
                MsgBox("保存失敗！")
                Exit Function
            End If
        Next

        MsgBox("保存成功！")

    End Function

    Private Sub RadioModle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioModle.Click
        If RadioModle.Checked = False Then
            RadioModle1.Checked = True
        Else
            RadioModle1.Checked = False
        End If
    End Sub

    Private Sub RadioModle1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioModle1.Click
        If RadioModle1.Checked = False Then
            RadioModle.Checked = True
            txtPer_NO.Enabled = True
            lueFacID.Enabled = False
            lueDepID.Enabled = False
            cboPer_Class.Enabled = False
        Else
            RadioModle.Checked = False
            txtPer_NO.Enabled = False
            lueFacID.Enabled = True
            lueDepID.Enabled = True
            cboPer_Class.Enabled = True
        End If
    End Sub

    Private Sub RadioModle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioModle.CheckedChanged
        If RadioModle.Checked = False Then
            RadioModle1.Checked = True
            txtPer_NO.Enabled = False
            lueFacID.Enabled = True
            lueDepID.Enabled = True
            cboPer_Class.Enabled = True
        Else
            RadioModle1.Checked = False
            txtPer_NO.Enabled = True
            lueFacID.Enabled = False
            lueDepID.Enabled = False
            cboPer_Class.Enabled = False
        End If
    End Sub

    Private Sub FrmPieceSumPayPersonnelLoad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''加載廠別名稱
        'Dim fc As New FacControler
        'lueFacID.Properties.DisplayMember = "FacName"
        'lueFacID.Properties.ValueMember = "FacID"
        'lueFacID.Properties.DataSource = fc.GetFacList(strInFacID, Nothing)

        CreateTable()
        Load_Fac()

        Date_YYMM.EditValue = Format(Now, "yyyy年MM月")
        RadioModle.Checked = True

        Dim PPPC2 As New ProductionPiecePersonnelControl
        txtPer_NO.Properties.DisplayMember = "Per_Name"
        txtPer_NO.Properties.ValueMember = "Per_NO"
        txtPer_NO.Properties.DataSource = PPPC2.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, strInDepID, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        txtPer_NO.Focus()
        txtPer_NO.Select()
    End Sub

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

    Private Sub RadioModle1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioModle1.CheckedChanged

    End Sub
End Class