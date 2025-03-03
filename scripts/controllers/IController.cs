using Common.Characters;

namespace Common.Controllers {
    public interface IController {
        void Depossess ();
        void Possess (ICharacter character);
    }
}
