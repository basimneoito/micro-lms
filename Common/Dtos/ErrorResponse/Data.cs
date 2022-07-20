namespace Common.Dtos.Models;

public class ResponseData<T> 
{
   public T Response { get; set; }
   public ErrorDetails Error {get; set;} = new ErrorDetails();
} 