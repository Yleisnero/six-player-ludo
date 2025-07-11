namespace SixPlayersLudo;

public class Piece
{
    public string PieceName { get; }
    public int Position { get; set; }

    public Piece(string pieceName)
    {
        PieceName = pieceName;
        // Piece postion 0 is the home base
        Position = 0;
    }
}