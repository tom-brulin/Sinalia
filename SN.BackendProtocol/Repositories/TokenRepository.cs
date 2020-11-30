using System.Collections.Generic;

namespace SN.BackendProtocol.Repositories
{
    public class TokenRepository
    {

        private readonly List<string> tokens = new List<string>();

        public TokenRepository()
        {

        }

        public void Add(string token)
        {
            tokens.Add(token);
        }

        public void Delete(string token)
        {
            tokens.Remove(token);
        }

        public bool Has(string token)
        {
            return tokens.Contains(token);
        }

    }
}
