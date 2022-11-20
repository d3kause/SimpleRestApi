namespace SimpleRestApi.Common;

public static class Guard
{
    public static T NotNull<T>(T value, string name) where T : class
    {
        return value != null ? value : throw new ArgumentException(name);
    }
}