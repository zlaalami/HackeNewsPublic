namespace HackerNewsAPI.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using HackerNewsAPI;

    public class Response<T>
    {
        public T Data { get; set; }
        public List<ErrorModel> Errors { get; set; }

        public void ThrowErrors()
        {
            if (Errors != null && Errors.Any())
                throw new HackerNewsAPIException(
                    $"Message: {Errors[0].Message} Code: {Errors[0].Code}");
        }
    }

    public class StoriesContainer
    {
        public List<StoryModel> Stories { get; set; }
    }
}
