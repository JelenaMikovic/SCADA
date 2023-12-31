﻿using scada_back.DTOs;
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
            foreach (Device device in deviceRepository.GetAllDevices())
            {
                Tag tag = tagRepository.GetByIOAddress(device.IOAddress);
                if ( tag == null ||
                    tag.TagType.Equals(TagType.AO) ||
                    tag.TagType.Equals(TagType.DO))
                    devices.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
            }
            return devices;
        }

        public List<DeviceDTO> GetAvailableOutputDevices()
        {
            List<DeviceDTO> devices = new List<DeviceDTO>();
            foreach (Device device in deviceRepository.GetAllDevices())
            {
                Tag tag = tagRepository.GetByIOAddress(device.IOAddress);
                if (tag == null ||
                    tag.TagType.Equals(TagType.AI) ||
                    tag.TagType.Equals(TagType.DI))
                    devices.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
            }
            return devices;
        }

        public void UpdateValues(List<DeviceDTO> devicesDtos)
        {
            this.deviceRepository.UpdateValue(devicesDtos);
        }

        public void CreateDevices(List<DeviceDTO> devicesDtos)
        {
            foreach (DeviceDTO deviceDTO in devicesDtos)
            {
                    this.deviceRepository.AddDevice(new Device { IOAddress = deviceDTO.IOAddress, Value = deviceDTO.Value, Type = (DeviceType)Enum.Parse(typeof(DeviceType), deviceDTO.Type) });
            }
        }

        public List<DeviceDTO> GetDevices()
        {
            List<DeviceDTO> devices = new List<DeviceDTO>();
            foreach (Device device in deviceRepository.GetAllDevices())
            {
               devices.Add(new DeviceDTO { IOAddress = device.IOAddress, Type = device.Type.ToString(), Value = device.Value });
            }
            return devices;
        }
    }
}
