namespace CustomerCareSystem.Util.SD;

public static class QueryAction
{
    public const string GetActions =
        """
        SELECT Id, FormId, PerformBy, ActionDate, Description
        FROM Actions
        """;

    public const string GetActionById =
        """
        SELECT Id, FormId, PerformBy, ActionDate, Description
        FROM Actions
        WHERE Id = @Id
        """;

    public const string GetActionByFormId =
        """
        SELECT Id, FormId, PerformBy, ActionDate, Description
        FROM Actions
        WHERE FormId = @FormId
        """;

    public const string GetActionByPerformBy =
        """
        SELECT Id, FormId, PerformBy, ActionDate, Description
        FROM Actions
        WHERE PerformBy = @PerformBy
        """;

    public const string GetActionByActionDate =
        """
        SELECT Id, FormId, PerformBy, ActionDate, Description
        FROM Actions
        WHERE ActionDate = @ActionDate
        """;

    public const string AddAction =
        """
        INSERT INTO Actions (FormId, PerformBy, ActionDate, Description)
        VALUES (@FormId, @PerformBy, @ActionDate, @Description)
        """;
}