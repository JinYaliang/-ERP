Namespace LFERP.Library.ProductionField
    Public Class ProductionFieldBadnessCauseInfo
        Private _PB_ID As String        '���}��]�s��
        Private _Pro_Type As String        '�u������
        Private _PM_M_Code As String    '���~�s��
        Private _PM_Type As String        '�t��W��
        Private _Pro_NO As String      '�u�ǽs��

        Private _PS_Name As String      '�u�ǦW��
        Private _PB_Cause As String   '���}��]
        Private _PB_Explain As String  '����
        Private _PB_AddUserID As String     '�K�[�H
        Private _PB_AddDate As Date         '�K�[�ɶ�


        Sub New()
            _PB_ID = Nothing
            _Pro_Type = Nothing
            _PM_M_Code = Nothing
            _PM_Type = Nothing
            _Pro_NO = Nothing

            _PS_Name = Nothing
            _PB_Cause = Nothing
            _PB_Explain = Nothing
            _PB_AddUserID = Nothing
            _PB_AddDate = Nothing
        End Sub

        Public Property PB_ID() As String
            Get
                Return _PB_ID
            End Get
            Set(ByVal value As String)
                _PB_ID = value
            End Set
        End Property

        Public Property Pro_Type() As String
            Get
                Return _Pro_Type
            End Get
            Set(ByVal value As String)
                _Pro_Type = value
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

        Public Property Pro_NO() As String
            Get
                Return _Pro_NO
            End Get
            Set(ByVal value As String)
                _Pro_NO = value
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

        Public Property PB_Cause() As String
            Get
                Return _PB_Cause
            End Get
            Set(ByVal value As String)
                _PB_Cause = value
            End Set
        End Property

        Public Property PB_Explain() As String
            Get
                Return _PB_Explain
            End Get
            Set(ByVal value As String)
                _PB_Explain = value
            End Set
        End Property

        Public Property PB_AddUserID() As String
            Get
                Return _PB_AddUserID
            End Get
            Set(ByVal value As String)
                _PB_AddUserID = value
            End Set
        End Property

        Public Property PB_AddDate() As String
            Get
                Return _PB_AddDate
            End Get
            Set(ByVal value As String)
                _PB_AddDate = value
            End Set
        End Property
    End Class

End Namespace

