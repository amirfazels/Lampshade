using _0_Framework.Application;
using ShopManagement.Application.Contacts.Product;
using ShopManagement.Domain.ProductAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var Operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return Operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var product = new Product
                (
                command.Name, 
                command.Code, 
                command.UnitPrice,
                command.ShortDescription,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.CategoryId,
                slug,
                command.Keywords,
                command.MetaDescription
                );
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return Operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var Operation = new OperationResult();
            var product = _productRepository.Get(command.Id);
            if (product == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return Operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            product.Edit
                (
                command.Name,
                command.Code,
                command.UnitPrice,
                command.ShortDescription,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.CategoryId,
                slug,
                command.Keywords,
                command.MetaDescription
                );
            _productRepository.SaveChanges();
            return Operation.Succedded();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public ICollection<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public OperationResult IsStock(long id)
        {
            var Operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);

            product.InStock();
            _productRepository.SaveChanges();
            return Operation.Succedded();
        }

        public OperationResult NotInStock(long id)
        {
            var Operation = new OperationResult();

            var product = _productRepository.Get(id);

            if (product == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);

            product.NotInStock();
            _productRepository.SaveChanges();
            return Operation.Succedded();
        }

        public ICollection<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
