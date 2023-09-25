using scada_back.DTOs;
using scada_back.Models;
using scada_back.Repositories;
using scada_back.Services.IServices;

namespace scada_back.Services
{
    public class DeviceService : IDeviceService
    {
        public DeviceRepository deviceRepository;
        public TagRepository tagRepository;
        public DeviceService(DeviceRepository deviceRepository, TagRepository tagRepository) {
            this.deviceRepository = deviceRepository;
            this.tagRepository = tagRepository;
        }

        public List<DeviceDTO> GetAvailableDevices()
        {
            List<DeviceDTO> devices = new List<DeviceDTO>();
            foreach(Device device in deviceRepository.GetAllDevices())
            {
                if (tagRepository.GetByIOAddress(device.IOAddress) == null) 
                    devices.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
            }
            return devices;
        }

        public void UpdateValues(List<DeviceDTO> devicesDtos)
        {
            foreach(DeviceDTO deviceDTO in devicesDtos)
            {
                if (deviceRepository.GetByIOAddress(deviceDTO.IOAddress) != null)
                {
                    this.deviceRepository.UpdateValue(deviceDTO);
                } else
                {
                    this.deviceRepository.AddDevice(new Device { IOAddress = deviceDTO.IOAddress, Value = deviceDTO.Value, Type = (DeviceType)Enum.Parse(typeof(DeviceType), deviceDTO.Type) });
                }
            }
        }
    }
}
