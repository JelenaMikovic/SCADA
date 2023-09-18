using System;
using scada_back.Models;

namespace scada_back.DTOs
{
	public class TagDTO
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double Value { get; set; }
        public TagDTO(Tag tag)
		{
            Name = tag.Name;
            Description = tag.Description;
            IOAddress = tag.IOAddress;
            Value = tag.Value;
		}
	}
}

