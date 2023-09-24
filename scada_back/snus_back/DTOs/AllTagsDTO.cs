using System;
using scada_back.Models;

namespace scada_back.DTOs
{
	public class AllTagsDTO
	{
		public List<AITagDTO> AnalogInputs { get; set; }
		public List<DITagDTO> DigitalInputs { get; set; }
		public AllTagsDTO(List<AITagDTO> ai,List<DITagDTO> di)
		{
			AnalogInputs = ai;
			DigitalInputs = di;
		}
	}
}

