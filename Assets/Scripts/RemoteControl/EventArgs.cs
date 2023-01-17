using System;

public class EventArgs<T> : EventArgs
{
    public EventArgs(T data) => Data = data;
    public T Data { get; }

    public static implicit operator EventArgs<T>(T data) => new EventArgs<T>(data);
}
