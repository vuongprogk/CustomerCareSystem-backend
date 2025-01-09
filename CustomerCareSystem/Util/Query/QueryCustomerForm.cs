namespace CustomerCareSystem.Util.SD;

public static class QueryCustomerForm
{
    public const string GetCustomerForms =
        """
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForm
        """;

    public const string GetCustomerFormsByCustomerId =
        """
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForm
        WHERE CustomerId = @CustomerId
        """;

    public const string GetCustomerFormsById =
        """
        SELECT Id, CustomerId, Title, Status, CreatedDate, ResolvedDate
        FROM CustomerForm
        WHERE Id = @Id
        """;

    public const string AddCustomerForm =
        """
        INSERT INTO CustomerForm
        VALUES (@CustomerId, @Title, @Status, @CreatedDate, @ResolvedDate)
        """;

    public const string UpdateCustomerForm =
        """
        UPDATE CustomerForm
        SET Title = @Title, Status = @Status, ResolvedDate = @ResolvedDate
        WHERE Id = @Id;
        """;

    public const string DeleteCustomerForm =
        """
        DELETE FROM CustomerForm
        WHERE Id = @Id;
        """;
}