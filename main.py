import random
import art

from game import Player, Board

def main():
    art.tprint("MENSCH AERGER DICH NICHT")
    board = Board()
    print(board.game_state())
    board.play()


if __name__ == "__main__":
    main()
