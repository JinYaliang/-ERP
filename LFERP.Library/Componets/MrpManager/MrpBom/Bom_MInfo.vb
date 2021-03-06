Namespace LFERP.Library.MrpManager.Bom_M
    Public Class Bom_MInfo
        Private _ParentGroup As String
        Private _M_Name As String
        Private _M_Gauge As String
        Private _SoureName As String
        Private _CheckDate As String
        Private _M_Unit As String
        Private _EffectiveDate As String
        Private _InvalidDate As String
        Private _Version As Integer
        Private _CheckUserID As String
        Private _CheckUserName As String
        Private _CheckBit As Boolean
        Private _CheckRemark As String

        Private _CreateUserName As String
        Private _CreateUserID As String
        Private _CreateDate As String
        Private _ModifyUserID As String
        Private _ModifyDate As String
        Private _AutoID As Decimal
        '-----
        Private _GroupAutoID As String
        Private _GroupNumber As String
        Private _GroupName As String
        Private _BomNumber As String
        Private _UseStatus As Boolean
        Private _CustomerNumber As String
        Private _Qty As Decimal
        Private _Yield As Decimal
        Private _Remark As String
        '--------------
        Public Sub New()
            _ParentGroup = Nothing
            _M_Name = Nothing
            _M_Gauge = Nothing
            _SoureName = Nothing
            _CheckDate = Nothing
            _M_Unit = Nothing
            _EffectiveDate = Nothing
            _InvalidDate = Nothing
            _Version = 1
            _CheckUserID = Nothing
            _CheckBit = False
            _CheckRemark = Nothing
            _CreateUserID = Nothing
            _CreateDate = Nothing
            _ModifyUserID = Nothing
            _ModifyDate = Nothing
            _AutoID = 0
            _CheckUserName = Nothing
            _CreateUserName = Nothing
            _GroupAutoID = 0
            _GroupNumber = Nothing
            _GroupName = Nothing
            _BomNumber = Nothing
            _UseStatus = False
            _CustomerNumber = Nothing
            _Qty = Nothing
            _Yield = 0
            _Remark = Nothing
        End Sub
        Public Property CheckUserName() As String
            Get
                Return _CheckUserName
            End Get
            Set(ByVal value As String)
                _CheckUserName = value
            End Set
        End Property
        Public Property SoureName() As String
            Get
                Return _SoureName
            End Get
            Set(ByVal value As String)
                _SoureName = value
            End Set
        End Property
        Public Property CreateUserName() As String
            Get
                Return _CreateUserName
            End Get
            Set(ByVal value As String)
                _CreateUserName = value
            End Set
        End Property

        Public Property ParentGroup() As String
            Get
                Return _ParentGroup
            End Get
            Set(ByVal value As String)
                _ParentGroup = value
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
        Public Property M_Gauge() As String
            Get
                Return _M_Gauge
            End Get
            Set(ByVal value As String)
                _M_Gauge = value
            End Set
        End Property

        Public Property CheckDate() As String
            Get
                Return _CheckDate
            End Get
            Set(ByVal value As String)
                _CheckDate = value
            End Set
        End Property
        Public Property M_Unit() As String
            Get
                Return _M_Unit
            End Get
            Set(ByVal value As String)
                _M_Unit = value
            End Set
        End Property
        Public Property EffectiveDate() As String
            Get
                Return _EffectiveDate
            End Get
            Set(ByVal value As String)
                _EffectiveDate = value
            End Set
        End Property
        Public Property InvalidDate() As String
            Get
                Return _InvalidDate
            End Get
            Set(ByVal value As String)
                _InvalidDate = value
            End Set
        End Property
        Public Property Version() As Integer
            Get
                Return _Version
            End Get
            Set(ByVal value As Integer)
                _Version = value
            End Set
        End Property
        Public Property CheckUserID() As String
            Get
                Return _CheckUserID
            End Get
            Set(ByVal value As String)
                _CheckUserID = value
            End Set
        End Property
        Public Property CheckBit() As Boolean
            Get
                Return _CheckBit
            End Get
            Set(ByVal value As Boolean)
                _CheckBit = value
            End Set
        End Property
        Public Property CheckRemark() As String
            Get
                Return _CheckRemark
            End Get
            Set(ByVal value As String)
                _CheckRemark = value
            End Set
        End Property
        Public Property CreateUserID() As String
            Get
                Return _CreateUserID
            End Get
            Set(ByVal value As String)
                _CreateUserID = value
            End Set
        End Property
        Public Property CreateDate() As String
            Get
                Return _CreateDate
            End Get
            Set(ByVal value As String)
                _CreateDate = value
            End Set
        End Property
        Public Property ModifyUserID() As String
            Get
                Return _ModifyUserID
            End Get
            Set(ByVal value As String)
                _ModifyUserID = value
            End Set
        End Property
        Public Property ModifyDate() As String
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As String)
                _ModifyDate = value
            End Set
        End Property
        Public Property AutoID() As Decimal
            Get
                Return _AutoID
            End Get
            Set(ByVal value As Decimal)
                _AutoID = value
            End Set
        End Property
        Public Property GroupNumber() As String
            Get
                Return _GroupNumber
            End Get
            Set(ByVal value As String)
                _GroupNumber = value
            End Set
        End Property
        Public Property GroupAutoID() As Integer
            Get
                Return _GroupAutoID
            End Get
            Set(ByVal value As Integer)
                _GroupAutoID = value
            End Set
        End Property
        Public Property GroupName() As String
            Get
                Return _GroupName
            End Get
            Set(ByVal value As String)
                _GroupName = value
            End Set
        End Property
        Public Property BomNumber() As String
            Get
                Return _BomNumber
            End Get
            Set(ByVal value As String)
                _BomNumber = value
            End Set
        End Property
        Public Property UseStatus() As Boolean
            Get
                Return _UseStatus
            End Get
            Set(ByVal value As Boolean)
                _UseStatus = value
            End Set
        End Property
        Public Property CustomerNumber() As String
            Get
                Return _CustomerNumber
            End Get
            Set(ByVal value As String)
                _CustomerNumber = value
            End Set
        End Property
        Public Property Qty() As Decimal
            Get
                Return _Qty
            End Get
            Set(ByVal value As Decimal)
                _Qty = value
            End Set
        End Property
        Public Property Yield() As Decimal
            Get
                Return _Yield
            End Get
            Set(ByVal value As Decimal)
                _Yield = value
            End Set
        End Property
        Public Property Remark() As String
            Get
                Return _Remark
            End Get
            Set(ByVal value As String)
                _Remark = value
            End Set
        End Property
    End Class
End Namespace