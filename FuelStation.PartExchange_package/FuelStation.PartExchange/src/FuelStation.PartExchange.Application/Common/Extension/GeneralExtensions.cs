using FuelStation.PartExchange.Application.DTOs.FileUpload;

namespace FuelStation.PartExchange.Application.Common.Extension;

public static class GeneralExtensions
{
    
    public static string GetPictureUrl(this List<FilesResponseDTO> fileUrls, Guid imageId, string clientType)
    {
        if (clientType is null) clientType = "APP";
        var file = fileUrls.FirstOrDefault(f => f.objectId.ToString().Equals(imageId.ToString()));
        if (file is null) return "";
        var url = file.urls.FirstOrDefault(x => x.clientType.ToUpper().Equals(clientType.ToUpper()));
        return url is null ? "" : url.urlAddress;
    }
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return str;

        string[] words = str.Split(new[] { ' ', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i].ToLower();
            string word = words[i];
            if (i == 0)
            {
                words[i] = char.ToLowerInvariant(word[0]) + word.Substring(1);
            }
            else
            {
                words[i] = char.ToUpperInvariant(word[0]) + word.Substring(1);
            }
        }

        return string.Join(string.Empty, words);
    }
}