namespace Akademik.Application.DTO.ResidentDTO;

public class FewResidentInfoDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int? RoomNumber { get; set; }
}