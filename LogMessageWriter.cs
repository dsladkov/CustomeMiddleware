namespace CustomMiddleware;

public class LogMessageWriter : IMessageWriter
{
  private readonly ILogger<LogMessageWriter> _logger;
  public LogMessageWriter(ILogger<LogMessageWriter> logger)
  {
    _logger = logger;
  }
    public void Write(string message)
    {
        _logger.LogInformation(message);
    }
}
