namespace friture;

public class SnackResult
{
   public bool Ok {get; private set; }
   public string? Message { get; private set; }

   public SnackResult( bool ok, string? message = null)
   {
      this.Ok = ok;
      this.Message = message;
   }
}