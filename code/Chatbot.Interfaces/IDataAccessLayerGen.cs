using System;
using System.Collections;
using System.Collections.Generic;

namespace Chatbot.DataAccessLayer
{
    public interface IDataAccessLayer<T>
    {
        string GetName();

        string GetTest(int id);

        T SelectFirst(Func<T, bool> condition);

        IEnumerable<T> Select(Func<T, bool> condition);

        int Insert(T item);
    }
}