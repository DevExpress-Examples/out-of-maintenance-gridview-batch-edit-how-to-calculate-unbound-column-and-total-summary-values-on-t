<!DOCTYPE html>
<html>
<head>
    <title>@ViewBag.Title</title>
    @Html.DevExpress().GetStyleSheets( 
        New StyleSheet  With{ .ExtensionSuite = ExtensionSuite.Editors }, 
        New StyleSheet  With{ .ExtensionSuite = ExtensionSuite.GridView },
        New StyleSheet  With { .ExtensionSuite = ExtensionSuite.NavigationAndLayout }
)
    @Html.DevExpress().GetScripts( 
        New Script With { .ExtensionSuite = ExtensionSuite.Editors }, 
        New Script With { .ExtensionSuite = ExtensionSuite.GridView },
        New Script With { .ExtensionSuite = ExtensionSuite.NavigationAndLayout })
</head>
<body>
    @RenderBody()
</body>
</html>