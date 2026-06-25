namespace LojaApi.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // passa para o próximo middleware
        }
        catch (ProdutoNotFoundException ex)
        {
            await HandleException(context, ex, HttpStatusCode.NotFound);
        }
        catch (CategoriaNotFoundException ex)
        {
            await HandleException(context, ex, HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex, HttpStatusCode.InternalServerError);
        }
    }

    private static async Task HandleException(HttpContext context, Exception ex, HttpStatusCode status)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var erro = new
        {
            status = (int)status,
            mensagem = ex.Message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(erro));
    }
}