Namespace LFERP.Library.MrpManager.Bom_D
    Public Class Bom_DInfo
        Private _ParentGroup As String ''''�ե�s���]���~���~�s���B�b���~�^
        Private _ChildGroup As String ''''����ƥ�
        Private _IsUnfold As Boolean ''''�O�_�i�}
        Private _Item As String ''''����
        Private _ReplaceType As String ''''���N�����Ʀr
        Private _ReplaceType1 As String ''''���N�����~�r
        Private _UseFeatures As String ''''�ϥίS��
        Private _EffectiveDate As String ''''�ͮĤ�� 
        Private _InvalidDate As String ''''���Ĥ��
        Private _Mount As Decimal ''''�զ��ζq
        Private _Tmrtc As Decimal ''''�D�󩳼�
        Private _SendUnit As String ''''�o�Ƴ��
        Private _LossRate As Decimal ''''�l�v
        Private _CreateUserID As String ''''�ЫؤH��
        Private _CreateDate As String ''''�Ыؤ��
        Private _ModifyUserID As String ''''�ק�H��
        Private _ModifyDate As String ''''�ק�ɶ�
        Private _ChildName As String ''''���ƦW��
        Private _ChildGauge As String ''''���ƳW��
        Private _ChildMC_Source As String
        Private _AutoID As Decimal ''''�۰ʽs��
        Private _PID As String
        Private _ID As String
        Private _IsBottom As String
        Private _M_Name As String
        Private _M_Gauge As String
        Private _M_Unit As String
        Private _M_Source As String
        Private _sonsNum As Integer
        Private _CreateUserName As String

        Private _SumQty As Decimal ''''�`�ζq
        Private _QtyPer As Decimal ''''���ζq
        Private _ActualQty As Decimal '��ڥζq
        Private _LossQty As Decimal '�l�Ӷq


        Public Sub New()
            _PID = Nothing
            _ID = Nothing
            _IsBottom = Nothing
            _M_Name = Nothing
            _M_Gauge = Nothing
            _M_Unit = Nothing
            _M_Source = Nothing
            _sonsNum = 0
            _CreateUserName = Nothing

            _ParentGroup = Nothing
            _ChildGroup = Nothing
            _IsUnfold = False
            _Item = Nothing
            _ReplaceType = Nothing
            _ReplaceType1 = Nothing
            _UseFeatures = Nothing
            _EffectiveDate = Nothing
            _InvalidDate = Nothing
            _Mount = 0
            _Tmrtc = 0
            _SumQty = 0
            _LossQty = 0
            _ActualQty = 0
            _QtyPer = 0
            _SendUnit = Nothing
            _LossRate = 0
            _CreateUserID = Nothing
            _CreateDate = Nothing
            _ModifyUserID = Nothing
            _ModifyDate = Nothing
            _AutoID = 0
            _ChildName = Nothing
            _ChildGauge = Nothing
            _ChildMC_Source = Nothing

        End Sub
        Public Property CreateUserName() As String
            Get
                Return _CreateUserName
            End Get
            Set(ByVal value As String)
                _CreateUserName = value
            End Set
        End Property
        Public Property sonsNum() As Integer
            Get
                Return _sonsNum
            End Get
            Set(ByVal value As Integer)
                _sonsNum = value
            End Set
        End Property
        Public Property M_Source() As String
            Get
                Return _M_Source
            End Get
            Set(ByVal value As String)
                _M_Source = value
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
        Public Property M_Unit() As String
            Get
                Return _M_Unit
            End Get
            Set(ByVal value As String)
                _M_Unit = value
            End Set
        End Property
        Public Property IsBottom() As String
            Get
                Return _IsBottom
            End Get
            Set(ByVal value As String)
                _IsBottom = value
            End Set
        End Property
        Public Property PID() As String
            Get
                Return _PID
            End Get
            Set(ByVal value As String)
                _PID = value
            End Set
        End Property
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Public Property ChildMC_Source() As String
            Get
                Return _ChildMC_Source
            End Get
            Set(ByVal value As String)
                _ChildMC_Source = value
            End Set
        End Property
        Public Property ChildName() As String
            Get
                Return _ChildName
            End Get
            Set(ByVal value As String)
                _ChildName = value
            End Set
        End Property
        Public Property ChildGauge() As String
            Get
                Return _ChildGauge
            End Get
            Set(ByVal value As String)
                _ChildGauge = value
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
        Public Property ChildGroup() As String
            Get
                Return _ChildGroup
            End Get
            Set(ByVal value As String)
                _ChildGroup = value
            End Set
        End Property
        Public Property IsUnfold() As Boolean
            Get
                Return _IsUnfold
            End Get
            Set(ByVal value As Boolean)
                _IsUnfold = value
            End Set
        End Property
        Public Property Item() As String
            Get
                Return _Item
            End Get
            Set(ByVal value As String)
                _Item = value
            End Set
        End Property
        Public Property ReplaceType() As String
            Get
                Return _ReplaceType
            End Get
            Set(ByVal value As String)
                _ReplaceType = value
            End Set
        End Property
        Public Property ReplaceType1() As String
            Get
                Return _ReplaceType1
            End Get
            Set(ByVal value As String)
                _ReplaceType1 = value
            End Set
        End Property
        Public Property UseFeatures() As String
            Get
                Return _UseFeatures
            End Get
            Set(ByVal value As String)
                _UseFeatures = value
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
        Public Property Mount() As Decimal
            Get
                Return _Mount
            End Get
            Set(ByVal value As Decimal)
                _Mount = value
            End Set
        End Property
        Public Property Tmrtc() As Decimal
            Get
                Return _Tmrtc
            End Get
            Set(ByVal value As Decimal)
                _Tmrtc = value
            End Set
        End Property
        Public Property SumQty() As Decimal
            Get
                Return _SumQty
            End Get
            Set(ByVal value As Decimal)
                _SumQty = value
            End Set
        End Property

        Public Property LossQty() As Decimal
            Get
                Return _LossQty
            End Get
            Set(ByVal value As Decimal)
                _LossQty = value
            End Set
        End Property

        Public Property ActualQty() As Decimal
            Get
                Return _ActualQty
            End Get
            Set(ByVal value As Decimal)
                _ActualQty = value
            End Set
        End Property
        Public Property QtyPer() As Decimal
            Get
                Return _QtyPer
            End Get
            Set(ByVal value As Decimal)
                _QtyPer = value
            End Set
        End Property
        Public Property SendUnit() As String
            Get
                Return _SendUnit
            End Get
            Set(ByVal value As String)
                _SendUnit = value
            End Set
        End Property
        Public Property LossRate() As Decimal
            Get
                Return _LossRate
            End Get
            Set(ByVal value As Decimal)
                _LossRate = value
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

    End Class
End Namespace