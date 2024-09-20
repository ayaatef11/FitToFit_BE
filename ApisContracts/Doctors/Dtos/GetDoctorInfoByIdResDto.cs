namespace ApisContracts.Doctors.Dtos
{
    public record GetDoctorInfoByIdResDto
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int Age { get; set; }
    }
}
