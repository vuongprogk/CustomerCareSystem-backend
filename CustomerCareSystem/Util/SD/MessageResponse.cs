namespace CustomerCareSystem.Util.SD;

public static class MessageResponse
{
    // User message
    public const string InvalidUserOrPassword = "Invalid user or password";
    public const string RegisterFailed = "Registration failed";
    public const string Unauthorized = "Unauthorized";
    public const string LoginSuccess = "Login successful";
    public const string RegisterSuccess = "Registration successful";


    // Query Message
    public const string FetchSuccess = "Successfully fetched";
    public const string FetchFailed = "Failed fetched";
    public const string NotFound  = "Not found";


    // Role message
    public const string AddRoleFailed = "Adding role failed";
    public const string UpdateRoleFailed = "Updating role failed";
    public const string UpdateRoleSuccess = "Updated role successful";
    public const string DeleteRoleFailed = "Deleting role failed";
    public const string DeleteRoleSuccess = "Deleted role successful";
    public static string AddRoleSuccess = "Added role successful";
    
    // Department message
    public const string AddDepartmentFailed = "Adding department failed";
    public const string UpdateDepartmentFailed = "Updating department failed";
    public const string UpdateDepartmentSuccess = "Updating department successful";
    public const string DeleteDepartmentFailed = "Deleting department failed";
    public const string DeleteDepartmentSuccess = "Deleting department successful";
    
    // Customer Form message
    public static string AddCustomerFormFailed = "Adding customer form failed";
    
    // Action message
    public static string AddActionFailed = "Adding action failed";
}