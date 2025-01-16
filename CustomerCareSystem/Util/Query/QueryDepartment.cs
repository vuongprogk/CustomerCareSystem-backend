using CustomerCareSystem.Model;

namespace CustomerCareSystem.Util.SD;

public static class QueryDepartment
{
    public const string GetDepartments =
        """
        SELECT ID, Name, Description 
        FROM Departments;
        """;

    public const string GetDepartmentById =
        """
        SELECT ID, Name, Description
        FROM Departments
        WHERE ID = @ID;
        """;

    public const string GetDepartmentByName =
        """
        SELECT ID, Name, Description
        FROM Departments
        WHERE Name = @Name;
        """;

    public const string AddDepartment =
        """
        INSERT INTO Departments (Id, Name, Description) 
        VALUES (@Id, @Name, @Description);
        SELECT ID, Name, Description
        FROM Departments
        WHERE ID = @ID;
        """;

    public const string UpdateDepartment =
        """
        UPDATE Departments SET Name = @Name, Description = @Description
        WHERE ID = @ID;
        SELECT ID, Name, Description
        FROM Departments
        WHERE ID = @ID;
        """;

    public const string DeleteDepartmentById =
        """
        DELETE FROM Departments
        WHERE ID = @ID;
        """;

    public static string DeleteDepartmentByName =
        """
        DELETE FROM Departments
        WHERE Name = @Name;
        """;
}