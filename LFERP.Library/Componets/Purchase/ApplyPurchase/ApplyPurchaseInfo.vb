
Namespace LFERP.Library.Purchase.ApplyPurchase

    Public Class ApplyPurchaseInfo
        Private _ap_id As String
        Private _ap_num As String
        Private _ap_applyid As String
        Private _ap_m_code As String
        Private _ap_m_gauge As String
        Private _ap_m_unit As String
        Private _ap_m_name As String
        Private _ap_qty As Single
        Private _ap_p_qty As Single
        Private _ap_applyreason As String
        Private _ap_applydate As String
        Private _ap_applydptid As String
        Private _ap_applydptname As String
        Private _ap_applyperson As String
        Private _ap_applypersonname As String
        Private _ap_action As String

        Private _ap_checkware As Boolean
        Private _ap_check As Boolean
        Private _ap_checkdate As String
        Private _ap_checkaction As String
        Private _ap_checktype As String
        Private _ap_checkremark As String

        Private _ap_recheck As Boolean
        Private _ap_recheckdate As String
        Private _ap_recheckaction As String
        Private _ap_rechecktype As String
        Private _ap_recheckremark As String

        Sub New()
            _ap_id = ""
            _ap_num = ""
            _ap_applyid = ""
            _ap_m_code = ""
            _ap_m_gauge = ""
            _ap_m_unit = ""
            _ap_m_name = ""
            _ap_qty = 0
            ' _ap_p_qty = 0
            _ap_applyreason = ""
            _ap_applydate = Nothing
            _ap_applydptid = ""
            _ap_applydptname = ""
            _ap_applyperson = ""
            _ap_applypersonname = ""
            _ap_action = ""
            _ap_checkware = False
            _ap_check = False
            _ap_checkdate = Nothing
            _ap_checkaction = ""
            _ap_checktype = ""
            _ap_checkremark = ""
            _ap_recheck = False
            _ap_recheckdate = Nothing
            _ap_recheckaction = ""
            _ap_rechecktype = ""
            _ap_recheckremark = ""

        End Sub
        ''' <summary>
        ''' ���ʳ渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ID() As String
            Get
                Return _ap_id
            End Get
            Set(ByVal value As String)
                _ap_id = value
            End Set
        End Property
        ''' <summary>
        ''' ���ʽs���]�O���ߤ@�^
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_Num() As String
            Get
                Return _ap_num
            End Get
            Set(ByVal value As String)
                _ap_num = value
            End Set
        End Property
        ''' <summary>
        ''' �ӻ�渹�]�{�ɤ�u��J�^
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ApplyID() As String
            Get
                Return _ap_applyid
            End Get
            Set(ByVal value As String)
                _ap_applyid = value
            End Set
        End Property
        ''' <summary>
        ''' ���ƽs�X
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_M_Code() As String
            Get
                Return _ap_m_code
            End Get
            Set(ByVal value As String)
                _ap_m_code = value
            End Set
        End Property
        ''' <summary>
        ''' ���ƳW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_M_Gauge() As String
            Get
                Return _ap_m_gauge
            End Get
            Set(ByVal value As String)
                _ap_m_gauge = value
            End Set
        End Property
        ''' <summary>
        ''' ���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_M_Unit() As String
            Get
                Return _ap_m_unit
            End Get
            Set(ByVal value As String)
                _ap_m_unit = value
            End Set
        End Property
        ''' <summary>
        ''' ���ƦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_M_Name() As String
            Get
                Return _ap_m_name
            End Get
            Set(ByVal value As String)
                _ap_m_name = value
            End Set
        End Property
        ''' <summary>
        ''' �ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_Qty() As Single
            Get
                Return _ap_qty
            End Get
            Set(ByVal value As Single)
                _ap_qty = value
            End Set
        End Property

        ''' <summary>
        ''' �ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_P_Qty() As Single
            Get
                Return _ap_p_qty
            End Get
            Set(ByVal value As Single)
                _ap_p_qty = value
            End Set
        End Property

        ''' <summary>
        ''' ���ʭ�]
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_Applyreason() As String
            Get
                Return _ap_applyreason
            End Get
            Set(ByVal value As String)
                _ap_applyreason = value
            End Set
        End Property
        ''' <summary>
        ''' ���ʤ��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ApplyDate() As String
            Get
                Return _ap_applydate
            End Get
            Set(ByVal value As String)
                _ap_applydate = value
            End Set
        End Property
        ''' <summary>
        ''' ���ʳ����N��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ApplyDptID() As String
            Get
                Return _ap_applydptid
            End Get
            Set(ByVal value As String)
                _ap_applydptid = value
            End Set
        End Property

        ''' <summary>
        ''' ���ʳ����W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ApplyDptName() As String
            Get
                Return _ap_applydptname
            End Get
            Set(ByVal value As String)
                _ap_applydptname = value
            End Set
        End Property

        ''' <summary>
        ''' ���ʤH�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ApplyPerson() As String
            Get
                Return _ap_applyperson
            End Get
            Set(ByVal value As String)
                _ap_applyperson = value
            End Set
        End Property
        ''' <summary>
        ''' ���ʤH�m�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ApplyPersonName() As String
            Get
                Return _ap_applypersonname
            End Get
            Set(ByVal value As String)
                _ap_applypersonname = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_Action() As String
            Get
                Return _ap_action
            End Get
            Set(ByVal value As String)
                _ap_action = value
            End Set
        End Property

        ''' <summary>
        ''' ���Ƽf��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_CheckWare() As Boolean
            Get
                Return _ap_checkware
            End Get
            Set(ByVal value As Boolean)
                _ap_checkware = value
            End Set
        End Property

        ''' <summary>
        ''' �f��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_Check() As Boolean
            Get
                Return _ap_check
            End Get
            Set(ByVal value As Boolean)
                _ap_check = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_CheckDate() As String
            Get
                Return _ap_checkdate
            End Get
            Set(ByVal value As String)
                _ap_checkdate = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_CheckAction() As String
            Get
                Return _ap_checkaction
            End Get
            Set(ByVal value As String)
                _ap_checkaction = value
            End Set
        End Property
        ''' <summary>
        ''' �f������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_CheckType() As String
            Get
                Return _ap_checktype
            End Get
            Set(ByVal value As String)
                _ap_checktype = value
            End Set
        End Property
        ''' <summary>
        ''' �f�ֳƵ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_CheckRemark() As String
            Get
                Return _ap_checkremark
            End Get
            Set(ByVal value As String)
                _ap_checkremark = value
            End Set
        End Property


        ''' <summary>
        ''' �Ʈ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ReCheck() As Boolean
            Get
                Return _ap_recheck
            End Get
            Set(ByVal value As Boolean)
                _ap_recheck = value

            End Set
        End Property
        ''' <summary>
        ''' �Ʈ֤��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ReCheckDate() As String
            Get
                Return _ap_recheckdate
            End Get
            Set(ByVal value As String)
                _ap_recheckdate = value
            End Set
        End Property
        ''' <summary>
        ''' �Ʈ֤H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ReCheckAction() As String
            Get
                Return _ap_recheckaction
            End Get
            Set(ByVal value As String)
                _ap_recheckaction = value
            End Set
        End Property
        ''' <summary>
        ''' �Ʈ�����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ReCheckType() As String
            Get
                Return _ap_rechecktype
            End Get
            Set(ByVal value As String)
                _ap_rechecktype = value
            End Set
        End Property
        ''' <summary>
        ''' �ƮֳƵ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AP_ReCheckRemark() As String
            Get
                Return _ap_recheckremark
            End Get
            Set(ByVal value As String)
                _ap_recheckremark = value
            End Set
        End Property
     
    End Class

End Namespace

