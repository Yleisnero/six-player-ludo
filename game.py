import random


def throw_dice() -> int:
    return random.randint(1, 6)


class Piece:
    def __init__(self, index):
        self.index = index
        self.position = 0


class Player:
    def __init__(self, color, start_position):
        self.color = color
        self.start_postion = start_position
        self.pieces = [Piece(i) for i in range(4)]

    def move_piece(self, piece, steps):
        self.pieces[piece].position += steps


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
        throw_count = 0
        rounds = 1
        game_over = False
        while not game_over:
            for player in self.players:
                print(f"{player.color} is playing")
                dice = throw_dice()
                throw_count += 1
                print(f"Player {player.color} throws {dice}")
                player.move_piece(0, dice)
                print(
                    f"Player {player.color} is at position {player.pieces[0].position}\n"
                )

                if player.pieces[0].position >= self.length:
                    print(
                        f"Player {player.color} wins after {throw_count} dice throws and {rounds} rounds!"
                    )
                    game_over = True
                    break

                rounds += 1
