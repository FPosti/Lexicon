using InterfaceExc;


Program1 p1 = new();
Program2 p2 = new();

/* Ursprungliga anrop före interfacet:
// p1.run();
// p2.Go();
*/
p1.Start();
p2.Start();
p1.Stop();
p2.Stop();


DubbelRunner runner = new();

IRunVoid utanSvar = runner;
IRunInt medSvar = runner;

utanSvar.Run();

int resultat = medSvar.Run();
Console.WriteLine($"Resultatet är {resultat}");