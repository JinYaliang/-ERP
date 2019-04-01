Namespace LFERP.Library.ProductionSumPiecePersonnel


    Public Class ProductionSumPiecePersonnelInfo



        Private _PP_NO As String '            *  nvarchar(50)                /�p��渹
        Private _Per_NO As String '           *  nvarchar(50)                /���u�s��
        Private _G_NO As String '              *  nvarchar(50)                /�էO�s��
        Private _DepID As String '             *  nvarchar(50)                /�����s��
        Private _FacID As String '             *  nvarchar(50)                /�t�O

        Private _Pro_Type As String '          *  nvarchar(50)                /�u������
        Private _PM_M_Code As String '         *  nvarchar(50)                /���~�s��
        Private _PM_Type As String '          *  nvarchar(50)                /�t��W��

        Private _PS_NameS As String '          *  nvarchar(50)                /�p�u�ǦW��

        Private _PP_factor As Double '         *  float                       /�ӥ]�t��
        Private _PP_Qty As Integer '            *  int                         /�ƶq 
        Private _PP_Date As String '           *  datetime                    /�p����
        Private _PP_AddDate As String '        *  datetime                 /�O�����
        Private _PP_Action As String  '        *  nvarchar(50)                /�ާ@�H

        Private _PP_Remark As String '         *  nvarchar(MAX)               /�ƪ`

        '�~��r�q
        Private _PP_ActionName As String ' �ާ@���W (SystemUser)
        Private _PP_Per_Name As String '    ���u�W�m�W(ProductionPiecePersonnel)
        Private _PP_DepName As String '    �����W
        Private _PP_FacName As String '    �t�O�W
        Private _PP_G_Name As String '      �էO�W(ProductionPieceWorkGroup)    ���Ҥ���

        Private _PS_Name As String                    '    /�j�u�ǦW��  ProcessSub
        Private _PS_NO As String                      '    �j�u�ǽs��   ProductionPieceProcess

        Private _PP_AutoID As String ''�u�����y�{
        Private _PP_Price As Double  ''�u��

        Private _PP_DateEnd As String
        Private _PP_DateStart As String

        Private _Print_Action As String ''�ѥ��L�� ��
        Private _Per_Class As String '�Z��


        Sub New()
            _Print_Action = Nothing

            _PP_DateEnd = Nothing
            _PP_DateStart = Nothing


            _PP_AutoID = Nothing
            _PP_Price = 0

            _PP_NO = Nothing
            _Per_NO = Nothing
            _G_NO = Nothing
            _DepID = Nothing
            _FacID = Nothing

            _Pro_Type = Nothing
            _PM_M_Code = Nothing
            _PM_Type = Nothing
            _PS_Name = Nothing
            _PS_NameS = Nothing

            _PP_factor = Nothing
            _PP_Qty = Nothing
            _PP_Date = Nothing
            _PP_AddDate = Nothing
            _PP_Action = Nothing

            _PP_Remark = Nothing

            _PP_ActionName = Nothing
            _PP_Per_Name = Nothing
            _PP_DepName = Nothing
            _PP_FacName = Nothing
            _PP_G_Name = Nothing

            _PS_NO = Nothing

            _Per_Class = Nothing

        End Sub
        ''' <summary>
        ''' �Z��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Per_Class() As String
            Get
                Return _Per_Class
            End Get
            Set(ByVal value As String)
                _Per_Class = value
            End Set
        End Property
        ''' <summary>
        ''' ���L�ɥΪ��ާ@�H
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
        '_PP_DateEnd
        Public Property PP_DateEnd() As String
            Get
                Return _PP_DateEnd
            End Get
            Set(ByVal value As String)
                _PP_DateEnd = value
            End Set
        End Property

        Public Property PP_DateStart() As String
            Get
                Return _PP_DateStart
            End Get
            Set(ByVal value As String)
                _PP_DateStart = value
            End Set
        End Property

        ''' <summary>
        ''' �u���y�{�����s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_AutoID() As String
            Get
                Return _PP_AutoID
            End Get
            Set(ByVal value As String)
                _PP_AutoID = value
            End Set
        End Property
        ''' <summary>
        ''' �u�� 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_Price() As Double
            Get
                Return _PP_Price
            End Get
            Set(ByVal value As Double)
                _PP_Price = value
            End Set
        End Property
        ''' <summary>
        ''' �j�u�ǽs��
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
        ''' �p��渹
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_NO() As String
            Get
                Return _PP_NO
            End Get
            Set(ByVal value As String)
                _PP_NO = value
            End Set
        End Property
        ''' <summary>
        ''' ���u�s��
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
        ''' �էO�s��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property G_NO() As String
            Get
                Return _G_NO
            End Get
            Set(ByVal value As String)
                _G_NO = value
            End Set
        End Property
        ''' <summary>
        ''' ''�����s��
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
        ''' �t�O
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FacID() As String
            Get
                Return _FacID
            End Get
            Set(ByVal value As String)
                _FacID = value
            End Set
        End Property
        ''' <summary>
        ''' �u������
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Pro_Type() As String
            Get
                Return _Pro_Type
            End Get
            Set(ByVal value As String)
                _Pro_Type = value
            End Set
        End Property
        ''' <summary>
        ''' ���~�s��
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
        ''' �t��W��
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
        ''' �j�u�ǦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_Name() As String
            Get
                Return _PS_Name
            End Get
            Set(ByVal value As String)
                _PS_Name = value
            End Set
        End Property
        ''' <summary>
        ''' �p�u�ǦW��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PS_NameS() As String
            Get
                Return _PS_NameS
            End Get
            Set(ByVal value As String)
                _PS_NameS = value
            End Set
        End Property

        ''' <summary>
        ''' �ӥ]�t��
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_factor() As Double
            Get
                Return _PP_factor
            End Get
            Set(ByVal value As Double)
                _PP_factor = value
            End Set
        End Property
        ''' <summary>
        ''' �ƶq
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_Qty() As Integer
            Get
                Return _PP_Qty
            End Get
            Set(ByVal value As Integer)
                _PP_Qty = value
            End Set
        End Property
        ''' <summary>
        ''' �p����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_Date() As String
            Get
                Return _PP_Date
            End Get
            Set(ByVal value As String)
                _PP_Date = value
            End Set
        End Property
        ''' <summary>
        ''' �O�����
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_AddDate() As String
            Get
                Return _PP_AddDate
            End Get
            Set(ByVal value As String)
                _PP_AddDate = value
            End Set
        End Property
        ''' <summary>
        ''' �ާ@�H
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_Action() As String
            Get
                Return _PP_Action
            End Get
            Set(ByVal value As String)
                _PP_Action = value
            End Set
        End Property

        'Private _PP_ActionName As String ' 
        'Private _PP_Per_Name As String '   
        'Private _PP_DepName As String '    
        'Private _PP_FacName As String '    
        'Private _PP_G_Name As String '     
        ''' <summary>
        ''' �ƪ`
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_Remark() As String
            Get
                Return _PP_Remark
            End Get
            Set(ByVal value As String)
                _PP_Remark = value
            End Set
        End Property
        ''�~��r�q
        ''' <summary>
        ''' �ާ@���W (SystemUser)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_ActionName() As String
            Get
                Return _PP_ActionName
            End Get
            Set(ByVal value As String)
                _PP_ActionName = value
            End Set
        End Property
        ''' <summary>
        '''  ���u�W�m�W(ProductionPiecePersonnel)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Property PP_Per_Name() As String
            Get
                Return _PP_Per_Name
            End Get
            Set(ByVal value As String)
                _PP_Per_Name = value
            End Set
        End Property
        ''' <summary>
        ''' �����W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>

        Public Property PP_DepName() As String
            Get
                Return _PP_DepName
            End Get
            Set(ByVal value As String)
                _PP_DepName = value
            End Set
        End Property
        ''' <summary>
        ''' �t�O�W
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_FacName() As String
            Get
                Return _PP_FacName
            End Get
            Set(ByVal value As String)
                _PP_FacName = value
            End Set
        End Property
        ''' <summary>
        '''  �էO�W(ProductionPieceWorkGroup)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PP_G_Name() As String
            Get
                Return _PP_G_Name
            End Get
            Set(ByVal value As String)
                _PP_G_Name = value
            End Set
        End Property

    End Class

End Namespace