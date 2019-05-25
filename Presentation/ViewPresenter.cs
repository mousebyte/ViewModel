using System;

namespace MouseNet.ViewModel.Presentation {
    public abstract class ViewPresenter<TView> : IViewPresenter<TView> where TView : IView {
        public TView View { get; private set; }

        public bool PresentDialog(TView view, object parent)
            {
            if (parent == null)
                throw new ArgumentException(@"parent cannot be null.", nameof(parent));
            InitializeViewInternal(view);
            return view.PresentModal(parent);
            }

        public bool IsPresenting { get; private set; }

        public void Dispose()
            {
            View?.Dispose();
            }

        public void Present(TView view)
            {
            Present(view, null);
            }

        public void Present(TView view, object parent)
            {
            InitializeViewInternal(view);
            if (parent == null) view.Present();
            else view.Present(parent);
            }

        private void OnClosed(object sender, EventArgs e)
            {
            IsPresenting = false;
            View.Dispose();
            }

        private void InitializeViewInternal(TView view)
            {
            View = view;
            View.Closed += OnClosed;
            IsPresenting = true;
            InitializeView();
            }

        protected abstract void InitializeView();
    }
}