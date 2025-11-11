namespace ETA.Consume.Contracts.Error.Responses;

public class Error
{
    public string Message { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
    public string PropertyPath { get; set; } = string.Empty;
    public List<Error> Details { get; set; } = [];

}
