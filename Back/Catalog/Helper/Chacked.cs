namespace Catalog.Host.Helper
{
    public static class Chacked
    {
        public static bool IsNeedUpdate<T>(T current, T newValue)
        {
            return !(current!.Equals(newValue) || current.Equals(DefaultValue.GetDefaultValue(newValue)));
        }
    }
}
