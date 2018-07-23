using System.Collections.Generic;

namespace TacChess.Core {
    public class Faction {
        public Faction(string name, int supply = 0) {
            this.name = name;
            this.supply = supply;
        }

        public string name;
        public int supply;

        public static bool operator ==(Faction a, Faction b) {
            if (a as object == null || b as object == null) {
                return false;
            }
            return a.name == b.name;
        }
        public static bool operator !=(Faction a, Faction b) {
            if (a as object == null || b as object == null) {
                return false;
            }
            return a.name != b.name;
        }

        public override bool Equals(object obj) {
            var faction = obj as Faction;
            return faction != null &&
                   name == faction.name &&
                   supply == faction.supply;
        }

        public override int GetHashCode() {
            var hashCode = -1388644900;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + supply.GetHashCode();
            return hashCode;
        }
    }
}
