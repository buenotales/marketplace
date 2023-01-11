using Microsoft.Extensions.Logging;
using Mkt.Domain.Product.Application.Dto.Request;
using Mkt.Domain.Product.Application.Dto.Response;
using Mkt.Domain.Product.Application.Infraestructure.Entities;
using Mkt.Domain.Product.Application.Infraestructure.Repositories;
using System.Text.Json;

namespace Mkt.Domain.Product.Application.Services
{
    public class ManagementService
    {
        private readonly ProductRepository productRepository;
        private readonly ILogger<ManagementService> logger;

        public ManagementService(ProductRepository productRepository, ILogger<ManagementService> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task<bool> SaveAsync(ProductPostRequestDto requestDto)
        {
            try
            {
                var entities = requestDto.Items.Select(x => (ProductEntity)x);
                await productRepository.AddRangeAsync(entities);
                return true;
            }
            catch (Exception e)
            {
                logger.LogCritical("Ocorreu uma exceção: {0}", JsonSerializer.Serialize(e));
                return false;
            }
        }

        public async Task<ProductGetResponseDto> ListAllAsync()
        {
            try
            {
                var entities = await productRepository.ListAllAsync();

                return new ProductGetResponseDto()
                {
                    Items = entities.Select(x => (ProductGetResponseDto.ItemDto)x)
                };
            }
            catch (Exception e)
            {
                logger.LogCritical("Ocorreu uma exceção: {0}", JsonSerializer.Serialize(e));
                return new ProductGetResponseDto();
            }
        }

        public ProductGetResponseDto.ItemDto GetByIdAsync(int productId)
        {
            try
            {
                var entity = productRepository.GetByFilter(x => x.ProductId == productId);
                return (ProductGetResponseDto.ItemDto)entity;
            }
            catch (Exception e)
            {
                logger.LogCritical("Ocorreu uma exceção: {0}", JsonSerializer.Serialize(e));
                return new ProductGetResponseDto.ItemDto();
            }
        }
    }
}
