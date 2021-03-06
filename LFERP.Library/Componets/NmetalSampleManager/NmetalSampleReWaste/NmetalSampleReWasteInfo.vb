Namespace LFERP.Library.NmetalSampleManager.NmetalSampleReWaste
    Public Class NmetalSampleReWasteInfo

        Private _AutoID As String
        Private _ReNO As String
        Private _ReDepID As String
        Private _OutDepID As String
        Private _ReDate As Date

        Private _ReType As String
        Private _PM_M_Code As String
        Private _PM_Type As String
        Private _PS_NO As String
        Private _ReWeight As Decimal
        Private _RealWeight As Decimal

        Private _Remark As String
        Private _AddUserID As String
        Private _AddDate As Date
        Private _ModifyUserID As String
        Private _ModifyDate As Date

        Private _ReCheck As Boolean
        Private _CheckDate As String
        Private _CheckUserID As String
        Private _CheckRemark As String

        Private _ReDepName As String
        Private _OutDepName As String
        Private _PS_Name As String
        Private _Add_Name As String
        Private _Modify_Name As String
        Private _Check_Name As String

        '------------------------------------
        Private _Incheck As Boolean
        Private _InCheckDate As String
        Private _InCheckUserID As String
        Private _InCheckWeight As Decimal
        Private _InCheckRemark As String
        Private _InCheckName As String
        Private _PS_NOIn As String
        Private _PS_NameIn As String

        Private _OutCardID As String
        Private _InCardID As String

        '类型
        Private _ReTypeID As String
        Private _ReTypeName As String
        Private _IsBarCode As Boolean
        Private _IsSamePS_NO As Boolean
        Private _IsSameDep As Boolean
        Private _IsDeductOut As Boolean
        Private _IsDeductIn As Boolean


        Public Sub New()
            _AutoID = Nothing
            _ReNO = Nothing
            _ReDepID = Nothing
            _OutDepID = Nothing
            _ReDate = Nothing

            _ReType = Nothing
            _PM_M_Code = Nothing
            _PM_Type = Nothing
            _PS_NO = Nothing
            _ReWeight = 0
            _RealWeight = 0

            _Remark = Nothing
            _AddUserID = Nothing
            _AddDate = Nothing
            _ModifyUserID = Nothing
            _ModifyDate = Nothing

            _ReCheck = Nothing
            _CheckDate = Nothing
            _CheckUserID = Nothing
            _CheckRemark = Nothing

            _ReDepName = Nothing
            _OutDepName = Nothing
            _PS_Name = Nothing
            _Add_Name = Nothing
            _Modify_Name = Nothing
            _Check_Name = Nothing

            _Incheck = False
            _InCheckDate = Nothing
            _InCheckUserID = Nothing
            _InCheckWeight = 0
            _InCheckRemark = Nothing

            _InCheckName = Nothing

            _ReTypeID = Nothing
            _ReTypeName = Nothing
            _IsBarCode = False
            _IsSamePS_NO = False
            _IsSameDep = False
            _IsDeductOut = False
            _IsDeductIn = False

            _PS_NOIn = Nothing
            _PS_NameIn = Nothing

            _OutCardID = Nothing
            _InCardID = Nothing

        End Sub

        Public Property OutCardID() As String
            Get
                Return _OutCardID
            End Get
            Set(ByVal value As String)
                _OutCardID = value
            End Set
        End Property

        Public Property InCardID() As String
            Get
                Return _InCardID
            End Get
            Set(ByVal value As String)
                _InCardID = value
            End Set
        End Property

        Public Property PS_NameIn() As String
            Get
                Return _PS_NameIn
            End Get
            Set(ByVal value As String)
                _PS_NameIn = value
            End Set
        End Property


        Public Property PS_NOIn() As String
            Get
                Return _PS_NOIn
            End Get
            Set(ByVal value As String)
                _PS_NOIn = value
            End Set
        End Property

        Public Property IsDeductIn() As Boolean
            Get
                Return _IsDeductIn
            End Get
            Set(ByVal value As Boolean)
                _IsDeductIn = value
            End Set
        End Property


        Public Property IsDeductOut() As Boolean
            Get
                Return _IsDeductOut
            End Get
            Set(ByVal value As Boolean)
                _IsDeductOut = value
            End Set
        End Property

        Public Property IsSameDep() As Boolean
            Get
                Return _IsSameDep
            End Get
            Set(ByVal value As Boolean)
                _IsSameDep = value
            End Set
        End Property

        Public Property IsSamePS_NO() As Boolean
            Get
                Return _IsSamePS_NO
            End Get
            Set(ByVal value As Boolean)
                _IsSamePS_NO = value
            End Set
        End Property

        Public Property IsBarCode() As Boolean
            Get
                Return _IsBarCode
            End Get
            Set(ByVal value As Boolean)
                _IsBarCode = value
            End Set
        End Property


        Public Property ReTypeName() As String
            Get
                Return _ReTypeName
            End Get
            Set(ByVal value As String)
                _ReTypeName = value
            End Set
        End Property


        Public Property ReTypeID() As String
            Get
                Return _ReTypeID
            End Get
            Set(ByVal value As String)
                _ReTypeID = value
            End Set
        End Property


        Public Property InCheckName() As String
            Get
                Return _InCheckName
            End Get
            Set(ByVal value As String)
                _InCheckName = value
            End Set
        End Property

        Public Property Incheck() As Boolean
            Get
                Return _Incheck
            End Get
            Set(ByVal value As Boolean)
                _Incheck = value
            End Set
        End Property


        Public Property InCheckDate() As String
            Get
                Return _InCheckDate
            End Get
            Set(ByVal value As String)
                _InCheckDate = value
            End Set
        End Property


        Public Property InCheckUserID() As String
            Get
                Return _InCheckUserID
            End Get
            Set(ByVal value As String)
                _InCheckUserID = value
            End Set
        End Property

        Public Property InCheckWeight() As String
            Get
                Return _InCheckWeight
            End Get
            Set(ByVal value As String)
                _InCheckWeight = value
            End Set
        End Property


        Public Property InCheckRemark() As String
            Get
                Return _InCheckRemark
            End Get
            Set(ByVal value As String)
                _InCheckRemark = value
            End Set
        End Property

        '---------------------------------------------------
        Public Property AutoID() As String
            Get
                Return _AutoID
            End Get
            Set(ByVal value As String)
                _AutoID = value
            End Set
        End Property
        Public Property ReNO() As String
            Get
                Return _ReNO
            End Get
            Set(ByVal value As String)
                _ReNO = value
            End Set
        End Property
        Public Property ReDepID() As String
            Get
                Return _ReDepID
            End Get
            Set(ByVal value As String)
                _ReDepID = value
            End Set
        End Property
        Public Property OutDepID() As String
            Get
                Return _OutDepID
            End Get
            Set(ByVal value As String)
                _OutDepID = value
            End Set
        End Property
        Public Property ReDate() As Date
            Get
                Return _ReDate
            End Get
            Set(ByVal value As Date)
                _ReDate = value
            End Set
        End Property


        Public Property ReType() As String
            Get
                Return _ReType
            End Get
            Set(ByVal value As String)
                _ReType = value
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
        Public Property PM_Type() As String
            Get
                Return _PM_Type
            End Get
            Set(ByVal value As String)
                _PM_Type = value
            End Set
        End Property
        Public Property PS_NO() As String
            Get
                Return _PS_NO
            End Get
            Set(ByVal value As String)
                _PS_NO = value
            End Set
        End Property
        Public Property ReWeight() As Decimal
            Get
                Return _ReWeight
            End Get
            Set(ByVal value As Decimal)
                _ReWeight = value
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
        Public Property AddDate() As Date
            Get
                Return _AddDate
            End Get
            Set(ByVal value As Date)
                _AddDate = value
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
        Public Property ModifyDate() As Date
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Date)
                _ModifyDate = value
            End Set
        End Property


        Public Property ReCheck() As Boolean
            Get
                Return _ReCheck
            End Get
            Set(ByVal value As Boolean)
                _ReCheck = value
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
        Public Property CheckUserID() As String
            Get
                Return _CheckUserID
            End Get
            Set(ByVal value As String)
                _CheckUserID = value
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


        Public Property ReDepName() As String
            Get
                Return _ReDepName
            End Get
            Set(ByVal value As String)
                _ReDepName = value
            End Set
        End Property
        Public Property OutDepName() As String
            Get
                Return _OutDepName
            End Get
            Set(ByVal value As String)
                _OutDepName = value
            End Set
        End Property
        Public Property PS_Name() As String
            Get
                Return _PS_Name
            End Get
            Set(ByVal value As String)
                _PS_Name = value
            End Set
        End Property
        Public Property Add_Name() As String
            Get
                Return _Add_Name
            End Get
            Set(ByVal value As String)
                _Add_Name = value
            End Set
        End Property
        Public Property Modify_Name() As String
            Get
                Return _Modify_Name
            End Get
            Set(ByVal value As String)
                _Modify_Name = value
            End Set
        End Property
        Public Property Check_Name() As String
            Get
                Return _Check_Name
            End Get
            Set(ByVal value As String)
                _Check_Name = value
            End Set
        End Property

        Public Property RealWeight() As Decimal
            Get
                Return _RealWeight
            End Get
            Set(ByVal value As Decimal)
                _RealWeight = value
            End Set
        End Property


    End Class
End Namespace