Namespace LFERP.Library.ProductionOWPAcceptance


    Public Class ProductionOWPAcceptanceInfo

        Private _A_AcceptanceNO As String ' *  nvarchar(50)              /�禬�s��
        Private _A_NO As String '              *  nvarchar(50)              /�禬�y����
        Private _O_NO As String '              *  nvarchar(50)              /�~�o�渹
        Private _ASend_NO As String '          *  nvarchar(50)              /�e�f�渹
        Private _DPT_ID As String '            *  nvarchar(50)              /�~�o����ID ���B�T�w���~�o��
        Private _PM_M_Code As String '         *  nvarchar(50)              /���~�W��
        Private _PM_Type As String '           *  nvarchar(50)              /����
        Private _PS_NO As String '             *  nvarchar(50)              /�~�o�u��
        Private _S_Supplier As String '        *  nvarchar(5)               /�ѳf�ӽs��
        Private _A_OK_Qty As Double '           *  real                  /�禬ok�ƶq
        Private _A_QQ_Qty As Double '         *  real                  /�禬�eQ�ƶq
        Private _A_TC_Qty As Double '         *  real                  /�禬�h���ƶq'
        Private _A_QT_Qty As Double '         *  real                  /�禬�䥦�ƶq
        Private _A_Ver As Integer '          *  real                  /�O���ާ@����
        Private _A_Detail As String '          *  nvarchar(50)              /�ާ@����(�Ȧ�/�禬)
        Private _A_Action As String '          *  nvarchar(50)              /�Ȧ��H�s��
        Private _A_SendDate As String '        *  datetime                  /�O���K�[�ɶ�
        Private _A_Remark As String '          *  nvarchar(max)             /�ާ@�ƪ`
        Private _A_Check As Boolean '           *  bit                 /�f�֧_
        Private _A_CheckAction As String '     *  nvarchar(50)        /�f�֤HID
        Private _A_CheckDate As String '      *  datetime            /�f�֮ɶ�
        Private _A_CheckRemark As String '     *  nvarchar(max)       /�f�ֳƪ`
        Private _A_AccCheck As Boolean '        *  bit                      /�_�f�֧_
        Private _A_AccCheckAction As String ' *  nvarchar(50)             /�_�f�֤HID
        Private _A_AccCheckDate As String '    *  datetime                 /�_�f�֮ɶ�
        Private _A_ACCCheckRemark As String '  *  nvarchar(max)            /�_�f�ֳƪ`
        Private _A_AccCheckType As String '    *  nvarchar(30)             /�_�f�ֳƪ`����(�L��,�T�{�L�~,�ݴ_)
        Private _A_UpdateDate As String '      *  datetime                 /�ק���
        Private _PS_NO_Next As String '���Ƥu�ǡA�Y�U�@�u��

        ''�䥦�����ƾ�
        Private _A_ActionName As String
        Private _A_CheckActionName As String
        Private _A_AccCheckActionName As String
        Private _A_PS_Name As String
        Private _A_SupplierName As String
        Private _A_PS_Name_Next As String

        Private _OPAutoID As String ''�~�o�椤���۰ʽs��ID
        Private _PO_NoSendQty As Int32  ''�~�o�椤�����

        ''�[Ū���U�u��
        Private _Pro_NO As String
        Private _PS_Num As String    ''�u�ǧǸ�
        Private _PS_Enable As String '�O�_�ҥΥ�

        Private _A_OW_Do As String '�[�u�n�D
        Private _PM_JiYu As String



        Sub New()

            _PM_JiYu = Nothing
            _A_AcceptanceNO = Nothing
            _A_NO = Nothing '              *  nvarchar(50)              /�禬�s��
            _O_NO = Nothing '              *  nvarchar(50)              /�~�o�渹
            _ASend_NO = Nothing '          *  nvarchar(50)              /�e�f�渹
            _DPT_ID = Nothing '            *  nvarchar(50)              /�~�o����ID ���B�T�w���~�o��
            _PM_M_Code = Nothing '         *  nvarchar(50)              /���~�W��
            _PM_Type = Nothing '           *  nvarchar(50)              /����
            _PS_NO = Nothing '             *  nvarchar(50)              /�~�o�u��
            _S_Supplier = Nothing '        *  nvarchar(5)               /�ѳf�ӽs��
            _A_OK_Qty = 0 '           *  real                  /�禬ok�ƶq
            _A_QQ_Qty = 0 '         *  real                  /�禬�eQ�ƶq
            _A_TC_Qty = 0 '         *  real                  /�禬�h���ƶq'
            _A_QT_Qty = 0 '         *  real                  /�禬�䥦�ƶq
            _A_Ver = 0 '          *  real                  /�O���ާ@����
            _A_Detail = Nothing '          *  nvarchar(50)              /�ާ@����(�Ȧ�/�禬)
            _A_Action = Nothing '          *  nvarchar(50)              /�Ȧ��H�s��
            _A_SendDate = Nothing '        *  datetime                  /�O���K�[�ɶ�
            _A_Remark = Nothing '          *  nvarchar(max)             /�ާ@�ƪ`
            _A_Check = False '           *  bit                 /�f�֧_
            _A_CheckAction = Nothing '     *  nvarchar(50)        /�f�֤HID
            _A_CheckDate = Nothing '      *  datetime            /�f�֮ɶ�
            _A_CheckRemark = Nothing '     *  nvarchar(max)       /�f�ֳƪ`
            _A_AccCheck = False  '        *  bit                      /�_�f�֧_
            _A_AccCheckAction = Nothing ' *  nvarchar(50)             /�_�f�֤HID
            _A_AccCheckDate = Nothing '    *  datetime                 /�_�f�֮ɶ�
            _A_ACCCheckRemark = Nothing '  *  nvarchar(max)            /�_�f�ֳƪ`
            _A_AccCheckType = Nothing '    *  nvarchar(30)             /�_�f�ֳƪ`����(�L��,�T�{�L�~,�ݴ_)
            _A_UpdateDate = Nothing '      *  datetime                 /�ק���
            _PS_NO_Next = Nothing

            ''�䥦�����ƾ�
            _A_ActionName = Nothing
            _A_CheckActionName = Nothing
            _A_AccCheckActionName = Nothing
            _A_PS_Name = Nothing
            _A_SupplierName = Nothing
            _A_PS_Name_Next = Nothing

            _OPAutoID = Nothing ''�~�o�椤���۰ʽs��ID
            _PO_NoSendQty = 0

            _A_OW_Do = Nothing '�[�u�n�D
        End Sub

        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_JiYu() As String
            Get
                Return _PM_JiYu
            End Get
            Set(ByVal value As String)
                _PM_JiYu = value
            End Set
        End Property
        ''' <summary>
        ''' �[�u�n�D
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_OW_Do() As String
            Get
                Return _A_OW_Do
            End Get
            Set(ByVal value As String)
                _A_OW_Do = value
            End Set
        End Property

        ''' <summary>
        ''' �禬�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AcceptanceNO() As String
            Get
                Return _A_AcceptanceNO
            End Get
            Set(ByVal value As String)
                _A_AcceptanceNO = value
            End Set
        End Property
        ''' <summary>
        ''' �禬�y����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_NO() As String
            Get
                Return _A_NO
            End Get
            Set(ByVal value As String)
                _A_NO = value
            End Set
        End Property
        ''' <summary>
        ''' �~�o�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property O_NO() As String
            Get
                Return _O_NO
            End Get
            Set(ByVal value As String)
                _O_NO = value
            End Set
        End Property
        ''' <summary>
        ''' �e�f�渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ASend_NO() As String
            Get
                Return _ASend_NO
            End Get
            Set(ByVal value As String)
                _ASend_NO = value
            End Set
        End Property
        ''' <summary>
        ''' �o����ID ���B�T�w���~�o��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DPT_ID() As String
            Get
                Return _DPT_ID
            End Get
            Set(ByVal value As String)
                _DPT_ID = value
            End Set
        End Property
        ''' <summary>
        ''' ���~�W��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_M_Code() As String
            Get
                Return _PM_M_Code
            End Get
            Set(ByVal value As String)
                _PM_M_Code = value
            End Set
        End Property
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PM_Type() As String
            Get
                Return _PM_Type
            End Get
            Set(ByVal value As String)
                _PM_Type = value
            End Set
        End Property
        ''' <summary>
        ''' �~�o�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_NO() As String
            Get
                Return _PS_NO
            End Get
            Set(ByVal value As String)
                _PS_NO = value
            End Set
        End Property
        ''' <summary>
        ''' �ѳf�ӽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property S_Supplier() As String
            Get
                Return _S_Supplier
            End Get
            Set(ByVal value As String)
                _S_Supplier = value
            End Set
        End Property
        ''' <summary>
        ''' �禬ok�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_OK_Qty() As Double
            Get
                Return _A_OK_Qty
            End Get
            Set(ByVal value As Double)
                _A_OK_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' /�禬�eQ�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_QQ_Qty() As Double
            Get
                Return _A_QQ_Qty
            End Get
            Set(ByVal value As Double)
                _A_QQ_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' �禬�h���ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_TC_Qty() As Double
            Get
                Return _A_TC_Qty
            End Get
            Set(ByVal value As Double)
                _A_TC_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' �禬�䥦�ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_QT_Qty() As Double
            Get
                Return _A_QT_Qty
            End Get
            Set(ByVal value As Double)
                _A_QT_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' �O���ާ@����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Ver() As Double
            Get
                Return _A_Ver
            End Get
            Set(ByVal value As Double)
                _A_Ver = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@����(�Ȧ�/�禬)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Detail() As String
            Get
                Return _A_Detail
            End Get
            Set(ByVal value As String)
                _A_Detail = value
            End Set
        End Property
        ''' <summary>
        ''' �Ȧ��H�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Action() As String
            Get
                Return _A_Action
            End Get
            Set(ByVal value As String)
                _A_Action = value
            End Set
        End Property
        ''' <summary>
        ''' �O���K�[�ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_SendDate() As String
            Get
                Return _A_SendDate
            End Get
            Set(ByVal value As String)
                _A_SendDate = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@�ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Remark() As String
            Get
                Return _A_Remark
            End Get
            Set(ByVal value As String)
                _A_Remark = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֧_ (�禬)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_Check() As Boolean
            Get
                Return _A_Check
            End Get
            Set(ByVal value As Boolean)
                _A_Check = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֤HID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_CheckAction() As String
            Get
                Return _A_CheckAction
            End Get
            Set(ByVal value As String)
                _A_CheckAction = value
            End Set
        End Property
        ''' <summary>
        ''' �f�֮ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_CheckDate() As String
            Get
                Return _A_CheckDate
            End Get
            Set(ByVal value As String)
                _A_CheckDate = value
            End Set
        End Property
        ''' <summary>
        ''' �f�ֳƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_CheckRemark() As String
            Get
                Return _A_CheckRemark
            End Get
            Set(ByVal value As String)
                _A_CheckRemark = value
            End Set
        End Property
        ''' <summary>
        ''' �_�f�֧_
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccCheck() As Boolean
            Get
                Return _A_AccCheck
            End Get
            Set(ByVal value As Boolean)
                _A_AccCheck = value
            End Set
        End Property
        ''' <summary>
        ''' �_�f�֤HID
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccCheckAction() As String
            Get
                Return _A_AccCheckAction
            End Get
            Set(ByVal value As String)
                _A_AccCheckAction = value
            End Set
        End Property
        ''' <summary>
        ''' �_�f�֮ɶ�
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccCheckDate() As String
            Get
                Return _A_AccCheckDate
            End Get
            Set(ByVal value As String)
                _A_AccCheckDate = value
            End Set
        End Property
        ''' <summary>
        ''' �_�f�ֳƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_ACCCheckRemark() As String
            Get
                Return _A_ACCCheckRemark
            End Get
            Set(ByVal value As String)
                _A_ACCCheckRemark = value
            End Set
        End Property

        ''' <summary>
        ''' �_�f�ֳƪ`����(�L��,�T�{�L�~,�ݴ_)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_AccCheckType() As String
            Get
                Return _A_AccCheckType
            End Get
            Set(ByVal value As String)
                _A_AccCheckType = value
            End Set
        End Property
        ''' <summary>
        ''' �ק���
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_UpdateDate() As String
            Get
                Return _A_UpdateDate
            End Get
            Set(ByVal value As String)
                _A_UpdateDate = value
            End Set
        End Property


        ''�䥦�����ƾ�
        '_A_ActionName = Nothing
        '_A_CheckActionName = Nothing
        '_A_AccCheckActionName = Nothing
        '_A_PS_Name = Nothing
        '_A_S_SupplierName = Nothing
        Public Property A_ActionName() As String
            Get
                Return _A_ActionName
            End Get
            Set(ByVal value As String)
                _A_ActionName = value
            End Set
        End Property

        Public Property A_CheckActionName() As String
            Get
                Return _A_CheckActionName
            End Get
            Set(ByVal value As String)
                _A_CheckActionName = value
            End Set
        End Property

        Public Property A_AccCheckActionName() As String
            Get
                Return _A_AccCheckActionName
            End Get
            Set(ByVal value As String)
                _A_AccCheckActionName = value
            End Set
        End Property

        Public Property A_PS_Name() As String
            Get
                Return _A_PS_Name
            End Get
            Set(ByVal value As String)
                _A_PS_Name = value
            End Set
        End Property

        Public Property A_SupplierName() As String
            Get
                Return _A_SupplierName
            End Get
            Set(ByVal value As String)
                _A_SupplierName = value
            End Set
        End Property
        ''' <summary>
        ''' �~�o�椤�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property OPAutoID() As String
            Get
                Return _OPAutoID
            End Get
            Set(ByVal value As String)
                _OPAutoID = value
            End Set
        End Property '


        ''' <summary>
        ''' �~�o�椤�����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PO_NoSendQty() As Int32
            Get
                Return _PO_NoSendQty
            End Get
            Set(ByVal value As Int32)
                _PO_NoSendQty = value
            End Set
        End Property '


        ''' <summary>
        ''' �u�ǽs��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Pro_NO() As String
            Get
                Return _Pro_NO
            End Get
            Set(ByVal value As String)
                _Pro_NO = value
            End Set
        End Property '
        ''' <summary>
        ''' '�[Ū���U�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_Num() As String
            Get
                Return _PS_Num
            End Get
            Set(ByVal value As String)
                _PS_Num = value
            End Set
        End Property
        ''' <summary>
        ''' �O�_�_��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_Enable() As String
            Get
                Return _PS_Enable
            End Get
            Set(ByVal value As String)
                _PS_Enable = value
            End Set
        End Property


        ''' <summary>
        ''' �禬�ɪ��U�@�u�ǦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property A_PS_Name_Next() As String
            Get
                Return _A_PS_Name_Next
            End Get
            Set(ByVal value As String)
                _A_PS_Name_Next = value
            End Set
        End Property

        ''' <summary>
        ''' �U�@�u�ǽs�� �U�@�u��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_NO_Next() As String
            Get
                Return _PS_NO_Next
            End Get
            Set(ByVal value As String)
                _PS_NO_Next = value
            End Set
        End Property
    End Class
End Namespace