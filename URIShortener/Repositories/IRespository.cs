using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URIShortener.Repositories
{
    interface IRespository<T> where T : class
    {

        void Add(T entity);
        T FindByField(string fieldName, string fieldValeu);
    }
}
