using AccountingSystem.Api.Models.Cards;
using AccountingSystem.Data.Model;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Queries.Queries
{
    public interface ICardsQueryProcessor
    {
        IQueryable<Card> Get();
        Card Get(int id);
        Task<Card> Create(CreateCardModel model);
        Task<Card> Update(int id, UpdateCardModel model);
        Task Delete(int id);
    }
}
