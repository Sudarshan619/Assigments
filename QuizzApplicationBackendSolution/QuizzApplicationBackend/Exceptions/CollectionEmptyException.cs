﻿namespace QuizzApplicationBackend.Exceptions
{
    public class CollectionEmptyException:Exception
    {
        string msg= string.Empty;

        public CollectionEmptyException(string message)
        {
            msg = message;
        }

        
    }
}