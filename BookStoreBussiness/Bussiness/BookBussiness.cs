using BookStoreBussiness.IBussiness;
using BookStoreCommon.Model;
using BookStoreRepository.IRepository;
using Microsoft.AspNetCore.Http;
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

        public bool DeleteBook(int bookId)
        {
            return this.bookrepository.DeleteBook(bookId);
        }

        public Book EditBook(Book book)
        {
            return this.bookrepository.EditBook(book);
        }

        public List<Book> GetAllBooks()
        {
            return this.bookrepository.GetAllBooks();
        }

        public string UploadImage(IFormFile file,int bookId)
        {
            return this.bookrepository.UploadImage(file,bookId);
        }
    }
}
