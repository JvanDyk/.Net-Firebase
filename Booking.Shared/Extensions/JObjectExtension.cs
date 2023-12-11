namespace Booking.Shared.Extensions;

public static class JObjectExtension
{
    public static bool VerifyBluePrintId(this JObject obj)
    {
        return obj.ContainsKey("Id");
    }

    public static bool VerifyBluePrintAccountId(this JObject obj)
    {
        return obj.ContainsKey("AccountId");
    }

    public static (string, string) GetIdAndAccountId(this JObject obj)
    {
        string id = obj.ContainsKey("Id") ? obj["Id"].ToString() : null;
        string accountId = obj.ContainsKey("AccountId") ? obj["AccountId"].ToString() : null;

        return (id, accountId);
    }
}
