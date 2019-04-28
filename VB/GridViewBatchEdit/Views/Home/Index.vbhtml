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
            _iterateRowValues(s, e, function (label, columnFieldName) {
                CalculateSummary(s, label, e.rowValues, e.visibleIndex, columnFieldName, true);
                total += parseInt(label.GetValue());
            });
            labelTotal.SetText(total);
        }
        function OnChangesCanceling(s, e) {
            if (s.batchEditApi.HasChanges())
                setTimeout(function () {
                    s.Refresh();
                }, 0);
        }


        function OnBatchEditRowRecovering(s, e) {
            var total = 0;
            _iterateRowValues(s, e, function (label, fn) {
                var columnTotal = parseInt(label.GetValue()) + e.rowValues[(s.GetColumnByField(fn).index)].value;
                label.SetValue(columnTotal);
                total += parseInt(label.GetValue());
            });
            labelTotal.SetText(total);
        }
        function _iterateRowValues(s, e, f) {
            for (var key in e.rowValues) {
                var columnFieldName = s.GetColumn(key).fieldName;
                if (columnFieldName == "Total")
                    continue;
                var label = ASPxClientControl.GetControlCollection().GetByName('label' + columnFieldName);
                f(label, columnFieldName);
            }
        }
        var dict = [];
        function OnEndCallback(s, e) {
            if (dict.length == 0) return;
            _iterateColumns(s, e, function (label, fn) {
                var v = dict.find(x => x.name === 'label' + fn).value;
                label.SetValue(v);
            });
        }
        function OnBeginCallback(s, e) {
            dict = []
            if (e.command == ASPxClientGridViewCallbackCommand.UpdateEdit || e.command == ASPxClientGridViewCallbackCommand.Refresh) return;
            _iterateColumns(s, e, function (label, fn) {
                dict.push({
                    name: 'label' + fn,
                    value: label.GetValue()
                });
            });
        }
        function _iterateColumns(s, e, f) {
            for (j = 0; j < s.GetColumnCount() ; j++) {
                var columnFieldName = s.GetColumn(j).fieldName;
                var label = ASPxClientControl.GetControlCollection().GetByName('label' + columnFieldName);
                if (label) {
                    f(label, columnFieldName);
                }
            }
        }
</script>
<form>
    @Html.Action("GridViewPartial")
</form>