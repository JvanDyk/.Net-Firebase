using Booking.Shared.Entities.Interfaces;
using System.Reflection;

namespace Booking.Shared.Repositories;

public class AccountRepository<T> : FirestoreContext<T> where T : AccountEntity
{
    public AccountRepository(FirestoreDb db) : base("accounts", db)
    {
    }

    public override async Task<string> SetAsync(T entity)
    {
        await VerifyEmail(entity.Email);
        DocumentReference docRef = _collection.Document();
        entity.Id = docRef.Id;
        var entityDict = entity.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToDictionary(prop => prop.Name, prop => (object)prop.GetValue(entity, null));

        var result = await docRef.SetAsync(entityDict);
        return entity.Id;
    }

    public override async Task UpdateAsync(T entity)
    {
        await VerifyEmail(entity.Email);
        await _collection.Document(entity.Id).SetAsync(entity);
    }
}