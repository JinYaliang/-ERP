Imports LFERP.Library.ProductionPiecePaySampType
Imports LFERP.Library.ProductionSumTimePersonnel
Imports LFERP.Library.ProductionSumTimeWorkGroup

Public Class frmProductionPiecePaySampType

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Exit Sub
    End Sub


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        SynchPieceData(Date_YYMM.EditValue)
    End Sub

    ''' <summary>
    ''' 判斷個人計時記錄
    ''' </summary>
    ''' <param name="strPer_NO"></param>
    ''' <param name="DepID"></param>
    ''' <param name="strDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckPiecePersonelTimeDate(ByVal strPer_NO As String, ByVal DepID As String, ByVal strDate As String) As Boolean
        CheckPiecePersonelTimeDate = True

        ''-------------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelContol
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayPersonnel.ProductionPiecePayPersonnelInfo)

        pdcl = pdc.ProductionPiecePayPersonnel_GetList(Nothing, strPer_NO, Nothing, Format(CDate(strDate), "yyyy/MM"), Nothing, True, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        If pdcl.Count > 0 Then   ''有記錄
            CheckPiecePersonelTimeDate = False
            Exit Function
        End If
        ''---------------------------------------------------------------------------

        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, DepID, Format(CDate(strDate), "yyyy/MM"))

        If plA.Count > 0 Then
            If plA(0).LockCheck = True Then

                Exit Function
            End If
        End If
        ''---------------------------------------------------------------------------

    End Function
    ''' <summary>
    ''' 判斷組別計時記錄
    ''' </summary>
    ''' <param name="strG_NO"></param>
    ''' <param name="strDate"></param>
    ''' <param name="DepID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckPieceWGTimeDate(ByVal strG_NO As String, ByVal DepID As String, ByVal strDate As String) As Boolean
        CheckPieceWGTimeDate = True
        ''-------------------------------------------------------------------------
        Dim pdc As New LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainControl
        Dim pdcl As New List(Of LFERP.Library.ProductionPiecePayWGMain.ProductionPiecePayWGMainInfo)


        pdcl = pdc.ProductionPiecePayWGMain_GetList(Nothing, Nothing, strG_NO, Format(CDate(strDate), "yyyy/MM"), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, True)

        If pdcl.Count > 0 Then   ''有記錄
            CheckPieceWGTimeDate = False
            Exit Function
        End If

        Dim pcA As New LFERP.Library.ProductionSumLock.ProductionSumLockControl
        Dim plA As New List(Of LFERP.Library.ProductionSumLock.ProductionSumLockInfo)
        plA = pcA.ProductionSumLock_GetList(Nothing, Nothing, DepID, Format(CDate(strDate), "yyyy/MM"))

        If plA.Count > 0 Then
            If plA(0).LockCheck = True Then
                CheckPieceWGTimeDate = False
                Exit Function
            End If
        End If

    End Function


    Function SynchPieceData(ByVal yyyyMM As String) As Boolean
        '更新個人計時數據,
        '查詢
        Dim strStat_Date, strEnd_Date As String
        ''-----------------------------------------------------------------------------|
        Dim intInputMonth, intInputYear As Integer '这是你输入的月份                                '|
        intInputMonth = Val(Format(CDate(yyyyMM), "MM"))                 '| 
        intInputYear = Val(Format(CDate(yyyyMM), "yyyy"))


        Dim dt As New DateTime(intInputYear, intInputMonth, 1)                 '|
        '计算该月份的天数
        Dim days As Integer = dt.AddMonths(1).DayOfYear - dt.DayOfYear                '|
        If days <= 0 Or days > 31 Then
            days = 31
        End If
        strStat_Date = (dt.AddDays(0).ToString("yyyy/MM/dd"))                         '|
        strEnd_Date = (dt.AddDays(days - 1).ToString("yyyy/MM/dd"))                   '|

        ''-----------------------------------------------------------------------------|

        Dim SignStr As String = ""
        Dim StrNONO As String = ""

        Dim PC As New ProductionSumTimePersonnelControl
        Dim PL As New List(Of ProductionSumTimePersonnelInfo)

        PL = PC.ProductionSumTimePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, Nothing, Nothing, Nothing)


        If PL.Count <= 0 Then
            GoTo W
        End If

        StrNONO = "Y"

        ProgressBar1.Visible = True
        ProgressBar1.Maximum = PL.Count

        Dim i As Integer
        For i = 0 To PL.Count - 1

            If CheckPiecePersonelTimeDate(PL(i).Per_NO, PL(i).DepID, yyyyMM) = True Then

                Dim SamC As New ProductionPiecePaySampTypeControl
                Dim SamL As New List(Of ProductionPiecePaySampTypeInfo)
                ''-----------------------------------------------------------------------------------------
                Dim StrSampID As String
                If PL(i).SampID = Nothing Then
                    StrSampID = "Z"
                Else
                    StrSampID = PL(i).SampID
                End If
                ''-----------------------------------------------------------------------------------------
                SamL = SamC.ProductionPiecePaySampType_GetList(StrSampID, Nothing, "True", Nothing) '查詢樣辦表

                If SamL.Count = 1 Then
                    If PC.ProductionSumTimePersonnelSamp_Update(PL(i).PT_NO, StrSampID, SamL(0).SampPrice) = False Then
                        SignStr = "E"
                    End If
                End If

            End If

            ProgressBar1.Value = i
        Next
W:


        '更新組別計時數據,
        Dim WC As New ProductionSumTimeWorkGroupControl
        Dim WL As New List(Of ProductionSumTimeWorkGroupInfo)

        WL = WC.ProductionSumTimeWorkGroup_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, strStat_Date, Nothing, strEnd_Date, Nothing, Nothing)

        If WL.Count <= 0 Then
            GoTo E
        End If

        StrNONO = "Y"

        ProgressBar1.Maximum = WL.Count

        Dim j As Integer
        For j = 0 To WL.Count - 1

            If CheckPieceWGTimeDate(WL(j).G_NO, WL(j).DepID, yyyyMM) = True Then

                Dim SamC1 As New ProductionPiecePaySampTypeControl
                Dim SamL1 As New List(Of ProductionPiecePaySampTypeInfo)
                ''-----------------------------------------------------------------------------------------
                Dim StrSampID1 As String
                If WL(j).SampID = Nothing Then
                    StrSampID1 = "Z"
                Else
                    StrSampID1 = WL(j).SampID
                End If
                ''-----------------------------------------------------------------------------------------

                SamL1 = SamC1.ProductionPiecePaySampType_GetList(StrSampID1, Nothing, "True", Nothing) '查詢樣辦表

                If SamL1.Count = 1 Then
                    If WC.ProductionSumTimeWorkGroupSamp_Update(WL(j).GT_NO, StrSampID1, SamL1(0).SampPrice) = False Then
                        SignStr = "E"
                    End If
                End If
            End If

            ProgressBar1.Value = j

        Next

E:

        ProgressBar1.Visible = False

        If StrNONO = "" Then
            MsgBox("無數據保存!")
            Exit Function
        End If


        If SignStr = "" Then
            MsgBox("同步成功!")
        Else
            MsgBox("部分保存失敗,請檢查!")
        End If


    End Function



    Private Sub frmProductionPiecePaySampType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ProgressBar1.Visible = False

        If Val(Format(Now, "dd")) <= 10 Then
            Date_YYMM.EditValue = Format(DateAdd(DateInterval.Month, -1, CDate(Format(Now, "yyyy/MM/dd"))), "yyyy年MM月")
        Else
            Date_YYMM.EditValue = Format(Now, "yyyy年MM月")
        End If


    End Sub
End Class