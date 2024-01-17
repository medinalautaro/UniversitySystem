namespace SystemUniversity.Contracts.Exceptions{
    public class NotFoundException : ExpectedException{
        public NotFoundException(string message): base (message)
        {
        }

        public override int Code => 404;
    }
}