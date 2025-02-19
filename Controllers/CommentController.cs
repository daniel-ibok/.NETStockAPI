using API.Mappers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Dtos.Comment;

namespace API.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentDto = comments.Select(c => c.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            return (comment is null) ? NotFound("Comment does not exists") : Ok(comment.ToCommentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto)
        {
            var stockExists = await _stockRepository.StockExists(commentDto.StockId);
            if (!stockExists)
            {
                return BadRequest("Stock does not exists");
            }

            var comment = await _commentRepository.CreateAsync(commentDto.ToCommentFromCreateDto());
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            var comment = await _commentRepository.UpdateAsync(id, commentDto.ToCommentFromUpdate());
            return (comment is null) ? NotFound("Comment does not exists") : Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);
            return (comment is null) ? NotFound("Comment does not exists") : Ok(comment.ToCommentDto());
        }
    }
}