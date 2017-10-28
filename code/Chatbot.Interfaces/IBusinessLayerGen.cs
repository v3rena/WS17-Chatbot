using System;

namespace Chatbot.Services
{
    public interface IBusinessLayer<T>
    {
        string GetName();
        string GetTest(int id);
        T SelectFirst(Func<T, bool> condition);
    }
}