Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Text
Namespace LFERP.Library.BarCode

    Public Class StringTxtInfo

        Private m_FontName As String   '字體名字
        Private m_FontSize As Integer    '字體大小
        Private m_String As String     '需要打印的文本
        Private m_FontBold As Boolean    '字體樣式
        Private m_X As Integer, m_Y As Integer  '打印時的x，y座標
        Private m_XZoom As Integer, m_YZoom As Integer
        Private List1 As List(Of Char), List2 As List(Of Integer)
        Private S As String
        Private Declare Auto Function GetDC Lib "user32" (ByVal Hdc As IntPtr) As IntPtr
        Private Declare Function DeleteDC Lib "gdi32" (ByVal hdc As IntPtr) As Integer
        Private Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As IntPtr) As IntPtr

        Public Property XZoom() As Integer
            Get
                Return m_XZoom
            End Get
            Set(ByVal value As Integer)
                If value > 0 AndAlso value <= 10 Then
                    m_XZoom = value
                End If
            End Set
        End Property
        Public Property YZoom() As Integer
            Get
                Return m_YZoom
            End Get
            Set(ByVal value As Integer)
                If value > 0 AndAlso value <= 10 Then
                    m_YZoom = value
                End If
            End Set
        End Property
        Public Property X() As Integer
            Get
                Return m_X
            End Get
            Set(ByVal value As Integer)
                If value >= 0 Then
                    m_X = value
                End If
            End Set
        End Property
        Public Property Y() As Integer
            Get
                Return m_Y
            End Get
            Set(ByVal value As Integer)
                If value >= 0 Then
                    m_Y = value
                End If
            End Set
        End Property
        Public Property FontName() As String
            Get
                Return m_FontName
            End Get
            Set(ByVal value As String)
                m_FontName = value
            End Set
        End Property

        Public Property FontSize() As Integer
            Get
                Return m_FontSize
            End Get
            Set(ByVal value As Integer)
                If value > 7 AndAlso value <= 500 Then
                    m_FontSize = value
                End If
            End Set
        End Property

        Public Property Text() As String
            Get
                Return m_String
            End Get
            Set(ByVal value As String)
                m_String = value
            End Set
        End Property
        Public Property FontBold() As Boolean
            Get
                Return m_FontBold
            End Get
            Set(ByVal value As Boolean)
                m_FontBold = value
            End Set
        End Property
        Sub New()
            m_FontName = "宋體"
            m_FontSize = 30
            m_FontBold = False
            m_X = 1
            m_Y = 1
            m_XZoom = 1
            m_YZoom = 1
            InitDictionary()
        End Sub
        Public Function Convert() As String
            Dim Hdc As IntPtr

            Hdc = GetDC(IntPtr.Zero)

            Dim Graphic As Graphics = Graphics.FromHdc(Hdc)


            Dim Width As Integer, Height As Integer
            Dim G As Graphics, Image As Bitmap
            Dim Font As Font

            If m_FontBold = True Then                                            '設置字體是否為粗體
                Font = New Font(m_FontName, m_FontSize, Drawing.FontStyle.Bold, GraphicsUnit.Pixel)
            Else
                Font = New Font(m_FontName, m_FontSize, Drawing.FontStyle.Regular, GraphicsUnit.Pixel)
            End If

            'Dim hg, wt As Integer
            'wt = CInt(Graphic.MeasureString(m_String, Font).Width)
            'hg = wt Mod 8
            'If hg = 0 Then
            '    Width = wt
            'Else
            '    Width = (wt \ 8 + 1) * 8
            'End If

            Width = (CInt(Graphic.MeasureString(m_String, Font).Width) \ 8 + 1) * 8  '通過獲取文本字體寬度設置圖片的長寬將行規格化便於計算字節
            Height = Font.Height                                                     '通過獲取文本字體的高度設置圖片列高

            Image = New Bitmap(Width, Height, PixelFormat.Format32bppArgb)
            G = Graphics.FromImage(Image)

            G.Clear(Color.White)
            G.DrawString(m_String, Font, Brushes.Black, 0, 2)                        '將文本寫入內存圖片


            Dim Value As Integer = Width * Height
            Dim TempString As New StringBuilder(Value)                              '轉換時的臨時存儲變量
            Dim DesString As New StringBuilder(Value)                               '轉換成字符的最終存儲變量


            DesString.Append("~DGOUTSTR01," & Width * Height \ 8 & "," & Width \ 8 & ",")      '加入ZPL的下載圖片命令
            Dim i As Integer, j As Integer                              '開始取點
            Dim Sum As Integer
            For j = 0 To Height - 1                                     '由於內存中的圖片是倒置，所以要反取數據
                For i = 0 To Width \ 4 - 1
                    Sum = 0
                    For m As Integer = 0 To 3                           '由於4個點可用1個十六進制數表示因此一次取4個點進行轉換
                        If Image.GetPixel(i * 4 + m, j).B = 0 Then      '由於只列印黑白點所以根據RGB分量判斷是否需要列印該點
                            Sum += 1 << (3 - m)                         '通過位移錯做將該店的信息與一個數據的BIT相對應
                        End If
                    Next
                    TempString.Append(Hex(Sum))                         '将4個點取得的數據轉換成16進制數據并存入臨時變量

                Next
            Next

            Dim Count As Integer = 1                                    '将轉成16進制的文本進行壓縮
            For i = 1 To TempString.Length - 1
                If TempString.Chars(i - 1) = TempString.Chars(i) Then
                    Count += 1
                    If i = TempString.Length - 1 Then
                        DesString.Append(CompressCode(Count) & TempString.Chars(i))
                    End If
                Else
                    If Count <> 1 Then
                        DesString.Append(CompressCode(Count) & TempString.Chars(i - 1))
                    Else
                        DesString.Append(TempString.Chars(i - 1))
                    End If

                    'If i = TempString.Length - 1 Then
                    '    DesString.Append(TempString.Chars(i))
                    'End If
                    S = String.Empty
                    Count = 1
                End If
            Next

            'DesString.Append(vbCrLf & "^FO" & m_X.ToString & "," & m_Y.ToString & "^XGOUTSTR01," & m_XZoom & "," & m_YZoom & ",^FS")
            DeleteDC(Hdc)
            G.Dispose()
            Image.Dispose()
            Graphic.Dispose()
            Return DesString.ToString
        End Function
        Private Function CompressCode(ByRef Input As Integer) As String

            If Input > 0 Then
                For i As Integer = List1.Count - 1 To 0 Step -1  '使用遞歸將連續的數字轉換成ZPL壓縮代碼，如000可用I0表示
                    If Input >= List2.Item(i) Then

                        S &= List1.Item(i)
                        Input -= List2.Item(i)
                        CompressCode(Input)
                    End If
                Next
            End If
            Return S
        End Function
        Private Sub InitDictionary()
            '將ZPL定義的壓縮代碼寫入集合
            List1 = New List(Of Char)
            List2 = New List(Of Integer)
            For i As Integer = 0 To 18
                List1.Add(ChrW(71 + i))
                List2.Add(i + 1)
            Next
            For i As Integer = 0 To 19

                List1.Add(ChrW(103 + i))
                List2.Add(20 * (i + 1))
            Next

        End Sub

    End Class

End Namespace