using Microsoft.EntityFrameworkCore;
using scada_back.Database;
using scada_back.DTOs;
using scada_back.Models;

namespace scada_back.Repositories
{
    public class DeviceRepository
    {
        private DatabaseContext dbContext;

        public DeviceRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddDevice(Device device)
        {
            this.dbContext.Add(device);
            dbContext.SaveChanges();
        }

        public Device GetByIOAddress(string iOAddress)
        {
            return dbContext.Devices.FirstOrDefault(t => t.IOAddress == iOAddress);
        }

        public void UpdateValue(DeviceDTO deviceDTO)
        {
            Device device = GetByIOAddress(deviceDTO.IOAddress);
            device.Value = deviceDTO.Value;
            dbContext.Entry(device).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public List<Device> GetAllDevices()
        {
            return dbContext.Devices.ToList();
        }
    }
}
