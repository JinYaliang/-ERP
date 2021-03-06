Imports LFERP.Library.MaterialParam


Public Class frmMaterialGauge
    Dim tInt As Integer
    Dim txt() As TextBox
    Dim lbl() As Label
    Public strGauge As String

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        strGauge = "取消"
        Dim i As Integer

        For i = 0 To tInt
            Me.Controls.Remove(txt(i))
            Me.Controls.Remove(lbl(i))

        Next
        Me.Close()

    End Sub

    Sub LoadParamType(ByVal TypeID As String, ByVal strValue As String)
        On Error Resume Next
        Dim mc As New MaterialParamController
        Dim objInfoList As New MaterialParamTypeInfo
        Dim iList As New List(Of MaterialParamInfo)
        Dim i As Integer
        Dim strEmpAssembly() As String
        Dim lngEmpCount As Long

        lngEmpCount = InstrCount(strValue, ";")
        strEmpAssembly = Split(strValue, ";")
        iList = mc.MaterialParam_Get(TypeID)     '導入類別參數
        tInt = iList.Count - 1

        ReDim txt(tInt)
        ReDim lbl(tInt)
        
        For i = 0 To tInt
            txt(i) = New TextBox
            'If iList.Count = 0 Or i = tInt Then
            '    txt(i).Multiline = True
            '    txt(i).Size = (New Size(444, 88))
            'Else
            txt(i).Multiline = False
            txt(i).Size = (New Size(444, 44))
            'End If
            If i = tInt Then
                If lngEmpCount > tInt Then
                    Dim l As Integer
                    For l = i To lngEmpCount
                        If l = lngEmpCount Then
                            txt(i).Text = txt(i).Text & strEmpAssembly(l)
                        Else
                            txt(i).Text = txt(i).Text & strEmpAssembly(l) & ";"
                        End If
                    Next
                End If
            Else
                txt(i).Text = strEmpAssembly(i)
            End If
            txt(i).Tag = iList(i).ParamGauge
            txt(i).Left = 100
            txt(i).Top = 30 * i + 50
            txt(i).Font = New Font("新細明體", 11)
            txt(i).TabIndex = i
            'txt(i).EnterMoveNextControl = True
            'txt(i).Anchor = PictureBox1.Anchor

            lbl(i) = New Label()
            lbl(i).Font = New Font("新細明體", 11)
            If iList.Count = 0 Then
                lbl(i).Text = "規格:"
            Else
                lbl(i).Text = iList(i).ParamName
            End If
            lbl(i).Left = 10
            lbl(i).Top = 30 * i + 53
            lbl(i).AutoSize = True
            Me.Controls.Add(txt(i))
            Me.Controls.Add(lbl(i))
            AddHandler txt(i).Validating, AddressOf TextEdit_Validating
            AddHandler txt(i).KeyDown, AddressOf TextEdit_KeyDown
            AddHandler txt(i).GotFocus, AddressOf TextEdit_GotFocus
        Next

        txt(0).Focus()


    End Sub
    Private Sub frmMaterialGauge_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp

    End Sub

    Private Sub frmMaterialGauge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadParamType(Label2.Text, strGauge)
    End Sub

    Public Function InstrCount(ByVal source As String, ByVal Search As String) As Long
        InstrCount = Len(Replace(source, Search, Search & "*")) - Len(source)

    End Function
    Private Sub TextEdit_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim a As TextBox
        a = CType(sender, TextBox)


        ' MsgBox("控件名稱:" & a.Name)

        'If a.Text.Length = 0 Then
        '    'MsgBox(1)
        '    a.Focus()
        'End If
    End Sub

    Private Sub TextEdit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus
        Dim a As TextBox
        a = CType(sender, TextBox)
        Label3.Text = a.Tag
       
    End Sub
    Private Sub TextEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim a As TextBox ' DevExpress.XtraEditors.TextEdit
        a = CType(sender, TextBox)
        Dim tPoint As Integer

        If e.Control AndAlso e.KeyCode = Keys.Q Then
            'a.Focus()
            'MsgBox("OK")
            tPoint = a.SelectionStart
            a.Text = Mid(a.Text, 1, a.SelectionStart) & "Ф" & Mid(a.Text, a.SelectionStart + 1)
            a.SelectionStart = tPoint + 1
            'a.AppendText("Ф")

        ElseIf e.KeyCode = Keys.Enter Then
            SelectNextControl(a, True, True, True, True)

        End If

    End Sub



    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim i As Integer
        Dim strtemp As String
        strtemp = ""
        For i = 0 To tInt
            strtemp = strtemp & txt(i).Text & ";"
            ' strtemp = strtemp & IIf(Len(txt(i).Text) = 0, IIf(i > 0, ";", ""), txt(i).Text & ";")      'txt(i).Text & ";"
        Next
        strGauge = strtemp
        For i = 0 To tInt
            Me.Controls.Remove(txt(i))
            Me.Controls.Remove(lbl(i))

        Next

        Me.Close()

    End Sub


 
End Class