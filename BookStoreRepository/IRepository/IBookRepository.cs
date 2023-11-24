using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface IBookRepository
    {
        public bool AddBook(Book book);
        public List<Book> GetAllBooks();
    }
}
