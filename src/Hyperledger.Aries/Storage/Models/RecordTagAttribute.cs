using System;

namespace Hyperledger.Aries.Storage.Models
{
    /// <summary>
    ///     Defines an attribute to be also saved as a tag in the record
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RecordTagAttribute : Attribute
    {
    }
}
