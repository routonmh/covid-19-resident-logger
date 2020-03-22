using System;
using ResidentLog.Models.Entities;

namespace ResidentLog.Models.DTOs
{
    public class ResidentDTO
    {
        public int ResidentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PGY { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? SymptomsDate { get; set; }
        public string SymptomsDescription { get; set; }
        public DateTime? Covid19TestDate { get; set; }
        public int Covid19TestResult { get; set; }
        public bool IsQuarantined { get; set; }
        public DateTime? QuarantinedUntil { get; set; }
    }
}