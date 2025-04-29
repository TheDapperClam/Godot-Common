using Godot;

namespace Common.Controllers.Npcs {
    public partial class TargetNonPlayableController : NonPlayableController {
        private Node target;

        public void SetTarget (Node target) {
            this.target = target;
        }
    }
}
