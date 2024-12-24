import random


def roll_dice() -> int:
    return random.randint(1, 6)


class Piece:
    def __init__(self, index):
        self.index = index
        self.position = None


class Player:
    def __init__(self, color, start_position):
        self.color = color
        self.start_postion = start_position
        self.pieces = [Piece(i) for i in range(4)]

    def move_piece(self, steps):
        for piece in self.pieces:
            # If a piece is on the starting position, it must be moved away first
            if self.start_postion in [piece.position for piece in self.pieces]:
                if piece.position == self.start_postion:
                    print(
                        f"Player {self.color} must move piece {piece.index} away from start position"
                    )
                    piece.position += steps
                    return

            # If rolled a six, piece must be moved out of the starting area
            if steps == 6 and None in [piece.position for piece in self.pieces]:
                if piece.position is None:
                    print(
                        f"Player {self.color} rolls a six and moves piece {piece.index} out"
                    )
                    piece.position = self.start_postion
                    return

        # Find most advanced piece
        best_piece = None
        for piece in self.pieces:
            if best_piece is None:
                best_piece = piece
            if piece.position is not None and piece.position > best_piece.position:
                best_piece = piece

        # Move most advanced piece
        if best_piece.position is not None:
            print(f"Player {self.color} moves {steps}")
            best_piece.position += steps
            return

        # Can't move any piece
        print(f"Player {self.color} can't move a piece")


class Board:
    def __init__(self):
        # The total length of the board is one line of 8 spots per player (8*6 = 48)
        self.length = 48
        self.colors = ["black", "blue", "red", "green", "purple", "yellow"]
        self.fields = [None] * self.length
        self.players = [Player(color, i * 8) for i, color in enumerate(self.colors)]

    def game_state(self):
        return f"Players: {', '.join([player.color for player in self.players])}"

    def play(self):
        roll_count = 0
        rounds = 1
        game_over = False
        while not game_over:
            for player in self.players:
                print(f"{player.color} is playing")
                dice = roll_dice()
                roll_count += 1
                print(f"Player {player.color} rolls a {dice}")
                player.move_piece(dice)
                rounds += 1

                for piece in player.pieces:
                    if (
                        piece.position is not None
                        and (piece.position - player.start_postion) > self.length
                    ):
                        print(f"Player {player.color} wins")
                        game_over = True
                        break

                if game_over:
                    print(f"Game over after {roll_count} dice rolls")
                    print(f"Game over after {rounds} rounds")
                    break
