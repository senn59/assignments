using ContainerTransport.Core;

var generator = new ShipGenerator();
var ship = generator.GenerateRandomShip(4, 6);
Console.WriteLine(UrlGenerator.GenerateFrom(ship));
