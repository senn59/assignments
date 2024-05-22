namespace friture.Models;

public class SnackResult(bool ok, string? message = null)
{
   public bool Ok {get; private set; } = ok;
   public string? Message { get; private set; } = message;
}