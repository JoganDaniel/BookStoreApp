using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.Bussiness
{
    public class BookBussiness : IBookBussiness
    {
        public readonly IBookRepository bookrepository;
        public BookBussiness(IBookRepository bookrepository)
        {
            this.bookrepository = bookrepository;
        }
        public bool AddBook(Book book)
        {
            return this.bookrepository.AddBook(book);
        }

        public List<Book> GetAllBooks()
        {
            return this.bookrepository.GetAllBooks();
        }
    }
}
