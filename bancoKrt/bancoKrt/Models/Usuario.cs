using Amazon.DynamoDBv2.DataModel;

namespace bancoKrt.Models
{
    [DynamoDBTable("Crud_Krt")]
    public class Usuario
    {
        [DynamoDBHashKey("pk")]
        public string Documento { get; set; }

        [DynamoDBRangeKey("sk")]
        public string NumertoAgencia { get; set; }

        [DynamoDBProperty]
        public string NumeroConta { get; set; }

        [DynamoDBProperty]
        public string LimitePix { get; set; }

        [DynamoDBProperty]
        public string Nome { get; set; }
    }
}