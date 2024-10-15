namespace IotRemoteLab.AdminPanel.EnumBox
{
    public struct EnumBox<T> where T : Enum
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public static IEnumerable<EnumBox<T>> EnumToValue()
        {
            var collection = new List<EnumBox<T>>();

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                collection.Add(new EnumBox<T>
                {
                    Name = item.ToString(),
                    Value = item
                });
            }

            return collection;
        }
    }
}