using ContainerTransport.Core;

var generator = new ContainerGenerator();
var ship = new Ship(7, 7);
var containers = generator.GenerateRandomContainers(ship);
ship.Load(containers, true);
Console.WriteLine(ship.Balance);
Console.WriteLine(UrlGenerator.GenerateFrom(ship));
// ship.Load(containers, false);
// Console.WriteLine(ship.Balance);
// Console.WriteLine(UrlGenerator.GenerateFrom(ship));
