namespace Booking.Shared.Repositories;

public class DynamicRepository : IDynamicRepository<DynamicEntity>
{
    private readonly IFirestoreContext _repository;
    public DynamicRepository(FirestoreDb db)
    {
        _repository = new FirestoreContext<DynamicEntity>("dynamic", db);
    }

    public async Task<string> SetAsync(DynamicEntity entity)
    {
        try
        {
            var document = _repository.Collection.Document(entity.AccountId).Collection("data").Document();
            entity.SetProperty("id", document.Id);
            entity.SetProperty("accountid", entity.AccountId);

            DocumentReference documentReference = _repository.Collection.Document(entity.AccountId).Collection("data").Document(document.Id);
            var result = await documentReference.SetAsync(entity.Properties);
            return document.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task DeleteAsync(string id, string accountId)
    {
        try
        {
            await _repository.Collection.Document(accountId).Collection("data").Document(id).DeleteAsync();
        } 
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<DynamicEntity>> GetAllAsync(string accountId)
    {
        try
        {
            var snapshot = await _repository.Collection.Document(accountId).Collection("data").GetSnapshotAsync();
            var entities = new List<DynamicEntity>();
            foreach (var document in snapshot.Documents)
            {
                var entity = document.ConvertTo<DynamicEntity>();

                Dictionary<string, object> dictionary = document.ConvertTo<Dictionary<string, object>>();
                JObject jObject = JObject.FromObject(dictionary);

                entity.Id = document.Id;
                entity.AccountId = document.GetValue<string>("accountid");
                foreach (var property in jObject.Properties())
                {
                    if (property.Name.ToLower().Equals("accountid"))
                    {
                        entity.AccountId = property.Value.ToObject<string>();
                    }
                    else if (property.Name.ToLower().Equals("id"))
                    {
                        entity.Id = property.Value.ToObject<string>();
                    }
                    else
                    {
                        entity.SetProperty(property.Name, property.Value.ToObject<dynamic>());
                    }
                }
                entities.Add(entity);
            }
            return entities;
        } 
        catch (Exception ex)
        {
            throw;
        }
      
    }

    public async Task<DynamicEntity> GetAsync(string id, string accountId)
    {
        try
        {
            var docSnap = await _repository.Collection.Document(accountId).Collection("data").Document(id).GetSnapshotAsync();
            var entity = docSnap.ConvertTo<DynamicEntity>();

            Dictionary<string, object> dictionary = docSnap.ConvertTo<Dictionary<string, object>>();
            JObject jObject = JObject.FromObject(dictionary);

            entity.Id = docSnap.Id;
            entity.AccountId = docSnap.GetValue<string>("accountid");
            foreach (var property in jObject.Properties())
            {
                if (property.Name.ToLower().Equals("accountid"))
                {
                    entity.AccountId = property.Value.ToObject<string>();
                }
                else if (property.Name.ToLower().Equals("id"))
                {
                    entity.Id = property.Value.ToObject<string>();
                }
                else
                {
                    entity.SetProperty(property.Name, property.Value.ToObject<dynamic>());
                }
            }
            return entity;
        } 
        catch(Exception ex)
        {
            throw;
        }
       
    }

    public async Task UpdateAsync(DynamicEntity entity)
    {
        try
        {
            await _repository.Collection.Document(entity.AccountId).Collection("data").Document(entity.Id).SetAsync(entity.Properties);
        }
        catch(Exception ex)
        {
            throw;
        }
    }
}
