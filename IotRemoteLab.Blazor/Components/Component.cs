using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Blazor.Components
{
    public abstract class Component : ComponentBase
    {
        #region Properties


        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> Attributes { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public string? Name { get; set; }
        public string UniqueId { get; set; }


        #endregion Properties


        #region Public & Protected Methods


        protected override void OnInitialized()
        {
            UniqueId = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace('-', '-').Replace("+", "-").Substring(0, 10);
        }


        public string GetId() 
        {
            if (Attributes != null && Attributes.TryGetValue("id", out var id) && !string.IsNullOrEmpty(Convert.ToString(@id)))
            {
                return $"{@id}";
            }

            return UniqueId;
        }

        #endregion Public & Protected Methods
    }
}
