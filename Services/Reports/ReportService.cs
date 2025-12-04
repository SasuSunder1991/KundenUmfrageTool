using KundenUmfrageTool.Api.Data;
using KundenUmfrageTool.Api.Dtos.Reports;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _db;

        public ReportService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<RestaurantReportDto> GetReportForRestaurant(int restaurantId)
        {
            // Restaurant + Surveys + Checkpoint + Rating Laden
            var restaurant = await _db.Restaurants
               .Include(r => r.Surveys)
                .ThenInclude(s => s.SurveyCheckpoints)
                  .ThenInclude(sc => sc.Checkpoint)
              .Include(r => r.Ratings)
              .ThenInclude(rg => rg.Checkpoint)
              .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)

                throw new Exception("Restaurant nicht gefunden");

            var result = new RestaurantReportDto
            {
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.Name,
            };
            //liste aller Checkpoint aus allen Umfragen
            var checkpoints = restaurant.Surveys
                .SelectMany(s => s.SurveyCheckpoints)
                .Select(s => s.Checkpoint)
                .Distinct()
                .ToList();

            foreach (var cp in checkpoints)
            {
                var ratingsForCp = restaurant.Ratings
                       .Where(r => r.CheckpointId == cp.Id)
                       .ToList();

                if (ratingsForCp.Any())
                {
                    //Durschnitt
                    result.Averages.Add(new CheckpointAverageDto
                    {
                        CheckpointName = cp.Name,
                        Average = ratingsForCp.Average(r => r.Score),
                        Count = ratingsForCp.Count()
                    });

                    // Kommentare
                    result.Comments.AddRange(
                        ratingsForCp
                            .Where(r => !string.IsNullOrWhiteSpace(r.Comment))
                            .Select(r => new CommentDto
                            {
                                Checkpoint = cp.Name,
                                Stars = r.Score,
                                Text = r.Comment!
                            })
                    );
                }
            }

            // Beste Bewertung
            result.BestComment = result.Comments
                .OrderByDescending(c => c.Stars)
                .FirstOrDefault();


            // Schlechste Bewertung
            result.WorstComment = result.Comments
                .OrderBy(c => c.Stars)
                .FirstOrDefault();


            return result;



        }
    }
}
