using scada_back.DTOs;

namespace scada_back.Services.IServices
{
    public interface IDeviceService
    {
        List<DeviceDTO> GetAvailableOutputDevices();
        List<DeviceDTO> GetAvailableInputDevices();
        void UpdateValues(List<DeviceDTO> devicesDtos);
        void CreateDevices(List<DeviceDTO> devicesDtos);
        List<DeviceDTO> GetDevices();
    }
}
