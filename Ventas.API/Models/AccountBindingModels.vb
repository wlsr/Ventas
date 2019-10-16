Imports System.ComponentModel.DataAnnotations
Imports Newtonsoft.Json

' Modelos usados como parámetros para las acciones de AccountController.

Public Class AddExternalLoginBindingModel
    <Required>
    <Display(Name := "Token de acceso externo")>
    Public Property ExternalAccessToken As String
End Class

Public Class ChangePasswordBindingModel
    <Required>
    <DataType(DataType.Password)>
    <Display(Name := "Contraseña actual")>
    Public Property OldPassword As String

    <Required>
    <StringLength(100, ErrorMessage := "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength := 6)>
    <DataType(DataType.Password)>
    <Display(Name := "Nueva contraseña")>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
    <Display(Name := "Confirmar la nueva contraseña")>
    <Compare("NewPassword", ErrorMessage := "La nueva contraseña y la contraseña de confirmación no coinciden.")>
    Public Property ConfirmPassword As String
End Class

Public Class RegisterBindingModel
    <Required>
    <Display(Name := "Correo electrónico")>
    Public Property Email As String

    <Required>
    <StringLength(100, ErrorMessage := "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength := 6)>
    <DataType(DataType.Password)>
    <Display(Name := "Contraseña")>
    Public Property Password As String

    <DataType(DataType.Password)>
    <Display(Name := "Confirmar contraseña")>
    <Compare("Password", ErrorMessage := "La contraseña y la contraseña de confirmación no coinciden.")>
    Public Property ConfirmPassword As String
End Class

Public Class RegisterExternalBindingModel
    <Required>
    <Display(Name := "Correo electrónico")>
    Public Property Email As String
End Class

Public Class RemoveLoginBindingModel
    <Required>
    <Display(Name := "Proveedor de inicio de sesión")>
    Public Property LoginProvider As String

    <Required>
    <Display(Name := "Clave de proveedor")>
    Public Property ProviderKey As String
End Class

Public Class SetPasswordBindingModel
    <Required>
    <StringLength(100, ErrorMessage := "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength := 6)>
    <DataType(DataType.Password)>
    <Display(Name := "Nueva contraseña")>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
    <Display(Name := "Confirmar la nueva contraseña")>
    <Compare("NewPassword", ErrorMessage := "La nueva contraseña y la contraseña de confirmación no coinciden.")>
    Public Property ConfirmPassword As String
End Class
