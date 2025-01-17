namespace Code.Infrastructure.Indentifiers
{
    public class IdentifierService : IIdentifierService
    {
        private int _lastId = 1;
        
        public int NextId() => ++_lastId;
    }
}