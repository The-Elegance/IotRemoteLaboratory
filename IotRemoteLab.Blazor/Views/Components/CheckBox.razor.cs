using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Blazor.Views.Components
{
    public partial class CheckBox : Component
    {
        #region Parameters


        /// <summary>
        /// Состояние checkbox.
        /// </summary>
        [Parameter]
        public bool IsChecked { get; set; }
        /// <summary>
        /// Текст.
        /// </summary>
        [Parameter]
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// Callback об изменении состояния.
        /// </summary>
        [Parameter]
        public EventCallback<Tuple<string, bool>> StateChanged { get; set; }


        #endregion Parameters


        #region Private Methods


        void CheckboxChanged(MouseEventArgs e)
        {
            IsChecked = !IsChecked;
            StateChanged.InvokeAsync(Tuple.Create(Text, IsChecked));
            InvokeAsync(StateHasChanged);
        }


        #endregion Private Methods
    }
}
