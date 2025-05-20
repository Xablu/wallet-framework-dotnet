namespace Hyperledger.Aries.AspNetCore.Features.Bases
{
  using System;

  public class BaseException : Exception
  {
    public BaseException() { }
 
    public BaseException(string aMessage) : base(aMessage) { }

    public BaseException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BaseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
  }
}
