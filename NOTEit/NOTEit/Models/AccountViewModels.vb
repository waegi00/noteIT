Imports System.ComponentModel.DataAnnotations

Public Class ExternalLoginConfirmationViewModel
    <Required>
    <Display(Name:="E-Mail")>
    Public Property Email As String
End Class

Public Class ExternalLoginListViewModel
    Public Property ReturnUrl As String
End Class

Public Class SendCodeViewModel
    Public Property SelectedProvider As String
    Public Property Providers As ICollection(Of System.Web.Mvc.SelectListItem)
    Public Property ReturnUrl As String
    Public Property RememberMe As Boolean
End Class

Public Class VerifyCodeViewModel
    <Required>
    Public Property Provider As String
    
    <Required>
    <Display(Name:="Code")>
    Public Property Code As String
    
    Public Property ReturnUrl As String
    
    <Display(Name:="Diesen Browser merken?")>
    Public Property RememberBrowser As Boolean

    Public Property RememberMe As Boolean
End Class

Public Class ForgotViewModel
    <Required>
    <Display(Name:="E-Mail")>
    Public Property Email As String
End Class

Public Class LoginViewModel
    <Required>
    <Display(Name:="E-Mail")>
    <EmailAddress>
    Public Property Email As String

    <Required>
    <DataType(DataType.Password)>
    <Display(Name:="Kennwort")>
    Public Property Password As String

    <Display(Name:="Speichern?")>
    Public Property RememberMe As Boolean
End Class

Public Class RegisterViewModel
    <Required>
    <EmailAddress>
    <Display(Name:="E-Mail")>
    Public Property Email As String

    <Required>
    <StringLength(100, ErrorMessage:="""{0}"" muss mindestens {2} Zeichen lang sein.", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="Kennwort")>
    Public Property Password As String

    <DataType(DataType.Password)>
    <Display(Name:="Kennwort bestätigen")>
    <Compare("Password", ErrorMessage:="Das Kennwort entspricht nicht dem Bestätigungskennwort.")>
    Public Property ConfirmPassword As String
End Class

Public Class ResetPasswordViewModel
    <Required>
    <EmailAddress>
    <Display(Name:="E-Mail")>
    Public Property Email() As String

    <Required>
    <StringLength(100, ErrorMessage:="""{0}"" muss mindestens {2} Zeichen lang sein.", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="Kennwort")>
    Public Property Password() As String

    <DataType(DataType.Password)>
    <Display(Name:="Kennwort bestätigen")>
    <Compare("Password", ErrorMessage:="Das Kennwort stimmt nicht mit dem Bestätigungskennwort überein.")>
    Public Property ConfirmPassword() As String

    Public Property Code() As String
End Class

Public Class ForgotPasswordViewModel
    <Required>
    <EmailAddress>
    <Display(Name:="E-Mail")>
    Public Property Email() As String
End Class
