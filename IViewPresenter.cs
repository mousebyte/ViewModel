using System;

namespace MouseNet.ViewModel {
    public interface IViewPresenter<TView> : IDisposable where TView : IView {
        TView View { get; }
        bool IsPresenting { get; }
        void Present(TView view);
        void Present(TView view, object parent);
        bool PresentDialog(TView view, object parent);
    }
}