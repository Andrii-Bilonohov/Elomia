namespace Application.Dtos.Requests.Base;

public record Request
(
    int Limit = 100, 
    int Offset = 0
);