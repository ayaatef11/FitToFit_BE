namespace Modules.Doctors.ApiContracts.Doctor.Dtos
{
    public sealed record GetDoctorInfoByIdDtoRequest
    {
        public int Id { get; set; }

    }
    public record GetDoctorInfoByIdResDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int Age { get; set; }
    }
}
