Namespace LFERP.Library.SampleManager.SampleTransaction

    Public Class SampleTransactionInfo
        Private _AutoID As Double
        Private _Code_ID As String
        Private _Qty As Integer
        Private _StatusType As String
        Private _Remark As String

        Private _TR_ID As String
        Private _PM_M_Code As String
        Private _SP_ID As String

        Private _CheckUserID As String
        Private _CheckUserName As String
        Private _CheckBit As Boolean
        Private _CheckDate As Date
        Private _CheckRemark As String

        Private _AddUserID As String
        Private _AddUserName As String
        Private _AddDate As Date
        Private _ModifyUserID As String
        Private _ModifyDate As Date

        Private _StatusTypeName As String
        Private _SO_ID As String
        Private _SS_Edition As String
        Private _SYesNO As Boolean
        Private _SP_IDItem As String
        Private _IsTransferred As String '是否可转

        Public Sub New()
            _AutoID = 0
            _Code_ID = Nothing
            _Qty = Nothing
            _StatusType = Nothing
            _Remark = Nothing

            _TR_ID = Nothing
            _PM_M_Code = Nothing
            _SP_ID = Nothing

            _CheckUserID = Nothing
            _CheckUserName = Nothing
            _CheckBit = Nothing
            _CheckDate = Nothing
            _CheckRemark = Nothing

            _AddUserID = Nothing
            _AddDate = Nothing
            _AddUserName = Nothing
            _ModifyUserID = Nothing
            _ModifyDate = Nothing

            _StatusTypeName = Nothing
            _SO_ID = Nothing
            _SS_Edition = Nothing
            _SYesNO = False
            _SP_IDItem = Nothing
            _IsTransferred = Nothing
        End Sub
        Public Property IsTransferred() As String
            Get
                Return _IsTransferred
            End Get
            Set(ByVal value As String)
                _IsTransferred = value
            End Set
        End Property

        Public Property SP_IDItem() As String
            Get
                Return _SP_IDItem
            End Get
            Set(ByVal value As String)
                _SP_IDItem = value
            End Set
        End Property

        Public Property SYesNO() As Boolean
            Get
                Return _SYesNO
            End Get
            Set(ByVal value As Boolean)
                _SYesNO = value
            End Set
        End Property

        Public Property SS_Edition() As String
            Get
                Return _SS_Edition
            End Get
            Set(ByVal value As String)
                _SS_Edition = value
            End Set
        End Property


        Public Property SO_ID() As String
            Get
                Return _SO_ID
            End Get
            Set(ByVal value As String)
                _SO_ID = value
            End Set
        End Property

        Public Property StatusTypeName() As String
            Get
                Return _StatusTypeName
            End Get
            Set(ByVal value As String)
                _StatusTypeName = value
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
        Public Property CheckDate() As Date
            Get
                Return _CheckDate
            End Get
            Set(ByVal value As Date)
                _CheckDate = value
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
        Public Property CheckUserName() As String
            Get
                Return _CheckUserName
            End Get
            Set(ByVal value As String)
                _CheckUserName = value
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

        Public Property SP_ID() As String
            Get
                Return _SP_ID
            End Get
            Set(ByVal value As String)
                _SP_ID = value
            End Set
        End Property

        Public Property PM_M_Code() As String
            Get
                Return _PM_M_Code
            End Get
            Set(ByVal value As String)
                _PM_M_Code = value
            End Set
        End Property

        Public Property TR_ID() As String
            Get
                Return _TR_ID
            End Get
            Set(ByVal value As String)
                _TR_ID = value
            End Set
        End Property


        Public Property AutoID() As Double
            Get
                Return _AutoID
            End Get
            Set(ByVal value As Double)
                _AutoID = value
            End Set
        End Property

        Public Property Code_ID() As String
            Get
                Return _Code_ID
            End Get
            Set(ByVal value As String)
                _Code_ID = value
            End Set
        End Property
        Public Property Qty() As Integer
            Get
                Return _Qty
            End Get
            Set(ByVal value As Integer)
                _Qty = value
            End Set
        End Property

        Public Property StatusType() As String
            Get
                Return _StatusType
            End Get
            Set(ByVal value As String)
                _StatusType = value
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
        Public Property AddUserID() As String
            Get
                Return _AddUserID
            End Get
            Set(ByVal value As String)
                _AddUserID = value
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

        Public Property AddDate() As Date
            Get
                Return _AddDate
            End Get
            Set(ByVal value As Date)
                _AddDate = value
            End Set
        End Property
        Public Property AddUserName() As String
            Get
                Return _AddUserName
            End Get
            Set(ByVal value As String)
                _AddUserName = value
            End Set
        End Property
        Public Property ModifyDate() As Date
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Date)
                _ModifyDate = value
            End Set
        End Property
    End Class
End Namespace

