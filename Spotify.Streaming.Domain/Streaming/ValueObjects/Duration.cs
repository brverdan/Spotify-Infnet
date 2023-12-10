using Spotify.Core.Exceptions;
using Spotify.Streaming.Domain.Streaming.Exception;

namespace Spotify.Streaming.Domain.Streaming.ValueObjects;
public class Duration
{
    private readonly DurationException Validation = new DurationException();

    public int Value { get; set; }

    public Duration(int value)
    {
        if (value < 0)
        {
            Validation.AddError(new BusinessValidation()
            {
                ErrorMessage = "Music cannot have negative duration",
                ErrorName = nameof(DurationException)
            });

            Validation.ValidateAndThrow();
        }

        Value = value;
    }
}