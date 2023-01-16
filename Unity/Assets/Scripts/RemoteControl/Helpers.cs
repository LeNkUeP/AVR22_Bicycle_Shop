using System;

public static class Helpers
{

    delegate bool TryParse<TIn, TOut>(TIn value, out TOut result);

    private static void DoIf<TIn, TOut>(TIn value, TryParse<TIn, TOut> tryParse, Action<TOut> action)
    {
        if (tryParse(value, out var result))
        {
            action(result);
        }
    }

    private static void DoIf<T>(Func<(bool result, T resValue)> condi, Action<T> action)
    {
        var x = condi();
        if (x.result)
        {
            action(x.resValue);
        }
    }

    private static void DoIf(bool condition, Action action)
    {
        if (condition)
        {
            action();
        }
    }

    private static void DoCase<T>(T value, Action defaultAction, params (T condition, Action action)[] cases)
    {
        foreach (var @case in cases)
        {
            if (Equals(@case.condition, value))
            {
                @case.action();
                return;
            }
        }
        defaultAction();
    }
}