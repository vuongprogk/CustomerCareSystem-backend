namespace CustomerCareSystem.Util.SD;

public static class QueryCustomerForm
{
    public const string GetCustomerForms =
        """
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForms;
        """;

    public const string GetCustomerFormsByCustomerId =
        """
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForms
        WHERE CustomerId = @CustomerId;
        """;

    public const string GetCustomerFormsById =
        """
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForms
        WHERE Id = @Id and CustomerId = @CustomerId;
        """;

    public const string GetCustomerFormsByIdAdmin =
        """
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForms
        WHERE Id = @Id;
        """;

    public const string AddCustomerForm =
        """
        INSERT INTO CustomerForms(Id,CustomerId, Title, Status, CreatedDate, ResolvedDate)
        VALUES (@Id,@CustomerId, @Title, @Status, @CreatedDate, @ResolvedDate);
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForms
        WHERE Id = @Id;
        """;

    public const string UpdateCustomerForm =
        """
        UPDATE CustomerForms
        SET Title = @Title, Status = @Status, ResolvedDate = @ResolvedDate
        WHERE Id = @Id;
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForms
        WHERE Id = @Id;
        """;

    public const string DeleteCustomerForm =
        """
        DELETE FROM CustomerForms
        WHERE Id = @Id;
        """;
}