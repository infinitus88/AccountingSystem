namespace AccountingSystem.Api.Models.Cards
{
    public class CardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public int UserId { get; set; }
    }
}
