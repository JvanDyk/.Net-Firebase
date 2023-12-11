using Booking.Shared.Entities.Interfaces;
using Google.Apis.Util;
using System.Reflection;

namespace Booking.Shared.Firestore
{
    public class FirestoreContext<T> : IFirestoreContext, IRepository<T> where T : IEntity
    {
        private readonly FirestoreDb _db;
        protected readonly CollectionReference _collection;

        public FirestoreContext(string collectionName, FirestoreDb firebaseDb)
        {
            _db = firebaseDb;
            _collection = _db.Collection(collectionName);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var snapshot = await _collection.GetSnapshotAsync();
            var entities = new List<T>();
            foreach (var document in snapshot.Documents)
            {
                var entity = document.ConvertTo<T>();
                entity.Id = document.Id;
                entities.Add(entity);
            }
            return entities;
        }

        public virtual async Task<T> GetAsync(string id)
        {
            var document = await _collection.Document(id).GetSnapshotAsync();
            var entity = document.ConvertTo<T>();
            entity.Id = document.Id;

            return entity;
        }

        public virtual async Task<string> AddAsync(T entity)
        {
            var documentReference = await _collection.AddAsync(entity);
            return documentReference.Id;
        }

        public virtual async Task<string> SetAsync(T entity)
        {
            DocumentReference docRef = _collection.Document();
            entity.Id = docRef.Id;
            var entityDict = entity.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (object)prop.GetValue(entity, null));

            var result = await docRef.SetAsync(entityDict);
            return docRef.Id;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _collection.Document(entity.Id).SetAsync(entity);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _collection.Document(id).DeleteAsync();
        }

        public async Task VerifyEmail(string email)
        {
            Query query = _collection.WhereEqualTo("Email", email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Count != 0)
            {
                throw new BadRequestException("Email already exist.");
            }
        }

        public CollectionReference Collection => _collection;
    }
}
