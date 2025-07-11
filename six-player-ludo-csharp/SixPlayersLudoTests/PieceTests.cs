using SixPlayersLudo;

namespace SixPlayersLudoTests;

public class PieceTests
{
    private Piece _piece;
    private const string PieceName = "piece";

    [SetUp]
    public void Setup()
    {
        _piece = new Piece(PieceName);
    }

    [Test]
    public void TestInitialPosition()
    {
        Assert.That(_piece.Position, Is.EqualTo(0));
    }

    [Test]
    public void TestPieceName()
    {
        Assert.That(_piece.PieceName, Is.EqualTo(PieceName));
    }
}