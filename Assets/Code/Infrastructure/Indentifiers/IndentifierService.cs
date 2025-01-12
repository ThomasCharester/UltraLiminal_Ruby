namespace Code.Infrastructure.Indentifiers
{
    public class IndentifierService : IIndentifierService
    {
        private int _lastId = 1;
        
        public int NextId() => ++_lastId;
    }
}