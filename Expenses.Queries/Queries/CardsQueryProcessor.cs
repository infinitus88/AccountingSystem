using AccountingSystem.Api.Common.Exceptions;
using AccountingSystem.Api.Models.Cards;
using AccountingSystem.Data.Access.DAL;
using AccountingSystem.Data.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Queries.Queries
{
    public class CardsQueryProcessor : ICardsQueryProcessor
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        
        public CardsQueryProcessor(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IQueryable<Card> Get()
        {
            var query = GetAllQuery();
            return query;
        }

        private IQueryable<Card> GetAllQuery()
        {
            var q = _uow.Query<Card>()
                .Where(x => !x.IsDeleted);

            return q;
        }

        public Card Get(int id)
        {
            var user = GetAllQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("Card is not found");
            }

            return user;
        }

        public async Task<Card> Create(CreateCardModel model)
        {
            var item = _mapper.Map<Card>(model);

            _uow.Add(item);
            await _uow.CommitAsync();

            return item;
        }

        public async Task<Card> Update(int id, UpdateCardModel model)
        {
            var card = GetAllQuery().FirstOrDefault(x => x.Id == id);
            
            if (card == null)
            {
                throw new NotFoundException("Card is not found");
            }

            card = _mapper.Map<Card>(model);

            await _uow.CommitAsync();

            return card;
        }

        public async Task Delete(int id)
        {
            var card = GetAllQuery().FirstOrDefault(u => u.Id == id);

            if (card == null)
            {
                throw new NotFoundException("Card is not found");
            }

            if (card.IsDeleted) return;

            card.IsDeleted = true;
            await _uow.CommitAsync();
        }
    }
}