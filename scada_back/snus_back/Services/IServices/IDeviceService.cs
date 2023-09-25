using scada_back.DTOs;

namespace scada_back.Services.IServices
{
    public interface IDeviceService
    {
        List<DeviceDTO> GetAvailableDevices();
        void UpdateValues(List<DeviceDTO> devicesDtos);
    }
}
