using System;

namespace UseCases
{
    public static class UseCaseUtils 
    {
        public static void ThrowArgumentNullException(string nameOf)
        {
            throw new ArgumentNullException(nameOf, $"A value for {nameOf} must be provided");
        }
    }
}