using System;
using System.Collections.Generic;
using ResidentLog.Models.DTOs;

namespace ResidentLog.Models.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class DutyAssignment
    {
        public int ResidentID { get; set; }
        public Duty Duty { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateAssigned { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <param name="duty"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        public DutyAssignment(int residentId, Duty duty,
            DateTime dateStart, DateTime dateEnd, DateTime dateAssigned)
        {
            ResidentID = residentId;
            Duty = duty;
            DateStart = dateStart;
            DateEnd = dateEnd;
            DateAssigned = dateAssigned;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dutyAssignmentDto"></param>
        /// <param name="dutyTypes"></param>
        /// <returns></returns>
        public static DutyAssignment ConvertDTO(DutyAssignmentDTO dutyAssignmentDto, List<Duty> dutyTypes)
        {
            int dutyAssignmentCode = dutyAssignmentDto.DutyType;
            Duty duty = new Duty(dutyAssignmentCode, dutyTypes.Find(it =>
                it.DutyType == dutyAssignmentCode).DutyDescription);

            DutyAssignment dutyAssignment = new DutyAssignment(
                dutyAssignmentDto.ResidentID,
                duty,
                dutyAssignmentDto.DateStart,
                dutyAssignmentDto.DateEnd,
                dutyAssignmentDto.DateAssigned);

            return dutyAssignment;
        }
    }
}