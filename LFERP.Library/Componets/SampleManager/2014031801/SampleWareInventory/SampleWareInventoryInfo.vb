Namespace LFERP.Library.SampleManager.SampleWareInventory

    ''' <summary>
    ''' 样办庫存表
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SampleWareInventoryInfo
        Private _PM_M_Code As String
        Private _M_Code As String
        Private _PS_NO As String
        Private _SWI_Qty As Integer
        Private _OutQty As Integer
        Private _InQty As Integer
        Private _BarCodeCount As Integer
        Private _LoseCount As Integer
        Private _DamageCount As Integer
        Private _FinishedCount As Integer
        Private _ReturnCount As Integer
        Private _SendCount As Integer

        Private _PS_Name As String
        Private _D_Dep As String
        Private _D_ID As String

        Private _AutoID As String
        Private _AddUserID As String
        Private _AddUserName As String
        Private _AddDate As Date
        Private _ModifyUserID As String
        Private _ModifyDate As Date

        Private _SWI_QtyQ As Int32
        Private _SWI_QtyH As Int32
        Private _SO_SampleID As String
        Private _MaterialTypeName As String

        Private _Borrow_Qty As Integer
        Private _RepayQty As Integer
        Private _SO_ID As String
        Private _AvailableQty As Integer
        Private _NoBorrow_Qty As Integer


        Public Sub New()

            _M_Code = Nothing
            _PM_M_Code = Nothing
            _PS_NO = Nothing
            _SWI_Qty = 0
            _OutQty = 0
            _InQty = 0
            _BarCodeCount = 0
            _LoseCount = 0
            _DamageCount = 0
            _FinishedCount = 0
            _ReturnCount = 0
            _SendCount = 0
            _Borrow_Qty = 0
            _RepayQty = 0
            _AvailableQty = 0
            _NoBorrow_Qty = 0

            _PS_Name = Nothing
            _D_Dep = Nothing
            _D_ID = Nothing
            _SO_ID = Nothing

            _AddUserID = Nothing
            _AddUserName = Nothing
            _AddDate = Nothing
            _ModifyUserID = Nothing
            _ModifyDate = Nothing
            _AutoID = Nothing

            _SWI_QtyQ = 0
            _SWI_QtyH = 0
            _SO_SampleID = Nothing
            _MaterialTypeName = Nothing
        End Sub
        Public Property NoBorrow_Qty() As Integer
            Get
                Return _NoBorrow_Qty
            End Get
            Set(ByVal value As Integer)
                _NoBorrow_Qty = value
            End Set
        End Property
        Public Property Borrow_Qty() As Integer
            Get
                Return _Borrow_Qty
            End Get
            Set(ByVal value As Integer)
                _Borrow_Qty = value
            End Set
        End Property
        Public Property RepayQty() As Integer
            Get
                Return _RepayQty
            End Get
            Set(ByVal value As Integer)
                _RepayQty = value
            End Set
        End Property
        Public Property AvailableQty() As Integer
            Get
                Return _AvailableQty
            End Get
            Set(ByVal value As Integer)
                _AvailableQty = value
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

        Public Property MaterialTypeName() As String
            Get
                Return _MaterialTypeName
            End Get
            Set(ByVal value As String)
                _MaterialTypeName = value
            End Set
        End Property
        Public Property SO_SampleID() As String
            Get
                Return _SO_SampleID
            End Get
            Set(ByVal value As String)
                _SO_SampleID = value
            End Set
        End Property
        Public Property SWI_QtyH() As Int32
            Get
                Return _SWI_QtyH
            End Get
            Set(ByVal value As Int32)
                _SWI_QtyH = value
            End Set
        End Property

        Public Property SWI_QtyQ() As Int32
            Get
                Return _SWI_QtyQ
            End Get
            Set(ByVal value As Int32)
                _SWI_QtyQ = value
            End Set
        End Property


        Public Property SendCount() As Integer
            Get
                Return _SendCount
            End Get
            Set(ByVal value As Integer)
                _SendCount = value
            End Set
        End Property
        Public Property LoseCount() As Integer
            Get
                Return _LoseCount
            End Get
            Set(ByVal value As Integer)
                _LoseCount = value
            End Set
        End Property
        Public Property DamageCount() As Integer
            Get
                Return _DamageCount
            End Get
            Set(ByVal value As Integer)
                _DamageCount = value
            End Set
        End Property
        Public Property FinishedCount() As Integer
            Get
                Return _FinishedCount
            End Get
            Set(ByVal value As Integer)
                _FinishedCount = value
            End Set
        End Property
        Public Property ReturnCount() As Integer
            Get
                Return _ReturnCount
            End Get
            Set(ByVal value As Integer)
                _ReturnCount = value
            End Set
        End Property

        Public Property BarCodeCount() As Integer
            Get
                Return _BarCodeCount
            End Get
            Set(ByVal value As Integer)
                _BarCodeCount = value
            End Set
        End Property
        Public Property OutQty() As Integer
            Get
                Return _OutQty
            End Get
            Set(ByVal value As Integer)
                _OutQty = value
            End Set
        End Property
        Public Property InQty() As Integer
            Get
                Return _InQty
            End Get
            Set(ByVal value As Integer)
                _InQty = value
            End Set
        End Property


        Public Property D_ID() As String
            Get
                Return _D_ID
            End Get
            Set(ByVal value As String)
                _D_ID = value
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
        Public Property D_Dep() As String
            Get
                Return _D_Dep
            End Get
            Set(ByVal value As String)
                _D_Dep = value
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

        Public Property PM_M_Code() As String
            Get
                Return _PM_M_Code
            End Get
            Set(ByVal value As String)
                _PM_M_Code = value
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



        Public Property AutoID() As String
            Get
                Return _AutoID
            End Get
            Set(ByVal value As String)
                _AutoID = value
            End Set
        End Property

        Public Property SWI_Qty() As Integer
            Get
                Return _SWI_Qty
            End Get
            Set(ByVal value As Integer)
                _SWI_Qty = value
            End Set
        End Property

        Public Property AddDate() As String
            Get
                Return _AddDate
            End Get
            Set(ByVal value As String)
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
        Public Property AddUserName() As String
            Get
                Return _AddUserName
            End Get
            Set(ByVal value As String)
                _AddUserName = value
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
        Public Property ModifyDate() As String
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As String)
                _ModifyDate = value
            End Set
        End Property
    End Class
End Namespace