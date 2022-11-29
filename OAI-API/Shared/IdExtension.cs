using HashidsNet;

namespace OAI_API.Shared
{
    public static class IdExtension
    {
        private static Hashids? _hashId;

        public static void SetSalt(string salt)
        {
            _hashId = new Hashids(salt);
        }

        public static string ToHashId(this int id) 
        {
            if (_hashId == null) return id.ToString();

            return _hashId.Encode(id);
        }

        public static int FromHashId(this string hashId)
        {
            if (_hashId == null) return Convert.ToInt32(hashId);

            return _hashId.Decode(hashId).First();
        }
    }
}
