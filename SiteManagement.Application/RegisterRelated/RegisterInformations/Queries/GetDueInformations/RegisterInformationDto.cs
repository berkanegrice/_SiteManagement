namespace SiteManagement.Application.RegisterRelated.RegisterInformations.Queries.GetDueInformations
{
    public class RegisterInformationDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public int AccountCode { get; set; }
        public string LeaseHolder { get; set; }
        public double? Debt { get; set; }
        public double? Credit { get; set; }
        public double? BalanceDebt { get; set; }
        public double? BalanceCredit { get; set; }
    }
}