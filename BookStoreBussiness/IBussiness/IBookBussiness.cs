using BookStoreCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBussiness.IBussiness
{
    public interface IBookBussiness
    {
        public bool AddBook(Book book);
        public List<Book> GetAllBooks();
    }
}
