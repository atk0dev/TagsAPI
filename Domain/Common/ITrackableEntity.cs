namespace Domain.Common
{
    using System;

    public interface ITrackableEntity
    {
        string CreatedByID { get; set; }

        DateTime CreatedDate { get; set; }

        string UpdatedByID { get; set; }

        DateTime? UpdatedDate { get; set; }
    }
}