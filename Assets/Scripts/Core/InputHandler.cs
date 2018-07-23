namespace TacChess.Core {
public interface IInputHandler {
        void HandleInput();
    }

    public class IH_Normal : IInputHandler {
        private static IH_Normal instance;
        public static IH_Normal Instance {
            get {
                if (instance == null) {
                    instance = new IH_Normal();
                }
                return instance;
            }
        }

        public void HandleInput() {
            
        }
    }

    public class IH_Help : IInputHandler {
        private static IH_Help instance;
        public static IH_Help Instance {
            get {
                if (instance == null) {
                    instance = new IH_Help();
                }
                return instance;
            }
        }

        public void HandleInput() {
            
        }
    }
}