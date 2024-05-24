using ContainerTransport.Core;

var generator = new ShipGenerator();
var ship = generator.GenerateRandomShip(4, 6);
Console.WriteLine(UrlGenerator.GenerateFrom(ship));
// var normal = new Container(ContainerType.Normal, ContainerLoad.Full);
// var coolable = new Container(ContainerType.Coolable, ContainerLoad.Full);
// var valuable = new Container(ContainerType.Valuable, ContainerLoad.Full);
// var coolableValuable = new Container(ContainerType.CoolableValuable, ContainerLoad.Full);
//
// var containers = new List<Container>();
// containers.AddRange(Enumerable.Repeat(normal, 100));
// containers.AddRange(Enumerable.Repeat(coolable, 5));
// containers.AddRange(Enumerable.Repeat(valuable, 2));
// containers.AddRange(Enumerable.Repeat(coolableValuable, 3));
// var ship = new Ship(4, 6);
// ship.Load(containers);
// Console.WriteLine(ship.WeightDifference);
// Console.WriteLine(UrlGenerator.GenerateFrom(ship));

// var normal = new Container(ContainerType.Normal, ContainerLoad.Full);
// var coolable = new Container(ContainerType.Coolable, ContainerLoad.Full);
// var valuable = new Container(ContainerType.Valuable, ContainerLoad.Full);
// var coolableValuable = new Container(ContainerType.CoolableValuable, ContainerLoad.Full);
//
// var containers = new List<Container>();
// containers.AddRange(Enumerable.Repeat(normal, 50));
// containers.AddRange(Enumerable.Repeat(coolable, 8));
// containers.AddRange(Enumerable.Repeat(valuable, 2));
// containers.AddRange(Enumerable.Repeat(coolableValuable, 3));
// var ship = new Ship(4, 6);
// ship.Load(containers);
// Console.WriteLine(ship.WeightDifference);
// Console.WriteLine(UrlGenerator.GenerateFrom(ship));
