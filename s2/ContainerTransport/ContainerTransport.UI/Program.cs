using ContainerTransport.Core;

var generator = new ShipGenerator();
var ship = new Ship(4, 6);
var containers = generator.GenerateRandomContainers(ship);
ship.Load(containers, true);
Console.WriteLine(UrlGenerator.GenerateFrom(ship));
ship.Load(containers, false);
Console.WriteLine(UrlGenerator.GenerateFrom(ship));
