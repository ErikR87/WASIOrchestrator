using System.Runtime.Serialization;

namespace WO.Hub.Contract;

[DataContract]
public class Response
{
    [DataMember(Order = 1)]
    public ResultStatus Status { get; set; }
    [DataMember(Order = 2)]
    public IList<string> ErrorMessages { get; internal set; } = new List<string>();
    [DataMember(Order = 3)]
    public IList<object> Entities { get; internal set; } = new List<object>();
    [DataMember(Order = 4)]
    public string ErrorText { get => String.Join('\n', ErrorMessages); }
    public bool HasError { get; internal set; }

    public void AddError(string message)
    {
        HasError = true;
        ErrorMessages.Add(message);
        Status = ResultStatus.Error;
    }

    public void AddEntity(object entity)
    {
        Entities.Add(entity);
    }

    public T? GetEntity<T>()
    {
        if (Entities.Any())
            return (T)Entities.First();
        else
            return default;
    }
}

public enum ResultStatus
{
    Success,
    Error
}
