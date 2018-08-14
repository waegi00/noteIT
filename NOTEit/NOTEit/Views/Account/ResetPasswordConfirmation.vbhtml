﻿@Code
    ViewBag.Title = "Kennwortbestätigung zurücksetzen"
End Code

<hgroup class="title">
    <h1>@ViewBag.Title.</h1>
</hgroup>
<div>
    <p>
        Ihr Kennwort wurde zurückgesetzt. Bitte @Html.ActionLink("klicken Sie hier, um sich anzumelden", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
    </p>
</div>
