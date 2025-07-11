namespace SixPlayersLudo;

public class Player
{
    public string PlayerColor { get; }
    private List<Piece> Pieces { get; }
    public int StartPosition { get; }
    private readonly List<bool> _goalPositions = Enumerable.Repeat(false, 4).ToList();

    public Player(string playerColor, int startPosition)
    {
        PlayerColor = playerColor;
        StartPosition = startPosition;
        Console.WriteLine($"Player {playerColor} has StartPosition {startPosition}");

        Pieces = new List<Piece>();
        for (var i = 0; i < 4; i++)
        {
            Pieces.Add(new Piece(playerColor + i));
        }
    }

    public bool CanMovePieceOutOfHome()
    {
        var currentPiecePositions = Pieces.Select(x => x.Position).ToList();
        // Player may have to move Piece on Start Position away or does not have piece in home base
        return currentPiecePositions.Contains(0) && !currentPiecePositions.Contains(StartPosition);
    }

    public Piece? GetPieceToMoveIntoGoal(int diceResult)
    {
        if (diceResult > 1)
        {
            foreach (var piece in Pieces)
            {
                if (piece.Position != 0)
                {
                    var goal = piece.Position + diceResult;
                    if (goal == StartPosition + 1 && !_goalPositions[0])
                    {
                        Console.WriteLine($"Moved piece {piece.PieceName} into goal 1!");
                        _goalPositions[0] = true;
                        return piece;
                    }

                    if (goal == StartPosition + 2 && !_goalPositions[1] && !_goalPositions[0])
                    {
                        Console.WriteLine($"Moved piece {piece.PieceName} into goal 2!");
                        _goalPositions[1] = true;
                        return piece;
                    }

                    if (goal == StartPosition + 3 && !_goalPositions[2] && !_goalPositions[1] && !_goalPositions[0])
                    {
                        Console.WriteLine($"Moved piece {piece.PieceName} into goal 3!");
                        _goalPositions[2] = true;
                        return piece;
                    }

                    if (goal == StartPosition + 4 && !_goalPositions[3] && !_goalPositions[2] && !_goalPositions[1] &&
                        !_goalPositions[0]
                       )
                    {
                        Console.WriteLine($"Moved piece {piece.PieceName} into goal 4!");
                        _goalPositions[3] = true;
                        return piece;
                    }
                }
            }
        }

        return null;
    }

    public bool CanMoveForwardInGoal(int diceResult)
    {
        if (diceResult == 1)
        {
            if (_goalPositions[2] && !_goalPositions[3]
                || _goalPositions[1] && !_goalPositions[2]
                || _goalPositions[0] && !_goalPositions[1])
            {
                return true;
            }
        }

        if (diceResult == 2)
        {
            if (_goalPositions[1] && !_goalPositions[2] && !_goalPositions[3]
                || _goalPositions[0] && !_goalPositions[1] && !_goalPositions[2])
            {
                return true;
            }
        }

        if (diceResult == 3)
        {
            if (_goalPositions[0] && !_goalPositions[1] && !_goalPositions[2] && !_goalPositions[3])
            {
                return true;
            }
        }

        return false;
    }

    public void MoveForwardInGoal(int diceResult)
    {
        switch (diceResult)
        {
            case 1:
                if (_goalPositions[2] && !_goalPositions[3])
                {
                    _goalPositions[3] = true;
                    _goalPositions[2] = false;
                }

                if (_goalPositions[1] && !_goalPositions[2])
                {
                    _goalPositions[2] = true;
                    _goalPositions[1] = false;
                }

                if (_goalPositions[0] && !_goalPositions[1])
                {
                    _goalPositions[1] = true;
                    _goalPositions[0] = false;
                }

                break;
            case 2:
                if (_goalPositions[1] && !_goalPositions[2] && !_goalPositions[3])
                {
                    _goalPositions[3] = true;
                    _goalPositions[1] = false;
                }

                if (_goalPositions[0] && !_goalPositions[1] && !_goalPositions[2])
                {
                    _goalPositions[2] = true;
                    _goalPositions[0] = false;
                }

                break;
            case 3:
                if (_goalPositions[0] && !_goalPositions[1] && !_goalPositions[2] && !_goalPositions[3])
                {
                    _goalPositions[3] = true;
                    _goalPositions[0] = false;
                }

                break;
        }
    }

    public void MovePieceIntoHome(Piece piece)
    {
        
    }

    public Piece? GetPieceToMoveOut()
    {
        foreach (var piece in Pieces)
        {
            if (piece.Position == 0)
            {
                return piece;
            }
        }

        return null;
    }

    public Piece? GetMostAdvancedPiece()
    {
        var mostAdvancedPiece = Pieces.OrderBy(x => x.Position).LastOrDefault();
        return mostAdvancedPiece;
    }

    public bool HasPieceOnBoard()
    {
        var currentPiecePositions = Pieces.Select(x => x.Position).ToList();
        return currentPiecePositions.Exists(x => x != 0);
    }

    public bool HasWon()
    {
        return _goalPositions.TrueForAll(x => x);
    }
}