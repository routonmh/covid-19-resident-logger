namespace ResidentLog.Models.Reports.Entities
{
    public class ResidentReportItem
    {
        public string ResidentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PGY { get; set; }
        public string PhoneNumber { get; set; }
        public string SymptomsDate { get; set; }
        public string SymptomsDescription { get; set; }
        public string Covid19TestDate { get; set; }
        public string Covid19TestResult { get; set; }
        public string IsQuarantined { get; set; }
        public string QuarantinedUntil { get; set; }

        public string DutyDescription { get; set; }
        public string DutyStartDate { get; set; }
        public string DutyEndDate { get; set; }
    }
}