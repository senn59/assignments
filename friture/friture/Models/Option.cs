namespace friture.Models;

public class Option
{
    private bool PasswordProtected { get; set; }
    private const string Password = "123";
    private readonly string _name;
    public string Name
    {
        get => _name;
        private init => _name = PasswordProtected ? value + " (requires password)" : value;
    }
    private readonly Action _callback;
    public Action Callback
    {
        get => _callback;
        private init
        {
            _callback = () =>
            {
                if (PasswordProtected)
                {
                    Console.WriteLine("Enter the password:");
                    var passwordInput = Console.ReadLine();
                    if (passwordInput != Password)
                    {
                        Console.WriteLine("Wrong password.");
                        return;
                    }
                }
                value();
            };
        }
    }

    public Option(string name, Action callback ,bool passwordProtected = false)
    {
        this.PasswordProtected = passwordProtected;
        this.Name = name;
        this.Callback = callback;
    }
}