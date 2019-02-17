using System;
using System.Collections.Generic;
using LibraryManagement.LibraryClient;

namespace LibraryManagementUI.Repositories
{
    public interface IBookRepository : IBaseRepository
    {
        List<Title> GetTitleList();
        List<BookCondition> GetBookConditionList();
        string GetBarcode(int titleId);
        bool IsBarcodeValid(string barcode);
        int GetBookId(string barcode);
        void UpdateBookCondition(List<string> barcodes, BookCondition bookCondition, DateTime damLostDate);
    }
}