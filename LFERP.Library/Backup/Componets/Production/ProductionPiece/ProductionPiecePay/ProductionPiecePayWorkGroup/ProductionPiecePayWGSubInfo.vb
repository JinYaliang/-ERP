Namespace LFERP.Library.ProductionPiecePayWGSub

    Public Class ProductionPiecePayWGSubInfo
        Private _AutoID As String     '�۰ʽs��ID
        Private _PY_ID As String     '�渹
        Private _Per_NO As String     '�t�ҽs��
        Private _Per_Name As String     '�m�W
        Private _PYS_FormulaID As String     '�p�⤽���s��
        Private _Per_DayPrice As Double '�~������
        Private _PYS_OnDutyDays As Double     '�W�Z�Ѽ�
        Private _PYS_UsualOverTime As Double     '���ɥ[�Z�p�ɼ�
        Private _PYS_HolidayOVerTime As Double     '�`����[�Z�p�ɼ�
        Private _PYS_Proportion As Double     '�u�ɤ��
        Private _PYS_Bonus As Double     '����
        Private _PYS_AllHours As Double     '�`�u��
        Private _PYS_MeritedHours As Double     '���p�u��
        Private _PYS_TimePay As Double     '�p�ɤu��
        Private _PYS_PiecePay As Double     '�p��u��
        Private _PYS_MeritedPay As Double     '���o�u��
        Private _PYS_Remark As String     '�ƪ`

        Private _PYS_WorkWGDay As Double '�b���w�դu�@���Ѽ�
        Private _Per_PayType As String   '�~������

        Sub New()
            _Per_PayType = Nothing
            _PYS_WorkWGDay = 0
            _AutoID = Nothing
            _PY_ID = Nothing
            _Per_NO = Nothing
            _Per_Name = Nothing
            _PYS_FormulaID = Nothing
            _Per_DayPrice = 0
            _PYS_OnDutyDays = 0
            _PYS_UsualOverTime = 0
            _PYS_HolidayOVerTime = 0
            _PYS_Proportion = 0
            _PYS_Bonus = 0
            _PYS_AllHours = 0
            _PYS_MeritedHours = 0
            _PYS_TimePay = 0
            _PYS_PiecePay = 0
            _PYS_MeritedPay = 0
            _PYS_Remark = Nothing
        End Sub
        ''' <summary>
        ''' �~������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_PayType() As String
            Get
                Return _Per_PayType
            End Get
            Set(ByVal value As String)
                _Per_PayType = value
            End Set
        End Property
        ''' <summary>
        ''' �b���w�դu�@���Ѽ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_WorkWGDay() As String
            Get
                Return _PYS_WorkWGDay
            End Get
            Set(ByVal value As String)
                _PYS_WorkWGDay = value
            End Set
        End Property
        ''' <summary>
        ''' �۰ʽs��ID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AutoID() As String
            Get
                Return _AutoID
            End Get
            Set(ByVal value As String)
                _AutoID = value
            End Set
        End Property


        ''' <summary>
        ''' �渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PY_ID() As String
            Get
                Return _PY_ID
            End Get
            Set(ByVal value As String)
                _PY_ID = value
            End Set
        End Property


        ''' <summary>
        ''' �t�ҽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_NO() As String
            Get
                Return _Per_NO
            End Get
            Set(ByVal value As String)
                _Per_NO = value
            End Set
        End Property


        ''' <summary>
        ''' �m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Name() As String
            Get
                Return _Per_Name
            End Get
            Set(ByVal value As String)
                _Per_Name = value
            End Set
        End Property


        ''' <summary>
        ''' �p�⤽���s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_FormulaID() As String
            Get
                Return _PYS_FormulaID
            End Get
            Set(ByVal value As String)
                _PYS_FormulaID = value
            End Set
        End Property


        ''' <summary>
        ''' ���~�~
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_DayPrice() As String
            Get
                Return _Per_DayPrice
            End Get
            Set(ByVal value As String)
                _Per_DayPrice = value
            End Set
        End Property


        ''' <summary>
        ''' �W�Z�Ѽ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_OnDutyDays() As Double
            Get
                Return _PYS_OnDutyDays
            End Get
            Set(ByVal value As Double)
                _PYS_OnDutyDays = value
            End Set
        End Property


        ''' <summary>
        ''' ���ɥ[�Z�p�ɼ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_UsualOverTime() As Double
            Get
                Return _PYS_UsualOverTime
            End Get
            Set(ByVal value As Double)
                _PYS_UsualOverTime = value
            End Set
        End Property


        ''' <summary>
        ''' �`����[�Z�p�ɼ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_HolidayOVerTime() As Double
            Get
                Return _PYS_HolidayOVerTime
            End Get
            Set(ByVal value As Double)
                _PYS_HolidayOVerTime = value
            End Set
        End Property


        ''' <summary>
        ''' �u�ɤ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_Proportion() As Double
            Get
                Return _PYS_Proportion
            End Get
            Set(ByVal value As Double)
                _PYS_Proportion = value
            End Set
        End Property


        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_Bonus() As Double
            Get
                Return _PYS_Bonus
            End Get
            Set(ByVal value As Double)
                _PYS_Bonus = value
            End Set
        End Property


        ''' <summary>
        ''' �`�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_AllHours() As Double
            Get
                Return _PYS_AllHours
            End Get
            Set(ByVal value As Double)
                _PYS_AllHours = value
            End Set
        End Property


        ''' <summary>
        ''' ���p�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_MeritedHours() As Double
            Get
                Return _PYS_MeritedHours
            End Get
            Set(ByVal value As Double)
                _PYS_MeritedHours = value
            End Set
        End Property


        ''' <summary>
        ''' �p�ɤu��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_TimePay() As Double
            Get
                Return _PYS_TimePay
            End Get
            Set(ByVal value As Double)
                _PYS_TimePay = value
            End Set
        End Property


        ''' <summary>
        ''' �p��u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_PiecePay() As Double
            Get
                Return _PYS_PiecePay
            End Get
            Set(ByVal value As Double)
                _PYS_PiecePay = value
            End Set
        End Property


        ''' <summary>
        ''' ���o�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_MeritedPay() As Double
            Get
                Return _PYS_MeritedPay
            End Get
            Set(ByVal value As Double)
                _PYS_MeritedPay = value
            End Set
        End Property


        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PYS_Remark() As String
            Get
                Return _PYS_Remark
            End Get
            Set(ByVal value As String)
                _PYS_Remark = value
            End Set
        End Property
    End Class

End Namespace