using Godot;

namespace Common.Components.Sockets {
    public interface ISocket {
        void Connect (Node child);
        void Disconnect ();
        bool IsConnected ();
    }
}
