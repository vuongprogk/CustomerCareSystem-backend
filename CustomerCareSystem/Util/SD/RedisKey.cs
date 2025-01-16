namespace CustomerCareSystem.Util.SD;

public class RedisKey
{
    public const string Users = "Users";
    public const string CustomerForms = "CustomerForms";
    public const string Actions = "Actions";
    public const string Departments = "Departments";
    public const string Roles = "Roles";
    public static string ActionsByPerformBy(string key) => $"ActionsByPerformBy/{key}";

    public static string UserKey(string id) => $"User/{id}";
    public static string CustomerFormKey(string id) => $"CustomerForm/{id}";
    public static string ActionKey(string id) => $"Action/{id}";
    public static string DepartmentKey(string id) => $"Department/{id}";
    public static string RoleKey(string id) => $"Role/{id}";
    public static string UserName(string name) => $"User/{name}";
    public static string CustomerFormCustomerKey(string key) => $"CustomerFormCustomerKey/{key}";
    public static string ActionName(string name) => $"Action/{name}";
    public static string DepartmentName(string name) => $"Department/{name}";
    public static string RoleName(string name) => $"Role/{name}";
}