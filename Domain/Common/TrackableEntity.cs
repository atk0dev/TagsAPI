namespace Domain.Common
{
    using System;

    public class TrackableEntity : ITrackableEntity
    {
        public string CreatedByID { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string UpdatedByID { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
    }
}
