Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Web

Namespace Models
	Public Class BatchEditRepository
		Public Shared ReadOnly Property GridData() As List(Of GridDataItem)
			Get
				Dim key = "34FAA431-CF79-4869-9488-93F6AAE81263"
				Dim Session = HttpContext.Current.Session
				If Session(key) Is Nothing Then
					Session(key) = Enumerable.Range(0, 3).Select(Function(i) New GridDataItem With {.ID = i, .Mon = i * 10 Mod 3, .Tue = i * 5 Mod 3, .Wen = i Mod 2}).ToList()
				End If
				Return CType(Session(key), List(Of GridDataItem))
			End Get
		End Property
		Public Shared Function InsertNewItem(ByVal postedItem As GridDataItem) As GridDataItem
			Dim newItem = New GridDataItem() With {.ID = GridData.Count}
			LoadNewValues(newItem, postedItem)
			GridData.Add(newItem)
			Return newItem
		End Function
		Public Shared Function UpdateItem(ByVal postedItem As GridDataItem) As GridDataItem
			Dim editedItem = GridData.First(Function(i) i.ID = postedItem.ID)
			LoadNewValues(editedItem, postedItem)
			Return editedItem
		End Function
		Public Shared Function DeleteItem(ByVal itemKey As Integer) As GridDataItem
			Dim item = GridData.First(Function(i) i.ID = itemKey)
			GridData.Remove(item)
			Return item
		End Function
		Protected Shared Sub LoadNewValues(ByVal newItem As GridDataItem, ByVal postedItem As GridDataItem)
			newItem.Mon = postedItem.Mon
			newItem.Tue = postedItem.Tue
			newItem.Wen = postedItem.Wen
		End Sub
	End Class

	Public Class GridDataItem
		Private privateID As Integer
		Public Property ID() As Integer
			Get
				Return privateID
			End Get
			Set(ByVal value As Integer)
				privateID = value
			End Set
		End Property
		Private privateMon As Integer
		Public Property Mon() As Integer
			Get
				Return privateMon
			End Get
			Set(ByVal value As Integer)
				privateMon = value
			End Set
		End Property
		Private privateTue As Integer
		Public Property Tue() As Integer
			Get
				Return privateTue
			End Get
			Set(ByVal value As Integer)
				privateTue = value
			End Set
		End Property
		Private privateWen As Integer
		Public Property Wen() As Integer
			Get
				Return privateWen
			End Get
			Set(ByVal value As Integer)
				privateWen = value
			End Set
		End Property
	End Class
End Namespace