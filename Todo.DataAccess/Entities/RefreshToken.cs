using System.ComponentModel.DataAnnotations;

namespace Todo.DataAccess.Entities;

public class RefreshToken
{
    [Key]
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Token { get; set; }
    public string JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime AddedTime { get; set; }
    public DateTime ExpiryData { get; set; }
}