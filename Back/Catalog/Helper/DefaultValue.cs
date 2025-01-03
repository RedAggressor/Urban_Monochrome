namespace Catalog.Host.Helper
{
    public static class DefaultValue
    {
        public static T GetDefaultValue<T>(T obj) => default(T)!;        
    }
}
