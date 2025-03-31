using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserService.Application.DTOs
{
    public class GetUsersQueryDto
    {
        [MinLength(3, ErrorMessage = "Search must contain 3 or more characters")]
        public string? Search {  get; set; }

        [DefaultValue(0)]
        [Range(0, int.MaxValue, ErrorMessage = "Skip must be 0 or greater")]
        public int Skip { get; set; }

        [DefaultValue(10)]
        [Range(1, 100, ErrorMessage = "Limit must be between 1 and 100")]
        public int Take { get; set; }
    }
}
