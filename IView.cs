using System;

namespace MouseNet.ViewModel {
    public interface IView : IDisposable {
        event EventHandler Closed;
        void Close();
        void Present(object parent = null);
        bool PresentModal(object parent);
    }
}