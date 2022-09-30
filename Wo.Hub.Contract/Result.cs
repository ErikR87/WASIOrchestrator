using System.Runtime.Serialization;

namespace WO.Hub.Contract;

[DataContract]
public class Result
{
    [DataMember(Order = 1)]
    public int Status { get; set; }
}

public enum ResultStatus
{
    Success,
    Error
}
