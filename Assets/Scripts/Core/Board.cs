using System.Collections.Generic;

namespace TacChess.Core {
    public class Board {
        public Board(int sizeX, int sizeY) {
            grids = new GameGrid[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++) {
                for (int j = 0; j < sizeY; j++) {
                    grids[i, j] = new GameGrid() { type = GameGrid.GridType.Normal, doll = null, supply = 0 };
                }
            }
        }

        public GameGrid this[int posX, int posY] {
            get {
                if (0 <= posX && posX < grids.GetLength(0) && 0 <= posY && posY < grids.GetLength(1)) {
                    return grids[posX, posY];
                }
                else {
                    return null;
                }
            }
        }

        public GameGrid[,] grids;
    }
}
