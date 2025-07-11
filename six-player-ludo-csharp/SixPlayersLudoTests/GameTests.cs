using SixPlayersLudo;

namespace SixPlayersLudoTests;

public class GameTests
{
    private Game _game;

    [SetUp]
    public void Setup()
    {
        _game = new Game();
    }

    [Test]
    public void TestGameDiceRoll()
    {
        Assert.That(_game.RollDice(), Is.InRange(1, 7));
    }
}