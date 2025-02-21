using _0_Framework.Application;
using ShopManagement.Application.Contacts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var Operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return Operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var Categoryslug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var picturePath = $"{Categoryslug}\\{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);
            var product = new Product
                (
                command.Name, 
                command.Code, 
                command.ShortDescription,
                command.Description,
                pictureName,
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
            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return Operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var categorySlug = product.Category.Slug;
            var picturePath = $"{categorySlug}\\{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);
            product.Edit
                (
                command.Name,
                command.Code,
                command.ShortDescription,
                command.Description,
                pictureName,
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

        public ICollection<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
