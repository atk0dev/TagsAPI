namespace Application.Features.Tags.Commands.CreateTag
{
    using System;

    public class CreateTagDto
    {
        public string Id { get; set; }

        public string TagID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool SelfAssign { get; set; }

        public bool RequiresOnboarding { get; set; }

        public bool IsArchived { get; set; }
    }
}
