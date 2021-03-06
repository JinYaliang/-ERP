Namespace LFERP.DataSetting
    ''' <summary>
    ''' 客戶信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CusterInfo
        Private _c_custerid As String    '客戶代號
        Private _c_engname As String '英文名
        Private _c_chsname As String '中文名
        Private _c_adddate As Date '建交日期
        Private _c_link As String '聯系人
        Private _c_linktel As String '聯系電話
        Private _c_Fax As String 'Fax_no
        Private _c_adder1 As String '客戶地址1
        Private _c_adder2 As String '客戶地址2
        Private _c_adder3 As String '客戶地址3
        Private _c_adder4 As String '客戶地址4	
        Private _C_Department As String '部門
        Private _C_Email As String

        '劉祥松 2014.05.13      客戶類別
        Private _C_CustomerType As String
        Public Sub New()
            _c_custerid = Nothing
            _c_engname = Nothing
            _c_chsname = Nothing
            _c_adddate = Nothing
            _c_link = Nothing
            _c_linktel = Nothing
            _c_Fax = Nothing
            _c_adder1 = Nothing
            _c_adder2 = Nothing
            _c_adder3 = Nothing
            _c_adder4 = Nothing
            _C_Department = Nothing
            _C_Email = Nothing

            _C_CustomerType = Nothing
        End Sub

        ''' <summary>
        ''' 邮箱地址
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Email() As String
            Get
                Return _C_Email
            End Get
            Set(ByVal value As String)
                _C_Email = value
            End Set
        End Property
        ''' <summary>
        ''' 客戶代號
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_CusterID() As String
            Get
                Return _c_custerid
            End Get
            Set(ByVal value As String)
                _c_custerid = value
            End Set
        End Property
        ''' <summary>
        ''' 英文名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_EngName() As String
            Get
                Return _c_engname
            End Get
            Set(ByVal value As String)
                _c_engname = value
            End Set
        End Property
        ''' <summary>
        ''' 中文名
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_ChsName() As String
            Get
                Return _c_chsname
            End Get
            Set(ByVal value As String)
                _c_chsname = value
            End Set
        End Property
        ''' <summary>
        ''' 建交日期
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_AddDate() As Date
            Get
                Return _c_adddate
            End Get
            Set(ByVal value As Date)
                _c_adddate = value
            End Set
        End Property
        ''' <summary>
        ''' 聯系人
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Link() As String
            Get
                Return _c_link
            End Get
            Set(ByVal value As String)
                _c_link = value
            End Set
        End Property
        ''' <summary>
        ''' 聯系人電話
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_LinkTel() As String
            Get
                Return _c_linktel
            End Get
            Set(ByVal value As String)
                _c_linktel = value
            End Set
        End Property
        ''' <summary>
        ''' 傳真
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Fax() As String
            Get
                Return _c_Fax
            End Get
            Set(ByVal value As String)
                _c_Fax = value
            End Set
        End Property
        ''' <summary>
        ''' 地址1
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Adder1() As String
            Get
                Return _c_adder1
            End Get
            Set(ByVal value As String)
                _c_adder1 = value
            End Set
        End Property
        ''' <summary>
        ''' 地址2
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Adder2() As String
            Get
                Return _c_adder2
            End Get
            Set(ByVal value As String)
                _c_adder2 = value
            End Set
        End Property
        ''' <summary>
        ''' 地址3
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Adder3() As String
            Get
                Return _c_adder3
            End Get
            Set(ByVal value As String)
                _c_adder3 = value
            End Set
        End Property
        ''' <summary>
        ''' 地址4
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property C_Adder4() As String
            Get
                Return _c_adder4
            End Get
            Set(ByVal value As String)
                _c_adder4 = value
            End Set
        End Property
        Public Property C_Department() As String
            Get
                Return _C_Department
            End Get
            Set(ByVal value As String)
                _C_Department = value
            End Set
        End Property

        Public Property CustomerType() As String
            Get
                Return _C_CustomerType
            End Get
            Set(ByVal value As String)
                _C_CustomerType = value
            End Set
        End Property
    End Class
End Namespace

