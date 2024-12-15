using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class ServiceWrapper : IServiceWrapper
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IConfiguration _configuration;

        public ServiceWrapper(IRepositoryWrapper repositoryWrapper, IConfiguration configuration)
        {
            _repositoryWrapper = repositoryWrapper;
            _configuration = configuration;
        }

        private IRecipeService _recipe;
        public IRecipeService Recipe
        {
            get{ return _recipe = _recipe == null ? new RecipeService(_repositoryWrapper) : _recipe; }
        }
   
    }
