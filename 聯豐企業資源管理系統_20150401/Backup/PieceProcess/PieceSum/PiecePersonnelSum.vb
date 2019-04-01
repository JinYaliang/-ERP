Public Class PiecePersonnelSum

    Private Sub PiecePersonnelSum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Grid.ContextMenuStrip = MenuStrip
        GridView1.Columns.Clear()
        Dim StrA As String, StrB As String

        StrA = "�t�ҽs��,�m�W,���,�~������,�t�O�W��,����,�ƶq,���B,�f��,�ާ@�H"
        StrB = "Per_NO,Per_Name,Per_Add,Per_Pay,FacName,DepName,PT_Qty,PT_SumQty,PT_Check,PT_Action"
        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")

        For i As Integer = 0 To UBound(StrAarray)
            Dim Coll As New DevExpress.XtraGrid.Columns.GridColumn
            Coll.Caption = StrAarray(i)
            Coll.FieldName = StrBarray(i)
            Coll.Visible = True
            Coll.VisibleIndex = GridView1.Columns.Count
            GridView1.Columns.Add(Coll)
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Grid.ContextMenuStrip = ContextMenuStrip1
        GridView1.Columns.Clear()
        Dim StrA As String, StrB As String

        StrA = "�էO�s��,�էO�W��,�~��,�t�O,����,�ƶq,���B,�f��,�ާ@�H"
        StrB = "Per_NO,Per_Name,Per_AddDate,FacName,DepName,PT_Qty,PT_SumQty,PT_Check,PT_Action"
        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")

        For i As Integer = 0 To UBound(StrAarray)
            Dim Coll As New DevExpress.XtraGrid.Columns.GridColumn
            Coll.Caption = StrAarray(i)
            Coll.FieldName = StrBarray(i)
            Coll.Visible = True
            Coll.VisibleIndex = GridView1.Columns.Count
            GridView1.Columns.Add(Coll)
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Grid.ContextMenuStrip = ContextMenuStrip2
        GridView1.Columns.Clear()
        Dim StrA As String, StrB As String

        StrA = "�t�ҽs��,�m�W,�~��,�~������,�t�O,����,���~,�W�Z�Ѽ�,�����[�Z,����[�Z,���ɪ��B,�p��u��,�f��,�ާ@��"
        StrB = "Per_NO,Per_Name,Per_AddDate,Per_Pay,FacName,DepName,PT_Qty,PT_SumQty,PT_Check,PT_Action,PT_Qty,PT_SumQty,PT_Check,PT_Action"
        Dim StrAarray As Array = Split(StrA, ",")
        Dim StrBarray As Array = Split(StrB, ",")

        For i As Integer = 0 To UBound(StrAarray)
            Dim Coll As New DevExpress.XtraGrid.Columns.GridColumn
            Coll.Caption = StrAarray(i)
            Coll.FieldName = StrBarray(i)
            Coll.Visible = True
            Coll.VisibleIndex = GridView1.Columns.Count
            GridView1.Columns.Add(Coll)
        Next
        'GridView1.BehaviorOptions.AutoSelectAllInEditor = False
        'GridView1.OptionsBehavior.a()

    End Sub

    Private Sub cmdSumDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSumDay.Click
        Dim fr As New FrmPieceSumDay
        fr.ShowDialog()

    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim fr As New FrmPieceSelect
        fr.ShowDialog()
    End Sub

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        Dim fr As New FrmPieceSumDayCheck
        fr.ShowDialog()
    End Sub
End Class