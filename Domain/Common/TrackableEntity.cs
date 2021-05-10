namespace Domain.Common
{
    using System;

    public class TrackableEntity : ITrackableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string LastModifiedBy { get; set; }
        
        public DateTime? LastModifiedDate { get; set; }
    }
}
