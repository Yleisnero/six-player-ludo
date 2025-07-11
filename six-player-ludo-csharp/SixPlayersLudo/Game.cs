namespace SixPlayersLudo;

public class Game
{
    private readonly Random _rand;

    // The total length of the board is one line of 8 spots per player (8*6 = 48)
    private const int BoardSize = 48;
    private static readonly string[] Colors = ["black", "blue", "red", "green", "purple", "yellow"];
    private readonly List<Player> _players = [];
    private readonly Piece?[] _mainPath = new Piece?[BoardSize];

    public Game()
    {
        _rand = new Random();
        var startPosition = 1;
        foreach (var color in Colors)
        {
            _players.Add(new Player(color, startPosition));
            startPosition += 8;
        }
    }

    public int RollDice()
    {
        return _rand.Next(1, 7);
    }

    public void Play()
    {
        var gameEnded = false;
        var rounds = 0;
        while (!gameEnded)
        {
            ShowGameState();

            foreach (var player in _players)
            {
                gameEnded = MakeAMove(player);
                if (gameEnded)
                {
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Console.WriteLine($"Player {player.PlayerColor} has won after {rounds} Rounds!");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    return;
                }
            }

            rounds++;
        }
    }

    private bool MakeAMove(Player player)
    {
        var result = RollDice();
        Console.WriteLine($"{player.PlayerColor} rolled {result}");

        // Move Piece
        if (result == 6 && player.CanMovePieceOutOfHome())
        {
            var piece = player.GetPieceToMoveOut();
            _mainPath[player.StartPosition] = piece;
            if (piece != null)
            {
                piece.Position = player.StartPosition;
            }
        }
        else if (player.CanMoveForwardInGoal(result))
        {
            player.MoveForwardInGoal(result);
            Console.WriteLine($" {player.PlayerColor} moved forward inside goal");
        }
        else if (player.HasPieceOnBoard())
        {
            var pieceIntoGoal = player.GetPieceToMoveIntoGoal(result);
            if (pieceIntoGoal != null)
            {
                _mainPath[pieceIntoGoal.Position] = null;
            }
            else
            {
                var piece = player.GetMostAdvancedPiece();
                if (piece != null)
                {
                    _mainPath[piece.Position] = null;
                    var newPosition = (piece.Position + result) % BoardSize;

                    var thrownPiece = _mainPath[newPosition];
                    if (thrownPiece != null)
                    {
                        piece.Position = 0;
                    }

                    _mainPath[newPosition] = piece;
                    piece.Position = newPosition;
                }
            }
        }

        if (player.HasWon())
        {
            return true;
        }

        // Roll Dice Again
        if (result == 6)
        {
            Console.WriteLine($"{player.PlayerColor} is allowed to roll the dice again");
            MakeAMove(player);
        }

        return false;
    }

    private void ShowGameState()
    {
        Console.WriteLine("------------------------------------------------------------------------------------");
        for (var i = 0; i < BoardSize; i++)
        {
            var piece = _mainPath[i];
            if (piece != null)
            {
                Console.WriteLine($"Piece {piece.PieceName} is in position {i}");
            }
        }
    }
}