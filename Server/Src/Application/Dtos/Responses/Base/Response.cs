namespace Application.Dtos.Responses.Base;

public record Response<T>
(
    int TotalCount,
    IEnumerable<T> Items
);

