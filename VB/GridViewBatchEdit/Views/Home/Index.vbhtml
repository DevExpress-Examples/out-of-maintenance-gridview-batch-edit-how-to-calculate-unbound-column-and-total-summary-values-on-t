<script type="text/javascript">
    var currentColumnName;
    function OnBatchEditStartEditing(s, e) {
        currentColumnName = e.focusedColumn.fieldName;
    }
    function OnBatchEditEndEditing(s, e) {
        var label = ASPxClientControl.GetControlCollection().GetByName('label' + currentColumnName);
        CalculateSummary(s, label, e.rowValues, e.visibleIndex, currentColumnName, false);
        window.setTimeout(function () {
            var rowTotal = 0;
            for (var key in e.rowValues) {
                if (s.GetColumn(key).fieldName == "Total")
                    continue;
                rowTotal += e.rowValues[key].value;
            }
            s.batchEditApi.SetCellValue(e.visibleIndex, "Total", rowTotal, null, true);
            var total = parseInt(labelMon.GetValue()) + parseInt(labelTue.GetValue()) + parseInt(labelWen.GetValue())
            labelTotal.SetText(total);
        }, 0);
    }
    function CalculateSummary(grid, labelSum, rowValues, visibleIndex, columnName, isDeleting) {
        var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, columnName);
        var newValue = rowValues[(grid.GetColumnByField(columnName).index)].value;
        var dif = isDeleting ? -newValue : newValue - originalValue;
        labelSum.SetValue((parseInt(labelSum.GetValue()) + dif));
    }
    function OnBatchEditRowDeleting(s, e) {
        var total = 0;
        for (var key in e.rowValues) {
            var columnFieldName = s.GetColumn(key).fieldName;
            if (columnFieldName == "Total")
                continue;
            var label = ASPxClientControl.GetControlCollection().GetByName('label' + columnFieldName);
            CalculateSummary(s, label, e.rowValues, e.visibleIndex, columnFieldName, true);
            total += parseInt(label.GetValue());
        }
        labelTotal.SetText(total);
    }
    function OnBatchEditChangesCanceling(s, e) {
        if (s.batchEditApi.HasChanges())
            setTimeout(function () {
                s.Refresh();
            }, 0);
    }
</script>
<form>
    @Html.Action("GridViewPartial")
</form>