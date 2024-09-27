using IotRemoteLab.Blazor.Tools;
using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Blazor.Components
{
    public partial class ComboBox<TItem> : Component
    {
        #region Parameters


        /// <summary>
        /// Список элементов в списке.
        /// </summary>
        [Parameter]
        public List<TItem> Items { get; set; } = [];
        
        private TItem? _selectedItem;
        [Parameter]
        public TItem? SelectedItem 
        { 
            get => _selectedItem; set 
            {
                if (_selectedItem == null)
                    return;

                if (_selectedItem.Equals(value)) {
                    return;
                }

                _selectedItem = value;
                SelectedItemChanged.InvokeAsync(value);
            }
        }
        /// <summary>
        /// Вы
        /// </summary>
        [Parameter]
        public EventCallback<TItem> SelectedItemChanged { get; set; }
        /// <summary>
        /// Состояние выпадающего меню (открыто/закрыто)
        /// </summary>
        [Parameter]
        public bool IsDropDownOpen { get; set; } = false;
        /// <summary>
        /// Оставляет меню открытым когда пользователь выбрал элемент.
        /// </summary>
        [Parameter]
        public bool IsStaysOpen { get; set; } = false;


        #endregion Parameters


        #region Constructors


        public ComboBox()
        {
            if (Items.Count > 0)
                SelectedItem = Items[0];
            else
                SelectedItem = default;
        }


        #endregion Constructors


        #region Public & Protected Methods


        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                SelectedItem = Items.Count == 0 ? default : Items[0];
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
        void SelectItem(TItem? item)
        {
            SelectedItem = item ?? default;
            if (!IsStaysOpen)
                IsDropDownOpen = false;
        }


        #endregion Private Methods


        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
