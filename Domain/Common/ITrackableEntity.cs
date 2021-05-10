namespace Domain.Common
{
    using System;

    public interface ITrackableEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedDate { get; set; }
    }
}