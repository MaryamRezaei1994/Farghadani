namespace FuelStation.PartExchange.Application.Common.Statics;

public static class ReturnMessages
{
    public const string InvalidLength = "The {0} value should be {1} characters.";
    public const string InvalidGuid = "The {0} as guid not valid.";
    public const string InvalidMinimumLength = "The {0} Less Than Allowed Character Length";
    public const string InvalidMaximumLength = "The {0} More Than Allowed Character Length";
    public const string InvalidCharacters = "The {0} contains invalid character";
    public const string InvalidTypeNumber = "The {0} Should only Contain Digits";
    public const string InvalidMacAddress = "Invalid MAcAddress";
    public const string InvalidSerial = "Serial Should Be 18 Digits";
    public const string InvalidDeviceModelSerial = "Device Model Serial Should Be 8 Digits";
    public static string SuccessfulAdd(string model) => $"Added {model} successfully.";
    public static string SuccessfulDelete(string model) => $"Deleted {model} successfully.";
    public static string SuccessfulUpdate(string model) => $"Updated {model} successfully.";
    public static string SuccessfulGet(string model) => $"Get {model} successfully.";

    public static string FailedAdd(string model) => $"Adding {model} failed.";
    public static string FailedDelete(string model) => $"Deleting {model} failed.";
    public static string FailedUpdate(string model) => $"Updating {model} failed.";
    public static string FailedGet(string model) => $"Getting {model} failed.";

    public static string Exception => "Internal Server Error.";

    public static string RequiredField(string field) => $"{field} is required.";
    public static string InvalidFormat(string field) => $"{field} is invalid.";
    public static string ContainsInvalidCharacter(string field) => $"{field} contains invalid character(s).";
    public static string OutOfRange(string field, int min, int max) => $"{field} must be between {min}-{max}.";
    public static string AlreadyExist(string? model) => $"{model} already exists.";
    public static string NotExist(string model) => $"{model} do(es) not exist.";
    public static string NoDeviceModelForSerial() => "Serial number doesn't belong to a device model";
    public static string AddAndDeleteMacIsSuccessfully(string model) => $"Add and Delete {model} successfully.";
    public static string AddAndDeleteMacIsFailed(string model) => $"Add and Delete {model} Failed.";

    public static string BetweenError(short minLenght, short maxLenght, string model) =>
        $"Length of {model} must be between {minLenght} and {maxLenght}";

    public static string GeneralPrint(string model) => $"{model}";
}