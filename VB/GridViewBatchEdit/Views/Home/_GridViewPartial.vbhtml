@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)
                                              settings.Name = "GridView"
                                              settings.Width = 600
                                              settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "GridViewPartial"}
                                              settings.SettingsEditing.BatchUpdateRouteValues = New With {.Controller = "Home", .Action = "BatchUpdatePartial"}
                                              settings.SettingsEditing.Mode = GridViewEditingMode.Batch

                                              settings.ClientSideEvents.BatchEditEndEditing = "OnBatchEditEndEditing"
                                              settings.ClientSideEvents.BatchEditStartEditing = "OnBatchEditStartEditing"
                                              settings.ClientSideEvents.BatchEditChangesCanceling = "OnBatchEditChangesCanceling"
                                              settings.ClientSideEvents.BatchEditRowDeleting = "OnBatchEditRowDeleting"

                                              settings.KeyFieldName = "ID"

                                              settings.CommandColumn.Visible = True
                                              settings.CommandColumn.ShowNewButtonInHeader = True
                                              settings.CommandColumn.ShowDeleteButton = True

                                              Dim days() As String = {"Mon", "Tue", "Wen"}
                                              For Each dayName In days
                                                  settings.Columns.Add(Sub(column)
                                                                           column.FieldName = dayName
                                                                           column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                           Dim summaryItem As New ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Sum)
                                                                           summaryItem.Tag = column.FieldName & "_Sum"
                                                                           summaryItem.DisplayFormat = "{0}"
                                                                           settings.TotalSummary.Add(summaryItem)
                                                                           column.SetFooterTemplateContent(Sub(c)

                                                                                                               Html.DevExpress().Label(Sub(lbSettings)

                                                                                                                                           Dim fieldName As String = (TryCast(c.Column, GridViewDataColumn)).FieldName
                                                                                                                                           lbSettings.Name = "label" + fieldName
                                                                                                                                           lbSettings.Properties.EnableClientSideAPI = True
                                                                                                                                           Dim summaryItem1 As ASPxSummaryItem = c.Grid.TotalSummary.First(Function(i) i.Tag = (fieldName & "_Sum"))
                                                                                                                                           lbSettings.Text = c.Grid.GetTotalSummaryValue(summaryItem1).ToString()
                                                                                                                                       End Sub).Render()
                                                                                                           End Sub)
                                                                       End Sub)
                                              Next dayName
                                              settings.Columns.Add(Sub(column)

                                                                       column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
                                                                       column.FieldName = "Total"
                                                                       column.ReadOnly = True
                                                                       column.Settings.ShowEditorInBatchEditMode = False
                                                                       Dim summaryItem As ASPxSummaryItem = New ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Sum)
                                                                       summaryItem.Tag = column.FieldName & "_Sum"
                                                                       summaryItem.DisplayFormat = "<strong>{0}</strong>"
                                                                       settings.TotalSummary.Add(summaryItem)
                                                                       column.SetFooterTemplateContent(Sub(c)
                                                                                                           Html.DevExpress().Label(Sub(lbSettings)
                                                                                                                                       Dim fieldName As String = (TryCast(c.Column, GridViewDataColumn)).FieldName
                                                                                                                                       lbSettings.Name = "label" & fieldName
                                                                                                                                       lbSettings.Properties.EnableClientSideAPI = True
                                                                                                                                       Dim summaryItem1 As ASPxSummaryItem = c.Grid.TotalSummary.First(Function(i) i.Tag = (fieldName & "_Sum"))
                                                                                                                                       lbSettings.Text = c.Grid.GetTotalSummaryValue(summaryItem1).ToString()
                                                                                                                                   End Sub).Render()
                                                                                                       End Sub)
                                                                   End Sub)
                                              settings.CustomUnboundColumnData = Sub(sender, e)
                                                                                     If (e.Column.FieldName = "Total") Then
                                                                                         Dim tue = Convert.ToInt32(e.GetListSourceFieldValue("Tue"))
                                                                                         Dim mon = Convert.ToInt32(e.GetListSourceFieldValue("Mon"))
                                                                                         Dim wen = Convert.ToInt32(e.GetListSourceFieldValue("Wen"))
                                                                                         e.Value = mon + tue + wen
                                                                                     End If
                                                                                 End Sub
                                              settings.Settings.ShowFooter = True
                                              settings.CellEditorInitialize = Sub(s, e)
                                                                                  Dim editor As ASPxEdit = CType(e.Editor, ASPxEdit)
                                                                                  editor.ValidationSettings.Display = Display.Dynamic
                                                                              End Sub
                                          End Sub)
End Code
@grid.Bind(Model).GetHtml()
