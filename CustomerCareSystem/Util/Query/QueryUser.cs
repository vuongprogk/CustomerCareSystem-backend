namespace CustomerCareSystem.Util.SD;

public static class QueryUser
{
    public const string UpdateUser =
        """
        UPDATE Users SET Address = @Address,
        DepartmentId = @DepartmentId,
        PhoneNumber = @PhoneNumber, 
        FirstName = @FirstName,
        LastName = @LastName,
        UpdatedDate = @UpdatedDate,
        Address = @Address,
        WHERE Id = @Id;
        SELECT Id, FirstName, LastName, Email, 
        PhoneNumber, HashedPassword, RoleId, 
        CreatedDate, UpdatedDate, Address, DepartmentID
        FROM Users
        WHERE Id = @Id;
        """;

    public const string RegisterNewUser =
        """
        INSERT INTO Users 
        VALUES (@Id, @FirstName, @LastName, @Email, @PhoneNumber, 
                @HashedPassword, @RoleId, @CreatedDate, 
                @UpdatedDate, @Address, @DepartmentId);
        SELECT Id, FirstName, LastName, Email, 
        PhoneNumber, HashedPassword, RoleId, 
        CreatedDate, UpdatedDate, Address, DepartmentID
        FROM Users
        WHERE Id = @Id;
        """;

    public const string GetUserById =
        """
        SELECT Id, FirstName, LastName, Email, 
        PhoneNumber, HashedPassword, RoleId, 
        CreatedDate, UpdatedDate, Address, DepartmentID
        FROM Users
        WHERE Id = @Id;
        """;

    public const string GetUsers =
        """
        SELECT Id, FirstName, LastName, Email, 
        PhoneNumber, HashedPassword, RoleId, 
        CreatedDate, UpdatedDate, Address, DepartmentID 
        FROM Users;
        """;

    public const string DeleteUserById =
        """
        DELETE FROM Users 
        WHERE Id = @Id;
        """;

    public const string DeleteUserByEmail =
        """
        DELETE FROM Users
        WHERE Email = @Email;
        """;

    public const string GetUserByEmail =
        """
        SELECT Id, FirstName, LastName, Email, 
        PhoneNumber, HashedPassword, RoleId, 
        CreatedDate, UpdatedDate, Address, DepartmentID 
        FROM Users 
        WHERE Email = @Email;
        """;
}