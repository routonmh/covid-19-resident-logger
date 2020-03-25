using System;
using ResidentLog.Models.Entities;

namespace ResidentLog.Models.DTOs
{
    public class DutyAssignmentDTO
    {
        public int ResidentID { get; set; }
        public int DutyType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateAssigned { get; set; }

    }
}