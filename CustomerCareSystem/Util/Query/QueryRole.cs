﻿namespace CustomerCareSystem.Util.SD;

public static class QueryRole
{
    public const string GetRoles =
        """
        SELECT Id, Name, Description 
        FROM Roles;
        """;

    public const string AddRole =
        """
        INSERT INTO Roles (Id,Name, Description) 
        VALUES (@Id, @Name, @Description);
        SELECT Id, Name, Description 
        FROM Roles
        WHERE Id = @Id;
        """;

    public const string DeleteRole =
        """
        DELETE FROM Roles 
        WHERE Id = @Id;
        """;

    public const string UpdateRole =
        """
        UPDATE Roles 
        SET Name = @Name, Description = @Description 
        WHERE Id = @Id;
        SELECT Id, Name, Description FROM Roles 
        WHERE Id = @Id;
        """;

    public const string GetRoleById =
        """
        SELECT Id, Name, Description FROM Roles 
        WHERE Id = @Id;
        """;

    public const string GetRoleByName =
        """
        SELECT Id, Name, Description 
        FROM Roles WHERE Name = @Name;
        """;
}