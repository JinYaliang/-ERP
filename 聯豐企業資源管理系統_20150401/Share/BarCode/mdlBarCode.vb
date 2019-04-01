Imports System.IO
Imports LFERP.Library.Material
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports EncodeMy

Module mdlBarCode


    Public Const GENERIC_WRITE = &H40000000

    Public Const OPEN_EXISTING = 3

    Public Const FILE_SHARE_WRITE = &H2

    Dim LPTPORT As String = "LPT1"

    Dim hPort As Integer

    Public Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" ( _
        ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, _
        ByVal dwShareMode As Integer, _
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.Struct)> ByRef lpSecurityAttributes As SECURITY_ATTRIBUTES, _
        ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, _
        ByVal hTemplateFile As Integer) As Integer

    Public Declare Function CloseHandle Lib "kernel32" Alias "CloseHandle" (ByVal hObject As Integer) As Integer

    Dim retval As Integer


    'Private Declare Function GETFONTHEX Lib "fnthex32.dll" (ByVal BarcodeText As String, ByVal FontName As String, ByVal FileName As String _
    '     , ByVal Orient As Integer, ByVal Height As Integer, ByVal Width As Integer, ByVal IsBold As Integer, ByVal IsItalic As Integer, ByVal hexbuf As System.Text.StringBuilder) As Integer

    Private Declare Function GETFONTHEX Lib "fnthex32.dll" (ByVal BarcodeText As String, ByVal FontName As String, ByVal FileName As String _
            , ByVal Orient As Integer, ByVal Height As Integer, ByVal Width As Integer, ByVal IsBold As Integer, ByVal IsItalic As Integer, ByVal hexbuf As System.Text.StringBuilder) As Integer


    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> Public Structure SECURITY_ATTRIBUTES

        Private nLength As Integer

        Private lpSecurityDescriptor As Integer

        Private bInheritHandle As Integer

    End Structure


    Sub PrintBar(ByVal M_Code As String, ByVal M_Name As String, ByVal PM_M_Code As String, ByVal M_Unit As String, ByVal Qty As String, ByVal M_Date As String, ByVal M_Mo As String)
        '�j���X�榡
        'EPL
        Dim SA As SECURITY_ATTRIBUTES

        Dim outFile As IO.FileStream
        Dim hPortP As IntPtr

        hPort = CreateFile(LPTPORT, GENERIC_WRITE, FILE_SHARE_WRITE, SA, OPEN_EXISTING, 0, 0)

        hPortP = New IntPtr(hPort) 'convert Integer to IntPtr 

        outFile = New FileStream(hPortP, FileAccess.Write, False) 'Create FileStream using Handle 

        Dim fileWriter As New IO.StreamWriter(outFile, System.Text.Encoding.GetEncoding("GB2312"))
        fileWriter.WriteLine(" ")
        fileWriter.WriteLine("N")

        fileWriter.Write("A200,20,0,8,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB(M_Name))   '�W��
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("B100,60,0,1,2,2,60,B,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(M_Code)   '���X
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,170,0,3,1,1,N,") 'A70,190    A70,230  ,A70,270
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("Product: " & PM_M_Code))  '���~�s��
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        'fileWriter.Write("A10,145,0,8,1,1,N,") 'A70,190    A70,230  ,A70,270
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB(Mid(M_Gauge, 1, 10)))  '�W��
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))

        'fileWriter.Write("A10,178,0,8,1,1,N,") 'A70,190    A70,230  ,A70,270
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB(Mid(M_Gauge, 11, 20)))  '�W��
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))

        'fileWriter.Write("A10,211,0,8,1,1,N,") 'A70,190    A70,230  ,A70,270
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB(Mid(M_Gauge, 21, 30)))  '�W��
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))


        fileWriter.Write("A100,200,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("Batch  : " & M_Mo))   '�妸
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,230,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("Date   : " & M_Date))   '���
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,260,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("QTY    : " & Qty & " " & M_Unit))   '�ƶq+���
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,290,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("MO     : "))   '�渹
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))


        fileWriter.WriteLine("P1")

        fileWriter.Flush()

        fileWriter.Close()

        outFile.Close()

        retval = CloseHandle(hPort)
    End Sub

    Sub PrintBar2(ByVal M_Code As String, ByVal M_Name As String)
        '�p���X�榡
        'EPL
        Dim SA As SECURITY_ATTRIBUTES

        Dim outFile As IO.FileStream
        Dim hPortP As IntPtr

        hPort = CreateFile(LPTPORT, GENERIC_WRITE, FILE_SHARE_WRITE, SA, OPEN_EXISTING, 0, 0)

        hPortP = New IntPtr(hPort) 'convert Integer to IntPtr 

        outFile = New FileStream(hPortP, FileAccess.Write, False) 'Create FileStream using Handle 

        Dim fileWriter As New IO.StreamWriter(outFile, System.Text.Encoding.GetEncoding("GB2312"))
        fileWriter.WriteLine(" ")
        fileWriter.WriteLine("N")

        fileWriter.Write("A200,0,0,8,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB(M_Name))   '�W��
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("B100,40,0,1,2,2,60,B,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(M_Code)   '���X
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        'fileWriter.Write("A100,160,0,3,1,1,N,") 'A70,190    A70,230  ,A70,270
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB("Product: " & PM_M_Code))  '���~�s��
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))




        'fileWriter.Write("A100,190,0,3,1,1,N,")
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB("Batch  : " & M_Mo))   '�妸
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))

        'fileWriter.Write("A100,220,0,1,1,1,N,")
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB("Date   : " & M_Date))   '���
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))

        'fileWriter.Write("A100,250,0,3,1,1,N,")
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB("QTY    : " & Qty & " " & M_Unit))   '�ƶq+���
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))

        'fileWriter.Write("A100,280,0,3,1,1,N,")
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(BIG5TOGB("MO     : "))   '�渹
        'fileWriter.Write(Chr(34))
        'fileWriter.Write(Chr(13))
        'fileWriter.Write(Chr(10))


        fileWriter.WriteLine("P1")

        fileWriter.Flush()

        fileWriter.Close()

        outFile.Close()

        retval = CloseHandle(hPort)
    End Sub

    Dim strLen As String
    Dim strBar As String

    Sub PrintQCBar(ByVal M_Code As String, ByVal M_Name As String, ByVal M_Gauge As String, ByVal OS_BatchID As String, ByVal WQS_Qty As String, ByVal A_AcceptanceNO As String, ByVal WQS_NO As String)
        '�j����---���a(6*4)---�]�D�n����QC���ˡ^Zebra888-TT-
        'EPL
        Dim SA As SECURITY_ATTRIBUTES

        Dim outFile As IO.FileStream
        Dim hPortP As IntPtr

        '�]�����LLPT1�ݤf�C�G���ծɥu���LPT3�C�����ϥήɻݧ�אּLPT1�ݤf
        hPort = CreateFile("LPT3", GENERIC_WRITE, FILE_SHARE_WRITE, SA, OPEN_EXISTING, 0, 0)

        hPortP = New IntPtr(hPort) 'convert Integer to IntPtr 

        outFile = New FileStream(hPortP, FileAccess.Write, False) 'Create FileStream using Handle 

        Dim fileWriter As New IO.StreamWriter(outFile, System.Text.Encoding.GetEncoding("GB2312"))
        fileWriter.WriteLine(" ")
        fileWriter.WriteLine("N")

        strLen = M_Name.Length
        If strLen <= 3 Then
            strBar = "A200,125,0,8,1,1,N,"
        ElseIf strLen >= 4 And strLen <= 5 Then
            strBar = "A170,125,0,8,1,1,N,"
        ElseIf strLen >= 6 And strLen < 8 Then
            strBar = "A150,125,0,8,1,1,N,"
        ElseIf strLen >= 8 And strLen <= 9 Then
            strBar = "A130,125,0,8,1,1,N,"
        ElseIf strLen > 9 And strLen <= 11 Then
            strBar = "A100,125,0,8,1,1,N,"
        ElseIf strLen > 11 And strLen <= 14 Then
            strBar = "A80,125,0,8,1,1,N,"
        ElseIf strLen >= 15 And strLen <= 16 Then
            strBar = "A50,125,0,8,1,1,N,"
        Else
            strBar = "A20,125,0,8,1,1,N,"
        End If

        fileWriter.Write("A160,2,0,8,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("QC���˱M��"))   '�W��
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("B100,40,0,1,2,2,60,B,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(M_Code)   '���X
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write(strBar)
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB(M_Name))  '���ƦW��
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,160,0,3,1,1,N,") 'A70,190    A70,230  ,A70,270
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("Gauge: " & M_Gauge))  '���ƳW��
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,190,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("Batch: " & OS_BatchID))   '�妸
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,220,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("Qty  : " & WQS_Qty))   '�ƶq
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,250,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("ANO  : " & A_AcceptanceNO))   '�禬�渹
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("A100,280,0,3,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("QNO  : " & WQS_NO))   ' ���˳渹
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))


        fileWriter.WriteLine("P1")

        fileWriter.Flush()

        fileWriter.Close()

        outFile.Close()

        retval = CloseHandle(hPort)
    End Sub

    Sub PrintAcceptanceNO(ByVal SendDate As String, ByVal AcceptanceNO As String)
        Dim SA As SECURITY_ATTRIBUTES

        Dim outFile As IO.FileStream
        Dim hPortP As IntPtr

        hPort = CreateFile("LPT3", GENERIC_WRITE, FILE_SHARE_WRITE, SA, OPEN_EXISTING, 0, 0)

        hPortP = New IntPtr(hPort) 'convert Integer to IntPtr 

        outFile = New FileStream(hPortP, FileAccess.Write, False) 'Create FileStream using Handle 

        Dim fileWriter As New IO.StreamWriter(outFile, System.Text.Encoding.GetEncoding("GB2312"))
        fileWriter.WriteLine(" ")
        fileWriter.WriteLine("N")

        fileWriter.Write("A100,0,0,8,1,1,N,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(BIG5TOGB("�禬���:" & SendDate))   '�禬���
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))

        fileWriter.Write("B150,40,0,1,2,2,60,B,")
        fileWriter.Write(Chr(34))
        fileWriter.Write(AcceptanceNO)   '�渹
        fileWriter.Write(Chr(34))
        fileWriter.Write(Chr(13))
        fileWriter.Write(Chr(10))


        fileWriter.WriteLine("P1")

        fileWriter.Flush()

        fileWriter.Close()

        outFile.Close()

        retval = CloseHandle(hPort)
    End Sub
    Function BIG5TOGB(ByVal BIG5Code As String) As String
        '��X�c����²��
        Dim gb As New EncodeRobert
        BIG5TOGB = gb.SCTCConvert(ConvertType.Traditional, ConvertType.Simplified, BIG5Code)
    End Function

    '��-2013-2-22----------------------------------------------------------------------------------------
    Public Sub CreateLPTFile(ByVal PrintPort As String, ByVal str As String)
        Try
            Dim SA As SECURITY_ATTRIBUTES
            Dim hPort As Integer = CreateFile(PrintPort, GENERIC_WRITE, FILE_SHARE_WRITE, SA, OPEN_EXISTING, 0, 0)
            'Dim hPort As Integer = Open()
            Dim hPortP As System.IntPtr = New IntPtr(hPort)
            Dim fs As FileStream
            fs = New FileStream(hPortP, FileAccess.Write, False)
            Dim writer As New StreamWriter(fs)
            writer.AutoFlush = False
            writer.WriteLine(str)
            writer.Flush()
            writer.Close()
            fs.Close()
            CloseHandle(hPort)
        Catch
            'statusBar1.Panels[0].Text = "I/O���~:���ˬd���L���O�_���}�s��!" + eIO.Message; 
            MsgBox("���ˬd���L���O�_�w�s���I")
        End Try
    End Sub

    Public Function StringTxtTest(ByVal M_gauge1 As String, ByVal Code1 As String, ByVal qty As String) As String

        Dim XY As Integer = 115 '1�A2�涡�Z

        Dim sCmd As String = ""
        Dim i As Integer
        Dim cmd1 As String = ""
        Dim cmd2 As String = ""
        Dim cmd3 As String = ""
        Dim cmd4 As String = ""
        Dim cmd5 As String = ""

        sCmd = "^XA"
        sCmd += "^MD" & "30" & Chr(13)
        sCmd += "^LH" & "80" & "," & "10" & Chr(13)

        Dim nCount As Integer
        Dim hexbuf As New System.Text.StringBuilder(99999)

        Dim text As String
        Dim FileName As String
        FileName = Trim("temp")
        nCount = GETFONTHEX(M_gauge1, "���^", FileName, 0, 25, 15, 0, 0, hexbuf)

        For i = 0 To 1
            sCmd += "^ACN" & "," & "18" & "," & "10" & Chr(13)
            text = hexbuf.ToString + "^FO" + CStr(40) + "," + CStr(XY * i + 1) + "^XG" + FileName + ",1,1^FS"

            sCmd += "~DGOUTSTR01,10192,052," + text + " & Chr(13)"
            sCmd += "^XGOUTSTR01,1,1^FS" & Chr(13)

            'MD
            sCmd += "^ACN" & "," & "18" & "," & "10" & Chr(13)
            sCmd += "^FO" & CStr(8) & "," + CStr(40 + XY * i) & "^FD" & "LF" & "^FS" & Chr(13)

            '���X���L
            sCmd += "^FO" + Str(40) + "," & CStr(30 + XY * i) & "^BY2" & Chr(13)
            sCmd += "^BAN,35,N,N,N" & Chr(13)  '�����L�`��
            sCmd += "^FD" + Code1 + "^FS" & Chr(13)

            sCmd += "^ACN" & "," & "18" & "," & "11" & Chr(13) '�������L�s�X
            sCmd += "^FO" & CStr(100) & "," + CStr(75 + XY * i) & "^FD" & Code1 & "^FS" & Chr(13)

        Next

        sCmd += "^PQ" + qty & Chr(13)
        sCmd += "^XZ"



        Return sCmd
    End Function


    Function StringTxtGK888TB(ByVal _Name As String, ByVal _NUM As String, ByVal KnifeType As String, ByVal Code As String, ByVal Qty As Integer) As String
        Dim sCmd As String = ""
        StringTxtGK888TB = ""

        Dim strBarX As Integer
        Dim strBarY As Integer


        strBarX = Val(GetIni("CommSet", "BarX"))
        strBarY = Val(GetIni("CommSet", "BarY"))


        Select Case KnifeType
            Case "0"

                Dim n, n1 As Integer
                Dim arrName(n) As String
                Dim arrNUM(n) As String
                arrName = Split(_Name, ",")
                arrNUM = Split(_NUM, ",")

                n = Len(Replace(_Name, ",", "," & "*")) - Len(_Name) + 1
                n1 = Len(Replace(_NUM, ",", "," & "*")) - Len(_NUM) + 1
                If n <> n1 Then
                    MsgBox("��J���L�����e���~�I")
                    Exit Function
                End If

                ''-----------------------------------------------------------------------
                Dim QTstr(2) As String
                ''���`�B�z�U
                Dim k As Integer = 0
                Dim kk, jj As Integer
                For k = 0 To n - 1
                    'If arrName(k) = BIG5TOGB("����") Or arrName(k) = BIG5TOGB("�~�P") Or arrName(k) = "����" Or arrName(k) = "�~�P" Then
                    '    QTstr(jj) = arrNUM(k)
                    '    jj = jj + 1
                    'Else
                    If arrName(k) = "�u�ǦW��" Or arrName(k) = BIG5TOGB("�u�ǦW��") Then
                        If Len(arrNUM(k)) > 10 Then
                            QTstr(jj) = Mid(arrNUM(k), 1, 8)
                            jj = jj + 1
                            QTstr(jj) = Mid(arrNUM(k), 9, Len(arrNUM(k)) - 8)
                            jj = jj + 1
                        Else
                            QTstr(jj) = arrNUM(k)
                            jj = jj + 1
                        End If

                    Else
                        arrName(kk) = arrName(k)
                        arrNUM(kk) = arrNUM(k)
                        kk = kk + 1
                    End If
                Next

                n = kk

                ''-----------------------------------------------------------------------


                ''---------------------------------------
                Dim XS, YS As Integer
                XS = 150 - 50 + strBarX : YS = 5 + 15 + 10 + strBarY'���X���_�I
                ' XS = 150 : YS = 5 '���X���_�I

                sCmd = "^XA"
                If Code <> "" Then
                    Dim BarH As Integer '���X������
                    BarH = 50
                    sCmd += "^MD" & "30" & Chr(13)
                    sCmd += "^LH" & "10" & "," & "5" & Chr(13)



                    ''���L�䥦
                    If jj > 0 Then
                        Dim ss As Integer
                        For ss = 0 To jj - 1
                            sCmd += Print_HZ_Str(XS + Len(Code) * 29, YS + 2 + 30 * ss, CStr(QTstr(ss)), 12, 23)
                        Next
                    End If



                    '�Ĥ@��
                    sCmd += "^ACN" & "," & "10" & "," & "10" & Chr(13)  ''���X���
                    sCmd += "^FO" & CStr(XS + 20) & "," + CStr(YS + BarH + 2) & "^FD" & Code & "^FS" & Chr(13)

                    sCmd += "^FO" + Str(XS + 20) + "," & CStr(YS + 0) & "^BY2"
                    sCmd += "^BAN," + CStr(BarH) + ",N,N,N"  '50�����X������
                    sCmd += "^FD" + Code + "^FS"

                    Dim x, y, yy As Integer
                    'x = 0 : y = 0 : yy = 3 '888tt
                    x = -50 + strBarX : y = 75 + strBarY : yy = 5 'zm400
                    Dim RecH, RecW, RecHhalf, RecWhalf As Integer '�x������.�e
                    RecH = 70 : RecW = 90 : RecHhalf = Int(RecH / 2) : RecWhalf = Int(RecW / 2)
                    ''�j�� 
                    sCmd += "^FO" & CStr(180 + x) & "," & CStr(75 + y) & Chr(13)
                    sCmd += "^GB" & CStr(RecW * n) & "," & CStr(RecH) & "," & "2^FS" & Chr(13)
                    '-------------------------------------------------------------------------------------------------
                    Dim i As Integer
                    For i = 0 To n - 1
                        Dim SizW, SizH, FW, FH As Integer
                        ''�p�� 1
                        sCmd += "^FO" & CStr(180 + x + i * RecW) & "," & CStr(75 + y) & Chr(13)
                        sCmd += "^GB" & CStr(RecW) & "," & CStr(RecHhalf - yy) & "," & "^FS" & Chr(13)

                        FW = 13 : FH = 23 '�r��j�p
                        SizW = 180 + x + i * RecW + RecWhalf - Int(LenB(arrName(i)) * FW / 2)
                        SizH = 75 + y + Int((RecHhalf - yy) / 2) - Int(FH / 2)
                        sCmd += Print_HZ_Str(SizW, SizH, CStr(arrName(i)), FW, FH)

                        ''�p�� 1-1 
                        sCmd += "^FO" & CStr(180 + x + i * RecW) & "," & CStr(75 + RecHhalf + y - yy) & Chr(13)
                        sCmd += "^GB" & CStr(RecW) & "," & CStr(RecHhalf + yy) & "," & "^FS" & Chr(13)

                        FW = 13 : FH = 23 '�r��j�p
                        SizW = 180 + x + i * RecW + RecWhalf - Int(LenB(arrNUM(i)) * FW / 2)
                        SizH = 75 + y + (RecHhalf - yy) + Int((RecHhalf + yy) / 2) - Int(FH / 2) '75 + y + (RecHhalf - yy) �o�q���_�I�A Int((RecHhalf + yy) / 2) - Int(FH / 2)�o�q���Z��
                        sCmd += Print_HZ_Str(SizW, SizH, CStr(arrNUM(i)), FW, FH)
                    Next
                End If

            Case "1"
                Dim XS, YS As Integer
                XS = 90 + strBarX : YS = 5 + 15 + 10 + strBarY '���X���_�I
                ' XS = 160 : YS = 5 '���X���_�I

                sCmd = "^XA"
                If Code <> "" Then
                    Dim BarH As Integer '���X������
                    BarH = 50
                    sCmd += "^MD" & "30" & Chr(13)
                    sCmd += "^LH" & "10" & "," & "5" & Chr(13)
                    '�Ĥ@��
                    sCmd += "^ACN" & "," & "10" & "," & "10" & Chr(13)  ''���X���
                    sCmd += "^FO" & CStr(XS + 20) & "," + CStr(YS + BarH + 2) & "^FD" & Code & "^FS" & Chr(13)

                    sCmd += "^FO" + Str(XS + 20) + "," & CStr(YS + 0) & "^BY2"
                    sCmd += "^BAN," + CStr(BarH) + ",N,N,N"  '50�����X������
                    sCmd += "^FD" + Code + "^FS"
                    '-------------------------------------------------------------------
                    Dim SizW, SizH, FW, FH As Integer
                    Dim x, y As Integer
                    SizW = x + 110 + strBarX
                    SizH = y + 170 + strBarY
                    FW = 14 : FH = 25 '�r��j�p
                    sCmd += Print_HZ_Str(SizW, SizH, CStr(_Name), FW, FH)

                End If
        End Select

        sCmd += "^PQ" + CStr(Qty) & Chr(13)
        sCmd += "^XZ"
        Return sCmd

    End Function

    Function Print_HZ_Str(ByVal X As Integer, ByVal Y As Integer, ByVal hz As String, ByVal fw As Integer, ByVal fh As Integer) As String
        Dim nCount As Integer
        Dim hexbuf As New System.Text.StringBuilder(99999)

        Dim text As String
        Dim FileName As String
        FileName = Trim("temp")
        nCount = GETFONTHEX(hz, "���^", FileName, 0, fh, fw, 0, 0, hexbuf)
        Dim sCmd As String = ""

        'sCmd += "^ACN" & "," & "18" & "," & "10" & Chr(13)
        text = hexbuf.ToString + "^FO" + CStr(X) + "," + CStr(Y) + "^XG" + FileName + ",1,1^FS"

        sCmd += "~DGOUTSTR01,10192,052," + text + " & Chr(13)"
        sCmd += "^XGOUTSTR01,1,1^FS" & Chr(13)

        Print_HZ_Str = sCmd

    End Function

    Public Function LenB(ByVal oString As String) As Integer
        Dim strArray() As Byte
        Dim Strlen As Integer
        Dim MyEncoder As System.Text.Encoding = System.Text.Encoding.Default
        strArray = MyEncoder.GetBytes(oString)
        Strlen = strArray.Length
        Return Strlen
    End Function


End Module
