using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Blazor.Components
{
    public partial class ComboBox : Component
    {
        #region Parameters


        /// <summary>
        /// Список элементов в списке.
        /// </summary>
        [Parameter]
        public List<object> Items { get; set; } = [];
        /// <summary>
        /// Состояние выпадающего меню (открыто/закрыто)
        /// </summary>
        [Parameter]
        public bool IsDropDownOpen { get; set; }
        /// <summary>
        /// Оставляет меню открытым когда пользователь выбрал элемент.
        /// </summary>
        [Parameter]
        public bool IsStaysOpen { get; set; }
        [Parameter]
        public object SelectedItem { get; set; }


        #endregion Parameters


        #region Constructors


        public ComboBox()
        {
            if (Items.Count > 0)
                SelectedItem = Items[0];
            else
                SelectedItem = "Not Selected";
        }


        #endregion Constructors


        #region Public & Protected Methods


        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                SelectedItem = Items[0];
                InvokeAsync(StateHasChanged);
            }
        }


        #endregion Public & Protected Methods


        #region Private Methods


        /// <summary>
        /// Открывает/Закрывает Combobox при клике на togglebutton (header).
        /// </summary>
        void HeaderClick()
        {
            IsDropDownOpen = !IsDropDownOpen;
        }

        /// <summary>
        /// Делает элемент из выпадающего списка который выбрал пользователь, выбранным.
        /// </summary>
        /// <param name="item">Элемент который выбрал пользователь</param>
        void SelectItem(object? item)
        {
            SelectedItem = item ?? "Not Selected";

            if (!IsStaysOpen)
                IsDropDownOpen = false;
        }


        #endregion Private Methods
    }
}
