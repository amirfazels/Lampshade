namespace _01_LampshadeQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
        ICollection<SlideQueryModel> GetSlides();
    }
}
