using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Common.Components.Modal
{
    public abstract class ModalBase<T> : ComponentBase
    {
        private bool _visible;
        [Parameter]
        public bool Visible
        {
            get => _visible; set
            {
                _visible = value;
                OnVisibilityChanged();
            }
        }


        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }

        [Parameter]
        public bool ConfirmLoading { get; set; }
        [Parameter]
        public Action<T?> OnOk { get; set; } = (obj) => { };
        [Parameter]
        public Action OnCancel { get; set; } = () => { };


        protected virtual void OnCanceled()
        {
            VisibleChanged.InvokeAsync(false);
        }

        protected virtual void OnOkHandle() 
        {
            OnOk?.Invoke(default);
            Visible = false;
        }

        protected virtual void OnVisibilityChanged()
        {

        }
    }
}
