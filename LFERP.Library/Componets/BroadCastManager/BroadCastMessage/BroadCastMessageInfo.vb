Namespace LFERP.Library.BroadCastManager.BroadCastMessage
    ''' <summary>
    ''' 样办寄送表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BroadCastMessageInfo
        Private _M_Date As String
        Private _M_Time As String
        Private _M_Message As String
        Private _M_Out As String
        Private _M_In As String
        Private _M_Type As String
        Private _M_Affirm As Boolean
        Private _M_AdduserID As String
        Private _M_Adddate As String
        Private _M_ModifyUserID As String
        Private _M_ModifyDate As String
        Private _AutoID As Integer
        Private _M_Status As String

        Public Sub New()
            _M_Date = Nothing
            _M_Time = Nothing
            _M_Message = Nothing
            _M_Out = Nothing
            _M_In = Nothing
            _M_Type = Nothing
            _M_Affirm = False
            _M_AdduserID = Nothing
            _M_Adddate = Nothing
            _M_ModifyUserID = Nothing
            _M_ModifyDate = Nothing
            _AutoID = 0
            _M_Status = Nothing
        End Sub
        Public Property M_Date() As String
            Get
                Return _M_Date
            End Get
            Set(ByVal value As String)
                _M_Date = value
            End Set
        End Property
        Public Property M_Time() As String
            Get
                Return _M_Time
            End Get
            Set(ByVal value As String)
                _M_Time = value
            End Set
        End Property
        Public Property M_Message() As String
            Get
                Return _M_Message
            End Get
            Set(ByVal value As String)
                _M_Message = value
            End Set
        End Property
        Public Property M_Out() As String
            Get
                Return _M_Out
            End Get
            Set(ByVal value As String)
                _M_Out = value
            End Set
        End Property
        Public Property M_In() As String
            Get
                Return _M_In
            End Get
            Set(ByVal value As String)
                _M_In = value
            End Set
        End Property
        Public Property M_Type() As String
            Get
                Return _M_Type
            End Get
            Set(ByVal value As String)
                _M_Type = value
            End Set
        End Property
        Public Property M_Affirm() As Boolean
            Get
                Return _M_Affirm
            End Get
            Set(ByVal value As Boolean)
                _M_Affirm = value
            End Set
        End Property
        Public Property M_AdduserID() As String
            Get
                Return _M_AdduserID
            End Get
            Set(ByVal value As String)
                _M_AdduserID = value
            End Set
        End Property
        Public Property M_Adddate() As String
            Get
                Return _M_Adddate
            End Get
            Set(ByVal value As String)
                _M_Adddate = value
            End Set
        End Property
        Public Property M_ModifyUserID() As String
            Get
                Return _M_ModifyUserID
            End Get
            Set(ByVal value As String)
                _M_ModifyUserID = value
            End Set
        End Property
        Public Property M_ModifyDate() As String
            Get
                Return _M_ModifyDate
            End Get
            Set(ByVal value As String)
                _M_ModifyDate = value
            End Set
        End Property
        Public Property AutoID() As Integer
            Get
                Return _AutoID
            End Get
            Set(ByVal value As Integer)
                _AutoID = value
            End Set
        End Property
        Public Property M_Status() As String
            Get
                Return _M_Status
            End Get
            Set(ByVal value As String)
                _M_Status = value
            End Set
        End Property
    End Class
End Namespace