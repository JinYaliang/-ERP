Imports LFERP.Library.Production.ProductionWareShipped

Public Class frmWarePrint

#Region "�ݩ�"
    Dim pws As New ProductionWareShippedControl
    Dim strdate1, strdate2 As String
    Private _EditItem As String
    Public Property EditItem() As String
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
#End Region

    Private Sub frmWarePrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GroupBox1.Text = EditItem
        Select Case EditItem
            Case "ProductionRetrocede"
                GroupBox1.Text = "�˰t�h�f���`"
            Case "ProductionWareShipped"
                GroupBox1.Text = "�˰t�ܥX�f���`"
            Case "ProductionWareOutA"
                GroupBox1.Text = "�Ͳ��ܥX�f���`"
        End Select
        DateEdit1.Text = Format(DateAdd("M", -1, Now()), "yyyy/MM/dd")
        DateEdit2.Text = Format(Now, "yyyy/MM/dd")
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim ds As New DataSet
        Dim ltc As New CollectionToDataSet

        ds.Tables.Clear()

        strdate1 = DateEdit1.Text
        strdate2 = DateEdit2.Text
        Dim StrPost As String
        Dim TableNum As Integer



        Select Case EditItem
            Case "ProductionRetrocede"
                TableNum = 1
                strName = "�˰t�h�f"
            Case "ProductionWareShipped"
                TableNum = 2
                strName = "�˰t�X�f"
            Case "ProductionWareOutA"
                TableNum = 3
                strName = "�Ͳ��X�f"
        End Select

        StrPost = strName + Format(CDate(strdate1), "yyyy/MM/dd") + Format(CDate(strdate2), "yyyy/MM/dd")


        ltc.CollToDataSet(ds, "ProductionTHreeTable", pws.ProductionTHreeTable_GetList(TableNum, strdate1, strdate2))

        PreviewRPT1(ds, "rptProductionThree", "��X���`����", StrPost, InUser, True, True)

            ltc = Nothing
            Me.Close()
    End Sub
End Class