using Booking.Shared.Entities.Interfaces;

namespace Booking.Shared.Entities;

[FirestoreData]
public class DynamicEntity : IEntity, IAccount
{
    public Dictionary<string, dynamic> Properties { get; set; } = new Dictionary<string, dynamic>();

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string AccountId { get; set; }

    public T GetProperty<T>(string name)
    {
        if (Properties.TryGetValue(name, out dynamic value))
        {
            return (T)value;
        }
        else
        {
            return default(T);
        }
    }

    public void SetProperty(string name, dynamic value)
    {
        Properties[name.ToLower()] = value;
    }
}
