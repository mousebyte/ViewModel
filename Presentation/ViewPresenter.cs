﻿using System;

namespace MouseNet.ViewModel.Presentation {
    public abstract class ViewPresenter<TView> : IViewPresenter<TView> where TView : IView {
        /// <summary>
        ///     The view currently being presented.
        /// </summary>
        public TView View { get; private set; }

        /// <summary>
        ///     Presents a <see cref="TView" /> as a modal dialog using the
        ///     given object as its parent.
        /// </summary>
        /// <param name="view">The view to present.</param>
        /// <param name="parent">The parent to use for the view.</param>
        /// <returns>The dialog result.</returns>
        public bool PresentDialog(TView view, object parent)
            {
            if (parent == null)
                throw new ArgumentException(@"parent cannot be null.", nameof(parent));
            InitializeViewInternal(view);
            return view.PresentModal(parent);
            }

        /// <inheritdoc />
        public bool IsPresenting { get; private set; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
            {
            View?.Dispose();
            }

        public void Present(TView view)
            {
            Present(view, null);
            }

        /// <summary>
        ///     Presents a <see cref="TView" /> using the given object as
        ///     its parent.
        /// </summary>
        /// <param name="view">The view to present.</param>
        /// <param name="parent">The parent to use for the view.</param>
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

        /// <summary>
        ///     When implemented in a derived class, performs tasks necessary to initialize
        ///     a view before it is presented to the user.
        /// </summary>
        protected abstract void InitializeView();
    }
}