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

        public List<DeviceDTO> GetAvailableInputDevices()
        {
            List<DeviceDTO> devices = new List<DeviceDTO>();
            lock (Utils._lock)
            {
                foreach (Device device in deviceRepository.GetAllDevices())
                {
                    if (tagRepository.GetByIOAddress(device.IOAddress) == null ||
                        tagRepository.GetByIOAddress(device.IOAddress).TagType.Equals(TagType.AO) ||
                        tagRepository.GetByIOAddress(device.IOAddress).TagType.Equals(TagType.DO))
                        devices.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
                }
            }
            return devices;
        }

        public List<DeviceDTO> GetAvailableOutputDevices()
        {
            List<DeviceDTO> devices = new List<DeviceDTO>();
            lock(Utils._lock){
                foreach (Device device in deviceRepository.GetAllDevices())
                {
                    if (tagRepository.GetByIOAddress(device.IOAddress) == null ||
                        tagRepository.GetByIOAddress(device.IOAddress).TagType.Equals(TagType.AI) ||
                        tagRepository.GetByIOAddress(device.IOAddress).TagType.Equals(TagType.DI))
                        devices.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
                }
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
                }
            }
        }

        public void CreateDevices(List<DeviceDTO> devicesDtos)
        {
            foreach (DeviceDTO deviceDTO in devicesDtos)
            {
                    this.deviceRepository.AddDevice(new Device { IOAddress = deviceDTO.IOAddress, Value = deviceDTO.Value, Type = (DeviceType)Enum.Parse(typeof(DeviceType), deviceDTO.Type) });
            }
        }
    }
}
