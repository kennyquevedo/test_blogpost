using AutoMapper;
using BlogPost.BLogic.Interfaces;
using BlogPost.Common;
using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.BLogic
{
    public class BLPosts : IBLPosts
    {
        private readonly IUnitWork unitWork;
        private readonly IMapper mapper;

        public BLPosts(IUnitWork unitWork, IMapper mapper)
        {
            this.unitWork = unitWork;
            this.mapper = mapper;
        }

        public async Task PublishPostAsync(Dto.Post post_dto)
        {
            if (post_dto == null)
                throw new ArgumentNullException("post is null");

            //Validate post status.
            if (!post_dto.Statuses.IsAny())
                throw new ArgumentNullException("post doesn't have status assigned.");

            try
            {
                await Task.Factory.StartNew(() =>
                {
                    var post = mapper.Map<Post>(post_dto);
                    unitWork.Posts.Add(post);
                    unitWork.Complete();
                });
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IList<Dto.Post>> GetAllAsync()
        {
            try
            {
                IList<Dto.Post> posts = null;

                var posts_db = await unitWork.Posts.GetAllAsync();
                if (posts_db.IsAny())
                    posts = mapper.Map<IEnumerable<Post>, IList<Dto.Post>>(posts_db);

                return posts;
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IList<Dto.Post>> GetPostsByStatusAsync(int statusId)
        {
            try
            {
                IList<Dto.Post> posts = null;

                var posts_db = await unitWork.Posts.GetPostsByStatusAsync(statusId);
                if (posts_db.IsAny())
                    posts = mapper.Map<IEnumerable<Post>, IList<Dto.Post>>(posts_db);

                return posts;
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Dto.Post> GetPostsByIdAsync(int postId)
        {
            try
            {
                Dto.Post post = null;

                var posts_db = await unitWork.Posts.GetPostsByIdAsync(postId);
                if (posts_db != null)
                    post = mapper.Map<Dto.Post>(posts_db);

                return post;
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePostAsync(Dto.Post post_dto)
        {
            if (post_dto == null)
                throw new ArgumentNullException("post is null");

            //Validate post status.
            if (!post_dto.Statuses.IsAny())
                throw new ArgumentNullException("post doesn't have status assigned.");

            try
            {
                await Task.Factory.StartNew(() =>
                {
                    var post = mapper.Map<Post>(post_dto);
                    post.LastChangedDate = DateTime.UtcNow;
                    unitWork.Posts.Update(post);
                    unitWork.Complete();
                });
            }
            catch (AutoMapperMappingException)
            {
                throw new Exception("Error mapping entities...");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
