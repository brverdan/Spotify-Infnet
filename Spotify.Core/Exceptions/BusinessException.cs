namespace Spotify.Core.Exceptions;
public class BusinessException : Exception
{
    public List<BusinessValidation> Errors { get; set; } = new List<BusinessValidation>();

    public BusinessException() { }

    public BusinessException(BusinessValidation validation)
    {
        AddError(validation);
    }

    public void AddError(BusinessValidation validation)
    {
        Errors.Add(validation);
    }

    public void ValidateAndThrow()
    {
        if (Errors.Any())
            throw this;
    }
}


public class BusinessValidation
{
    public string ErrorName { get; set; } = "Erros de Validação";
    public string ErrorMessage { get; set; }
}
