using Common.Logging;
using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Input.Piping {
    public class PipeMapper : IActionPipe {
        private static readonly Logger logger = new (typeof (PipeMapper));

        private readonly Dictionary<PipeMapperKey, MethodInfo> actions = [];
        private readonly object target;

        public PipeMapper (object target) {
            this.target = target;
 
            MethodInfo[] pippedActionMethods = target.GetType ().GetMethods (BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where (m => m.GetCustomAttributes (typeof (PippedAction), true).Length > 0).ToArray ();
            if (pippedActionMethods.Length > 0) {
                foreach (MethodInfo method in pippedActionMethods) {
                    foreach (PippedAction action in method.GetCustomAttributes<PippedAction> (true)) {
                        logger.Debug ("Mapping action {0} to {1} on object {2}: {3}", action.Action, method.Name, target, action.Pressed);
                        actions.Add (new PipeMapperKey (action), method);
                    }
                }
            } else {
                logger.Debug ("No pipped actions found");
            }
        }

        public void Pipe (InputEvent inputEvent) {
            foreach (PipeMapperKey key in actions.Keys) {
                if (key.Pressed) {
                    if (inputEvent.IsActionPressed (key.Action)) {
                        actions[key].Invoke (target, null);
                    }
                } else {
                    if (inputEvent.IsActionReleased (key.Action)) {
                        actions[key].Invoke (target, null);
                    }
                }
            }
        }
    }
}
