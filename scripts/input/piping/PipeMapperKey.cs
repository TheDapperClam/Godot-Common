namespace Common.Input.Piping {
    public struct PipeMapperKey {
        public string Action { get; set; }
        public bool Pressed { get; set; }

        public PipeMapperKey (string action, bool pressed) {
            Action = action;
            Pressed = pressed;
        }

        public PipeMapperKey (PippedAction action) {
            Action = action.Action;
            Pressed = action.Pressed;
        }
    }
}
