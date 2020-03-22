using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResidentLog.Models.DTOs;

namespace ResidentLog.Models.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class Resident
    {
        public int ResidentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PGY { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? SymptomsDate { get; set; }
        public string SymptomsDescription { get; set; }
        public DateTime? Covid19TestDate { get; set; }
        public TestResult Covid19TestResult { get; set; }
        public bool IsQuarantined { get; set; }
        public DateTime? QuarantinedUntil { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="residentId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="pgy"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="symptomsDate"></param>
        /// <param name="symptomsDescription"></param>
        /// <param name="covid19TestDate"></param>
        /// <param name="covid19TestResult"></param>
        /// <param name="isQuarantined"></param>
        /// <param name="quarantinedUntil"></param>
        public Resident(int residentId, string firstName, string lastName, string pgy,
            string phoneNumber, DateTime? symptomsDate, string symptomsDescription,
            DateTime? covid19TestDate, TestResult covid19TestResult, bool isQuarantined,
            DateTime? quarantinedUntil)
        {
            ResidentID = residentId;
            FirstName = firstName;
            LastName = lastName;
            PGY = pgy;
            PhoneNumber = phoneNumber;
            SymptomsDate = symptomsDate;
            SymptomsDescription = symptomsDescription;
            Covid19TestDate = covid19TestDate;
            Covid19TestResult = covid19TestResult;
            IsQuarantined = isQuarantined;
            QuarantinedUntil = quarantinedUntil;
        }

        public static Resident ConvertDTO(ResidentDTO residentDto, List<TestResult> testResultTypes)
        {
            int testResultCode = Convert.ToInt32(residentDto.Covid19TestResult);

            Resident resident = new Resident(
                residentDto.ResidentID,
                residentDto.FirstName,
                residentDto.LastName,
                residentDto.PGY,
                residentDto.PhoneNumber,
                residentDto.SymptomsDate,
                residentDto.SymptomsDescription,
                residentDto.Covid19TestDate,
                new TestResult(
                    testResultCode,
                    testResultTypes.Find(it =>
                        it.TestResultType == testResultCode).TestResultDescription),
                residentDto.IsQuarantined,
                residentDto.QuarantinedUntil
            );

            return resident;
        }
    }
}