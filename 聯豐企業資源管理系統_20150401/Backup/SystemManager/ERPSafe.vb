Public Class ERPSafe
    Private _materialtype As String
    Private _suppliertype As String
    Private _warehouselist As String

    Private _DepID As String
    Private _FacID As String

    Public Sub New()
        _materialtype = Nothing
        _suppliertype = Nothing
        _warehouselist = Nothing

        _DepID = ""
        _FacID = Nothing
    End Sub

    ''' <summary>
    ''' �R�\�d�ݪ��������O
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaterialType() As String
        Get
            Return _materialtype
        End Get
        Set(ByVal value As String)
            _materialtype = value
        End Set

    End Property
    ''' <summary>
    ''' �i�d�ݨ�����������,���},�p ���ʳ�,�]�˳�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierType() As String
        Get
            Return _suppliertype
        End Get
        Set(ByVal value As String)
            _suppliertype = value
        End Set

    End Property
    ''' <summary>
    ''' ���\�d�ݪ��ܮw�C��,�榡�p�G'�ܮw1','�ܮw2'
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WareHouseList() As String
        Get
            Return _warehouselist
        End Get
        Set(ByVal value As String)
            _warehouselist = value
        End Set

    End Property


    ''' <summary>
    ''' ���\�d�ݪ�����
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
    ''' ���\�d�ݪ��t�O
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

End Class
