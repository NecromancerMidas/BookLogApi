using BookLogApi.Model;
using BookLogApi.Repositories;
using BookLogApi.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookLogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _unitOfWork.Books.GetAllBooks();


        }

        [HttpPost("upload")]
        public async Task<IActionResult> HandleImage(IFormFile image)
        {
            Console.WriteLine(image);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Img",image.FileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imagePath = $@"/Uploads/Img/{image.FileName}"; 

            return Ok(new { path = imagePath});
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<Book>> Put(string guid, [FromBody]Book book)
        {
        /*   var bookToChange = await _unitOfWork.Books.GetBookById(Guid.Parse(guid));
          
           bookToChange.AlterBook(book);
           await _unitOfWork.SaveChangesAsync();*/
        await _unitOfWork.UpdateChangesAsync(Guid.Parse(guid), book);
        return CreatedAtAction("Get", new { id = Guid.Parse(guid) }, book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody]Book book)
        {
            var createdBook = await _unitOfWork.AddNewBook(book);
            return CreatedAtAction("Get", new { id = createdBook.Id },createdBook);

        }

        [HttpDelete("{guid}")]
        public async Task<ActionResult<Book>> Delete(string guid)
        {
            await _unitOfWork.DeleteBook(guid);
            return NoContent();
        }
        

        }
    }

