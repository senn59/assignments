namespace ContainerTransport.Core;

public class UrlGenerator
{
    public static string GenerateFrom(Ship ship)
    {
        string str = "";
        str += $"?length={ship.Length}&width={ship.Width}";
        var cargoInfo = GenerateCargoInfo(ship.Cargo);
        str += $"&stacks={cargoInfo.Types}";
        str += $"&weights={cargoInfo.Weight}";
        return str;
    }
    
    private static (string Types, string Weight) GenerateCargoInfo(Stack?[,] cargo)
    {
        string types = "";
        string weight = "";
        for (var i = 0; i < cargo.GetLength(0); i++)
        {
            var info = GenerateRowStackInfo(i, cargo);
            types += info.Types.TrimEnd(',');
            weight += info.Weight.TrimEnd(',');
            types += "/";
            weight += "/";
        }
        types = types.TrimEnd('/');
        weight = weight.TrimEnd('/');
        return (types, weight);
    }

    private static (string Types, string Weight) GenerateRowStackInfo(int row, Stack?[,] cargo)
    {
        string types = "";
        string weight = "";
        for (var i = 0; i < cargo.GetLength(1); i++)
        {
            var stack = cargo[row, i];
            if (stack == null) continue;
            stack.Containers.ToList().ForEach(c =>
            {
                types += (int) c.Type + "-";
                weight += c.Weight + "-";
            });
            types = types.TrimEnd('-');
            weight = weight.TrimEnd('-');
            types += ",";
            weight += ",";
        }
        return (types, weight);
    }
}