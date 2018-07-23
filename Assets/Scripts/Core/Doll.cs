using System.Collections.Generic;

namespace TacChess.Core {
    public class Doll {
        public Doll(DollType type, Faction faction) {
            this.type = type;
            this.faction = faction;
        }

        public DollType type;
        public Faction faction;

        public enum DollType {
            Scout = 0,
            Assault = 1,
            Elite = 2,
            Bomb = 3
        }

        public static string[] displayName = new string[4]
            //{ "S", "A", "E", "B" };
            { "侦", "突", "锐", "砰" };
        public static int[] deployCost = new int[4] { 1, 1, 2, 1 };
    }
}
