Imports LFERP.Library.ProductionPiecePersonnel
Imports System.Data.SqlClient

Public Class frmProductionPieceKaoQin

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frmProductionPieceKaoQin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Date_YYMM.EditValue = Format(Now, "yyyy�~MM��")
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        AddDate()
    End Sub

    Sub AddDate()
        Dim pc As New ProductionPiecePersonnelControl
        Dim pl As New List(Of ProductionPiecePersonnelInfo)
        pl = pc.ProductionPiecePersonnel_GetList(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, False, Nothing, Nothing)

        If pl.Count <= 0 Then
            MsgBox("�L�H���H���s�b�I")
            Exit Sub
        End If

        Dim i As Integer
        '---------------------------------------------------------------
        ProgressBar1.Visible = True
        ProgressBar1.Maximum = pl.Count
        For i = 0 To pl.Count - 1
            Dim pi As New ProductionPiecePersonnelInfo
            pi.Per_NO = pl(i).Per_NO

            Dim StrA, StrB As String
            StrA = GetKaoQinClass(pl(i).Per_NO, Format(CDate(Date_YYMM.EditValue), "yyyy-MM"), "�կZ")
            StrB = GetKaoQinClass(pl(i).Per_NO, Format(CDate(Date_YYMM.EditValue), "yyyy-MM"), "�]�Z")
            If StrA = 0 And StrB = 0 Then
                pi.KQClass = "�կZ"
            ElseIf Val(StrA) >= Val(StrB) Then
                pi.KQClass = "�կZ"
            Else
                pi.KQClass = "�]�Z"
            End If

            pi.KQMonth = Format(CDate(Date_YYMM.EditValue), "yyyy-MM")

            If pc.ProductionPiecePersonnelKaoQin_Update(pi) = True Then
            Else
                MsgBox(pl(i).Per_NO & "��s�ҶԯZ���!")
            End If

            ProgressBar1.Value = i + 1

        Next

        ProgressBar1.Visible = True
        MsgBox("��s�ҶԯZ��\!")
        '---------------------------------------------------------------
    End Sub

    Function GetKaoQinClass(ByVal _Per_NO As String, ByVal _KQMonth As String, ByVal _KaoQinClass As String) As String

        GetKaoQinClass = ""

        Dim strSql As String
        Dim myConn As New SqlConnection(KaoQinConn)
        Dim da As SqlDataAdapter

        Dim dsT As New DataSet
        ''---------------------------------------------------------------
        Dim strStat_Date, strEnd_Date, strStarEndDate As String
        strStat_Date = Format(CDate(_KQMonth), "yyyy-MM") & "-1"
        strEnd_Date = DateAdd("d", -1, DateAdd("M", 1, strStat_Date))
        strStarEndDate = "BETWEEN '" & strStat_Date & "' AND '" & strEnd_Date & "'"

        myConn.Open()

        dsT.Tables.Clear()
        strSql = "select count(*) from vwCQMX  where BadgeID ='" + _Per_NO + "' and flag=1 and   CQDate  " + strStarEndDate + " and  BZType like '%" + _KaoQinClass + "%' "
        da = New SqlDataAdapter(strSql, myConn)
        da.Fill(dsT, "KQ")
        da.Dispose()
        myConn.Close()
        '----------------------------------------------------------------------------
        'select * from vwCQMX  where BadgeID ='13071390' and   CQDate<='2013-7-31' and CQDate >='2013-7-1'
        'and BZType LIKE '%�կZ%'
        If dsT.Tables("KQ").Rows.Count <= 0 Then
        Else
            GetKaoQinClass = dsT.Tables("KQ").Rows(0)(0).ToString
        End If

    End Function

End Class