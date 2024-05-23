using ContainerTransport.Core;

Console.WriteLine("Hello World!");
var normal = new Container(ContainerType.Normal, ContainerLoad.Empty);
var coolable = new Container(ContainerType.Coolable, ContainerLoad.Empty);
var valuable = new Container(ContainerType.Valuable, ContainerLoad.Empty);
var coolableValuable = new Container(ContainerType.CoolableValuable, ContainerLoad.Empty);

var containers = new List<Container>()
{
    normal,
    normal,
    normal,
    normal,
    normal,
    normal,
    coolable,
    coolable,
    valuable,
    coolableValuable
};
var ship = new Ship(4, 4, containers);
Console.WriteLine(UrlGenerator.GenerateFrom(ship));
