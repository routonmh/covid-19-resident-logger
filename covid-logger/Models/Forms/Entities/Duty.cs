namespace ResidentLog.Models.Entities
{
    /// <summary>
    /// Floor, ICU, COVID, etc.
    /// </summary>
    public class Duty
    {
        public int DutyType { get; set; }
        public string DutyDescription { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dutyType"></param>
        /// <param name="dutyDescription"></param>
        public Duty(int dutyType, string dutyDescription)
        {
            DutyType = dutyType;
            DutyDescription = dutyDescription;
        }
    }
}