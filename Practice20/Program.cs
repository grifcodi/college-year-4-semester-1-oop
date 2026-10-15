Player player1 = new Player("Josh");
Player player2 = new Player("Rob");
Player player3 = new Player("Kaleb");

List<Player> players = new List<Player>
{
    player1,
    player2,
    player3,
};

Team team = new Team(players);

team.ShowPlayers();

team.RemovePlayer(player2);

team.ShowPlayers();

Console.WriteLine(player2.Name);

public class Player
{
    public string Name { get; set; }

    public Player(string name)
    {
        Name = name;
    }
}

public class Team
{
    private readonly List<Player> _players;

    public Team(List<Player> players)
    {
        _players = players;
    }

    public void RemovePlayer(Player player)
    {
        _players.Remove(player);
    }

    public void ShowPlayers()
    {
        Console.WriteLine("Team players: ");
        foreach (Player player in _players)
        {
            Console.WriteLine(player.Name);
        }
        Console.WriteLine("...End of Team\n");
    }
}

//Тут використовується агрегація оскільки об'єкти Команда й Гравець мають різні методи й властивості не зважаючи на те що один включає всередині себе інший
