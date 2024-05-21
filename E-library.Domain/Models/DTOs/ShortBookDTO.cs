namespace E_library.Domain.Models.DTOs;

public class ShortBookDTO
{
    public int Id { get; set; }
    public double TotalScore { get; set; }
    public string Name { get; set; } = null!;
    public string CoverPath { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public DateTime DateOfPublishing { get; set; }
    public DateTime? DateOfWriting { get; set; }
}
