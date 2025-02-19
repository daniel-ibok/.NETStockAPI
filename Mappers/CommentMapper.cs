using API.Dtos.Comment;
using API.Models;

namespace API.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                StockId = comment.StockId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = comment.StockId,
            };
        }
    }
}