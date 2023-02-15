﻿
namespace Shared.DataTransferObjects
{
    public record DepartmentDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Location { get; init; } = string.Empty;
    }
}
