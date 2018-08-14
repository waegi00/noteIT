@ModelType ExternalLoginConfirmationViewModel
@Code
    ViewBag.Title = "Registrieren"
End Code

<h2>@ViewBag.Title.</h2>
<h3>@ViewBag.LoginProvider-Konto zuordnen.</h3>

@Using Html.BeginForm("ExternalLoginConfirmation", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
    @Html.AntiForgeryToken()

    @<text>
    <h4>Zuordnungsformular</h4>
    <hr />
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <p class="text-info">
        Sie haben sich erfolgreich authentifiziert mit <strong>@ViewBag.LoginProvider</strong>.
        Bitte geben Sie unten einen Benutzernamen für diese Website ein, und klicken Sie dann auf die Schaltfläche "Registrieren", um die Anmeldung
        fertigzustellen.
    </p>
    <div class="form-group">
        @Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})
        <div class="col-md-10">
            @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control"})
            @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger"})
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" class="btn btn-default" value="Registrieren" />
        </div>
    </div>
    </text>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
