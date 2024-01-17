namespace SystemUniversity.Contracts.Exceptions{
    public abstract class ExpectedException : Exception{
        public abstract int Code {get;}
        public ExpectedException(string message): base (message)
        {
        }
    }
}