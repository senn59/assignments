namespace ContainerTransport.Core;

public class UrlGenerator
{
    private static string url = "https://i872272.luna.fhict.nl/ContainerVisualizer/index.html";
    public static string GenerateFrom(Ship ship)
    {
        string args = "";
        args += $"?length={ship.Length}&width={ship.Width}";
        var cargoInfo = GenerateCargoInfo(ship.Cargo);
        args += $"&stacks={cargoInfo.Types}";
        args += $"&weights={cargoInfo.Weight}";
        return url + args;
    }
    
    private static (string Types, string Weight) GenerateCargoInfo(Stack[,] cargo)
    {
        string types = "";
        string weight = "";
        for (var i = 0; i < cargo.GetLength(0); i++)
        {
            var info = GenerateRowStackInfo(i, cargo);
            types += RemoveTrailingChar(info.Types, ',');
            weight += RemoveTrailingChar(info.Weight, ',');
            types += "/";
            weight += "/";
        }
        types = types.TrimEnd('/');
        weight = weight.TrimEnd('/');
        return (types, weight);
    }

    private static (string Types, string Weight) GenerateRowStackInfo(int row, Stack[,] cargo)
    {
        string types = "";
        string weight = "";
        for (var i = 0; i < cargo.GetLength(1); i++)
        {
            var stack = cargo[row, i];
            if (stack.Containers.Count == 0)
            {
                types += ",";
                weight += ",";
                continue;
            }
            stack.Containers.ToList().ForEach(c =>
            {
                types += (int) c.Type + "-";
                weight += (int) c.Load + "-";
            });
            types = types.TrimEnd('-');
            weight = weight.TrimEnd('-');
            types += ",";
            weight += ",";
        }
        return (types, weight);
    }

    private static string RemoveTrailingChar(string str, char character)
    {
        if (str[str.Length - 1] == character)
        {
            return str.Substring(0, str.Length - 1);
        }

        return str;
    }
}