using Blog_Platform.Data;
using Blog_Platform.IRepository;
using Blog_Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_Platform.Repository
{
    public class BlogPostHasTagRepo : IBlogPostHasTagRepo
    {
        private readonly AppDbContext context;

        public BlogPostHasTagRepo(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(BlogPostHasTag blogPostHasTag)
        {
            context.Add(blogPostHasTag);
        }

        public void DeleteByPostId(int PostId)
        {
            context.BlogPostHasTags.Where(x=>x.PostId == PostId).ExecuteDelete();
        }

        public void save()
        {
            context.SaveChanges();
        }
    }
}
