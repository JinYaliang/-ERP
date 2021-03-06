Imports LFERP.Library.BroadCastManager.BroadCastMessage
Imports LFERP.Library.BroadCastManager.BroadCastFactory

Public Class frmView
    Dim bcm As New BroadCastMessageControler
    Private _EditItem As String '属性栏位
    Private _MessageIn As String
    Private _mdate As String
    Private _mtime As String
    Property EditItem() As String '属性
        Get
            Return _EditItem
        End Get
        Set(ByVal value As String)
            _EditItem = value
        End Set
    End Property
    Property mdate() As String '属性
        Get
            Return _mdate
        End Get
        Set(ByVal value As String)
            _mdate = value
        End Set
    End Property
    Property MessageIn() As String '属性
        Get
            Return _MessageIn
        End Get
        Set(ByVal value As String)
            _MessageIn = value
        End Set
    End Property
    Property mtime() As String '属性
        Get
            Return _mtime
        End Get
        Set(ByVal value As String)
            _mtime = value
        End Set
    End Property
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        frmOut.responseTo = lbl_M_out.Text
        frmOut.isResponse = "isResponse"
        frmOut.ShowDialog()
    End Sub

    Private Sub Savebutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Savebutton.Click
        Dim bm As New List(Of BroadCastMessageInfo)
        bm = bcm.BroadCastMessage_Getlist(Nothing, Nothing, Nothing, False, EditItem)

        If bm.Count <= 0 Then
            Exit Sub
        Else
            Dim bcmi As New BroadCastMessageInfo
            bcmi.AutoID = bm(0).AutoID
            bcmi.M_Adddate = Format(CDate(bm(0).M_Adddate), "yyyy/MM/dd")
            bcmi.M_AdduserID = bm(0).M_AdduserID
            bcmi.M_Affirm = True
            bcmi.M_Date = bm(0).M_Date
            bcmi.M_In = bm(0).M_In
            bcmi.M_Message = bm(0).M_Message
            bcmi.M_ModifyDate = Format(Now, "yyyy/MM/dd")
            bcmi.M_ModifyUserID = bm(0).M_ModifyUserID
            bcmi.M_Out = bm(0).M_Out
            bcmi.M_Time = bm(0).M_Time
            bcmi.M_Type = bm(0).M_Type
            bcmi.M_Status = bm(0).M_Status
            bcm.BroadCastMessage_Update(bcmi)
        End If
        Me.Close()
    End Sub

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub frmView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim bm As New List(Of BroadCastMessageInfo)
        Dim bm1 As New List(Of BroadCastMessageInfo)
        bm = bcm.BroadCastMessage_Getlist(Nothing, Nothing, Nothing, False, EditItem)
        bm1 = bcm.BroadCastMessage_Getlist1(MessageIn, InUser, Nothing, mdate + " " + mtime)
        Dim i As Integer = 0
        Dim strMessage As String = ""
        If bm.Count <= 0 Then
            Exit Sub
        Else
            Me.lbl_M_Date.Text = Format(CDate(bm(0).M_Date), "yyyy年MM月dd日")
            Me.lbl_M_In.Text = InUser
            Me.lbl_M_out.Text = bm(0).M_Out
            Me.lbl_M_Time.Text = Format(CDate(bm(0).M_Date), "HH:mm:ss")
            For i = 0 To bm1.Count - 1
                strMessage += bm1(i).M_Out + Space(10 - Len(bm1(i).M_Out)) + " " + Format(CDate(bm1(i).M_Date), "yyyy-MM-dd HH:mm:ss") + vbCrLf + vbCrLf + bm1(i).M_Message & vbCrLf + "-------------------------------------------------" + vbCrLf
            Next
            Me.txtMessage.Text = strMessage
            Me.lbl_M_Type.Text = bm(0).M_Type
            txtMessage.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub frmView_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        txtMessage.SelectionStart = txtMessage.Text.Length
        txtMessage.ScrollToCaret()
    End Sub
End Class