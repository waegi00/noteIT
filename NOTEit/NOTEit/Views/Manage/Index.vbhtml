@ModelType IndexViewModel
@Code
    ViewBag.Title = "Verwalten"
End Code

<h2>@ViewBag.Title.</h2>

<p class="text-success">@ViewBag.StatusMessage</p>
<div>
    <h4>Kontoeinstellungen ändern</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>Kennwort:</dt>
        <dd>
            [
            @If Model.HasPassword Then
                @Html.ActionLink("Kennwort ändern", "ChangePassword")
            Else
                @Html.ActionLink("Erstellen", "SetPassword")
            End If
            ]
        </dd>
        <dt>Externe Anmeldungen:</dt>
        <dd>
            @Model.Logins.Count [
            @Html.ActionLink("Verwalten", "ManageLogins") ]
        </dd>
        @*
            Phone Numbers can used as a second factor of verification in a two-factor authentication system.
             
             See <a href="https://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                for details on setting up this ASP.NET application to support two-factor authentication using SMS.
             
             Uncomment the following block after you have set up two-factor authentication
        *@
        @* 
            <dt>Telefonnummer:</dt>
            <dd>
                @(If(Model.PhoneNumber, "None"))
                @If (Model.PhoneNumber <> Nothing) Then
                    @<br />
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Change", "AddPhoneNumber")&nbsp;&nbsp;]</text>
                    @Using Html.BeginForm("RemovePhoneNumber", "Manage", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                        @Html.AntiForgeryToken
                        @<text>[<input type="submit" value="Remove" class="btn-link" />]</text>
                    End Using
                Else
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Add", "AddPhoneNumber") &nbsp;&nbsp;]</text>
                End If
            </dd>
        *@
        <dt>Zweistufige Authentifizierung:</dt>
        <dd>
            <p>
                There are no two-factor authentication providers configured. See <a href="https://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                for details on setting up this ASP.NET application to support two-factor authentication.
            </p>
            @*
                @If Model.TwoFactor Then
                    @Using Html.BeginForm("DisableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      Aktiviert
                      <input type="submit" value="Deaktivieren" class="btn btn-link" />
                      </text>
                    End Using
                Else
                    @Using Html.BeginForm("EnableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      Deaktiviert
                      <input type="submit" value="Aktivieren" class="btn btn-link" />
                      </text>
                    End Using
                End If
	     *@
        </dd>
    </dl>
</div>
