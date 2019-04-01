
Namespace LFERP.Library.ProductionPiecePayPersonnel


    Public Class ProductionPiecePayPersonnelInfo
        Private _AutoID As String                ' * int                 /�۰ʽs��ID
        Private _Per_NO As String                 ' * nvarchar(20)        /�t�ҽs��
        Private _Per_Name As String               ' * nvarchar(20)        /�m�W
        Private _PL_YYMM As String                '* datetime            /�~��
        Private _DepID As String                   '* nvarchar(10)        /����
        Private _Per_DayPrice As Double           '* real                /���~  

        Private _PL_OnDutyDays As Double           '* real                  /�W�Z�Ѽ�
        Private _PL_UsualOverHours As Double       ' * real                  /���ɥ[�Z�p�ɼ�
        Private _PL_HolidayOVerHours As Double     ' * real                  /�`����[�Z�p�ɼ�
        Private _PL_TimeHours As Double           ' * real                  /�p�ɤu��
        Private _PL_CompensateSum As Double        ' * real                  /���ɪ��B

        Private _PL_SubtractSum As Double          '* real                /�������B
        Private _PL_TimePay As Double              '* real                /�p�ɤu��
        Private _PL_PiecePay As Double             '* real                /�p��u��
        Private _PL_MeritedPay As Double          '* real                /���o�u��
        Private _PL_Remark As String              ' * nvarchar(MAX)       /�ƪ`

        Private _PL_Check As Boolean               '* bit                   /�f��
        Private _PL_CheckUserID As String          '   * nvarchar(20)          /�f�ֽs��
        Private _PL_CheckDate As String           ' * datetime              /�f�֤��
        Private _PL_AddUserID As String          '  * nvarchar(20)          /�K�[�H(�ާ@�H)�s��
        Private _PL_AddDate As String         '* datetime             /�K�[���

        Private _PL_ModifyUserID As String      '    * nvarchar(20)         /�ק�H
        Private _PL_ModifyDate As String         ' * datetime             /�ק���

        Private _PL_AddUserName As String      '     �O���K�[�H���W
        Private _PL_CheckUserName As String    '   �f�֤H���W
        Private _PL_ModifyUserName As String   '    �ק�H���W
        Private _PL_DepName As String          '    �����W

        Private _PL_DateEnd As String         ''���L�ζ}�l�ɶ�
        Private _PL_DateStart As String
        Private _Print_Action As String

        Private _FacID As String
        Private _PL_FacName As String

        Private _PPGtotal_P As Double  '�ӤH�p���`�B
        Private _PPGtotal_T As Double  '�ӤH�p���`�B

        Private _WPdate As String
        Private _Per_Class As String

        Private _PT_Total_Sum As Double '�ӤH�p��,���`�u��


        Sub New()
            _WPdate = Nothing
            _PPGtotal_P = 0
            _PPGtotal_T = 0

            _AutoID = Nothing                ' * int                 /�۰ʽs��ID
            _Per_NO = Nothing                 ' * nvarchar(20)        /�t�ҽs��
            _Per_Name = Nothing               ' * nvarchar(20)        /�m�W
            _PL_YYMM = Nothing                '* datetime            /�~��
            _DepID = Nothing                   '* nvarchar(10)        /����
            _Per_DayPrice = Nothing           '* real                /���~  

            _PL_OnDutyDays = Nothing           '* real                  /�W�Z�Ѽ�
            _PL_UsualOverHours = Nothing      ' * real                  /���ɥ[�Z�p�ɼ�
            _PL_HolidayOVerHours = Nothing    ' * real                  /�`����[�Z�p�ɼ�
            _PL_TimeHours = Nothing          ' * real                  /�p�ɤu��
            _PL_CompensateSum = Nothing       ' * real                  /���ɪ��B

            _PL_SubtractSum = Nothing         '* real                /�������B
            _PL_TimePay = Nothing             '* real                /�p�ɤu��
            _PL_PiecePay = Nothing            '* real                /�p��u��
            _PL_MeritedPay = Nothing         '* real                /���o�u��
            _PL_Remark = Nothing              ' * nvarchar(MAX)       /�ƪ`

            _PL_Check = False              '* bit                   /�f��
            _PL_CheckUserID = Nothing          '   * nvarchar(20)          /�f�ֽs��
            _PL_CheckDate = Nothing           ' * datetime              /�f�֤��
            _PL_AddUserID = Nothing          '  * nvarchar(20)          /�K�[�H(�ާ@�H)�s��
            _PL_AddDate = Nothing         '* datetime             /�K�[���

            _PL_ModifyUserID = Nothing      '    * nvarchar(20)         /�ק�H
            _PL_ModifyDate = Nothing         ' * datetime             /�ק���

            _PL_AddUserName = Nothing      '     �O���K�[�H���W
            _PL_CheckUserName = Nothing    '   �f�֤H���W
            _PL_ModifyUserName = Nothing   '    �ק�H���W
            _PL_DepName = Nothing          '    �����W

            _PL_DateEnd = Nothing         ''���L�ζ}�l�ɶ�
            _PL_DateStart = Nothing
            _Print_Action = Nothing

            _FacID = Nothing
            _PL_FacName = Nothing

            _Per_Class = Nothing

            _PT_Total_Sum = 0

        End Sub

        Public Property PT_Total_Sum() As Double
            Get
                Return _PT_Total_Sum
            End Get
            Set(ByVal value As Double)
                _PT_Total_Sum = value
            End Set
        End Property


        Public Property Per_Class() As String
            Get
                Return _Per_Class
            End Get
            Set(ByVal value As String)
                _Per_Class = value
            End Set
        End Property

        Public Property WPdate() As String
            Get
                Return _WPdate
            End Get
            Set(ByVal value As String)
                _WPdate = value
            End Set
        End Property
        ''' <summary>
        ''' �ӤH�p�ɤu��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PPGtotal_T() As Double
            Get
                Return _PPGtotal_T
            End Get
            Set(ByVal value As Double)
                _PPGtotal_T = value
            End Set
        End Property
        ''' <summary>
        ''' �ӤH�p���`�B���`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PPGtotal_P() As Double
            Get
                Return _PPGtotal_P
            End Get
            Set(ByVal value As Double)
                _PPGtotal_P = value
            End Set
        End Property

        Public Property PL_FacName() As String
            Get
                Return _PL_FacName
            End Get
            Set(ByVal value As String)
                _PL_FacName = value
            End Set
        End Property
        Public Property FacID() As String
            Get
                Return _FacID
            End Get
            Set(ByVal value As String)
                _FacID = value
            End Set
        End Property
        ''' <summary>
        ''' ���L�H�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Print_Action() As String
            Get
                Return _Print_Action
            End Get
            Set(ByVal value As String)
                _Print_Action = value
            End Set
        End Property
        ''' <summary>
        ''' ���L�����ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_DateStart() As String
            Get
                Return _PL_DateStart
            End Get
            Set(ByVal value As String)
                _PL_DateStart = value
            End Set
        End Property
        ''' <summary>
        ''' ���L�ζ}�l�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_DateEnd() As String
            Get
                Return _PL_DateEnd
            End Get
            Set(ByVal value As String)
                _PL_DateEnd = value
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
        '''  /�t�ҽs��
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
        ''' �~��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_YYMM() As String
            Get
                Return _PL_YYMM
            End Get
            Set(ByVal value As String)
                _PL_YYMM = value
            End Set
        End Property
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DepID() As String
            Get
                Return _DepID
            End Get
            Set(ByVal value As String)
                _DepID = value
            End Set
        End Property
        ''' <summary>
        ''' ���~
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_DayPrice() As Double
            Get
                Return _Per_DayPrice
            End Get
            Set(ByVal value As Double)
                _Per_DayPrice = value
            End Set
        End Property
        ''' <summary>
        ''' �W�Z�Ѽ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_OnDutyDays() As Double
            Get
                Return _PL_OnDutyDays
            End Get
            Set(ByVal value As Double)
                _PL_OnDutyDays = value
            End Set
        End Property
        ''' <summary>
        ''' ���ɥ[�Z�p�ɼ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_UsualOverHours() As Double
            Get
                Return _PL_UsualOverHours
            End Get
            Set(ByVal value As Double)
                _PL_UsualOverHours = value
            End Set
        End Property

        ''' <summary>
        ''' �`����[�Z�p�ɼ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_HolidayOVerHours() As Double
            Get
                Return _PL_HolidayOVerHours
            End Get
            Set(ByVal value As Double)
                _PL_HolidayOVerHours = value
            End Set
        End Property
        ''' <summary>
        '''  /�p�ɤu��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_TimeHours() As Double
            Get
                Return _PL_TimeHours
            End Get
            Set(ByVal value As Double)
                _PL_TimeHours = value
            End Set
        End Property

        ''' <summary>
        ''' ���ɪ��B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_CompensateSum() As Double
            Get
                Return _PL_CompensateSum
            End Get
            Set(ByVal value As Double)
                _PL_CompensateSum = value
            End Set
        End Property

        ''' <summary>
        ''' �������B
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_SubtractSum() As Double
            Get
                Return _PL_SubtractSum
            End Get
            Set(ByVal value As Double)
                _PL_SubtractSum = value
            End Set
        End Property
        ''' <summary>
        ''' �p�ɤu��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_TimePay() As Double
            Get
                Return _PL_TimePay
            End Get
            Set(ByVal value As Double)
                _PL_TimePay = value
            End Set
        End Property
        ''' <summary>
        ''' �p��u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_PiecePay() As Double
            Get
                Return _PL_PiecePay
            End Get
            Set(ByVal value As Double)
                _PL_PiecePay = value
            End Set
        End Property
        ''' <summary>
        ''' ���o�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_MeritedPay() As Double
            Get
                Return _PL_MeritedPay
            End Get
            Set(ByVal value As Double)
                _PL_MeritedPay = value
            End Set
        End Property
        ''' <summary>
        ''' /�ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_Remark() As String
            Get
                Return _PL_Remark
            End Get
            Set(ByVal value As String)
                _PL_Remark = value
            End Set
        End Property
        ''' <summary>
        ''' �f��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_Check() As Boolean
            Get
                Return _PL_Check
            End Get
            Set(ByVal value As Boolean)
                _PL_Check = value
            End Set
        End Property
        ''' <summary>
        ''' �f�ֽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_CheckUserID() As String
            Get
                Return _PL_CheckUserID
            End Get
            Set(ByVal value As String)
                _PL_CheckUserID = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_CheckDate() As String
            Get
                Return _PL_CheckDate
            End Get
            Set(ByVal value As String)
                _PL_CheckDate = value
            End Set
        End Property
        ''' <summary>
        ''' �K�[�H(�ާ@�H)�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_AddUserID() As String
            Get
                Return _PL_AddUserID
            End Get
            Set(ByVal value As String)
                _PL_AddUserID = value
            End Set
        End Property
        ''' <summary>
        ''' �K�[���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_AddDate() As String
            Get
                Return _PL_AddDate
            End Get
            Set(ByVal value As String)
                _PL_AddDate = value
            End Set
        End Property
        ''' <summary>
        ''' �ק�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_ModifyUserID() As String
            Get
                Return _PL_ModifyUserID
            End Get
            Set(ByVal value As String)
                _PL_ModifyUserID = value
            End Set
        End Property
        ''' <summary>
        ''' �ק���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_ModifyDate() As String
            Get
                Return _PL_ModifyDate
            End Get
            Set(ByVal value As String)
                _PL_ModifyDate = value
            End Set
        End Property
        ''' <summary>
        ''' �O���K�[�H���W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_AddUserName() As String
            Get
                Return _PL_AddUserName
            End Get
            Set(ByVal value As String)
                _PL_AddUserName = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤H���W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_CheckUserName() As String
            Get
                Return _PL_CheckUserName
            End Get
            Set(ByVal value As String)
                _PL_CheckUserName = value
            End Set
        End Property
        ''' <summary>
        '''  �ק�H���W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_ModifyUserName() As String
            Get
                Return _PL_ModifyUserName
            End Get
            Set(ByVal value As String)
                _PL_ModifyUserName = value
            End Set
        End Property
        ''' <summary>
        ''' �����W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PL_DepName() As String
            Get
                Return _PL_DepName
            End Get
            Set(ByVal value As String)
                _PL_DepName = value
            End Set
        End Property
    End Class

End Namespace