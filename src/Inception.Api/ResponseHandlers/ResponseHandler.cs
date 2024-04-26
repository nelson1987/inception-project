namespace Inception.Api.ResponseHandlers;

public abstract class Response
{
    protected Response(int statusCode)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}

public class OkResponse : Response
{
    public OkResponse() : base(200)
    {
    }
}

public class CreatedResponse : Response
{
    public CreatedResponse() : base(201)
    {
    }
}

public class NotFoundResponse : Response
{
    public NotFoundResponse() : base(204)
    {
    }
}