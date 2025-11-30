using Common.Characters;
using System;

namespace Common.Controllers {
    [Obsolete("Controllers are deprecated, use sockets instead")]
    public interface IController {
        void Depossess ();
        void Possess (ICharacter character);
    }
}
