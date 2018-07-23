using System.Collections.Generic;

namespace TacChess.Core {
    public class Game {
        public const int EXIT_VICTORIOUS = 999;

        private static Game instance;
        public static Game Instance {
            get {
                if (instance == null) {
                    instance = new Game();
                }
                return instance;
            }
        }

        public List<Faction> factions = new List<Faction>();
        public int victorious = -1;
        public Board board;
        public int currentFactionIndex = 0;
        public IInputHandler inputHandler;
        public bool showHelp = false;

        public Faction CurrentFaction {
            get {
                return factions[currentFactionIndex];
            }
        }

        public int GetFactionIndex(Faction faction) {
            for (int i = 0; i < factions.Count; i++) {
                if (factions[i] == faction) {
                    return i;
                }
            }
            return -1;
        }

        public bool DeployDoll(Doll doll) {
            return DeployDoll(doll, factions[currentFactionIndex]);
        }

        public bool DeployDoll(Doll doll, Faction faction) {
            GameGrid hq = null;
            for (int i = 0; i < board.grids.GetLength(0); i++) {
                for (int j = 0; j < board.grids.GetLength(1); j++) {
                    if (board[i, j].type == GameGrid.GridType.HQ && board[i, j].faction == factions[currentFactionIndex]) {
                        hq = board[i, j];
                    }
                }
            }
            if (hq == null || hq.doll != null || factions[currentFactionIndex].supply < Doll.deployCost[(int)doll.type]) {
                return false;
            }

            factions[currentFactionIndex].supply -= Doll.deployCost[(int)doll.type];
            hq.doll = doll;
            return true;
        }

        public bool MoveDoll(int posX, int posY, int deltaX, int deltaY) {
            if (board[posX, posY] == null || board[posX, posY].doll == null || board[posX, posY].doll.faction != CurrentFaction) {
                return false;
            }
            int x = posX + deltaX, y = posY + deltaY;
            if (board[x, y] == null) {
                return false;
            }
            if (board[x, y].doll != null && board[x, y].doll.faction == board[posX, posY].faction) {
                return false;
            }

            if (board[x, y].doll != null) {
                board[x, y].doll = Battle(board[posX, posY].doll, board[x, y].doll);
            }
            else {
                board[x, y].doll = board[posX, posY].doll;
            }
            board[posX, posY].doll = null;
            
            if (board[x, y].doll != null && board[x, y].doll.type == Doll.DollType.Scout) {
                board[x, y].doll.faction.supply += board[x, y].supply;
                board[x, y].supply = 0;
            }

            if (board[x, y].type == GameGrid.GridType.HQ && board[x, y].doll != null && board[x, y].faction != board[x, y].doll.faction && board[x, y].doll.type != Doll.DollType.Bomb) {
                victorious = GetFactionIndex(board[x, y].doll.faction);
            }
            var loser = new List<bool>();
            for (int i = 0; i < factions.Count; i++) {
                loser.Add(true);
            }
            for (int i = 0; i < board.grids.GetLength(0); i++) {
                for (int j = 0; j < board.grids.GetLength(1); j++) {
                    if (board[i, j].doll != null) {
                        loser[GetFactionIndex(board[i, j].doll.faction)] = false;
                    }
                }
            }
            if (loser[0]) {
                victorious = 1;
            }
            if (loser[1]) {
                victorious = 0;
            }

            return true;
        }

        public Doll Battle(Doll d1, Doll d2) {
            if (d1.type == Doll.DollType.Bomb || d2.type == Doll.DollType.Bomb) {
                return null;
            }
            else {
                if ((int)d1.type > (int)d2.type) {
                    return d1;
                }
                else if ((int)d1.type < (int)d2.type) {
                    return d2;
                }
                else {
                    return null;
                }
            }
        }

        public void EndCurrentTurn() {
            currentFactionIndex++;
            if (currentFactionIndex >= factions.Count) {
                currentFactionIndex = 0;
            }
        }

        
        

        public void OnGameEnd() {
            if (victorious == EXIT_VICTORIOUS) {
                return;
            }
        }
    }
}
