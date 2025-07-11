using SixPlayersLudo;

namespace SixPlayersLudoTests;

public class PlayerTests
{
    private Player _player;
    private const int StartPosition = 56;
    private const string Color = "rainbow";

    [SetUp]
    public void Setup()
    {
        _player = new Player(Color, StartPosition);
    }

    [Test]
    public void TestStartPosition()
    {
        Assert.That(_player.StartPosition, Is.EqualTo(StartPosition));
    }

    [Test]
    public void TestColor()
    {
        Assert.That(_player.PlayerColor, Is.EqualTo(Color));
    }
}