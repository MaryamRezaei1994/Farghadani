using FuelStation.PartExchange.Application.Statics;

namespace FuelStation.PartExchange.Application.Common.Extension;

public static class EncryptionExtensions
{
    public static string Encrypt(this string obj) => CryptographyService.EncryptString(obj);


    public static string Decrypt(this string obj) => CryptographyService.DecryptString(obj);


}