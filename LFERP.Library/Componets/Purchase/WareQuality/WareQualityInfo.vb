Namespace LFERP.Library.Purchase.WareQuality

    Public Class WareQualityInfo
        Private _WQ_Code As String
        Private _WQ_Dpt As String
        Private _AP_UpDptName As String
        Private _AP_ApplyDptName As String
        Private _M_Code As String
        Private _M_Name As String
        Private _M_Gauge As String
        Private _WO_Qty As Single
        Private _M_Unit As String
        Private _WO_ID As String
        Private _WQ_Description As String
        Private _WQ_UserName As String
        Private _PS_Opinion As String
        Private _PS_UserName As String
        Private _ACC_Opinion As String
        Private _AddUser As String
        Private _AddDate As String
        Private _ModifyUser As String
        Private _ModifyDate As String
        Private _CostAcc_Opinion As String
        Private _PD_Opinion As String


        Sub New()
            _WQ_Code = Nothing
            _WQ_Dpt = Nothing
            _AP_UpDptName = Nothing
            _AP_ApplyDptName = Nothing
            _M_Code = Nothing
            _M_Name = Nothing
            _M_Gauge = Nothing
            _WO_Qty = 0
            _M_Unit = Nothing
            _WO_ID = Nothing
            _WQ_Description = Nothing
            _WQ_UserName = Nothing
            _PS_Opinion = Nothing
            _PS_UserName = Nothing
            _ACC_Opinion = Nothing
            _AddUser = Nothing
            _AddDate = Nothing
            _ModifyUser = Nothing
            _ModifyDate = Nothing
            _CostAcc_Opinion = Nothing
            _PD_Opinion = Nothing
        End Sub

        ''' <summary>
        ''' ���X�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WQ_Code() As String
            Get
                Return _WQ_Code
            End Get
            Set(ByVal value As String)
                _WQ_Code = value
            End Set
        End Property

        ''' <summary>
        ''' ���X�����s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WQ_Dpt() As String
            Get
                Return _WQ_Dpt
            End Get
            Set(ByVal value As String)
                _WQ_Dpt = value
            End Set
        End Property

        ''' <summary>
        ''' ���X�����W�ŦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_UpDptName() As String
            Get
                Return _AP_UpDptName
            End Get
            Set(ByVal value As String)
                _AP_UpDptName = value
            End Set
        End Property

        ''' <summary>
        ''' ���X�����W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ApplyDptName() As String
            Get
                Return _AP_ApplyDptName
            End Get
            Set(ByVal value As String)
                _AP_ApplyDptName = value
            End Set
        End Property

        ''' <summary>
        ''' ���ƽs�X
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Code() As String
            Get
                Return _M_Code
            End Get
            Set(ByVal value As String)
                _M_Code = value
            End Set
        End Property

        ''' <summary>
        ''' ���ƦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Name() As String
            Get
                Return _M_Name
            End Get
            Set(ByVal value As String)
                _M_Name = value
            End Set
        End Property

        ''' <summary>
        ''' ���ƳW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Gauge() As String
            Get
                Return _M_Gauge
            End Get
            Set(ByVal value As String)
                _M_Gauge = value
            End Set
        End Property

        ''' <summary>
        ''' �o�X�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WO_Qty() As Single
            Get
                Return _WO_Qty
            End Get
            Set(ByVal value As Single)
                _WO_Qty = value
            End Set
        End Property

        ''' <summary>
        ''' ���Ƴ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property M_Unit() As String
            Get
                Return _M_Unit
            End Get
            Set(ByVal value As String)
                _M_Unit = value
            End Set
        End Property

        ''' <summary>
        ''' ���Ƶo�X�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WO_ID() As String
            Get
                Return _WO_ID
            End Get
            Set(ByVal value As String)
                _WO_ID = value
            End Set
        End Property

        ''' <summary>
        ''' ���ƫ~����D�y�z
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WQ_Description() As String
            Get
                Return _WQ_Description
            End Get
            Set(ByVal value As String)
                _WQ_Description = value
            End Set
        End Property

        ''' <summary>
        ''' ���ƫ~����D���X�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WQ_UserName() As String
            Get
                Return _WQ_UserName
            End Get
            Set(ByVal value As String)
                _WQ_UserName = value
            End Set
        End Property

        ''' <summary>
        ''' ���ʳ��B�z�N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_Opinion() As String
            Get
                Return _PS_Opinion
            End Get
            Set(ByVal value As String)
                _PS_Opinion = value
            End Set
        End Property

        ''' <summary>
        ''' ���Ʊ��ʭ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_UserName() As String
            Get
                Return _PS_UserName
            End Get
            Set(ByVal value As String)
                _PS_UserName = value
            End Set
        End Property

        ''' <summary>
        ''' �f�p���B�z�N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ACC_Opinion() As String
            Get
                Return _ACC_Opinion
            End Get
            Set(ByVal value As String)
                _ACC_Opinion = value
            End Set
        End Property

        ''' <summary>
        ''' �O���K�[�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AddUser() As String
            Get
                Return _AddUser
            End Get
            Set(ByVal value As String)
                _AddUser = value
            End Set
        End Property

        ''' <summary>
        ''' �O���K�[�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AddDate() As String
            Get
                Return _AddDate
            End Get
            Set(ByVal value As String)
                _AddDate = value
            End Set
        End Property

        ''' <summary>
        ''' �O���ק�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ModifyUser() As String
            Get
                Return _ModifyUser
            End Get
            Set(ByVal value As String)
                _ModifyUser = value
            End Set
        End Property

        ''' <summary>
        ''' �O���ק�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ModifyDate() As String
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As String)
                _ModifyDate = value
            End Set
        End Property
        ''' <summary>
        ''' �����|�p�B�z�N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CostAcc_Opinion() As String
            Get
                Return _CostAcc_Opinion
            End Get
            Set(ByVal value As String)
                _CostAcc_Opinion = value
            End Set
        End Property
        ''' <summary>
        ''' �`�Ͳ����B�z�N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PD_Opinion() As String
            Get
                Return _PD_Opinion
            End Get
            Set(ByVal value As String)
                _PD_Opinion = value
            End Set
        End Property
    End Class
End Namespace

