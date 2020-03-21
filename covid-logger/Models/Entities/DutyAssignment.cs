using System;

namespace ResidentLog.Models.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class DutyAssignment
    {
        public int ResidentID { get; set; }
        public int DutyType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <param name="dutyType"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        public DutyAssignment(int residentId, int dutyType, DateTime dateStart, DateTime dateEnd)
        {
            ResidentID = residentId;
            DutyType = dutyType;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }
    }
}