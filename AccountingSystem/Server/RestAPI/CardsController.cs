using AccountingSystem.Api.Models.Cards;
using AccountingSystem.Data.Model;
using AccountingSystem.Filters;
using AccountingSystem.Queries.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860



namespace AccountingSystem.Server.RestAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsQueryProcessor _query;
        private readonly IMapper _mapper;

        public CardsController(ICardsQueryProcessor query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        //[HttpGet]
        //public IQueryable<CardModel> Get()
        //{
        //    var result = _query.Get();
        //    var models = result.ProjectTo<CardModel>(_mapper.ConfigurationProvider);

        //    return models;
        //}
        
        [HttpGet]
        [QueryableResult]
        public ActionResult<IQueryable<CardModel>> Get()
        {
            var result = _query.Get();
            var models = result.ProjectTo<CardModel>(_mapper.ConfigurationProvider);

            return Ok(models);
        }

        [HttpGet("{id}")]
        public CardModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<CardModel>(item);

            return model;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<CardModel> Post([FromBody]CreateCardModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<CardModel>(item);

            return model;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<CardModel> Put(int id, [FromBody]UpdateCardModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<CardModel>(item);

            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
