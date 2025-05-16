using System;

namespace WalletFramework.Core.Functional
{
    public static class FunctionalExtensions
    {
        public static T Tap<T>(this T value, Action<T> action)
        {
            action(value);
            return value;
        }

        public static TResult Pipe<T, TResult>(this T value, Func<T, TResult> func)
        {
            return func(value);
        }
    }
}