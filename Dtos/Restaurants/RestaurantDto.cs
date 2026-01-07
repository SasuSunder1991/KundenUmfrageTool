using System.ComponentModel.DataAnnotations;

public class RestaurantDto
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Street { get; set; } = string.Empty;

    [Required]
    public string ZipCode { get; set; } = string.Empty;

    [Required]
    public string City { get; set; } = string.Empty;

    public string? Country { get; set; } = "DE";

    public int? ManagerUserId { get; set; }

    public int? SurveyId { get; set; }

    public string? ManagerName { get; set; }
    public string? SurveyName { get; set; }
    public string? QrCodeKey { get; set; }

    public int RatingCount { get; set; }
    public double? AverageRating { get; set; }
}
