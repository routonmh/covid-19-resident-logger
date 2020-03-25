namespace ResidentLog.Models.Entities
{
    public class LocalIDSet<T>
    {
        public T PreviousID { get; set; }
        public T NextID { get; set; }
    }
}