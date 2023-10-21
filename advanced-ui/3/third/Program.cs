using third;

var objects = new List<TypeAnnouncer>
{
    new Ant(),
    new Bear(),
    new BmwE46(),
    new PeterPan(),
    new CSGOplayer(),
};

foreach (var entry in objects) 
{
    Console.WriteLine(
        $"Hello, i am an object of type: {entry.AnnounceType()}" 
    );
}
    