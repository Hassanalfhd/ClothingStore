using System.Threading;
using ClothingStore.Application.Features.Catalog.Cart.Dtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Application.Features.Catalog.Cart
{
    public class CartService:ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IProductReadRepos _productReadRepos;
        private readonly IUserRepo _userRepo;
        private readonly IProductVariantRepo _productVariantRepo;
        private readonly IUnitOfWork _unitOfWork;


        public CartService(ICartRepo cartRepo, IUserRepo userRepo, IProductReadRepos productReadRepos, IProductVariantRepo productVariantRepo, IUnitOfWork unitOfWork)
        {
            _cartRepo = cartRepo;
            _userRepo = userRepo;
            _productReadRepos = productReadRepos;
            _productVariantRepo = productVariantRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> AddToCart(AddToCartDto cartDto, CancellationToken cancellationToken)
        {

            var variant = await _productVariantRepo.GetVariantDtoByIdAsync(cartDto.VariantId, cancellationToken);

            if (variant == null)
                return Result.Failure("Variant not found");

            if (variant.StockQuantity < cartDto.Quantity)
                return Result.Failure("Insufficient stock");


            var product = await _productReadRepos.GetByIdAsync(cartDto.ProductId, cancellationToken);

            if (product is null)
                return Result.Failure("Product not found");


            var userId = await _userRepo.GetIdAsync(cartDto.UserId, cancellationToken);

            

            var cart = await _cartRepo.GetByUserIdAsync(userId.Value, cancellationToken);

            if (cart is null)
            {
                cart  = new Domain.Entities.Cart(userId.Value);
                await _cartRepo.AddAsync(cart, cancellationToken);
            }

            cart.AddItem(
                product.Id,
                variant.Id,
                variant.PublicId,
                product.ProductName,
                new Money(variant.Price, variant.Currency),
                cartDto.Quantity
                );



            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }

        public async Task<Result> RemoveFromCart(ManageCartItemQuantityDto dto, CancellationToken cancellationToken)
        {
            var userId = await _userRepo.GetIdAsync(dto.UserId, cancellationToken);
            if (userId is null)
                return Result.Failure("User not found");

            var cart = await _cartRepo.GetByUserIdAsync(userId.Value, cancellationToken);
           
            if (cart is null)
                return Result.Failure("Cart not found");

            var variant = await _productVariantRepo.GetProductVariantId(dto.VariantId, cancellationToken);

            if (variant is null)
                return Result.Failure("Variant not found");

            cart.RemoveItem(variant.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }


        
        public async Task<Result> IncreaseQuantity(
    ManageCartItemQuantityDto dto,
    CancellationToken cancellationToken)
        {
            var userId = await _userRepo.GetIdAsync(dto.UserId, cancellationToken);

            if (userId is null)
                return Result.Failure("User not found");

            var cart = await _cartRepo.GetByUserIdAsync(userId.Value, cancellationToken);

            if (cart is null)
                return Result.Failure("Cart not found");

            var variant = await _productVariantRepo
                .GetProductVariantId(dto.VariantId, cancellationToken);

            if (variant is null)
                return Result.Failure("Variant not found");

            // optional safety check
            
            cart.IncreaseQuantity(variant.Value);


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        public async Task<Result> DecreaseQuantity(
    ManageCartItemQuantityDto dto,
    CancellationToken cancellationToken)
        {
            var userId = await _userRepo.GetIdAsync(dto.UserId, cancellationToken);

            if (userId is null)
                return Result.Failure("User not found");

            var cart = await _cartRepo.GetByUserIdAsync(userId.Value, cancellationToken);

            if (cart is null)
                return Result.Failure("Cart not found");

            var variant = await _productVariantRepo
                .GetProductVariantId(dto.VariantId, cancellationToken);

            if (variant is null)
                return Result.Failure("Variant not found");


            cart.DecreaseQuantity(variant.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ClearCart(Guid UserId, CancellationToken cancellationToken)
        {
            var userId = await _userRepo.GetIdAsync(UserId, cancellationToken);

            if (userId is null)
                return Result.Failure("User not found");

            var cart = await _cartRepo.GetByUserIdAsync(userId.Value, cancellationToken);

            if (cart is null)
                return Result.Failure("Cart not found");

            if(cart.Items.Count <= 0)
                return Result.Failure("Cart is already cleared.");

            cart.Clear();  
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }

        public async Task<Result<CartDto>> GetCartAsync(Guid UserId, CancellationToken cancellationToken)
        {
            var userId = await _userRepo.GetIdAsync(UserId, cancellationToken);

            if (userId is null)
                return Result<CartDto>.Failure("User not found");

            var cart = await _cartRepo.GetByUserIdAsync(userId.Value, cancellationToken);

            if (cart is null)
                return Result<CartDto>.Failure("Cart not found");

            var result = await _cartRepo.GetCartAsync(userId.Value, cancellationToken);

            if (result is null)
                return Result<CartDto>.Failure("No items found.");

            return Result<CartDto>.Success(result);
        }


    }
}
