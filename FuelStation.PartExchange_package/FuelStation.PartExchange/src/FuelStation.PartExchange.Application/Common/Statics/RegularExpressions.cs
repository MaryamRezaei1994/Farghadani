namespace FuelStation.PartExchange.Application.Common.Statics;

public static class RegularExpressions
{
    public const string InvalidCharacters = "^[^\"'!@#%\\^&\\*()_\\-\\$\\+=~`\\[\\]\\{\\}\\|;:<>\\?\\/,\\\\]{1,}$";
    public const string InvalidAddress = "^[^\"'!@#%\\^&\\*()_\\-\\+=~`\\[\\]\\{\\}\\|;:<>\\?\\/,\\\\]{1,}$";
    public const string InvalidNumber = "^[0-9]{1,}$";
    public const string InvalidSerial = "^[0-9]{18}$";
    public const string InvalidDeviceModelSerial = "^[0-9]{8}$";

    public const string InvalidMac =
        "^(?:[0-9a-fA-F]{2}:){5}[0-9a-fA-F]{2}|(?:[0-9a-fA-F]{2}-){5}[0-9a-fA-F]{2}|(?:[0-9a-fA-F]{2}){5}[0-9a-fA-F]{2}$";

    public const string InvalidGuid = "^((?!00000000-0000-0000-0000-000000000000).)*$";
}