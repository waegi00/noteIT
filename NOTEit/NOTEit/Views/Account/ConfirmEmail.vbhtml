@Code
    ViewBag.Title = "E-Mail-Adresse bestätigen"
End Code

<h2>@ViewBag.Title.</h2>
<div>
    <p>
        Danke für die Bestätigung Ihrer E-Mail-Adresse. Bitte @Html.ActionLink("klicken Sie hier, um sich anzumelden", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {Key .id = "loginLink"})
    </p>
</div>
