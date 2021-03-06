Namespace LFERP.Library.BroadCastManager.BroadCastFactory

    ''' <summary>
    ''' 样办寄送表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BroadCastFactoryInfo
        Private _Fac_no As String
        Private _Fac_Name As String
        Private _Fac_AdduserID As String
        Private _Fac_Adddate As Integer
        Private _Fac_ModifyUserID As String
        Private _Fac_ModifyDate As String
        Private _M_Name As String
        Private _M_Code As String
        Private _M_PID As String
        Private _M_KEY As String
        Private _U_Name As String
        Private _Type As String
        Private _AutoID As Integer


        Public Sub New()
            _Fac_no = Nothing
            _Fac_Name = Nothing
            _Fac_AdduserID = Nothing
            _Fac_Adddate = Nothing
            _Fac_ModifyUserID = Nothing
            _Fac_ModifyDate = Nothing
            _M_Name = Nothing
            _M_Code = Nothing
            _M_PID = Nothing
            _M_KEY = Nothing
            _U_Name = Nothing
            _Type = Nothing
            _AutoID = 0
        End Sub
        Public Property U_Name() As String
            Get
                Return _U_Name
            End Get
            Set(ByVal value As String)
                _U_Name = value
            End Set
        End Property
        Public Property M_Name() As String
            Get
                Return _M_Name
            End Get
            Set(ByVal value As String)
                _M_Name = value
            End Set
        End Property
        Public Property M_Code() As String
            Get
                Return _M_Code
            End Get
            Set(ByVal value As String)
                _M_Code = value
            End Set
        End Property
        Public Property M_PID() As String
            Get
                Return _M_PID
            End Get
            Set(ByVal value As String)
                _M_PID = value
            End Set
        End Property
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property
        Public Property M_KEY() As String
            Get
                Return _M_KEY
            End Get
            Set(ByVal value As String)
                _M_KEY = value
            End Set
        End Property

        Public Property Fac_ModifyUserID() As String
            Get
                Return _Fac_ModifyUserID
            End Get
            Set(ByVal value As String)
                _Fac_ModifyUserID = value
            End Set
        End Property
        Public Property Fac_ModifyDate() As String
            Get
                Return _Fac_ModifyDate
            End Get
            Set(ByVal value As String)
                _Fac_ModifyDate = value
            End Set
        End Property
        Public Property Fac_Adddate() As String
            Get
                Return _Fac_Adddate
            End Get
            Set(ByVal value As String)
                _Fac_Adddate = value
            End Set
        End Property
        Public Property Fac_AdduserID() As String
            Get
                Return _Fac_AdduserID
            End Get
            Set(ByVal value As String)
                _Fac_AdduserID = value
            End Set
        End Property
        Public Property Fac_Name() As String
            Get
                Return _Fac_Name
            End Get
            Set(ByVal value As String)
                _Fac_Name = value
            End Set
        End Property
        Public Property Fac_no() As String
            Get
                Return _Fac_no
            End Get
            Set(ByVal value As String)
                _Fac_no = value
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
    End Class

End Namespace

