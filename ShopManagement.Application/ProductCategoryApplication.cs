using _0_Framework.Application;
using ShopManagement.Application.Contacts.Product;
using ShopManagement.Application.Contacts.ProductCategory;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            OperationResult operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var picturePath = command.Slug;
            var fileName = _fileUploader.Upload(command.Picture, picturePath);
            var productCategory = new ProductCategory
                (command.Name,
                command.Description,
                fileName,
                command.PictureAlt,
                command.PictureTitle,
                command.Keywords,
                command.MetaDescription,
                command.Slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();


        }

        public OperationResult Edit(EditProductCategory command)
        {
            OperationResult operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);

            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);


            var slug = command.Slug.Slugify();
            var picturePath = command.Slug;
            var fileName = _fileUploader.Upload(command.Picture, picturePath);
            productCategory.Edit
                (command.Name,
                command.Description,
                fileName,
                command.PictureAlt,
                command.PictureTitle,
                command.Keywords,
                command.MetaDescription,
                command.Slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public ICollection<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public ICollection<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
