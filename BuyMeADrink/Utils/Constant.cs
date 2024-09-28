namespace BuyMeADrink.Utils;

public class Constant
{
    public static readonly DateTime DefaultDateTime =
        new DateTime(1776, 7, 4, 0, 0, 0, DateTimeKind.Utc);

    public static readonly int DefaultInt = 0;
    public static readonly decimal DefaultDecimal = 0.00M;
    public static readonly double DefaultDouble = 0.00;
    public static readonly float DefaultFloat = 0f;
    public static readonly Guid DefaultGuid = Guid.Empty;
    public static readonly long DefaultLong = 0L;
    public static readonly string DefaultString = string.Empty;
    public static readonly bool DefaultBoolean = false;
    public static readonly TimeSpan DefaultTimeSpan = TimeSpan.Zero;
}