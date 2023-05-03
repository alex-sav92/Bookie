using Bookie.Data.Entities;

namespace Bookie.DirectApp.Services
{
    public interface IAuthorsService
    {
        public List<Author> GetAuthors();
        public Author? GetAuthor(int id);
        public List<Author> GetBookAuthors(int bookId);

        public int AddAuthor(Author author);

        public void UpdateAuthor(int id, Author author);

        public void DeleteAuthor(int id);

        public int Count();
    }
}
