using System.Collections.Generic;

namespace TacChess.Core {
    public class GameGrid {
        public int supply = 0;
        public Doll doll = null;
        public GridType type = GridType.Normal;
        public Faction faction;

        public enum GridType {
            Normal = 0,
            HQ = 1
        }
    }
}
