using System.Runtime.Serialization;

namespace SimpleRestApi.Common.Exceptions;


[Serializable]
public class IncorrectDataException : Exception
{
    public IncorrectDataException()
    {
    }

    public IncorrectDataException(string message) : base(message)
    {
    }

    public IncorrectDataException(string message, Exception inner) : base(message, inner)
    {
    }

    protected IncorrectDataException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}
