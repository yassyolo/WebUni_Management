﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUni_Management.Core.Models.Library;

namespace WebUni_Management.Core.Contracts
{
    public interface ILibraryService
    {
        Task<IEnumerable<BookInfoViewModel>> LastThreeBooksAsync();
    }
}
