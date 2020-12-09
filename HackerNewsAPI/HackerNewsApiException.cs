namespace HackerNewsAPI
{
    using System;

    public class HackerNewsAPIException : ApplicationException
    {
        public HackerNewsAPIException(string message) : base(message)
        {
        }
    }
}