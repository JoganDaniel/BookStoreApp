using BookStoreCommon.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface IBookRepository
    {
        public bool AddBook(Book book);
        public List<Book> GetAllBooks();
        public Book EditBook(Book book);
        public string UploadImage(IFormFile file, int bookId);
        public bool DeleteBook(int bookId);
    }
}
