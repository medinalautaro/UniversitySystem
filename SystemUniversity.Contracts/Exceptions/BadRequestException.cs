namespace SystemUniversity.Contracts.Exceptions{
    public class BadRequestException : ExpectedException{
        public BadRequestException(string message): base (message)
        {
        }

        public override int Code => 400;
    }
}